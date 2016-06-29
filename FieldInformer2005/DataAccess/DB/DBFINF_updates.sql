
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






