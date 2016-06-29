using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Setup.PlugIn
{
	public class WebSettingsForm : System.Windows.Forms.Form
	{

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblCurVesrion;
		private System.Windows.Forms.Label lblPrevVersion;
		private System.Windows.Forms.Label lblCurVersionCapt;
		private System.Windows.Forms.Label lblPrevVersionCapt;
		private System.Windows.Forms.TextBox txtIni;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnIniFileDialog;
		private System.Windows.Forms.OpenFileDialog iniFileDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WebSettingsForm()
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblCurVesrion = new System.Windows.Forms.Label();
			this.lblPrevVersion = new System.Windows.Forms.Label();
			this.lblCurVersionCapt = new System.Windows.Forms.Label();
			this.lblPrevVersionCapt = new System.Windows.Forms.Label();
			this.txtIni = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnIniFileDialog = new System.Windows.Forms.Button();
			this.iniFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(352, 152);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(62, 19);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(352, 128);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(62, 20);
			this.btnOk.TabIndex = 17;
			this.btnOk.Text = "&Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblCurVesrion);
			this.groupBox1.Controls.Add(this.lblPrevVersion);
			this.groupBox1.Controls.Add(this.lblCurVersionCapt);
			this.groupBox1.Controls.Add(this.lblPrevVersionCapt);
			this.groupBox1.Location = new System.Drawing.Point(16, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 90);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Version Information";
			// 
			// lblCurVesrion
			// 
			this.lblCurVesrion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCurVesrion.Location = new System.Drawing.Point(127, 55);
			this.lblCurVesrion.Name = "lblCurVesrion";
			this.lblCurVesrion.Size = new System.Drawing.Size(160, 20);
			this.lblCurVesrion.TabIndex = 9;
			// 
			// lblPrevVersion
			// 
			this.lblPrevVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPrevVersion.Location = new System.Drawing.Point(127, 28);
			this.lblPrevVersion.Name = "lblPrevVersion";
			this.lblPrevVersion.Size = new System.Drawing.Size(160, 20);
			this.lblPrevVersion.TabIndex = 8;
			// 
			// lblCurVersionCapt
			// 
			this.lblCurVersionCapt.Location = new System.Drawing.Point(7, 55);
			this.lblCurVersionCapt.Name = "lblCurVersionCapt";
			this.lblCurVersionCapt.Size = new System.Drawing.Size(113, 20);
			this.lblCurVersionCapt.TabIndex = 7;
			this.lblCurVersionCapt.Text = "Installation Version:";
			// 
			// lblPrevVersionCapt
			// 
			this.lblPrevVersionCapt.Location = new System.Drawing.Point(7, 28);
			this.lblPrevVersionCapt.Name = "lblPrevVersionCapt";
			this.lblPrevVersionCapt.Size = new System.Drawing.Size(113, 21);
			this.lblPrevVersionCapt.TabIndex = 6;
			this.lblPrevVersionCapt.Text = "Existing Version:";
			// 
			// txtIni
			// 
			this.txtIni.Location = new System.Drawing.Point(112, 16);
			this.txtIni.Name = "txtIni";
			this.txtIni.Size = new System.Drawing.Size(266, 20);
			this.txtIni.TabIndex = 11;
			this.txtIni.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 20);
			this.label1.TabIndex = 13;
			this.label1.Text = "INI file path:";
			// 
			// btnIniFileDialog
			// 
			this.btnIniFileDialog.Location = new System.Drawing.Point(384, 16);
			this.btnIniFileDialog.Name = "btnIniFileDialog";
			this.btnIniFileDialog.Size = new System.Drawing.Size(27, 21);
			this.btnIniFileDialog.TabIndex = 12;
			this.btnIniFileDialog.Text = "...";
			this.btnIniFileDialog.Click += new System.EventHandler(this.btnIniFileDialog_Click);
			// 
			// WebSettingsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 188);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtIni);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnIniFileDialog);
			this.Name = "WebSettingsForm";
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		
		public string ExceptionMessage;

		private void SettingsForm_Load(object sender, System.EventArgs e)
		{
			this.lblCurVesrion.Text=VersionBase.LatestVersion.VersionString;
			this.lblPrevVersion.Text="Clean installation";
			this.btnOk.Enabled=false;
		}


		private void btnIniFileDialog_Click(object sender, System.EventArgs e)
		{
			this.iniFileDialog.Filter="INI files (*.INI)|*.INI";
			this.iniFileDialog.ShowDialog();

			this.txtIni.Text=this.iniFileDialog.FileName;

			try
			{
				VersionBase.LatestVersion.IniPath=this.txtIni.Text;
				VersionBase.LatestVersion.LoadConfigWeb();
			}
			catch(Exception exc)
			{
				MessageBox.Show(exc.Message , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
			}

			this.lblPrevVersion.Text=VersionBase.LatestVersion.OverrideVersionString;
			this.btnOk.Enabled=true;
		}


		private void btnOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				VersionBase.LatestVersion.InstallWeb();
			}
			catch(Exception exc)
			{
				this.ExceptionMessage=exc.Message;
				this.DialogResult=DialogResult.Abort;
				return;
			}
			this.DialogResult=DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult result=MessageBox.Show("Cancel installation?" , "Cancel" , MessageBoxButtons.YesNo , MessageBoxIcon.Question);
			if(result==DialogResult.Yes)
			{
				this.ExceptionMessage="Cancelled";
				this.DialogResult=DialogResult.Abort;
				return;
			}
		}

		private void txtPass_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		

		
	}
}
