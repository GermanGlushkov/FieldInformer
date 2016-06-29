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
	public abstract class Tab : System.Web.UI.UserControl
	{

		protected System.Web.UI.WebControls.Panel ctrlPanel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			
			//load images
			for(int i=0; i<_imageUrls.Count ;i++)
			{
				System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
				img.EnableViewState=false;
				img.ImageUrl=(string)_imageUrls[i];
				Table.Rows[1].Cells[1].Controls.Add(img);
			}
			//


			if (IsActive==true)
			{
				if (IsRoot==true)
				{
					leftImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/left_root_act.gif";
					rightImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/right_root_act.gif";
					Table.Rows[0].Cells[0].CssClass="t_white";
					Table.Rows[0].Cells[1].CssClass="t_white";
					Table.Rows[0].Cells[2].CssClass="t_white";
				}
				else
				{
					leftImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/left_act.gif";
					rightImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/right_act.gif";
					Table.Rows[0].Cells[0].CssClass="t_act";
					Table.Rows[0].Cells[1].CssClass="t_white";
					Table.Rows[0].Cells[2].CssClass="t_act";
				}

				
				if(IsButton)
				{
					Button btn=new Button();
					btn.CssClass="t_act";
					btn.Text=Caption.Trim();
					btn.ID=Href;
					Table.Rows[1].Cells[1].Controls.Add(btn);
				}
				else
				{
					HyperLink hl=new HyperLink();
					hl.CssClass="t_act";
					hl.Text=Caption.Trim();
					hl.NavigateUrl=Href;
					Table.Rows[1].Cells[1].Controls.Add(hl);
				}
								
				Table.Rows[1].Cells[0].CssClass="t_act";
				Table.Rows[1].Cells[1].CssClass="t_act";
				Table.Rows[1].Cells[2].CssClass="t_act";

				Table.Rows[2].Cells[0].CssClass="t_act";
				Table.Rows[2].Cells[1].CssClass="t_act";
				Table.Rows[2].Cells[2].CssClass="t_act";

			}
			else
			{
				if (IsRoot==true)
				{
					leftImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/left_root_notact.gif";
					rightImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/right_root_notact.gif";
					Table.Rows[0].Cells[0].CssClass="t_white";
					Table.Rows[0].Cells[1].CssClass="t_white";
					Table.Rows[0].Cells[2].CssClass="t_white";
				}
				else
				{
					leftImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/left_notact.gif";
					rightImage.ImageUrl="images/style" + CssStyleNum.ToString() + "/right_notact.gif";
					Table.Rows[0].Cells[0].CssClass="t_act";
					Table.Rows[0].Cells[1].CssClass="t_white";
					Table.Rows[0].Cells[2].CssClass="t_act";
				}

				if(IsButton)
				{
					Button btn=new Button();
					btn.CssClass="t_nact";
					btn.Text=Caption.Trim();
					btn.ID=Href;
					Table.Rows[1].Cells[1].Controls.Add(btn);
				}
				else
				{
					HyperLink hl=new HyperLink();
					hl.CssClass="t_nact";
					hl.Text=Caption.Trim();
					hl.NavigateUrl=Href;
					Table.Rows[1].Cells[1].Controls.Add(hl);
				}

				Table.Rows[1].Cells[0].CssClass="t_act";
				Table.Rows[1].Cells[1].CssClass="t_nact";
				Table.Rows[1].Cells[2].CssClass="t_act";

				Table.Rows[2].Cells[0].CssClass="t_white";
				Table.Rows[2].Cells[1].CssClass="t_white";
				Table.Rows[2].Cells[2].CssClass="t_white";
				
				
			}

			Table.Rows[3].Cells[0].CssClass="t_act";
			Table.Rows[3].Cells[1].CssClass="t_act";
			Table.Rows[3].Cells[2].CssClass="t_act";

		}

		public void AddImage(string Url)
		{
			_imageUrls.Add(Url);
		}

		public bool IsActive=false;
		public bool IsRoot=false;
		public bool IsButton=false;
		public int Id=0;
		public string Href="";
		public string Caption="Tab";
		public byte CssStyleNum=0;
		
		private System.Collections.ArrayList _imageUrls=new System.Collections.ArrayList();

		protected System.Web.UI.WebControls.Table Table;
		protected System.Web.UI.WebControls.HyperLink   linkButton;
		protected System.Web.UI.WebControls.Image leftImage;
		protected System.Web.UI.WebControls.Image rightImage;
		public System.Web.UI.WebControls.Panel Panel;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
