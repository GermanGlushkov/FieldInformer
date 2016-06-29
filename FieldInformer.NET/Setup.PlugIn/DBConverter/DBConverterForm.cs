using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Setup.PlugIn.DBConverter
{
	/// <summary>
	/// Summary description for DBConverterForm.
	/// </summary>
	public class DBConverterForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox txtSqlServerIP;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DBConverterForm()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.txtSqlServerIP = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(152, 112);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Convert";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(8, 112);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(120, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "Olap Server IP";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(8, 80);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(120, 20);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "Sa Password";
			// 
			// txtSqlServerIP
			// 
			this.txtSqlServerIP.Location = new System.Drawing.Point(8, 48);
			this.txtSqlServerIP.Name = "txtSqlServerIP";
			this.txtSqlServerIP.Size = new System.Drawing.Size(120, 20);
			this.txtSqlServerIP.TabIndex = 4;
			this.txtSqlServerIP.Text = "Sql Server IP";
			// 
			// DBConverterForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(240, 148);
			this.Controls.Add(this.txtSqlServerIP);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.button1);
			this.Name = "DBConverterForm";
			this.Text = "DBConverterForm";
			this.Load += new System.EventHandler(this.DBConverterForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			DBConverter.Converter conv=new DBConverter.Converter();
			conv.Convert((int)this.numericUpDown1.Value , this.textBox1.Text, this.txtSqlServerIP.Text , this.textBox2.Text);
			MessageBox.Show("Done");
		}

		private void DBConverterForm_Load(object sender, System.EventArgs e)
		{
			DBConverter.Converter.InitializeRemoting();
		}



	}
}
