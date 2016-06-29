using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Threading;

namespace WinServices.Emulate
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
//		private ProcessChannel _channel;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


//			_channel=new ProcessChannel(1, "Global\\Test", 20000);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
			this.btnReceive = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(80, 80);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 20);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(64, 16);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(112, 23);
			this.btnSend.TabIndex = 1;
			this.btnSend.Text = "SendMessage";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// btnReceive
			// 
			this.btnReceive.Location = new System.Drawing.Point(64, 48);
			this.btnReceive.Name = "btnReceive";
			this.btnReceive.Size = new System.Drawing.Size(112, 23);
			this.btnReceive.TabIndex = 2;
			this.btnReceive.Text = "ReceiveMessage";
			this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(80, 104);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "button2";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(80, 136);
			this.button3.Name = "button3";
			this.button3.TabIndex = 4;
			this.button3.Text = "button3";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(80, 168);
			this.button4.Name = "button4";
			this.button4.TabIndex = 5;
			this.button4.Text = "button4";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(233, 319);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnReceive);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
//		[STAThread]
		static void Main() 
		{
            //string path = @"C:\Documents and Settings\German.Glushkov.FIELDFORCE\Desktop\report_result.txt";
            //ReportResult rr = new ReportResult();
            //rr.LoadCellset(System.IO.File.ReadAllText(path, System.Text.Encoding.Unicode));
            //string s = rr.BuildCSV();
            //System.IO.File.WriteAllText(path + ".csv", s, System.Text.Encoding.Unicode);
            //ReportResult.CellsetMember[] colTuple = rr.GetColumnTuple(0);
            //ReportResult.CellsetMember[] rowTuple = rr.GetRowTuple(0);

			Application.Run(new Form1());
		}


		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Button btnReceive;

		private System.Windows.Forms.Button button1;

		

//		GenuineChannelsServer _remotingServer;
		private void Form1_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				// db schema
				FI.DataAccess.DataBase.Instance.VerifyDbSchema();

				// reset system
                FI.DataAccess.OlapSystem sys = new FI.DataAccess.OlapSystem();
				sys.ResetOlapSystem();

				// set realtime priority
				System.Diagnostics.Process proc=System.Diagnostics.Process.GetCurrentProcess();
				proc.PriorityClass=System.Diagnostics.ProcessPriorityClass.RealTime;

				// remoting init
//				_remotingServer=new GenuineChannelsServer();
//				_remotingServer.Initialize();
				System.Runtime.Remoting.RemotingConfiguration.Configure(System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);

				// event logs
				FI.Common.LogWriter.InitCommonEventLogs();
						
			}
			catch(Exception exc)
			{
				FI.Common.LogWriter.Instance.WriteException(exc);
				throw exc;
			}

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
//			object o=FI.DataAccess.OlapServices.ProcessorPool.Instance;
//
//			FI.DataAccess.OlapServices.ProcessorPool.Instance.GetAvailableFromPool("test", "test", Guid.NewGuid().ToString());
//			FI.DataAccess.OlapServices.ProcessorPool.Instance.GetAvailableFromPool("test", "test", Guid.NewGuid().ToString());
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FI.DataAccess.OlapServices.ProcessorPool.Instance.Reset();
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
//			_channel.Send("test", TimeSpan.FromSeconds(1));
		}

		private void btnReceive_Click(object sender, System.EventArgs e)
		{
//			Receive();
			ReceiveInOtherThread();
		}


		private void ReceiveInOtherThread()
		{
			System.Threading.ThreadStart ts=new System.Threading.ThreadStart(Receive);
			System.Threading.Thread t=new System.Threading.Thread(ts);
			t.Start();
		}

		private void Receive()
		{
//			while(true)
//			{
//				string s=_channel.Receive() as string;
//				MessageBox.Show(s + ", received by " + this.GetHashCode());
//			}
		}

        FI.DataAccess.OlapServices.AdomdWrapper _adomd;
        Exception _exc;
        string _dataSource;
        string _catalog;
        string _mdx;

		private void button2_Click(object sender, System.EventArgs e)
		{
            _dataSource="GER-INSPIRON\\SQL2005";
            //_dataSource = "http://10.2.0.1/olap/msmdpump.dll";

            _catalog="AbileReporting";
			_mdx= @"
WITH
SET [Date_Weekly_set] AS '{[Date].[Weekly].[All],[*SET Weekly.2008.Children*],[*SET Weekly.200829.Children*],[*SET Weekly.200828.Children*],[*SET Weekly.200827.Children*],[*SET Weekly.200826.Children*],[*SET Weekly.200825.Children*]}'
SET [*SET Weekly.200825.Children*] AS '{[Date].[Weekly].[Week].&[200825].Children}'
SET [*SET Weekly.200826.Children*] AS '{[Date].[Weekly].[Week].&[200826].Children}'
SET [*SET Weekly.200827.Children*] AS '{[Date].[Weekly].[Week].&[200827].Children}'
SET [*SET Weekly.200828.Children*] AS '{[Date].[Weekly].[Week].&[200828].Children}'
SET [*SET Weekly.200829.Children*] AS '{[Date].[Weekly].[Week].&[200829].Children}'
SET [*SET Weekly.2008.Children*] AS '{[Date].[Weekly].[Year].&[2008].Children}'
SET [Date_Weekly_set_wcalc] AS '{{[Date_Weekly_set]}}'
SET [Proxy Packet Type_Proxy Packet Type_set] AS '{[Proxy Packet Type].[Proxy Packet Type].[All],[*SET Proxy Packet Type.All.Children*]}'
SET [*SET Proxy Packet Type.All.Children*] AS '{[Proxy Packet Type].[Proxy Packet Type].[All].Children}'
SET [Proxy Packet Type_Proxy Packet Type_set_wcalc] AS '{{[Proxy Packet Type_Proxy Packet Type_set]}}'
SET [Date_Monthly_set] AS '{[Date].[Monthly].[All],[*SET Monthly.2008.Children*],[*SET Monthly.2008Q1.Children*],[*SET Monthly.200801.Children*],[*SET Monthly.2008Q4.Children*],[*SET Monthly.200810.Children*],[*SET Monthly.200811.Children*],[*SET Monthly.200812.Children*],[*SET Monthly.2008Q3.Children*],[*SET Monthly.200809.Children*],[*SET Monthly.200808.Children*],[*SET Monthly.200807.Children*]}'
SET [*SET Monthly.200807.Children*] AS '{[Date].[Monthly].[Month].&[200807].Children}'
SET [*SET Monthly.200808.Children*] AS '{[Date].[Monthly].[Month].&[200808].Children}'
SET [*SET Monthly.200809.Children*] AS '{[Date].[Monthly].[Month].&[200809].Children}'
SET [*SET Monthly.2008Q3.Children*] AS '{[Date].[Monthly].[Quarter].&[2008Q3].Children}'
SET [*SET Monthly.200812.Children*] AS '{[Date].[Monthly].[Month].&[200812].Children}'
SET [*SET Monthly.200811.Children*] AS '{[Date].[Monthly].[Month].&[200811].Children}'
SET [*SET Monthly.200810.Children*] AS '{[Date].[Monthly].[Month].&[200810].Children}'
SET [*SET Monthly.2008Q4.Children*] AS '{[Date].[Monthly].[Quarter].&[2008Q4].Children}'
SET [*SET Monthly.200801.Children*] AS '{[Date].[Monthly].[Month].&[200801].Children}'
SET [*SET Monthly.2008Q1.Children*] AS '{[Date].[Monthly].[Quarter].&[2008Q1].Children}'
SET [*SET Monthly.2008.Children*] AS '{[Date].[Monthly].[Year].&[2008].Children}'
SET [Date_Monthly_set_wcalc] AS '{{[Date_Monthly_set]}}'
SET [Measures_set] AS '{[Measures].[Packet Count],[Measures].[Subscription Distinct Count],[Measures].[Data Size Kb],[Measures].[Data Size Mb],[Measures].[Data Per Packet Kb],[Measures].[Data Per Subscription Kb],[Measures].[Packets Per Subscription]}'
SET [Measures_set_wcalc] AS '{{[Measures_set]}}'
SET [Time Of Day_Time Of Day_set] AS '{[Time Of Day].[Time Of Day].[All],[*SET Time Of Day.All.Children*],[*SET Time Of Day.13.Children*],[*SET Time Of Day.14.Children*],[*SET Time Of Day.15.Children*],[*SET Time Of Day.16.Children*]}'
SET [*SET Time Of Day.16.Children*] AS '{[Time Of Day].[Time Of Day].[Hour].&[16].Children}'
SET [*SET Time Of Day.15.Children*] AS '{[Time Of Day].[Time Of Day].[Hour].&[15].Children}'
SET [*SET Time Of Day.14.Children*] AS '{[Time Of Day].[Time Of Day].[Hour].&[14].Children}'
SET [*SET Time Of Day.13.Children*] AS '{[Time Of Day].[Time Of Day].[Hour].&[13].Children}'
SET [*SET Time Of Day.All.Children*] AS '{[Time Of Day].[Time Of Day].[All].Children}'
SET [Time Of Day_Time Of Day_set_wcalc] AS '{{[Time Of Day_Time Of Day_set]}}'
MEMBER [Proxy Subscription].[Proxy Subscription].[*AGGREGATE*] AS 'AGGREGATE({[Proxy Subscription].[Proxy Subscription].[Customer].&[3C Asset Management],[Proxy Subscription].[Proxy Subscription].[Customer].&[AFONDOCONSULTING],[Proxy Subscription].[Proxy Subscription].[Customer].&[Controlmatic],[Proxy Subscription].[Proxy Subscription].[Customer].&[CRESCOM],[Proxy Subscription].[Proxy Subscription].[Customer].&[DEFERON_SERVICES],[Proxy Subscription].[Proxy Subscription].[Customer].&[DEMO],[Proxy Subscription].[Proxy Subscription].[Customer].&[FIELDFORCE],[Proxy Subscription].[Proxy Subscription].[Customer].&[FinnPro],[Proxy Subscription].[Proxy Subscription].[Customer].&[Gui],[Proxy Subscription].[Proxy Subscription].[Customer].&[JAVERDEL],[Proxy Subscription].[Proxy Subscription].[Customer].&[MEPCO],[Proxy Subscription].[Proxy Subscription].[Customer].&[MIRASYS],[Proxy Subscription].[Proxy Subscription].[Customer].&[POHJOLAN LIIKENNE],[Proxy Subscription].[Proxy Subscription].[Customer].&[Satama],[Proxy Subscription].[Proxy Subscription].[Customer].&[SOFTAVENUE],[Proxy Subscription].[Proxy Subscription].[Customer].&[SOKONET],[Proxy Subscription].[Proxy Subscription].[Customer].&[TAMRO],[Proxy Subscription].[Proxy Subscription].[Customer].&[TIOLAT]})' , SOLVE_ORDER=-100, SCOPE_ISOLATION=CUBE
 SELECT 
 NON EMPTY  HIERARCHIZE({{[Date_Weekly_set_wcalc]}*{[Proxy Packet Type_Proxy Packet Type_set_wcalc]}}) ON Columns,
 NON EMPTY  HIERARCHIZE({{[Date_Monthly_set_wcalc]}*{[Measures_set_wcalc]}*{[Time Of Day_Time Of Day_set_wcalc]}}) ON Rows
 FROM [ProxyLog]
 WHERE ([Proxy Subscription].[Proxy Subscription].[*AGGREGATE*])
";

            Thread t=new Thread(new ThreadStart(ExecuteCellset));
            t.Start();

            Thread.Sleep(150000);

            if (_exc != null)
                throw _exc;

            CancelExecuteCellset();
		}

        private void ExecuteCellset()
        {
            try
            {
                _exc = null;
                _adomd = new FI.DataAccess.OlapServices.AdomdWrapper();
                _adomd.BuildCellset(_dataSource, _catalog, _mdx);
            }
            catch (Exception exc)
            {
                _exc = exc;
            }
        }

        private void CancelExecuteCellset()
        {
            if (_adomd == null)
                return;

            _adomd.CancelCommand();
            _adomd = null;
        }

		private void button3_Click(object sender, System.EventArgs e)
		{		
			FI.DataAccess.OlapServices.AdomdWrapper obj=new FI.DataAccess.OlapServices.AdomdWrapper();
			MessageBox.Show(
				obj.GetSchemaMembers("GER-INSPIRON", "Foodmart 2000", "Sales", new string[] { "[Measures].[Unit Sales]", "[Gender].[All Gender].[F]"})
				);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{		
			string openNodes=null;

			FI.DataAccess.OlapServices.AdomdWrapper obj=new FI.DataAccess.OlapServices.AdomdWrapper();
			string ret=
				obj.GetReportSchemaXml("10.3.0.247", "DBSALESPP", "Virtual", ref openNodes);
			MessageBox.Show(ret);
		}
	}
}
