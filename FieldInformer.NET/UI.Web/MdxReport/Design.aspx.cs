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

namespace FI.UI.Web.MdxReport
{
	/// <summary>
	/// Summary description for Design.
	/// </summary>
	public class Design : PageBase
	{
		protected System.Web.UI.WebControls.Button btnUpdate;

		protected FI.UI.Web.Controls.Tabs.TabView _tabView;
		protected System.Web.UI.HtmlControls.HtmlTableCell contentsCell;

		protected string _contentType="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			_tabView=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TvC");
			LoadContents();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			LoadTabs();
			base.Render (writer);
		}



		private void LoadTabs()
		{
			int id=0 , id1=0;

			id=this.CreateRootTabs(_tabView , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Custom_MDX_Reports);

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

			_tabView.AddTab(id1 , "  Table  " , Request.ApplicationPath + "/MdxReport/Table.aspx", false , false);
			_tabView.AddTab(id1 , "  Design  " , Request.ApplicationPath  + "/MdxReport/Design.aspx" , true , false);
		}



		private void LoadContents()
		{
			try
			{
				if(Request.QueryString["content"]!=null)
					Session[this.ToString() + ":ContentType"]=Request.QueryString["content"];
			}
			catch
			{
				//do nothing
			}

			if(Session[this.ToString()  + ":ContentType"]==null)
				_contentType="List";
			else
				_contentType=(string)Session[this.ToString()  + ":ContentType"];




			if(_contentType=="ImportOlapReport")
			{
				FI.UI.Web.MdxReport.ImportOlapReportControl control = (FI.UI.Web.MdxReport.ImportOlapReportControl)Page.LoadControl("ImportOlapReportControl.ascx");
				control._user=this._user;
				control._report=this._report;
				control.ID="ImpOlRC";
				this.contentsCell.Controls.Add(control);
			}
			else
			{
				FI.UI.Web.MdxReport.DesignControl control = (FI.UI.Web.MdxReport.DesignControl)Page.LoadControl("DesignControl.ascx");
				control._user=this._user;
				control._report=this._report;
				control.ID="DesignC";
				this.contentsCell.Controls.Add(control);
			}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



	}
}
