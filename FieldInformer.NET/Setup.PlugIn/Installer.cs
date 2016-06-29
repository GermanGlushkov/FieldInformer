using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace Setup.PlugIn
{
	/// <summary>
	/// Summary description for Installer.
	/// </summary>
	[RunInstaller(true)]
	public class Installer : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Installer()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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

		public override void Install(IDictionary stateSaver)
		{
			
			System.Windows.Forms.DialogResult result;
			string exceptionMessage="";

			if(this.Context.Parameters["Mode"].ToUpper()=="SQL")
			{
				SqlSettingsForm form=new SqlSettingsForm();
				result=form.ShowDialog();
				exceptionMessage=form.ExceptionMessage;
			}
			else if(this.Context.Parameters["Mode"].ToUpper()=="SERVICE")
			{
				ServiceSettingsForm form=new ServiceSettingsForm();
				result=form.ShowDialog();
				exceptionMessage=form.ExceptionMessage;
			}
			else if(this.Context.Parameters["Mode"].ToUpper()=="WEB")
			{
				WebSettingsForm form=new WebSettingsForm();
				result=form.ShowDialog();
				exceptionMessage=form.ExceptionMessage;
			}
			else
				throw new InstallException("Unknown install mode");
			

			if(result==System.Windows.Forms.DialogResult.Abort)
			{
				throw new InstallException(exceptionMessage);
			}

			base.Install (stateSaver);
		}


		private void Log(string Text)
		{
			System.IO.StreamWriter sw=System.IO.File.AppendText(@"c:\test_log.txt");
			sw.WriteLine(Text);
			sw.Flush();
			sw.Close();
		}



		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
