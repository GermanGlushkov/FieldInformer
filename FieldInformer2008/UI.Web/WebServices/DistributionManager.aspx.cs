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
	public partial class DistributionManager : System.Web.UI.Page	
	{
		private FI.UI.Web.Controls.FIDataTableGrid _gr;

		protected void Page_Load(object sender, System.EventArgs e)
		{			
			CreateGridControl();			
		}


		private void CreateGridControl()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gr.ID="DQueueGr";
			_gr.PrimaryKeyColumnArray=new string[] {"Id", "UserId", "ReportId", "ReportTypeCode"}; 
			_gr.ColumnNameArray=
				new string[] {
								"LastTimestamp", "CheckStatus", "LastStatus", "LastMessage", "IsQueued",								 	
								"ScheduleType", "ScheduleValue",
								"Domain", "UserName", 
								 "ReportType", "ReportName" , "ReportDescr",	
								 "Contact" , "ContactEmail" , 
								"OkCount", "OkLastDuration", "OkAvgDuration", 
								"CancelCount", "CancelAvgDuration",
								"ErrorCount"
				};
			_gr.ColumnCaptionArray=_gr.ColumnNameArray;
			_gr.ColumnWidthArray=
				new int[] {
							  100, 100, 80 , 200, 80 , 
							  80, 80,
							  80 , 80 ,
							  80, 100 , 200 ,
							  80, 80 , 
							  80, 100 , 100,
							  80, 100 ,
							  80
						  };
			_gr.EnableCheckBoxes=true;
			_gr.EnableMultipleSelection=true;
			_gr.EnablePages=true;
			_gr.DefaultPageSize=100;
			_gr.EnableSort=true;
			_gr.InMemory=true;
			if(_gr.Sort.Count==0)
			{
				//_gr.Sort.Add("CheckStatus", "DESC");
                _gr.Sort.Add("LastTimestamp", "DESC");
                _gr.Sort.Add("IsQueued", "DESC");
			}

			ControlPanel.Controls.Add(_gr);
			//end loading grid control
		}


		private FI.Common.Data.FIDataTable GetDistributionInfo()
		{
			return FI.BusinessObjects.DistributionManager.Instance.GetDistributionInfo(false);
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
			_gr.DataSource=GetDistributionInfo();
		}

		protected void btnEnqueue_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;

				foreach(string[] keys in pks)
				{
					decimal id=decimal.Parse(keys[0]);
					FI.BusinessObjects.DistributionManager.Instance.EnqueueDistribution(id);
				}
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		protected void btnRemove_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;

				foreach(string[] keys in pks)
				{
					decimal id=decimal.Parse(keys[0]);
					FI.BusinessObjects.DistributionManager.Instance.CancelDistributionQueueItems(id);
				}
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		protected void btnSend_Click(object sender, System.EventArgs e)
		{
			try
			{
				FI.BusinessObjects.DistributionManager.Instance.AsyncSendAllQueuedDistributions(false);
				System.Threading.Thread.Sleep(3000); // let thread start working
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

        protected void btnForceSend_Click(object sender, System.EventArgs e)
        {
            try
            {
                FI.BusinessObjects.DistributionManager.Instance.AsyncSendAllQueuedDistributions(true);
                System.Threading.Thread.Sleep(3000); // let thread start working
            }
            catch (Exception exc)
            {
                ShowException(exc);
            }
        }

		private void ShowException(Exception exc)
		{
			lblError.Text=(exc==null ? "" : exc.Message);
		}

		protected void btnDeleteDistr_Click(object sender, System.EventArgs e)
		{		
			// delete
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;
				foreach(string[] keys in pks)
				{
					decimal id=decimal.Parse(keys[0]);					
					FI.BusinessObjects.DistributionManager.Instance.DeleteDistribution(id);
				}
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			// do nothing, refrersh page
		}

		protected void btnDetails_Click(object sender, System.EventArgs e)
		{						
			try
			{
				System.Collections.ArrayList pks=_gr.SelectedPrimaryKeys;
				if(pks==null || pks.Count==0)
					return;

				string[] keys=(string[])pks[0];
				decimal userId=decimal.Parse(keys[1]);	
				decimal rptId=decimal.Parse(keys[2]);	
				int rptType=int.Parse(keys[3]);		
				
				Server.Transfer(
					string.Format("DistributionDetails.aspx?userId={0}&rptId={1}&rptType={2}", userId, rptId, rptType)
					);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}

		}


	}
}
