namespace FI.UI.Web.OlapReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;
	using FI.BusinessObjects.Olap;
	using FI.BusinessObjects.Olap.CalculatedMemberTemplates;
	using FI.Common.Data;

	/// <summary>
	///		Summary description for TableControl.
	/// </summary>
	public class TableControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Table tblPivot;
		protected System.Web.UI.WebControls.Panel pnlPivot;
		protected System.Web.UI.WebControls.Table tblMain;
		protected Button btnRefresh;
		protected Button btnCancel;
		public BusinessObjects.OlapReport _report;
		private Controller _contr;

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			_contr=new Controller(_report, this.Page);
			LoadHtmlTable();
			base.Render (writer);
		}



		public void LoadHtmlTable()
		{

			System.Web.UI.HtmlControls.HtmlTable tblPivot=new System.Web.UI.HtmlControls.HtmlTable();
			tblPivot.CellPadding=0;
			tblPivot.CellSpacing=0;
			tblPivot.Width="100%";
			tblPivot.Height="100%";
			tblPivot.Attributes.Add("class" , "tbl1_T");
			pnlPivot.Controls.Add(tblPivot);

			if(_report==null || _report.Cellset.IsValid==false)
				return;

			int Ax0MemCount=_report.Cellset.Axis0TupleMemCount;
			int Ax1MemCount=_report.Cellset.Axis1TupleMemCount;
			int Ax0PosCount=_report.Cellset.Axis0PosCount;
			int Ax1PosCount=_report.Cellset.Axis1PosCount;

			int ax0OrderPos=_report.GetOrderPosition(_report.Axes[0]);
			int ax1OrderPos=_report.GetOrderPosition(_report.Axes[1]);

			Hierarchy ax1Hier=null;
			Hierarchy ax0Hier=null;
			
			//table
			System.Web.UI.HtmlControls.HtmlTableRow tr=null;
			System.Web.UI.HtmlControls.HtmlTableCell td=null;

			if(Ax0PosCount==0 && Ax1PosCount==0)
			{
				tr=new HtmlTableRow();
				tblPivot.Rows.Add(tr);
				td=new HtmlTableCell();
				tr.Cells.Add(td);
				td.Attributes.Add("class" , "tbl1_err");
				td.Attributes.Add("nowrap" , "true");
				td.InnerText="Query successfully executed, cellset contains no data";
				return;
			}
			

			for (int i=0 ; i<Ax0MemCount  ; i++ )
			{
				tr=new System.Web.UI.HtmlControls.HtmlTableRow();
				tblPivot.Rows.Add(tr);

				for (int j=0 ; j<Ax1MemCount  ; j++ )
				{
					td=new System.Web.UI.HtmlControls.HtmlTableCell();
					td.Attributes.Add("class" , "tbl1_HC");
					td.NoWrap=true;
					tr.Cells.Add(td);

					//hier controls in last row
					if(i==Ax0MemCount-1)
					{
						this.CreateHierControls(_report.Axes[1].Hierarchies[j] , td);
					}
					
				}

				ax0Hier=_report.Axes[0].Hierarchies[i];
				for (int j=0 ; j<Ax0PosCount   ; j++ )
				{
					CellsetMember mem=_report.Cellset.GetCellsetMember(0, i, j);
					bool inOrderTuple=false;

					//if same as prev, continue
					if(j!=0 && _report.Cellset.GetCellsetMember(0, i, j-1).UniqueName==mem.UniqueName)
						continue;

					td=new System.Web.UI.HtmlControls.HtmlTableCell();
					td.NoWrap=true;

					// handle order position highlight
					if(j==ax0OrderPos) // in order tuple
						inOrderTuple=true;


					// handle colspan
					int spanCount=1;
					for(int n=j+1;n<Ax0PosCount;n++)
					{
						CellsetMember nextMem=_report.Cellset.GetCellsetMember(0, i, n);
						if(nextMem.UniqueName==mem.UniqueName)
						{
							spanCount++;

							// handle order position highlight
							if(n==ax0OrderPos)
								inOrderTuple=true;
						}
						else
							break;
					}

					// handle order position highlight
					if(inOrderTuple) // in order tuple
						td.Attributes.Add("class" , "tbl1_H3");
					else
						td.Attributes.Add("class" , "tbl1_H2");

					// if we span 
					if(spanCount>1)
						td.ColSpan=spanCount;


					if(mem.ChildCount==0)
					{ // leaf-level
						System.Web.UI.HtmlControls.HtmlImage img=new System.Web.UI.HtmlControls.HtmlImage();
						img.Src="../images/leaf.gif";
						td.Controls.Add(img);
					}

					System.Web.UI.HtmlControls.HtmlInputCheckBox chb=new System.Web.UI.HtmlControls.HtmlInputCheckBox();
					chb.ID="m:" + _contr.IdentifierFromCellsetPosition(0 , j , i);
					chb.EnableViewState=false;
					td.EnableViewState=false;
					td.Controls.Add(chb);
					
					System.Web.UI.LiteralControl literal=new System.Web.UI.LiteralControl(mem.Name);
					td.Controls.Add(literal);

					tr.Cells.Add(td);
				}


				// hier controls in last col
				td=new System.Web.UI.HtmlControls.HtmlTableCell();
				td.Attributes.Add("class" , "tbl1_HC");
				td.NoWrap=true;
				CreateHierControls(ax0Hier , td);
				tr.Cells.Add(td);
			}




			for (int i=0 ; i<Ax1PosCount ; i++ )
			{
				tr=new System.Web.UI.HtmlControls.HtmlTableRow();
				tblPivot.Rows.Add(tr);
						
				for (int j=0 ; j<Ax1MemCount  ; j++ )
				{	
					ax1Hier=_report.Axes[1].Hierarchies[j];
					CellsetMember mem=_report.Cellset.GetCellsetMember(1, j, i);
					bool inOrderTuple=false;

					//if same as prev, continue
					if(i!=0 && _report.Cellset.GetCellsetMember(1, j, i-1).UniqueName==mem.UniqueName)
						continue;

					td=new System.Web.UI.HtmlControls.HtmlTableCell();
					td.NoWrap=true;

					// handle order position highlight
					if(i==ax1OrderPos) // in order tuple
						inOrderTuple=true;


					// handle rowspan
					int spanCount=1;
					for(int n=i+1;n<Ax1PosCount;n++)
					{
						CellsetMember nextMem=_report.Cellset.GetCellsetMember(1, j, n);
						if(nextMem.UniqueName==mem.UniqueName)
						{
							spanCount++;

							// handle order position highlight
							if(n==ax1OrderPos)
								inOrderTuple=true;
						}
						else
							break;
					}

					// handle order position highlight
					if(inOrderTuple) // in order tuple
						td.Attributes.Add("class" , "tbl1_H1");
					else
						td.Attributes.Add("class" , "tbl1_H");

					// if we span 
					if(spanCount>1)
						td.RowSpan=spanCount;



					if(mem.ChildCount==0)
					{ // leaf-level
						System.Web.UI.HtmlControls.HtmlImage img=new System.Web.UI.HtmlControls.HtmlImage();
						img.Src="../images/leaf.gif";
						td.Controls.Add(img);
					}

					System.Web.UI.HtmlControls.HtmlInputCheckBox chb=new System.Web.UI.HtmlControls.HtmlInputCheckBox();
					chb.ID="m:" + _contr.IdentifierFromCellsetPosition(1 , i , j);
					chb.EnableViewState=false;
					td.EnableViewState=false;
					td.Controls.Add(chb);
					
					System.Web.UI.LiteralControl literal=new System.Web.UI.LiteralControl(mem.Name);
					td.Controls.Add(literal);


					tr.Cells.Add(td);
				}

				for (int j=0 ; j<Ax0PosCount   ; j++ )
				{
					td=new System.Web.UI.HtmlControls.HtmlTableCell();
					td.Attributes.Add("class" , "tbl1_C");
					td.NoWrap=true;
					Cell olapCell=_report.Cellset.GetCell(j , i);
					td.InnerText=olapCell.FormattedValue;
					tr.Cells.Add(td);
				}

			}
		}


		private bool HasAggragate(Hierarchy hier , VisualAggregate.AggregateFunction aggr)
		{
			foreach(DataMember mem in hier.CalculatedMembers)
			{
				VisualAggregate aggrMem=mem as VisualAggregate;
				if(aggrMem!=null && aggrMem.Aggregation==aggr)
					return true;
			}
			return false;
		}


		private void CreateHierControls(Hierarchy hier , HtmlTableCell td)
		{
			string hierName=hier.UniqueName;
			Button btn;

			Literal lit=new Literal();
			lit.Text=hierName;
			td.Controls.Add(lit);

			// visual avg button
			btn=new Button();
			if(HasAggragate(hier, VisualAggregate.AggregateFunction.AVG))
			{
				btn.ToolTip="Remove Avg";
				btn.ID="tbl_avg:off:"+ hierName;
				btn.CssClass="tbl_avg_on";
			}
			else
			{
				btn.ToolTip="Add Avg";
				btn.ID="tbl_avg:on:"+ hierName;
				btn.CssClass="tbl_avg_off";
			}
			td.Controls.Add(btn);

			// visual sum button
			btn=new Button();
			if(HasAggragate(hier, VisualAggregate.AggregateFunction.SUM))
			{
				btn.ToolTip="Remove Sum";
				btn.ID="tbl_sum:off:"+ hierName;
				btn.CssClass="tbl_sum_on";
			}
			else
			{
				btn.ToolTip="Add Sum";
				btn.ID="tbl_sum:on:"+ hierName;
				btn.CssClass="tbl_sum_off";
			}
			td.Controls.Add(btn);

			// visual min button
			btn=new Button();
			if(HasAggragate(hier, VisualAggregate.AggregateFunction.MIN))
			{
				btn.ToolTip="Remove Min";
				btn.ID="tbl_min:off:"+ hierName;
				btn.CssClass="tbl_min_on";
			}
			else
			{
				btn.ToolTip="Add Min";
				btn.ID="tbl_min:on:"+ hierName;
				btn.CssClass="tbl_min_off";
			}
			td.Controls.Add(btn);

			// visual avg button
			btn=new Button();
			if(HasAggragate(hier, VisualAggregate.AggregateFunction.MAX))
			{
				btn.ToolTip="Remove Max";
				btn.ID="tbl_max:off:"+ hierName;
				btn.CssClass="tbl_max_on";
			}
			else
			{
				btn.ToolTip="Add Max";
				btn.ID="tbl_max:on:"+ hierName;
				btn.CssClass="tbl_max_off";
			}
			td.Controls.Add(btn);
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
