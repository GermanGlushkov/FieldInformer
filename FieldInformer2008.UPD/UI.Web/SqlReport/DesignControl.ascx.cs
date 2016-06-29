namespace FI.UI.Web.SqlReport
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
	public partial class DesignControl : System.Web.UI.UserControl
	{
		
		protected FI.UI.Web.SqlReport.ExecuteControl _execControl;
		protected FI.UI.Web.SqlReport.ReportPropertiesControl _propControl;


        public FI.BusinessObjects.User _user;
        public FI.BusinessObjects.CustomSqlReport _report;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			_execControl=(FI.UI.Web.SqlReport.ExecuteControl)this.FindControl("ExC");
			_propControl=(FI.UI.Web.SqlReport.ReportPropertiesControl)this.FindControl("RPrC");

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
			this.txtSql.Text=_report.Sql;
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


		protected void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../ReportList.aspx?content=Save" , true);
		}

		protected void btnUpdateXsl_Click(object sender, System.EventArgs e)
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

		}
		#endregion

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				this._propControl.UpdateReportHeader();
				_report.SaveHeader();
								
				_report.Sql=this.txtSql.Text;
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}
	}
}
