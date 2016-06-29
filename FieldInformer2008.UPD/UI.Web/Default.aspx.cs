

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
	public partial class Default:Page
	{
		protected System.Web.UI.WebControls.Panel ContentsPanel;

		protected FI.BusinessObjects.User _user;
		protected string _message=null;
		protected bool _navigationEnabled=false;
		protected byte _cssStyleNum=FI.Common.AppConfig.DefaultCssStyle; // default
        protected string _flashVarsString = "";

		protected FI.UI.Web.UserPassChangeControl _userPassChangeControl;

		protected override void Render(HtmlTextWriter writer)
		{
			bool userLoggedIn=(_user!=null && _user.IsLoggedIn);

			ShowHeader();
            if (userLoggedIn)
            {
                _cssStyleNum = _user.CssStyle;
                LoadAsLoggedIn();
            }
            else
            {
                _cssStyleNum = FI.Common.AppConfig.DefaultCssStyle;
                LoadAsLoggedOut();
            }


			LoadTabs(userLoggedIn , _navigationEnabled);

			base.Render (writer);
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
//			FI.BusinessObjects.BusinessFacade.Instance.ExecuteAllOlapReports("SALESPP_UNI", 60000, "olap_reports_uni.txt");
//			bool b=false;

//			FI.BusinessObjects.DistributionManager.Instance.TestSendMail();

//			System.Threading.Thread.Sleep(15000);
//			TestLoadRemotingSocketsMethod();
//			TestLoadRemotingSockets();            

			string reqMsg=Request.QueryString["msg"];
			if(reqMsg!=null)
				_message=reqMsg;

			CheckUser();

            // must always be loaded, otherwise button event's won't fire inside it
            _userPassChangeControl = (FI.UI.Web.UserPassChangeControl)Page.LoadControl("UserPassChangeControl.ascx");
            _userPassChangeControl._user = this._user;
            _userPassChangeControl.Visible = true;
            this.cellPassChange.Controls.Add(_userPassChangeControl);

            this.cellContents.Visible = false;
            this.cellPassChange.Visible = false;
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
				FI.Common.DataAccess.IUsersDA dac=FI.BusinessObjects.DataAccessFactory.Instance.GetUsersDA();
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
                    HttpCookie cookie = Response.Cookies["FINFLoginCookie"];
                    if (cookie != null)
                    {
                        cookie.Value = "-1";
                        cookie.Expires = DateTime.Now.AddMonths(-6);
                    }
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
                // check by cookie
                if (LoginByCookie())
                    return true;
                

				if(_message==null && Request.QueryString["action"]=="NewSession") //if no error before
					_message="Session expired or per-session cookies disabled";

				return false;
			}

		}

		private void LoadAsLoggedIn()
		{
			// hide login controls
			txtCompany.Visible=false;
            lblCompany.Visible = false;
            txtLogin.Visible = false;
            lblLogin.Visible = false;
            txtPassword.Visible = false;
            lblPassword.Visible = false;
            chkForce.Visible = false;
            chkRemember.Visible = false;
            btnLogin.Visible = false;
            btnLogout.Visible = true;


			//show change pass contrl if necessary
            if (_user.PasswordExpired)
            {
                this.cellPassChange.Visible = true;
                this.cellContents.Visible = false;
                _navigationEnabled = false;
            }
            else
            {
                this.cellContents.Visible = true;
                this.cellPassChange.Visible = false;
                _navigationEnabled = true;

                DashboardServiceServer.ClearReportsCache();
                _flashVarsString=string.Format("userId={0}&serviceUrl={1}",
                    _user.ID.ToString(),
                    Request.Url.AbsoluteUri.Replace("Default.aspx", "WebServices/DashboardService.aspx")
                    );
                    
            }
		}

        private void LoadAsLoggedOut()
        {
            btnLogin.Visible = true;
            btnLogout.Visible = false;
        }

		private void LoadTabs(bool UserLoggedIn, bool NavigationEnabled)
		{
			FI.UI.Web.Controls.Tabs.TabView tv=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TabView1");
			tv.CssStyleNum=_cssStyleNum;
			PageBase._CreateRootTabs(tv , (_user!=null?_user.Name:"") , UserLoggedIn , NavigationEnabled , PageBase.RootTabsEnum.Home);
		}


		private void ShowHeader()
		{
			
			System.Version version=AssemblyInfoHelper.GetVersion();
			lblVersion.Text=FI.Common.AppConfig.AppName + " " + version.Major + "." + version.Minor + "." + version.Build;

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

		}
		#endregion

		protected void btnLogin_Click(object sender, System.EventArgs e)
		{
            LoginByPassword();
		}

        protected void btnLogout_Click(object sender, System.EventArgs e)
		{
            Server.Transfer("~/Default.aspx?action=Logout");
		}

        private void LoginByPassword()
        {
            try
            {
                _user = new FI.BusinessObjects.User(this.txtCompany.Text, this.txtLogin.Text, this.txtPassword.Text);
                _user.Login(this.chkForce.Checked, Request.UserHostAddress, Session.SessionID);
                Session["User"] = _user;

                // set cookie
                if (this.chkRemember.Checked)
                {
                    HttpCookie cookie = new HttpCookie("FINFLoginCookie");
                    cookie.Value = _user.ID.ToString();
                    cookie.Expires = DateTime.Now.AddMonths(6);
                    Response.Cookies.Add(cookie);
                }

                _message = null;
            }
            catch (Exception exc)
            {
                if (Common.AppConfig.IsDebugMode)
                    Common.LogWriter.Instance.WriteEventLogEntry(exc);

                _message = exc.Message;
                ShowHeader();
            }
        }

        private bool LoginByCookie()
        {
            try
            {
                HttpCookie cookie=Request.Cookies["FINFLoginCookie"];
                if (cookie != null)
                {
                    _user = new FI.BusinessObjects.User(decimal.Parse(cookie.Value), false);
                    _user.Login(true, Request.UserHostAddress, Session.SessionID);
                    Session["User"] = _user;

                    _message = null;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exc)
            {
                if (Common.AppConfig.IsDebugMode)
                    Common.LogWriter.Instance.WriteEventLogEntry(exc);

                _message = exc.Message;
                ShowHeader();
                return false;
            }
        }

        private void Logout()
        {
            Session["User"] = null;
        }
	}
}


