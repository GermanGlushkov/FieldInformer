namespace FI.UI.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;

	public partial class UserPassChangeControl : System.Web.UI.UserControl
	{


        public FI.BusinessObjects.User _user;
		protected FI.BusinessObjects.Report _reportProxy;
		protected decimal _reportId;
		protected int _reportType;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;


		protected void Page_Load(object sender, System.EventArgs e)
		{
		}





		private void ShowException(Exception exc)
		{
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(exc);

			System.Web.UI.WebControls.Label lbl=new System.Web.UI.WebControls.Label();
			lbl.CssClass="tbl1_err";
			lbl.Text=exc.Message;
			System.Web.UI.WebControls.TableRow row=new System.Web.UI.WebControls.TableRow();
			System.Web.UI.WebControls.TableCell cell=new System.Web.UI.WebControls.TableCell();
			cell.Controls.Add(lbl);
			row.Cells.Add(cell);

			this.ErrTable.Rows.Add(row);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.txtPass1.Text!=this.txtPass2.Text)
					throw new Exception("Passwords don't match");
				
				this._user.Password=txtPass1.Text;
				this._user.SaveUser(this._user);
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}


	}
}
