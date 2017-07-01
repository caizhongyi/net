using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PowerTalkBox
{
    /// <summary>
    /// PowerTalk 的摘要说明
    /// </summary>
    public class PowerTalk
    {
        /// <summary>
        /// WWW地址
        /// </summary>
        public static string FaceWwwPath = "";
        /// <summary>
        /// PowerTalk
        /// </summary>
        public PowerTalk()
        {
         
        }
        #region 操作用户
        /// <summary>
        /// 随机游客用户添加
        /// </summary>
        public static string NewClientUserLogin(bool AutoInList)
        {
            string UserIDStr = "";
            UserIDStr = RadomName();

            while (ExistUser("游客" + UserIDStr))
            {
                UserIDStr = RadomName();
            }
            UserInfo UserItem = new UserInfo();
            UserItem.UserID = "游客" + UserIDStr;
            UserItem.UserPersonInfo = UserIDStr + " 登录时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            UserItem.Level = "0";
            if (AutoInList)
            {
            ListClass.Engin_UserList.Add(UserItem);
            }
            return UserItem.UserID;
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="UserIDStr">用户ID</param>
        /// <param name="UserNameStr">用户信息</param>
        /// <param name="Level">级别</param>
        public static void NewUserLogin(string UserIDStr, string UserNameStr, string Level)
        {
            if (ExistUser(UserIDStr))
            {
                throw new Exception("UserID有重复!");
            }
            UserInfo UserItem = new UserInfo();
            UserItem.UserID = UserIDStr;
            UserItem.UserPersonInfo = UserNameStr;
            UserItem.Level = Level;
            ListClass.Engin_UserList.Add(UserItem);
        }
        /// <summary>
        /// 随机起名
        /// </summary>
        /// <returns></returns>
        public static string RadomName()
        {
            Random rdm = new Random();
            string UserIDStr = "";
            for (int i = 1; i <= 5; i++)
            {
                UserIDStr += rdm.Next(1, 9).ToString();
            }
            return UserIDStr;
        }
        /// <summary>
        /// 判断有无重名
        /// </summary>
        /// <param name="ID">姓名</param>
        /// <returns></returns>
        public static bool ExistUser(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            return ListClass.Engin_UserList.Exists(Fc.PredicateUser);
        }
        /// <summary>
        /// 用户List列表
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> UserInfos(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            return ListClass.Engin_UserList.FindAll(Fc.PredicateUserList);
        }
        /// <summary>
        /// 查找用户名
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns></returns>
        public static UserInfo FindUserInfo(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            UserInfo Ui = ListClass.Engin_UserList.Find(Fc.PredicateUser);
            return Ui;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns></returns>
        public static bool DeleteUserInfo(string ID)
        {
            UserInfo Ui = FindUserInfo(ID);
            DeleteChatInfo(ID);//删除记录
            return ListClass.Engin_UserList.Remove(Ui);
        }
        #endregion
        #region 记录操作
        /// <summary>
        /// 添加新记录:群聊
        /// </summary>
        /// <param name="_Sender">发送者</param>
        /// <param name="_SendContent">内容</param>
        public static void AddChatInfo(string _Sender, string _SendContent)
        {
            foreach (UserInfo Uif in ListClass.Engin_UserList)
            {
                ChatInfo CiItem = new ChatInfo();
                CiItem.Sender = _Sender;
                CiItem.Reciver = Uif.UserID;
                CiItem.SendContent = _SendContent;
                CiItem.SendTime = DateTime.Now;
                ListClass.Engin_ChatList.Add(CiItem);
            }
        }
        /// <summary>
        /// 添加新记录:单聊
        /// </summary>
        /// <param name="_Sender">发送者</param>
        /// <param name="_Reciver">接收者</param>
        /// <param name="_SendContent">内容</param>
        public static void AddChatInfo(string _Sender, string _Reciver, string _SendContent)
        {
            ChatInfo CiItem = new ChatInfo();
            CiItem.Sender = _Sender;
            CiItem.SendContent = _SendContent;
            CiItem.Reciver = _Reciver;
            CiItem.SendTime = DateTime.Now;                 
            ListClass.Engin_ChatList.Add(CiItem);
        }
        /*三种模式 1群聊，多对多 2单对多 3单对单 
         *1聊天室效果时
         *2管理员回答问题时
         *3客户问问题咨询时
         */
        /// <summary>
        /// 读取记录
        /// </summary>
        /// <param name="ID">接收者ID</param>
        /// <returns></returns>
        public static List<ChatInfo> ReadChatInfo(string ID,PowerTalkBoxEnum.Enum.SystemMode MsnMd)
        {
            List<ChatInfo> ChatInfoArray = null;
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
             ChatInfoArray = ListClass.Engin_ChatList.FindAll(Fc.PredicateChat);
            foreach (ChatInfo Cif in ChatInfoArray)
            {
                ListClass.Engin_ChatList.Remove(Cif);
            }
            return ChatInfoArray;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ID">接收ID</param>
        /// <returns></returns>
        public static bool DeleteChatInfo(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            List<ChatInfo> ChatInfoArray = ListClass.Engin_ChatList.FindAll(Fc.PredicateChat);
            foreach (ChatInfo Cif in ChatInfoArray)
            {
                ListClass.Engin_ChatList.Remove(Cif);
            }
            return true;
        }

        #endregion
    }
    /// <summary>
    /// 列表
    /// </summary>
    public class ListClass
    {
        public static PowerTalkBoxEnum.Enum.ChatMode _MsnChatMode = PowerTalkBoxEnum.Enum.ChatMode.OneToMore;
        //在线人数
        public static int OnlineMsnCount=0;
        private static List<UserInfo> _Engin_UserList = new List<UserInfo>();
        private static List<ChatInfo> _Engin_ChatList = new List<ChatInfo>();
        private static List<MsnUserInfo> _Engin_MsnList = new List<MsnUserInfo>();
        #region 定义
        //用户列表
        public static List<UserInfo> Engin_UserList
        {
            get 
            { 
                return _Engin_UserList; 
            }
            set 
            { 
                _Engin_UserList = value;
            }

        }
        //聊天记录
        public static List<ChatInfo> Engin_ChatList
        {
            get 
            {
                return _Engin_ChatList; 
            }
            set 
            { 
                _Engin_ChatList = value; 
            }
        }
        //MSN记录
        public static List<MsnUserInfo> Engin_MsnList
        {
            
            get
            {
                return _Engin_MsnList;
            }
            set
            {
                _Engin_MsnList = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// 查找类
    /// </summary>
    public class FindClass
    {
        public string UserId;
        public bool PredicateUser(UserInfo s)
        {
            if (s.UserID == UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PredicateUserList(UserInfo s)
        {
            if (s.UserID != UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// MSN一对一时
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool PredicateMsnUser(ChatInfo c)
        {
            if ((c.Reciver == UserId) && (c.Sender == UserId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool PredicateChat(ChatInfo c)
        {
            if ((c.Reciver == UserId) && (c.Sender != UserId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    /// <summary>
    /// MSN用户信息
    /// </summary>
    public class MsnUserInfo
    {
        private string _userID;
 
        private DateTime _lasttime;
        public string UserID
        {
            get { return _userID; }
            set
            {
                if ((value.IndexOf('$') >= 0) || (value.IndexOf('|') >= 0))
                {
                    throw new Exception("不能有'$','|'等非法字符");
                }
                _userID = value;
            }
        }

        public DateTime LastTime
        {
            get { return _lasttime; }
            set { _lasttime = value; }
        }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        private string _userID;
        private string _userInfo;
        private string _level;
        public string UserID
        {
            get { return _userID; }
            set
            {
                if ((value.IndexOf('$') >= 0) || (value.IndexOf('|') >= 0))
                {
                    throw new Exception("不能有'$','|'等非法字符");
                }
                _userID = value;
            }
        }
        public string UserPersonInfo
        {
            get { return _userInfo; }
            set
            {
                if (value.IndexOf('$') >= 0 || (value.IndexOf('|') >= 0))
                {
                    throw new Exception("不能有'$','|'等非法字符");
                }
                _userInfo = value;
            }
        }
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

    }
    /// <summary>
    /// 聊天暂存记录
    /// </summary>
    public class ChatInfo
    {
        private string _Sender;
        private string _Reciver;
        private string _SendContent;
        private DateTime _SendTime;
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender
        {
            set { _Sender = value; }
            get { return _Sender; }
        }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Reciver
        {
            set { _Reciver = value; }
            get { return _Reciver; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string SendContent
        {
            set { _SendContent = value; }
            get { return _SendContent; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            set { _SendTime = value; }
            get { return _SendTime; }
        }
    }

}
