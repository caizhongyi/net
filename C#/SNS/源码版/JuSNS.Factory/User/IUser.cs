using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace JuSNS.Factory.User
{
    public interface IUser
    {
        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>true或Flase</returns>
        bool IsAdmin(object userid);
        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="cn">链接字符串</param>
        /// <returns>true或Flase</returns>
        bool IsAdmin(object userid, SqlConnection cn);
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="inputstr">传入的字符串</param>
        /// <param name="logintype"></param>
        /// <returns></returns>
        bool CheckUserExsit(string inputstr, int logintype);
        /// <summary>
        /// 根据用户检查用户是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool CheckUserExsit(object userid);
        /// <summary>
        /// 检查用户登录状态
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <param name="loginnum">返回登录次数</param>
        /// <returns></returns>
        EnumLoginState CheckUser(int userid, string password, out int loginnum);
        /// <summary>
        /// 得到用户实体类
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        UserInfo GetUserInfo(object userid);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        bool ChangePassword(int userid, string password);
        /// <summary>
        /// 修改居住地
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="homeprovince">省份</param>
        /// <param name="city"></param>
        /// <returns></returns>
        int ChangeNowCity(int userid, int homeprovince, int city);
        /// <summary>
        /// 检查电子邮件是否存在
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="email">电子邮件</param>
        /// <returns></returns>
        bool CheckEmail(int userid, string email);
        /// <summary>
        /// 更改电子邮件帐户
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="email">电子邮件</param>
        /// <returns></returns>
        int ChangeEmail(int userid, string email);
        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="basi"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        EnumRegister Register(UserInfo ui, UserBaseInfo basi, out int uid);
        /// <summary>
        /// 邮件激活
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ui"></param>
        /// <returns></returns>
        int EmailActive(string email, UserInfo ui);
        /// <summary>
        /// 判断2个用户之间是否是好友
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="friendid"></param>
        /// <returns></returns>
        bool IsFriends(object userid, object friendid);
        /// <summary>
        /// 注册时候插入默认用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int InserDefaultFriend(int userid);
        /// <summary>
        /// 插入好友
        /// </summary>
        /// <param name="info">FriendInfo实体类</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        int InsertFriend(FriendInfo info, int flag);
        /// <summary>
        /// 得到最大用户ID
        /// </summary>
        /// <returns></returns>
        int GetMaxUserID();
        /// <summary>
        /// 更新用户的积分，金币及帐户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="point"></param>
        /// <param name="flag"></param>
        /// <param name="ifoat"></param>
        /// <param name="content"></param>
        void UpdateInte(int userid, object point, int flag, int ifoat, string content);
       /// <summary>
       /// 插入附件
       /// </summary>
       /// <param name="userid"></param>
       /// <param name="infoid"></param>
       /// <param name="Type"></param>
       /// <returns></returns>
        int InsertFilesRecord(int userid, int infoid, int Type);
        /// <summary>
        /// 附件是否下载过
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="infoid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool isFilesDownload(int userid, int infoid, int type);
        /// <summary>
        /// 得到邀请注册
        /// </summary>
        /// <param name="userid"></param>
        void GetInvReg(int userid);
        /// <summary>
        /// 更改地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        int ChangeAddr(int userid, int province, int city);
        /// <summary>
        /// 更改MSN
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="msn"></param>
        /// <returns></returns>
        int ChangeMSN(int userid, string msn);
        /// <summary>
        /// 更改Google Talk
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="gtalk"></param>
        /// <returns></returns>
        int ChangeGTalk(int userid, string gtalk);
        /// <summary>
        /// 更改手机
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        int ChangeMobile(int userid, string mobile);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="inputstr"></param>
        /// <param name="password"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="truename"></param>
        /// <param name="loginnum"></param>
        /// <param name="logintype"></param>
        /// <returns></returns>
        EnumLoginState Login(string inputstr, string password, out int userid, out string username, out string truename, out int loginnum, int logintype);
        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="loginnum"></param>
        /// <returns></returns>
        EnumLoginState CheckLogin(int userid, string password, out int loginnum);
        /// <summary>
        /// 得到用户基础信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        UserBaseInfo GetUserBaseInfo(object userid);
        /// <summary>
        /// 得到用户扩展信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        UserSettingInfo GetUserSettingInfo(object userid);
        int InsertEducation(EducationInfo info, int flag);
        int DeleteEducation(object uid, object eid);
        EducationInfo GetEducationInfo(object eid);
        int UpdateLike(UserBaseInfo basi);
        int InsertCareer(CareerInfo info, int flag);
        int DeleteCareer(object uid, object cid);
        CareerInfo GetCareerInfo(object cid);
        int Retrieve(string email, int logintype);
        int ResetPwd(string newPwd, string code, out int userid, out string username,out string truename, out string userportrait);
        bool GetRestPwdRecord(string r, string email);
        PrivacyInfo GetPrivacy(int userid);
        int SetPrivacy(int userid, UserSettingInfo privacyinfo);
        string GetUserHeadPic(int userid, out int sex);
        int GetUserIDForEmail(string email);
        int UpdateUserHead(int pid, int userid);
        int UpdateUserInfo(UserInfo us, UserBaseInfo basi);
        int UpdateUserBasicInfo(UserInfo us, UserBaseInfo basi);
        int DeleteFriend(object fid, object uid);
        DataTable GetFriendClass(int userid);
        int InsertFriendClass(object cname, object uid, int fid);
        int DeleteFriendClass(object fid, object uid);
        int UpdateFriendClass(object fid, object cid, object uid);
        FriendInfo GetFriendInfo(object fid);
        string GetFriendList(object userid);
        DataTable GetUserFriendList(int number, int userid, int city, int sex);
        DataTable GetUserPossibleList(int number, int userid, string lastip);
        Dictionary<string, int> InviteFriends(int userid, string username, string[] emails, string desc);
        int GetFriendInvite(int userid, string email, string code);
        int ReplayInvite(int userid, int uid, string email);
        int DeleteVisite(int id, int userid);
        int InsertChargeOrder(ChargeOrderInfo info);
        MagicInfo GetMagicInfo(object mid);
        int BuyMagic(int userid, int mid, int num);
        int SendCommentReplay(int bid, int cid, int uid, string cont, string type);
        List<FriendInfo> GetFriendListTop(int number, int userid);
        int InsertGiftUser(GiftUserInfo info);
        List<GiftClassInfo> GetGiftClassList();
        GiftInfo GetGiftInfo(object gid);
        int InsertCalend(CalendInfo info);
        CalendInfo GetCalendInfo(int cid);
        int DeleteCalend(int cid, int uid);
        int InsertPoke(PokeInfo info);
        int DeletePoke(int pid, int uid);
        List<FavoriteClassInfo> GetFavorList(int userid);
        int deleteFavorite(int fid, int uid);
        int deleteFavoriteClass(int fid, int uid);
        FavoriteInfo GetFavorInfo(object fid);
        int InsertFavorite(FavoriteInfo info);
        int insertFavoriteClass(FavoriteClassInfo info);
        int UpdateMailState(int mid, int state);
        int DleteMailBox(int mid, int userid);
        int DleteMailSend(int mid, int userid);
        int SendMail(int userid, int reciveid, string title, string content);
        int InsertGbook(GBookInfo info);
        int DeleteGbook(int gid, int uid);
        List<UserInfo> RegisterUserNew(int number, int userid);
        int GetNote(int userid, int infoid, int flag);
        List<FriendInfo> GetFriendRequest(int userid);
        List<GroupInviteInfo> GetGroupRequest(int userid);
        int CheckFriend(int fid, int uid, int flag);
        int CheckGroup(int userid, int uid, int gid, int flag);
        void UpdateOnlineUser(OnlineUserInfo info);
        /// <summary>
        /// 得到用户在线数
        /// </summary>
        /// <param name="flag">0游客在线，1注册用户在线，2所有用户在线</param>
        /// <returns></returns>
        int GetOnlineCount(int flag);
        int InsertDyn(DynInfo info);
        int InsertNotice(NoticeInfo info);
        void UpdateNote(int userid);
        List<DynInfo> GetDynList(int number, int userid, string friendstr, string keys, string dyntype);
        string GetAtt(int userid);
        JoinVipInfo GetVipInfo(int userid);
        int JoinVip(int userid, string today, string joincontents);
        int UpdateVip(int userid, int flag);
        int DeleteAtt(int aid, int uid);
        int InsertATT(ATTInfo info);
        void UpdateUserState(int uid, int userid);
        SpaceTemplateInfo GetSpaceTemplate(int tid);
        int UpdateSpaceTemplate(int tid);
        int InsertSpaceTemplate(SpaceTemplateInfo info);
        int DeleteSpace(int tid, int uid);
        int DeleteUser(int userid, int uid);
        int DeleteFlash(int fid, int uid);
        int DeleteGift(int fid, int uid);
        GiftClassInfo GetGiftClassInfo(int cid);
        int DeleteGiftClass(int fid, int uid);
        int InsertGift(GiftInfo info);
        int InsertGiftClass(GiftClassInfo info);
        int DeleteUserAll(int uid);
        int UpdateATT(int bid, int uid);
        int UpdateChargeOrderState(int oid, int flag);
        int DeleteChargeOrder(int oid, int uid);
        /// <summary>
        /// 插入Flash幻灯片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int InsertFlash(FlashInfo info);
        FlashInfo GetFlashInfo(int fid);
        int ActivationEmail(int userid);
    }

    public sealed partial class DataAccess
    {
        public static IUser CreateUser()
        {
            string className = path + ".User.User";
            object objType = JuSNS.Common.DataCache.GetCache(className);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(className);
                    JuSNS.Common.DataCache.SetCache(className, objType);// 写入缓存
                }
                catch { }
            }
            return (IUser)objType;
        }
    }

}
