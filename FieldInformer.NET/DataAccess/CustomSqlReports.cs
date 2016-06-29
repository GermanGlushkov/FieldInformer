using System;
using System.Data;
using System.Data.SqlClient;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for CustomSqlReports.
	/// </summary>
	public class CustomSqlReports:MarshalByRefObject
	{
		public CustomSqlReports()
		{
		}


		public FI.Common.Data.FIDataTable ReadReportHeader(decimal UserId, decimal ReportID)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadSqlReportHeader" , CommandType.StoredProcedure , parameters , data);
			return data;
		}


		public FI.Common.Data.FIDataTable ReadReportHeaders(decimal UserId)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@UserId" , UserId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadSqlReportHeaders" , CommandType.StoredProcedure , parameters , data);
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
			ref string Sql , ref string Xsl , ref byte UndoCount , ref byte RedoCount)
		{
			System.Data.DataTable tbl=new DataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadSqlReport" , CommandType.StoredProcedure , parameters , tbl);


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

			if(tbl.Rows[0]["Sql"]!=System.DBNull.Value)
				Sql=(string)tbl.Rows[0]["Sql"];

			if(tbl.Rows[0]["Xsl"]!=System.DBNull.Value)
				Xsl=(string)tbl.Rows[0]["Xsl"];

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
			string Sql,
			string Xsl)
		{
			decimal reportId=-1;

			SqlParameter[] parameters=new SqlParameter[9];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , reportId);
			parameters[1].Direction=ParameterDirection.Output;
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);
			parameters[7]=new SqlParameter("@Sql" , Sql);
			parameters[8]=new SqlParameter("@Xsl" , Xsl);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertSqlReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[1].Value!=System.DBNull.Value)
				reportId=(decimal)parameters[1].Value;
			
			return reportId;
		}


		public void SaveReport(
			decimal UserID, 
			decimal ReportID, 
			string Sql,
			string Xsl)
		{
			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@Sql" , Sql);
			parameters[3]=new SqlParameter("@Xsl" , Xsl);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveSqlReport" , CommandType.StoredProcedure , parameters , null);
		}


		public void UpdateReportHeader(
			decimal UserID, 
			decimal ReportID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected)
		{
			SqlParameter[] parameters=new SqlParameter[7];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveSqlReportHeader" , CommandType.StoredProcedure , parameters , null);
		}


		public void SaveState(decimal ReportId, byte MaxStateCount, string Sql, string Xsl , ref byte UndoCount)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[5];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@MaxStateCount" , MaxStateCount);
			parameters[2]=new SqlParameter("@Sql" , Sql);
			parameters[3]=new SqlParameter("@Xsl" , Xsl);
			parameters[4]=new SqlParameter("@UndoCount" , UndoCount);
			parameters[4].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveSqlReportState" , CommandType.StoredProcedure , parameters , null);

			if(parameters[4].Value!=System.DBNull.Value)
				UndoCount=(byte)parameters[4].Value;
		}



		public void LoadState(decimal ReportId, short StateCode , ref string Sql , ref string Xsl, ref byte UndoCount , ref byte RedoCount)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);
			parameters[1]=new SqlParameter("@StateCode" , StateCode);
			parameters[2]=new SqlParameter("@UndoCount" , UndoCount);
			parameters[2].Direction=ParameterDirection.Output;
			parameters[3]=new SqlParameter("@RedoCount" , RedoCount);
			parameters[3].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadSqlReportState" , CommandType.StoredProcedure , parameters , data);

			if(parameters[2].Value!=System.DBNull.Value)
				UndoCount=(byte)parameters[2].Value;
			if(parameters[3].Value!=System.DBNull.Value)
				RedoCount=(byte)parameters[3].Value;

			Sql=(string)data.Rows[0]["sql"];
			Xsl=(string)data.Rows[0]["xsl"];
		}




		
		public void DeleteReport(decimal UserId , decimal ReportId, bool DenyShared)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[3];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@ReportId" , ReportId);
			parameters[2]=new SqlParameter("@DenyShared" , DenyShared);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteSqlReport" , CommandType.StoredProcedure , parameters , null);
		}


				
		public void DeleteReportStates(decimal ReportId)
		{
			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@ReportId" , ReportId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteSqlReportStates" , CommandType.StoredProcedure , parameters , null);
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

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertSharedSqlReport" , CommandType.StoredProcedure , parameters , null);

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

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteSharedSqlReport" , CommandType.StoredProcedure , parameters , null);

			if(parameters[2].Value!=System.DBNull.Value)
				MaxSubscriberSharingStatus=(short)parameters[2].Value;
		}






		
		public FI.Common.Data.FIDataTable ReadReportResult(string Sql , string Database)
		{
			FI.Common.Data.FIDataTable data=new FI.Common.Data.FIDataTable();

			string connString=FI.Common.AppConfig.DA_OltpConnectionString.Replace("@DATABASE" , Database);

			DataBase.Instance.ExecuteCommand(Sql , connString , CommandType.Text , null , data);

			return data;
		}





	}
}
