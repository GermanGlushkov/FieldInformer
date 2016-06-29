using System;
using System.Collections;
using System.Collections.Specialized;
using FI.Common;
using FI.Common.Data;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for StorecheckReport.
	/// </summary>
	public class StorecheckReport:Report
	{

		public enum ProductsJoinLogicEnum
		{
			OR=0,
			AND=1
		}

		public enum ReportPageTypeEnum
		{
			Delivered=0,
			NotDelivered=1,
			NeverDelivered=2
		}

		public enum DataSourceEnum:byte
		{
			Deliveries_And_Sales=0,
			Deliveries=1,
			Sales=2
		}



		protected StringCollection _productsSerNoList=new StringCollection();
		protected ProductsJoinLogicEnum _productsJoinLogic=ProductsJoinLogicEnum.OR;
		protected short _days=0;
		protected DataSourceEnum _dataSource=DataSourceEnum.Deliveries_And_Sales;
		protected bool _inSelOnly=false;
		protected bool _inBSelOnly=true;
		protected string  _oltpDatabase=null;
		protected NameValueCollection _filterList=new NameValueCollection();
		protected DateTime _cacheTimestamp=DateTime.Today;
		protected bool _cacheExists=false;


		internal StorecheckReport(decimal ID , User Owner):base(ID,Owner)
		{
			if(ID==0) //if new
			{
				string productsXml=null;
				string filterXml=null;
				this.SaveToXml(ref productsXml , ref filterXml);

				FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
				_id=dacObj.InsertReport(_owner.ID , 0 , 0 , "New Report" , "" , this.IsSelected,  productsXml , 
					(byte)this.ProductsJoinLogic ,  this.Days , filterXml , this._cacheTimestamp, this.InSelOnly, this.InBSelOnly, (byte)this.DataSource);

				_isProxy=false;
				_isDirty=false;
			}

			_oltpDatabase=Owner.OltpDatabase;
		}


		protected override void OnChangeReport(bool ClearResult)
		{
			base.OnChangeReport (ClearResult);
			if(ClearResult)
				this.ClearCache();
		}

		public ProductsJoinLogicEnum ProductsJoinLogic
		{
			get { return _productsJoinLogic; }
			set 
			{
				if(_productsJoinLogic!=value)
					this.OnChangeReport(true);

				_productsJoinLogic=value;
			}
		}

		public short Days
		{
			get { return _days; }
			set 
			{
				if(_days!=value)
					this.OnChangeReport(true);

				_days=value;
			}
		}

		public DataSourceEnum DataSource
		{
			get { return _dataSource; }
			set 
			{
				if(_dataSource!=value)
					this.OnChangeReport(true);

				_dataSource=value;
			}
		}

		public bool InSelOnly
		{
			get { return _inSelOnly; }
			set 
			{
				if(_inSelOnly!=value)
					this.OnChangeReport(true);

				_inSelOnly=value;
			}
		}

		public bool InBSelOnly
		{
			get { return _inBSelOnly; }
			set 
			{
				if(_inBSelOnly!=value)
					this.OnChangeReport(true);

				_inBSelOnly=value;
			}
		}

		public void ClearFilter()
		{
			if(this._filterList.Count>0)
				this.OnChangeReport(false); //do not clear result

			this._filterList.Clear();
		}

		public string GetFilterItem(string ColumnName)
		{
			return this._filterList[ColumnName];
		}

		public void SetFilterItem(string ColumnName , string Value)
		{
			string existingValue=this._filterList[ColumnName];
			if(existingValue!=Value)
				this.OnChangeReport(false);  //do not clear result
			
			if(Value==null || Value=="")
				this._filterList.Remove(ColumnName);
			else if(existingValue==null)
				this._filterList.Add(ColumnName, Value);
			else
				this._filterList[ColumnName]=Value;
		}


		public void RemoveFilterItem(string SerNo)
		{
			this.OnChangeReport(true);
			this._productsSerNoList.Remove(SerNo);
		}



		public string FilterExpression
		{
			get
			{
				string expr="";
				if(this._filterList.Count==0)
					return expr;

				foreach(string col in this._filterList)
				{
					string filt=this._filterList[col];
					if(filt!=null && filt!="")
					{
						expr=expr + col + " LIKE '" + filt.Replace("'", "''");

						//apply wildcard
						if(filt.EndsWith("%")==false)
							expr=expr + "%";

						expr=expr +"' AND ";
					}
				}

				//remove last 5 digits
				if(expr.Length>=5)
					expr=expr.Remove(expr.Length-5,5);

				return expr;
			}
		}



		public void ClearAllProductSerNo()
		{
			if(this._productsSerNoList.Count>0)
				this.OnChangeReport(true);

			this._productsSerNoList.Clear();
		}

		public void AddProductSerNo(string SerNo)
		{
			this.OnChangeReport(true);
			this._productsSerNoList.Add(SerNo);
		}


		public void RemoveProductSerNo(string SerNo)
		{
			this.OnChangeReport(true);
			this._productsSerNoList.Remove(SerNo);
		}



		private void LoadFromXml(string ProductsXml , string FilterXml)
		{
			System.Xml.XmlDocument doc=null;

			// products
			this._productsSerNoList.Clear();
			doc=new System.Xml.XmlDocument();
			doc.LoadXml(ProductsXml);
			foreach(System.Xml.XmlElement el in doc.GetElementsByTagName("PR"))
			{
				string serno=el.GetAttribute("SN");
				if(serno!=null)
					this._productsSerNoList.Add(serno);
			}

			// filter
			this._filterList.Clear();
			doc=new System.Xml.XmlDocument();
			doc.LoadXml(FilterXml);
			foreach(System.Xml.XmlElement el in doc.GetElementsByTagName("COL"))
			{
				string colName=el.GetAttribute("N");
				string colValue=el.GetAttribute("V");
				if(colName!=null && colValue!=null)
					this._filterList.Add(colName , colValue);
			}
		}


		private void SaveToXml(ref string ProductsXml , ref string FilterXml)
		{
			System.Xml.XmlDocument doc=null;
			System.Xml.XmlElement rootEl=null;

			// products
			doc=new System.Xml.XmlDocument();
			rootEl=doc.CreateElement("PRODUCTS");
			doc.AppendChild(rootEl);
			foreach(string serNo in this._productsSerNoList)
			{
				System.Xml.XmlElement childEl=doc.CreateElement("PR");
				childEl.SetAttribute("SN" , serNo);
				rootEl.AppendChild(childEl);
			}
			ProductsXml=doc.InnerXml;
			
			// filter
			doc=new System.Xml.XmlDocument();
			rootEl=doc.CreateElement("FILTER");
			doc.AppendChild(rootEl);
			for(int i=0;i<this._filterList.Count;i++)
			{
				string colName=this._filterList.Keys[i];
				string val=this._filterList[i];

				System.Xml.XmlElement childEl=doc.CreateElement("COL");
				childEl.SetAttribute("N" , colName);
				childEl.SetAttribute("V" , val);
				rootEl.AppendChild(childEl);
			}
			FilterXml=doc.InnerXml;

		}



		private void ClearCache()
		{
			if(_cacheExists)
			{
				FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
				dacObj.DeleteReportCache(this.ID);
			}
			
			_cacheExists=false;
		}

		private void CreateCache()
		{
			if(_cacheExists && this._cacheTimestamp.Date==DateTime.Today)
				return; //cache already exists

			DateTime endDate=DateTime.Today;
			DateTime startDate=endDate.Subtract(TimeSpan.FromDays(this.Days));

			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.DeleteReportCache(this.ID);
			dacObj.CreateReportCache(this.ID , this._oltpDatabase , this._productsSerNoList , 
				(byte)this.ProductsJoinLogic, startDate , endDate, this.InSelOnly , this.InBSelOnly, (byte)this.DataSource);

			_cacheExists=true;
			_cacheTimestamp=DateTime.Today;
			this.SaveHeader();
		}

		public FI.Common.Data.FIDataTable GetSppProductsPage(bool IncludeExistingProducts, int StartIndex , int RecordCount, string FilterExpression , string SortExpression)
		{
			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			return dacObj.GetSppProductsPage(this._oltpDatabase , this._productsSerNoList , IncludeExistingProducts , StartIndex , RecordCount , FilterExpression , SortExpression);
		}


		public FI.Common.Data.FIDataTable GetReportPage(ReportPageTypeEnum PageType, int StartIndex , int RecordCount, string FilterExpression , string SortExpression)
		{
			if(this.State!=StateEnum.Executed)
				throw new Exception("Report is not executed");
			
			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			return dacObj.GetReportPage(this.ID, this._oltpDatabase , (byte)PageType , StartIndex , RecordCount , FilterExpression , SortExpression);
		}


		protected internal override  Report _Clone(string Name , string Description)
		{
			string productsXml=null;
			string filterXml=null;
			this.SaveToXml(ref productsXml , ref filterXml);

			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			decimal newId=dacObj.InsertReport(this.Owner.ID , 0 , 0 , Name , Description , false,  productsXml , 
				(byte)this.ProductsJoinLogic ,  this.Days , filterXml , this._cacheTimestamp, this.InSelOnly, this.InBSelOnly, (byte)this.DataSource);

			return _owner.ReportSystem.GetReport(newId , typeof(StorecheckReport) , false);
		}

		
		protected internal override void _SaveHeader()
		{
			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.UpdateReportHeader(this.Owner.ID , this.ID , this._parentReportId , (byte)this.SharingStatus , this.Name , this.Description , this.IsSelected , this._cacheTimestamp );
		}


		public override void LoadHeader()
		{
			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			FI.Common.Data.FIDataTable dataTable=dacObj.ReadReportHeader(this.Owner.ID, this.ID);

			this._parentReportId=(decimal)dataTable.Rows[0]["parent_report_id"];
			this._name=(string)dataTable.Rows[0]["name"];
			this._description=(string)dataTable.Rows[0]["description"];
			this._isSelected=(bool)dataTable.Rows[0]["is_selected"];
			this._sharing=(Report.SharingEnum)((byte)dataTable.Rows[0]["sharing_status"]);
			this._maxSubscriberSharing=(Report.SharingEnum)((byte)dataTable.Rows[0]["max_subscriber_sharing_status"]);
			this._cacheTimestamp=(DateTime)dataTable.Rows[0]["cache_timestamp"];
		}

		override protected internal void _Open()
		{
			short sharing=0;
			short maxSubscriberSharing=0;
			byte productsLogic=0;
			string productsXml=null;
			string filterXml=null;
			byte dataSource=0;

			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.ReadReport(_owner.ID , this.ID, 
				ref this._parentReportId,
				ref this._name , 
				ref this._description , 
				ref sharing,
				ref maxSubscriberSharing,
				ref this._isSelected,
				ref productsXml,
				ref productsLogic,
				ref this._days,
				ref filterXml,
				ref this._cacheTimestamp,
				ref this._cacheExists,
				ref this._inSelOnly,
				ref this._inBSelOnly,
				ref dataSource,
				ref this._oltpDatabase,
				ref _undoStateCount,
				ref _redoStateCount);

			this._sharing=(Report.SharingEnum)sharing;
			this._maxSubscriberSharing=(Report.SharingEnum)maxSubscriberSharing;
			this._productsJoinLogic=(ProductsJoinLogicEnum)productsLogic;
			this._dataSource=(DataSourceEnum)dataSource;
			this.LoadFromXml(productsXml , filterXml);
		}

        override protected internal void _Save()
		{
			string productsXml=null;
			string filterXml=null;
			this.SaveToXml(ref productsXml , ref filterXml);

			if(this.IsDirty) // because cache is valid for current state
				this.ClearCache();

			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.SaveReport(_owner.ID , this._id  , productsXml , 
				(byte)this.ProductsJoinLogic , this.Days , filterXml, this._cacheTimestamp, this.InSelOnly , this.InBSelOnly, (byte)this.DataSource );

		}

		override protected internal void _LoadState(short StateCode)
		{
			string productsXml="";
			byte productsLogic=0;
			string filterXml="";
			byte dataSource=0;

			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.LoadState(this.ID , StateCode, ref productsXml, 
				ref productsLogic , ref this._days, ref filterXml, ref this._inSelOnly, ref this._inBSelOnly,  ref dataSource, ref this._undoStateCount , ref this._redoStateCount);

			this.ProductsJoinLogic=(ProductsJoinLogicEnum)productsLogic;
			this.DataSource=(DataSourceEnum)dataSource;
			this.LoadFromXml(productsXml , filterXml);
		}


		override protected internal void _SaveState()
		{
			string productsXml="";
			string filterXml="";
			this.SaveToXml(ref productsXml , ref filterXml);

			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.SaveState(this.ID , this.MaxStateCount , productsXml , 
				(byte)this.ProductsJoinLogic, this.Days , filterXml, this.InSelOnly , this.InBSelOnly,  (byte)this.DataSource, ref this._undoStateCount);
		}
		
		
		override protected internal void _DeleteStates()
		{
			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.DeleteReportStates(this.ID);
		}


		override protected internal void _ClearResult()
		{
			//this._cacheTimestamp=DateTime.Today;
			//ClearCache();
		}

		

		override protected internal void _Execute()
		{
			this.CreateCache();
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
			FI.Common.DataAccess.IStorecheckReportsDA dacObj=DataAccessFactory.Instance.GetStorecheckReportsDA();
			dacObj.DeleteReportCache(this.ID);	
			dacObj.DeleteReport(_owner.ID , this.ID, DenyShared);	
		}





		public override string Export(ExportFormat Format)
		{			
			if(this.State!=StateEnum.Executed)
				throw new Exception("Report is not executed");


			if (Format==ExportFormat.HTML)
				return ExportToHTML();
			else if (Format==ExportFormat.CSV)
				return ExportToCSV();
			else
				throw new NotSupportedException();

		}
		






		public string ExportToHTML()
		{

			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			FI.Common.Data.FIDataTable notDelivered=this.GetReportPage(ReportPageTypeEnum.NotDelivered , 0 , 50000 , this.FilterExpression , "");
			FI.Common.Data.FIDataTable neverDelivered=this.GetReportPage(ReportPageTypeEnum.NeverDelivered , 0 , 50000 , this.FilterExpression , "");

			// style
			sb.Append(@"
			<html><head><meta charset='utf-8'></meta>
			<style>
			.tbl1_H {
				text-align:left; PADDING-RIGHT: 2pt; border: solid 1px #aaaaaa; PADDING-LEFT: 2pt; FONT-SIZE: 8pt;  COLOR: black;   font-face: tahoma ; ; padding-top: 1px ; padding-bottom: 1px; BACKGROUND-COLOR: #e0e0e0; 
				}
			.tbl1_H1 {
				text-align:left; PADDING-RIGHT: 2pt; border: solid 1px #aaaaaa; PADDING-LEFT: 2pt; FONT-SIZE: 8pt;  COLOR: black; FONT-WEIGHT: bold;   font-face: tahoma ; ; padding-top: 1px ; padding-bottom: 1px; BACKGROUND-COLOR: #e0e0e0; 
				}

			.tbl1_H2 {
				text-align:center; PADDING-RIGHT: 2pt; border: solid 1px #aaaaaa;  PADDING-LEFT: 2pt; FONT-SIZE: 8pt;  COLOR: black;   font-face: tahoma ; padding-top: 1px ; padding-bottom: 1px; BACKGROUND-COLOR: #e0e0e0; 
				}
			.tbl1_H3 {
				text-align:center; PADDING-RIGHT: 2pt; border: solid 1px #aaaaaa; PADDING-LEFT: 2pt; FONT-SIZE: 8pt;  COLOR: black; FONT-WEIGHT: bold;   font-face: tahoma ; ; padding-top: 1px ; padding-bottom: 1px; BACKGROUND-COLOR: #e0e0e0; 
				}

			.tbl1_HC {
				background-color:#FFFFC0; FONT-SIZE: 8pt; font-face: tahoma;PADDING-LEFT: 2pt;PADDING-RIGHT: 2pt;
				}

			.tbl1_T {
				border-collapse:collapse; 
				}
			.tbl1_C {
				text-align:right; PADDING-RIGHT: 2pt; PADDING-LEFT: 2pt; FONT-SIZE: 8pt; BACKGROUND-COLOR: white; font-face: tahoma; padding-top: 1px ; padding-bottom: 1px; border: solid 1px #888888;
				}
			.tbl1_C1 {
				text-align:left; PADDING-RIGHT: 2pt; PADDING-LEFT: 2pt; FONT-SIZE: 8pt; BACKGROUND-COLOR: white; font-face: tahoma; padding-top: 1px ; padding-bottom: 1px; border: solid 1px #888888;
				}
			.tbl1_C2 {
				text-align:right; PADDING-RIGHT: 2pt; PADDING-LEFT: 6pt; FONT-SIZE: 8pt; BACKGROUND-COLOR: white; font-face: tahoma; padding-top: 1px ; padding-bottom: 1px; border: solid 1px #888888;
				}
			</style>
			</head>
			<body>
			");


			// ----- HEADER ------
			sb.Append(@"
			<table cellspacing=0 cellpadding=3 width=100% class=capt><tr><td align=right style='BORDER-WIDTH:0px;background-color:red'>
			<font face=ArialBlack color=white size=1><b><i>&copy;&nbsp;" + AppConfig.CompanyName + @"&nbsp;</b></i></font>
			</tr></td>
			<tr><td style='BACKGROUND:#e0e0e0; BORDER-WIDTH:0px;'>
			<B><font color=red size=-1>" + this.Description + @"</font></B><font color=black size=-2><i>&nbsp;(description)</i></font>
			<br>
			<B><font color=4682b4 size=-2>" + this.Name + @"</font></B><font color=black size=-2><i>&nbsp;(name)</i></font>
			<BR>
			<B><font color=4682b4 size=-2>" + this.Owner.Name + @"</font></B><font color=black size=-2><i>&nbsp;(owner)</i></font>
			<BR>
			<B><font color=4682b4 size=-2>" + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() + @"</font></B><font color=black size=-2><i>&nbsp;(distributed)</i></font>
			</td></tr>
			</table>
			<hr>
			");
			


			// not delivered
			if(notDelivered!=null)
			{
				sb.Append("<br><div class=capt><font color=red><b>No Transactions Within ");
				sb.Append(this.Days);
				sb.Append(" Days:</b></font></div><br>");
				sb.Append("<table>");

				// -- headers
				sb.Append("<tr>");
				sb.Append("<td class=tbl1_H>Salesman</td>");
				sb.Append("<td class=tbl1_H>Central Chain</td>");
				sb.Append("<td class=tbl1_H>Chain</td>");
				sb.Append("<td class=tbl1_H>Store Name</td>");
				sb.Append("<td class=tbl1_H>Store City</td>");
				sb.Append("<td class=tbl1_H>Store Postal Code</td>");
				sb.Append("<td class=tbl1_H>Store Address</td>");
				sb.Append("<td class=tbl1_H>Last Transaction</td>");
				sb.Append("</tr>");
				// -- headers

				foreach(System.Data.DataRow row in notDelivered.Rows)
				{
					sb.Append("<tr>");
					sb.Append("<td class=tbl1_C>" + row["SALMNAME"] + "</td>");
					sb.Append("<td class=tbl1_C>" + row["CCHNAME"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["CHNNAME"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMNAME"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMCITY"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMPCODE"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMADDR"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["DELDATE"]  + "</td>");
					sb.Append("</tr>");
				}
				sb.Append("</table>");
			}


			// -- never delivered
			if(neverDelivered!=null)
			{
				sb.Append("<br><div class=capt><font color=red><b>No Transactions Found");
				sb.Append("</b></font></div><br>");
				sb.Append("<table>");

				// -- headers
				sb.Append("<tr>");
				sb.Append("<td class=tbl1_H>Salesman</td>");
				sb.Append("<td class=tbl1_H>Central Chain</td>");
				sb.Append("<td class=tbl1_H>Chain</td>");
				sb.Append("<td class=tbl1_H>Store Name</td>");
				sb.Append("<td class=tbl1_H>Store City</td>");
				sb.Append("<td class=tbl1_H>Store Postal Code</td>");
				sb.Append("<td class=tbl1_H>Store Address</td>");
				sb.Append("<td class=tbl1_H>Last Transaction</td>");
				sb.Append("</tr>");
				// -- headers

				foreach(System.Data.DataRow row in neverDelivered.Rows)
				{
					sb.Append("<tr>");
					sb.Append("<td class=tbl1_C>" + row["SALMNAME"] + "</td>");
					sb.Append("<td class=tbl1_C>" + row["CCHNAME"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["CHNNAME"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMNAME"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMCITY"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMPCODE"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["COMADDR"]  + "</td>");
					sb.Append("<td class=tbl1_C>" + row["DELDATE"]  + "</td>");
					sb.Append("</tr>");
				}
				sb.Append("</table>");
			}

			sb.Append("</body></html>");


			return sb.ToString();

		}



		public string ExportToCSV()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			FI.Common.Data.FIDataTable notDelivered=this.GetReportPage(ReportPageTypeEnum.NotDelivered , 0 , 50000 , this.FilterExpression , "");
			FI.Common.Data.FIDataTable neverDelivered=this.GetReportPage(ReportPageTypeEnum.NeverDelivered , 0 , 50000 , this.FilterExpression , "");


			// not delivered
			if(notDelivered!=null)
			{
				sb.Append("\r\nNo Transactions Within ");
				sb.Append(this.Days);
				sb.Append(" Days:\r\n");

				// -- headers
				sb.Append("Salesman\t");
				sb.Append("Central Chain\t");
				sb.Append("Chain\t");
				sb.Append("Store Name\t");
				sb.Append("Store City\t");
				sb.Append("Store Postal Code\t");
				sb.Append("Store Address\t");
				sb.Append("Last Transaction\t");
				sb.Append("\r\n");
				// -- headers

				foreach(System.Data.DataRow row in notDelivered.Rows)
				{
					sb.Append(row["SALMNAME"] + "\t");
					sb.Append(row["CCHNAME"]  + "\t");
					sb.Append(row["CHNNAME"]  + "\t");
					sb.Append(row["COMNAME"]  + "\t");
					sb.Append(row["COMCITY"]  + "\t");
					sb.Append(row["COMPCODE"]  + "\t");
					sb.Append(row["COMADDR"]  + "\t");
					sb.Append(row["DELDATE"]  + "\t");
					sb.Append("\r\n");
				}
			}


			// -- never delivered
			if(neverDelivered!=null)
			{
				sb.Append("\r\nNo Transactions Found\r\n");

				// -- headers
				sb.Append("Salesman\t");
				sb.Append("Central Chain\t");
				sb.Append("Chain\t");
				sb.Append("Store Name\t");
				sb.Append("Store City\t");
				sb.Append("Store Postal Code\t");
				sb.Append("Store Address\t");
				sb.Append("Last Transaction\t");
				sb.Append("\r\n");
				// -- headers

				foreach(System.Data.DataRow row in neverDelivered.Rows)
				{
					sb.Append(row["SALMNAME"] + "\t");
					sb.Append(row["CCHNAME"]  + "\t");
					sb.Append(row["CHNNAME"]  + "\t");
					sb.Append(row["COMNAME"]  + "\t");
					sb.Append(row["COMCITY"]  + "\t");
					sb.Append(row["COMPCODE"]  + "\t");
					sb.Append(row["COMADDR"]  + "\t");
					sb.Append(row["DELDATE"]  + "\t");
					sb.Append("\r\n");
				}
			}

			return sb.ToString();
		}



	}


}
