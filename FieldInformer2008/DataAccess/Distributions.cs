using System;
using System.Data;
using System.Data.SqlClient;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for Distributions.
	/// </summary>
    public class Distributions : MarshalByRefObject, FI.Common.DataAccess.IDistributionsDA
	{
		public Distributions()
		{
		}

        public DateTime GetCurrentTimestamp()
        {
            DateTime ret=(DateTime)DataBase.Instance.ExecuteScalar("select GetDate()", null);
            return ret;
        }

		public FI.Common.Data.FIDataTable ReadDistribution(decimal UserID , decimal DistributionID)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@DistributionId" , DistributionID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadDistribution" , CommandType.StoredProcedure , parameters , dataTable);
			return dataTable;
		}


		public FI.Common.Data.FIDataTable ReadDistributions(decimal UserID)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@UserId" , UserID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadDistributionsByUserId" , CommandType.StoredProcedure , parameters , dataTable);
			return dataTable;
		}


        public int GetQueuedDistributionsCount(decimal companyId)
        {
            string sql = @"
select count(*) from 
tdistribution_log dlog 
inner join tdistribution dist on dlog.distribution_id=dist.id
inner join tcontacts cnt on dist.contact_id=cnt.id
inner join tusers usr on cnt.user_id=usr.id
where usr.company_id=" + companyId.ToString() + @" and dlog.status='Pending'
";
            object ret = DataBase.Instance.ExecuteScalar(sql, null);
            return (ret == null || ret == DBNull.Value ? 0 : (int)ret);
        }

		public decimal ReadNextQueuedDistribution(decimal companyId)
		{
			object ret=null;

			// select pending, which can be resolved from cache (that is ok_log has greater timestamp than pending_log)
			string sql=@"
select top 1 dlog.id from 
tdistribution_log dlog 
inner join tdistribution dist on dlog.distribution_id=dist.id
inner join tcontacts cnt on dist.contact_id=cnt.id
inner join tusers usr on cnt.user_id=usr.id
where usr.company_id=" + companyId.ToString() + @" and dlog.status='Pending'
and exists(
select top 1 1 from tdistribution_log ok_log inner join tdistribution ok_dist on ok_log.distribution_id=ok_dist.id
where ok_log.Status='Ok' 
and ok_dist.rpt_id=dist.rpt_id  and ok_dist.rpt_type=dist.rpt_type
and ok_log.timestamp>=dlog.timestamp)
";
			ret=DataBase.Instance.ExecuteScalar(sql, null);
			if(ret!=null && ret!=DBNull.Value)
				return (decimal)ret;


			// select pending report by smallest duration time for last month. if last attempt wasn't ok or wasn't found, duration=999999			
			sql=@"
select top 1 dlog.id from 
tdistribution_log dlog 
inner join tdistribution dist on dlog.distribution_id=dist.id
inner join tcontacts cnt on dist.contact_id=cnt.id
inner join tusers usr on cnt.user_id=usr.id
where usr.company_id=" + companyId.ToString() + @" and dlog.status='Pending' --and isnull(dlog.isfromcache,0)=0
order by 
isnull((select top 1 duration 
from tdistribution_log ok_log inner join tdistribution ok_dist on ok_log.distribution_id=ok_dist.id
where ok_log.Status='Ok' 
and ok_dist.rpt_id=dist.rpt_id  and ok_dist.rpt_type=dist.rpt_type
and DATEDIFF(dd, ok_log.timestamp, dlog.timestamp)<=31 and isnull(ok_log.isfromcache,0)=0
),999999) 
asc";
			ret=DataBase.Instance.ExecuteScalar(sql, null);
			return (ret!=null && ret!=DBNull.Value ? (decimal)ret : -1);
		}


		public decimal InsertDistribution(decimal UserID , decimal ReportID , decimal ContactID , int ReportType , string FrequencyType , string FrequencyValue, int Format)
		{
			decimal DistributionID=-1;

			SqlParameter[] parameters=new SqlParameter[8];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@DistributionId" , DistributionID);
			parameters[1].Direction=ParameterDirection.Output;
			parameters[2]=new SqlParameter("@ReportId" , ReportID);
			parameters[3]=new SqlParameter("@ContactId" , ContactID);
			parameters[4]=new SqlParameter("@ReportType" , ReportType);
			parameters[5]=new SqlParameter("@FrequencyType" , FrequencyType);
			parameters[6]=new SqlParameter("@FrequencyValue" , FrequencyValue);
			parameters[7]=new SqlParameter("@Format" , Format);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertDistribution" , CommandType.StoredProcedure , parameters , null);

			if(parameters[1].Value!=System.DBNull.Value)
				DistributionID=(decimal)parameters[1].Value;

			return DistributionID;
		}


		public void UpdateDistribution(decimal UserID , decimal DistributionID , decimal ReportID , decimal ContactID , int ReportType , string FrequencyType , string FrequencyValue, int Format)
		{
			SqlParameter[] parameters=new SqlParameter[8];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@DistributionId" , DistributionID);
			parameters[2]=new SqlParameter("@ReportId" , ReportID);
			parameters[3]=new SqlParameter("@ContactId" , ContactID);
			parameters[4]=new SqlParameter("@ReportType" , ReportType);
			parameters[5]=new SqlParameter("@FrequencyType" , FrequencyType);
			parameters[6]=new SqlParameter("@FrequencyValue" , FrequencyValue);
			parameters[7]=new SqlParameter("@Format" , Format);

			DataBase.Instance.ExecuteCommand("dbo.sproc_UpdateDistribution" , CommandType.StoredProcedure , parameters , null);
		}


		public void DeleteDistribution(decimal DistributionID)
		{
			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , decimal.Zero); // parameter is obsolete, not used
			parameters[1]=new SqlParameter("@DistributionId" , DistributionID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteDistribution" , CommandType.StoredProcedure , parameters , null);
		}

		public void DeleteDistributionsByContact(decimal UserID , decimal ContactID)
		{
			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ContactId" , ContactID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteDistributionsByContact" , CommandType.StoredProcedure , parameters , null);
		}


		public void DeleteDistributionsByReport(decimal ReportID , int ReportType)
		{
			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@ReportId" , ReportID);
			parameters[1]=new SqlParameter("@ReportType" , ReportType);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteDistributionsByReport" , CommandType.StoredProcedure , parameters , null);
		}




		public FI.Common.Data.FIDataTable ReadDistributionsWithContactsPage(decimal UserID, decimal ReportID , int ReportType , int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[8];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ReportType" , ReportType);
			parameters[3]=new SqlParameter("@StartIndex" , StartIndex);
			parameters[4]=new SqlParameter("@RecordCount" , RecordCount);
			parameters[5]=new SqlParameter("@FilterExpression" , FilterExpression ); 
			parameters[6]=new SqlParameter("@SortExpression" , SortExpression); 
			parameters[7]=new SqlParameter("@TotalCount" , dataTable.TotalCount);
			parameters[7].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadDistributionsWithContactsPage" , CommandType.StoredProcedure , parameters , dataTable);

			if(parameters[7].Value!=System.DBNull.Value)
				dataTable.TotalCount=(int)parameters[7].Value;

			return dataTable;
		}




		public FI.Common.Data.FIDataTable ReadReportDistributionLog(decimal UserID, decimal ReportID , int ReportType , int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[8];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ReportId" , ReportID);
			parameters[2]=new SqlParameter("@ReportType" , ReportType);
			parameters[3]=new SqlParameter("@StartIndex" , StartIndex);
			parameters[4]=new SqlParameter("@RecordCount" , RecordCount);
			parameters[5]=new SqlParameter("@FilterExpression" , FilterExpression ); 
			parameters[6]=new SqlParameter("@SortExpression" , SortExpression); 
			parameters[7]=new SqlParameter("@TotalCount" , dataTable.TotalCount);
			parameters[7].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadReportDistributionLogPage" , CommandType.StoredProcedure , parameters , dataTable);

			if(parameters[7].Value!=System.DBNull.Value)
				dataTable.TotalCount=(int)parameters[7].Value;

			return dataTable;
		}



		public FI.Common.Data.FIDataTable ReadDistributionLog(decimal UserID , int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[6];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@StartIndex" , StartIndex);
			parameters[2]=new SqlParameter("@RecordCount" , RecordCount);
			parameters[3]=new SqlParameter("@FilterExpression" , FilterExpression ); 
			parameters[4]=new SqlParameter("@SortExpression" , SortExpression); 
			parameters[5]=new SqlParameter("@TotalCount" , dataTable.TotalCount);
			parameters[5].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadDistributionLogPage" , CommandType.StoredProcedure , parameters , dataTable);

			if(parameters[5].Value!=System.DBNull.Value)
				dataTable.TotalCount=(int)parameters[5].Value;

			return dataTable;
		}

		public FI.Common.Data.FIDataTable ReadDistributionQueue(decimal CompanyId, int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[6];
			parameters[0]=new SqlParameter("@CompanyId" , CompanyId);
			parameters[1]=new SqlParameter("@StartIndex" , StartIndex);
			parameters[2]=new SqlParameter("@RecordCount" , RecordCount);
			parameters[3]=new SqlParameter("@FilterExpression" , FilterExpression ); 
			parameters[4]=new SqlParameter("@SortExpression" , SortExpression); 
			parameters[5]=new SqlParameter("@TotalCount" , dataTable.TotalCount);
			parameters[5].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadDistributionQueuePage" , CommandType.StoredProcedure , parameters , dataTable);

			if(parameters[5].Value!=System.DBNull.Value)
				dataTable.TotalCount=(int)parameters[5].Value;

			return dataTable;
		}


		public FI.Common.Data.FIDataTable GetDistributionInfo(bool checkOnly)
		{
			FI.Common.Data.FIDataTable ret=new FI.Common.Data.FIDataTable();

			string sql=string.Format(@"
select * from 
(
SELECT   
d.Id,
d.freq_type as ScheduleType,
d.freq_value as ScheduleValue,
com.short_name as Domain, 
usr.id as UserId,
usr.name as UserName, 

cnt.[name] as Contact , 
cnt.[email] as ContactEmail , 

rpt.Id as ReportId,
rpt.rpt_type as ReportTypeCode,
case
	when rpt.rpt_type=0 then 'Olap'
	when rpt.rpt_type=1 then 'Storecheck'
	when rpt.rpt_type=2 then 'CustomSql'
	when rpt.rpt_type=3 then 'CustomMdx'
end as ReportType ,
rpt.name as ReportName , 
rpt.description as ReportDescr,


isnull((select top 1 status from tdistribution_log l1 where l1.distribution_id=d.id and status in ('Pending', 'Executing')),'') as IsQueued,

case 
	when isnull(rpt_ok_log.avg_duration,0)>7200 or isnull(rpt_ok_log.last_duration,0)>7200 -- avg or last ok more than 2 hours
		then 'Duration'
	when last_log.status='Canceled' and isnull(rpt_cancel_log.avg_duration,0)>7200 
		then 'Duration'
	when last_log.status='Error'
		then 'Last Error'
	when last_log.status='Canceled'
		then 'Last Canceled'
	else ''
end as CheckStatus,

last_log.status as LastStatus,
convert(varchar(25), last_log.timestamp, 120) as LastTimestamp,
last_log.message as LastMessage,

rpt_ok_log.log_count as OkCount,
rpt_ok_log.min_duration as OkMinDuration,
rpt_ok_log.max_duration as OkMaxDuration,
rpt_ok_log.avg_duration as OkAvgDuration,
rpt_ok_log.last_duration as OkLastDuration,

rpt_cancel_log.log_count as CancelCount,
rpt_cancel_log.avg_duration as CancelAvgDuration,

rpt_error_log.log_count as ErrorCount

from 
tdistribution d 
INNER JOIN tcontacts cnt on d.contact_id=cnt.[id]
INNER JOIN 
(
select [id] , 0 as rpt_type , name , description, user_id from v_olap_reports 
union
select [id] , 1 as rpt_type , name , description, user_id from v_storecheck_reports
union
select [id] , 2 as rpt_type , name , description, user_id from v_sql_reports
union
select [id] , 3 as rpt_type ,name , description, user_id from v_mdx_reports
) rpt on d.rpt_id=rpt.[id] and d.rpt_type=rpt.rpt_type
INNER JOIN tusers usr on rpt.user_id=usr.[id]
INNER JOIN tcompany com on usr.company_id=com.[id]
left outer join 
(select rpt_id, rpt_type, 
	count(*) as log_count, max(timestamp) as last_timestamp,
	max(duration) as max_duration, min(duration) as min_duration, avg(duration) as avg_duration, 	
	(select top 1 duration
		from tdistribution_log l2 inner join tdistribution d2 on l2.distribution_id=d2.id 
		where l2.status='Ok' and l2.timestamp=max(l1.timestamp) and isnull(l2.isfromcache,0)=0 
		and d1.rpt_id=d2.rpt_id and d1.rpt_type=d2.rpt_type
	) as last_duration
	from tdistribution_log l1 inner join tdistribution d1 on l1.distribution_id=d1.id 
	where l1.timestamp>=DATEADD(mm, -3, GetDate()) and l1.status='Ok' and isnull(l1.isfromcache,0)=0
	group by rpt_id, rpt_type
) rpt_ok_log on rpt_ok_log.rpt_id=d.rpt_id and rpt_ok_log.rpt_type=d.rpt_type
left outer join 
(select rpt_id, rpt_type, 
	count(*) as log_count, avg(duration) as avg_duration
	from tdistribution_log l1 inner join tdistribution d1 on l1.distribution_id=d1.id 
	where l1.timestamp>=DATEADD(mm, -3, GetDate()) and l1.status='Canceled'
	group by rpt_id, rpt_type
) rpt_cancel_log on rpt_cancel_log.rpt_id=d.rpt_id and rpt_cancel_log.rpt_type=d.rpt_type
left outer join 
(select rpt_id, rpt_type, 
	count(*) as log_count
	from tdistribution_log l1 inner join tdistribution d1 on l1.distribution_id=d1.id 
	where l1.timestamp>=DATEADD(mm, -3, GetDate()) and l1.status='Error'
	group by rpt_id, rpt_type
) rpt_error_log on rpt_error_log.rpt_id=d.rpt_id and rpt_error_log.rpt_type=d.rpt_type
left outer join 
(
select l2.distribution_id, l2.timestamp, l2.message, l2.status from
(select distribution_id, max(id) as log_id from tdistribution_log 
--where status in ('Ok', 'Canceled', 'Error') 
group by distribution_id) l1 inner join
tdistribution_log l2 on l1.distribution_id=l2.distribution_id and l1.log_id=l2.id
) last_log on last_log.distribution_id=d.id
) tbl 
{0}", (checkOnly ? "where CheckStatus!=''" : ""));

			DataBase.Instance.ExecuteCommand(sql , CommandType.Text , null , ret);
			return ret;
		}

		/*
		public FI.Common.Data.FIDataTable ReadMasterDistributionQueue(int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[5];
			parameters[0]=new SqlParameter("@StartIndex" , StartIndex);
			parameters[1]=new SqlParameter("@RecordCount" , RecordCount);
			parameters[2]=new SqlParameter("@FilterExpression" , FilterExpression ); 
			parameters[3]=new SqlParameter("@SortExpression" , SortExpression); 
			parameters[4]=new SqlParameter("@TotalCount" , dataTable.TotalCount);
			parameters[4].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadMasterDistributionQueuePage" , CommandType.StoredProcedure , parameters , dataTable);

			if(parameters[4].Value!=System.DBNull.Value)
				dataTable.TotalCount=(int)parameters[4].Value;

			return dataTable;
		}
		*/


		public decimal GetDistributionOwnerId(decimal distributionId)
		{
			DataTable dt=new DataTable();

			DataBase.Instance.ExecuteCommand(
				"select user_id from tdistribution inner join tcontacts on tdistribution.contact_id=tcontacts.id where tdistribution.id=" + distributionId.ToString(),
				CommandType.Text, 
				null,
				dt);

			return (dt==null || dt.Rows.Count==0 ? decimal.Zero : (decimal)dt.Rows[0][0]);
		}

		public decimal[] GetActiveDistributionQueueItems(decimal distributionId)
		{
			System.Collections.ArrayList ret=new System.Collections.ArrayList();

			DataTable dt=new DataTable();
			DataBase.Instance.ExecuteCommand(
				"select id from tdistribution_log where status in ('Executing', 'Pending') and distribution_id=" + distributionId.ToString(),
				CommandType.Text, 
				null,
				dt);

			if(dt!=null)
				foreach(DataRow r in dt.Rows)
					ret.Add(Convert.ToDecimal(r[0]));

			return (decimal[])ret.ToArray(typeof(decimal));
		}

		public void GetQueueItemInfo(decimal queueItemId, out decimal distributionId, out string status, out DateTime timestamp)
		{
			DataTable dt=new DataTable();

			DataBase.Instance.ExecuteCommand(
				"select distribution_id, status, timestamp from tdistribution_log where id=" + queueItemId.ToString(),
				CommandType.Text, 
				null,
				dt);

			distributionId=(dt==null || dt.Rows.Count==0 ? decimal.Zero : (decimal)dt.Rows[0][0]);
			status=(dt==null || dt.Rows.Count==0 ? null : dt.Rows[0][1].ToString());
			timestamp=(dt==null || dt.Rows.Count==0 || dt.Rows[0][2]==DBNull.Value? DateTime.MinValue : (DateTime)dt.Rows[0][2]);
		}

		public void EnqueueReportDistributions(decimal ReportID , int ReportType)
		{
			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@ReportId" , ReportID);
			parameters[1]=new SqlParameter("@ReportType" , ReportType);

			// get distributions
			DataTable dt=new DataTable();
			DataBase.Instance.ExecuteCommand(
				"select id from tdistribution where rpt_id=@ReportId and rpt_type=@ReportType" , 
				CommandType.Text , 
				parameters , 
				dt);

			// enqueue
			if(dt==null || dt.Rows.Count==0)
				return;
			foreach(DataRow dr in dt.Rows)
				this.EnqueueDistribution((decimal)dr[0], null);
		}

		public void EnqueueDistribution(decimal distributionId, string message)
		{
			SqlParameter[] parameters=new SqlParameter[3];
			parameters[0]=new SqlParameter("@DistributionId" , distributionId);
			parameters[1]=new SqlParameter("@Status" , "Pending"); // enqueued status = pending
			parameters[2]=new SqlParameter("@Message" , (message==null ? "" : message)); 

			DataBase.Instance.ExecuteCommand(
				@"
				if exists(select top 1 1 from tdistribution where id=@DistributionId) 
					and not exists(select top 1 1 from tdistribution_log where distribution_id=@DistributionId and status=@Status)
				begin
					insert into tdistribution_log(distribution_id, status, message) values(@DistributionId, @Status, @Message)
				end", 
				CommandType.Text, 
				parameters,
				null);
		}

		//		public bool HaveAllDistributionAttemptsFailed(decimal distributionId, ushort historyAttempts)
		//		{
		//			string sql="select top " + historyAttempts.ToString() + 
		//				" status from tdistribution_log where distribution_id=" + distributionId.ToString() + 
		//				" and status in ('Ok', 'Error') order by timestamp";
		//
		//			DataTable dt=new DataTable();
		//			DataBase.Instance.ExecuteCommand(sql, CommandType.Text, null, dt);
		//			if(dt==null || dt.Rows.Count==0)
		//				return false;
		//			foreach(DataRow r in dt.Rows)
		//				if(r[0].ToString().ToUpper()=="OK")
		//					return false;
		//
		//			return true;
		//	}

		public bool CancelQueuedItem(decimal queueItemId, string message)
		{			
			bool ok=false;

			// try to cancel		
			DataTable dt=new DataTable();
			DataBase.Instance.ExecuteCommand(
				string.Format("select taskGuid from tdistribution_log where id={0} and status in ('Pending', 'Executing')", queueItemId.ToString()), 
				CommandType.Text, 
				null,
				dt);
			if(dt!=null && dt.Rows.Count>0)
				foreach(DataRow dr in dt.Rows)
				{
					// if taskId is not empty, cancel it
					if(dr[0]!=null && dr[0]!=DBNull.Value)
					{
						Guid taskId=(Guid)dr[0];
						
						OlapSystem os=new OlapSystem();
						os.CancelOlapCommand(taskId.ToString());
					}			

					// write status		
					WriteDistributionQueueCanceled(queueItemId, (message==null ? "" : message));
				}

			return ok;
		}

		internal void OnSystemRestart()
		{
			while(true)
			{
				DataTable dt=new DataTable();
				DataBase.Instance.ExecuteCommand("select top 1 id, distribution_id from tdistribution_log where status='Executing' order by timestamp",
					CommandType.Text, null, dt);
				if(dt==null || dt.Rows.Count==0)
					return;

				decimal logId=(decimal)dt.Rows[0][0];
				decimal distrId=(decimal)dt.Rows[0][1];
				this.CancelQueuedItem(logId, "Canceled on system restart");
				this.EnqueueDistribution(distrId, "Requeued on system restart");	
			}
		}


		public void WriteDistributionQueueOk(decimal queueItemId , bool isFromCache)
		{
			string sql=string.Format(
@"update tdistribution_log set status='Ok', message='', taskguid=NULL, isfromcache={0}, timestamp=GetDate(),
duration= (case when timestamp is null then null else DATEDIFF(ss, timestamp, GetDate()) end)
where status in ('Pending', 'Executing') and id={1}", 
				(isFromCache ? "1" : "0"),
				queueItemId.ToString());

			DataBase.Instance.ExecuteCommand(sql, CommandType.Text, null, null);
		}

		public void WriteDistributionQueueError(decimal queueItemId , string message)
		{			
			string sql=string.Format(
@"update tdistribution_log set status='Error', message='{0}', taskguid=NULL, isfromcache=0, timestamp=GetDate(),
duration= (case when timestamp is null then null else DATEDIFF(ss, timestamp, GetDate()) end)
where status in ('Pending', 'Executing') and id={1}", 
				(message==null ? null : message.Replace("'", "''")),
				queueItemId.ToString());

			DataBase.Instance.ExecuteCommand(sql, CommandType.Text, null, null);
		}

		public void WriteDistributionQueueExecuting(decimal queueItemId , Guid taskGuid)
		{
			string sql=string.Format(
@"update tdistribution_log set status='Executing', message='', taskguid='{0}', isfromcache=0, timestamp=GetDate(),
duration=NULL
where status='Pending' and id={1}", 
				taskGuid.ToString(),
				queueItemId.ToString());

			DataBase.Instance.ExecuteCommand(sql, CommandType.Text, null, null);
		}

		public void WriteDistributionQueueCanceled(decimal queueItemId, string message)
		{
			string sql=string.Format(
				@"update tdistribution_log set status='Canceled', message='{0}', taskguid=NULL, isfromcache=0, timestamp=GetDate(),
duration= (case when timestamp is null then null else DATEDIFF(ss, timestamp, GetDate()) end)
where status in ('Pending', 'Executing') and id={1}", 
				(message==null ? null : message.Replace("'", "''")),
				queueItemId.ToString());

			DataBase.Instance.ExecuteCommand(sql, CommandType.Text, null, null);
		}



	}
}
