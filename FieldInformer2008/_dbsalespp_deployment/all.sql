
/****** Object:  Table [spp].[OLAP_BASE_SELECTION]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_BASE_SELECTION]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_BASE_SELECTION]
GO
/****** Object:  Table [spp].[OLAP_CCH]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_CCH]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_CCH]
GO
/****** Object:  Table [spp].[OLAP_CHN]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_CHN]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_CHN]
GO
/****** Object:  Table [spp].[OLAP_CURRENCY]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_CURRENCY]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_CURRENCY]
GO
/****** Object:  Table [spp].[OLAP_DATE]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DATE]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DATE]
GO
/****** Object:  Table [spp].[OLAP_DATE_CORRUPT]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DATE_CORRUPT]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DATE_CORRUPT]
GO
/****** Object:  Table [spp].[OLAP_DELDISTR]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DELDISTR]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DELDISTR]
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_EXP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DELDISTR_EXP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DELDISTR_EXP]
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_NOTEXP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DELDISTR_NOTEXP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DELDISTR_NOTEXP]
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_TMP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DELDISTR_TMP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DELDISTR_TMP]
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_TMP2]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DELDISTR_TMP2]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DELDISTR_TMP2]
GO
/****** Object:  Table [spp].[OLAP_DPM]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DPM]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DPM]
GO
/****** Object:  Table [spp].[OLAP_DPM_EXP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DPM_EXP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DPM_EXP]
GO
/****** Object:  Table [spp].[OLAP_DPM_NOTEXP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DPM_NOTEXP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DPM_NOTEXP]
GO
/****** Object:  Table [spp].[OLAP_DPM_TMP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_DPM_TMP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_DPM_TMP]
GO
/****** Object:  Table [spp].[OLAP_FIXTURE]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_FIXTURE]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_FIXTURE]
GO
/****** Object:  Table [spp].[OLAP_LCOMPGR]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_LCOMPGR]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_LCOMPGR]
GO
/****** Object:  Table [spp].[OLAP_LCOMSEL]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_LCOMSEL]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_LCOMSEL]
GO
/****** Object:  Table [spp].[OLAP_LPROPGR]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_LPROPGR]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_LPROPGR]
GO
/****** Object:  Table [spp].[OLAP_LPROPROD]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_LPROPROD]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_LPROPROD]
GO
/****** Object:  Table [spp].[OLAP_MEASURES_HIERARCHY]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_MEASURES_HIERARCHY]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_MEASURES_HIERARCHY]
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_ORDDISTR]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_ORDDISTR]
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_EXP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_ORDDISTR_EXP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_ORDDISTR_EXP]
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_NOTEXP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_ORDDISTR_NOTEXP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_ORDDISTR_NOTEXP]
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_TMP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_ORDDISTR_TMP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_ORDDISTR_TMP]
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_TMP2]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_ORDDISTR_TMP2]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_ORDDISTR_TMP2]
GO
/****** Object:  Table [spp].[OLAP_PGROUPS]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_PGROUPS]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_PGROUPS]
GO
/****** Object:  Table [spp].[OLAP_PRODUCT]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_PRODUCT]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_PRODUCT]
GO
/****** Object:  Table [spp].[OLAP_SALESCALL]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_SALESCALL]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_SALESCALL]
GO
/****** Object:  Table [spp].[OLAP_SALESFORCE]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_SALESFORCE]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_SALESFORCE]
GO
/****** Object:  Table [spp].[OLAP_SALESFORCE_HIERARCHY]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_SALESFORCE_HIERARCHY]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_SALESFORCE_HIERARCHY]
GO
/****** Object:  Table [spp].[OLAP_SELDATE]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_SELDATE]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_SELDATE]
GO
/****** Object:  Table [spp].[OLAP_SELECTION]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_SELECTION]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_SELECTION]
GO
/****** Object:  Table [spp].[OLAP_STORE]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_STORE]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_STORE]
GO
/****** Object:  Table [spp].[OLAP_STORE_SALESFORCE_HIERARCHY]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_STORE_SALESFORCE_HIERARCHY]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_STORE_SALESFORCE_HIERARCHY]
GO
/****** Object:  Table [spp].[OLAP_STOREACT]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_STOREACT]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_STOREACT]
GO
/****** Object:  Table [spp].[OLAP_SURVEY]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_SURVEY]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_SURVEY]
GO
/****** Object:  Table [spp].[OLAP_TSELENTR]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_TSELENTR]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_TSELENTR]
GO
/****** Object:  Table [spp].[OLAP_TTARENTR_PRODGRP]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_TTARENTR_PRODGRP]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_TTARENTR_PRODGRP]
GO
/****** Object:  Table [spp].[OLAP_TTARENTR_PRODUCT]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_TTARENTR_PRODUCT]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_TTARENTR_PRODUCT]
GO
/****** Object:  Table [spp].[OLAP_WHOLESALER]    Script Date: 01/09/2008 18:01:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[OLAP_WHOLESALER]') AND type in (N'U'))
DROP TABLE [spp].[OLAP_WHOLESALER]
GO




/****** Object:  Table [spp].[OLAP_BASE_SELECTION]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_BASE_SELECTION](
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[SELDATE] [varchar](8) NOT NULL,
	[INSEL] [smallint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_CCH]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_CCH](
	[COMSERNCCH] [varchar](15) NOT NULL,
	[TGRNAME] [varchar](50) NULL DEFAULT ('Default Trade Group'),
	[CCHNAME] [varchar](50) NULL DEFAULT ('Undefined'),
PRIMARY KEY CLUSTERED 
(
	[COMSERNCCH] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_CHN]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_CHN](
	[COMSERNCHN] [varchar](15) NOT NULL,
	[CHNNAME] [varchar](50) NULL DEFAULT ('Undefined'),
PRIMARY KEY CLUSTERED 
(
	[COMSERNCHN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_CURRENCY]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_CURRENCY](
	[CURRENCY] [varchar](4) NOT NULL,
	[RATE_TO_EURO] [float] NOT NULL,
	[RATE_TO_DEFAULT] [float] NOT NULL,
	[DEFAULT] [varchar](4) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DATE]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DATE](
	[DATE] [varchar](8) NOT NULL,
	[YEAR] [varchar](4) NOT NULL,
	[QUARTER] [varchar](5) NOT NULL,
	[MONTH] [varchar](6) NOT NULL,
	[WEEK] [varchar](6) NOT NULL,
	[SALENUM] [varchar](6) NOT NULL,
	[SNAPSHOT_DATE] [varchar](8) NOT NULL,
	[YEAR_SNAPSHOT_DATE] [varchar](8) NULL,
	[WRKDAY] [tinyint] NULL DEFAULT ((1)),
	[WRKDAY_SUM] [numeric](18, 0) NULL DEFAULT ((1.0)),
	[WRKDAY_SERNO] [int] NOT NULL DEFAULT ((0)),
	[MSA_WRKDAY] [tinyint] NULL DEFAULT ((1)),
	[WEEKEND] [tinyint] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[DATE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DATE_CORRUPT]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DATE_CORRUPT](
	[DATE] [varchar](8) NULL,
	[YEAR] [varchar](4) NULL DEFAULT ('0000'),
	[QUARTER] [varchar](5) NULL DEFAULT ('00000'),
	[MONTH] [varchar](6) NULL DEFAULT ('000000'),
	[WEEK] [varchar](6) NULL DEFAULT ('000000'),
	[SALENUM] [varchar](6) NULL DEFAULT ('00000'),
	[SNAPSHOT_DATE] [varchar](8) NULL DEFAULT ('00000000'),
	[YEAR_SNAPSHOT_DATE] [varchar](8) NULL DEFAULT ('00000000'),
	[WRKDAY] [tinyint] NULL DEFAULT ((0)),
	[MSA_WRKDAY] [tinyint] NULL DEFAULT ((0)),
	[WEEKEND] [tinyint] NULL DEFAULT ((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DELDISTR]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DELDISTR](
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[DELDATE] [varchar](8) NULL,
	[INDISTR] [smallint] NULL,
	[INSELDISTR] [smallint] NULL,
	[INBSELDISTR] [smallint] NULL,
	[PRODEXPAND] [tinyint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_EXP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DELDISTR_EXP](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[DELDATE] [char](8) NOT NULL,
	[PREV_INDISTR_CUM_SUM] [smallint] NOT NULL,
	[PREV_INSEL_CUM_SUM] [smallint] NOT NULL,
	[PREV_INBSEL_CUM_SUM] [smallint] NOT NULL,
	[CUR_INDISTR_CUM_SUM] [smallint] NOT NULL,
	[CUR_INSEL_CUM_SUM] [smallint] NOT NULL,
	[CUR_INBSEL_CUM_SUM] [smallint] NOT NULL,
	[INDISTR_CUM] [smallint] NOT NULL,
	[INSELDISTR_CUM] [smallint] NOT NULL,
	[INBSELDISTR_CUM] [smallint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_NOTEXP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DELDISTR_NOTEXP](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[DELDATE] [char](8) NOT NULL,
	[INSEL_CUM] [smallint] NOT NULL,
	[INBSEL_CUM] [smallint] NOT NULL,
	[INDISTR_CUM] [smallint] NOT NULL,
	[INSELDISTR_CUM] [smallint] NOT NULL,
	[INBSELDISTR_CUM] [smallint] NOT NULL,
	[PRODEXPAND] [tinyint] NOT NULL,
	[PRODEXPAND_INTERSECT] [tinyint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_TMP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DELDISTR_TMP](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[DELDATE] [char](8) NOT NULL,
	[RANGESTART_DATE] [char](8) NOT NULL,
	[OUTOFSTOCK_DATE] [char](8) NOT NULL,
	[WRKDAYS_IN_RANGE] [int] NOT NULL,
	[DELDMONTH] [char](6) NOT NULL,
	[WRKDAY_SERNO] [int] NOT NULL,
	[DELEACTVOL] [float] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DELDISTR_TMP2]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DELDISTR_TMP2](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[DELDATE] [char](8) NOT NULL,
	[PREV_DELDATE] [char](8) NOT NULL,
	[INDISTR] [smallint] NOT NULL,
	[INBSEL] [bit] NOT NULL,
	[INSEL] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DPM]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DPM](
	[DPMHDATE] [varchar](15) NULL,
	[DPMHDSERN] [varchar](15) NULL,
	[SALMSERN] [varchar](15) NULL,
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[PRODEXPAND] [tinyint] NULL,
	[DPMEFACING_CUM] [float] NULL,
	[DPMECHAN_CUM] [float] NULL,
	[DPMECOVER_CUM] [smallint] NULL,
	[DPMESELCOVER_CUM] [smallint] NULL,
	[DPMEBSELCOVER_CUM] [smallint] NULL,
	[DPMESALESP_CUM] [float] NULL,
	[DPMEAVESTP_CUM] [float] NULL,
	[DPMEPRICE_NET] [float] NULL,
	[DPMEPRICE_GROSS] [float] NULL,
	[DPMMEASURED] [tinyint] NULL,
	[DPMBSELMEASURED] [smallint] NULL,
	[DPMESALESP] [float] NULL,
	[DPMCOUNT] [tinyint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DPM_EXP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DPM_EXP](
	[comserno] [char](15) NOT NULL,
	[prodsern] [char](15) NOT NULL,
	[dpmhdate] [char](8) NOT NULL,
	[prev_dpmecover_cum_sum] [smallint] NOT NULL,
	[prev_dpmeinsel_cum_sum] [smallint] NOT NULL,
	[prev_dpmeinbsel_cum_sum] [smallint] NOT NULL,
	[cur_dpmecover_cum_sum] [smallint] NOT NULL,
	[cur_dpmeinsel_cum_sum] [smallint] NOT NULL,
	[cur_dpmeinbsel_cum_sum] [smallint] NOT NULL,
	[dpmecover_cum] [smallint] NOT NULL,
	[dpmeselcover_cum] [smallint] NOT NULL,
	[dpmebselcover_cum] [smallint] NOT NULL,
	[dpmmeasured_cum] [smallint] NOT NULL,
	[dpmbselmeasured_cum] [smallint] NOT NULL,
	[dpmefacing_cum] [real] NOT NULL,
	[dpmechan_cum] [real] NOT NULL,
	[dpmesalesp_cum] [real] NOT NULL,
	[dpmeavestp_cum] [real] NOT NULL,
	[dpmeprice_net] [real] NOT NULL,
	[dpmeprice_gross] [real] NOT NULL,
	[dpmcount] [tinyint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DPM_NOTEXP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DPM_NOTEXP](
	[comserno] [char](15) NOT NULL,
	[prodsern] [char](15) NOT NULL,
	[dpmhdate] [char](8) NOT NULL,
	[dpmeinsel_cum] [smallint] NOT NULL,
	[dpmeinbsel_cum] [smallint] NOT NULL,
	[dpmefacing_cum] [real] NOT NULL,
	[dpmechan_cum] [real] NOT NULL,
	[dpmesalesp_cum] [real] NOT NULL,
	[dpmeavestp_cum] [real] NOT NULL,
	[dpmeprice_net] [real] NOT NULL,
	[dpmeprice_gross] [real] NOT NULL,
	[dpmecover_cum] [smallint] NOT NULL,
	[dpmeselcover_cum] [smallint] NOT NULL,
	[dpmebselcover_cum] [smallint] NOT NULL,
	[dpmmeasured_cum] [smallint] NOT NULL,
	[dpmbselmeasured_cum] [smallint] NOT NULL,
	[dpmcount] [tinyint] NOT NULL,
	[prodexpand] [tinyint] NOT NULL,
	[prodexpand_intersect] [tinyint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_DPM_TMP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_DPM_TMP](
	[comserno] [char](15) NOT NULL,
	[prodsern] [char](15) NOT NULL,
	[dpmhdate] [char](8) NOT NULL,
	[dpmecover] [bit] NOT NULL,
	[dpmefacing] [real] NOT NULL,
	[dpmechan] [real] NOT NULL,
	[dpmesalesp] [real] NOT NULL,
	[dpmeavestp] [real] NOT NULL,
	[dpmeprice_net] [real] NOT NULL,
	[dpmeprice_gross] [real] NOT NULL,
	[dpmeinsel] [bit] NOT NULL,
	[dpmeinbsel] [bit] NOT NULL,
	[sel_placeholder] [bit] NOT NULL,
	[prev_dpmhdate] [char](8) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_FIXTURE]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_FIXTURE](
	[FXTRSERN] [varchar](15) NOT NULL,
	[FXTRNAME] [varchar](30) NOT NULL DEFAULT ('Undefined'),
	[PARENT_FXTRNAME] [varchar](30) NOT NULL DEFAULT ('Undefined'),
PRIMARY KEY CLUSTERED 
(
	[FXTRSERN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_LCOMPGR]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_LCOMPGR](
	[COMSERNO] [varchar](15) NOT NULL,
	[PGRSERN] [varchar](15) NOT NULL,
	[SALMSERN] [varchar](15) NOT NULL,
	[DUMMY_LINK_HIERARCHY] [int] NOT NULL DEFAULT ((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_LCOMSEL]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_LCOMSEL](
	[SELSERN] [varchar](15) NULL,
	[COMSERNO] [varchar](15) NULL,
	[SALMSERN] [varchar](15) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_LPROPGR]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_LPROPGR](
	[PRODSERN] [varchar](15) NOT NULL,
	[PGRSERN] [varchar](15) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_LPROPROD]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_LPROPROD](
	[PARENT_PRODSERN] [varchar](15) NOT NULL,
	[PRODSERN] [varchar](15) NOT NULL,
	[PRODCPG_MULT] [real] NOT NULL,
	[PRODCASE_MULT] [real] NOT NULL,
	[CHILDCASE_MULT] [real] NOT NULL,
	[PRODPALLET_MULT] [real] NOT NULL,
	[PRODUNIT_MULT] [real] NOT NULL,
	[PRODMONEY_MULT] [real] NOT NULL,
	[PRODSIZE] [float] NOT NULL DEFAULT (1),
	[PRODCPS] [float] NOT NULL DEFAULT (1),
	[PRODTAX] [float] NOT NULL DEFAULT (0),
	[PRODPALLET] [float] NOT NULL DEFAULT (1),
	[PRODPRICE] [float] NOT NULL DEFAULT (0),
	[PRODCPWNET] [float] NOT NULL DEFAULT (0),
	[PRODCPWGR] [float] NOT NULL DEFAULT (0),
	[PRODCASEWGR] [float] NOT NULL DEFAULT (0),
	[PRODEXPAND] [tinyint] NOT NULL,
	[PRODEXPAND_INTERSECT] [tinyint] NOT NULL DEFAULT (0)
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_MEASURES_HIERARCHY]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_MEASURES_HIERARCHY](
	[DUMMY_KEY] [smallint] NOT NULL,
	[MEASURE_KEY] [int] IDENTITY(1,1) NOT NULL,
	[MEASURE_LVL1] [varchar](75) NOT NULL,
	[MEASURE_LVL2] [varchar](75) NOT NULL,
	[MEASURE_MEMBER] [varchar](75) NOT NULL,
	[MEASURE_DESCRIPTION] [varchar](255) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_ORDDISTR](
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[ORDDDATE] [varchar](8) NULL,
	[INDISTR] [smallint] NULL,
	[INSELDISTR] [smallint] NULL,
	[INBSELDISTR] [smallint] NULL,
	[PRODEXPAND] [tinyint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_EXP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_ORDDISTR_EXP](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[ORDDDATE] [char](8) NOT NULL,
	[PREV_INDISTR_CUM_SUM] [smallint] NOT NULL,
	[PREV_INSEL_CUM_SUM] [smallint] NOT NULL,
	[PREV_INBSEL_CUM_SUM] [smallint] NOT NULL,
	[CUR_INDISTR_CUM_SUM] [smallint] NOT NULL,
	[CUR_INSEL_CUM_SUM] [smallint] NOT NULL,
	[CUR_INBSEL_CUM_SUM] [smallint] NOT NULL,
	[INDISTR_CUM] [smallint] NOT NULL,
	[INSELDISTR_CUM] [smallint] NOT NULL,
	[INBSELDISTR_CUM] [smallint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_NOTEXP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_ORDDISTR_NOTEXP](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[ORDDDATE] [char](8) NOT NULL,
	[INSEL_CUM] [smallint] NOT NULL,
	[INBSEL_CUM] [smallint] NOT NULL,
	[INDISTR_CUM] [smallint] NOT NULL,
	[INSELDISTR_CUM] [smallint] NOT NULL,
	[INBSELDISTR_CUM] [smallint] NOT NULL,
	[PRODEXPAND] [tinyint] NOT NULL,
	[PRODEXPAND_INTERSECT] [tinyint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_TMP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_ORDDISTR_TMP](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[ORDDDATE] [char](8) NOT NULL,
	[RANGESTART_DATE] [char](8) NOT NULL,
	[OUTOFSTOCK_DATE] [char](8) NOT NULL,
	[WRKDAYS_IN_RANGE] [int] NOT NULL,
	[ORDDMONTH] [char](6) NOT NULL,
	[WRKDAY_SERNO] [int] NOT NULL,
	[ORDEVOL] [float] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_ORDDISTR_TMP2]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_ORDDISTR_TMP2](
	[COMSERNO] [char](15) NOT NULL,
	[PRODSERN] [char](15) NOT NULL,
	[ORDDDATE] [char](8) NOT NULL,
	[PREV_ORDDDATE] [char](8) NOT NULL,
	[INDISTR] [smallint] NOT NULL,
	[INBSEL] [bit] NOT NULL,
	[INSEL] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_PGROUPS]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_PGROUPS](
	[PGRSERN] [varchar](15) NOT NULL,
	[PGRPNAME] [varchar](50) NULL,
	[PGRPVAL] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[PGRSERN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_PRODUCT]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_PRODUCT](
	[PRODSERN] [varchar](15) NOT NULL,
	[NR] [smallint] IDENTITY(1,1) NOT NULL,
	[PRODNAME] [varchar](65) NULL DEFAULT ('Undefined'),
	[PRODSNAME] [varchar](15) NULL DEFAULT ('Undefined'),
	[PRODSIZE] [float] NULL DEFAULT ((1)),
	[PRODCPS] [float] NULL DEFAULT ((1)),
	[PRODTAX] [float] NULL DEFAULT ((0)),
	[PRODPALLET] [float] NULL DEFAULT ((1)),
	[PRODPRICE] [float] NULL DEFAULT ((0)),
	[PRODCPWNET] [float] NULL DEFAULT ((0)),
	[PRODCPWGR] [float] NULL DEFAULT ((0)),
	[PRODCASEWGR] [float] NULL DEFAULT ((0)),
	[PRODSUPPLIER] [varchar](35) NULL DEFAULT ('Undefined'),
	[GRP@#@Brand] [varchar](30) NULL DEFAULT ('Undefined'),
	[GRP@#@Category] [varchar](30) NULL DEFAULT ('Undefined'),
	[GRP@#@Salesforce] [varchar](30) NULL DEFAULT ('Undefined'),
	[GRP@#@SELECTION CLASS] [varchar](30) NULL DEFAULT ('Undefined'),
	[GRP@#@TukoClass] [varchar](30) NULL DEFAULT ('Undefined'),
PRIMARY KEY CLUSTERED 
(
	[PRODSERN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_SALESCALL]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_SALESCALL](
	[SALESCALL_KEY] [varchar](15) NOT NULL,
	[SALESCALL_MEM] [varchar](50) NOT NULL,
	[SALESCALL_LVL] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SALESCALL_KEY] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_SALESFORCE]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_SALESFORCE](
	[SALMSERN] [varchar](15) NOT NULL,
	[SALMNAME] [varchar](30) NOT NULL,
	[PGRSERN] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SALMSERN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_SALESFORCE_HIERARCHY]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_SALESFORCE_HIERARCHY](
	[SALMGRPSERN] [varchar](15) NOT NULL,
	[SALMSERN] [varchar](15) NOT NULL,
	[SALMNAME] [varchar](30) NOT NULL,
	[SALMGRP] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SALMGRPSERN] ASC,
	[SALMSERN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_SELDATE]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_SELDATE](
	[SELDATE] [varchar](8) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SELDATE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_SELECTION]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_SELECTION](
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[SELDATE] [varchar](8) NOT NULL,
	[INSEL] [smallint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_STORE]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_STORE](
	[COMSERNO] [varchar](15) NOT NULL,
	[NR] [int] IDENTITY(1,1) NOT NULL,
	[COMNAME] [varchar](50) NULL DEFAULT ('Undefined'),
	[COMPCODE] [varchar](10) NULL DEFAULT ('Undefined'),
	[COMPCITY] [varchar](30) NULL DEFAULT ('Undefined'),
	[COMPADDR] [varchar](40) NULL DEFAULT ('Undefined'),
	[COMTYNAME] [varchar](15) NULL DEFAULT ('Undefined'),
	[COMCLASS] [varchar](8) NULL DEFAULT ('Undef'),
	[COMTURNCLS] [varchar](4) NULL DEFAULT ('Undf'),
	[TGRNAME] [varchar](50) NULL DEFAULT ('Default Trade Group'),
	[CCHNAME] [varchar](50) NULL DEFAULT ('Undefined'),
	[CHNNAME] [varchar](50) NULL DEFAULT ('Undefined'),
	[COMCALL] [varchar](3) NULL DEFAULT ('No'),
	[COMINACT] [varchar](8) NULL DEFAULT ('99999999'),
	[GRP@#@COMTEXT1] [varchar](30) NULL DEFAULT ('Undefined'),
	[GRP@#@COMTEXT5] [varchar](30) NULL DEFAULT ('Undefined'),
PRIMARY KEY CLUSTERED 
(
	[COMSERNO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_STORE_SALESFORCE_HIERARCHY]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_STORE_SALESFORCE_HIERARCHY](
	[SALMSERN] [varchar](15) NOT NULL,
	[COMSERNO] [varchar](15) NOT NULL,
	[PGRSERN] [varchar](15) NOT NULL,
	[SALMNAME] [varchar](30) NOT NULL,
	[COMNAME] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SALMSERN] ASC,
	[COMSERNO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_STOREACT]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_STOREACT](
	[COMSERNO] [varchar](15) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_SURVEY]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_SURVEY](
	[SURVEY_KEY] [int] IDENTITY(1,1) NOT NULL,
	[SALMSERN] [varchar](15) NULL,
	[COMSERNO] [varchar](15) NULL,
	[SAMCHDATE] [varchar](8) NULL,
	[ANSWER] [varchar](40) NULL,
	[QUESTION] [varchar](60) NULL,
	[ANSWER_MEASURE] [real] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_TSELENTR]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_TSELENTR](
	[SELSERN] [varchar](15) NULL,
	[SELESERN] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[SELESTART] [varchar](8) NULL,
	[SELEEND] [varchar](8) NULL,
	[SELE_VALID_START] [varchar](8) NULL,
	[SELE_VALID_END] [varchar](8) NULL,
	[SELE_PURCH_PRICE_NET] [numeric](19, 4) NULL,
	[SELE_PURCH_PRICE_GROSS] [numeric](19, 4) NULL,
	[SELE_CONS_PRICE_NET] [numeric](19, 4) NULL,
	[SELE_CONS_PRICE_GROSS] [numeric](19, 4) NULL,
	[SELE_MARGIN] [numeric](19, 4) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_TTARENTR_PRODGRP]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_TTARENTR_PRODGRP](
	[TARGSERN] [varchar](15) NULL,
	[SALMSERN] [varchar](15) NULL,
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[DATE] [varchar](8) NULL,
	[TARGEVOLUM] [float] NULL,
	[TARGEMONEY] [float] NULL,
	[TARGENUM1] [float] NULL,
	[TARGENUM2] [float] NULL,
	[TARGENUM3] [float] NULL,
	[TARGENUM4] [float] NULL,
	[TARGMONEY1] [float] NULL,
	[TARGMONEY2] [float] NULL,
	[TARGMONEY3] [float] NULL,
	[TARGMONEY4] [float] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_TTARENTR_PRODUCT]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_TTARENTR_PRODUCT](
	[TARGSERN] [varchar](15) NULL,
	[SALMSERN] [varchar](15) NULL,
	[COMSERNO] [varchar](15) NULL,
	[PRODSERN] [varchar](15) NULL,
	[DATE] [varchar](8) NULL,
	[TARGEVOLUM] [float] NULL,
	[TARGEMONEY] [float] NULL,
	[TARGENUM1] [float] NULL,
	[TARGENUM2] [float] NULL,
	[TARGENUM3] [float] NULL,
	[TARGENUM4] [float] NULL,
	[TARGMONEY1] [float] NULL,
	[TARGMONEY2] [float] NULL,
	[TARGMONEY3] [float] NULL,
	[TARGMONEY4] [float] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [spp].[OLAP_WHOLESALER]    Script Date: 01/09/2008 18:01:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [spp].[OLAP_WHOLESALER](
	[COMSERNWHS] [varchar](15) NOT NULL,
	[COMNAME] [varchar](30) NULL DEFAULT ('Unknown'),
PRIMARY KEY CLUSTERED 
(
	[COMSERNWHS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

GO





/****** Object:  View [spp].[V_OLAP_BASE_SELECTION]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_BASE_SELECTION]'))
DROP VIEW [spp].[V_OLAP_BASE_SELECTION]
GO
/****** Object:  View [spp].[V_OLAP_CALENDAR]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_CALENDAR]'))
DROP VIEW [spp].[V_OLAP_CALENDAR]
GO
/****** Object:  View [spp].[V_OLAP_CROSSJOIN]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_CROSSJOIN]'))
DROP VIEW [spp].[V_OLAP_CROSSJOIN]
GO
/****** Object:  View [spp].[v_olap_date]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_olap_date]'))
DROP VIEW [spp].[v_olap_date]
GO
/****** Object:  View [spp].[V_OLAP_DEL_CHN_ORIG]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_DEL_CHN_ORIG]'))
DROP VIEW [spp].[V_OLAP_DEL_CHN_ORIG]
GO
/****** Object:  View [spp].[V_OLAP_DELDISTR]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_DELDISTR]'))
DROP VIEW [spp].[V_OLAP_DELDISTR]
GO
/****** Object:  View [spp].[V_OLAP_DELIVER]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_DELIVER]'))
DROP VIEW [spp].[V_OLAP_DELIVER]
GO
/****** Object:  View [spp].[V_OLAP_DELIVER_DISTINCT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_DELIVER_DISTINCT]'))
DROP VIEW [spp].[V_OLAP_DELIVER_DISTINCT]
GO
/****** Object:  View [spp].[V_OLAP_DELIVER_POS]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_DELIVER_POS]'))
DROP VIEW [spp].[V_OLAP_DELIVER_POS]
GO
/****** Object:  View [spp].[V_OLAP_DPM]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_DPM]'))
DROP VIEW [spp].[V_OLAP_DPM]
GO
/****** Object:  View [spp].[V_OLAP_LCOMSEL]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_LCOMSEL]'))
DROP VIEW [spp].[V_OLAP_LCOMSEL]
GO
/****** Object:  View [spp].[V_OLAP_LSALMPROD]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_LSALMPROD]'))
DROP VIEW [spp].[V_OLAP_LSALMPROD]
GO
/****** Object:  View [spp].[V_OLAP_MEASURES_HIERARCHY]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_MEASURES_HIERARCHY]'))
DROP VIEW [spp].[V_OLAP_MEASURES_HIERARCHY]
GO
/****** Object:  View [spp].[V_OLAP_MSA]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_MSA]'))
DROP VIEW [spp].[V_OLAP_MSA]
GO
/****** Object:  View [spp].[V_OLAP_MSA_ATTRIBUTES]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_MSA_ATTRIBUTES]'))
DROP VIEW [spp].[V_OLAP_MSA_ATTRIBUTES]
GO
/****** Object:  View [spp].[V_OLAP_MSA_DISTRIBUTED]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_MSA_DISTRIBUTED]'))
DROP VIEW [spp].[V_OLAP_MSA_DISTRIBUTED]
GO
/****** Object:  View [spp].[V_OLAP_MSADATETYPE]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_MSADATETYPE]'))
DROP VIEW [spp].[V_OLAP_MSADATETYPE]
GO
/****** Object:  View [spp].[V_OLAP_ORDDATETYPE]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDDATETYPE]'))
DROP VIEW [spp].[V_OLAP_ORDDATETYPE]
GO
/****** Object:  View [spp].[V_OLAP_ORDDIRDEL]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDDIRDEL]'))
DROP VIEW [spp].[V_OLAP_ORDDIRDEL]
GO
/****** Object:  View [spp].[V_OLAP_ORDDISTR]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDDISTR]'))
DROP VIEW [spp].[V_OLAP_ORDDISTR]
GO
/****** Object:  View [spp].[V_OLAP_ORDER]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDER]'))
DROP VIEW [spp].[V_OLAP_ORDER]
GO
/****** Object:  View [spp].[V_OLAP_ORDER_ATTRIBUTES]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDER_ATTRIBUTES]'))
DROP VIEW [spp].[V_OLAP_ORDER_ATTRIBUTES]
GO
/****** Object:  View [spp].[V_OLAP_ORDERATTR_HIERARCHY]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDERATTR_HIERARCHY]'))
DROP VIEW [spp].[V_OLAP_ORDERATTR_HIERARCHY]
GO
/****** Object:  View [spp].[V_OLAP_ORDPRESAL]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDPRESAL]'))
DROP VIEW [spp].[V_OLAP_ORDPRESAL]
GO
/****** Object:  View [spp].[V_OLAP_ORDSENT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDSENT]'))
DROP VIEW [spp].[V_OLAP_ORDSENT]
GO
/****** Object:  View [spp].[V_OLAP_ORDSTAT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_ORDSTAT]'))
DROP VIEW [spp].[V_OLAP_ORDSTAT]
GO
/****** Object:  View [spp].[V_OLAP_PLANOGRAM]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PLANOGRAM]'))
DROP VIEW [spp].[V_OLAP_PLANOGRAM]
GO
/****** Object:  View [spp].[V_OLAP_PRICELIST_DIM]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PRICELIST_DIM]'))
DROP VIEW [spp].[V_OLAP_PRICELIST_DIM]
GO
/****** Object:  View [spp].[V_OLAP_PRICELIST_FACT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PRICELIST_FACT]'))
DROP VIEW [spp].[V_OLAP_PRICELIST_FACT]
GO
/****** Object:  View [spp].[V_OLAP_PRICELIST_SELRANGE]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PRICELIST_SELRANGE]'))
DROP VIEW [spp].[V_OLAP_PRICELIST_SELRANGE]
GO
/****** Object:  View [spp].[V_OLAP_PRODEXPAND]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PRODEXPAND]'))
DROP VIEW [spp].[V_OLAP_PRODEXPAND]
GO
/****** Object:  View [spp].[V_OLAP_PRODEXPAND_HIEARCHY]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PRODEXPAND_HIEARCHY]'))
DROP VIEW [spp].[V_OLAP_PRODEXPAND_HIEARCHY]
GO
/****** Object:  View [spp].[V_OLAP_PRODUCT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_PRODUCT]'))
DROP VIEW [spp].[V_OLAP_PRODUCT]
GO
/****** Object:  View [spp].[V_OLAP_SALESCALL_FACT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SALESCALL_FACT]'))
DROP VIEW [spp].[V_OLAP_SALESCALL_FACT]
GO
/****** Object:  View [spp].[V_OLAP_SALESCALL_SALUNCALL]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SALESCALL_SALUNCALL]'))
DROP VIEW [spp].[V_OLAP_SALESCALL_SALUNCALL]
GO
/****** Object:  View [spp].[V_OLAP_SALESCALL_SUM_FACT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SALESCALL_SUM_FACT]'))
DROP VIEW [spp].[V_OLAP_SALESCALL_SUM_FACT]
GO
/****** Object:  View [spp].[V_OLAP_SALMCAL]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SALMCAL]'))
DROP VIEW [spp].[V_OLAP_SALMCAL]
GO
/****** Object:  View [spp].[V_OLAP_SELECTION]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SELECTION]'))
DROP VIEW [spp].[V_OLAP_SELECTION]
GO
/****** Object:  View [spp].[V_OLAP_STORE_FACT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_STORE_FACT]'))
DROP VIEW [spp].[V_OLAP_STORE_FACT]
GO
/****** Object:  View [spp].[V_OLAP_STORE_SALESFORCE]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_STORE_SALESFORCE]'))
DROP VIEW [spp].[V_OLAP_STORE_SALESFORCE]
GO
/****** Object:  View [spp].[V_OLAP_STORE_SALESFORCE_HIERARCHY]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_STORE_SALESFORCE_HIERARCHY]'))
DROP VIEW [spp].[V_OLAP_STORE_SALESFORCE_HIERARCHY]
GO
/****** Object:  View [spp].[V_OLAP_SURVEY_DIM]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SURVEY_DIM]'))
DROP VIEW [spp].[V_OLAP_SURVEY_DIM]
GO
/****** Object:  View [spp].[V_OLAP_SURVEY_FACT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_SURVEY_FACT]'))
DROP VIEW [spp].[V_OLAP_SURVEY_FACT]
GO
/****** Object:  View [spp].[V_OLAP_TARGET_DIM]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_TARGET_DIM]'))
DROP VIEW [spp].[V_OLAP_TARGET_DIM]
GO
/****** Object:  View [spp].[V_OLAP_TTARENTR_PRODGRP]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_TTARENTR_PRODGRP]'))
DROP VIEW [spp].[V_OLAP_TTARENTR_PRODGRP]
GO
/****** Object:  View [spp].[V_OLAP_TTARENTR_PRODUCT]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[V_OLAP_TTARENTR_PRODUCT]'))
DROP VIEW [spp].[V_OLAP_TTARENTR_PRODUCT]
GO
/****** Object:  View [spp].[v_pgrprod]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_pgrprod]'))
DROP VIEW [spp].[v_pgrprod]
GO
/****** Object:  View [spp].[v_prodpgr]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_prodpgr]'))
DROP VIEW [spp].[v_prodpgr]
GO
/****** Object:  View [spp].[v_proprod]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_proprod]'))
DROP VIEW [spp].[v_proprod]
GO
/****** Object:  View [spp].[v_SalCom]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_SalCom]'))
DROP VIEW [spp].[v_SalCom]
GO
/****** Object:  View [spp].[v_select_product_groups]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_select_product_groups]'))
DROP VIEW [spp].[v_select_product_groups]
GO
/****** Object:  View [spp].[v_select_store_groups]    Script Date: 01/09/2008 17:59:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[spp].[v_select_store_groups]'))
DROP VIEW [spp].[v_select_store_groups]
Go



GO
/****** Object:  View [spp].[V_OLAP_BASE_SELECTION]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO












CREATE  VIEW [spp].[V_OLAP_BASE_SELECTION]
AS
SELECT 
OLAP_BASE_SELECTION.COMSERNO , 
OLAP_BASE_SELECTION.PRODSERN , 
OLAP_BASE_SELECTION.SELDATE , 
OLAP_BASE_SELECTION.INSEL ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=OLAP_BASE_SELECTION.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_BASE_SELECTION.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM spp.OLAP_BASE_SELECTION OLAP_BASE_SELECTION
















GO
/****** Object:  View [spp].[V_OLAP_CALENDAR]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_CALENDAR]
AS
select distinct spp.tcalndar.salmsern AS SALMSERN , salmname , caldate  AS CALDATE , 'Undefined' as CCHNAME  from spp.tcalndar inner join spp.tsalman on spp.tcalndar.salmsern=spp.tsalman.salmsern where calstatus1=2




















GO
/****** Object:  View [spp].[V_OLAP_CROSSJOIN]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO











CREATE VIEW [spp].[V_OLAP_CROSSJOIN]
AS
SELECT COMSERNO , PRODSERN , 

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=OLAP_STORE.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_PRODUCT.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM 
spp.OLAP_STORE OLAP_STORE
CROSS JOIN
spp.OLAP_PRODUCT OLAP_PRODUCT














GO
/****** Object:  View [spp].[v_olap_date]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [spp].[v_olap_date]
as
select *, cast(date as int) as date_num, LEFT(WEEK, 4) AS WEEKYEAR from spp.olap_date

GO
/****** Object:  View [spp].[V_OLAP_DEL_CHN_ORIG]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO







CREATE VIEW [spp].[V_OLAP_DEL_CHN_ORIG]
AS
SELECT DISTINCT ISNULL(DELTEXT1,'') AS CHN_ORIG FROM spp.TDELIVER









GO
/****** Object:  View [spp].[V_OLAP_DELDISTR]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO











CREATE VIEW [spp].[V_OLAP_DELDISTR]
AS

SELECT 
COMSERNO , 
PRODSERN ,
DELDATE ,
INDISTR_CUM AS INDISTR ,
INSELDISTR_CUM AS INSELDISTR ,
INBSELDISTR_CUM AS INBSELDISTR ,
PRODEXPAND ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TBL.COMSERNO AND OLAP_LPROPGR.PRODSERN=TBL.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM spp.OLAP_DELDISTR_NOTEXP TBL
WHERE PRODEXPAND IN (0,1)

UNION ALL

SELECT 
COMSERNO , 
PRODSERN ,
DELDATE ,
INDISTR_CUM AS INDISTR ,
INSELDISTR_CUM AS INSELDISTR ,
INBSELDISTR_CUM AS INBSELDISTR ,
2 AS PRODEXPAND ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TBL.COMSERNO AND OLAP_LPROPGR.PRODSERN=TBL.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM spp.OLAP_DELDISTR_EXP TBL















GO
/****** Object:  View [spp].[V_OLAP_DELIVER]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO








CREATE VIEW [spp].[V_OLAP_DELIVER]
AS
SELECT  
spp.OLAP_STORE.COMSERNO, 
spp.OLAP_STORE.NR, 
ISNULL(spp.TDELIVER.COMSERNCCH , '') AS COMSERNCCH, 
ISNULL(spp.TDELIVER.COMSERNCHN , '') AS COMSERNCHN, 
ISNULL(spp.TDELIVER.COMSERNWHS , '') AS COMSERNWHS, 
OLAP_LPROPROD.PRODSERN, 
OLAP_LPROPROD.PRODEXPAND ,
ISNULL(spp.TDELENTR.SALMSERN,'') AS SALMSERN ,
ISNULL(spp.TDELIVER.DELTEXT1,'') AS CHN_ORIG ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=spp.OLAP_STORE.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_LPROPROD.PRODSERN
),'0')
 AS STORE_SALMSERN ,


spp.TDELIVER.DELDATE,

	CASE DELEVOLFLG
		WHEN '1' THEN (CASE DELEPRFLAG
					WHEN '1'   THEN  1 								-- VOL IN CASES, PRICE IN CASES
					WHEN '2'   THEN (1/OLAP_LPROPROD.PRODPALLET)			-- VOL IN CASES , PRICE IN PALLETS
					ELSE 	(OLAP_LPROPROD.PRODSIZE) 					-- VOL IN CASES, PRICE IN CONS PKGS
					END)*spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT* spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT
		ELSE (CASE DELEPRFLAG
				WHEN '1'   THEN (1/OLAP_LPROPROD.PRODSIZE) 							-- VOL IN CONS PKG, PRICE IN CASES
				WHEN '2'   THEN 1/(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET)		-- VOL IN CONS PKG , PRICE IN PALLETS
				ELSE 	 1												-- VOL IN CONS PKG, PRICE IN CONS PKGS
				END)*spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT
	END AS DELEMONETARY , 

	-- price is per CONS PKG in product table
	CASE DELEVOLFLG
		WHEN '1' THEN OLAP_LPROPROD.PRODSIZE* OLAP_LPROPROD.PRODPRICE * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT*(OLAP_LPROPROD.PRODMONEY_MULT) 			-- VOL IN CASES, PRICE IN CONS PKG
		ELSE OLAP_LPROPROD.PRODPRICE * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT*(OLAP_LPROPROD.PRODMONEY_MULT)	-- VOL IN CONS PKG, PRICE IN CONS PKG
	END AS DELEMONETARY_PRODPRICE , 


	ISNULL((SELECT TOP 1 SELE_PURCH_PRICE_NET FROM spp.OLAP_TSELENTR t1 WHERE t1.PRODSERN=spp.TDELENTR.PRODSERN and  t1.SELESTART<=spp.TDELIVER.DELDATE and  t1.SELEEND>=spp.TDELIVER.DELDATE and EXISTS(SELECT TOP 1 1 FROM spp.OLAP_LCOMSEL t2 WHERE t1.SELSERN=t2.SELSERN AND t2.COMSERNO=spp.TDELIVER.COMSERNO) ),0)*
		(CASE DELEVOLFLG
		WHEN '1' THEN  OLAP_LPROPROD.PRODSIZE * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT 	-- VOL IN CASES, PRICE IN CONS PKGS
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT							-- VOL IN CONS PKG, PRICE IN CONS PKGS
		END)*OLAP_LPROPROD.PRODMONEY_MULT
	AS DELEMONETARY_PRICELIST , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL* OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCASE_MULT
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT
	END AS DELEACTVOL , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPS*OLAP_LPROPROD.PRODCASE_MULT
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPS*OLAP_LPROPROD.PRODCPG_MULT
	END AS DELEACTUNITS , 


	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPWNET*OLAP_LPROPROD.PRODCASE_MULT/1000 --kilograms
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPWNET*OLAP_LPROPROD.PRODCPG_MULT/1000 --kilograms
	END AS DELENETWT , 


	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPWGR*OLAP_LPROPROD.PRODCASE_MULT/1000 --kilograms
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPWGR*OLAP_LPROPROD.PRODCPG_MULT/1000 --kilograms
	END AS DELECPWGR , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASEWGR*OLAP_LPROPROD.PRODCASE_MULT/1000 --kilograms
		ELSE (spp.TDELENTR.DELEACTVOL/OLAP_LPROPROD.PRODSIZE)*OLAP_LPROPROD.PRODCASEWGR*OLAP_LPROPROD.PRODCPG_MULT/1000 --kilograms
	END AS DELECASEWGR , 
	
	
	-- cases are calculated from child product sum cases
	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.CHILDCASE_MULT
		ELSE (spp.TDELENTR.DELEACTVOL/OLAP_LPROPROD.PRODSIZE)*OLAP_LPROPROD.PRODCPG_MULT*OLAP_LPROPROD.CHILDCASE_MULT
	END AS DELEACTCASEVOL , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT/OLAP_LPROPROD.PRODPALLET
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT/(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET)
	END AS DELEACTPALLETVOL ,

	CASE DELEPRFLAG
		WHEN '1'   THEN (spp.TDELENTR.DELEPRICE/ OLAP_LPROPROD.PRODSIZE)*OLAP_LPROPROD.PRODMONEY_MULT		-- PRICE IN CASES
		WHEN '2'   THEN (spp.TDELENTR.DELEPRICE /(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET))*OLAP_LPROPROD.PRODMONEY_MULT		-- PRICE IN PALLETS
		ELSE 	spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT												-- PRICE IN CONS PKGS
	END AS DELEPRICE_NET,

	CASE DELEPRFLAG
		WHEN '1'   THEN spp.TDELENTR.DELEPRICE / OLAP_LPROPROD.PRODSIZE * ( 1 + OLAP_LPROPROD.PRODTAX/100)*OLAP_LPROPROD.PRODMONEY_MULT		-- PRICE IN CASES
		WHEN '2'   THEN spp.TDELENTR.DELEPRICE / (OLAP_LPROPROD.PRODSIZE * OLAP_LPROPROD.PRODPALLET )* ( 1 +OLAP_LPROPROD.PRODTAX/100)*OLAP_LPROPROD.PRODMONEY_MULT			-- PRICE IN PALLETS
		ELSE 	spp.TDELENTR.DELEPRICE* ( 1 + OLAP_LPROPROD.PRODTAX/100)*OLAP_LPROPROD.PRODMONEY_MULT			-- PRICE IN CONS PKGS
	END AS DELEPRICE_GROSS

FROM  spp.TDELIVER INNER JOIN
               spp.TDELENTR ON spp.TDELIVER.DELSERN = spp.TDELENTR.DELSERN
INNER JOIN spp.OLAP_STORE ON ISNULL(spp.TDELIVER.COMSERNO,'0')=spp.OLAP_STORE.COMSERNO
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON ISNULL(spp.TDELENTR.PRODSERN,'0')=OLAP_LPROPROD.PARENT_PRODSERN
WHERE spp.TDELIVER.COMSERNWHS='0' OR LEN(LTRIM(spp.TDELIVER.COMSERNWHS))=15  -- IN POS = 0
--AND OLAP_LPROPROD.PRODEXPAND IN (1,2)


GO
/****** Object:  View [spp].[V_OLAP_DELIVER_DISTINCT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO














CREATE  VIEW [spp].[V_OLAP_DELIVER_DISTINCT]
AS
SELECT  
spp.OLAP_STORE.COMSERNO, 
spp.OLAP_STORE.NR, 
ISNULL(spp.TDELIVER.COMSERNCCH , '') AS COMSERNCCH, 
ISNULL(spp.TDELIVER.COMSERNCHN , '') AS COMSERNCHN, 
ISNULL(spp.TDELIVER.COMSERNWHS , '') AS COMSERNWHS, 
OLAP_LPROPROD.PRODSERN, 
ISNULL(spp.TDELIVER.DELTEXT1,'') AS CHN_ORIG ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=spp.OLAP_STORE.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_LPROPROD.PRODSERN
),'0')
 AS STORE_SALMSERN ,

4 AS PRODEXPAND ,
ISNULL(spp.TDELENTR.SALMSERN,'') AS SALMSERN,
spp.TDELIVER.DELDATE
FROM  spp.TDELIVER INNER JOIN
               spp.TDELENTR ON spp.TDELIVER.DELSERN = spp.TDELENTR.DELSERN
INNER JOIN spp.OLAP_STORE ON spp.TDELIVER.COMSERNO=spp.OLAP_STORE.COMSERNO
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.TDELENTR.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN
WHERE LEN(LTRIM(spp.TDELIVER.COMSERNWHS))=15  -- IN POS = 0
AND OLAP_LPROPROD.PRODEXPAND IN ('0' , '1')

UNION ALL

SELECT  
spp.OLAP_STORE.COMSERNO, 
spp.OLAP_STORE.NR, 
ISNULL(spp.TDELIVER.COMSERNCCH , '') AS COMSERNCCH, 
ISNULL(spp.TDELIVER.COMSERNCHN , '') AS COMSERNCHN, 
ISNULL(spp.TDELIVER.COMSERNWHS , '') AS COMSERNWHS, 
OLAP_LPROPROD.PRODSERN, 
ISNULL(spp.TDELIVER.DELTEXT1,'') AS CHN_ORIG ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=spp.OLAP_STORE.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_LPROPROD.PRODSERN
),'0')
 AS STORE_SALMSERN ,

3 AS PRODEXPAND ,
ISNULL(spp.TDELENTR.SALMSERN,'') AS SALMSERN,
spp.TDELIVER.DELDATE
FROM  spp.TDELIVER INNER JOIN
               spp.TDELENTR ON spp.TDELIVER.DELSERN = spp.TDELENTR.DELSERN
INNER JOIN spp.OLAP_STORE ON spp.TDELIVER.COMSERNO=spp.OLAP_STORE.COMSERNO
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON ISNULL(spp.TDELENTR.PRODSERN,'0')=OLAP_LPROPROD.PARENT_PRODSERN
WHERE LEN(LTRIM(spp.TDELIVER.COMSERNWHS))=15  -- IN POS = 0
AND OLAP_LPROPROD.PRODEXPAND IN ('0' , '2')















GO
/****** Object:  View [spp].[V_OLAP_DELIVER_POS]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO












CREATE VIEW [spp].[V_OLAP_DELIVER_POS]
AS

SELECT  
spp.OLAP_STORE.COMSERNO,
ISNULL(spp.TDELIVER.COMSERNCCH , '') AS COMSERNCCH, 
ISNULL(spp.TDELIVER.COMSERNCHN , '') AS COMSERNCHN, 
ISNULL(spp.TDELIVER.COMSERNWHS , '') AS COMSERNWHS, 
OLAP_LPROPROD.PRODSERN, 

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=spp.OLAP_STORE.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_LPROPROD.PRODSERN
),'0')
 AS STORE_SALMSERN ,

OLAP_LPROPROD.PRODEXPAND ,
ISNULL(spp.TDELENTR.SALMSERN , '') AS SALMSERN , 
spp.TDELIVER.DELDATE,

	CASE DELEVOLFLG
		WHEN '1' THEN (CASE DELEPRFLAG
					WHEN '1'   THEN  1 								-- VOL IN CASES, PRICE IN CASES
					WHEN '2'   THEN (1/OLAP_LPROPROD.PRODPALLET)			-- VOL IN CASES , PRICE IN PALLETS
					ELSE 	(OLAP_LPROPROD.PRODSIZE) 					-- VOL IN CASES, PRICE IN CONS PKGS
					END)*spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT* spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT
		ELSE (CASE DELEPRFLAG
				WHEN '1'   THEN (1/OLAP_LPROPROD.PRODSIZE) 							-- VOL IN CONS PKG, PRICE IN CASES
				WHEN '2'   THEN 1/(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET)		-- VOL IN CONS PKG , PRICE IN PALLETS
				ELSE 	 1												-- VOL IN CONS PKG, PRICE IN CONS PKGS
				END)*spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT
	END AS DELEMONETARY_NET , 

	CASE DELEVOLFLG
		WHEN '1' THEN (CASE DELEPRFLAG
					WHEN '1'   THEN  1 								-- VOL IN CASES, PRICE IN CASES
					WHEN '2'   THEN (1/OLAP_LPROPROD.PRODPALLET)			-- VOL IN CASES , PRICE IN PALLETS
					ELSE 	(OLAP_LPROPROD.PRODSIZE) 					-- VOL IN CASES, PRICE IN CONS PKGS
					END)*spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT* spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT*( 1 + OLAP_LPROPROD.PRODTAX/100)
		ELSE (CASE DELEPRFLAG
				WHEN '1'   THEN (1/OLAP_LPROPROD.PRODSIZE) 							-- VOL IN CONS PKG, PRICE IN CASES
				WHEN '2'   THEN 1/(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET)		-- VOL IN CONS PKG , PRICE IN PALLETS
				ELSE 	 1												-- VOL IN CONS PKG, PRICE IN CONS PKGS
				END)*spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT*( 1 + OLAP_LPROPROD.PRODTAX/100)
	END AS DELEMONETARY_GROSS , 


	-- price is per CONS PKG in product table
	CASE DELEVOLFLG
		WHEN '1' THEN OLAP_LPROPROD.PRODSIZE* OLAP_LPROPROD.PRODPRICE * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT*(OLAP_LPROPROD.PRODMONEY_MULT) 			-- VOL IN CASES, PRICE IN CONS PKG
		ELSE OLAP_LPROPROD.PRODPRICE * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT*(OLAP_LPROPROD.PRODMONEY_MULT)	-- VOL IN CONS PKG, PRICE IN CONS PKG
	END AS DELEMONETARY_PRODPRICE , 


	ISNULL((SELECT TOP 1 SELE_PURCH_PRICE_NET FROM spp.OLAP_TSELENTR t1 WHERE t1.PRODSERN=spp.TDELENTR.PRODSERN and  t1.SELESTART<=spp.TDELIVER.DELDATE and  t1.SELEEND>=spp.TDELIVER.DELDATE and EXISTS(SELECT TOP 1 1 FROM spp.OLAP_LCOMSEL t2 WHERE t1.SELSERN=t2.SELSERN AND t2.COMSERNO=spp.TDELIVER.COMSERNO) ),0)*
		(CASE DELEVOLFLG
		WHEN '1' THEN  OLAP_LPROPROD.PRODSIZE * spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT 	-- VOL IN CASES, PRICE IN CONS PKGS
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT							-- VOL IN CONS PKG, PRICE IN CONS PKGS
		END)*OLAP_LPROPROD.PRODMONEY_MULT
	AS DELEMONETARY_PRICELIST , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL* OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCASE_MULT
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT
	END AS DELEACTVOL , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPS*OLAP_LPROPROD.PRODCASE_MULT
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPS*OLAP_LPROPROD.PRODCPG_MULT
	END AS DELEACTUNITS , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPWNET*OLAP_LPROPROD.PRODCASE_MULT/1000 --kilograms
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPWNET*OLAP_LPROPROD.PRODCPG_MULT/1000 --kilograms
	END AS DELENETWT , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPWGR*OLAP_LPROPROD.PRODCASE_MULT/1000 --kilograms
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPWGR*OLAP_LPROPROD.PRODCPG_MULT/1000 --kilograms
	END AS DELECPWGR , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASEWGR*OLAP_LPROPROD.PRODCASE_MULT/1000 --kilograms
		ELSE (spp.TDELENTR.DELEACTVOL/OLAP_LPROPROD.PRODSIZE)*OLAP_LPROPROD.PRODCASEWGR*OLAP_LPROPROD.PRODCPG_MULT/1000 --kilograms
	END AS DELECASEWGR , 
	
	-- cases are calculated from child product sum cases
	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.CHILDCASE_MULT
		ELSE (spp.TDELENTR.DELEACTVOL/OLAP_LPROPROD.PRODSIZE)*OLAP_LPROPROD.PRODCPG_MULT*OLAP_LPROPROD.CHILDCASE_MULT
	END AS DELEACTCASEVOL , 

	CASE DELEVOLFLG
		WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCASE_MULT/OLAP_LPROPROD.PRODPALLET
		ELSE spp.TDELENTR.DELEACTVOL*OLAP_LPROPROD.PRODCPG_MULT/(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET)
	END AS DELEACTPALLETVOL ,

	CASE DELEPRFLAG
		WHEN '1'   THEN (spp.TDELENTR.DELEPRICE/ OLAP_LPROPROD.PRODSIZE)*OLAP_LPROPROD.PRODMONEY_MULT		-- PRICE IN CASES
		WHEN '2'   THEN (spp.TDELENTR.DELEPRICE /(OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODPALLET))*OLAP_LPROPROD.PRODMONEY_MULT		-- PRICE IN PALLETS
		ELSE 	spp.TDELENTR.DELEPRICE*OLAP_LPROPROD.PRODMONEY_MULT												-- PRICE IN CONS PKGS
	END AS DELEPRICE_NET,

	CASE DELEPRFLAG
		WHEN '1'   THEN spp.TDELENTR.DELEPRICE / OLAP_LPROPROD.PRODSIZE * ( 1 + OLAP_LPROPROD.PRODTAX/100)*OLAP_LPROPROD.PRODMONEY_MULT		-- PRICE IN CASES
		WHEN '2'   THEN spp.TDELENTR.DELEPRICE / (OLAP_LPROPROD.PRODSIZE * OLAP_LPROPROD.PRODPALLET )* ( 1 +OLAP_LPROPROD.PRODTAX/100)*OLAP_LPROPROD.PRODMONEY_MULT			-- PRICE IN PALLETS
		ELSE 	spp.TDELENTR.DELEPRICE* ( 1 + OLAP_LPROPROD.PRODTAX/100)*OLAP_LPROPROD.PRODMONEY_MULT			-- PRICE IN CONS PKGS
	END AS DELEPRICE_GROSS

FROM  spp.TDELIVER INNER JOIN
               spp.TDELENTR ON spp.TDELIVER.DELSERN = spp.TDELENTR.DELSERN
INNER JOIN spp.OLAP_STORE ON  ISNULL(spp.TDELIVER.COMSERNO,'0')=spp.OLAP_STORE.COMSERNO
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON ISNULL(spp.TDELENTR.PRODSERN,'0')=OLAP_LPROPROD.PARENT_PRODSERN
WHERE LEN(LTRIM(ISNULL(spp.TDELIVER.COMSERNWHS,'')))=0


GO
/****** Object:  View [spp].[V_OLAP_DPM]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO










CREATE   VIEW [spp].[V_OLAP_DPM]
AS
SELECT 
DPMHDATE , 
OLAP_DPM.PRODSERN ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=OLAP_DPM.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_DPM.PRODSERN
),'0')
 AS STORE_SALMSERN ,

'0' AS SALMSERN , 
COMSERNO,
DPMEFACING_CUM,
DPMECHAN_CUM,
DPMECOVER_CUM,
DPMESELCOVER_CUM,
DPMEBSELCOVER_CUM,
DPMESALESP_CUM,
DPMEAVESTP_CUM,
DPMEPRICE_NET,
DPMEPRICE_GROSS,
DPMMEASURED_CUM AS DPMMEASURED,
DPMBSELMEASURED_CUM AS DPMBSELMEASURED,
PRODEXPAND ,
DPMCOUNT
FROM
spp.OLAP_DPM_NOTEXP OLAP_DPM
WHERE OLAP_DPM.PRODEXPAND IN (0,1)


UNION ALL


SELECT 
DPMHDATE , 
OLAP_DPM.PRODSERN ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=OLAP_DPM.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_DPM.PRODSERN
),'0')
 AS STORE_SALMSERN ,


'0' AS SALMSERN , 
COMSERNO,
DPMEFACING_CUM,
DPMECHAN_CUM,
DPMECOVER_CUM,
DPMESELCOVER_CUM,
DPMEBSELCOVER_CUM,
DPMESALESP_CUM,
DPMEAVESTP_CUM,
DPMEPRICE_NET,
DPMEPRICE_GROSS,
DPMMEASURED_CUM AS DPMMEASURED,
DPMBSELMEASURED_CUM AS DPMBSELMEASURED,
2 AS PRODEXPAND ,
DPMCOUNT
FROM
spp.OLAP_DPM_EXP OLAP_DPM
























GO
/****** Object:  View [spp].[V_OLAP_LCOMSEL]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [spp].[V_OLAP_LCOMSEL]
AS
SELECT SELSERN, COMSERNO FROM spp.OLAP_LCOMSEL

GO
/****** Object:  View [spp].[V_OLAP_LSALMPROD]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [spp].[V_OLAP_LSALMPROD]
AS

SELECT p.PRODSERN, p.NR, ISNULL(s.SALMSERN,0) AS SALMSERN
FROM spp.OLAP_SALESFORCE s 
inner join spp.LPROPGR l ON s.PGRSERN=l.PGRSERN
right outer JOIN spp.OLAP_PRODUCT p ON l.PRODSERN=p.PRODSERN
GO
/****** Object:  View [spp].[V_OLAP_MEASURES_HIERARCHY]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO












CREATE view [spp].[V_OLAP_MEASURES_HIERARCHY]
AS
SELECT * FROM spp.OLAP_MEASURES_HIERARCHY















GO
/****** Object:  View [spp].[V_OLAP_MSA]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [spp].[V_OLAP_MSA]
AS

SELECT
COMSERNO , 
ISNULL(COMSERNCCH , '') AS COMSERNCCH ,
ISNULL(COMSERNCHN , '') AS COMSERNCHN ,
ISNULL(COMSERNWHS , '') AS COMSERNWHS ,
ISNULL(SALMSERN,'') AS SALMSERN , 

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TBL.COMSERNO AND OLAP_LPROPGR.PRODSERN=TBL.PRODSERN
),'0')
 AS STORE_SALMSERN ,

PRODSERN , 
MSEPKGS  AS MSEPKGS ,
MSEMONEY AS MSEMONEY_GROSS ,
(MSEMONEY/(1+MSETAX/100)) AS MSEMONEY_NET ,
MSEMONEY-(MSEMONEY/(1+MSETAX/100)) AS MSETAX ,
MSA_DATE ,
MSADATE_KEY,
MSA_STATUS_KEY ,
PRODEXPAND
FROM 
(
SELECT 'MSACRIT_' + ISNULL(MSACRIT,'NULL') AS MSA_STATUS_KEY ,
CASE 
	WHEN  ISDATE(MSAPAID)=1 THEN MSAPAID
	WHEN  ISDATE(MSAPAY)=1 THEN MSAPAY
	ELSE MSASTART
END AS MSA_DATE ,
 'P' AS MSADATE_KEY,
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT  AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSASTAT_' + ISNULL(MSASTAT,'NULL') AS MSA_STATUS_KEY ,
CASE 
	WHEN  ISDATE(MSAPAID)=1 THEN MSAPAID
	WHEN  ISDATE(MSAPAY)=1 THEN MSAPAY
	ELSE MSASTART
END AS MSA_DATE ,
 'P' AS MSADATE_KEY,
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSETYPE_' + ISNULL(TMSAENTR.MSETYPE ,'NULL') AS MSA_STATUS_KEY ,
CASE 
	WHEN  ISDATE(MSAPAID)=1 THEN MSAPAID
	WHEN  ISDATE(MSAPAY)=1 THEN MSAPAY
	ELSE MSASTART
END AS MSA_DATE ,
 'P' AS MSADATE_KEY,
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSEALWTYPE_' + ISNULL(TMSAENTR.MSEALWTYPE ,'NULL') AS MSA_STATUS_KEY ,
CASE 
	WHEN  ISDATE(MSAPAID)=1 THEN MSAPAID
	WHEN  ISDATE(MSAPAY)=1 THEN MSAPAY
	ELSE MSASTART
END AS MSA_DATE ,
 'P' AS MSADATE_KEY,
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL


SELECT 'MSACRIT_' + ISNULL(MSACRIT,'NULL') AS MSA_STATUS_KEY ,
MSADATE AS MSA_DATE ,  'C' AS MSADATE_KEY,
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSASTAT_' + ISNULL(MSASTAT,'NULL') AS MSA_STATUS_KEY ,
MSADATE AS MSA_DATE ,  'C' AS MSADATE_KEY,
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSETYPE_' + ISNULL(TMSAENTR.MSETYPE ,'NULL') AS MSA_STATUS_KEY ,
MSADATE AS MSA_DATE ,  'C' AS MSADATE_KEY, 
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSEALWTYPE_' + ISNULL(TMSAENTR.MSEALWTYPE ,'NULL') AS MSA_STATUS_KEY ,
MSADATE AS MSA_DATE ,  'C' AS MSADATE_KEY, 
TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP  TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND   AND
ISDATE(MSADATE)=1

) TBL








GO
/****** Object:  View [spp].[V_OLAP_MSA_ATTRIBUTES]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_MSA_ATTRIBUTES]
AS
SELECT 'MSASTAT_0' AS MSA_STATUS_KEY, 'Proposal' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_1' AS MSA_STATUS_KEY, 'Agreed' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_2' AS MSA_STATUS_KEY, 'Alarm' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_3' AS MSA_STATUS_KEY, 'Confirmed By Salesman' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_4' AS MSA_STATUS_KEY, 'Rejected' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_5' AS MSA_STATUS_KEY, 'Confirmed For Payment' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_6' AS MSA_STATUS_KEY, 'Transported To Accounts Payable' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_7' AS MSA_STATUS_KEY, 'Sent To Bank/Post' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT 'MSASTAT_8' AS MSA_STATUS_KEY, 'Money Received' AS MSA_STATUS_MEM , 'Status' AS MSA_STATUS_LVL
UNION
SELECT DISTINCT 'MSASTAT_' + ISNULL(MSASTAT, 'NULL') AS MSA_STATUS_KEY ,
	 ISNULL(MSASTAT, 'NULL') + ' - Unknown' AS MSA_STATUS_MEM  , 'Status' AS MSA_STATUS_LVL
	FROM spp.TMSASLIP WHERE MSASTAT NOT IN ('0' , '1' , '2' , '3' , '4' , '5' , '6' , '7' , '8') OR MSASTAT IS NULL

UNION ALL


SELECT 'MSACRIT_1' AS MSA_STATUS_KEY, 'Order' AS MSA_STATUS_MEM , 'Payment Criterium' AS MSA_STATUS_LVL
UNION
SELECT 'MSACRIT_2' AS MSA_STATUS_KEY, 'Delivery' AS MSA_STATUS_MEM , 'Payment Criterium' AS MSA_STATUS_LVL
UNION
SELECT 'MSACRIT_3' AS MSA_STATUS_KEY, 'Scanner' AS MSA_STATUS_MEM , 'Payment Criterium' AS MSA_STATUS_LVL
UNION
SELECT DISTINCT 'MSACRIT_' + ISNULL(MSACRIT, 'NULL') AS MSA_STATUS_KEY ,
	 ISNULL(MSACRIT, 'NULL') + ' - Unknown' AS MSA_STATUS_MEM  , 'Payment Criterium' AS MSA_STATUS_LVL
	FROM spp.TMSASLIP WHERE MSACRIT NOT IN ('1' , '2' , '3') OR MSACRIT IS NULL


UNION ALL


SELECT 'MSETYPE_0' AS MSA_STATUS_KEY, 'Marketing Allowance ' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT 'MSETYPE_1' AS MSA_STATUS_KEY, 'Advertising Support ' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT 'MSETYPE_2' AS MSA_STATUS_KEY, 'Rotation ' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT 'MSETYPE_3' AS MSA_STATUS_KEY, 'Compensation Money ' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT 'MSETYPE_4' AS MSA_STATUS_KEY, 'Compensation Goods' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT 'MSETYPE_5' AS MSA_STATUS_KEY, 'Campaign' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT 'MSETYPE_6' AS MSA_STATUS_KEY, 'Contract' AS MSA_STATUS_MEM , 'Type' AS MSA_STATUS_LVL
UNION
SELECT DISTINCT 'MSETYPE_' + ISNULL(MSETYPE, 'NULL') AS MSA_STATUS_KEY ,
	 ISNULL(MSETYPE, 'NULL') + ' - Unknown' AS MSA_STATUS_MEM  , 'Type' AS MSA_STATUS_LVL
	FROM spp.TMSAENTR WHERE MSETYPE NOT IN ('0' , '1' , '2' , '3' , '4' , '5' , '6' ) OR MSETYPE IS NULL


UNION ALL


SELECT DISTINCT   'MSEALWTYPE_' + ISNULL(MSEALWTYPE, 'NULL') AS MSA_STATUS_KEY ,   ISNULL(MSEALWTYPE, 'NULL')  AS MSA_STATUS_MEM , 'Marketing Allowance Type ' AS MSA_STATUS_LVL
	FROM spp.TMSAENTR 






























GO
/****** Object:  View [spp].[V_OLAP_MSA_DISTRIBUTED]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO














CREATE VIEW [spp].[V_OLAP_MSA_DISTRIBUTED]
AS

SELECT
COMSERNO , 
ISNULL(SALMSERN,'') AS SALMSERN , 

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TMSASLIP.COMSERNO AND OLAP_LPROPGR.PRODSERN=TMSASLIP.PRODSERN
),'0')
 AS STORE_SALMSERN ,

ISNULL(COMSERNCCH , '') AS COMSERNCCH ,
ISNULL(COMSERNCHN , '') AS COMSERNCHN ,
ISNULL(COMSERNWHS , '') AS COMSERNWHS ,
PRODSERN , 
CAST(MSEPKGS/(MSA_WRKDAYS)as real) AS MSEPKGS ,
CAST(MSEMONEY/(MSA_WRKDAYS)as real) AS MSEMONEY_GROSS ,
CAST((MSEMONEY/(1+MSETAX/100))/(MSA_WRKDAYS)as real) AS MSEMONEY_NET ,
CAST((MSEMONEY-(MSEMONEY/(1+MSETAX/100)))/(MSA_WRKDAYS)as real) AS MSETAX ,
OLAP_DATE.DATE MSAVDATE,
MSADATE_KEY,
MSA_STATUS_KEY ,
PRODEXPAND
FROM 
( 
SELECT 'MSACRIT_' + ISNULL(MSACRIT,'NULL') AS MSA_STATUS_KEY,  'V' AS MSADATE_KEY  , (SELECT SUM(MSA_WRKDAY) FROM spp.OLAP_DATE WHERE DATE>=MSASTART AND DATE<=MSAEND) AS MSA_WRKDAYS ,  TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS  ,  OLAP_LPROPROD.PRODEXPAND   FROM spp.TMSASLIP   
TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE  ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND  
AND ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSASTAT_' + ISNULL(MSASTAT,'NULL') AS MSA_STATUS_KEY,  'V' AS MSADATE_KEY  , (SELECT SUM(MSA_WRKDAY) FROM spp.OLAP_DATE WHERE DATE>=MSASTART AND DATE<=MSAEND) AS MSA_WRKDAYS  ,  TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS ,  OLAP_LPROPROD.PRODEXPAND  FROM spp.TMSASLIP 
TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE  ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND  
AND ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSETYPE_' + ISNULL(TMSAENTR.MSETYPE,'NULL') AS MSA_STATUS_KEY,  'V' AS MSADATE_KEY  , (SELECT SUM(MSA_WRKDAY) FROM spp.OLAP_DATE WHERE DATE>=MSASTART AND DATE<=MSAEND) AS MSA_WRKDAYS  , TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS ,  OLAP_LPROPROD.PRODEXPAND   FROM spp.TMSASLIP   
TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE  ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND  
AND ISDATE(MSADATE)=1

UNION ALL

SELECT 'MSEALWTYPE_' + ISNULL(TMSAENTR.MSEALWTYPE,'NULL') AS MSA_STATUS_KEY,  'V' AS MSADATE_KEY  , (SELECT SUM(MSA_WRKDAY) FROM spp.OLAP_DATE WHERE DATE>=MSASTART AND DATE<=MSAEND) AS MSA_WRKDAYS  , TMSASLIP.* ,  TMSAENTR.MSEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS MSEMONEY ,  TMSAENTR.MSETAX*OLAP_LPROPROD.PRODMONEY_MULT  AS MSETAX ,  OLAP_LPROPROD.PRODSERN   ,  TMSAENTR.MSEPKGS*OLAP_LPROPROD.PRODCPG_MULT AS MSEPKGS ,  OLAP_LPROPROD.PRODEXPAND   FROM spp.TMSASLIP   
TMSASLIP
INNER JOIN spp.TMSAENTR TMSAENTR ON TMSASLIP.MSASERN=TMSAENTR.MSASERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON (CASE LEN(RTRIM(TMSAENTR.PRODSERN)) WHEN 15 THEN TMSAENTR.PRODSERN ELSE '0' END)  =OLAP_LPROPROD.PARENT_PRODSERN
WHERE  ISDATE(MSASTART)=1 AND ISDATE(MSAEND)=1 AND MSASTART<=MSAEND  
AND ISDATE(MSADATE)=1

)
TMSASLIP
INNER JOIN spp.OLAP_DATE OLAP_DATE ON (MSASTART<=OLAP_DATE.DATE AND MSAEND>=OLAP_DATE.DATE AND MSA_WRKDAY=1) 

















































GO
/****** Object:  View [spp].[V_OLAP_MSADATETYPE]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO













CREATE VIEW [spp].[V_OLAP_MSADATETYPE]
AS
SELECT 'C' AS MSADATE_KEY , 'Creation Date' AS MSADATE_TYPE
UNION
SELECT 'P' AS MSADATE_KEY , 'Payment Date' AS MSADATE_TYPE
UNION
SELECT 'V' AS MSADATE_KEY , 'Validity Date' AS MSADATE_TYPE

















GO
/****** Object:  View [spp].[V_OLAP_ORDDATETYPE]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO













CREATE VIEW [spp].[V_OLAP_ORDDATETYPE]
AS
SELECT 'C' AS ORDDATE_KEY , 'Creation Date' AS ORDDATE_TYPE
UNION
SELECT 'O' AS ORDDATE_KEY , 'Order Date' AS ORDDATE_TYPE
UNION
SELECT 'D' AS ORDDATE_KEY , 'Delivery Date' AS ORDDATE_TYPE














GO
/****** Object:  View [spp].[V_OLAP_ORDDIRDEL]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_ORDDIRDEL]
AS
SELECT '0' AS ORDDIRDEL, 'Not Direct Delivery' AS DESCRIPTION
UNION
SELECT '1' AS ORDDIRDEL, 'Direct Delivery' AS DESCRIPTION
UNION
SELECT DISTINCT ISNULL(ORDDIRDEL, 'NULL') , ISNULL(ORDDIRDEL, 'NULL') + ' - Unknown' AS DESCRIPTION FROM spp.TORDER WHERE ORDDIRDEL NOT IN ('0' , '1')



















GO
/****** Object:  View [spp].[V_OLAP_ORDDISTR]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO











CREATE VIEW [spp].[V_OLAP_ORDDISTR]
AS

SELECT 
COMSERNO , 
PRODSERN ,
ORDDDATE ,
INDISTR_CUM AS INDISTR ,
INSELDISTR_CUM AS INSELDISTR ,
INBSELDISTR_CUM AS INBSELDISTR ,
PRODEXPAND ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TBL.COMSERNO AND OLAP_LPROPGR.PRODSERN=TBL.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM spp.OLAP_ORDDISTR_NOTEXP TBL
WHERE PRODEXPAND IN (0,1)

UNION ALL

SELECT 
COMSERNO , 
PRODSERN ,
ORDDDATE ,
INDISTR_CUM AS INDISTR ,
INSELDISTR_CUM AS INSELDISTR ,
INBSELDISTR_CUM AS INBSELDISTR ,
2 AS PRODEXPAND ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TBL.COMSERNO AND OLAP_LPROPGR.PRODSERN=TBL.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM spp.OLAP_ORDDISTR_EXP TBL














GO
/****** Object:  View [spp].[V_OLAP_ORDER]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_ORDER]
AS
SELECT 
TORDER.COMSERNO, 
ISNULL(TORDER.SALMSERN,'') AS SALMSERN ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=TORDER.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_LPROPROD.PRODSERN
),'0')
 AS STORE_SALMSERN ,

ISNULL(TORDER.COMSERNCCH , '') AS COMSERNCCH, 
ISNULL(TORDER.COMSERNCHN , '') AS COMSERNCHN, 
ISNULL(TORDER.COMSERNWHS , '') AS COMSERNWHS, 
TORDER.ORDDATE, 
TORDER.ORDER_STATUS_KEY, 
OLAP_LPROPROD.PRODSERN, 
OLAP_LPROPROD.PRODEXPAND ,
/*
spp.TORDENTR.ORDEVOL ,
spp.TORDENTR.ORDEPRFLAG ,
spp.TORDENTR.ORDEPRICE ,
OLAP_LPROPROD.PRODSIZE ,
*/
spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT AS ORDEVOL_CASE, 
spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCASE_MULT AS ORDEVOL_CONSPKG, 
CASE ORDEPRFLAG
	WHEN 0 THEN spp.TORDENTR.ORDEPRICE*spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODMONEY_MULT  	/*price per cons pkg*/
	WHEN 1 THEN spp.TORDENTR.ORDEPRICE*spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.PRODMONEY_MULT				/*price per case*/
END AS MONETARY_WITHOUT_TAX ,
CASE ORDEPRFLAG
	WHEN 0 THEN spp.TORDENTR.ORDEACTPRG*spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODMONEY_MULT	/*price per cons pkg*/
	WHEN 1 THEN spp.TORDENTR.ORDEACTPRG*spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.PRODMONEY_MULT	/*price per case*/
END AS MONETARY_WITH_TAX ,
spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT*OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCPS  AS ORDEVOL_UNIT  ,
spp.TORDENTR.ORDEVOL*OLAP_LPROPROD.PRODCASE_MULT / OLAP_LPROPROD.PRODPALLET  AS ORDEVOL_PALLET ,
ORDDATETYPE
FROM  spp.TORDENTR INNER JOIN
(
SELECT 'ORDDIRDEL_' + ISNULL(ORDDIRDEL,'NULL') AS ORDER_STATUS_KEY , ORDCDATE AS ORDDATE, 'C' AS ORDDATETYPE  , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDSENT_' + ISNULL(ORDSENT,'NULL') AS ORDER_STATUS_KEY , ORDCDATE AS ORDDATE, 'C' AS ORDDATETYPE , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDSTAT_' + ISNULL(ORDSTAT,'NULL') AS ORDER_STATUS_KEY , ORDCDATE AS ORDDATE, 'C' AS ORDDATETYPE , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDPRESAL_' + ISNULL(ORDPRESAL,'NULL') AS ORDER_STATUS_KEY , ORDCDATE AS ORDDATE, 'C' AS ORDDATETYPE , *  FROM spp.TORDER

UNION ALL

SELECT 'ORDDIRDEL_' + ISNULL(ORDDIRDEL,'NULL') AS ORDER_STATUS_KEY , ORDODATE AS ORDDATE, 'O' AS ORDDATETYPE  , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDSENT_' + ISNULL(ORDSENT,'NULL') AS ORDER_STATUS_KEY , ORDODATE AS ORDDATE, 'O' AS ORDDATETYPE , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDSTAT_' + ISNULL(ORDSTAT,'NULL') AS ORDER_STATUS_KEY , ORDODATE AS ORDDATE, 'O' AS ORDDATETYPE , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDPRESAL_' + ISNULL(ORDPRESAL,'NULL') AS ORDER_STATUS_KEY , ORDODATE AS ORDDATE, 'O' AS ORDDATETYPE , *  FROM spp.TORDER

UNION ALL

SELECT 'ORDDIRDEL_' + ISNULL(ORDDIRDEL,'NULL') AS ORDER_STATUS_KEY , ORDDDATE AS ORDDATE, 'D' AS ORDDATETYPE  , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDSENT_' + ISNULL(ORDSENT,'NULL') AS ORDER_STATUS_KEY , ORDDDATE AS ORDDATE, 'D' AS ORDDATETYPE , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDSTAT_' + ISNULL(ORDSTAT,'NULL') AS ORDER_STATUS_KEY , ORDDDATE AS ORDDATE, 'D' AS ORDDATETYPE , *  FROM spp.TORDER
UNION ALL
SELECT 'ORDPRESAL_' + ISNULL(ORDPRESAL,'NULL') AS ORDER_STATUS_KEY , ORDDDATE AS ORDDATE, 'D' AS ORDDATETYPE , *  FROM spp.TORDER

)
TORDER
ON spp.TORDENTR.ORDSERN = TORDER.ORDSERN 
 INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.TORDENTR.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN


























GO
/****** Object:  View [spp].[V_OLAP_ORDER_ATTRIBUTES]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO















CREATE VIEW [spp].[V_OLAP_ORDER_ATTRIBUTES]
AS

SELECT 'ORDDIRDEL_0' AS ORDER_STATUS_KEY, 'Not Direct Delivery' AS ORDER_STATUS_MEM , 'Direct Delivery' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDDIRDEL_1' AS ORDER_STATUS_KEY, 'Direct Delivery' AS ORDER_STATUS_MEM , 'Direct Delivery' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDDIRDEL_' + ISNULL(ORDDIRDEL, 'NULL') AS ORDER_STATUS_KEY ,
	 ISNULL(ORDDIRDEL, 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Direct Delivery' AS ORDER_STATUS_LVL
	FROM spp.TORDER WHERE ORDDIRDEL NOT IN ('0' , '1')  OR ORDDIRDEL IS NULL

UNION

SELECT 'ORDPRESAL_0' AS ORDER_STATUS_KEY, 'Not Pre-Sales' AS ORDER_STATUS_MEM , 'Pre-Sales' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDPRESAL_1' AS ORDER_STATUS_KEY, 'Pre-Sales' AS ORDER_STATUS_MEM , 'Pre-Sales' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDPRESAL_' + ISNULL(ORDPRESAL , 'NULL') AS ORDER_STATUS_KEY, 
	ISNULL(ORDPRESAL, 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Pre-Sales' AS ORDER_STATUS_LVL
	FROM spp.TORDER WHERE ORDPRESAL NOT IN ('0' , '1')  OR ORDPRESAL IS NULL

UNION

SELECT 'ORDSENT_0' AS ORDER_STATUS_KEY, 'Not Sent' AS ORDER_STATUS_MEM , 'Sent' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDSENT_1' AS ORDER_STATUS_KEY, 'Sent' AS ORDER_STATUS_MEM , 'Sent' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDSENT_' + ISNULL(ORDSENT, 'NULL') AS ORDER_STATUS_KEY, 
	ISNULL(ORDSENT , 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Sent' AS ORDER_STATUS_LVL
	FROM spp.TORDER  WHERE ORDSENT NOT IN ('0' , '1')  OR ORDSENT IS NULL


UNION

SELECT 'ORDSTAT_0' AS ORDER_STATUS_KEY, 'Proposal' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDSTAT_1' AS ORDER_STATUS_KEY, 'Pending' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDSTAT_2' AS ORDER_STATUS_KEY, 'Closed By Retailer' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDSTAT_3' AS ORDER_STATUS_KEY, 'Closed By Salesman' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDSTAT_4' AS ORDER_STATUS_KEY, 'Cancelled' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDSTAT_' + ISNULL(ORDSTAT, 'NULL') AS ORDER_STATUS_KEY , 
	ISNULL(ORDSTAT, 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Status' AS ORDER_STATUS_LVL
	FROM spp.TORDER WHERE ORDSTAT NOT IN ('0' , '1' , '2' , '3' , '4')  OR ORDSTAT IS NULL






















GO
/****** Object:  View [spp].[V_OLAP_ORDERATTR_HIERARCHY]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE view [spp].[V_OLAP_ORDERATTR_HIERARCHY]
AS

SELECT '0' AS DUMMY_KEY , '[SrcOrder].[Orddirdel].&[0]' AS ORDER_STATUS_KEY, 'Not Direct Delivery' AS ORDER_STATUS_MEM , 'Direct Delivery' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDDIRDEL_1'  AS DUMMY_KEY ,  '[SrcOrder].[Orddirdel].&[1]' AS ORDER_STATUS_KEY, 'Direct Delivery' AS ORDER_STATUS_MEM , 'Direct Delivery' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT  'ORDDIRDEL_' + ISNULL(ORDDIRDEL, 'NULL') AS DUMMY_KEY,  '[SrcOrder].[Orddirdel].&[' + ISNULL(ORDDIRDEL, 'NULL') + ']'  AS ORDER_STATUS_KEY ,
	 ISNULL(ORDDIRDEL, 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Direct Delivery' AS ORDER_STATUS_LVL
	FROM spp.TORDER WHERE ORDDIRDEL NOT IN ('0' , '1')

UNION

SELECT 'ORDPRESAL_0' AS DUMMY_KEY , '[SrcOrder].[Ordpresal].&[0]' AS ORDER_STATUS_KEY, 'Not Pre-Sales' AS ORDER_STATUS_MEM , 'Pre-Sales' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDPRESAL_1' AS DUMMY_KEY , '[SrcOrder].[Ordpresal].&[1]' AS ORDER_STATUS_KEY, 'Pre-Sales' AS ORDER_STATUS_MEM , 'Pre-Sales' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDPRESAL_' + ISNULL(ORDPRESAL , 'NULL') AS DUMMY_KEY,  '[SrcOrder].[Ordpresal].&[' + ISNULL(ORDPRESAL , 'NULL') + ']'  AS ORDER_STATUS_KEY, 
	ISNULL(ORDPRESAL, 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Pre-Sales' AS ORDER_STATUS_LVL
	FROM spp.TORDER WHERE ORDPRESAL NOT IN ('0' , '1')

UNION

SELECT 'ORDSENT_0' AS DUMMY_KEY , '[SrcOrder].[Ordsent].&[0]' AS ORDER_STATUS_KEY, 'Not Sent' AS ORDER_STATUS_MEM , 'Sent' AS ORDER_STATUS_LVL
UNION
SELECT 'ORDSENT_1' AS DUMMY_KEY , '[SrcOrder].[Ordsent].&[1]' AS ORDER_STATUS_KEY, 'Sent' AS ORDER_STATUS_MEM , 'Sent' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDSENT_' + ISNULL(ORDSENT, 'NULL') AS DUMMY_KEY  ,  '[SrcOrder].[Ordsent].&[' + ISNULL(ORDSENT, 'NULL') + ']'  AS ORDER_STATUS_KEY, 
	ISNULL(ORDSENT , 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Sent' AS ORDER_STATUS_LVL
	FROM spp.TORDER  WHERE ORDSENT NOT IN ('0' , '1')


UNION

SELECT  'ORDSTAT_0' AS DUMMY_KEY , '[SrcOrder].[Ordstat].&[0]' AS ORDER_STATUS_KEY, 'Proposal' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT  'ORDSTAT_1' AS DUMMY_KEY , '[SrcOrder].[Ordstat].&[1]' AS ORDER_STATUS_KEY, 'Pending' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT  'ORDSTAT_2' AS DUMMY_KEY , '[SrcOrder].[Ordstat].&[2]' AS ORDER_STATUS_KEY, 'Closed By Retailer' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT  'ORDSTAT_3' AS DUMMY_KEY , '[SrcOrder].[Ordstat].&[3]' AS ORDER_STATUS_KEY, 'Closed By Salesman' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT  'ORDSTAT_4' AS DUMMY_KEY , '[SrcOrder].[Ordstat].&[4]' AS ORDER_STATUS_KEY, 'Cancelled' AS ORDER_STATUS_MEM , 'Status' AS ORDER_STATUS_LVL
UNION
SELECT DISTINCT 'ORDSTAT_' + ISNULL(ORDSTAT, 'NULL') AS DUMMY_KEY ,  '[SrcOrder].[Ordstat].&[' + ISNULL(ORDSTAT, 'NULL') + ']'  AS ORDER_STATUS_KEY , 
	ISNULL(ORDSTAT, 'NULL') + ' - Unknown' AS ORDER_STATUS_MEM  , 'Status' AS ORDER_STATUS_LVL
	FROM spp.TORDER WHERE ORDSTAT NOT IN ('0' , '1' , '2' , '3' , '4')




















GO
/****** Object:  View [spp].[V_OLAP_ORDPRESAL]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_ORDPRESAL]
AS
SELECT '0' AS ORDPRESAL, 'Not Pre-Sales' AS DESCRIPTION
UNION
SELECT '1' AS ORDPRESAL, 'Pre-Sales' AS DESCRIPTION
UNION
SELECT DISTINCT ISNULL(ORDPRESAL , 'NULL') , ISNULL(ORDPRESAL, 'NULL') + ' - Unknown' AS DESCRIPTION FROM spp.TORDER WHERE ORDPRESAL NOT IN ('0' , '1')




















GO
/****** Object:  View [spp].[V_OLAP_ORDSENT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_ORDSENT]
AS
SELECT '0' AS ORDSENT, 'Not Sent' AS DESCRIPTION
UNION
SELECT '1' AS ORDSENT, 'Sent' AS DESCRIPTION
UNION
SELECT DISTINCT ISNULL(ORDSENT, 'NULL') , ISNULL(ORDSENT , 'NULL') + ' - Unknown' AS DESCRIPTION FROM spp.TORDER  WHERE ORDSENT NOT IN ('0' , '1')






















GO
/****** Object:  View [spp].[V_OLAP_ORDSTAT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_ORDSTAT]
AS
SELECT '0' AS ORDSTAT, 'Proposal' AS DESCRIPTION
UNION
SELECT '1' AS ORDSTAT, 'Pending' AS DESCRIPTION
UNION
SELECT '2' AS ORDSTAT, 'Closed By Retailer' AS DESCRIPTION
UNION
SELECT '3' AS ORDSTAT, 'Closed By Salesman' AS DESCRIPTION
UNION
SELECT '4' AS ORDSTAT, 'Cancelled' AS DESCRIPTION
UNION
SELECT DISTINCT ISNULL(ORDSTAT, 'NULL') , ISNULL(ORDSTAT, 'NULL') + ' - Unknown' AS DESCRIPTION FROM spp.TORDER WHERE ORDSTAT NOT IN ('0' , '1' , '2' , '3' , '4')


















GO
/****** Object:  View [spp].[V_OLAP_PLANOGRAM]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE view [spp].[V_OLAP_PLANOGRAM]
AS
select tdpmhdr.COMSERNO ,  
tdpmhdr.SALMSERN , 
OLAP_LPROPROD.PRODSERN , 
OLAP_LPROPROD.PRODEXPAND ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=tdpmhdr.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_LPROPROD.PRODSERN
),'0')
 AS STORE_SALMSERN ,

TFIXTURE.FXTRSERN ,
tplnentr.PLNECPSUM  AS PLNECPSUM , 
tplnentr.PLNEFCSUM*OLAP_LPROPROD.PRODMONEY_MULT AS  PLNEFCSUM ,
tplnentr.PLNEFCSUM*tplnentr.PLNEPERF*OLAP_LPROPROD.PRODMONEY_MULT AS PLNECHANSUM  
from 
spp.tdpmhdr tdpmhdr inner join spp.tplnhdr tplnhdr on tdpmhdr.dpmhdsern=tplnhdr.dpmhdsern 
inner join spp.tplnentr tplnentr on tplnhdr.plnsern=tplnentr.plnsern
inner join spp.TFIXTURE TFIXTURE on tplnentr.FXTRSERN=TFIXTURE.FXTRSERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON tplnentr.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN
where dpmlast=1























GO
/****** Object:  View [spp].[V_OLAP_PRICELIST_DIM]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_PRICELIST_DIM]
AS
SELECT SELSERN , SELNAME FROM spp.TSELECT WHERE SELSERN IN (SELECT SELSERN FROM spp.OLAP_TSELENTR)
--UNION
--SELECT '0'  AS SELSERN , 'No Data' AS SELNAME WHERE NOT EXISTS(SELECT * FROM spp.TSELECT WHERE SELSERN IN (SELECT SELSERN FROM spp.OLAP_TSELENTR) )














GO
/****** Object:  View [spp].[V_OLAP_PRICELIST_FACT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO























CREATE VIEW [spp].[V_OLAP_PRICELIST_SELRANGE]
AS

select SELE_VALID_RANGE_SERN , DATE
FROM 
spp.OLAP_SELRANGE OLAP_SELRANGE, spp.OLAP_DATE OLAP_DATE
WHERE 
OLAP_SELRANGE.SELE_VALID_START<=OLAP_DATE.DATE
AND
OLAP_SELRANGE.SELE_VALID_END>=OLAP_DATE.DATE













GO
/****** Object:  View [spp].[V_OLAP_PRODEXPAND]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [spp].[V_OLAP_PRODEXPAND]
AS
SELECT cast(0 as tinyint) AS PRODEXPAND , 1 AS ISEXPANDED, 1 AS ISNOTEXPANDED
UNION
SELECT cast(1 as tinyint) AS PRODEXPAND , 0 AS ISEXPANDED, 1 AS ISNOTEXPANDED
UNION
SELECT cast(2 as tinyint) AS PRODEXPAND , 1 AS ISEXPANDED, 0 AS ISNOTEXPANDED


GO
/****** Object:  View [spp].[V_OLAP_PRODEXPAND_HIEARCHY]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    VIEW [spp].[V_OLAP_PRODEXPAND_HIEARCHY]
AS
SELECT 'No' AS PRODEXPAND_CAPTION
UNION
SELECT 'Yes' AS PRODEXPAND_CAPTION

GO
/****** Object:  View [spp].[V_OLAP_PRODUCT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE VIEW [spp].[V_OLAP_PRODUCT]
AS 
SELECT * FROM OLAP_PRODUCT













GO
CREATE VIEW [spp].[V_OLAP_SALESCALL_SALUNCALL]
AS
SELECT SALMSERN , COMSERNO , CALEDATE , 
CASE 
WHEN (  LTRIM(CALESTAT1) IN ('' , '0') AND LTRIM(CALESTAT2) IN ('' , '0') ) THEN 'SALUNCALL_0' 
ELSE 'SALUNCALL_1'
END AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE 
ISNUMERIC(CALESTART)=1 AND ISNUMERIC(CALEEND)=1 AND CALESTART IS NOT NULL AND CALEEND IS NOT NULL AND
DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0










GO
/****** Object:  View [spp].[V_OLAP_SALESCALL_SUM_FACT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF


GO
/****** Object:  View [spp].[V_OLAP_SALESCALL_FACT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_SALESCALL_FACT]
AS
SELECT 
SALMSERN , 
OLAP_STORE.COMSERNO , 
OLAP_STORE.NR , 
CALEDATE,
CALL_LENGTH ,
SALESCALL_KEY 
from 
(
SELECT SALMSERN , COMSERNO , CALEDATE  , 'SALCTYPE_' + ISNULL(SALCTYPE,'') AS SALESCALL_KEY ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'CALESTAT1_' + ISNULL(CALESTAT1,'') AS SALESCALL_KEY ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'CALESTAT2_' + ISNULL(CALESTAT2,'') AS SALESCALL_KEY ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'CALESTAT4_' + ISNULL(CALESTAT4,'') AS SALESCALL_KEY ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT10_' + ISNULL(SALCSTAT10,'') AS SALESCALL_KEY ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT1_' + ISNULL(SALCSTAT1,'') AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT2_' + ISNULL(SALCSTAT2,'') AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT3_' + ISNULL(SALCSTAT3,'') AS SALESCALL_KEY ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT4_' + ISNULL(SALCSTAT4,'') AS SALESCALL_KEY  ,
CASE CALESTART

	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT5_' + ISNULL(SALCSTAT5,'') AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT6_' + ISNULL(SALCSTAT6,'') AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALCSTAT7_' + ISNULL(SALCSTAT7,'') AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
UNION ALL
-------------- ADJUSTABLE VIEW BEING CHANGED BY NIGHTLY JOB----------------------
SELECT * FROM spp.V_OLAP_SALESCALL_SALUNCALL
--------------------------------------------------------------------------------------------------------------------------
UNION ALL
SELECT SALMSERN , COMSERNO , CALEDATE , 'SALNOTCALL_' + ISNULL(SALNOTCALL,'') AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0

)
tcalentr
inner join spp.OLAP_STORE OLAP_STORE
on tcalentr.COMSERNO=OLAP_STORE.COMSERNO

















GO
/****** Object:  View [spp].[V_OLAP_SALESCALL_SALUNCALL]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




















CREATE VIEW [spp].[V_OLAP_SALESCALL_SUM_FACT]
AS
SELECT SALMSERN , CALEDATE ,
CASE MIN(CALESTART)
	WHEN MAX(CALEEND) THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(MIN(CALESTART),2)+':'+RIGHT(MIN(CALESTART),2) as datetime) , CAST(LEFT(MAX(CALEEND) ,2)+':'+RIGHT(MAX(CALEEND) ,2) as datetime)) 
END AS CALL_SUM 
FROM spp.TCALENTR TCALENTR
WHERE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+':'+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+':'+RIGHT(CALEEND,2) as datetime))>=0
AND EXISTS(SELECT COMSERNO FROM spp.OLAP_STORE OLAP_STORE WHERE OLAP_STORE.COMSERNO=TCALENTR.COMSERNO )
AND EXISTS(SELECT SALMSERN  FROM spp.OLAP_SALESFORCE OLAP_SALESFORCE WHERE OLAP_SALESFORCE.SALMSERN=TCALENTR.SALMSERN )
GROUP BY SALMSERN , CALEDATE

















GO
/****** Object:  View [spp].[V_OLAP_SALMCAL]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_SALMCAL]
AS
---------------------------- exceptions ----------------------------
SELECT SALMSERN , CALDATE  , -0.5 as  day_exception , 0.0 as field_day_exception from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE  (LTRIM(CALSTATUS1) IN ('','0' , '1') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('1')) 
	AND OLAP_DATE.WRKDAY_SUM=1.0
UNION ALL
SELECT SALMSERN , CALDATE  , 0.5 as  day_exception , 0.0 as field_day_exception from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE  (LTRIM(CALSTATUS1) IN ('','0' , '1') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('1')) 
	AND OLAP_DATE.WRKDAY_SUM=0.0
UNION ALL
SELECT SALMSERN , CALDATE  , -1.0 as  day_exception , 0.0 as field_day_exception from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE  NOT (LTRIM(CALSTATUS1) IN ('','0' , '1') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('','0' , '1')) 
	AND OLAP_DATE.WRKDAY_SUM=1.0
UNION ALL
SELECT SALMSERN , CALDATE  , 1.0 as  day_exception , 0.0 as field_day_exception  from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE  (LTRIM(CALSTATUS1) IN ('','0' , '1') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('','0')) 
	AND OLAP_DATE.WRKDAY_SUM=0.0

---------------------------- field days----------------------------
UNION ALL
SELECT SALMSERN , CALDATE  , 0.0 as  day_exception , -0.5 as field_day_exception  from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE  (LTRIM(CALSTATUS1) IN ('','0') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('1')) 
	AND OLAP_DATE.WRKDAY_SUM=1.0
UNION ALL
SELECT SALMSERN , CALDATE  , 0.0 as  day_exception , 0.5 as field_day_exception  from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE  (LTRIM(CALSTATUS1) IN ('','0') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('1')) 
	AND OLAP_DATE.WRKDAY_SUM=0.0
UNION ALL
SELECT SALMSERN , CALDATE  , 0.0 as  day_exception , -1.0 as field_day_exception  from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE NOT (LTRIM(CALSTATUS1) IN ('','0') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('','0' , '1')) 
	AND OLAP_DATE.WRKDAY_SUM=1.0
UNION ALL
SELECT SALMSERN , CALDATE  , 0.0 as  day_exception , 1.0 as field_day_exception  from spp.TCALNDAR  TCALNDAR
	INNER JOIN spp.OLAP_DATE OLAP_DATE ON TCALNDAR.CALDATE=OLAP_DATE.DATE
	WHERE (LTRIM(CALSTATUS1) IN ('','0') AND LTRIM(CALSTATUS2) IN ('','0') AND LTRIM(CALSTATUS3) IN ('','0') AND LTRIM(CALSTATUS4) IN ('','0')) 
	AND OLAP_DATE.WRKDAY_SUM=0.0










































GO
/****** Object:  View [spp].[V_OLAP_SELECTION]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [spp].[V_OLAP_SELECTION]
AS
SELECT 
OLAP_SELECTION.COMSERNO , OLAP_SELECTION.PRODSERN , OLAP_SELECTION.SELDATE  , OLAP_SELECTION.INSEL ,

ISNULL((SELECT 
CASE
	WHEN COUNT(*)>1 THEN '-1'
	ELSE MAX(SALMSERN)
END AS SALMSERN 
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  INNER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
WHERE OLAP_LCOMPGR.COMSERNO=OLAP_SELECTION.COMSERNO AND OLAP_LPROPGR.PRODSERN=OLAP_SELECTION.PRODSERN
),'0')
 AS STORE_SALMSERN 

FROM spp.OLAP_SELECTION OLAP_SELECTION
GO
/****** Object:  View [spp].[V_OLAP_STORE_FACT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO










CREATE  VIEW [spp].[V_OLAP_STORE_FACT]
AS
select 
ISNULL(olap_lcompgr.SALMSERN , '0') AS SALMSERN , 
olap_store.COMSERNO , 
olap_store.NR ,
/*
CASE
	WHEN isnull(store_distinct_count,1)=1 THEN ISNULL(COMTURNOVR,0.0) 
	ELSE 0.0
END AS STORE_TURNOVR,
ISNULL(COMTURNOVR,0.0) AS SALESFORCE_TURNOVR
,*/
ISNULL(COMTURNOVR,0.0) AS STORE_TURNOVR
from 
spp.olap_store olap_store 
inner join spp.tcompany tcompany on olap_store.comserno=tcompany.comserno
left outer join 
(
select comserno , salmsern ,
case
	when exists(select * from  spp.olap_lcompgr t2 where t2.comserno=spp.olap_lcompgr.comserno and t2.salmsern<spp.olap_lcompgr.salmsern) then 0
	-- t2.salmsern<spp.olap_lcompgr.salmsern  -- this expression is correct because one of salesmen will have 1 as store_distinct_count
	else 1
end as store_distinct_count
from spp.olap_lcompgr
)olap_lcompgr on olap_store.comserno=olap_lcompgr.comserno






















GO
/****** Object:  View [spp].[V_OLAP_STORE_SALESFORCE]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO











CREATE VIEW [spp].[V_OLAP_STORE_SALESFORCE]
AS
SELECT * FROM spp.OLAP_SALESFORCE
--:)




















GO
/****** Object:  View [spp].[V_OLAP_STORE_SALESFORCE_HIERARCHY]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [spp].[V_OLAP_STORE_SALESFORCE_HIERARCHY]
AS
SELECT * FROM spp.OLAP_STORE_SALESFORCE_HIERARCHY
GO
/****** Object:  View [spp].[V_OLAP_SURVEY_DIM]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_SURVEY_DIM]
AS
SELECT 'NODATA' as QUESTION ,  'NODATA' as ANSWER , 0 as SURVEY_KEY WHERE NOT EXISTS(SELECT * FROM spp.OLAP_SURVEY)
UNION
SELECT QUESTION , ANSWER , SURVEY_KEY FROM spp.OLAP_SURVEY






















GO
/****** Object:  View [spp].[V_OLAP_SURVEY_FACT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO













CREATE VIEW [spp].[V_OLAP_SURVEY_FACT]
AS

SELECT
SURVEY_KEY ,
SALMSERN,
COMSERNO,
SAMCHDATE,
ANSWER_MEASURE AS ANSWER
FROM spp.OLAP_SURVEY
























GO
/****** Object:  View [spp].[V_OLAP_TARGET_DIM]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_TARGET_DIM]
AS
SELECT 'NO_DATA' AS TARGSERN , 'NO_DATA' AS TARGNAME , 'NO_DATA' AS TARGMEASURE , 'NO_DATA' AS DIM_KEY , 'NO_DATA' AS TARGCAP
	WHERE NOT EXISTS(SELECT * FROM spp.TTARGET)
UNION ALL
SELECT TARGSERN , TARGNAME  , 'TARGEVOLUM' AS TARGMEASURE , ISNULL(TARGSERN,'') + '1' AS DIM_KEY  , 'Target Volume' AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME , 'TARGEMONEY' AS TARGMEASURE ,  ISNULL(TARGSERN,'') + '2' AS DIM_KEY   , 'Target Money' AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGENUM1' AS TARGMEASURE ,  ISNULL(TARGSERN,'') + '3' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP1,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGENUM2' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '4' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP2,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGENUM3' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '5' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP3,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGENUM4' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '6' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP4,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGMONEY1' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '7' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP5,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGMONEY2' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '8' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP6,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,  'TARGMONEY3' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '9' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP7,''))  AS TARGCAP
FROM spp.TTARGET
UNION ALL
SELECT TARGSERN , TARGNAME ,   'TARGMONEY4' AS TARGMEASURE ,   ISNULL(TARGSERN,'') + '10' AS DIM_KEY ,
RTRIM(ISNULL(TARGCAP8,''))  AS TARGCAP
FROM spp.TTARGET



















GO
/****** Object:  View [spp].[V_OLAP_TTARENTR_PRODGRP]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_TTARENTR_PRODGRP]
AS
SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGEVOLUM' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGEMONEY' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM1' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM2' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM3' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM4' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGMONEY1' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGMONEY2' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
TARGMONEY3,
0 AS TARGMONEY4,
'TARGMONEY3' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP


UNION ALL


SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
TARGMONEY4,
'TARGMONEY4' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODGRP




















GO
/****** Object:  View [spp].[V_OLAP_TTARENTR_PRODUCT]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO













CREATE VIEW [spp].[V_OLAP_TTARENTR_PRODUCT]
AS



SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
TARGEVOLUM AS TARGEVOLUM ,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGEVOLUM' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
TARGEMONEY*OLAP_LPROPROD.PRODMONEY_MULT AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGEMONEY' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
TARGENUM1  AS TARGENUM1 ,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM1' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
TARGENUM2  AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM2' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
TARGENUM3  AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM3' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
TARGENUM4  AS TARGENUM4 ,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGENUM4' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
TARGMONEY1*OLAP_LPROPROD.PRODMONEY_MULT AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGMONEY1' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN


UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
TARGMONEY2*OLAP_LPROPROD.PRODMONEY_MULT AS TARGMONEY2,
0 AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGMONEY2' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN

UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
TARGMONEY3*OLAP_LPROPROD.PRODMONEY_MULT AS TARGMONEY3,
0 AS TARGMONEY4,
'TARGMONEY3' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN


UNION ALL


SELECT 
TARGSERN,
ISNULL(SALMSERN,'') AS SALMSERN,
COMSERNO,
OLAP_LPROPROD.PRODSERN,
OLAP_LPROPROD.PRODEXPAND,
DATE,
0 AS TARGEVOLUM,
0 AS TARGEMONEY,
0 AS TARGENUM1,
0 AS TARGENUM2,
0 AS TARGENUM3,
0 AS TARGENUM4,
0 AS TARGMONEY1,
0 AS TARGMONEY2,
0 AS TARGMONEY3,
TARGMONEY4*OLAP_LPROPROD.PRODMONEY_MULT AS TARGMONEY4,
'TARGMONEY4' AS TARGMEASURE
FROM
spp.OLAP_TTARENTR_PRODUCT
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD ON spp.OLAP_TTARENTR_PRODUCT.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN










GO
/****** Object:  View [spp].[v_pgrprod]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE  VIEW [spp].[v_pgrprod]
AS
SELECT     spp.LPROPGR.PRODSERN, spp.LPROPGR.PGRSERN, spp.TPRODUCT.COMSERNO, spp.TPRODUCT.PRODCODE, spp.TPRODUCT.PRODNAME, 
                      spp.TPRODUCT.PRODSNAME, spp.TPRODUCT.PRODCOMP, spp.TPRODUCT.PRODPRICE, spp.TPRODUCT.PRODCOST, spp.TPRODUCT.PRODEANC, 
                      spp.TPRODUCT.PRODEANXXX, spp.TPRODUCT.PRODEANCC, spp.TPRODUCT.PRODEANCXX, spp.TPRODUCT.PRODDUNC, 
                      spp.TPRODUCT.PRODDUNXXX, spp.TPRODUCT.PRODCPS, spp.TPRODUCT.PRODCPU, spp.TPRODUCT.PRODCPL, spp.TPRODUCT.PRODCPWI, 
                      spp.TPRODUCT.PRODCPH, spp.TPRODUCT.PRODCPWE, spp.TPRODUCT.PRODSIZE, spp.TPRODUCT.PRODLEN, spp.TPRODUCT.PRODWID, 
                      spp.TPRODUCT.PRODHE, spp.TPRODUCT.PRODWE, spp.TPRODUCT.PRODPALLET, spp.TPRODUCT.PRODPALL, spp.TPRODUCT.PRODDELIV, 
                      spp.TPRODUCT.PRODDELIVE, spp.TPRODUCT.PRODSELS, spp.TPRODUCT.PRODSELE, spp.TPRODUCT.PRODINACT, spp.TPRODUCT.PRODTAX, 
                      spp.TPRODUCT.PRODSTCOEF, spp.TPRODUCT.PRODFUCOST, spp.TPRODUCT.PRODFCDATE, spp.TPRODUCT.PRODDPM, 
                      spp.TPRODUCT.PRODPLAN, spp.TPRODUCT.PRODCURR, spp.TPRODUCT.PRODMON1, spp.TPRODUCT.PRODMON2, spp.TPRODUCT.PRODMON3, 
                      spp.TPRODUCT.PRODMON4, spp.TPRODUCT.PRODMON5, spp.TPRODUCT.PRODMON6, spp.TPRODUCT.PRODMON7, spp.TPRODUCT.PRODMON8, 
                      spp.TPRODUCT.PRODMON9, spp.TPRODUCT.PRODMON10, spp.TPRODUCT.PRODMON11, spp.TPRODUCT.PRODMON12, 
                      spp.TPRODUCT.PRODCPMIN, spp.TPRODUCT.PRODCPMAX, spp.TPRODUCT.PRODCPAVG, spp.TPRODUCT.PRODTEXT1, spp.TPRODUCT.PRODTEXT2, 
                      spp.TPRODUCT.PRODNUM1, spp.TPRODUCT.PRODNUM2, spp.TPRODUCT.PRODMONEY1, spp.TPRODUCT.PRODMONEY2, 
                      spp.LPROPGR.DATESTAMP
FROM         spp.LPROPGR INNER JOIN
                      spp.TPRODUCT ON spp.LPROPGR.PRODSERN = spp.TPRODUCT.PRODSERN









GO
/****** Object:  View [spp].[v_prodpgr]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE  VIEW [spp].[v_prodpgr]
AS
SELECT     spp.LPROPGR.PRODSERN, spp.LPROPGR.PGRSERN, spp.TPGROUPS.PGRPNAME, spp.TPGROUPS.PGRABBR, spp.TPGROUPS.PGRPVAL, 
                      spp.TPGROUPS.PGRPCODE, spp.LPROPGR.DATESTAMP
FROM         spp.LPROPGR INNER JOIN
                      spp.TPGROUPS ON spp.LPROPGR.PGRSERN = spp.TPGROUPS.PGRSERN









GO
/****** Object:  View [spp].[v_proprod]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE  VIEW [spp].[v_proprod]
AS
SELECT     spp.LPROPROD.PRODSERN1, spp.LPROPROD.PRODSERN2, spp.LPROPROD.PRODPKGS, spp.TPRODUCT.PRODCODE, spp.TPRODUCT.PRODNAME, 
                      spp.TPRODUCT.PRODSNAME, spp.TPRODUCT.PRODEANC, spp.TPRODUCT.PRODEANCC, spp.TPRODUCT.PRODDUNC, 
                      spp.LPROPROD.DATESTAMP
FROM         spp.LPROPROD INNER JOIN
                      spp.TPRODUCT ON spp.LPROPROD.PRODSERN2 = spp.TPRODUCT.PRODSERN









GO
/****** Object:  View [spp].[v_SalCom]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE   VIEW [spp].[v_SalCom]
AS
SELECT     spp.LSALCOM.SALMSERN, spp.LSALCOM.COMSERNO, spp.TCOMPANY.COMNAME, spp.TCOMPANY.COMCODE, spp.TCOMPANY.COMADDR, 
                      spp.TCOMPANY.COMPCODE, spp.TCOMPANY.COMCITY
FROM         spp.LSALCOM INNER JOIN
                      spp.TCOMPANY ON spp.LSALCOM.COMSERNO = spp.TCOMPANY.COMSERNO









GO
/****** Object:  View [spp].[v_select_product_groups]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE view [spp].[v_select_product_groups]
AS
SELECT PRODUCT_GROUP_NAME FROM
--THIS SH*T TO PREVENT ERROR IN MSSQL7
(
SELECT 
CASE 
	WHEN LEN(COLUMN_NAME)>6 THEN SUBSTRING(COLUMN_NAME , 7 , LEN(COLUMN_NAME)-6) 
	ELSE NULL
END AS PRODUCT_GROUP_NAME 
from INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_CATALOG=(SELECT DB_NAME()) AND TABLE_SCHEMA='spp' AND TABLE_NAME='OLAP_PRODUCT'
AND LEFT(COLUMN_NAME ,6)='GRP@#@'
)
TEMP_TBL
WHERE PRODUCT_GROUP_NAME IS NOT  NULL











GO
/****** Object:  View [spp].[v_select_store_groups]    Script Date: 01/09/2008 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE view [spp].[v_select_store_groups]
as
SELECT STORE_GROUP_NAME FROM
(
SELECT
--THIS SH*T TO PREVENT ERROR IN MSSQL7
CASE 
	WHEN LEN(COLUMN_NAME)>11 THEN SUBSTRING(COLUMN_NAME , 12 , LEN(COLUMN_NAME)-11) 
	ELSE NULL
END AS STORE_GROUP_NAME 
from INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_CATALOG=(SELECT DB_NAME()) AND TABLE_SCHEMA='spp' AND TABLE_NAME='OLAP_STORE'
AND LEFT(COLUMN_NAME ,11)='RP@#@'
) TEMP_TBL WHERE STORE_GROUP_NAME IS NOT NULL















Go








/****** Object:  StoredProcedure [spp].[proc_create_SALUNCALL]    Script Date: 01/09/2008 18:01:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_create_SALUNCALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_create_SALUNCALL]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DATE]    Script Date: 01/09/2008 18:01:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_DATE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_DATE]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DELDISTR]    Script Date: 01/09/2008 18:01:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_DELDISTR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_DELDISTR]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DELDISTR_SEL]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_DELDISTR_SEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_DELDISTR_SEL]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DPM]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_DPM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_DPM]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DPM_SEL]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_DPM_SEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_DPM_SEL]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_FIXTURE]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_FIXTURE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_FIXTURE]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_LCOMPGR]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_LCOMPGR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_LCOMPGR]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_LPROPROD]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_LPROPROD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_LPROPROD]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_ORDDISTR]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_ORDDISTR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_ORDDISTR]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_ORDDISTR_SEL]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_ORDDISTR_SEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_ORDDISTR_SEL]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_PRODUCT]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_PRODUCT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_PRODUCT]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SALESCALL]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_SALESCALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_SALESCALL]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SELECTION]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_SELECTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_SELECTION]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SELECTION_DIMS]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_SELECTION_DIMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_SELECTION_DIMS]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_STORE]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_STORE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_STORE]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SURVEY]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_SURVEY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_SURVEY]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_TARGET]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_TARGET]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_TARGET]
GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_WHOLESALER]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_fill_OLAP_WHOLESALER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_fill_OLAP_WHOLESALER]
GO
/****** Object:  StoredProcedure [spp].[proc_process_main]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_process_main]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_process_main]
GO
/****** Object:  StoredProcedure [spp].[proc_rename_OLAP_COLUMNS]    Script Date: 01/09/2008 18:01:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[spp].[proc_rename_OLAP_COLUMNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [spp].[proc_rename_OLAP_COLUMNS]
GO





/****** Object:  StoredProcedure [spp].[proc_create_SALUNCALL]    Script Date: 01/09/2008 18:00:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

















CREATE PROCEDURE [spp].[proc_create_SALUNCALL]
@i_param varchar(1024)
AS

/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[V_OLAP_SALESCALL_SALUNCALL]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [spp].[V_OLAP_SALESCALL_SALUNCALL]
/***********************************/


EXECUTE('
CREATE VIEW spp.V_OLAP_SALESCALL_SALUNCALL
AS
SELECT SALMSERN , COMSERNO , CALEDATE , 
 ''SALUNCALL_'' + ISNULL(SALUNCALL , '''' ) AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+'':''+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+'':''+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE 
ISNUMERIC(CALESTART)=1 AND ISNUMERIC(CALEEND)=1 AND CALESTART IS NOT NULL AND CALEEND IS NOT NULL AND
DATEDIFF(mi , CAST(LEFT(CALESTART,2)+'':''+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+'':''+RIGHT(CALEEND,2) as datetime))>=0
')


EXECUTE('
ALTER VIEW spp.V_OLAP_SALESCALL_SALUNCALL
AS
SELECT SALMSERN , COMSERNO , CALEDATE , 
CASE 
WHEN ( ' + @i_param + ') THEN ''SALUNCALL_0'' 
ELSE ''SALUNCALL_1''
END AS SALESCALL_KEY  ,
CASE CALESTART
	WHEN CALEEND THEN 1
	ELSE DATEDIFF(mi , CAST(LEFT(CALESTART,2)+'':''+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+'':''+RIGHT(CALEEND,2) as datetime)) 
END AS CALL_LENGTH 
FROM spp.tcalentr
WHERE 
ISNUMERIC(CALESTART)=1 AND ISNUMERIC(CALEEND)=1 AND CALESTART IS NOT NULL AND CALEEND IS NOT NULL AND
DATEDIFF(mi , CAST(LEFT(CALESTART,2)+'':''+RIGHT(CALESTART,2) as datetime) , CAST(LEFT(CALEEND,2)+'':''+RIGHT(CALEEND,2) as datetime))>=0
')












GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DATE]    Script Date: 01/09/2008 18:00:23 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO








CREATE  PROCEDURE [spp].[proc_fill_OLAP_DATE] AS

DECLARE @MAX_DATE varchar(8)
DECLARE @MIN_DATE varchar(8)

DECLARE @temp_salenum varchar(6)

DECLARE @temp_year char(4)
DECLARE @temp_week_year char(4)
DECLARE @temp_quarter char(2)
DECLARE @temp_month char(2)
DECLARE @temp_week char(2)
DECLARE @temp_day char(2)

DECLARE @snapshot_date char(8)

DECLARE @temp_date char(8)
DECLARE @temp_date_real smalldatetime


DECLARE @count int
DECLARE @cur_date varchar(8)
DECLARE @wrkday bit

SET DATEFORMAT dmy
SET DATEFIRST 1
SET NOCOUNT ON

/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DATE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_DATE]
/***********************************/

/***********************************/
CREATE TABLE [spp].[OLAP_DATE] (
	[DATE] [varchar] (8)   NOT NULL PRIMARY KEY ,
	[YEAR] [varchar] (4)  NOT NULL ,
	[QUARTER] [varchar] (6)  NOT NULL ,
	[MONTH] [varchar] (6) NOT NULL ,
	[WEEK] [varchar] (6)  NOT NULL ,
	[SALENUM] [varchar] (6)  NOT NULL ,
	[SNAPSHOT_DATE] [varchar] (8) NOT NULL ,
	[YEAR_SNAPSHOT_DATE] [varchar] (8)  NULL ,
	[WRKDAY] tinyint DEFAULT 1 ,
	[WRKDAY_SUM] numeric DEFAULT 1.0 ,
	[WRKDAY_SERNO] int   NOT NULL DEFAULT 0 ,
	[MSA_WRKDAY] tinyint DEFAULT 1,
	[WEEKEND] tinyint DEFAULT 0 ,
) ON [PRIMARY]
/***********************************/




CREATE TABLE #DATE_TABLE (DATE varchar(8))


INSERT INTO #DATE_TABLE(DATE)
SELECT DISTINCT TEMPDATE FROM 
(
SELECT DPMHDATE AS TEMPDATE FROM TDPMHDR 

UNION

SELECT DELDATE  AS TEMPDATE FROM TDELIVER 

UNION

SELECT ORDCDATE AS TEMPDATE FROM TORDER 

UNION

SELECT ORDODATE AS TEMPDATE FROM TORDER 

UNION

SELECT ORDDDATE AS TEMPDATE FROM TORDER 

UNION

SELECT SAMCHDATE AS TEMPDATE FROM TSAMERCH 

UNION

SELECT CALEDATE AS TEMPDATE FROM TCALENTR 

UNION

SELECT MSADATE AS TEMPDATE FROM TMSASLIP 
	WHERE EXISTS(SELECT * FROM TMSAENTR WHERE TMSASLIP.MSASERN=TMSAENTR.MSASERN)

UNION

SELECT MSAPAID AS TEMPDATE FROM TMSASLIP 
	WHERE EXISTS(SELECT * FROM TMSAENTR WHERE TMSASLIP.MSASERN=TMSAENTR.MSASERN)

UNION

SELECT MSAPAY AS TEMPDATE FROM TMSASLIP 
	WHERE EXISTS(SELECT * FROM TMSAENTR WHERE TMSASLIP.MSASERN=TMSAENTR.MSASERN)


UNION

SELECT MSAEND AS TEMPDATE FROM TMSASLIP 
	WHERE EXISTS(SELECT * FROM TMSAENTR WHERE TMSASLIP.MSASERN=TMSAENTR.MSASERN)

UNION

SELECT MSASTART AS TEMPDATE FROM TMSASLIP 
	WHERE EXISTS(SELECT * FROM TMSAENTR WHERE TMSASLIP.MSASERN=TMSAENTR.MSASERN)

UNION

SELECT LEFT(TARGETIME,4) + '12' + '31' AS TEMPDATE FROM TTARENTR

UNION

SELECT LEFT(TARGETIME,4) + '01' + '01' AS TEMPDATE FROM TTARENTR

UNION

SELECT SELSTART AS TEMPDATE FROM TSELECT 
	WHERE EXISTS(SELECT * FROM TSELENTR WHERE TSELECT.SELSERN=TSELENTR.SELSERN)

UNION

SELECT SELESTART AS TEMPDATE FROM TSELENTR 

UNION

SELECT SELEND AS TEMPDATE FROM TSELECT 
	WHERE EXISTS(SELECT * FROM TSELENTR WHERE TSELECT.SELSERN=TSELENTR.SELSERN)

UNION

SELECT SELEEND AS TEMPDATE FROM TSELENTR 

) DATE_TBL







SELECT @MAX_DATE=MAX(TEMPDATE) , @MIN_DATE=MIN(TEMPDATE) FROM
(
SELECT convert(varchar(8) , DATEADD(m , 13 , getdate())  , 112) AS TEMPDATE

UNION

SELECT LEFT(convert(varchar(8) , DATEADD(m , -25 , getdate())  , 112) , 4) + '0101'  AS TEMPDATE

UNION

SELECT DATE AS TEMPDATE FROM #DATE_TABLE WHERE ISDATE(DATE)=1 AND DATE>='1980' AND DATE<='2020'

) TBL







IF RIGHT(@MIN_DATE,2)>'28'
	SET @MIN_DATE=LEFT(@MIN_DATE , 6) + '28' 

IF RIGHT(@MAX_DATE,2)<'28'
	SET @MAX_DATE=LEFT(@MAX_DATE , 6) + '28' 





SET @temp_date_real=CONVERT(smalldatetime , @MIN_DATE , 112)

SET @temp_year=YEAR(@temp_date_real)
SET @temp_quarter='Q' + cast(DATEPART(qq , @temp_date_real) as char(1))
SET @temp_month=MONTH(@temp_date_real)
IF LEN(@temp_month)=1
	SET @temp_month='0' + @temp_month
SET @temp_week=DATEPART(wk , @temp_date_real)
SET @temp_week_year=@temp_year

-- finnish week conversion
IF DATEPART(dw , CONVERT(smalldatetime , @temp_week_year + '0101' , 112))>4 -- if first jan is after thursday	
	BEGIN
		IF @temp_week='1'
			BEGIN
				SET @temp_week_year=CAST(@temp_week_year as int)-1
				SET @temp_week='53'
			END
		ELSE
			SET @temp_week=CAST(@temp_week as int)-1
	END
-- end finnish week conversion

IF LEN(@temp_week)=1
	SET @temp_week='0' + @temp_week
SET @temp_day=DAY(@temp_date_real)
IF LEN(@temp_day)=1
	SET @temp_day='0' + @temp_day

SET @snapshot_date=@temp_year+@temp_month+'28'

SET @temp_salenum='00000000'
SELECT @temp_salenum=ISNULL(SALENUM,'00000000') FROM TSALEPER WHERE SALESTART<=@MIN_DATE AND SALEEND>=@MIN_DATE

IF NOT EXISTS(SELECT * FROM OLAP_DATE WHERE DATE=@temp_year+@temp_month+@temp_day)
	INSERT INTO OLAP_DATE(YEAR , QUARTER , MONTH , WEEK , DATE , SALENUM , SNAPSHOT_DATE )
		VALUES(@temp_year , @temp_year + @temp_quarter , @temp_year+@temp_month ,  @temp_week_year+@temp_week , @temp_year+@temp_month+@temp_day , @temp_salenum , @snapshot_date )











IF EXISTS(SELECT * FROM OLAP_DATE WHERE DATE<@MAX_DATE )
	BEGIN
		SELECT @temp_date=MAX(DATE)  FROM OLAP_DATE WHERE DATE<=@MAX_DATE
		WHILE @temp_date<@MAX_DATE
			BEGIN

				SET @temp_date_real=CONVERT(smalldatetime , @temp_date , 112)
				
				SET @temp_date_real=DATEADD ( d , 1, @temp_date_real) 
				
				SET @temp_year=YEAR(@temp_date_real)
				SET @temp_quarter='Q' + cast(DATEPART(qq , @temp_date_real) as char(1))
				SET @temp_month=MONTH(@temp_date_real)
				IF LEN(@temp_month)=1
					SET @temp_month='0' + @temp_month
				SET @temp_week=DATEPART(wk , @temp_date_real)
				SET @temp_week_year=@temp_year
				
				-- finnish week conversion
				IF DATEPART(dw , CONVERT(smalldatetime , @temp_week_year + '0101' , 112))>4 -- if first jan is after thursday	
					BEGIN
						IF @temp_week='1'
							BEGIN
								SET @temp_week_year=CAST(@temp_week_year as int)-1
								SET @temp_week='53'
							END
						ELSE
							SET @temp_week=CAST(@temp_week as int)-1
					END
				-- end finnish week conversion


				IF LEN(@temp_week)=1
					SET @temp_week='0' + @temp_week
				SET @temp_day=DAY(@temp_date_real)
				IF LEN(@temp_day)=1
					SET @temp_day='0' + @temp_day
		
				
				--IF EXISTS(SELECT  * FROM TDPMHDR WHERE LEFT(DPMHDATE,6)=@temp_year+@temp_month AND EXISTS(SELECT * FROM TDPMENTR WHERE TDPMENTR.DPMHDSERN=TDPMHDR.DPMHDSERN))
					SET @snapshot_date=@temp_year+@temp_month+'28'
				--OTHERWISE JUST PREVIOUSE NOT EMPTY SNAPSHOT DATE

				SET @temp_salenum='00000000'
				SELECT @temp_salenum=ISNULL(SALENUM,'00000000') FROM TSALEPER WHERE SALESTART<=@temp_year+@temp_month+@temp_day AND SALEEND>=@temp_year+@temp_month+@temp_day

				IF NOT EXISTS(SELECT * FROM OLAP_DATE WHERE DATE=@temp_year+@temp_month+@temp_day)
					INSERT INTO OLAP_DATE(YEAR , QUARTER , MONTH , WEEK , DATE , SALENUM , SNAPSHOT_DATE )
						VALUES(@temp_year , @temp_year +@temp_quarter , @temp_year+@temp_month ,  @temp_week_year+@temp_week , @temp_year+@temp_month+@temp_day , @temp_salenum ,@snapshot_date )

				SELECT @temp_date=MAX(DATE) FROM OLAP_DATE WHERE DATE<=@MAX_DATE
			END
	END


UPDATE OLAP_DATE SET YEAR_SNAPSHOT_DATE=year_table.SNAPSHOT_DATE
	FROM OLAP_DATE , (SELECT MAX(SNAPSHOT_DATE) AS SNAPSHOT_DATE , YEAR FROM OLAP_DATE GROUP BY YEAR) year_table
	WHERE OLAP_DATE.YEAR=year_table.YEAR

UPDATE OLAP_DATE SET WRKDAY=0 , MSA_WRKDAY=0 , WEEKEND=1 , WRKDAY_SUM=0
	WHERE DATEPART(dw, DATE ) IN ( 6 ,7 )


UPDATE OLAP_DATE SET WRKDAY=1 , MSA_WRKDAY=1 , WRKDAY_SUM=1 WHERE DATE IN (SELECT CALGDATE FROM TCALGLOB WHERE CALGSTAT1='0'  )

UPDATE OLAP_DATE SET WRKDAY=1 , MSA_WRKDAY=1 , WRKDAY_SUM=0.5   WHERE DATE IN (SELECT CALGDATE FROM TCALGLOB WHERE CALGSTAT1='1'  )

UPDATE OLAP_DATE SET WRKDAY=0 , MSA_WRKDAY=0 ,  WRKDAY_SUM=0  WHERE DATE IN (SELECT CALGDATE FROM TCALGLOB WHERE CALGSTAT1='2'  )

--UPDATE OLAP_DATE SET WEEKEND=1 WHERE DATEPART(dw , DATE )>5

UPDATE OLAP_DATE SET MSA_WRKDAY=1 
FROM OLAP_DATE WHERE EXISTS(SELECT * FROM spp.TMSASLIP TMSASLIP WHERE TMSASLIP.MSASTART=OLAP_DATE.DATE OR TMSASLIP.MSAEND=OLAP_DATE.DATE)




SET @count=0
DECLARE temp_cursor CURSOR FOR SELECT [DATE] , WRKDAY FROM OLAP_DATE ORDER BY DATE
OPEN temp_cursor
FETCH  NEXT FROM temp_cursor INTO @cur_date , @wrkday

WHILE @@FETCH_STATUS=0
	BEGIN
		IF @wrkday=1
			SET @count=@count+1

		UPDATE OLAP_DATE SET WRKDAY_SERNO=@count WHERE DATE=@cur_date

		FETCH  NEXT FROM temp_cursor INTO @cur_date , @wrkday
	END

CLOSE temp_cursor
DEALLOCATE temp_cursor

/*************************************************************************************************************/
create index ix_olap_date_WRKDAY_SERNO on olap_date(WRKDAY_SERNO)
/*************************************************************************************************************/






CREATE NONCLUSTERED INDEX IX_OLAP_DATE_YEAR
ON OLAP_DATE(YEAR)

CREATE NONCLUSTERED INDEX IX_OLAP_DATE_MONTH
ON OLAP_DATE(MONTH)

CREATE NONCLUSTERED INDEX IX_OLAP_DATE_WEEK
ON OLAP_DATE(WEEK)

CREATE NONCLUSTERED INDEX IX_OLAP_DATE_SALENUM
ON OLAP_DATE(SALENUM)



DROP TABLE #DATE_TABLE








GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DELDISTR]    Script Date: 01/09/2008 18:00:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
















CREATE PROCEDURE [spp].[proc_fill_OLAP_DELDISTR]
AS

SET NOCOUNT ON
SET DATEFIRST 1



--------------------   DATE TO INSERT CUMULATIVE SUMS FROM  -----------------------------

DECLARE @DEL_START_DELDATE varchar(8)
DECLARE @AUDIT_START_DELDATE varchar(8)
DECLARE @START_DELDATE varchar(8)
DECLARE @LAST_DELDATE varchar(8)


SELECT @DEL_START_DELDATE=MIN(DELDATE) FROM 
TDELIVER INNER JOIN TDELENTR ON TDELIVER.DELSERN=TDELENTR.DELSERN
WHERE 
EXISTS(SELECT * FROM OLAP_DATE WHERE TDELIVER.DELDATE=OLAP_DATE.DATE)
AND EXISTS(SELECT * FROM OLAP_STORE WHERE TDELIVER.COMSERNO=OLAP_STORE.COMSERNO)
AND EXISTS(SELECT * FROM OLAP_PRODUCT WHERE TDELENTR.PRODSERN=OLAP_PRODUCT.PRODSERN)
AND NOT EXISTS(SELECT * FROM OLAP_DELDISTR_NOTEXP WHERE TDELIVER.DELDATE=OLAP_DELDISTR_NOTEXP.DELDATE)
AND DELDATE<=convert(varchar(8) , DATEADD(m , 2  , GETDATE() )  , 112) 

SELECT @AUDIT_START_DELDATE=MIN(DELDATE) FROM TDELIVER WHERE DELSERN IN ( SELECT KEYSERN1 FROM OLAP_AUDIT WHERE KEYTYPE='DELSERN' )
AND EXISTS(SELECT * FROM OLAP_DATE WHERE TDELIVER.DELDATE=OLAP_DATE.DATE)

IF @DEL_START_DELDATE IS NULL AND @AUDIT_START_DELDATE IS NULL
	RETURN
ELSE
IF @DEL_START_DELDATE IS NOT NULL AND @AUDIT_START_DELDATE IS NOT NULL
	IF @DEL_START_DELDATE<@AUDIT_START_DELDATE
		SET @START_DELDATE=@DEL_START_DELDATE
	ELSE
		SET @START_DELDATE=@AUDIT_START_DELDATE
ELSE
IF @DEL_START_DELDATE IS NOT NULL
	SET @START_DELDATE=@DEL_START_DELDATE
ELSE
IF @AUDIT_START_DELDATE IS NOT NULL
	SET @START_DELDATE=@AUDIT_START_DELDATE



print 'date to start: ' + @START_DELDATE

------------------------------------------------------------------------------------------






print 'DELETE FROM OLAP_DELDISTR_TMP'
print getdate()

DELETE FROM OLAP_DELDISTR_TMP WHERE DELDATE>=@START_DELDATE

print 'end'
print getdate()




print 'insert into OLAP_DELDISTR_TMP'
print getdate()


insert into OLAP_DELDISTR_TMP(comserno , prodsern , DELDATE , rangestart_date , outofstock_date , wrkdays_in_range , deldmonth , wrkday_serno , deleactvol )
select
comserno ,
tdelentr.prodsern ,
DELDATE ,
convert(varchar(8) , DATEADD(m , -12 , CONVERT(datetime , DELDATE ) )  , 112) as rangestart_date ,
'' as outofstock_date ,
0 as wrkdays_in_range ,
'' as deldmonth,
max(olap_date.wrkday_serno) as wrkday_serno ,
sum(ISNULL(
(CASE DELEVOLFLG
	WHEN '1'  THEN  spp.TDELENTR.DELEACTVOL* OLAP_LPROPROD.PRODSIZE*OLAP_LPROPROD.PRODCASE_MULT
	ELSE spp.TDELENTR.DELEACTVOL
END)
,0)) as deleactvol
from tdeliver inner join tdelentr on tdeliver.delsern=tdelentr.delsern
inner join olap_date on olap_date.date=tdeliver.DELDATE
inner join OLAP_LPROPROD on OLAP_LPROPROD.prodsern=tdelentr.prodsern
where exists(select * from olap_store where olap_store.comserno=tdeliver.comserno)
and DELDATE>=@START_DELDATE
group by comserno , tdelentr.prodsern , DELDATE


print 'end'
print getdate()




print 'UPDATE OLAP_DELDISTR_TMP'
print getdate()

UPDATE OLAP_DELDISTR_TMP
SET wrkdays_in_range=wrkday_serno-(SELECT TOP 1 WRKDAY_SERNO FROM OLAP_DELDISTR_TMP t2 WHERE t2.comserno=OLAP_DELDISTR_TMP.comserno and t2.prodsern=OLAP_DELDISTR_TMP.prodsern and t2.DELDATE>=OLAP_DELDISTR_TMP.rangestart_date ORDER BY t2.DELDATE ASC)
WHERE DELDATE>=@START_DELDATE

print 'end'
print getdate()






print 'UPDATE OLAP_DELDISTR_TMP'
print getdate()

DECLARE @MAX_DATE char(8)

SELECT @MAX_DATE=MAX(DATE) FROM OLAP_DATE


UPDATE OLAP_DELDISTR_TMP
SET
OUTOFSTOCK_DATE=ISNULL(
(SELECT MIN(DATE) FROM OLAP_DATE d1 WHERE d1.WRKDAY_SERNO=(OLAP_DELDISTR_TMP.WRKDAY_SERNO+ceiling( (case when OLAP_DELDISTR_TMP.wrkdays_in_range>0 then OLAP_DELDISTR_TMP.wrkdays_in_range else 21 end)*(OLAP_DELDISTR_TMP.deleactvol*0.8)/isnull((select case when sum(t2.deleactvol)=0 then NULL else sum(t2.deleactvol) end from OLAP_DELDISTR_TMP t2 
	where t2.comserno=OLAP_DELDISTR_TMP.comserno and t2.prodsern=OLAP_DELDISTR_TMP.prodsern and t2.DELDATE<OLAP_DELDISTR_TMP.DELDATE and t2.DELDATE>=OLAP_DELDISTR_TMP.rangestart_date) , case when OLAP_DELDISTR_TMP.deleactvol=0 then 1 else (OLAP_DELDISTR_TMP.deleactvol*0.8) end  ))
)
) , @MAX_DATE )
WHERE DELDATE>=@START_DELDATE

print 'end'
print getdate()





print 'DELETE FROM OLAP_DELDISTR_TMP2'
print getdate()

DELETE FROM OLAP_DELDISTR_TMP2 WHERE DELDATE>=@START_DELDATE

print 'end'
print getdate()




print 'INSERT INTO OLAP_DELDISTR_TMP2'
print getdate()


INSERT INTO OLAP_DELDISTR_TMP2 (
COMSERNO ,
PRODSERN , 
DELDATE ,
PREV_DELDATE ,
INDISTR ,
INBSEL ,  
INSEL
)
select 
comserno , 
prodsern , 
DELDATE , 
'00000000' as prev_DELDATE , 
case 
	when sum(indistr)=0	then 0
	else 1
end as indistr , 
0 as inbsel,
0 as insel
from
(
select comserno , prodsern , DELDATE , 1 as indistr 
from OLAP_DELDISTR_TMP t1 where not exists(select * from OLAP_DELDISTR_TMP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.DELDATE<t1.DELDATE and t1.DELDATE>t2.outofstock_date)
and DELDATE>=@START_DELDATE

union all

select comserno , prodsern , outofstock_date as DELDATE , 0 as indistr 
from OLAP_DELDISTR_TMP t1 where not exists(select * from OLAP_DELDISTR_TMP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.DELDATE<t1.outofstock_date and t1.outofstock_date>t2.outofstock_date)
and outofstock_date>=@START_DELDATE
)tbl
group by comserno , prodsern , DELDATE

print 'end'
print getdate()





print 'insert into OLAP_DELDISTR_TMP2'
print getdate()


insert into OLAP_DELDISTR_TMP2(
COMSERNO ,
PRODSERN , 
DELDATE ,
PREV_DELDATE ,
INDISTR ,
INBSEL ,  
INSEL 
)
select 
comserno , 
prodsern , 
seldate as DELDATE , 
'00000000' as prev_DELDATE , 
(select top 1 indistr from OLAP_DELDISTR_TMP2 d2 where d2.comserno=s1.comserno and d2.prodsern=s1.prodsern and d2.DELDATE<s1.seldate order by DELDATE desc) as indistr ,
0 as inbsel,
0 as insel 
from 
(

select comserno , prodsern , seldate from olap_selection s1
where seldate>=@START_DELDATE
and exists(select * from  OLAP_DELDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.DELDATE<s1.seldate)
	and not exists(select * from  OLAP_DELDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.DELDATE=s1.seldate)

union

select comserno , prodsern , seldate from olap_base_selection s1
where seldate>=@START_DELDATE
and exists(select * from  OLAP_DELDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.DELDATE<s1.seldate)
	and not exists(select * from  OLAP_DELDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.DELDATE=s1.seldate)
) s1


print 'end'
print getdate()






print 'update OLAP_DELDISTR_TMP2'
print getdate()


update OLAP_DELDISTR_TMP2
set
prev_DELDATE=IsNUll(
(select top 1 DELDATE from OLAP_DELDISTR_TMP2 t2 where OLAP_DELDISTR_TMP2.comserno=t2.comserno and OLAP_DELDISTR_TMP2.prodsern=t2.prodsern and OLAP_DELDISTR_TMP2.DELDATE>t2.DELDATE order by t2.DELDATE desc)  
, '00000000') ,
inbsel=(case IsNUll((select top 1 insel from olap_base_selection s2 where s2.comserno=OLAP_DELDISTR_TMP2.comserno and s2.prodsern=OLAP_DELDISTR_TMP2.prodsern and s2.seldate<=OLAP_DELDISTR_TMP2.DELDATE order by seldate desc),0)
	when 1 then 1
	else 0
end),
insel=(case IsNUll((select top 1 insel from olap_selection s2 where s2.comserno=OLAP_DELDISTR_TMP2.comserno and s2.prodsern=OLAP_DELDISTR_TMP2.prodsern and s2.seldate<=OLAP_DELDISTR_TMP2.DELDATE order by seldate desc),0)
	when 1 then 1
	else 0
end) 
WHERE DELDATE>=@START_DELDATE

print 'end'
print getdate()







print 'DELETE FROM OLAP_DELDISTR_NOTEXP'
print getdate()

DELETE FROM OLAP_DELDISTR_NOTEXP WHERE DELDATE>=@START_DELDATE

print 'end'
print getdate()







print 'insert into OLAP_DELDISTR_NOTEXP'
print getdate()


insert into OLAP_DELDISTR_NOTEXP(comserno , prodsern , DELDATE , insel_cum , inbsel_cum , indistr_cum , inseldistr_cum , inbseldistr_cum , prodexpand , prodexpand_intersect)
select t1.comserno, p1.prodsern, t1.DELDATE, 
(case
	when isnull(t2.insel,0)=1 and t1.insel=0	then -1
	when isnull(t2.insel,0)=0 and t1.insel=1	then 1
	else 0
end) as insel_cum,
(case
	when isnull(t2.inbsel,0)=1 and t1.inbsel=0	then -1
	when isnull(t2.inbsel,0)=0 and t1.inbsel=1	then 1
	else 0
end) as inbsel_cum,
(case
	when isnull(t2.indistr,0)=1 and t1.indistr=0	then -1
	when isnull(t2.indistr,0)=0 and t1.indistr=1	then 1
	else 0
end) as indistr_cum,
(case
	when (isnull(t2.indistr,0)=1 and isnull(t2.insel,0)=1) and (t1.indistr=0 or t1.insel=0)	then -1
	when (isnull(t2.indistr,0)=0 or isnull(t2.insel,0)=0) and (t1.indistr=1 and t1.insel=1)	then 1
	else 0
end) as inseldistr_cum ,
(case
	when (isnull(t2.indistr,0)=1 and isnull(t2.inbsel,0)=1) and (t1.indistr=0 or t1.inbsel=0)	then -1
	when (isnull(t2.indistr,0)=0 or isnull(t2.inbsel,0)=0) and (t1.indistr=1 and t1.inbsel=1)	then 1
	else 0
end) as inbseldistr_cum ,
prodexpand ,
prodexpand_intersect
from OLAP_DELDISTR_TMP2 t1
inner join olap_lproprod p1 
	on t1.prodsern=p1.parent_prodsern
left outer join OLAP_DELDISTR_TMP2 t2
	on t1.comserno=t2.comserno and t1.prodsern=t2.prodsern and t1.prev_DELDATE=t2.DELDATE
where t1.DELDATE>=@START_DELDATE



print 'end'
print getdate()








print 'delete from OLAP_DELDISTR_EXP'
print getdate()

delete from OLAP_DELDISTR_EXP where DELDATE>=@START_DELDATE

print 'end'
print getdate()

 



print 'insert into OLAP_DELDISTR_EXP'
print getdate()


insert into OLAP_DELDISTR_EXP(comserno , prodsern , DELDATE , 
				prev_indistr_cum_sum , prev_insel_cum_sum , prev_inbsel_cum_sum , 
				cur_indistr_cum_sum , cur_insel_cum_sum, cur_inbsel_cum_sum , 
				indistr_cum , inseldistr_cum , inbseldistr_cum)
select comserno , prodsern , DELDATE ,
isnull((select sum(indistr_cum) from OLAP_DELDISTR_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.DELDATE<t1.DELDATE),0) 
as prev_indistr_cum_sum ,
isnull((select sum(insel_cum) from OLAP_DELDISTR_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.DELDATE<t1.DELDATE),0) 
as prev_insel_cum_sum ,
isnull((select sum(inbsel_cum) from OLAP_DELDISTR_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.DELDATE<t1.DELDATE),0) 
as prev_inbsel_cum_sum ,
sum(indistr_cum) as cur_indistr_cum_sum ,
sum(insel_cum) as cur_insel_cum_sum ,
sum(inbsel_cum) as cur_inbsel_cum_sum ,
0 as indistr_cum ,
0 as inseldistr_cum , 
0 as inbseldistr_cum 
from 
OLAP_DELDISTR_NOTEXP t1 
where prodexpand_intersect=2 and DELDATE>=@START_DELDATE
group by comserno , prodsern , DELDATE


print 'end'
print getdate()




print 'update OLAP_DELDISTR_EXP'
print getdate()

update OLAP_DELDISTR_EXP
set indistr_cum=
(case
	when prev_indistr_cum_sum>0 and prev_indistr_cum_sum+cur_indistr_cum_sum<=0	then -1
	when prev_indistr_cum_sum<=0 and prev_indistr_cum_sum+cur_indistr_cum_sum>0	then 1
	else 0
end) ,
inseldistr_cum=
(case
	when (prev_indistr_cum_sum>0 and prev_insel_cum_sum>0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)<=0 or (prev_insel_cum_sum+cur_insel_cum_sum)<=0)	then -1
	when (prev_indistr_cum_sum<=0 or prev_insel_cum_sum<=0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)>0 and (prev_insel_cum_sum+cur_insel_cum_sum)>0)	then 1
	else 0
end) ,
inbseldistr_cum=
(case
	when (prev_indistr_cum_sum>0 and prev_inbsel_cum_sum>0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)<=0 or (prev_inbsel_cum_sum+cur_inbsel_cum_sum)<=0)	then -1
	when (prev_indistr_cum_sum<=0 or prev_inbsel_cum_sum<=0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)>0 and (prev_inbsel_cum_sum+cur_inbsel_cum_sum)>0)	then 1
	else 0
end)
where DELDATE>=@START_DELDATE


print 'end'
print getdate()


DELETE FROM OLAP_AUDIT WHERE KEYTYPE='DELSERN'















GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DELDISTR_SEL]    Script Date: 01/09/2008 18:00:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
















CREATE procedure [spp].[proc_fill_OLAP_DELDISTR_SEL] 
@start_seldate varchar(8)
as

declare @seldate varchar(8)
declare @prev_seldate varchar(8)

set nocount on 


create table #cumulative_set(comserno varchar(15), prodsern varchar(15) , prodexpand tinyint , indistr smallint , prev_indistr smallint , insel smallint , prev_insel smallint , inbsel smallint , prev_inbsel smallint)



SELECT @start_seldate=ISNULL(MAX(SELDATE) , '00000000')  FROM olap_seldate t2 WHERE SELDATE<=@start_seldate



delete from olap_deldistr where indistr=0 and (inseldistr!=0  or inbseldistr!=0) and deldate>=@start_seldate
update olap_deldistr set inseldistr=0 ,  inbseldistr=0  where deldate>=@start_seldate




--------------------- filling cumulative set -----------------------

IF @start_seldate!='00000000'
	BEGIN

		insert into #cumulative_set(comserno , prodsern , prodexpand , indistr , prev_indistr,  insel , prev_insel,  inbsel , prev_inbsel)
			select comserno , prodsern , prodexpand , sum(indistr) as  indistr, 0 as prev_indistr , 0  as insel , 0 as prev_insel  , 0  as inbsel , 0 as prev_inbsel from olap_deldistr
			where deldate<@start_seldate
			group by comserno , prodsern , prodexpand
		
		
		
		UPDATE #cumulative_set
			SET INSEL=1
			FROM #cumulative_set WHERE EXISTS(SELECT * FROM OLAP_TSELENTR , OLAP_LCOMSEL WHERE 
			OLAP_LCOMSEL.COMSERNO=#cumulative_set.COMSERNO AND OLAP_TSELENTR.PRODSERN=#cumulative_set.PRODSERN AND
			OLAP_TSELENTR.SELSERN=OLAP_LCOMSEL.SELSERN AND SELESTART<=convert(varchar(8) , DATEADD(d , -1 , CONVERT(datetime , @start_seldate ) )  , 112)  AND SELEEND>=@start_seldate )
		
		
		
		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN AND OLAP_BASE_SELECTION.SELDATE<@start_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, 0 ) --ISNULL end
			FROM  #cumulative_set 
	END

--------------------------------------------------------------------------




declare temp_cursor cursor for
select * from 
(
select seldate from olap_seldate 
union
select '00000000' as seldate
union
select '99999999' as seldate
)tbl
where seldate>=@start_seldate
order by seldate

open temp_cursor


fetch next from temp_cursor into @seldate

set @prev_seldate=@seldate


fetch next from temp_cursor into @seldate

while @@fetch_status=0
	begin
		print @prev_seldate + '-' + @seldate

		update #cumulative_set
			set prev_indistr=indistr , prev_insel=insel , prev_inbsel=inbsel , insel=0 , inbsel=0



		insert into #cumulative_set(comserno , prodsern , prodexpand , indistr , prev_indistr,  insel , prev_insel,  inbsel , prev_inbsel)
			select comserno , prodsern , prodexpand , 0 as indistr, 0 as prev_indistr , 0  as insel , 0 as prev_insel , 0  as inbsel , 0 as prev_inbsel from olap_deldistr
			where deldate>=@prev_seldate and deldate<@seldate
			and not exists(select * from #cumulative_set t1 where t1.comserno=olap_deldistr.comserno and t1.prodsern=olap_deldistr.prodsern and t1.prodexpand=olap_deldistr.prodexpand)
			group by comserno , prodsern , prodexpand



		UPDATE #cumulative_set
			SET INSEL=1
			FROM #cumulative_set WHERE EXISTS(SELECT * FROM OLAP_TSELENTR , OLAP_LCOMSEL WHERE 
			OLAP_LCOMSEL.COMSERNO=#cumulative_set.COMSERNO AND OLAP_TSELENTR.PRODSERN=#cumulative_set.PRODSERN AND
			OLAP_TSELENTR.SELSERN=OLAP_LCOMSEL.SELSERN AND SELESTART<=@prev_seldate AND SELEEND>@prev_seldate )


		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN AND OLAP_BASE_SELECTION.SELDATE<=@prev_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, 0 ) --ISNULL end
			FROM  #cumulative_set 


		--select * from #cumulative_set where comserno='IMP000000009354' and prodsern='IMP000000014524'

		update olap_deldistr
			set olap_deldistr.inseldistr=
			(case
				when olap_deldistr.indistr=1 and #cumulative_set.insel=1 then 1
				when olap_deldistr.indistr=-1 and #cumulative_set.insel=1 then -1
				else 0
			end) , 
			olap_deldistr.inbseldistr=(case
				when olap_deldistr.indistr=1 and #cumulative_set.inbsel=1 then 1
				when olap_deldistr.indistr=-1 and #cumulative_set.inbsel=1 then -1
				else 0
			end)
			from #cumulative_set , olap_deldistr
			where #cumulative_set.comserno=olap_deldistr.comserno and #cumulative_set.prodsern=olap_deldistr.prodsern  and #cumulative_set.prodexpand=olap_deldistr.prodexpand
			and deldate>=@prev_seldate and deldate<@seldate



		insert into olap_deldistr(comserno , prodsern , prodexpand , deldate , indistr , inseldistr ,  inbseldistr)
			select #cumulative_set.comserno , #cumulative_set.prodsern , #cumulative_set.prodexpand , @prev_seldate as deldate , 0 as indistr ,  insel-prev_insel as inseldistr ,  inbsel-prev_inbsel as inbseldistr
			from #cumulative_set
			where 
			prev_indistr=1 and indistr=1 and ( prev_insel!=insel  or  prev_inbsel!=inbsel  )

		--select * from olap_deldistr where comserno='IMP000000009354' and prodsern='IMP000000014524'  and deldate>=@prev_seldate and deldate<@seldate

		print @@rowcount



		update #cumulative_set
			set #cumulative_set.indistr=#cumulative_set.indistr+sum_indistr
			from #cumulative_set , (select comserno , prodsern , prodexpand , sum(indistr) as sum_indistr from olap_deldistr where deldate>=@prev_seldate and deldate<@seldate group by comserno , prodsern , prodexpand ) olap_deldistr
			where #cumulative_set.comserno=olap_deldistr.comserno and #cumulative_set.prodsern=olap_deldistr.prodsern  and #cumulative_set.prodexpand=olap_deldistr.prodexpand 



		set @prev_seldate=@seldate

		fetch next from temp_cursor into @seldate
	end

close temp_cursor
deallocate temp_cursor

drop table #cumulative_set












GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DPM]    Script Date: 01/09/2008 18:00:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO



















create procedure [spp].[proc_fill_OLAP_DPM]
AS



SET NOCOUNT ON
SET DATEFIRST 1




--------------------   DATE TO INSERT CUMULATIVE SUMS FROM  -----------------------------

DECLARE @DPM_START_DPMHDATE varchar(8)
DECLARE @AUDIT_START_DPMHDATE varchar(8)
DECLARE @START_DPMHDATE varchar(8)
DECLARE @LAST_DPMHDATE varchar(8)


SELECT @DPM_START_DPMHDATE=MIN(DPMHDATE) FROM 
TDPMHDR INNER JOIN TDPMENTR ON TDPMHDR.DPMHDSERN=TDPMENTR.DPMHDSERN
WHERE 
EXISTS(SELECT * FROM OLAP_DATE WHERE TDPMHDR.DPMHDATE=OLAP_DATE.DATE)
AND EXISTS(SELECT * FROM OLAP_STORE WHERE TDPMHDR.COMSERNO=OLAP_STORE.COMSERNO)
AND EXISTS(SELECT * FROM OLAP_PRODUCT WHERE TDPMENTR.PRODSERN=OLAP_PRODUCT.PRODSERN)
AND NOT EXISTS(SELECT * FROM OLAP_DPM_NOTEXP WHERE TDPMHDR.DPMHDATE=OLAP_DPM_NOTEXP.DPMHDATE)

SELECT @AUDIT_START_DPMHDATE=MIN(DPMHDATE) FROM TDPMHDR WHERE DPMHDSERN IN ( SELECT KEYSERN1 FROM OLAP_AUDIT WHERE KEYTYPE='DPMHDSERN' )
AND ISDATE(DPMHDATE)=1  AND DPMHDATE IS NOT NULL

IF @DPM_START_DPMHDATE IS NULL AND @AUDIT_START_DPMHDATE IS NULL
	RETURN
ELSE
IF @DPM_START_DPMHDATE IS NOT NULL AND @AUDIT_START_DPMHDATE IS NOT NULL
	IF @DPM_START_DPMHDATE<@AUDIT_START_DPMHDATE
		SET @START_DPMHDATE=@DPM_START_DPMHDATE
	ELSE
		SET @START_DPMHDATE=@AUDIT_START_DPMHDATE
ELSE
IF @DPM_START_DPMHDATE IS NOT NULL
	SET @START_DPMHDATE=@DPM_START_DPMHDATE
ELSE
IF @AUDIT_START_DPMHDATE IS NOT NULL
	SET @START_DPMHDATE=@AUDIT_START_DPMHDATE



print 'date to start: ' + @START_DPMHDATE

------------------------------------------------------------------------------------------


print 'delete from OLAP_DPM_TMP'
print getdate()

delete from OLAP_DPM_TMP where dpmhdate>=@START_DPMHDATE

print 'end'
print getdate()




print 'insert into OLAP_DPM_TMP'
print getdate()

insert into OLAP_DPM_TMP(comserno , prodsern , dpmhdate , 
			dpmecover , dpmefacing , dpmechan , dpmesalesp , dpmeavestp , dpmeprice_net , dpmeprice_gross ,
			dpmeinsel , dpmeinbsel , sel_placeholder ,  prev_dpmhdate)
select tdpmhdr.comserno , tdpmentr.prodsern , dpmhdate , 
(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0
	else    max(case when dpmecover='0' or dpmecover='1' then dpmecover else 0 end) 
end) as dpmecover ,

(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0.0
	else    max(isnull(dpmefacing,0.0)) 
end) as dpmefacing ,
(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0.0
	else    max(isnull(dpmechan,0.0)) 
end) as dpmechan ,
(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0.0
	else    max(isnull(dpmesalesp,0.0)) 
end) as dpmesalesp ,
(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0.0
	else    max(isnull(dpmeavestp,0.0)) 
end) as dpmeavestp ,
(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0.0
	else    max(
			ISNULL(CASE
				WHEN TDPMENTR.DPMEPRFLAG!='1' AND TDPMENTR.DPMEPRTAX!='1'  THEN DPMEPRICE				--per cons pkg without tax
				WHEN TDPMENTR.DPMEPRFLAG!='1' AND TDPMENTR.DPMEPRTAX='1'  THEN DPMEPRICE/(1+DPMETAX/100)		--per cons pkg with tax
				WHEN TDPMENTR.DPMEPRFLAG='1' AND TDPMENTR.DPMEPRTAX!='1'  THEN DPMEPRICE/(case isnull(DPMECSIZE,0) when 0 then 1 else DPMECSIZE end)	--per case without tax
				WHEN TDPMENTR.DPMEPRFLAG='1' AND TDPMENTR.DPMEPRTAX='1'  THEN (DPMEPRICE/(1+DPMETAX/100))/(case isnull(DPMECSIZE,0) when 0 then 1 else DPMECSIZE end)	--per case with tax
				END , 
			0.0) 
		    )
end) as dpmeprice_net ,
(case 
	when 	dpmhdate>=min(olap_store.cominact)	then 0.0
	else    max(
			ISNULL(CASE
				WHEN TDPMENTR.DPMEPRFLAG!='1' AND TDPMENTR.DPMEPRTAX!='1' THEN DPMEPRICE*(1+DPMETAX/100)		--per cons pkg without tax
				WHEN TDPMENTR.DPMEPRFLAG!='1' AND TDPMENTR.DPMEPRTAX='1' THEN DPMEPRICE					--per cons pkg with tax
				WHEN TDPMENTR.DPMEPRFLAG='1' AND TDPMENTR.DPMEPRTAX!='1' THEN (DPMEPRICE*(1+DPMETAX/100))/(case isnull(DPMECSIZE,0) when 0 then 1 else DPMECSIZE end)	--per case without tax
				WHEN TDPMENTR.DPMEPRFLAG='1' AND TDPMENTR.DPMEPRTAX='1' THEN DPMEPRICE/(case isnull(DPMECSIZE,0) when 0 then 1 else DPMECSIZE end)			--per case with tax
				END , 
			0.0) 
		    )
end) as dpmeprice_gross ,
0 as dpmeinsel,
0 as dpmeinbsel,
0 as sel_placeholder,
'00000000' as prev_dpmhdate
from 
tdpmhdr inner join tdpmentr on tdpmhdr.dpmhdsern=tdpmentr.dpmhdsern
inner join olap_store on tdpmhdr.comserno=olap_store.comserno
inner join olap_product on tdpmentr.prodsern=olap_product.prodsern
where dpmhdate>=@START_DPMHDATE
group by tdpmhdr.comserno , tdpmentr.prodsern , dpmhdate
OPTION (ORDER GROUP)



print 'end'
print getdate()






print 'update OLAP_DPM_TMP'
print getdate()


update OLAP_DPM_TMP
set 
-- NB!! until next update prev_dpmhdate is actually next_dpmhdate
prev_dpmhdate=IsNUll((select top 1 dpmhdate from OLAP_DPM_TMP t2 where OLAP_DPM_TMP.comserno=t2.comserno and OLAP_DPM_TMP.prodsern=t2.prodsern and OLAP_DPM_TMP.dpmhdate<t2.dpmhdate order by t2.dpmhdate asc) , '99999999') 
where dpmhdate>=@START_DPMHDATE


print 'end'
print getdate()


print 'insert into OLAP_DPM_TMP'
print getdate()


insert into OLAP_DPM_TMP(comserno , prodsern , dpmhdate , prev_dpmhdate , 
			dpmecover , dpmefacing , dpmechan , dpmesalesp , dpmeavestp , dpmeprice_net , dpmeprice_gross ,
			dpmeinsel , dpmeinbsel , sel_placeholder  )
select comserno , prodsern , dpmhdate , '00000000' as  prev_dpmhdate, 
			max(cast(dpmecover as tinyint)) , max(dpmefacing) , max(dpmechan) , max(dpmesalesp) , max(dpmeavestp) , 0.0 as dpmeprice_net , 0.0 as dpmeprice_gross ,
			0 as dpmeinsel , 0 as dpmeinbsel , 1 as sel_placeholder
from
(
select t1.comserno , t1.prodsern , t2.seldate as dpmhdate , 
t1.dpmecover , 
t1.dpmefacing , 
t1.dpmechan , 
t1.dpmesalesp , 
t1.dpmeavestp 
from OLAP_DPM_TMP t1 
inner join OLAP_SELECTION t2 on t1.comserno=t2.comserno and t1.prodsern=t2.prodsern and t1.dpmhdate<t2.seldate and t1.prev_dpmhdate>t2.seldate --don't forget , it's next_dpmhdate
where t1.dpmhdate>=@START_DPMHDATE

union all

select t1.comserno , t1.prodsern , t2.seldate as dpmhdate ,  
t1.dpmecover , 
t1.dpmefacing , 
t1.dpmechan , 
t1.dpmesalesp , 
t1.dpmeavestp 
from OLAP_DPM_TMP t1 
inner join OLAP_BASE_SELECTION t2 on t1.comserno=t2.comserno and t1.prodsern=t2.prodsern and t1.dpmhdate<t2.seldate and t1.prev_dpmhdate>t2.seldate --don't forget , it's next_dpmhdate
where t1.dpmhdate>=@START_DPMHDATE
) tbl
group by comserno , prodsern , dpmhdate


print 'end'
print getdate()




print 'update OLAP_DPM_TMP'
print getdate()


update OLAP_DPM_TMP
set 
prev_dpmhdate=IsNUll((select top 1 dpmhdate from OLAP_DPM_TMP t2 where OLAP_DPM_TMP.comserno=t2.comserno and OLAP_DPM_TMP.prodsern=t2.prodsern and OLAP_DPM_TMP.dpmhdate>t2.dpmhdate order by t2.dpmhdate desc) , '00000000') ,
dpmeinbsel=
(case IsNUll((select top 1 insel from olap_base_selection s2 where s2.comserno=OLAP_DPM_TMP.comserno and s2.prodsern=OLAP_DPM_TMP.prodsern and s2.seldate<=OLAP_DPM_TMP.dpmhdate order by seldate desc),0)
	when 1 then 1 
	else 0
end),
dpmeinsel=
(case IsNUll((select top 1 insel from olap_selection s2 where s2.comserno=OLAP_DPM_TMP.comserno and s2.prodsern=OLAP_DPM_TMP.prodsern and s2.seldate<=OLAP_DPM_TMP.dpmhdate order by seldate desc),0)
	when 1 then 1 
	else 0
end)
where dpmhdate>=@START_DPMHDATE 


print 'end'
print getdate()






print 'delete from OLAP_DPM_NOTEXP'
print getdate()

delete from OLAP_DPM_NOTEXP where dpmhdate>=@START_DPMHDATE

print 'end'
print getdate()



print 'insert into OLAP_DPM_NOTEXP'
print getdate()


insert into OLAP_DPM_NOTEXP(comserno , prodsern , dpmhdate , dpmeinsel_cum , dpmeinbsel_cum , 
				dpmefacing_cum , dpmechan_cum , dpmesalesp_cum , dpmeavestp_cum , dpmeprice_net , dpmeprice_gross ,
				dpmecover_cum , dpmeselcover_cum , dpmebselcover_cum , 
				dpmmeasured_cum , dpmbselmeasured_cum , dpmcount , prodexpand , prodexpand_intersect)
select t1.comserno, p1.prodsern, t1.dpmhdate, 
cast(t1.dpmeinsel as smallint)-cast(isnull(t2.dpmeinsel,0) as smallint) as dpmeinsel_cum,
cast(t1.dpmeinbsel as smallint)-cast(isnull(t2.dpmeinbsel,0) as smallint) as dpmeinbsel_cum,
t1.dpmefacing-isnull(t2.dpmefacing,0) as dpmefacing_cum,
t1.dpmechan-isnull(t2.dpmechan,0) as dpmechan_cum,
t1.dpmesalesp-isnull(t2.dpmesalesp,0) as dpmesalesp_cum,
t1.dpmeavestp-isnull(t2.dpmeavestp,0) as dpmeavestp_cum,
t1.dpmeprice_net as dpmeprice_net,
t1.dpmeprice_gross as dpmeprice_gross,
cast(t1.dpmecover as smallint)-cast(isnull(t2.dpmecover,0) as smallint)  as dpmecover_cum,
(case
	when (isnull(t2.dpmecover,0)=1 and isnull(t2.dpmeinsel,0)=1) and (t1.dpmecover=0 or t1.dpmeinsel=0)	then -1
	when (isnull(t2.dpmecover,0)=0 or isnull(t2.dpmeinsel,0)=0) and (t1.dpmecover=1 and t1.dpmeinsel=1)	then 1
	else 0
end) as dpmeselcover_cum ,
(case
	when (isnull(t2.dpmecover,0)=1 and isnull(t2.dpmeinbsel,0)=1) and (t1.dpmecover=0 or t1.dpmeinbsel=0)	then -1
	when (isnull(t2.dpmecover,0)=0 or isnull(t2.dpmeinbsel,0)=0) and (t1.dpmecover=1 and t1.dpmeinbsel=1)	then 1
	else 0
end) as dpmebselcover_cum ,
(case 
	when t1.prev_dpmhdate ='00000000' then 1
	else 0
end) as dpmmeasured_cum,
(case
	when isnull(t2.dpmeinbsel,0)=1 and t1.dpmeinbsel=0	then -1
	when isnull(t2.dpmeinbsel,0)=0 and t1.dpmeinbsel=1	then 1
	else 0
end) as dpmbselmeasured_cum,
(case
	when t1.sel_placeholder=1	then 0
	else 1
end ) as dpmcount,
prodexpand ,
prodexpand_intersect
from OLAP_DPM_TMP t1
inner join olap_lproprod p1 
	on t1.prodsern=p1.parent_prodsern
left outer join OLAP_DPM_TMP t2
	on t1.comserno=t2.comserno and t1.prodsern=t2.prodsern and t1.prev_dpmhdate=t2.dpmhdate
where t1.dpmhdate>=@START_DPMHDATE



print 'end'
print getdate()







print 'delete from OLAP_DPM_EXP'
print getdate()

delete from OLAP_DPM_EXP where dpmhdate>=@START_DPMHDATE

print 'end'
print getdate()

 

print 'insert into OLAP_DPM_EXP'
print getdate()


insert into OLAP_DPM_EXP(comserno , prodsern , dpmhdate , 
				prev_dpmecover_cum_sum , prev_dpmeinsel_cum_sum , prev_dpmeinbsel_cum_sum , 
				dpmmeasured_cum , dpmbselmeasured_cum , 
				cur_dpmecover_cum_sum , cur_dpmeinsel_cum_sum, cur_dpmeinbsel_cum_sum , 
				dpmecover_cum , dpmeselcover_cum , dpmebselcover_cum , 
				dpmefacing_cum ,dpmechan_cum ,dpmesalesp_cum , dpmeavestp_cum , dpmeprice_net , dpmeprice_gross ,
				dpmcount)
select comserno , prodsern , dpmhdate ,
isnull((select sum(dpmecover_cum) from OLAP_DPM_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.dpmhdate<t1.dpmhdate),0) 
as prev_dpmecover_cum_sum ,
isnull((select sum(dpmefacing_cum) from OLAP_DPM_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.dpmhdate<t1.dpmhdate),0) 
as prev_dpmeinsel_cum_sum ,
isnull((select sum(dpmeinbsel_cum) from OLAP_DPM_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.dpmhdate<t1.dpmhdate),0) 
as prev_dpmeinbsel_cum_sum ,
case
	when not exists(select * from OLAP_DPM_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.dpmhdate<t1.dpmhdate and dpmmeasured_cum=1)  then 1
	else 0
end as dpmmeasured_cum , --?!
0 as dpmbselmeasured_cum ,
sum(dpmecover_cum) as cur_dpmecover_cum_sum ,
sum(dpmeinsel_cum) as cur_dpmeinsel_cum_sum ,
sum(dpmeinbsel_cum) as cur_dpmeinbsel_cum_sum ,
0 as dpmecover_cum ,
0 as dpmeselcover_cum , 
0 as dpmebselcover_cum ,
sum(dpmefacing_cum) as dpmefacing_cum ,
sum(dpmechan_cum) as dpmechan_cum ,
sum(dpmesalesp_cum) as dpmesalesp_cum , 
sum(dpmeavestp_cum) as dpmeavestp_cum , 
avg(dpmeprice_net) as dpmeprice_net , 
avg(dpmeprice_gross) as dpmeprice_gross ,
case
	when sum(dpmcount)>0	then 1
	else 0
end as dpmcount
from 
OLAP_DPM_NOTEXP t1 
where prodexpand_intersect=2 and dpmhdate>=@START_DPMHDATE
group by comserno , prodsern , dpmhdate


print 'end'
print getdate()



print 'update OLAP_DPM_EXP'
print getdate()

update OLAP_DPM_EXP
set dpmecover_cum=
(case
	when prev_dpmecover_cum_sum>0 and prev_dpmecover_cum_sum+cur_dpmecover_cum_sum<=0	then -1
	when prev_dpmecover_cum_sum<=0 and prev_dpmecover_cum_sum+cur_dpmecover_cum_sum>0	then 1
	else 0
end) ,
dpmeselcover_cum=
(case
	when (prev_dpmecover_cum_sum>0 and prev_dpmeinsel_cum_sum>0) and ((prev_dpmecover_cum_sum+cur_dpmecover_cum_sum)<=0 or (prev_dpmeinsel_cum_sum+cur_dpmeinsel_cum_sum)<=0)	then -1
	when (prev_dpmecover_cum_sum<=0 or prev_dpmeinsel_cum_sum<=0) and ((prev_dpmecover_cum_sum+cur_dpmecover_cum_sum)>0 and (prev_dpmeinsel_cum_sum+cur_dpmeinsel_cum_sum)>0)	then 1
	else 0
end) ,
dpmebselcover_cum=
(case
	when (prev_dpmecover_cum_sum>0 and prev_dpmeinbsel_cum_sum>0) and ((prev_dpmecover_cum_sum+cur_dpmecover_cum_sum)<=0 or (prev_dpmeinbsel_cum_sum+cur_dpmeinbsel_cum_sum)<=0)	then -1
	when (prev_dpmecover_cum_sum<=0 or prev_dpmeinbsel_cum_sum<=0) and ((prev_dpmecover_cum_sum+cur_dpmecover_cum_sum)>0 and (prev_dpmeinbsel_cum_sum+cur_dpmeinbsel_cum_sum)>0)	then 1
	else 0
end) ,
dpmbselmeasured_cum=
(case
	when prev_dpmeinbsel_cum_sum>0 and (prev_dpmeinbsel_cum_sum+cur_dpmeinbsel_cum_sum)<=0	then -1
	when prev_dpmeinbsel_cum_sum<=0 and (prev_dpmeinbsel_cum_sum+cur_dpmeinbsel_cum_sum)>0	then 1
	else 0
end) 
where dpmhdate>=@START_DPMHDATE


print 'end'
print getdate()



DELETE FROM OLAP_AUDIT WHERE KEYTYPE='DPMHDSERN'













GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_DPM_SEL]    Script Date: 01/09/2008 18:00:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO















CREATE procedure [spp].[proc_fill_OLAP_DPM_SEL] 
@start_seldate varchar(8)
as
declare @seldate varchar(8)
declare @prev_seldate varchar(8)
declare @prev_prev_seldate varchar(8)
DECLARE @prev_time datetime
set nocount on 
create table #cumulative_set(comserno varchar(15), prodsern varchar(15) , prodexpand tinyint ,  indistr smallint , prev_indistr smallint , insel smallint , prev_insel smallint , inbsel smallint , prev_inbsel smallint , cur_set_sum_indistr smallint, cur_set_min_dpmhdate varchar(8))
create table #current_set(comserno varchar(15), prodsern varchar(15) , prodexpand tinyint  , min_dpmhdate varchar(8) , sum_indistr smallint)
SELECT @start_seldate=ISNULL(MAX(SELDATE) , '00000000')  FROM olap_seldate t2 WHERE SELDATE<=@start_seldate
print 'date to start from - ' + @start_seldate
delete from olap_dpm where dpmhdsern='sel_placeholder' and dpmhdate>=@start_seldate
print 'deleted - ' + cast(@@rowcount as varchar(15))
update olap_dpm set dpmeselcover_cum=0 ,  dpmebselcover_cum=0  where dpmhdate>=@start_seldate and (dpmeselcover_cum!=0 or  dpmebselcover_cum!=0)
print 'updated sel and bsel to 0 - ' + cast(@@rowcount as varchar(15))
--------------------- filling cumulative set -----------------------
IF @start_seldate!='00000000'
	BEGIN
		SET @prev_time=GETDATE()
		insert into #cumulative_set(comserno , prodsern , prodexpand, indistr , prev_indistr,  insel , prev_insel,  inbsel , prev_inbsel)
			select comserno , prodsern , prodexpand,  sum(dpmecover_cum) as  indistr, 0 as prev_indistr , 0  as insel , 0 as prev_insel  , 0  as inbsel , 0 as prev_inbsel from olap_dpm
			where dpmhdate<@start_seldate
			group by comserno , prodsern , prodexpand
		print '- getting initial set  took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
		SET @prev_time=GETDATE()
		UPDATE #cumulative_set
			SET INSEL=1
			FROM #cumulative_set WHERE EXISTS(SELECT * FROM OLAP_TSELENTR , OLAP_LCOMSEL WHERE 
			OLAP_LCOMSEL.COMSERNO=#cumulative_set.COMSERNO AND OLAP_TSELENTR.PRODSERN=#cumulative_set.PRODSERN AND
			OLAP_TSELENTR.SELSERN=OLAP_LCOMSEL.SELSERN AND SELESTART<=convert(varchar(8) , DATEADD(d , -1 , CONVERT(datetime , @start_seldate ) )  , 112)  AND SELEEND>=@start_seldate )
		print '- getting initial set  SEL  took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
		SET @prev_time=GETDATE()
		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN AND OLAP_BASE_SELECTION.SELDATE<@start_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, 0 ) --ISNULL end
			FROM  #cumulative_set 
		print '- getting initial set  BSEL  took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
	END

--------------------------------------------------------------------------
declare temp_cursor cursor for
select * from 
(
select seldate from olap_seldate 
union
select '00000000' as seldate
union
select '99999999' as seldate
)tbl
where seldate>=@start_seldate
order by seldate
open temp_cursor
fetch next from temp_cursor into @seldate
set @prev_seldate=@seldate
--set @prev_prev_seldate='00000000'
fetch next from temp_cursor into @seldate
while @@fetch_status=0
	begin
		print @prev_seldate + '-' + @seldate


		SET @prev_time=GETDATE()
		delete from #current_set
		insert into #current_set(comserno , prodsern , prodexpand , min_dpmhdate , sum_indistr)
		select comserno , prodsern , prodexpand , min(dpmhdate) as min_dpmhdate , sum(ISNULL(dpmecover_cum,0)) as sum_indistr from olap_dpm
			where dpmhdate>=@prev_seldate and dpmhdate<@seldate
			group by comserno , prodsern , prodexpand
		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- getting current_set  set took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'


		SET @prev_time=GETDATE()


		update #cumulative_set
			set prev_indistr=indistr , prev_insel=insel , prev_inbsel=inbsel , insel=0 , inbsel=0 , 
			cur_set_sum_indistr=0 , cur_set_min_dpmhdate=''

		update #cumulative_set
			set 
			#cumulative_set.indistr=#cumulative_set.indistr+#current_set.sum_indistr ,
			#cumulative_set.cur_set_sum_indistr=#current_set.sum_indistr ,
			#cumulative_set.cur_set_min_dpmhdate='' --#current_set.min_dpmhdate
			from #cumulative_set ,  #current_set
			where #cumulative_set.comserno=#current_set.comserno and #cumulative_set.prodsern=#current_set.prodsern  and #cumulative_set.prodexpand=#current_set.prodexpand 

		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- updating cumulative_set from current_set took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'


		SET @prev_time=GETDATE()
		insert into #cumulative_set(comserno , prodsern , prodexpand , indistr , prev_indistr,  insel , prev_insel,  inbsel , prev_inbsel , cur_set_sum_indistr , cur_set_min_dpmhdate )
			select comserno , prodsern , prodexpand , SUM_INDISTR as indistr, 0 as prev_indistr , 0  as insel , 0 as prev_insel , 0  as inbsel , 0 as prev_inbsel , sum_indistr as  cur_set_sum_indistr , min_dpmhdate as cur_set_min_dpmhdate  from #current_set
			where  not exists(select * from #cumulative_set t1 where t1.comserno=#current_set.comserno and t1.prodsern=#current_set.prodsern and t1.prodexpand=#current_set.prodexpand)
		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- inserting new records into cumulative_set took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'


		SET @prev_time=GETDATE()
		UPDATE #cumulative_set
			SET INSEL=1
			FROM #cumulative_set WHERE EXISTS(SELECT * FROM OLAP_TSELENTR , OLAP_LCOMSEL WHERE 
			OLAP_LCOMSEL.COMSERNO=#cumulative_set.COMSERNO AND OLAP_TSELENTR.PRODSERN=#cumulative_set.PRODSERN AND
			OLAP_TSELENTR.SELSERN=OLAP_LCOMSEL.SELSERN AND SELESTART<=@prev_seldate AND SELEEND>@prev_seldate )
		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- getting olap_dpm SEL took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
		SET @prev_time=GETDATE()
/*
		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN 
				AND OLAP_BASE_SELECTION.SELDATE<=@prev_seldate
				AND OLAP_BASE_SELECTION.SELDATE>@prev_prev_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, PREV_INBSEL ) 
			FROM  #cumulative_set 
*/
		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN AND OLAP_BASE_SELECTION.SELDATE<=@prev_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, 0 ) --ISNULL end
			FROM  #cumulative_set 
		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- getting olap_dpm BSEL took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'



		SET @prev_time=GETDATE()
		update olap_dpm
			set olap_dpm.dpmeselcover_cum=
			(case
				when olap_dpm.dpmecover_cum=1 and #cumulative_set.insel=1 then 1
				when olap_dpm.dpmecover_cum=-1 and #cumulative_set.insel=1 then -1
				else 0
			end) , 
			olap_dpm.dpmebselcover_cum=(case
				when olap_dpm.dpmecover_cum=1 and #cumulative_set.inbsel=1 then 1
				when olap_dpm.dpmecover_cum=-1 and #cumulative_set.inbsel=1 then -1
				else 0
			end) ,
			olap_dpm.dpmbselmeasured=(case
				when olap_dpm.dpmhdate=#cumulative_set.cur_set_min_dpmhdate 
					/* and  NOT(prev_indistr=1 and ( prev_insel!=insel  or  prev_inbsel!=inbsel  )) 
						and NOT(prev_indistr=0 and indistr=0) */
					/*and prev_indistr=0*/ then  inbsel /*-prev_inbsel*/ --because they are new in this set
				else 0
			end)
			from #cumulative_set ,  olap_dpm
			where #cumulative_set.comserno=olap_dpm.comserno and #cumulative_set.prodsern=olap_dpm.prodsern  and #cumulative_set.prodexpand=olap_dpm.prodexpand
			and dpmhdate>=@prev_seldate and dpmhdate<@seldate
		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- updating olap_dpm took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
		
/*
		SET @prev_time=GETDATE()
		update olap_dpm set dpmbselmeasured=inbsel-prev_inbsel
			from #cumulative_set , olap_dpm
			where #cumulative_set.comserno=olap_dpm.comserno and #cumulative_set.prodsern=olap_dpm.prodsern  and #cumulative_set.prodexpand=olap_dpm.prodexpand 
			and #cumulative_set.cur_set_min_dpmhdate=olap_dpm.dpmhdate
			and prev_inbsel!=inbsel  --!!!!!!!!
		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- inserting sel_placeholder(DMPEBSELMEASURED) took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
*/
		SET @prev_time=GETDATE()

		insert into olap_dpm(dpmhdsern , comserno , prodsern , prodexpand , dpmhdate , dpmmeasured , dpmecover_cum , dpmeselcover_cum ,  dpmebselcover_cum , dpmcount , dpmbselmeasured )
			select 'sel_placeholder'as dpmhdsern , #cumulative_set.comserno , #cumulative_set.prodsern , #cumulative_set.prodexpand , @prev_seldate as dpmhdate , 0  as dpmmeasured, 0 as indistr ,  
			case 
				when prev_indistr=1 then insel-prev_insel 
				else 0
			end as dpmeselcover_cum ,  
			case 
				when prev_indistr=1 then inbsel-prev_inbsel 
				else 0
			end as dpmebselcover_cum ,  
			0  as dpmcount ,  
			case 
				when cur_set_min_dpmhdate='' then inbsel-prev_inbsel 
				else 0
			end as dpmbselmeasured
			from #cumulative_set
			where 
			 ( prev_insel!=insel  or  prev_inbsel!=inbsel  )

		print '- affected-' + CAST(@@ROWCOUNT AS VARCHAR(15))
		print '- inserting sel_placeholder(DPMESELCOVER) took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
		--check--
		--select * from #cumulative_set  where comserno='IMP000000013352' and prodsern='IMP000000014435'
		--check--
		--select * from #current_set where comserno='IMP000000013352' and prodsern='IMP000000014435'
		--check--
		--select * from olap_dpm where comserno='IMP000000013352' and prodsern='IMP000000014435'   and dpmhdate>=@prev_seldate and dpmhdate<@seldate

		--set @prev_prev_seldate=@prev_seldate
		set @prev_seldate=@seldate
		fetch next from temp_cursor into @seldate
	end
close temp_cursor
deallocate temp_cursor
drop table #cumulative_set
drop table #current_set












GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_FIXTURE]    Script Date: 01/09/2008 18:00:28 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO


















CREATE PROCEDURE [spp].[proc_fill_OLAP_FIXTURE] AS


SET NOCOUNT ON 

/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_FIXTURE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_FIXTURE]
/***********************************/


/***********************************/
CREATE TABLE [spp].[OLAP_FIXTURE] (
	[FXTRSERN] [varchar] (15)   NOT NULL PRIMARY KEY ,
	[FXTRNAME] [varchar] (30) NOT NULL DEFAULT 'Undefined',
	[PARENT_FXTRNAME] [varchar] (30) NOT NULL DEFAULT 'Undefined'
) ON [PRIMARY]
/***********************************/

INSERT INTO OLAP_FIXTURE(FXTRSERN , FXTRNAME , PARENT_FXTRNAME)
	select  tfixture.fxtrsern, tfixture.fxtrname ,  IsNull(parent_fixture.fxtrname, 'Undefined')  as parent_fxtrname  from lfxtfxt
	inner join tfixture parent_fixture
	on lfxtfxt.fxtrsern1=parent_fixture.fxtrsern
	right outer join (select  tfixture.* from tfixture where fxtrsern in (select distinct fxtrsern from tplnentr)) tfixture
	on lfxtfxt.fxtrsern2=tfixture.fxtrsern

IF NOT EXISTS(SELECT * FROM OLAP_FIXTURE)
	INSERT INTO OLAP_FIXTURE(FXTRSERN , FXTRNAME , PARENT_FXTRNAME)
		VALUES('NODATA' , 'No Data', 'No Data')

SET NOCOUNT OFF














GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_LCOMPGR]    Script Date: 01/09/2008 18:00:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROCEDURE [spp].[proc_fill_OLAP_LCOMPGR] 
AS

SET NOCOUNT ON



----------------------------------------- OLAP_LCOMPGR and OLAP_LPROPGR TABLES----------------------------------------------





/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_LPROPGR]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_LPROPGR]
/***********************************/


/***********************************/
CREATE TABLE [spp].[OLAP_LPROPGR] (
	[PRODSERN] [varchar] (15)   NOT NULL  ,
	[PGRSERN] [varchar] (15)  NOT NULL ,
) ON [PRIMARY]
/***********************************/


/***********************************/
CREATE CLUSTERED INDEX IX_OLAP_LPROPGR ON spp.OLAP_LPROPGR(PRODSERN , PGRSERN)
/***********************************/










/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_LCOMPGR]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_LCOMPGR]
/***********************************/


/***********************************/
CREATE TABLE [spp].[OLAP_LCOMPGR] (
	[COMSERNO] [varchar] (15)   NOT NULL ,
	[PGRSERN] [varchar] (15)  NOT NULL ,
	[SALMSERN] [varchar] (15)  NOT NULL ,
	[DUMMY_LINK_HIERARCHY] [int] NOT NULL DEFAULT 0
) ON [PRIMARY]
/***********************************/

/***********************************/
CREATE CLUSTERED INDEX IX_OLAP_LCOMPGR
ON spp.OLAP_LCOMPGR(COMSERNO , PGRSERN)
/***********************************/







INSERT INTO OLAP_LPROPGR(PRODSERN , PGRSERN)
	SELECT PRODSERN , PGRSERN FROM
	LPROPGR 
	WHERE 
	EXISTS(SELECT TOP 1 1 FROM TPGROUPS WHERE TPGROUPS.PGRSERN=LPROPGR.PGRSERN)
	AND EXISTS(SELECT TOP 1 1 FROM TSALMAN WHERE TSALMAN.PGRSERN=LPROPGR.PGRSERN)
	AND EXISTS(SELECT TOP 1 1 FROM OLAP_PRODUCT WHERE OLAP_PRODUCT.PRODSERN=LPROPGR.PRODSERN)

INSERT INTO  OLAP_LPROPGR(PRODSERN , PGRSERN)
	SELECT PRODSERN , 'ALL' AS PGRSERN FROM OLAP_PRODUCT









INSERT INTO OLAP_LCOMPGR(COMSERNO , PGRSERN , SALMSERN)
	SELECT 
	COMSERNO , 
	CASE 
		WHEN EXISTS(SELECT * FROM TPGROUPS WHERE TPGROUPS.PGRSERN=TSALMAN.PGRSERN)	THEN TSALMAN.PGRSERN
		ELSE 'ALL'
	END AS PGRSERN , 
	TSALMAN.SALMSERN
	FROM LSALCOM 
	INNER JOIN TSALMAN ON LSALCOM.SALMSERN=TSALMAN.SALMSERN
	WHERE 
	EXISTS(SELECT  TOP 1 1  FROM OLAP_STORE WHERE OLAP_STORE.COMSERNO=LSALCOM.COMSERNO)
	AND EXISTS(SELECT  TOP 1 1  FROM OLAP_SALESFORCE WHERE OLAP_SALESFORCE.SALMSERN=TSALMAN.SALMSERN)









GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_LPROPROD]    Script Date: 01/09/2008 18:00:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE   procedure [spp].[proc_fill_OLAP_LPROPROD]
as
set nocount on
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--							COMPOUND PRODUCT LINK TABLE
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_LPROPROD]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_LPROPROD]
/***********************************/
/***********************************/
CREATE TABLE [spp].[OLAP_LPROPROD] (
	[PARENT_PRODSERN] [varchar] (15)   NOT NULL ,
	[PRODSERN] [varchar] (15)  NOT NULL ,
	[PRODCPG_MULT] real NOT NULL,
	[PRODCASE_MULT] real NOT NULL,
	[CHILDCASE_MULT] real NOT NULL,
	[PRODPALLET_MULT] real NOT NULL,
	[PRODUNIT_MULT] real NOT NULL,
	[PRODMONEY_MULT] real NOT NULL ,
	[PRODSIZE] [float]   NOT  NULL  DEFAULT 1 ,
	[PRODCPS] [float]  NOT  NULL DEFAULT 1  ,
	[PRODTAX] [float]  NOT  NULL DEFAULT 0  ,
	[PRODPALLET] [float]  NOT  NULL DEFAULT 1,
	[PRODPRICE] [float]  NOT  NULL DEFAULT 0,
	[PRODCPWNET] [float]  NOT  NULL DEFAULT 0,
	[PRODCPWGR] [float]  NOT  NULL DEFAULT 0,
	[PRODCASEWGR] [float]  NOT  NULL DEFAULT 0,
	[PRODEXPAND] tinyint NOT NULL,
	[PRODEXPAND_INTERSECT] tinyint NOT NULL DEFAULT 0
) ON [PRIMARY]
/***********************************/
/***********************************/
CREATE CLUSTERED INDEX IX_OLAP_LPROPROD
ON spp.OLAP_LPROPROD(PARENT_PRODSERN , PRODSERN , PRODEXPAND , PRODEXPAND_INTERSECT)
/***********************************/
DECLARE @STACK_PARENT_PRODSERN varchar(15)
DECLARE @STACK_PRODSERN varchar(15)
DECLARE @STACK_PRODPKGS int
DECLARE @STACK_COUNTER int
DECLARE @STACK_MULTIPLIER int
CREATE TABLE #stack(PARENT_PRODSERN varchar(15) , PRODSERN varchar(15) ,  PRODPKGS int)
INSERT INTO #stack
	SELECT PRODSERN1 , PRODSERN2 , PRODPKGS
	FROM LPROPROD WHERE EXISTS(SELECT * FROM TPRODUCT WHERE TPRODUCT.PRODSERN=PRODSERN1 AND TPRODUCT.PRODCOMP='1')
				AND  EXISTS(SELECT * FROM TPRODUCT WHERE TPRODUCT.PRODSERN=PRODSERN2)
				AND PRODPKGS>0 AND PRODSERN1<>PRODSERN2
------------------------------------------------------------------------ EXPANDING COMPOUND PRODUCTS -----------------------------------------------------------
SET @STACK_PRODSERN=NULL
SELECT TOP 1 @STACK_PARENT_PRODSERN=stack.PARENT_PRODSERN , @STACK_PRODSERN=stack.PRODSERN , @STACK_PRODPKGS=stack.PRODPKGS FROM #stack stack  WHERE
			EXISTS(SELECT * FROM #stack stack2 WHERE stack.PRODSERN=stack2.PARENT_PRODSERN) 
WHILE @STACK_PRODSERN IS NOT NULL
	BEGIN
		DELETE FROM #stack WHERE PARENT_PRODSERN=@STACK_PARENT_PRODSERN AND PRODSERN=@STACK_PRODSERN
		SET @STACK_COUNTER=0
		WHILE @STACK_COUNTER<@STACK_PRODPKGS
			BEGIN
				INSERT INTO #stack (PARENT_PRODSERN , PRODSERN , PRODPKGS)
					SELECT @STACK_PARENT_PRODSERN AS PARENT_PRODSERN , PRODSERN , PRODPKGS FROM #stack stack2 
					WHERE PARENT_PRODSERN=@STACK_PRODSERN
				SET @STACK_COUNTER=@STACK_COUNTER+1
			END
		SET @STACK_PRODSERN=NULL
		SELECT TOP 1 @STACK_PARENT_PRODSERN=stack.PARENT_PRODSERN , @STACK_PRODSERN=stack.PRODSERN , @STACK_PRODPKGS=stack.PRODPKGS FROM #stack stack  WHERE
					EXISTS(SELECT * FROM #stack stack2 WHERE stack.PRODSERN=stack2.PARENT_PRODSERN) 
	END
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
INSERT INTO OLAP_LPROPROD(PARENT_PRODSERN , PRODSERN ,  PRODCPG_MULT , PRODCASE_MULT, PRODPALLET_MULT, PRODUNIT_MULT , PRODMONEY_MULT , PRODEXPAND)
	SELECT 
			STACK.PARENT_PRODSERN , 
			STACK.PRODSERN ,
			STACK.PRODPKGS AS PRODCPG_MULT , 
			STACK.PRODPKGS/CHILD.PRODSIZE AS PRODCASE_MULT,
			STACK.PRODPKGS/(CHILD.PRODSIZE*CHILD.PRODPALLET) AS PRODPALLET_MULT,
			STACK.PRODPKGS*CHILD.PRODCPS AS PRODUNIT_MULT , 
			0 AS PRODMONEY_MULT , 
			2 AS PRODEXPAND 
	FROM #stack STACK , OLAP_PRODUCT PARENT , OLAP_PRODUCT CHILD
	WHERE STACK.PARENT_PRODSERN=PARENT.PRODSERN  AND STACK.PRODSERN=CHILD.PRODSERN
INSERT INTO OLAP_LPROPROD(PARENT_PRODSERN , PRODSERN , PRODCPG_MULT , PRODCASE_MULT, PRODPALLET_MULT, PRODUNIT_MULT , PRODMONEY_MULT , PRODEXPAND)
	SELECT 
			STACK.PARENT_PRODSERN AS PARENT_PRODSERN , 
			STACK.PARENT_PRODSERN AS PRODSERN , 
			SUM(STACK.PRODPKGS) AS PRODCPG_MULT , 
			SUM(STACK.PRODPKGS/CHILD.PRODSIZE) AS PRODCASE_MULT,
			1/MAX(PARENT.PRODPALLET) AS PRODPALLET_MULT,
			SUM(STACK.PRODPKGS*CHILD.PRODCPS) AS PRODUNIT_MULT , 
			1 AS PRODMONEY_MULT , 
			1 AS PRODEXPAND 
	FROM #stack STACK , OLAP_PRODUCT PARENT , OLAP_PRODUCT CHILD
	WHERE STACK.PARENT_PRODSERN=PARENT.PRODSERN  AND STACK.PRODSERN=CHILD.PRODSERN
	GROUP BY STACK.PARENT_PRODSERN
*/

/* EXPANDABLE PRODUCTS - EXPANDED */
INSERT INTO OLAP_LPROPROD(PARENT_PRODSERN , PRODSERN ,  PRODSIZE , PRODCPS, PRODTAX , PRODPALLET, PRODPRICE, PRODCPWNET, PRODCPWGR, PRODCASEWGR , PRODCPG_MULT , PRODCASE_MULT, CHILDCASE_MULT, PRODPALLET_MULT, PRODUNIT_MULT , PRODMONEY_MULT , PRODEXPAND)
	SELECT 
			STACK.PARENT_PRODSERN , 
			STACK.PRODSERN ,
			CHILD.PRODSIZE AS PRODSIZE ,
			CHILD.PRODCPS AS PRODCPS ,
			CHILD.PRODTAX AS PRODTAX ,
			CHILD.PRODPALLET AS PRODPALLET ,
			CHILD.PRODPRICE AS PRODPRICE ,
			CHILD.PRODCPWNET AS PRODCPWNET ,		
			CHILD.PRODCPWGR AS PRODCPWGR ,
			CHILD.PRODCASEWGR AS PRODCASEWGR ,	
			CAST(STACK.PRODPKGS as real)/ISNULL((SELECT SUM(PRODPKGS) FROM #stack t1 WHERE t1.PARENT_PRODSERN=STACK.PARENT_PRODSERN),1) AS PRODCPG_MULT , 
			STACK.PRODPKGS/CHILD.PRODSIZE AS PRODCASE_MULT,
			1 AS CHILDCASE_MULT,
			STACK.PRODPKGS/(CHILD.PRODSIZE*CHILD.PRODPALLET) AS PRODPALLET_MULT,
			STACK.PRODPKGS*CHILD.PRODCPS AS PRODUNIT_MULT , 
			0 AS PRODMONEY_MULT , 
			2 AS PRODEXPAND 
	FROM #stack STACK , OLAP_PRODUCT PARENT , OLAP_PRODUCT CHILD
	WHERE STACK.PARENT_PRODSERN=PARENT.PRODSERN  AND STACK.PRODSERN=CHILD.PRODSERN

/* EXPANDABLE PRODUCTS - NOT EXPANDED */
INSERT INTO OLAP_LPROPROD(PARENT_PRODSERN , PRODSERN ,  PRODSIZE , PRODCPS, PRODTAX , PRODPALLET, PRODPRICE, PRODCPWNET, PRODCPWGR, PRODCASEWGR , PRODCPG_MULT , PRODCASE_MULT, CHILDCASE_MULT, PRODPALLET_MULT, PRODUNIT_MULT , PRODMONEY_MULT, PRODEXPAND)
	SELECT 
			STACK.PARENT_PRODSERN AS PARENT_PRODSERN , 
			STACK.PARENT_PRODSERN AS PRODSERN , 
			SUM(STACK.PRODPKGS)  AS PRODSIZE ,
			MAX(PARENT.PRODCPS) AS PRODCPS ,
			MAX(PARENT.PRODTAX) AS PRODTAX ,  
			MAX(PARENT.PRODPALLET) AS PRODPALLET ,
			MAX(PARENT.PRODPRICE) AS PRODPRICE ,
			MAX(PARENT.PRODCPWNET) AS PRODCPWNET ,
			MAX(PARENT.PRODCPWGR) AS PRODCPWGR ,
			MAX(PARENT.PRODCASEWGR) AS PRODCASEWGR ,
			1 AS PRODCPG_MULT , 
			1 AS PRODCASE_MULT,
			SUM(STACK.PRODPKGS/CHILD.PRODSIZE) AS CHILDCASE_MULT, -- sum of cases is calc from child product cases
			1 AS PRODPALLET_MULT,
			1 AS PRODUNIT_MULT , 
			1 AS PRODMONEY_MULT , 
			1 AS PRODEXPAND 
	FROM #stack STACK , OLAP_PRODUCT PARENT , OLAP_PRODUCT CHILD
	WHERE STACK.PARENT_PRODSERN=PARENT.PRODSERN  AND STACK.PRODSERN=CHILD.PRODSERN
	GROUP BY STACK.PARENT_PRODSERN

/* NOT EXPANDABLE PRODUCTS */
INSERT INTO OLAP_LPROPROD(PARENT_PRODSERN , PRODSERN ,  PRODSIZE , PRODCPS, PRODTAX , PRODPALLET , PRODPRICE, PRODCPWNET, PRODCPWGR, PRODCASEWGR, PRODCPG_MULT , PRODCASE_MULT, CHILDCASE_MULT, PRODPALLET_MULT, PRODUNIT_MULT , PRODMONEY_MULT, PRODEXPAND)
	SELECT 
		PRODSERN AS PARENT_PRODSERN , 
		PRODSERN AS PRODSERN ,
		PRODSIZE AS PRODSIZE ,
		PRODCPS AS PRODCPS ,
		PRODTAX AS PRODTAX ,
		PRODPALLET AS PRODPALLET ,
		PRODPRICE AS PRODPRICE ,
		PRODCPWNET AS PRODCPWNET ,
		PRODCPWGR AS PRODCPWGR ,
		PRODCASEWGR AS PRODCASEWGR ,
		1 AS PRODCPG_MULT , 
		1 AS PRODCASE_MULT , 
		1 AS CHILDCASE_MULT , 
		1 AS PRODPALLET_MULT, 
		1 AS PRODUNIT_MULT , 
		1 AS   PRODMONEY_MULT, 
		0  AS PRODEXPAND 
	FROM OLAP_PRODUCT
		WHERE NOT EXISTS(SELECT *  FROM #stack stack WHERE stack.PARENT_PRODSERN=OLAP_PRODUCT.PRODSERN)

DROP TABLE #stack

UPDATE OLAP_LPROPROD
	SET PRODEXPAND_INTERSECT=2
	WHERE PARENT_PRODSERN IN (SELECT PRODSERN FROM OLAP_LPROPROD t2 WHERE PRODEXPAND=2)

UPDATE OLAP_LPROPROD
	SET PRODEXPAND_INTERSECT=PRODEXPAND
	WHERE PRODEXPAND IN (1,2)
/*
UPDATE OLAP_LPROPROD
	SET 
	OLAP_LPROPROD.PRODSIZE=OLAP_PRODUCT.PRODSIZE ,
	OLAP_LPROPROD.PRODCPS=OLAP_PRODUCT.PRODCPS ,
	OLAP_LPROPROD.PRODTAX=OLAP_PRODUCT.PRODTAX ,
	OLAP_LPROPROD.PRODPALLET=OLAP_PRODUCT.PRODPALLET 
	FROM OLAP_PRODUCT , OLAP_LPROPROD
	WHERE OLAP_LPROPROD.PRODSERN=OLAP_PRODUCT.PRODSERN
*/






GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_ORDDISTR]    Script Date: 01/09/2008 18:00:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO















CREATE PROCEDURE [spp].[proc_fill_OLAP_ORDDISTR]
AS


SET NOCOUNT ON
SET DATEFIRST 1




--------------------   DATE TO INSERT CUMULATIVE SUMS FROM  -----------------------------

DECLARE @ORD_START_ORDDDATE varchar(8)
DECLARE @AUDIT_START_ORDDDATE varchar(8)
DECLARE @START_ORDDDATE varchar(8)
DECLARE @LAST_ORDDDATE varchar(8)


SELECT @ORD_START_ORDDDATE=MIN(ORDDDATE) FROM 
TORDER INNER JOIN TORDENTR ON TORDER.ORDSERN=TORDENTR.ORDSERN
WHERE 
EXISTS(SELECT * FROM OLAP_DATE WHERE TORDER.ORDDDATE=OLAP_DATE.DATE)
AND EXISTS(SELECT * FROM OLAP_STORE WHERE TORDER.COMSERNO=OLAP_STORE.COMSERNO)
AND EXISTS(SELECT * FROM OLAP_PRODUCT WHERE TORDENTR.PRODSERN=OLAP_PRODUCT.PRODSERN)
AND NOT EXISTS(SELECT * FROM OLAP_ORDDISTR_NOTEXP WHERE TORDER.ORDDDATE=OLAP_ORDDISTR_NOTEXP.ORDDDATE)
AND ORDDDATE<=convert(varchar(8) , DATEADD(m , 2  , GETDATE() )  , 112) 

SELECT @AUDIT_START_ORDDDATE=MIN(ORDDDATE) FROM TORDER WHERE ORDSERN IN ( SELECT KEYSERN1 FROM OLAP_AUDIT WHERE KEYTYPE='ORDSERN' )
AND EXISTS(SELECT * FROM OLAP_DATE WHERE TORDER.ORDDDATE=OLAP_DATE.DATE)

IF @ORD_START_ORDDDATE IS NULL AND @AUDIT_START_ORDDDATE IS NULL
	RETURN
ELSE
IF @ORD_START_ORDDDATE IS NOT NULL AND @AUDIT_START_ORDDDATE IS NOT NULL
	IF @ORD_START_ORDDDATE<@AUDIT_START_ORDDDATE
		SET @START_ORDDDATE=@ORD_START_ORDDDATE
	ELSE
		SET @START_ORDDDATE=@AUDIT_START_ORDDDATE
ELSE
IF @ORD_START_ORDDDATE IS NOT NULL
	SET @START_ORDDDATE=@ORD_START_ORDDDATE
ELSE
IF @AUDIT_START_ORDDDATE IS NOT NULL
	SET @START_ORDDDATE=@AUDIT_START_ORDDDATE



print 'date to start: ' + @START_ORDDDATE

------------------------------------------------------------------------------------------






print 'DELETE FROM OLAP_ORDDISTR_TMP'
print getdate()

DELETE FROM OLAP_ORDDISTR_TMP WHERE ORDDDATE>=@START_ORDDDATE

print 'end'
print getdate()




print 'insert into OLAP_ORDDISTR_TMP'
print getdate()


insert into OLAP_ORDDISTR_TMP(comserno , prodsern , ordddate , rangestart_date , outofstock_date , wrkdays_in_range , orddmonth , wrkday_serno , ordevol )
select
comserno ,
prodsern ,
ordddate ,
convert(varchar(8) , DATEADD(m , -12 , CONVERT(datetime , ordddate ) )  , 112) as rangestart_date ,
'' as outofstock_date ,
0 as wrkdays_in_range ,
'' as orddmonth,
max(olap_date.wrkday_serno) as wrkday_serno ,
sum(ISNULL(ordevol,0)) as ordevol
from torder inner join tordentr on torder.ordsern=tordentr.ordsern
inner join olap_date on olap_date.date=torder.ordddate
where exists(select * from olap_store where olap_store.comserno=torder.comserno)
and exists(select * from olap_product where olap_product.prodsern=tordentr.prodsern)
and ORDDDATE>=@START_ORDDDATE
group by comserno , prodsern , ordddate


print 'end'
print getdate()




print 'UPDATE OLAP_ORDDISTR_TMP'
print getdate()

UPDATE OLAP_ORDDISTR_TMP
SET wrkdays_in_range=wrkday_serno-(SELECT TOP 1 WRKDAY_SERNO FROM OLAP_ORDDISTR_TMP t2 WHERE t2.comserno=OLAP_ORDDISTR_TMP.comserno and t2.prodsern=OLAP_ORDDISTR_TMP.prodsern and t2.ordddate>=OLAP_ORDDISTR_TMP.rangestart_date ORDER BY t2.ordddate ASC)
WHERE ORDDDATE>=@START_ORDDDATE

print 'end'
print getdate()






print 'UPDATE OLAP_ORDDISTR_TMP'
print getdate()

DECLARE @MAX_DATE char(8)

SELECT @MAX_DATE=MAX(DATE) FROM OLAP_DATE


UPDATE OLAP_ORDDISTR_TMP
SET
OUTOFSTOCK_DATE=ISNULL(
(SELECT MIN(DATE) FROM OLAP_DATE d1 WHERE d1.WRKDAY_SERNO=(OLAP_ORDDISTR_TMP.WRKDAY_SERNO+ceiling( (case when OLAP_ORDDISTR_TMP.wrkdays_in_range>0 then OLAP_ORDDISTR_TMP.wrkdays_in_range else 21 end)*(OLAP_ORDDISTR_TMP.ordevol*0.8)/isnull((select case when sum(t2.ordevol)=0 then NULL else sum(t2.ordevol) end from OLAP_ORDDISTR_TMP t2 
	where t2.comserno=OLAP_ORDDISTR_TMP.comserno and t2.prodsern=OLAP_ORDDISTR_TMP.prodsern and t2.ordddate<OLAP_ORDDISTR_TMP.ordddate and t2.ordddate>=OLAP_ORDDISTR_TMP.rangestart_date) , case when OLAP_ORDDISTR_TMP.ordevol=0 then 1 else (OLAP_ORDDISTR_TMP.ordevol*0.8) end )) 
)
) , @MAX_DATE )
WHERE ORDDDATE>=@START_ORDDDATE

print 'end'
print getdate()





print 'DELETE FROM OLAP_ORDDISTR_TMP2'
print getdate()

DELETE FROM OLAP_ORDDISTR_TMP2 WHERE ORDDDATE>=@START_ORDDDATE

print 'end'
print getdate()




print 'INSERT INTO OLAP_ORDDISTR_TMP2'
print getdate()


INSERT INTO OLAP_ORDDISTR_TMP2 (
COMSERNO ,
PRODSERN , 
ORDDDATE ,
PREV_ORDDDATE ,
INDISTR ,
INBSEL ,  
INSEL
)
select 
comserno , 
prodsern , 
ordddate , 
'00000000' as prev_ordddate , 
case 
	when sum(indistr)=0	then 0
	else 1
end as indistr , 
0 as inbsel,
0 as insel
from
(
select comserno , prodsern , ordddate , 1 as indistr 
from OLAP_ORDDISTR_TMP t1 where not exists(select * from OLAP_ORDDISTR_TMP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.ordddate<t1.ordddate and t1.ordddate>t2.outofstock_date)
and ORDDDATE>=@START_ORDDDATE

union all

select comserno , prodsern , outofstock_date as ordddate , 0 as indistr 
from OLAP_ORDDISTR_TMP t1 where not exists(select * from OLAP_ORDDISTR_TMP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.ordddate<t1.outofstock_date and t1.outofstock_date>t2.outofstock_date)
and outofstock_date>=@START_ORDDDATE
)tbl
group by comserno , prodsern , ordddate

print 'end'
print getdate()





print 'insert into OLAP_ORDDISTR_TMP2'
print getdate()


insert into OLAP_ORDDISTR_TMP2(
COMSERNO ,
PRODSERN , 
ORDDDATE ,
PREV_ORDDDATE ,
INDISTR ,
INBSEL ,  
INSEL 
)
select 
comserno , 
prodsern , 
seldate as ORDDDATE , 
'00000000' as prev_ordddate , 
(select top 1 indistr from OLAP_ORDDISTR_TMP2 d2 where d2.comserno=s1.comserno and d2.prodsern=s1.prodsern and d2.ordddate<s1.seldate order by ordddate desc) as indistr ,
0 as inbsel,
0 as insel 
from 
(

select comserno , prodsern , seldate from olap_selection s1
where seldate>=@START_ORDDDATE
and exists(select * from  OLAP_ORDDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.ordddate<s1.seldate)
	and not exists(select * from  OLAP_ORDDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.ordddate=s1.seldate)

union

select comserno , prodsern , seldate from olap_base_selection s1
where seldate>=@START_ORDDDATE
and exists(select * from  OLAP_ORDDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.ordddate<s1.seldate)
	and not exists(select * from  OLAP_ORDDISTR_TMP2 d1 where d1.comserno=s1.comserno and d1.prodsern=s1.prodsern and d1.ordddate=s1.seldate)
) s1


print 'end'
print getdate()






print 'update OLAP_ORDDISTR_TMP2'
print getdate()


update OLAP_ORDDISTR_TMP2
set
prev_ordddate=IsNUll(
(select top 1 ordddate from OLAP_ORDDISTR_TMP2 t2 where OLAP_ORDDISTR_TMP2.comserno=t2.comserno and OLAP_ORDDISTR_TMP2.prodsern=t2.prodsern and OLAP_ORDDISTR_TMP2.ordddate>t2.ordddate order by t2.ordddate desc)  
, '00000000') ,
inbsel=(case IsNUll((select top 1 insel from olap_base_selection s2 where s2.comserno=OLAP_ORDDISTR_TMP2.comserno and s2.prodsern=OLAP_ORDDISTR_TMP2.prodsern and s2.seldate<=OLAP_ORDDISTR_TMP2.ordddate order by seldate desc),0)
	when 1 then 1
	else 0
end),
insel=(case IsNUll((select top 1 insel from olap_selection s2 where s2.comserno=OLAP_ORDDISTR_TMP2.comserno and s2.prodsern=OLAP_ORDDISTR_TMP2.prodsern and s2.seldate<=OLAP_ORDDISTR_TMP2.ordddate order by seldate desc),0)
	when 1 then 1
	else 0
end) 
WHERE ORDDDATE>=@START_ORDDDATE

print 'end'
print getdate()







print 'DELETE FROM OLAP_ORDDISTR_NOTEXP'
print getdate()

DELETE FROM OLAP_ORDDISTR_NOTEXP WHERE ORDDDATE>=@START_ORDDDATE

print 'end'
print getdate()







print 'insert into OLAP_ORDDISTR_NOTEXP'
print getdate()


insert into OLAP_ORDDISTR_NOTEXP(comserno , prodsern , ordddate , insel_cum , inbsel_cum , indistr_cum , inseldistr_cum , inbseldistr_cum , prodexpand , prodexpand_intersect)
select t1.comserno, p1.prodsern, t1.ordddate, 
(case
	when isnull(t2.insel,0)=1 and t1.insel=0	then -1
	when isnull(t2.insel,0)=0 and t1.insel=1	then 1
	else 0
end) as insel_cum,
(case
	when isnull(t2.inbsel,0)=1 and t1.inbsel=0	then -1
	when isnull(t2.inbsel,0)=0 and t1.inbsel=1	then 1
	else 0
end) as inbsel_cum,
(case
	when isnull(t2.indistr,0)=1 and t1.indistr=0	then -1
	when isnull(t2.indistr,0)=0 and t1.indistr=1	then 1
	else 0
end) as indistr_cum,
(case
	when (isnull(t2.indistr,0)=1 and isnull(t2.insel,0)=1) and (t1.indistr=0 or t1.insel=0)	then -1
	when (isnull(t2.indistr,0)=0 or isnull(t2.insel,0)=0) and (t1.indistr=1 and t1.insel=1)	then 1
	else 0
end) as inseldistr_cum ,
(case
	when (isnull(t2.indistr,0)=1 and isnull(t2.inbsel,0)=1) and (t1.indistr=0 or t1.inbsel=0)	then -1
	when (isnull(t2.indistr,0)=0 or isnull(t2.inbsel,0)=0) and (t1.indistr=1 and t1.inbsel=1)	then 1
	else 0
end) as inbseldistr_cum ,
prodexpand ,
prodexpand_intersect
from OLAP_ORDDISTR_TMP2 t1
inner join olap_lproprod p1 
	on t1.prodsern=p1.parent_prodsern
left outer join OLAP_ORDDISTR_TMP2 t2
	on t1.comserno=t2.comserno and t1.prodsern=t2.prodsern and t1.prev_ordddate=t2.ordddate
where t1.ordddate>=@START_ORDDDATE



print 'end'
print getdate()








print 'delete from OLAP_ORDDISTR_EXP'
print getdate()

delete from OLAP_ORDDISTR_EXP where ordddate>=@START_ORDDDATE

print 'end'
print getdate()

 



print 'insert into OLAP_ORDDISTR_EXP'
print getdate()


insert into OLAP_ORDDISTR_EXP(comserno , prodsern , ordddate , 
				prev_indistr_cum_sum , prev_insel_cum_sum , prev_inbsel_cum_sum , 
				cur_indistr_cum_sum , cur_insel_cum_sum, cur_inbsel_cum_sum , 
				indistr_cum , inseldistr_cum , inbseldistr_cum)
select comserno , prodsern , ordddate ,
isnull((select sum(indistr_cum) from OLAP_ORDDISTR_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.ordddate<t1.ordddate),0) 
as prev_indistr_cum_sum ,
isnull((select sum(insel_cum) from OLAP_ORDDISTR_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.ordddate<t1.ordddate),0) 
as prev_insel_cum_sum ,
isnull((select sum(inbsel_cum) from OLAP_ORDDISTR_NOTEXP t2 where t2.comserno=t1.comserno and t2.prodsern=t1.prodsern and t2.ordddate<t1.ordddate),0) 
as prev_inbsel_cum_sum ,
sum(indistr_cum) as cur_indistr_cum_sum ,
sum(insel_cum) as cur_insel_cum_sum ,
sum(inbsel_cum) as cur_inbsel_cum_sum ,
0 as indistr_cum ,
0 as inseldistr_cum , 
0 as inbseldistr_cum 
from 
OLAP_ORDDISTR_NOTEXP t1 
where prodexpand_intersect=2 and ordddate>=@START_ORDDDATE
group by comserno , prodsern , ordddate


print 'end'
print getdate()




print 'update OLAP_ORDDISTR_EXP'
print getdate()

update OLAP_ORDDISTR_EXP
set indistr_cum=
(case
	when prev_indistr_cum_sum>0 and prev_indistr_cum_sum+cur_indistr_cum_sum<=0	then -1
	when prev_indistr_cum_sum<=0 and prev_indistr_cum_sum+cur_indistr_cum_sum>0	then 1
	else 0
end) ,
inseldistr_cum=
(case
	when (prev_indistr_cum_sum>0 and prev_insel_cum_sum>0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)<=0 or (prev_insel_cum_sum+cur_insel_cum_sum)<=0)	then -1
	when (prev_indistr_cum_sum<=0 or prev_insel_cum_sum<=0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)>0 and (prev_insel_cum_sum+cur_insel_cum_sum)>0)	then 1
	else 0
end) ,
inbseldistr_cum=
(case
	when (prev_indistr_cum_sum>0 and prev_inbsel_cum_sum>0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)<=0 or (prev_inbsel_cum_sum+cur_inbsel_cum_sum)<=0)	then -1
	when (prev_indistr_cum_sum<=0 or prev_inbsel_cum_sum<=0) and ((prev_indistr_cum_sum+cur_indistr_cum_sum)>0 and (prev_inbsel_cum_sum+cur_inbsel_cum_sum)>0)	then 1
	else 0
end)
where ordddate>=@START_ORDDDATE


print 'end'
print getdate()


DELETE FROM OLAP_AUDIT WHERE KEYTYPE='ORDSERN'













GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_ORDDISTR_SEL]    Script Date: 01/09/2008 18:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO















CREATE procedure [spp].[proc_fill_OLAP_ORDDISTR_SEL] 
@start_seldate varchar(8)
as

declare @seldate varchar(8)
declare @prev_seldate varchar(8)

set nocount on 


create table #cumulative_set(comserno varchar(15), prodsern varchar(15) , prodexpand tinyint , indistr smallint , prev_indistr smallint , insel smallint , prev_insel smallint , inbsel smallint , prev_inbsel smallint)



SELECT @start_seldate=ISNULL(MAX(SELDATE) , '00000000')  FROM olap_seldate t2 WHERE SELDATE<=@start_seldate



delete from olap_orddistr where indistr=0 and (inseldistr!=0  or inbseldistr!=0) and ordddate>=@start_seldate
update olap_orddistr set inseldistr=0 ,  inbseldistr=0  where ordddate>=@start_seldate




--------------------- filling cumulative set -----------------------

IF @start_seldate!='00000000'
	BEGIN

		insert into #cumulative_set(comserno , prodsern , prodexpand , indistr , prev_indistr,  insel , prev_insel,  inbsel , prev_inbsel)
			select comserno , prodsern , prodexpand , sum(indistr) as  indistr, 0 as prev_indistr , 0  as insel , 0 as prev_insel  , 0  as inbsel , 0 as prev_inbsel from olap_orddistr
			where ordddate<@start_seldate
			group by comserno , prodsern , prodexpand
		
		
		
		UPDATE #cumulative_set
			SET INSEL=1
			FROM #cumulative_set WHERE EXISTS(SELECT * FROM OLAP_TSELENTR , OLAP_LCOMSEL WHERE 
			OLAP_LCOMSEL.COMSERNO=#cumulative_set.COMSERNO AND OLAP_TSELENTR.PRODSERN=#cumulative_set.PRODSERN AND
			OLAP_TSELENTR.SELSERN=OLAP_LCOMSEL.SELSERN AND SELESTART<=convert(varchar(8) , DATEADD(d , -1 , CONVERT(datetime , @start_seldate ) )  , 112)  AND SELEEND>=@start_seldate )
		
		
		
		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN AND OLAP_BASE_SELECTION.SELDATE<@start_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, 0 ) --ISNULL end
			FROM  #cumulative_set 
	END

--------------------------------------------------------------------------




declare temp_cursor cursor for
select * from 
(
select seldate from olap_seldate 
union
select '00000000' as seldate
union
select '99999999' as seldate
)tbl
where seldate>=@start_seldate
order by seldate

open temp_cursor


fetch next from temp_cursor into @seldate

set @prev_seldate=@seldate


fetch next from temp_cursor into @seldate

while @@fetch_status=0
	begin
		print @prev_seldate + '-' + @seldate

		update #cumulative_set
			set prev_indistr=indistr , prev_insel=insel , prev_inbsel=inbsel , insel=0 , inbsel=0



		insert into #cumulative_set(comserno , prodsern , prodexpand , indistr , prev_indistr,  insel , prev_insel,  inbsel , prev_inbsel)
			select comserno , prodsern , prodexpand , 0 as indistr, 0 as prev_indistr , 0  as insel , 0 as prev_insel , 0  as inbsel , 0 as prev_inbsel from olap_orddistr
			where ordddate>=@prev_seldate and ordddate<@seldate
			and not exists(select * from #cumulative_set t1 where t1.comserno=olap_orddistr.comserno and t1.prodsern=olap_orddistr.prodsern and t1.prodexpand=olap_orddistr.prodexpand)
			group by comserno , prodsern , prodexpand



		UPDATE #cumulative_set
			SET INSEL=1
			FROM #cumulative_set WHERE EXISTS(SELECT * FROM OLAP_TSELENTR , OLAP_LCOMSEL WHERE 
			OLAP_LCOMSEL.COMSERNO=#cumulative_set.COMSERNO AND OLAP_TSELENTR.PRODSERN=#cumulative_set.PRODSERN AND
			OLAP_TSELENTR.SELSERN=OLAP_LCOMSEL.SELSERN AND SELESTART<=@prev_seldate AND SELEEND>@prev_seldate )


		UPDATE #cumulative_set
			SET INBSEL=ISNULL(
			(SELECT TOP 1 CASE OLAP_BASE_SELECTION.INSEL  WHEN 1 THEN 1 ELSE 0 END AS INSELSUM
			FROM OLAP_BASE_SELECTION
			WHERE OLAP_BASE_SELECTION.COMSERNO=#cumulative_set.COMSERNO AND OLAP_BASE_SELECTION.PRODSERN= #cumulative_set.PRODSERN AND OLAP_BASE_SELECTION.SELDATE<=@prev_seldate
			ORDER BY OLAP_BASE_SELECTION.COMSERNO , OLAP_BASE_SELECTION.PRODSERN, OLAP_BASE_SELECTION.SELDATE DESC
			)
			, 0 ) --ISNULL end
			FROM  #cumulative_set 


		--select * from #cumulative_set where comserno='IMP000000009354' and prodsern='IMP000000014524'

		update olap_orddistr
			set olap_orddistr.inseldistr=
			(case
				when olap_orddistr.indistr=1 and #cumulative_set.insel=1 then 1
				when olap_orddistr.indistr=-1 and #cumulative_set.insel=1 then -1
				else 0
			end) , 
			olap_orddistr.inbseldistr=(case
				when olap_orddistr.indistr=1 and #cumulative_set.inbsel=1 then 1
				when olap_orddistr.indistr=-1 and #cumulative_set.inbsel=1 then -1
				else 0
			end)
			from #cumulative_set , olap_orddistr
			where #cumulative_set.comserno=olap_orddistr.comserno and #cumulative_set.prodsern=olap_orddistr.prodsern  and #cumulative_set.prodexpand=olap_orddistr.prodexpand
			and ordddate>=@prev_seldate and ordddate<@seldate



		insert into olap_orddistr(comserno , prodsern , prodexpand , ordddate , indistr , inseldistr ,  inbseldistr)
			select #cumulative_set.comserno , #cumulative_set.prodsern , #cumulative_set.prodexpand , @prev_seldate as ordddate , 0 as indistr ,  insel-prev_insel as inseldistr ,  inbsel-prev_inbsel as inbseldistr
			from #cumulative_set
			where 
			prev_indistr=1 and indistr=1 and ( prev_insel!=insel  or  prev_inbsel!=inbsel  )

		--select * from olap_orddistr where comserno='IMP000000009354' and prodsern='IMP000000014524'  and ordddate>=@prev_seldate and ordddate<@seldate

		print @@rowcount



		update #cumulative_set
			set #cumulative_set.indistr=#cumulative_set.indistr+sum_indistr
			from #cumulative_set , (select comserno , prodsern , prodexpand , sum(indistr) as sum_indistr from olap_orddistr where ordddate>=@prev_seldate and ordddate<@seldate group by comserno , prodsern , prodexpand ) olap_orddistr
			where #cumulative_set.comserno=olap_orddistr.comserno and #cumulative_set.prodsern=olap_orddistr.prodsern  and #cumulative_set.prodexpand=olap_orddistr.prodexpand 



		set @prev_seldate=@seldate

		fetch next from temp_cursor into @seldate
	end

close temp_cursor
deallocate temp_cursor

drop table #cumulative_set












GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_PRODUCT]    Script Date: 01/09/2008 18:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [spp].[proc_fill_OLAP_PRODUCT] AS
DECLARE @command_string varchar(8000)
DECLARE @PRODSERN varchar(15)
DECLARE @PGRPNAME varchar(15)
DECLARE @PGRPVAL varchar(30)
DECLARE @TEMP_COUNTER int
DECLARE @position int
DECLARE @BLN_SUPPLIER_DIM_2 bit
SET @BLN_SUPPLIER_DIM_2=0

DECLARE @COUNTER int
SET @COUNTER=0

SET NOCOUNT ON


--BEGIN TRAN



----------------------- CREATE TABLE

if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_PRODUCT]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_PRODUCT]

SET @command_string='CREATE TABLE [spp].[OLAP_PRODUCT] (
[PRODSERN] [varchar] (15) NOT NULL  PRIMARY KEY,
[NR] smallint NOT NULL  IDENTITY(1,1),
[PRODNAME] [varchar] (65)   NULL  DEFAULT ''Undefined'',
[PRODSNAME] [varchar] (15)    NULL  DEFAULT ''Undefined'',
[PRODSIZE] [float]  NULL  DEFAULT 1,
[PRODCPS] [float]  NULL DEFAULT 1,
[PRODTAX] [float]  NULL DEFAULT 0,
[PRODPALLET] [float]  NULL  DEFAULT 1,
[PRODPRICE] [float]  NULL  DEFAULT 0,
[PRODCPWNET] [float]  NULL  DEFAULT 0,
[PRODCPWGR] [float]  NULL  DEFAULT 0,
[PRODCASEWGR] [float]  NULL  DEFAULT 0,
[PRODSUPPLIER] [varchar] (35)    NULL  DEFAULT ''Undefined'' '


DECLARE temp_cursor CURSOR FOR 
SELECT DISTINCT PGRPNAME FROM TPGROUPS WHERE LTRIM(ISNULL(PGRPNAME,''))!=''

OPEN temp_cursor

FETCH NEXT FROM temp_cursor INTO @PGRPNAME

WHILE @@FETCH_STATUS=0
	BEGIN
		IF CHARINDEX(@command_string , '[GRP@#@' + REPLACE(@PGRPNAME , ']' , ']]')  + ']' )=0
			BEGIN
				SET @command_string=@command_string + ' , '
				SET @command_string=@command_string +'[GRP@#@' + REPLACE(@PGRPNAME , ']' , ']]')  + '] [varchar] (30)   NULL  DEFAULT ' + CHAR(39) + 'Undefined' + CHAR(39)
				FETCH NEXT FROM temp_cursor INTO @PGRPNAME
			END
	END

CLOSE temp_cursor 
DEALLOCATE temp_cursor

SET @command_string=@command_string + ') ON [PRIMARY]'
EXECUTE(@command_string)




---------------- INSERT VALUES

INSERT INTO OLAP_PRODUCT(PRODSERN , PRODNAME , PRODSNAME  , PRODSIZE , PRODCPS , PRODTAX ,  PRODPALLET, PRODPRICE, PRODCPWNET, PRODCPWGR , PRODCASEWGR )
	SELECT PRODSERN , ISNULL(PRODNAME,'') + (CASE WHEN PRODCODE IS NULL THEN '' ELSE ' ' + PRODCODE END) , PRODSNAME ,
	CASE ISNULL(PRODSIZE,0)
		WHEN 0 THEN 1
		ELSE 	PRODSIZE
	END AS PRODSIZE ,
	CASE ISNULL(PRODCPS,0)
		WHEN 0 THEN 1
		ELSE 	PRODCPS
	END AS PRODCPS ,
	ISNULL(PRODTAX,0) AS PRODTAX ,
	CASE ISNULL(PRODPALLET,0)
		WHEN 0 THEN 1
		ELSE 	PRODPALLET
	END AS PRODPALLET ,
	ISNULL(PRODPRICE,0) AS PRODPRICE ,
	ISNULL(PRODCPWNET,0) AS PRODCPWNET ,
	ISNULL(PRODCPWE,0) AS PRODCPWGR ,
	ISNULL(PRODWE,0) AS PRODCASEWGR 
	FROM TPRODUCT

INSERT INTO OLAP_PRODUCT(PRODSERN , PRODNAME , PRODSNAME , PRODSIZE , PRODCPS , PRODPALLET )
	VALUES ( '0' , 'Undefined' , 'Undefined' , 1 , 1 , 1 )
UPDATE OLAP_PRODUCT 
	SET OLAP_PRODUCT.PRODSUPPLIER=ISNULL((SELECT COMNAME FROM  TCOMPANY WHERE TPRODUCT.COMSERNO=TCOMPANY.COMSERNO), 'Undefined')
	FROM TPRODUCT, OLAP_PRODUCT WHERE TPRODUCT.PRODSERN=OLAP_PRODUCT.PRODSERN


---------------- INSERT GROUP VALUES

DECLARE temp_cursor CURSOR FOR
SELECT LPROPGR.PRODSERN , TPGROUPS.PGRPNAME , TPGROUPS.PGRPVAL FROM
LPROPGR INNER JOIN TPGROUPS ON LPROPGR.PGRSERN=TPGROUPS.PGRSERN  WHERE LTRIM(ISNULL(TPGROUPS.PGRPNAME,''))!=''
ORDER BY LPROPGR.DATESTAMP
OPEN temp_cursor
FETCH NEXT FROM temp_cursor INTO @PRODSERN , @PGRPNAME , @PGRPVAL
WHILE @@FETCH_STATUS=0
	BEGIN

		SET @command_string='UPDATE [OLAP_PRODUCT]  SET [GRP@#@' +  REPLACE(@PGRPNAME , ']' , ']]')  + ']=' + CHAR(39) +REPLACE(@PGRPVAL , CHAR(39) , CHAR(39)+'+CHAR(39)+' +CHAR(39) ) + CHAR(39) + '  WHERE PRODSERN=' + CHAR(39) + @PRODSERN + CHAR(39)
		EXECUTE(@command_string)

		FETCH NEXT FROM temp_cursor INTO @PRODSERN , @PGRPNAME , @PGRPVAL
	END

CLOSE temp_cursor 
DEALLOCATE temp_cursor








---------------- OLAP_PGROUPS

-- drop and create table
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_PGROUPS]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_PGROUPS]

CREATE TABLE [spp].[OLAP_PGROUPS] (
	[PGRSERN] [varchar] (15)   NOT NULL PRIMARY KEY ,
	[PGRPNAME] [varchar] (50)  NULL ,
	[PGRPVAL] [varchar] (30)  NULL 
) ON [PRIMARY]


-- insert data

INSERT INTO OLAP_PGROUPS(PGRSERN , PGRPNAME , PGRPVAL)
SELECT PGRSERN , PGRPNAME , PGRPVAL
FROM TPGROUPS
WHERE PGRSERN IN (SELECT PGRSERN FROM LPROPGR)
INSERT INTO OLAP_PGROUPS(PGRSERN , PGRPNAME , PGRPVAL)
SELECT DISTINCT CAST(PGRPNAME as varchar(15))  AS PGRSERN, PGRPNAME , 'Undefined' AS PGRPVAL
FROM OLAP_PGROUPS
WHERE PGRPVAL<>'Undefined'




/*
---------------- RENAMING COLUMNS TO MATCH DSO NAMING CONVENTIONS
DECLARE @PRODUCT_GROUP_NAME varchar(50)
DECLARE @NEW_PRODUCT_GROUP_NAME varchar(50)
DECLARE @TEMP_PRODUCT_GROUP_NAME varchar(50)
DECLARE @CURRENT_ASCII_CODE tinyint
SET @CURRENT_ASCII_CODE=0

DECLARE temp_cursor CURSOR 
FOR
-- NB! MUST USE DATALENGTH/2 INSTEAD OF LEN, CAUSE LEN FUNCTION REMOVES TRAILING SPACES!!
select SUBSTRING(COLUMN_NAME , 7 , DATALENGTH(COLUMN_NAME)/2-6) AS PRODUCT_GROUP_NAME from INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_CATALOG=(SELECT DB_NAME())  AND TABLE_SCHEMA='spp' AND TABLE_NAME='OLAP_PRODUCT'
AND LEFT(COLUMN_NAME ,6)='GRP@#@'
OPEN temp_cursor
FETCH NEXT FROM temp_cursor INTO @PRODUCT_GROUP_NAME
WHILE @@FETCH_STATUS=0
	BEGIN
		SET @NEW_PRODUCT_GROUP_NAME=RTRIM(LTRIM(Replace(@PRODUCT_GROUP_NAME , '  ' ,' ')))
		SET @NEW_PRODUCT_GROUP_NAME=Replace(@NEW_PRODUCT_GROUP_NAME , '   ' ,' ')
		SET @NEW_PRODUCT_GROUP_NAME=Replace(@NEW_PRODUCT_GROUP_NAME , '    ' ,' ')
		SET @NEW_PRODUCT_GROUP_NAME=Replace(@NEW_PRODUCT_GROUP_NAME , '     ' ,' ')
		SET @position=1
		SET @TEMP_PRODUCT_GROUP_NAME=''
		
		WHILE @position <= DATALENGTH(@NEW_PRODUCT_GROUP_NAME)
		   BEGIN
		
			SET  @CURRENT_ASCII_CODE=ASCII(SUBSTRING(@NEW_PRODUCT_GROUP_NAME, @position, 1))
		
			IF (@CURRENT_ASCII_CODE<32) 
			OR (@CURRENT_ASCII_CODE>32 AND @CURRENT_ASCII_CODE<48)
			OR (@CURRENT_ASCII_CODE>57 AND @CURRENT_ASCII_CODE<65)
			OR (@CURRENT_ASCII_CODE>90 AND @CURRENT_ASCII_CODE<97)
			OR (@CURRENT_ASCII_CODE>122 AND @CURRENT_ASCII_CODE<192)
				BEGIN
					IF RIGHT(@TEMP_PRODUCT_GROUP_NAME , 1)<> ' '
						SET @TEMP_PRODUCT_GROUP_NAME=@TEMP_PRODUCT_GROUP_NAME + ' '
				END
			ELSE
				BEGIN
					SET @TEMP_PRODUCT_GROUP_NAME=@TEMP_PRODUCT_GROUP_NAME + SUBSTRING(@NEW_PRODUCT_GROUP_NAME, @position, 1)
				END
		
		   	SET @position = @position + 1
		
		   END

			--- check if first symbol is letter ,  it's not allowed
			SET @CURRENT_ASCII_CODE=ASCII(LEFT(@TEMP_PRODUCT_GROUP_NAME, 1))
			IF (@CURRENT_ASCII_CODE<65)
			OR (@CURRENT_ASCII_CODE>90 AND @CURRENT_ASCII_CODE<97)
			OR (@CURRENT_ASCII_CODE>122 AND @CURRENT_ASCII_CODE<129)
			OR (@CURRENT_ASCII_CODE>165)
				BEGIN
					SET @TEMP_PRODUCT_GROUP_NAME='Group ' + @TEMP_PRODUCT_GROUP_NAME
				END


			SET @NEW_PRODUCT_GROUP_NAME=@TEMP_PRODUCT_GROUP_NAME
			-- NB! AGAIN DATALENGTH MUST BE COMPARED, OTHERWISE TRAILING SPACES ARE NOT TAKEN INTO ACCOUNT
			IF @NEW_PRODUCT_GROUP_NAME<>@PRODUCT_GROUP_NAME OR DATALENGTH(@NEW_PRODUCT_GROUP_NAME)<>DATALENGTH(@PRODUCT_GROUP_NAME)
					BEGIN
						SET @TEMP_PRODUCT_GROUP_NAME=@NEW_PRODUCT_GROUP_NAME
						SET @TEMP_COUNTER=0
						WHILE EXISTS(SELECT *  FROM v_select_product_groups WHERE PRODUCT_GROUP_NAME=@TEMP_PRODUCT_GROUP_NAME)
							BEGIN
								SET @TEMP_COUNTER=@TEMP_COUNTER+1
								SET @TEMP_PRODUCT_GROUP_NAME=@NEW_PRODUCT_GROUP_NAME + CAST(@TEMP_COUNTER AS varchar(5))
							END
						SET @NEW_PRODUCT_GROUP_NAME='GRP@#@' + @TEMP_PRODUCT_GROUP_NAME
						SET @PRODUCT_GROUP_NAME='OLAP_PRODUCT.[GRP@#@' + REPLACE(@PRODUCT_GROUP_NAME , ']' , ']]') + ']'

						---  rename OLAP_PRODUCT columns 
						EXEC  sp_rename @PRODUCT_GROUP_NAME , @NEW_PRODUCT_GROUP_NAME, 'COLUMN'			
			
						--- rename OLAP_PGROUPS_TRANS records
						UPDATE OLAP_PGROUPS SET PGRPNAME=@NEW_PRODUCT_GROUP_NAME WHERE PGRPNAME=@PRODUCT_GROUP_NAME
						
					END
		FETCH NEXT FROM temp_cursor INTO @PRODUCT_GROUP_NAME
	END
CLOSE temp_cursor 
DEALLOCATE temp_cursor
*/


--COMMIT TRAN

GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SALESCALL]    Script Date: 01/09/2008 18:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


















CREATE PROCEDURE [spp].[proc_fill_OLAP_SALESCALL]
AS

SET NOCOUNT ON


/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SALESCALL]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SALESCALL]
/***********************************/

/***********************************/
CREATE TABLE [spp].[OLAP_SALESCALL] (
	[SALESCALL_KEY] [varchar] (15)   NOT NULL PRIMARY KEY ,
	[SALESCALL_MEM] [varchar] (50)  NOT NULL ,
	[SALESCALL_LVL] [varchar] (50) NOT NULL 
) ON [PRIMARY]
/***********************************/


INSERT INTO OLAP_SALESCALL(SALESCALL_KEY, SALESCALL_MEM , SALESCALL_LVL)

SELECT DISTINCT  'SALCTYPE_'  + ISNULL(SALCTYPE, '')  AS SALESCALL_KEY, 'Sales Call Type - ' + ISNULL(SALCTYPE, '') AS SALESCALL_MEM , 'Sales Call Type' AS SALESCALL_LVL FROM spp.TCALENTR 
UNION

SELECT 'CALESTAT1_0' AS SALESCALL_KEY, 'Time Reserved - No' AS SALESCALL_MEM , 'Time Reserved' AS SALESCALL_LVL
UNION
SELECT 'CALESTAT1_1' AS SALESCALL_KEY, 'Time Reserved - Yes' AS SALESCALL_MEM , 'Time Reserved' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'CALESTAT1_'  + ISNULL(CALESTAT1, '')  AS SALESCALL_KEY, 'Time Reserved - Unknown' AS SALESCALL_MEM , 'Time Reserved' AS SALESCALL_LVL FROM spp.TCALENTR WHERE CALESTAT1 NOT IN ('0' , '1') OR CALESTAT1 IS NULL
UNION

SELECT 'CALESTAT2_0' AS SALESCALL_KEY, 'Status - OK' AS SALESCALL_MEM , 'Status' AS SALESCALL_LVL
UNION
SELECT 'CALESTAT2_1' AS SALESCALL_KEY, 'Status - Cancelled' AS SALESCALL_MEM , 'Status' AS SALESCALL_LVL
UNION
SELECT 'CALESTAT2_2' AS SALESCALL_KEY, 'Status - Moved' AS SALESCALL_MEM , 'Status' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'CALESTAT2_'  + ISNULL(CALESTAT2, '')  AS SALESCALL_KEY, 'Status - Unknown' AS SALESCALL_MEM , 'Status' AS SALESCALL_LVL FROM spp.TCALENTR WHERE CALESTAT2 NOT IN ('0' , '1' , '2') OR CALESTAT2 IS NULL
UNION

SELECT 'CALESTAT4_0' AS SALESCALL_KEY, 'Phone Call - No' AS SALESCALL_MEM , 'Phone Call' AS SALESCALL_LVL
UNION
SELECT 'CALESTAT4_1' AS SALESCALL_KEY, 'Phone Call - Yes' AS SALESCALL_MEM , 'Phone Call' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'CALESTAT4_'  + ISNULL(CALESTAT4, '')  AS SALESCALL_KEY, 'Phone Call - Unknown' AS SALESCALL_MEM , 'Phone Call' AS SALESCALL_LVL FROM spp.TCALENTR WHERE CALESTAT4 NOT IN ('0' , '1') OR CALESTAT4 IS NULL
UNION

SELECT 'SALCSTAT10_0' AS SALESCALL_KEY, 'DPM Status - Not Done' AS SALESCALL_MEM , 'DPM Status' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT10_1' AS SALESCALL_KEY, 'DPM Status - Done' AS SALESCALL_MEM , 'DPM Status' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT10_2' AS SALESCALL_KEY, 'DPM Status - Sales Registered' AS SALESCALL_MEM , 'DPM Status' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT10_3' AS SALESCALL_KEY, 'DPM Status - Shelf Reorg Proposed' AS SALESCALL_MEM , 'DPM Status' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT10_4' AS SALESCALL_KEY, 'DPM Status - Shelf Reorg Done' AS SALESCALL_MEM , 'DPM Status' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT10_'  + ISNULL(SALCSTAT10, '')  AS SALESCALL_KEY, 'DPM Status - Unknown' AS SALESCALL_MEM , 'DPM Status' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT10 NOT IN ('0' , '1' , '2' , '3' , '4') OR SALCSTAT10 IS NULL
UNION

SELECT 'SALCSTAT1_0' AS SALESCALL_KEY, 'Pending - No' AS SALESCALL_MEM , 'Pending' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT1_1' AS SALESCALL_KEY, 'Pending - Yes' AS SALESCALL_MEM , 'Pending' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT1_'  + ISNULL(SALCSTAT1, '')  AS SALESCALL_KEY, 'Pending - Unknown' AS SALESCALL_MEM , 'Pending' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT1 NOT IN ('0' , '1' ) OR SALCSTAT1 IS NULL
UNION

SELECT 'SALCSTAT2_0' AS SALESCALL_KEY, 'Proposal Left - No' AS SALESCALL_MEM , 'Proposal Left' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT2_1' AS SALESCALL_KEY, 'Proposal Left - Yes' AS SALESCALL_MEM , 'Proposal Left' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT2_'  + ISNULL(SALCSTAT2, '')  AS SALESCALL_KEY, 'Proposal Left - Unknown' AS SALESCALL_MEM , 'Proposal Left' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT2 NOT IN ('0' , '1' ) OR SALCSTAT2 IS NULL
UNION

SELECT 'SALCSTAT3_0' AS SALESCALL_KEY, 'Salesman Took The Order - No' AS SALESCALL_MEM , 'Salesman Took The Order' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT3_1' AS SALESCALL_KEY, 'Salesman Took The Order - Yes' AS SALESCALL_MEM , 'Salesman Took The Order' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT3_'  + ISNULL(SALCSTAT3, '')  AS SALESCALL_KEY, 'Salesman Took The Order - Unknown' AS SALESCALL_MEM , 'Salesman Took The Order' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT3 NOT IN ('0' , '1' ) OR SALCSTAT3 IS NULL
UNION

SELECT 'SALCSTAT4_0' AS SALESCALL_KEY, 'Retailer Took The Order - No' AS SALESCALL_MEM , 'Retailer Took The Order' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT4_1' AS SALESCALL_KEY, 'Retailer Took The Order - Yes' AS SALESCALL_MEM , 'Retailer Took The Order' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT4_'  + ISNULL(SALCSTAT4, '')  AS SALESCALL_KEY, 'Retailer Took The Order - Unknown' AS SALESCALL_MEM , 'Retailer Took The Order' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT4 NOT IN ('0' , '1' ) OR SALCSTAT4 IS NULL
UNION

SELECT 'SALCSTAT5_0' AS SALESCALL_KEY, 'Survey Done - No' AS SALESCALL_MEM , 'Survey Done' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT5_1' AS SALESCALL_KEY, 'Survey Done - Yes' AS SALESCALL_MEM , 'Survey Done' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT5_'  + ISNULL(SALCSTAT5, '')  AS SALESCALL_KEY, 'Survey Done - Unknown' AS SALESCALL_MEM , 'Survey Done' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT5 NOT IN ('0' , '1' ) OR SALCSTAT5 IS NULL
UNION

SELECT 'SALCSTAT6_0' AS SALESCALL_KEY, 'Planning Done - No' AS SALESCALL_MEM , 'Planning Done' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT6_1' AS SALESCALL_KEY, 'Planning Done - Yes' AS SALESCALL_MEM , 'Planning Done' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT6_'  + ISNULL(SALCSTAT6, '')  AS SALESCALL_KEY, 'Planning Done - Unknown' AS SALESCALL_MEM , 'Planning Done' AS SALESCALL_LVL FROM spp.TCALENTR WHERE SALCSTAT6 NOT IN ('0' , '1' ) OR SALCSTAT6 IS NULL
UNION

SELECT 'SALCSTAT7_0' AS SALESCALL_KEY, 'MSA Done - No' AS SALESCALL_MEM , 'MSA Done' AS SALESCALL_LVL
UNION
SELECT 'SALCSTAT7_1' AS SALESCALL_KEY, 'MSA Done - Yes' AS SALESCALL_MEM , 'MSA Done' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALCSTAT7_'  + ISNULL(SALCSTAT7, '')  AS SALESCALL_KEY, 'MSA Done - Unknown' AS SALESCALL_MEM , 'MSA Done' AS SALESCAL_LVL FROM spp.TCALENTR WHERE SALCSTAT7 NOT IN ('0' , '1' ) OR SALCSTAT7 IS NULL
UNION

-- IN THIS CASE INVALID VALUES COUNT AS "NO" 
SELECT 'SALNOTCALL_0' AS SALESCALL_KEY, 'Is Call - Yes' AS SALESCALL_MEM , 'Is Call' AS SALESCALL_LVL
UNION
SELECT 'SALNOTCALL_1' AS SALESCALL_KEY, 'Is Call - No' AS SALESCALL_MEM , 'Is Call' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALNOTCALL_'  + ISNULL(SALNOTCALL, '')  AS SALESCALL_KEY, 'Is Call - No' AS SALESCALL_MEM , 'Is Call' AS SALESCAL_LVL FROM spp.TCALENTR WHERE SALNOTCALL NOT IN ('0' , '1' ) OR SALNOTCALL IS NULL
UNION

-- IN THIS CASE INVALID VALUES COUNT AS "NO" 
SELECT 'SALUNCALL_0' AS SALESCALL_KEY, 'Unnecessary Call - No' AS SALESCALL_MEM , 'Unnecessary Call' AS SALESCALL_LVL
UNION
SELECT 'SALUNCALL_1' AS SALESCALL_KEY, 'Unnecessary Call - Yes' AS SALESCALL_MEM , 'Unnecessary Call' AS SALESCALL_LVL
UNION
SELECT DISTINCT  'SALUNCALL_'  + ISNULL(SALUNCALL, '')  AS SALESCALL_KEY, 'Unnecessary Call - No' AS SALESCALL_MEM , 'Unnecessary Call' AS SALESCAL_LVL FROM spp.TCALENTR WHERE SALUNCALL NOT IN ('0' , '1' ) OR SALUNCALL IS NULL













GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SELECTION]    Script Date: 01/09/2008 18:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





















CREATE procedure [spp].[proc_fill_OLAP_SELECTION]  AS

set nocount on

declare @PRODSERN varchar(15)
DECLARE @COMSERNO varchar(15)
DECLARE @COMSELCLS varchar(15)
DECLARE @COMSELCLSH bit

DECLARE @SELSERN varchar(15)
DECLARE @SELESERN varchar(15)

DECLARE @prev_position int
DECLARE @position int
DECLARE @ordinal smallint

declare @min_seldate varchar(8)
select @min_seldate=min(date) from olap_date



------------------------------ fill SELECTION DIMS ---------------------------------
EXEC proc_fill_OLAP_SELECTION_DIMS






---------------------- DROP AND INSERT OLAP_SELECTION -----------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SELECTION]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SELECTION]


select distinct COMSERNO, PRODSERN , SELDATE , CAST(1 as smallint) as INSEL 
into OLAP_SELECTION
from
(select top 100 percent * from OLAP_SELDATE order by seldate) tmp_seldate,
OLAP_LCOMSEL, OLAP_TSELENTR
where 
OLAP_LCOMSEL.selsern=OLAP_TSELENTR.selsern
and OLAP_TSELENTR.selestart=seldate
and not exists(
select * from
OLAP_LCOMSEL OLAP_LCOMSEL2, OLAP_TSELENTR tselentr2
where 
OLAP_LCOMSEL2.selsern=tselentr2.selsern
and tselentr2.selestart<seldate and tselentr2.seleend>=seldate
and OLAP_LCOMSEL.comserno=OLAP_LCOMSEL2.comserno and OLAP_TSELENTR.prodsern=tselentr2.prodsern
)


union all


select distinct COMSERNO, PRODSERN , SELDATE , CAST(-1 as smallint) as INSEL 
from
(select top 100 percent * from OLAP_SELDATE order by seldate) tmp_seldate,
OLAP_LCOMSEL, OLAP_TSELENTR
where 
OLAP_LCOMSEL.selsern=OLAP_TSELENTR.selsern
and OLAP_TSELENTR.seleend=seldate
and not exists(
select * from
OLAP_LCOMSEL OLAP_LCOMSEL2, OLAP_TSELENTR tselentr2
where 
OLAP_LCOMSEL2.selsern=tselentr2.selsern
and tselentr2.selestart<=seldate and tselentr2.seleend>seldate
and OLAP_LCOMSEL.comserno=OLAP_LCOMSEL2.comserno and OLAP_TSELENTR.prodsern=tselentr2.prodsern
)


---------------------- CREATE INDICES OLAP_SELECTION -----------------------------
print 'creating indices on OLAP_SELECTION'
print getdate()

CREATE CLUSTERED INDEX IX_OLAP_SELECTION
ON spp.OLAP_SELECTION( COMSERNO , PRODSERN , SELDATE              )

CREATE NONCLUSTERED INDEX  IX_OLAP_SELECTION_SELDATE ON spp.OLAP_SELECTION( SELDATE )

print 'done'
print getdate()









--------------------------  DROP AND CREATE TEMP TABLES -----------------------------
if object_id('tempdb..#chain') is not null
	drop table #chain
CREATE TABLE #chain(CCHSERN varchar(15), ORDINAL smallint , HIER_MEMBER varchar(15) , IS_HIER bit)

if object_id('tempdb..#storecls') is not null
	drop table #storecls
CREATE TABLE #storecls(COMSERNO varchar(15), SELSERN  varchar(15), HIER_MEMBER varchar(15) , CHILD varchar(15) , IS_HIER bit)

if object_id('tempdb..#tselectcls') is not null
	drop table #tselectcls
CREATE TABLE #tselectcls(SELSERN varchar(15), SELESERN  varchar(15),  PRODSERN varchar(15), HIER_MEMBER varchar(15))

if object_id('tempdb..#lcompgr') is not null
	drop table #lcompgr



--------------------------  INSERT INTO #lcompgr -----------------------------
SELECT COMSERNO , PGRSERN , COMSELCLS
INTO #lcompgr
FROM LCOMPGR



--------------------------  CREATE INDICES ON TEMP TABLES -----------------------------
CREATE CLUSTERED INDEX ix_tmp_storecls
ON #storecls(SELSERN , COMSERNO , CHILD )
--ON #storecls(SELSERN , COMSERNO , HIER_MEMBER )

CREATE CLUSTERED INDEX ix_tmp_tselectcls_keys
ON #tselectcls(SELSERN , PRODSERN )

CREATE CLUSTERED INDEX ix_tmp_lcompgr_keys
ON #lcompgr(COMSERNO , PGRSERN , COMSELCLS )






--------------------------  INSERT INTO #chain -----------------------------

DECLARE temp_cursor CURSOR FOR
SELECT COMSERNO ,REPLACE(COMSELCLS , ' ' , '') AS  COMSELCLS, 
CASE COMSELCLSH 
	WHEN '1' THEN 1
	ELSE 0
END AS COMSELCLSH
FROM TCOMPANY WHERE LEN(REPLACE(COMSELCLS , ' ' , ''))>0

OPEN temp_cursor

FETCH NEXT FROM temp_cursor INTO @COMSERNO , @COMSELCLS , @COMSELCLSH

WHILE @@FETCH_STATUS=0
	BEGIN
		SET @ordinal=1

		SET @prev_position=1
		SET @position=CHARINDEX(',' , @COMSELCLS ,  @prev_position)
		
		------------------------------
		IF @position=0
			INSERT INTO #chain(CCHSERN , ORDINAL , HIER_MEMBER , IS_HIER)
				VALUES(@COMSERNO , @ordinal , @COMSELCLS , @COMSELCLSH )

		------------------------------
		WHILE @position>0
			BEGIN
				INSERT INTO #chain(CCHSERN , ORDINAL , HIER_MEMBER , IS_HIER)
					VALUES(@COMSERNO , @ordinal , SUBSTRING(@COMSELCLS , @prev_position, @position-@prev_position) , @COMSELCLSH )


				SET @prev_position=@position+1
				SET @position=CHARINDEX(',' , @COMSELCLS ,  @prev_position)
				
				SET @ordinal=@ordinal+1
			END

		------------------------------

		IF @prev_position>1 AND @position=0
			INSERT INTO #chain(CCHSERN , ORDINAL , HIER_MEMBER , IS_HIER)
				VALUES(@COMSERNO , @ordinal , SUBSTRING(@COMSELCLS , @prev_position, LEN(@COMSELCLS)-@prev_position+1) , @COMSELCLSH )



		

		FETCH NEXT FROM temp_cursor INTO @COMSERNO , @COMSELCLS , @COMSELCLSH


	END

CLOSE temp_cursor
DEALLOCATE temp_cursor



--------------------------  INSERT INTO #storecls -----------------------------

INSERT INTO #storecls (COMSERNO , SELSERN , HIER_MEMBER, CHILD , IS_HIER)
select distinct COMSERN2 AS COMSERNO , OLAP_LCOMSEL.SELSERN , chain.HIER_MEMBER, chain.CHILD , chain.IS_HIER 
from 
(
select parent.cchsern , parent.ordinal , parent.hier_member , children.hier_member as child , parent.is_hier from  #chain parent ,  #chain children 
where parent.cchsern=children.cchsern and ( ( parent.is_hier=1 and parent.ordinal<=children.ordinal) or (  parent.is_hier=0 and parent.ordinal=children.ordinal )  )
) chain
inner join LCOMCOM on LCOMCOM.comsern1=chain.cchsern and UPPER(LCOMCOM.lcomatr1)='CHAIN'
inner join OLAP_LCOMSEL on OLAP_LCOMSEL.comserno=lcomcom.comsern2
inner join TSELECT on TSELECT.SELSERN=OLAP_LCOMSEL.SELSERN
	 AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=(CASE WHEN LEN(TSELECT.SELEND)=8 THEN TSELECT.SELEND ELSE '99999999' END)
	 AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=(CASE WHEN LEN(TSELECT.SELSTART)=8 THEN TSELECT.SELSTART ELSE '00000000' END)






--------------------------  INSERT INTO #tselectcls -----------------------------

DECLARE temp_cursor CURSOR FOR
SELECT SELSERN, SELESERN , PRODSERN , REPLACE(SELCLS , ' ' , '') AS  COMSELCLS
FROM TSELENTR WHERE LEN(REPLACE(SELCLS , ' ' , ''))>0

OPEN temp_cursor

FETCH NEXT FROM temp_cursor INTO @SELSERN, @SELESERN , @PRODSERN , @COMSELCLS 

WHILE @@FETCH_STATUS=0
	BEGIN
		SET @ordinal=1

		SET @prev_position=1
		SET @position=CHARINDEX(',' , @COMSELCLS ,  @prev_position)
		
		------------------------------
		IF @position=0
			INSERT INTO #tselectcls(SELSERN, SELESERN, PRODSERN  , HIER_MEMBER )
				VALUES(@SELSERN, @SELESERN, @PRODSERN , @COMSELCLS )

		------------------------------
		WHILE @position>0
			BEGIN
				INSERT INTO #tselectcls(SELSERN, SELESERN, PRODSERN , HIER_MEMBER )
					VALUES(@SELSERN, @SELESERN, @PRODSERN  , SUBSTRING(@COMSELCLS , @prev_position, @position-@prev_position)  )


				SET @prev_position=@position+1
				SET @position=CHARINDEX(',' , @COMSELCLS ,  @prev_position)
				
				SET @ordinal=@ordinal+1
			END

		------------------------------

		IF @prev_position>1 AND @position=0
			INSERT INTO #tselectcls(SELSERN , SELESERN, PRODSERN  , HIER_MEMBER )
				VALUES(@SELSERN, @SELESERN, @PRODSERN  , SUBSTRING(@COMSELCLS , @prev_position, LEN(@COMSELCLS)-@prev_position+1) )



		

		FETCH NEXT FROM temp_cursor INTO @SELSERN, @SELESERN, @PRODSERN , @COMSELCLS 


	END
CLOSE temp_cursor
DEALLOCATE temp_cursor


delete from #tselectcls where selesern not in (select selesern from OLAP_TSELENTR)






--------------------------  DROP BASE SELECTION -----------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_BASE_SELECTION]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_BASE_SELECTION]



--------------------------  INSERT INTO BASE SELECTION -----------------------------
select distinct COMSERNO, PRODSERN , SELDATE , CAST(1 as smallint) as INSEL 
into OLAP_BASE_SELECTION
from
(select top 100 percent * from OLAP_SELDATE order by seldate) tmp_seldate,
OLAP_LCOMSEL, OLAP_TSELENTR
where 
OLAP_LCOMSEL.selsern=OLAP_TSELENTR.selsern
and OLAP_TSELENTR.selestart=seldate
and exists(select * from #tselectcls where #tselectcls.selsern=OLAP_LCOMSEL.selsern and #tselectcls.prodsern=OLAP_TSELENTR.prodsern  
	and exists( select * from #storecls where #storecls.selsern=OLAP_LCOMSEL.selsern and #storecls.comserno=OLAP_LCOMSEL.comserno and #tselectcls.hier_member=#storecls.child
		and exists(select * from lpropgr where lpropgr.prodsern=OLAP_TSELENTR.prodsern 
		and exists(select * from #lcompgr where #lcompgr.pgrsern=lpropgr.pgrsern and #lcompgr.comserno=OLAP_LCOMSEL.comserno and #lcompgr.comselcls=#storecls.hier_member) )  ) )

and not exists(
select * from
OLAP_LCOMSEL OLAP_LCOMSEL2, OLAP_TSELENTR tselentr2
where 
OLAP_LCOMSEL2.selsern=tselentr2.selsern
and tselentr2.selestart<seldate and tselentr2.seleend>=seldate
and OLAP_LCOMSEL.comserno=OLAP_LCOMSEL2.comserno and OLAP_TSELENTR.prodsern=tselentr2.prodsern
and exists(select * from #tselectcls where #tselectcls.selsern=OLAP_LCOMSEL2.selsern and #tselectcls.prodsern=tselentr2.prodsern  
	and exists( select * from #storecls where #storecls.selsern=OLAP_LCOMSEL2.selsern and #storecls.comserno=OLAP_LCOMSEL2.comserno and #tselectcls.hier_member=#storecls.child
		and exists(select * from lpropgr where lpropgr.prodsern=tselentr2.prodsern 
			and exists(select * from #lcompgr where #lcompgr.pgrsern=lpropgr.pgrsern and #lcompgr.comserno=OLAP_LCOMSEL2.comserno and #lcompgr.comselcls=#storecls.hier_member) )  ) )

)


union all


select distinct COMSERNO, PRODSERN , SELDATE , CAST(-1 as smallint) as INSEL
from
(select top 100 percent * from OLAP_SELDATE order by seldate) tmp_seldate,
OLAP_LCOMSEL, OLAP_TSELENTR
where 
OLAP_LCOMSEL.selsern=OLAP_TSELENTR.selsern
and OLAP_TSELENTR.seleend=seldate
and exists(select * from #tselectcls where #tselectcls.selsern=OLAP_LCOMSEL.selsern and #tselectcls.prodsern=OLAP_TSELENTR.prodsern  
	and exists( select * from #storecls where #storecls.selsern=OLAP_LCOMSEL.selsern and #storecls.comserno=OLAP_LCOMSEL.comserno and #tselectcls.hier_member=#storecls.child
		and exists(select * from lpropgr where lpropgr.prodsern=OLAP_TSELENTR.prodsern 
			and exists(select * from #lcompgr where #lcompgr.pgrsern=lpropgr.pgrsern and #lcompgr.comserno=OLAP_LCOMSEL.comserno and #lcompgr.comselcls=#storecls.hier_member) )  ) )

and not exists(
select * from
OLAP_LCOMSEL OLAP_LCOMSEL2, OLAP_TSELENTR tselentr2
where 
OLAP_LCOMSEL2.selsern=tselentr2.selsern
and tselentr2.selestart<=seldate and tselentr2.seleend>seldate
and OLAP_LCOMSEL.comserno=OLAP_LCOMSEL2.comserno and OLAP_TSELENTR.prodsern=tselentr2.prodsern
and exists(select * from #tselectcls where #tselectcls.selsern=OLAP_LCOMSEL2.selsern and #tselectcls.prodsern=tselentr2.prodsern  
	and exists( select * from #storecls where #storecls.selsern=OLAP_LCOMSEL2.selsern and #storecls.comserno=OLAP_LCOMSEL2.comserno and #tselectcls.hier_member=#storecls.child
		and exists(select * from lpropgr where lpropgr.prodsern=tselentr2.prodsern 
			and exists(select * from #lcompgr where #lcompgr.pgrsern=lpropgr.pgrsern and #lcompgr.comserno=OLAP_LCOMSEL2.comserno and #lcompgr.comselcls=#storecls.hier_member) )  ) )


)



--------------------------  CREATE INDICES BASE SELECTION -----------------------------
print 'creating indices on OLAP_BASE_SELECTION'
print GetDate()

CREATE CLUSTERED INDEX IX_OLAP_BASE_SELECTION
ON spp.OLAP_BASE_SELECTION( COMSERNO , PRODSERN , SELDATE              )

CREATE NONCLUSTERED INDEX  IX_OLAP_BASE_SELECTION_SELDATE ON spp.OLAP_BASE_SELECTION( SELDATE )

print 'done'
print GetDate()


DROP TABLE #storecls
DROP TABLE #tselectcls
DROP TABLE #chain
DROP TABLE #lcompgr





GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SELECTION_DIMS]    Script Date: 01/09/2008 18:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO















CREATE procedure [spp].[proc_fill_OLAP_SELECTION_DIMS]  AS

set nocount on

declare @min_seldate varchar(8)
select @min_seldate=min(date) from olap_date




/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_TSELENTR]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_TSELENTR]

create table OLAP_TSELENTR(
	SELSERN varchar(15) , 
	SELESERN varchar(15) , 
	PRODSERN varchar(15) , 
	SELESTART varchar(8) , 
	SELEEND varchar(8) , 
	SELE_VALID_START varchar(8) , 
	SELE_VALID_END varchar(8)  , 
	SELE_PURCH_PRICE_NET numeric(19,4) , 
	SELE_PURCH_PRICE_GROSS numeric(19,4), 
	SELE_CONS_PRICE_NET  numeric(19,4), 
	SELE_CONS_PRICE_GROSS numeric(19,4) ,
	SELE_MARGIN numeric(19,4)
	 )





insert into OLAP_TSELENTR(selsern , selesern , prodsern , selestart , seleend , sele_valid_start , sele_valid_end , SELE_PURCH_PRICE_NET , SELE_PURCH_PRICE_GROSS , SELE_CONS_PRICE_NET , SELE_CONS_PRICE_GROSS , SELE_MARGIN  )
select tselect.selsern , selesern ,  tselentr.prodsern ,
CASE
	WHEN TSELECT.SELSTART>=TSELENTR.SELESTART AND TSELECT.SELSTART<=TSELECT.SELEND  AND TSELECT.SELSTART<=TSELENTR.SELEEND THEN 
		(CASE TSELECT.SELSTART
			WHEN '00000000' THEN @min_seldate
			ELSE TSELECT.SELSTART 
		END)
	WHEN TSELENTR.SELESTART>TSELECT.SELSTART AND TSELENTR.SELESTART<=TSELECT.SELEND AND TSELENTR.SELESTART<=TSELENTR.SELEEND THEN 
		(CASE TSELENTR.SELESTART
			WHEN '00000000' THEN @min_seldate
			ELSE TSELENTR.SELESTART 
		END)
	ELSE '99999999'
END as selestart,
CASE
	WHEN TSELECT.SELEND<=TSELENTR.SELEEND AND TSELECT.SELEND>=TSELECT.SELSTART AND TSELECT.SELEND>=SELESTART THEN 
		(CASE TSELECT.SELEND
			WHEN '99999999' THEN '99999999'
			ELSE convert(varchar(8) , DATEADD(d , 1 , CONVERT(datetime , TSELECT.SELEND) )  , 112)  
		END)
	WHEN TSELENTR.SELEEND<=TSELECT.SELEND AND TSELENTR.SELEEND>=TSELECT.SELSTART AND TSELENTR.SELEEND>=TSELENTR.SELESTART THEN 
		(CASE TSELENTR.SELEEND
			WHEN '99999999' THEN '99999999'
			ELSE convert(varchar(8) , DATEADD(d , 1 , CONVERT(datetime , TSELENTR.SELEEND) )  , 112)  
		END)
	ELSE '00000000'

END as seleend ,
CASE
	WHEN TSELECT.SELSTART>=TSELENTR.SELESTART AND TSELECT.SELSTART<=TSELECT.SELEND  AND TSELECT.SELSTART<=TSELENTR.SELEEND THEN 
		(CASE TSELECT.SELSTART
			WHEN '00000000' THEN @min_seldate
			ELSE TSELECT.SELSTART 
		END)
	WHEN TSELENTR.SELESTART>TSELECT.SELSTART AND TSELENTR.SELESTART<=TSELECT.SELEND AND TSELENTR.SELESTART<=TSELENTR.SELEEND THEN 
		(CASE TSELENTR.SELESTART
			WHEN '00000000' THEN @min_seldate
			ELSE TSELENTR.SELESTART 
		END)
	ELSE '99999999'
END as sele_valid_start,
CASE
	WHEN TSELECT.SELEND<=TSELENTR.SELEEND AND TSELECT.SELEND>=TSELECT.SELSTART AND TSELECT.SELEND>=SELESTART THEN 
		(CASE TSELECT.SELEND
			WHEN '99999999' THEN '99999999'
			ELSE TSELECT.SELEND
		END)
	WHEN TSELENTR.SELEEND<=TSELECT.SELEND AND TSELENTR.SELEEND>=TSELECT.SELSTART AND TSELENTR.SELEEND>=TSELENTR.SELESTART THEN 
		(CASE TSELENTR.SELEEND
			WHEN '99999999' THEN '99999999'
			ELSE TSELECT.SELEND
		END)
	ELSE '00000000'
END as sele_valid_end ,
CASE
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1'  THEN SELEPRICE							--per cons pkg without tax
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1'  AND SELETAX>=0 THEN SELEPRICE/(1+SELETAX/100)				--per cons pkg with tax and tax is here
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1'  AND SELETAX<0 THEN SELEPRICE/(1+OLAP_LPROPROD.PRODTAX/100)		--per cons pkg with tax and tax is in product
	WHEN TSELENTR.SELEPRFLAG='1' AND TSELENTR.SELEPRTAX!='1'  THEN SELEPRICE/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)						--per case without tax
	WHEN TSELENTR.SELEPRFLAG='1' AND TSELENTR.SELEPRTAX='1'  AND SELETAX>=0 THEN (SELEPRICE/(1+SELETAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)		--per case with tax and tax is here
	WHEN TSELENTR.SELEPRFLAG='1' AND TSELENTR.SELEPRTAX='1'  AND SELETAX<0 THEN (SELEPRICE/(1+OLAP_LPROPROD.PRODTAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)	--per case with tax and tax is in product
END AS SELE_PURCH_PRICE_NET ,
CASE
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1' AND SELETAX>=0 THEN  SELEPRICE*(1+SELETAX/100)				--per cons pkg without tax and tax is here
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1' AND SELETAX<0 THEN  SELEPRICE*(1+OLAP_LPROPROD.PRODTAX/100)		--per cons pkg without tax and tax is in product
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1' THEN SELEPRICE								--per cons pkg with tax
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  AND SELETAX>=0 THEN   (SELEPRICE*(1+SELETAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)		--per case without tax and tax is here
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  AND SELETAX<0 THEN   (SELEPRICE*(1+OLAP_LPROPROD.PRODTAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)--per case without tax and tax is in product
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX='1' THEN SELEPRICE/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)						--per case with tax
	ELSE 0
END AS SELE_PURCH_PRICE_GROSS ,
CASE
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1'  THEN SELECONPR							--per cons pkg without tax
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1'  AND SELETAX>=0 THEN SELECONPR/(1+SELETAX/100)				--per cons pkg with tax and tax is here
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1'  AND SELETAX<0 THEN SELECONPR/(1+OLAP_LPROPROD.PRODTAX/100)		--per cons pkg with tax and tax is in product
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  THEN SELECONPR/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)						--per case without tax
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX='1'  AND SELETAX>=0 THEN (SELECONPR/(1+SELETAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)		--per case with tax and tax is here
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX='1'  AND SELETAX<0 THEN (SELECONPR/(1+OLAP_LPROPROD.PRODTAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)	--per case with tax and tax is in product
	ELSE 0
END AS SELE_CONS_PRICE_NET ,
CASE
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1' AND SELETAX>=0 THEN  SELECONPR*(1+SELETAX/100)				--per cons pkg without tax and tax is here
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1' AND SELETAX<0 THEN  SELECONPR*(1+OLAP_LPROPROD.PRODTAX/100)		--per cons pkg without tax and tax is in product
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1' THEN SELECONPR								--per cons pkg with tax
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  AND SELETAX>=0 THEN   (SELECONPR*(1+SELETAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)		--per case without tax and tax is here
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  AND SELETAX<0 THEN   (SELECONPR*(1+OLAP_LPROPROD.PRODTAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)--per case without tax and tax is in product
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX='1' THEN SELECONPR/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)						--per case with tax
	ELSE 0
END AS SELE_CONS_PRICE_GROSS ,
CASE
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1' AND SELETAX>=0 THEN  SELECONPR*(1+SELETAX/100)-SELEPRICE*(1+SELETAX/100)							--per cons pkg without tax and tax is here
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX!='1' AND SELETAX<0 THEN  SELECONPR*(1+OLAP_LPROPROD.PRODTAX/100)-SELEPRICE*(1+OLAP_LPROPROD.PRODTAX/100)		--per cons pkg without tax and tax is in product
	WHEN TSELENTR.SELEPRFLAG!='1' AND TSELENTR.SELEPRTAX='1' THEN SELECONPR-SELEPRICE														--per cons pkg with tax
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  AND SELETAX>=0 THEN   (SELECONPR*(1+SELETAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)-(SELEPRICE*(1+SELETAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)	--per case without tax and tax is here
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX!='1'  AND SELETAX<0 THEN   (SELECONPR*(1+OLAP_LPROPROD.PRODTAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)-(SELEPRICE*(1+OLAP_LPROPROD.PRODTAX/100))/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END) --per case without tax and tax is in product
	WHEN (TSELENTR.SELEPRFLAG='1' AND SELECSIZE>0) AND TSELENTR.SELEPRTAX='1' THEN SELECONPR/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)	-SELEPRICE/(CASE WHEN SELECSIZE=0 THEN 1 ELSE SELECSIZE END)								--per case with tax
	ELSE 0
END AS SELE_MARGIN
from TSELECT INNER JOIN TSELENTR
on  TSELECT.SELSERN=TSELENTR.SELSERN
INNER JOIN spp.OLAP_LPROPROD OLAP_LPROPROD 
ON TSELENTR.PRODSERN=OLAP_LPROPROD.PARENT_PRODSERN
WHERE 
OLAP_LPROPROD.PARENT_PRODSERN=OLAP_LPROPROD.PRODSERN
AND TSELECT.SELTYPE='0'



delete from OLAP_TSELENTR where selestart='99999999' or seleend='00000000'

create clustered index ix_tmp_tselentr_keys
on OLAP_TSELENTR(selsern , prodsern)

create nonclustered index ix_tmp_tselentr_selestart
on OLAP_TSELENTR(selestart)

create nonclustered index ix_tmp_tselentr_seleend
on OLAP_TSELENTR(seleend)










/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_LCOMSEL]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_LCOMSEL]

create table OLAP_LCOMSEL(SELSERN varchar(15) , COMSERNO varchar(15) , SALMSERN varchar(15) )




insert into OLAP_LCOMSEL(SELSERN , COMSERNO , SALMSERN )
	select distinct 
	SELSERN ,
	COMSERNO , 
	CASE LEN(SALMSERN)
		WHEN 15 THEN  SALMSERN
		ELSE  '0'
	END AS SALMSERN
	from lcomsel where seltype='0' and SELSERN IN (SELECT SELSERN FROM OLAP_TSELENTR)


create clustered index ix_tmp_olap_lcomsel_keys
on OLAP_LCOMSEL(selsern , comserno)


create nonclustered index ix_tmp_olap_lcomsel_comserno
on OLAP_LCOMSEL(comserno)






/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SELDATE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SELDATE]

create table OLAP_SELDATE(SELDATE varchar(8) PRIMARY KEY)



insert into OLAP_SELDATE(SELDATE)
	select distinct selestart from OLAP_TSELENTR
	union
	select distinct seleend from OLAP_TSELENTR where seleend<>'99999999'



GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_STORE]    Script Date: 01/09/2008 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [spp].[proc_fill_OLAP_STORE] AS


DECLARE @TODAY VARCHAR(8)
SET @TODAY=CONVERT(VARCHAR(8), GetDate(), 112)


DECLARE @COMSERNO varchar(15)
DECLARE @SALMNAME varchar(30)
DECLARE @COMNAME varchar(30)

DECLARE @command_string varchar(8000)

DECLARE @UGRPSERN varchar(15)
DECLARE @UGRPNAME varchar(15)


DECLARE @PGRSERN varchar(15)
DECLARE @SALMSERN varchar(15)


DECLARE @USERNAME varchar(30)

DECLARE @STORE_GROUP_NAME varchar(50)
DECLARE @NEW_STORE_GROUP_NAME varchar(50)
DECLARE @TEMP_STORE_GROUP_NAME varchar(50)

DECLARE @CURRENT_ASCII_CODE tinyint

DECLARE @SQL_SRVR_VERSION tinyint

DECLARE @TEMP_COUNTER int

DECLARE @position int

DECLARE @COLUMN_NAME varchar(100)

SET NOCOUNT ON 



--BEGIN TRAN


if object_id('tempdb..#comserno_table') is not null
	drop table #comserno_table

if object_id('tempdb..#salmsern_table') is not null
	drop table #salmsern_table



/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_CCH]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_CCH]
/***********************************/


/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_CHN]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_CHN]
/***********************************/


/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_STORE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_STORE]
/***********************************/


/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SALESFORCE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SALESFORCE]
/***********************************/




/********************************************************************************************* UNION ****************************************************************************************************/

CREATE TABLE #COMSERNO_TABLE(COMSERNO varchar(15)  PRIMARY KEY)

INSERT INTO #COMSERNO_TABLE
SELECT  COMSERNO
FROM (
select  COMSERNO  from TDELIVER  
union  
select  COMSERNO from TDPMHDR 
union 
select  COMSERNO from TORDER 
union 
select  COMSERNO from  TPLNHDR 
union 
select  COMSERNO from  TSAMERCH
union 
select  COMSERNO from  TCALENTR
union 
select  COMSERNO from  TTARENTR
union 
select  COMSERNO from  TMSASLIP
union 
select  COMSERNO from  TTARENTR
union 
select  COMSERNO from  LCOMSEL
union 
select  COMSERNO from  LSALCOM
) STORES
WHERE COMSERNO IS NOT NULL








CREATE TABLE #SALMSERN_TABLE(SALMSERN varchar(15)  PRIMARY KEY)

INSERT INTO #SALMSERN_TABLE
SELECT DISTINCT  salmsern
FROM (
select  salmsern  from TDELENTR  
union  
select   salmsern from TDPMHDR 
union 
select   salmsern from TORDER 
union 
--select  salmsern from  TPLNHDR 
--union 
select  salmsern from  TSAMERCH
union 
select  salmsern  from  TCALENTR
union
select  salmsern from  TTARENTR
union 
select  salmsern  from  TMSASLIP
union
select salmsern  from  TTARENTR
union 
select  salmsern  from  LCOMSEL
union 
select  salmsern  from  LSALCOM
) SALESMEN
WHERE salmsern IS NOT NULL






/***********************************************************************************************************************************************************************************************************/


/***********************************/
CREATE TABLE [spp].[OLAP_CCH] (
	[COMSERNCCH] [varchar] (15)  NOT NULL PRIMARY KEY ,
	[TGRNAME] [varchar] (50)   NULL DEFAULT 'Default Trade Group' ,
	[CCHNAME] [varchar] (50)   NULL DEFAULT 'Undefined' 
) ON [PRIMARY]
/***********************************/


/***********************************/
CREATE TABLE [spp].[OLAP_CHN] (
	[COMSERNCHN] [varchar] (15)  NOT NULL PRIMARY KEY ,
	[CHNNAME] [varchar] (50)   NULL DEFAULT 'Undefined' 
) ON [PRIMARY]
/***********************************/



/***********************  CREATE TABLE OLAP_STORE + OLAP_SALESFORCE **************************/

SET @command_string='CREATE TABLE [spp].[OLAP_STORE] (
[COMSERNO] [varchar] (15)  NOT NULL  PRIMARY KEY,
[NR] int  NOT NULL  IDENTITY(1,1),
[COMNAME] [varchar] (50)   NULL  DEFAULT ''Undefined'',
[COMPCODE] [varchar] (10)  NULL DEFAULT ''Undefined'',
[COMPCITY] [varchar] (30)  NULL DEFAULT ''Undefined'',
[COMPADDR] [varchar] (40)  NULL DEFAULT ''Undefined'',
[COMTYNAME] [varchar] (15)  NULL DEFAULT ''Undefined'',
[COMCLASS] [varchar] (8)    NULL DEFAULT ''Undef'',
[COMTURNCLS] [varchar] (4)    NULL DEFAULT ''Undf'',
[TGRNAME] [varchar] (50)   NULL  DEFAULT ''Default Trade Group'',
[CCHNAME] [varchar] (50)   NULL  DEFAULT ''Undefined'',
[CHNNAME] [varchar] (50)   NULL  DEFAULT ''Undefined'',
[COMCALL] [varchar] (3)   NULL  DEFAULT ''No'',
[COMINACT] [varchar] (8)   NULL  DEFAULT ''99999999''  '



IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT1 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT1)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		SET @command_string=@command_string +', [GRP@#@COMTEXT1] [varchar] (30)   NULL  DEFAULT ' + CHAR(39) + 'Undefined' + CHAR(39)
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT2 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT2)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		SET @command_string=@command_string +', [GRP@#@COMTEXT2] [varchar] (30)   NULL  DEFAULT ' + CHAR(39) + 'Undefined' + CHAR(39)
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT3 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT3)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		SET @command_string=@command_string + ', [GRP@#@COMTEXT3] [varchar] (30)   NULL  DEFAULT ' + CHAR(39) + 'Undefined' + CHAR(39)
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT4 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT4)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		SET @command_string=@command_string + ', [GRP@#@COMTEXT4] [varchar] (30)   NULL  DEFAULT ' + CHAR(39) + 'Undefined' + CHAR(39)
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT5 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT5)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		SET @command_string=@command_string + ', [GRP@#@COMTEXT5] [varchar] (30)   NULL  DEFAULT ' + CHAR(39) + 'Undefined' + CHAR(39)
	END


SET @command_string=@command_string + ') ON [PRIMARY]'
EXECUTE(@command_string)



/***********************************/
CREATE NONCLUSTERED INDEX IX_OLAP_STORE_COMINACT
ON spp.OLAP_STORE(COMINACT)


























INSERT INTO OLAP_STORE(COMSERNO , COMNAME, COMPCODE, COMPCITY , COMPADDR,  COMCLASS , COMTURNCLS, COMCALL , COMINACT)
SELECT  TCOMPANY.COMSERNO , CAST(TCOMPANY.COMNAME + '-' + TCOMPANY.COMCODE as varchar(50)) AS COMNAME , 

CASE LEN(LTRIM(TCOMPANY.COMPCODE))
WHEN 0 THEN 'Undefined'
ELSE TCOMPANY.COMPCODE
END AS COMPCODE,

CASE 
	WHEN LEN(LTRIM(TCOMPANY.COMPCITY))>0 THEN TCOMPANY.COMPCITY
	WHEN LEN(LTRIM(TCOMPANY.COMCITY))>0 THEN TCOMPANY.COMCITY
	ELSE 'Undefined'
END AS COMPCITY,

CASE 
	WHEN LEN(LTRIM(TCOMPANY.COMPADDR))>0 THEN TCOMPANY.COMPADDR
	WHEN LEN(LTRIM(TCOMPANY.COMADDR))>0 THEN TCOMPANY.COMADDR
	ELSE 'Undefined'
END AS COMPADDR,

CASE LEN(LTRIM(TCOMPANY.COMCLASS))
WHEN 0 THEN 'Undef'
ELSE TCOMPANY.COMCLASS
END AS COMCLASS ,

CASE LEN(LTRIM(TCOMPANY.COMTURNCLS))
WHEN 0 THEN 'Undf'
ELSE TCOMPANY.COMTURNCLS
END AS COMTURNCLS ,

CASE TCOMPANY.COMCALL
WHEN 1 THEN 'Yes'
ELSE 'No'
END AS COMCALL,

CASE 
WHEN TCOMPANY.COMINACT IS NOT NULL AND ISDATE(TCOMPANY.COMINACT)=1 THEN TCOMPANY.COMINACT
ELSE '99999999'
END AS COMINACT

FROM (SELECT COMSERNO FROM #COMSERNO_TABLE) STORES
INNER JOIN TCOMPANY ON STORES.COMSERNO=TCOMPANY.COMSERNO




IF NOT EXISTS(SELECT * FROM OLAP_STORE WHERE COMSERNO='')
	INSERT INTO OLAP_STORE(COMSERNO , COMNAME)
		SELECT '' AS COMSERNO  , 'Undefined'  AS COMNAME

IF NOT EXISTS(SELECT * FROM OLAP_STORE WHERE COMSERNO='0')
	INSERT INTO OLAP_STORE(COMSERNO , COMNAME)
		SELECT '0' AS COMSERNO  , 'Undefined'  AS COMNAME













--------- DIRECT Central Chain -----


INSERT INTO OLAP_CCH(COMSERNCCH)
SELECT DISTINCT ISNULL(COMSERNCCH,'')  AS COMSERNCCH  FROM
	(
	SELECT  TDELIVER.COMSERNCCH FROM TDELIVER
	UNION
	SELECT  TORDER.COMSERNCCH FROM TORDER
	UNION
	SELECT  TMSASLIP.COMSERNCCH FROM TMSASLIP
	) CCH


UPDATE OLAP_CCH
	SET CCHNAME=ISNULL((SELECT TOP 1 COMNAME FROM TCOMPANY WHERE TCOMPANY.COMSERNO=OLAP_CCH.COMSERNCCH ),CCHNAME)


UPDATE OLAP_CCH
SET TGRNAME=ISNULL(
(SELECT TOP 1 TCOMPANY.COMNAME 
	FROM LCOMCOM INNER JOIN TCOMPANY ON LCOMCOM.COMSERN1=TCOMPANY.COMSERNO 
	WHERE UPPER(LCOMCOM.LCOMATR2)='CENTRAL CHAIN' AND LCOMCOM.COMSERN2=OLAP_CCH.COMSERNCCH
	AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
	AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	ORDER BY LCOMCOM.LCOMSTART DESC)
, TGRNAME)




--------- DIRECT Chain -----

INSERT INTO OLAP_CHN(COMSERNCHN)
SELECT DISTINCT ISNULL(COMSERNCHN,'')  AS COMSERNCHN  FROM
	(
	SELECT  TDELIVER.COMSERNCHN FROM TDELIVER
	UNION
	SELECT  TORDER.COMSERNCHN FROM TORDER
	UNION
	SELECT  TMSASLIP.COMSERNCHN FROM TMSASLIP
	) CHN


UPDATE OLAP_CHN
	SET CHNNAME=ISNULL((SELECT TOP 1 COMNAME FROM TCOMPANY WHERE TCOMPANY.COMSERNO=OLAP_CHN.COMSERNCHN),CHNNAME)








/********************** CENTRAL CHAIN **********************/

--central chain through cch->ret
UPDATE OLAP_STORE
SET CCHNAME=ISNULL(
(SELECT TOP 1 TCOMPANY.COMNAME 
	FROM LCOMCOM INNER JOIN TCOMPANY ON LCOMCOM.COMSERN1=TCOMPANY.COMSERNO 
	WHERE UPPER(LCOMCOM.LCOMATR1)='CENTRAL CHAIN' AND LCOMCOM.COMSERN2=OLAP_STORE.COMSERNO
	AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
	AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	ORDER BY LCOMCOM.LCOMSTART DESC)
, CCHNAME)


--central chain through cch->chn->ret
UPDATE OLAP_STORE
SET CCHNAME=ISNULL(
(SELECT TOP 1 CCH.COMNAME 
	FROM
	(SELECT COMSERN1 AS CHNSERN , COMSERN2 AS COMSERNO , LCOMSTART  FROM LCOMCOM 
		WHERE UPPER(LCOMCOM.LCOMATR1)='CHAIN' 
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) CHN_TBL
	INNER JOIN 
	(SELECT COMSERN1 AS CCHSERN , COMSERN2 AS CHNSERN , LCOMSTART FROM LCOMCOM 
		WHERE UPPER(LCOMCOM.LCOMATR1)='CENTRAL CHAIN'  AND UPPER(LCOMCOM.LCOMATR2)='CHAIN'  
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) CCH_TBL
	ON CHN_TBL.CHNSERN=CCH_TBL.CHNSERN
	INNER JOIN TCOMPANY CCH ON CCH_TBL.CCHSERN=CCH.COMSERNO
	WHERE CHN_TBL.COMSERNO=OLAP_STORE.COMSERNO
	ORDER BY CCH_TBL.LCOMSTART DESC , CHN_TBL.LCOMSTART DESC)
, CCHNAME)



-- trading group through tgr->cch->ret
UPDATE OLAP_STORE
SET TGRNAME=ISNULL(
(SELECT TOP 1 TGR.COMNAME 
	FROM
	(SELECT COMSERN1 AS CCHSERN , COMSERN2 AS COMSERNO , LCOMSTART FROM LCOMCOM 
		WHERE UPPER(LCOMCOM.LCOMATR1)='CENTRAL CHAIN'  
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) CCH_TBL
	INNER JOIN 
	(SELECT COMSERN1 AS TGRSERN , COMSERN2 AS CCHSERN , LCOMSTART FROM LCOMCOM 
		WHERE  UPPER(LCOMCOM.LCOMATR2)='CENTRAL CHAIN'  
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) TGR_TBL
	ON TGR_TBL.CCHSERN=CCH_TBL.CCHSERN
	INNER JOIN TCOMPANY TGR ON TGR_TBL.TGRSERN=TGR.COMSERNO
	WHERE CCH_TBL.COMSERNO=OLAP_STORE.COMSERNO
	ORDER BY TGR_TBL.LCOMSTART DESC,  CCH_TBL.LCOMSTART DESC )
, TGRNAME)


-- trading group through tgr->cch->chn->ret
UPDATE OLAP_STORE
SET TGRNAME=ISNULL(
(SELECT TOP 1 TGR.COMNAME 
	FROM
	(SELECT COMSERN1 AS CHNSERN , COMSERN2 AS COMSERNO , LCOMSTART  FROM LCOMCOM 
		WHERE UPPER(LCOMCOM.LCOMATR1)='CHAIN' 
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) CHN_TBL
	INNER JOIN 
	(SELECT COMSERN1 AS CCHSERN , COMSERN2 AS CHNSERN , LCOMSTART FROM LCOMCOM 
		WHERE UPPER(LCOMCOM.LCOMATR1)='CENTRAL CHAIN'  AND UPPER(LCOMCOM.LCOMATR2)='CHAIN'  
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) CCH_TBL
	ON CHN_TBL.CHNSERN=CCH_TBL.CHNSERN
	INNER JOIN 
	(SELECT COMSERN1 AS TGRSERN , COMSERN2 AS CCHSERN , LCOMSTART FROM LCOMCOM 
		WHERE  UPPER(LCOMCOM.LCOMATR2)='CENTRAL CHAIN'  
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	) TGR_TBL
	ON TGR_TBL.CCHSERN=CCH_TBL.CCHSERN
	INNER JOIN TCOMPANY TGR ON TGR_TBL.TGRSERN=TGR.COMSERNO
	WHERE CHN_TBL.COMSERNO=OLAP_STORE.COMSERNO
	ORDER BY TGR_TBL.LCOMSTART DESC,  CCH_TBL.LCOMSTART DESC , CHN_TBL.LCOMSTART DESC)
, TGRNAME)

/*******************************************************************/





/**************************** CHAIN ******************************/

UPDATE OLAP_STORE
SET CHNNAME=ISNULL(
(SELECT TOP 1 TCOMPANY.COMNAME 
	FROM LCOMCOM INNER JOIN TCOMPANY ON LCOMCOM.COMSERN1=TCOMPANY.COMSERNO 
	WHERE UPPER(LCOMCOM.LCOMATR1)='CHAIN' 
	AND LCOMCOM.COMSERN2=OLAP_STORE.COMSERNO
	AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
	AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	ORDER BY LCOMCOM.LCOMSTART DESC)
, CHNNAME)


-- chains that appear as stores will have their names as chain names ---
UPDATE OLAP_STORE
SET OLAP_STORE.CHNNAME=CHAINS.COMNAME
FROM
( SELECT DISTINCT COMSERNO , COMNAME FROM TCOMPANY WHERE COMSERNO IN 
	(SELECT COMSERN1 FROM LCOMCOM 
		WHERE UPPER(LCOMATR1)='CHAIN' 
		AND (CASE WHEN LEN(LCOMCOM.LCOMSTART)=8 THEN LCOMCOM.LCOMSTART ELSE '00000000' END)<=@TODAY
		AND (CASE WHEN LEN(LCOMCOM.LCOMEND)=8 THEN LCOMCOM.LCOMEND ELSE '99999999' END)>=@TODAY
	)
) CHAINS
WHERE OLAP_STORE.COMSERNO=CHAINS.COMSERNO



/**************************** COMTYNAME ******************************/

UPDATE OLAP_STORE
SET OLAP_STORE.COMTYNAME=ISNULL(
(SELECT TOP 1 COMTYNAME FROM TCOMTYPE WHERE TCOMTYPE.COMSERNO=OLAP_STORE.COMSERNO ORDER BY DATESTAMP DESC)
,COMTYNAME)







/**************************** COMTEXT1-5 ******************************/

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT1 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT1)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		EXECUTE ('UPDATE OLAP_STORE
			SET OLAP_STORE.GRP@#@COMTEXT1=TCOMPANY.COMTEXT1 FROM TCOMPANY 
			WHERE OLAP_STORE.COMSERNO=TCOMPANY.COMSERNO AND TCOMPANY.COMTEXT1 IS NOT NULL AND LEN(RTRIM(LTRIM(TCOMPANY.COMTEXT1)))>0')
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT2 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT2)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		EXECUTE ('UPDATE OLAP_STORE 
			SET OLAP_STORE.GRP@#@COMTEXT2=TCOMPANY.COMTEXT2 FROM TCOMPANY 
			WHERE OLAP_STORE.COMSERNO=TCOMPANY.COMSERNO AND TCOMPANY.COMTEXT2 IS NOT NULL AND LEN(RTRIM(LTRIM(TCOMPANY.COMTEXT2)))>0')
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT3 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT3)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		EXECUTE ('UPDATE OLAP_STORE 
			SET OLAP_STORE.GRP@#@COMTEXT3=TCOMPANY.COMTEXT3 FROM TCOMPANY 
			WHERE OLAP_STORE.COMSERNO=TCOMPANY.COMSERNO AND TCOMPANY.COMTEXT3 IS NOT NULL AND LEN(RTRIM(LTRIM(TCOMPANY.COMTEXT3)))>0')
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT4 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT4)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		EXECUTE ('UPDATE OLAP_STORE 
			SET OLAP_STORE.GRP@#@COMTEXT4=TCOMPANY.COMTEXT4 FROM TCOMPANY 
			WHERE OLAP_STORE.COMSERNO=TCOMPANY.COMSERNO AND TCOMPANY.COMTEXT4 IS NOT NULL AND LEN(RTRIM(LTRIM(TCOMPANY.COMTEXT4)))>0')
	END

IF EXISTS(SELECT * FROM TCOMPANY WHERE COMTEXT5 IS NOT NULL AND LEN(RTRIM(LTRIM(COMTEXT5)))>0 AND COMSERNO IN (SELECT COMSERNO FROM #COMSERNO_TABLE))
	BEGIN
		EXECUTE ('UPDATE OLAP_STORE 

			SET OLAP_STORE.GRP@#@COMTEXT5=TCOMPANY.COMTEXT5 FROM TCOMPANY 

			WHERE OLAP_STORE.COMSERNO=TCOMPANY.COMSERNO AND TCOMPANY.COMTEXT5 IS NOT NULL AND LEN(RTRIM(LTRIM(TCOMPANY.COMTEXT5)))>0')
	END











------------------------------- SALESFORCE_HIERARCHY-------------------------------------

--- OLAP_SALESFORCE
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SALESFORCE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SALESFORCE]

CREATE TABLE [spp].[OLAP_SALESFORCE] (
	[SALMSERN] [varchar] (15) PRIMARY KEY NOT NULL,
	[SALMNAME] [varchar] (30)  NOT NULL,
	[PGRSERN] [varchar](15) NOT NULL
) ON [PRIMARY]

INSERT INTO OLAP_SALESFORCE(SALMSERN , SALMNAME, PGRSERN )
SELECT SALMSERN , SALMNAME, ISNULL(PGRSERN ,'')
FROM TSALMAN WHERE SALMSERN IN (SELECT SALMSERN FROM #SALMSERN_TABLE)


--- INDEX ON PGRSERN
CREATE NONCLUSTERED INDEX IX_OLAP_SALESFORCE_PGRSERN
ON spp.OLAP_SALESFORCE(PGRSERN)

--- OLAP_SALESFORCE_HIERARCHY
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SALESFORCE_HIERARCHY]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SALESFORCE_HIERARCHY]

CREATE TABLE [spp].[OLAP_SALESFORCE_HIERARCHY] (
	[SALMGRPSERN] [varchar] (15)  NOT NULL ,
	[SALMSERN] [varchar] (15) NOT NULL ,
	[SALMNAME] [varchar] (30)  NOT NULL ,
	[SALMGRP] [varchar] (30)  NOT NULL ,
PRIMARY KEY ([SALMGRPSERN], [SALMSERN])
) ON [PRIMARY]

INSERT INTO OLAP_SALESFORCE_HIERARCHY([SALMGRPSERN], SALMSERN , SALMNAME , SALMGRP )
SELECT DISTINCT UGRP.UGRPSERN, OLAP_SALESFORCE.SALMSERN , OLAP_SALESFORCE.SALMNAME , UGRP.UGRPNAME
	FROM
	(SELECT TUSERGRP.UGRPNAME, LGRPUSER.USERSERN, LGRPUSER.UGRPSERN FROM LGRPUSER, TUSERGRP WHERE LGRPUSER.UGRPSERN=TUSERGRP.UGRPSERN) UGRP
	INNER JOIN OLAP_SALESFORCE ON UGRP.USERSERN=OLAP_SALESFORCE.SALMSERN


--- OLAP_STORE_SALESFORCE_HIERARCHY
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_STORE_SALESFORCE_HIERARCHY]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_STORE_SALESFORCE_HIERARCHY]

CREATE TABLE [spp].[OLAP_STORE_SALESFORCE_HIERARCHY] (
	[SALMSERN] [varchar] (15)  NOT NULL ,
	[COMSERNO] [varchar] (15) NOT NULL,
	[PGRSERN] [varchar] (15) NOT NULL,
	[SALMNAME] [varchar] (30)  NOT NULL ,
	[COMNAME] [varchar] (50) NULL,
PRIMARY KEY (SALMSERN, COMSERNO)
) ON [PRIMARY]

INSERT INTO OLAP_STORE_SALESFORCE_HIERARCHY(SALMSERN , COMSERNO, PGRSERN, SALMNAME,  COMNAME )
SELECT DISTINCT OLAP_SALESFORCE.SALMSERN , STORES.COMSERNO , OLAP_SALESFORCE.PGRSERN, OLAP_SALESFORCE.SALMNAME, STORES.COMNAME	
	FROM OLAP_SALESFORCE
	INNER JOIN 
	(
	SELECT DISTINCT OLAP_STORE.COMSERNO , OLAP_STORE.COMNAME, TSALMAN.SALMSERN
		FROM LSALCOM INNER JOIN TSALMAN ON LSALCOM.SALMSERN=TSALMAN.SALMSERN
		INNER JOIN OLAP_STORE ON LSALCOM.COMSERNO=OLAP_STORE.COMSERNO
		-- WHERE (CASE ISNULL(LSCEND,'') WHEN '' THEN '99999999' ELSE LSCEND END)>=CONVERT(varchar(8) , GetDate() , 112)  -- commented out because of verion incompatibility
	) STORES
	 ON STORES.SALMSERN=OLAP_SALESFORCE.SALMSERN

--intersection 
INSERT INTO OLAP_STORE_SALESFORCE_HIERARCHY(SALMSERN , SALMNAME , COMSERNO, PGRSERN, COMNAME )
SELECT '-1', 'Intersection', OLAP_STORE.COMSERNO, '', OLAP_STORE.COMNAME
FROM
(SELECT DISTINCT COMSERNO
FROM spp.OLAP_LCOMPGR OLAP_LCOMPGR  LEFT OUTER JOIN spp.OLAP_LPROPGR OLAP_LPROPGR ON OLAP_LCOMPGR.PGRSERN=OLAP_LPROPGR.PGRSERN 
GROUP BY COMSERNO, PRODSERN
HAVING COUNT(*)>1
) TBL
INNER JOIN OLAP_STORE ON TBL.COMSERNO=OLAP_STORE.COMSERNO



------------------------------------------------------------------------------------------------------------------------------





DROP TABLE #COMSERNO_TABLE
DROP TABLE #SALMSERN_TABLE



GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_SURVEY]    Script Date: 01/09/2008 18:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO














CREATE PROCEDURE [spp].[proc_fill_OLAP_SURVEY]
AS

SET NOCOUNT ON

/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SURVEY]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_SURVEY]
/***********************************/


/* SELECT INTO */
select 
IDENTITY(int,1,1) AS SURVEY_KEY ,
SALMSERN,
COMSERNO,
SAMCHDATE,
ANSWER,
QUESTION,
ANSWER_MEASURE
INTO spp.OLAP_SURVEY
from 
(
select  tsamerch.samchsern + tquest.questsern as SURVEY_KEY , tsamerch.salmsern , tsamerch.comserno , tsamerch.samchdate , 
CAST(1 as real) AS ANSWER_MEASURE ,
question , answer
from spp.tsamerch tsamerch
inner join spp.tqanswer tqanswer on tsamerch.samchsern=tqanswer.samchsern
inner join spp.tquest tquest on tqanswer.questsern=tquest.questsern
where tquest.ANSWFORM!='2'
UNION
select  tsamerch.samchsern + tquest.questsern as SURVEY_KEY ,  tsamerch.salmsern , tsamerch.comserno , tsamerch.samchdate , 
CASE 
	WHEN LTRIM(RTRIM(tqanswer.answer))='-' THEN 0 -- STUPID SQL SERVER BUG!!!
	WHEN  ISNUMERIC(tqanswer.answer)=1 THEN CAST(REPLACE(tqanswer.answer , ',' , '.') as real)
	ELSE 0
END AS ANSWER_MEASURE ,
question , 'Numeric' as answer
from spp.tsamerch tsamerch
inner join spp.tqanswer tqanswer on tsamerch.samchsern=tqanswer.samchsern
inner join spp.tquest tquest on tqanswer.questsern=tquest.questsern
where tquest.ANSWFORM='2' 
) tbl



-- INVALID CHARACTER WORKAROUND
SET NOCOUNT ON
DECLARE @c CHAR 
DECLARE @i INT
SET @i=0

WHILE @i<32
BEGIN
	SET @c=CAST(@i AS CHAR)

	EXEC( 
	'UPDATE spp.OLAP_SURVEY
	SET 
	QUESTION=REPLACE(QUESTION, CHAR(' + @c + '), ''''),
	ANSWER=REPLACE(ANSWER, CHAR(' + @c + '), '''')
	'
	)

	SET @i=@i+1
END

GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_TARGET]    Script Date: 01/09/2008 18:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

















CREATE PROCEDURE [spp].[proc_fill_OLAP_TARGET] AS
SET NOCOUNT ON 
/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_TTARENTR_PRODGRP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_TTARENTR_PRODGRP]
/***********************************/
/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_TTARENTR_PRODUCT]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_TTARENTR_PRODUCT]
/***********************************/
/***********************************/
CREATE TABLE [spp].[OLAP_TTARENTR_PRODGRP] (
	[TARGSERN] [varchar] (15) ,
	[SALMSERN] [varchar] (15) ,
	[COMSERNO] [varchar] (15) ,
	[PRODSERN] [varchar] (15) ,
	[DATE] [varchar] (8) ,
	[TARGEVOLUM] float ,
	[TARGEMONEY] float ,
	[TARGENUM1] float ,
	[TARGENUM2] float ,
	[TARGENUM3] float ,
	[TARGENUM4] float ,
	[TARGMONEY1] float ,
	[TARGMONEY2] float ,
	[TARGMONEY3] float ,
	[TARGMONEY4] float ,
) ON [PRIMARY]
/***********************************/
/***********************************/
CREATE TABLE [spp].[OLAP_TTARENTR_PRODUCT] (
	[TARGSERN] [varchar] (15) ,
	[SALMSERN] [varchar] (15) ,
	[COMSERNO] [varchar] (15) ,
	[PRODSERN] [varchar] (15) ,
	[DATE] [varchar] (8) ,
	[TARGEVOLUM] float ,
	[TARGEMONEY] float ,
	[TARGENUM1] float ,
	[TARGENUM2] float ,
	[TARGENUM3] float ,
	[TARGENUM4] float ,
	[TARGMONEY1] float ,
	[TARGMONEY2] float ,
	[TARGMONEY3] float ,
	[TARGMONEY4] float ,
) ON [PRIMARY]
/***********************************/
INSERT INTO 
OLAP_TTARENTR_PRODGRP
(
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
TARGEVOLUM,
TARGEMONEY,
TARGENUM1,
TARGENUM2,
TARGENUM3,
TARGENUM4,
TARGMONEY1,
TARGMONEY2,
TARGMONEY3,
TARGMONEY4
)
SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
OLAP_DATE.DATE,
TARGEVOLUM/WRKDAYS AS TARGEVOLUM,
TARGEMONEY/WRKDAYS AS TARGEMONEY,
TARGENUM1/WRKDAYS AS TARGENUM1,
TARGENUM2/WRKDAYS AS TARGENUM2,
TARGENUM3/WRKDAYS AS TARGENUM3,
TARGENUM4/WRKDAYS AS TARGENUM4,
TARGMONEY1/WRKDAYS AS TARGMONEY1,
TARGMONEY2/WRKDAYS AS TARGMONEY2,
TARGMONEY3/WRKDAYS AS TARGMONEY3,
TARGMONEY4/WRKDAYS AS TARGMONEY4
FROM
(
SELECT  * FROM 
(
SELECT
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
TARGETIME,
TARGEVOLUM,
TARGEMONEY,
TARGENUM1,
TARGENUM2,
TARGENUM3,
TARGENUM4,
TARGMONEY1,
TARGMONEY2,
TARGMONEY3,
TARGMONEY4,
TARGTIMEUN,
(SELECT SUM(WRKDAY) FROM spp.OLAP_DATE  OLAP_DATE
WHERE 
(TARGTIMEUN=2 AND TARGETIME<=OLAP_DATE.WEEK AND TARGETIME>=OLAP_DATE.WEEK ) 
OR
(TARGTIMEUN=3 AND TARGETIME<=OLAP_DATE.MONTH AND TARGETIME>=OLAP_DATE.MONTH ) 
OR
(TARGTIMEUN=4 AND TARGETIME<=OLAP_DATE.YEAR AND TARGETIME>=OLAP_DATE.YEAR ) 
OR
(TARGTIMEUN=5 AND TARGETIME<=OLAP_DATE.QUARTER AND TARGETIME>=OLAP_DATE.QUARTER) 
OR
(TARGTIMEUN=6 AND TARGETIME<=OLAP_DATE.SALENUM AND TARGETIME>=OLAP_DATE.SALENUM) 
) AS WRKDAYS
FROM spp.TTARENTR  TTARENTR
WHERE COMSERNO<>'*' AND PRODSERN<>'*'   AND TARGEPRODF=1
) TTARENTR_COM_PROD
UNION 
SELECT  * FROM 
(
SELECT
TARGSERN,
SALMSERN,
'0' AS COMSERNO,
PRODSERN,
TARGETIME,
TARGEVOLUM,
TARGEMONEY,
TARGENUM1,
TARGENUM2,
TARGENUM3,
TARGENUM4,
TARGMONEY1,
TARGMONEY2,
TARGMONEY3,
TARGMONEY4,
TARGTIMEUN,
(SELECT SUM(WRKDAY) FROM spp.OLAP_DATE OLAP_DATE
WHERE 
(TARGTIMEUN=2 AND TARGETIME<=OLAP_DATE.WEEK AND TARGETIME>=OLAP_DATE.WEEK ) 
OR
(TARGTIMEUN=3 AND TARGETIME<=OLAP_DATE.MONTH AND TARGETIME>=OLAP_DATE.MONTH ) 
OR
(TARGTIMEUN=4 AND TARGETIME<=OLAP_DATE.YEAR AND TARGETIME>=OLAP_DATE.YEAR ) 
OR
(TARGTIMEUN=5 AND TARGETIME<=OLAP_DATE.QUARTER AND TARGETIME>=OLAP_DATE.QUARTER) 
OR
(TARGTIMEUN=6 AND TARGETIME<=OLAP_DATE.SALENUM AND TARGETIME>=OLAP_DATE.SALENUM) 
) AS WRKDAYS
FROM spp.TTARENTR  TTARENTR
WHERE COMSERNO='*' AND PRODSERN<>'*'   AND TARGEPRODF=1
) TTARENTR_PROD
) TTARENTR
INNER JOIN spp.OLAP_DATE OLAP_DATE ON 
(
TARGETIME<=(
CASE TARGTIMEUN
	WHEN '2' THEN OLAP_DATE.WEEK 
	WHEN '3' THEN OLAP_DATE.MONTH
	WHEN '4' THEN OLAP_DATE.YEAR
	WHEN '5' THEN OLAP_DATE.QUARTER
	WHEN '6' THEN OLAP_DATE.SALENUM
END
)
AND 
TARGETIME>=(
CASE TARGTIMEUN
	WHEN '2' THEN OLAP_DATE.WEEK 
	WHEN '3' THEN OLAP_DATE.MONTH
	WHEN '4' THEN OLAP_DATE.YEAR
	WHEN '5' THEN OLAP_DATE.QUARTER
	WHEN '6' THEN OLAP_DATE.SALENUM
END)
AND WRKDAY=1
)


















INSERT INTO 
OLAP_TTARENTR_PRODUCT
(
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
DATE,
TARGEVOLUM,
TARGEMONEY,
TARGENUM1,
TARGENUM2,
TARGENUM3,
TARGENUM4,
TARGMONEY1,
TARGMONEY2,
TARGMONEY3,
TARGMONEY4
)
SELECT 
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
OLAP_DATE.DATE,
TARGEVOLUM/WRKDAYS AS TARGEVOLUM,
TARGEMONEY/WRKDAYS AS TARGEMONEY,
TARGENUM1/WRKDAYS AS TARGENUM1,
TARGENUM2/WRKDAYS AS TARGENUM2,
TARGENUM3/WRKDAYS AS TARGENUM3,
TARGENUM4/WRKDAYS AS TARGENUM4,
TARGMONEY1/WRKDAYS AS TARGMONEY1,
TARGMONEY2/WRKDAYS AS TARGMONEY2,
TARGMONEY3/WRKDAYS AS TARGMONEY3,
TARGMONEY4/WRKDAYS AS TARGMONEY4
FROM
(
SELECT * FROM 
(
SELECT 
TTARENTR_COM.TARGSERN,
TTARENTR_COM.SALMSERN,
TTARENTR_COM.COMSERNO,
'0' AS PRODSERN,
TTARENTR_COM.TARGETIME,
TTARENTR_COM.TARGEVOLUM-ISNULL(TTARENTR_COM_PROD.TARGEVOLUM, 0 )  AS TARGEVOLUM,
TTARENTR_COM.TARGEMONEY-ISNULL(TTARENTR_COM_PROD.TARGEMONEY, 0 )   AS TARGEMONEY,
TTARENTR_COM.TARGENUM1-ISNULL(TTARENTR_COM_PROD.TARGENUM1, 0 )   AS TARGENUM1,
TTARENTR_COM.TARGENUM2-ISNULL(TTARENTR_COM_PROD.TARGENUM2, 0 )   AS TARGENUM2,
TTARENTR_COM.TARGENUM3-ISNULL(TTARENTR_COM_PROD.TARGENUM3, 0 )   AS TARGENUM3,
TTARENTR_COM.TARGENUM4-ISNULL(TTARENTR_COM_PROD.TARGENUM4, 0 )   AS TARGENUM4,
TTARENTR_COM.TARGMONEY1-ISNULL(TTARENTR_COM_PROD.TARGMONEY1, 0 )   AS TARGMONEY1,
TTARENTR_COM.TARGMONEY2-ISNULL(TTARENTR_COM_PROD.TARGMONEY2, 0 )   AS TARGMONEY2,
TTARENTR_COM.TARGMONEY3-ISNULL(TTARENTR_COM_PROD.TARGMONEY3, 0 )   AS TARGMONEY3,
TTARENTR_COM.TARGMONEY4-ISNULL(TTARENTR_COM_PROD.TARGMONEY4, 0 )   AS TARGMONEY4,
TTARENTR_COM.TARGTIMEUN,
(SELECT SUM(WRKDAY) FROM spp.OLAP_DATE  OLAP_DATE
WHERE 
(TARGTIMEUN=2 AND TTARENTR_COM.TARGETIME<=OLAP_DATE.WEEK AND TTARENTR_COM.TARGETIME>=OLAP_DATE.WEEK ) 
OR
(TARGTIMEUN=3 AND TTARENTR_COM.TARGETIME<=OLAP_DATE.MONTH AND TTARENTR_COM.TARGETIME>=OLAP_DATE.MONTH ) 
OR
(TARGTIMEUN=4 AND TTARENTR_COM.TARGETIME<=OLAP_DATE.YEAR AND TTARENTR_COM.TARGETIME>=OLAP_DATE.YEAR ) 
OR
(TARGTIMEUN=5 AND TTARENTR_COM.TARGETIME<=OLAP_DATE.QUARTER AND TTARENTR_COM.TARGETIME>=OLAP_DATE.QUARTER) 
OR
(TARGTIMEUN=6 AND TTARENTR_COM.TARGETIME<=OLAP_DATE.SALENUM AND TTARENTR_COM.TARGETIME>=OLAP_DATE.SALENUM) 
) AS WRKDAYS
FROM
(SELECT * FROM spp.TTARENTR TTARENTR WHERE COMSERNO<>'*' AND PRODSERN='*' ) TTARENTR_COM
LEFT OUTER JOIN
(
SELECT
TARGSERN,
SALMSERN,
COMSERNO,
TARGETIME,
SUM(TARGEVOLUM) AS TARGEVOLUM,
SUM(TARGEMONEY) AS TARGEMONEY,
SUM(TARGENUM1) AS TARGENUM1,
SUM(TARGENUM2) AS TARGENUM2,
SUM(TARGENUM3) AS TARGENUM3,
SUM(TARGENUM4) AS TARGENUM4,
SUM(TARGMONEY1) AS TARGMONEY1,
SUM(TARGMONEY2) AS TARGMONEY2,
SUM(TARGMONEY3) AS TARGMONEY3,
SUM(TARGMONEY4) AS TARGMONEY4
FROM spp.TTARENTR  TTARENTR
WHERE COMSERNO<>'*' AND PRODSERN<>'*' 
GROUP BY
TARGSERN,
SALMSERN,
COMSERNO,
TARGETIME 
) TTARENTR_COM_PROD
ON
TTARENTR_COM_PROD.TARGSERN=TTARENTR_COM.TARGSERN AND 
TTARENTR_COM_PROD.SALMSERN=TTARENTR_COM.SALMSERN AND 
TTARENTR_COM_PROD.COMSERNO=TTARENTR_COM.COMSERNO AND 
TTARENTR_COM_PROD.TARGETIME=TTARENTR_COM.TARGETIME 
) TTARENTR_COM
UNION
SELECT  * FROM 
(
SELECT
TARGSERN,
SALMSERN,
COMSERNO,
PRODSERN,
TARGETIME,
TARGEVOLUM,
TARGEMONEY,
TARGENUM1,
TARGENUM2,
TARGENUM3,
TARGENUM4,
TARGMONEY1,
TARGMONEY2,
TARGMONEY3,
TARGMONEY4,
TARGTIMEUN,
(SELECT SUM(WRKDAY) FROM spp.OLAP_DATE  OLAP_DATE
WHERE 
(TARGTIMEUN=2 AND TARGETIME<=OLAP_DATE.WEEK AND TARGETIME>=OLAP_DATE.WEEK ) 
OR
(TARGTIMEUN=3 AND TARGETIME<=OLAP_DATE.MONTH AND TARGETIME>=OLAP_DATE.MONTH ) 
OR
(TARGTIMEUN=4 AND TARGETIME<=OLAP_DATE.YEAR AND TARGETIME>=OLAP_DATE.YEAR ) 
OR
(TARGTIMEUN=5 AND TARGETIME<=OLAP_DATE.QUARTER AND TARGETIME>=OLAP_DATE.QUARTER) 
OR
(TARGTIMEUN=6 AND TARGETIME<=OLAP_DATE.SALENUM AND TARGETIME>=OLAP_DATE.SALENUM) 
) AS WRKDAYS
FROM spp.TTARENTR  TTARENTR
WHERE COMSERNO<>'*' AND PRODSERN<>'*'    AND TARGEPRODF=0
) TTARENTR_COM_PROD
UNION 
SELECT  * FROM 
(
SELECT
TARGSERN,
SALMSERN,
'0' AS COMSERNO,
PRODSERN,
TARGETIME,
TARGEVOLUM,
TARGEMONEY,
TARGENUM1,
TARGENUM2,
TARGENUM3,
TARGENUM4,
TARGMONEY1,
TARGMONEY2,
TARGMONEY3,
TARGMONEY4,
TARGTIMEUN,
(SELECT SUM(WRKDAY) FROM spp.OLAP_DATE  OLAP_DATE
WHERE 
(TARGTIMEUN=2 AND TARGETIME<=OLAP_DATE.WEEK AND TARGETIME>=OLAP_DATE.WEEK ) 
OR
(TARGTIMEUN=3 AND TARGETIME<=OLAP_DATE.MONTH AND TARGETIME>=OLAP_DATE.MONTH ) 
OR
(TARGTIMEUN=4 AND TARGETIME<=OLAP_DATE.YEAR AND TARGETIME>=OLAP_DATE.YEAR ) 
OR
(TARGTIMEUN=5 AND TARGETIME<=OLAP_DATE.QUARTER AND TARGETIME>=OLAP_DATE.QUARTER) 
OR
(TARGTIMEUN=6 AND TARGETIME<=OLAP_DATE.SALENUM AND TARGETIME>=OLAP_DATE.SALENUM) 
) AS WRKDAYS
FROM spp.TTARENTR  TTARENTR
WHERE COMSERNO='*' AND PRODSERN<>'*'    AND TARGEPRODF=0
) TTARENTR_PROD
) TTARENTR
INNER JOIN spp.OLAP_DATE OLAP_DATE ON 
(
TARGETIME<=(
CASE TARGTIMEUN
	WHEN '2' THEN OLAP_DATE.WEEK 
	WHEN '3' THEN OLAP_DATE.MONTH
	WHEN '4' THEN OLAP_DATE.YEAR
	WHEN '5' THEN OLAP_DATE.QUARTER
	WHEN '6' THEN OLAP_DATE.SALENUM
END
)
AND 
TARGETIME>=(
CASE TARGTIMEUN
	WHEN '2' THEN OLAP_DATE.WEEK 
	WHEN '3' THEN OLAP_DATE.MONTH
	WHEN '4' THEN OLAP_DATE.YEAR
	WHEN '5' THEN OLAP_DATE.QUARTER
	WHEN '6' THEN OLAP_DATE.SALENUM
END)
AND WRKDAY=1
)


SET NOCOUNT OFF












GO
/****** Object:  StoredProcedure [spp].[proc_fill_OLAP_WHOLESALER]    Script Date: 01/09/2008 18:00:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO















CREATE PROCEDURE [spp].[proc_fill_OLAP_WHOLESALER] AS

SET NOCOUNT ON

/***********************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_WHOLESALER]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [spp].[OLAP_WHOLESALER]
/***********************************/

/***********************************/
CREATE TABLE [spp].[OLAP_WHOLESALER] (
	[COMSERNWHS] [varchar] (15)  NOT NULL PRIMARY KEY ,
	[COMNAME] [varchar] (30)   NULL DEFAULT 'Unknown' 
) ON [PRIMARY]
/***********************************/


INSERT INTO OLAP_WHOLESALER(COMSERNWHS)
SELECT DISTINCT ISNULL(COMSERNWHS,'')  AS COMSERNWHS  FROM
	(
	SELECT  TDELIVER.COMSERNWHS FROM TDELIVER
	UNION
	SELECT  TORDER.COMSERNWHS FROM TORDER
	UNION
	SELECT  TMSASLIP.COMSERNWHS FROM TMSASLIP
	) WHOLESALER


UPDATE OLAP_WHOLESALER
	SET OLAP_WHOLESALER.COMNAME=TCOMPANY.COMNAME
	FROM OLAP_WHOLESALER , TCOMPANY
	WHERE OLAP_WHOLESALER.COMSERNWHS=TCOMPANY.COMSERNO











GO
/****** Object:  StoredProcedure [spp].[proc_process_main]    Script Date: 01/09/2008 18:00:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


















CREATE    procedure [spp].[proc_process_main]
@i_force_load_selection bit = 0 
as


DECLARE @prev_time datetime

SET DATEFIRST 1


SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_DATE	-- DATE MUST BE DONE  BEFORE STORE
---------------------------

print '- filling DATE took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_PRODUCT	-- PRODUCT MUST BE DONE  BEFORE STORE (SALESFORCE IS WITHIN STORE)
---------------------------

print '- filling PRODUCT took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_STORE
---------------------------

print '- filling STORE took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_LCOMPGR
---------------------------

print '- filling OLAP_LCOMPGR took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_WHOLESALER
---------------------------

print '- filling WHOLESALER took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()



---------------------------
EXEC proc_fill_OLAP_FIXTURE
---------------------------

print '- filling FIXTURE took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_TARGET
---------------------------

print '- filling TARGET took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_SALESCALL
---------------------------

print '- filling SALESCALL took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()

---------------------------
EXEC proc_fill_OLAP_SURVEY
---------------------------

print '- filling SURVEY took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()






---------------------------
-- PROCESS ON SATURDAY, SUNDAY 
--IF DATEPART(dw , GETDATE() )=6 OR DATEPART(dw , GETDATE() )=7 OR @i_force_load_selection=1


IF 1=1	-- eFORCE every day
	BEGIN

		EXEC proc_fill_OLAP_LPROPROD

		EXEC proc_fill_OLAP_SELECTION

		----------------------------------------------------------------

		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_ORDDISTR_EXP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_ORDDISTR_EXP]
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_ORDDISTR_NOTEXP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_ORDDISTR_NOTEXP]
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_ORDDISTR_TMP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_ORDDISTR_TMP]
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_ORDDISTR_TMP2]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_ORDDISTR_TMP2]
		
		CREATE TABLE [spp].[OLAP_ORDDISTR_EXP] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[ORDDDATE] [char] (8) NOT NULL ,
			[PREV_INDISTR_CUM_SUM] [smallint] NOT NULL ,
			[PREV_INSEL_CUM_SUM] [smallint] NOT NULL ,
			[PREV_INBSEL_CUM_SUM] [smallint] NOT NULL ,
			[CUR_INDISTR_CUM_SUM] [smallint] NOT NULL ,
			[CUR_INSEL_CUM_SUM] [smallint] NOT NULL ,
			[CUR_INBSEL_CUM_SUM] [smallint] NOT NULL ,
			[INDISTR_CUM] [smallint] NOT NULL ,
			[INSELDISTR_CUM] [smallint] NOT NULL ,
			[INBSELDISTR_CUM] [smallint] NOT NULL 
		) ON [PRIMARY]

		
		CREATE TABLE [spp].[OLAP_ORDDISTR_NOTEXP] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[ORDDDATE] [char] (8) NOT NULL ,
			[INSEL_CUM] [smallint] NOT NULL ,
			[INBSEL_CUM] [smallint] NOT NULL ,
			[INDISTR_CUM] [smallint] NOT NULL ,
			[INSELDISTR_CUM] [smallint] NOT NULL ,
			[INBSELDISTR_CUM] [smallint] NOT NULL ,
			[PRODEXPAND] [tinyint] NOT NULL ,
			[PRODEXPAND_INTERSECT] [tinyint] NOT NULL 
		) ON [PRIMARY]

		
		CREATE TABLE [spp].[OLAP_ORDDISTR_TMP] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[ORDDDATE] [char] (8) NOT NULL ,
			[RANGESTART_DATE] [char] (8) NOT NULL ,
			[OUTOFSTOCK_DATE] [char] (8) NOT NULL ,
			[WRKDAYS_IN_RANGE] [int] NOT NULL ,
			[ORDDMONTH] [char] (6) NOT NULL ,
			[WRKDAY_SERNO] [int] NOT NULL ,
			[ORDEVOL] [float] NOT NULL 
		) ON [PRIMARY]

		
		CREATE TABLE [spp].[OLAP_ORDDISTR_TMP2] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[ORDDDATE] [char] (8) NOT NULL ,
			[PREV_ORDDDATE] [char] (8) NOT NULL ,
			[INDISTR] [smallint] NOT NULL ,
			[INBSEL] [bit] NOT NULL ,
			[INSEL] [bit] NOT NULL 
		) ON [PRIMARY]

		
		 CREATE  CLUSTERED  INDEX [IX_OLAP_ORDDISTR_EXP_DATE] ON [spp].[OLAP_ORDDISTR_EXP]([ORDDDATE]) ON [PRIMARY]

		
		 CREATE  CLUSTERED  INDEX [IX_OLAP_ORDDISTR_NOTEXP] ON [spp].[OLAP_ORDDISTR_NOTEXP]([COMSERNO], [PRODSERN], [ORDDDATE]) ON [PRIMARY]

		
		 CREATE  UNIQUE  CLUSTERED  INDEX [IX_OLAP_ORDDISTR_TMP] ON [spp].[OLAP_ORDDISTR_TMP]([COMSERNO], [PRODSERN], [ORDDDATE]) ON [PRIMARY]

		
		 CREATE  UNIQUE  CLUSTERED  INDEX [IX_OLAP_ORDDISTR_TMP2] ON [spp].[OLAP_ORDDISTR_TMP2]([COMSERNO], [PRODSERN], [ORDDDATE]) ON [PRIMARY]


		-----------------------------------------------------------------
		
		----------------------------------------------------------------
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DELDISTR_EXP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DELDISTR_EXP]
		
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DELDISTR_NOTEXP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DELDISTR_NOTEXP]
		
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DELDISTR_TMP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DELDISTR_TMP]
		
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DELDISTR_TMP2]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DELDISTR_TMP2]
		
		
		CREATE TABLE [spp].[OLAP_DELDISTR_EXP] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[DELDATE] [char] (8) NOT NULL ,
			[PREV_INDISTR_CUM_SUM] [smallint] NOT NULL ,
			[PREV_INSEL_CUM_SUM] [smallint] NOT NULL ,
			[PREV_INBSEL_CUM_SUM] [smallint] NOT NULL ,
			[CUR_INDISTR_CUM_SUM] [smallint] NOT NULL ,
			[CUR_INSEL_CUM_SUM] [smallint] NOT NULL ,
			[CUR_INBSEL_CUM_SUM] [smallint] NOT NULL ,
			[INDISTR_CUM] [smallint] NOT NULL ,
			[INSELDISTR_CUM] [smallint] NOT NULL ,
			[INBSELDISTR_CUM] [smallint] NOT NULL 
		) ON [PRIMARY]
		
		
		CREATE TABLE [spp].[OLAP_DELDISTR_NOTEXP] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[DELDATE] [char] (8) NOT NULL ,
			[INSEL_CUM] [smallint] NOT NULL ,
			[INBSEL_CUM] [smallint] NOT NULL ,
			[INDISTR_CUM] [smallint] NOT NULL ,
			[INSELDISTR_CUM] [smallint] NOT NULL ,
			[INBSELDISTR_CUM] [smallint] NOT NULL ,
			[PRODEXPAND] [tinyint] NOT NULL ,
			[PRODEXPAND_INTERSECT] [tinyint] NOT NULL 
		) ON [PRIMARY]
		
		
		CREATE TABLE [spp].[OLAP_DELDISTR_TMP] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[DELDATE] [char] (8) NOT NULL ,
			[RANGESTART_DATE] [char] (8) NOT NULL ,
			[OUTOFSTOCK_DATE] [char] (8) NOT NULL ,
			[WRKDAYS_IN_RANGE] [int] NOT NULL ,
			[DELDMONTH] [char] (6) NOT NULL ,
			[WRKDAY_SERNO] [int] NOT NULL ,
			[DELEACTVOL] [float] NOT NULL 
		) ON [PRIMARY]
		
		
		CREATE TABLE [spp].[OLAP_DELDISTR_TMP2] (
			[COMSERNO] [char] (15) NOT NULL ,
			[PRODSERN] [char] (15) NOT NULL ,
			[DELDATE] [char] (8) NOT NULL ,
			[PREV_DELDATE] [char] (8) NOT NULL ,
			[INDISTR] [smallint] NOT NULL ,
			[INBSEL] [bit] NOT NULL ,
			[INSEL] [bit] NOT NULL 
		) ON [PRIMARY]
		
		
		 CREATE  CLUSTERED  INDEX [IX_OLAP_DELDISTR_EXP_DATE] ON [spp].[OLAP_DELDISTR_EXP]([DELDATE]) ON [PRIMARY]
		
		
		 CREATE  CLUSTERED  INDEX [IX_OLAP_DELDISTR_NOTEXP] ON [spp].[OLAP_DELDISTR_NOTEXP]([COMSERNO], [PRODSERN], [DELDATE]) ON [PRIMARY]
		
		
		 CREATE  UNIQUE  CLUSTERED  INDEX [IX_OLAP_DELDISTR_TMP] ON [spp].[OLAP_DELDISTR_TMP]([COMSERNO], [PRODSERN], [DELDATE]) ON [PRIMARY]
		
		
		 CREATE  UNIQUE  CLUSTERED  INDEX [IX_OLAP_DELDISTR_TMP2] ON [spp].[OLAP_DELDISTR_TMP2]([COMSERNO], [PRODSERN], [DELDATE]) ON [PRIMARY]


		-----------------------------------------------------------------



		----------------------------------------------------------------
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DPM_EXP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DPM_EXP]
		
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DPM_NOTEXP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DPM_NOTEXP]
		
		
		if exists (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_DPM_TMP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		drop table [spp].[OLAP_DPM_TMP]
		
		
		CREATE TABLE [spp].[OLAP_DPM_EXP] (
			[comserno] [char] (15) NOT NULL ,
			[prodsern] [char] (15) NOT NULL ,
			[dpmhdate] [char] (8) NOT NULL ,
			[prev_dpmecover_cum_sum] [smallint] NOT NULL ,
			[prev_dpmeinsel_cum_sum] [smallint] NOT NULL ,
			[prev_dpmeinbsel_cum_sum] [smallint] NOT NULL ,
			[cur_dpmecover_cum_sum] [smallint] NOT NULL ,
			[cur_dpmeinsel_cum_sum] [smallint] NOT NULL ,
			[cur_dpmeinbsel_cum_sum] [smallint] NOT NULL ,
			[dpmecover_cum] [smallint] NOT NULL ,
			[dpmeselcover_cum] [smallint] NOT NULL ,
			[dpmebselcover_cum] [smallint] NOT NULL ,
			[dpmmeasured_cum] [smallint] NOT NULL ,
			[dpmbselmeasured_cum] [smallint] NOT NULL ,
			[dpmefacing_cum] [real] NOT NULL ,
			[dpmechan_cum] [real] NOT NULL ,
			[dpmesalesp_cum] [real] NOT NULL ,
			[dpmeavestp_cum] [real] NOT NULL ,
			[dpmeprice_net] [real] NOT NULL ,
			[dpmeprice_gross] [real] NOT NULL ,
			[dpmcount] [tinyint] NOT NULL 
		) ON [PRIMARY]
		
		
		CREATE TABLE [spp].[OLAP_DPM_NOTEXP] (
			[comserno] [char] (15) NOT NULL ,
			[prodsern] [char] (15) NOT NULL ,
			[dpmhdate] [char] (8) NOT NULL ,
			[dpmeinsel_cum] [smallint] NOT NULL ,
			[dpmeinbsel_cum] [smallint] NOT NULL ,
			[dpmefacing_cum] [real] NOT NULL ,
			[dpmechan_cum] [real] NOT NULL ,
			[dpmesalesp_cum] [real] NOT NULL ,
			[dpmeavestp_cum] [real] NOT NULL ,
			[dpmeprice_net] [real] NOT NULL ,
			[dpmeprice_gross] [real] NOT NULL ,
			[dpmecover_cum] [smallint] NOT NULL ,
			[dpmeselcover_cum] [smallint] NOT NULL ,
			[dpmebselcover_cum] [smallint] NOT NULL ,
			[dpmmeasured_cum] [smallint] NOT NULL ,
			[dpmbselmeasured_cum] [smallint] NOT NULL ,
			[dpmcount] [tinyint] NOT NULL ,
			[prodexpand] [tinyint] NOT NULL ,
			[prodexpand_intersect] [tinyint] NOT NULL 
		) ON [PRIMARY]
		
		
		CREATE TABLE [spp].[OLAP_DPM_TMP] (
			[comserno] [char] (15) NOT NULL ,
			[prodsern] [char] (15) NOT NULL ,
			[dpmhdate] [char] (8) NOT NULL ,
			[dpmecover] [bit] NOT NULL ,
			[dpmefacing] [real] NOT NULL ,
			[dpmechan] [real] NOT NULL ,
			[dpmesalesp] [real] NOT NULL ,
			[dpmeavestp] [real] NOT NULL ,
			[dpmeprice_net] [real] NOT NULL ,
			[dpmeprice_gross] [real] NOT NULL ,
			[dpmeinsel] [bit] NOT NULL ,
			[dpmeinbsel] [bit] NOT NULL ,
			[sel_placeholder] [bit] NOT NULL ,
			[prev_dpmhdate] [char] (8) NOT NULL 
		) ON [PRIMARY]
		
		
		 CREATE  CLUSTERED  INDEX [ix_OLAP_DPM_NOTEXP] ON [spp].[OLAP_DPM_NOTEXP]([comserno], [prodsern], [dpmhdate]) ON [PRIMARY]
		
		
		 CREATE  UNIQUE  CLUSTERED  INDEX [ix_OLAP_DPM_TMP] ON [spp].[OLAP_DPM_TMP]([comserno], [prodsern], [dpmhdate]) ON [PRIMARY]
		

		-----------------------------------------------------------------


	END
ELSE
IF EXISTS (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_SELRANGE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	AND EXISTS (select * from dbo.sysobjects where id = object_id(N'[spp].[OLAP_LPROPROD]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	BEGIN
		IF NOT EXISTS(SELECT * FROM OLAP_SELRANGE) 
			OR NOT EXISTS(SELECT * FROM OLAP_LPROPROD)
			BEGIN
				EXEC proc_fill_OLAP_LPROPROD -- !!! BEFORE SELECTION_DIMS
				EXEC proc_fill_OLAP_SELECTION_DIMS
			END
	END
ELSE
	BEGIN
		EXEC proc_fill_OLAP_LPROPROD -- !!! BEFORE SELECTION_DIMS
		EXEC proc_fill_OLAP_SELECTION_DIMS
	END



print '- OLAP_SELECTION  took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()




---------------------------
TRUNCATE TABLE spp.OLAP_AUDIT
---------------------------



---------------------------
--EXEC proc_fill_OLAP_DPM
---------------------------

print '- OLAP_DPM took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()




---------------------------
--EXEC proc_fill_OLAP_ORDDISTR
---------------------------

print '- OLAP_ORDDISTR took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()



---------------------------
--EXEC proc_fill_OLAP_DELDISTR
---------------------------

print '- OLAP_DELDISTR took ' + CAST(DATEDIFF(ss, @prev_time , getdate() ) as varchar(25)) + ' sec.'
SET @prev_time=GETDATE()




GO
/****** Object:  StoredProcedure [spp].[proc_rename_OLAP_COLUMNS]    Script Date: 01/09/2008 18:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [spp].[proc_rename_OLAP_COLUMNS]
@TABLE_NAME as varchar(50)
AS

DECLARE @NAME varchar(128)
DECLARE @NEW_NAME varchar(128)
DECLARE @TEMP_NAME varchar(128)
DECLARE @position int
DECLARE @TEMP_COUNTER int

DECLARE @CURRENT_ASCII_CODE tinyint
SET @CURRENT_ASCII_CODE=0



DECLARE temp_cursor CURSOR 
FOR
select SUBSTRING(COLUMN_NAME , 7 , LEN(COLUMN_NAME)-6) AS NAME from INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_CATALOG=DB_NAME()  AND TABLE_SCHEMA='spp' AND TABLE_NAME=@TABLE_NAME
AND LEFT(COLUMN_NAME ,6)='GRP@#@'
OPEN temp_cursor
FETCH NEXT FROM temp_cursor INTO @NAME
WHILE @@FETCH_STATUS=0
	BEGIN
		SET @NEW_NAME=RTRIM(LTRIM(Replace(@NAME , '  ' ,' ')))
		SET @NEW_NAME=Replace(@NEW_NAME , '   ' ,' ')
		SET @NEW_NAME=Replace(@NEW_NAME , '    ' ,' ')
		SET @NEW_NAME=Replace(@NEW_NAME , '     ' ,' ')
		SET @position=1
		SET @TEMP_NAME=''
		
		WHILE @position <= DATALENGTH(@NEW_NAME)
		   BEGIN
		
			SET  @CURRENT_ASCII_CODE=ASCII(SUBSTRING(@NEW_NAME, @position, 1))
		
			IF (@CURRENT_ASCII_CODE<32) 
			OR (@CURRENT_ASCII_CODE>32 AND @CURRENT_ASCII_CODE<48)
			OR (@CURRENT_ASCII_CODE>57 AND @CURRENT_ASCII_CODE<65)
			OR (@CURRENT_ASCII_CODE>90 AND @CURRENT_ASCII_CODE<97)
			OR (@CURRENT_ASCII_CODE>122 AND @CURRENT_ASCII_CODE<192)
				BEGIN
					IF RIGHT(@TEMP_NAME , 1)<> ' '
						SET @TEMP_NAME=@TEMP_NAME + ' '
				END
			ELSE
				BEGIN
					SET @TEMP_NAME=@TEMP_NAME + SUBSTRING(@NEW_NAME, @position, 1)
				END
		
		   	SET @position = @position + 1
		
		   END

			--- check if first symbol is letter ,  it's not allowed
			SET @CURRENT_ASCII_CODE=ASCII(LEFT(@TEMP_NAME, 1))
			IF (@CURRENT_ASCII_CODE<65)
			OR (@CURRENT_ASCII_CODE>90 AND @CURRENT_ASCII_CODE<97)
			OR (@CURRENT_ASCII_CODE>122 AND @CURRENT_ASCII_CODE<129)
			OR (@CURRENT_ASCII_CODE>165)
				BEGIN
					SET @TEMP_NAME='Group ' + @TEMP_NAME
				END


			SET @NEW_NAME=@TEMP_NAME
			IF @NEW_NAME<>@NAME
					BEGIN
						SET @TEMP_NAME=@NEW_NAME
						SET @TEMP_COUNTER=0

						-- check if exists with same name
						WHILE EXISTS(SELECT TOP 1 1 from INFORMATION_SCHEMA.COLUMNS
									WHERE TABLE_CATALOG=DB_NAME()  AND TABLE_SCHEMA='spp' AND TABLE_NAME=@TABLE_NAME
									AND LEFT(COLUMN_NAME ,6)='GRP@#@' + @TEMP_NAME)
							BEGIN
								SET @TEMP_COUNTER=@TEMP_COUNTER+1
								SET @TEMP_NAME=@NEW_NAME + CAST(@TEMP_COUNTER AS varchar(5))
							END

						SET @NEW_NAME='GRP@#@' + @TEMP_NAME
						SET @NAME=@TABLE_NAME + '.[GRP@#@' + REPLACE(@NAME , ']' , ']]') + ']'

						------  rename column 
						EXEC  sp_rename @NAME , @NEW_NAME, 'COLUMN'

					END
		FETCH NEXT FROM temp_cursor INTO @NAME
	END
CLOSE temp_cursor 
DEALLOCATE temp_cursor



GO

sp_change_users_login 'Auto_fix', spp
Go

sp_change_users_login 'Auto_fix', spp_readonly
GO

GO



setuser 'spp'
go
proc_process_main
go
