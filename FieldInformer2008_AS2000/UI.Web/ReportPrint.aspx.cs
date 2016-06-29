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
	/// Summary description for ReportPrint.
	/// </summary>
	public partial class ReportPrint : PageBase
	{

		protected override void Render(HtmlTextWriter writer)
		{
			Report report=null;
			try
			{
				report=((Report)Session["Report"]);
			}
			catch(Exception exc)
			{
				//do nothing
				if(Common.AppConfig.IsDebugMode)
					Common.LogWriter.Instance.WriteEventLogEntry(exc);
				Response.Write("Error getting report from session");
			}
			
			try
			{
				Response.Write(report.Export(Report.ExportFormat.HTML));
			}
			catch(Exception exc)
			{
				//do nothing
				if(Common.AppConfig.IsDebugMode)
					Common.LogWriter.Instance.WriteEventLogEntry(exc);
				Response.Write(exc.Message);
			}

			base.Render (writer);
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
