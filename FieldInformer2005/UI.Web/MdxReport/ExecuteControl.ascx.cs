namespace FI.UI.Web.MdxReport
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
	public class ExecuteControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Button btnRefresh;

		public FI.BusinessObjects.CustomMdxReport _report;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Button btnUndo;
		protected System.Web.UI.WebControls.Button btnRedo;

		private void Page_Load(object sender, System.EventArgs e)
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
			this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
			this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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



		private void btnUndo_Click(object sender, System.EventArgs e)
		{
			_report.Undo();
		}

		private void btnRedo_Click(object sender, System.EventArgs e)
		{
			_report.Redo();
		}
	}
}
