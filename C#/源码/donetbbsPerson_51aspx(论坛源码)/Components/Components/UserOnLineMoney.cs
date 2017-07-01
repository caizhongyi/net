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
    public class UserOnLine
    {
        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            UserOnLineID = int.Parse(rs["UserOnLineID"].ToString());
            UserOnLineUserID = int.Parse(rs["UserOnLineUserID"].ToString());
            UserOnLineLastTime = System.Convert.ToDateTime(rs["UserOnLineLastTime"].ToString());
            UserOnLineUserNickName = rs["UserOnLineUserNickName"].ToString();
            UserOnLineComeFromPath = rs["UserOnLineComeFromPath"].ToString();
            UserOnLineBrowserPath = rs["UserOnLineBrowserPath"].ToString();
            UserOnLineBrowserTitle = rs["UserOnLineBrowserTitle"].ToString();
            UserOnLineSystem = rs["UserOnLineSystem"].ToString();
            UserOnLineIP = rs["UserOnLineIP"].ToString();
        }//
        private int _UserOnLineID;
        public int UserOnLineID
        {
            get { return _UserOnLineID; }
            set { _UserOnLineID = value; }
        }

        private DateTime _UserOnLineLastTime;
        public DateTime UserOnLineLastTime
        {
            get { return _UserOnLineLastTime; }
            set { _UserOnLineLastTime = value; }
        }
        private int _UserOnLineUserID;
        public int UserOnLineUserID
        {
            get { return _UserOnLineUserID; }
            set { _UserOnLineUserID = value; }
        }
        private string _UserOnLineUserNickName;
        public string UserOnLineUserNickName
        {
            get { return _UserOnLineUserNickName; }
            set { _UserOnLineUserNickName = value; }
        }

        private string _UserOnLineComeFromPath;
        public string UserOnLineComeFromPath
        {
            get { return _UserOnLineComeFromPath; }
            set { _UserOnLineComeFromPath = value; }
        }

        private string _UserOnLineBrowserPath;
        public string UserOnLineBrowserPath
        {
            get { return _UserOnLineBrowserPath; }
            set { _UserOnLineBrowserPath = value; }
        }

        private string _UserOnLineBrowserTitle;
        public string UserOnLineBrowserTitle
        {
            get { return _UserOnLineBrowserTitle; }
            set { _UserOnLineBrowserTitle = value; }
        }

        private string _UserOnLineSystem;
        public string UserOnLineSystem
        {
            get { return _UserOnLineSystem; }
            set { _UserOnLineSystem = value; }
        }

        private string _UserOnLineIP;
        public string UserOnLineIP
        {
            get { return _UserOnLineIP; }
            set { _UserOnLineIP = value; }
        }
    }
}
