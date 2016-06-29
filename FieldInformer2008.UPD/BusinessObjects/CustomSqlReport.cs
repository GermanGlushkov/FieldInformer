using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;
using System.Web;
using FI.Common;
using FI.Common.Data;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for CustomSqlReport.
	/// </summary>
	public class CustomSqlReport:Report
	{

		protected internal System.Data.DataTable _resultData=null;
		protected internal string _sql="";
		protected internal string _error="";
		protected internal string _xsl="";


		internal CustomSqlReport(decimal ID , User Owner):base(ID,Owner)
		{
			if(ID==0) //if new
			{
				_xsl=DefaultXsl();

				FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
				_id=dacObj.InsertReport(_owner.ID , 0 , 0 , "New Report" , "" , this.IsSelected , this.Sql , this.Xsl);

				_isProxy=false;
				_isDirty=false;
			}
		}


		public string Sql
		{
			get { return _sql;}
			set
			{
				if(_sql!=value)
					OnChangeReport(true);

				_sql=value;
			}
		}


		public string Xsl
		{
			get { return _xsl;}
			set
			{
				if(_xsl!=value)
					OnChangeReport(false);

				_xsl=value;
			}
		}


		protected internal override  Report _Clone(string Name , string Description)
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();

			decimal newId=dacObj.InsertReport(_owner.ID , 0 , 0 , Name , Description , false , this.Sql , this.Xsl);
			return _owner.ReportSystem.GetReport(newId , typeof(CustomSqlReport) , false);
		}

		
		protected internal override void _SaveHeader()
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			dacObj.UpdateReportHeader(this.Owner.ID , this.ID , this._parentReportId , (byte)this.SharingStatus , this.Name , this.Description , this.IsSelected );
		}


		public override void LoadHeader()
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			FI.Common.Data.FIDataTable dataTable=dacObj.ReadReportHeader(this.Owner.ID, this.ID);

			this._parentReportId=(decimal)dataTable.Rows[0]["parent_report_id"];
			this._name=(string)dataTable.Rows[0]["name"];
			this._description=(string)dataTable.Rows[0]["description"];
			this._isSelected=(bool)dataTable.Rows[0]["is_selected"];
			this._sharing=(Report.SharingEnum)((byte)dataTable.Rows[0]["sharing_status"]);
			this._maxSubscriberSharing=(Report.SharingEnum)((byte)dataTable.Rows[0]["max_subscriber_sharing_status"]);
		}

		override protected internal void _Open()
		{
			short sharing=0;
			short maxSubscriberSharing=0;

			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			dacObj.ReadReport(_owner.ID , this.ID, 
				ref this._parentReportId,
				ref this._name , 
				ref this._description , 
				ref sharing,
				ref maxSubscriberSharing,
				ref this._isSelected, 
				ref this._sql,
				ref this._xsl,
				ref _undoStateCount,
				ref _redoStateCount);

			this._sharing=(Report.SharingEnum)sharing;
			this._maxSubscriberSharing=(Report.SharingEnum)maxSubscriberSharing;
		}

        override protected internal void _Save()
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
            dacObj.SaveReport(_owner.ID , this._id  , this.Sql , this.Xsl );
		}

		override protected internal void _LoadState(short StateCode)
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			dacObj.LoadState(this.ID , StateCode, ref this._sql , ref this._xsl , ref this._undoStateCount , ref this._redoStateCount);
		}


		override protected internal void _SaveState()
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			dacObj.SaveState(this.ID , this.MaxStateCount , this.Sql, this.Xsl, ref this._undoStateCount);
		}
		
		
		override protected internal void _DeleteStates()
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			dacObj.DeleteReportStates(this.ID);
		}


		override protected internal void _ClearResult()
		{
			_resultData=null;
		}

		

		override protected internal void _Execute()
		{
			try
			{
				FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
				this._resultData=dacObj.ReadReportResult(this.Sql ,this.Owner.OltpDatabase);
				this._error="";
			}
			catch(Exception exc)
			{
				this._error=exc.ToString();
				throw exc;
			}
		}


		protected internal override void _BeginExecute()
		{
			throw new NotImplementedException();
		}

		protected internal override void _EndExecute()
		{
			throw new NotImplementedException();
		}

		protected internal override void _CancelExecute()
		{
			throw new NotImplementedException();
		}



		override protected internal void _Delete(bool DenyShared)
		{
			FI.Common.DataAccess.ICustomSqlReportsDA dacObj=DataAccessFactory.Instance.GetCustomSqlReportsDA();
			dacObj.DeleteReport(_owner.ID , this.ID, DenyShared);	
		}





		public override string Export(ExportFormat Format)
		{
			if(_error!=null && _error!="")
				return "Error: " + _error;

			if(this.State!=StateEnum.Executed)
				throw new Exception("Report is not executed");

			if(this._resultData==null)
				throw new Exception("Report contains no data");

			if (Format==ExportFormat.HTML)
				return ExportToHTML();
			else if (Format==ExportFormat.CSV)
				return ExportToCSV();
			else
				throw new NotSupportedException();

		}



        public static string DefaultXsl()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // HTML FIRST PART
            sb.Append(@"

<HTML>
<HEAD>
<meta charset='utf-8'></meta>

<style type=""text/css"">
		TABLE { BORDER-COLLAPSE: collapse }
		th {cursor: hand; font-family:Tahoma; font-size :12px; font-weight :bold; background-color: cccccc;  padding-left:6pt; padding-right:6pt; BORDER-RIGHT: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-BOTTOM: navy 1px solid;}
        .thsort { color: red;}
		.th1 {cursor: hand; font-family:Tahoma; font-size :12px; font-weight :bold; color: navy; background-color: white;  padding-left:6pt; padding-right:6pt; }
		.th2 {font-family:Tahoma; font-size :12px; font-weight :bold; color: navy; background-color: d5d5d5;  padding-left:6pt; padding-right:6pt; }
		.th3 {cursor: hand; font-family:Tahoma; font-size :12px; font-weight :bold; color: navy; background-color: white;  padding-left:2pt; padding-right:2pt;}
		td {font-family:Tahoma; font-size :10px; background-color: white; padding-left:2pt; padding-right:2pt; BORDER-RIGHT: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-BOTTOM: navy 1px solid;}
		select {font-family:Tahoma; font-size :10px; font-weight :bold; color: navy; background-color: white; padding-left:2pt; padding-right:2pt;}
</style>


</HEAD>
<BODY>

<table cellspacing=0 cellpadding=3 width=100% class=capt><tr><td align=right style='BORDER-WIDTH:0px;background-color:red'>
<font face=ArialBlack color=white size=1><b><i>&copy;&nbsp;FieldForce Solutions &nbsp;</b></i></font>
</tr></td>
<tr><td style='BACKGROUND:#e0e0e0; BORDER-WIDTH:0px;'>
<B><font color=red size=-1><!--@DESCRIPTION--></font></B><font color=black size=-2><i>&nbsp;(description)</i></font>
<br>
<B><font color=4682b4 size=-2><!--@NAME--></font></B><font color=black size=-2><i>&nbsp;(name)</i></font>
<BR>
<B><font color=4682b4 size=-2><!--@OWNER--></font></B><font color=black size=-2><i>&nbsp;(owner)</i></font>
<BR>
<B><font color=4682b4 size=-2><!--@TIME--></font></B><font color=black size=-2><i>&nbsp;(executed)</i></font>
</td></tr>
</table>
<hr>

<script type='text/javascript'>
    
	var curSortCol=-1;
	var curSortOrder=0;
	var curSortHdr;
	
	function sort(colIndex, sortType, sortHdr){
		
		if(curSortHdr)
			curSortHdr.className=null;
		sortOrder=1;
		if(curSortOrder===1)
			sortOrder=-1;
	
				
		var tbl = document.getElementById('tableBody');				
		var store = [];
		for(var i=0, len=tbl.rows.length; i<len; i++){
			var row = tbl.rows[i];
			var sortVal=row.cells[colIndex].textContent || row.cells[colIndex].innerText;
			if(sortType==='num') {
				sortVal=parseFloat(sortVal);
				if(isNaN(sortVal))
					sortVal=0;
			} 
			store.push([sortVal, row]);
		}
		store.sort(function(a, b){
			return (a[0] === b[0]) ? 0 : (a[0] > b[0] ? sortOrder : -1*sortOrder);
		}); 
		for(var i=0, len=store.length; i<len; i++){
			tbl.appendChild(store[i][1]);
		}
		store = null;
		curSortCol=colIndex;
		curSortOrder=sortOrder;	
		curSortHdr=sortHdr;
		curSortHdr.className = 'thsort';		
	}
</script>

<!--@CONTENT-->

</BODY>
</HTML>
");



            return sb.ToString();
        }




        private string ExportToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // XML DOC
            //sb.Append("<root>");
            //sb.Append("<Schema>");
            //foreach (System.Data.DataColumn col in this._resultData.Columns)
            //{
            //    sb.Append("<ElementType name='row'>");
            //    sb.Append("<AttributeType name='");
            //    sb.Append(System.Xml.XmlConvert.EncodeName(col.ColumnName));
            //    sb.Append("'>");
            //    if (FI.Common.Data.FIDataTable.IsNumeric(col))
            //        sb.Append("<datatype type='numeric'/>");
            //    else
            //        sb.Append("<datatype type='string'/>");

            //    sb.Append("</AttributeType>");
            //    sb.Append("</ElementType>");
            //}

            //sb.Append("</Schema>");

            //sb.Append("<data>");
            //foreach (System.Data.DataRow row in this._resultData.Rows)
            //{
            //    sb.Append("<row ");
            //    foreach (System.Data.DataColumn col in this._resultData.Columns)
            //    {
            //        sb.Append(System.Xml.XmlConvert.EncodeName(col.ColumnName));
            //        sb.Append(@"=""");
            //        sb.Append(System.Web.HttpUtility.HtmlEncode(row[col].ToString()));
            //        sb.Append(@""" ");
            //    }
            //    sb.Append(" />");
            //}
            //sb.Append("</data>");
            //sb.Append("</root>");
            //var xml = sb.ToString();

            // data
            sb.Append("<table border=\"0\" bgcolor=\"white\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<thead><tr>");
            int cols = 0;
            foreach (System.Data.DataColumn col in this._resultData.Columns)
            {
                sb.AppendFormat("<th onclick=\"sort({0},'{1}', this)\" nowrap=\"\">{2}</th>",
                    cols,
                    FI.Common.Data.FIDataTable.IsNumeric(col) ? "num" : "txt",
                    HttpUtility.HtmlEncode(col.ColumnName));
                cols++;
            }
            sb.Append("</tr></thead>");
            sb.Append("<tbody id=\"tableBody\" style=\"overflow:scroll;\">");
            foreach (System.Data.DataRow row in this._resultData.Rows)
            {
                sb.Append("<tr>");
                foreach (System.Data.DataColumn col in this._resultData.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[col]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");

            // XSL DOC
            string xsl = this.Xsl;
            xsl = xsl.Replace("<!--@NAME-->", this.Name);
            xsl = xsl.Replace("<!--@DESCRIPTION-->", this.Description);
            xsl = xsl.Replace("<!--@OWNER-->", this.Owner.Name);
            xsl = xsl.Replace("<!--@TIME-->", System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString());
            xsl = xsl.Replace("<!--@CONTENT-->", sb.ToString());

            return xsl;
        }



		private string ExportToCSV()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			// column headers
			foreach(System.Data.DataColumn col in this._resultData.Columns)
			{
				sb.Append(col.ColumnName);
				sb.Append("\t");
			}
			sb.Append("\r\n");

			// row data
			foreach(System.Data.DataRow row in this._resultData.Rows)
			{
				foreach(System.Data.DataColumn col in this._resultData.Columns)
				{
					sb.Append(row[col].ToString());
					sb.Append("\t");
				}
				sb.Append("\r\n");
			}

			return sb.ToString();
		}




	}


}
