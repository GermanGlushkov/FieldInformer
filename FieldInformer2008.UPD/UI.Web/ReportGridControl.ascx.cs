namespace FI.UI.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class ReportGridControl : FI.UI.Web.Controls.FIDataTableGrid
	{
		public int _reportsType=-1;

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			base.Render (writer);
		}

		protected override void CreateFooterCell(TableCell cell, string ColumnName, int ColumnWidth)
		{
			if(ColumnName!="sharing_status")
				base.CreateFooterCell (cell, ColumnName, ColumnWidth);
		}


		protected override void CreateTableCell(TableCell cell, string ColumnName, int RowIndex, DataRow Row)
		{
			if(ColumnName=="sharing_status")
			{
				FI.BusinessObjects.Report.SharingEnum rptSharingStatus=(FI.BusinessObjects.Report.SharingEnum)(byte)Row["sharing_status"];
				FI.BusinessObjects.Report.SharingEnum rptMaxSubscriberSharingStatus=(FI.BusinessObjects.Report.SharingEnum)(byte)Row["max_subscriber_sharing_status"];

				System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
				if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
				{
					img.ImageUrl="images/share.gif";
					cell.Controls.Add(img);
				}
				else if(rptSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
				{
					img.ImageUrl="images/share_change.gif";
					cell.Controls.Add(img);
				}
				else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.SnapshotSubscriber)
				{
					img.ImageUrl="images/distr.gif";
					cell.Controls.Add(img);
				}
				else if(rptMaxSubscriberSharingStatus==FI.BusinessObjects.Report.SharingEnum.InheriteSubscriber)
				{
					img.ImageUrl="images/distr_change.gif";
					cell.Controls.Add(img);
				}

				if(Row["is_in_distribution"].ToString()=="1")
				{
					System.Web.UI.WebControls.Image img1=new System.Web.UI.WebControls.Image();
					img1.ImageUrl="images/envelope.gif";
					cell.Controls.Add(img1);
				}
			}
			else if(ColumnName=="name")
			{
				HyperLink href=new HyperLink();
				href.Text=Row[ColumnName].ToString();
				href.CssClass="tbl1_href_item";
				href.NavigateUrl=Request.ApplicationPath + "/ReportList.aspx?content=Load&action=Open&rptid=" + Row["id"].ToString() + "&rpttype=" + _reportsType.ToString()  ;
				cell.Controls.Add(href);
			}
			else
			{
				base.CreateTableCell(cell , ColumnName , RowIndex , Row);
			}
		}

	}


}
