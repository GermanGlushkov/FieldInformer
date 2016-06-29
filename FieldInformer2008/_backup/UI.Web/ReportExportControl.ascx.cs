namespace FI.UI.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;

	/// <summary>
	///		Summary description for ReportDistributionControl.
	/// </summary>
	public class ReportExportControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Panel ReportPanel;
		protected System.Web.UI.WebControls.Button BackButton;
		protected System.Web.UI.WebControls.Table ErrTable;

		
		protected internal FI.BusinessObjects.User _user;
		protected FI.BusinessObjects.Report _report;
		protected decimal _reportId;
		protected int _reportType;
		protected System.Web.UI.WebControls.RadioButton radioExcel;
		protected System.Web.UI.WebControls.Button ExportButton;
		protected System.Web.UI.WebControls.HyperLink hrefExcel;
		protected System.Web.UI.WebControls.RadioButton radioHtml;
		protected System.Web.UI.WebControls.HyperLink hrefHtml;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;


		private void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
		}

		private void LoadParameters()
		{
			try
			{
				_reportId=decimal.Parse(Request.QueryString["rptid"]);
				_reportType=int.Parse(Request.QueryString["rpttype"]);

				Session[this.UniqueID + ":ReportId"]=_reportId;
				Session[this.UniqueID + ":ReportType"]=_reportType;
			}
			catch
			{
				//do nothing
			}
			
			_reportId=(decimal)Session[this.UniqueID + ":ReportId"];
			_reportType=(int)Session[this.UniqueID + ":ReportType"];
			if(Session[this.UniqueID + ":Report"]!=null)
				_report=(Report)Session[this.UniqueID + ":Report"];
			else
				_report=_user.ReportSystem.GetReport(_reportId , _user.ReportSystem.GetReportType(_reportType) , false);
		}


		private void LoadReportPanel()
		{
			// load table
			_report.LoadHeader();
			FI.Common.Data.FIDataTable rptTable=new FI.Common.Data.FIDataTable();
			rptTable.Columns.Add("name" , typeof(string));
			rptTable.Columns.Add("description" , typeof(string));
			rptTable.Rows.Add(new object[] {_report.Name , _report.Description});

			//loading grid control
			_rptGr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_rptGr.ID="RptGrid";
			_rptGr.InMemory=true;
			_rptGr.DataSource=rptTable;
			_rptGr.ColumnNameArray=new string[] {"name" , "description" };
			_rptGr.ColumnCaptionArray=new string[] {"Report Name" , "Description"};
			_rptGr.ColumnWidthArray=new int[] {200 , 400};
			_rptGr.EnableSort=false;
			_rptGr.EnableFilter=false;
			_rptGr.EnableCheckBoxes=false;
			_rptGr.EnablePages=false;
			ReportPanel.Controls.Add(_rptGr);
		}



		private void BackButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("ReportList.aspx?content=List" , false);
		}




		private void ShowException(Exception exc)
		{
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(exc);

			System.Web.UI.WebControls.Label lbl=new System.Web.UI.WebControls.Label();
			lbl.CssClass="tbl1_err";
			lbl.Text=exc.Message;
			System.Web.UI.WebControls.TableRow row=new System.Web.UI.WebControls.TableRow();
			System.Web.UI.WebControls.TableCell cell=new System.Web.UI.WebControls.TableCell();
			cell.Controls.Add(lbl);
			row.Cells.Add(cell);

			this.ErrTable.Rows.Add(row);
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
			this.ExportButton.Click += new System.EventHandler(this.UpdateButton_Click);
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UpdateButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(_report.State==Report.StateEnum.Closed)
					_report.Open();
				if(_report.State==Report.StateEnum.Open)
					_report.Execute();

				string path="";
				string reportName="";
				if(radioExcel.Checked)
				{
					reportName=_report.GetType().Name + _report.ID.ToString() + ".XLS";
					path=FI.Common.AppConfig.TempDir + @"\" + reportName;
					_report.Export( path, Report.ExportFormat.CSV);
					hrefExcel.Text=reportName;
					hrefExcel.NavigateUrl=Request.ApplicationPath + "/" + FI.Common.AppConfig.TempVirtualDir + "/" + reportName;
					Session["Report"]=_report;
				}
				else if(radioHtml.Checked)
				{
					reportName=_report.GetType().Name + _report.ID.ToString() + ".HTML";
					path=FI.Common.AppConfig.TempDir + @"\" + reportName;
					_report.Export( path , Report.ExportFormat.HTML);
					hrefHtml.Text=reportName;
					hrefHtml.NavigateUrl=Request.ApplicationPath + "/" + FI.Common.AppConfig.TempVirtualDir  + "/" + reportName;
					Session["Report"]=_report;
				}
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}

		}

	}
}
