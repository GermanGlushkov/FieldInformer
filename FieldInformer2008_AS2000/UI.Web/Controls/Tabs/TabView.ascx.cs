namespace FI.UI.Web.Controls.Tabs
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for TabView.
	/// </summary>
	public partial  class TabView : System.Web.UI.UserControl
	{
		public bool EnableLogoutButton=true;
		public string LogoutHref="";
		public string WelcomeNote="";
		private FI.UI.Web.Controls.Tabs.TabsData _tabsDataSet=new Tabs.TabsData();		
		public byte CssStyleNum;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			CreateTabs();
			base.Render (writer);
		}

		public int AddTab(int ParentId , string Label , string Href , bool IsActive , bool IsButton)
		{
			System.Data.DataRow row;

			if(ParentId!=0)
			{
				row=_tabsDataSet.Tabs.Select("Id=" + ParentId)[0];
				return (int)_tabsDataSet.Tabs.AddTabsRow((int)row["Level"]+1 , ParentId , Label , Href , IsActive , IsButton )["Id"];
			}
			else
			{
				return (int)_tabsDataSet.Tabs.AddTabsRow(0 , 0 , Label , Href , IsActive,  IsButton)["Id"];
			}

		}

		public void RemoveTab(int TabId)
		{
		}

		public void SetActiveTab(int TabId , bool SetParentsActive , bool ClearAllActive)
		{
			if (ClearAllActive==true)
			{
				System.Data.DataRow[] rows=_tabsDataSet.Tabs.Select("IsActive=true");
				foreach (System.Data.DataRow row in rows)
				{
					row["IsActive"]=false;
				}
			}

			System.Data.DataRow[] tabRows;
			System.Data.DataRow tabRow=_tabsDataSet.Tabs.Select("Id=" + TabId)[0];
			tabRow["IsActive"]=true;

			if (SetParentsActive==true)
			{
				tabRows=_tabsDataSet.Tabs.Select("Id=" + tabRow["ParentId"]);
				if (tabRows.Length>0)
				{	
					SetActiveTab((int)tabRows[0]["Id"] , true , false);
				}
			}
		}


		public void AddImage(int TabId , string Url)
		{
			_tabsDataSet.TabImages.AddTabImagesRow(TabId , Url);
		}


		public void ReadXml(System.IO.Stream Stream)
		{
			_tabsDataSet.ReadXml(Stream);
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

		}
		#endregion


		private void CreateTabs()
		{
			System.Data.DataRow[] tabRows;
			System.Web.UI.WebControls.Table innerTable;
			System.Web.UI.WebControls.TableCell tableCell;
			System.Web.UI.WebControls.TableCell parentTableCell;
			System.Web.UI.WebControls.TableRow tableRow;
			System.Web.UI.WebControls.TableRow parentTableRow;
			System.Web.UI.Control control;

			for(int i=0 ; i<30 ; i++)
			{
				tabRows=_tabsDataSet.Tabs.Select("Level=" + i.ToString());
				if (tabRows.Length==0)
					break;

				parentTableRow=new System.Web.UI.WebControls.TableRow() ;
				ParentTable.Rows.Add(parentTableRow);
				parentTableCell=new System.Web.UI.WebControls.TableCell();
				parentTableRow.Cells.Add(parentTableCell);

				innerTable=new System.Web.UI.WebControls.Table();
				innerTable.CellPadding=0;
				innerTable.CellSpacing=0;
				parentTableCell.Controls.Add(innerTable);
				tableRow=new System.Web.UI.WebControls.TableRow() ;
				innerTable.Rows.Add(tableRow);
				
				foreach(System.Data.DataRow row in tabRows)
				{
					tableCell=new System.Web.UI.WebControls.TableCell();
					tableRow.Cells.Add(tableCell);

					control = LoadControl("Tab.ascx");
					//adding images
					foreach(System.Data.DataRow imagesRow in _tabsDataSet.TabImages.Select("TabId=" + row["Id"].ToString()))
					{
						((Tab)control).AddImage(imagesRow["ImageUrl"].ToString());
					}
					//
					if(i==0)
						((Tab)control).IsRoot=true;
					((Tab)control).Caption  = (string)row["Caption"];
					((Tab)control).IsActive=(bool)row["IsActive"];
					((Tab)control).IsButton=(bool)row["IsButton"];
					((Tab)control).Id=(int)row["Id"];
					((Tab)control).Href=row["Href"].ToString();
					((Tab)control).CssStyleNum=this.CssStyleNum;
					tableCell.Controls.Add(control);
					
				}

				tableCell=new System.Web.UI.WebControls.TableCell();
				tableCell.Width=System.Web.UI.WebControls.Unit.Percentage(100);
				tableCell.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Right;
				
				tableRow.Cells.Add(tableCell);


				Space space = (Space)LoadControl("Space.ascx");
				if(i==0)
					space.IsRoot=true;
				space.EnableLogoutButton=this.EnableLogoutButton;
				space.LogoutHref=this.LogoutHref;
				space.WelcomeNote=this.WelcomeNote;
				space.CssStyleNum=this.CssStyleNum;
				tableCell.Controls.Add(space);

			}

		}







	}
}
