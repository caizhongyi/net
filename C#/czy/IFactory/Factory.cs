using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using czy.MyDAL;
using czy.SQLAccess.DataPager;
using czy.SQLAccess.Login;

namespace czy.IFactory
{
    public class Factory
    {
        private static string connectString = string.Empty;
        /// <summary>
        /// 链接字符窜
        /// </summary>
        public static string ConnectString
        {
            get { return Factory.connectString; }
            set { Factory.connectString = value; }
        }
        public static IDataBase GetDataBase()
        {
            return new SQLDataBase(connectString,DataBase.ConnStringType.String);
        }
        public static IDataBaseAdvance GetDataBaseAdvance()
        {
            return new SQLDataBase(connectString, DataBase.ConnStringType.String);
        }
        public static IDataPager GetDataPager(DataPagerQueryParams dp)
        {
            DataPagerDALParams p = new DataPagerDALParams();
            p.ConnString = connectString;
            p.ConnStringType = DataBase.ConnStringType.String;
            return new SqlPagerHelper(dp, p);
        }
        public static ILogin GetLogin()
        {
            return new SqlLogin(connectString, DataBase.ConnStringType.String);
        }
    }
}
