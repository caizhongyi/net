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
    public class EmailWork
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            EmailWorkID = int.Parse(rs["EmailWorkID"].ToString());
            EmailWorkFromName = rs["EmailWorkFromName"].ToString();
            EmailWorkType = int.Parse(rs["EmailWorkType"].ToString());
            EmailWorkFromEmail = rs["EmailWorkFromEmail"].ToString();
            EmailWorkFromUserID = int.Parse(rs["EmailWorkFromUserID"].ToString());
            EmailWorkReceiveUserID = int.Parse(rs["EmailWorkReceiveUserID"].ToString());
            EmailWorkReceiveName = rs["EmailWorkReceiveName"].ToString();
            EmailWorkCreateTime = System.Convert.ToDateTime(rs["EmailWorkCreateTime"].ToString());
            EmailWorkPostTime = System.Convert.ToDateTime(rs["EmailWorkPostTime"].ToString());
            EmailWorkReceiveEmail = rs["EmailWorkReceiveEmail"].ToString();
            EmailWorkContent = rs["EmailWorkContent"].ToString();
            EmailWorkTitle = rs["EmailWorkTitle"].ToString();
            EmailWorkStatus = int.Parse(rs["EmailWorkStatus"].ToString());
        }//
        private int _EmailWorkID;
        public int EmailWorkID
        {
            get { return _EmailWorkID; }
            set { _EmailWorkID = value; }
        }
        private int _EmailWorkType;
        public int EmailWorkType
        {
            get { return _EmailWorkType; }
            set { _EmailWorkType = value; }
        }
        private string _EmailWorkFromName;
        public string EmailWorkFromName
        {
            get { return _EmailWorkFromName; }
            set { _EmailWorkFromName = value; }
        }
        private string _EmailWorkReceiveName;
        public string EmailWorkReceiveName
        {
            get { return _EmailWorkReceiveName; }
            set { _EmailWorkReceiveName = value; }
        }
        private int _EmailWorkFromUserID;
        public int EmailWorkFromUserID
        {
            get { return _EmailWorkFromUserID; }
            set { _EmailWorkFromUserID = value; }
        }
        private string _EmailWorkFromEmail = string.Empty;
        public string EmailWorkFromEmail
        {
            get { return _EmailWorkFromEmail; }
            set { _EmailWorkFromEmail = value; }
        }
        private int _EmailWorkReceiveUserID;
        public int EmailWorkReceiveUserID
        {
            get { return _EmailWorkReceiveUserID; }
            set { _EmailWorkReceiveUserID = value; }
        }
        private int _EmailWorkStatus;
        public int EmailWorkStatus
        {
            get { return _EmailWorkStatus; }
            set { _EmailWorkStatus = value; }
        }
        private string _EmailWorkReceiveEmail;
        public string EmailWorkReceiveEmail
        {
            get { return _EmailWorkReceiveEmail; }
            set { _EmailWorkReceiveEmail = value; }
        }
        private DateTime _EmailWorkPostTime;
        public DateTime EmailWorkPostTime
        {
            get { return _EmailWorkPostTime; }
            set { _EmailWorkPostTime = value; }
        }
        private DateTime _EmailWorkCreateTime;
        public DateTime EmailWorkCreateTime
        {
            get { return _EmailWorkCreateTime; }
            set { _EmailWorkCreateTime = value; }
        }
        private string _EmailWorkContent;
        public string EmailWorkContent
        {
            get { return _EmailWorkContent; }
            set { _EmailWorkContent = value; }
        }
        private string _EmailWorkTitle = string.Empty;
        public string EmailWorkTitle
        {
            get { return _EmailWorkTitle; }
            set { _EmailWorkTitle = value; }
        }
    }
}