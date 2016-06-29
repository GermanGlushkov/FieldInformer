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
	public class Design : StorecheckPageBase
	{
		protected System.Web.UI.WebControls.Button btnUpdate;

		protected FI.UI.Web.Controls.Tabs.TabView _tabView;
		protected System.Web.UI.HtmlControls.HtmlTableCell contentsCell;

		protected string _contentType="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			_tabView=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TvC");
			LoadContents();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			this.CreateReportTabs(_tabView, null);
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
				_contentType="List";
			else
				_contentType=(string)Session[this.ToString()  + ":ContentType"];




			if(_contentType=="AddProducts")
			{
				FI.UI.Web.StorecheckReport.AddProductsControl control = (FI.UI.Web.StorecheckReport.AddProductsControl)Page.LoadControl("AddProductsControl.ascx");
				control._user=this._user;
				control._report=this._report;
				control.ID="AddProdC";
				this.contentsCell.Controls.Add(control);
			}
			else
			{
				FI.UI.Web.StorecheckReport.DesignControl control = (FI.UI.Web.StorecheckReport.DesignControl)Page.LoadControl("DesignControl.ascx");
				control._user=this._user;
				control._report=this._report;
				control.ID="DesignC";
				this.contentsCell.Controls.Add(control);
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



	}
}
