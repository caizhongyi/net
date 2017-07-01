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
    public class UserLevel
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            UserLevelID = int.Parse(rs["UserLevelID"].ToString());
            UserLevelTitle = rs["UserLevelTitle"].ToString();
            UserLevelImages = rs["UserLevelImages"].ToString();
            //UserLevelExp = int.Parse(rs["UserLevelExp"].ToString());
            UserLevelPoint = int.Parse(rs["UserLevelPoint"].ToString());
            //UserLevelPost = int.Parse(rs["UserLevelPost"].ToString());
        }//
        private int _UserLevelID;
        public int UserLevelID
        {
            get { return _UserLevelID; }
            set { _UserLevelID = value; }
        }
        private string _UserLevelTitle;
        public string UserLevelTitle
        {
            get { return _UserLevelTitle; }
            set { _UserLevelTitle = value; }
        }
        private string _UserLevelImages;
        public string UserLevelImages
        {
            get { return _UserLevelImages; }
            set { _UserLevelImages = value; }
        }
        //private int _UserLevelExp;
        //public int UserLevelExp
        //{
        //    get { return _UserLevelExp; }
        //    set { _UserLevelExp = value; }
        //}
        private int _UserLevelPoint;
        public int UserLevelPoint
        {
            get { return _UserLevelPoint; }
            set { _UserLevelPoint = value; }
        }
        //private int _UserLevelPost;
        //public int UserLevelPost
        //{
        //    get { return _UserLevelPost; }
        //    set { _UserLevelPost = value; }
        //}

    }
}