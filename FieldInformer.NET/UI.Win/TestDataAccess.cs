using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace UI.Win
{
	/// <summary>
	/// Summary description for TestDataAccess.
	/// </summary>
	public class TestDataAccess : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestDataAccess()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		[STAThread] //[MTAThread]
		static void Main() 
		{
			Application.Run(new TestDataAccess());
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(128, 480);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(184, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Start Thread";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(432, 480);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(184, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Abort Thread";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(32, 56);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(720, 80);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "textBox1";
			// 
			// TestDataAccess
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(784, 524);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1,
																		  this.button2,
																		  this.button1});
			this.Name = "TestDataAccess";
			this.Text = "TestDataAccess";
			this.Load += new System.EventHandler(this.TestDataAccess_Load);
			this.ResumeLayout(false);

		}
		#endregion





		private void InitializeRemoting()
		{
			/*
			System.Runtime.Remoting.Channels.Tcp.TcpChannel channel=new System.Runtime.Remoting.Channels.Tcp.TcpChannel();
			System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(channel);

			System.Runtime.Remoting.RemotingConfiguration.RegisterActivatedClientType(typeof(FI.BusinessObjects.User) , "tcp://localhost:8085/FIApplicationServer");
			*/
		}


		
		

		private void button1_Click(object sender, System.EventArgs e)
		{
//			TestClassLibrary1.Class1 dacObj=(TestClassLibrary1.Class1)Activator.GetObject(
//				typeof(TestClassLibrary1.Class1),
//				"tcp://localhost:8089/TestClass");
//			dacObj.Execute("", "" , "" );
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			
		}

		private void TestDataAccess_Load(object sender, System.EventArgs e)
		{
		}


	}
}
