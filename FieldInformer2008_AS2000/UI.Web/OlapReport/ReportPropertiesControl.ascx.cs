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

	/// <summary>
	///		Summary description for ReportPropertiesControl.
	/// </summary>
	public partial class ReportPropertiesControl : System.Web.UI.UserControl
	{

		public FI.BusinessObjects.OlapReport _report;
		private Controller _contr;

		protected void Page_Load(object sender, System.EventArgs e)
		{			
			_contr=new Controller(_report , this.Page);			
		}

		private bool IsOpen
		{
			get
			{
				object o=Session[this.UniqueID+ ":" + this._report.ID.ToString() + ":IsOpen"];
				return (o==null ? false : (bool)o);
			}
			set
			{
				Session[this.UniqueID+ ":" + this._report.ID.ToString() + ":IsOpen"]=value;
			}
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			//txtName
			txtName.Value=_report.Name;

			//txtDescr
			txtDescr.Value=_report.Description;
			
			bool isOpen=this.IsOpen;
			this.btnProperties.CssClass=(isOpen ? "sel_close" : "sel_open");
			this.btnProperties.ToolTip=(isOpen ? "Hide Properties" : "Show Properties");
			emptyTuplesCell.Visible=isOpen;
			timeRangeCell.Visible=isOpen;
			calcRatioCell.Visible=isOpen;
			calcMeaCell.Visible=isOpen;
			
			if(isOpen)
			{
				// empty cells
				this.selRowEmpty.Items.Add(new ListItem("Show All", ((int)Axis.EmptyTupleOptionEnum.NONE).ToString()));
				this.selColEmpty.Items.Add(new ListItem("Show All", ((int)Axis.EmptyTupleOptionEnum.NONE).ToString()));

				this.selRowEmpty.Items.Add(new ListItem("Hide Empty", ((int)Axis.EmptyTupleOptionEnum.HIDE_EMPTY).ToString()));
				this.selColEmpty.Items.Add(new ListItem("Hide Empty", ((int)Axis.EmptyTupleOptionEnum.HIDE_EMPTY).ToString()));

				this.selRowEmpty.Items.Add(new ListItem("Hide Non-Empty", ((int)Axis.EmptyTupleOptionEnum.HIDE_NONEMPTY).ToString()));
				this.selColEmpty.Items.Add(new ListItem("Hide Non-Empty", ((int)Axis.EmptyTupleOptionEnum.HIDE_NONEMPTY).ToString()));

				this.selRowEmpty.Value=((int)_report.Axes[1].EmptyTupleOption).ToString();
				if(_report.Axes[1].EmptyTupleOption!=Axis.EmptyTupleOptionEnum.NONE)
					this.selRowEmpty.Attributes.Add("style", "color:red;");
				this.selColEmpty.Value=((int)_report.Axes[0].EmptyTupleOption).ToString();
				if(_report.Axes[0].EmptyTupleOption!=Axis.EmptyTupleOptionEnum.NONE)
					this.selColEmpty.Attributes.Add("style", "color:red;");


				// selTimeRangeLevel
				foreach(Dimension dim in _report.Schema.Dimensions)
				{
					if(dim.Name=="Time")
						foreach(Hierarchy hier in dim.Hierarchies)
							foreach(Level lev in hier.Levels)
								selTimeRangeLevel.Items.Add(lev.UniqueName);
				}



				// selRatioType
				selRatioType.Items.Add("Ratio To Visual Sum");
				selRatioType.Items.Add("Ratio To Visual Avg");
				selRatioType.Items.Add("Ratio To Visual Min");
				selRatioType.Items.Add("Ratio To Visual Max");
				selRatioType.Items.Add("Ratio To Parent Member");
				selRatioType.Items.Add("Ratio To (All) Member");


				// sel operation
				selCalcOperation.Items.Add("-");
				selCalcOperation.Items.Add("+");
				selCalcOperation.Items.Add("/");
				selCalcOperation.Items.Add("*");
				selCalcOperation.Items.Add("Inherite NULL");


				// dims
				foreach(Dimension dim in _report.Schema.Dimensions)
				{
					foreach(Hierarchy hier in dim.Hierarchies)
						selRatioDim.Items.Add(hier.UniqueName);
				}


				// measures
				Hierarchy measuresHier=_report.Schema.Hierarchies["[Measures]"];
				if(measuresHier.SchemaMembers.Count==0)
				{
					//because we need list of measures
					measuresHier.Open();
					measuresHier.Close();
				}
				foreach(SchemaMember mem in measuresHier.SchemaMembers)
					AddSelCalcMeasureWithChildren(mem);

			}


			base.Render (writer);
		}

		private void AddSelCalcMeasureWithChildren(SchemaMember smem)
		{
			if(smem.IsPlaceholder==false)
			{
				selRatioMeasure.Items.Add(smem.Name);
				selCalcMeasure1.Items.Add(smem.Name);
				selCalcMeasure2.Items.Add(smem.Name);
			}

			// recursion on children
			foreach(SchemaMember child in smem.Children)
				AddSelCalcMeasureWithChildren(child);
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


		protected void btnProperties_Click(object sender, System.EventArgs e)
		{			
			this.IsOpen=!this.IsOpen;
		}

		public void UpdateReportHeader()
		{			
			_report.Name=txtName.Value;
			_report.Description=txtDescr.Value;
		}


		protected void btnAddTimeRange_Click(object sender, System.EventArgs e)
		{
			try
			{
				string levUN=Request.Form[this.UniqueID + ":selTimeRangeLevel"];
				Level lev=_report.Schema.Levels[levUN];
				if(lev==null)
					throw new Exception("Cannot find level");

				_contr.AddFilteredByNameSet(lev , this.txtTimeRangeStart.Value , this.txtTimeRangeEnd.Value , this.chkTimeRangePrompt.Checked);

				_report.SaveState();
			}
			catch(Exception exc)
			{
				((PageBase)this.Page).ShowException(exc);
			}
		}

		protected void btnAddRatio_Click(object sender, System.EventArgs e)
		{
			try
			{
				string ratioType=Request.Form[this.UniqueID + ":selRatioType"];
				string ratioHier=Request.Form[this.UniqueID + ":selRatioDim"];
				string ratioMeasure=Request.Form[this.UniqueID + ":selRatioMeasure"];
				_contr.AddRatioMeasure(ratioType , ratioHier , ratioMeasure);

				_report.SaveState();
			}
			catch(Exception exc)
			{
				((PageBase)this.Page).ShowException(exc);
			}
		}

		protected void btnAddCalc_Click(object sender, System.EventArgs e)
		{
			try
			{
				string measure1=Request.Form[this.UniqueID + ":selCalcMeasure1"];
				string measure2=Request.Form[this.UniqueID + ":selCalcMeasure2"];
				string operation=Request.Form[this.UniqueID + ":selCalcOperation"];

				_contr.AddCalculatedMeasure(measure1, measure2, operation);

				_report.SaveState();
			}
			catch(Exception exc)
			{
				((PageBase)this.Page).ShowException(exc);
			}
		}

		protected void btnEmptyCellsUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				_report.Axes[1].EmptyTupleOption=(Axis.EmptyTupleOptionEnum)int.Parse(Request.Form[this.UniqueID + ":selRowEmpty"]);
				_report.Axes[0].EmptyTupleOption=(Axis.EmptyTupleOptionEnum)int.Parse(Request.Form[this.UniqueID + ":selColEmpty"]);

				_report.SaveState();
			}
			catch(Exception exc)
			{
				((PageBase)this.Page).ShowException(exc);
			}
		}

	}
}
