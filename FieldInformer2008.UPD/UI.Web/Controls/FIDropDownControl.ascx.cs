namespace FI.UI.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Collections.Specialized;

	/// <summary>
	///		Summary description for FIDropDownControl.
	/// </summary>
	public partial class FIDropDownControl : System.Web.UI.UserControl
	{
		public string CssClass=null;
		private ArrayList _captions=new ArrayList(); 		
		private ArrayList _values=new ArrayList(); 		
		private StringCollection _groups=new StringCollection(); 

		public FIDropDownControl()
		{			
			this.EnableViewState=false;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		public void AddGroupItem(string caption)
		{
			AddItem(caption, null, true);
		}

		public void AddItem(string caption, string value)
		{
			AddItem(caption, value, false);
		}

		private void AddItem(string caption, string value, bool isGroup)
		{
			if(caption==null)
				throw new ArgumentNullException();

			_captions.Add(caption);
			_values.Add((value==null ? caption : value));
			if(isGroup)
				_groups.Add(caption);
		}
		
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{			
			base.Render (writer);

			writer.Write("<SELECT name=\"" + this.UniqueID + "\" class=\"" + this.CssClass + "\">");
			for(int i=0;i<_values.Count;i++)
			{
				string val=(string)_values[i];

				if(_groups.Contains(val))
				{
					writer.Write("<OPTGROUP ");					
					writer.WriteAttribute("label", val);
					writer.Write(" />");
				}
				else
				{
					string capt=(string)_captions[i];

					writer.Write("<OPTION");
					if(capt!=val)
						writer.Write(" VALUE=\"" + val.Replace("\"", "\"\"") + "\">");
					else
						writer.Write(">");

					writer.Write(capt);
//					writer.Write("</OPTION>");
				}
			}
			writer.Write("</SELECT>");
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
	}
}
