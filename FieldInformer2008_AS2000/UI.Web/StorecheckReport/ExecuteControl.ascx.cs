namespace FI.UI.Web.StorecheckReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ExecuteControl.
	/// </summary>
	public partial class ExecuteControl : System.Web.UI.UserControl
	{

		public FI.BusinessObjects.StorecheckReport _report;

		protected void Page_Load(object sender, System.EventArgs e)
		{		
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

		}
		#endregion

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			if(_report.UndoStateCount<=0)
				btnUndo.Visible=false;

			if(_report.RedoStateCount<=0)
				btnRedo.Visible=false;

			base.Render (writer);
		}



		protected void btnUndo_Click(object sender, System.EventArgs e)
		{
			_report.Undo();
		}

		protected void btnRedo_Click(object sender, System.EventArgs e)
		{
			_report.Redo();
		}
	}
}
