namespace FI.UI.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ReportDistributionLogControl.
	/// </summary>
	public class ReportDistributionLogControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Panel ReportPanel;
		protected System.Web.UI.WebControls.Panel DistributionLogPanel;
		protected System.Web.UI.WebControls.Button BackButton;


		protected internal FI.BusinessObjects.User _user;
		protected internal FI.BusinessObjects.Report _reportProxy;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;
		private FI.UI.Web.Controls.FIDataTableGrid _gr;


		private void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
			LoadDistributionLogPanel();
		}


		private void LoadParameters()
		{
			decimal reportId=-1;
			int reportType=-1;

			try
			{
				reportId=decimal.Parse(Request.QueryString["rptid"]);
				reportType=int.Parse(Request.QueryString["rpttype"]);

				Session[this.UniqueID + ":ReportId"]=reportId;
				Session[this.UniqueID + ":ReportType"]=reportType;
			}
			catch
			{
				//do nothing
			}
			
			reportId=(decimal)Session[this.UniqueID + ":ReportId"];
			reportType=(int)Session[this.UniqueID + ":ReportType"];
			_reportProxy=_user.ReportSystem.GetReport(reportId , _user.ReportSystem.GetReportType(reportType) , false);
		}

		private void LoadReportPanel()
		{
			// load table
			_reportProxy.LoadHeader();
			FI.Common.Data.FIDataTable rptTable=new FI.Common.Data.FIDataTable();
			rptTable.Columns.Add("name" , typeof(string));
			rptTable.Columns.Add("description" , typeof(string));
			rptTable.Rows.Add(new object[] {_reportProxy.Name , _reportProxy.Description});

			//loading grid control
			_rptGr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
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


		private void LoadDistributionLogPanel()
		{

			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_gr.DefaultPageSize=15;
			_gr.DataSourceDelegate=new FI.UI.Web.Controls.FIDataTableGrid.GridDataSourceDelegate(GetDistributionLogPage);
			_gr.PrimaryKeyColumnArray=new string[] {"log_id"};
			_gr.ColumnNameArray=new string[] {"timestamp", "status" , "duration", "isfromcache", "message" , "contact_name" , "contact_email"};
			_gr.ColumnCaptionArray=new string[] {"Timestamp", "Status" , "Duration(sec)", "FromCache", "Message" , "Contact Name" , "Contact Email"};
			_gr.ColumnWidthArray=new int[] {150, 150 , 80, 80, 150 , 150 , 150};
			_gr.EnableCheckBoxes=false;
			_gr.EnablePages=true;			
			DistributionLogPanel.Controls.Add(_gr);
		}




		private FI.Common.Data.FIDataTable GetDistributionLogPage(int CurrentPage, int PageSize, string FilterExpression, string SortExpression)
		{
			if(SortExpression==null || SortExpression==string.Empty)
				SortExpression="timestamp desc, log_id desc";
			return _user.DistributionSystem.GetDistributionLogPage(_reportProxy , CurrentPage, PageSize, FilterExpression, SortExpression);
		}


		private void BackButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("ReportList.aspx?content=Dispatch" , false);
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
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
