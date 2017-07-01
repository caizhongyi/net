USE [czy_database]
GO
/****** 对象:  Table [dbo].[City]    脚本日期: 02/25/2011 08:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[c_id] [int] IDENTITY(1,1) NOT NULL,
	[c_name] [varchar](100) NULL,
	[c_provinceId] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[c_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [czy_database]
GO
/****** 对象:  Table [dbo].[Country]    脚本日期: 02/25/2011 08:48:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[c_id] [int] IDENTITY(1,1) NOT NULL,
	[c_name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[c_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


USE [czy_database]
GO
/****** 对象:  Table [dbo].[Express]    脚本日期: 02/25/2011 08:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Express](
	[e_id] [int] IDENTITY(1,1) NOT NULL,
	[e_name] [nvarchar](100) NULL,
	[e_price] [money] NULL CONSTRAINT [DF_Express_e_price]  DEFAULT ((0)),
	[e_createDate] [datetime] NULL CONSTRAINT [DF_Express_e_createDate]  DEFAULT (getdate()),
	[e_remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_Express] PRIMARY KEY CLUSTERED 
(
	[e_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [czy_database]
GO
/****** 对象:  Table [dbo].[Menu]    脚本日期: 02/25/2011 08:49:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[m_id] [int] IDENTITY(1,1) NOT NULL,
	[m_name] [nvarchar](50) NULL,
	[m_parentId] [nvarchar](50) NULL,
	[m_url] [varchar](200) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[m_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF


USE [czy_database]
GO
/****** 对象:  Table [dbo].[NewsInfo]    脚本日期: 02/25/2011 08:49:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsInfo](
	[n_id] [bigint] IDENTITY(1,1) NOT NULL,
	[n_title] [nvarchar](100) NULL,
	[n_content] [nvarchar](max) NULL,
	[n_newsTypeId] [int] NOT NULL,
	[n_createDate] [datetime] NULL CONSTRAINT [DF_NewsInfo_n_createDate]  DEFAULT (getdate()),
	[n_startDate] [datetime] NULL CONSTRAINT [DF_NewsInfo_n_startDate]  DEFAULT (getdate()),
	[n_endDate] [datetime] NULL CONSTRAINT [DF_NewsInfo_n_endDate]  DEFAULT (getdate()),
	[n_source] [varchar](50) NULL,
	[n_author] [varchar](50) NULL,
	[n_show] [bit] NULL,
 CONSTRAINT [PK_NewsInfo] PRIMARY KEY CLUSTERED 
(
	[n_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [czy_database]
GO
/****** 对象:  Table [dbo].[NewsType]    脚本日期: 02/25/2011 08:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsType](
	[nt_id] [int] IDENTITY(1,1) NOT NULL,
	[nt_name] [varchar](100) NULL,
	[nt_remark] [varchar](50) NULL,
	[parentId] [int] NULL,
	[nt_parentId] [int] NULL,
 CONSTRAINT [PK_NewsType] PRIMARY KEY CLUSTERED 
(
	[nt_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [czy_database]
GO
/****** 对象:  Table [dbo].[Order]    脚本日期: 02/25/2011 08:49:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[o_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Order_o_id]  DEFAULT (newid()),
	[o_userId] [varchar](50) NOT NULL,
	[o_orderUserId] [varchar](50) NOT NULL,
	[o_createDate] [datetime] NULL CONSTRAINT [DF_Order_o_createDate]  DEFAULT (getdate()),
	[o_startDate] [datetime] NULL CONSTRAINT [DF_Order_o_startDate]  DEFAULT (getdate()),
	[o_endDate] [datetime] NULL CONSTRAINT [DF_Order_o_endDate]  DEFAULT (getdate()),
	[o_fromAddress] [nvarchar](500) NULL,
	[o_toAddress] [nvarchar](500) NULL,
	[o_orderDetailId] [uniqueidentifier] NULL,
	[o_state] [int] NULL CONSTRAINT [DF_Order_o_state]  DEFAULT ((0)),
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[o_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [czy_database]
GO
/****** 对象:  Table [dbo].[OrderDetail]    脚本日期: 02/25/2011 08:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[od_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_OrderDetail_od_id]  DEFAULT (newid()),
	[od_productId] [uniqueidentifier] NOT NULL,
	[od_remark] [nvarchar](300) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[od_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [czy_database]
GO
/****** 对象:  Table [dbo].[Product]    脚本日期: 02/25/2011 08:50:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[p_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Product_p_id]  DEFAULT (newid()),
	[p_name] [nvarchar](100) NULL,
	[p_price] [money] NOT NULL CONSTRAINT [DF_Product_p_price]  DEFAULT ((0)),
	[p_currentPrice] [money] NOT NULL CONSTRAINT [DF_Product_p_currentPrice]  DEFAULT ((0)),
	[p_rebateValue] [int] NULL CONSTRAINT [DF_Product_p_rebateId]  DEFAULT ((0)),
	[p_count] [bigint] NOT NULL CONSTRAINT [DF_Product_p_count]  DEFAULT ((0)),
	[p_totalCount] [bigint] NOT NULL CONSTRAINT [DF_Product_p_totalCount]  DEFAULT ((0)),
	[p_needCount] [bigint] NOT NULL CONSTRAINT [DF_Product_p_needCount]  DEFAULT ((0)),
	[p_userId] [varchar](50) NULL,
	[p_createDate] [datetime] NOT NULL CONSTRAINT [DF_Product_p_createDate]  DEFAULT (getdate()),
	[p_typeId] [bigint] NULL CONSTRAINT [DF_Product_p_typeId]  DEFAULT ((0)),
	[P_imageId] [int] NULL CONSTRAINT [DF_Product_P_imageId]  DEFAULT ((0)),
	[p_description] [nvarchar](max) NULL,
	[p_brandId] [bigint] NULL CONSTRAINT [DF_Product_p_brandId]  DEFAULT ((0)),
	[p_materialId] [bigint] NULL CONSTRAINT [DF_Product_p_materialId]  DEFAULT ((0)),
	[p_sizeId] [bigint] NULL CONSTRAINT [DF_Product_p_sizeId]  DEFAULT ((0)),
	[p_colorId] [bigint] NULL CONSTRAINT [DF_Product_p_colorId]  DEFAULT ((0)),
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前实际价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_currentPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-1之间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_rebateValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_typeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_brandId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'材料' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_materialId'

USE [czy_database]
GO
/****** 对象:  Table [dbo].[Product_NewsInfo]    脚本日期: 02/25/2011 08:50:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_NewsInfo](
	[p_n_id] [bigint] IDENTITY(1,1) NOT NULL,
	[p_n_newsId] [bigint] NULL,
	[p_n_productId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Product_NewsInfo] PRIMARY KEY CLUSTERED 
(
	[p_n_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'如果为0000-0000-0000-0000则为全部商品活' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_NewsInfo', @level2type=N'COLUMN',@level2name=N'p_n_productId'
USE [czy_database]
GO
/****** 对象:  Table [dbo].[ProductImages]    脚本日期: 02/25/2011 08:50:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[pi_id] [int] IDENTITY(1,1) NOT NULL,
	[pi_title] [nvarchar](50) NULL,
	[pi_url] [nvarchar](300) NULL,
	[pi_remark] [nvarchar](300) NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[pi_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
USE [czy_database]
GO
/****** 对象:  Table [dbo].[ProductType]    脚本日期: 02/25/2011 08:51:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductType](
	[pt_id] [bigint] IDENTITY(1,1) NOT NULL,
	[pt_name] [varchar](50) NULL,
	[pt_parentId] [bigint] NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[pt_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
USE [czy_database]
GO
/****** 对象:  Table [dbo].[Province]    脚本日期: 02/25/2011 08:51:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[p_id] [int] IDENTITY(1,1) NOT NULL,
	[p_name] [nvarchar](100) NULL,
	[p_countryId] [int] NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
USE [czy_database]
GO
/****** 对象:  Table [dbo].[Rebate]    脚本日期: 02/25/2011 08:51:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rebate](
	[r_id] [int] IDENTITY(1,1) NOT NULL,
	[R_userId] [varchar](50) NOT NULL,
	[R_userTypeId] [int] NOT NULL CONSTRAINT [DF_Rebate_r_value]  DEFAULT ((100)),
	[R_rebateValue] [int] NULL CONSTRAINT [DF_Rebate_R_rebateValue]  DEFAULT ((0)),
 CONSTRAINT [PK_Rebate] PRIMARY KEY CLUSTERED 
(
	[r_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布折扣的用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rebate', @level2type=N'COLUMN',@level2name=N'R_userId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布给其它用户的用户类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rebate', @level2type=N'COLUMN',@level2name=N'R_userTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-1之间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rebate', @level2type=N'COLUMN',@level2name=N'R_rebateValue'

USE [czy_database]
GO
/****** 对象:  Table [dbo].[Town]    脚本日期: 02/25/2011 08:51:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Town](
	[t_id] [int] IDENTITY(1,1) NOT NULL,
	[t_name] [varchar](100) NULL,
	[t_cityId] [int] NOT NULL,
 CONSTRAINT [PK_Town] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [czy_database]
GO
/****** 对象:  Table [dbo].[UserBank]    脚本日期: 02/25/2011 08:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBank](
	[ub_id] [bigint] IDENTITY(1,1) NOT NULL,
	[ub_name] [nvarchar](50) NULL,
	[ub_number] [int] NULL,
	[ub_country] [nvarchar](50) NULL,
	[ub_province] [int] NULL,
	[ub_city] [int] NULL,
	[ub_town] [int] NULL,
	[ub_branch] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserBank] PRIMARY KEY CLUSTERED 
(
	[ub_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
USE [czy_database]
GO
/****** 对象:  Table [dbo].[UserExAddress]    脚本日期: 02/25/2011 08:51:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserExAddress](
	[uea_id] [int] IDENTITY(1,1) NOT NULL,
	[uea_name] [nvarchar](50) NULL,
	[uea_address] [nvarchar](500) NULL,
	[uea_type] [int] NOT NULL CONSTRAINT [DF_UserExAddress_uea_type]  DEFAULT ((0)),
 CONSTRAINT [PK_UserExAddress] PRIMARY KEY CLUSTERED 
(
	[uea_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0为自己的地址/1为其它人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserExAddress', @level2type=N'COLUMN',@level2name=N'uea_type'
USE [czy_database]
GO
/****** 对象:  Table [dbo].[UserInfo]    脚本日期: 02/25/2011 08:51:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[u_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_UserInfo_u_id]  DEFAULT (newid()),
	[u_name] [nvarchar](50) NOT NULL,
	[u_pwd] [nvarchar](100) NOT NULL,
	[u_typeId] [int] NOT NULL,
	[u_birthday] [datetime] NULL,
	[u_province] [int] NULL,
	[u_city] [int] NULL,
	[u_town] [int] NULL,
	[u_address] [nvarchar](500) NULL,
	[u_sex] [nchar](2) NULL CONSTRAINT [DF_UserInfo_u_sex]  DEFAULT (N'男'),
	[u_tel] [nvarchar](50) NULL,
	[u_qq] [int] NULL,
	[u_email] [nvarchar](50) NULL,
	[u_bussinessType] [int] NULL,
	[u_createDate] [datetime] NULL CONSTRAINT [DF_UserInfo_u_createDate]  DEFAULT (getdate()),
	[u_loginDate] [datetime] NULL CONSTRAINT [DF_UserInfo_u_loginDate]  DEFAULT (getdate()),
	[u_loginTime] [int] NULL CONSTRAINT [DF_UserInfo_u_loginTime]  DEFAULT ((0)),
	[u_loginMaxTime] [int] NULL CONSTRAINT [DF_UserInfo_u_loginMaxTime]  DEFAULT ((0)),
	[u_money] [money] NULL CONSTRAINT [DF_UserInfo_u_money]  DEFAULT ((0)),
	[u_bankId] [bigint] NULL,
	[u_state] [int] NOT NULL CONSTRAINT [DF_UserInfo_u_state]  DEFAULT ((1)),
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0为买家,1为卖家' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'u_bussinessType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0为禁用,1为启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'u_state'

USE [czy_database]
GO
/****** 对象:  Table [dbo].[UserType]    脚本日期: 02/25/2011 08:52:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserType](
	[ut_id] [int] IDENTITY(1,1) NOT NULL,
	[ut_name] [nvarchar](50) NULL,
	[ut_remark] [nvarchar](50) NULL,
	[ut_right] [varchar](50) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[ut_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF