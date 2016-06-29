using System;
using System.Collections;
using FI.Common.Data;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for CustomMdxReport.
	/// </summary>
	public class CustomMdxReport:Report
	{

		protected internal Olap.Cellset _cellset=new Olap.Cellset();
		protected internal string _mdx="";
		protected internal string _xsl="";
		protected internal string _error="";
		protected internal string _server="";
		protected internal string _database="";



		internal CustomMdxReport(decimal ID , User Owner):base(ID,Owner)
		{
			if(ID==0) //if new
			{
				_xsl=DefaultXsl();

				FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
				_id=dacObj.InsertReport(_owner.ID , 0 , 0 , "New Report" , "" , this.IsSelected , this.Mdx , this.Xsl);

				_isProxy=false;
				_isDirty=false;
			}
		}


		public string SchemaServer
		{
			get { return _server;}
		}

		public string SchemaDatabase
		{
			get { return _database;}
		}

		public string Mdx
		{
			get { return _mdx;}
			set
			{
				if(_mdx!=value)
					OnChangeReport(true);

				_mdx=value;
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


		public Olap.Cellset Cellset
		{
			get { return _cellset;}
		}






		protected internal override  Report _Clone(string Name , string Description)
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();

			decimal newId=dacObj.InsertReport(_owner.ID , 0 , 0 , Name , Description , false , this.Mdx , this.Xsl);
			return _owner.ReportSystem.GetReport(newId , typeof(CustomMdxReport) , false);
		}

		
		protected internal override void _SaveHeader()
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
			dacObj.UpdateReportHeader(this.Owner.ID , this.ID , this._parentReportId , (byte)this.SharingStatus , this.Name , this.Description , this.IsSelected );
		}


		public override void LoadHeader()
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
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

			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
			dacObj.ReadReport(_owner.ID , this.ID, 
				ref this._parentReportId,
				ref this._name , 
				ref this._description , 
				ref sharing,
				ref maxSubscriberSharing,
				ref this._isSelected, 
				ref this._mdx,
				ref this._xsl,
				ref this._server , 
				ref this._database,
				ref _undoStateCount,
				ref _redoStateCount);

			this._sharing=(Report.SharingEnum)sharing;
			this._maxSubscriberSharing=(Report.SharingEnum)maxSubscriberSharing;
		}

		override protected internal void _Close(bool SaveFromState)
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();

			if(SaveFromState)
				dacObj.SaveReport(_owner.ID , this._id  , this.Mdx , this.Xsl );
		}

		override protected internal void _LoadState(short StateCode)
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
			dacObj.LoadState(this.ID , StateCode, ref this._mdx , ref this._xsl , ref this._undoStateCount , ref this._redoStateCount);
		}


		override protected internal void _SaveState()
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
			dacObj.SaveState(this.ID , this.MaxStateCount , this.Mdx, this.Xsl, ref this._undoStateCount);
		}

		
		
		override protected internal void _DeleteStates()
		{
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
			dacObj.DeleteReportStates(this.ID);
		}


		override protected internal void _ClearResult()
		{
			this._cellset.Clear();
		}

		

		override protected internal void _Execute()
		{
			try
			{
				string taskGuid=Guid.NewGuid().ToString();
				string taskTag=string.Format("User: {0} ({1}), CustomMdxReport: {2} ({3})",
					this._owner.Name, this._owner.ID, this.Name, this.ID);

				FI.DataAccess.OlapSystem dacOlapSystem=DataAccessFactory.Instance.GetOlapSystemDA();
				string data=dacOlapSystem.BuildCellset(this.SchemaServer , this.SchemaDatabase , this.Mdx,  taskGuid, taskTag);

				this._cellset.LoadCellset(data);

				_error="";
			}
			catch(Exception exc)
			{
				_error=exc.Message;
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
			FI.DataAccess.CustomMdxReports dacObj=DataAccessFactory.Instance.GetCustomMdxReportsDA();
			dacObj.DeleteReport(_owner.ID , this.ID, DenyShared);	
		}





		public override string Export(ExportFormat Format)
		{
			if(_error!=null && _error!="")
				return "Error: " + _error;
			
			if(this.State!=StateEnum.Executed)
				throw new Exception("Report is not executed");

			if(this.Cellset.IsValid==false || this.Cellset.Axis0PosCount==0 || this.Cellset.Axis1PosCount==0)
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
			return @"

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

		sortby = xslDoc.selectSingleNode(""//xsl:sort/@select"");
		orderby  = xslDoc.selectSingleNode(""//xsl:sort/@order"");
		
		cur_sort_field=xslDoc.selectSingleNode(""//xsl:variable[@name='cur_sort_field']/@select"");
		cur_sort_order=xslDoc.selectSingleNode(""//xsl:variable[@name='cur_sort_order']/@select"");
		
		if(sortby.text == field){
			
			if(orderby.text == 'ascending')
				{
				cur_sort_order.text =""'descending'"" ;
				orderby.text = ""descending"";
				}
			else
				{
				cur_sort_order.text = ""'ascending'"";
				orderby.text = ""ascending"";
				}
				
		}
		
		if (cur_sort_order.text !=""'ascending'"" &&  cur_sort_order.text !=""'descending'"" )
			{
				cur_sort_order.text =""'ascending'"" 
			}
		
		if (orderby.text !=""ascending"" &&  orderby.text !=""descending"" )
			{
				orderby.text =""ascending"" 
			}	
		
		cur_sort_field.text=""'"" + field.replace(/@/, """") + ""'"" ;
		sortby.text=field;
		output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc);
		
	}





	function prev(pageNumber)
	{
	param = xslDoc.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
	if (isNaN(param.text) || Number(param.text)<=0)
			{ }
	else
			{
			param.text=String(Number(param.text)-1)
			output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc);
			}
	}





	function next(pageNumber , CountOfPages)
	{
	param = xslDoc.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
	if (isNaN(param.text) || Number(param.text)>Number(CountOfPages))
			{ }
	else
			{
			param.text=String(Number(param.text)+1)
			output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc);
			}
	}




		
	function page(curPage , pageNumber)
	{

			param = xslDoc.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
			if (Number(curPage)==Number(pageNumber))
					{ }
			else
					{
					param.text=pageNumber-1;
					output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc);
					}

	}

		
		
				

	function SetRecsPerPage(numOfRecs)
	{
		param = xslDoc.selectSingleNode(""//xsl:param[@name='recordsPerPage']/@select"");
		param.text=numOfRecs;
		
		param2 = xslDoc.selectSingleNode(""//xsl:param[@name='pageNumber']/@select"");
		param2.text=0;
		
		output.innerHTML = xmlDoc.documentElement.transformNode(xslDoc);
	}
					
					




	//--></SCRIPT>

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




	<XML ID=""xslDoc"">


	<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""  
	version=""1.0""  xmlns:html=""http://www.w3.org/tr/rec-html40""  >


	<xsl:template match=""/"">



					<div id=""output"">
						<xsl:apply-templates select=""cellset""/>
					</div>
			

	</xsl:template>




	<xsl:template match=""cellset"" >




	<xsl:param name=""recordsPerPage"" select=""50""/>
	<xsl:param name=""pageNumber"" select=""0""/>


	<xsl:variable name=""records_total"" select=""count(/cellset/row[@head='0'])""/>
	<xsl:variable name=""count_of_pages"" select=""$records_total div $recordsPerPage ""/>


	<xsl:variable name=""cur_sort_field"" select=""'no_sort_field'""/>
	<xsl:variable name=""cur_sort_order"" select=""'no_sort_order'""/>



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

	<xsl:for-each select=""/cellset/row[@head='1']"">

	<THEAD>
			<xsl:for-each select=""@*[name()!='ord' and name()!='head' ]"">
			
				<xsl:variable name=""temp_sort"" select=""name()"" />
				
				<xsl:choose>
						<xsl:when test=""starts-with(name(),'ce')"">
							<th></th>			
						</xsl:when>
						<xsl:otherwise>
				
    						<TH onclick=""sort('@c{substring-after($temp_sort, 'ch')}')"" NOWRAP='True'>
    						<xsl:value-of select=""."" />
    						<xsl:if test=""concat('c' , substring-after($temp_sort, 'ch') )=$cur_sort_field"">
    							<xsl:if test=""$cur_sort_order='ascending'"">
    								<font face='webdings' color='red'> 5</font>
    							</xsl:if>
    							<xsl:if test=""$cur_sort_order='descending'"">
    								<font face='webdings' color='red'> 6</font>
    							</xsl:if>
						    	
    						</xsl:if>
    						</TH>
				
    					</xsl:otherwise>	
						
    			</xsl:choose>		
					
					
		</xsl:for-each>
	</THEAD>

	</xsl:for-each>

	<TBODY CLASS=""TableBody"" id='tableBody' style=""overflow:scroll;"">
		
		
		
		<xsl:for-each select=""/cellset/row[@head='0']"">		
		<xsl:sort select=""no_sort_when_page_loaded"" order=""ascending"" data-type='number'/>
		
		<xsl:if test=""position()=0 or (position() &gt; ($recordsPerPage) * number($pageNumber)  and
	position()  &lt;= number($recordsPerPage * number($pageNumber) + $recordsPerPage))"">
		
		
			
		
			<TR>	
			<xsl:for-each select=""@*[name()!='ord' and name()!='head' ]"">
				<xsl:choose>
					<xsl:when  test=""starts-with(name(),'ch')"">
   						<th><xsl:value-of select=""."" /></th>
					</xsl:when>
					<xsl:otherwise>
   						<td><xsl:value-of select=""."" /></td>
   					</xsl:otherwise>
   				</xsl:choose>	
				
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

	<TH  class=""th3"" style=""BORDER-RIGHT: white 1px solid; BORDER-LEFT: white 1px solid; BORDER-TOP: white 1px solid; BORDER-BOTTOM: white 1px solid;"" onclick='page({$cur_page} , {$page_count})'>
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
			";
		}



		private string ExportToHTML()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			
						
			// XSL DOC
			string xsl=this.Xsl;
			xsl=xsl.Replace("<!--@NAME-->" , this.Name);
			xsl=xsl.Replace("<!--@DESCRIPTION-->" , this.Description);
			xsl=xsl.Replace("<!--@OWNER-->" , this.Owner.Name);
			xsl=xsl.Replace("<!--@TIME-->" , System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() );
			sb.Append(xsl);



			sb.Append(@"<XML ID=""xmlDoc"">");
			sb.Append(@"<cellset>");

			int Ax0MemCount=_cellset.Axis0TupleMemCount;
			int Ax1MemCount=_cellset.Axis1TupleMemCount;
			int Ax0PosCount=_cellset.Axis0PosCount;
			int Ax1PosCount=_cellset.Axis1PosCount;

			for(int i=0; i<Ax0MemCount ; i++)
			{
				sb.Append("<row ord='" + i.ToString() + "' head='1' ");
				// empty rows in beginning
				for(int j=0; j<Ax1MemCount ; j++)
				{  
					sb.Append("ce" + j.ToString() + "='' ");
				}
				// column captions
				for(int n=0; n<Ax0PosCount ; n++)
				{  
					string memCaption=_cellset.GetCellsetMember(0 , i , n).Name;
					sb.Append("ch" + (n+Ax1MemCount).ToString() + @"=""" + System.Web.HttpUtility.HtmlEncode(memCaption) + @""" ");
				}
				sb.Append(" />");
			}

			for(int i=0; i<Ax1PosCount ; i++)
			{
				sb.Append("<row ord='" + (Ax0MemCount + i).ToString() + "' head='0' ");
				// row captions
				for(int j=0; j<Ax1MemCount ; j++)
				{  
					string memCaption=_cellset.GetCellsetMember(1 , j , i).Name;
					sb.Append("ch" + j.ToString() + @"=""" + System.Web.HttpUtility.HtmlEncode(memCaption) +@""" ");
				}
				// cells
				for(int n=0; n<Ax0PosCount ; n++)
				{  
					Olap.Cell cell=_cellset.GetCell(n , i);
					sb.Append("c" + (n+Ax1MemCount).ToString() + @"=""" + cell.FormattedValue + @""" ");
				}
				sb.Append(" />");
			}

			sb.Append(@"</cellset>");
			sb.Append("</XML>");

			return sb.ToString();

		}




		
		private string ExportToCSV()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();


			int Ax0MemCount=this.Cellset.Axis0TupleMemCount;
			int Ax1MemCount=this.Cellset.Axis1TupleMemCount;
			int Ax0PosCount=this.Cellset.Axis0PosCount;
			int Ax1PosCount=this.Cellset.Axis1PosCount;
			
			//-------------------------------- table---------------------------------------

			if(Ax0PosCount==0 && Ax0PosCount==0)
			{
				return "Cellset contains no data";
			}


			for (int i=0 ; i<Ax0MemCount  ; i++ )
			{

				for (int j=0 ; j<Ax1MemCount  ; j++ )
					sb.Append("\t");

				for (int j=0 ; j<Ax0PosCount   ; j++ )
				{
					Olap.CellsetMember mem=this.Cellset.GetCellsetMember(0 , i , j);
					sb.Append(mem.Name);
					sb.Append("\t"); 
				}
				
				sb.Append("\r\n");
			}


			for (int i=0 ; i<Ax1PosCount ; i++ )
			{
						
				for (int j=0 ; j<Ax1MemCount  ; j++ )
				{	
					Olap.CellsetMember mem=this.Cellset.GetCellsetMember(1,j,i);
					sb.Append(mem.Name);
					sb.Append("\t"); 
				}

				for (int j=0 ; j<Ax0PosCount   ; j++ )
				{
					Olap.Cell olapCell=this.Cellset.GetCell(j , i);
					sb.Append(olapCell.FormattedValue);
					sb.Append("\t"); 
				}

				sb.Append("\r\n");
			}

			return sb.ToString();
		}



	}


}
