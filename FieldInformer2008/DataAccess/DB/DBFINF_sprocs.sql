

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteDistribution]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteDistribution]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteDistributionsByContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteDistributionsByContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteDistributionsByReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteDistributionsByReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteMdxReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteMdxReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteMdxReportStates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteMdxReportStates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteOlapReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteOlapReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteOlapReportStates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteOlapReportStates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteSharedMdxReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteSharedMdxReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteSharedOlapReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteSharedOlapReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteSharedSqlReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteSharedSqlReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteSharedStorecheckReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteSharedStorecheckReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteSqlReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteSqlReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteSqlReportStates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteSqlReportStates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteStorecheckReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteStorecheckReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteStorecheckReportCache]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteStorecheckReportCache]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteStorecheckReportStates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteStorecheckReportStates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_DeleteUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_DeleteUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_GetUserCurrentSession]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_GetUserCurrentSession]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertDistribution]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertDistribution]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertDistributionLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertDistributionLog]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertMdxReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertMdxReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertOlapReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertOlapReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertSharedMdxReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertSharedMdxReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertSharedOlapReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertSharedOlapReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertSharedSqlReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertSharedSqlReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertSharedStorecheckReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertSharedStorecheckReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertSqlReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertSqlReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertStorecheckReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertStorecheckReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertStorecheckReportCache]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertStorecheckReportCache]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_InsertUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_InsertUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadCompanies]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadCompanies]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadContactsByUserId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadContactsByUserId]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadContactsPage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadContactsPage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadDistribution]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadDistribution]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadDistributionLogPage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadDistributionLogPage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadDistributionsByUserId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadDistributionsByUserId]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadDistributionsWithContactsPage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadDistributionsWithContactsPage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadMdxReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadMdxReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadMdxReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadMdxReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadMdxReportHeaders]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadMdxReportHeaders]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadMdxReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadMdxReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadOlapReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadOlapReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadOlapReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadOlapReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadOlapReportHeaders]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadOlapReportHeaders]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadOlapReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadOlapReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadReportDistributionLogPage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadReportDistributionLogPage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadSqlReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadSqlReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadSqlReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadSqlReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadSqlReportHeaders]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadSqlReportHeaders]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadSqlReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadSqlReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadStorecheckReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadStorecheckReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadStorecheckReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadStorecheckReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadStorecheckReportHeaders]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadStorecheckReportHeaders]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadStorecheckReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadStorecheckReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadUserByAuthentication]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadUserByAuthentication]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadUserByUserId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadUserByUserId]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadUsers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadUsers]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadUsersWithChildReports]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadUsersWithChildReports]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveMdxReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveMdxReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveMdxReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveMdxReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveMdxReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveMdxReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveOlapReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveOlapReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveOlapReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveOlapReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveOlapReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveOlapReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveSqlReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveSqlReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveSqlReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveSqlReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveSqlReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveSqlReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveStorecheckReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveStorecheckReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveStorecheckReportHeader]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveStorecheckReportHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_SaveStorecheckReportState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_SaveStorecheckReportState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_UpdateContact]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_UpdateContact]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_UpdateDistribution]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_UpdateDistribution]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_UpdateUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_UpdateUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_UpdateUserSession]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_UpdateUserSession]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteContact] 
@ContactId numeric  ,
@UserId numeric 
AS


BEGIN TRAN

SET NOCOUNT ON

-- that is not actually necessary , business logic will take care of this
/*
delete from tdistribution_log where distribution_id in (select distribution_id from  tdistribution where contact_id=@ContactId)
delete from tdistribution where contact_id=@ContactId
*/

delete from tcontacts where [id]=@ContactId and  user_id=@UserId

COMMIT TRAN


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteDistribution] 
@UserId numeric ,
@DistributionId numeric 
AS

BEGIN TRAN

delete from tdistribution_log where distribution_id=@DistributionId 		
delete from tdistribution where [id]=@DistributionId

COMMIT TRAN


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteDistributionsByContact] 
@UserId numeric ,
@ContactId numeric 
AS

SET NOCOUNT ON

BEGIN TRAN

delete from tdistribution_log where exists(select * from tdistribution where tdistribution.[id]=tdistribution_log.distribution_id and tdistribution.contact_id=@ContactId)
delete from tdistribution where contact_id=@ContactId

COMMIT TRAN


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteDistributionsByReport] 
@ReportId numeric ,
@ReportType int 
AS

SET NOCOUNT ON

BEGIN TRAN

delete from tdistribution_log where exists(select * from tdistribution where tdistribution.[id]=tdistribution_log.distribution_id and tdistribution.rpt_id=@ReportId and tdistribution.rpt_type=@ReportType)
delete from tdistribution where [rpt_id]=@ReportId and rpt_type=@ReportType

COMMIT TRAN


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_DeleteMdxReport] 
@ReportId numeric  ,
@UserId numeric ,
@DenyShared bit
AS


BEGIN TRAN

SET NOCOUNT ON

-- that is not actually necessary , business logic will take care of this
/*
delete from tdistribution_log where distribution_id in (select distribution_id from  tdistribution where rpt_id=@ReportId and rpt_type=0)
delete from tdistribution where  rpt_id=@ReportId and rpt_type=0
*/

IF @DenyShared=1 AND EXISTS(SELECT  * FROM t_mdx_reports WHERE [id]=@ReportId AND  user_id=@UserId AND sharing_status!=0)
	RAISERROR( 'Cannot delete shared report' , 16 ,1)
ELSE
	delete from t_mdx_reports where [id]=@ReportId and  user_id=@UserId

COMMIT TRAN

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_DeleteMdxReportStates] 
@ReportId numeric 
AS


SET NOCOUNT ON

delete from t_mdx_reports_state where [rpt_id]=@ReportId



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteOlapReport] 
@ReportId numeric  ,
@UserId numeric ,
@DenyShared bit
AS


BEGIN TRAN

SET NOCOUNT ON

-- that is not actually necessary , business logic will take care of this
/*
delete from tdistribution_log where distribution_id in (select distribution_id from  tdistribution where rpt_id=@ReportId and rpt_type=0)
delete from tdistribution where  rpt_id=@ReportId and rpt_type=0
*/

IF @DenyShared=1 AND EXISTS(SELECT  * FROM t_olap_reports WHERE [id]=@ReportId AND  user_id=@UserId AND sharing_status!=0)
	RAISERROR( 'Cannot delete shared report' , 16 ,1)
ELSE
	delete from t_olap_reports where [id]=@ReportId and  user_id=@UserId

COMMIT TRAN

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteOlapReportStates] 
@ReportId numeric 
AS


SET NOCOUNT ON

delete from t_olap_reports_state where [rpt_id]=@ReportId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE [dbo].[sproc_DeleteSharedMdxReport] 
@ParentReportId numeric  ,
@ChildReportId numeric  ,
@MaxSubscriberSharingStatus tinyint OUT
AS


SET NOCOUNT ON

DELETE FROM t_mdx_reports WHERE [parent_report_id]=@ParentReportId  AND [id]=@ChildReportId

SET @MaxSubscriberSharingStatus=ISNULL( (SELECT max_subscriber_sharing_status FROM v_mdx_reports WHERE [id]=@ParentReportId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE [dbo].[sproc_DeleteSharedOlapReport] 
@ParentReportId numeric  ,
@ChildReportId numeric  ,
@MaxSubscriberSharingStatus tinyint OUT
AS


SET NOCOUNT ON

DELETE FROM t_olap_reports WHERE [parent_report_id]=@ParentReportId  AND [id]=@ChildReportId

SET @MaxSubscriberSharingStatus=ISNULL( (SELECT max_subscriber_sharing_status FROM v_olap_reports WHERE [id]=@ParentReportId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE [dbo].[sproc_DeleteSharedSqlReport] 
@ParentReportId numeric  ,
@ChildReportId numeric  ,
@MaxSubscriberSharingStatus tinyint OUT
AS


SET NOCOUNT ON

DELETE FROM t_sql_reports WHERE [parent_report_id]=@ParentReportId  AND [id]=@ChildReportId

SET @MaxSubscriberSharingStatus=ISNULL( (SELECT max_subscriber_sharing_status FROM v_sql_reports WHERE [id]=@ParentReportId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE [dbo].[sproc_DeleteSharedStorecheckReport] 
@ParentReportId numeric  ,
@ChildReportId numeric  ,
@MaxSubscriberSharingStatus tinyint OUT
AS


SET NOCOUNT ON

DELETE FROM t_storecheck_reports WHERE [parent_report_id]=@ParentReportId  AND [id]=@ChildReportId

SET @MaxSubscriberSharingStatus=ISNULL( (SELECT max_subscriber_sharing_status FROM v_storecheck_reports WHERE [id]=@ParentReportId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteSqlReport] 
@ReportId numeric  ,
@UserId numeric ,
@DenyShared bit
AS


BEGIN TRAN

SET NOCOUNT ON

-- that is not actually necessary , business logic will take care of this
/*
delete from tdistribution_log where distribution_id in (select distribution_id from  tdistribution where rpt_id=@ReportId and rpt_type=0)
delete from tdistribution where  rpt_id=@ReportId and rpt_type=0
*/

IF @DenyShared=1 AND EXISTS(SELECT  * FROM t_sql_reports WHERE [id]=@ReportId AND  user_id=@UserId AND sharing_status!=0)
	RAISERROR( 'Cannot delete shared report' , 16 ,1)
ELSE
	delete from t_sql_reports where [id]=@ReportId and  user_id=@UserId

COMMIT TRAN

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteSqlReportStates] 
@ReportId numeric 
AS


SET NOCOUNT ON

delete from t_sql_reports_state where [rpt_id]=@ReportId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_DeleteStorecheckReport] 
@ReportId numeric  ,
@UserId numeric ,
@DenyShared bit
AS


BEGIN TRAN

SET NOCOUNT ON

-- that is not actually necessary , business logic will take care of this
/*
delete from tdistribution_log where distribution_id in (select distribution_id from  tdistribution where rpt_id=@ReportId and rpt_type=0)
delete from tdistribution where  rpt_id=@ReportId and rpt_type=0
*/

IF @DenyShared=1 AND EXISTS(SELECT  * FROM t_storecheck_reports WHERE [id]=@ReportId AND  user_id=@UserId AND sharing_status!=0)
	RAISERROR( 'Cannot delete shared report' , 16 ,1)
ELSE
	delete from t_storecheck_reports where [id]=@ReportId and  user_id=@UserId

COMMIT TRAN

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteStorecheckReportCache] 
@ReportId numeric
AS

SET NOCOUNT ON


DELETE FROM t_storecheck_reports_cache WHERE rpt_id=@ReportId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_DeleteStorecheckReportStates] 
@ReportId numeric 
AS


SET NOCOUNT ON

delete from t_storecheck_reports_state where [rpt_id]=@ReportId




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_DeleteUser] 
@UserId numeric 
AS


BEGIN TRAN

SET NOCOUNT ON

delete from tusers where [id]=@UserId

COMMIT TRAN


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE [dbo].[sproc_GetUserCurrentSession]
@UserId numeric,
@SessionId varchar(50) OUT
 AS
SET NOCOUNT OFF


SELECT 
	@SessionId=session_id FROM tusers WHERE [id]=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[sproc_InsertAudit] 
@UserId decimal,
@CompanyId decimal,
@ConnectionAddress varchar(50),
@SessionId varchar(50),
@Timestamp datetime
AS

SET NOCOUNT ON

INSERT INTO dbo.taudit (user_id, company_id , conn_address, session_id, timestamp)
	VALUES(@UserId , @CompanyId , @ConnectionAddress , @SessionId , @Timestamp)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_InsertContact] 
@ContactId numeric OUT ,
@UserId numeric ,
@ContactName varchar(128) ,
@ContactEMail varchar(128),
@DistributionFormat varchar(50)
AS

SET NOCOUNT OFF  -- OFF because DAL need's number of rows affected

insert into tcontacts( user_id ,  [name] , [email], distr_format)
	values (@UserId , @ContactName , @ContactEMail , @DistributionFormat)

set @ContactId=@@identity


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE procedure [dbo].[sproc_InsertDistribution]
@UserId numeric,
@ReportId numeric,
@DistributionId numeric OUT,
@ContactId numeric,
@ReportType tinyint,
@FrequencyType varchar(50),
@FrequencyValue varchar(512),
@Format tinyint
as

SET NOCOUNT OFF

IF 
(
(@ReportType=0 and exists(select * from v_olap_reports where [id]=@ReportId and user_id=@UserId))
OR (@ReportType=1 and exists(select * from v_storecheck_reports where [id]=@ReportId and user_id=@UserId))
OR (@ReportType=2 and exists(select * from v_sql_reports where [id]=@ReportId and user_id=@UserId))
OR (@ReportType=3 and exists(select * from v_mdx_reports where [id]=@ReportId and user_id=@UserId))
)
AND
(
exists(select * from tcontacts where [id]=@ContactId and user_id=@UserId)
)
	begin
		insert into tdistribution(contact_id , rpt_id , rpt_type  , freq_type , freq_value, [format])
			values (@ContactId , @ReportId , @ReportType,  @FrequencyType , @FrequencyValue, @Format)
		
		set @DistributionId=@@identity
	end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE procedure [dbo].[sproc_InsertDistributionLog]
@DistributionId numeric,
@Status varchar(15),
@Message varchar(256)
as

SET NOCOUNT OFF

insert into tdistribution_log(distribution_id , status , message)
	values(@DistributionId , @Status , @Message)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_InsertMdxReport] 
@ReportId numeric OUT, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@Mdx text,
@Xsl text


AS

SET NOCOUNT ON

INSERT INTO t_mdx_reports(parent_report_id, sharing_status, user_id, name, description, is_selected, [mdx] , [xsl] )
	VALUES(@ParentReportId , @SharingStatus , @UserId , @Name , @Description , @IsSelected , @Mdx , @Xsl )

SET @ReportId=@@identity



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_InsertOlapReport] 
@ReportId numeric OUT, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@GraphType tinyint,
@GraphOptions int,
@ReportXml text,
@OpenNodesXml text


AS

SET NOCOUNT ON

INSERT INTO t_olap_reports(parent_report_id, sharing_status, user_id, name, description, is_selected, graph_type, graph_options, data , open_nodes)
	VALUES(@ParentReportId , @SharingStatus , @UserId , @Name , @Description , @IsSelected , @GraphType , @GraphOptions , @ReportXml , @OpenNodesXml )

SET @ReportId=@@identity


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





CREATE PROCEDURE [dbo].[sproc_InsertSharedMdxReport] 
@ParentReportId numeric, 
@SubscriberUserId numeric,
@SubscriberSharingStatus tinyint, 
@SubscriberReportId numeric OUT
AS

SET NOCOUNT ON

-------------------------------------------------------------
-- temp table because mdx server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp([id] numeric, [Mdx] text, [Xsl] text)

INSERT INTO #tmp([id] , [Mdx] , [Xsl])
	SELECT [rpt_id] , [mdx] , [xsl] from t_mdx_reports_state where is_current=1 and t_mdx_reports_state.rpt_id=@ParentReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(id , [Mdx], [Xsl])
		SELECT [id] , [mdx], [xsl] from v_mdx_reports where [id]=@ParentReportId

-------------------------------------------------------------

INSERT INTO t_mdx_reports (parent_report_id, sharing_status, user_id, name, description, is_selected, [mdx] , [xsl])
	SELECT @ParentReportId , @SubscriberSharingStatus, @SubscriberUserId, name,  description , 0, tmp.[Mdx], tmp.[xsl]
	FROM t_mdx_reports INNER JOIN #tmp tmp ON t_mdx_reports.[id]=tmp.[id]
	WHERE t_mdx_reports.[id]=@ParentReportId

SET @SubscriberReportId=@@identity

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_InsertSharedOlapReport] 
@ParentReportId numeric, 
@SubscriberUserId numeric,
@SubscriberSharingStatus tinyint, 
@SubscriberReportId numeric OUT
AS

SET NOCOUNT ON

-------------------------------------------------------------
-- temp table because sql server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp([id] numeric, [ReportXml] text)

INSERT INTO #tmp([id] , ReportXml)
	SELECT [rpt_id] , data from t_olap_reports_state where is_current=1 and t_olap_reports_state.rpt_id=@ParentReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(id , ReportXml)
		SELECT [id] , data from v_olap_reports where [id]=@ParentReportId

-------------------------------------------------------------

INSERT INTO t_olap_reports (parent_report_id, sharing_status, user_id, name, description, is_selected, graph_type, graph_options, data, open_nodes)
	SELECT @ParentReportId , @SubscriberSharingStatus, @SubscriberUserId, name,  description , 0, graph_type, graph_options, tmp.ReportXml, open_nodes
	FROM t_olap_reports INNER JOIN #tmp tmp ON t_olap_reports.[id]=tmp.[id]
	WHERE t_olap_reports.[id]=@ParentReportId

SET @SubscriberReportId=@@identity

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE [dbo].[sproc_InsertSharedSqlReport] 
@ParentReportId numeric, 
@SubscriberUserId numeric,
@SubscriberSharingStatus tinyint, 
@SubscriberReportId numeric OUT
AS

SET NOCOUNT ON

-------------------------------------------------------------
-- temp table because sql server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp([id] numeric, [Sql] text, [Xsl] text)

INSERT INTO #tmp([id] , [Sql] , [Xsl])
	SELECT [rpt_id] , [sql] , [xsl] from t_sql_reports_state where is_current=1 and t_sql_reports_state.rpt_id=@ParentReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(id , [Sql], [Xsl])
		SELECT [id] , [sql], [xsl] from v_sql_reports where [id]=@ParentReportId

-------------------------------------------------------------

INSERT INTO t_sql_reports (parent_report_id, sharing_status, user_id, name, description, is_selected, [sql] , [xsl])
	SELECT @ParentReportId , @SubscriberSharingStatus, @SubscriberUserId, name,  description , 0, tmp.[Sql], tmp.[xsl]
	FROM t_sql_reports INNER JOIN #tmp tmp ON t_sql_reports.[id]=tmp.[id]
	WHERE t_sql_reports.[id]=@ParentReportId

SET @SubscriberReportId=@@identity

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






CREATE PROCEDURE [dbo].[sproc_InsertSharedStorecheckReport] 
@ParentReportId numeric, 
@SubscriberUserId numeric,
@SubscriberSharingStatus tinyint, 
@SubscriberReportId numeric OUT
AS

SET NOCOUNT ON

-------------------------------------------------------------
-- temp table because storecheck server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp([rpt_id] numeric, [products_xml] text, [products_logic] tinyint, days smallint, filter_xml varchar(4000), insel bit , inbsel bit, datasource tinyint)

INSERT INTO #tmp([rpt_id] , [products_xml] , [products_logic], days , filter_xml, insel , inbsel, datasource )
	SELECT [rpt_id] , [products_xml] , [products_logic], days , filter_xml, insel , inbsel, datasource  from t_storecheck_reports_state where is_current=1 and t_storecheck_reports_state.rpt_id=@ParentReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(rpt_id , [products_xml] , [products_logic], days , filter_xml, insel, inbsel, datasource )
		SELECT [id] , [products_xml] , [products_logic], days , filter_xml, insel , inbsel, datasource  from v_storecheck_reports where [id]=@ParentReportId

-------------------------------------------------------------

INSERT INTO t_storecheck_reports (parent_report_id, sharing_status, user_id, name, description, is_selected, [products_xml] , [products_logic], days , filter_xml, insel , inbsel , datasource, cache_timestamp)
	SELECT @ParentReportId , @SubscriberSharingStatus, @SubscriberUserId, name,  description , 0, tmp.[products_xml] , tmp.[products_logic], tmp.days , tmp.filter_xml, tmp.insel , tmp.inbsel , tmp.datasource, cache_timestamp
	FROM t_storecheck_reports INNER JOIN #tmp tmp ON t_storecheck_reports.[id]=tmp.[rpt_id]
	WHERE t_storecheck_reports.[id]=@ParentReportId

SET @SubscriberReportId=@@identity

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_InsertSqlReport] 
@ReportId numeric OUT, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@Sql text,
@Xsl text


AS

SET NOCOUNT ON

INSERT INTO t_sql_reports(parent_report_id, sharing_status, user_id, name, description, is_selected, [sql] , [xsl] )
	VALUES(@ParentReportId , @SharingStatus , @UserId , @Name , @Description , @IsSelected , @Sql , @Xsl )

SET @ReportId=@@identity


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_InsertStorecheckReport] 
@ReportId numeric OUT, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@ProductsXml text,
@ProductsLogic tinyint,
@Days smallint,
@InSelOnly bit,
@InBSelOnly bit,
@DataSource tinyint,
@FilterXml varchar(4000),
@CacheTimestamp datetime
AS

SET NOCOUNT ON

INSERT INTO t_storecheck_reports(parent_report_id, sharing_status, user_id, name, description, is_selected, [products_xml] , [products_logic], days , insel, inbsel, datasource, filter_xml , cache_timestamp )
	VALUES(@ParentReportId , @SharingStatus , @UserId , @Name , @Description , @IsSelected , @ProductsXml , @ProductsLogic , @Days , @InSelOnly , @InBSelOnly, @DataSource, @FilterXml , @CacheTimestamp )

SET @ReportId=@@identity


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE   PROCEDURE [dbo].[sproc_InsertStorecheckReportCache] 
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
			ISNULL((SELECT SUM(INSEL) FROM ' + @Database + '.spp.OLAP_BASE_SELECTION sel 
					WHERE sel.COMSERNO=T_STORECHECK_REPORTS_CACHE.COMSERNO and sel.PRODSERN IN (' + @ProductsSernList + ')
						and sel.seldate<=''' + @EndDate + '''),0)<(CASE
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
			TYPE!=0  --not and never delivered only
			AND
			ISNULL((SELECT SUM(INSEL) FROM ' + @Database + '.spp.OLAP_SELECTION sel 
					WHERE sel.COMSERNO=T_STORECHECK_REPORTS_CACHE.COMSERNO and sel.PRODSERN IN (' + @ProductsSernList + ')
						and sel.seldate<=''' + @EndDate + '''),0)<(CASE
												WHEN ' + @StrProductsJoinLogic + '=1	THEN ' + @StrProductsSernListCount + '	-- AND logic, all in sel
												ELSE	1 -- OR logic, atleast 1 in sel
											END)
		')

	END

--------------------------------------------------------------------------------------------------





GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_InsertUser]
@Id numeric OUT,
@CompanyId numeric,
@Logon varchar(50),
@Password varchar(50),
@PasswordTimestamp datetime,
@Name nvarchar(50),
@Email nvarchar(50),
@IsAdmin bit,
@CssStyle tinyint
 AS
SET NOCOUNT OFF

IF EXISTS(SELECT * FROM tusers WHERE company_id=@CompanyId AND logon=@Logon)
	BEGIN
		RAISERROR('User with same name already exists' ,16,1)
		RETURN
	END

INSERT INTO tusers(company_id , logon, pwd , pwd_timestamp, name , email , is_admin , is_logged_in, css_style)
	VALUES(@CompanyId , @Logon, @Password, @PasswordTimestamp, @Name , @Email , @IsAdmin , 0, @CssStyle)

SET @Id=@@identity
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadCompanies]
 AS
SET NOCOUNT OFF


SELECT 
	short_name as CompanyNameShort,
	name as CompanyNameLong,
	ISNULL( SchemaServer ,'') as SchemaServer,
	ISNULL( SchemaDatabase ,'') as SchemaDatabase,
	ISNULL( SchemaName ,'') as SchemaName,
	ping
	from
	tcompany left outer join t_olap_schema
	on t_olap_schema.SchemaName=tcompany.olap_cube and  t_olap_schema.SchemaDatabase=tcompany.olap_db and  t_olap_schema.SchemaServer=tcompany.olap_server


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE  PROCEDURE [dbo].[sproc_LoadContact]
@ContactId numeric,
@UserId numeric
AS
SET NOCOUNT ON

SELECT [id] as [ContactId] , [name] as [Name] , email as [EMail], distr_format as DistributionFormat -- ,
--CAST((SELECT COUNT(*) FROM tdistribution WHERE tdistribution.contact_id=tcontacts.contact_id) as int) as [ReportsDistributed]
FROM tcontacts WHERE user_id=@UserId AND [id]=@ContactId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadContactsByUserId]
@UserId numeric
 AS
SET NOCOUNT ON

SELECT [id] as [ContactId] , [name] as  [Name] , email as [EMail], distr_format as DistributionFormat -- ,
--CAST((SELECT COUNT(*) FROM tdistribution WHERE tdistribution.contact_id=tcontacts.contact_id) as int) as [ReportsDistributed]
FROM tcontacts WHERE user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadContactsPage]
@UserId numeric,
@StartIndex int,
@RecordCount int,
@FilterExpression varchar(8000),
@SortExpression varchar(8000),
@TotalCount int OUT
 AS
SET NOCOUNT ON


if object_id('tempdb..#tmp') is not null
drop table #tmp

DECLARE @SqlString varchar(8000)
DECLARE @contact_id numeric
DECLARE @i int

SET @SqlString = 'declare temp_cursor CURSOR STATIC FOR select [id] FROM  tcontacts WHERE user_id=' + cast(@UserId as varchar(15))


IF Len(@FilterExpression)>0
	BEGIN
		SET @SqlString = @SqlString + ' AND  (' + @FilterExpression + ')'
	END


IF Len(@SortExpression)>0
	SET @SqlString = @SqlString + ' ORDER BY ' + @SortExpression



create table #tmp(
	[serno] smallint IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[contact_id] [numeric] NOT NULL 
)


EXECUTE(@SqlString)
SET @i=0
SET @StartIndex=@StartIndex+1
OPEN temp_cursor
FETCH ABSOLUTE @StartIndex from temp_cursor INTO @contact_id

WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
	BEGIN
		    INSERT INTO #tmp(contact_id)
			VALUES(@contact_id)
		
		    FETCH NEXT from temp_cursor INTO @contact_id
		    SET @i = @i + 1
	END

-- WORKS FOR STATIC CURSORS ONLY !!
SET @TotalCount = @@CURSOR_ROWS

CLOSE temp_cursor
DEALLOCATE temp_cursor



SELECT 
	tcontacts.[id],
	[name],
	[email],
	distr_format
	FROM tcontacts 
	inner join #tmp on tcontacts.[id]=#tmp.contact_id
	where user_id=@UserId
	order by serno


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadDistribution]
@UserId numeric,
@DistributionId numeric
 AS
SET NOCOUNT ON

SELECT [id] as [DistributionId] , contact_id as [ContactId] , rpt_id as [ReportId] , rpt_type as ReportType , freq_type as [FrequencyType]  , freq_value as [FrequencyValue] ,[format] as [Format],
		isnull((select top 1 [timestamp] from tdistribution_log where tdistribution_log.distribution_id=tdistribution.[id] order by [timestamp] desc) , cast(0 as datetime) ) as [LastLogTimestamp]
		FROM tdistribution WHERE [id]=@DistributionId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_LoadDistributionLogPage]
@UserId numeric,
@StartIndex int,
@RecordCount int,
@FilterExpression varchar(8000),
@SortExpression varchar(8000),
@TotalCount int OUT
 AS
SET NOCOUNT ON


SET NOCOUNT ON


if object_id('tempdb..#tmp') is not null
drop table #tmp

DECLARE @SqlString varchar(8000)
DECLARE @log_id numeric
DECLARE @status varchar(15)
DECLARE @duration int
DECLARE @message varchar(256)
DECLARE @timestamp datetime
DECLARE @contact_name varchar(128)
DECLARE @contact_email varchar(128)
DECLARE @rpt_type varchar(50)
DECLARE @rpt_name varchar(128)
DECLARE @rpt_description varchar(512)
DECLARE @i int



SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
SELECT * FROM 
(
SELECT   tlog.[id] as log_id , tlog.status , tlog.duration, tlog.message , tlog.[timestamp] , tcnt.[name] as contact_name , tcnt.[email] as contact_email , 
case
	when trpt.rpt_type=0 then ''Olap Report''
	when trpt.rpt_type=1 then ''Storecheck Report''
	when trpt.rpt_type=2 then ''Sql Report''
	when trpt.rpt_type=3 then ''Mdx Report''
end as rpt_type ,
trpt.name as rpt_name , trpt.description as rpt_description
FROM tdistribution_log tlog
INNER JOIN tdistribution tdistr on tlog.distribution_id=tdistr.[id]
INNER JOIN tcontacts tcnt on tdistr.contact_id=tcnt.[id]
INNER JOIN 
(
select [id] , 0 as rpt_type , name , description from v_olap_reports 
union
select [id] , 1 as rpt_type , name , description from v_storecheck_reports
union
select [id] , 2 as rpt_type , name , description from v_sql_reports
union
select [id] , 3 as rpt_type ,name , description from v_mdx_reports
) trpt 
on tdistr.rpt_id=trpt.[id] and tdistr.rpt_type=trpt.rpt_type
WHERE
tcnt.user_id=' + cast(@UserId as varchar(15)) + '
) TBL
'



IF Len(@FilterExpression)>0
	BEGIN
		SET @SqlString = @SqlString + ' WHERE  (' + @FilterExpression + ')'
	END


IF Len(@SortExpression)>0
	SET @SqlString = @SqlString + ' ORDER BY ' + @SortExpression
ELSE
	SET @SqlString = @SqlString + ' ORDER BY id DESC '



create table #tmp(
	[serno] int IDENTITY(1,1) PRIMARY KEY,
	[log_id] numeric NOT NULL ,
	[duration] int NULL,
	[status] varchar(15) NULL,
	[message] varchar(256) NULL,
	[timestamp] datetime NULL,
	[contact_name] varchar(128) NULL,
	[contact_email] varchar(128) NULL,
	[rpt_type] varchar(50) NULL,
	[rpt_name] varchar(128) NULL,
	[rpt_description] varchar(512) NULL
	
)


EXECUTE(@SqlString)
SET @i=0
SET @StartIndex=@StartIndex+1
OPEN temp_cursor
FETCH ABSOLUTE @StartIndex from temp_cursor INTO @log_id , @status ,  @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description

WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
	BEGIN
		    INSERT INTO #tmp( log_id ,status , duration, message , timestamp  , contact_name , contact_email , rpt_type  , rpt_name , rpt_description )
			VALUES(@log_id , @status , @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description)
		
		    FETCH NEXT from temp_cursor INTO @log_id , @status , @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description
		    SET @i = @i + 1
	END

-- WORKS FOR STATIC CURSORS ONLY !!
SET @TotalCount = @@CURSOR_ROWS

CLOSE temp_cursor
DEALLOCATE temp_cursor



SELECT 
	 log_id ,
	status , 
	duration ,
	message ,
	timestamp  , 
	contact_name , 
	contact_email ,
	rpt_type  , 
	rpt_name , 
	rpt_description
	FROM #tmp
	order by serno
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE  PROCEDURE [dbo].[sproc_LoadDistributionsByUserId]
@UserId numeric
 AS
SET NOCOUNT ON

SELECT [id] as [DistributionId] , contact_id as [ContactId] , rpt_id as [ReportId] , rpt_type as [ReportType] , freq_type as [FrequencyType]  , freq_value as [FrequencyValue]  ,[format] as [Format],
isnull((select top 1 [timestamp] from tdistribution_log where tdistribution_log.distribution_id=tdistribution.[id] order by [timestamp] desc) , cast(0 as datetime) ) as [LastLogTimestamp]
FROM tdistribution where 
exists(select * from tcontacts where tcontacts.[id]=tdistribution.contact_id and user_id=@UserId)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadDistributionsWithContactsPage] 
@UserId numeric,
@ReportId numeric,
@ReportType tinyint,
@StartIndex int,
@RecordCount int,
@FilterExpression varchar(8000),
@SortExpression varchar(8000),
@TotalCount int OUT
AS

SET NOCOUNT ON


if object_id('tempdb..#tmp') is not null
drop table #tmp

DECLARE @SqlString varchar(8000)
DECLARE @contact_id numeric
DECLARE @name varchar(128)
DECLARE @email varchar(128)
DECLARE @distr_format varchar(50)
DECLARE @distribution_id numeric
DECLARE @freq_type varchar(50)
DECLARE @freq_value varchar(512)
DECLARE @format tinyint
DECLARE @i int


IF @ReportType=0
	BEGIN
		SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
		SELECT * FROM 
		(
		SELECT  cont.[id] as cont_id , cont.[name]  , cont.email  , cont.distr_format , dist.[id] as distr_id , dist.freq_type , dist.freq_value, dist.[format] FROM 
			(select tdistribution.[id] ,  tdistribution.freq_type , tdistribution.freq_value , tdistribution.contact_id, tdistribution.[format] FROM v_olap_reports INNER JOIN tdistribution ON v_olap_reports.[id]=tdistribution.rpt_id 
			WHERE v_olap_reports.user_id=' + cast(@UserId as varchar(15)) + ' AND tdistribution.rpt_type=' + cast(@ReportType as varchar(15)) + ' AND v_olap_reports.[id]=' + cast(@ReportId as varchar(15)) + ') dist
			RIGHT OUTER JOIN tcontacts cont ON dist.contact_id=cont.[id] where cont.user_id=' + cast(@UserId as varchar(15)) + '
		) TBL
		'
	END
ELSE
IF @ReportType=1
	BEGIN
		SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
		SELECT * FROM 
		(
		SELECT  cont.[id] as cont_id  , cont.[name] as name  , cont.email as email  , cont.distr_format , dist.[id] as dist_id  , dist.freq_type , dist.freq_value, dist.[format] FROM 
			(select tdistribution.[id] ,  tdistribution.freq_type , tdistribution.freq_value , tdistribution.contact_id, tdistribution.[format] FROM v_storecheck_reports INNER JOIN tdistribution ON v_storecheck_reports.[id]=tdistribution.rpt_id 
			WHERE v_storecheck_reports.user_id=' + cast(@UserId as varchar(15)) + ' AND tdistribution.rpt_type=' + cast(@ReportType as varchar(15)) + ' AND v_storecheck_reports.[id]=' + cast(@ReportId as varchar(15)) + ') dist
			RIGHT OUTER JOIN tcontacts cont ON dist.contact_id=cont.[id] where cont.user_id=' + cast(@UserId as varchar(15)) + '
		) TBL
		'
	END
ELSE
IF @ReportType=2
	BEGIN
		SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
		SELECT * FROM 
		(
		SELECT  cont.[id] as cont_id  , cont.[name]  , cont.email  , cont.distr_format , dist.[id] as dist_id  , dist.freq_type , dist.freq_value, dist.[format] FROM 
			(select tdistribution.[id] ,  tdistribution.freq_type , tdistribution.freq_value , tdistribution.contact_id, tdistribution.[format] FROM v_sql_reports INNER JOIN tdistribution ON v_sql_reports.[id]=tdistribution.rpt_id 
			WHERE v_sql_reports.user_id=' + cast(@UserId as varchar(15)) + ' AND tdistribution.rpt_type=' + cast(@ReportType as varchar(15)) + ' AND v_sql_reports.[id]=' + cast(@ReportId as varchar(15)) + ') dist
			RIGHT OUTER JOIN tcontacts cont ON dist.contact_id=cont.[id] where cont.user_id=' + cast(@UserId as varchar(15)) + '
		) TBL
		'
	END
IF @ReportType=3
	BEGIN
		SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
		SELECT * FROM 
		(
		SELECT  cont.[id] as cont_id  , cont.[name]  , cont.email  , cont.distr_format , dist.[id] as dist_id  , dist.freq_type , dist.freq_value, dist.[format] FROM 
			(select tdistribution.[id] ,  tdistribution.freq_type , tdistribution.freq_value , tdistribution.contact_id, tdistribution.[format] FROM v_mdx_reports INNER JOIN tdistribution ON v_mdx_reports.[id]=tdistribution.rpt_id 
			WHERE v_mdx_reports.user_id=' + cast(@UserId as varchar(15)) + ' AND tdistribution.rpt_type=' + cast(@ReportType as varchar(15)) + ' AND v_mdx_reports.[id]=' + cast(@ReportId as varchar(15)) + ') dist
			RIGHT OUTER JOIN tcontacts cont ON dist.contact_id=cont.[id] where cont.user_id=' + cast(@UserId as varchar(15)) + '
		) TBL
		'
	END


IF Len(@FilterExpression)>0
	BEGIN
		SET @SqlString = @SqlString + ' WHERE  (' + @FilterExpression + ')'
	END


IF Len(@SortExpression)>0
	SET @SqlString = @SqlString + ' ORDER BY ' + @SortExpression



create table #tmp(
	[serno] smallint IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[contact_id] numeric NOT NULL ,
	[name] varchar(128) NULL,
	[email] varchar(128) NULL,
	[distr_format] varchar(50) NULL,
	[distribution_id] numeric  NULL ,
	[freq_type] varchar(50) NULL,
	[freq_value] varchar(512) NULL,
	[format] tinyint NULL
)


EXECUTE(@SqlString)
SET @i=0
SET @StartIndex=@StartIndex+1
OPEN temp_cursor
FETCH ABSOLUTE @StartIndex from temp_cursor INTO @contact_id , @name , @email , @distr_format , @distribution_id , @freq_type ,@freq_value, @format

WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
	BEGIN
		    INSERT INTO #tmp(contact_id , name , email, distr_format ,distribution_id ,freq_type , freq_value, [format] )
			VALUES(@contact_id , @name , @email , @distr_format , @distribution_id , @freq_type ,@freq_value, @format )
		
		    FETCH NEXT from temp_cursor INTO @contact_id , @name , @email , @distr_format , @distribution_id , @freq_type ,@freq_value, @format
		    SET @i = @i + 1
	END

-- WORKS FOR STATIC CURSORS ONLY !!
SET @TotalCount = @@CURSOR_ROWS

CLOSE temp_cursor
DEALLOCATE temp_cursor



SELECT 
	contact_id,
	name,
	email,
	distr_format,
	distribution_id,
	freq_type,
	freq_value,
	[format]
	FROM #tmp
	order by serno
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





CREATE  PROCEDURE [dbo].[sproc_LoadMdxReport] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

DECLARE @CurStateId numeric
DECLARE @UndoCount tinyint
DECLARE @RedoCount tinyint

SET @CurStateId=(SELECT [id] FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND is_current=1)

IF @CurStateId IS NULL
	BEGIN
		SET @UndoCount=0
		SET @RedoCount=0
	END
ELSE
	BEGIN
		SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND  [id]<@CurStateId) , 0)
		SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND  [id]>@CurStateId) , 0)
	END

-- current state might been left, delete it
IF @UndoCount=0 AND @RedoCount=0
	DELETE FROM t_mdx_reports_state WHERE rpt_id=@ReportId


-------------------------------------------------------------
-- temp table because mdx server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp(rpt_id numeric, [Mdx] text, [Xsl] text)

INSERT INTO #tmp(rpt_id , [Mdx], [Xsl])
	SELECT rpt_id , [mdx], [xsl] from t_mdx_reports_state where is_current=1 and t_mdx_reports_state.rpt_id=@ReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(rpt_id , [Mdx], [Xsl])
		SELECT [id] , [mdx], [xsl] from v_mdx_reports where [id]=@ReportId

-------------------------------------------------------------

SELECT
v_mdx_reports.parent_report_id as [ParentReportId], 
v_mdx_reports.name as [Name] ,  description as [Description] , sharing_status as [SharingStatus],
max_subscriber_sharing_status as [MaxSubscriberSharingStatus],
is_selected as IsSelected,
tmp.[Mdx],
tmp.[Xsl],
olap_server as SchemaServer , olap_db as SchemaDatabase, olap_cube as SchemaName ,
@UndoCount as UndoCount, @RedoCount as RedoCount
 from v_mdx_reports 
inner join tusers on v_mdx_reports.user_id=tusers.[id]
inner join tcompany on tusers.company_id=tcompany.[id]
inner join  #tmp tmp on tmp.rpt_id=v_mdx_reports.[id]
where v_mdx_reports.[id]=@ReportId and tusers.[id]=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_LoadMdxReportHeader] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

select [id]  , parent_report_id , owner_id , name , description , is_selected  ,sharing_status, max_subscriber_sharing_status , is_in_distribution
	from v_mdx_reports where [id]=@ReportId  and user_id=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_LoadMdxReportHeaders] 
@UserId numeric
AS
set nocount on

select rpt.[id]  , parent_report_id , owner_id , owner.name as owner_name , rpt.name , description , is_selected , sharing_status, max_subscriber_sharing_status, is_in_distribution , rpt.[timestamp]
			from v_mdx_reports rpt
			inner join tusers owner  on rpt.owner_id=owner.id
			where user_id=@UserId
			order by rpt.name

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_LoadMdxReportState]
@ReportId numeric,
@StateCode smallint,
@UndoCount tinyint OUT,
@RedoCount tinyint OUT
 AS
SET NOCOUNT ON

DECLARE @NewStateId numeric
DECLARE @CurrentStateId numeric

SELECT @CurrentStateId=[id] from t_mdx_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId=NULL
	RAISERROR ('No current state found.', 16, 1)

IF @StateCode=0 --load current state
	SET @NewStateId=@CurrentStateId
ELSE IF @StateCode<0 -- it's Undo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId ORDER BY [id] DESC)
ELSE -- it's Redo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND [id]>@CurrentStateId ORDER BY [id] ASC)

IF @NewStateId IS NULL
	RAISERROR ('Cannot find next state.', 16, 1)

UPDATE t_mdx_reports_state 
	SET is_current=0
	WHERE [id]=@CurrentStateId

UPDATE t_mdx_reports_state 
	SET is_current=1
	WHERE [id]=@NewStateId

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND [id]<@NewStateId) , 0)
SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND [id]>@NewStateId) , 0)

SELECT  [Mdx] , [Xsl] FROM t_mdx_reports_state WHERE [id]=@NewStateId



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE  PROCEDURE [dbo].[sproc_LoadOlapReport] 
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
graph_options as GraphOptions,
is_selected as IsSelected,
olap_server as SchemaServer , olap_db as SchemaDatabase, olap_cube as SchemaName ,
@UndoCount as UndoCount, @RedoCount as RedoCount
 from v_olap_reports 
inner join tusers on v_olap_reports.user_id=tusers.[id]
inner join tcompany on tusers.company_id=tcompany.[id]
inner join  #tmp tmp on tmp.rpt_id=v_olap_reports.[id]
where v_olap_reports.[id]=@ReportId and tusers.[id]=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadOlapReportHeader] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

select [id]  , parent_report_id , owner_id , name , description , is_selected  ,sharing_status, max_subscriber_sharing_status , is_in_distribution , open_nodes
	from v_olap_reports where [id]=@ReportId  and user_id=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadOlapReportHeaders] 
@UserId numeric
AS
set nocount on

select rpt.[id]  , parent_report_id , owner_id , owner.name as owner_name , rpt.name , description , is_selected , sharing_status, max_subscriber_sharing_status, is_in_distribution , rpt.[timestamp]
			from v_olap_reports rpt
			inner join tusers owner  on rpt.owner_id=owner.id
			where user_id=@UserId
			order by rpt.name

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadOlapReportState]
@ReportId numeric,
@StateCode smallint,
@UndoCount tinyint OUT,
@RedoCount tinyint OUT
 AS
SET NOCOUNT ON

DECLARE @NewStateId numeric
DECLARE @CurrentStateId numeric

SELECT @CurrentStateId=[id] from t_olap_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId=NULL
	RAISERROR ('No current state found.', 16, 1)

IF @StateCode=0 --load current state
	SET @NewStateId=@CurrentStateId
ELSE IF @StateCode<0 -- it's Undo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_olap_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId ORDER BY [id] DESC)
ELSE -- it's Redo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_olap_reports_state WHERE rpt_id=@ReportId AND [id]>@CurrentStateId ORDER BY [id] ASC)

IF @NewStateId IS NULL
	RAISERROR ('Cannot find next state.', 16, 1)

UPDATE t_olap_reports_state 
	SET is_current=0
	WHERE [id]=@CurrentStateId

UPDATE t_olap_reports_state 
	SET is_current=1
	WHERE [id]=@NewStateId

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_olap_reports_state WHERE rpt_id=@ReportId AND [id]<@NewStateId) , 0)
SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_olap_reports_state WHERE rpt_id=@ReportId AND [id]>@NewStateId) , 0)

SELECT  [data] as ReportXml  FROM t_olap_reports_state WHERE [id]=@NewStateId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_LoadReportDistributionLogPage]
@UserId numeric,
@ReportId numeric,
@ReportType tinyint,
@StartIndex int,
@RecordCount int,
@FilterExpression varchar(8000),
@SortExpression varchar(8000),
@TotalCount int OUT
 AS
SET NOCOUNT ON


SET NOCOUNT ON


if object_id('tempdb..#tmp') is not null
drop table #tmp

DECLARE @SqlString varchar(8000)
DECLARE @log_id numeric
DECLARE @duration int
DECLARE @isfromcache varchar(5)
DECLARE @status varchar(15)
DECLARE @message varchar(256)
DECLARE @timestamp datetime
DECLARE @contact_name varchar(128)
DECLARE @contact_email varchar(128)
DECLARE @i int



SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
SELECT 
*
FROM 
(
SELECT   tlog.[id] as log_id , tlog.status , 
isnull(tlog.duration,0) as duration, 
(case when isnull(tlog.isfromcache,0)=0 then ''No'' else ''Yes'' end) as isfromcache, 
tlog.message , tlog.[timestamp] , tcnt.name as contact_name , tcnt.email as contact_email
FROM tdistribution_log tlog
INNER JOIN tdistribution tdistr on tlog.distribution_id=tdistr.[id]
INNER JOIN tcontacts tcnt on tdistr.contact_id=tcnt.[id]
WHERE tdistr.rpt_id=' + cast(@ReportId as varchar(15)) + ' and tdistr.rpt_type=' + cast(@ReportType as varchar(15)) + ' 
AND tcnt.user_id=' + cast(@UserId as varchar(15)) + '
) TBL
'



IF Len(@FilterExpression)>0
	BEGIN
		SET @SqlString = @SqlString + ' WHERE  (' + @FilterExpression + ')'
	END


IF Len(@SortExpression)>0
	SET @SqlString = @SqlString + ' ORDER BY ' + @SortExpression
ELSE
	SET @SqlString = @SqlString + ' ORDER BY log_id DESC '



create table #tmp(
	[serno] int IDENTITY(1,1) PRIMARY KEY,
	[log_id] numeric NOT NULL ,	
	[status] varchar(15) NULL,
	[duration] int NULL,
	[isfromcache] varchar(5) NULL,
	[message] varchar(256) NULL,
	[timestamp] datetime NULL,
	[contact_name] varchar(128) NULL,
	[contact_email] varchar(128) NULL
	
)


EXECUTE(@SqlString)
SET @i=0
SET @StartIndex=@StartIndex+1
OPEN temp_cursor
FETCH ABSOLUTE @StartIndex from temp_cursor INTO @log_id , @status , @duration, @isfromcache, @message , @timestamp  , @contact_name , @contact_email

WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
	BEGIN
		    INSERT INTO #tmp( log_id ,status , duration, isfromcache, message , timestamp  , contact_name , contact_email )
			VALUES(@log_id , @status , @duration, @isfromcache, @message , @timestamp  , @contact_name , @contact_email)
		
		    FETCH NEXT from temp_cursor INTO @log_id , @status , @duration, @isfromcache, @message , @timestamp  , @contact_name , @contact_email
		    SET @i = @i + 1
	END

-- WORKS FOR STATIC CURSORS ONLY !!
SET @TotalCount = @@CURSOR_ROWS

CLOSE temp_cursor
DEALLOCATE temp_cursor



SELECT 
	 log_id ,
	status , 
	duration, 
	isfromcache,
	message ,
	timestamp  , 
	contact_name , 
	contact_email 
	FROM #tmp
	order by serno


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




CREATE  PROCEDURE [dbo].[sproc_LoadSqlReport] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

DECLARE @CurStateId numeric
DECLARE @UndoCount tinyint
DECLARE @RedoCount tinyint

SET @CurStateId=(SELECT [id] FROM t_sql_reports_state WHERE rpt_id=@ReportId AND is_current=1)

IF @CurStateId IS NULL
	BEGIN
		SET @UndoCount=0
		SET @RedoCount=0
	END
ELSE
	BEGIN
		SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_sql_reports_state WHERE rpt_id=@ReportId AND  [id]<@CurStateId) , 0)
		SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_sql_reports_state WHERE rpt_id=@ReportId AND  [id]>@CurStateId) , 0)
	END

-- current state might been left, delete it
IF @UndoCount=0 AND @RedoCount=0
	DELETE FROM t_sql_reports_state WHERE rpt_id=@ReportId


-------------------------------------------------------------
-- temp table because sql server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp(rpt_id numeric, [Sql] text, [Xsl] text)

INSERT INTO #tmp(rpt_id , [Sql], [Xsl])
	SELECT rpt_id , [sql], [xsl] from t_sql_reports_state where is_current=1 and t_sql_reports_state.rpt_id=@ReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(rpt_id , [Sql], [Xsl])
		SELECT [id] , [sql], [xsl] from v_sql_reports where [id]=@ReportId

-------------------------------------------------------------

SELECT
v_sql_reports.parent_report_id as [ParentReportId], 
v_sql_reports.name as [Name] ,  description as [Description] , sharing_status as [SharingStatus],
max_subscriber_sharing_status as [MaxSubscriberSharingStatus],
is_selected as IsSelected,
tmp.[Sql],
tmp.[Xsl],
@UndoCount as UndoCount, @RedoCount as RedoCount
 from v_sql_reports 
inner join tusers on v_sql_reports.user_id=tusers.[id]
inner join tcompany on tusers.company_id=tcompany.[id]
inner join  #tmp tmp on tmp.rpt_id=v_sql_reports.[id]
where v_sql_reports.[id]=@ReportId and tusers.[id]=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_LoadSqlReportHeader] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

select [id]  , parent_report_id , owner_id , name , description , is_selected  ,sharing_status, max_subscriber_sharing_status , is_in_distribution
	from v_sql_reports where [id]=@ReportId  and user_id=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_LoadSqlReportHeaders] 
@UserId numeric
AS
set nocount on

select rpt.[id]  , parent_report_id , owner_id , owner.name as owner_name , rpt.name , description , is_selected , sharing_status, max_subscriber_sharing_status, is_in_distribution , rpt.[timestamp]
			from v_sql_reports rpt
			inner join tusers owner  on rpt.owner_id=owner.id
			where user_id=@UserId
			order by rpt.name

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_LoadSqlReportState]
@ReportId numeric,
@StateCode smallint,
@UndoCount tinyint OUT,
@RedoCount tinyint OUT
 AS
SET NOCOUNT ON

DECLARE @NewStateId numeric
DECLARE @CurrentStateId numeric

SELECT @CurrentStateId=[id] from t_sql_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId=NULL
	RAISERROR ('No current state found.', 16, 1)

IF @StateCode=0 --load current state
	SET @NewStateId=@CurrentStateId
ELSE IF @StateCode<0 -- it's Undo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_sql_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId ORDER BY [id] DESC)
ELSE -- it's Redo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_sql_reports_state WHERE rpt_id=@ReportId AND [id]>@CurrentStateId ORDER BY [id] ASC)

IF @NewStateId IS NULL
	RAISERROR ('Cannot find next state.', 16, 1)

UPDATE t_sql_reports_state 
	SET is_current=0
	WHERE [id]=@CurrentStateId

UPDATE t_sql_reports_state 
	SET is_current=1
	WHERE [id]=@NewStateId

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_sql_reports_state WHERE rpt_id=@ReportId AND [id]<@NewStateId) , 0)
SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_sql_reports_state WHERE rpt_id=@ReportId AND [id]>@NewStateId) , 0)

SELECT  [Sql] , [Xsl] FROM t_sql_reports_state WHERE [id]=@NewStateId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






CREATE  PROCEDURE [dbo].[sproc_LoadStorecheckReport] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

DECLARE @CurStateId numeric
DECLARE @UndoCount tinyint
DECLARE @RedoCount tinyint

SET @CurStateId=(SELECT [id] FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND is_current=1)

IF @CurStateId IS NULL
	BEGIN
		SET @UndoCount=0
		SET @RedoCount=0
	END
ELSE
	BEGIN
		SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND  [id]<@CurStateId) , 0)
		SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND  [id]>@CurStateId) , 0)
	END

-- current state might been left, delete it
IF @UndoCount=0 AND @RedoCount=0
	DELETE FROM t_storecheck_reports_state WHERE rpt_id=@ReportId


-------------------------------------------------------------
-- temp table because storecheck server 7 has bug , which prevents loading text in one query
-------------------------------------------------------------


CREATE TABLE #tmp([rpt_id] numeric, [products_xml] text, [products_logic] tinyint, days smallint, filter_xml varchar(4000), insel bit, inbsel bit, datasource tinyint)

INSERT INTO #tmp([rpt_id] , [products_xml] , [products_logic], days , filter_xml, insel, inbsel, datasource )
	SELECT [rpt_id] , [products_xml] , [products_logic], days , filter_xml, insel, inbsel, datasource  from t_storecheck_reports_state where is_current=1 and t_storecheck_reports_state.rpt_id=@ReportId

IF NOT EXISTS(SELECT * FROM #tmp)
	INSERT INTO #tmp(rpt_id , [products_xml] , [products_logic], days , filter_xml, insel, inbsel, datasource )
		SELECT [id] , [products_xml] , [products_logic], days , filter_xml, insel, inbsel, datasource from v_storecheck_reports where [id]=@ReportId

-------------------------------------------------------------

SELECT
v_storecheck_reports.parent_report_id as [ParentReportId], 
v_storecheck_reports.name as [Name] ,  description as [Description] , sharing_status as [SharingStatus],
max_subscriber_sharing_status as [MaxSubscriberSharingStatus],
is_selected as IsSelected,
tmp.products_xml as ProductsXml , 
tmp.products_logic as ProductsLogic, 
tmp.days as Days , 
tmp.filter_xml as FilterXml , 
cache_exists as CacheExists,
cache_timestamp as CacheTimestamp,
tmp.insel as InSelOnly, 
tmp.inbsel as InBSelOnly,
tmp.datasource as DataSource,
tcompany.src_db as OltpDatabase,
@UndoCount as UndoCount, @RedoCount as RedoCount
 from v_storecheck_reports 
inner join tusers on v_storecheck_reports.user_id=tusers.[id]
inner join tcompany on tusers.company_id=tcompany.[id]
inner join  #tmp tmp on tmp.rpt_id=v_storecheck_reports.[id]
where v_storecheck_reports.[id]=@ReportId and tusers.[id]=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_LoadStorecheckReportHeader] 
@UserId numeric,
@ReportId numeric
AS
set nocount on

select [id]  , parent_report_id , owner_id , name , description , is_selected  ,sharing_status, max_subscriber_sharing_status , is_in_distribution ,
	cache_timestamp
	from v_storecheck_reports where [id]=@ReportId  and user_id=@UserId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_LoadStorecheckReportHeaders] 
@UserId numeric
AS
set nocount on

select rpt.[id]  , parent_report_id , owner_id , owner.name as owner_name , rpt.name , description , is_selected , sharing_status, max_subscriber_sharing_status, is_in_distribution , rpt.[timestamp]
			from v_storecheck_reports rpt
			inner join tusers owner  on rpt.owner_id=owner.id
			where user_id=@UserId
			order by rpt.name

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_LoadStorecheckReportState]
@ReportId numeric,
@StateCode smallint,
@UndoCount tinyint OUT,
@RedoCount tinyint OUT
 AS
SET NOCOUNT ON

DECLARE @NewStateId numeric
DECLARE @CurrentStateId numeric

SELECT @CurrentStateId=[id] from t_storecheck_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId=NULL
	RAISERROR ('No current state found.', 16, 1)

IF @StateCode=0 --load current state
	SET @NewStateId=@CurrentStateId
ELSE IF @StateCode<0 -- it's Undo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId ORDER BY [id] DESC)
ELSE -- it's Redo
	SET @NewStateId=(SELECT TOP 1 [id]  FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND [id]>@CurrentStateId ORDER BY [id] ASC)

IF @NewStateId IS NULL
	RAISERROR ('Cannot find next state.', 16, 1)

UPDATE t_storecheck_reports_state 
	SET is_current=0
	WHERE [id]=@CurrentStateId

UPDATE t_storecheck_reports_state 
	SET is_current=1
	WHERE [id]=@NewStateId

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND [id]<@NewStateId) , 0)
SET @RedoCount=ISNULL( (SELECT COUNT(*) FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND [id]>@NewStateId) , 0)

SELECT  [products_xml] , [products_logic], days , filter_xml, insel, inbsel, datasource FROM t_storecheck_reports_state WHERE [id]=@NewStateId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadUserByAuthentication]
@Logon varchar(50),
@Password varchar(50),
@CompanyNameShort nvarchar(15)
 AS
SET NOCOUNT OFF


SELECT 
	tusers.[id], 
	--company_id, 
	logon as Logon, 
	pwd as Password, 
	pwd_timestamp as PasswordTimestamp, 
	tusers.name as Name,
	tusers.email as Email,
	tcompany.id as CompanyId,
	tcompany.short_name as CompanyNameShort,
	tcompany.name as CompanyNameLong,
	tcompany.olap_server as SchemaServer,
	tcompany.olap_db as SchemaDatabase,
	tcompany.olap_cube as SchemaName,
	tcompany.src_db as OltpDatabase,
	--user_email, 
	tusers.conn_address as ConnectionAddress, 
	tusers.session_id as SessionId, 
	--user_timestamp, 
	is_admin as IsAdmin,
	is_logged_in as IsLoggedIn,
	css_style as CssStyle
	from tusers inner join tcompany on tusers.company_id=tcompany.[id]
	where logon=@Logon and pwd=@Password and tcompany.short_name=@CompanyNameShort
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadUserByUserId]
@UserId numeric
 AS
SET NOCOUNT OFF


SELECT 
	tusers.[id] as UserId, 
	--company_id, 
	logon as Logon, 
	pwd as Password, 
	pwd_timestamp as PasswordTimestamp, 
	tusers.name as Name,
	tusers.email as Email,
	tcompany.id as CompanyId,
	tcompany.short_name as CompanyNameShort,
	tcompany.name as CompanyNameLong,
	tcompany.olap_server as SchemaServer,
	tcompany.olap_db as SchemaDatabase,
	tcompany.olap_cube as SchemaName,
	tcompany.src_db as OltpDatabase,
	--user_email, 
	tusers.conn_address as ConnectionAddress, 
	tusers.session_id as SessionId, 
	--user_timestamp, 
	is_admin as IsAdmin,
	is_logged_in as IsLoggedIn,
	css_style as CssStyle
	from tusers inner join tcompany on tusers.company_id=tcompany.[id]
	where tusers.[id]=@UserId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadUsers]
@CompanyId numeric
 AS
SET NOCOUNT OFF


SELECT 
	tusers.[id] as Id, 
	--company_id, 
	logon as Logon, 
	--pwd as Password, 
	tusers.name as Name,
	tusers.email as Email,
	tusers.is_admin as IsAdmin,
	short_name as CompanyNameShort,
	tcompany.name as CompanyNameLong,
	tcompany.olap_server as SchemaServer,
	tcompany.olap_db as SchemaDatabase,
	tcompany.olap_cube as SchemaName,
	tcompany.src_db as OltpDatabase
	--user_email, 
	--user_conn_address, 
	--user_session_id, 
	--user_timestamp, 
	--user_admin, 
	--user_logged_in
	from tusers inner join tcompany on tusers.company_id=tcompany.[id]
	where @CompanyId=0 OR (@CompanyId!=0 AND tcompany.[id]=@CompanyId)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_LoadUsersWithChildReports] 
@ParentReportId numeric,
@ParentReportType int
AS
set nocount on

DECLARE @OwnerId numeric
DECLARE @CompanyId numeric

SET @OwnerId=(select top 1 user_id from (
select user_id from t_olap_reports where [id]=@ParentReportId and  @ParentReportType=0
union all
select user_id from t_storecheck_reports where [id]=@ParentReportId and  @ParentReportType=1
union all
select user_id from t_sql_reports where [id]=@ParentReportId and  @ParentReportType=2
union all
select user_id from t_mdx_reports where [id]=@ParentReportId and  @ParentReportType=3
) tbl )

SET @CompanyId=(SELECT TOP 1 company_id FROM tusers WHERE id=@OwnerId)


 
SELECT tusers.[id] as user_id, tusers.[name] , isnull(tbl.[id],0) as report_id , isnull(tbl.sharing_status,0) as sharing_status , @ParentReportType as report_type
from tusers left outer join
(
select [id] , user_id , sharing_status  from v_olap_reports where @ParentReportType=0 and parent_report_id=@ParentReportId
union all
select [id] , user_id , sharing_status  from v_storecheck_reports where @ParentReportType=1 and parent_report_id=@ParentReportId
union all
select [id] , user_id , sharing_status  from v_sql_reports where @ParentReportType=2 and parent_report_id=@ParentReportId
union all
select [id] , user_id , sharing_status  from v_mdx_reports where @ParentReportType=3 and parent_report_id=@ParentReportId
) tbl
on tusers.[id]=tbl.user_id
where tusers.[id]!=@OwnerId and tusers.company_id=@CompanyId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_SaveMdxReport] 
@UserId numeric, 
@ReportId numeric, 
@Mdx text,
@Xsl text
AS

SET NOCOUNT ON

UPDATE t_mdx_reports
	SET 
	[mdx]=@Mdx,
	[xsl]=@Xsl,
	[timestamp]=GetDate()
	WHERE [id]=@ReportId and user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_SaveMdxReportHeader] 
@ReportId numeric, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit

AS

SET NOCOUNT ON

UPDATE t_mdx_reports
SET 
parent_report_id=@ParentReportId,
sharing_status=@SharingStatus, 
user_id=@UserId,
name=@Name, 
description=@Description, 
is_selected=@IsSelected
WHERE [id]=@ReportId and user_id=@UserId




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO






CREATE PROCEDURE [dbo].[sproc_SaveMdxReportState]
@ReportId numeric,
@MaxStateCount tinyint,
@UndoCount tinyint OUT,
@Mdx text,
@Xsl text
 AS
SET NOCOUNT ON

DECLARE @CurrentStateId numeric
DECLARE @ExecSql varchar(4000)

SELECT @CurrentStateId=[id]  FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId IS NOT NULL
	BEGIN
		-- remove states exceeding limit before current and after current
		SET @ExecSql='
		DELETE FROM t_mdx_reports_state WHERE  rpt_id=' + CAST(@ReportId as varchar(15)) + '
			AND
			[id] NOT IN (SELECT TOP ' + CAST(@MaxStateCount as varchar(15)) + ' [id] FROM t_mdx_reports_state WHERE rpt_id=' + CAST(@ReportId as varchar(15)) + ' AND [id]<= ' + CAST(@CurrentStateId as varchar(15)) + ' ORDER BY [id] DESC)
		'
		EXECUTE(@ExecSql)

		UPDATE t_mdx_reports_state
			SET is_current=0
			WHERE [id]=@CurrentStateId
	END

INSERT INTO t_mdx_reports_state(rpt_id , is_current, [mdx] , [xsl])
		values(@ReportId , 1, @Mdx, @Xsl)
	
SET @CurrentStateId=@@identity

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_mdx_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_SaveOlapReport] 
@UserId numeric, 
@ReportId numeric, 
@ReportXml text,
@OpenNodesXml text
AS

SET NOCOUNT ON

UPDATE t_olap_reports
	SET 
	data=@ReportXml,
	open_nodes=@OpenNodesXml,
	[timestamp]=GetDate()
	WHERE [id]=@ReportId and user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_SaveOlapReportHeader] 
@ReportId numeric, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@OpenNodesXml text,
@GraphType tinyint,
@GraphOptions int

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
graph_options=@GraphOptions
WHERE [id]=@ReportId and user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_SaveOlapReportState]
@ReportId numeric,
@MaxStateCount tinyint,
@UndoCount tinyint OUT,
@ReportXml text
 AS
SET NOCOUNT ON

DECLARE @CurrentStateId numeric
DECLARE @ExecSql varchar(4000)

SELECT @CurrentStateId=[id]  FROM t_olap_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId IS NOT NULL
	BEGIN
		-- remove states exceeding limit before current and after current
		SET @ExecSql='
		DELETE FROM t_olap_reports_state WHERE rpt_id=' + CAST(@ReportId as varchar(15)) + '
			AND
			[id] NOT IN (SELECT TOP ' + CAST(@MaxStateCount as varchar(15)) + ' [id] FROM t_olap_reports_state WHERE rpt_id=' + CAST(@ReportId as varchar(15)) + ' AND [id]<= ' + CAST(@CurrentStateId as varchar(15)) + ' ORDER BY [id] DESC)
		'
		EXECUTE(@ExecSql)

		UPDATE t_olap_reports_state
			SET is_current=0
			WHERE [id]=@CurrentStateId
	END

INSERT INTO t_olap_reports_state(rpt_id , is_current, data)
		values(@ReportId , 1, @ReportXml)
	
SET @CurrentStateId=@@identity

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_olap_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_SaveSqlReport] 
@UserId numeric, 
@ReportId numeric, 
@Sql text,
@Xsl text
AS

SET NOCOUNT ON

UPDATE t_sql_reports
	SET 
	[sql]=@Sql,
	[xsl]=@Xsl,
	[timestamp]=GetDate()
	WHERE [id]=@ReportId and user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_SaveSqlReportHeader] 
@ReportId numeric, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit

AS

SET NOCOUNT ON

UPDATE t_sql_reports
SET 
parent_report_id=@ParentReportId,
sharing_status=@SharingStatus, 
user_id=@UserId,
name=@Name, 
description=@Description, 
is_selected=@IsSelected
WHERE [id]=@ReportId and user_id=@UserId



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_SaveSqlReportState]
@ReportId numeric,
@MaxStateCount tinyint,
@UndoCount tinyint OUT,
@Sql text,
@Xsl text
 AS
SET NOCOUNT ON

DECLARE @CurrentStateId numeric
DECLARE @ExecSql varchar(4000)

SELECT @CurrentStateId=[id]  FROM t_sql_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId IS NOT NULL
	BEGIN
		-- remove states exceeding limit before current and after current
		SET @ExecSql='
		DELETE FROM t_sql_reports_state WHERE  rpt_id=' + CAST(@ReportId as varchar(15)) + '
			AND
			[id] NOT IN (SELECT TOP ' + CAST(@MaxStateCount as varchar(15)) + ' [id] FROM t_sql_reports_state WHERE rpt_id=' + CAST(@ReportId as varchar(15)) + ' AND [id]<= ' + CAST(@CurrentStateId as varchar(15)) + ' ORDER BY [id] DESC)
		'
		EXECUTE(@ExecSql)

		UPDATE t_sql_reports_state
			SET is_current=0
			WHERE [id]=@CurrentStateId
	END

INSERT INTO t_sql_reports_state(rpt_id , is_current, [sql] , [xsl])
		values(@ReportId , 1, @Sql, @Xsl)
	
SET @CurrentStateId=@@identity

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_sql_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_SaveStorecheckReport] 
@UserId numeric, 
@ReportId numeric, 
@ProductsXml text,
@ProductsLogic tinyint,
@Days smallint,
@FilterXml varchar(4000),
@CacheTimestamp datetime,
@InSelOnly bit,
@InBSelOnly bit,
@DataSource tinyint
AS

SET NOCOUNT ON

UPDATE t_storecheck_reports
	SET 
	products_xml=@ProductsXml,
	products_logic=@ProductsLogic,
	days=@Days,
	filter_xml=@FilterXml,
	cache_timestamp=@CacheTimestamp,
	insel=@InSelOnly,
	inbsel=@InBSelOnly,
	datasource=@DataSource,
	[timestamp]=GetDate()
	WHERE [id]=@ReportId and user_id=@UserId
	


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_SaveStorecheckReportHeader] 
@ReportId numeric, 
@ParentReportId numeric, 
@SharingStatus tinyint, 
@UserId numeric, 
@Name varchar(50), 
@Description varchar(255),
@IsSelected bit,
@CacheTimestamp datetime
AS

SET NOCOUNT ON

UPDATE t_storecheck_reports
SET 
parent_report_id=@ParentReportId,
sharing_status=@SharingStatus, 
user_id=@UserId,
name=@Name, 
description=@Description, 
is_selected=@IsSelected,
cache_timestamp=@CacheTimestamp
WHERE [id]=@ReportId and user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO







CREATE PROCEDURE [dbo].[sproc_SaveStorecheckReportState]
@ReportId numeric,
@MaxStateCount tinyint,
@UndoCount tinyint OUT,
@ProductsXml text,
@ProductsLogic tinyint,
@Days smallint,
@FilterXml varchar(4000),
@InSelOnly bit,
@InBSelOnly bit,
@DataSource tinyint
 AS
SET NOCOUNT ON

DECLARE @CurrentStateId numeric
DECLARE @ExecSql varchar(4000)

SELECT @CurrentStateId=[id]  FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND is_current=1

IF @CurrentStateId IS NOT NULL
	BEGIN
		-- remove states exceeding limit before current and after current
		SET @ExecSql='
		DELETE FROM t_storecheck_reports_state WHERE  rpt_id=' + CAST(@ReportId as varchar(15)) + '
			AND
			[id] NOT IN (SELECT TOP ' + CAST(@MaxStateCount as varchar(15)) + ' [id] FROM t_storecheck_reports_state WHERE rpt_id=' + CAST(@ReportId as varchar(15)) + ' AND [id]<= ' + CAST(@CurrentStateId as varchar(15)) + ' ORDER BY [id] DESC)
		'
		EXECUTE(@ExecSql)

		UPDATE t_storecheck_reports_state
			SET is_current=0
			WHERE [id]=@CurrentStateId
	END

INSERT INTO t_storecheck_reports_state(rpt_id , is_current, products_xml , products_logic , days , filter_xml, insel, inbsel, datasource)
		values(@ReportId , 1, @ProductsXml,@ProductsLogic ,@Days ,@FilterXml, @InSelOnly , @InBSelOnly, @DataSource)
	
SET @CurrentStateId=@@identity

SET @UndoCount=ISNULL( (SELECT COUNT(*) FROM t_storecheck_reports_state WHERE rpt_id=@ReportId AND [id]<@CurrentStateId) , 0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[sproc_UpdateContact] 
@ContactId numeric ,
@UserId numeric ,
@ContactName varchar(128) ,
@ContactEMail varchar(128),
@DistributionFormat varchar(50)
AS

SET NOCOUNT OFF  -- OFF because DAL need's number of rows affected

update tcontacts
	set name=@ContactName ,
	email=@ContactEMail,
	distr_format=@DistributionFormat
	where [id]=@ContactId and user_id=@UserId


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




CREATE procedure [dbo].[sproc_UpdateDistribution]
@UserId numeric,
@ReportId numeric,
@DistributionId numeric ,
@ContactId numeric,
@ReportType tinyint,
@FrequencyType varchar(50),
@FrequencyValue varchar(512),
@Format tinyint
as

SET NOCOUNT OFF

IF 
(
(@ReportType=0 and exists(select * from v_olap_reports where [id]=@ReportId and user_id=@UserId))
OR (@ReportType=1 and exists(select * from v_storecheck_reports where [id]=@ReportId and user_id=@UserId))
OR (@ReportType=2 and exists(select * from v_sql_reports where [id]=@ReportId and user_id=@UserId))
OR (@ReportType=3 and exists(select * from v_mdx_reports where [id]=@ReportId and user_id=@UserId))
)
AND
(
exists(select * from tcontacts where [id]=@ContactId and user_id=@UserId)
)
	begin
		update tdistribution
			set 
			contact_id=@ContactId , 
			rpt_id=@ReportId , 
			rpt_type=@ReportType , 
			freq_type=@FrequencyType , 
			freq_value=@FrequencyValue,
			[format]=@Format
			where [id]=@DistributionId
	end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO




CREATE PROCEDURE [dbo].[sproc_UpdateUser]
@Id numeric,
@Logon varchar(50),
@Password varchar(50),
@PasswordTimestamp datetime,
@Name nvarchar(50),
@Email nvarchar(50),
@IsAdmin bit,
@CssStyle tinyint
 AS
SET NOCOUNT OFF

IF EXISTS(SELECT * FROM tusers WHERE logon=@Logon AND id!=@Id AND company_id=(SELECT company_id FROM tusers WHERE id=@id))
	BEGIN
		RAISERROR('User with same logon already exists' ,16,1)
		RETURN
	END

UPDATE tusers
	SET
		logon=@Logon,
		pwd=@Password,
		pwd_timestamp=@PasswordTimestamp,
		name=@Name,
		email=@Email,
		is_admin=@IsAdmin,
		css_style=@CssStyle
	WHERE [id]=@Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






CREATE PROCEDURE [dbo].[sproc_UpdateUserSession]
@Id numeric,
@ConnectionAddress varchar(50),
@SessionId varchar(50),
@IsLoggedIn bit
 AS
SET NOCOUNT OFF

UPDATE tusers
	SET
		conn_address=@ConnectionAddress,
		session_id=@SessionId,
		is_logged_in=@IsLoggedIn,
		timestamp=GetDate()
	WHERE [id]=@Id

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
















if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadDistributionQueuePage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadDistributionQueuePage]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE [dbo].[sproc_LoadDistributionQueuePage]
@CompanyId decimal,
@StartIndex int,
@RecordCount int,
@FilterExpression varchar(8000),
@SortExpression varchar(8000),
@TotalCount int OUT
 AS
SET NOCOUNT ON


SET NOCOUNT ON


if object_id('tempdb..#tmp') is not null
drop table #tmp

DECLARE @SqlString varchar(8000)
DECLARE @log_id decimal
DECLARE @distribution_id decimal
DECLARE @user_name varchar(128)
DECLARE @status varchar(15)
DECLARE @duration int
DECLARE @message varchar(256)
DECLARE @timestamp datetime
DECLARE @contact_name varchar(128)
DECLARE @contact_email varchar(128)
DECLARE @rpt_type varchar(50)
DECLARE @rpt_name varchar(128)
DECLARE @rpt_description varchar(512)
DECLARE @i int


SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
SELECT * FROM 
(
SELECT   tlog.[id] as log_id , tlog.distribution_id, tusr.name as user_name, tlog.status , tlog.duration, tlog.message , tlog.[timestamp] , tcnt.[name] as contact_name , tcnt.[email] as contact_email , 
case
	when trpt.rpt_type=0 then ''Olap Report''
	when trpt.rpt_type=1 then ''Storecheck Report''
	when trpt.rpt_type=2 then ''Sql Report''
	when trpt.rpt_type=3 then ''Mdx Report''
end as rpt_type ,
trpt.name as rpt_name , trpt.description as rpt_description
FROM tdistribution_log tlog
INNER JOIN tdistribution tdistr on tlog.distribution_id=tdistr.[id]
INNER JOIN tcontacts tcnt on tdistr.contact_id=tcnt.[id]
INNER JOIN 
(
select [id] , 0 as rpt_type , name , description, user_id from v_olap_reports 
union
select [id] , 1 as rpt_type , name , description, user_id from v_storecheck_reports
union
select [id] , 2 as rpt_type , name , description, user_id from v_sql_reports
union
select [id] , 3 as rpt_type ,name , description, user_id from v_mdx_reports
) trpt on tdistr.rpt_id=trpt.[id] and tdistr.rpt_type=trpt.rpt_type
INNER JOIN tusers tusr on trpt.user_id=tusr.[id]
WHERE tusr.company_id=' + cast(isnull(@CompanyId,0) as varchar(15)) + '
) TBL
'



IF Len(@FilterExpression)>0
	BEGIN
		SET @SqlString = @SqlString + ' WHERE  (' + @FilterExpression + ')'
	END


IF Len(@SortExpression)>0
	SET @SqlString = @SqlString + ' ORDER BY ' + @SortExpression
ELSE
	SET @SqlString = @SqlString + ' ORDER BY id DESC '



create table #tmp(
	[serno] int IDENTITY(1,1) PRIMARY KEY,
	[log_id] decimal NOT NULL ,
	[distribution_id] decimal NOT NULL,
	[user_name] varchar(128) NULL,
	[status] varchar(15) NULL,
	[duration] int NULL,
	[message] varchar(256) NULL,
	[timestamp] datetime NULL,
	[contact_name] varchar(128) NULL,
	[contact_email] varchar(128) NULL,
	[rpt_type] varchar(50) NULL,
	[rpt_name] varchar(128) NULL,
	[rpt_description] varchar(512) NULL
	
)


EXECUTE(@SqlString)
SET @i=0
SET @StartIndex=@StartIndex+1
OPEN temp_cursor
FETCH ABSOLUTE @StartIndex from temp_cursor INTO @log_id , @distribution_id, @user_name, @status , @duration,  @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description

WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
	BEGIN
		    INSERT INTO #tmp( log_id, distribution_id, user_name, status , duration, message , timestamp  , contact_name , contact_email , rpt_type  , rpt_name , rpt_description )
			VALUES(@log_id , @distribution_id, @user_name, @status , @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description)
		
		    FETCH NEXT from temp_cursor INTO @log_id , @distribution_id, @user_name, @status , @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description
		    SET @i = @i + 1
	END

-- WORKS FOR STATIC CURSORS ONLY !!
SET @TotalCount = @@CURSOR_ROWS

CLOSE temp_cursor
DEALLOCATE temp_cursor



SELECT 
	log_id ,
	distribution_id,
	user_name,
	status , 
	duration, 
	message ,
	timestamp  , 
	contact_name , 
	contact_email ,
	rpt_type  , 
	rpt_name , 
	rpt_description
	FROM #tmp
	order by serno
GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO














if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sproc_LoadMasterDistributionQueuePage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sproc_LoadMasterDistributionQueuePage]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[sproc_LoadMasterDistributionQueuePage]
@StartIndex int,
@RecordCount int,
@FilterExpression varchar(8000),
@SortExpression varchar(8000),
@TotalCount int OUT
 AS
SET NOCOUNT ON


SET NOCOUNT ON


if object_id('tempdb..#tmp') is not null
drop table #tmp

DECLARE @SqlString varchar(8000)
DECLARE @log_id decimal
DECLARE @distribution_id decimal
DECLARE @user_name varchar(128)
DECLARE @company_name varchar(128)
DECLARE @status varchar(15)
DECLARE @duration int
DECLARE @message varchar(256)
DECLARE @timestamp datetime
DECLARE @contact_name varchar(128)
DECLARE @contact_email varchar(128)
DECLARE @rpt_type varchar(50)
DECLARE @rpt_name varchar(128)
DECLARE @rpt_description varchar(512)
DECLARE @i int


SET @SqlString =  'declare temp_cursor CURSOR STATIC FOR
SELECT * FROM 
(
SELECT   tlog.[id] as log_id , tlog.distribution_id, tcom.short_name as company_name, tusr.name as user_name, tlog.status , tlog.duration, tlog.message , tlog.[timestamp] , tcnt.[name] as contact_name , tcnt.[email] as contact_email , 
case
	when trpt.rpt_type=0 then ''Olap Report''
	when trpt.rpt_type=1 then ''Storecheck Report''
	when trpt.rpt_type=2 then ''Sql Report''
	when trpt.rpt_type=3 then ''Mdx Report''
end as rpt_type ,
trpt.name as rpt_name , trpt.description as rpt_description
FROM tdistribution_log tlog
INNER JOIN tdistribution tdistr on tlog.distribution_id=tdistr.[id]
INNER JOIN tcontacts tcnt on tdistr.contact_id=tcnt.[id]
INNER JOIN 
(
select [id] , 0 as rpt_type , name , description, user_id from v_olap_reports 
union
select [id] , 1 as rpt_type , name , description, user_id from v_storecheck_reports
union
select [id] , 2 as rpt_type , name , description, user_id from v_sql_reports
union
select [id] , 3 as rpt_type ,name , description, user_id from v_mdx_reports
) trpt on tdistr.rpt_id=trpt.[id] and tdistr.rpt_type=trpt.rpt_type
INNER JOIN tusers tusr on trpt.user_id=tusr.[id]
INNER JOIN tcompany tcom on tusr.company_id=tcom.[id]
) TBL
'



IF Len(@FilterExpression)>0
	BEGIN
		SET @SqlString = @SqlString + ' WHERE  (' + @FilterExpression + ')'
	END


IF Len(@SortExpression)>0
	SET @SqlString = @SqlString + ' ORDER BY ' + @SortExpression
ELSE
	SET @SqlString = @SqlString + ' ORDER BY id DESC '



create table #tmp(
	[serno] int IDENTITY(1,1) PRIMARY KEY,
	[log_id] decimal NOT NULL ,
	[distribution_id] decimal NOT NULL,
	[company_name] varchar(128) NULL,
	[user_name] varchar(128) NULL,
	[status] varchar(15) NULL,
	[duration] int NULL,
	[message] varchar(256) NULL,
	[timestamp] datetime NULL,
	[contact_name] varchar(128) NULL,
	[contact_email] varchar(128) NULL,
	[rpt_type] varchar(50) NULL,
	[rpt_name] varchar(128) NULL,
	[rpt_description] varchar(512) NULL
	
)


EXECUTE(@SqlString)
SET @i=0
SET @StartIndex=@StartIndex+1
OPEN temp_cursor
FETCH ABSOLUTE @StartIndex from temp_cursor INTO @log_id , @distribution_id, @company_name, @user_name, @status , @duration,  @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description

WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
	BEGIN
		    INSERT INTO #tmp( log_id, distribution_id, company_name, user_name, status , duration, message , timestamp  , contact_name , contact_email , rpt_type  , rpt_name , rpt_description )
			VALUES(@log_id , @distribution_id, @company_name, @user_name, @status , @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description)
		
		    FETCH NEXT from temp_cursor INTO @log_id , @distribution_id, @company_name, @user_name, @status , @duration, @message , @timestamp  , @contact_name , @contact_email , @rpt_type  , @rpt_name , @rpt_description
		    SET @i = @i + 1
	END

-- WORKS FOR STATIC CURSORS ONLY !!
SET @TotalCount = @@CURSOR_ROWS

CLOSE temp_cursor
DEALLOCATE temp_cursor



SELECT 
	log_id ,
	distribution_id,
	company_name,
	user_name,
	status , 
	duration, 
	message ,
	timestamp  , 
	contact_name , 
	contact_email ,
	rpt_type  , 
	rpt_name , 
	rpt_description
	FROM #tmp
	order by serno

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

