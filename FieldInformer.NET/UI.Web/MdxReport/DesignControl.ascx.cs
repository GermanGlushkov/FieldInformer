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
	///		Summary description for DesignControl.
	/// </summary>
	public class DesignControl : System.Web.UI.UserControl
	{
		
		protected FI.UI.Web.MdxReport.ExecuteControl _execControl;
		protected FI.UI.Web.MdxReport.ReportPropertiesControl _propControl;

		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Table ErrTable;
		protected System.Web.UI.WebControls.TextBox txtMdx;
		protected System.Web.UI.WebControls.Button btnUpdateXsl;
		protected System.Web.UI.WebControls.TextBox txtXsl;

		protected internal FI.BusinessObjects.User _user;
		protected System.Web.UI.WebControls.Button btnIMportOlapReport;
		protected System.Web.UI.WebControls.Button btnSave;
		protected internal FI.BusinessObjects.CustomMdxReport _report;

		private void Page_Load(object sender, System.EventArgs e)
		{
			_execControl=(FI.UI.Web.MdxReport.ExecuteControl)this.FindControl("ExC");
			_propControl=(FI.UI.Web.MdxReport.ReportPropertiesControl)this.FindControl("RPrC");

			_propControl._report=_report;
			_execControl._report=_report;
			if(_report.SharingStatus==Report.SharingEnum.InheriteSubscriber || _report.SharingStatus==Report.SharingEnum.SnapshotSubscriber)
				_propControl.Visible=false;
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			LoadContents();
			base.Render (writer);
		}


		private void LoadContents()
		{
			this.txtMdx.Text=_report.Mdx;
			this.txtXsl.Text=_report.Xsl;
		}


		
		public void ShowException(Exception exc)
		{
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(exc);
			ShowMessage(exc.Message);
		}


		private void ShowMessage(string Message)
		{
			this.ErrTable.Rows.Clear();
			System.Web.UI.WebControls.Label lbl=new System.Web.UI.WebControls.Label();
			lbl.CssClass="tbl1_err";
			lbl.Text=Message;
			System.Web.UI.WebControls.TableRow row=new System.Web.UI.WebControls.TableRow();
			System.Web.UI.WebControls.TableCell cell=new System.Web.UI.WebControls.TableCell();
			cell.Controls.Add(lbl);
			row.Cells.Add(cell);
			this.ErrTable.Rows.Add(row);
		}


		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../ReportList.aspx?content=Save" , true);
		}

		private void btnUpdateXsl_Click(object sender, System.EventArgs e)
		{
			try
			{
				_report.Xsl=this.txtXsl.Text;
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.btnIMportOlapReport.Click += new System.EventHandler(this.btnIMportOlapReport_Click);
			this.btnUpdateXsl.Click += new System.EventHandler(this.btnUpdateXsl_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnIMportOlapReport_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Design.aspx?content=ImportOlapReport" , true);
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				this._propControl.UpdateReportHeader();
				_report.SaveHeader();
								
				_report.Mdx=this.txtMdx.Text;
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}
	}
}
