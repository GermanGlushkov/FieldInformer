using System;
using System.Data;
using System.Collections;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for ReportSystem.
	/// </summary>
	public class ReportSystem:MarshalByRefObject
	{
		protected User _owner=null;
		private SortedList _executingReports=new SortedList();


		public event EventHandler BeforeDeleteReport;





		internal ReportSystem(User Owner)
		{
			_owner=Owner;
		}

		public User Owner
		{
			get { return _owner; }
		}


		public Report GetReport(decimal ID , System.Type ReportType , bool Open)
		{
			Report report=null;

			if(ReportType==typeof(OlapReport))
			{
				report=new OlapReport(ID , this._owner);
			}
			else if(ReportType==typeof(StorecheckReport))
			{
				report=new StorecheckReport(ID , this._owner);
			}
			else if(ReportType==typeof(CustomSqlReport))
			{
				report=new CustomSqlReport(ID , this._owner);
			}
			else if(ReportType==typeof(CustomMdxReport))
			{
				report=new CustomMdxReport(ID , this._owner);
			}
			else
			{
				throw new Exception("ReportType " + ReportType.ToString() + " is not supported");
			}

			if(Open)
				report.Open();

			report.StartExecuteEvent+=new EventHandler(report_StartExecuteEvent);
			report.EndExecuteEvent+=new EventHandler(report_EndExecuteEvent);
			return report;
		}



		public Report NewReport(System.Type ReportType)
		{
			return GetReport(0 , ReportType , false);
		}


		private void report_StartExecuteEvent(object sender, EventArgs e)
		{
			Report report=(Report)sender;
			if(this._executingReports[report.ID]==null)
				this._executingReports.Add(report.ID , report);
		}

		private void report_EndExecuteEvent(object sender, EventArgs e)
		{
			Report report=(Report)sender;
			this._executingReports.Remove(report.ID);
		}


		public void CancelExecutingReports()
		{
			// asyncronous at this moment
			while(this._executingReports.Count>0)
			{
				Report report=(Report)this._executingReports.GetByIndex(0);
				report.CancelExecute(); // it will reduce this._executingReports.Count
			}

			this._executingReports.Clear();
		}

		public void DeleteAll()
		{
			this.DeleteAll(true);
		}

		internal void DeleteAll(bool DenyShared)
		{
			// olap
			System.Type type=typeof(OlapReport);
			FI.Common.Data.FIDataTable table=this.GetReportHeaders(type);
			foreach(System.Data.DataRow row in table.Rows)
			{
				Report rpt=this.GetReport((decimal)row["id"], type , false);
				this.DeleteReport(rpt, DenyShared);
			}

			// sql
			type=typeof(CustomSqlReport);
			table=this.GetReportHeaders(type);
			foreach(System.Data.DataRow row in table.Rows)
			{
				Report rpt=this.GetReport((decimal)row["id"], type , false);
				this.DeleteReport(rpt, DenyShared);
			}

			// mdx
			type=typeof(CustomMdxReport);
			table=this.GetReportHeaders(type);
			foreach(System.Data.DataRow row in table.Rows)
			{
				Report rpt=this.GetReport((decimal)row["id"], type , false);
				this.DeleteReport(rpt, DenyShared);
			}

			// storecheck
			type=typeof(StorecheckReport);
			table=this.GetReportHeaders(type);
			foreach(System.Data.DataRow row in table.Rows)
			{
				Report rpt=this.GetReport((decimal)row["id"], type , false);
				this.DeleteReport(rpt, DenyShared);
			}

		}



		public void DeleteReport(Report report)
		{
			this.DeleteReport(report, true);
		}

		internal void DeleteReport(Report report, bool DenyShared)
		{
			report.Validate(true);

			if(report.State==Report.StateEnum.Executing)
				try
				{
					report.CancelExecute();
				}
				catch
				{
					// do nothing
				}

			if (BeforeDeleteReport != null)
				BeforeDeleteReport(report, EventArgs.Empty);

			DeleteSharedReports(report);
			report._DeleteStates();
			report._Delete(DenyShared);

			report=null;
		}


		
		public void DeleteSharedReports(Report ParentReport)
		{
			
			FI.Common.Data.FIDataTable dataTable=this.GetUsersWithChildReports(ParentReport);
			if(dataTable==null || dataTable.Rows.Count==0)
				return;

			for(int i=0; i<dataTable.Rows.Count; i++)
			{
				DataRow row=dataTable.Rows[i];

				decimal userId=(decimal)row["user_id"];
				decimal reportId=(decimal)row["report_id"];
				int reportType=(int)row["report_type"];

				if(reportId==0)
					continue;

				User user=new User(userId, true);
				Report childReport=user.ReportSystem.GetReport(reportId, user.ReportSystem.GetReportType(reportType) , false);
				user.ReportSystem.DeleteSharedReport(ParentReport, childReport);
			}
			
		}
		



		
		public void DeleteSharedReport(Report parentReport, Report childReport)
		{
			System.Type reportType=childReport.GetType();

			if(parentReport.GetType()!=reportType)
				throw new Exception("Parent and child report type mismatch");

			short maxSurscriberSharing=0;

			// delete child report states
			childReport._DeleteStates();
			

			// ----------------------------------------
			if(reportType==typeof(OlapReport))
			{
				FI.Common.DataAccess.IOlapReportsDA dacObj=DataAccessFactory.Instance.GetOlapReportsDA();
				dacObj.DeleteSharedReport(parentReport.ID , childReport.ID, ref maxSurscriberSharing);
			}
			else if(reportType==typeof(StorecheckReport))
			{
				FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
				dacObj.DeleteSharedReport(parentReport.ID , childReport.ID, ref maxSurscriberSharing);
			}
			else if(reportType==typeof(CustomSqlReport))
			{
				FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
				dacObj.DeleteSharedReport(parentReport.ID , childReport.ID, ref maxSurscriberSharing);
			}
			else if(reportType==typeof(CustomMdxReport))
			{
				FI.Common.DataAccess.ICustomMdxReportsDA dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
				dacObj.DeleteSharedReport(parentReport.ID , childReport.ID, ref maxSurscriberSharing);
			}
			else
				throw new NotSupportedException();
			// ----------------------------------------

			childReport._sharing=Report.SharingEnum.None;
			parentReport._maxSubscriberSharing=(Report.SharingEnum)maxSurscriberSharing;
		}
		



		public decimal CreateAsSharedFrom(Report parentReport , Report.SharingEnum subscriberSharing)
		{

			if(this.Owner==parentReport.Owner)
				throw new Exception("Cannot share to same user");

			parentReport.LoadHeader();

			if(subscriberSharing==Report.SharingEnum.None)
				throw new Exception("Wrong sharing option");

			if(parentReport.SharingStatus!=Report.SharingEnum.None)
				throw new Exception("Shared report cannot be source of other shared report");


			decimal newReportId=0;
			System.Type reportType=parentReport.GetType();
			// ----------------------------------------
			if(reportType==typeof(OlapReport))
			{
				FI.Common.DataAccess.IOlapReportsDA dacObj=DataAccessFactory.Instance.GetOlapReportsDA();
				newReportId=dacObj.CreateSharedReport(parentReport.ID, this.Owner.ID, (int)subscriberSharing );
			}
			else if(reportType==typeof(StorecheckReport))
			{
				FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
				newReportId=dacObj.CreateSharedReport(parentReport.ID , this.Owner.ID, (int)subscriberSharing );
			}
			else if(reportType==typeof(CustomSqlReport))
			{
				FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
				newReportId=dacObj.CreateSharedReport(parentReport.ID , this.Owner.ID, (int)subscriberSharing );
			}
			else if(reportType==typeof(CustomMdxReport))
			{
				FI.Common.DataAccess.ICustomMdxReportsDA dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
				newReportId=dacObj.CreateSharedReport(parentReport.ID , this.Owner.ID, (int)subscriberSharing );
			}
			// ----------------------------------------


			// update max 
			if( ((int)parentReport._maxSubscriberSharing)<((int)subscriberSharing))
				parentReport._maxSubscriberSharing=subscriberSharing;

			return newReportId;
		}




		public int GetReportTypeCode(System.Type ReportType)
		{
			if(ReportType==typeof(OlapReport))
				return 0;
			else if(ReportType==typeof(StorecheckReport))
				return 1;
			else if(ReportType==typeof(CustomSqlReport))
				return 2;
			else if(ReportType==typeof(CustomMdxReport))
				return 3;
			else
			{
				throw new NotSupportedException();
			}
		}


		public System.Type GetReportType(int ReportTypeCode)
		{
			if(ReportTypeCode==0)
				return typeof(OlapReport);
			else if(ReportTypeCode==1)
				return typeof(StorecheckReport);
			else if(ReportTypeCode==2)
				return typeof(CustomSqlReport);
			else if(ReportTypeCode==3)
				return typeof(CustomMdxReport);
			else
			{
				throw new NotSupportedException();
			}
		}




		public FI.Common.Data.FIDataTable GetReportHeaders(System.Type reportType)
		{
            return GetReportHeaders(_owner.ID, reportType);
		}


        public static FI.Common.Data.FIDataTable GetReportHeaders(decimal userId, System.Type reportType)
        {
            FI.Common.Data.FIDataTable table = null;


            if (reportType == typeof(OlapReport))
            {
                FI.Common.DataAccess.IOlapReportsDA dacObj = DataAccessFactory.Instance.GetOlapReportsDA();
                table = dacObj.ReadReportHeaders(userId);
            }
            else if (reportType == typeof(StorecheckReport))
            {
                FI.Common.DataAccess.IStorecheckReportsDA dacObj = DataAccessFactory.Instance.GetStorecheckReportsDA();
                table = dacObj.ReadReportHeaders(userId);
            }
            else if (reportType == typeof(CustomSqlReport))
            {
                FI.Common.DataAccess.ICustomSqlReportsDA dacObj = DataAccessFactory.Instance.GetCustomSqlReportsDA();
                table = dacObj.ReadReportHeaders(userId);
            }
            else if (reportType == typeof(CustomMdxReport))
            {
                FI.Common.DataAccess.ICustomMdxReportsDA dacObj = DataAccessFactory.Instance.GetCustomMdxReportsDA();
                table = dacObj.ReadReportHeaders(userId);
            }
            else
            {
                throw new NotSupportedException();
            }

            return table;
        }



		public FI.Common.Data.FIDataTable GetUsersWithChildReports(Report ParentReport)
		{
			FI.Common.Data.FIDataTable table=null;


			if(ParentReport.GetType()==typeof(OlapReport))
			{
				FI.Common.DataAccess.IOlapReportsDA dacObj=DataAccessFactory.Instance.GetOlapReportsDA();
				table=dacObj.ReadUsersWithChildReports(ParentReport.ID , this.GetReportTypeCode(ParentReport.GetType()));
			}
			else if(ParentReport.GetType()==typeof(StorecheckReport))
			{
				FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
				table=dacObj.ReadUsersWithChildReports(ParentReport.ID , this.GetReportTypeCode(ParentReport.GetType()));
			}
			else if(ParentReport.GetType()==typeof(CustomSqlReport))
			{
				FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
				table=dacObj.ReadUsersWithChildReports(ParentReport.ID , this.GetReportTypeCode(ParentReport.GetType()));
			}
			else if(ParentReport.GetType()==typeof(CustomMdxReport))
			{
				FI.Common.DataAccess.ICustomMdxReportsDA dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
				table=dacObj.ReadUsersWithChildReports(ParentReport.ID , this.GetReportTypeCode(ParentReport.GetType()));
			}
			else
			{
				throw new NotSupportedException();
			}

			return table;
		}


                


	}
}
