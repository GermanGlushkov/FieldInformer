using System;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for DistributionSystem.
	/// </summary>
	public class DistributionSystem:MarshalByRefObject
	{
		protected User _owner=null;
		
		public event EventHandler BeforeDeleteDistribution;

		internal DistributionSystem(User Owner)
		{
			_owner=Owner;
			_owner.ContactSystem.BeforeDeleteContact+=new EventHandler(OnBeforeDeleteContact);
			_owner.ReportSystem.BeforeDeleteReport+=new EventHandler(OnBeforeDeleteReport);
		}

		public User Owner
		{
			get { return _owner; }
		}


		public Distribution GetDistribution(decimal ID , bool Fetch)
		{
			Distribution distribution=new Distribution(ID , this.Owner);
			if(Fetch)
				distribution.Fetch();

			return distribution;
		}


		public Distribution NewDistribution(Report report, Contact contact, Report.ExportFormat format)
		{
			return new Distribution(this.Owner , report , contact, format);
		}

		public void DeleteDistribution(Distribution distribution)
		{
			distribution.Validate(true);

			if (BeforeDeleteDistribution != null)
				BeforeDeleteDistribution(distribution, EventArgs.Empty);

			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.DeleteDistribution(distribution.ID);			

			distribution=null;
		}


		public void DeleteDistributions(Contact contact)
		{
			
			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.DeleteDistributionsByContact(_owner.ID, contact.ID);
		}

		public void DeleteDistributions(Report report)
		{
			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.DeleteDistributionsByReport(report.ID , report.GetTypeCode());
		}


		private void OnBeforeDeleteContact(object sender , System.EventArgs e)
		{
			DeleteDistributions((Contact)sender);
		}

		private void OnBeforeDeleteReport(object sender , System.EventArgs e)
		{
			DeleteDistributions((Report)sender);
		}
		





		public FI.Common.Data.FIDataTable GetDistributionsWithContactsPage(Report report, int CurrentPage , int RowCount , string FilterExpression , string SortExpression)
		{
			int StartIndex=(CurrentPage-1)*RowCount;

			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable table=new FI.Common.Data.FIDataTable();

			table=dacObj.ReadDistributionsWithContactsPage(_owner.ID, report.ID , report.GetTypeCode() ,  StartIndex , RowCount , FilterExpression , SortExpression);
			ConvertEnums(ref table);
			return table;
		}


		private void ConvertEnums(ref FI.Common.Data.FIDataTable dt)
		{
			if(dt==null)
				return;

			System.Data.DataColumn col=dt.Columns["Format"];
			if(col==null)
				return;

			// set data
			System.Data.DataColumn newCol=dt.Columns.Add("_" + col.ColumnName, typeof(string));
			foreach(System.Data.DataRow dr in dt.Rows)
				if(dr[col]!=DBNull.Value)
					dr[newCol]=Enum.GetName(typeof(Report.ExportFormat), dr[col]);

			// replace cols
			dt.Columns.Remove(col);
			newCol.ColumnName=col.ColumnName;
		}

		public FI.Common.Data.FIDataTable GetDistributionLogPage(Report report, int CurrentPage , int RowCount , string FilterExpression , string SortExpression)
		{
			int StartIndex=(CurrentPage-1)*RowCount;

			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable table=null;

			table=dacObj.ReadReportDistributionLog(_owner.ID, report.ID , report.GetTypeCode() ,  StartIndex , RowCount , FilterExpression , SortExpression);

			return table;
		}



		public FI.Common.Data.FIDataTable GetDistributionLogPage(int CurrentPage , int RowCount , string FilterExpression , string SortExpression)
		{
			int StartIndex=(CurrentPage-1)*RowCount;

			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable table=null;

			table=dacObj.ReadDistributionLog(_owner.ID,  StartIndex , RowCount , FilterExpression , SortExpression);

			return table;
		}

		public FI.Common.Data.FIDataTable GetDistributionQueuePage(int CurrentPage , int RowCount , string FilterExpression , string SortExpression)
		{
			int StartIndex=(CurrentPage-1)*RowCount;

			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable table=null;

			table=dacObj.ReadDistributionQueue(_owner.CompanyId, StartIndex , RowCount , FilterExpression , SortExpression);

			return table;
		}



		public void EnqueueDistributions(System.DateTime Date)
		{
			FI.Common.Data.FIDataTable table=new FI.Common.Data.FIDataTable();
			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable distrTable=dacObj.ReadDistributions(_owner.ID);

			if(distrTable==null || distrTable.Rows.Count==0)
				return;

			for(int i=0;i<distrTable.Rows.Count;i++)
			{
				Distribution distr=_owner.DistributionSystem.GetDistribution((decimal)distrTable.Rows[i]["DistributionId"] , true);
				if(distr.IsScheduledFor(Date))
				{
					try
					{
						dacObj.EnqueueDistribution(distr.ID, "");
					}
					catch
					{
						// do nothing , exception is logged
					}
				}
			}
		}


		public void EnqueueReportDistributions(decimal reportId, int reportType)
		{
			FI.DataAccess.Distributions dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.EnqueueReportDistributions(reportId, reportType);
		}



	}
}
