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

namespace FI.UI.Web
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class ColorScheme : FI.UI.Web.PageBase	
	{
		protected System.Web.UI.WebControls.Panel ControlPanel;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.RadioButton radio;


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			
			LoadTabs();
		}


		protected override void Render(HtmlTextWriter writer)
		{
			this.radioStyle1.Checked=(_user.CssStyle==1);
			this.radioStyle2.Checked=(_user.CssStyle==2);
			this.radioStyle3.Checked=(_user.CssStyle==3);

			base.Render (writer);
		}


		private void LoadTabs()
		{
			int id ;

			FI.UI.Web.Controls.Tabs.TabView tv=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TabView1");
			
			id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Administrative_Tools);

			tv.AddTab(id , "Users" , Request.ApplicationPath + "/UserList.aspx" , false , false);
			tv.AddTab(id , "Contacts" , Request.ApplicationPath + "/ContactList.aspx", false , false);
			tv.AddTab(id , "Distribution Log" , Request.ApplicationPath + "/DistributionLog.aspx" , false , false);
			tv.AddTab(id , "Distribution Queue" , Request.ApplicationPath + "/DistributionQueue.aspx" , false , false);
			tv.AddTab(id , "Color Scheme" , Request.ApplicationPath + "/ColorScheme.aspx" , true , false);
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

		}
		#endregion

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			if(this.radioStyle1.Checked)
				_user.CssStyle=1;
			else if(this.radioStyle2.Checked)
				_user.CssStyle=2;
			else if(this.radioStyle3.Checked)
				_user.CssStyle=3;
			
			_user.SaveUser(_user); // save itself
			Server.Transfer("ColorScheme.aspx", false); // reload
		}

		public override void ShowException(Exception exc)
		{
			Label lbl=new Label();
			lbl.CssClass="tbl1_err";
			lbl.Text=exc.Message;
			this.cellControls.Controls.Add(lbl);
		}


	}
}
