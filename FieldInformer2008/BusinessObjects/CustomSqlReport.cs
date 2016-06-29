using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;

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
			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			// HTML FIRST PART
			sb.Append(@"

<HTML>
<HEAD>
<meta charset='utf-8'></meta>

<style type=""text/css"">
		TABLE { BORDER-COLLAPSE: collapse }
		th {cursor: hand; font-family:Tahoma; font-size :12px; font-weight :bold; background-color: cccccc;  padding-left:6pt; padding-right:6pt; BORDER-RIGHT: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-BOTTOM: navy 1px solid;}
		.th1 {cursor: hand; font-family:Tahoma; font-size :12px; font-weight :bold; color: navy; background-color: white;  padding-left:6pt; padding-right:6pt; }
		.th2 {font-family:Tahoma; font-size :12px; font-weight :bold; color: navy; background-color: d5d5d5;  padding-left:6pt; padding-right:6pt; }
		.th3 {cursor: hand; font-family:Tahoma; font-size :12px; font-weight :bold; color: navy; background-color: white;  padding-left:2pt; padding-right:2pt;}
		td {font-family:Tahoma; font-size :10px; background-color: white; padding-left:2pt; padding-right:2pt; BORDER-RIGHT: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-BOTTOM: navy 1px solid;}
		select {font-family:Tahoma; font-size :10px; font-weight :bold; color: navy; background-color: white; padding-left:2pt; padding-right:2pt;}
</style>


<SCRIPT LANGUAGE=""JScript""><!--




function window.onload()
{
	divMenuToken.outerHTML = xmlDoc.transformNode(xslDoc.documentElement);
}
	









function sort(field)
{
	
	sortby=xslDoc.documentElement.selectSingleNode(""//xsl:sort/@select"");
	
	cur_sort_field=xslDoc.documentElement.selectSingleNode(""//xsl:variable[@name='cur_sort_field']/@select"");
	cur_sort_order=xslDoc.documentElement.selectSingleNode(""//xsl:variable[@name='cur_sort_order']/@select"");
	cur_sort_data_type=xslDoc.documentElement.selectSingleNode(""//xsl:variable[@name='cur_sort_data_type']/@select"");
	
	if (xmlDoc.selectSingleNode(""/root/Schema/ElementType[@name='row']/AttributeType[@cid='""+ field.replace(/@/, """")  +""']/datatype[@type='number']"") )
			cur_sort_data_type.text=""'number'"" ;
	else	
			cur_sort_data_type.text=""'text'"" ;
			

	if(cur_sort_field.text ==  ""'"" + field.replace(/@/, """")  + ""'"")
	{
		
		if(cur_sort_order.text == ""'ascending'"")
			{
			cur_sort_order.text =""'descending'"" ;
			}
		else
			{
			cur_sort_order.text = ""'ascending'"";
			}
			
	}
	
	if (cur_sort_order.text !=""'ascending'"" &&  cur_sort_order.text !=""'descending'"" )
		{
			cur_sort_order.text =""'ascending'"" ;
		}
	
	
	cur_sort_field.text=""'"" + field.replace(/@/, """") + ""'"" ;
	sortby.text=field;

	output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc.documentElement);
	
}





function prev(pageNumber)
{
param = xslDoc.documentElement.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
if (isNaN(param.text) || Number(param.text)<=0)
		{ }
else
		{
		param.text=String(Number(param.text)-1)
		output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc.documentElement);
		}
}





function next(pageNumber , CountOfPages)
{
param = xslDoc.documentElement.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
if (isNaN(param.text) || Number(param.text)>Number(CountOfPages))
		{ }
else
		{
		param.text=String(Number(param.text)+1)
		output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc.documentElement);
		}
}




	
function page(curPage , pageNumber)
{

		param = xslDoc.documentElement.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
		if (Number(curPage)==Number(pageNumber))
				{ }
		else
				{
				param.text=pageNumber-1;
				output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc.documentElement);
				}

}

	
	
			

function SetRecsPerPage(numOfRecs)
{
	param = xslDoc.documentElement.selectSingleNode(""//xsl:param[@name='recordsPerPage']/@select"");
	param.text=numOfRecs;
	
	param2 = xslDoc.documentElement.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
	param2.text=0;
	
	output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc.documentElement);
}
				
				




//--></SCRIPT>

</HEAD>
<BODY>
");


// REPORT HEADER 

sb.Append(@"
<table cellspacing=0 cellpadding=3 width=100% class=capt><tr><td align=right style='BORDER-WIDTH:0px;background-color:red'>
<font face=ArialBlack color=white size=1><b><i>&copy;&nbsp;" + AppConfig.CompanyName + @"&nbsp;</b></i></font>
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
<hr>");


// HTML SECOND PART
sb.Append(@"

<XML ID=""xslDoc"">

<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""  
version=""1.0""  xmlns:html=""http://www.w3.org/tr/rec-html40"">


<xsl:template match=""/"">



				<div id=""output"">
					<xsl:apply-templates select=""root/data""/>
				</div>
		

</xsl:template>




<xsl:template match=""root/data"" >




<xsl:param name=""recordsPerPage"" select=""50""/>
<xsl:param name=""pageNumber"" select=""0""/>


<xsl:variable name=""records_total"" select=""count(/root/data/row)""/>
<xsl:variable name=""count_of_pages"" select=""$records_total div $recordsPerPage ""/>


<xsl:variable name=""cur_sort_field"" select=""'no_sort_field'""/>
<xsl:variable name=""cur_sort_order"" select=""'ascending'""/>


<xsl:variable name=""cur_sort_data_type"" select=""'text'""/>


	


<TABLE border='0' bgcolor='white' cellpadding='0' cellspacing='0' >
<THEAD>
	<TH class='th2' nowrap='1' valign='middle'>
			Records Per Page: 
			<SELECT NAME='selRecsPerPage' onChange='SetRecsPerPage(this.options[selectedIndex].value)'>
				
				<xsl:choose>
					<xsl:when test='$recordsPerPage=10'>
						<OPTION VALUE='10' SELECTED='True'>10</OPTION>
					</xsl:when>
					<xsl:otherwise>
						<OPTION VALUE='10'>10</OPTION>
					</xsl:otherwise>
				</xsl:choose>
				
				
				<xsl:choose>
					<xsl:when test='$recordsPerPage=25'>
						<OPTION VALUE='25' SELECTED='True'>25</OPTION>
					</xsl:when>
					<xsl:otherwise>
						<OPTION VALUE='25'>25</OPTION>
					</xsl:otherwise>
				</xsl:choose>
				
				
				<xsl:choose>
					<xsl:when test='$recordsPerPage=50'>
						<OPTION VALUE='50' SELECTED='True'>50</OPTION>
					</xsl:when>
					<xsl:otherwise>
						<OPTION VALUE='50'>50</OPTION>
					</xsl:otherwise>
				</xsl:choose>
				
				
				<xsl:choose>
					<xsl:when test='$recordsPerPage=100'>
						<OPTION VALUE='100' SELECTED='True'>100</OPTION>
					</xsl:when>
					<xsl:otherwise>
						<OPTION VALUE='100'>100</OPTION>
					</xsl:otherwise>
				</xsl:choose>
				
				
			</SELECT>
			Total: <xsl:value-of select=""$records_total""/>
	</TH>
	<xsl:choose>
		<xsl:when test='$pageNumber>0'>
			<TH class='th3' onclick='prev({$pageNumber})' nowrap='1'>
					&lt;&lt; Previous page
			</TH>
		</xsl:when>
		<xsl:otherwise>
			<TH class='th3' nowrap='1'>
					<font color='d5d5d5'>&lt;&lt; Previous page</font>
			</TH>
		</xsl:otherwise>
	</xsl:choose>	
	<TH class='th1' nowrap='1'>
			<TABLE border='0' bgcolor='white' cellpadding='0' cellspacing='0'>
				<THEAD>
					<xsl:call-template name=""page_loop"">  
						<xsl:with-param name=""page_count"">  
							<xsl:value-of select=""1""/>  
						</xsl:with-param>  
						<xsl:with-param name=""max_page_count"">  
							<xsl:value-of select=""$count_of_pages""/>  
						</xsl:with-param>  
						<xsl:with-param name=""cur_page"">  
							<xsl:value-of select=""$pageNumber+1""/>  
						</xsl:with-param>  
					</xsl:call-template>  
				</THEAD>
			</TABLE>
	</TH>
	<xsl:choose>
		<xsl:when test='$pageNumber+1 &lt; $count_of_pages'>
			<TH class='th1' onclick='next({$pageNumber} , {$count_of_pages} )' nowrap='1'>
					Next page &gt;&gt;
			</TH>
		</xsl:when>
		<xsl:otherwise>
			<TH class='th3' nowrap='1'>
					<font color='d5d5d5'>Next page &gt;&gt;</font>
			</TH>
		</xsl:otherwise>
	</xsl:choose>	
</THEAD>
</TABLE>


<TABLE border='0' bgcolor='white' cellpadding='0' cellspacing='0'>
<TR>
	<TD HEIGHT='10' style=""BORDER-RIGHT: white 1px solid; BORDER-LEFT: white 1px solid; BORDER-TOP: white 1px solid; BORDER-BOTTOM: white 1px solid;"">
	</TD>
</TR>
</TABLE>



<TABLE border='0' bgcolor='white' cellpadding='0' cellspacing='0'>
<THEAD>
<xsl:for-each select=""/root/Schema/ElementType[@name='row']/AttributeType"">
    			<TH onclick=""sort('@{@cid}')"" NOWRAP='True'>
    			<xsl:value-of select=""@name"" />
    			<xsl:if test=""@cid=$cur_sort_field"">
    				<xsl:if test=""$cur_sort_order='ascending'"">
    					<font face='webdings' color='red'> 5</font>
    				</xsl:if>
    				<xsl:if test=""$cur_sort_order='descending'"">
    					<font face='webdings' color='red'> 6</font>
    				</xsl:if>
					
    			</xsl:if>
    			</TH>
</xsl:for-each>
</THEAD>



<TBODY id='tableBody' style=""overflow:scroll;"">
	<xsl:for-each select=""/root/data/row"">					
   	<xsl:sort select=""no_sort_field"" order=""{$cur_sort_order}"" data-type=""{$cur_sort_data_type}"" />
	
	
					
	<xsl:if test=""position()=0 or (position() &gt; ($recordsPerPage) * number($pageNumber)  and
position()  &lt;= number($recordsPerPage * number($pageNumber) + $recordsPerPage))"">
	
	
		<TR>	
		<xsl:for-each select=""@*"">		
		
   			 <td><xsl:value-of select=""."" /></td>
				
		</xsl:for-each>	
		</TR>
		
	</xsl:if>
		
	</xsl:for-each>		
</TBODY>
</TABLE>
</xsl:template>





<xsl:template name=""page_loop"">  
<xsl:param name=""page_count""/>  
<xsl:param name=""max_page_count""/>
<xsl:param name=""cur_page""/>

<TH class=""th3"" style=""BORDER-RIGHT: white 1px solid; BORDER-LEFT: white 1px solid; BORDER-TOP: white 1px solid; BORDER-BOTTOM: white 1px solid;"" onclick='page({$cur_page} , {$page_count})'>
<xsl:choose>
<xsl:when test='$page_count=$cur_page'>
	<font color='red'>
	<xsl:value-of select=""$page_count""/>
	</font>
</xsl:when>
<xsl:otherwise>
	<xsl:value-of select=""$page_count""/>
</xsl:otherwise>
</xsl:choose>
</TH>

<xsl:if test='$page_count mod 30=0'>
	<THEAD></THEAD>
</xsl:if>

<xsl:if test=""$page_count &lt; $max_page_count "">  
<xsl:call-template name=""page_loop"">  
	<xsl:with-param name=""page_count"">  
		<xsl:value-of select=""$page_count + 1""/>  
	</xsl:with-param>  
	<xsl:with-param name=""max_page_count"">  
		<xsl:value-of select=""$max_page_count ""/>  
	</xsl:with-param>  	
	<xsl:with-param name=""cur_page"">  
		<xsl:value-of select=""$cur_page""/>  
	</xsl:with-param>  		
</xsl:call-template>  
</xsl:if>  
	
</xsl:template>  





</xsl:stylesheet>

</XML>

<DIV ID=""divMenuToken""></DIV>

</BODY>
</HTML>

				");

			return sb.ToString();
		}



		private string ExportToHTML()
		{
			
            MemoryStream ms=new MemoryStream();
            XmlTextWriter sb = new XmlTextWriter(ms, Encoding.UTF8);

			// XSL DOC
			string xsl=this.Xsl;
			xsl=xsl.Replace("<!--@NAME-->" , this.Name);
			xsl=xsl.Replace("<!--@DESCRIPTION-->" , this.Description);
			xsl=xsl.Replace("<!--@OWNER-->" , this.Owner.Name);
			xsl=xsl.Replace("<!--@TIME-->" , System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() );
            sb.WriteRaw(xsl);

			// XML DOC
            sb.WriteRaw(@"<XML ID=""xmlDoc"">");
            sb.WriteRaw("<root>");
            sb.WriteRaw("<Schema>");

			for(int i=0;i<_resultData.Columns.Count;i++)
			{
                DataColumn col = _resultData.Columns[i];

                sb.WriteRaw("<ElementType name='row'>");
                sb.WriteStartElement("AttributeType");
                sb.WriteAttributeString("cid", "c"+ i.ToString());
                sb.WriteAttributeString("name", col.ColumnName);
                sb.WriteStartElement("datatype");
                if (FI.Common.Data.FIDataTable.IsNumeric(col))
                    sb.WriteAttributeString("type", "number");
                else
                    sb.WriteAttributeString("type", "string");
                sb.WriteEndElement();
                sb.WriteEndElement();
                sb.WriteRaw("</ElementType>");
			}

            sb.WriteRaw("</Schema>");

            sb.WriteRaw("<data>");
			foreach(System.Data.DataRow row in this._resultData.Rows)
            {
                sb.WriteStartElement("row");
                for (int i = 0; i < _resultData.Columns.Count; i++)
                {
                    DataColumn col = _resultData.Columns[i];
                    sb.WriteAttributeString("c" + i.ToString(), row[col].ToString());
                }
                sb.WriteEndElement();
			}
            sb.WriteRaw("</data>");
            sb.WriteRaw("</root>");
            sb.WriteRaw("</XML>");
            sb.Flush();

            string ret = Encoding.UTF8.GetString(ms.ToArray());
            sb.Close();

			return ret;
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
