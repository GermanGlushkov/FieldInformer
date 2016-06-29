namespace FI.UI.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;

	/// <summary>
	///		Summary description for ReportDistributionControl.
	/// </summary>
	public partial class ReportDistributionControl : System.Web.UI.UserControl
	{


        public FI.BusinessObjects.User _user;
		protected FI.BusinessObjects.Report _reportProxy;
		protected decimal _reportId;
		protected int _reportType;

		private FI.UI.Web.Controls.FIDataTableGrid _rptGr;
		private FI.UI.Web.Controls.FIDataTableGrid _gr;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadParameters();
			LoadReportPanel();
			LoadDistributionPanel();
		}

		private void LoadParameters()
		{
			try
			{
				_reportId=decimal.Parse(Request.QueryString["rptid"]);
				_reportType=int.Parse(Request.QueryString["rpttype"]);

				Session[this.UniqueID + ":ReportId"]=_reportId;
				Session[this.UniqueID + ":ReportType"]=_reportType;
			}
			catch
			{
				//do nothing
			}
			
			_reportId=(decimal)Session[this.UniqueID + ":ReportId"];
			_reportType=(int)Session[this.UniqueID + ":ReportType"];
			_reportProxy=_user.ReportSystem.GetReport(_reportId , _user.ReportSystem.GetReportType(_reportType) , false);
		}


		private void LoadReportPanel()
		{
			// load table
			_reportProxy.LoadHeader();
			FI.Common.Data.FIDataTable rptTable=new FI.Common.Data.FIDataTable();
			rptTable.Columns.Add("name" , typeof(string));
			rptTable.Columns.Add("description" , typeof(string));
			rptTable.Rows.Add(new object[] {_reportProxy.Name , _reportProxy.Description});

			//loading grid control
			_rptGr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_rptGr.ID="RptGrid";
			_rptGr.InMemory=true;
			_rptGr.DataSource=rptTable;
			_rptGr.ColumnNameArray=new string[] {"name" , "description" };
			_rptGr.ColumnCaptionArray=new string[] {"Report Name" , "Description"};
			_rptGr.ColumnWidthArray=new int[] {200 , 400};
			_rptGr.EnableSort=false;
			_rptGr.EnableFilter=false;
			_rptGr.EnableCheckBoxes=false;
			_rptGr.EnablePages=false;
			ReportPanel.Controls.Add(_rptGr);
		}


		private void LoadDistributionPanel()
		{
			//loading grid control
			_gr = (FI.UI.Web.Controls.FIDataTableGrid)Page.LoadControl("Controls/FIDataTableGrid.ascx");
			_gr.ID="DistrGrid";
			_gr.DefaultPageSize=15;
			_gr.DataSourceDelegate=new FI.UI.Web.Controls.FIDataTableGrid.GridDataSourceDelegate(GetDistributionsWithContactsPage);
			_gr.PrimaryKeyColumnArray=new string[] {"contact_id" , "distribution_id"};
			_gr.ColumnNameArray=new string[] {"name" , "email" , "format", "distr_format" , "freq_type" , "freq_value"};
			_gr.ColumnCaptionArray=new string[] {"Contact Name" , "Contact EMail" , "Format", "EMail View" , "Frequency Type" , "Frequency Value" };
			_gr.ColumnWidthArray=new int[] {150 , 200 , 75, 125 , 100 , 250};
			_gr.EnableMultipleSelection=true;
			DistributionPanel.Controls.Add(_gr);

			// load export types
			this.ddlFormat.Items.Clear();
			string[] names=Enum.GetNames(typeof(Report.ExportFormat));
			foreach(string name in names)
				this.ddlFormat.Items.Add(name);
		}

		private FI.Common.Data.FIDataTable GetDistributionsWithContactsPage(int CurrentPage, int PageSize, string FilterExpression, string SortExpression)
		{
			return _user.DistributionSystem.GetDistributionsWithContactsPage(_reportProxy , CurrentPage, PageSize, FilterExpression, SortExpression);
		}




		
		protected void UpdateButton_Click(object sender, System.EventArgs e)
		{
			Report.ExportFormat exportFormat=(Report.ExportFormat )Enum.Parse(typeof(Report.ExportFormat ), this.ddlFormat.SelectedValue, true);

			Distribution.FrequencyTypeEnum frequencyType=Distribution.FrequencyTypeEnum.Monthly;
			Distribution.FrequencyValueEnum frequencyValue=Distribution.FrequencyValueEnum.Monthly;			

			if(radioWeekDays.Checked==true)
			{
				frequencyType=Distribution.FrequencyTypeEnum.Weekdays;

				//setting FrequencyValue
				if(chkMon.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Mon;
				if(chkTue.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Tue;
				if(chkWed.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Wed;					
				if(chkThu.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Thu;					
				if(chkFri.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Fri;					
				if(chkSat.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Sat;					
				if(chkSun.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Sun;
			}
			else if(radioWeeks.Checked==true)
			{
				frequencyType=Distribution.FrequencyTypeEnum.Weeks;

				//setting FrequencyValue
				if(chkWeek1.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Week1;
				if(chkWeek2.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Week2;
				if(chkWeek3.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Week3;					
				if(chkWeek4.Checked==true)
					frequencyValue=frequencyValue | Distribution.FrequencyValueEnum.Week4;
			}
			else if(radioMonthly.Checked==true)
			{
				frequencyType=Distribution.FrequencyTypeEnum.Monthly;
			}


			foreach(string[] keys in _gr.SelectedPrimaryKeys)
			{				
				try
				{
					string contact_id=keys[0];
					string distribution_id=keys[1];

					Contact cnt=_user.ContactSystem.GetContact(decimal.Parse(contact_id) , false);

					Distribution distr=null;
					if(distribution_id!=null && distribution_id.Trim()!="")
					{
						distr=_user.DistributionSystem.GetDistribution(decimal.Parse(distribution_id) , false);
						if(radioNone.Checked==true)
						{
							_user.DistributionSystem.DeleteDistribution(distr);
							continue;
						}
						else
							distr.Fetch();
					}
					else
						distr=_user.DistributionSystem.NewDistribution(_reportProxy , cnt, exportFormat);

					distr.FrequencyType=frequencyType;
					distr.FrequencyValue=frequencyValue;
					distr.Format=exportFormat;
					distr.Save();
				}
				catch(Exception exc)
				{
					ShowException(exc);
				}

			}

			//_gr.DataBind(true);
		}


		protected void ViewLogButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("ReportList.aspx?content=DispatchLog&rptid=" + _reportId.ToString() + "&rpttype=" + _reportType.ToString() , false);
		}

		protected void BackButton_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("ReportList.aspx?content=List" , false);
		}



		protected void SendNowButton_Click(object sender, System.EventArgs e)
		{
			int keyCount=_gr.SelectedPrimaryKeys.Count;

			if (keyCount==0)
				return;

			Report.ExportFormat exportFormat=(Report.ExportFormat )Enum.Parse(typeof(Report.ExportFormat ), this.ddlFormat.SelectedValue, true);
			Contact[] contacts=new Contact[_gr.SelectedPrimaryKeys.Count];

			for(int i=0;i<keyCount;i++)
			{
				string[] keys=(string[])_gr.SelectedPrimaryKeys[i];
				decimal contactId=decimal.Parse(keys[0]);
				contacts[i]=_user.ContactSystem.GetContact(contactId , true);	
			}

			try
			{
				_reportProxy.Open();
				_reportProxy.Execute();
                Guid olapTaskGuid = Guid.NewGuid();
				bool isFromCache=false;

                DistributionManager.Instance.SendReport(_reportProxy, olapTaskGuid, contacts, exportFormat, DateTime.MaxValue, DateTime.Now, out isFromCache);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
			
		}



		protected void EnqueueNowButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				_user.DistributionSystem.EnqueueReportDistributions(this._reportId, this._reportType);
			}
			catch(Exception exc)
			{
				ShowException(exc);
			}
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



	}
}
