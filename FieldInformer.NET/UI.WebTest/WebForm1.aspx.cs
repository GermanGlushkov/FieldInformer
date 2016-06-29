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

namespace UI.WebTest
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Table Table1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			FI.BusinessObjects.User usr=new FI.BusinessObjects.User();
			usr.Authenticate(4);

			System.DateTime time1=System.DateTime.Now;
			OlapReport rpt=(OlapReport)usr.ReportSystem.GetReport(61 , typeof(OlapReport) , true);
			//rpt.AddToExecuteQueue();
			System.DateTime time2=System.DateTime.Now;

			Button1.Text=((System.TimeSpan)time2.Subtract(time1)).ToString();
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			Table1.Rows.Clear();
			OlapQueue q=OlapQueue.Instance;

			for(int i=0;i<q.Count;i++)
			{
				TableRow row=new TableRow();
				Table1.Rows.Add(row);

				TableCell cell;
				cell=new TableCell();
				row.Cells.Add(cell);
				cell.Text=q[i].ID.ToString();

				cell=new TableCell();
				row.Cells.Add(cell);
				cell.Text=q[i].Report.Name;

				cell=new TableCell();
				row.Cells.Add(cell);
				cell.Text=q[i].Status.ToString();

				cell=new TableCell();
				row.Cells.Add(cell);
				cell.Text=q[i].ExecutionStarted.ToLongTimeString();
			}
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			FI.BusinessObjects.User usr=new FI.BusinessObjects.User();
			usr.Authenticate(4);

			System.DateTime time1=System.DateTime.Now;
			OlapReport rpt=(OlapReport)usr.ReportSystem.GetReport(61 , typeof(OlapReport) , true);
			for(int i=0;i<100;i++)
			{
				rpt.AddToExecuteQueue();
			}
			System.DateTime time2=System.DateTime.Now;

			Button1.Text=((System.TimeSpan)time2.Subtract(time1)).ToString();
		}
	}
}
