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
    public class Board
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }

        public void SetDataProviders(DataRow rs)
        {
            BoardID = int.Parse(rs["BoardID"].ToString());
            BoardParentID = int.Parse(rs["BoardParentID"].ToString());
            BoardName = rs["BoardName"].ToString();
            BoardTypeID = int.Parse(rs["BoardTypeID"].ToString());
            BoardSubject = rs["BoardSubject"].ToString();
            BoardOrders = int.Parse(rs["BoardOrders"].ToString());
            BoardMaster = rs["BoardMaster"].ToString();
            BoardLastTopicTitle = rs["BoardLastTopicTitle"].ToString();
            if (rs["BoardLastTopicID"].ToString() != string.Empty)
            {
                BoardLastTopicID = int.Parse(rs["BoardLastTopicID"].ToString());
            }
            if (rs["BoardLastTopicTime"].ToString() != string.Empty)
            {
                BoardLastTopicTime = System.Convert.ToDateTime(rs["BoardLastTopicTime"].ToString());
            }
            if (rs["BoardLastTopicUserNickName"].ToString() != string.Empty)
            {
                BoardLastTopicUserNickName = rs["BoardLastTopicUserNickName"].ToString();
            }
            if (rs["BoardLastTopicUserID"].ToString() != string.Empty)
            {
                BoardLastTopicUserID = int.Parse(rs["BoardLastTopicUserID"].ToString());
            }
            BoardFalse = int.Parse(rs["BoardFalse"].ToString());
            BoardImages = rs["BoardImages"].ToString();
            BoardAbout = rs["BoardAbout"].ToString();
            BoardPostNumber = int.Parse(rs["BoardPostNumber"].ToString());
            BoardTodayPostNumber = int.Parse(rs["BoardTodayPostNumber"].ToString());
            BoardTopicNumber = int.Parse(rs["BoardTopicNumber"].ToString());
            BoardDelPoint = int.Parse(rs["BoardDelPoint"].ToString());
            BoardPostRole = rs["BoardPostRole"].ToString();
            BoardRePostRole = rs["BoardRePostRole"].ToString();
            BoardViewRole = rs["BoardViewRole"].ToString();

        }//
        private int _Length = 0;
        public int Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        private int _BoardID;
        public int BoardID
        {
            get { return _BoardID; }
            set { _BoardID = value; }

        }
        private int _BoardParentID;
        public int BoardParentID
        {
            get { return _BoardParentID; }
            set { _BoardParentID = value; }

        }
        private string _BoardName;
        public string BoardName
        {
            get { return _BoardName; }
            set { _BoardName = value; }

        }
        private int _BoardTypeID;
        /// <summary>
        /// 等于1为可以发布文章，0为不能发布文章
        /// </summary>
        public int BoardTypeID
        {
            get { return _BoardTypeID; }
            set { _BoardTypeID = value; }

        }
        private string _BoardSubject = string.Empty;
        public string BoardSubject
        {
            get { return _BoardSubject; }
            set { _BoardSubject = value; }

        }

        private int _BoardOrders;
        public int BoardOrders
        {
            get { return _BoardOrders; }
            set { _BoardOrders = value; }

        }
        private string _BoardMaster;
        public string BoardMaster
        {
            get { return _BoardMaster; }
            set { _BoardMaster = value; }

        }
        private string _BoardLastTopicTitle;
        public string BoardLastTopicTitle
        {
            get { return _BoardLastTopicTitle; }
            set { _BoardLastTopicTitle = value; }

        }
        private int _BoardLastTopicID;
        public int BoardLastTopicID
        {
            get { return _BoardLastTopicID; }
            set { _BoardLastTopicID = value; }

        }
        private DateTime _BoardLastTopicTime = DateTime.Now;
        public DateTime BoardLastTopicTime
        {
            get { return _BoardLastTopicTime; }
            set { _BoardLastTopicTime = value; }

        }
        private string _BoardLastTopicUserNickName = string.Empty;
        public string BoardLastTopicUserNickName
        {
            get { return _BoardLastTopicUserNickName; }
            set { _BoardLastTopicUserNickName = value; }

        }
        private int _BoardLastTopicUserID = 0;
        public int BoardLastTopicUserID
        {
            get { return _BoardLastTopicUserID; }
            set { _BoardLastTopicUserID = value; }

        }
        private int _BoardFalse;
        public int BoardFalse
        {
            get { return _BoardFalse; }
            set { _BoardFalse = value; }

        }
        private string _BoardImages;
        public string BoardImages
        {
            get { return _BoardImages; }
            set { _BoardImages = value; }

        }
        private string _BoardAbout;
        public string BoardAbout
        {
            get { return _BoardAbout; }
            set { _BoardAbout = value; }

        }
        private int _BoardPostNumber;
        public int BoardPostNumber
        {
            get { return _BoardPostNumber; }
            set { _BoardPostNumber = value; }

        }
        private int _BoardTodayPostNumber;
        public int BoardTodayPostNumber
        {
            get { return _BoardTodayPostNumber; }
            set { _BoardTodayPostNumber = value; }

        }
        private int _BoardTopicNumber;
        public int BoardTopicNumber
        {
            get { return _BoardTopicNumber; }
            set { _BoardTopicNumber = value; }

        }
        private int _BoardDelPoint;
        public int BoardDelPoint
        {
            get { return _BoardDelPoint; }
            set { _BoardDelPoint = value; }

        }
        private string _BoardPostRole;
        public string BoardPostRole
        {
            get { return _BoardPostRole; }
            set { _BoardPostRole = value; }

        }
        private string _BoardRePostRole;
        public string BoardRePostRole
        {
            get { return _BoardRePostRole; }
            set { _BoardRePostRole = value; }

        }
        private string _BoardViewRole;
        public string BoardViewRole
        {
            get { return _BoardViewRole; }
            set { _BoardViewRole = value; }

        }

    }
}
