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
    public class WebSite
    {
        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }
        public void SetDataProviders(DataRow rs)
        {
            WebSiteID = int.Parse(rs["WebSiteID"].ToString());
            Forum_MaxOnline = int.Parse(rs["Forum_MaxOnline"].ToString());
            Forum_MaxOnlineDate = System.Convert.ToDateTime(rs["Forum_MaxOnlineDate"].ToString());
            Forum_TopicNumber = int.Parse(rs["Forum_TopicNumber"].ToString());
            Forum_PostNumber = int.Parse(rs["Forum_PostNumber"].ToString());
            Forum_TodayNumber = int.Parse(rs["Forum_TodayNumber"].ToString());
            Forum_UserNumber = int.Parse(rs["Forum_UserNumber"].ToString());
            Forum_YesTerdayNumber = int.Parse(rs["Forum_YesTerdayNumber"].ToString());
            Forum_MaxPostNumber = int.Parse(rs["Forum_MaxPostNumber"].ToString());
            Forum_MaxPostDate = System.Convert.ToDateTime(rs["Forum_MaxPostDate"].ToString());
            Forum_LastUserID = int.Parse(rs["Forum_LastUserID"].ToString());
            Forum_LockIP = rs["Forum_LockIP"].ToString();
            Forum_TodyDate = System.Convert.ToDateTime(rs["Forum_TodyDate"].ToString());
            Forum_StartDate = System.Convert.ToDateTime(rs["Forum_StartDate"].ToString());
            Forum_LastUserNickName = rs["Forum_LastUserNickName"].ToString();
            Forum_UserOnline = int.Parse(rs["Forum_UserOnline"].ToString());
            Forum_GuestOnline = int.Parse(rs["Forum_GuestOnline"].ToString());
            Forum_AllOnline = int.Parse(rs["Forum_AllOnline"].ToString());
            Forum_UserIllegal = rs["Forum_UserIllegal"].ToString();
            Forum_SystemIllegal = rs["Forum_SystemIllegal"].ToString();

        }//
        private int _WebSiteID;
        public int WebSiteID
        {
            get { return _WebSiteID; }
            set { _WebSiteID = value; }
        }//
        private int _Forum_MaxOnline;
        public int Forum_MaxOnline
        {
            get { return _Forum_MaxOnline; }
            set { _Forum_MaxOnline = value; }
        }//
        private DateTime _Forum_MaxOnlineDate;
        public DateTime Forum_MaxOnlineDate
        {
            get { return _Forum_MaxOnlineDate; }
            set { _Forum_MaxOnlineDate = value; }
        }//
        private int _Forum_TopicNumber;
        public int Forum_TopicNumber
        {
            get { return _Forum_TopicNumber; }
            set { _Forum_TopicNumber = value; }
        }//
        private int _Forum_PostNumber;
        public int Forum_PostNumber
        {
            get { return _Forum_PostNumber; }
            set { _Forum_PostNumber = value; }
        }//
        private int _Forum_TodayNumber;
        public int Forum_TodayNumber
        {
            get { return _Forum_TodayNumber; }
            set { _Forum_TodayNumber = value; }
        }//
        private int _Forum_UserNumber;
        public int Forum_UserNumber
        {
            get { return _Forum_UserNumber; }
            set { _Forum_UserNumber = value; }
        }//
        private int _Forum_YesTerdayNumber;
        public int Forum_YesTerdayNumber
        {
            get { return _Forum_YesTerdayNumber; }
            set { _Forum_YesTerdayNumber = value; }
        }//
        private int _Forum_MaxPostNumber;
        public int Forum_MaxPostNumber
        {
            get { return _Forum_MaxPostNumber; }
            set { _Forum_MaxPostNumber = value; }
        }//
        private DateTime _Forum_MaxPostDate;
        public DateTime Forum_MaxPostDate
        {
            get { return _Forum_MaxPostDate; }
            set { _Forum_MaxPostDate = value; }
        }//
        private int _Forum_LastUserID;
        public int Forum_LastUserID
        {
            get { return _Forum_LastUserID; }
            set { _Forum_LastUserID = value; }
        }//
        private string _Forum_LockIP;
        public string Forum_LockIP
        {
            get { return _Forum_LockIP; }
            set { _Forum_LockIP = value; }
        }//
        private DateTime _Forum_TodyDate;
        public DateTime Forum_TodyDate
        {
            get { return _Forum_TodyDate; }
            set { _Forum_TodyDate = value; }
        }//
        private DateTime _Forum_StartDate;
        public DateTime Forum_StartDate
        {
            get { return _Forum_StartDate; }
            set { _Forum_StartDate = value; }
        }//
        private string _Forum_LastUserNickName;
        public string Forum_LastUserNickName
        {
            get { return _Forum_LastUserNickName; }
            set { _Forum_LastUserNickName = value; }
        }//
        private int _Forum_UserOnline;
        public int Forum_UserOnline
        {
            get { return _Forum_UserOnline; }
            set { _Forum_UserOnline = value; }
        }//
        private int _Forum_GuestOnline;
        public int Forum_GuestOnline
        {
            get { return _Forum_GuestOnline; }
            set { _Forum_GuestOnline = value; }
        }//
        private int _Forum_AllOnline;
        public int Forum_AllOnline
        {
            get { return _Forum_AllOnline; }
            set { _Forum_AllOnline = value; }
        }//
        private string _Forum_UserIllegal;
        public string Forum_UserIllegal
        {
            get { return _Forum_UserIllegal; }
            set { _Forum_UserIllegal = value; }
        }//
        private string _Forum_SystemIllegal;
        public string Forum_SystemIllegal
        {
            get { return _Forum_SystemIllegal; }
            set { _Forum_SystemIllegal = value; }
        }//

    }
}
