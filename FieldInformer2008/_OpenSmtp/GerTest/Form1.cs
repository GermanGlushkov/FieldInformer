using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GerTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
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
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(104, 48);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 267);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			string s=this.GetTestReportString();
			SendMessage(s);

//			string s1=OpenSmtp.Mail.MailEncoder.ConvertToQP(s, "UTF-8");
//			SendMessage(s1);
//
//			s1=s1.Substring(s1.Length-100);
//
//			s1=null;
		}


		private void SendMessage(string htmlBody)
		{
			// message obejct
			OpenSmtp.Mail.MailMessage msg=new OpenSmtp.Mail.MailMessage();
			msg.From=new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com");

			msg.To.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger1"));
			msg.To.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger2"));
			msg.To.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger3"));
			msg.To.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger4"));
			msg.To.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger5"));

			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger1"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger2"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger3"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger4"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger5"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger1"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger2"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger3"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger4"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger5"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger1"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger2"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger3"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger4"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger5"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger1"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger2"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger3"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger4"));
			msg.CC.Add(new OpenSmtp.Mail.EmailAddress("german.glushkov@fieldforce.com", "Ger5"));

			msg.Subject="OpenSmtp test";							

			msg.HtmlBody=htmlBody;

			OpenSmtp.Mail.SmtpConfig.LogToText=false;

			OpenSmtp.Mail.Smtp smtp=new OpenSmtp.Mail.Smtp();
			smtp.SendTimeout=60;
			smtp.Host="10.0.0.244";
//			if(FI.Common.AppConfig.SmtpUserName!=null && FI.Common.AppConfig.SmtpUserName!="")
//			{						
//				smtp.Username=FI.Common.AppConfig.SmtpUserName;
//				smtp.Password=FI.Common.AppConfig.SmtpPassword;
//			}
			smtp.SendMail(msg);
		}


		private string GetTestReportString()
		{
			string s=null;
			StreamReader sr=new StreamReader(@"..\..\OlapReport_21620.HTML", System.Text.Encoding.Unicode, true);
			if(sr!=null)
			{
				s=sr.ReadToEnd();
				sr.Close();
			}

			return s;
		}
	}
}
