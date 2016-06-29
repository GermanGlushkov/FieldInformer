using System;
using System.Windows.Forms;


namespace Setup.PlugIn
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class MainClass
	{
		public MainClass()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[STAThread]
		static void Main() 
		{			

			DBConverter.DBConverterForm convForm=new DBConverter.DBConverterForm();
			convForm.ShowDialog();
			return;

			string mode="SERVICE";

			System.Windows.Forms.DialogResult result;

			string exceptionMessage="";

			if(mode.ToUpper()=="SQL")
			{
				SqlSettingsForm form=new SqlSettingsForm();
				result=form.ShowDialog();
				exceptionMessage=form.ExceptionMessage;
			}
			else if(mode.ToUpper()=="SERVICE")
			{
				ServiceSettingsForm form=new ServiceSettingsForm();
				result=form.ShowDialog();
				exceptionMessage=form.ExceptionMessage;
			}
			else if(mode.ToUpper()=="WEB")
			{
				WebSettingsForm form=new WebSettingsForm();
				result=form.ShowDialog();
				exceptionMessage=form.ExceptionMessage;
			}
			else
				throw new Exception("Unknown install mode");
			

			if(result==System.Windows.Forms.DialogResult.Abort)
			{
				throw new Exception(exceptionMessage);
			}

			if(exceptionMessage!="")
				MessageBox.Show(exceptionMessage);
		}
	}
}
