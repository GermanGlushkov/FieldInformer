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
using FI.BusinessObjects.Olap;

namespace FI.UI.Web.OlapReport
{

	public partial class Format : OlapPageBase
	{
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected FI.UI.Web.OlapReport.FormatControl _formControl;
		protected FI.UI.Web.Controls.Tabs.TabView _tabView;

		protected string _contentType="";
		protected string _pageScrollId=null;
		protected Controller _contr;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			_tabView=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TvC");
			_formControl=(FI.UI.Web.OlapReport.FormatControl)this.FindControl("ForC");

			_formControl._report=_report;
			_contr=new Controller(_report, this);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			LoadTabs();
			base.Render (writer);
		}



		private void LoadTabs()
		{
			int id=0 , id1=0;

			id=this.CreateRootTabs(_tabView , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Olap_Reports);

			_tabView.AddTab(id , "  List  " , Request.ApplicationPath + "/ReportList.aspx?content=List&rpttype=" + _report.GetTypeCode().ToString() , false , false);



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
					id1=_tabView.AddTab(id , rptName , Request.ApplicationPath + "/ReportList.aspx?content=Load&action=Open&rptid=" + rptId + "&rpttype=" + reportType.ToString() , rptOpen , false);

					if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						_tabView.AddImage(id1,"images/share.gif");
					else if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						_tabView.AddImage(id1,"images/share_change.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						_tabView.AddImage(id1,"images/distr.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						_tabView.AddImage(id1,"images/distr_change.gif");
				}
			}

			_tabView.AddTab(id1 , "  Table  " , Request.ApplicationPath + "/OlapReport/Table.aspx", false , false);
			_tabView.AddTab(id1 , "  Graph  " , Request.ApplicationPath + "/OlapReport/Graph.aspx" , false , false);
			_tabView.AddTab(id1 , "  Design  " , Request.ApplicationPath  + "/OlapReport/Design.aspx" , false , false);
			_tabView.AddTab(id1 , "  Format  " , Request.ApplicationPath  + "/OlapReport/Format.aspx" , true , false);
		}




		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion



	}
}