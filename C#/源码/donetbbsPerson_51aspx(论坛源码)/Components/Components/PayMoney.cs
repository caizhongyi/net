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
    public class PayMoney
    {
        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            PayMoneyID = int.Parse(rs["TopicID"].ToString());
            PayMoneys = double.Parse(rs["PayMoneys"].ToString());
            PayMoneyFromUserID = int.Parse(rs["PayMoneyFromUserID"].ToString());
            PayMoneyToUserID = int.Parse(rs["PayMoneyToUserID"].ToString());
            PayMoneyCreateTime = System.Convert.ToDateTime(rs["PayMoneyCreateTime"].ToString());
            PayMoneyType = int.Parse(rs["PayMoneyType"].ToString());
            PayMoneyContent = rs["PayMoneyContent"].ToString();
        }//
        private int _PayMoneyID;
        public int PayMoneyID
        {
            get { return _PayMoneyID; }
            set { _PayMoneyID = value; }
        }
        private double _PayMoneys;
        public double PayMoneys
        {
            get { return _PayMoneys; }
            set { _PayMoneys = value; }
        }
        private int _PayMoneyFromUserID;
        public int PayMoneyFromUserID
        {
            get { return _PayMoneyFromUserID; }
            set { _PayMoneyFromUserID = value; }
        }
        private int _PayMoneyToUserID;
        public int PayMoneyToUserID
        {
            get { return _PayMoneyToUserID; }
            set { _PayMoneyToUserID = value; }
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