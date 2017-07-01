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
    /// PowerTalk ��ժҪ˵��
    /// </summary>
    public class PowerTalk
    {
        /// <summary>
        /// WWW��ַ
        /// </summary>
        public static string FaceWwwPath = "";
        /// <summary>
        /// PowerTalk
        /// </summary>
        public PowerTalk()
        {
         
        }
        #region �����û�
        /// <summary>
        /// ����ο��û����
        /// </summary>
        public static string NewClientUserLogin(bool AutoInList)
        {
            string UserIDStr = "";
            UserIDStr = RadomName();

            while (ExistUser("�ο�" + UserIDStr))
            {
                UserIDStr = RadomName();
            }
            UserInfo UserItem = new UserInfo();
            UserItem.UserID = "�ο�" + UserIDStr;
            UserItem.UserPersonInfo = UserIDStr + " ��¼ʱ��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            UserItem.Level = "0";
            if (AutoInList)
            {
            ListClass.Engin_UserList.Add(UserItem);
            }
            return UserItem.UserID;
        }
        /// <summary>
        /// �û����
        /// </summary>
        /// <param name="UserIDStr">�û�ID</param>
        /// <param name="UserNameStr">�û���Ϣ</param>
        /// <param name="Level">����</param>
        public static void NewUserLogin(string UserIDStr, string UserNameStr, string Level)
        {
            if (ExistUser(UserIDStr))
            {
                throw new Exception("UserID���ظ�!");
            }
            UserInfo UserItem = new UserInfo();
            UserItem.UserID = UserIDStr;
            UserItem.UserPersonInfo = UserNameStr;
            UserItem.Level = Level;
            ListClass.Engin_UserList.Add(UserItem);
        }
        /// <summary>
        /// �������
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
        /// �ж���������
        /// </summary>
        /// <param name="ID">����</param>
        /// <returns></returns>
        public static bool ExistUser(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            return ListClass.Engin_UserList.Exists(Fc.PredicateUser);
        }
        /// <summary>
        /// �û�List�б�
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> UserInfos(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            return ListClass.Engin_UserList.FindAll(Fc.PredicateUserList);
        }
        /// <summary>
        /// �����û���
        /// </summary>
        /// <param name="ID">�û�ID</param>
        /// <returns></returns>
        public static UserInfo FindUserInfo(string ID)
        {
            FindClass Fc = new FindClass();
            Fc.UserId = ID;
            UserInfo Ui = ListClass.Engin_UserList.Find(Fc.PredicateUser);
            return Ui;
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="ID">�û�ID</param>
        /// <returns></returns>
        public static bool DeleteUserInfo(string ID)
        {
            UserInfo Ui = FindUserInfo(ID);
            DeleteChatInfo(ID);//ɾ����¼
            return ListClass.Engin_UserList.Remove(Ui);
        }
        #endregion
        #region ��¼����
        /// <summary>
        /// ����¼�¼:Ⱥ��
        /// </summary>
        /// <param name="_Sender">������</param>
        /// <param name="_SendContent">����</param>
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
        /// ����¼�¼:����
        /// </summary>
        /// <param name="_Sender">������</param>
        /// <param name="_Reciver">������</param>
        /// <param name="_SendContent">����</param>
        public static void AddChatInfo(string _Sender, string _Reciver, string _SendContent)
        {
            ChatInfo CiItem = new ChatInfo();
            CiItem.Sender = _Sender;
            CiItem.SendContent = _SendContent;
            CiItem.Reciver = _Reciver;
            CiItem.SendTime = DateTime.Now;                 
            ListClass.Engin_ChatList.Add(CiItem);
        }
        /*����ģʽ 1Ⱥ�ģ���Զ� 2���Զ� 3���Ե� 
         *1������Ч��ʱ
         *2����Ա�ش�����ʱ
         *3�ͻ���������ѯʱ
         */
        /// <summary>
        /// ��ȡ��¼
        /// </summary>
        /// <param name="ID">������ID</param>
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
        /// ɾ����¼
        /// </summary>
        /// <param name="ID">����ID</param>
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
    /// �б�
    /// </summary>
    public class ListClass
    {
        public static PowerTalkBoxEnum.Enum.ChatMode _MsnChatMode = PowerTalkBoxEnum.Enum.ChatMode.OneToMore;
        //��������
        public static int OnlineMsnCount=0;
        private static List<UserInfo> _Engin_UserList = new List<UserInfo>();
        private static List<ChatInfo> _Engin_ChatList = new List<ChatInfo>();
        private static List<MsnUserInfo> _Engin_MsnList = new List<MsnUserInfo>();
        #region ����
        //�û��б�
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
        //�����¼
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
        //MSN��¼
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
    /// ������
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
        /// MSNһ��һʱ
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
    /// MSN�û���Ϣ
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
                    throw new Exception("������'$','|'�ȷǷ��ַ�");
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
    /// �û���Ϣ
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
                    throw new Exception("������'$','|'�ȷǷ��ַ�");
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
                    throw new Exception("������'$','|'�ȷǷ��ַ�");
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
    /// �����ݴ��¼
    /// </summary>
    public class ChatInfo
    {
        private string _Sender;
        private string _Reciver;
        private string _SendContent;
        private DateTime _SendTime;
        /// <summary>
        /// ������
        /// </summary>
        public string Sender
        {
            set { _Sender = value; }
            get { return _Sender; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Reciver
        {
            set { _Reciver = value; }
            get { return _Reciver; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string SendContent
        {
            set { _SendContent = value; }
            get { return _SendContent; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime SendTime
        {
            set { _SendTime = value; }
            get { return _SendTime; }
        }
    }

}
