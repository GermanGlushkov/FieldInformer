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
	public partial class DistributionLog : FI.UI.Web.PageBase	
	{
		private FI.UI.Web.Controls.FIDataTableGrid _gr;
		private string _sessionContent;

		protected void Page_Load(object sender, System.EventArgs e)
		{

			LoadTabs();
			LoadGridControl();

		}



		private void LoadTabs()
		{
			int id ;

			FI.UI.Web.Controls.Tabs.TabView tv=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TabView1");

			id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Administrative_Tools);

			tv.AddTab(id , "Users" , Request.ApplicationPath + "/UserList.aspx" , false , false);
			tv.AddTab(id , "Contacts" , Request.ApplicationPath + "/ContactList.aspx", false , false);
			tv.AddTab(id , "Distribution Log" , Request.ApplicationPath + "/DistributionLog.aspx" , true , false);
			tv.AddTab(id , "Distribution Queue" , Request.ApplicationPath + "/DistributionQueue.aspx" , false , false);
			tv.AddTab(id , "Color Scheme" , Request.ApplicationPath + "/ColorScheme.aspx" , false , false);
		}



		private void LoadGridControl()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_gr.ID="DLogGr";
			_gr.ColumnNameArray=new string[] {"timestamp" , "status" , "duration", "message" , "rpt_type" , "rpt_name" , "rpt_description", "contact_name" , "contact_email"};
			_gr.ColumnCaptionArray=new string[] {"Timestamp" , "Status", "Duration(sec)", "Message" , "Report Type",  "Report Name" , "Report Descr.", "Contact Name" , "Contact Email"};
			_gr.ColumnWidthArray=new int[] {100 , 50 , 80, 200 , 100 , 150 , 150 , 150 , 150 };
			_gr.EnableSort=true;
			_gr.EnableCheckBoxes=false;
			_gr.EnablePages=true;
			_gr.DefaultPageSize=25;
			_gr.DataSourceDelegate=new FI.UI.Web.Controls.FIDataTableGrid.GridDataSourceDelegate(GetDistributionLogPage);
			ControlPanel.Controls.Add(_gr);
			//end loading grid control
		}


		private FI.Common.Data.FIDataTable GetDistributionLogPage(int CurrentPage, int PageSize, string FilterExpression, string SortExpression)
		{
			//prepare data
			if(SortExpression==null || SortExpression==string.Empty)
				SortExpression="log_id desc";
			return _user.DistributionSystem.GetDistributionLogPage(CurrentPage , PageSize , FilterExpression , SortExpression);
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


		protected void Page_PreRender(object sender, System.EventArgs e)
		{

		}

	}
}
