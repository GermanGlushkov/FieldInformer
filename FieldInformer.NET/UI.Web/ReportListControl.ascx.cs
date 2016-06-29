namespace FI.UI.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;
	using System.Collections;

	public class ReportListControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Button btnNew;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnCopy;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnExport;
		protected System.Web.UI.WebControls.Button btnShare;
		protected System.Web.UI.WebControls.Button btnDispatch;
		protected System.Web.UI.WebControls.Panel ReportPanel;

		protected internal int _reportsType;
		protected internal FI.BusinessObjects.User _user;
		protected internal ReportGridControl _gr;

		private void Page_Load(object sender, System.EventArgs e)
		{
			LoadReportPanel();
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
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
			this.btnDispatch.Click += new System.EventHandler(this.btnDispatch_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		private void LoadReportPanel()
		{

			//loading grid control			
			_gr = (ReportGridControl)Page.LoadControl("ReportGridControl.ascx");
			_gr._reportsType=_reportsType;
			_gr.DefaultPageSize=20;
			_gr.InMemory=true;
			_gr.DataSource=GetReportHeaders();
			_gr.EnableMultipleSelection=false;
			_gr.PrimaryKeyColumnArray=new string[] {"id"};
			_gr.ColumnNameArray=new string[] {"sharing_status", "name" , "description" , "owner_name" , "timestamp"};
			_gr.ColumnCaptionArray=new string[] {" ", "Name" , "Description" , "Owner", "Timestamp" };
			_gr.ColumnWidthArray=new int[] {5, 150 , 350 , 100, 85};
			_gr.EnableCheckBoxes=true;
			_gr.EnablePages=true;
			ReportPanel.Controls.Add(_gr);
			
		}


		public ArrayList SelectedPrimaryKeys
		{
			get{return _gr.SelectedPrimaryKeys;}
		}


		public static ArrayList GetSelectedPrimaryKeys(System.Web.HttpRequest Request , string ControlUniqueId)
		{
			return FI.UI.Web.Controls.FIDataTableGrid.GetSelectedPrimaryKeys(Request, ControlUniqueId);
		}
	

		private FI.Common.Data.FIDataTable GetReportHeaders()
		{
			return _user.ReportSystem.GetReportHeaders(_user.ReportSystem.GetReportType(_reportsType));
		}


		private void btnNew_Click(object sender, System.EventArgs e)
		{
			Report rpt=_user.ReportSystem.NewReport(_user.ReportSystem.GetReportType(_reportsType));
			Response.Redirect("ReportList.aspx?content=Load&action=Edit&rptid=" + rpt.ID.ToString() + "&rpttype=" +  this._reportsType , true);
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			ArrayList ids=_gr.SelectedPrimaryKeys;
			if(ids!=null && ids.Count>0)
			{
				string [] key=(string[])ids[0];
				Response.Redirect("ReportList.aspx?content=Load&action=Edit&rptid=" + key[0] + "&rpttype=" +  this._reportsType , true);
				return;
			}
		}

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			ArrayList ids=_gr.SelectedPrimaryKeys;
			if(ids!=null && ids.Count>0)
			{
				string [] key=(string[])ids[0];
				Response.Redirect("ReportList.aspx?content=Copy&rptid=" + key[0] + "&rpttype=" +  this._reportsType , true);
				return;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			ArrayList ids=_gr.SelectedPrimaryKeys;
			if(ids!=null && ids.Count>0)
			{
				string [] key=(string[])ids[0];
				Response.Redirect("ReportList.aspx?content=Delete&rptid=" + key[0] + "&rpttype=" +  this._reportsType , true);
				return;
			}
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			ArrayList ids=_gr.SelectedPrimaryKeys;
			if(ids!=null && ids.Count>0)
			{
				string [] key=(string[])ids[0];
				Response.Redirect("ReportList.aspx?content=Export&rptid=" + key[0] + "&rpttype=" +  this._reportsType , true);
				return;
			}
		}

		private void btnShare_Click(object sender, System.EventArgs e)
		{
			ArrayList ids=_gr.SelectedPrimaryKeys;
			if(ids!=null && ids.Count>0)
			{
				string [] key=(string[])ids[0];
				Response.Redirect("ReportList.aspx?content=Sharing&rptid=" + key[0] + "&rpttype=" +  this._reportsType , true);
				return;
			}		
		}

		private void btnDispatch_Click(object sender, System.EventArgs e)
		{
			ArrayList ids=_gr.SelectedPrimaryKeys;
			if(ids!=null && ids.Count>0)
			{
				string [] key=(string[])ids[0];
				Response.Redirect("ReportList.aspx?content=Dispatch&rptid=" + key[0] + "&rpttype=" +  this._reportsType , true);
				return;
			}
		}
	}






}
