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
	public class ReportSharingControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Panel ReportPanel;
		protected System.Web.UI.WebControls.RadioButton radioNone;
		protected System.Web.UI.WebControls.Button UpdateButton;
		protected System.Web.UI.WebControls.Button BackButton;
		protected System.Web.UI.WebControls.Table ErrTable;

		
		protected internal FI.BusinessObjects.User _user;
		protected FI.BusinessObjects.Report _reportProxy;
		protected decimal _reportId;
		protected int _reportType;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;
		protected System.Web.UI.WebControls.Panel SharingPanel;
		protected System.Web.UI.WebControls.RadioButton radioInherite;
		protected System.Web.UI.WebControls.RadioButton radioSnapshot;
		protected System.Web.UI.WebControls.Button btnDeleteAll;
		private FI.UI.Web.Controls.FIDataTableGrid _gr;


		private void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
			LoadSharingPanel();
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
			_reportProxy=_user.ReportSystem.GetReport(_reportId , _user.ReportSystem.GetReportType(_reportType) , false);
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


		private void LoadSharingPanel()
		{
			SharingPanel.Controls.Clear();

			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_gr.ID="SharingGrid";
			_gr.DefaultPageSize=15;
			_gr.InMemory=true;
			_gr.DataSource=GetUsersWithChildReports();
			_gr.PrimaryKeyColumnArray=new string[] {"user_id" , "report_id"};
			_gr.ColumnNameArray=new string[] {"name" , "sharing_string"};
			_gr.ColumnCaptionArray=new string[] {"User Name" , "Sharing" };
			_gr.ColumnWidthArray=new int[] {200 , 150};
			_gr.EnableMultipleSelection=true;
			_gr.EnablePages=true;
			SharingPanel.Controls.Add(_gr);
		}

		private FI.Common.Data.FIDataTable GetUsersWithChildReports()
		{
			FI.Common.Data.FIDataTable table=_user.ReportSystem.GetUsersWithChildReports(this._reportProxy);
			table.Columns.Add("sharing_string" , typeof(string));
			for(int i=0;i<table.Rows.Count;i++)
			{
				DataRow row=table.Rows[i];
				int shSt=int.Parse(((byte)row["sharing_status"]).ToString());
				Report.SharingEnum sh=(Report.SharingEnum)shSt;
				row["sharing_string"]=sh.ToString();
			}
			return table;
		}




		
		private void UpdateButton_Click(object sender, System.EventArgs e)
		{
			Report.SharingEnum sharing=Report.SharingEnum.None;

			if(radioNone.Checked==true)
				sharing=Report.SharingEnum.None;
			else if(radioSnapshot.Checked==true)
				sharing=Report.SharingEnum.SnapshotSubscriber;
			else if(radioInherite.Checked==true)
				sharing=Report.SharingEnum.InheriteSubscriber;
			else
				return; //none of radios is checked

			System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;

			foreach(string[] keys in pks)
			{
				decimal user_id=decimal.Parse(keys[0]);
				decimal report_id=decimal.Parse(keys[1]);

				try
				{
					User user=_user.GetUser(user_id, true);
					
					if(report_id!=0)
					{
						Report childReport=user.ReportSystem.GetReport(report_id, _reportProxy.GetType() , false);
						user.ReportSystem.DeleteSharedReport(_reportProxy , childReport);
					}

					if(sharing!=Report.SharingEnum.None)
						user.ReportSystem.CreateAsSharedFrom(_reportProxy , sharing);
				}
				catch(Exception exc)
				{
					ShowException(exc);
				}

			}

			LoadSharingPanel();

		}


		
		private void btnDeleteAll_Click(object sender, System.EventArgs e)
		{
			try
			{
				_user.ReportSystem.DeleteSharedReports(_reportProxy);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}

			LoadSharingPanel();
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
			this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


	}
}
