CREATE PROCEDURE [JuSNS_Register]
@Email nvarchar(100),
@IP char(15),
@Password nchar(32),
@Username nvarchar(50),
@TrueName nvarchar(20),
@State int,
@ProvinceID int,
@City int,
@NowTime datetime,--Ϊͳһʱ��,�ô������
@VerifyCode char(11),
@EmailItem varchar(1000),---������ID,�Զ��ŷָ�
@PrivDef int,--�û���˽����Ĭ��ֵ
@Sex int,
@InviteID int,
@Birthday datetime,
@MobileCode int
 AS
DECLARE @R int
DECLARE @NewUid int
select @R=count(UserID) from NT_User where Email=@Email
IF @R >0 
RETURN 2
select @R=count(ID) from NT_SpareEmail where Email=@Email
IF @R > 0
RETURN 2
BEGIN TRAN
   Insert into NT_User ([Email],[Password],[Username],[TrueName],[sex],[State],[LoginTimes],[ProvinceID],[City],[RegTime],[RegIP],[VerifyCode],[MobileCode],[InviterID]) values (@Email,@Password,@Username,@TrueName,@Sex,@State,0,@ProvinceID,@City,@NowTime,@IP,@VerifyCode,@MobileCode,@InviteID)
   SELECT @NewUid=SCOPE_IDENTITY()
   Insert into NT_UserSetting (UserID,PrivSpace,PrivFavourite,PrivEducate,PrivLasso,PrivFriends,PrivLeaveWord,PrivMiniBlog,PrivShare,PrivGroup,PrivMovies,ActUpdateData,ActAddFriend,ActLeaveWord,ActPicComment,ActSecede,ActDeliver,ActLogComment,ActMovieComment,ActPhotoLasso,ActShareComment,LastPostTime,LastPostIP) values (@NewUid,@PrivDef,@PrivDef,@PrivDef,@PrivDef,@PrivDef,@PrivDef,@PrivDef,@PrivDef,@PrivDef,@PrivDef,1,1,1,1,1,1,1,1,1,1,@NowTime,@IP)
-------����Ϊ�����ʼ�֪ͨ
   insert into nt_userinfo(UserID,Birthday)values(@NewUid,@Birthday) 
	
--- EXEC NTP_EmailNotifyInit @EmailItem,@NewUid,@NowTime,@IP
IF @@error=0
BEGIN
  COMMIT TRAN
  RETURN 1
END
ELSE
BEGIN
  ROLLBACK TRAN
  RETURN 3
END