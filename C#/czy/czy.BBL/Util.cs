using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using czy.MyDAL;
using czy.SQLCommon.DataPagerHelper;
using czy.SQLCommon.Login;

namespace czy.BBL
{
    public class Util
    {
        static readonly string connectString = System.Configuration.ConfigurationSettings.AppSettings["constr"].ToString();
        static IDataBase _IDatabase;
        static IDataBaseAdvance _IDataBaseAdvance;
        static IDataPager _IDataPager;
        static ILogin _ILogin;

        public static  string ConnectString
        {
            get { return Util.connectString; }
        }
        public static DataPagerDALParams PagerDALParams { get; set; }
        public static DataPagerQueryParams PagerQueryParams { get; set; }
        public static IDataBase IDatabase {
            get { return new czy.MyDAL.SQLDataBase(Util.ConnectString, czy.MyDAL.DataBase.ConnStringType.String); } 
        }
        public static IDataBaseAdvance IDataBaseAdvance { 
            get{ return new czy.MyDAL.SQLDataBase(Util.ConnectString, czy.MyDAL.DataBase.ConnStringType.String); } 
        }
        public static IDataPager IDataPager {
            get
            {
                PagerDALParams.ConnString = connectString;
                PagerDALParams.ConnStringType = DataBase.ConnStringType.String;
                return new SqlPagerHelper(PagerQueryParams, PagerDALParams);
            }
        }
        public static ILogin ILogin
        {
            get { return new SqlLogin(connectString, DataBase.ConnStringType.String); }
        }
    }
}
