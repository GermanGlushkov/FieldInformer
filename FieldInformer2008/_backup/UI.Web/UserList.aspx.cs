

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FI.BusinessObjects;

namespace FI.UI.Web
{
	public class UserList : PageBase
	{

		private FI.UI.Web.Controls.FIDataEdit _ed;
		private FI.UI.Web.Controls.FIDataTableGrid _gr;
		private string _sessionContent;
		private string _sessionItemCurrentObject;


		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Panel ControlPanel;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlTableCell cellControls;


		protected override void Render(HtmlTextWriter writer)
		{
			base.Render (writer);
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			lblError.Text="";

			_sessionContent=this.ToString() + ":Content";
			_sessionItemCurrentObject=this.ToString() + ":CurrentObject";

			if(Session[_sessionContent]==null)
				Session[_sessionContent]="List";

			LoadTabs();


			if(Session[_sessionContent].ToString()=="Insert")
			{

				if(Session[_sessionItemCurrentObject]==null)
					LoadGridControl();
				
				LoadEditControl(FI.UI.Web.Controls.FIDataEdit.ModeEnum.Insert);
				
			}
			else if(Session[_sessionContent].ToString()=="Edit")
			{
				if(Session[_sessionItemCurrentObject]==null)
					LoadGridControl();
				
				LoadEditControl(FI.UI.Web.Controls.FIDataEdit.ModeEnum.Edit);

			}
			else if(Session[_sessionContent].ToString()=="Delete")
			{
				if(Session[_sessionItemCurrentObject]==null)
					LoadGridControl();
				
				LoadEditControl(FI.UI.Web.Controls.FIDataEdit.ModeEnum.Delete);
			}	
			else
			{
				Session[_sessionItemCurrentObject]=null;
				Session[_sessionContent]="List";
				LoadGridControl();
			}
		}


		private void LoadTabs()
		{
			int id ;

			FI.UI.Web.Controls.Tabs.TabView tv=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TabView1");
			
			id=this.CreateRootTabs(tv , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Administrative_Tools);

			tv.AddTab(id , "Users" , Request.ApplicationPath + "/UserList.aspx" , true , false);
			tv.AddTab(id , "Contacts" , Request.ApplicationPath + "/ContactList.aspx", false , false);
			tv.AddTab(id , "Distribution Log" , Request.ApplicationPath + "/DistributionLog.aspx" , false , false);
			tv.AddTab(id , "Distribution Queue" , Request.ApplicationPath + "/DistributionQueue.aspx" , false , false);
			tv.AddTab(id , "Color Scheme" , Request.ApplicationPath + "/ColorScheme.aspx" , false , false);
		}




		private void LoadEditControl(FI.UI.Web.Controls.FIDataEdit.ModeEnum Mode)
		{
			//hide control buttons
			this.cellControls.Visible=false;

			//loading edit control
			_ed = (FI.UI.Web.Controls.FIDataEdit)Page.LoadControl("Controls/FIDataEdit.ascx");
			_ed.Mode=Mode;
			_ed.CurrentObject=Session[_sessionItemCurrentObject];
			_ed.PropertiesArray=new string[] {"Logon" , "Password" , "Name" , "Email" , "IsAdmin"};
			_ed.CaptionsArray=new string[] {"Logon" , "Password" , "Name" , "Email" , "IsAdmin"};
			
			FI.UI.Web.Controls.FieldEditControl logonEdit=(FI.UI.Web.Controls.FieldEditControl)Page.LoadControl("Controls/FieldEditControl.ascx");
			logonEdit.ControlType=FI.UI.Web.Controls.FieldEditControl.ControlTypeEnum.TextBox;
			logonEdit.Width=Unit.Pixel(200);

			FI.UI.Web.Controls.FieldEditControl pwdEdit=(FI.UI.Web.Controls.FieldEditControl)Page.LoadControl("Controls/FieldEditControl.ascx");
			pwdEdit.ControlType=FI.UI.Web.Controls.FieldEditControl.ControlTypeEnum.Password;
			pwdEdit.Width=Unit.Pixel(200);

			FI.UI.Web.Controls.FieldEditControl nameEdit=(FI.UI.Web.Controls.FieldEditControl)Page.LoadControl("Controls/FieldEditControl.ascx");
			nameEdit.ControlType=FI.UI.Web.Controls.FieldEditControl.ControlTypeEnum.TextBox;
			nameEdit.Width=Unit.Pixel(200);

			FI.UI.Web.Controls.FieldEditControl emailEdit=(FI.UI.Web.Controls.FieldEditControl)Page.LoadControl("Controls/FieldEditControl.ascx");
			emailEdit.ControlType=FI.UI.Web.Controls.FieldEditControl.ControlTypeEnum.TextBox;
			emailEdit.Width=Unit.Pixel(300);

			FI.UI.Web.Controls.FieldEditControl adminEdit=(FI.UI.Web.Controls.FieldEditControl)Page.LoadControl("Controls/FieldEditControl.ascx");
			adminEdit.ControlType=FI.UI.Web.Controls.FieldEditControl.ControlTypeEnum.DropDownList;
			ListItem item1=new ListItem("False" , "False");
			ListItem item2=new ListItem("True" , "True");
			adminEdit.ListItems=new ListItem[]{item1, item2};
			adminEdit.Width=Unit.Pixel(200);

			_ed.ControlsArray=new FI.UI.Web.Controls.FieldEditControl[]{logonEdit , pwdEdit , nameEdit , emailEdit , adminEdit};

			_ed.LabelsWidth=150;

			if(Mode==FI.UI.Web.Controls.FIDataEdit.ModeEnum.Insert)
				_ed.InsertButtonClick += new System.Web.UI.WebControls.CommandEventHandler(this.FIDataEdit_InsertButtonClick);
			else if(Mode==FI.UI.Web.Controls.FIDataEdit.ModeEnum.Edit)
				_ed.UpdateButtonClick += new System.Web.UI.WebControls.CommandEventHandler(this.FIDataEdit_UpdateButtonClick);
			else if(Mode==FI.UI.Web.Controls.FIDataEdit.ModeEnum.Delete)
				_ed.DeleteButtonClick += new System.Web.UI.WebControls.CommandEventHandler(this.FIDataEdit_DeleteButtonClick);

			_ed.CancelButtonClick += new System.Web.UI.WebControls.CommandEventHandler(this.FIDataEdit_CancelButtonClick);	

			ControlPanel.Controls.Add(_ed);

			//end loading edit control
		}


		private void LoadGridControl()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_gr.ID="ContactsGrid";
			_gr.PrimaryKeyColumnArray=new string[] {"Id"};
			_gr.ColumnNameArray=new string[] {"Logon" , "Name" , "Email" , "IsAdmin"};
			_gr.ColumnCaptionArray=new string[] {"Logon" , "Name" , "Email"  , "IsAdmin" };
			_gr.ColumnWidthArray=new int[] {200 , 200 , 200, 50};
			_gr.EnableSort=true;
			_gr.EnableMultipleSelection=true;
			_gr.EnablePages=true;
			_gr.DefaultPageSize=20;
			_gr.InMemory=true;
			_gr.DataSource=_user.GetSystemUsers();
			ControlPanel.Controls.Add(_gr);
			//end loading grid control
		}




		private void FIDataEdit_InsertButtonClick(object Sender , System.Web.UI.WebControls.CommandEventArgs e)
		{
			
			FI.UI.Web.Controls.FIDataEdit dataEdit=(FI.UI.Web.Controls.FIDataEdit)Sender;

			if(dataEdit.IsValid==false)
				return;

			FI.BusinessObjects.User user=(FI.BusinessObjects.User)dataEdit.CurrentObject;
			
			try
			{
				_user.SaveUser(user);
				Session[_sessionContent]="List";
				
				Server.Transfer(Request.FilePath, false);
			}
			catch(System.Exception err)
			{
				_ed.ShowException(err);
			}	
            			

		}




		private void FIDataEdit_UpdateButtonClick(object Sender , System.Web.UI.WebControls.CommandEventArgs e)
		{
			
			FI.UI.Web.Controls.FIDataEdit dataEdit=(FI.UI.Web.Controls.FIDataEdit)Sender;

			if(dataEdit.IsValid==false)
				return;

			FI.BusinessObjects.User user=(FI.BusinessObjects.User)dataEdit.CurrentObject;
			
			try
			{
				_user.SaveUser(user);
				Session[_sessionContent]="List";
				
				Server.Transfer(Request.FilePath, false);
			}
			catch(System.Exception err)
			{
				_ed.ShowException(err);
			}	
            			

		}

		private void FIDataEdit_DeleteButtonClick(object Sender , System.Web.UI.WebControls.CommandEventArgs e)
		{
			FI.UI.Web.Controls.FIDataEdit dataEdit=(FI.UI.Web.Controls.FIDataEdit)Sender;

			if(dataEdit.IsValid==false)
				return;

			FI.BusinessObjects.User user=(FI.BusinessObjects.User)dataEdit.CurrentObject;
			
			try
			{
				_user.DeleteUser(user);
				Session[_sessionContent]="List";
				
				Server.Transfer(Request.FilePath, false);
			}
			catch(System.Exception err)
			{
				_ed.ShowException(err);
			}	

		}

		private void FIDataEdit_CancelButtonClick(object Sender , System.Web.UI.WebControls.CommandEventArgs e)
		{
			FI.BusinessObjects.User user=null;

			if(Session[_sessionItemCurrentObject]!=null)
				user=(FI.BusinessObjects.User)Session[_sessionItemCurrentObject];
			

			user=null;

			Session[_sessionContent]="List";
			Session[_sessionItemCurrentObject]=null;
			
			Server.Transfer(Request.FilePath, false);
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				Session[_sessionItemCurrentObject]=_user.NewUser();

				Session[_sessionContent]="Insert"; // just before transfer
				Server.Transfer(Request.FilePath, false);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{

			try
			{
				ArrayList keysArr=_gr.SelectedPrimaryKeys;

				if(keysArr==null || keysArr.Count==0)
				{
					Session[_sessionContent]="List";
					Session[_sessionItemCurrentObject]=null;
				}
				else
				{
					decimal userId=decimal.Parse(((string[])keysArr[0])[0]);
					FI.BusinessObjects.User user=_user.GetUser(userId,false);

					Session[_sessionItemCurrentObject]=user;
					Session[_sessionContent]="Edit"; // just before transfer
				}
				
				Server.Transfer(Request.FilePath, false);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				ArrayList keysArr=_gr.SelectedPrimaryKeys;

				if(keysArr==null || keysArr.Count==0)
				{
					Session[_sessionContent]="List";
					Session[_sessionItemCurrentObject]=null;
				}
				else
				{
					decimal userId=decimal.Parse(((string[])keysArr[0])[0]);
					FI.BusinessObjects.User user=_user.GetUser(userId,false);
				
					Session[_sessionItemCurrentObject]=user;
					Session[_sessionContent]="Delete"; // just before transfer
				}
			
			Server.Transfer(Request.FilePath, false);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
		}



		override public void ShowException(Exception exc)
		{
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(exc);

			lblError.Text=exc.Message;
		}
		
	}
}


