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

namespace FI.UI.Web.WebServices
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class OlapProcessorManager : System.Web.UI.Page	
	{
		private FI.UI.Web.Controls.FIDataTableGrid _gr;

		protected void Page_Load(object sender, System.EventArgs e)
		{							
			LoadGridControl();
		}


		private void LoadGridControl()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.ID="ProcInfoGr";
			_gr.PrimaryKeyColumnArray=new string[] {"TcpPort"}; 
			_gr.ColumnNameArray=new string[] {"Server", "Database", "State", "AllocatedSpan" , "TaskId", "TaskTag"};			
			_gr.ColumnCaptionArray=new string[] {"Server", "Database", "State", "AllocatedSpan" , "TaskId", "TaskTag"};			
			_gr.ColumnWidthArray=new int[] {100, 100, 100, 100 , 150 , 250};
			_gr.EnableCheckBoxes=false;
			_gr.EnableMultipleSelection=false;
			_gr.EnablePages=false;
			//_gr.DefaultPageSize=50;
			_gr.EnableSort=true;
			_gr.EnableFilter=false;
			_gr.InMemory=true;
			_gr.DataSource=FI.BusinessObjects.BusinessFacade.Instance.GetOlapProcessorInfo();
			ControlPanel.Controls.Add(_gr);
			//end loading grid control
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

		protected void btnRefresh_Click(object sender, System.EventArgs e)
		{
			// do nothing, just refresh
		}


	}
}
