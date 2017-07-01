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
    public class Topic
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            TopicID = int.Parse(rs["TopicID"].ToString());
            TopicTitle = rs["TopicTitle"].ToString();
            TopicBoardID = int.Parse(rs["TopicBoardID"].ToString());
            TopicImages = rs["TopicImages"].ToString();
            TopicReNumber = int.Parse(rs["TopicReNumber"].ToString());
            TopicViewNumber = int.Parse(rs["TopicViewNumber"].ToString());
            TopicReLastUserID = int.Parse(rs["TopicReLastUserID"].ToString());
            TopicReLastUserNickName = rs["TopicReLastUserNickName"].ToString();
            TopicBest = int.Parse(rs["TopicBest"].ToString());
            TopicRecommend = int.Parse(rs["TopicRecommend"].ToString());
            TopicTotalAtTop = int.Parse(rs["TopicTotalAtTop"].ToString());
            TopicLastReTime = System.Convert.ToDateTime(rs["TopicLastReTime"].ToString());
            TopicSubjectID = int.Parse(rs["TopicSubjectID"].ToString());
            TopicFalse = int.Parse(rs["TopicFalse"].ToString());
            TopicPostUserID = int.Parse(rs["TopicPostUserID"].ToString());
            TopicPostUserNickName = rs["TopicPostUserNickName"].ToString();
            TopicPostTime = System.Convert.ToDateTime(rs["TopicPostTime"].ToString());
            TopicSpecialTitle = rs["TopicSpecialTitle"].ToString();
            TopicRePostEmail = int.Parse(rs["TopicRePostEmail"].ToString());
        }//
        private int _TopicID;
        public int TopicID
        {
            get { return _TopicID; }
            set { _TopicID = value; }
        }
        private int _TopicRePostEmail;
        public int TopicRePostEmail
        {
            get { return _TopicRePostEmail; }
            set { _TopicRePostEmail = value; }
        }
        private string _TopicTitle;
        public string TopicTitle
        {
            get { return _TopicTitle; }
            set { _TopicTitle = value; }
        }
        private int _TopicBoardID;
        public int TopicBoardID
        {
            get { return _TopicBoardID; }
            set { _TopicBoardID = value; }
        }
        private string _TopicImages = string.Empty;
        public string TopicImages
        {
            get { return _TopicImages; }
            set { _TopicImages = value; }
        }
        private int _TopicReNumber;
        public int TopicReNumber
        {
            get { return _TopicReNumber; }
            set { _TopicReNumber = value; }
        }
        private int _TopicViewNumber;
        public int TopicViewNumber
        {
            get { return _TopicViewNumber; }
            set { _TopicViewNumber = value; }
        }
        private int _TopicReLastUserID;
        public int TopicReLastUserID
        {
            get { return _TopicReLastUserID; }
            set { _TopicReLastUserID = value; }
        }
        private string _TopicReLastUserNickName;
        public string TopicReLastUserNickName
        {
            get { return _TopicReLastUserNickName; }
            set { _TopicReLastUserNickName = value; }
        }
        private int _TopicBest;
        public int TopicBest
        {
            get { return _TopicBest; }
            set { _TopicBest = value; }
        }
        private int _TopicRecommend;
        public int TopicRecommend
        {
            get { return _TopicRecommend; }
            set { _TopicRecommend = value; }
        }
        private int _TopicTotalAtTop;
        public int TopicTotalAtTop
        {
            get { return _TopicTotalAtTop; }
            set { _TopicTotalAtTop = value; }
        }

        private DateTime _TopicLastReTime;
        public DateTime TopicLastReTime
        {
            get { return _TopicLastReTime; }
            set { _TopicLastReTime = value; }
        }
        private int _TopicSubjectID;
        public int TopicSubjectID
        {
            get { return _TopicSubjectID; }
            set { _TopicSubjectID = value; }
        }
        private int _TopicFalse;
        public int TopicFalse
        {
            get { return _TopicFalse; }
            set { _TopicFalse = value; }
        }
        private int _TopicPostUserID;
        public int TopicPostUserID
        {
            get { return _TopicPostUserID; }
            set { _TopicPostUserID = value; }
        }
        private string _TopicPostUserNickName;
        public string TopicPostUserNickName
        {
            get { return _TopicPostUserNickName; }
            set { _TopicPostUserNickName = value; }
        }
        private DateTime _TopicPostTime;
        public DateTime TopicPostTime
        {
            get { return _TopicPostTime; }
            set { _TopicPostTime = value; }
        }
        private string _TopicSpecialTitle = string.Empty;
        public string TopicSpecialTitle
        {
            get { return _TopicSpecialTitle; }
            set { _TopicSpecialTitle = value; }
        }
    }
}