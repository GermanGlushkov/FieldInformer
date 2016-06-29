using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace FI.UI.Web.WebServices
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	public class PingService : System.Web.Services.WebService
	{
		public PingService()
		{			
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		[WebMethod]
		public void PingOlapSystem(string Mdx , string MailTo)
		{
			FI.BusinessObjects.BusinessFacade facade=FI.BusinessObjects.BusinessFacade.Instance;
			facade.PingOlapSystem(Mdx , MailTo);
		}

		[WebMethod]
		public void ExecuteAllOlapReports(string companyShortName, int millisecondsTimeout)
		{			
			FI.BusinessObjects.BusinessFacade.Instance.ExecuteAllOlapReports(companyShortName, millisecondsTimeout, companyShortName + "_reportexec.log");
		}

	}
}
