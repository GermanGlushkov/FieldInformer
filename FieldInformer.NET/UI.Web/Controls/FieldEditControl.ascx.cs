namespace FI.UI.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for FieldEditControl.
	/// </summary>
	public abstract class FieldEditControl : System.Web.UI.UserControl
	{
		public enum ControlTypeEnum
		{
			TextBox,
			Password,
			DropDownList
		}

		private string _editValue="";
		private string _cssClass="";
		private bool _enabled=true;
		public System.Web.UI.WebControls.ListItem[] ListItems;
		private System.Web.UI.WebControls.Unit _width=System.Web.UI.WebControls.Unit.Pixel(0);
		private ControlTypeEnum _controlType=ControlTypeEnum.TextBox;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(_controlType==ControlTypeEnum.TextBox)
			{
				System.Web.UI.WebControls.TextBox control=new System.Web.UI.WebControls.TextBox();
				control.CssClass=this.CssClass;
				control.Width=this.Width;
				control.Enabled=this.Enabled;
				control.Text=this._editValue.ToString();
				this.Controls.Add(control);
			}
			else if(_controlType==ControlTypeEnum.Password)
			{
				System.Web.UI.WebControls.TextBox control=new System.Web.UI.WebControls.TextBox();
				control.CssClass=this.CssClass;
				control.Width=this.Width;
				control.Enabled=this.Enabled;
				control.TextMode=System.Web.UI.WebControls.TextBoxMode.Password;
				if(this._editValue.ToString()!="")
					control.Attributes.Add("VALUE", "*********");
				control.Text=this._editValue.ToString();
				this.Controls.Add(control);
			}
			else if(_controlType==ControlTypeEnum.DropDownList)
			{
				System.Web.UI.WebControls.DropDownList control=new System.Web.UI.WebControls.DropDownList();
				control.CssClass=this.CssClass;
				control.Width=this.Width;
				control.Enabled=this.Enabled;
				if(ListItems!=null)
				{
					for(int i=0; i<ListItems.Length;i++)
					{
						control.Items.Add(ListItems[i]);
						if(ListItems[i].Value==this._editValue)
							ListItems[i].Selected=true;
					}
				}
				this.Controls.Add(control);
			}
		}

		public ControlTypeEnum ControlType
		{
			get { return _controlType; }
			set { _controlType=value;}
		}

		public string EditValue
		{
			get 
			{ 
				if(_controlType==ControlTypeEnum.TextBox)
				{
					if(this.Controls.Count>0)
						_editValue=((TextBox)this.Controls[0]).Text;
				}
				else if(_controlType==ControlTypeEnum.Password)
				{
					if(this.Controls.Count>0)
					{
						string val=((TextBox)this.Controls[0]).Text;
						if(val!="*********")
							_editValue=val;
					}
				}
				else if(_controlType==ControlTypeEnum.DropDownList)
				{
					if(this.Controls.Count>0)
						_editValue=((DropDownList)this.Controls[0]).SelectedItem.Value;
				}

				return _editValue; 
			}
			set 
			{ 
				_editValue=value;
			}
		}

		public string CssClass
		{
			get { return _cssClass; }
			set { _cssClass=value;}
		}

		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled=value;}
		}

		public System.Web.UI.WebControls.Unit Width
		{
			get { return _width; }
			set { _width=value;}
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
