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
    public class Messages
    {
        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            MessagesID = int.Parse(rs["MessagesID"].ToString());
            MessagesFromUserID = int.Parse(rs["MessagesFromUserID"].ToString());
            MessagesFromUserName = rs["MessagesFromUserName"].ToString();
            MessagesFromUserIP = rs["MessagesFromUserIP"].ToString();
            MessagesReceiveUserID = int.Parse(rs["MessagesReceiveUserID"].ToString());
            MessagesReceiveUserName = rs["MessagesReceiveUserName"].ToString();
            MessagesReceiveUserIP = rs["MessagesReceiveUserIP"].ToString();
            MessagesCreateTime = System.Convert.ToDateTime(rs["MessagesCreateTime"].ToString());
            MessagesFromDelete = int.Parse(rs["MessagesFromDelete"].ToString());
            MessagesReceiveDelete = int.Parse(rs["MessagesReceiveDelete"].ToString());
            MessagesReadFalse = int.Parse(rs["MessagesReadFalse"].ToString());
            MessagesNoticeFalse = int.Parse(rs["MessagesNoticeFalse"].ToString());
            MessagesTitle = rs["MessagesTitle"].ToString();
            MessagesContent = rs["MessagesContent"].ToString();
            MessagesSystemFalse = int.Parse(rs["MessagesSystemFalse"].ToString());

        }//
        private int _MessagesID = 0;
        public int MessagesID
        {
            get { return _MessagesID; }
            set { _MessagesID = value; }
        }
        private int _MessagesFromUserID = 0;
        public int MessagesFromUserID
        {
            get { return _MessagesFromUserID; }
            set { _MessagesFromUserID = value; }
        }
        private string _MessagesFromUserName;
        public string MessagesFromUserName
        {
            get { return _MessagesFromUserName; }
            set { _MessagesFromUserName = value; }

        }
        private string _MessagesFromUserIP;
        public string MessagesFromUserIP
        {
            get { return _MessagesFromUserIP; }
            set { _MessagesFromUserIP = value; }

        }
        private int _MessagesReceiveUserID;
        public int MessagesReceiveUserID
        {
            get { return _MessagesReceiveUserID; }
            set { _MessagesReceiveUserID = value; }

        }
        private string _MessagesReceiveUserName;
        public string MessagesReceiveUserName
        {
            get { return _MessagesReceiveUserName; }
            set { _MessagesReceiveUserName = value; }

        }
        private string _MessagesReceiveUserIP = string.Empty;
        public string MessagesReceiveUserIP
        {
            get { return _MessagesReceiveUserIP; }
            set { _MessagesReceiveUserIP = value; }

        }

        private DateTime _MessagesCreateTime;
        public DateTime MessagesCreateTime
        {
            get { return _MessagesCreateTime; }
            set { _MessagesCreateTime = value; }

        }
        private int _MessagesFromDelete;
        public int MessagesFromDelete
        {
            get { return _MessagesFromDelete; }
            set { _MessagesFromDelete = value; }

        }
        private int _MessagesReceiveDelete;
        public int MessagesReceiveDelete
        {
            get { return _MessagesReceiveDelete; }
            set { _MessagesReceiveDelete = value; }

        }
        private int _MessagesReadFalse;
        public int MessagesReadFalse
        {
            get { return _MessagesReadFalse; }
            set { _MessagesReadFalse = value; }

        }
        private int _MessagesNoticeFalse;
        public int MessagesNoticeFalse
        {
            get { return _MessagesNoticeFalse; }
            set { _MessagesNoticeFalse = value; }

        }
        private string _MessagesTitle;
        public string MessagesTitle
        {
            get { return _MessagesTitle; }
            set { _MessagesTitle = value; }

        }
        private string _MessagesContent;
        public string MessagesContent
        {
            get { return _MessagesContent; }
            set { _MessagesContent = value; }

        }
        private int _MessagesSystemFalse;
        /// <summary>
        /// 1系统消息，0用户短信
        /// </summary>
        public int MessagesSystemFalse
        {
            get { return _MessagesSystemFalse; }
            set { _MessagesSystemFalse = value; }

        }

    }
}
