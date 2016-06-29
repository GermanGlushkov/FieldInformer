namespace FI.UI.Web.Controls.Tabs
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Tab.
	/// </summary>
	public partial  class Space : System.Web.UI.UserControl
	{
		public byte CssStyleNum=0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			cornerImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/tab_corn.gif";
			cornerImage.CssClass="t_act";

			if (IsRoot==true)
			{
				Table.Rows[0].Cells[0].CssClass="t_white";
				Table.Rows[0].Cells[1].CssClass="t_white";

				if(EnableLogoutButton==true && IsRoot==true)
				{
					System.Web.UI.WebControls.Literal capt=new System.Web.UI.WebControls.Literal();
					capt.Text=WelcomeNote + "&nbsp;&nbsp;";
					System.Web.UI.HtmlControls.HtmlAnchor anch=new System.Web.UI.HtmlControls.HtmlAnchor();
					anch.HRef=LogoutHref;
					System.Web.UI.HtmlControls.HtmlImage img=new System.Web.UI.HtmlControls.HtmlImage();
					img.Alt="Logout";
					img.Src="images/logout.gif";
					img.Border=0;
					anch.Controls.Add(img);
					Table.Rows[0].Cells[0].HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Right;
					Table.Rows[0].Cells[0].Controls.Add(capt);
					Table.Rows[0].Cells[1].Controls.Add(anch);
				}

				
				Table.Rows[0].Cells[2].CssClass="t_white";

			}
			else
			{
				Table.Rows[0].Cells[0].CssClass="t_act";
				Table.Rows[0].Cells[1].CssClass="t_act";
				Table.Rows[0].Cells[2].CssClass="t_act";
			}

			Table.Rows[1].Cells[0].CssClass="t_white";
			Table.Rows[1].Cells[1].CssClass="t_white";
			Table.Rows[1].Cells[2].CssClass="t_white";

			Table.Rows[2].Cells[0].CssClass="t_act";
			Table.Rows[2].Cells[1].CssClass="t_act";
			Table.Rows[2].Cells[2].CssClass="t_act";

		}

		public bool IsRoot=false;
		public bool EnableLogoutButton=false;
		public string LogoutHref="";
		public string WelcomeNote="";


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
