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
	///		Summary description for FilteredLevelSetControl.
	/// </summary>
	public class FilteredLevelSetControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList ddlLevel;
		protected System.Web.UI.WebControls.TextBox txtGrOrEq;
		protected System.Web.UI.WebControls.TextBox txtLessOrEq;
		protected System.Web.UI.HtmlControls.HtmlTableCell cellErr;
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected System.Web.UI.WebControls.Label lblName;

		protected internal FilteredLevelSet _filtLevSet;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.lblName.Text=_filtLevSet.Name;
			this.txtGrOrEq.Text=_filtLevSet.GrOrEq;
			this.txtLessOrEq.Text=_filtLevSet.LessOrEq;

			foreach(Level lev in _filtLevSet.Level.Hierarchy.Levels)
			{
				this.ddlLevel.Items.Add(lev.UniqueName);
				if(lev.UniqueName==_filtLevSet.Level.UniqueName)
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
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				FilteredLevelSet newSet=new FilteredLevelSet(
					null, 
					_filtLevSet.Hierarchy,
					_filtLevSet.Hierarchy.Levels[this.ddlLevel.SelectedValue],
					this.txtGrOrEq.Text,
					this.txtLessOrEq.Text);
				newSet.Prompt=_filtLevSet.Prompt;

				_filtLevSet=(FilteredLevelSet)_filtLevSet.Hierarchy.ReplaceMember(_filtLevSet, newSet);
			}
			catch(Exception exc)
			{
				if(Common.AppConfig.IsDebugMode)
					Common.LogWriter.Instance.WriteEventLogEntry(exc);
				cellErr.InnerText=exc.Message;
				return;
			}

			//update name
			this.lblName.Text=_filtLevSet.Name;

			cellErr.InnerText="";
		}
	}
}
