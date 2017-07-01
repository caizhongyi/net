using System.Collections.Generic;
using JuSNS.Factory.User;
using JuSNS.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace JuSNS.Home.User
{
    public class User
    {
        static readonly private User _instance = new User();
        JuSNS.Factory.User.IUser dal;
        private User()
        {
            dal = DataAccess.CreateUser();
        }
       

        /// <summary>
        /// 取得实例
        /// </summary>
        static public User Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool IsAdmin(object userid)
        {
            return dal.IsAdmin(userid);
        }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool IsAdmin(object userid,SqlConnection cn)
        {
            return dal.IsAdmin(userid, cn);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="inputstr">用户名/或电子邮件</param>
        /// <param name="logintype">0电子邮件，1用户名，2手机</param>
        public bool CheckUserExsit(string inputstr, int logintype)
        {
            return dal.CheckUserExsit(inputstr, logintype);
        }

        /// <summary>
        /// 根据用户ID检查用户是否在系统中存在
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>返回True或False</returns>
        public bool CheckUserExsit(object userid)
        {
            return dal.CheckUserExsit(userid);
        }

        /// <summary>
        /// 检查用户合法性
        /// </summary>
        /// <param name="userid">ID</param>
        /// <param name="password">已MD5加密的密码</param>
        /// <returns></returns>
        public EnumLoginState CheckUser(int userid, string password)
        {
            int loginNum = 0;
            return CheckUser(userid, password, out loginNum);
        }

        /// <summary>
        /// 检查用户合法性
        /// </summary>
        /// <param name="userid">ID</param>
        /// <param name="password">已MD5加密的密码</param>
        /// <param name="loginNum">返回得登录次数</param>
        /// <returns></returns>
        public EnumLoginState CheckUser(int userid, string password, out int loginnum)
        {
            return dal.CheckUser(userid, password, out loginnum);
        }


        /// <summary>
        /// 得到用户基本信息，(表NT_User)
        /// </summary>
        /// <param name="userid">传入的用户ID</param>
        /// <returns>返回用户实例</returns>
        public UserInfo GetUserInfo(object userid)
        {
            return dal.GetUserInfo(userid);
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="password">新密码(加密后)</param>
        /// <returns>返回成功或失败</returns>
        public bool ChangePassword(int userid, string password)
        {
            return dal.ChangePassword(userid, password);
        }

        /// <summary>
        /// 修改居住地
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="homeprovince">省份ID</param>
        /// <param name="city">城市ID</param>
        /// <returns>0失败，1成功</returns>
        public int ChangeNowCity(int userid, int homeprovince, int city)
        {
            return dal.ChangeNowCity(userid, homeprovince, city);
        }

        /// <summary>
        /// 检查邮箱是否被占用
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool CheckEmail(int userid, string Email)
        {
            return dal.CheckEmail(userid, Email);
        }

        /// <summary>
        /// 修改Email
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="Email">新Email</param>
        /// <returns>0修改成功，1你修改过了，还未激活,2修改成功，但未发送电子邮件。</returns>
        public int ChangeEmail(int userid, string Email)
        {
            return dal.ChangeEmail(userid, Email);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="ui">用户基础实体类</param>
        /// <param name="basi">用户扩展实体类</param>
        /// <param name="uid">返回的userid</param>
        /// <returns>返回EnumRegister结果</returns>
        public EnumRegister Register(UserInfo ui,UserBaseInfo basi, out int uid)
        {
            return dal.Register(ui, basi, out uid);
        }

        /// <summary>
        /// 激活电子邮件
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ui"></param>
        /// <returns>0成功，1邮件发送失败，不需要激活</returns>
        public int EmailActive(string email, UserInfo ui)
        {
            return dal.EmailActive(email, ui);
        }

        /// <summary>
        /// 是否是好友。
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="friendid"></param>
        /// <returns></returns>
        public bool IsFriends(object userid, object friendid)
        {
            return dal.IsFriends(userid, friendid);
        }

        /// <summary>
        /// 插入默认好友
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int InserDefaultFriend(int userid)
        {
            return dal.InserDefaultFriend(userid);
        }
        
        /// <summary>
        /// 插入好友
        /// </summary>
        /// <param name="Info">实例</param>
        /// <param name="flag">大于0表示直接为好友，不需要审核</param>
        /// <returns></returns>
        public int InsertFriend(FriendInfo Info,int flag)
        {
            return dal.InsertFriend(Info, flag);
        }

        /// <summary>
        /// 增加积分或者金币
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="point">增加数量单位</param>
        /// <param name="flag">0增加积分，1增加金币，2帐户资金</param>
        /// <param name="ifoat">0增加，1减少</param>
        /// <param name="content">描述</param>
        public void UpdateInte(int userid, object point, int flag, int ifoat, string content)
        {
            dal.UpdateInte(userid, point, flag, ifoat, content);
        }

        /// <summary>
        /// 插入资源阅读记录
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="infoid">操作的ID</param>
        /// <param name="type">0，资讯，1其他</param>
        /// <returns></returns>
        public int InsertFilesRecord(int userid, int infoid, int type)
        {
            return dal.InsertFilesRecord(userid, infoid, type);
        }

        /// <summary>
        /// 得到是否已经下载过资源
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="infoid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool isFilesDownload(int userid, int infoid, int type)
        {
            return dal.isFilesDownload(userid, infoid, type);
        }

        /// <summary>
        /// 为邀请注册者增加积分
        /// </summary>
        /// <param name="userid"></param>
        public void GetInvReg(int userid)
        {
            dal.GetInvReg(userid);
        }

        /// <summary>
        /// 取得最大的userid
        /// </summary>
        /// <returns></returns>
        public int GetMaxuserid()
        {
            return dal.GetMaxUserID();
        }

        /// <summary>
        /// 修改所在地
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <returns></returns>
        public int ChangeAddr(int userid, int province, int city)
        {
            return dal.ChangeAddr(userid, province, city);
        }

        /// <summary>
        /// 修改MSN
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="MSN"></param>
        /// <returns></returns>
        public int ChangeMSN(int userid, string msn)
        {
            return dal.ChangeMSN(userid, msn);
        }

        /// <summary>
        /// 修改GTalk
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="gtalk"></param>
        /// <returns></returns>
        public int ChangeGTalk(int userid, string gtalk)
        {
            return dal.ChangeGTalk(userid, gtalk);
        }

        /// <summary>
        /// 修改手机
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int ChangeMobile(int userid, string mobile)
        {
            return dal.ChangeMobile(userid, mobile);
        }

        /// <summary>
        /// 开始登录
        /// </summary>
        /// <param name="inputstr">需要验证的字段</param>
        /// <param name="password">密码</param>
        /// <param name="userid">返回的userid</param>
        /// <param name="username">用户名</param>
        /// <param name="truename">真实姓名</param>
        /// <param name="loginnum">登录次数</param>
        /// <param name="logintype">0电子邮件，1用户名，2手机</param>
        /// <returns></returns>
        public EnumLoginState Login(string inputstr, string password, out int userid, out string username, out string truename, out int loginnum, int logintype)
        {
            return dal.Login(inputstr, password, out userid, out username, out truename, out loginnum, logintype);
        }

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="loginnum">登录次数</param>
        /// <returns>EnumloginState实例</returns>
        public EnumLoginState CheckLogin(int userid, string password, out int loginnum)
        {
            return dal.CheckLogin(userid, password, out loginnum);
        }

        /// <summary>
        /// 获得用户实例（表NT_userInfo）
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserBaseInfo GetUserBaseInfo(object userid)
        {
            return dal.GetUserBaseInfo(userid);
        }

        /// <summary>
        /// 得到用户隐私设置（表NT_UserSetting）
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserSettingInfo GetUserSettingInfo(object userid)
        {
            return dal.GetUserSettingInfo(userid);
        }

        /// <summary>
        /// 设置隐私信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="privacyinfo">隐私信息</param>
        /// <returns></returns>
        public int SetPrivacy(int userid, UserSettingInfo privacyinfo)
        {
            return dal.SetPrivacy(userid, privacyinfo);
        }

        /// <summary>
        /// 插入教育信息
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="flag">0增加，1修改</param>
        /// <returns></returns>
        public int InsertEducation(EducationInfo info,int flag)
        {
            return dal.InsertEducation(info, flag);
        }
        
        /// <summary>
        /// 删除教育信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public int DeleteEducation(object uid, object eid)
        {
            return dal.DeleteEducation(uid, eid);
        }

        /// <summary>
        /// 得到教育信息
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public EducationInfo GetEducationInfo(object eid)
        {
            return dal.GetEducationInfo(eid);
        }

        /// <summary>
        /// 更新爱好信息
        /// </summary>
        /// <param name="basi"></param>
        /// <returns></returns>
        public int UpdateLike(UserBaseInfo basi)
        {
            return dal.UpdateLike(basi);
        }

        /// <summary>
        /// 插入工作信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="flag">0增加，1修改</param>
        /// <returns></returns>
        public int InsertCareer(CareerInfo info, int flag)
        {
            return dal.InsertCareer(info, flag);
        }

        /// <summary>
        /// 删除工作信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int DeleteCareer(object uid, object cid)
        {
            return dal.DeleteCareer(uid, cid);
        }

        /// <summary>
        /// 得到工作信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public CareerInfo GetCareerInfo(object cid)
        {
            return dal.GetCareerInfo(cid);
        }


        /// <summary>
        /// 取回密码
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="logintype">0电子邮件，1手机</param>
        /// <returns>0,成功,-2,邮箱无效,-3邮件发送失败</returns>
        public int Retrieve(string email, int logintype)
        {
            return dal.Retrieve(email, logintype);
        }

         /// <summary>
       /// 重设密码,并设为已录录状态
       /// </summary>
       /// <param name="newPwd">加密过的新密码</param>
       /// <param name="code"></param>
       /// <returns>0成功，-1没有找到相关的申请记录,-2该申请已完成重设而无效,-3邮箱无效</returns>
        public int ResetPwd(string newpwd, string code, out int userid, out string username,out string truename, out string userportrait)
        {
            return dal.ResetPwd(newpwd, code, out userid, out username, out truename, out userportrait);
        }

        /// <summary>
        /// 重置密码验证是否已经有记录
        /// </summary>
        /// <param name="r"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool GetRestPwdRecord(string r, string email)
        {
            return dal.GetRestPwdRecord(r, email);
        }

        /// <summary>
        /// 取得隐私信息
        /// </summary>
        /// <param name="userid">用户ID信息</param>
        /// <returns></returns>
        public PrivacyInfo GetPrivacy(int userid)
        {
            return dal.GetPrivacy(userid);
        }

        /// <summary>
        /// 取得头像名称
        /// </summary>
        public string GetUserHeadPic(int userid,out int sex)
        {
            return dal.GetUserHeadPic(userid, out sex);
        }

        /// <summary>
        /// 根据电子邮件获得UserID
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetUserIDForEmail(string email)
        {
            return dal.GetUserIDForEmail(email);
        }

        /// <summary>
        /// 更新头像。
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UpdateUserHead(int pid,int userid)
        {
            return dal.UpdateUserHead(pid, userid);
        }

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <param name="us"></param>
        /// <param name="basi"></param>
        /// <returns></returns>
        public int UpdateUserInfo(UserInfo us, UserBaseInfo basi)
        {
            return dal.UpdateUserInfo(us, basi);
        }

        /// <summary>
        /// 更新基本信息Base
        /// </summary>
        /// <param name="us"></param>
        /// <param name="basi"></param>
        /// <returns></returns>
        public int UpdateUserBasicInfo(UserInfo us, UserBaseInfo basi)
        {
            return dal.UpdateUserBasicInfo(us, basi);
        }

        /// <summary>
        /// 断开好友关系
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteFriend(object fid, object uid)
        {
            return dal.DeleteFriend(fid, uid);
        }

        /// <summary>
        /// 得到好友分类
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetFriendClass(int userid)
        {
            return dal.GetFriendClass(userid);
        }

        /// <summary>
        /// 插入/更新好有分类
        /// </summary>
        /// <param name="cname"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public int InsertFriendClass(object cname, object uid, int fid)
        {
            return dal.InsertFriendClass(cname, uid, fid);
        }

        /// <summary>
        /// 删除好友分类
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteFriendClass(object fid, object uid)
        {
            return dal.DeleteFriendClass(fid, uid);
        }

        /// <summary>
        /// 更新好友的分组
        /// </summary>
        /// <param name="fid">好友ID</param>
        /// <param name="cid">分类ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateFriendClass(object fid, object cid,object uid)
        {
            return dal.UpdateFriendClass(fid, cid, uid);
        }

        /// <summary>
        /// 获得某个好友的实体类
        /// </summary>
        /// <param name="fid">FID好友ID</param>
        /// <returns>得到实体类</returns>
        public FriendInfo GetFriendInfo(object fid)
        {
            return dal.GetFriendInfo(fid);
        }

        /// <summary>
        /// 得到我的好友的ID字符串
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>字符串：如：0,2,3,2</returns>
        public string GetFriendList(object userid)
        {
            return dal.GetFriendList(userid);
        }

        /// <summary>
        /// 得到同城好友
        /// </summary>
        /// <param name="userid">用户</param>
        /// <param name="city">城市ID</param>
        /// <param name="sex">性别(0男，1女)</param>
        /// <returns></returns>
        public DataTable GetUserFriendList(int number, int userid, int city, int sex)
        {
            int gSEX = -1;
            if (sex == 0) gSEX = 1;
            if (sex == 1) gSEX = 0;
            return dal.GetUserFriendList(number, userid, city, gSEX);
        }

        /// <summary>
        /// 得到可能认识的朋友
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="userid">当前用户ID</param>
        /// <param name="lastip">最后登录IP</param>
        /// <returns></returns>
        public DataTable GetUserPossibleList(int number,int userid,string lastip)
        {
            return dal.GetUserPossibleList(number, userid, lastip);
        }

        /// <summary>
        /// 邀请用户注册
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="userName">用户名或真实姓名</param>
        /// <param name="emails">要发送的电子邮件</param>
        /// <param name="desc">描述</param>
        /// <returns>0成功,1该邮箱已经注册,2该邮箱已被邀请,3写入数据库失败,4未发送成功</returns>
        public Dictionary<string, int> InviteFriends(int userid, string username, string emails, string desc)
        {
            string[] email = Regex.Split(emails, @"\r\n|,");
            return dal.InviteFriends(userid, username, email, desc);
        }

        /// <summary>
        /// 验证邀请注册是否有效
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="email">电子邮件</param>
        /// <param name="code">验证字符串</param>
        /// <returns>0正确，1无效的参数，2已经验证过了。</returns>
        public int GetFriendInvite(int userid, string email, string code)
        {
            return dal.GetFriendInvite(userid, email, code);
        }

        /// <summary>
        /// 回应邀请请求
        /// </summary>
        /// <param name="userid">邀请者</param>
        /// <param name="uid">注册者</param>
        /// <param name="email">注册者邮箱</param>
        /// <returns>0失败，1成功</returns>
        public int ReplayInvite(int userid, int uid, string email)
        {
            return dal.ReplayInvite(userid, uid, email);
        }

        /// <summary>
        /// 删除访问者或被访问者
        /// </summary>
        /// <param name="id">访问者的自动编号ID</param>
        /// <param name="userid">删除的用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteVisite(int id, int userid)
        {
            return dal.DeleteVisite(id, userid);
        }

        /// <summary>
        /// 插入充值订单号
        /// </summary>
        /// <param name="Info">订单实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertChargeOrder(ChargeOrderInfo info)
        {
            return dal.InsertChargeOrder(info);
        }

        /// <summary>
        /// 得到道具实体类
        /// </summary>
        /// <param name="mid">道具ID</param>
        /// <returns>实体类</returns>
        public MagicInfo GetMagicInfo(object mid)
        {
            return dal.GetMagicInfo(mid);
        }

        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="mid">道具ID</param>
        /// <param name="Num">购买数量</param>
        /// <returns>0购买成功，1积分不够，2金币不够，3库存不够</returns>
        public int BuyMagic(int userid, int mid, int num)
        {
            return dal.BuyMagic(userid, mid, num);
        }

        /// <summary>
        /// 保存评论回复。
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="cid"></param>
        /// <param name="uid"></param>
        /// <param name="cont"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int SendCommentReplay(int bid, int cid, int uid, string cont, string type)
        {
            return dal.SendCommentReplay(bid,cid, uid, cont, type);
        }

        /// <summary>
        /// 得到好友列表
        /// </summary>
        /// <param name="number">得到数量</param>
        /// <param name="userid">用户</param>
        /// <returns>得到List实体类</returns>
        public List<FriendInfo> GetFriendListTop(int number, int userid)
        {
            return dal.GetFriendListTop(number, userid);
        }

        /// <summary>
        /// 赠送礼物
        /// </summary>
        /// <param name="info">GiftUserInfo实体类</param>
        /// <returns>0失败，1成功，-1赠送已经达到今日最大赠送礼物数量,-2积分或金币不够</returns>
        public int InsertGiftUser(GiftUserInfo info)
        {
            return dal.InsertGiftUser(info);
        }

       /// <summary>
        /// 得到礼物类型
       /// </summary>
       /// <param name="gid">礼物ID</param>
        /// <returns>得到List实体类</returns>
        public GiftInfo GetGiftInfo(object gid)
        {
            return dal.GetGiftInfo(gid);
        }
        /// <summary>
        /// 得到礼物分类
        /// </summary>
        /// <returns>得到List实体类</returns>
        public List<GiftClassInfo> GetGiftClassList()
        {
            return dal.GetGiftClassList();
        }

        /// <summary>
        /// 插入日历选项
        /// </summary>
        /// <param name="info">CalendInfo实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertCalend(CalendInfo info)
        {
            return dal.InsertCalend(info);
        }
        /// <summary>
        /// 得到日历具体信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public CalendInfo GetCalendInfo(int cid)
        {
            return dal.GetCalendInfo(cid);
        }

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteCalend(int cid, int uid)
        {
            return dal.DeleteCalend(cid, uid);
        }

        /// <summary>
        /// 插入招呼
        /// </summary>
        /// <param name="info">招呼实体类</param>
        /// <returns>0失败，1成功，-1达到每日上限</returns>
        public int InsertPoke(PokeInfo info)
        {
            return dal.InsertPoke(info);
        }

        /// <summary>
        /// 删除招呼
        /// </summary>
        /// <param name="pid">招呼ID</param>
        /// <param name="uid">操作ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeletePoke(int pid, int uid)
        {
            return dal.DeletePoke(pid, uid);
        }

        /// <summary>
        /// 得到收藏分类
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>FavoriteClassInfo实体类</returns>
        public List<FavoriteClassInfo> GetFavorList(int userid)
        {
            return dal.GetFavorList(userid);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int deleteFavorite(int fid, int uid)
        {
            return dal.deleteFavorite(fid, uid);
        }
        /// <summary>
        /// 删除收藏分类
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int deleteFavoriteClass(int fid, int uid)
        {
            return dal.deleteFavoriteClass(fid, uid);
        }

        /// <summary>
        /// 得到收藏夹
        /// </summary>
        /// <param name="fid">收藏夹ID</param>
        /// <returns></returns>
        public FavoriteInfo GetFavorInfo(object fid)
        {
            return dal.GetFavorInfo(fid);
        }

        /// <summary>
        /// 插入收藏
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertFavorite(FavoriteInfo info)
        {
            return dal.InsertFavorite(info);
        }

        /// <summary>
        /// 插入收藏分类名称
        /// </summary>
        /// <param name="info">FavoriteClassInfo实体类</param>
        /// <returns>1成功，0失败</returns>
        public int insertFavoriteClass(FavoriteClassInfo info)
        {
            return dal.insertFavoriteClass(info);
        }

        /// <summary>
        /// 更新邮件状态
        /// </summary>
        /// <param name="mid">邮件ID</param>
        /// <param name="state">状态</param>
        /// <returns>0成功，1失败</returns>
        public int UpdateMailState(int mid, int state)
        {
            return dal.UpdateMailState(mid, state);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mid">邮件ID</param>
        /// <param name="uid">操作用户</param>
        /// <returns>0成功，1失败</returns>
        public int DleteMailBox(int mid, int userid)
        {
            return dal.DleteMailBox(mid, userid);
        }
        /// <summary>
        /// 删除发件箱
        /// </summary>
        /// <param name="mid">邮件ID</param>
        /// <param name="uid">操作用户</param>
        /// <returns>0成功，1失败</returns>
        public int DleteMailSend(int mid, int userid)
        {
            return dal.DleteMailSend(mid, userid);
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="reciveid">接受用户ID</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns>0失败，否则成功</returns>
        public int SendMail(int userid, int reciveid, string title, string content)
        {
            return dal.SendMail(userid, reciveid, title, content);
        }

        /// <summary>
        /// 插入留言
        /// </summary>
        /// <param name="info">GBookInfo实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertGbook(GBookInfo info)
        {
            return dal.InsertGbook(info);
        }

        /// <summary>
        /// 删除留言
        /// </summary>
        /// <param name="info">留言ID</param>
        /// <param name="uid">操作者</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteGbook(int gid,int uid)
        {
            return dal.DeleteGbook(gid, uid);
        }

        /// <summary>
        /// 刚注册的朋友
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<UserInfo> RegisterUserNew(int number,int userid)
        {
            return dal.RegisterUserNew(number,userid);
        }

        /// <summary>
        /// 得到提醒信息
        /// </summary>
        /// <param name="userid">当前用户</param>
        /// <param name="infoid">相关ID</param>
        /// <param name="flag">0好友请求，1群组请求，2通知信息，3短信，4提醒</param>
        /// <returns>返回数量</returns>
        public int GetNote(int userid, int infoid, int flag)
        {
            return dal.GetNote(userid, infoid, flag);
        }

        /// <summary>
        /// 得到需要审核的好友！
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>List列表实体</returns>
        public List<FriendInfo> GetFriendRequest(int userid)
        {
            return dal.GetFriendRequest(userid);
        }

        /// <summary>
        /// 得到受邀请的群组！
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>List列表实体</returns>
        public List<GroupInviteInfo> GetGroupRequest(int userid)
        {
            return dal.GetGroupRequest(userid);
        }

        /// <summary>
        /// 通过或拒绝好友请求
        /// </summary>
        /// <param name="fid">好友ID</param>
        /// <param name="uid">操作者ID</param>
        /// <param name="flag">0通过，1拒绝</param>
        /// <returns>0失败，1成功</returns>
        public int CheckFriend(int fid,int uid,int flag)
        {
            return dal.CheckFriend(fid, uid, flag);
        }

        /// <summary>
        /// 通过或拒绝加入群
        /// </summary>
        /// <param name="userid">邀请者</param>
        /// <param name="uid">被邀请者</param>
        /// <param name="gid">群组ID</param>
        /// <param name="flag">0通过，1拒绝</param>
        /// <returns>0失败，-1已经加入过了，1操作成功</returns>
        public int CheckGroup(int userid, int uid, int gid, int flag)
        {
            return dal.CheckGroup(userid, uid, gid, flag);
        }

        /// <summary>
        /// 更新在线人数
        /// </summary>
        /// <param name="info">OnlineUserInfo实体类</param>
        public void UpdateOnlineUser(OnlineUserInfo info)
        {
            dal.UpdateOnlineUser(info);
        }

        /// <summary>
        /// 得到用户在线数
        /// </summary>
        /// <param name="flag">0游客在线，1注册用户在线，2所有用户在线</param>
        /// <returns></returns>
        public int GetOnlineCount(int flag)
        {
            return dal.GetOnlineCount(flag);
        }

        /// <summary>
        /// 插入动态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertDyn(DynInfo info)
        {
           return dal.InsertDyn(info);
        }

        /// <summary>
        /// 插入通知
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertNotice(NoticeInfo info)
        {
            return dal.InsertNotice(info);
        }

        /// <summary>
        /// 更新所有通知信息为已读
        /// </summary>
        /// <param name="userid">用户ID</param>
        public void UpdateNote(int userid)
        {
            dal.UpdateNote(userid);
        }

        /// <summary>
        /// 得到动态列表(会员中心)
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="userid">用户</param>
        /// <param name="friendstr">好友列表</param>
        /// <param name="keys">屏蔽的用户列表</param>
        /// <param name="dyntype">获得类型</param>
        /// <returns>List列表</returns>
        public List<DynInfo> GetDynList(int number,int userid,string friendstr, string keys,string dyntype)
        {
            return dal.GetDynList(number, userid, friendstr, keys, dyntype);
        }

        /// <summary>
        /// 得到我关注的人
        /// </summary>
        /// <param name="userid">关注这UserID</param>
        /// <returns>我关注的人的列表</returns>
        public string GetAtt(int userid)
        {
            return dal.GetAtt(userid);
        }

        /// <summary>
        /// 得到VIP信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>JoinVipInfo实体类</returns>
        public JoinVipInfo GetVipInfo(int userid)
        {
            return dal.GetVipInfo(userid);
        }

        /// <summary>
        /// 加入VIP
        /// </summary>
        /// <param name="userid">加入者</param>
        /// <param name="today">到期时间</param>
        /// <param name="joincontents">加入理由</param>
        /// <returns>0失败，1成功，-1已经申请过了</returns>
        public int JoinVip(int userid, string today, string joincontents)
        {
            return dal.JoinVip(userid, today, joincontents);
        }

        /// <summary>
        /// 更新VIP会员状态
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="flag">0取消VIP会员资格，增加VIP会员资格</param>
        /// <returns></returns>
        public int UpdateVip(int userid, int flag)
        {
            return dal.UpdateVip(userid, flag);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="aid">关注ID</param>
        /// <param name="uid">操作者</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteAtt(int aid, int uid)
        {
            return dal.DeleteAtt(aid, uid);
        }

        public void UpdateUserState(int uid,int userid)
        {
            dal.UpdateUserState(uid,userid);
        }

        /// <summary>
        /// 插入关注
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0失败，-1关注过了，1成功</returns>
        public int InsertATT(ATTInfo info)
        {
            return dal.InsertATT(info);
        }

        /// <summary>
        /// 得到空间模板（指定ID）
        /// </summary>
        /// <param name="tid">空间模板</param>
        /// <returns>SpaceTemplateInfo实体类</returns>
        public SpaceTemplateInfo GetSpaceTemplate(int tid)
        {
            return dal.GetSpaceTemplate(tid);
        }

        /// <summary>
        /// 更新模板使用次数
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int UpdateSpaceTemplate(int tid)
        {
            return dal.UpdateSpaceTemplate(tid);
        }

        /// <summary>
        /// 插入或更新模板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertSpaceTemplate(SpaceTemplateInfo info)
        {
            return dal.InsertSpaceTemplate(info);
        }

        /// <summary>
        /// 删除空间模板
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteSpace(int tid, int uid)
        {
            return dal.DeleteSpace(tid, uid);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid">被删除者</param>
        /// <param name="uid">删除者</param>
        /// <returns></returns>
        public int DeleteUser(int userid, int uid)
        {
            return dal.DeleteUser(userid, uid);
        }

        /// <summary>
        /// 删除首页Flash幻灯片
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteFlash(int fid, int uid)
        {
            return dal.DeleteFlash(fid, uid);
        }
        /// <summary>
        /// 删除礼物
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteGift(int fid, int uid)
        {
            return dal.DeleteGift(fid, uid);
        }

        /// <summary>
        /// 得到礼物分类
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public GiftClassInfo GetGiftClassInfo(int cid)
        {
            return dal.GetGiftClassInfo(cid);
        }

        /// <summary>
        /// 删除礼物分类
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteGiftClass(int fid, int uid)
        {
            return dal.DeleteGiftClass(fid, uid);
        }

        /// <summary>
        /// 插入礼物或更新礼物
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGift(GiftInfo info)
        {
            return dal.InsertGift(info);
        }

        /// <summary>
        /// 插入礼物或更新礼物分类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGiftClass(GiftClassInfo info)
        {
            return dal.InsertGiftClass(info);
        }

        /// <summary>
        /// 删除所有会员
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteUserAll(int uid)
        {
            return dal.DeleteUserAll(uid);
        }

        /// <summary>
        /// 更新关注
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int UpdateATT(int bid, int uid)
        {
            return dal.UpdateATT(bid, uid);
        }

        /// <summary>
        /// 更改充值订单的状态
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateChargeOrderState(int oid, int flag)
        {
            return dal.UpdateChargeOrderState(oid, flag);
        }

        /// <summary>
        /// 删除充值订单
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteChargeOrder(int oid, int uid)
        {
            return dal.DeleteChargeOrder(oid, uid);
        }

        /// <summary>
        /// 插入Flash幻灯片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertFlash(FlashInfo info)
        {
            return dal.InsertFlash(info);
        }

        /// <summary>
        /// 得到FLASH幻灯片信息
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public FlashInfo GetFlashInfo(int fid)
        {
            return dal.GetFlashInfo(fid);
        }

        /// <summary>
        /// 激活电子邮件
        /// </summary>
        /// <param name="userid">激活用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int ActivationEmail(int userid)
        {
            return dal.ActivationEmail(userid);
        }

        public bool CheckPrivacy(int Privacy, int userid, int uid)
        {
            bool Privacyflag = false;
            switch (Privacy)
            {
                case 0://全站用户可见
                    Privacyflag = true;
                    break;
                case 1://仅好友可见
                    bool isFriend = IsFriends(uid, userid);
                    if (isFriend || userid == uid)
                    {
                        Privacyflag = true;
                    }
                    break;
                case 2://仅自己可见
                    if (uid == userid)
                    {
                        Privacyflag = true;
                    }
                    break;
            }
            return Privacyflag;
        }
    }

}