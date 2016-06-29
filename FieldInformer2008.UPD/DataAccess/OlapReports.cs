using System;
using System.Data;
using System.Data.SqlClient;
using FI.Common.Data;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for OlapReports.
	/// </summary>
    public class OlapReports : MarshalByRefObject, FI.Common.DataAccess.IOlapReportsDA
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
            ref string GraphTheme,
			ref int GraphOptions,
            ref short GraphWidth,
            ref short GraphHeight,
            ref int GraphNum1,
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

            if (tbl.Rows[0]["GraphTheme"] != System.DBNull.Value)
                GraphTheme = (string)tbl.Rows[0]["GraphTheme"];

			if(tbl.Rows[0]["GraphOptions"]!=System.DBNull.Value)
                GraphOptions = (int)tbl.Rows[0]["GraphOptions"];

            if (tbl.Rows[0]["GraphWidth"] != System.DBNull.Value)
                GraphWidth = (short)tbl.Rows[0]["GraphWidth"];

            if (tbl.Rows[0]["GraphHeight"] != System.DBNull.Value)
                GraphHeight = (short)tbl.Rows[0]["GraphHeight"];

            if (tbl.Rows[0]["GraphNum1"] != System.DBNull.Value)
                GraphNum1 = (int)tbl.Rows[0]["GraphNum1"];

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

			ReportXml=this.ValidateReportXml(SchemaServer , SchemaDatabase, SchemaName, ReportXml);
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
            string GraphTheme,
			int GraphOptions,
            short GraphWidth,
            short GraphHeight,
            int GraphNum1,
			string ReportXml,
			string OpenNodesXml)
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
            parameters[7] = new SqlParameter("@GraphType", GraphType);
            parameters[8] = new SqlParameter("@GraphTheme", (GraphTheme==null ? DBNull.Value : (object)GraphTheme));
			parameters[9] = new SqlParameter("@GraphOptions" , GraphOptions);
            parameters[10] = new SqlParameter("@GraphWidth", GraphWidth);
            parameters[11] = new SqlParameter("@GraphHeight", GraphHeight);
            parameters[12] = new SqlParameter("@GraphNum1", GraphNum1);
			parameters[13]=new SqlParameter("@ReportXml" , ReportXml);
			parameters[14]=new SqlParameter("@OpenNodesXml" , OpenNodesXml);

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
            string GraphTheme,
			int GraphOptions,
            short GraphWidth,
            short GraphHeight,
            int GraphNum1
            )
		{
			SqlParameter[] parameters=new SqlParameter[14];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ParentReportID" , ParentReportID);
			parameters[3]=new SqlParameter("@SharingStatus" , SharingStatus);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Description" , Description);
			parameters[6]=new SqlParameter("@IsSelected" , IsSelected);
			parameters[7]=new SqlParameter("@OpenNodesXml" , OpenNodesXml);
			parameters[8]=new SqlParameter("@GraphType" , GraphType);
            parameters[9] = new SqlParameter("@GraphTheme", GraphTheme);
			parameters[10]=new SqlParameter("@GraphOptions" , GraphOptions);
            parameters[11] = new SqlParameter("@GraphWidth", GraphWidth);
            parameters[12] = new SqlParameter("@GraphHeight", GraphHeight);
            parameters[13] = new SqlParameter("@GraphNum1", GraphNum1);

			DataBase.Instance.ExecuteCommand("dbo.sproc_SaveOlapReportHeader" , CommandType.StoredProcedure , parameters , null);
		}


        internal void UpdateReportCache(
            decimal reportId,
            string server,
            string database,
            string cube,
            string mdx,
            string executeId,            
            string executeResult,
            DateTime cubeProcessedOn
            )
        {

            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@ReportId", reportId);
            parameters[1] = new SqlParameter("@Mdx", mdx);
            parameters[2] = new SqlParameter("@ExecuteId", executeId);
            parameters[3] = new SqlParameter("@ExecuteResult", executeResult);
            parameters[4] = new SqlParameter("@Server", (server == null ? DBNull.Value : (object)server));
            parameters[5] = new SqlParameter("@Database", (database == null ? DBNull.Value : (object)database));
            parameters[6] = new SqlParameter("@Cube", (cube == null ? DBNull.Value : (object)cube));
            parameters[7] = new SqlParameter("@CubeProcessedOn", (cubeProcessedOn == DateTime.MinValue ? System.Data.SqlTypes.SqlDateTime.MinValue : cubeProcessedOn));

            string sql =
@"update dbo.t_olap_reports_cache set 
execute_id=@ExecuteId, 
executed_on=GetDate(), 
mdx=@Mdx,
result=@ExecuteResult, 
server=@Server, 
[database]=@Database, 
cube=@Cube, 
cube_processed_on=@CubeProcessedOn 
where rpt_id=(select top 1 (case when sharing_status=4 then parent_report_id else id end) from dbo.t_olap_reports where id=@ReportId)";
            int count = DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, null);

            if (count <= 0)
            {
                sql = @"insert into dbo.t_olap_reports_cache 
(
rpt_id,
execute_id,
executed_on,
mdx,
result,
server,
[database],
cube,
cube_processed_on
)
select top 1
(case when sharing_status=4 then parent_report_id else id end),  
@ExecuteId,
GetDate(),
@Mdx,
@ExecuteResult,
@Server,
@Database,
@Cube,
@CubeProcessedOn
from dbo.t_olap_reports where id=@ReportId
";
                DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, null);
            }
        }

        public string GetCachedReportResult(
            decimal reportId
            )
        {
            string sql = string.Format(@"select result from dbo.t_olap_reports_cache where rpt_id=
                    (select top 1 id from dbo.t_olap_reports where sharing_status!=4 and id={0})", reportId.ToString());
            string ret = DataBase.Instance.ExecuteScalar(sql, null) as string;
            if (ret == null || ret=="")
            {

                sql = string.Format(@"select result from dbo.t_olap_reports_cache where rpt_id=
                    (select top 1 parent_report_id from dbo.t_olap_reports where id={0})", reportId.ToString());
                ret = DataBase.Instance.ExecuteScalar(sql, null) as string;

            }
            return (ret == "" ? null : ret);

            /*
            DataTable dt = new DataTable();
            DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                string ret = dt.Rows[0]["execute_result"] as string;
                if (ret == null || ret == "")
                    return null;
                if (skipInvalid)
                {
                    string server = dt.Rows[0]["server"] as string;
                    string database = dt.Rows[0]["database"] as string;
                    string cube = dt.Rows[0]["cube"] as string;
                    DateTime cubeProcessedOn = (DateTime)(dt.Rows[0]["cube_processed_on"] == DBNull.Value ? DateTime.MinValue : dt.Rows[0]["cube_processed_on"]);

                    OlapSystem os = new OlapSystem();
                    DateTime lastProcessed=os.GetCubeLastProcessed(server, database, cube, false);

                    return (cubeProcessedOn>=lastProcessed ? ret : null);
                }
            }
            */
        }


        public FIDataTable GetCashedReportsToRefresh(decimal companyId)
        {
            FIDataTable dt = new FIDataTable();

            // seelct reports linked to gauges
            string sql = string.Format(@"
select tbl.user_id, tbl.rpt_id, c.server, c.[database], c.cube, c.cube_processed_on
from
(
select distinct r.user_id, l.rpt_id
from dbo.t_gauges_reports l
inner join dbo.t_olap_reports r on l.rpt_type=0 and l.rpt_id=(case when r.sharing_status=4 then parent_report_id else id end)
inner join dbo.tusers u on r.user_id=u.id and u.company_id={0}
) tbl
left outer join dbo.t_olap_reports_cache c on tbl.rpt_id=c.rpt_id
",
            companyId.ToString());
            DataBase.Instance.ExecuteCommand(sql, CommandType.Text, null, dt);

            // check whther they are outdated
            OlapSystem osys=new OlapSystem();
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                string server=dt.Rows[i]["server"] as string;
                string database=dt.Rows[i]["database"] as string;
                string cube=dt.Rows[i]["cube"] as string;

                // no cache exists
                if (cube==null)
                    continue;

                // check if cube wasn't processed after last run
                DateTime rptProcess = (dt.Rows[i]["cube_processed_on"]==DBNull.Value ? DateTime.MinValue : (DateTime)dt.Rows[i]["cube_processed_on"]);
                DateTime lastProcess = osys.GetCubeLastProcessed(server, database, cube, false);
                if (rptProcess >= lastProcess)
                    dt.Rows.RemoveAt(i);
            }

            return dt;
        }

        public void DeleteReportCache(decimal reportId)
        {
            DataBase.Instance.ExecuteScalar(
                string.Format(
                "delete from dbo.t_olap_reports_cache where rpt_id={0}",
                reportId.ToString()),
                null);
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



		
		private string ValidateReportXml(string Server , string Database, string Cube, string InReportXml)
		{
			FI.DataAccess.OlapSystem olapSystem=new OlapSystem();
			return olapSystem.ValidateReportXml(Server , Database, Cube , InReportXml);
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

            // might throw exception
			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteOlapReport" , CommandType.StoredProcedure , parameters , null);

            DeleteReportCache(ReportId);
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
            DeleteReportCache(ChildReportId);

			if(parameters[2].Value!=System.DBNull.Value)
				MaxSubscriberSharingStatus=(short)parameters[2].Value;
		}


	}
}
