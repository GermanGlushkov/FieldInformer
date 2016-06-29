using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using FI.Common;

namespace FI.UI.Web 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{

		public Global()
		{
			InitializeComponent();
		}	
				
		protected void Application_Start(Object sender, EventArgs e)
		{
			System.Runtime.Remoting.RemotingConfiguration.Configure(System.AppDomain.CurrentDomain.BaseDirectory +  "web.config");
						
			//debug
			//FI.BusinessObjects.BusinessFacade.Instance.ResetDataAccess();
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
		}
		
		protected void Application_Error(Object sender, EventArgs e)
		{
			// get exception message
			Exception exc=Server.GetLastError();
			string msg=LogWriter.Instance.MessageFromException(exc);

			// session info
			msg+="\r\n***** Session info *****";
			for(int i=0;i<Session.Count;i++)
				msg+="\r\n" + Session.Keys[i] + ": " + Session[i].ToString();

			// write
			LogWriter.Instance.WriteEventLogEntry(msg, System.Diagnostics.EventLogEntryType.Error);
		}

		protected void Session_End(Object sender, EventArgs e)
		{
			try
			{
				FI.BusinessObjects.User user=(FI.BusinessObjects.User)Session["User"];
				if(user!=null)
				{
					user.Logout();
				}
			}
			catch
			{
				//do nothing
			}
			
		}

		protected void Application_End(Object sender, EventArgs e)
		{
			FI.BusinessObjects.DistributionManager.Instance.OnSystemRestart();
		}		
		

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			// 
			// Global
			// 

		}
		#endregion

	}
}

