namespace FI.UI.Web.OlapReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;
	using FI.BusinessObjects.Olap;
	using FI.BusinessObjects.Olap.CalculatedMemberTemplates;


	public class FormatControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Table ErrTable;

		
		protected internal FI.BusinessObjects.User _user;
		protected internal FI.BusinessObjects.OlapReport _report;
		protected internal string _action;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.DropDownList listFormat;
		protected System.Web.UI.WebControls.PlaceHolder pnlSourceMember;
		private Controller _contr;
		protected System.Web.UI.WebControls.Panel pnlFormattedMembers;		
		private FI.UI.Web.Controls.FIDataTableGrid _gridFormattedMembers;
		protected System.Web.UI.WebControls.Button btnFormat;
		protected System.Web.UI.WebControls.Button btnReset;
		private Controls.FIDropDownControl _ddlUniqueName;


		private void Page_Load(object sender, System.EventArgs e)
		{
			_contr=new Controller(_report, this.Page);
			LoadControls();
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			DataBindControls();
			base.Render(writer);
		}



		private void LoadControls()
		{			
			// _ddlUniqueName
			_ddlUniqueName=new Controls.FIDropDownControl();
			_ddlUniqueName.CssClass="tbl1_edit";
			this.pnlSourceMember.Controls.Add(_ddlUniqueName);

			
			// listFormat
			foreach(string format in Enum.GetNames(typeof(FI.BusinessObjects.Olap.CalculatedMember.FormatEnum)))
				this.listFormat.Items.Add(format);

			//  formatted members grid
			_gridFormattedMembers = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("../Controls/FIDataTableGrid.ascx");
			_gridFormattedMembers.ID="gridFormattedMembers";
			_gridFormattedMembers.EnablePages=true;
			_gridFormattedMembers.DefaultPageSize=15;
			_gridFormattedMembers.EnableUpdatePager=false;
			_gridFormattedMembers.InMemory=true;
			_gridFormattedMembers.PrimaryKeyColumnArray=new string[] {"uniquename"};
			_gridFormattedMembers.ColumnNameArray=new string[] {"hierarchy", "default_name", "name", "format"};
			_gridFormattedMembers.ColumnCaptionArray=new string[] {"Hierarchy" , "Default Name",  "Name", "Format" };
			_gridFormattedMembers.ColumnWidthArray=new int[] {150 , 200, 200, 80};
			_gridFormattedMembers.EnableMultipleSelection=true;
			this.pnlFormattedMembers.Controls.Add(_gridFormattedMembers);		
		}
		
		private void DataBindControls()
		{
			// explicitly cleanup txtName
			txtName.Text="";

			// listSourceMember
			foreach(Hierarchy hier in _report.Schema.Hierarchies)	
			{
				_ddlUniqueName.AddGroupItem(hier.UniqueName);

				// data members
				for(int i=0;i<hier.DataMembers.Count;i++)
					_ddlUniqueName.AddItem(hier.DataMembers[i].Name, hier.DataMembers[i].UniqueName);

				// calculated members
				for(int i=0;i<hier.CalculatedMembers.Count;i++)
				{
					// skip sets
					if(hier.CalculatedMembers[i] is Set)
						continue;

					_ddlUniqueName.AddItem(hier.CalculatedMembers[i].Name, hier.CalculatedMembers[i].UniqueName);
				}
			}	

			// _gridFormattedMembers
			_gridFormattedMembers.DataSource=GetFormattedMembers();
		}

		private FI.Common.Data.FIDataTable GetFormattedMembers()
		{
			FI.Common.Data.FIDataTable ret=new FI.Common.Data.FIDataTable();
			ret.Columns.Add("uniquename", typeof(string));
			ret.Columns.Add("hierarchy", typeof(string));
			ret.Columns.Add("default_name", typeof(string));
			ret.Columns.Add("name", typeof(string));
			ret.Columns.Add("format", typeof(string));

			// data members
			foreach(Hierarchy hier in _report.Schema.Hierarchies)
				foreach(CalculatedMember cmem in hier.CalculatedMembers)
				{
					// skip sets
					if(cmem is Set)
						continue;

					// skip default formatted members
					if(!(cmem is MemberWrapper) && cmem.Name==cmem.GetDefaultName() && cmem.Format==CalculatedMember.FormatEnum.Default)
						continue;

					ret.Rows.Add(new object[]{cmem.UniqueName, cmem.Hierarchy.UniqueName, cmem.GetDefaultName(), cmem.Name, cmem.Format.ToString() });
				}

			return ret;
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
			this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnFormat_Click(object sender, System.EventArgs e)
		{
			string uniqueName=Request.Form[this._ddlUniqueName.UniqueID];
			string name=Request.Form[this.txtName.UniqueID];
			string format=Request.Form[this.listFormat.UniqueID];

			try
			{
				_contr.SetFormattedMember(uniqueName, name, format);
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
				return;
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			System.Collections.ArrayList list=_gridFormattedMembers.SelectedPrimaryKeys;
			if(list==null || list.Count==0)
				return;

			try
			{
				foreach(string[] key in list)
					_contr.ResetFormattedMember(key[0]);

				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
				return;
			}
		}







	}
}
