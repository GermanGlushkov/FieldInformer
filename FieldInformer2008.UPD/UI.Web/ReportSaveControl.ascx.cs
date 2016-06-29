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
	public partial class ReportSaveControl : System.Web.UI.UserControl
	{


        public FI.BusinessObjects.User _user;
		protected FI.BusinessObjects.Report _report;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
		}

		private void LoadParameters()
		{
			// comes from report page
			_report=(FI.BusinessObjects.Report)Session["Report"];

			// out if saved or not dirty
			if(_report.IsSaved)
			{
				try
				{
					_report.Close(false);
					_report.IsSelected=false;
					_report.SaveHeader();
				}
				catch(Exception exc)
				{
					this.ShowException(exc);
				}
				Server.Transfer("ReportList.aspx?content=List" , false);
			}
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

			// if shared report	, saving is disabled
			if(_report.SharingStatus==Report.SharingEnum.InheriteSubscriber || _report.SharingStatus==Report.SharingEnum.SnapshotSubscriber)			
				this.radioSave.Enabled=false;

			//load values into text fields
			string name=((string)rptTable.Rows[0]["name"]).Trim();
			name=(name.Length>42?name.Substring(0,42):name);
			txtName.Text="Copy of " + name;
			txtDescription.Text=((string)rptTable.Rows[0]["description"]).Trim();
		}


		protected void BackButton_Click(object sender, System.EventArgs e)
		{
			if(_report.GetType()==typeof(FI.BusinessObjects.OlapReport))
				Response.Redirect("OlapReport/Design.aspx" , true);
			else if(_report.GetType()==typeof(FI.BusinessObjects.CustomSqlReport))
				Response.Redirect("SqlReport/Design.aspx" , true);
			else if(_report.GetType()==typeof(FI.BusinessObjects.CustomMdxReport))
				Response.Redirect("MdxReport/Design.aspx" , true);
			else if(_report.GetType()==typeof(FI.BusinessObjects.StorecheckReport))
				Response.Redirect("StorecheckReport/Design.aspx" , true);
			else
			{
				throw new NotSupportedException();
			}
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

		protected void OkButton_Click(object sender, System.EventArgs e)
		{
			try
			{

				if(this.radioSave.Checked)
				{
					_report.Close(true);
					_report.IsSelected=false;
					_report.SaveHeader();
					Server.Transfer("ReportList.aspx?content=List" , false);
				}
				else if(this.radioDiscard.Checked)
				{
					_report.Close(false);
					_report.IsSelected=false;
					_report.SaveHeader();
					Server.Transfer("ReportList.aspx?content=List" , false);
				}
				else if(this.radioSaveAs.Checked)
				{
					// save copy
					_report.Clone(this.txtName.Text , this.txtDescription.Text);

					// discard changes
					_report.Close(false);
					_report.IsSelected=false;
					_report.SaveHeader();
					Server.Transfer("ReportList.aspx?content=List" , false);
				}
				
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}

		}

	}
}
