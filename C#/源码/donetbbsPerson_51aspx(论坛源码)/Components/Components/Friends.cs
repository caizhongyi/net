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
    public class Friends
    {
        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            FriendID = int.Parse(rs["FriendID"].ToString());
            FriendMyUserID = int.Parse(rs["FriendMyUserID"].ToString());
            FriendFriendUserID = int.Parse(rs["FriendFriendUserID"].ToString());
            FriendCreateTime = System.Convert.ToDateTime(rs["FriendCreateTime"].ToString());
            FriendType = int.Parse(rs["FriendType"].ToString());
            FriendContent = rs["FriendContent"].ToString();
        }//
        private int _FriendID = 0;
        public int FriendID
        {
            get { return _FriendID; }
            set { _FriendID = value; }
        }
        private int _FriendMyUserID = 0;
        public int FriendMyUserID
        {
            get { return _FriendMyUserID; }
            set { _FriendMyUserID = value; }
        }
        private int _FriendFriendUserID;
        public int FriendFriendUserID
        {
            get { return _FriendFriendUserID; }
            set { _FriendFriendUserID = value; }
        }
        private DateTime _FriendCreateTime;
        public DateTime FriendCreateTime
        {
            get { return _FriendCreateTime; }
            set { _FriendCreateTime = value; }
        }
        private int _FriendType;
        public int FriendType
        {
            get { return _FriendType; }
            set { _FriendType = value; }
        }
        private string _FriendContent;
        public string FriendContent
        {
            get { return _FriendContent; }
            set { _FriendContent = value; }
        }
    }
}
