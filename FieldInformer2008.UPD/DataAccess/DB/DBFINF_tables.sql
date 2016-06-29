if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_mdx_reports_state_t_mdx_reports]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_mdx_reports_state] DROP CONSTRAINT FK_t_mdx_reports_state_t_mdx_reports
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_olap_reports_state_t_olap_reports]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_olap_reports_state] DROP CONSTRAINT FK_t_olap_reports_state_t_olap_reports
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_sql_reports_state_t_sql_reports]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_sql_reports_state] DROP CONSTRAINT FK_t_sql_reports_state_t_sql_reports
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_storecheck_reports_cache_t_storecheck_reports]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_storecheck_reports_cache] DROP CONSTRAINT FK_t_storecheck_reports_cache_t_storecheck_reports
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_storecheck_reports_state_t_storecheck_reports]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_storecheck_reports_state] DROP CONSTRAINT FK_t_storecheck_reports_state_t_storecheck_reports
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tusers_tcompany]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tusers] DROP CONSTRAINT FK_tusers_tcompany
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tdistribution_tcontacts]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tdistribution] DROP CONSTRAINT FK_tdistribution_tcontacts
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tdistribution_log_tdistribution]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tdistribution_log] DROP CONSTRAINT FK_tdistribution_log_tdistribution
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_mdx_reports_tusers]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_mdx_reports] DROP CONSTRAINT FK_t_mdx_reports_tusers
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_olap_reports_tusers]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_olap_reports] DROP CONSTRAINT FK_t_olap_reports_tusers
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_sql_reports_tusers]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_sql_reports] DROP CONSTRAINT FK_t_sql_reports_tusers
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_t_storecheck_reports_tusers]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[t_storecheck_reports] DROP CONSTRAINT FK_t_storecheck_reports_tusers
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tcontacts_tusers]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tcontacts] DROP CONSTRAINT FK_tcontacts_tusers
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_mdx_reports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_mdx_reports]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_mdx_reports_state]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_mdx_reports_state]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_olap_reports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_olap_reports]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_olap_reports_state]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_olap_reports_state]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_sql_reports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_sql_reports]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_sql_reports_state]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_sql_reports_state]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_storecheck_reports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_storecheck_reports]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_storecheck_reports_cache]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_storecheck_reports_cache]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_storecheck_reports_state]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[t_storecheck_reports_state]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[taudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[taudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tcompany]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tcompany]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tcontacts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tcontacts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tdistribution]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tdistribution]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tdistribution_log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tdistribution_log]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tusers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tusers]
GO

CREATE TABLE [dbo].[t_mdx_reports] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[parent_report_id] [decimal](18, 0) NOT NULL ,
	[sharing_status] [tinyint] NOT NULL ,
	[user_id] [decimal](18, 0) NOT NULL ,
	[name] [varchar] (50) NOT NULL ,
	[description] [varchar] (255) NOT NULL ,
	[is_selected] [bit] NOT NULL ,
	[mdx] [text] NOT NULL ,
	[xsl] [text] NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_mdx_reports_state] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[rpt_id] [decimal](18, 0) NOT NULL ,
	[is_current] [bit] NOT NULL ,
	[mdx] [text] NOT NULL ,
	[xsl] [text] NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_olap_reports] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[parent_report_id] [decimal](18, 0) NOT NULL ,
	[sharing_status] [tinyint] NOT NULL ,
	[user_id] [decimal](18, 0) NOT NULL ,
	[name] [varchar] (50) NOT NULL ,
	[description] [varchar] (255) NOT NULL ,
	[is_selected] [bit] NOT NULL ,
	[graph_type] [tinyint] NOT NULL ,
	[graph_options] [int] NOT NULL ,
	[data] [text] NOT NULL ,
	[open_nodes] [text] NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_olap_reports_state] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[rpt_id] [decimal](18, 0) NOT NULL ,
	[is_current] [bit] NOT NULL ,
	[data] [text] NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_sql_reports] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[parent_report_id] [decimal](18, 0) NOT NULL ,
	[sharing_status] [tinyint] NOT NULL ,
	[user_id] [decimal](18, 0) NOT NULL ,
	[name] [varchar] (50) NOT NULL ,
	[description] [varchar] (255) NOT NULL ,
	[is_selected] [bit] NOT NULL ,
	[sql] [text] NOT NULL ,
	[xsl] [text] NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_sql_reports_state] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[rpt_id] [decimal](18, 0) NOT NULL ,
	[is_current] [bit] NOT NULL ,
	[sql] [text] NOT NULL ,
	[xsl] [text] NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_storecheck_reports] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[parent_report_id] [decimal](18, 0) NOT NULL ,
	[sharing_status] [tinyint] NOT NULL ,
	[user_id] [decimal](18, 0) NOT NULL ,
	[name] [varchar] (50) NOT NULL ,
	[description] [varchar] (255) NOT NULL ,
	[is_selected] [bit] NOT NULL ,
	[products_xml] [text] NOT NULL ,
	[products_logic] [tinyint] NOT NULL ,
	[days] [smallint] NOT NULL ,
	[filter_xml] [varchar] (4000) NOT NULL ,
	[cache_timestamp] [datetime] NULL ,
	[timestamp] [datetime] NOT NULL ,
	[insel] [bit] NOT NULL ,
	[inbsel] [bit] NOT NULL ,
	[datasource] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_storecheck_reports_cache] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[rpt_id] [decimal](18, 0) NOT NULL ,
	[type] [tinyint] NOT NULL ,
	[COMSERNO] [varchar] (15) NULL ,
	[DELDATE] [varchar] (8) NULL ,
	[SALMSERN] [varchar] (15) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[t_storecheck_reports_state] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[rpt_id] [decimal](18, 0) NOT NULL ,
	[is_current] [bit] NOT NULL ,
	[products_xml] [text] NOT NULL ,
	[products_logic] [tinyint] NOT NULL ,
	[days] [smallint] NOT NULL ,
	[filter_xml] [varchar] (4000) NOT NULL ,
	[timestamp] [datetime] NOT NULL ,
	[insel] [bit] NOT NULL ,
	[inbsel] [bit] NOT NULL ,
	[datasource] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[taudit] (
	[conn_address] [varchar] (50) NOT NULL ,
	[session_id] [varchar] (50) NOT NULL ,
	[company_id] [decimal](18, 0) NOT NULL ,
	[user_id] [decimal](18, 0) NOT NULL ,
	[timestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tcompany] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[name] [nvarchar] (50) NULL ,
	[short_name] [nvarchar] (15) NULL ,
	[olap_server] [nvarchar] (50) NULL ,
	[olap_provider] [nvarchar] (50) NULL ,
	[olap_db] [nvarchar] (50) NULL ,
	[olap_cube] [nvarchar] (50) NULL ,
	[src_db] [nvarchar] (50) NULL ,
	[src_user] [nvarchar] (50) NULL ,
	[ping] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tcontacts] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[user_id] [decimal](18, 0) NOT NULL ,
	[name] [varchar] (128) NOT NULL ,
	[email] [varchar] (128) NOT NULL ,
	[distr_format] [varchar] (50) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tdistribution] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[contact_id] [decimal](18, 0) NOT NULL ,
	[rpt_id] [decimal](18, 0) NOT NULL ,
	[rpt_type] [tinyint] NOT NULL ,
	[freq_type] [varchar] (50) NOT NULL ,
	[freq_value] [varchar] (512) NOT NULL ,
	[format] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tdistribution_log] (
	[distribution_id] [decimal](18, 0) NOT NULL ,
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[status] [varchar] (15) NOT NULL ,
	[message] [varchar] (256) NOT NULL ,
	[timestamp] [datetime] NOT NULL ,
	[duration] [int] NULL ,
	[taskguid] [uniqueidentifier] NULL ,
	[isfromcache] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tusers] (
	[id] [decimal](18, 0) IDENTITY (1, 1) NOT NULL ,
	[company_id] [decimal](18, 0) NOT NULL ,
	[logon] [varchar] (50) NULL ,
	[pwd] [varchar] (50) NULL ,
	[pwd_timestamp] [datetime] NOT NULL ,
	[name] [varchar] (50) NULL ,
	[email] [varchar] (50) NULL ,
	[conn_address] [varchar] (50) NULL ,
	[session_id] [varchar] (50) NULL ,
	[timestamp] [datetime] NULL ,
	[is_admin] [bit] NULL ,
	[is_logged_in] [bit] NULL ,
	[css_style] [tinyint] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[t_mdx_reports] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_mdx_reports] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_mdx_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_mdx_reports_state] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_olap_reports] WITH NOCHECK ADD 
	CONSTRAINT [PK_treports] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_olap_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_olap_reports_state] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_sql_reports] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_sql_reports] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_sql_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_sql_reports_state] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_storecheck_reports] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_storecheck_reports] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_storecheck_reports_cache] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_storecheck_reports_cache] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_storecheck_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [PK_t_storecheck_reports_state] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tcompany] WITH NOCHECK ADD 
	CONSTRAINT [PK_tcompany] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tcontacts] WITH NOCHECK ADD 
	CONSTRAINT [PK_tcontacts] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tdistribution] WITH NOCHECK ADD 
	CONSTRAINT [PK_tdistribution] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tdistribution_log] WITH NOCHECK ADD 
	CONSTRAINT [PK_tdistribution_log] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tusers] WITH NOCHECK ADD 
	CONSTRAINT [PK__tusers__0EA330E9] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[t_mdx_reports] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_mdx_reports_rpt_parent_report_id] DEFAULT (0) FOR [parent_report_id],
	CONSTRAINT [DF_t_mdx_reports_rpt_sharing_status] DEFAULT (0) FOR [sharing_status],
	CONSTRAINT [DF_t_mdx_reports_rpt_name] DEFAULT ('') FOR [name],
	CONSTRAINT [DF_t_mdx_reports_rpt_description] DEFAULT ('') FOR [description],
	CONSTRAINT [DF_t_mdx_reports_rpt_open] DEFAULT (0) FOR [is_selected],
	CONSTRAINT [DF_t_mdx_reports_rpt_mdx] DEFAULT ('') FOR [mdx],
	CONSTRAINT [DF_t_mdx_reports_rpt_xsl] DEFAULT ('') FOR [xsl],
	CONSTRAINT [DF_t_mdx_reports_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_mdx_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_mdx_reports_state_rpt_data] DEFAULT ('') FOR [mdx],
	CONSTRAINT [DF_t_mdx_reports_state_xsl] DEFAULT ('') FOR [xsl],
	CONSTRAINT [DF_t_mdx_reports_state_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_olap_reports] WITH NOCHECK ADD 
	CONSTRAINT [DF_treports_rpt_parent_report_id] DEFAULT (0) FOR [parent_report_id],
	CONSTRAINT [DF_treports_rpt_sharing_status] DEFAULT (0) FOR [sharing_status],
	CONSTRAINT [DF_treports_rpt_name] DEFAULT ('') FOR [name],
	CONSTRAINT [DF_treports_rpt_description] DEFAULT ('') FOR [description],
	CONSTRAINT [DF_treports_rpt_open] DEFAULT (0) FOR [is_selected],
	CONSTRAINT [DF_treports_rpt_graph_type] DEFAULT (0) FOR [graph_type],
	CONSTRAINT [DF_treports_rpt_graph_category] DEFAULT (0) FOR [graph_options],
	CONSTRAINT [DF_treports_rpt_data] DEFAULT ('') FOR [data],
	CONSTRAINT [DF_t_olap_reports_open_nodes] DEFAULT ('') FOR [open_nodes],
	CONSTRAINT [DF_t_olap_reports_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_olap_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_olap_reports_state_rpt_data] DEFAULT ('') FOR [data],
	CONSTRAINT [DF_t_olap_reports_state_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_sql_reports] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_sql_reports_rpt_parent_report_id] DEFAULT (0) FOR [parent_report_id],
	CONSTRAINT [DF_t_sql_reports_rpt_sharing_status] DEFAULT (0) FOR [sharing_status],
	CONSTRAINT [DF_t_sql_reports_rpt_name] DEFAULT ('') FOR [name],
	CONSTRAINT [DF_t_sql_reports_rpt_description] DEFAULT ('') FOR [description],
	CONSTRAINT [DF_t_sql_reports_rpt_open] DEFAULT (0) FOR [is_selected],
	CONSTRAINT [DF_t_sql_reports_rpt_sql] DEFAULT ('') FOR [sql],
	CONSTRAINT [DF_t_sql_reports_rpt_xsl_path] DEFAULT ('') FOR [xsl],
	CONSTRAINT [DF_t_sql_reports_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_sql_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_sql_reports_state_rpt_data] DEFAULT ('') FOR [sql],
	CONSTRAINT [DF_t_sql_reports_state_xsl] DEFAULT ('') FOR [xsl],
	CONSTRAINT [DF_t_sql_reports_state_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_storecheck_reports] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_storecheck_reports_rpt_parent_report_id] DEFAULT (0) FOR [parent_report_id],
	CONSTRAINT [DF_t_storecheck_reports_rpt_sharing_status] DEFAULT (0) FOR [sharing_status],
	CONSTRAINT [DF_t_storecheck_reports_rpt_name] DEFAULT ('') FOR [name],
	CONSTRAINT [DF_t_storecheck_reports_rpt_description] DEFAULT ('') FOR [description],
	CONSTRAINT [DF_t_storecheck_reports_rpt_open] DEFAULT (0) FOR [is_selected],
	CONSTRAINT [DF_t_storecheck_reports_products_xml] DEFAULT ('') FOR [products_xml],
	CONSTRAINT [DF_t_storecheck_reports_products_logic] DEFAULT (0) FOR [products_logic],
	CONSTRAINT [DF_t_storecheck_reports_days] DEFAULT (0) FOR [days],
	CONSTRAINT [DF_t_storecheck_reports_filter] DEFAULT ('') FOR [filter_xml],
	CONSTRAINT [DF_t_storecheck_reports_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[t_storecheck_reports_state] WITH NOCHECK ADD 
	CONSTRAINT [DF_t_storecheck_reports_state_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[tcompany] WITH NOCHECK ADD 
	CONSTRAINT [DF_tcompany_ping] DEFAULT (1) FOR [ping]
GO

ALTER TABLE [dbo].[tusers] WITH NOCHECK ADD 
	CONSTRAINT [DF_tusers_pwd_timestamp] DEFAULT (GetDate()) FOR [pwd_timestamp]
GO

ALTER TABLE [dbo].[tcontacts] WITH NOCHECK ADD 
	CONSTRAINT [DF_tcontacts_contact_name] DEFAULT ('') FOR [name],
	CONSTRAINT [DF_tcontacts_contact_email] DEFAULT ('') FOR [email],
	CONSTRAINT [DF_tcontacts_distr_preference] DEFAULT ('') FOR [distr_format]
GO

ALTER TABLE [dbo].[tdistribution] WITH NOCHECK ADD 
	CONSTRAINT [DF_tdistribution_rpt_distr_frq_type] DEFAULT ('') FOR [freq_type],
	CONSTRAINT [DF_tdistribution_rpt_distr_freq_value] DEFAULT ('') FOR [freq_value]
GO

ALTER TABLE [dbo].[tdistribution_log] WITH NOCHECK ADD 
	CONSTRAINT [DF_tdistribution_log_timestamp] DEFAULT (getdate()) FOR [timestamp]
GO

ALTER TABLE [dbo].[tusers] WITH NOCHECK ADD 
	CONSTRAINT [DF_tusers_user_logon] DEFAULT ('') FOR [logon],
	CONSTRAINT [DF_tusers_user_pwd] DEFAULT ('') FOR [pwd],
	CONSTRAINT [DF_tusers_user_name] DEFAULT ('') FOR [name],
	CONSTRAINT [DF_tusers_user_email] DEFAULT ('') FOR [email],
	CONSTRAINT [DF_tusers_user_conn_address] DEFAULT ('') FOR [conn_address],
	CONSTRAINT [DF_tusers_user_session_id] DEFAULT ('') FOR [session_id],
	CONSTRAINT [DF__tusers__user_adm__75A278F5] DEFAULT (0) FOR [is_admin],
	CONSTRAINT [DF__tusers__user_log__76969D2E] DEFAULT (0) FOR [is_logged_in]
GO

 CREATE  INDEX [IX_t_mdx_reports] ON [dbo].[t_mdx_reports]([parent_report_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_mdx_reports_1] ON [dbo].[t_mdx_reports]([sharing_status]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_mdx_reports_2] ON [dbo].[t_mdx_reports]([user_id]) ON [PRIMARY]
GO

 CREATE  INDEX [t_mdx_reports_state_rpt_id] ON [dbo].[t_mdx_reports_state]([rpt_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_treports] ON [dbo].[t_olap_reports]([parent_report_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_treports_1] ON [dbo].[t_olap_reports]([sharing_status]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_treports_2] ON [dbo].[t_olap_reports]([user_id]) ON [PRIMARY]
GO

 CREATE  INDEX [t_olap_reports_state_rpt_id] ON [dbo].[t_olap_reports_state]([rpt_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_sql_reports] ON [dbo].[t_sql_reports]([parent_report_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_sql_reports_1] ON [dbo].[t_sql_reports]([sharing_status]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_sql_reports_2] ON [dbo].[t_sql_reports]([user_id]) ON [PRIMARY]
GO

 CREATE  INDEX [t_sql_reports_state_rpt_id] ON [dbo].[t_sql_reports_state]([rpt_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_storecheck_reports] ON [dbo].[t_storecheck_reports]([parent_report_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_storecheck_reports_1] ON [dbo].[t_storecheck_reports]([sharing_status]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_storecheck_reports_2] ON [dbo].[t_storecheck_reports]([user_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_t_storecheck_reports_cache] ON [dbo].[t_storecheck_reports_cache]([rpt_id], [COMSERNO], [type]) ON [PRIMARY]
GO

 CREATE  INDEX [t_storecheck_reports_state_rpt_id] ON [dbo].[t_storecheck_reports_state]([rpt_id]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_tdistribution_log_1] ON [dbo].[tdistribution_log]([distribution_id], [status]) ON [PRIMARY]
GO

 CREATE  INDEX [tusers_session_id_index] ON [dbo].[tusers]([session_id]) ON [PRIMARY]
GO

 CREATE  UNIQUE  INDEX [IX_tusers] ON [dbo].[tusers]([company_id], [logon]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[t_mdx_reports] ADD 
	CONSTRAINT [FK_t_mdx_reports_tusers] FOREIGN KEY 
	(
		[user_id]
	) REFERENCES [dbo].[tusers] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_mdx_reports_state] ADD 
	CONSTRAINT [FK_t_mdx_reports_state_t_mdx_reports] FOREIGN KEY 
	(
		[rpt_id]
	) REFERENCES [dbo].[t_mdx_reports] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_olap_reports] ADD 
	CONSTRAINT [FK_t_olap_reports_tusers] FOREIGN KEY 
	(
		[user_id]
	) REFERENCES [dbo].[tusers] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_olap_reports_state] ADD 
	CONSTRAINT [FK_t_olap_reports_state_t_olap_reports] FOREIGN KEY 
	(
		[rpt_id]
	) REFERENCES [dbo].[t_olap_reports] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_sql_reports] ADD 
	CONSTRAINT [FK_t_sql_reports_tusers] FOREIGN KEY 
	(
		[user_id]
	) REFERENCES [dbo].[tusers] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_sql_reports_state] ADD 
	CONSTRAINT [FK_t_sql_reports_state_t_sql_reports] FOREIGN KEY 
	(
		[rpt_id]
	) REFERENCES [dbo].[t_sql_reports] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_storecheck_reports] ADD 
	CONSTRAINT [FK_t_storecheck_reports_tusers] FOREIGN KEY 
	(
		[user_id]
	) REFERENCES [dbo].[tusers] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_storecheck_reports_cache] ADD 
	CONSTRAINT [FK_t_storecheck_reports_cache_t_storecheck_reports] FOREIGN KEY 
	(
		[rpt_id]
	) REFERENCES [dbo].[t_storecheck_reports] (
		[id]
	)
GO

ALTER TABLE [dbo].[t_storecheck_reports_state] ADD 
	CONSTRAINT [FK_t_storecheck_reports_state_t_storecheck_reports] FOREIGN KEY 
	(
		[rpt_id]
	) REFERENCES [dbo].[t_storecheck_reports] (
		[id]
	)
GO

ALTER TABLE [dbo].[tcontacts] ADD 
	CONSTRAINT [FK_tcontacts_tusers] FOREIGN KEY 
	(
		[user_id]
	) REFERENCES [dbo].[tusers] (
		[id]
	)
GO

ALTER TABLE [dbo].[tdistribution] ADD 
	CONSTRAINT [FK_tdistribution_tcontacts] FOREIGN KEY 
	(
		[contact_id]
	) REFERENCES [dbo].[tcontacts] (
		[id]
	)
GO

ALTER TABLE [dbo].[tdistribution_log] ADD 
	CONSTRAINT [FK_tdistribution_log_tdistribution] FOREIGN KEY 
	(
		[distribution_id]
	) REFERENCES [dbo].[tdistribution] (
		[id]
	)
GO

ALTER TABLE [dbo].[tusers] ADD 
	CONSTRAINT [FK_tusers_tcompany] FOREIGN KEY 
	(
		[company_id]
	) REFERENCES [dbo].[tcompany] (
		[id]
	)
GO

