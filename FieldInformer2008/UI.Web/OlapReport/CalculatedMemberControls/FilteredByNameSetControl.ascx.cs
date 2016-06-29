namespace FI.UI.Web.OlapReport.CalculatedMemberControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects.Olap;
	using FI.BusinessObjects.Olap.CalculatedMemberTemplates;

	/// <summary>
	///		Summary description for FilteredByNameSetControl.
	/// </summary>
	public partial class FilteredByNameSetControl : System.Web.UI.UserControl
	{

		public FilteredByNameSet _filtByNameSet;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.lblName.Text=_filtByNameSet.Name;
			this.txtGrOrEq.Text=_filtByNameSet.GrOrEq;
			this.txtLessOrEq.Text=_filtByNameSet.LessOrEq;

			foreach(Level lev in _filtByNameSet.Level.Hierarchy.Levels)
			{
				this.ddlLevel.Items.Add(lev.UniqueName);
				if(lev.UniqueName==_filtByNameSet.Level.UniqueName)
					this.ddlLevel.SelectedIndex=this.ddlLevel.Items.Count-1; // set current item selected
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

		}
		#endregion

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				FilteredByNameSet newSet=new FilteredByNameSet(
					null, 
					_filtByNameSet.Hierarchy,
					_filtByNameSet.Hierarchy.Levels[this.ddlLevel.SelectedValue],
					this.txtGrOrEq.Text,
					this.txtLessOrEq.Text);
				newSet.Prompt=_filtByNameSet.Prompt;

				_filtByNameSet=(FilteredByNameSet)_filtByNameSet.Hierarchy.ReplaceMember(_filtByNameSet, newSet);
			}
			catch(Exception exc)
			{
				if(Common.AppConfig.IsDebugMode)
					Common.LogWriter.Instance.WriteEventLogEntry(exc);
				cellErr.InnerText=exc.Message;
				return;
			}

			//update name
			this.lblName.Text=_filtByNameSet.Name;

			cellErr.InnerText="";
		}
	}
}
