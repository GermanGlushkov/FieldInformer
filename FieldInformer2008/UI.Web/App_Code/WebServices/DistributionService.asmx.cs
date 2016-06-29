using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace FI.UI.Web.WebServices
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	public class DistributionService : System.Web.Services.WebService
	{
		public DistributionService()
		{			
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

        [WebMethod]
        public void ProcessBackgroundJobs(string CompanyNameShort)
        {
            try
            {
                FI.BusinessObjects.DistributionManager.Instance.EnqueueScheduledDistributions(System.DateTime.Now, CompanyNameShort);
                FI.BusinessObjects.DistributionManager.Instance.SendQueuedDistributions(CompanyNameShort);
                FI.BusinessObjects.DistributionManager.Instance.RefreshCachedReports(CompanyNameShort);
            }
            catch (Exception exc)
            {
                Common.LogWriter.Instance.WriteException(exc);
            }
        }

        [WebMethod]
        public void ProcessDistributionJobs(string CompanyNameShort)
        {
            try
            {
                FI.BusinessObjects.DistributionManager.Instance.EnqueueScheduledDistributions(System.DateTime.Now, CompanyNameShort);
                FI.BusinessObjects.DistributionManager.Instance.SendQueuedDistributions(CompanyNameShort);
            }
            catch (Exception exc)
            {
                Common.LogWriter.Instance.WriteException(exc);
            }
        }

		[WebMethod]
		public void SendQueuedDistributions(string CompanyNameShort)
		{
            try
            {
                FI.BusinessObjects.DistributionManager.Instance.SendQueuedDistributions(CompanyNameShort);
            }
            catch (Exception exc)
            {
                Common.LogWriter.Instance.WriteException(exc);
            }
		}

		[WebMethod]
		public void EnqueueScheduledDistributions(string CompanyNameShort)
		{
            try
            {
			    FI.BusinessObjects.DistributionManager.Instance.EnqueueScheduledDistributions(System.DateTime.Now , CompanyNameShort);
            }
            catch (Exception exc)
            {
                Common.LogWriter.Instance.WriteException(exc);
            }
		}

		[WebMethod]
		public void EnqueueScheduledDistributionsWDate(string CompanyNameShort, System.DateTime Date)
		{
            try
            {
			    FI.BusinessObjects.DistributionManager.Instance.EnqueueScheduledDistributions(Date, CompanyNameShort);
            }
            catch (Exception exc)
            {
                Common.LogWriter.Instance.WriteException(exc);
            }
		}

//		[WebMethod]
//		public void EnqueueScheduledDistribution(decimal distributionId)
//		{
//			FI.BusinessObjects.DistributionManager.Instance.EnqueueDistribution(distributionId);
//		}

	}
}
