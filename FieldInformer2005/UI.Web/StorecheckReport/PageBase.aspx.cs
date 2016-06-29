using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FI.BusinessObjects;

namespace FI.UI.Web.StorecheckReport
{
	/// <summary>
	/// Summary description for PageBase.
	/// </summary>
	public class PageBase : FI.UI.Web.PageBase
	{
		protected internal FI.BusinessObjects.StorecheckReport _report;


		protected override void LoadSession()
		{
			base.LoadSession();

			//debug
			//LoadReport();
			//return;


			
			if(Session["Report"]==null)
				throw new Exception("Session failure : report");

			_report=(FI.BusinessObjects.StorecheckReport)Session["Report"];
			
		}

		public void CreateReportTabs(FI.UI.Web.Controls.Tabs.TabView tv, string contentType)
		{
			int reportTabId=0;
			int rootTabId=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Storecheck_Reports);

			tv.AddTab(rootTabId , "  List  " , Request.ApplicationPath + "/ReportList.aspx?content=List&rpttype=" + _report.GetTypeCode().ToString() , false , false);


			FI.Common.Data.FIDataTable rptTable=_user.ReportSystem.GetReportHeaders(_report.GetType());
			foreach(System.Data.DataRow row in rptTable.Rows)
			{
				decimal rptId=decimal.Parse(row["id"].ToString());
				bool rptSelected=(bool)row["is_selected"];
				bool rptOpen=(_report!=null && rptId==_report.ID?true:false);
				string rptName=(string)row["name"];
				FI.BusinessObjects.Report.SharingEnum rptSharingStatus=(FI.BusinessObjects.Report.SharingEnum)int.Parse(row["sharing_status"].ToString());
				FI.BusinessObjects.Report.SharingEnum rptMaxSubscriberSharingStatus=(FI.BusinessObjects.Report.SharingEnum)int.Parse(row["max_subscriber_sharing_status"].ToString());

				if(rptSelected)
				{
					int reportType=_report.GetTypeCode();
					reportTabId=tv.AddTab(rootTabId , rptName , Request.ApplicationPath + "/ReportList.aspx?content=Load&action=Open&rptid=" + rptId + "&rpttype=" + reportType.ToString() , rptOpen , false);

					if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						tv.AddImage(reportTabId,"images/share.gif");
					else if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						tv.AddImage(reportTabId,"images/share_change.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						tv.AddImage(reportTabId,"images/distr.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						tv.AddImage(reportTabId,"images/distr_change.gif");
				}
			}

			this.AddNotDeliveredTab(tv, reportTabId, (contentType=="NotDelivered"?true:false));
			this.AddNeverDeliveredTab(tv, reportTabId, (contentType=="NeverDelivered"?true:false));
			this.AddDeliveredTab(tv, reportTabId, (contentType=="Delivered"?true:false));
			this.AddDesignTab(tv, reportTabId, (contentType=="" || contentType==null?true:false));
		}

		private int AddNotDeliveredTab(FI.UI.Web.Controls.Tabs.TabView tv, int parentId, bool isActive)
		{
			string capt=null;			
			if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Deliveries)
				capt="  Not Delivered Within " + _report.Days + " Days ";
			else if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Sales)
				capt="  Not Sold Within " + _report.Days + " Days ";
			else
				capt="  No Transactions Within " + _report.Days + " Days ";

			return tv.AddTab(parentId , capt , Request.ApplicationPath + "/StorecheckReport/Table.aspx?content=NotDelivered", isActive , false);
		}

		private int AddDeliveredTab(FI.UI.Web.Controls.Tabs.TabView tv, int parentId, bool isActive)
		{
			string capt=null;			
			if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Deliveries)
				capt="  Delivered Within " + _report.Days + " Days ";
			else if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Sales)
				capt="  Sold Within " + _report.Days + " Days ";
			else
				capt="  Transactions Within " + _report.Days + " Days ";

			return tv.AddTab(parentId , capt , Request.ApplicationPath + "/StorecheckReport/Table.aspx?content=Delivered", isActive , false);
		}

		private int AddNeverDeliveredTab(FI.UI.Web.Controls.Tabs.TabView tv, int parentId, bool isActive)
		{
			string capt=null;			
			if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Deliveries)
				capt="  Never Delivered ";
			else if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Sales)
				capt="  Never Sold ";
			else
				capt=" No Transactions Found ";

			return tv.AddTab(parentId , capt , Request.ApplicationPath + "/StorecheckReport/Table.aspx?content=NeverDelivered", isActive , false);
		}

			
		private int AddDesignTab(FI.UI.Web.Controls.Tabs.TabView tv, int parentId, bool isActive)
		{			
			string capt=null;			
			if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Deliveries)
				capt="  Delivered Within " + _report.Days + " Days ";
			else if(_report.DataSource==FI.BusinessObjects.StorecheckReport.DataSourceEnum.Sales)
				capt="  Sold Within " + _report.Days + " Days ";
			else
				capt="  Transactions Within " + _report.Days + " Days ";

			return tv.AddTab(parentId , "  Design  " , Request.ApplicationPath  + "/StorecheckReport/Design.aspx" , isActive , false);		
		}


		protected override void SaveSession()
		{
			Session["Report"]=_report;
			base.SaveSession();
		}

	}
}
