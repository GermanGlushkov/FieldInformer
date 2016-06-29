

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
	public class Default:Page
	{
		protected System.Web.UI.WebControls.TextBox txtCompany;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtLogin;
		protected System.Web.UI.WebControls.CheckBox chkForce;
		protected System.Web.UI.WebControls.Button btnLogin;
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Panel ContentsPanel;

		protected FI.BusinessObjects.User _user;
		protected System.Web.UI.WebControls.Label lblVersion;
		protected System.Web.UI.WebControls.Label lblCompany;
		protected System.Web.UI.WebControls.Label lblLogin;
		protected System.Web.UI.WebControls.Label lblPassword;
		protected System.Web.UI.HtmlControls.HtmlTableCell cellContents;
		protected string _message=null;
		protected bool _navigationEnabled=false;
		protected byte _cssStyleNum=3; // default 3

		protected FI.UI.Web.UserPassChangeControl _userPassChangeControl;

		protected override void Render(HtmlTextWriter writer)
		{
			bool userLoggedIn=(_user!=null && _user.IsLoggedIn);

			ShowHeader();
			if(userLoggedIn)
			{				
				_cssStyleNum=_user.CssStyle;
				LoadAsLoggedIn();
			}

			LoadTabs(userLoggedIn , _navigationEnabled);

			base.Render (writer);
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
//			FI.BusinessObjects.DistributionManager.Instance.TestSendMail();

//			System.Threading.Thread.Sleep(15000);
//			TestLoadRemotingSocketsMethod();
//			TestLoadRemotingSockets();

			string reqMsg=Request.QueryString["msg"];
			if(reqMsg!=null)
				_message=reqMsg;

			CheckUser();

			_userPassChangeControl=(FI.UI.Web.UserPassChangeControl)Page.LoadControl("UserPassChangeControl.ascx");
			_userPassChangeControl.Visible=false;
			_userPassChangeControl._user=this._user;
			this.cellContents.Controls.Add(_userPassChangeControl);
		}


		private void TestLoadRemotingSockets()
		{
			int n=0;
			for(int i=0;i<100;i++)
			{
				System.Threading.ThreadStart ts=new System.Threading.ThreadStart(this.TestLoadRemotingSocketsMethod);
				System.Threading.Thread t=new System.Threading.Thread(ts);
				t.Start();
			}
		}

		private void TestLoadRemotingSocketsMethod()
		{
			try
			{
				FI.DataAccess.Users dac=FI.BusinessObjects.DataAccessFactory.Instance.GetUsersDA();
				dac.ReadUser((decimal)1);
				
				FI.Common.LogWriter.Instance.WriteEventLogEntry("TestLoadRemotingSocketsMethod completed");
			}
			catch(Exception exc)
			{
				FI.Common.LogWriter.Instance.WriteEventLogEntry(exc);
			}
		}


		private bool CheckUser()
		{
			
			if(Session["User"]!=null)
			{
				if(Request.QueryString["action"]=="Logout")
				{
					_user=(User)Session["User"];
					_user.Logout();

					Session.Abandon();
					Response.Redirect("Default.aspx" , true);
					return false; //it won't actually return
				}
				else
				{
					_user=(User)Session["User"];
					if(_user.IsProxy==false && _user.IsNew==false)
					{
						// check like in PageBase
						if(_user.IsLoggedIn && _user.CheckSessionValidity()==false)
						{
							_user.Logout();
							if(_message==null) //if no error before
							{
								_message="Another user logged you out";
								return false;
							}
						}

						return true;
					}
					else
						return false;
				}
			}
			else
			{
				if(_message==null && Request.QueryString["action"]=="NewSession") //if no error before
					_message="Session expired or per-session cookies disabled";

				return false;
			}

		}

		private void LoadAsLoggedIn()
		{
			// hide login controls
			txtCompany.Visible=false;
			lblCompany.Visible=false;
			txtLogin.Visible=false;
			lblLogin.Visible=false;
			txtPassword.Visible=false;
			lblPassword.Visible=false;
			chkForce.Visible=false;
			btnLogin.Visible=false;

			//show change pass contrl if necessary
			if(_user.PasswordExpired)
			{
				_userPassChangeControl.Visible=true;
				_navigationEnabled=false;
			}
			else
				_navigationEnabled=true;
		}

		private void LoadTabs(bool UserLoggedIn, bool NavigationEnabled)
		{
			FI.UI.Web.Controls.Tabs.TabView tv=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TabView1");
			tv.CssStyleNum=_cssStyleNum;
			PageBase._CreateRootTabs(tv , (_user!=null?_user.Name:"") , UserLoggedIn , NavigationEnabled , PageBase.RootTabsEnum.Home);
		}


		private void ShowHeader()
		{
			
			System.Version version=System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			lblVersion.Text=FI.Common.AppConfig.AppName + " " + version.Major + "." + version.Minor + "." + version.Build + ".NET";

			lblHeader.Text=_message;
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
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			try
			{
				_user=new FI.BusinessObjects.User(this.txtCompany.Text , this.txtLogin.Text , this.txtPassword.Text);
				_user.Login(this.chkForce.Checked, Request.UserHostAddress , Session.SessionID);
				Session["User"]=_user;
				_message=null;
			}
			catch(Exception exc)
			{
				if(Common.AppConfig.IsDebugMode)
					Common.LogWriter.Instance.WriteEventLogEntry(exc);

				_message=exc.Message;
				ShowHeader();
			}
		}


		
	}
}


