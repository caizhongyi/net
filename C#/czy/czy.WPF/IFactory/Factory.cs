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
        static readonly string connectString = System.Configuration.ConfigurationSettings.AppSettings["constr"].ToString();
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
