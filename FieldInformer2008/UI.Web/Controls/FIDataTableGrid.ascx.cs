namespace FI.UI.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections.Specialized;



	public partial  class FIDataTableGrid : System.Web.UI.UserControl
	{
		public FI.Common.Data.FIDataTable _dataSource;
		public int[] ColumnWidthArray;
		public string[] ColumnNameArray;
		public string[] ColumnCaptionArray;
		public string[] PrimaryKeyColumnArray;
		public bool InMemory=false;
		public bool EnableMultipleSelection=false;
		public bool EnableSort=true;
		public bool EnableFilter=true;
		public bool EnableCheckBoxes=true;
		public bool EnablePages=true;
		public bool EnableFilterAutoWildcard=true;
		public string FilterAutoWildcard="%";
		public int _defaultPageSize=10;
		public int _totalRowCount=-1;
		public int _pageCount=-1;
		private bool _enableUpdatePager=true;

		private bool _sessionVariablesInitialized=false;
		private static string _primaryKeySeparator="¤~";

		private string _sessionItemCurrentPage;
		private string _sessionItemCurrentPageSize;
		private string _sessionItemCurrentFilter;
		private string _sessionItemCurrentSort;

		protected System.Web.UI.HtmlControls.HtmlTableCell cellPager;


		//public event System.Web.UI.WebControls.CommandEventHandler SortButtonClick;
		//public event System.Web.UI.WebControls.CommandEventHandler PagerButtonClick;
		public event EventHandler FilterChanged;
		public event EventHandler SortChanged;


		// NB!! DataSourceDelegate !
		public delegate FI.Common.Data.FIDataTable GridDataSourceDelegate(int CurrentPage, int PageSize, string FilterExpression, string SortExpression);
		public GridDataSourceDelegate DataSourceDelegate=null;


		bool _initialized;
		public void Initialize()
		{
			if(!_initialized)
			{
				InitializeSessionVariables();
				this.ExecuteCommands();
				_initialized=true;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			Initialize();
			base.OnLoad (e);
		}


		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			this.PrepareData();
			this.CreatePager();
			this.CreateTableHeader();
			this.CreateTable();
			this.CreateTableFooter();

			base.Render (writer);
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}




		public void InitializeSessionVariables()
		{
			if(_sessionVariablesInitialized==false)
			{
				_sessionItemCurrentPage=Page.ToString()+":" + this.UniqueID+":CurrentPage";
				_sessionItemCurrentPageSize=Page.ToString()+":" + this.UniqueID+":CurrentPageSize";
				_sessionItemCurrentFilter=Page.ToString()+":" + this.UniqueID+":CurrentFilter";
				_sessionItemCurrentSort=Page.ToString()+":" + this.UniqueID+":CurrentSort";

				if (Session[_sessionItemCurrentPage]==null)
					Session[_sessionItemCurrentPage]=1;

				if (Session[_sessionItemCurrentPageSize]==null)
					Session[_sessionItemCurrentPageSize]=this.DefaultPageSize;

				if (Session[_sessionItemCurrentFilter]==null)
					Session[_sessionItemCurrentFilter]=new NameValueCollection();

				if (Session[_sessionItemCurrentSort]==null)
					Session[_sessionItemCurrentSort]=new NameValueCollection();

				_sessionVariablesInitialized=true;
			}
		}

		public FI.Common.Data.FIDataTable DataSource
		{
			get {return _dataSource;}
			set
			{ _dataSource=value; }
		}



		public int PageCount
		{
			get
			{
				return _pageCount;
			}
		}


		public int TotalRowCount
		{
			get
			{
				return _totalRowCount;
			}
		}

		public int DefaultPageSize
		{
			get {return _defaultPageSize;}
			set
			{
				_defaultPageSize=value;
			}
		}

		public bool EnableUpdatePager
		{
			get {return _enableUpdatePager;}
			set
			{
				_enableUpdatePager=value;
			}
		}

		public int PageSize
		{
			get
			{
				InitializeSessionVariables();
				object o=Session[_sessionItemCurrentPageSize];
				return (o==null ? this.DefaultPageSize : (int)o);
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentPageSize]=value;
			}
		}


		public int CurrentPage
		{
			get
			{
				InitializeSessionVariables();
				return (int)Session[_sessionItemCurrentPage];
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentPage]=value;
			}
		}


		public NameValueCollection Filter
		{
			get
			{
				InitializeSessionVariables();
				return (NameValueCollection)Session[_sessionItemCurrentFilter];
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentFilter]=value;
			}
		}


		public string FilterExpression
		{
			get
			{
				string expr="";
				if(this.ColumnNameArray==null)
					return expr;

				foreach(string col in this.ColumnNameArray)
				{
					string filt=this.Filter[col];
					if(filt==null || filt=="")
						continue;

					if(this.InMemory)
					{
						// DataSet filter expression
						expr=expr + "Convert([" + FI.Common.Data.FIDataTable.EscapeSearchColumn(col) + "] , 'System.String' ) LIKE '" + FI.Common.Data.FIDataTable.EscapeSearchExpression(filt);
						if(this.EnableFilterAutoWildcard)
							expr+= this.FilterAutoWildcard;
						expr+= "'";
					}
					else
					{
						// Sql filter expression
						expr=expr + col + " LIKE '" + filt.Replace("'", "''");
						if(this.EnableFilterAutoWildcard)
							expr+= this.FilterAutoWildcard;
						expr+= "'";
					}
					expr=expr + " AND ";
				}

				//remove last 5 digits
				if(expr.Length>=5)
					expr=expr.Remove(expr.Length-5,5);

				return expr;
			}
		}


		public NameValueCollection Sort
		{
			get
			{
				InitializeSessionVariables();
				return (NameValueCollection)Session[_sessionItemCurrentSort];
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentSort]=value;
			}
		}

		
		public string SortExpression
		{
			get
			{
				string expr="";
				foreach(string col in this.Sort.Keys)
					if(ColumnExists(col))
						expr=expr + col + " " + this.Sort[col] + ", ";

				//remove last 2 digits
				if(expr.Length>=2)
					expr=expr.Remove(expr.Length-2,2);

				return expr;
			}
		}


		private bool ColumnExists(string ColumnName)
		{
			if(this.ColumnNameArray==null)
				return false;

			foreach(string colName in this.ColumnNameArray)
				if(colName==ColumnName)
					return true;

			return false;
		}


		public System.Collections.ArrayList SelectedPrimaryKeys
		{

			get 
			{
				return GetSelectedPrimaryKeys(this.Request , this.UniqueID);

			}
	
		}


		public static System.Collections.ArrayList GetSelectedPrimaryKeys(System.Web.HttpRequest Request , string ControlUniqueId)
		{

			System.Collections.ArrayList selectedPrimaryKeys=new System.Collections.ArrayList();

			for(int i=0 ; i<Request.Form.Count; i++)
			{
				if(Request.Form.Keys[i].StartsWith(ControlUniqueId+":chk_"))
				{
					selectedPrimaryKeys.Add( System.Text.RegularExpressions.Regex.Split(Request.Form[i], _primaryKeySeparator)); 
				}
			}

			return selectedPrimaryKeys;
		}




		private void ExecuteCommands()
		{
			
			try
			{
				for(int i=0;i<Request.Form.Count;i++)
				{
					string key=Request.Form.Keys[i];
					string val=Request.Form[i];

					if(key==this.UniqueID + ":btnUpdatePager" || key==this.UniqueID + ":btnUpdatePagerFooter")
					{
						SetPagesFromRequest();
						this.SetFilterFromRequest();
						break;
					}
					else if(key==this.UniqueID + ":btnPrevPage")
					{
						if(this.CurrentPage!=1)
							this.CurrentPage--;
						break;
					}
					else if(key==this.UniqueID + ":btnNextPage")
					{
						this.CurrentPage++;
						break;
					}
					else if(key.StartsWith(this.UniqueID + ":sort_"))
					{
						this.SetSortFromRequest();
						break;
					}
				}
			}
			catch(Exception exc)
			{
				if(Common.AppConfig.IsDebugMode)
					Common.LogWriter.Instance.WriteEventLogEntry(exc);
				this.ShowException(exc);
			}
		}


		private void SetFilterFromRequest()
		{
			bool filterUpdated=false;
			NameValueCollection filter=new NameValueCollection();

			foreach(string colName in this.ColumnNameArray)
			{
				string colFilt=Request.Form[this.UniqueID+":flt_" + colName];
				if(colFilt=="")
					colFilt=null;

				//check if filter updated, raise event
				string existingFilt=this.Filter[colName];
				if(existingFilt!=colFilt)
					filterUpdated=true;

				if(colFilt!=null)
					filter.Add(colName , colFilt);
			}
			
			this.Filter=filter;
			
			if(filterUpdated)
				if(FilterChanged!=null)
					FilterChanged(this, EventArgs.Empty);
		}


		private void SetSortFromRequest()
		{
			bool sortUpdated=false;
			NameValueCollection sort=new NameValueCollection();

			foreach(string colName in this.ColumnNameArray)
			{
				string existsingSort=this.Sort[colName];
				string newSort=null;

				if(Request.Form[this.UniqueID+":sort_no_" + colName]!=null)
					newSort="ASC";
				else if(Request.Form[this.UniqueID+":sort_asc_" + colName]!=null)
					newSort="DESC";
				else if(Request.Form[this.UniqueID+":sort_desc_" + colName]!=null)
					newSort="ASC";

				//check if sort updated, raise event
				string existingSort=this.Sort[colName];
				if(existingSort!=newSort)
					sortUpdated=true;

				if(newSort!=null)
				{
					sort.Add(colName , newSort);
					break;
				}
			}
			
			this.Sort=sort;

			if(sortUpdated)
				if(SortChanged!=null)
					SortChanged(this, EventArgs.Empty);
		}

		private void SetPagesFromRequest()
		{
			if(!this.EnableUpdatePager)
				return;
			
			string curPage=Request.Form[this.txtCurPage.UniqueID];
			string pageSize=Request.Form[this.txtPageSize.UniqueID];

			this.PageSize=int.Parse(pageSize);
			this.CurrentPage=int.Parse(curPage);
		}


		private void ShowException(Exception exc)
		{
			ShowMessage(exc.Message);
		}

		private void ShowMessage(string Message)
		{
			this.lblError.Text=Message;
		}



		private void PrepareData()
		{
		

			// get data
			GetData();


			// set sort
			if(this.InMemory && this.DataSource!=null)
				this.DataSource.DefaultView.Sort=this.SortExpression;


			//set filter
			if(this.InMemory && this.DataSource!=null)
				DataSource.DefaultView.RowFilter=this.FilterExpression;


			
			// --- correct PageCount value ---

			if(PageSize<=0)
				PageSize=this.DefaultPageSize;

			if(this.InMemory)
			{
				this._totalRowCount=(this.DataSource==null ? 0 : this.DataSource.DefaultView.Count);
				this._pageCount=(int)Math.Ceiling((double)System.Decimal.Divide((decimal)_totalRowCount, (decimal)PageSize));
			}
			else
			{
				if(this.DataSource==null)
				{
					this._totalRowCount=0;
					this._pageCount=0;
				}
				else
				{
					this._totalRowCount=this.DataSource.TotalCount;
					this._pageCount=(int)Math.Ceiling((double)System.Decimal.Divide((decimal)this._totalRowCount , (decimal)PageSize));
				}
			}

			if(_pageCount==0)
				this._pageCount=1;



			// --- correct CurrentPage value ---

			if(this.CurrentPage>this.PageCount)
			{
				this.CurrentPage=1;

				// get data one more time
				GetData();
			}

			//------------------------------------
			

		}

		private void GetData()
		{
			// get data if not in-memory
			if(this.InMemory==false)
			{
				// get datasource using delegate method
				if(this.DataSourceDelegate==null)
					throw new Exception("For not-in-memory data DataSourceDelegate must be assigned");

				this.DataSource=this.DataSourceDelegate(this.CurrentPage , this.PageSize , this.FilterExpression , this.SortExpression);
			}
		}


		private void CreatePager()
		{
			if(this.EnablePages)
			{
				if(this.CurrentPage==1)
					this.btnPrevPage.Enabled=false;
				
				if(this.CurrentPage==this.PageCount)
					this.btnNextPage.Enabled=false;

				this.txtCurPage.Text=this.CurrentPage.ToString();
				this.txtPageSize.Text=this.PageSize.ToString();
				this.lblPageCount.Text=this.PageCount.ToString();
				this.lblRowCount.Text=this.TotalRowCount.ToString();
			}
			else
			{
				this.cellHeader.Visible=false;
				/*
				this.lblLiteral1.Visible=false;
				this.lblLiteral2.Visible=false;
				this.lblLiteral3.Visible=false;
				this.lblLiteral4.Visible=false;
				this.lblPageCount.Visible=false;
				this.btnPrevPage.Visible=false;
				this.btnNextPage.Visible=false;
				this.btnUpdatePager.Visible=false;
				this.txtCurPage.Visible=false;
				this.txtPageSize.Visible=false;
				this.lblRowCount.Visible=false;
				*/
			}

			this.txtCurPage.Enabled=this.EnableUpdatePager;
			this.txtPageSize.Enabled=this.EnableUpdatePager;
			this.btnUpdatePager.Visible=this.EnableUpdatePager;
		}

		private void CreateTableHeader()
		{
			

			if(this.ColumnWidthArray.Length!=this.ColumnNameArray.Length || this.ColumnNameArray.Length!=this.ColumnCaptionArray.Length)
				throw new Exception("ColumnWidthArray , ColumnNameArray , ColumnCaptionArray lengths must be equal");


			TableRow row=new TableRow();
			TableCell cell;

			GridTable.CellPadding=0;
			GridTable.CellSpacing=0;
//			GridTable.BorderWidth=Unit.Pixel(1);
			GridTable.CssClass="tbl1_T";
//			GridTable.BorderColor=System.Drawing.Color.Black;


			//create header
			//first column empty

			cell=new TableCell();
			if(this.EnableCheckBoxes==false)
				cell.Visible=false;
			cell.CssClass="tbl1_hdr";
			row.Cells.Add(cell);

			//others from dataset

			for(int i=0;i<this.ColumnNameArray.Length;i++)
			{
				string columnName=this.ColumnNameArray[i];
				string columnCaption=this.ColumnCaptionArray[i];
				int columnWidth=this.ColumnWidthArray[i];

	
				if (columnWidth<=0)
					continue;	//we're not displaying curDSColumn
			


				cell=new TableCell();
				cell.CssClass="tbl1_hdr";	
				CreateHeaderCell(cell , columnName , columnCaption , columnWidth);
				row.Cells.Add(cell);

			}

			GridTable.Rows.Add(row);
		}



		protected virtual void CreateHeaderCell(TableCell cell, string ColumnName , string ColumnCaption, int ColumnWidth)
		{
			if(EnableSort==true)
			{
				Button button=new Button();
				button.Width=Unit.Pixel(ColumnWidth);
				button.CssClass="tbl1_hdr_ctrl";
				button.EnableViewState=false; //disable view state
				button.CommandName="Sort";
				button.CommandArgument=ColumnName;
				button.Text=ColumnCaption;

				if(this.Sort[ColumnName]==null)
				{
					button.ID="sort_no_" + ColumnName;
					button.CssClass="tbl1_hdr_ctrl";
				}
				else if(this.Sort[ColumnName]=="ASC")
				{
					button.ID="sort_asc_" + ColumnName;
					button.CssClass="tbl1_hdr_ctrl_act";
				}
				else if(this.Sort[ColumnName]=="DESC")
				{
					button.ID="sort_desc_" + ColumnName;
					button.CssClass="tbl1_hdr_ctrl_act";
				}
				else
					throw new Exception("Unknown sort option");

				cell.Controls.Add(button);
			}
			else
			{
				cell.Width=Unit.Pixel(ColumnWidth);
				cell.Text=ColumnCaption;
			}
		}



		


		private void CreateTableFooter()
		{
			TableRow row=new TableRow();
			TableCell cell;

			GridTable.CellPadding=0;
			GridTable.CellSpacing=0;
//			GridTable.BorderWidth=Unit.Pixel(1);
			GridTable.CssClass="tbl1_T";
//			GridTable.BorderColor=System.Drawing.Color.Black;


			//create footer
			//first column update button

			cell=new TableCell();
			if(this.EnableCheckBoxes==false)
				cell.Visible=false;
			cell.CssClass="tbl1_hdr";
			row.Cells.Add(cell);
			
			Button btnUpd=new Button();
			btnUpd.ID="btnUpdatePagerFooter";
			btnUpd.CssClass="tbl1_pgr_arrow";
			btnUpd.Text="¤";
			btnUpd.Font.Size=12;
			cell.Controls.Add(btnUpd);

			//others from dataset

			for(int i=0;i<this.ColumnNameArray.Length;i++)
			{
				string columnName=this.ColumnNameArray[i];
				int columnWidth=this.ColumnWidthArray[i];

				if (columnWidth<=0)
					continue;	//we're not displaying curDSColumn

				cell=new TableCell();
				cell.CssClass="tbl1_hdr";
				CreateFooterCell(cell , columnName , columnWidth);
				row.Cells.Add(cell);

			}

			GridTable.Rows.Add(row);
		}

		
		protected virtual void CreateFooterCell(TableCell cell, string ColumnName , int ColumnWidth)
		{
			if(this.EnableFilter==true)
			{
				TextBox txt=new TextBox();
				txt.Width=Unit.Pixel(ColumnWidth);
				txt.CssClass="tbl1_edit_box";
				txt.EnableViewState=false; //disable view state
				txt.ID="flt_" + ColumnName;

				txt.Text=this.Filter[ColumnName];

				cell.Controls.Add(txt);
			}
		}


		private void CreateTable()
		{
			if(this.DataSource==null)
				return;

			TableRow row;
			TableCell cell;
			HtmlInputRadioButton radio;
			HtmlInputCheckBox chkbox;

			int currentPage=this.CurrentPage;

			if(EnablePages==false)
				PageSize=DataSource.Rows.Count;

			int startIndex=this.CurrentPage*PageSize-PageSize;
			int endIndex=this.CurrentPage*PageSize-1;

			if(endIndex>=DataSource.DefaultView.Count)
				endIndex=DataSource.DefaultView.Count-1;

			if(startIndex>endIndex)
				if(endIndex+1>PageSize)
				{
					startIndex=0;
					endIndex=PageSize-1;
				}
				else
				{
					startIndex=0;
					//endIndex is same
				}



			for(int rowIndex=startIndex ; rowIndex<=endIndex ; rowIndex++)
			{
				row=new TableRow();
				//first column 

				cell=new TableCell();
				cell.CssClass="tbl1_chk";
				cell.Visible=false;

				if(EnableCheckBoxes==true)
				{
					cell.Visible=true;
					//string primaryKeyString=string.Join(_primaryKeySeparator, this.PrimaryKeyColumnArray);
					string primaryKeyValue="";
					for(int i=0;i<this.PrimaryKeyColumnArray.Length;i++)
					{
						if(i!=0)
							primaryKeyValue+=_primaryKeySeparator;
						primaryKeyValue+=DataSource.DefaultView[rowIndex][this.PrimaryKeyColumnArray[i].ToString()].ToString();
					}

					if(EnableMultipleSelection==false)
					{
						
						radio=new HtmlInputRadioButton();
						radio.Name="chk_";
						radio.Value=primaryKeyValue;
						radio.EnableViewState=false;
						cell.Controls.Add(radio);
					}
					else
					{
						chkbox=new HtmlInputCheckBox();
						chkbox.Name=this.UniqueID;
						chkbox.Value=primaryKeyValue;
						chkbox.ID="chk_" + rowIndex.ToString();
						chkbox.EnableViewState=false;
						cell.Controls.Add(chkbox);
					}
					
				}

				row.Cells.Add(cell);

				for(int i=0;i<this.ColumnNameArray.Length;i++)
				{
					string columnName=this.ColumnNameArray[i];
					int columnWidth=this.ColumnWidthArray[i];

					if (columnWidth<=0)
					{
						continue;	//we're not displaying curDSColumn
					}

					cell=new TableCell();
					cell.CssClass="tbl1_item";
					CreateTableCell(cell , columnName , rowIndex , DataSource.DefaultView[rowIndex].Row);
					row.Cells.Add(cell);
				}
				GridTable.Rows.Add(row);
			}


		}

		private void InitializeComponent()
		{

		}


		protected virtual void CreateTableCell(TableCell cell, string ColumnName , int RowIndex , DataRow Row)
		{
			cell.Text=Row[ColumnName].ToString();
		}





	}
}