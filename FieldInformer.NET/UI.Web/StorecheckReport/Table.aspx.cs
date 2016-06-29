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
using FI.BusinessObjects;

namespace FI.UI.Web.StorecheckReport
{
	/// <summary>
	/// Summary description for Design.
	/// </summary>
	public class Table : PageBase
	{
		protected System.Web.UI.WebControls.Button btnUpdate;

		protected FI.UI.Web.Controls.Tabs.TabView _tabView;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Table ErrTable;
		protected System.Web.UI.HtmlControls.HtmlTableCell cellContents;

		protected FI.UI.Web.Controls.FIDataTableGrid _gr;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected string _contentType="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			_tabView=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TvC");
			LoadContents();
			LoadGrid();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			this.CreateReportTabs(_tabView, _contentType);
			base.Render (writer);
		}

		private void LoadContents()
		{
			try
			{
				if(Request.QueryString["content"]!=null)
					Session[this.ToString() + ":ContentType"]=Request.QueryString["content"];
			}
			catch
			{
				//do nothing
			}

			if(Session[this.ToString()  + ":ContentType"]==null)
				throw new Exception("ContentType not defined");
			else
				_contentType=(string)Session[this.ToString()  + ":ContentType"];
		}


		private void LoadGrid()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.ID="ProductsGrid";
			_gr.DefaultPageSize=15;
			_gr.DataSourceDelegate=new FI.UI.Web.Controls.FIDataTableGrid.GridDataSourceDelegate(GetReportPage);
			//_gr.PrimaryKeyColumnArray=new string[] {"prodsern"};
			_gr.ColumnNameArray=new string[] {"SALMNAME", "CCHNAME" , "CHNNAME" , "COMNAME" , "COMCITY" , "COMPCODE" , "COMADDR" , "DELDATE"};
			_gr.ColumnCaptionArray=new string[] {"Salesman" , "Centr. Chain" ,  "Chain" , "Store" , "City", "Post. Code" , "Address" , "Last Transact."};
			_gr.ColumnWidthArray=new int[] {150 , 150 , 150 , 150 , 100 , 75 , 150 , 75 };
			_gr.EnableCheckBoxes=false;
			_gr.EnablePages=true;
			_gr.FilterChanged+=new EventHandler(_gr_FilterChanged);

			// assign report filter
			foreach(string colName in _gr.ColumnNameArray)
			{
				string val=_report.GetFilterItem(colName);
				if(val!=null)
					_gr.Filter[colName]=val;
				else
					_gr.Filter.Remove(colName);
			}

			this.cellContents.Controls.Add(_gr);
		}

		private FI.Common.Data.FIDataTable GetReportPage(int CurrentPage, int PageSize, string FilterExpression, string SortExpression)
		{
			int startIndex=(CurrentPage-1)*PageSize;
			FI.BusinessObjects.StorecheckReport.ReportPageTypeEnum pageType=(FI.BusinessObjects.StorecheckReport.ReportPageTypeEnum)Enum.Parse(typeof(FI.BusinessObjects.StorecheckReport.ReportPageTypeEnum) , this._contentType , true);

			if(_report.State!=Report.StateEnum.Executed)
				_report.Execute();

			return _report.GetReportPage(pageType, startIndex, PageSize , FilterExpression , SortExpression);
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
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../ReportList.aspx?content=Save" , true);
		}

		private void _gr_FilterChanged(object sender, EventArgs e)
		{
			foreach(string colName in _gr.ColumnNameArray)
				_report.SetFilterItem(colName , _gr.Filter[colName] );

			//_report.SaveState(); don't want, because it'll discard cache
		}
	}
}
