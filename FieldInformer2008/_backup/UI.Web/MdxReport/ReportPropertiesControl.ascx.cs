namespace FI.UI.Web.MdxReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;

	/// <summary>
	///		Summary description for ReportPropertiesControl.
	/// </summary>
	public class ReportPropertiesControl : System.Web.UI.UserControl
	{

		public FI.BusinessObjects.CustomMdxReport _report;
		protected System.Web.UI.HtmlControls.HtmlInputText txtName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtDescr;

		private void Page_Load(object sender, System.EventArgs e)
		{				
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			//txtName
			txtName.Value=_report.Name;

			//txtDescr
			txtDescr.Value=_report.Description;

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void UpdateReportHeader()
		{			
			_report.Name=txtName.Value;
			_report.Description=txtDescr.Value;
		}

	}
}
