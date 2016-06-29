namespace FI.UI.Web.OlapReport
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
		protected System.Web.UI.WebControls.Button btnExecute;
		protected System.Web.UI.WebControls.Button btnCancel;

		public FI.BusinessObjects.OlapReport _report;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Button btnUndo;
		protected System.Web.UI.WebControls.Button btnRedo;
		private Controller _contr;

		protected internal bool ForceExecute=false;
		private bool _cancelled=false;

		private void Page_Load(object sender, System.EventArgs e)
		{
			_contr=new Controller(_report , this.Page);			
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
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			if(_report.State==FI.BusinessObjects.Report.StateEnum.Open)
			{
				if(ForceExecute && _cancelled==false) // if wasn't cancelled
					_contr.BeginExecute();
				else
				{
					btnCancel.Visible=false;
					btnExecute.Visible=true;
					lblStatus.Text=lblStatus.Text + "Ready";
				}

			}

			// beginExecute might change status , so check status again
			if(_report.State==FI.BusinessObjects.Report.StateEnum.Executed)
			{
				btnExecute.Visible=false;
				btnCancel.Visible=false;
				lblStatus.Text=lblStatus.Text + "Completed";
			}
			else if(_report.State==FI.BusinessObjects.Report.StateEnum.Executing)
			{
				btnExecute.Visible=false;
				lblStatus.Text=lblStatus.Text + "Running...";
			}

			if(_report.UndoStateCount<=0)
				btnUndo.Visible=false;

			if(_report.RedoStateCount<=0)
				btnRedo.Visible=false;

			base.Render (writer);
		}


		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			_contr.CancelExecute();
			_cancelled=true; //this will not force execution this time
		}

		private void btnExecute_Click(object sender, System.EventArgs e)
		{
			if(_report.State==FI.BusinessObjects.Report.StateEnum.Open)
				_contr.BeginExecute();
		}

		private void btnUndo_Click(object sender, System.EventArgs e)
		{
			try
			{
				_contr.Undo();
			}
			catch(Exception exc)
			{
				((PageBase)this.Page).ShowException(exc);
			}
		}

		private void btnRedo_Click(object sender, System.EventArgs e)
		{
			try
			{
				_contr.Redo();
			}
			catch(Exception exc)
			{
				((PageBase)this.Page).ShowException(exc);
			}
		}
	}
}
