namespace FI.UI.Web.StorecheckReport
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
		
		protected FI.UI.Web.StorecheckReport.ExecuteControl _execControl;
		protected FI.UI.Web.StorecheckReport.ReportPropertiesControl _propControl;

		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Table ErrTable;

		protected internal FI.BusinessObjects.User _user;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnRemove;
		protected System.Web.UI.HtmlControls.HtmlTableCell cellContents;
		protected internal FI.BusinessObjects.StorecheckReport _report;
		protected System.Web.UI.WebControls.Button btnSave;

		protected FI.UI.Web.Controls.FIDataTableGrid _gr;

		private void Page_Load(object sender, System.EventArgs e)
		{
			_execControl=(FI.UI.Web.StorecheckReport.ExecuteControl)this.FindControl("ExC");
			_propControl=(FI.UI.Web.StorecheckReport.ReportPropertiesControl)this.FindControl("RPrC");

			_propControl._report=_report;
			_execControl._report=_report;

			LoadContents();
			LoadProductsGrid();
		}


		private void LoadContents()
		{
			/*
			this.txtMdx.Text=_report.Mdx;
			this.txtXsl.Text=_report.Xsl;
			*/
		}



		private void LoadProductsGrid()
		{

			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.ID="ProductsGrid";
			_gr.DefaultPageSize=15;
			_gr.DataSourceDelegate=new FI.UI.Web.Controls.FIDataTableGrid.GridDataSourceDelegate(GetSppProductsPage);
			_gr.PrimaryKeyColumnArray=new string[] {"prodsern"};
			_gr.ColumnNameArray=new string[] {"prodname" , "prodsname" , "prodcode" , "proddunc", "prodeanc" , "prodprice", "prodcps", "prodcpu" , "prodsize" , "prodpallet"};
			_gr.ColumnCaptionArray=new string[] {"Name" , "Short Name" ,  "Code" , "DUN Code" , "EAN Code", "Price" , "Cons Pkg Size" , "Units", "Case Size" , "Pallet Size"};
			_gr.ColumnWidthArray=new int[] {200 , 100 , 100 , 100 , 100 , 75 , 75 , 75 , 75 , 75};
			_gr.EnableMultipleSelection=true;
			_gr.EnablePages=true;
			this.cellContents.Controls.Add(_gr);
		}

		private FI.Common.Data.FIDataTable GetSppProductsPage(int CurrentPage, int PageSize, string FilterExpression, string SortExpression)
		{
			int startIndex=(CurrentPage-1)*PageSize;
			return _report.GetSppProductsPage(true , startIndex, PageSize , FilterExpression , SortExpression);
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Design.aspx?content=AddProducts" , true);
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach(string[] keys in _gr.SelectedPrimaryKeys)
				{
					_report.RemoveProductSerNo(keys[0]);
				}

				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				this._propControl.UpdateReport();
				_report.SaveHeader();
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}

	}
}
