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

namespace FI.UI.Web.WebServices
{
	/// <summary>
	/// Summary description for DistributionDetails.
	/// </summary>
	public partial class DistributionDetails : System.Web.UI.Page
	{
		
		protected internal FI.BusinessObjects.User _userProxy;
		protected internal FI.BusinessObjects.Report _reportProxy;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;
		private FI.UI.Web.Controls.FIDataTableGrid _gr;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
			LoadDistributionLogPanel();
		}

		private void LoadParameters()
		{
			decimal userId=-1;
			decimal reportId=-1;
			int reportType=-1;

			try
			{
				userId=decimal.Parse(Request.QueryString["userId"]);
				reportId=decimal.Parse(Request.QueryString["rptid"]);
				reportType=int.Parse(Request.QueryString["rptType"]);

				Session[this.UniqueID + ":UserId"]=userId;
				Session[this.UniqueID + ":ReportId"]=reportId;
				Session[this.UniqueID + ":ReportType"]=reportType;
			}
			catch
			{
				//do nothing
			}
			
			userId=(decimal)Session[this.UniqueID + ":UserId"];
			reportId=(decimal)Session[this.UniqueID + ":ReportId"];
			reportType=(int)Session[this.UniqueID + ":ReportType"];

			_userProxy=new FI.BusinessObjects.User(userId, true);
			_reportProxy=_userProxy.ReportSystem.GetReport(reportId , _userProxy.ReportSystem.GetReportType(reportType) , false);
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
			_rptGr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
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
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.DefaultPageSize=20;
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
			return _userProxy.DistributionSystem.GetDistributionLogPage(_reportProxy , CurrentPage, PageSize, FilterExpression, SortExpression);
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

		protected void BackButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("DistributionManager.aspx");
		}
	}
}
