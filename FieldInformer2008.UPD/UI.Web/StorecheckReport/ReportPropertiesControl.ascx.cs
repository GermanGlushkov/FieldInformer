namespace FI.UI.Web.StorecheckReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;

	/// <summary>
	///		Summary description for ReportPropertiesControl.
	/// </summary>
	public partial class ReportPropertiesControl : System.Web.UI.UserControl
	{

		public FI.BusinessObjects.StorecheckReport _report;

		protected void Page_Load(object sender, System.EventArgs e)
		{				
			//ddlLogic
			ddlLogic.Items.Add("OR");
			ddlLogic.Items.Add("AND");

			//ddlSelection
			ddlSelection.Items.Add(new ListItem("None" , "0"));
			ddlSelection.Items.Add(new ListItem("Base Selection" , "1"));
			ddlSelection.Items.Add(new ListItem("Selection" , "2"));

			// ddlDataSource
			ddlDataSource.Items.Add(new ListItem("Deliveries+Sales" , ((int)FI.BusinessObjects.StorecheckReport.DataSourceEnum.Deliveries_And_Sales).ToString() ));
			ddlDataSource.Items.Add(new ListItem("Deliveries" , ((int)FI.BusinessObjects.StorecheckReport.DataSourceEnum.Deliveries).ToString() ));
			ddlDataSource.Items.Add(new ListItem("Sales" , ((int)FI.BusinessObjects.StorecheckReport.DataSourceEnum.Sales).ToString() ));
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			//txtName
			txtName.Value=_report.Name;

			//txtDescr
			txtDescr.Value=_report.Description;

			//txtDays
			txtDays.Text=_report.Days.ToString();

			//ddlLogic
			ddlLogic.SelectedItem.Selected=false;
			if(_report.ProductsJoinLogic==FI.BusinessObjects.StorecheckReport.ProductsJoinLogicEnum.OR)
				ddlLogic.Items[0].Selected=true;
			else
				ddlLogic.Items[1].Selected=true;

			//lblToday
			lblToday.Text=DateTime.Today.ToString("dd.MM.yyyy");

			//lblPeriod
			lblPeriod.Text=DateTime.Today.Subtract(TimeSpan.FromDays(_report.Days)).ToString("dd.MM.yyyy") + "-" + DateTime.Today.ToString("dd.MM.yyyy");

			//ddlSelection
			ddlSelection.SelectedItem.Selected=false;
			if(_report.InBSelOnly==true)
				ddlSelection.Items.FindByValue("1").Selected=true;
			else if(_report.InSelOnly==true)
				ddlSelection.Items.FindByValue("2").Selected=true;
			else
				ddlSelection.Items.FindByValue("0").Selected=true;

			// ddlDataSource
			ddlDataSource.SelectedItem.Selected=false;
			ddlDataSource.Items.FindByValue( ((int)_report.DataSource).ToString() ).Selected=true;

			base.Render (writer);
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

		public void UpdateReport()
		{
			// header			
			_report.Name=this.txtName.Value;
			_report.Description=this.txtDescr.Value;
			this.txtName.Value=_report.Name;
			this.txtDescr.Value=_report.Description;

			// state
			_report.ProductsJoinLogic=(FI.BusinessObjects.StorecheckReport.ProductsJoinLogicEnum)Enum.Parse(typeof(FI.BusinessObjects.StorecheckReport.ProductsJoinLogicEnum) , this.ddlLogic.SelectedValue);

			_report.Days=short.Parse(this.txtDays.Text);

			if(ddlSelection.SelectedValue=="1")
			{
				_report.InBSelOnly=true;
				_report.InSelOnly=false;
			}
			else if(ddlSelection.SelectedValue=="2")
			{
				_report.InBSelOnly=false;
				_report.InSelOnly=true;
			}
			else
			{
				_report.InBSelOnly=false;
				_report.InSelOnly=false;
			}

			_report.DataSource=(FI.BusinessObjects.StorecheckReport.DataSourceEnum)Enum.Parse(typeof(FI.BusinessObjects.StorecheckReport.DataSourceEnum) , this.ddlDataSource.SelectedValue);

		}

	}
}
