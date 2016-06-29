

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

namespace FI.UI.Web
{
	public partial class ReportList : PageBase
	{

		protected internal FI.UI.Web.ReportListControl _listControl;
		protected internal FI.UI.Web.ReportDistributionControl _dispControl;
		protected internal FI.UI.Web.ReportDistributionLogControl _dispLogControl;
		protected internal FI.UI.Web.ReportExportControl _exportControl;
		protected internal FI.UI.Web.ReportDeleteControl _deleteControl;
		protected internal FI.UI.Web.ReportCopyControl _copyControl;
		protected internal FI.UI.Web.ReportSharingControl _sharingControl;
		protected internal FI.UI.Web.ReportSaveControl _saveControl;

		protected internal int _reportsType=0;
		protected internal string _contentType=null;


		protected override void Render(HtmlTextWriter writer)
		{
			base.Render (writer);
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			//_contr=new Controller(_report, this);
			//ExecuteCommands();
			LoadContents();
			LoadTabs();
		}


		private void LoadTabs()
		{
			int id=0 , id1=0;

			FI.UI.Web.Controls.Tabs.TabView tv=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TabView1");
			

			if(_reportsType==_user.ReportSystem.GetReportTypeCode(typeof(BusinessObjects.OlapReport)))
			{
				id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Olap_Reports);

				tv.AddTab(id , "  List  " , "" , true , false);
			}
			else if(_reportsType==_user.ReportSystem.GetReportTypeCode(typeof(BusinessObjects.StorecheckReport)))
			{
				id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Storecheck_Reports);

				tv.AddTab(id , "  List  " , "" , true , false);
			}
			else if(_reportsType==_user.ReportSystem.GetReportTypeCode(typeof(BusinessObjects.CustomSqlReport)))
			{
				id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Custom_SQL_Reports);

				tv.AddTab(id , "  List  " , "" , true , false);
			}
			else if(_reportsType==_user.ReportSystem.GetReportTypeCode(typeof(BusinessObjects.CustomMdxReport)))
			{
				id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Custom_MDX_Reports);

				tv.AddTab(id , "  List  " , "" , true , false);
			}


			FI.Common.Data.FIDataTable rptTable=_user.ReportSystem.GetReportHeaders(_user.ReportSystem.GetReportType(_reportsType));
			foreach(System.Data.DataRow row in rptTable.Rows)
			{
				decimal rptId=decimal.Parse(row["id"].ToString());
				bool rptSelected=(bool)row["is_selected"];
				string rptName=(string)row["name"];
				FI.BusinessObjects.Report.SharingEnum rptSharingStatus=(FI.BusinessObjects.Report.SharingEnum)int.Parse(row["sharing_status"].ToString());
				FI.BusinessObjects.Report.SharingEnum rptMaxSubscriberSharingStatus=(FI.BusinessObjects.Report.SharingEnum)int.Parse(row["max_subscriber_sharing_status"].ToString());

				if(rptSelected)
				{
					id1=tv.AddTab(id , rptName , Request.ApplicationPath + "/ReportList.aspx?content=Load&action=Open&rptid=" + rptId + "&rpttype=" + _reportsType.ToString() , false , false);

					if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						tv.AddImage(id1,"images/share.gif");
					else if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						tv.AddImage(id1,"images/share_change.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						tv.AddImage(id1,"images/distr.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						tv.AddImage(id1,"images/distr_change.gif");
				}
			}
		}



		private void LoadContents()
		{
			try
			{
				if(Request.QueryString["content"]!=null)
					Session[this.ToString() + ":ContentType"]=Request.QueryString["content"];

				Session[this.ToString() + ":ReportsType"]=int.Parse(Request.QueryString["rpttype"]);
			}
			catch
			{
				//do nothing
			}

			if(Session[this.ToString()  + ":ContentType"]==null)
				_contentType="List";
			else
				_contentType=(string)Session[this.ToString()  + ":ContentType"];


			// reports type
			if(Session[this.ToString()  + ":ReportsType"]==null)
				_reportsType=-1;
			else
				_reportsType=(int)Session[this.ToString()  + ":ReportsType"];




			if(_contentType=="Dispatch")
			{
				_dispControl = (FI.UI.Web.ReportDistributionControl)Page.LoadControl("ReportDistributionControl.ascx");
				_dispControl._user=this._user;
				_dispControl.ID="DispC";
				this.ContentsPanel.Controls.Add(_dispControl);
			}
			else if(_contentType=="DispatchLog")
			{
				_dispLogControl = (FI.UI.Web.ReportDistributionLogControl)Page.LoadControl("ReportDistributionLogControl.ascx");
				_dispLogControl._user=this._user;
				_dispLogControl.ID="DispLogC";
				this.ContentsPanel.Controls.Add(_dispLogControl);
			}
			else if(_contentType=="Export")
			{
				_exportControl = (FI.UI.Web.ReportExportControl)Page.LoadControl("ReportExportControl.ascx");
				_exportControl._user=this._user;
				_exportControl.ID="ExpC";
				this.ContentsPanel.Controls.Add(_exportControl);
			}
			else if(_contentType=="Delete")
			{
				_deleteControl = (FI.UI.Web.ReportDeleteControl)Page.LoadControl("ReportDeleteControl.ascx");
				_deleteControl._user=this._user;
				_deleteControl.ID="DelC";
				this.ContentsPanel.Controls.Add(_deleteControl);
			}
			else if(_contentType=="Copy")
			{
				_copyControl = (FI.UI.Web.ReportCopyControl)Page.LoadControl("ReportCopyControl.ascx");
				_copyControl._user=this._user;
				_copyControl.ID="CopyC";
				this.ContentsPanel.Controls.Add(_copyControl);
			}
			else if(_contentType=="Sharing")
			{
				_sharingControl = (FI.UI.Web.ReportSharingControl)Page.LoadControl("ReportSharingControl.ascx");
				_sharingControl._user=this._user;
				_sharingControl.ID="SharingC";
				this.ContentsPanel.Controls.Add(_sharingControl);
			}
			else if(_contentType=="Save")
			{
				_saveControl = (FI.UI.Web.ReportSaveControl)Page.LoadControl("ReportSaveControl.ascx");
				_saveControl._user=this._user;
				_saveControl.ID="SaveC";
				this.ContentsPanel.Controls.Add(_saveControl);
			}
			else if(_contentType=="Load")
			{
				if(this._reportsType==_user.ReportSystem.GetReportTypeCode(typeof(FI.BusinessObjects.OlapReport)))
				{
                    FI.UI.Web.OlapReport.OlapReportLoadControl loadControl = (FI.UI.Web.OlapReport.OlapReportLoadControl)Page.LoadControl("OlapReport/OlapReportLoadControl.ascx");
					loadControl._user=this._user;
					loadControl.ID="LoadC";
					this.ContentsPanel.Controls.Add(loadControl);
				}
				else if(this._reportsType==_user.ReportSystem.GetReportTypeCode(typeof(FI.BusinessObjects.CustomSqlReport)))
				{
                    FI.UI.Web.SqlReport.SqlReportLoadControl loadControl = (FI.UI.Web.SqlReport.SqlReportLoadControl)Page.LoadControl("SqlReport/SqlReportLoadControl.ascx");
					loadControl._user=this._user;
					loadControl.ID="LoadC";
					this.ContentsPanel.Controls.Add(loadControl);
				}
				else if(this._reportsType==_user.ReportSystem.GetReportTypeCode(typeof(FI.BusinessObjects.CustomMdxReport)))
				{
                    FI.UI.Web.MdxReport.MdxReportLoadControl loadControl = (FI.UI.Web.MdxReport.MdxReportLoadControl)Page.LoadControl("MdxReport/MdxReportLoadControl.ascx");
					loadControl._user=this._user;
					loadControl.ID="LoadC";
					this.ContentsPanel.Controls.Add(loadControl);
				}
				else if(this._reportsType==_user.ReportSystem.GetReportTypeCode(typeof(FI.BusinessObjects.StorecheckReport)))
				{
                    FI.UI.Web.StorecheckReport.StorecheckReportLoadControl loadControl = (FI.UI.Web.StorecheckReport.StorecheckReportLoadControl)Page.LoadControl("StorecheckReport/StorecheckReportLoadControl.ascx");
					loadControl._user=this._user;
					loadControl.ID="LoadC";
					this.ContentsPanel.Controls.Add(loadControl);
				}
				else
				{
					throw new NotSupportedException();
				}
			}
			else
			{
				// load list
				_listControl = (FI.UI.Web.ReportListControl)Page.LoadControl("ReportListControl.ascx");
				_listControl._user=this._user;
				_listControl._reportsType=this._reportsType;
				_listControl.ID="ListC";
				this.ContentsPanel.Controls.Add(_listControl);
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

		}
		#endregion


		
	}
}


