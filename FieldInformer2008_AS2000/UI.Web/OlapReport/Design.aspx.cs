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
using FI.BusinessObjects.Olap;

namespace FI.UI.Web.OlapReport
{
	/// <summary>
	/// Summary description for Design.
	/// </summary>
	public partial class Design : OlapPageBase
	{
		protected System.Web.UI.WebControls.Button btnUpdate;

		protected FI.UI.Web.Controls.Tabs.TabView _tabView;
		protected FI.UI.Web.OlapReport.SliceControl _sliceControl;
		protected FI.UI.Web.OlapReport.SelectControl _selControl;
		protected FI.UI.Web.OlapReport.ExecuteControl _execControl;
		protected FI.UI.Web.OlapReport.ReportPropertiesControl _propControl;

		protected string _contentType="";

		protected string _pageScrollId=null;
		protected Controller _contr;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			_tabView=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TvC");
			_sliceControl=(FI.UI.Web.OlapReport.SliceControl)this.FindControl("SlC");
			_selControl=(FI.UI.Web.OlapReport.SelectControl)this.FindControl("SeC");
			_execControl=(FI.UI.Web.OlapReport.ExecuteControl)this.FindControl("ExC");
			_propControl=(FI.UI.Web.OlapReport.ReportPropertiesControl)this.FindControl("RPrC");

			_sliceControl._report=_report;
			_selControl._report=_report;

			_execControl._report=_report;
			_execControl.ForceExecute=false;
			
			_propControl._report=_report;

			if(_report.SharingStatus==Report.SharingEnum.InheriteSubscriber || _report.SharingStatus==Report.SharingEnum.SnapshotSubscriber)
				_propControl.Visible=false;
			
		}

		protected override void Render(HtmlTextWriter writer)
		{
			_contr=new Controller(_report, this);
			ExecuteCommands();
			LoadTabs();
			CreateScrollScript();
			base.Render (writer);
		}





		private void CreateScrollScript()
		{
			string script=null;
			if(_pageScrollId==null)
			{
				script=@"
				<script language='javascript' type='text/javascript'>
				function SetScrollPosition()
				{
				}
				</script>
				";
			}
			else
			{
				script=@"
				<script language='javascript' type='text/javascript'>
				function SetScrollPosition()
				{
					try{
					document.getElementById('"+ _pageScrollId + @"').scrollIntoView();
					}
					catch(exc){}
				}
				</script>
				";
			}
			this.RegisterClientScriptBlock("SetScrollPosition" , script);

		}


		private void LoadTabs()
		{
			int id=0 , id1=0;

			id=this.CreateRootTabs(_tabView , _user.Name , _user.IsLoggedIn , true , PageBase.RootTabsEnum.Olap_Reports);

			_tabView.AddTab(id , "  List  " , Request.ApplicationPath + "/ReportList.aspx?content=List&rpttype=" + _report.GetTypeCode().ToString() , false , false);



			FI.Common.Data.FIDataTable rptTable=_user.ReportSystem.GetReportHeaders(_report.GetType());
			foreach(System.Data.DataRow row in rptTable.Rows)
			{
				decimal rptId=decimal.Parse(row["id"].ToString());
				bool rptSelected=(bool)row["is_selected"];
				bool rptOpen=(_report!=null && rptId==_report.ID?true:false);
				string rptName=(string)row["name"];
				FI.BusinessObjects.Report.SharingEnum rptSharingStatus=(FI.BusinessObjects.Report.SharingEnum)int.Parse(row["sharing_status"].ToString());
				FI.BusinessObjects.Report.SharingEnum rptMaxSubscriberSharingStatus=(FI.BusinessObjects.Report.SharingEnum)int.Parse(row["max_subscriber_sharing_status"].ToString());

				if(rptSelected)
				{
					int reportType=_report.GetTypeCode();
					id1=_tabView.AddTab(id , rptName , Request.ApplicationPath + "/ReportList.aspx?content=Load&action=Open&rptid=" + rptId + "&rpttype=" + reportType.ToString() , rptOpen , false);

					if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						_tabView.AddImage(id1,"images/share.gif");
					else if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						_tabView.AddImage(id1,"images/share_change.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
						_tabView.AddImage(id1,"images/distr.gif");
					else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
						_tabView.AddImage(id1,"images/distr_change.gif");
				}
			}

			_tabView.AddTab(id1 , "  Table  " , Request.ApplicationPath + "/OlapReport/Table.aspx", false , false);
			_tabView.AddTab(id1 , "  Graph  " , Request.ApplicationPath + "/OlapReport/Graph.aspx" , false , false);
			_tabView.AddTab(id1 , "  Design  " , Request.ApplicationPath  + "/OlapReport/Design.aspx" , true , false);
			_tabView.AddTab(id1 , "  Format  " , Request.ApplicationPath  + "/OlapReport/Format.aspx" , false , false);
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

		}
		#endregion


		public override void ShowException(Exception exc)
		{
			if(Common.AppConfig.IsDebugMode)
				Common.LogWriter.Instance.WriteEventLogEntry(exc);

			ShowMessage(exc.Message);
		}


		private void ShowMessage(string Message)
		{
			this.ErrTable.Rows.Clear();
			System.Web.UI.WebControls.Label lbl=new System.Web.UI.WebControls.Label();
			lbl.CssClass="tbl1_err";
			lbl.Text=Message;
			System.Web.UI.WebControls.TableRow row=new System.Web.UI.WebControls.TableRow();
			System.Web.UI.WebControls.TableCell cell=new System.Web.UI.WebControls.TableCell();
			cell.Controls.Add(lbl);
			row.Cells.Add(cell);
			this.ErrTable.Rows.Add(row);
		}







		private void ExecuteCommands()
		{

			for(int i=0;i<Request.Form.Count;i++)
			{
				string key=Request.Form.Keys[i];
				string val=Request.Form[i];

				if(key.StartsWith(_tabView.UniqueID + ":")) // TABVIEW
				{
					int index=key.LastIndexOf(":")+1;
					string tabCmd=key.Substring(index , key.Length-index);

					_contr.Transfer(tabCmd);
					break;
				}
				else if(key==_selControl.UniqueID + ":btnPivot")
				{
					
					try
					{
						_contr.Pivot();
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					break;
				}
				else if(key==_selControl.UniqueID + ":btnUpdate")
				{
					this.UpdateSelectedMembers();

					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_fopen:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string folderName=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.OpenFolder(folderName);
						_report.SaveHeader(); // save open nodes
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_fopen" , "sel_fclose"); //it will hage it's id
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_fclose:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string folderName=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.CloseFolder(folderName);
						_report.SaveHeader(); // save open nodes
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_fclose" , "sel_fopen"); //it will hage it's id
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_del:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.HierarchyToAxis(hierUn , 2);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_del" , "sel_torow"); //it will hage it's id , it's in filter now
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_torow:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.HierarchyToAxis(hierUn ,1);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_torow" , "sel_del"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_tocol:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.HierarchyToAxis(hierUn , 0);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_tocol" , "sel_del"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_up:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.HierarchyUp(hierUn);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_down:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.HierarchyDown(hierUn);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_hopen:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.OpenHierarchy(hierUn);
						_report.SaveHeader(); // save open nodes
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_hopen" , "sel_hclose"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_hclose:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.CloseHierarchy(hierUn);
						_report.SaveHeader(); // save open nodes
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_hclose" , "sel_hopen"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_hselall:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.AddHierarchyChildren(hierUn);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_hdeselall:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.RemoveHierarchyChildren(hierUn);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_aggr_on:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.SetHierarchyAggregate(hierUn , true);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_aggr_on" , "sel_aggr_off"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_aggr_off:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string hierUn=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.SetHierarchyAggregate(hierUn , false);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_aggr_off" , "sel_aggr_on"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_open:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string identifier=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.OpenMember(identifier);
						_report.SaveHeader(); // save open nodes
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_open" , "sel_close"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_close:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string identifier=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.CloseMember(identifier);
						_report.SaveHeader(); // save open nodes
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key.Replace("sel_close" , "sel_open"); //it will hage it's id 
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_selauto:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string identifier=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.AddMemberChildren(identifier, true);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_selall:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string identifier=key.Substring(index , key.Length-index);
					
					try
					{
						_contr.AddMemberChildren(identifier, false);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(_selControl.UniqueID + ":sel_deselall:"))
				{
					int index=key.IndexOf(":");
					index=key.IndexOf(":" , index+1)+1;
					string identifier=key.Substring(index , key.Length-index);

					try
					{
						_contr.RemoveMemberChildren(identifier);
					}
					catch(Exception exc)
					{
						this.ShowException(exc);
					}

					_pageScrollId=key;
					break;
				}
				else if(key.StartsWith(this._sliceControl.UniqueID + ":slc_del:"))
				{
					int hierIndex=key.LastIndexOf(":")+1;
					string hierUn=key.Substring(hierIndex , key.Length-hierIndex);

					_contr.SetDefaultMember(hierUn);

					break;
				}
			}

			// save state
			
			_report.SaveState();


		}


		private void UpdateSelectedMembers()
		{
			//get keys of selected visible members
			System.Collections.Specialized.StringCollection chkList=new System.Collections.Specialized.StringCollection();
			System.Collections.Specialized.StringCollection radioList=new System.Collections.Specialized.StringCollection();

			for(int j=0;j<Request.Form.Count;j++)
			{
				string mKey=Request.Form.Keys[j];
				string mVal=Request.Form[j];
						
						
				if(mKey.StartsWith(_selControl.UniqueID + ":m:") )
				{
					if(mVal=="on") //checkboxes
					{
						int index=mKey.IndexOf(":");
						index=mKey.IndexOf(":" , index+1)+1;
						string uniqueName=mKey.Substring(index , mKey.Length-index);
						chkList.Add(uniqueName);
					}
					else if(mVal.StartsWith("m"))//radios
					{
						int index=mVal.IndexOf(":")+1;
						string identifier=mVal.Substring(index , mVal.Length-index);
						radioList.Add(identifier);
					}
				}
			}


			try
			{
				_contr.AddMembersAndRemoveSiblings(chkList, true);
				_contr.AddMembersAndRemoveSiblings(radioList, false);
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
				return;
			}
		}



		protected void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../ReportList.aspx?content=Save" , true);
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				this._propControl.UpdateReportHeader();
				_report.SaveHeader();
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}













	}
}
