namespace FI.UI.Web.OlapReport
{
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
	using FI.BusinessObjects.Olap;

	/// <summary>
	///		Summary description for SliceControl.
	/// </summary>
	public class SliceControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Table tblSlice;
		protected System.Web.UI.WebControls.Table tblMain;
		public BusinessObjects.OlapReport _report;


		protected override void Render(HtmlTextWriter writer)
		{
			LoadTable();
			base.Render (writer);
		}

		private void LoadTable()
		{		
			Hierarchy[] hiers=_report.Axes[2].Hierarchies.ToSortedByUniqueNameArray();
			for (int i=0 ; i<hiers.Length  ; i++ )
			{
				Hierarchy hier=hiers[i];

				//not displaying without mems
				if(hier.FilterMember==null)
					continue;

				DataMember mem=hier.FilterMember;
				//not displaying only with "All" members
				if(mem.LevelDepth==0 && !(mem is CalculatedMember) && hier.Levels[0].IsAllLevel)
					continue;

				AddHierRow(hier);
				AddMemRow(mem);
			}
		}

		private void AddHierRow(Hierarchy hier)
		{
			TableRow tr1=new TableRow();
			TableCell td1= new TableCell();

			td1.CssClass="tbl1_H";
			td1.Wrap=false;

			Button btn=new Button();
			btn.ToolTip="Set Default Filter";
			btn.ID="slc_del:" + hier.UniqueName;//hierIdentifier;
			btn.CssClass="sel_del";
			td1.Controls.Add(btn);			
			
			Label lbl=new Label();
			lbl.Text=hier.Caption;
			td1.Controls.Add(lbl);					

			tr1.Cells.Add(td1);
			tblSlice.Rows.Add(tr1);
		}

		private void AddMemRow(FI.BusinessObjects.Olap.Object mem)
		{
			//if aggregate, show children instead of agg itself
			FI.BusinessObjects.Olap.CalculatedMemberTemplates.MembersAggregate aggMem=mem as FI.BusinessObjects.Olap.CalculatedMemberTemplates.MembersAggregate;
			if(aggMem!=null)
			{
				foreach(FI.BusinessObjects.Olap.Object childMem in aggMem.Members)
					this.AddMemRow(childMem); //recursion

				return;
			}

			TableRow tr2=new TableRow();
			TableCell td2= new TableCell();
			td2.Text=mem.Name;
			td2.CssClass="tbl1_C2";
			td2.Wrap=false;
			tr2.Cells.Add(td2);
			tblSlice.Rows.Add(tr2);
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
	}
}
