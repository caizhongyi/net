USE [JUSNS_INSTALL_DATA]

CREATE TABLE [NT_Ads](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Content] [nvarchar](200) NULL,
	[Pic] [nvarchar](30) NULL,
	[URL] [nvarchar](200) NULL,
	[Click] [int] NULL,
	[IsLock] [bit] NULL,
	[positionType] [nvarchar](50) NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_NT_Ads] PRIMARY KEY CLUSTERED 
(
	[id] ASC 
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Album](
	[AlbumID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[ImagesCount] [int] NOT NULL CONSTRAINT [DF_NT_AlbumCategory_ImagesCount]  DEFAULT ((0)),
	[CreateTime] [datetime] NOT NULL,
	[Privacy] [int] NOT NULL,
	[LastUploadTime] [datetime] NULL,
	[GroupID] [int] NULL,
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_Album_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_AlbumCategory] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Api](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[apikey] [nvarchar](32) NULL,
	[islock] [bit] NULL,
 CONSTRAINT [PK_NT_Api] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_App](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[appname] [nvarchar](60) NULL,
	[appkey] [nvarchar](32) NULL,
	[classid] [int] NULL,
	[icon] [nvarchar](30) NULL,
	[pic] [nvarchar](30) NULL,
	[UserID] [int] NULL,
	[CreatTime] [datetime] NULL,
	[IsLock] [tinyint] NULL,
	[Content] [ntext] NULL,
	[url] [nvarchar](250) NULL,
	[click] [int] NULL,
	[setupNumber] [int] NULL,
	[targetStyle] [tinyint] NULL,
	[width] [int] NULL,
	[height] [int] NULL,
	[SetupContent] [ntext] NULL,
 CONSTRAINT [PK_NT_App] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0getpass,1deving,2tocheck,3normal,4stop' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_App', @level2type=N'COLUMN',@level2name=N'IsLock'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0frame,1blank' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_App', @level2type=N'COLUMN',@level2name=N'targetStyle'
;
CREATE TABLE [NT_AppDeveloper](
	[userid] [int] NOT NULL,
	[username] [nvarchar](20) NULL,
	[userkey] [nvarchar](32) NULL,
	[JoinTime] [datetime] NULL,
	[tel] [nvarchar](20) NULL,
	[mobile] [nvarchar](30) NULL,
	[email] [nvarchar](150) NULL,
	[IsLock] [tinyint] NULL,
 CONSTRAINT [PK_NT_AppDeveloper] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_AppSetup](
	[userid] [int] NOT NULL,
	[appid] [int] NULL,
	[PostTime] [datetime] NULL
) ON [PRIMARY]
;
CREATE TABLE [NT_Ask](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NOT NULL CONSTRAINT [DF_NT_ask_ClassID]  DEFAULT ((0)),
	[ParentID] [int] NOT NULL CONSTRAINT [DF_NT_ask_ParentID]  DEFAULT ((0)),
	[Title] [nvarchar](100) NULL,
	[Content] [nvarchar](2000) NULL,
	[PostTime] [datetime] NULL,
	[jiFen] [int] NOT NULL CONSTRAINT [DF_NT_ask_jiFen]  DEFAULT ((0)),
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_ask_UserID]  DEFAULT ((0)),
	[isLock] [tinyint] NOT NULL CONSTRAINT [DF_NT_ask_isLock]  DEFAULT ((0)),
	[Tag] [nvarchar](100) NULL,
	[click] [int] NOT NULL CONSTRAINT [DF_NT_ask_click]  DEFAULT ((0)),
	[Pic] [nvarchar](30) NULL,
	[isClose] [tinyint] NOT NULL CONSTRAINT [DF_NT_ask_isClose]  DEFAULT ((0)),
	[isJinji] [tinyint] NOT NULL CONSTRAINT [DF_NT_ask_isJinji]  DEFAULT ((0)),
	[isBest] [tinyint] NULL CONSTRAINT [DF_NT_ask_isBest]  DEFAULT ((0)),
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_Ask_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_ask] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_AskClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_NT_askClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_AskUser](
	[UserID] [int] NULL,
	[jifen] [int] NULL,
	[huida] [int] NULL CONSTRAINT [DF_NT_askUser_huida]  DEFAULT (0)
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回答的问题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_AskUser', @level2type=N'COLUMN',@level2name=N'huida'
;
CREATE TABLE [NT_Ative](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtiveName] [nvarchar](60) NULL,
	[AreaID] [int] NOT NULL CONSTRAINT [DF_NT_ative_AreaID]  DEFAULT ((0)),
	[AreaID1] [int] NOT NULL CONSTRAINT [DF_NT_Ative_ClassID1]  DEFAULT ((0)),
	[ClassID] [int] NOT NULL CONSTRAINT [DF_NT_ative_ClassID]  DEFAULT ((0)),
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_ative_UserID]  DEFAULT ((0)),
	[Members] [int] NOT NULL CONSTRAINT [DF_NT_ative_Members]  DEFAULT ((0)),
	[IsLock] [tinyint] NOT NULL CONSTRAINT [DF_NT_ative_IsLock]  DEFAULT ((0)),
	[Money] [nvarchar](30) NULL,
	[BaoMingTime] [datetime] NULL,
	[PersionNumber] [int] NOT NULL CONSTRAINT [DF_NT_ative_PersionNumber]  DEFAULT ((0)),
	[Clicks] [int] NOT NULL CONSTRAINT [DF_NT_ative_Clicks]  DEFAULT ((0)),
	[ATT] [int] NOT NULL CONSTRAINT [DF_NT_ative_ATT]  DEFAULT ((0)),
	[AddRess] [nvarchar](100) NULL,
	[GroupID] [int] NOT NULL CONSTRAINT [DF_NT_ative_GroupID]  DEFAULT ((0)),
	[Content] [ntext] NULL,
	[PostTime] [datetime] NOT NULL,
	[PostIP] [nvarchar](15) NOT NULL,
	[Links] [nvarchar](200) NULL,
	[Photo] [nvarchar](30) NULL,
	[IsChecks] [tinyint] NOT NULL,
	[IsRec] [tinyint] NOT NULL CONSTRAINT [DF_NT_ative_IsRec]  DEFAULT ((0)),
	[Note] [nvarchar](200) NOT NULL CONSTRAINT [DF_NT_Ative_Note]  DEFAULT (''),
 CONSTRAINT [PK_NT_ative] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_AtiveATT](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Aid] [int] NULL,
	[PostTime] [datetime] NULL,
 CONSTRAINT [PK_NT_AtiveATT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_AtiveClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](30) NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_NT_AtiveClass_ParentID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_AtiveClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_AtiveComment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[ativeID] [int] NULL,
	[posttime] [datetime] NULL,
	[postIP] [nvarchar](15) NULL,
	[IsLock] [bit] NULL,
	[content] [nvarchar](500) NULL,
	[CommentID] [int] NULL CONSTRAINT [DF_NT_AtiveComment_CommentID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_AtiveComment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_AtiveMember](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_AtiveMember_UserID]  DEFAULT ((0)),
	[Aid] [int] NOT NULL CONSTRAINT [DF_NT_AtiveMember_Aid]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[Members] [int] NOT NULL CONSTRAINT [DF_NT_AtiveMember_Members]  DEFAULT ((0)),
	[State] [tinyint] NOT NULL CONSTRAINT [DF_NT_AtiveMember_State]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_AtiveMember] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'报名人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_AtiveMember', @level2type=N'COLUMN',@level2name=N'Members'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0锁定，1有意向参与，2已经报名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_AtiveMember', @level2type=N'COLUMN',@level2name=N'State'
;
CREATE TABLE [NT_Att](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[atterid] [int] NULL,
 CONSTRAINT [PK_NT_Att] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Blog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [nvarchar](40) NOT NULL,
	[Content] [ntext] NULL,
	[PostTime] [datetime] NOT NULL,
	[PostIP] [nvarchar](50) NOT NULL,
	[IsLock] [bit] NOT NULL CONSTRAINT [DF__NT_Blog__IsLock__17A421EC]  DEFAULT ((0)),
	[Comments] [int] NOT NULL CONSTRAINT [DF__NT_Blog__Comment__18984625]  DEFAULT ((0)),
	[Privacy] [int] NOT NULL,
	[PicPath] [nvarchar](50) NULL,
	[LastModTime] [datetime] NOT NULL,
	[Reads] [int] NOT NULL CONSTRAINT [DF_NT_Blog_Reads]  DEFAULT ((0)),
	[isRec] [tinyint] NULL,
	[IsDraft] [tinyint] NULL,
	[attnumber] [int] NULL,
	[click] [int] NULL,
	[ShareNumber] [int] NULL CONSTRAINT [DF_NT_Blog_ShareNumber]  DEFAULT ((0)),
	[myClassID] [int] NULL CONSTRAINT [DF_NT_Blog_myClassID]  DEFAULT ((0)),
	[sysClassID] [int] NULL CONSTRAINT [DF_NT_Blog_sysClassID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_Blog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_BlogClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CName] [nvarchar](30) NULL,
	[UserID] [int] NULL CONSTRAINT [DF_NT_BlogClass_UserID]  DEFAULT ((0)),
	[OrderID] [int] NULL CONSTRAINT [DF_NT_BlogClass_OrderID]  DEFAULT ((0)),
	[ParentID] [int] NULL CONSTRAINT [DF_NT_BlogClass_ParentID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_BlogClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_BlogComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](300) NOT NULL,
	[PostTime] [datetime] NOT NULL,
	[PostIP] [nvarchar](50) NOT NULL,
	[IsLock] [bit] NOT NULL CONSTRAINT [DF__NT_BLogCo__IsLoc__748FEFD9]  DEFAULT ((0)),
	[CommID] [int] NULL CONSTRAINT [DF_NT_BlogComment_CommentID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_BLOGCOMMENT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Blogfoot](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NULL,
	[UserID] [int] NULL,
	[CreatTime] [datetime] NULL,
 CONSTRAINT [PK_NT_BlogFoot] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Calend](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL CONSTRAINT [DF_NT_Calend_UserID]  DEFAULT ((0)),
	[Title] [nvarchar](50) NULL,
	[Content] [nvarchar](250) NULL,
	[PostTime] [datetime] NULL,
	[NoteNumber] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_NT_Calend] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_ChargeOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Money] [int] NULL,
	[point] [int] NULL,
	[gpoint] [int] NULL,
	[orderNumber] [nvarchar](20) NULL,
	[UserID] [int] NULL,
	[IsSucces] [bit] NULL,
	[CreatTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
 CONSTRAINT [PK_NT_ChargeOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Constellation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Constellation] [nvarchar](10) NULL,
 CONSTRAINT [PK_NT_Constellation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Dict_Area](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_NT_Dict_Area_ParentID]  DEFAULT (0),
	[Name] [nvarchar](50) NOT NULL,
	[IsLock] [bit] NOT NULL CONSTRAINT [DF__NT_Dict_A__IsLoc__1F4543B4]  DEFAULT (0),
 CONSTRAINT [PK_NT_Dict_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Dict_Vocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VocName] [nvarchar](20) NOT NULL,
	[IsLock] [bit] NOT NULL,
 CONSTRAINT [PK_NT_DICT_VOCATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Dyn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[cUserID] [int] NULL,
	[dynType] [int] NULL,
	[Content] [nvarchar](300) NULL,
	[PostTime] [datetime] NULL,
	[infoarr] [int] NULL,
 CONSTRAINT [PK_NT_Dyn] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_EmailNotice](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[email] [nvarchar](150) NULL,
	[vcode] [nvarchar](16) NULL,
	[posttime] [datetime] NULL,
	[ntype] [tinyint] NULL,
 CONSTRAINT [PK_NT_EmailNotice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0找回密码，1激活帐户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_EmailNotice', @level2type=N'COLUMN',@level2name=N'ntype'
;
CREATE TABLE [NT_Favorite](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[URL] [nvarchar](200) NULL,
	[ClassID] [int] NULL,
	[IsPub] [bit] NULL CONSTRAINT [DF_NT_Favorite_IsPub]  DEFAULT ((1)),
	[title] [nvarchar](60) NULL,
	[content] [nvarchar](500) NULL,
	[PostTime] [datetime] NULL,
 CONSTRAINT [PK_NT_Favorite] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_FavoriteClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[IsPub] [bit] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_NT_FavoriteClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL CONSTRAINT [DF_NT_Files_UserID]  DEFAULT ((0)),
	[title] [nvarchar](50) NULL,
	[GroupID] [int] NULL CONSTRAINT [DF_NT_Files_GroupID]  DEFAULT ((0)),
	[FileName] [nvarchar](30) NULL,
	[FileSize] [int] NULL CONSTRAINT [DF_NT_Files_FileSize]  DEFAULT ((0)),
	[PostIP] [nvarchar](15) NULL,
	[PostTime] [datetime] NULL,
	[DownNumber] [int] NULL CONSTRAINT [DF_NT_Files_DownNumber]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_Files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_FilesRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[InfoID] [int] NULL,
	[Types] [tinyint] NULL,
	[PostIP] [nvarchar](15) NULL,
	[PostTime] [datetime] NULL,
 CONSTRAINT [PK_NT_FilesRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Flash](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bPic] [nvarchar](30) NULL,
	[sPic] [nvarchar](30) NULL,
	[URL] [nvarchar](200) NULL,
	[IsLock] [bit] NULL,
	[OrderID] [int] NULL,
	[PostTime] [datetime] NULL,
 CONSTRAINT [PK_NT_Flash] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Friend](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[FriendID] [int] NOT NULL,
	[State] [int] NOT NULL CONSTRAINT [DF__NT_Friend__State__17D92C16]  DEFAULT ((1)),
	[descript] [nvarchar](100) NOT NULL CONSTRAINT [DF_NT_Friend_Relation]  DEFAULT ((0)),
	[PostTime] [datetime] NOT NULL,
	[ClassID] [int] NOT NULL CONSTRAINT [DF_NT_Friend_ClassID]  DEFAULT ((0)),
	[FDegree] [int] NOT NULL CONSTRAINT [DF_NT_friend_FDegree]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_FRIEND] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0通过，1审核中，2拒绝' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_Friend', @level2type=N'COLUMN',@level2name=N'State'
;
CREATE TABLE [NT_Friendclass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CName] [nvarchar](15) NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_NT_FriendClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Friendinvite](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[email] [nvarchar](150) NULL,
	[Reply] [int] NOT NULL CONSTRAINT [DF_NT_Friendinvite_Reply]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[ReplyTime] [datetime] NULL,
	[ReplyIP] [nvarchar](15) NULL,
	[ValidCode] [nvarchar](10) NULL,
	[RegUserID] [int] NULL,
 CONSTRAINT [PK_NT_FRIENDINVITE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_GBook](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SendID] [int] NULL,
	[Content] [ntext] NULL,
	[ParentID] [int] NULL,
	[PostTime] [datetime] NULL,
	[IsLock] [tinyint] NULL,
 CONSTRAINT [PK_NT_GBook] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_Gift](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GiftName] [nvarchar](50) NULL,
	[Pic] [nvarchar](30) NULL,
	[GPoint] [int] NULL CONSTRAINT [DF_NT_Gift_GPoint]  DEFAULT ((0)),
	[Point] [int] NULL CONSTRAINT [DF_NT_Gift_Point]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[SendNumber] [int] NULL CONSTRAINT [DF_NT_Gift_SendNumber]  DEFAULT ((0)),
	[Content] [nvarchar](250) NULL,
	[ClassID] [int] NULL CONSTRAINT [DF_NT_Gift_ClassID]  DEFAULT ((0)),
	[IsAd] [bit] NULL CONSTRAINT [DF_NT_Gift_IsAd]  DEFAULT ((0)),
	[IsLock] [tinyint] NULL,
 CONSTRAINT [PK_NT_Gife] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_GiftClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[ParentID] [int] NULL CONSTRAINT [DF_NT_GiftClass_ParentID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_GiftClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_GiftUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[giftID] [int] NULL,
	[UserID] [int] NULL,
	[ReviceID] [int] NULL,
	[PostTime] [datetime] NULL,
	[Content] [nvarchar](500) NULL,
 CONSTRAINT [PK_NT_GiftUser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

;
CREATE TABLE [NT_Group](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[GroupName] [nvarchar](30) NOT NULL,
	[Members] [int] NOT NULL CONSTRAINT [DF__NT_UserGr__Membe__67F50ECA]  DEFAULT ((1)),
	[Bulletin] [nvarchar](255) NULL CONSTRAINT [DF_NT_Group_Bulletin]  DEFAULT (''),
	[CityID] [int] NOT NULL CONSTRAINT [DF_NT_Group_NetWork]  DEFAULT ((0)),
	[Privacy] [int] NOT NULL CONSTRAINT [DF_NT_Group_Publicity]  DEFAULT ((0)),
	[Publics] [int] NULL CONSTRAINT [DF_NT_Group_Public]  DEFAULT ((0)),
	[Portrait] [nvarchar](30) NULL,
	[IsLock] [bit] NOT NULL CONSTRAINT [DF_NT_Group_State]  DEFAULT ((0)),
	[PostTime] [datetime] NOT NULL,
	[PostIP] [nvarchar](15) NOT NULL,
	[isRec] [tinyint] NOT NULL CONSTRAINT [DF_NT_Group_isRec]  DEFAULT ((0)),
	[ClassID] [int] NULL CONSTRAINT [DF_NT_Group_ClassID]  DEFAULT ((0)),
	[skinDir] [nvarchar](30) NULL CONSTRAINT [DF_NT_Group_skinDir]  DEFAULT (N'default'),
	[Click] [int] NULL CONSTRAINT [DF_NT_Group_Click]  DEFAULT ((0)),
	[Islight] [bit] NULL CONSTRAINT [DF_NT_Group_Islight]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_UserGroup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_GroupClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[className] [nvarchar](30) NULL,
	[parentid] [int] NULL CONSTRAINT [DF_NT_GroupClass_parentid]  DEFAULT ((0)),
	[isCreat] [bit] NULL CONSTRAINT [DF_NT_GroupClass_isCreat]  DEFAULT ((1)),
 CONSTRAINT [PK_NT_GroupClass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_GroupInvite](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL CONSTRAINT [DF_NT_Groupinvite_GroupID]  DEFAULT ((0)),
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_Groupinvite_UserID]  DEFAULT ((0)),
	[ReviceID] [int] NOT NULL CONSTRAINT [DF_NT_Groupinvite_JoinUserID]  DEFAULT ((0)),
	[Content] [nvarchar](100) NULL,
	[PostTime] [datetime] NOT NULL,
	[IsAccept] [bit] NOT NULL CONSTRAINT [DF__NT_GroupI__Respo__2903B818]  DEFAULT ((0)),
	[AccTime] [datetime] NULL,
 CONSTRAINT [PK_NT_GROUPINVITE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_GroupMember](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[JoinTime] [datetime] NOT NULL,
	[Grade] [int] NOT NULL CONSTRAINT [DF_NT_GroupMember_Grade]  DEFAULT ((0)),
	[Islock] [bit] NULL CONSTRAINT [DF_NT_GroupMember_Islock]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_GROUPMEMBER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0普通成员1管理员2创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_GroupMember', @level2type=N'COLUMN',@level2name=N'Grade'
;
CREATE TABLE [NT_GroupTopic](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[groupid] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[title] [nvarchar](80) NULL,
	[content] [ntext] NULL,
	[posttime] [datetime] NULL,
	[TopicID] [int] NOT NULL CONSTRAINT [DF_NT_GroupTopic_TopicID]  DEFAULT ((0)),
	[isTop] [bit] NOT NULL CONSTRAINT [DF_NT_GroupTopic_isTop]  DEFAULT ((0)),
	[lastpostTime] [datetime] NULL,
	[Replynumber] [int] NOT NULL CONSTRAINT [DF_NT_GroupTopic_Replynumber]  DEFAULT ((0)),
	[Clicks] [int] NOT NULL CONSTRAINT [DF_NT_GroupTopic_Clicks]  DEFAULT ((0)),
	[IsLock] [tinyint] NULL CONSTRAINT [DF_NT_GroupTopic_IsLock]  DEFAULT ((0)),
	[IsBest] [tinyint] NULL CONSTRAINT [DF_NT_GroupTopic_IsBest]  DEFAULT ((0)),
	[PostIP] [nvarchar](15) NULL,
	[FoolNumber] [int] NULL CONSTRAINT [DF_NT_GroupTopic_FoolNumber]  DEFAULT ((1)),
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_GroupTopic_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_GroupTopic] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_Help](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HelpID] [nvarchar](30) NULL,
	[Title] [nvarchar](150) NULL,
	[Content] [ntext] NULL,
 CONSTRAINT [PK_NT_Help] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

;
CREATE TABLE [NT_JoinVip](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[PostTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[IsLock] [tinyint] NULL,
	[Content] [nvarchar](100) NULL CONSTRAINT [DF_NT_JoinVip_Content]  DEFAULT (''),
 CONSTRAINT [PK_NT_JoinVip] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Links](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LinkName] [nvarchar](50) NULL,
	[URL] [nvarchar](100) NULL,
	[LinkType] [tinyint] NULL,
	[Pic] [nvarchar](30) NULL,
	[Islock] [bit] NULL,
 CONSTRAINT [PK_NT_Links] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Magic](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[mName] [nvarchar](30) NULL,
	[pic] [nvarchar](30) NULL,
	[point] [int] NULL,
	[gpoint] [int] NULL,
	[number] [int] NULL,
	[buynumber] [int] NULL,
	[mdesc] [nvarchar](200) NULL,
	[mType] [int] NULL,
	[CreatTime] [datetime] NULL,
	[state] [tinyint] NULL,
	[vTime] [int] NULL,
 CONSTRAINT [PK_NT_magic] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'道具类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_Magic', @level2type=N'COLUMN',@level2name=N'mType'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'道具有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_Magic', @level2type=N'COLUMN',@level2name=N'vTime'
;
CREATE TABLE [NT_MagicInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[MID] [int] NULL,
	[PostTime] [datetime] NULL,
	[IsUse] [tinyint] NULL,
	[SendUserID] [int] NULL,
	[IsUserTime] [datetime] NULL,
	[Number] [int] NULL,
 CONSTRAINT [PK_NT_MagicInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_MagicLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[mID] [int] NULL,
	[Num] [int] NULL,
	[MDesc] [nvarchar](30) NULL,
	[mType] [tinyint] NULL,
	[PostTime] [datetime] NULL,
 CONSTRAINT [PK_NT_MagicLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0获得1赠送2使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_MagicLogs', @level2type=N'COLUMN',@level2name=N'mType'
;
CREATE TABLE [NT_Mailbox](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SendID] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Content] [ntext] NULL,
	[PostTime] [datetime] NULL,
	[IsRead] [bit] NULL,
 CONSTRAINT [PK_NT_Mailbox] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_MailSend](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ReviceID] [int] NULL,
	[PostTime] [datetime] NULL,
	[title] [nvarchar](50) NULL,
	[content] [ntext] NULL,
 CONSTRAINT [PK_NT_MailSend] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_News](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](60) NULL,
	[color] [nvarchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[bold] [tinyint] NULL,
	[italic] [tinyint] NULL,
	[Content] [ntext] NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_News_UserID]  DEFAULT ((0)),
	[ClassID] [int] NOT NULL CONSTRAINT [DF_NT_News_ClassID]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL CONSTRAINT [DF_NT_News_PostIP]  DEFAULT (''),
	[IsLock] [tinyint] NOT NULL CONSTRAINT [DF_NT_News_islock]  DEFAULT ((0)),
	[IsRec] [tinyint] NULL CONSTRAINT [DF_NT_News_IsRec]  DEFAULT ((0)),
	[Click] [int] NULL CONSTRAINT [DF_NT_News_click]  DEFAULT ((0)),
	[IsSys] [tinyint] NOT NULL CONSTRAINT [DF_NT_News_isSys]  DEFAULT ((0)),
	[ShareNumber] [int] NOT NULL CONSTRAINT [DF_NT_News_shareNum]  DEFAULT ((0)),
	[Pic] [nvarchar](30) NULL CONSTRAINT [DF_NT_News_Pic]  DEFAULT (''),
	[Files] [nvarchar](40) NULL CONSTRAINT [DF_NT_News_Files]  DEFAULT (''),
	[AttNumber] [int] NULL CONSTRAINT [DF_NT_News_AttNumber]  DEFAULT ((0)),
	[Comments] [int] NULL CONSTRAINT [DF_NT_News_Comments]  DEFAULT ((0)),
	[Keywords] [nvarchar](30) NULL CONSTRAINT [DF_NT_News_Keywords]  DEFAULT (''),
	[Source] [nvarchar](20) NULL CONSTRAINT [DF_NT_News_Source]  DEFAULT (''),
	[Point] [int] NULL CONSTRAINT [DF_NT_News_Point]  DEFAULT ((0)),
	[GPoint] [int] NULL CONSTRAINT [DF_NT_News_GPoint]  DEFAULT ((0)),
	[SpecialList] [nvarchar](30) NULL CONSTRAINT [DF_NT_News_SpecialList]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_news] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否系统公告' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_News', @level2type=N'COLUMN',@level2name=N'IsSys'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分享次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_News', @level2type=N'COLUMN',@level2name=N'ShareNumber'
;
CREATE TABLE [NT_NewsChannel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ChannelName] [nvarchar](30) NOT NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_NT_NewsChannel_ParentID]  DEFAULT ((0)),
	[PerPageNumber] [int] NOT NULL CONSTRAINT [DF_NT_NewsChannel_PerPageNumber]  DEFAULT ((20)),
	[Pic] [nvarchar](20) NULL CONSTRAINT [DF_NT_NewsChannel_Pic]  DEFAULT (''),
	[ChannelType] [tinyint] NULL CONSTRAINT [DF_NT_NewsChannel_ChannelType]  DEFAULT ((0)),
	[OrderID] [int] NULL CONSTRAINT [DF_NT_NewsChannel_OrderID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_newsclass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_NewsComment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NewsID] [int] NULL,
	[content] [nvarchar](200) NULL,
	[UserID] [int] NOT NULL,
	[parentID] [int] NOT NULL,
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[IsLock] [tinyint] NULL,
 CONSTRAINT [PK_NT_newscomment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Notice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ReviceID] [int] NOT NULL,
	[Content] [nvarchar](255) NULL,
	[IsRead] [bit] NOT NULL CONSTRAINT [DF__NT_Notice__IsRea__459FF6C6]  DEFAULT ((0)),
	[PostTime] [datetime] NOT NULL,
	[PostIP] [char](15) NOT NULL,
	[MsgType] [tinyint] NOT NULL,
	[CorrID] [int] NULL,
 CONSTRAINT [PK_NT_NOTICE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

;
CREATE TABLE [NT_Onlineuser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[LastIP] [nvarchar](15) NULL,
	[LastUrl] [nvarchar](200) NULL,
	[LastTime] [datetime] NULL,
	[UserName] [nvarchar](20) NULL,
 CONSTRAINT [PK_NT_Onlineuser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Photo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Views] [int] NOT NULL CONSTRAINT [DF_NT_Photo_ViewTimes]  DEFAULT ((0)),
	[FileSize] [int] NOT NULL CONSTRAINT [DF_NT_Photo_FileSize]  DEFAULT ((0)),
	[State] [int] NOT NULL CONSTRAINT [DF_NT_Photo_State]  DEFAULT ((0)),
	[IsCover] [bit] NOT NULL CONSTRAINT [DF_NT_Photo_IsCover]  DEFAULT ((0)),
	[Comments] [int] NOT NULL CONSTRAINT [DF_NT_Photo_CommentCount]  DEFAULT ((0)),
	[PostTime] [datetime] NOT NULL,
	[PostIP] [nvarchar](50) NOT NULL,
	[PhotoType] [int] NOT NULL,
	[Width] [int] NOT NULL CONSTRAINT [DF_NT_Photo_Width]  DEFAULT ((0)),
	[Height] [int] NOT NULL CONSTRAINT [DF_NT_Photo_Height]  DEFAULT ((0)),
	[FilePath] [nvarchar](50) NOT NULL,
	[ShareNumber] [int] NOT NULL CONSTRAINT [DF_NT_Photo_ShareNumber]  DEFAULT ((0)),
	[IsLock] [bit] NOT NULL CONSTRAINT [DF_NT_Photo_IsLock]  DEFAULT ((0)),
	[AtiveID] [int] NOT NULL CONSTRAINT [DF_NT_Photo_AtiveID]  DEFAULT ((0)),
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_Photo_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_Photos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0头像相册，-1活动相册，其他为普通相册' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_Photo', @level2type=N'COLUMN',@level2name=N'AlbumID'
;
CREATE TABLE [NT_PhotoComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhotoID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](300) NOT NULL,
	[PostTime] [datetime] NOT NULL,
	[PostIP] [nvarchar](15) NOT NULL,
	[IsLock] [bit] NOT NULL CONSTRAINT [DF__NT_PhotoC__IsLoc__4F296100]  DEFAULT ((0)),
	[CommentID] [int] NULL CONSTRAINT [DF_NT_PhotoComment_CommentID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_PhotoComment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Poke](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ReviceID] [int] NULL,
	[PokeKey] [int] NULL,
	[PokeForm] [nvarchar](50) NULL,
	[Poketo] [nvarchar](50) NULL,
	[PostTime] [datetime] NULL,
	[IsPub] [tinyint] NULL,
	[IsRead] [bit] NULL CONSTRAINT [DF_NT_Poke_IsRead]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_Poke] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Report](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NULL,
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[Urls] [nvarchar](200) NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_NT_Report] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_Share](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL CONSTRAINT [DF_NT_Share_UserID]  DEFAULT ((0)),
	[ShareType] [tinyint] NULL,
	[infoid] [int] NOT NULL CONSTRAINT [DF_NT_Share_infoid]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[IsLock] [tinyint] NULL CONSTRAINT [DF_NT_Share_IsLock]  DEFAULT ((0)),
	[Title] [nvarchar](50) NULL,
	[Content] [ntext] NULL,
	[webURL] [nvarchar](120) NULL,
	[Comments] [int] NOT NULL CONSTRAINT [DF_NT_Share_Comments]  DEFAULT ((0)),
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_Share_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_Share] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_Shop](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ShopName] [nvarchar](80) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[Faren] [nvarchar](20) NULL,
	[FarenMobile] [nvarchar](26) NULL,
	[linkMan] [nvarchar](20) NULL,
	[Mobile] [nvarchar](26) NULL,
	[Tel] [nvarchar](20) NULL,
	[Fax] [nvarchar](15) NULL,
	[AddRess] [nvarchar](60) NULL,
	[PostCode] [nvarchar](10) NULL,
	[ClassID] [int] NOT NULL CONSTRAINT [DF_NT_Shop_ClassID]  DEFAULT ((0)),
	[AreaID] [int] NOT NULL CONSTRAINT [DF_NT_Shop_AreaID]  DEFAULT ((0)),
	[ShopRName] [nvarchar](50) NULL,
	[ShopAddress] [nvarchar](60) NULL,
	[JoinCase] [nvarchar](100) NULL,
	[HasSerive] [tinyint] NOT NULL CONSTRAINT [DF_NT_Shop_HasSerive]  DEFAULT ((0)),
	[Content] [ntext] NULL,
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[IsLock] [tinyint] NOT NULL CONSTRAINT [DF_NT_Shop_IsLock]  DEFAULT ((0)),
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_Shop_UserID]  DEFAULT ((0)),
	[Keywords] [nvarchar](30) NULL,
	[TopNumber] [int] NULL CONSTRAINT [DF_NT_Shop_TopNumber]  DEFAULT ((0)),
	[DownNumber] [int] NULL CONSTRAINT [DF_NT_Shop_DownNumber]  DEFAULT ((0)),
	[Click] [int] NULL CONSTRAINT [DF_NT_Shop_Click]  DEFAULT ((0)),
	[Pic] [nvarchar](30) NULL,
	[IsRec] [bit] NULL,
 CONSTRAINT [PK_NT_Shop] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_ShopClass](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_NT_ShopClass_ParentID]  DEFAULT ((0)),
	[IsLock] [bit] NOT NULL CONSTRAINT [DF_NT_ShopClass_IsLock]  DEFAULT ((0)),
	[OrderID] [int] NOT NULL CONSTRAINT [DF_NT_ShopClass_OrderID]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_ShopClass] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_ShopComment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NULL CONSTRAINT [DF_NT_ShopComment_ShopID]  DEFAULT ((0)),
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_ShopComment_UserID]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NOT NULL CONSTRAINT [DF_NT_ShopComment_PostIP]  DEFAULT (''),
	[Islock] [bit] NOT NULL CONSTRAINT [DF_NT_ShopComment_Islock]  DEFAULT ((0)),
	[commid] [int] NOT NULL CONSTRAINT [DF_NT_ShopComment_CommentID]  DEFAULT ((0)),
	[cType] [tinyint] NOT NULL CONSTRAINT [DF_NT_ShopComment_cType]  DEFAULT ((0)),
	[ShopID] [int] NULL CONSTRAINT [DF_NT_ShopComment_ShopID_1]  DEFAULT ((0)),
	[Content] [ntext] NULL,
 CONSTRAINT [PK_NT_ShopComment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0商品评论，1店铺评论,2团购' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_ShopComment', @level2type=N'COLUMN',@level2name=N'cType'
;
CREATE TABLE [NT_ShopGoods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsName] [nvarchar](60) NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_UserID]  DEFAULT ((0)),
	[ShopID] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_ShopID]  DEFAULT ((0)),
	[Price] [float] NOT NULL CONSTRAINT [DF_NT_ShopGoods_Price]  DEFAULT ((0)),
	[mPrice] [float] NOT NULL CONSTRAINT [DF_NT_ShopGoods_mPrice]  DEFAULT ((0)),
	[Tel] [nvarchar](50) NULL,
	[IsOld] [bit] NOT NULL CONSTRAINT [DF_NT_ShopGoods_IsOld]  DEFAULT ((0)),
	[AddRess] [nvarchar](100) NULL,
	[keywords] [nvarchar](30) NULL,
	[Content] [ntext] NULL,
	[Click] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_Click]  DEFAULT ((0)),
	[TopNumber] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_TopNumber]  DEFAULT ((0)),
	[DownNumber] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_DownNumber]  DEFAULT ((0)),
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Number] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_Number]  DEFAULT ((0)),
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[ClassID] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_ClassID]  DEFAULT ((0)),
	[AreaID] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_AreaID]  DEFAULT ((0)),
	[ExpressStyle] [tinyint] NULL,
	[ExpressContent] [nvarchar](100) NULL,
	[MulteBuy] [tinyint] NOT NULL CONSTRAINT [DF_NT_ShopGoods_MulteBuy]  DEFAULT ((0)),
	[MulteMinNumber] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_MulteMinNumber]  DEFAULT ((0)),
	[MulteMaxNumber] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_MulteMaxNumber]  DEFAULT ((0)),
	[multePrice] [float] NOT NULL CONSTRAINT [DF_NT_ShopGoods_multePrice]  DEFAULT ((0)),
	[IsLock] [tinyint] NOT NULL CONSTRAINT [DF_NT_ShopGoods_IsLock]  DEFAULT ((0)),
	[buyNumber] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_MulteMembers]  DEFAULT ((0)),
	[Pic] [nvarchar](30) NULL,
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_ShopGoods_IsRec]  DEFAULT ((0)),
	[GPoint] [int] NOT NULL CONSTRAINT [DF_NT_ShopGoods_GPoint]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_ShopGoods] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计销售' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_ShopGoods', @level2type=N'COLUMN',@level2name=N'buyNumber'
;
CREATE TABLE [NT_ShopMultebuy](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[GoodsID] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Content] [ntext] NULL,
	[MinMember] [int] NULL,
	[MaxMember] [int] NULL,
	[IsLock] [tinyint] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[JoinMember] [int] NULL,
	[AttMember] [int] NULL,
	[Price] [money] NULL,
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[ProvinceID] [int] NULL,
	[CityID] [int] NULL,
	[AddRess] [nvarchar](60) NULL,
	[LinkStyle] [nvarchar](50) NULL,
	[Keywords] [nvarchar](30) NULL,
	[Pic] [nvarchar](30) NULL,
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_ShopMultebuy_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_ShopMultebuy] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_ShopMulteMember](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[MID] [int] NULL,
	[PostTime] [datetime] NULL,
	[Tel] [nvarchar](250) NULL,
 CONSTRAINT [PK_NT_MulteMember] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_ShopNews](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ShopID] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Content] [ntext] NULL,
	[creatTime] [datetime] NULL,
	[islock] [bit] NULL,
	[click] [int] NULL,
 CONSTRAINT [PK_NT_ShopNews] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_ShopOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsID] [int] NULL,
	[UserID] [int] NULL,
	[OrderNumber] [nvarchar](16) NULL,
	[PostTime] [datetime] NULL,
	[IsLock] [tinyint] NULL,
	[PostIP] [nvarchar](15) NULL,
	[Money] [money] NULL,
	[GPoint] [int] NULL,
	[IsPost] [bit] NOT NULL CONSTRAINT [DF_NT_ShopOrder_IsPost]  DEFAULT ((0)),
	[IsRevice] [bit] NULL CONSTRAINT [DF_NT_ShopOrder_IsRevice]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_ShopOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_ShopUserComment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GoodsID] [int] NOT NULL,
	[Sore] [tinyint] NOT NULL CONSTRAINT [DF_NT_ShopUserComment_Sore]  DEFAULT ((5)),
	[UserID] [int] NULL,
	[Content] [ntext] NULL,
	[PostTime] [datetime] NULL,
	[PostIP] [nvarchar](15) NULL,
	[CommentID] [int] NULL,
 CONSTRAINT [PK_NT_ShopUserComment_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_ShopUserComment', @level2type=N'COLUMN',@level2name=N'Sore'
;
CREATE TABLE [NT_SpaceTemplate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TName] [nvarchar](20) NOT NULL,
	[TEName] [nvarchar](20) NULL,
	[PostTime] [datetime] NOT NULL,
	[IsLock] [tinyint] NOT NULL,
	[IPoint] [int] NULL,
	[GPoint] [int] NULL,
	[UseNumber] [int] NULL,
 CONSTRAINT [PK_NT_SpaceTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

;
CREATE TABLE [NT_spareemail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Email] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PostTime] [datetime] NULL,
	[PostIP] [char](15) COLLATE Chinese_PRC_CI_AS NULL,
	[vCode] [nvarchar](15) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_User_SpareEmail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Twitter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](200) NOT NULL,
	[PostTime] [datetime] NOT NULL,
	[PostIP] [char](15) NOT NULL,
	[IsLock] [bit] NOT NULL CONSTRAINT [DF__NT_MiniBl__IsLoc__3845FBA8]  DEFAULT ((0)),
	[Comments] [int] NOT NULL CONSTRAINT [DF_NT_MiniBlog_Comments]  DEFAULT ((0)),
	[MType] [nvarchar](20) NULL,
	[isRec] [tinyint] NULL CONSTRAINT [DF_NT_MiniBlog_isRec]  DEFAULT ((0)),
	[pic] [nvarchar](30) NULL CONSTRAINT [DF_NT_Twitter_pic]  DEFAULT (''),
	[media] [nvarchar](200) NULL CONSTRAINT [DF_NT_Twitter_media]  DEFAULT (''),
 CONSTRAINT [PK_NT_MiniBlog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_TwitterComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Tid] [int] NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
	[CommentID] [int] NULL,
	[PostTime] [datetime] NOT NULL,
	[PostIP] [char](15) NOT NULL,
	[IsLock] [bit] NULL,
 CONSTRAINT [PK_NT_MiniBlogComment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Mobile] [nvarchar](15) NULL CONSTRAINT [DF_NT_User_Mobile]  DEFAULT (''),
	[Password] [nchar](32) NOT NULL,
	[TrueName] [nvarchar](20) NULL,
	[Sex] [tinyint] NULL CONSTRAINT [DF_NT_user_Sex]  DEFAULT ((0)),
	[Marriage] [tinyint] NOT NULL CONSTRAINT [DF_NT_user_Marriage]  DEFAULT ((0)),
	[BindMoblie] [bit] NULL CONSTRAINT [DF_NT_user_BindMoblie]  DEFAULT ((0)),
	[State] [tinyint] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[LastLoginIP] [nvarchar](15) NULL,
	[LoginTimes] [int] NOT NULL CONSTRAINT [DF_NT_User_LoginTimes]  DEFAULT ((0)),
	[ProvinceID] [int] NULL CONSTRAINT [DF_NT_User_ProvinceID]  DEFAULT ((0)),
	[City] [int] NULL,
	[RegTime] [datetime] NOT NULL,
	[RegIP] [nvarchar](50) NOT NULL,
	[Portrait] [int] NULL CONSTRAINT [DF_NT_user_Portrait]  DEFAULT ((0)),
	[InviterID] [int] NULL CONSTRAINT [DF_NT_user_InviterID]  DEFAULT ((0)),
	[VerifyCode] [nvarchar](15) NULL,
	[ConfirmTime] [datetime] NULL,
	[isRec] [tinyint] NULL CONSTRAINT [DF_NT_user_isRec]  DEFAULT ((0)),
	[integral] [int] NULL CONSTRAINT [DF_NT_User_integral]  DEFAULT ((0)),
	[inteyb] [int] NULL CONSTRAINT [DF_NT_User_inteyb]  DEFAULT ((0)),
	[Click] [int] NULL CONSTRAINT [DF_NT_user_Click]  DEFAULT ((0)),
	[isAdmin] [tinyint] NULL CONSTRAINT [DF_NT_user_isAdmin]  DEFAULT ((0)),
	[AttNumber] [int] NULL CONSTRAINT [DF_NT_user_AttNumber]  DEFAULT ((0)),
	[MobileCode] [int] NULL CONSTRAINT [DF_NT_user_MobileCode]  DEFAULT ((0)),
	[IsVip] [bit] NULL CONSTRAINT [DF_NT_User_IsVideo]  DEFAULT ((0)),
	[Money] [money] NOT NULL CONSTRAINT [DF_NT_User_Money]  DEFAULT ((0)),
	[MemberLevels] [int] NOT NULL CONSTRAINT [DF_NT_User_MemberLevels]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_User', @level2type=N'COLUMN',@level2name=N'Marriage'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'guanzhu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NT_User', @level2type=N'COLUMN',@level2name=N'AttNumber'
;
CREATE TABLE [NT_UserCareer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Company] [nvarchar](50) NOT NULL,
	[JoinTime] [nvarchar](10) NULL,
	[PostTime] [datetime] NOT NULL,
	[LeaveTime] [nvarchar](10) NULL,
 CONSTRAINT [PK_NT_USERCAREER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_UserEducation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[schoolName] [nvarchar](50) NOT NULL,
	[leaveyear] [nvarchar](10) NOT NULL,
	[PostTime] [datetime] NOT NULL,
	[levels] [tinyint] NULL,
 CONSTRAINT [PK_NT_USEREDUCATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_UserInfo](
	[UserID] [int] NOT NULL,
	[Birthday] [datetime] NULL CONSTRAINT [DF_NT_userinfo_Birthday]  DEFAULT (((1950)-(1))-(1)),
	[BirthidayDisplay] [int] NULL CONSTRAINT [DF_NT_userinfo_BirthidayDisplay]  DEFAULT ((0)),
	[Constellation] [int] NULL CONSTRAINT [DF_NT_userinfo_Constellation]  DEFAULT ((0)),
	[MSN] [nvarchar](50) NULL CONSTRAINT [DF_NT_UserInfo_MSN]  DEFAULT (''),
	[QQ] [nvarchar](12) NULL CONSTRAINT [DF_NT_UserInfo_QQ]  DEFAULT (''),
	[Tel] [nvarchar](20) NULL CONSTRAINT [DF_NT_UserInfo_Tel]  DEFAULT (''),
	[Addr] [nvarchar](50) NULL CONSTRAINT [DF_NT_UserInfo_Addr]  DEFAULT (''),
	[WebSite] [nvarchar](60) NULL CONSTRAINT [DF_NT_UserInfo_WebSite]  DEFAULT (''),
	[Favourite] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_Favourite]  DEFAULT (''),
	[FavMusics] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_FavMusics]  DEFAULT (''),
	[FavMovies] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_FavMovies]  DEFAULT (''),
	[FavCartoons] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_FavCartoons]  DEFAULT (''),
	[FavGames] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_FavGames]  DEFAULT (''),
	[FavSports] [nvarchar](80) NULL CONSTRAINT [DF_NT_UserInfo_FavSports]  DEFAULT (''),
	[FavBooks] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_FavBooks]  DEFAULT (''),
	[FavAdages] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_FavAdages]  DEFAULT (''),
	[AppreciateMen] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_AppreciateMen]  DEFAULT (''),
	[Presentation] [nvarchar](100) NULL CONSTRAINT [DF_NT_UserInfo_Presentation]  DEFAULT (''),
	[Vocation] [int] NOT NULL CONSTRAINT [DF_NT_UserInfo_Vocation]  DEFAULT ((0)),
	[HomeCity] [int] NOT NULL CONSTRAINT [DF_NT_UserInfo_HomeCity]  DEFAULT ((0)),
	[MobilePrivacy] [tinyint] NOT NULL CONSTRAINT [DF_NT_userinfo_MobilePrivacy]  DEFAULT ((0)),
	[EmailPrivacy] [tinyint] NOT NULL CONSTRAINT [DF_NT_userinfo_EmailPrivacy]  DEFAULT ((0)),
	[MSNPrivacy] [int] NOT NULL CONSTRAINT [DF_NT_userinfo_MSNPrivacy]  DEFAULT ((0)),
	[QQPrivacy] [int] NOT NULL CONSTRAINT [DF_NT_userinfo_QQPrivacy]  DEFAULT ((0)),
	[TelPrivacy] [int] NOT NULL CONSTRAINT [DF_NT_userinfo_TelPrivacy]  DEFAULT ((0)),
	[AddrPrivacy] [int] NOT NULL CONSTRAINT [DF_NT_userinfo_AddrPrivacy]  DEFAULT ((0)),
	[WebSitePrivacy] [int] NOT NULL CONSTRAINT [DF_NT_userinfo_WebSitePrivacy]  DEFAULT ((0)),
	[taobaoIM] [nvarchar](50) NULL,
	[BaiduIM] [nvarchar](50) NULL,
 CONSTRAINT [PK_NT_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Userlog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ActionType] [int] NOT NULL,
	[LogTime] [datetime] NOT NULL,
	[Description] [nvarchar](30) NULL,
	[CorrespondInfo] [int] NULL,
	[CorreUserID] [int] NULL,
 CONSTRAINT [PK_NT_USERLOG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_UserPointHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Content] [nvarchar](50) NULL,
	[Point] [int] NULL CONSTRAINT [DF_NT_UserPointHistory_Point]  DEFAULT ((0)),
	[GPoint] [int] NULL CONSTRAINT [DF_NT_UserPointHistory_GPoint]  DEFAULT ((0)),
	[Money] [float] NULL CONSTRAINT [DF_NT_UserPointHistory_Money]  DEFAULT ((0)),
	[UTF] [tinyint] NULL,
	[CreatTime] [datetime] NULL,
 CONSTRAINT [PK_NT_UserPointHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_UserSetting](
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_UserID]  DEFAULT ((0)),
	[PrivSpace] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivSpace]  DEFAULT ((0)),
	[PrivFavourite] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivFavourite]  DEFAULT ((0)),
	[PrivEducate] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivEducate]  DEFAULT ((0)),
	[PrivLasso] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivLasso]  DEFAULT ((0)),
	[PrivFriends] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivFriends]  DEFAULT ((0)),
	[PrivLeaveWord] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivLeaveWord]  DEFAULT ((0)),
	[PrivMiniBlog] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivMiniBlog]  DEFAULT ((0)),
	[PrivShare] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivShare]  DEFAULT ((0)),
	[PrivGroup] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivGroup]  DEFAULT ((0)),
	[PrivMovies] [int] NOT NULL CONSTRAINT [DF_NT_UserSetting_PrivMovies]  DEFAULT ((0)),
	[ActUpdateData] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActUpdateData]  DEFAULT ((1)),
	[ActAddFriend] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActAddFriend]  DEFAULT ((1)),
	[ActLeaveWord] [bit] NOT NULL CONSTRAINT [DF_NT_UserSetting_ActLeaveWord]  DEFAULT ((1)),
	[ActPicComment] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActPicComment]  DEFAULT ((1)),
	[ActSecede] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActSecede]  DEFAULT ((1)),
	[ActDeliver] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActDeliver]  DEFAULT ((1)),
	[ActLogComment] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActLogComment]  DEFAULT ((1)),
	[ActMovieComment] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActMovieComment]  DEFAULT ((1)),
	[ActPhotoLasso] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActPhotoLasso]  DEFAULT ((1)),
	[ActShareComment] [bit] NOT NULL CONSTRAINT [DF_NT_usersetting_ActShareComment]  DEFAULT ((1)),
	[LastPostTime] [datetime] NOT NULL,
	[LastPostIP] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_NT_USERSETTING] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Visit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[VisitorID] [int] NOT NULL,
	[LastVisitTime] [datetime] NOT NULL,
	[LastVisitIP] [nvarchar](15) NOT NULL,
	[VisitTimes] [int] NOT NULL CONSTRAINT [DF_NT_Visit_VisitTimes]  DEFAULT ((1)),
 CONSTRAINT [PK_NT_Visit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_Vote](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF_NT_Vote_UserID]  DEFAULT ((0)),
	[Title] [nvarchar](60) NULL,
	[Content] [ntext] NULL,
	[PostTime] [datetime] NULL,
	[Mode] [tinyint] NOT NULL CONSTRAINT [DF_NT_Vote_Mode]  DEFAULT ((0)),
	[EndTime] [datetime] NULL,
	[JCnt] [int] NOT NULL CONSTRAINT [DF_NT_Vote_JCnt]  DEFAULT ((0)),
	[VCnt] [int] NOT NULL CONSTRAINT [DF_NT_Vote_VCnt]  DEFAULT ((0)),
	[IsFriend] [tinyint] NOT NULL CONSTRAINT [DF_NT_Vote_IsFriend]  DEFAULT ((0)),
	[IsRec] [bit] NOT NULL CONSTRAINT [DF_NT_Vote_IsRec]  DEFAULT ((0)),
 CONSTRAINT [PK_NT_Vote] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
CREATE TABLE [NT_VoteDis](
	[ID] [int] NOT NULL,
	[UserID] [int] NULL,
	[VoteID] [int] NULL,
 CONSTRAINT [PK_NT_VoteDis] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_VoteOption](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoteID] [int] NOT NULL CONSTRAINT [DF_NT_VoteOption_VoteID]  DEFAULT ((0)),
	[OptionName] [nvarchar](100) NULL,
	[Cnt] [int] NOT NULL CONSTRAINT [DF_NT_VoteOption_Cnt]  DEFAULT ((0)),
 CONSTRAINT [PK_Nt_VoteOption] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE [NT_VoteTo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[VoteID] [int] NULL,
	[OptionID] [nvarchar](1000) NULL,
	[Content] [nvarchar](200) NULL CONSTRAINT [DF_NT_VoteTo_Content]  DEFAULT (''),
	[PostTime] [datetime] NULL,
 CONSTRAINT [PK_Nt_VoteTo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
;
ALTER TABLE [NT_Album]  WITH CHECK ADD  CONSTRAINT [FK_NT_Album_NT_User] FOREIGN KEY([UserID])
REFERENCES [NT_User] ([UserID])
;
ALTER TABLE [NT_Album] CHECK CONSTRAINT [FK_NT_Album_NT_User]
;
ALTER TABLE [NT_UserCareer]  WITH CHECK ADD  CONSTRAINT [FK_NT_UserCareer_NT_User] FOREIGN KEY([UserID])
REFERENCES [NT_User] ([UserID])
;
ALTER TABLE [NT_UserCareer] CHECK CONSTRAINT [FK_NT_UserCareer_NT_User]
;
ALTER TABLE [NT_UserEducation]  WITH CHECK ADD  CONSTRAINT [FK_NT_UserEducation_NT_User] FOREIGN KEY([UserID])
REFERENCES [NT_User] ([UserID])
;
ALTER TABLE [NT_UserEducation] CHECK CONSTRAINT [FK_NT_UserEducation_NT_User]

;
