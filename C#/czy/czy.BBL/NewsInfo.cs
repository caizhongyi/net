using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using czy;

namespace czy.BBL
{
    public class NewsInfo
    {

        public static DataSet Select()
        {
            string sql =czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.NewsInfo(), "n_isdel='False' and n_isshow='true' order by n_createDate desc");
            return Util.IDatabase.GetDataSet(sql);

        }
        public static DataSet Select(int menuId)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.NewsInfo(), "n_typeId=" + menuId + " and  n_isdel='false' and n_isshow='true' order by n_createDate desc");
            return Util.IDatabase.GetDataSet(sql);

        }
        public static DataSet Select(int menuId,int count)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.NewsInfo(), " top " + count + " * ", "n_typeId=" + menuId + " and  n_isdel='false' and n_isshow='true' order by n_createDate desc");
            return Util.IDatabase.GetDataSet(sql);

        }
        public static DataSet GetSubItemNews(int menuId)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL("newsinfo,typeInfo", " * ", "n_typeId in (select t_id from typeInfo where t_parentId=" + menuId + " and t_isdel='false') and  n_isdel='false' and n_isshow='true' and t_id=n_typeId order by n_createDate desc");
            string sql1 = "select * from typeinfo where t_parentId=" + menuId + " and t_isdel='false'";
            return Util.IDatabase.GetDataSet(sql + sql1);

        }
        public static int Insert(Models.NewsInfo m_newsInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_newsInfo, "n_id");
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Models.NewsInfo m_newsInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_newsInfo,"n_id","n_id="+id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int Update(int id, bool isShow)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("newsInfo", new string[] { "n_isshow" }, new string[]{ isShow.ToString () }, "n_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int ShowAndHidden(int id,bool isShow)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("newsInfo", new string[]{"n_isshow"}, new string[]{isShow.ToString()}, "n_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int Delete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("newsInfo", new string[]{"n_isdel"}, new string[]{"'false'"}, "n_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Models.NewsInfo(), "n_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static DataSet GetCurrentData(int cur, int size)
        {
            Util.PagerQueryParams.Size = size;
            Util.PagerQueryParams.TableName = "NewsInfo";
            Util.PagerQueryParams.TableId = "u_id";
            Util.PagerQueryParams.Order = " u_createDate asc";
            Util.PagerQueryParams.KeyFieldOrder = "asc";
            Util.PagerQueryParams.Colums = "*";
            DataSet ds = Util.IDataPager.GetCurrentPageData(cur);
            return ds;
        }
        public static long GetTotalCount()
        {  
            Util.PagerQueryParams.Size = 10;
            Util.PagerQueryParams.TableName = "NewsInfo";
            Util.PagerQueryParams.TableId = "u_id";
            //Util.PagerQueryParams.Order = "asc";
            Util.PagerQueryParams.KeyFieldOrder = "asc";
            Util.PagerQueryParams.Colums = "*";
            return Util.IDataPager.GetTotalCount();
        }
   
    }
}
