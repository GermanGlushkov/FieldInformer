using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

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

		private void button2_Click(object sender, System.EventArgs e)
		{
			string mdx=@"
select
    { [Measures].[Units Shipped], [Measures].[Units Ordered] } on columns,
    NON EMPTY [Store].[Store Name].members on rows
from Warehouse
";

			FI.DataAccess.OlapServices.AdomdWrapper obj=new FI.DataAccess.OlapServices.AdomdWrapper();
			MessageBox.Show(
				obj.BuildCellset("GER-INSPIRON", "Foodmart 2000", mdx)
				);
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
