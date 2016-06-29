using System;
using System.Collections;
using FI.Common.Data;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for CrystalReport.
	/// </summary>
	public class CrystalReport:Report
	{
		private ArrayList _parameters=new ArrayList();
		private byte[] _data;
		private string _reportPath=null;
		private string _preprocessSql=null;

		internal CrystalReport(decimal ID , User Owner):base(ID,Owner)
		{
			if(ID==0) //if new
			{

				//FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
				//_id=dacObj.InsertReport(_owner.ID , 0 , 0 , "New Report" , "" , this.IsSelected , this.Sql , this.Xsl);

				_isProxy=false;
				_isDirty=false;
			}
		}



		protected internal override  Report _Clone(string Name , string Description)
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();

			decimal newId=dacObj.InsertReport(_owner.ID , 0 , 0 , Name , Description , false , this.Sql , this.Xsl);
			return _owner.ReportSystem.GetReport(newId , typeof(CustomSqlReport) , false);
			*/

			//wrong
			return null;
		}

		
		protected internal override void _SaveHeader()
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
			dacObj.UpdateReportHeader(this.Owner.ID , this.ID , this._parentReportId , (byte)this.SharingStatus , this.Name , this.Description , this.IsSelected );
			*/
		}


		public override void LoadHeader()
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
			FI.Common.Data.FIDataTable dataTable=dacObj.ReadReportHeader(this.Owner.ID, this.ID);

			this._parentReportId=(decimal)dataTable.Rows[0]["parent_report_id"];
			this._name=(string)dataTable.Rows[0]["name"];
			this._description=(string)dataTable.Rows[0]["description"];
			this._isSelected=(bool)dataTable.Rows[0]["is_selected"];
			this._sharing=(Report.SharingEnum)((byte)dataTable.Rows[0]["sharing_status"]);
			this._maxSubscriberSharing=(Report.SharingEnum)((byte)dataTable.Rows[0]["max_subscriber_sharing_status"]);
			*/
		}

		override protected internal void _Open()
		{
			short sharing=0;
			short maxSubscriberSharing=0;

			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
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
			*/

			this._sharing=(Report.SharingEnum)sharing;
			this._maxSubscriberSharing=(Report.SharingEnum)maxSubscriberSharing;
		}

		override protected internal void _Close(bool SaveFromState)
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();

			if(SaveFromState)
				dacObj.SaveReport(_owner.ID , this._id  , this.Sql , this.Xsl );
			*/
		}

		override protected internal void _LoadState(short StateCode)
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
			dacObj.LoadState(this.ID , StateCode, ref this._sql , ref this._xsl , ref this._undoStateCount , ref this._redoStateCount);
			*/
		}


		override protected internal void _SaveState()
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
			dacObj.SaveState(this.ID , this.MaxStateCount , this.Sql, this.Xsl, ref this._undoStateCount);
			*/
		}
		
		
		override protected internal void _DeleteStates()
		{
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
			dacObj.DeleteReportStates(this.ID);
			*/
		}


		override protected internal void _ClearResult()
		{
			_reportPath=null;
		}

		

		override protected internal void _Execute()
		{
			//execute preprocess sql
			if(_preprocessSql!=null && _preprocessSql.Trim()!="")
			{
				throw new NotImplementedException();
			}

			// extract report data to disk
			if(this.ReportData==null || this.ReportData.Length==0)
				throw new Exception("Report data is missing");

			System.IO.FileStream fs=null;
			System.IO.StreamWriter sw=null; 
			string path=FI.Common.AppConfig.TempDir + @"\" + this.GetType().Name + this.ID.ToString() + ".rpt";

			try
			{
				fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
				sw = new System.IO.StreamWriter(fs);
				sw.BaseStream.Seek(0, System.IO.SeekOrigin.End);
				sw.Write(this.ReportData);
				sw.Flush();

				_reportPath=path;
			}
			catch
			{
				_reportPath=null;
				throw;
			}
			finally
			{
				if(sw!=null)
					sw.Close();

				if(fs!=null)
					fs.Close();
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
			/*
			FI.DataAccess.CustomSqlReports dacObj=new FI.DataAccess.CustomSqlReports();
			dacObj.DeleteReport(_owner.ID , this.ID, DenyShared);	
			*/
		}





		public override string Export(ExportFormat Format)
		{
			if (Format==ExportFormat.HTML)
				return ExportToHTML();
			else if (Format==ExportFormat.CSV)
				return ExportToCSV();
			else
				throw new NotSupportedException();
		}


		private string ExportToHTML()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			return sb.ToString();
		}



		private string ExportToCSV()
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			return sb.ToString();
		}




		private void  LoadFromXml(string xml)
		{
		}


		private string SaveToXml()
		{
			System.Xml.XmlDocument doc=null;
			System.Xml.XmlElement rootEl=null;

			// parameters
			doc=new System.Xml.XmlDocument();
			rootEl=doc.CreateElement("PARAMETERS");
			doc.AppendChild(rootEl);
			/*
			foreach(string serNo in this._productsSerNoList)
			{
				System.Xml.XmlElement childEl=doc.CreateElement("PR");
				childEl.SetAttribute("SN" , serNo);
				rootEl.AppendChild(childEl);
			}
			*/
			return doc.InnerXml;
		}



		public void AddParameter(Crystal.Parameter param)
		{
			if(_parameters.Contains(param)==false)
			{
				OnChangeReport(false);
				_parameters.Add(param);
				
				//subscribe to change event
				param.BeforeChange+=new EventHandler(Parameter_BeforeChange);
			}
		}

		public void RemoveParameter(Crystal.Parameter param)
		{
			OnChangeReport(false);
			_parameters.Remove(param);

			//unsubscribe from change event
			param.BeforeChange-=new EventHandler(Parameter_BeforeChange);
		}

		public int ParameterCount
		{
			get { return _parameters.Count;}
		}

		public void ClearParameters()
		{
			OnChangeReport(false);
			_parameters.Clear();
		}


		public Crystal.Parameter GetParameter(int index)
		{
			return (Crystal.Parameter)_parameters[index];
		}

		private void Parameter_BeforeChange(object sender, EventArgs e)
		{
			this.OnChangeReport(true);
		}



		public byte[] ReportData
		{
			get { return _data; }
			set 
			{ 
				this.OnChangeReport(true);
				_data=value; 
			}
		}


		public string PreprocessSql
		{
			get { return _preprocessSql; }
			set
			{
				if(_preprocessSql!=value)
					OnChangeReport(true);

				_preprocessSql=value;
			}
		}


		public string ReportPath
		{
			get { return _reportPath; }
		}


	}


}
