namespace FI.UI.Web.MdxReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using FI.BusinessObjects;

	/// <summary>
	///		Summary description for ReportDistributionControl.
	/// </summary>
	public partial class ImportOlapReportControl : System.Web.UI.UserControl
	{


        public FI.BusinessObjects.User _user;
        public FI.BusinessObjects.CustomMdxReport _report;

		private FI.UI.Web.Controls.FIDataTableGrid _gr;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
		}

		private void LoadParameters()
		{
			
		}


		private void LoadReportPanel()
		{

			//loading grid control
			
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.DefaultPageSize=20;
			_gr.InMemory=true;
			_gr.DataSource=_user.ReportSystem.GetReportHeaders(typeof(FI.BusinessObjects.OlapReport));
			_gr.EnableMultipleSelection=false;
			_gr.PrimaryKeyColumnArray=new string[] {"id"};
			_gr.ColumnNameArray=new string[] {"name" , "description" , "owner_name" , "timestamp"};
			_gr.ColumnCaptionArray=new string[] {"Name" , "Description" , "Owner", "Timestamp" };
			_gr.ColumnWidthArray=new int[] {150 , 350 , 100, 85};
			_gr.EnableCheckBoxes=true;
			_gr.EnablePages=true;
			ReportPanel.Controls.Add(_gr);
			
		}



		protected void BackButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("Design.aspx?content=Design" , false);
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



		protected void btnImport_Click(object sender, System.EventArgs e)
		{
			try
			{
				ArrayList ids=_gr.SelectedPrimaryKeys;
				if(ids!=null && ids.Count>0)
				{
					string [] key=(string[])ids[0];
					
					// import mdx
					FI.BusinessObjects.OlapReport olapReport=(FI.BusinessObjects.OlapReport )_user.ReportSystem.GetReport(decimal.Parse(key[0]) , typeof(FI.BusinessObjects.OlapReport) , true);
					_report.Mdx=olapReport.BuildMdx();
				}

				Server.Transfer("Design.aspx?content=Design" , false);
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}

	}
}
