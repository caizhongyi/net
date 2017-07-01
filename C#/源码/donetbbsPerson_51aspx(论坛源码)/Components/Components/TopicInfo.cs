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
    public class TopicInfo
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            TopicInfoID = int.Parse(rs["TopicInfoID"].ToString());
            TopicInfoTitle = rs["TopicInfoTitle"].ToString();
            TopicInfoUserID = int.Parse(rs["TopicInfoUserID"].ToString());
            TopicInfoRootID = int.Parse(rs["TopicInfoRootID"].ToString());
            TopicInfoParentID = int.Parse(rs["TopicInfoParentID"].ToString());
            TopicInfoUserNickName = rs["TopicInfoUserNickName"].ToString();
            TopicInfoUserIP = rs["TopicInfoUserIP"].ToString();
            TopicInfoFalse = int.Parse(rs["TopicInfoFalse"].ToString());
            TopicInfoHtml = rs["TopicInfoHtml"].ToString();
            TopicInfoText = rs["TopicInfoText"].ToString();
            TopicInfoViewRole = rs["TopicInfoViewRole"].ToString();
            TopicInfoRePostRole = rs["TopicInfoRePostRole"].ToString();
            TopicInfoFace = rs["TopicInfoFace"].ToString();
            TopicInfoBuyMoney = int.Parse(rs["TopicInfoBuyMoney"].ToString());
            TopicInfoPostTime = System.Convert.ToDateTime(rs["TopicInfoPostTime"].ToString());
            TopicInfoViewUserGroup = rs["TopicInfoViewUserGroup"].ToString();
            TopicInfoRePostUserGroup = rs["TopicInfoRePostUserGroup"].ToString();
            TopicInfoSignFalse = int.Parse(rs["TopicInfoSignFalse"].ToString());
            TopicInfoReply = int.Parse(rs["TopicInfoReply"].ToString());
            TopicInfoEditHistory = rs["TopicInfoEditHistory"].ToString();

        }//
        private int _TopicInfoRootID;
        public int TopicInfoRootID
        {
            get { return _TopicInfoRootID; }
            set { _TopicInfoRootID = value; }
        }
        private int _TopicInfoUserID;
        public int TopicInfoUserID
        {
            get { return _TopicInfoUserID; }
            set { _TopicInfoUserID = value; }
        }


        private int _TopicInfoID;
        public int TopicInfoID
        {
            get { return _TopicInfoID; }
            set { _TopicInfoID = value; }
        }
        private string _TopicInfoTitle;
        public string TopicInfoTitle
        {
            get { return _TopicInfoTitle; }
            set { _TopicInfoTitle = value; }
        }
        private int _TopicInfoParentID;
        public int TopicInfoParentID
        {
            get { return _TopicInfoParentID; }
            set { _TopicInfoParentID = value; }
        }
        private string _TopicInfoUserNickName;
        public string TopicInfoUserNickName
        {
            get { return _TopicInfoUserNickName; }
            set { _TopicInfoUserNickName = value; }
        }
        private string _TopicInfoUserIP;
        public string TopicInfoUserIP
        {
            get { return _TopicInfoUserIP; }
            set { _TopicInfoUserIP = value; }
        }
        private int _TopicInfoFalse;
        public int TopicInfoFalse
        {
            get { return _TopicInfoFalse; }
            set { _TopicInfoFalse = value; }
        }
        private string _TopicInfoHtml;
        public string TopicInfoHtml
        {
            get { return _TopicInfoHtml; }
            set { _TopicInfoHtml = value; }
        }
        private string _TopicInfoText;
        public string TopicInfoText
        {
            get { return _TopicInfoText; }
            set { _TopicInfoText = value; }
        }
        private string _TopicInfoViewRole;
        public string TopicInfoViewRole
        {
            get { return _TopicInfoViewRole; }
            set { _TopicInfoViewRole = value; }
        }
        private string _TopicInfoRePostRole;
        public string TopicInfoRePostRole
        {
            get { return _TopicInfoRePostRole; }
            set { _TopicInfoRePostRole = value; }
        }
        private string _TopicInfoFace;
        public string TopicInfoFace
        {
            get { return _TopicInfoFace; }
            set { _TopicInfoFace = value; }
        }
        private int _TopicInfoBuyMoney;
        public int TopicInfoBuyMoney
        {
            get { return _TopicInfoBuyMoney; }
            set { _TopicInfoBuyMoney = value; }
        }
        private DateTime _TopicInfoPostTime;
        public DateTime TopicInfoPostTime
        {
            get { return _TopicInfoPostTime; }
            set { _TopicInfoPostTime = value; }
        }
        private string _TopicInfoViewUserGroup;
        public string TopicInfoViewUserGroup
        {
            get { return _TopicInfoViewUserGroup; }
            set { _TopicInfoViewUserGroup = value; }
        }
        private string _TopicInfoRePostUserGroup;
        public string TopicInfoRePostUserGroup
        {
            get { return _TopicInfoRePostUserGroup; }
            set { _TopicInfoRePostUserGroup = value; }
        }
        private int _TopicInfoSignFalse;
        public int TopicInfoSignFalse
        {
            get { return _TopicInfoSignFalse; }
            set { _TopicInfoSignFalse = value; }
        }

        private int _TopicInfoReply;
        public int TopicInfoReply
        {
            get { return _TopicInfoReply; }
            set { _TopicInfoReply = value; }
        }

        private string _TopicInfoEditHistory;
        public string TopicInfoEditHistory
        {
            get { return _TopicInfoEditHistory; }
            set { _TopicInfoEditHistory = value; }
        }
    }
}