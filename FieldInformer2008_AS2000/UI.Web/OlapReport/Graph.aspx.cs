

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

namespace FI.UI.Web.OlapReport
{

	public partial class Graph : OlapPageBase
	{
		protected System.Web.UI.WebControls.Button btnUpdate;
		
		protected FI.UI.Web.OlapReport.GraphControl _graphControl;
		protected FI.UI.Web.OlapReport.SliceControl _sliceControl;
		protected FI.UI.Web.Controls.Tabs.TabView _tabView;
		protected FI.UI.Web.OlapReport.ExecuteControl _execControl;
		protected FI.UI.Web.OlapReport.ReportPropertiesControl _propControl;

		private Controller _contr;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			_graphControl=(FI.UI.Web.OlapReport.GraphControl)this.FindControl("GrC");
			_sliceControl=(FI.UI.Web.OlapReport.SliceControl)this.FindControl("SlC");
			_tabView=(FI.UI.Web.Controls.Tabs.TabView)this.FindControl("TvC");
			_execControl=(FI.UI.Web.OlapReport.ExecuteControl)this.FindControl("ExC");

			_sliceControl._report=_report;
			_graphControl._report=_report;

			_execControl._report=_report;
			_execControl.ForceExecute=true;
		}

		protected override void Render(HtmlTextWriter writer)
		{
			_contr=new Controller(_report, this);
			ExecuteCommands();
			LoadTabs();
			base.Render (writer);
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

			_tabView.AddTab(id1 , "  Table  " , Request.ApplicationPath + "/OlapReport/Table.aspx" , false , false);
			_tabView.AddTab(id1 , "  Graph  " , Request.ApplicationPath + "/OlapReport/Graph.aspx" , true , false);
			_tabView.AddTab(id1 , "  Design  " , Request.ApplicationPath + "/OlapReport/Design.aspx" , false , false);
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
			
			try
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
					else if(key==_graphControl.UniqueID + ":btnPivot")
					{
						_contr.Pivot();
						break;
					}
					else if(key==_graphControl.UniqueID + ":btnUpdate")
					{
                        Size size = _graphControl.GraphSize;
                        _contr.SetGraph(_graphControl.GraphType, _graphControl.GraphTheme, 
                            (short)size.Width, (short)size.Height, _graphControl.GraphPieColumns, _graphControl.GraphMixedLinePos,
                            _graphControl.ShowValues, _graphControl.ShowSeries, _graphControl.ShowCat, 
                            _graphControl.SetScalingBySeries, _graphControl.SetPivot, _graphControl.SetPercentByCategories);
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
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}


			// save state
			_report.SaveState();


		}




		protected void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../ReportList.aspx?content=Save" , true);
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
//				this._propControl.UpdateReportHeader();
//				_report.SaveHeader();
				_report.SaveState();
			}
			catch(Exception exc)
			{
				this.ShowException(exc);
			}
		}




	}
}

