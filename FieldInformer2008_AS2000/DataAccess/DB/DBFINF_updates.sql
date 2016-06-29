
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


			


---------- ALTER DEPENDANT OBJECTS (VIEWS AND SPROCS) --------------
ALTER VIEW dbo.v_olap_reports
 
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




