namespace FI.UI.Web.OlapReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.Common.Data;
	using FI.BusinessObjects;
	using FI.BusinessObjects.Olap;
	using FI.BusinessObjects.Olap.CalculatedMemberTemplates;

	/// <summary>
	///		Summary description for SelectControl.
	/// </summary>
	public class SelectControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Table tblMain;
		protected System.Web.UI.HtmlControls.HtmlTable tblColumns;
		protected System.Web.UI.HtmlControls.HtmlTable tblRows;
		protected System.Web.UI.HtmlControls.HtmlTable tblFilter;
		public BusinessObjects.OlapReport _report;
		private Controller _contr;


		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			_contr=new Controller(_report , this.Page);
			LoadSelectTable(tblColumns , "Columns" , 0);
			LoadSelectTable(tblRows , "Rows" , 1);
			LoadSelectTable(tblFilter , "Filter" , 2);
			base.Render (writer);
		}




		private void LoadSelectTable(HtmlTable hostTable , string Header, short AxisOrdinal)
		{
		
			HtmlTableRow tr;
			HtmlTableCell td;

			//header
			tr=new HtmlTableRow();
			td= new HtmlTableCell();
			td.InnerText=Header;
			td.ColSpan=3;
			td.Attributes.Add("class","sel_H");
			td.NoWrap=true;
			tr.Cells.Add(td);		

			td= new HtmlTableCell();
			td.NoWrap=true;
			tr.Cells.Add(td);

			td= new HtmlTableCell();
			td.NoWrap=true;
			tr.Cells.Add(td);
			hostTable.Rows.Add(tr);
			

			if(AxisOrdinal==2)
			{				
				string curFolder="";
				bool curFolderOpen=false;
				Hierarchy[] hiers=_report.Schema.Hierarchies.ToSortedByDisplayPathArray();
				foreach(Hierarchy hier in hiers)
				{
					if(hier.Axis.Ordinal!=AxisOrdinal)
						continue;

					string folder=hier.DisplayFolder;
					// if folder is empty, check whther dimesnion name should be displayed as folder
					if(folder=="" && hier.Dimension.Hierarchies.Count>1)
						folder=hier.Dimension.Name;			
					bool folderOpen=(folder!="" && _report.Schema.IsNodeOpen(folder));

					// create folder if needed
					if(curFolder!=folder && folder!="")		
						CreateFolder(2, hostTable, folder, folderOpen);							
					curFolder=folder;			
					curFolderOpen=folderOpen;

					// create hierarchy if folder is open or there's no folder			
					if(curFolder=="" || curFolderOpen)
					{
						byte depth=(byte)(curFolder=="" ? 0 : 1);
						CreateHierarchy(2, hostTable, hier, depth);
					}
				}
			}
			else
			{
				for (int i=0 ; i<_report.Axes[AxisOrdinal].Hierarchies.Count  ; i++ )
				{
					Hierarchy hier=_report.Axes[AxisOrdinal].Hierarchies[i]; //not alph order 
					//hier header
					CreateHierarchy(AxisOrdinal, hostTable, hier, 0);
				}
			}


		}


		

		private void CreateFolder(short AxisOrdinal, HtmlTable hostTable , string name, bool isOpen)
		{
			HtmlTableRow tr=new HtmlTableRow();
			HtmlTableCell td;
			System.Web.UI.WebControls.Button btn;

			// --- node contr col--
			tr=new HtmlTableRow();
			td= new HtmlTableCell();
			td.Attributes.Add("class","sel_C1");
			td.NoWrap=true;
			tr.Cells.Add(td);

			// --- node name col--
			td= new HtmlTableCell();

			btn=new System.Web.UI.WebControls.Button();
			if(isOpen)
			{
				btn.ToolTip="Close";
				btn.ID="sel_fclose:"+ name;
				btn.CssClass="sel_close";
			}
			else
			{
				btn.ToolTip="Open";
				btn.ID="sel_fopen:"+ name;
				btn.CssClass="sel_open";
			}
			td.Controls.Add(btn);

			Literal lit=new Literal();
			lit.Text=name;
			td.Controls.Add(lit);
			td.Attributes.Add("class","sel_C");
			td.NoWrap=true;
			tr.Cells.Add(td);


			// --- node select col--
			td= new HtmlTableCell();
			td.Attributes.Add("class","sel_C");
			td.NoWrap=true;
			tr.Cells.Add(td);



			hostTable.Rows.Add(tr);

			
//			if(!isOpen)
//				return;

//			// members level depth=0
//			for(int j=0;j<dim.Hierarchies.Count;j++)
//				if(dim.Hierarchies[j].Axis.Ordinal==AxisOrdinal) // only if axis matches
//					CreateHierarchy(AxisOrdinal, hostTable, dim.Hierarchies[j],  1); //treedepth is 1 because 0 is dim

		}



		private void CreateHierarchy(short AxisOrdinal, HtmlTable hostTable , Hierarchy hier, byte TreeDepth)
		{
			//string hierIdentifier=_contr.IdentifierFromHierarchy(hier);
			bool hierIsAggregated=false;
			bool hierIsMeasures=(hier.UniqueName=="[Measures]");
			MembersAggregate aggr=hier.FilterMember as MembersAggregate;
			if(aggr!=null)
				hierIsAggregated=true;

			HtmlTableRow tr=new HtmlTableRow();
			HtmlTableCell td;
			System.Web.UI.WebControls.Button btn;

			// --- node contr col--
			tr=new HtmlTableRow();
			td= new HtmlTableCell();
			if(AxisOrdinal!=2)
			{
				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Move To Filter";
				btn.ID="sel_del:" + hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_del";
				td.Controls.Add(btn);

				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Move Up";
				btn.ID="sel_up:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_up";
				td.Controls.Add(btn);

				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Move Down";
				btn.ID="sel_down:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_down";
				td.Controls.Add(btn);
			}
			else
			{
				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Move To Row";
				btn.ID="sel_torow:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_torow";
				td.Controls.Add(btn);

				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Move To Column";
				btn.ID="sel_tocol:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_tocol";
				td.Controls.Add(btn);
			}

			td.Attributes.Add("class","sel_C1");
			td.NoWrap=true;
			tr.Cells.Add(td);

			// --- node name col--
			td= new HtmlTableCell();

			Literal lit=new Literal();
			for(int i=0;i<TreeDepth;i++)
				lit.Text=lit.Text + "&nbsp;&nbsp;";
			td.Controls.Add(lit);

			btn=new System.Web.UI.WebControls.Button();
			if(hier.IsOpen)
			{
				btn.ToolTip="Close";
				btn.ID="sel_hclose:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_close";
			}
			else
			{
				btn.ToolTip="Open";
				btn.ID="sel_hopen:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_open";
			}
			td.Controls.Add(btn);

			lit=new Literal();
			lit.Text=hier.DisplayName;
			td.Controls.Add(lit);
			td.Attributes.Add("class","sel_C");
			td.NoWrap=true;
			tr.Cells.Add(td);


			// --- node select col--
			td= new HtmlTableCell();

			if(AxisOrdinal!=2 && hier.IsOpen && !hierIsMeasures)
			{
				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Select Children";
				btn.ID="sel_hselall:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_selall";
				td.Controls.Add(btn);

				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Deselect All Children";
				btn.ID="sel_hdeselall:"+ hier.UniqueName;//hierIdentifier;
				btn.CssClass="sel_deselall";
				td.Controls.Add(btn);
			}
			else if(AxisOrdinal==2 && !hierIsMeasures)
			{
				btn=new System.Web.UI.WebControls.Button();
				if(hierIsAggregated)
				{
					btn.ToolTip="Set Aggregate Off";
					btn.ID="sel_aggr_off:"+ hier.UniqueName;//hierIdentifier;
					btn.CssClass="sel_aggr_on";
				}
				else
				{
					btn.ToolTip="Set Aggregate On";
					btn.ID="sel_aggr_on:"+ hier.UniqueName;//hierIdentifier;
					btn.CssClass="sel_aggr_off";
				}
				td.Controls.Add(btn);
			}

			td.Attributes.Add("class","sel_C2");
			td.NoWrap=true;
			tr.Cells.Add(td);



			hostTable.Rows.Add(tr);


			
			if(hier.IsOpen==false)
				return;

			
			TreeDepth++;
			// data members level depth=0
			for(int j=0;j<hier.SchemaMembers.Count;j++)
				CreateMemHierarchy(AxisOrdinal, hostTable, hier.SchemaMembers[j] , hierIsAggregated, TreeDepth, false);

			// calc members
			for(int j=0;j<hier.CalculatedMembers.Count;j++)
				CreateMemHierarchy(AxisOrdinal, hostTable, hier.CalculatedMembers[j] , hierIsAggregated, 0, false);
		}



		private void CreateMemHierarchy(short AxisOrdinal, HtmlTable hostTable , Member mem, bool HierIsAggregated , byte TreeDepth, bool autoSelect)
		{
			//do not display aggregate, display undlying calc members instead
			if(HierIsAggregated==true && mem.UniqueName==mem.Hierarchy.FilterMember.UniqueName)
			{
				MembersAggregate maggr=mem.Hierarchy.FilterMember as MembersAggregate;
				if(maggr!=null)
				{
					for(int i=0;i<maggr.Members.Count;i++)
					{
						CalculatedMember cmem=maggr.Members[i] as CalculatedMember;
						if(cmem!=null)
							this.CreateMemHierarchy(AxisOrdinal, hostTable, cmem, HierIsAggregated, TreeDepth, false); // recursion
					}
				}
				return;
			}

			string memIdentifier=_contr.IdentifierFromSchemaMember(mem);
			string hierIdentifier=mem.Hierarchy.Axis.Ordinal.ToString() + ":" + mem.Hierarchy.Ordinal.ToString();
			bool memIsSelected=false;
			bool memIsOpen=false;
			bool memIsPlaceholder=false;
			bool memChildrenAutoSelected=(mem.Hierarchy.CalculatedMembers.GetMemberChildrenSet(mem.UniqueName)!=null);
			SchemaMember smem=mem as SchemaMember;
			if(smem!=null)
			{
				memIsOpen=smem.IsOpen;
				memIsPlaceholder=smem.IsPlaceholder;
			}

			if(HierIsAggregated)
				memIsSelected=(((MembersAggregate)mem.Hierarchy.FilterMember).Members[mem.UniqueName]!=null?true:false);
			else
				memIsSelected=(mem.Hierarchy.GetMember(mem.UniqueName)!=null);

			HtmlTableRow tr=new HtmlTableRow();
			HtmlTableCell td;
			System.Web.UI.WebControls.Button btn;
			Literal lit;

			// --- node contr col--
			td= new HtmlTableCell();
			td.Attributes.Add("class","sel_C1");
			td.NoWrap=true;
			tr.Cells.Add(td);

			// --- node name col--
			td= new HtmlTableCell();

			lit=new Literal();
			for(int i=0;i<TreeDepth;i++)
				lit.Text=lit.Text + "&nbsp;&nbsp;";
			td.Controls.Add(lit);
			
			if(memIsOpen)
			{
				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip="Close";
				btn.ID="sel_close:" + memIdentifier;
				btn.CssClass="sel_close";
				td.Controls.Add(btn);
			}
			else
			{
				if(mem.CanDrillDown)
				{										
					btn=new System.Web.UI.WebControls.Button();
					btn.ToolTip="Open";
					btn.ID="sel_open:"+memIdentifier;
					btn.CssClass="sel_open";
					td.Controls.Add(btn);
				}
				else
				{
					// no image
					lit.Text=lit.Text + "&nbsp;&nbsp;&nbsp;";
				}
			}
			

			if(memIsPlaceholder==false)
				if(AxisOrdinal==2 && HierIsAggregated==false)
				{
					HtmlInputRadioButton radio=new HtmlInputRadioButton();
					radio.Name="m:" + hierIdentifier;
					radio.ID="m:" + memIdentifier;
					radio.EnableViewState=false;
					radio.Checked=(memIsSelected || autoSelect);
					radio.Disabled=autoSelect;
					radio.Attributes.Add("class" , "sel_chk");
					td.Controls.Add(radio);
				}
				else
				{
					HtmlInputCheckBox chk=new HtmlInputCheckBox();
					chk.ID="m:" + mem.UniqueName; //note, UniqueName !
					chk.EnableViewState=false;
					chk.Checked=(memIsSelected || autoSelect);	
					chk.Disabled=autoSelect;
					chk.Attributes.Add("class" , "sel_chk");
					td.Controls.Add(chk);
				}

			lit=new Literal();
			lit.Text=mem.Name;
			td.Controls.Add(lit);

			td.Attributes.Add("class","sel_C");
			td.NoWrap=true;
			tr.Cells.Add(td);


			// --- node select col--
			td= new HtmlTableCell();

			if(AxisOrdinal!=2 && memIsOpen)
			{
				if(!memChildrenAutoSelected)
				{
					// placeholder cannot have auto-children, because it is not native olap object
					if(!memIsPlaceholder)
					{
						btn=new System.Web.UI.WebControls.Button();
						btn.ToolTip="Auto-Select Children";
						btn.ID="sel_selauto:"+memIdentifier;
						btn.CssClass="sel_selauto";
						td.Controls.Add(btn);
					}

					btn=new System.Web.UI.WebControls.Button();
					btn.ToolTip="Select Children";
					btn.ID="sel_selall:"+memIdentifier;
					btn.CssClass="sel_selall";
					td.Controls.Add(btn);
				}

				btn=new System.Web.UI.WebControls.Button();
				btn.ToolTip ="Deselect All Children";
				btn.ID="sel_deselall:"+memIdentifier;
				btn.CssClass="sel_deselall";
				td.Controls.Add(btn);
			}

			td.Attributes.Add("class","sel_C2");
			td.NoWrap=true;
			tr.Cells.Add(td);


			hostTable.Rows.Add(tr);


			if(memIsOpen==false)
				return;

			// next level members if it's schema member
			TreeDepth++;
			if(smem!=null)
			{
				for(int j=0;j<smem.Children.Count;j++)
					CreateMemHierarchy(AxisOrdinal, hostTable, smem.Children[j] , HierIsAggregated , TreeDepth, memChildrenAutoSelected);
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
