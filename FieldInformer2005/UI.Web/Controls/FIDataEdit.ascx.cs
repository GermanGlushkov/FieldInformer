namespace FI.UI.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for FIDataEdit.
	/// </summary>
	public abstract class FIDataEdit : System.Web.UI.UserControl
	{
		
		public ModeEnum Mode;
		public object CurrentObject;
		public FieldEditControl[] ControlsArray;
		public string[] PropertiesArray;
		public string[] CaptionsArray;
		public int LabelsWidth=100;
		private string errorText="";

		protected System.Web.UI.WebControls.Label CaptionLabel;
		protected System.Web.UI.WebControls.Table EditTable;
		protected System.Web.UI.WebControls.Button InsertButton;
		protected System.Web.UI.WebControls.Button UpdateButton;
		protected System.Web.UI.WebControls.Button DeleteButton;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.WebControls.Label ErrorLabel;

		public event System.Web.UI.WebControls.CommandEventHandler InsertButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler UpdateButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler DeleteButtonClick;
		public event System.Web.UI.WebControls.CommandEventHandler CancelButtonClick;

		public enum ModeEnum
		{
		Insert=0,
		Edit=1,
		Delete=2
		};

		private void Page_Load(object sender, System.EventArgs e)
		{	
			CreatePage();
		}

		private void CreatePage()
		{
			CaptionLabel.Text=Mode.ToString() + " Record:";

			if(Mode==ModeEnum.Insert)
			{
				UpdateButton.Visible=false;
				DeleteButton.Visible=false;
			}
			else if(Mode==ModeEnum.Edit)
			{
				InsertButton.Visible=false;
				DeleteButton.Visible=false;
			}
			else if(Mode==ModeEnum.Delete)
			{
				InsertButton.Visible=false;
				UpdateButton.Visible=false;
			}


			EditTable.CellSpacing=0;
			EditTable.CellPadding=0;


			Label	label;
			TableCell cell;
			TableRow row;
			
			for(int counter=0; counter<PropertiesArray.Length; counter++)
			{			
				row=new TableRow();
				
				cell=new TableCell();
				label=new Label();
				label.Text=CaptionsArray[counter];
				cell.Controls.Add(label);
				cell.Width=Unit.Pixel(LabelsWidth);
				cell.CssClass="tbl1_edit_lbl";
				cell.Height=Unit.Pixel(20);
				row.Cells.Add(cell);

				//empty cell
				cell=new TableCell();
				cell.Width=Unit.Pixel(10);
				row.Cells.Add(cell);


				cell=new TableCell();
				
				FieldEditControl control=(FieldEditControl)ControlsArray[counter];
				control.CssClass="tbl1_edit_box";		
				control.EditValue=CurrentObject.GetType().GetProperty(PropertiesArray[counter]).GetValue(CurrentObject , null).ToString();
				control.ID="edit" + PropertiesArray[counter];
				if (CurrentObject.GetType().GetProperty(PropertiesArray[counter]).CanWrite==false || Mode==ModeEnum.Delete)
					control.Enabled=false;

				cell.Controls.Add(control);


				cell.Width=Unit.Pixel(0);
				cell.CssClass="tbl1_edit_cell";
				row.Cells.Add(cell);
				

				EditTable.Rows.Add(row);

				//empty row
				row=new TableRow();
				cell=new TableCell();
				cell.Height=Unit.Pixel(5);
				row.Cells.Add(cell);
				cell=new TableCell();
				row.Cells.Add(cell);
				cell=new TableCell();
				row.Cells.Add(cell);
				EditTable.Rows.Add(row);

			}	
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
			this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
			this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion

		protected override bool OnBubbleEvent(object source, EventArgs e) 
		{   
			bool handled = false;
			if (e is CommandEventArgs)
			{
				CommandEventArgs ce = (CommandEventArgs)e;
				if (ce.CommandName == "Insert")
				{
					if(Page.IsValid)
					{
						if (InsertButtonClick!=null)
							InsertButtonClick(this , ce);
						handled = true;   
					}
				}  
				else if (ce.CommandName == "Update")
				{
					if(Page.IsValid)
					{
						if (UpdateButtonClick!=null)
							UpdateButtonClick(this , ce);
						handled = true;   
					}
				}
				else if (ce.CommandName == "Delete")
				{
					if (DeleteButtonClick!=null)
						DeleteButtonClick(this , ce);
					handled = true;   
				}
				else if (ce.CommandName == "Cancel")
				{
					if (CancelButtonClick!=null)
						CancelButtonClick(this , ce);
					handled = true;   
				}
            
			}
			return handled;            
		}

		private void InsertButton_Click(object sender, System.EventArgs e)
		{
			SetCurrentObjectProperties();
		}

		private void UpdateButton_Click(object sender, System.EventArgs e)
		{
			SetCurrentObjectProperties();
		}




		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			//do nothing, nothing is edited
		}



		private void Page_PreRender(object sender, System.EventArgs e)
		{
			ErrorLabel.Text=errorText;
		}


		public bool IsValid
		{
			get
			{
				if(errorText=="")
					return true;
				else
					return false;
			}
		}


		private void SetCurrentObjectProperties()
		{
			for(int counter=0 ; counter<PropertiesArray.Length ; counter++)
			{
				SetCurrentObjectProperty(PropertiesArray[counter]);
			}
		}


		private void SetCurrentObjectProperty(string PropertyName)
		{
			System.Reflection.PropertyInfo pi=CurrentObject.GetType().GetProperty(PropertyName);

			if(pi.CanWrite==true)
			{
				try
				{
					object val=((FieldEditControl)EditTable.FindControl("edit" + PropertyName)).EditValue;

					if(pi.PropertyType.IsEnum)
					{
						pi.SetValue(CurrentObject , System.Enum.Parse(pi.PropertyType , val.ToString()) , null);
					}
					else
					{
						pi.SetValue(CurrentObject , System.Convert.ChangeType(val , pi.PropertyType), null);
					}
				}
				catch(System.Exception exc)
				{
					ShowException(exc);
				}
			}
		}



		public void ShowException(Exception exc)
		{
			Exception newExc=exc;
			if(exc.InnerException!=null)
				newExc=exc.InnerException;
			
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(newExc);

			errorText=newExc.Message;
			if(FI.Common.AppConfig.IsDebugMode)
				errorText+=newExc.StackTrace;
		}



	}
}
