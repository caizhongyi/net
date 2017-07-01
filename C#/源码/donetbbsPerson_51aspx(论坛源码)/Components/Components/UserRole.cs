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
    public class UserRole
    {

        private ArrayList _Arraylist = new ArrayList();
        public ArrayList Arraylist
        {
            get { return _Arraylist; }
            set { _Arraylist = value; }
        }
        public void SetDataProviders(DataRow rs)
        {
            UserRoleID = int.Parse(rs["UserRoleID"].ToString());
            UserRoleTitle = rs["UserRoleTitle"].ToString();
            UserRolePostPoint = int.Parse(rs["UserRolePostPoint"].ToString());
            UserRoleRePostPoint = int.Parse(rs["UserRoleRePostPoint"].ToString());
            UserRoleFalse = int.Parse(rs["UserRoleFalse"].ToString());
            UserRolePostMoney = int.Parse(rs["UserRolePostMoney"].ToString());
            UserRolePostExp = int.Parse(rs["UserRolePostExp"].ToString());
            UserRolePostCP = int.Parse(rs["UserRolePostCP"].ToString());
            UserRoleRePostMoney = int.Parse(rs["UserRoleRePostMoney"].ToString());
            UserRoleRePostExp = int.Parse(rs["UserRoleRePostExp"].ToString());
            UserRoleRePostCP = int.Parse(rs["UserRoleRePostCP"].ToString());
            UserRoles = rs["UserRoles"].ToString();

        }//


        private int _UserRoleID;
        public int UserRoleID
        {
            get { return _UserRoleID; }
            set { _UserRoleID = value; }
        }
        private string _UserRoleTitle;
        public string UserRoleTitle
        {
            get { return _UserRoleTitle; }
            set { _UserRoleTitle = value; }
        }
        private int _UserRolePostPoint;
        public int UserRolePostPoint
        {
            get { return _UserRolePostPoint; }
            set { _UserRolePostPoint = value; }
        }
        private int _UserRoleRePostPoint;
        public int UserRoleRePostPoint
        {
            get { return _UserRoleRePostPoint; }
            set { _UserRoleRePostPoint = value; }
        }

        private int _UserRoleFalse;
        public int UserRoleFalse
        {
            get { return _UserRoleFalse; }
            set { _UserRoleFalse = value; }
        }
        private int _UserRolePostMoney;
        public int UserRolePostMoney
        {
            get { return _UserRolePostMoney; }
            set { _UserRolePostMoney = value; }
        }

        private int _UserRolePostExp;
        public int UserRolePostExp
        {
            get { return _UserRolePostExp; }
            set { _UserRolePostExp = value; }
        }

        private int _UserRolePostCP;
        public int UserRolePostCP
        {
            get { return _UserRolePostCP; }
            set { _UserRolePostCP = value; }
        }

        private int _UserRoleRePostMoney;
        public int UserRoleRePostMoney
        {
            get { return _UserRoleRePostMoney; }
            set { _UserRoleRePostMoney = value; }
        }

        private int _UserRoleRePostExp;
        public int UserRoleRePostExp
        {
            get { return _UserRoleRePostExp; }
            set { _UserRoleRePostExp = value; }
        }

        private int _UserRoleRePostCP;
        public int UserRoleRePostCP
        {
            get { return _UserRoleRePostCP; }
            set { _UserRoleRePostCP = value; }
        }
        private string _UserRoles = string.Empty;
        public string UserRoles
        {
            get { return _UserRoles; }
            set { _UserRoles = value; }
        }

    }
}
