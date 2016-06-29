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
	/// <summary>
	/// Summary description for PageBase.
	/// </summary>
	public partial class PageBase : System.Web.UI.Page
	{
		protected HtmlGenericControl pageHead;
		protected System.Web.UI.WebControls.Panel ContentsPanel;
		protected byte _cssStyleNum;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Init += new System.EventHandler(this.PageBase_Init);

		}
		#endregion


		protected void PageBase_Init(object sender, System.EventArgs e)
		{
			//debug
			//FI.BusinessObjects.BusinessFacade.Instance.TestGetSchemaMembersXML();
			//Response.End();

			if(Response.IsClientConnected==false)
				Response.End();

			LoadSession();

		}

		protected override void Render(HtmlTextWriter writer)
		{
			SaveSession();
		    base.Render (writer);
		}


		protected internal FI.BusinessObjects.User _user;

		protected virtual void LoadSession()
		{
			//debug
			/*
			if(Session["User"]==null)
			{
				_user=new FI.BusinessObjects.User("salespp", "spp" , "spp");
				_user.Login(true);
			}
			else
				_user=(FI.BusinessObjects.User)Session["User"];

			return;
			*/

			if(Session["User"]==null)
				Response.Redirect(Request.ApplicationPath + "/Default.aspx?action=NewSession");

			_user=(FI.BusinessObjects.User)Session["User"];
            _cssStyleNum=_user.CssStyle;


			if(_user.IsLoggedIn && _user.CheckSessionValidity()==false)
			{
				_user.Logout();
				Session.Abandon();
				Response.Redirect(Request.ApplicationPath + "/Default.aspx?action=NewSession&msg=Another user forced Your logout");
			}

			if(FI.Common.AppConfig.AuditPageHits)
				_user.AuditPageHit();
			// 
		}


		protected virtual void SaveSession()
		{
			Session["User"]=_user;
		}


		public virtual void ShowException(Exception exc)
		{
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(exc);
			throw exc;
		}






		public enum RootTabsEnum
		{
			Home,
			Olap_Reports,
			Custom_SQL_Reports,
			Custom_MDX_Reports,
			Storecheck_Reports,
			Administrative_Tools
		}
	
		protected int CreateRootTabs(FI.UI.Web.Controls.Tabs.TabView tv, string UserName, bool UserLoggedIn, bool NavigationEnabled, RootTabsEnum ActiveTab)
		{
			int ret=_CreateRootTabs(tv, UserName, UserLoggedIn, NavigationEnabled, ActiveTab);
			tv.CssStyleNum=_user.CssStyle;
			return ret;
		}

		public static int _CreateRootTabs(FI.UI.Web.Controls.Tabs.TabView tv, string UserName, bool UserLoggedIn, bool NavigationEnabled, RootTabsEnum ActiveTab)
		{
			int tabId=0;
			int activeTabId=0;
			string activeLabelStr=ActiveTab.ToString().Replace("_", " ");
            string homeLabel = RootTabsEnum.Home.ToString();
            //tv.WelcomeNote = FI.Common.AppConfig.AppName;
			
            //if(UserLoggedIn)
            //{
            //    //tv.EnableLogoutButton=true;
            //    //tv.LogoutHref="~/Default.aspx?action=Logout";
            //    tv.WelcomeNote = FI.Common.AppConfig.AppName; // +"-[" + UserName + "]";
            //    homeLabel = "Home: " + UserName;
            //}
            //else
            //{
            //    //tv.EnableLogoutButton=false;
            //    tv.WelcomeNote=FI.Common.AppConfig.AppName;
            //}

            if(UserLoggedIn)
                homeLabel = "Home: " + UserName;			

			string label="";
			string href="";

            label = RootTabsEnum.Home.ToString();
			href=(NavigationEnabled? "~/Default.aspx?action=Home" : "");
            tabId = tv.AddTab(0, homeLabel, href, (activeLabelStr == label), false);
			activeTabId=(activeLabelStr==label?tabId:activeTabId);

			label=RootTabsEnum.Olap_Reports.ToString().Replace("_", " ");
			href=(NavigationEnabled? "~/ReportList.aspx?content=List&rpttype=0" : "");
			tabId=tv.AddTab(0 , label , href , (activeLabelStr==label) , false);
			activeTabId=(activeLabelStr==label?tabId:activeTabId);

			if(!FI.Common.AppConfig.HideCustomReports)
			{
				label=RootTabsEnum.Custom_SQL_Reports.ToString().Replace("_", " ");
				href=(NavigationEnabled? "~/ReportList.aspx?content=List&rpttype=2" : "");
				tabId=tv.AddTab(0 , label , href , (activeLabelStr==label) , false);
				activeTabId=(activeLabelStr==label?tabId:activeTabId);

				label=RootTabsEnum.Custom_MDX_Reports.ToString().Replace("_", " ");
				href=(NavigationEnabled? "~/ReportList.aspx?content=List&rpttype=3" : "");
				tabId=tv.AddTab(0 , label , href , (activeLabelStr==label) , false);
				activeTabId=(activeLabelStr==label?tabId:activeTabId);

				label=RootTabsEnum.Storecheck_Reports.ToString().Replace("_", " ");
				href=(NavigationEnabled? "~/ReportList.aspx?content=List&rpttype=1" : "");
				tabId=tv.AddTab(0 , label , href , (activeLabelStr==label) , false);
				activeTabId=(activeLabelStr==label?tabId:activeTabId);
			}

			label=RootTabsEnum.Administrative_Tools.ToString().Replace("_", " ");
			href=(NavigationEnabled? "~/UserList.aspx" : "");
			tabId=tv.AddTab(0 , label , href , (activeLabelStr==label) , false);
			activeTabId=(activeLabelStr==label?tabId:activeTabId);


			return activeTabId;
		}



	}
}
