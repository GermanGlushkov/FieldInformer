namespace FI.UI.Web.StorecheckReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;

	public partial class AddProductsControl : System.Web.UI.UserControl
	{
		
		public FI.BusinessObjects.User _user;
        public FI.BusinessObjects.StorecheckReport _report;

		protected FI.UI.Web.Controls.FIDataTableGrid _gr;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		
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
			return _report.GetSppProductsPage(false , startIndex, PageSize , FilterExpression , SortExpression);
		}


		
		public void ShowException(Exception exc)
		{
			ShowMessage(exc.Message);
		}


		private void ShowMessage(string Message)
		{
			lblError.Text=Message;
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

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach(string[] keys in _gr.SelectedPrimaryKeys)
				{
					_report.AddProductSerNo(keys[0]);
				}
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			_report.SaveState();
			Response.Redirect("Design.aspx?content=Design" , true);
		}

	}
}
