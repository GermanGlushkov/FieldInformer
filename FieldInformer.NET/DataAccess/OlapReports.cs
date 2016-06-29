using System;
using System.Data;
using System.Data.SqlClient;
using FI.Common.Data;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for OlapReports.
	/// </summary>
	public class OlapReports:MarshalByRefObject
	{
		public OlapReports()
		{
		}


		public FI.Common.Data.FIDataTable ReadReportHeader(decimal UserId, decimal ReportID)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadOlapReportHeader" , CommandType.StoredProcedure , parameters , data);
			return data;
		}


		public FI.Common.Data.FIDataTable ReadReportHeaders(decimal UserId)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@UserId" , UserId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadOlapReportHeaders" , CommandType.StoredProcedure , parameters , data);
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
			ref byte GraphType,
			ref int GraphOptions,
			ref string SchemaServer,
			ref string SchemaDatabase,
			ref string SchemaName,
			ref string ReportXml , ref string SchemaXml, ref string OpenNodesXml , ref byte UndoCount , ref byte RedoCount)
		{
			System.Data.DataTable tbl=new DataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadOlapReport" , CommandType.StoredProcedure , parameters , tbl);


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

			if(tbl.Rows[0]["GraphType"]!=System.DBNull.Value)
				GraphType=(byte)tbl.Rows[0]["GraphType"];

			if(tbl.Rows[0]["GraphOptions"]!=System.DBNull.Value)
				GraphOptions=(int)tbl.Rows[0]["GraphOptions"];

			if(tbl.Rows[0]["SchemaServer"]!=System.DBNull.Value)
				SchemaServer=(string)tbl.Rows[0]["SchemaServer"];

			if(tbl.Rows[0]["SchemaDatabase"]!=System.DBNull.Value)
				SchemaDatabase=(string)tbl.Rows[0]["SchemaDatabase"];

			if(tbl.Rows[0]["SchemaName"]!=System.DBNull.Value)
				SchemaName=(string)tbl.Rows[0]["SchemaName"];

			if(tbl.Rows[0]["ReportXml"]!=System.DBNull.Value)
				ReportXml=(string)tbl.Rows[0]["ReportXml"];

			if(tbl.Rows[0]["OpenNodesXml"]!=System.DBNull.Value)
				OpenNodesXml=(string)tbl.Rows[0]["OpenNodesXml"];

			if(tbl.Rows[0]["UndoCount"]!=System.DBNull.Value)
				UndoCount=(byte)tbl.Rows[0]["UndoCount"];

			if(tbl.Rows[0]["RedoCount"]!=System.DBNull.Value)
				RedoCount=(byte)tbl.Rows[0]["RedoCount"];

			ReportXml=this.GetReportXml(SchemaServer , SchemaDatabase, SchemaName, ReportXml);
			SchemaXml=this.GetReportSchemaXml(SchemaServer , SchemaDatabase, SchemaName, OpenNodesXml);
		}


		public decimal InsertReport(
			decimal UserID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			byte GraphType,
			int GraphOptions,
			string ReportXml,
			string OpenNodesXml)
		{
			decimal reportId=-1;

			SqlParameter[] parameters=new SqlParameter[11];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , reportId);
			parameters[1].Direction=ParameterDirection.Output;
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);
			parameters[7]=new SqlParameter("@GraphType" , GraphType);
			parameters[8]=new SqlParameter("@GraphOptions" , GraphOptions);
			parameters[9]=new SqlParameter("@ReportXml" , ReportXml);
			parameters[10]=new SqlParameter("@OpenNodesXml" , OpenNodesXml);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertOlapReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[1].Value!=System.DBNull.Value)
				reportId=(decimal)parameters[1].Value;
			
			return reportId;
		}


		public void SaveReport(
			decimal UserID, 
			decimal ReportID, 
			string ReportXml,
			string OpenNodesXml)
		{
			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ReportXml" , ReportXml);
			parameters[3]=new SqlParameter("@OpenNodesXml" , OpenNodesXml);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveOlapReport" , CommandType.StoredProcedure , parameters , null);
		}


		public void UpdateReportHeader(
			decimal UserID, 
			decimal ReportID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			string OpenNodesXml,
			byte GraphType,
			int GraphOptions)
		{
			SqlParameter[] parameters=new SqlParameter[10];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);
			parameters[7]=new SqlParameter("@OpenNodesXml" , OpenNodesXml);
			parameters[8]=new SqlParameter("@GraphType" , GraphType);
			parameters[9]=new SqlParameter("@GraphOptions" , GraphOptions);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveOlapReportHeader" , CommandType.StoredProcedure , parameters , null);
		}


		public void SaveState(decimal ReportId, byte MaxStateCount, string ReportXml , ref byte UndoCount)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@MaxStateCount" , MaxStateCount);
			parameters[2]=new SqlParameter("@ReportXml" , ReportXml);
			parameters[3]=new SqlParameter("@UndoCount" , UndoCount);
			parameters[3].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveOlapReportState" , CommandType.StoredProcedure , parameters , null);

			if(parameters[3].Value!=System.DBNull.Value)
				UndoCount=(byte)parameters[3].Value;
		}



		public string LoadState(decimal ReportId, short StateCode , ref byte UndoCount , ref byte RedoCount)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@StateCode" , StateCode);
			parameters[2]=new SqlParameter("@UndoCount" , UndoCount);
			parameters[2].Direction=ParameterDirection.Output;
			parameters[3]=new SqlParameter("@RedoCount" , RedoCount);
			parameters[3].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadOlapReportState" , CommandType.StoredProcedure , parameters , data);

			if(parameters[2].Value!=System.DBNull.Value)
				UndoCount=(byte)parameters[2].Value;
			if(parameters[3].Value!=System.DBNull.Value)
				RedoCount=(byte)parameters[3].Value;

			return (string)data.Rows[0][0];
		}



		
		private string GetReportXml(string Server , string Database, string Cube, string InReportXml)
		{
			FI.DataAccess.OlapSystem olapSystem=new OlapSystem();
			return olapSystem.GetReportXml(Server , Database, Cube , InReportXml);
		}


		private string GetReportSchemaXml(string Server , string Database, string Cube, string OpenNodesXml)
		{
			FI.DataAccess.OlapSystem olapSystem=new OlapSystem();
			return olapSystem.GetReportSchemaXml(Server , Database, Cube , OpenNodesXml);
		}





		
		public void DeleteReport(decimal UserId , decimal ReportId, bool DenyShared)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[3];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@ReportId" , ReportId);
			parameters[2]=new SqlParameter("@DenyShared" , DenyShared);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteOlapReport" , CommandType.StoredProcedure , parameters , null);
		}


				
		public void DeleteReportStates(decimal ReportId)
		{
			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteOlapReportStates" , CommandType.StoredProcedure , parameters , null);
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

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertSharedOlapReport" , CommandType.StoredProcedure , parameters , null);

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

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteSharedOlapReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[2].Value!=System.DBNull.Value)
				MaxSubscriberSharingStatus=(short)parameters[2].Value;
		}


	}
}
