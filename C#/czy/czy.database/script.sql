USE [czy_database]
GO
/****** 对象:  Table [dbo].[UserExAddress]    脚本日期: 03/03/2011 16:04:00 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'0为自己的地址/1为其它人' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserExAddress', @level2type=N'COLUMN',@level2name=N'uea_type'
GO
/****** 对象:  Table [dbo].[UserBank]    脚本日期: 03/03/2011 16:03:57 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[UserInfo]    脚本日期: 03/03/2011 16:04:17 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'0为买家,1为卖家' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'u_bussinessType'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'0为禁用,1为启用' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'u_state'
GO
/****** 对象:  Table [dbo].[Menu]    脚本日期: 03/03/2011 16:02:50 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[ProductType]    脚本日期: 03/03/2011 16:03:40 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Rebate]    脚本日期: 03/03/2011 16:03:47 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'发布折扣的用户ID' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rebate', @level2type=N'COLUMN',@level2name=N'R_userId'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'发布给其它用户的用户类型ID' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rebate', @level2type=N'COLUMN',@level2name=N'R_userTypeId'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'0-1之间' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rebate', @level2type=N'COLUMN',@level2name=N'R_rebateValue'
GO
/****** 对象:  Table [dbo].[Country]    脚本日期: 03/03/2011 16:02:42 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[City]    脚本日期: 03/03/2011 16:02:40 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Province]    脚本日期: 03/03/2011 16:03:43 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Town]    脚本日期: 03/03/2011 16:03:50 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Order]    脚本日期: 03/03/2011 16:03:11 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[OrderDetail]    脚本日期: 03/03/2011 16:03:14 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[Express]    脚本日期: 03/03/2011 16:02:47 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  Table [dbo].[NewsInfo]    脚本日期: 03/03/2011 16:02:59 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[NewsType]    脚本日期: 03/03/2011 16:03:03 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[Product_NewsInfo]    脚本日期: 03/03/2011 16:03:34 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'如果为0000-0000-0000-0000则为全部商品活' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_NewsInfo', @level2type=N'COLUMN',@level2name=N'p_n_productId'
GO
/****** 对象:  UserDefinedFunction [dbo].[F_GetWeek]    脚本日期: 03/03/2011 16:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*=========================================================
函数：F_GetWeek
功能：获取周几
参数：sDateTime 日期
@TableName
日期：2008-07-03
=========================================================*/

Create Function [dbo].[F_GetWeek](@sDateTime Datetime,@sWeek int) 
Returns int
As 
Begin 
	DECLARE @tWeek int
	DECLARE @outInt int
	IF (@sDateTime is null)
		SET @tWeek =@sWeek
	else
		SET @tWeek =(datepart(dw, getdate())+ @@datefirst)%7 
	SELECT @outInt=case @tWeek 
		WHEN 2 THEN 1
		WHEN 3 THEN 2
		WHEN 4 THEN 3
		WHEN 5 THEN 4
		WHEN 6 THEN 5
		WHEN 0 THEN 6
		WHEN 1 THEN 7
		ELSE 1
	END
	
RETURN @outInt
End
GO
/****** 对象:  Table [dbo].[ProductImages]    脚本日期: 03/03/2011 16:03:37 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** 对象:  UserDefinedFunction [dbo].[F_GetFormatDate]    脚本日期: 03/03/2011 16:04:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[F_GetFormatDate](@Format varchar(14)) 
ReturnS varchar(14) 
As 
Begin 
	--declare @Format varchar(14)
	--set @Format='yyyyddhhmiss'
	declare @TimeStr varchar(14)
	declare @yyyy varchar(4)
		set @yyyy =datename(YYYY,getdate())
	declare @mm varchar(2)
		set @mm =datename(MM,getdate())
		if (len( @mm)=1)
			set @mm='0'+@mm
	declare @dd varchar(2)
		set @dd =datename(DD,getdate())
		if (len(@dd)=1)
			set @dd='0'+@dd
	declare @hh varchar(2)
		set @hh =datename(hh,getdate())
		if (len(@hh)=1)
			set @hh='0'+@hh
	declare @mi varchar(2)
		set @mi =datename(mi,getdate())
		if (len(@mi)=1)
			set @mi='0'+@mi
	declare @ss varchar(2)
		set @ss =datename(ss,getdate())
		if (len(@ss)=1)
			set @ss='0'+@ss
	set @TimeStr =upper(@Format)
	set @TimeStr =replace(@TimeStr,'YYYY',@yyyy)
	set @TimeStr =replace(@TimeStr,'MM',@mm)
	set @TimeStr =replace(@TimeStr,'DD',@dd)
	set @TimeStr =replace(@TimeStr,'HH',@hh)
	set @TimeStr =replace(@TimeStr,'MI',@mi)
	set @TimeStr =replace(@TimeStr,'SS',@ss)
	Return @TimeStr
End
GO
/****** 对象:  StoredProcedure [dbo].[P_GetPagerData]    脚本日期: 03/03/2011 16:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[P_GetPagerData]
              (
        @pagesize int, --每页的记录条数
        @pagenum int, --当前页面  1为第一页
        @QuerySql varchar(1000),--部分查询字符串,如 '  FROM t_Company where...'
        @keyId varchar(500), --列ID
        @keyIdOrder varchar(500), --ID列排序:desc/asc
        @order varchar(50),--order by [clos] desc/asc
        @clos varchar(50)--orderseq,companycode... 

        )
        AS
        Begin 
        Declare @SqlText AS Varchar(1000)
        declare @SqlText1 AS Varchar(1000)
        set @SqlText=' SELECT * '
        +' FROM (SELECT ROW_NUMBER() OVER(ORDER BY '+@keyId+' '+@keyIdOrder+', '+@keyId+') AS rownum,'+@clos +' '+ @QuerySql+' ) AS D '  
        +' WHERE rownum BETWEEN '+Cast((@pagenum-1)*(@pagesize+1) as varchar(10))+' AND '+Cast(@pagenum*@pagesize as varchar(10))+ '  '+@order +' '
        Exec(@SqlText)
      
        end
GO
/****** 对象:  StoredProcedure [dbo].[P_GetTotalsCount]    脚本日期: 03/03/2011 16:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[P_GetTotalsCount]
        @tableName varchar(50),
        @filter varchar(500)
        as
        begin
        Declare @SqlText AS Varchar(1000)
        set @SqlText='select count(*) as totalCount from ' + @tableName +' ' + @filter
        Exec(@SqlText)
        end
GO
/****** 对象:  Table [dbo].[Product]    脚本日期: 03/03/2011 16:03:31 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_price'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'当前实际价格' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_currentPrice'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'0-1之间' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_rebateValue'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'当前数量' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_count'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'类型ID' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_typeId'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_brandId'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'材料' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'p_materialId'
GO
/****** 对象:  Table [dbo].[UserType]    脚本日期: 03/03/2011 16:04:21 ******/
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
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** 对象:  View [dbo].[V_Product]    脚本日期: 03/03/2011 16:04:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Product]
AS
SELECT     dbo.ProductImages.*
FROM         dbo.Product INNER JOIN
                      dbo.ProductType ON dbo.Product.p_typeId = dbo.ProductType.pt_id INNER JOIN
                      dbo.ProductImages ON dbo.Product.P_imageId = dbo.ProductImages.pi_id
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProductImages"
            Begin Extent = 
               Top = 6
               Left = 235
               Bottom = 125
               Right = 374
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductType"
            Begin Extent = 
               Top = 6
               Left = 412
               Bottom = 110
               Right = 559
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 197
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Product'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Product'
GO
/****** 对象:  View [dbo].[V_UserInfo]    脚本日期: 03/03/2011 16:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_UserInfo]
AS
SELECT     dbo.UserBank.*, dbo.UserType.*
FROM         dbo.UserBank INNER JOIN
                      dbo.UserInfo ON dbo.UserBank.ub_id = dbo.UserInfo.u_bankId INNER JOIN
                      dbo.UserType ON dbo.UserInfo.u_typeId = dbo.UserType.ut_id
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserBank"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 186
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserType"
            Begin Extent = 
               Top = 6
               Left = 433
               Bottom = 110
               Right = 572
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserInfo"
            Begin Extent = 
               Top = 6
               Left = 224
               Bottom = 125
               Right = 395
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_UserInfo'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_UserInfo'
GO
/****** 对象:  View [dbo].[V_Order]    脚本日期: 03/03/2011 16:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Order]
AS
SELECT     dbo.[Order].*, dbo.OrderDetail.*
FROM         dbo.[Order] INNER JOIN
                      dbo.OrderDetail ON dbo.[Order].o_orderDetailId = dbo.OrderDetail.od_id
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Order"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 202
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "OrderDetail"
            Begin Extent = 
               Top = 6
               Left = 240
               Bottom = 110
               Right = 394
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Order'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_Order'
GO
/****** 对象:  View [dbo].[V_NewsInfo]    脚本日期: 03/03/2011 16:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_NewsInfo]
AS
SELECT     dbo.NewsInfo.*, dbo.NewsType.*
FROM         dbo.NewsInfo INNER JOIN
                      dbo.NewsType ON dbo.NewsInfo.n_newsTypeId = dbo.NewsType.nt_id
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "NewsInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "NewsType"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 110
               Right = 375
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_NewsInfo'
GO
EXEC dbo.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'USER',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_NewsInfo'
GO
/****** 对象:  UserDefinedFunction [dbo].[F_GetIDFromTable]    脚本日期: 03/03/2011 16:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[F_GetIDFromTable](@TableName varchar(50)) 
ReturnS bigint 
As 
Begin 
/*
([dbo].[F_GetID]('t_userInfo'))
t_UserInfo YYYYMMXXXXX
([dbo].[F_GetID]('t_SmsSends'))
([dbo].[F_GetID]('t_SmsReceive'))
YYYYMMDDhhmmssXXXXX 2008061209013900001

t_ManagementTask
年月+5位数 YYYYMMXXXXX
t_TaskCompany
年月+5位数 YYYYMMXXXXX
t_TaskUser
年月日+5位数 
t_ExecTask
日期+时分秒+5位数 YYYYMMDDXXXXX YYYYMMDDhhmmssXXXXX 2008061209013900001

--测试
SELECT [dbo].[F_GetNewID]('t_UserInfo')
SELECT [dbo].[F_GetNewID]('t_SmsSends')
SELECT [dbo].[F_GetNewID]('t_SmsReceive')
SELECT [dbo].[F_GetNewID]('t_ManagementTask')
SELECT [dbo].[F_GetNewID]('t_TaskCompany')
SELECT [dbo].[F_GetNewID]('t_TaskUser')
*/
/*
	DECLARE @TableName varchar(50)
	set @TableName='t_SmsSends'
*/
	DECLARE @Len int
	declare @TimeStr varchar(14)
	declare @LenID int
	declare @OutID varchar(20)
	declare @MinID varchar(20)
	declare @MaxID varchar(20)
	declare @Str0 varchar(20)
	if ((@TableName='t_userInfo') or (@TableName='t_ManagementTask') or (@TableName='t_TaskCompany'))
	begin
		--11位时
		SET @Len=11
		select @TimeStr=dbo.F_GetFormatDate ('yyyymm')	
	end
	else if ((@TableName='t_TaskUser') )
	begin
		--11位时
		SET @Len=13
		select @TimeStr=dbo.F_GetFormatDate ('yyyymmdd')	
	end
	else
	begin
		--19位时
		SET @Len=19
		select @TimeStr=dbo.F_GetFormatDate ('YYYYMMDDhhmmss')	
	end

		SET @LenID=@Len-len(@TimeStr)
		SET @Str0='0000000000'
		SET @MinID=@TimeStr+SUBSTRING(@Str0,1,@LenID-1)+'1'
	if (@TableName='t_userInfo')	
		SELECT @MaxID=MAX(UserInfoID)+1 FROM dbo.t_UserInfo WHERE UserInfoID>= @MinID 
	else if (@TableName='t_SmsSends')
		SELECT @MaxID=MAX(SmsSendsID)+1 FROM dbo.t_SmsSends WHERE SmsSendsID>= @MinID 
	else if (@TableName='t_ManagementTask')
		SELECT @MaxID=MAX(ManagementTaskID)+1 FROM dbo.t_ManagementTask WHERE ManagementTaskID>= @MinID 
	else if (@TableName='t_TaskCompany')
		SELECT @MaxID=MAX(TaskCompanyID)+1 FROM dbo.t_TaskCompany WHERE TaskCompanyID>= @MinID 
	else if (@TableName='t_TaskUser')
		SELECT @MaxID=MAX(TaskUserID)+1 FROM dbo.t_TaskUser WHERE TaskUserID>= @MinID 
	else if (@TableName='t_ExecTask')
		SELECT @MaxID=MAX(ExecTaskID)+1 FROM dbo.t_ExecTask WHERE ExecTaskID>= @MinID 
	else
		SELECT @MaxID=MAX(SmsReceiveID)+1 FROM dbo.t_SmsReceive WHERE SmsReceiveID>= @MinID 

		if (@MaxID='' or @MaxID is null)
			set @MaxID='1'
		else
			SET @MaxID =SUBSTRING(@MaxID,LEN(@TimeStr)+1,@LenID)
		SET @Str0 =@Str0+@MaxID
		SET @OutID=@TimeStr+SUBSTRING(@Str0,LEN(@Str0)-@LenID+1,@LenID)


	Return cast(@OutID as bigint)
End
GO
/****** 对象:  UserDefinedFunction [dbo].[F_GetNewID]    脚本日期: 03/03/2011 16:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[F_GetNewID](@TableName varchar(50)) 
ReturnS bigint
As 
Begin 
	DECLARE @OutID bigint	
	SELECT @OutID=[DBO].[F_GetIDFromTable](@TableName)
	Return @OutID
End
GO
