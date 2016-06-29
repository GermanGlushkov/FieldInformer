using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for StorecheckReports.
	/// </summary>
	public class StorecheckReports:MarshalByRefObject
	{

		public StorecheckReports()
		{
		}


		public FI.Common.Data.FIDataTable ReadReportHeader(decimal UserId, decimal ReportID)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadStorecheckReportHeader" , CommandType.StoredProcedure , parameters , data);
			return data;
		}


		public FI.Common.Data.FIDataTable ReadReportHeaders(decimal UserId)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@UserId" , UserId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadStorecheckReportHeaders" , CommandType.StoredProcedure , parameters , data);
			return data;
		}


		
		
		public FI.Common.Data.FIDataTable ReadUsersWithChildReports(decimal ParentReportId , int ParentReportType)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[2];			
			parameters[0]=new SqlParameter("@ParentReportId" , ParentReportId);
			parameters[1]=new SqlParameter("@ParentReportType" , ParentReportType);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadUsersWithChildReports" , CommandType.StoredProcedure , parameters , data);
			return data;
		}


		public void  ReadReport(decimal UserID, decimal ReportID, 
			ref decimal ParentReportId, 
			ref string Name, 
			ref string Description,
			ref short SharingStatus,
			ref short MaxSubscriberSharingStatus,
			ref bool IsSelected,
			ref string ProductsXml , ref byte ProductsLogic , ref short Days , ref string FilterXml , ref System.DateTime CacheTimestamp, ref bool CacheExists,
			ref bool InSelOnly , ref bool InBSelOnly, ref byte DataSource,
			ref string OltpDatabase,
			ref byte UndoCount , ref byte RedoCount)
		{
			System.Data.DataTable tbl=new DataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadStorecheckReport" , CommandType.StoredProcedure , parameters , tbl);


			if(tbl.Rows[0]["ParentReportId"]!=System.DBNull.Value)
				ParentReportId=(decimal)tbl.Rows[0]["ParentReportId"];

			if(tbl.Rows[0]["Name"]!=System.DBNull.Value)
				Name=(string)tbl.Rows[0]["Name"];

			if(tbl.Rows[0]["Description"]!=System.DBNull.Value)
				Description=(string)tbl.Rows[0]["Description"];

			if(tbl.Rows[0]["SharingStatus"]!=System.DBNull.Value)
				SharingStatus=(short.Parse(tbl.Rows[0]["SharingStatus"].ToString()));

			if(tbl.Rows[0]["MaxSubscriberSharingStatus"]!=System.DBNull.Value)
				MaxSubscriberSharingStatus=(short.Parse(tbl.Rows[0]["MaxSubscriberSharingStatus"].ToString()));

			if(tbl.Rows[0]["IsSelected"]!=System.DBNull.Value)
				IsSelected=(bool)tbl.Rows[0]["IsSelected"];

			if(tbl.Rows[0]["ProductsXml"]!=System.DBNull.Value)
				ProductsXml=(string)tbl.Rows[0]["ProductsXml"];

			if(tbl.Rows[0]["ProductsLogic"]!=System.DBNull.Value)
				ProductsLogic=(byte)tbl.Rows[0]["ProductsLogic"];

			if(tbl.Rows[0]["Days"]!=System.DBNull.Value)
				Days=(short)tbl.Rows[0]["Days"];

			if(tbl.Rows[0]["FilterXml"]!=System.DBNull.Value)
				FilterXml=(string)tbl.Rows[0]["FilterXml"];

			if(tbl.Rows[0]["CacheTimestamp"]!=System.DBNull.Value)
				CacheTimestamp=(System.DateTime)tbl.Rows[0]["CacheTimestamp"];

			if(tbl.Rows[0]["CacheExists"]!=System.DBNull.Value)
				CacheExists=(bool)tbl.Rows[0]["CacheExists"];

			if(tbl.Rows[0]["InSelOnly"]!=System.DBNull.Value)
				InSelOnly=(bool)tbl.Rows[0]["InSelOnly"];

			if(tbl.Rows[0]["InBSelOnly"]!=System.DBNull.Value)
				InBSelOnly=(bool)tbl.Rows[0]["InBSelOnly"];

			if(tbl.Rows[0]["DataSource"]!=System.DBNull.Value)
				DataSource=(byte)tbl.Rows[0]["DataSource"];

			if(tbl.Rows[0]["OltpDatabase"]!=System.DBNull.Value)
				OltpDatabase=(string)tbl.Rows[0]["OltpDatabase"];

			if(tbl.Rows[0]["UndoCount"]!=System.DBNull.Value)
				UndoCount=(byte)tbl.Rows[0]["UndoCount"];

			if(tbl.Rows[0]["RedoCount"]!=System.DBNull.Value)
				RedoCount=(byte)tbl.Rows[0]["RedoCount"];
		}


		public decimal InsertReport(
			decimal UserID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			string ProductsXml , byte ProductsLogic , short Days , string FilterXml , System.DateTime CacheTimestamp, bool InSelOnly , bool InBSelOnly, byte DataSource
			)
		{
			decimal reportId=-1;

			SqlParameter[] parameters=new SqlParameter[15];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , reportId);
			parameters[1].Direction=ParameterDirection.Output;
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);
			parameters[7]=new SqlParameter("@ProductsXml" , ProductsXml);
			parameters[8]=new SqlParameter("@ProductsLogic" , ProductsLogic);
			parameters[9]=new SqlParameter("@Days" , Days);
			parameters[10]=new SqlParameter("@FilterXml" , FilterXml);
			parameters[11]=new SqlParameter("@CacheTimestamp" , CacheTimestamp);
			parameters[12]=new SqlParameter("@InSelOnly" , InSelOnly);
			parameters[13]=new SqlParameter("@InBSelOnly" , InBSelOnly);
			parameters[14]=new SqlParameter("@DataSource" , DataSource);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertStorecheckReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[1].Value!=System.DBNull.Value)
				reportId=(decimal)parameters[1].Value;
			
			return reportId;
		}


		public void SaveReport(
			decimal UserID, 
			decimal ReportID, 
			string ProductsXml , byte ProductsLogic , short Days , string FilterXml , System.DateTime CacheTimestamp, bool InSelOnly , bool InBSelOnly, byte DataSource
			)
		{
			SqlParameter[] parameters=new SqlParameter[10];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ProductsXml" , ProductsXml);
			parameters[3]=new SqlParameter("@ProductsLogic" , ProductsLogic);
			parameters[4]=new SqlParameter("@Days" , Days);
			parameters[5]=new SqlParameter("@FilterXml" , FilterXml);
			parameters[6]=new SqlParameter("@CacheTimestamp" , CacheTimestamp);
			parameters[7]=new SqlParameter("@InSelOnly" , InSelOnly);
			parameters[8]=new SqlParameter("@InBSelOnly" , InBSelOnly);
			parameters[9]=new SqlParameter("@DataSource" , DataSource);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveStorecheckReport" , CommandType.StoredProcedure , parameters , null);
		}


		public void UpdateReportHeader(
			decimal UserID, 
			decimal ReportID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			DateTime CacheTimestamp)
		{
			SqlParameter[] parameters=new SqlParameter[8];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);
			parameters[7]=new SqlParameter("@CacheTimestamp" , CacheTimestamp);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveStorecheckReportHeader" , CommandType.StoredProcedure , parameters , null);
		}


		public void SaveState(decimal ReportId, byte MaxStateCount, 
			string ProductsXml , byte ProductsLogic , short Days , string FilterXml , 
			bool InSelOnly, bool InBSelOnly, byte DataSource,
			ref byte UndoCount)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[10];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@MaxStateCount" , MaxStateCount);
			parameters[2]=new SqlParameter("@ProductsXml" , ProductsXml);
			parameters[3]=new SqlParameter("@ProductsLogic" , ProductsLogic);
			parameters[4]=new SqlParameter("@Days" , Days);
			parameters[5]=new SqlParameter("@FilterXml" , FilterXml);
			parameters[6]=new SqlParameter("@InSelOnly" , InSelOnly);
			parameters[7]=new SqlParameter("@InBSelOnly" , InBSelOnly);
			parameters[8]=new SqlParameter("@DataSource" , DataSource);
			parameters[9]=new SqlParameter("@UndoCount" , UndoCount);
			parameters[9].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveStorecheckReportState" , CommandType.StoredProcedure , parameters , null);

			if(parameters[9].Value!=System.DBNull.Value)
				UndoCount=(byte)parameters[9].Value;
		}



		public void LoadState(decimal ReportId, short StateCode , 
			ref string ProductsXml , ref byte ProductsLogic , ref short Days , ref string FilterXml , 
			ref bool InSelOnly, ref bool InBSelOnly, ref byte DataSource,
			ref byte UndoCount , ref byte RedoCount)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@StateCode" , StateCode);
			parameters[2]=new SqlParameter("@UndoCount" , UndoCount);
			parameters[2].Direction=ParameterDirection.Output;
			parameters[3]=new SqlParameter("@RedoCount" , RedoCount);
			parameters[3].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadStorecheckReportState" , CommandType.StoredProcedure , parameters , data);

			if(parameters[2].Value!=System.DBNull.Value)
				UndoCount=(byte)parameters[2].Value;
			if(parameters[3].Value!=System.DBNull.Value)
				RedoCount=(byte)parameters[3].Value;

			ProductsXml=(string)data.Rows[0]["products_xml"];
			ProductsLogic=(byte)data.Rows[0]["products_logic"];
			Days=(short)data.Rows[0]["days"];
			FilterXml=(string)data.Rows[0]["filter_xml"];
			InSelOnly=(bool)data.Rows[0]["insel"];
			InBSelOnly=(bool)data.Rows[0]["inbsel"];
			DataSource=(byte)(data.Rows[0]["datasource"]==null || data.Rows[0]["datasource"]==DBNull.Value ? 0 : data.Rows[0]["datasource"]);
		}




		
		public void DeleteReport(decimal UserId , decimal ReportId, bool DenyShared)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[3];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@ReportId" , ReportId);
			parameters[2]=new SqlParameter("@DenyShared" , DenyShared);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteStorecheckReport" , CommandType.StoredProcedure , parameters , null);
		}


				
		public void DeleteReportStates(decimal ReportId)
		{
			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteStorecheckReportStates" , CommandType.StoredProcedure , parameters , null);
		}


		public decimal CreateSharedReport(decimal ParentReportId , decimal SubscriberUserId , int SubscriberSharingStatus)
		{
			decimal reportId=0;

			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@ParentReportId" , ParentReportId);
			parameters[1]=new SqlParameter("@SubscriberUserId" , SubscriberUserId);
			parameters[2]=new SqlParameter("@SubscriberSharingStatus" , SubscriberSharingStatus);
			parameters[3]=new SqlParameter("@SubscriberReportId" , reportId);
			parameters[3].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertSharedStorecheckReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[3].Value!=System.DBNull.Value)
				reportId=(decimal)parameters[3].Value;

			return reportId;
		}



		public void DeleteSharedReport(decimal ParentReportId, decimal ChildReportId, ref short MaxSubscriberSharingStatus)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[3];
			parameters[0]=new SqlParameter("@ParentReportId" , ParentReportId);
			parameters[1]=new SqlParameter("@ChildReportId" , ChildReportId);
			parameters[2]=new SqlParameter("@MaxSubscriberSharingStatus" , MaxSubscriberSharingStatus);
			parameters[2].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteSharedStorecheckReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[2].Value!=System.DBNull.Value)
				MaxSubscriberSharingStatus=(short)parameters[2].Value;
		}







		public FI.Common.Data.FIDataTable GetSppProductsPage(string Database , StringCollection ProdSernList , bool IncludeProdSernList , int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			string sql=@"
					SELECT 
						prodsern , 
						prodname , 
						prodsname , 
						prodcode, 
						proddunc,
						prodeanc,
						prodprice,
						prodcps,
						prodcpu,
						prodsize,
						prodpallet
						FROM " + Database + ".spp.TPRODUCT";

			if(IncludeProdSernList)
				sql=sql + " WHERE prodsern IN ("; 
			else
				sql=sql + " WHERE prodsern NOT IN ("; 

			
			if(ProdSernList!=null && ProdSernList.Count>0)
			{
				foreach(string prodsern in ProdSernList)
					sql=sql + "'" + prodsern + "', ";
				//remove las 2 chars
				sql=sql.Remove(sql.Length-2,2);
			}
			else
				sql=sql + "''";

			sql=sql + ")";


			SqlParameter[] selectColumns=new SqlParameter[11];
			selectColumns[0]=new SqlParameter("prodsern" , SqlDbType.VarChar , 15);
			selectColumns[1]=new SqlParameter("prodname" , SqlDbType.VarChar , 45);
			selectColumns[2]=new SqlParameter("prodsname" , SqlDbType.VarChar , 15);
			selectColumns[3]=new SqlParameter("prodcode" , SqlDbType.VarChar , 15);
			selectColumns[4]=new SqlParameter("proddunc" , SqlDbType.VarChar , 14);
			selectColumns[5]=new SqlParameter("prodeanc" , SqlDbType.VarChar , 13);
			selectColumns[6]=new SqlParameter("prodprice" , SqlDbType.Float );
			selectColumns[7]=new SqlParameter("prodcps" , SqlDbType.Float );
			selectColumns[8]=new SqlParameter("prodcpu" , SqlDbType.VarChar , 4);
			selectColumns[9]=new SqlParameter("prodsize" , SqlDbType.Float);
			selectColumns[10]=new SqlParameter("prodpallet" , SqlDbType.Float);

			return DataBase.Instance.ExecutePagedCommand(StartIndex , RecordCount , selectColumns , sql , FilterExpression , SortExpression);
		}



		public void CreateReportCache(decimal ReportId, string Database, StringCollection ProductsSernList, byte ProductsJoinLogic, DateTime StartDate , DateTime EndDate, bool InSelOnly , bool InBSelOnly, byte DataSource)
		{
			string prodsernList="";
			int prodsernCount=0;
			if(ProductsSernList!=null && ProductsSernList.Count>0)
			{
				foreach(string prodsern in ProductsSernList)
					prodsernList=prodsernList + "'" + prodsern + "', ";
				//remove las 2 chars
				prodsernList=prodsernList.Remove(prodsernList.Length-2,2);

				prodsernCount=ProductsSernList.Count;
			}
			else
				prodsernList=prodsernList + "''";

			string startDate=StartDate.ToString("yyyyMMdd");
			string endDate=EndDate.ToString("yyyyMMdd");

			SqlParameter[] parameters=new SqlParameter[10];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@Database" , Database);
			parameters[2]=new SqlParameter("@ProductsSernList" , prodsernList);
			parameters[3]=new SqlParameter("@ProductsSernListCount" , prodsernCount);
			parameters[4]=new SqlParameter("@ProductsJoinLogic" , ProductsJoinLogic);
			parameters[5]=new SqlParameter("@StartDate" , startDate);
			parameters[6]=new SqlParameter("@EndDate" , endDate);
			parameters[7]=new SqlParameter("@InSelOnly" , InSelOnly);
			parameters[8]=new SqlParameter("@InBSelOnly" , InBSelOnly);
			parameters[9]=new SqlParameter("@DataSource" , DataSource);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertStorecheckReportCache" , CommandType.StoredProcedure , parameters , null);
		}


		public void DeleteReportCache(decimal ReportId)
		{
			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteStorecheckReportCache" , CommandType.StoredProcedure , parameters , null);
		}


		public FI.Common.Data.FIDataTable GetReportPage(decimal ReportId, string Database, byte PageType , int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			string sql=@"
					select 
						SALMNAME,
						OLAP_STORE.CCHNAME,
						OLAP_STORE.CHNNAME,
						TCOMPANY.COMNAME,
						TCOMPANY.COMPCODE,
						TCOMPANY.COMCITY,
						TCOMPANY.COMADDR,
						cache.DELDATE
						from 
							" + Database + @".spp.TCOMPANY TCOMPANY
						inner join
							dbo.t_storecheck_reports_cache cache
							ON cache.RPT_ID=" + ReportId.ToString() + @"
							AND cache.TYPE=" + PageType.ToString() + @" 
							AND cache.COMSERNO=TCOMPANY.COMSERNO
						left outer join " + Database + @".spp.TSALMAN TSALMAN on cache.SALMSERN=TSALMAN.SALMSERN
						left outer join " + Database + @".spp.OLAP_STORE OLAP_STORE on cache.COMSERNO=OLAP_STORE.COMSERNO
			";


			SqlParameter[] selectColumns=new SqlParameter[8];
			selectColumns[0]=new SqlParameter("SALMNAME" , SqlDbType.VarChar , 30);
			selectColumns[1]=new SqlParameter("CCHNAME" , SqlDbType.VarChar , 30);
			selectColumns[2]=new SqlParameter("CHNNAME" , SqlDbType.VarChar , 30);
			selectColumns[3]=new SqlParameter("COMNAME" , SqlDbType.VarChar , 30);
			selectColumns[4]=new SqlParameter("COMPCODE" , SqlDbType.VarChar , 10);
			selectColumns[5]=new SqlParameter("COMCITY" , SqlDbType.VarChar , 30);
			selectColumns[6]=new SqlParameter("COMADDR" , SqlDbType.VarChar , 40);
			selectColumns[7]=new SqlParameter("DELDATE" , SqlDbType.VarChar , 8 );

			return DataBase.Instance.ExecutePagedCommand(StartIndex , RecordCount , selectColumns , sql , FilterExpression , SortExpression);
		}


	}
}
