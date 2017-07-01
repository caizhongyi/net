using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Company.BBL
{
    public class NewsInfo
    {
    
        public static DataSet Select()
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.NewsInfo(), "n_isdel='False' and n_isshow='true' order by n_createDate desc");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }
        public static DataSet Select(int menuId)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.NewsInfo(), "n_typeId=" + menuId + " and  n_isdel='false' and n_isshow='true' order by n_createDate desc");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }
        public static DataSet Select(int menuId,int count)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.NewsInfo(), " top " + count + " * ", "n_typeId=" + menuId + " and  n_isdel='false' and n_isshow='true' order by n_createDate desc");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }
        public static DataSet GetSubItemNews(int menuId)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL("newsinfo,typeInfo", " * ", "n_typeId in (select t_id from typeInfo where t_parentId=" + menuId + " and t_isdel='false') and  n_isdel='false' and n_isshow='true' and t_id=n_typeId order by n_createDate desc");
            string sql1 = "select * from typeinfo where t_parentId=" + menuId + " and t_isdel='false'";
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql+sql1);

        }
        public static int Insert(Company.Models.NewsInfo m_newsInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_newsInfo, "n_id");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Company.Models.NewsInfo m_newsInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_newsInfo,"n_id","n_id="+id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int ShowAndHidden(int id,bool isShow)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("newsInfo", new string[]{"n_isshow"}, new string[]{isShow.ToString()}, "n_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int Delete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("newsInfo", new string[]{"n_isdel"}, new string[]{"'false'"}, "n_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Company.Models.NewsInfo(), "n_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static DataSet GetCurrentNews(int cur, int size, out int totalCount,out int pageCount)
        {
            MyClass.PageHelper.SqlPagerHelper sp = new MyClass.PageHelper.SqlPagerHelper("*", "n_createDate", "desc", "newsinfo", "n_isdel='false' and n_isshow='true'", size, "", Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            DataSet ds= sp.GetCurrentPageDataBy2005(cur+1,out totalCount);
            pageCount = sp.PageCount;
            return ds;
        }
        public static DataSet GetCurrentNews(int cur, int size,int menuId, out int totalCount, out int pageCount)
        {
            MyClass.PageHelper.SqlPagerHelper sp = new MyClass.PageHelper.SqlPagerHelper("*", "n_createDate", "desc", "newsinfo", "n_isdel='false' and n_isshow='true' and n_typeId=" + menuId, size, "", Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            DataSet ds = sp.GetCurrentPageDataBy2005(cur + 1, out totalCount);
            pageCount = sp.PageCount;
            return ds;
        }
    }
}
