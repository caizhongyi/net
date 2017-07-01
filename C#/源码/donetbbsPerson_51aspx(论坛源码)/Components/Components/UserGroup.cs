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
    public class UserGroup
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            UserGroupID = int.Parse(rs["UserGroupID"].ToString());
            UserGroupTitle = rs["UserGroupTitle"].ToString();
            UserGroupPayMoney = int.Parse(rs["UserGroupPayMoney"].ToString());
            UserGroupUserID = int.Parse(rs["UserGroupUserID"].ToString());
            UserGroupFalse = int.Parse(rs["UserGroupFalse"].ToString());
            UserGroupAbout = rs["UserGroupAbout"].ToString();
            UserGroupPoint = int.Parse(rs["UserGroupPoint"].ToString());
            UserGroupUserList = rs["UserGroupUserList"].ToString();


        }//

        private int _UserGroupID;
        public int UserGroupID
        {
            get { return _UserGroupID; }
            set { _UserGroupID = value; }
        }
        private string _UserGroupTitle;
        public string UserGroupTitle
        {
            get { return _UserGroupTitle; }
            set { _UserGroupTitle = value; }
        }
        private int _UserGroupPayMoney;
        public int UserGroupPayMoney
        {
            get { return _UserGroupPayMoney; }
            set { _UserGroupPayMoney = value; }
        }
        private int _UserGroupUserID;
        public int UserGroupUserID
        {
            get { return _UserGroupUserID; }
            set { _UserGroupUserID = value; }
        }
        private int _UserGroupFalse;
        public int UserGroupFalse
        {
            get { return _UserGroupFalse; }
            set { _UserGroupFalse = value; }
        }
        private string _UserGroupAbout;
        public string UserGroupAbout
        {
            get { return _UserGroupAbout; }
            set { _UserGroupAbout = value; }
        }
        private int _UserGroupPoint;
        public int UserGroupPoint
        {
            get { return _UserGroupPoint; }
            set { _UserGroupPoint = value; }
        }
        private string _UserGroupUserList;
        public string UserGroupUserList
        {
            get { return _UserGroupUserList; }
            set { _UserGroupUserList = value; }
        }
    }
}
