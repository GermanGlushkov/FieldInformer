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

namespace FI.UI.Web.CrystalReport
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//LoadReportTest1();
			LoadReportTest2();
		}


		private void LoadReportTest2()
		{
			CrystalDecisions.CrystalReports.Engine.ReportDocument doc=new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			doc.Load(@"C:\CrystalReport1.rpt");

			this.CrystalReportViewer1.ReportSource=doc;
			this.CrystalReportViewer1.HasSearchButton=false;
			this.CrystalReportViewer1.HasZoomFactorList=false;
			this.CrystalReportViewer1.HasGotoPageButton=false;

			
			/*

			CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition fieldDef=doc.DataDefinition.ParameterFields["ids_param"];
			CrystalDecisions.Shared.ParameterValues vals=new CrystalDecisions.Shared.ParameterValues();

			CrystalDecisions.Shared.ParameterDiscreteValue val=new CrystalDecisions.Shared.ParameterDiscreteValue();
			val.Value=1;
			vals.Add(val);

			val=new CrystalDecisions.Shared.ParameterDiscreteValue();
			val.Value=2;
			vals.Add(val);

			val=new CrystalDecisions.Shared.ParameterDiscreteValue();
			val.Value=3;
			vals.Add(val);

			fieldDef.ApplyCurrentValues(vals);
			


			string connString=@"Server=.;Database=DBSALESPP;User ID=spp;Password=spp";
			System.Data.SqlClient.SqlDataAdapter adapter=new System.Data.SqlClient.SqlDataAdapter("select * from olap_log" , connString);
			System.Data.DataSet dataSet=new DataSet();
			adapter.Fill(dataSet);
			doc.SetDataSource(dataSet);
			*/


			
			CrystalDecisions.Shared.ConnectionInfo conn=new CrystalDecisions.Shared.ConnectionInfo();
			conn.DatabaseName="DBSALESPP";
			conn.UserID="spp";
			conn.Password="spp";

			CrystalDecisions.Shared.TableLogOnInfo tableLogOn=new CrystalDecisions.Shared.TableLogOnInfo();
			tableLogOn.ConnectionInfo=conn;
			
			for(int i=0;i<doc.Database.Tables.Count;i++)
				doc.Database.Tables[i].ApplyLogOnInfo(tableLogOn);
			
			//System.IO.Stream stream=doc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.HTML32);
			//doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat ,  @"c:\test.pdf");
		}


		private void LoadReportTest1()
		{
			CrystalDecisions.CrystalReports.Engine.ReportDocument doc=new CrystalDecisions.CrystalReports.Engine.ReportDocument();
			doc.Load(@"C:\test1.rpt");
			

			this.CrystalReportViewer1.ReportSource=doc;
			this.CrystalReportViewer1.HasSearchButton=false;
			this.CrystalReportViewer1.HasZoomFactorList=false;
			this.CrystalReportViewer1.HasGotoPageButton=false;

			

			CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition fieldDef=doc.DataDefinition.ParameterFields["ids_param"];
			CrystalDecisions.Shared.ParameterValues vals=new CrystalDecisions.Shared.ParameterValues();

			CrystalDecisions.Shared.ParameterDiscreteValue val=new CrystalDecisions.Shared.ParameterDiscreteValue();
			val.Value=1;
			vals.Add(val);

			val=new CrystalDecisions.Shared.ParameterDiscreteValue();
			val.Value=2;
			vals.Add(val);

			val=new CrystalDecisions.Shared.ParameterDiscreteValue();
			val.Value=3;
			vals.Add(val);

			fieldDef.ApplyCurrentValues(vals);




			CrystalDecisions.Shared.ConnectionInfo conn=new CrystalDecisions.Shared.ConnectionInfo();
			conn.DatabaseName="DBSALESPP";
			conn.ServerName=".";
			conn.UserID="spp";
			conn.Password="spp";

			CrystalDecisions.Shared.TableLogOnInfo tableLogOn=new CrystalDecisions.Shared.TableLogOnInfo();
			tableLogOn.ConnectionInfo=conn;

			doc.Database.Tables[0].ApplyLogOnInfo(tableLogOn);

			//System.IO.Stream stream=doc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.HTML32);
			doc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat ,  @"c:\test.pdf");
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
