//===============================================
//　　　　　　　　　　\\\|///                      
//　　　　　　　　　　\\　- -　//                   
//　　　　　　　　　　  ( @ @ )                    
//┏━━━━━━━━━oOOo-(_)-oOOo━━━┓          
//┃                                     ┃
//┃             东 网 原 创！           ┃
//┃      lenlong 作品，请保留此信息！   ┃
//┃      ** lenlenlong@hotmail.com **   ┃
//┃                                     ┃
//┃　　　　　　　　　　　　　Dooo　     ┃
//┗━━━━━━━━━ oooD━-(　 )━━━┛
//　　　　　　　　　　 (  )　  ) /
//　　　　　　　　　　　\ (　 (_/
//　　　　　　　　　　　 \_)
//===============================================
using System;
using System.Data;
using System.Collections;
namespace Components.Components
{
    public class User
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }
        public void SetDataProviders(DataRow rs)
        {
            UserID = int.Parse(rs["UserID"].ToString());
            UserName = rs["UserName"].ToString();
            UserNickName = rs["UserNickName"].ToString();
            UserPassWord = rs["UserPassWord"].ToString();
            UserPassWordAnswer = rs["UserPassWordAnswer"].ToString();
            UserEmail = rs["UserEmail"].ToString();
            UserRecommendUserID = int.Parse(rs["UserRecommendUserID"].ToString());
            UserSex = rs["UserSex"].ToString();
            UserBirthday = System.Convert.ToDateTime(rs["UserBirthday"].ToString());
            UserTrueName = rs["UserTrueName"].ToString();
            UserComeFrom = rs["UserComeFrom"].ToString();
            UserContactAddress = rs["UserContactAddress"].ToString();
            UserCode = rs["UserCode"].ToString();
            UserContactTel = rs["UserContactTel"].ToString();
            UserMobile = rs["UserMobile"].ToString();
            UserOICQ = rs["UserOICQ"].ToString();
            UserIdCard = rs["UserIdCard"].ToString();
            UserMaritalStatus = int.Parse(rs["UserMaritalStatus"].ToString());
            UserWorkUnit = rs["UserWorkUnit"].ToString();
            UserSchool = rs["UserSchool"].ToString();
            UserSign = rs["UserSign"].ToString();
            UserAbout = rs["UserAbout"].ToString();
            UserFace = rs["UserFace"].ToString();
            UserLastIP = rs["UserLastIP"].ToString();
            UserPrivacy = int.Parse(rs["UserPrivacy"].ToString());
            UserRegTime = System.Convert.ToDateTime(rs["UserRegTime"].ToString());
            UserLastLoginTime = System.Convert.ToDateTime(rs["UserLastLoginTime"].ToString());
            UserLoginNumber = int.Parse(rs["UserLoginNumber"].ToString());
            UserOnlineTime = int.Parse(rs["UserOnlineTime"].ToString());
            UserLastActTime = System.Convert.ToDateTime(rs["UserLastActTime"].ToString());
            UserPoint = int.Parse(rs["UserPoint"].ToString());
            UserPrestige = int.Parse(rs["UserPrestige"].ToString());
            UserRmb = int.Parse(rs["UserRmb"].ToString());
            UserTicket = int.Parse(rs["UserTicket"].ToString());
            UserMoney = int.Parse(rs["UserMoney"].ToString());
            UserExp = int.Parse(rs["UserExp"].ToString());
            UserCP = int.Parse(rs["UserCP"].ToString());
            UserOnLineStatic = rs["UserOnLineStatic"].ToString();
            UserGroup = rs["UserGroupID"].ToString();
            if (rs["UserLevelID"].ToString() != string.Empty)
            {
                UserLevelID = int.Parse(rs["UserLevelID"].ToString());
            }
            UserRole = rs["UserRole"].ToString();
            UserPostNumber = int.Parse(rs["UserPostNumber"].ToString());
            UserTopicNumber = int.Parse(rs["UserTopicNumber"].ToString());
            UserReTopicNumber = int.Parse(rs["UserReTopicNumber"].ToString());
            UserDelTopicNumber = int.Parse(rs["UserDelTopicNumber"].ToString());
            UserWebAddress = rs["UserWebAddress"].ToString();
            UserWebLog = rs["UserWebLog"].ToString();
            UserWebGallery = rs["UserWebGallery"].ToString();
            UserInterests = rs["UserInterests"].ToString();
            UserTrueMoney = double.Parse(rs["UserTrueMoney"].ToString());
            UserFalse = int.Parse(rs["UserFalse"].ToString());
            UserReceiveType = int.Parse(rs["UserReceiveType"].ToString());
        }//

        private int _UserID;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int _UserFalse;
        /// <summary>
        /// 用户是否被禁闭
        /// </summary>
        public int UserFalse
        {
            get { return _UserFalse; }
            set { _UserFalse = value; }
        }
        private string _UserName;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _UserNickName;
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName
        {
            get { return _UserNickName; }
            set { _UserNickName = value; }
        }
        private string _UserPassWord;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassWord
        {
            get { return _UserPassWord; }
            set { _UserPassWord = value; }
        }
        private string _UserPassWordAnswer;
        /// <summary>
        /// 密码答案
        /// </summary>
        public string UserPassWordAnswer
        {
            get { return _UserPassWordAnswer; }
            set { _UserPassWordAnswer = value; }
        }
        private string _UserEmail;
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }
        private int _UserRecommendUserID;
        /// <summary>
        /// 推荐用户的ID
        /// </summary>
        public int UserRecommendUserID
        {
            get { return _UserRecommendUserID; }
            set { _UserRecommendUserID = value; }
        }
        private string _UserSex;
        /// <summary>
        /// 用户性别,M男，F女，N，保密
        /// </summary>
        public string UserSex
        {
            get { return _UserSex; }
            set { _UserSex = value; }
        }
        private DateTime _UserBirthday;
        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime UserBirthday
        {
            get { return _UserBirthday; }
            set { _UserBirthday = value; }
        }
        private string _UserTrueName;
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserTrueName
        {
            get { return _UserTrueName; }
            set { _UserTrueName = value; }
        }
        private string _UserComeFrom;
        /// <summary>
        /// 来自那里
        /// </summary>
        public string UserComeFrom
        {
            get { return _UserComeFrom; }
            set { _UserComeFrom = value; }
        }
        private string _UserContactAddress;
        /// <summary>
        /// 联系地址
        /// </summary>
        public string UserContactAddress
        {
            get { return _UserContactAddress; }
            set { _UserContactAddress = value; }
        }
        private string _UserCode;
        /// <summary>
        /// 邮编号码
        /// </summary>
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        private string _UserContactTel;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string UserContactTel
        {
            get { return _UserContactTel; }
            set { _UserContactTel = value; }
        }
        private string _UserMobile;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string UserMobile
        {
            get { return _UserMobile; }
            set { _UserMobile = value; }
        }
        private string _UserOICQ;
        /// <summary>
        /// OICQ号码
        /// </summary>
        public string UserOICQ
        {
            get { return _UserOICQ; }
            set { _UserOICQ = value; }
        }
        private string _UserIdCard;
        /// <summary>
        /// 身份证
        /// </summary>
        public string UserIdCard
        {
            get { return _UserIdCard; }
            set { _UserIdCard = value; }
        }
        private int _UserMaritalStatus;
        /// <summary>
        /// 结婚状态,0未婚,1已婚,2离异,3丧偶
        /// </summary>
        public int UserMaritalStatus
        {
            get { return _UserMaritalStatus; }
            set { _UserMaritalStatus = value; }
        }
        private string _UserWorkUnit;
        /// <summary>
        /// 工作单位
        /// </summary>
        public string UserWorkUnit
        {
            get { return _UserWorkUnit; }
            set { _UserWorkUnit = value; }
        }
        private string _UserSchool;
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string UserSchool
        {
            get { return _UserSchool; }
            set { _UserSchool = value; }
        }
        private string _UserSign;
        /// <summary>
        /// 个人签名
        /// </summary>
        public string UserSign
        {
            get { return _UserSign; }
            set { _UserSign = value; }
        }
        private string _UserAbout;
        /// <summary>
        /// 个人说明
        /// </summary>
        public string UserAbout
        {
            get { return _UserAbout; }
            set { _UserAbout = value; }
        }
        private string _UserFace;
        /// <summary>
        /// 头像
        /// </summary>
        public string UserFace
        {
            get { return _UserFace; }
            set { _UserFace = value; }
        }
        private string _UserLastIP;
        /// <summary>
        /// IP地址
        /// </summary>
        public string UserLastIP
        {
            get { return _UserLastIP; }
            set { _UserLastIP = value; }
        }
        private int _UserPrivacy;
        /// <summary>
        /// 保密方式
        /// </summary>
        public int UserPrivacy
        {
            get { return _UserPrivacy; }
            set { _UserPrivacy = value; }
        }
        private DateTime _UserRegTime;
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime UserRegTime
        {
            get { return _UserRegTime; }
            set { _UserRegTime = value; }
        }
        private DateTime _UserLastLoginTime;
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime UserLastLoginTime
        {
            get { return _UserLastLoginTime; }
            set { _UserLastLoginTime = value; }
        }
        private int _UserLoginNumber;
        /// <summary>
        /// 登录次数
        /// </summary>
        public int UserLoginNumber
        {
            get { return _UserLoginNumber; }
            set { _UserLoginNumber = value; }
        }
        private int _UserOnlineTime;
        /// <summary>
        /// 在线时间
        /// </summary>
        public int UserOnlineTime
        {
            get { return _UserOnlineTime; }
            set { _UserOnlineTime = value; }
        }
        private DateTime _UserLastActTime;
        /// <summary>
        /// 最后活动时间
        /// </summary>
        public DateTime UserLastActTime
        {
            get { return _UserLastActTime; }
            set { _UserLastActTime = value; }
        }
        private int _UserPoint;
        /// <summary>
        /// 积分
        /// </summary>
        public int UserPoint
        {
            get { return _UserPoint; }
            set { _UserPoint = value; }
        }
        private int _UserPrestige;
        /// <summary>
        /// 信用
        /// </summary>
        public int UserPrestige
        {
            get { return _UserPrestige; }
            set { _UserPrestige = value; }
        }
        private int _UserRmb;
        /// <summary>
        /// 人民币
        /// </summary>
        public int UserRmb
        {
            get { return _UserRmb; }
            set { _UserRmb = value; }
        }
        private int _UserTicket;
        /// <summary>
        /// 点券
        /// </summary>
        public int UserTicket
        {
            get { return _UserTicket; }
            set { _UserTicket = value; }
        }
        private int _UserMoney;
        /// <summary>
        /// 虚拟金钱
        /// </summary>
        public int UserMoney
        {
            get { return _UserMoney; }
            set { _UserMoney = value; }
        }
        private int _UserExp;
        /// <summary>
        /// 经验值
        /// </summary>
        public int UserExp
        {
            get { return _UserExp; }
            set { _UserExp = value; }
        }
        private int _UserCP;
        /// <summary>
        /// 魅力值
        /// </summary>
        public int UserCP
        {
            get { return _UserCP; }
            set { _UserCP = value; }
        }
        private string _UserOnLineStatic;
        /// <summary>
        /// 在线状态
        /// </summary>
        public string UserOnLineStatic
        {
            get { return _UserOnLineStatic; }
            set { _UserOnLineStatic = value; }
        }
        private string _UserGroup;
        /// <summary>
        /// 俱乐部
        /// </summary>
        public string UserGroup
        {
            get { return _UserGroup; }
            set { _UserGroup = value; }
        }
        private int _UserLevelID;
        /// <summary>
        /// 等级
        /// </summary>
        public int UserLevelID
        {
            get { return _UserLevelID; }
            set { _UserLevelID = value; }
        }
        private string _UserRole;
        /// <summary>
        /// 用户角色
        /// </summary>
        public string UserRole
        {
            get { return _UserRole; }
            set { _UserRole = value; }
        }
        private int _UserPostNumber;
        /// <summary>
        /// 发表文章总数
        /// </summary>
        public int UserPostNumber
        {
            get { return _UserPostNumber; }
            set { _UserPostNumber = value; }
        }
        private int _UserTopicNumber;
        /// <summary>
        /// 发表主题数
        /// </summary>
        public int UserTopicNumber
        {
            get { return _UserTopicNumber; }
            set { _UserTopicNumber = value; }
        }
        private int _UserReTopicNumber;
        /// <summary>
        /// 回复帖子数
        /// </summary>
        public int UserReTopicNumber
        {
            get { return _UserReTopicNumber; }
            set { _UserReTopicNumber = value; }
        }
        private int _UserDelTopicNumber;
        /// <summary>
        /// 被删除文章数
        /// </summary>
        public int UserDelTopicNumber
        {
            get { return _UserDelTopicNumber; }
            set { _UserDelTopicNumber = value; }
        }
        private string _UserWebAddress;
        /// <summary>
        /// 个人主页
        /// </summary>
        public string UserWebAddress
        {
            get { return _UserWebAddress; }
            set { _UserWebAddress = value; }
        }
        private string _UserWebLog;
        /// <summary>
        /// 个人博客地址
        /// </summary>
        public string UserWebLog
        {
            get { return _UserWebLog; }
            set { _UserWebLog = value; }
        }
        private string _UserWebGallery;
        /// <summary>
        /// 个人相册地址
        /// </summary>
        public string UserWebGallery
        {
            get { return _UserWebGallery; }
            set { _UserWebGallery = value; }
        }
        private string _UserInterests;
        /// <summary>
        /// 个人爱好
        /// </summary>
        public string UserInterests
        {
            get { return _UserInterests; }
            set { _UserInterests = value; }
        }
        private double _UserTrueMoney;
        /// <summary>
        /// 真实金钱
        /// </summary>
        public double UserTrueMoney
        {
            get { return _UserTrueMoney; }
            set { _UserTrueMoney = value; }
        }
        /// <summary>
        /// 接收短信方式 0接收所有人的短信,1只接收注册用户的短信,2只接收好友的短信,3不接收任何短信
        /// </summary>
        private int _UserReceiveType;
        public int UserReceiveType
        {
            get { return _UserReceiveType; }
            set { _UserReceiveType = value; }
        }
    }
}
