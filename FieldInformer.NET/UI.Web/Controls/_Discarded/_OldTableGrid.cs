/*
namespace FI.UI.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;



	public abstract class _FIDataTableGrid : UI.Web.FIControl
	{
		public FI.Common.Data.FIDataTable _dataSource;
		public int[] ColumnWidthArray;
		public string[] ColumnNameArray;
		public string[] ColumnCaptionArray;
		public string[] PrimaryKeyColumnArray;
		public bool InMemory=false;
		public bool EnableMultipleSelection=false;
		public bool EnableSort=true;
		public bool EnableAddButton=true;
		public bool EnableEditButton=true;
		public bool EnableDeleteButton=true;
		public bool EnableSearchControls=true;
		public bool EnableCheckBoxes=true;
		public bool EnablePages=true;
		public int _defaultPageSize=10;
		public int _pageCount=-1;

		private bool _sessionVariablesInitialized=false;
		private static string _primaryKeySeparator="¤~";

		private string _sessionItemCurrentPage;
		private string _sessionItemCurrentPageSize;
		private string _sessionItemCurrentFilterColumn;
		private string _sessionItemCurrentFilterExpression;
		private string _sessionItemCurrentSortExpression;

		protected System.Web.UI.WebControls.Panel PagerPanel;
		protected System.Web.UI.WebControls.Table GridTable;
		protected System.Web.UI.WebControls.Button AddButton;
		protected System.Web.UI.WebControls.Button EditButton;
		protected System.Web.UI.WebControls.Button DeleteButton;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList SearchField;
		protected System.Web.UI.WebControls.Button SearchButton;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox SearchText;
		protected System.Web.UI.WebControls.Label ErrorLabel;



		public event System.Web.UI.WebControls.CommandEventHandler AddButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler EditButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler DeleteButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler SearchButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler SortButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler PagerButtonClick;


		// NB!! DataSourceDelegate !
		public delegate FI.Common.Data.FIDataTable GridDataSourceDelegate(int CurrentPage, int PageSize, string FilterColumn, string FilterExpression, string SortExpression);
		public GridDataSourceDelegate DataSourceDelegate=null;





		public override void InitializeSessionVariables()
		{
			if(_sessionVariablesInitialized==false)
			{
				_sessionItemCurrentPage=Page.ToString()+":" + this.UniqueID+":CurrentPage";
				_sessionItemCurrentPageSize=Page.ToString()+":" + this.UniqueID+":CurrentPageSize";
				_sessionItemCurrentFilterColumn=Page.ToString()+":" + this.UniqueID+":CurrentFilterColumn";
				_sessionItemCurrentFilterExpression=Page.ToString()+":" + this.UniqueID+":CurrentFilterExpression";
				_sessionItemCurrentSortExpression=Page.ToString()+":" + this.UniqueID+":CurrentSortExpression";

				if (Session[_sessionItemCurrentPage]==null)
					Session[_sessionItemCurrentPage]=1;

				if (Session[_sessionItemCurrentPageSize]==null)
					Session[_sessionItemCurrentPageSize]=this.DefaultPageSize;

				if (Session[_sessionItemCurrentFilterColumn]==null)
					Session[_sessionItemCurrentFilterColumn]="";

				if (Session[_sessionItemCurrentFilterExpression]==null)
					Session[_sessionItemCurrentFilterExpression]="";

				if (Session[_sessionItemCurrentSortExpression]==null)
					Session[_sessionItemCurrentSortExpression]="";

				_sessionVariablesInitialized=true;
			}
		}

		public FI.Common.Data.FIDataTable DataSource
		{
			get {return _dataSource;}
			set
			{ _dataSource=value; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.CreatePager();
			this.CreateTableHeader();

			if(this.IsPostBack==false)
			{
				//DataBind(true);
			}

		}

		private void Page_PreRender(object sender, System.EventArgs e)
		{
			DataBind();
		}


		public int PageCount
		{
			get
			{
				return _pageCount;
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

		public int PageSize
		{
			get
			{
				InitializeSessionVariables();
				return (int)Session[_sessionItemCurrentPageSize];
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


		public string FilterColumn
		{
			get
			{
				InitializeSessionVariables();
				return (string)Session[_sessionItemCurrentFilterColumn];
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentFilterColumn]=value;
			}
		}


		public string FilterExpression
		{
			get
			{
				InitializeSessionVariables();
				return (string)Session[_sessionItemCurrentFilterExpression];
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentFilterExpression]=value;
			}
		}


		public string SortExpression
		{
			get
			{
				InitializeSessionVariables();
				return (string)Session[_sessionItemCurrentSortExpression];
			}
			set
			{
				InitializeSessionVariables();
				Session[_sessionItemCurrentSortExpression]=value;
			}
		}



		private void CreateTableHeader()
		{
			

			if(this.ColumnWidthArray.Length!=this.ColumnNameArray.Length || this.ColumnNameArray.Length!=this.ColumnCaptionArray.Length)
				throw new Exception("ColumnWidthArray , ColumnNameArray , ColumnCaptionArray lengths must be equal");


			TableRow row=new TableRow();
			TableCell cell;
			Button button;

			GridTable.CellPadding=0;
			GridTable.CellSpacing=0;
			GridTable.BorderWidth=Unit.Pixel(1);
			GridTable.BorderColor=System.Drawing.Color.Black;


			//make controls invisible if needed
			AddButton.Visible=EnableAddButton;
			EditButton.Visible=EnableEditButton;
			DeleteButton.Visible=EnableDeleteButton;
			//Label1.Visible=false;
			//Label2.Visible=false;
			//SearchField.Visible=false;
			//SearchText.Visible=false;
			//SearchButton.Visible=false;
			//ErrorLabel.Visible=false;


			if(EnableSearchControls==false)
			{
				Label1.Visible=false;
				Label2.Visible=false;
				SearchField.Visible=false;
				SearchText.Visible=false;
				SearchButton.Visible=false;
				ErrorLabel.Visible=false;
			}


			//create header
			//first column empty

			cell=new TableCell();
			if(this.EnableCheckBoxes==false)
				cell.Visible=false;
			row.CssClass="tbl1_hdr";
			row.Cells.Add(cell);

			//others from dataset

			for(int i=0;i<this.ColumnNameArray.Length;i++)
			{
				string columnName=this.ColumnNameArray[i];
				string columnCaption=this.ColumnCaptionArray[i];
				int columnWidth=this.ColumnWidthArray[i];

	
				if (columnWidth<=0)
				{
					continue;	//we're not displaying curDSColumn
				}
			
				if(SearchField.Items.FindByValue(columnName)==null)
				{
					ListItem listItem=new ListItem(columnCaption , columnName);
					SearchField.Items.Add(listItem);
				}

				cell=new TableCell();
				cell.CssClass="tbl1_hdr";	

				if(EnableSort==true)
				{
					button=new Button();
					button.Width=Unit.Pixel(columnWidth);
					button.CssClass="tbl1_hdr_ctrl";
					button.EnableViewState=false; //disable view state
					button.ID="sort_" + columnName;
					button.CommandName="Sort";
					button.CommandArgument=columnName;
					button.Text=columnCaption;

					cell.Controls.Add(button);
					button.Click += new System.EventHandler(this.SortButton_Click);
				}
				else
				{
					cell.Width=Unit.Pixel(columnWidth);
					cell.Text=columnCaption;
				}

				row.Cells.Add(cell);

			}
			GridTable.Rows.Add(row);


		}

		private void DataBindTableHeader()
		{
			// sort
			if(this.InMemory)
				this.DataSource.DefaultView.Sort=SortExpression;

			string sortExpression="sort_" + this.SortExpression;

			foreach(TableCell cell in GridTable.Rows[0].Cells)
			{
				if(cell.Controls.Count==0)
					continue;

				Button button=(Button)cell.Controls[0];
				
				if(sortExpression.StartsWith(button.ID))
					button.CssClass="tbl1_hdr_ctrl_act";
				else
					button.CssClass="tbl1_hdr_ctrl";
			}			
		}


		private void CreateAndBindTableGrid()
		{

			TableRow row;
			TableCell cell;
			HtmlInputRadioButton radio;
			HtmlInputCheckBox chkbox;

			// load view state of search boxes
			SearchText.Text=this.FilterExpression;
			ListItem listItem=SearchField.Items.FindByValue(this.FilterColumn);
			if(listItem!=null)
				listItem.Selected=true;
			//

			//set filter
			if(this.InMemory)
			{
				if(this.FilterColumn!="")
					DataSource.DefaultView.RowFilter="Convert([" + FI.Common.Data.FIDataTable.EscapeSearchColumn(this.FilterColumn) + "] , 'System.String' ) LIKE '" + FI.Common.Data.FIDataTable.EscapeSearchExpression(this.FilterExpression) + "*'";
			}


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


		protected virtual void CreateTableCell(TableCell cell, string ColumnName , int RowIndex , DataRow Row)
		{
			cell.Text=Row[ColumnName].ToString();
		}


		private void CreatePager()
		{
			PagerPanel.Controls.Clear();

			if (this.EnablePages==false)
				return;

			System.Web.UI.WebControls.Button btn;

			PagerPanel.Visible=true;
			PagerPanel.Height=System.Web.UI.WebControls.Unit.Pixel(25);

			// left arrow
			btn=new System.Web.UI.WebControls.Button();
			btn.ID="left_arrow";
			btn.CommandName="Page";
			btn.Text= "3";
			btn.CssClass="tbl1_pgr_arrow";
			PagerPanel.Controls.Add(btn);
			btn.Click += new System.EventHandler(this.PagerButton_Click);


			// right arrow
			btn=new System.Web.UI.WebControls.Button();
			btn.ID="right_arrow";
			btn.CommandName="Page";
			btn.Text= "4";
			btn.CssClass="tbl1_pgr_arrow";
			PagerPanel.Controls.Add(btn);
			btn.Click += new System.EventHandler(this.PagerButton_Click);

			//page size
			System.Web.UI.WebControls.Literal litSize=new System.Web.UI.WebControls.Literal();
			litSize.Text=" Page Size:";
			PagerPanel.Controls.Add(litSize);

			// page size listbox
			System.Web.UI.WebControls.DropDownList dlSize=new System.Web.UI.WebControls.DropDownList();
			dlSize.ID="page_size";
			dlSize.CssClass="tbl1_hdr_input";
			PagerPanel.Controls.Add(dlSize);

			//current page literal
			System.Web.UI.WebControls.Literal litPage=new System.Web.UI.WebControls.Literal();
			litPage.Text=" Current Page:";
			PagerPanel.Controls.Add(litPage);

			// page number listbox
			System.Web.UI.WebControls.DropDownList dlPageNr=new System.Web.UI.WebControls.DropDownList();
			dlPageNr.ID="page_nr";
			dlPageNr.CssClass="tbl1_hdr_input";
			PagerPanel.Controls.Add(dlPageNr);
			

			// update button
			btn=new System.Web.UI.WebControls.Button();
			btn.ID="update_pager";
			btn.CommandName="Page";
			btn.Text= "Update";
			btn.CssClass="tbl1_pgr";
			PagerPanel.Controls.Add(btn);
			btn.Click += new System.EventHandler(this.PagerButton_Click);


		}



		private void DataBindPager()
		{
			if(this.EnablePages)
			{
				System.Web.UI.WebControls.Button btn;

				//left arrow
				btn=(System.Web.UI.WebControls.Button)PagerPanel.FindControl("left_arrow");
				if(this.CurrentPage==1)
					btn.Enabled=false;

				//right arrow
				btn=(System.Web.UI.WebControls.Button)PagerPanel.FindControl("right_arrow");
				if(this.CurrentPage==this.PageCount)
					btn.Enabled=false;

				System.Web.UI.WebControls.DropDownList dl;
				// page size listbox
				dl=(System.Web.UI.WebControls.DropDownList)PagerPanel.FindControl("page_size");
				dl.Items.Add("10");
				dl.Items.Add("15");
				dl.Items.Add("20");
				dl.Items.Add("30");
				dl.Items.Add("50");
				dl.Items.Add("100");
				dl.Items.FindByText(PageSize.ToString()).Selected=true;


				//page number listbox
				dl=(System.Web.UI.WebControls.DropDownList)PagerPanel.FindControl("page_nr");
				for(int i=1;i<=this.PageCount;i++)
				{
					dl.Items.Add(i.ToString());
				}
				System.Web.UI.WebControls.ListItem li= dl.Items.FindByText(this.CurrentPage.ToString());
				if(li!=null)
					li.Selected=true;
			}

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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);
		}
#endregion





		public void DataBind()
		{

			if(this.InMemory==false)
			{
				// get datasource using delegate method
				if(this.DataSourceDelegate==null)
					throw new Exception("For not-in-memory data DataSourceDelegate must be assigned");

				this.DataSource=this.DataSourceDelegate(this.CurrentPage , this.PageSize , this.FilterColumn , this.FilterExpression , this.SortExpression);
			}


			// --- correct PageCount value ---

			if(PageSize<=0)
				PageSize=this.DefaultPageSize;

			if(this.InMemory)
				_pageCount=(int)Math.Ceiling((double)System.Decimal.Divide((decimal)this.DataSource.DefaultView.Count , (decimal)PageSize));
			else
			{
				if(this.DataSource==null)
					_pageCount=0;
				else
					_pageCount=(int)Math.Ceiling((double)System.Decimal.Divide((decimal)this.DataSource.TotalCount , (decimal)PageSize));
			}

			if(_pageCount==0)
				_pageCount=1;

			// --- correct CurrentPage value ---

			if(this.CurrentPage>this.PageCount)
				this.CurrentPage=1;

			//------------------------------------


			DataBindPager();
			DataBindTableHeader();
			CreateAndBindTableGrid();
		}


		protected override bool OnBubbleEvent(object source, EventArgs e) 
		{   
			bool handled = false;
			if (e is CommandEventArgs)
			{
				CommandEventArgs ce = (CommandEventArgs)e;
				if (ce.CommandName == "Add")
				{
					if(AddButtonClick!=null)
						AddButtonClick(this , ce);
					handled = true;  
				}  
				else if (ce.CommandName == "Edit")
				{
					if(EditButtonClick!=null)
						EditButtonClick(this , ce);
					handled = true;   
				}
				else if (ce.CommandName == "Delete")
				{
					if(DeleteButtonClick!=null)
						DeleteButtonClick(this , ce);
					handled = true;   
				}
				else if (ce.CommandName == "Search")
				{
					if(SearchButtonClick!=null)
						SearchButtonClick(this , ce);
					handled = true;   
				}
				else if (ce.CommandName == "Sort")
				{
					if(SortButtonClick!=null)
						SortButtonClick(this , ce);
					handled = true;   
				}
				else if (ce.CommandName == "Page")
				{
					if(PagerButtonClick!=null)
						PagerButtonClick(this , ce);
					handled = true;   
				}
            
			}

			return handled;            
		}



		private void PagerButton_Click(object Sender , System.EventArgs e)
		{
			Button btn=(Button)Sender;

			if(btn.ID=="left_arrow")
				this.CurrentPage--;
			else if(btn.ID=="right_arrow")
				this.CurrentPage++;
			else if(btn.ID=="update_pager")
			{
				DropDownList ddl=(DropDownList)this.PagerPanel.FindControl("page_size");
				int newPageSize=int.Parse(GetPageSizeSelectedItemKey());

				if(	this.PageSize!=newPageSize)
				{
					this.PageSize=newPageSize;
					this.CurrentPage=1;
				}
				else
				{
					ddl=(DropDownList)this.PagerPanel.FindControl("page_nr");
					this.CurrentPage=int.Parse(GetPageNumberSelectedItemKey());
				}
			}
			
			//DataBind(true);
		}


		private void SortButton_Click(object sender, System.EventArgs e)
		{
			//current sort column
			Button btn=(Button)sender;
			if(this.SortExpression.StartsWith(btn.CommandArgument))
			{
				if (this.SortExpression.EndsWith("ASC"))
					this.SortExpression=btn.CommandArgument + " DESC";
				else
					this.SortExpression=btn.CommandArgument + " ASC";
			}
			else
			{
				this.SortExpression=btn.CommandArgument + " ASC";
			}

			//DataBind(true);
		}

		private void SearchButton_Click(object sender, System.EventArgs e)
		{
			this.FilterColumn=GetSearchFieldSelectedItemKey();
			this.FilterExpression=SearchText.Text;
			this.CurrentPage=1;

			//DataBind(true);
		}


		private void AddButton_Click(object sender, System.EventArgs e)
		{
		}

		private void EditButton_Click(object sender, System.EventArgs e)
		{
		}

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
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



		private string GetSearchFieldSelectedItemKey()
		{
			for(int i=0 ; i<Page.Request.Form.Count; i++)
			{
				if(Page.Request.Form.Keys[i]==this.UniqueID+":SearchField")
				{
					return Page.Request.Form[i]; 
				}
			}
			return SearchField.Items[0].Value;
		}


		private string GetPageSizeSelectedItemKey()
		{
			for(int i=0 ; i<Page.Request.Form.Count; i++)
			{
				if(Page.Request.Form.Keys[i]==this.UniqueID+":page_size")
				{
					return Page.Request.Form[i]; 
				}
			}
			return SearchField.Items[0].Value;
		}


		private string GetPageNumberSelectedItemKey()
		{
			for(int i=0 ; i<Page.Request.Form.Count; i++)
			{
				if(Page.Request.Form.Keys[i]==this.UniqueID+":page_nr")
				{
					return Page.Request.Form[i]; 
				}
			}
			return SearchField.Items[0].Value;
		}





	}
}
*/