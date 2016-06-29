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
	///		Summary description for ReportDistributionControl.
	/// </summary>
	public partial class ReportLoadControl : System.Web.UI.UserControl
	{


        public FI.BusinessObjects.User _user;
        public FI.BusinessObjects.CustomMdxReport _report;
        public string _action;
		//protected bool _wasSelected=false;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadContents();
			LoadReport();
			LoadReportPanel();
			bool parametersExist=LoadParametersTable();

			
			if(_report.IsSelected || parametersExist==false)
				this.RedirectToReport();
		}





		private void LoadReport()
		{
			if(Request.QueryString["rptid"]!=null && Request.QueryString["rptid"]!="")
			{
				decimal rptId=decimal.Parse((string)Request.QueryString["rptid"]);

				if(Session["Report"]==null)
					LoadReportFromId(rptId);
				else
					LoadReportFromSession(rptId);
			}
			else if(Session["Report"]==null)
			{
				throw new Exception("Cannot load report from session or QueryString");
				//LoadReportFromId(92);
			}
			else
				LoadReportFromSession(-1);
		}


		private void LoadReportFromSession(decimal Id)
		{
			try
			{
				_report=(FI.BusinessObjects.CustomMdxReport)Session["Report"];
			}
			catch(InvalidCastException exc) 
			{
				if(Id==-1)
					throw new Exception("Cannot load report from session");

				LoadReportFromId(Id);
			}

			if(_report.ID!=Id || _report.State==Report.StateEnum.Closed)
			{
				LoadReportFromId(Id);
			}
		}

		
		private void LoadReportFromId(decimal Id)
		{
			_report=(FI.BusinessObjects.CustomMdxReport)_user.ReportSystem.GetReport(Id , typeof(FI.BusinessObjects.CustomMdxReport) , true);
			Session["Report"]=_report;
		}



		private void LoadContents()
		{
			try
			{				
				if(Request.QueryString["action"]!=null)
					Session[this.ToString() + ":Action"]=Request.QueryString["action"];
			}
			catch
			{
				//do nothing
			}

			_action=(string)Session[this.ToString()  + ":Action"];
		}



		private void RedirectToReport()
		{
			if(_report.IsSelected==false)
			{
				_report.IsSelected=true;
				_report.SaveHeader();
			}

			Session["Report"]=_report;
			
			// edit or run
			if(_action=="Open" && FI.Common.AppConfig.ReportRunOnClick)
				Response.Redirect(Request.ApplicationPath + "/MdxReport/Table.aspx" , true);
			else
				Response.Redirect(Request.ApplicationPath + "/MdxReport/Design.aspx" , true);
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
			_rptGr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl(Request.ApplicationPath + "/Controls/FIDataTableGrid.ascx");
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



		private bool LoadParametersTable()
		{
			bool parametersExist=false;

			return parametersExist;
		}


		private void AddParameterInput()
		{
			HtmlTableRow row=new HtmlTableRow();
			this.paramsTable.Rows.Add(row);		
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

		}
		#endregion

		protected void btnLoad_Click(object sender, System.EventArgs e)
		{
			RedirectToReport();
		}

		protected void BackButton_Click(object sender, System.EventArgs e)
		{
			_report.Close(false);
			string reportId=_user.ReportSystem.GetReportTypeCode(_report.GetType()).ToString();

			_report=null;
			Response.Redirect(Request.ApplicationPath + "/ReportList.aspx?content=List&rpttype=" + reportId);
		}






	}
}
