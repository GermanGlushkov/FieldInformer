
-- CHECKS DUE TO BUGS FROM PREVIOUS VERSIONS
DELETE FROM TDISTRIBUTION_LOG WHERE DISTRIBUTION_ID IN
(
SELECT ID FROM TDISTRIBUTION WHERE RPT_TYPE=0 AND RPT_ID NOT IN (SELECT ID FROM T_OLAP_REPORTS)
UNION
SELECT ID FROM TDISTRIBUTION  WHERE RPT_TYPE=1 AND RPT_ID NOT IN (SELECT ID FROM T_STORECHECK_REPORTS)
UNION
SELECT ID FROM TDISTRIBUTION  WHERE RPT_TYPE=2 AND RPT_ID NOT IN (SELECT ID FROM T_SQL_REPORTS)
UNION
SELECT ID FROM TDISTRIBUTION  WHERE RPT_TYPE=3 AND RPT_ID NOT IN (SELECT ID FROM T_MDX_REPORTS)
)
GO

DELETE FROM TDISTRIBUTION WHERE RPT_TYPE=0 AND RPT_ID NOT IN (SELECT ID FROM T_OLAP_REPORTS)
GO
DELETE FROM TDISTRIBUTION WHERE RPT_TYPE=1 AND RPT_ID NOT IN (SELECT ID FROM T_STORECHECK_REPORTS)
GO
DELETE FROM TDISTRIBUTION WHERE RPT_TYPE=2 AND RPT_ID NOT IN (SELECT ID FROM T_SQL_REPORTS)
GO
DELETE FROM TDISTRIBUTION WHERE RPT_TYPE=3 AND RPT_ID NOT IN (SELECT ID FROM T_MDX_REPORTS)
GO






-------------------------- tusers -----------------------------------
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 'tusers' and 
		COLUMN_NAME = 'css_style') 
BEGIN 
		ALTER TABLE dbo.tusers 
			ADD css_style tinyint NULL
END
GO


--------------------------- tdistribution ---------------------------

IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 'tdistribution' and 
		COLUMN_NAME = 'format') 
BEGIN 
		ALTER TABLE dbo.tdistribution 
			ADD [format] tinyint NULL

		UPDATE dbo.tdistribution SET [format]=0

		ALTER TABLE dbo.tdistribution
			ALTER COLUMN [format]tinyint NOT NULL
END		


------------------------- tdistribution_log duration column
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 'tdistribution_log' and 
		COLUMN_NAME = 'duration') 
BEGIN 
		ALTER TABLE dbo.tdistribution_log 
			ADD duration int NULL
END
GO	


------------------------- tdistribution_log taskguid column
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 'tdistribution_log' and 
		COLUMN_NAME = 'taskguid') 
BEGIN 
		ALTER TABLE dbo.tdistribution_log 
			ADD taskguid uniqueidentifier NULL
END
GO	

------------------------- tdistribution_log isfromcache column
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 'tdistribution_log' and 
		COLUMN_NAME = 'isfromcache') 
BEGIN 
		ALTER TABLE dbo.tdistribution_log 
			ADD isfromcache bit NULL
END
GO	

------------------------- t_storecheck_reports datasource column
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 't_storecheck_reports' and 
		COLUMN_NAME = 'datasource') 
BEGIN 
		ALTER TABLE dbo.t_storecheck_reports 
			ADD datasource tinyint NULL
END
GO	

------------------------- t_storecheck_reports_state datasource column
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 't_storecheck_reports_state' and 
		COLUMN_NAME = 'datasource') 
BEGIN 
		ALTER TABLE dbo.t_storecheck_reports_state 
			ADD datasource tinyint NULL
END
GO	


---------------------- changing tdistribution_log index
IF EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[tdistribution_log]') AND name = N'IX_tdistribution_log_distribution_id')
BEGIN
	DROP INDEX [dbo].[tdistribution_log].[IX_tdistribution_log_distribution_id]
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[tdistribution_log]') AND name = N'IX_tdistribution_log_1')
BEGIN
	CREATE NONCLUSTERED INDEX [IX_tdistribution_log_1] ON [dbo].[tdistribution_log] 
	(
		[distribution_id] ASC,
		[status] ASC
	) 
END
GO


------------------------- t_olap_reports graph extensions
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 't_olap_reports' and 
		COLUMN_NAME = 'graph_theme') 
BEGIN 
		ALTER TABLE dbo.t_olap_reports
			ADD graph_theme varchar(50) NULL
		
		ALTER TABLE dbo.t_olap_reports
			ADD graph_width smallint NULL
		
		ALTER TABLE dbo.t_olap_reports
			ADD graph_height smallint NULL
			
		ALTER TABLE dbo.t_olap_reports
			ADD graph_pie_columns tinyint NULL

END
GO	


-- add graph_num1
IF NOT EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 't_olap_reports' and 
		COLUMN_NAME = 'graph_num1') 
BEGIN 			
		ALTER TABLE dbo.t_olap_reports
			ADD graph_num1 int NULL
END
GO

-- pie columns renamed to num1, so drop pie columns
IF EXISTS (select * from INFORMATION_SCHEMA.COLUMNS WHERE 
		TABLE_SCHEMA = 'dbo' AND 
		TABLE_NAME = 't_olap_reports' and 
		COLUMN_NAME = 'graph_pie_columns') 
BEGIN 		
		EXEC('UPDATE t_olap_reports SET graph_num1=graph_pie_columns')
		
		ALTER TABLE dbo.t_olap_reports
			DROP COLUMN graph_pie_columns
END
GO


			





ALTER VIEW [dbo].[v_olap_reports]
 
as
SELECT
	usr_reports.[id],
	usr_reports.[parent_report_id],
	usr_reports.[sharing_status],
	ISNULL( (SELECT MAX(sharing_status) FROM dbo.T_OLAP_REPORTS children WHERE children.parent_report_id=usr_reports.[id]) , 0) as max_subscriber_sharing_status,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[user_id]
		WHEN 4 THEN parent_reports.[user_id]
		ELSE usr_reports.[user_id]
	END AS [owner_id],
	usr_reports.[user_id] ,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[name]
		WHEN 4 THEN parent_reports.[name]
		ELSE usr_reports.[name]
	END AS [name],
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[description]
		WHEN 4 THEN parent_reports.[description]
		ELSE usr_reports.[description]
	END AS [description],
	usr_reports.[is_selected],
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[graph_type]
		WHEN 4 THEN parent_reports.[graph_type]
		ELSE usr_reports.[graph_type]
	END AS [graph_type] ,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[graph_theme]
		WHEN 4 THEN parent_reports.[graph_theme]
		ELSE usr_reports.[graph_theme]
	END AS [graph_theme] ,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[graph_options]
		WHEN 4 THEN parent_reports.[graph_options]
		ELSE usr_reports.[graph_options]
	END AS [graph_options] ,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[graph_width]
		WHEN 4 THEN parent_reports.[graph_width]
		ELSE usr_reports.[graph_width]
	END AS [graph_width] ,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[graph_height]
		WHEN 4 THEN parent_reports.[graph_height]
		ELSE usr_reports.[graph_height]
	END AS [graph_height] ,
	CASE usr_reports.sharing_status
		WHEN 3 THEN parent_reports.[graph_num1]
		WHEN 4 THEN parent_reports.[graph_num1]
		ELSE usr_reports.[graph_num1]
	END AS [graph_num1] ,
	CASE usr_reports.sharing_status
		WHEN 4 THEN parent_reports.[data]
		ELSE usr_reports.[data]
	END AS [data] ,
	usr_reports.[open_nodes],
	CASE
		WHEN EXISTS(SELECT * FROM tdistribution WHERE tdistribution.rpt_id=usr_reports.id and rpt_type=0)	THEN	1
		ELSE	0
	END as is_in_distribution,
	usr_reports.[timestamp]
FROM dbo.T_OLAP_REPORTS usr_reports
LEFT OUTER JOIN dbo.T_OLAP_REPORTS parent_reports
ON usr_reports.parent_report_id=parent_reports.id
GO






ALTER  PROCEDURE [dbo].[sproc_LoadOlapReport] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

DECLARE @CurStateId numeric
DECLARE @UndoCount tinyint
DECLARE @RedoCount tinyint

SET @CurStateId=(SELECT [id] FROM t_olap_reports_state WHERE rpt_id=@ReportId AND is_current=1)

IF @CurStateId IS NULL
	BEGIN
		SET @UndoCount=0
		SET @RedoCount=0
	END
ELSE
	BEGIN
		SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_olap_reports_state WHERE rpt_id=@ReportId AND  [id]<@CurStateId) , 0)
		SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_olap_reports_state WHERE rpt_id=@ReportId AND  [id]>@CurStateId) , 0)
	END

-- current state might been left, delete it
IF @UndoCount=0 AND @RedoCount=0
	DELETE FROM t_olap_reports_state WHERE rpt_id=@ReportId


-------------------------------------------------------------
-- temp table because sql server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp(rpt_id numeric, [ReportXml] text)

INSERT INTO #tmp(rpt_id , ReportXml)
	SELECT rpt_id , data from t_olap_reports_state where is_current=1 and t_olap_reports_state.rpt_id=@ReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(rpt_id , ReportXml)
		SELECT [id] , data from v_olap_reports where [id]=@ReportId

-------------------------------------------------------------

SELECT
v_olap_reports.parent_report_id as [ParentReportId], 
v_olap_reports.name as [Name] ,  description as [Description] , sharing_status as [SharingStatus],
max_subscriber_sharing_status as [MaxSubscriberSharingStatus],
tmp.ReportXml,
[open_nodes] as [OpenNodesXml],
graph_type as GraphType,
graph_theme as GraphTheme,
graph_options as GraphOptions,
graph_width as GraphWidth,
graph_height as GraphHeight,
graph_num1 as GraphNum1,
is_selected as IsSelected,
olap_server as SchemaServer , olap_db as SchemaDatabase, olap_cube as SchemaName ,
@UndoCount as UndoCount, @RedoCount as RedoCount
 from v_olap_reports 
inner join tusers on v_olap_reports.user_id=tusers.[id]
inner join tcompany on tusers.company_id=tcompany.[id]
inner join  #tmp tmp on tmp.rpt_id=v_olap_reports.[id]
where v_olap_reports.[id]=@ReportId and tusers.[id]=@UserId

GO

ALTER PROCEDURE [dbo].[sproc_InsertOlapReport] 
@ReportId numeric OUT, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@GraphType tinyint,
@GraphTheme varchar(50),
@GraphOptions int,
@GraphWidth smallint,
@GraphHeight smallint,
@GraphNum1 int,
@ReportXml text,
@OpenNodesXml text


AS

SET NOCOUNT ON

INSERT INTO t_olap_reports(parent_report_id, sharing_status, user_id, name, description, is_selected, graph_type, graph_theme, graph_options, graph_width, graph_height, graph_num1, data , open_nodes)
	VALUES(@ParentReportId , @SharingStatus , @UserId , @Name , @Description , @IsSelected , @GraphType , @GraphTheme, @GraphOptions , @GraphWidth, @GraphHeight, @GraphNum1, @ReportXml , @OpenNodesXml )

SET @ReportId=@@identity
GO




ALTER PROCEDURE [dbo].[sproc_SaveOlapReportHeader] 
@ReportId numeric, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@OpenNodesXml text,
@GraphType tinyint,
@GraphTheme varchar(50),
@GraphOptions int,
@GraphWidth smallint,
@GraphHeight smallint,
@GraphNum1 int

AS

SET NOCOUNT ON

UPDATE t_olap_reports
SET 
parent_report_id=@ParentReportId,
sharing_status=@SharingStatus, 
user_id=@UserId,
name=@Name, 
description=@Description, 
is_selected=@IsSelected, 
open_nodes=@OpenNodesXml,
graph_type=@GraphType, 
graph_theme=@GraphTheme, 
graph_options=@GraphOptions,
graph_width=@GraphWidth,
graph_height=@GraphHeight,
graph_num1=@GraphNum1
WHERE [id]=@ReportId and user_id=@UserId


GO


if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_gauges]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin

	CREATE TABLE [dbo].[t_gauges] (
		[id] uniqueidentifier NOT NULL,
		[user_id] [decimal](18, 0) NOT NULL,
		[name] [nvarchar] (50) NULL DEFAULT(''),
		[type] [nvarchar] (50) NULL DEFAULT(''),
		[x] [int] NOT NULL,
		[y] [int] NOT NULL,
		[width] [int] NOT NULL,
		[height] [int] NOT NULL,
		[visible] [bit] NOT NULL,
		[refresh] [int] NOT NULL,
		[config] ntext NULL,
		[timestamp] timestamp NOT NULL
	) 

	ALTER TABLE [dbo].[t_gauges] WITH NOCHECK ADD 
		CONSTRAINT [PK_t_gauges] PRIMARY KEY  CLUSTERED 
		(
			[id]
		) 
	
	
	CREATE  INDEX [IX_t_gauges_user_id] ON [dbo].[t_gauges]([user_id]) ON [PRIMARY]
end
go


if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_gauges_reports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin

	CREATE TABLE [dbo].[t_gauges_reports] (
		[gauge_id] uniqueidentifier NOT NULL,
		[rpt_id] [decimal](18, 0) NOT NULL,
		[rpt_type] tinyint NOT NULL
	) 

	ALTER TABLE [dbo].[t_gauges_reports] WITH NOCHECK ADD 
		CONSTRAINT [PK_t_gauges_reports] PRIMARY KEY  CLUSTERED 
		(
			[gauge_id], [rpt_id], [rpt_type]
		) 
		
end

go




if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_olap_reports_cache]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin

CREATE TABLE [dbo].[t_olap_reports_cache] 
	(
		rpt_id [decimal](18, 0) NOT NULL,
		execute_id varchar(50) NULL,
		executed_on datetime NULL,
		mdx ntext NULL,
		result ntext NULL,
		server varchar(128) NULL,
		[database] varchar(128) NULL,
		cube varchar(128) NULL,
		cube_processed_on datetime NULL
	) 

	ALTER TABLE [dbo].[t_olap_reports_cache] WITH NOCHECK ADD 
		CONSTRAINT [PK_t_olap_reports_cache] PRIMARY KEY  CLUSTERED 
		(
			[rpt_id]
		) 
	
end
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_olap_reports_cache]'))
begin

exec('
CREATE VIEW [dbo].[v_olap_reports_cache] 
as
SELECT cache.*
FROM dbo.t_olap_reports rpt
INNER JOIN dbo.t_olap_reports_cache cache ON cache.rpt_id=(case when rpt.sharing_status=4 then parent_report_id else id end)
')

end
GO




ALTER   PROCEDURE [dbo].[sproc_InsertStorecheckReportCache] 
@ReportId decimal,
@Database varchar(50),
@ProductsSernList text,
@ProductsSernListCount int,
@ProductsJoinLogic tinyint,
@InSelOnly bit,
@InBSelOnly bit,
@DataSource tinyint,
@StartDate varchar(8),
@EndDate varchar(8)
AS

SET NOCOUNT ON

DECLARE @StrReportId varchar(15)
SET @StrReportId=CAST(@ReportId as varchar(15))	

DECLARE @StrProductsSernListCount varchar(15)
SET @StrProductsSernListCount=CAST(@ProductsSernListCount as varchar(15))

DECLARE @StrProductsJoinLogic varchar(2)
SET @StrProductsJoinLogic=CAST(@ProductsJoinLogic as varchar(2))


----------------------------------------- DATASOURCE FILTER ------------------------------------------------
DECLARE @DataSourceFilter varchar(256)

IF @DataSource=1
	SET @DataSourceFilter='LEN(LTRIM(ISNULL(TDELIVER.COMSERNWHS,'''')))>0'   -- DELIVERIES
ELSE IF @DataSource=2
	SET @DataSourceFilter='LEN(LTRIM(ISNULL(TDELIVER.COMSERNWHS,'''')))=0'   -- POS
ELSE
	SET @DataSourceFilter='0=0' -- ALL TRANSACTIONS


----------------------------------------- CACHING ------------------------------------------------

IF EXISTS(SELECT * FROM t_storecheck_reports_cache WHERE [rpt_id]=@ReportId)
	BEGIN
		RAISERROR ('Chache already exists' ,16,1)
		RETURN
	END


IF @ProductsJoinLogic=0  -- OR Logic
	BEGIN

		EXECUTE('
		INSERT INTO t_storecheck_reports_cache(RPT_ID  , COMSERNO , DELDATE , TYPE , SALMSERN)
			SELECT ' + @StrReportId + ' AS RPT_ID , COMSERNO , MAX(DELDATE) AS DELDATE , 0  AS TYPE , NULL AS SALMSERN
			FROM ' + @Database + '.spp.TDELIVER TDELIVER
			INNER JOIN ' + @Database + '.spp.TDELENTR TDELENTR ON TDELIVER.DELSERN=TDELENTR.DELSERN
			WHERE 
			' + @DataSourceFilter + ' 
			AND DELDATE<=' + @EndDate + ' AND DELDATE>=' + @StartDate + '
			AND PRODSERN IN (' + @ProductsSernList + ')
			GROUP BY COMSERNO
			')


		EXECUTE('
		INSERT INTO t_storecheck_reports_cache(RPT_ID  , COMSERNO , DELDATE ,TYPE , SALMSERN)
			SELECT ' + @StrReportId + ' AS RPT_ID , COMSERNO , MAX(DELDATE) AS DELDATE , 1 AS TYPE , NULL AS SALMSERN
			FROM ' + @Database + '.spp.TDELIVER TDELIVER
			INNER JOIN ' + @Database + '.spp.TDELENTR TDELENTR ON TDELIVER.DELSERN=TDELENTR.DELSERN
			WHERE 
			' + @DataSourceFilter + ' 
			AND NOT EXISTS(SELECT * FROM t_storecheck_reports_cache CACHE_CHK 
				WHERE CACHE_CHK.RPT_ID=' + @StrReportId + '
				AND CACHE_CHK.TYPE=0
				AND CACHE_CHK.COMSERNO=TDELIVER.COMSERNO )
			AND PRODSERN IN (' + @ProductsSernList + ')
			GROUP BY COMSERNO
			')

	END
ELSE			-- AND Logic
	BEGIN
		EXECUTE('
		INSERT INTO t_storecheck_reports_cache(RPT_ID , COMSERNO , DELDATE , TYPE , SALMSERN)
			SELECT ' + @StrReportId + ' AS RPT_ID , COMSERNO , MAX(DELDATE) AS DELDATE , 0 AS TYPE , NULL AS SALMSERN
			FROM ' + @Database + '.spp.TDELIVER TDELIVER
			INNER JOIN ' + @Database + '.spp.TDELENTR TDELENTR ON TDELIVER.DELSERN=TDELENTR.DELSERN
			WHERE 
			' + @DataSourceFilter + ' 
			AND DELDATE<=' + @EndDate + ' AND DELDATE>=' + @StartDate + '
			AND PRODSERN IN (' + @ProductsSernList + ')
			GROUP BY COMSERNO 
			HAVING COUNT(DISTINCT PRODSERN)=' + @StrProductsSernListCount + '
			')






		EXECUTE('
		INSERT INTO t_storecheck_reports_cache(RPT_ID  , COMSERNO , DELDATE , TYPE , SALMSERN )
			SELECT ' + @StrReportId + ' AS RPT_ID , COMSERNO , MAX(DELDATE) AS DELDATE , 1 AS TYPE , NULL AS SALMSERN
			FROM ' + @Database + '.spp.TDELIVER TDELIVER
			INNER JOIN ' + @Database + '.spp.TDELENTR TDELENTR ON TDELIVER.DELSERN=TDELENTR.DELSERN
			WHERE 
			' + @DataSourceFilter + ' 
			AND  NOT EXISTS(SELECT * FROM t_storecheck_reports_cache CACHE_CHK 
				WHERE CACHE_CHK.RPT_ID=' + @StrReportId + '
				AND CACHE_CHK.TYPE=0
				AND CACHE_CHK.COMSERNO=TDELIVER.COMSERNO )
			AND PRODSERN IN (' + @ProductsSernList + ')
			GROUP BY COMSERNO 
			HAVING COUNT(DISTINCT PRODSERN)=' + @StrProductsSernListCount + '
			')




	END


EXECUTE('
INSERT INTO t_storecheck_reports_cache(RPT_ID  , COMSERNO , DELDATE , TYPE , SALMSERN)
	SELECT ' + @StrReportId + ' AS RPT_ID , COMSERNO , MAX(DELDATE) AS DELDATE , 2 AS TYPE , NULL AS SALMSERN
	FROM ' + @Database + '.spp.TDELIVER TDELIVER
	INNER JOIN ' + @Database + '.spp.TDELENTR TDELENTR ON TDELIVER.DELSERN=TDELENTR.DELSERN
	WHERE 
	' + @DataSourceFilter + ' 
	AND NOT EXISTS(SELECT * FROM t_storecheck_reports_cache CACHE_CHK 
		WHERE CACHE_CHK.RPT_ID=' + @StrReportId + '
		AND CACHE_CHK.COMSERNO=TDELIVER.COMSERNO )
	GROUP BY COMSERNO
	')



---------- UPDATE SALMSERN ------------
EXECUTE('
UPDATE t_storecheck_reports_cache
SET SALMSERN=ISNULL(
	(SELECT MAX(SALMSERN)
	FROM ' + @Database + '.spp.OLAP_LCOMPGR lcompgr  INNER JOIN ' + @Database + '.spp.OLAP_LPROPGR lpropgr 
	ON lcompgr.PGRSERN=lpropgr.PGRSERN 
	WHERE lcompgr.COMSERNO=t_storecheck_reports_cache.COMSERNO AND lpropgr.PRODSERN IN (' + @ProductsSernList + ')
	),''0'')	
WHERE RPT_ID=' + @StrReportId + '
')





---------- UPDATE SELECTIONS ------------

IF @InBSelOnly=1  -- IN BASE SELECTION
	BEGIN

		EXECUTE('
		DELETE  FROM T_STORECHECK_REPORTS_CACHE
			WHERE 
			RPT_ID=' + @StrReportId + '
			AND
			TYPE!=0 --not and never delivered only
			AND
			ISNULL((SELECT COUNT(DISTINCT PRODSERN) FROM ' + @Database + '.spp.OLAP_SELECTION sel 
					WHERE sel.COMSERNO=T_STORECHECK_REPORTS_CACHE.COMSERNO and sel.PRODSERN IN (' + @ProductsSernList + ')
						and sel.code in (''1'', ''2'')
						and sel.sstart>=''' + @EndDate + ''' and sel.send<=''' + @EndDate + '''),0)<(CASE
												WHEN ' + @StrProductsJoinLogic + '=1	THEN ' + @StrProductsSernListCount + '	-- AND logic, all in sel
												ELSE	1 -- OR logic, atleast 1 in sel
											END)
		')

	END
ELSE
IF @InSelOnly=1  -- IN SELECTION
	BEGIN

		EXECUTE('
		DELETE  FROM T_STORECHECK_REPORTS_CACHE
			WHERE 
			RPT_ID=' + @StrReportId + '
			AND
			TYPE!=0 --not and never delivered only
			AND
			ISNULL((SELECT COUNT(DISTINCT PRODSERN) FROM ' + @Database + '.spp.OLAP_SELECTION sel 
					WHERE sel.COMSERNO=T_STORECHECK_REPORTS_CACHE.COMSERNO and sel.PRODSERN IN (' + @ProductsSernList + ')
						and sel.sstart>=''' + @EndDate + ''' and sel.send<=''' + @EndDate + '''),0)<(CASE
												WHEN ' + @StrProductsJoinLogic + '=1	THEN ' + @StrProductsSernListCount + '	-- AND logic, all in sel
												ELSE	1 -- OR logic, atleast 1 in sel
											END)
		')

	END

--------------------------------------------------------------------------------------------------
GO