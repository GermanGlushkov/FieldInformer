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
	public class MasterDistributionQueue : System.Web.UI.Page	
	{
		private FI.UI.Web.Controls.FIDataTableGrid _gr;
		protected System.Web.UI.HtmlControls.HtmlTableCell cellControls;
		protected System.Web.UI.WebControls.Button btnRemove;
		protected System.Web.UI.WebControls.Button btnEnqueue;
		protected System.Web.UI.WebControls.Button btnSend;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Button btnDeleteDistr;
		protected System.Web.UI.WebControls.Panel ControlPanel;

		private void Page_Load(object sender, System.EventArgs e)
		{							
			LoadGridControl();
		}


		private void LoadGridControl()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.ID="DQueueGr";
			_gr.PrimaryKeyColumnArray=new string[] {"log_id", "distribution_id"}; 
			_gr.ColumnNameArray=new string[] {"distribution_id", "company_name", "user_name", "timestamp" , "status" , "duration", "message" , "rpt_type" , "rpt_name" , "rpt_description", "contact_name" , "contact_email"};
			_gr.ColumnCaptionArray=new string[] {"Id", "Domain", "User", "Timestamp" , "Status", "Duration(sec)", "Message" , "Report Type",  "Report Name" , "Report Descr.", "Contact Name" , "Contact Email"};
			_gr.ColumnWidthArray=new int[] {30, 100, 100, 100 , 50 , 80, 200 , 100 , 150 , 150 , 150 , 150 };
			_gr.EnableCheckBoxes=true;
			_gr.EnableMultipleSelection=true;
			_gr.EnablePages=true;
			_gr.DefaultPageSize=25;
			_gr.EnableSort=true;
			_gr.DataSourceDelegate=new FI.UI.Web.Controls.FIDataTableGrid.GridDataSourceDelegate(GetMasterDistributionQueuePage);
			ControlPanel.Controls.Add(_gr);
			//end loading grid control
		}


		private FI.Common.Data.FIDataTable GetMasterDistributionQueuePage(int CurrentPage, int PageSize, string FilterExpression, string SortExpression)
		{
			//prepare data
			if(SortExpression==null || SortExpression==string.Empty)
				SortExpression="timestamp desc, log_id desc";
			return FI.BusinessObjects.DistributionManager.Instance.GetMasterDistributionQueuePage(CurrentPage , PageSize , FilterExpression , SortExpression);
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
			this.btnEnqueue.Click += new System.EventHandler(this.btnEnqueue_Click);
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			this.btnDeleteDistr.Click += new System.EventHandler(this.btnDeleteDistr_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion


		private void Page_PreRender(object sender, System.EventArgs e)
		{

		}

		private void btnEnqueue_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;

				foreach(string[] keys in pks)
				{
					decimal log_id=decimal.Parse(keys[0]);
					decimal distribution_id=decimal.Parse(keys[1]);

					FI.BusinessObjects.DistributionManager.Instance.EnqueueDistribution(distribution_id);
				}
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;

				foreach(string[] keys in pks)
				{
					decimal log_id=decimal.Parse(keys[0]);
					decimal distribution_id=decimal.Parse(keys[1]);

					FI.BusinessObjects.DistributionManager.Instance.CancelQueuedItem(log_id);
				}
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
			try
			{
				FI.BusinessObjects.DistributionManager.Instance.AsyncSendAllQueuedDistributions();
				System.Threading.Thread.Sleep(1000); // let thread start working
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		private void ShowException(Exception exc)
		{
			lblError.Text=(exc==null ? "" : exc.Message);
		}

		private void btnDeleteDistr_Click(object sender, System.EventArgs e)
		{		
			// delete
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;

				foreach(string[] keys in pks)
				{
					decimal log_id=decimal.Parse(keys[0]);
					decimal distribution_id=decimal.Parse(keys[1]);

					// delete
					FI.BusinessObjects.DistributionManager.Instance.DeleteDistribution(distribution_id);
				}
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

	}
}
