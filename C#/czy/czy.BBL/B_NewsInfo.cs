using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using czy.IFactory;
using czy.MyClass.CommandHelper;
 
namespace BBL
{
    public class NewsInfo
    {
       #region SelectData
       /// <summary>
       /// 查询
       /// </summary>
       public static DataSet Select()
       {
         string sql = SQLCommandBuilder.GetSelectSQL(new Models.NewsInfo(), "");
         return Factory.GetDataBase().GetDataSet(sql);
       }
       /// <summary>
       /// 查询
       /// </summary>
       public static List<Models.NewsInfo> SelectList()
       {
         return Select().Tables[0].TableToList<Models.NewsInfo>();
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">跟据ID查询</param>
       public static DataSet Select(object id)
       {
         string sql = SQLCommandBuilder.GetSelectSQL(new Models.NewsInfo(), string .Format ( "n_id={0}",id.ToString()));
         return Factory.GetDataBase().GetDataSet(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">跟据ID查询</param>
       public static List<Models.NewsInfo> SelectList(object id)
       {
          return Select(id).Tables[0].TableToList<Models.NewsInfo>();
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columnParams">列用,号隔开</param>
       /// <param name="filter">条件</param>
       public static DataSet Select(string columnParams, string filter)
       {
         string sql = SQLCommandBuilder.GetSelectSQL("NewsInfo",columnParams,filter);
         return Factory.GetDataBase().GetDataSet(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columnParams">列用,号隔开</param>
       /// <param name="filter">条件</param>
       public static List<Models.NewsInfo>  SelectList(string columnParams, string filter)
       {
         return Select( columnParams,  filter).Tables[0].TableToList<Models.NewsInfo> ();
       }

       #endregion

       #region Execute
       /// <summary>
       /// 插入
       /// </summary>
       /// <param name="Models.NewsInfo">模型</param>
       public static int Insert(Models.NewsInfo NewsInfo)
       {
         string sql = SQLCommandBuilder.GetInsertSQL(NewsInfo, "n_id");
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">id</param>
       /// <param name="Mode.NewsInfo">模型</param>
       public static int Update(object id,Models.NewsInfo NewsInfo)
       {
         string sql = SQLCommandBuilder.GetUpdateSQL(NewsInfo, "n_id", "n_id=" + id.ToString());
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columns">列</param>
       /// <param name="values">值</param>
       /// <param name="filter">条件</param>
       public static int Update(string[] columns,string[] values,string filter)
       {
         string sql = SQLCommandBuilder.GetUpdateSQL("NewsInfo", columns, values,filter);
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id">id</param>
       public static int Delete(object id)
       {
         string sql = SQLCommandBuilder.GetDelSQL(new Models.NewsInfo(), "n_id=" + id);
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }


       #endregion

       #region 查询当前页数据
       /// <summary>
       /// 查询当前页数据
       /// </summary>
       /// <param name="cur">当前页</param>
       /// <param name="size">当前页大小</param>
       public static DataSet GetCurrentData(int cur, int size)
       {
        
czy.SQLCommon.DataPagerHelper.DataPagerQueryParams PagerQueryParams = new czy.SQLCommon.DataPagerHelper.DataPagerQueryParams();PagerQueryParams.Size = size;
         PagerQueryParams.TableName = "NewsInfo";
         PagerQueryParams.TableId = "n_id";
         //Util.PagerQueryParams.Order = "Order by u_createDate";
         PagerQueryParams.KeyFieldOrder = "asc";
         PagerQueryParams.Colums = "*";
         DataSet ds = Factory.GetDataPager(PagerQueryParams).GetCurrentPageData(cur);
         return ds;
       }

       /// <summary>
       /// 查询当前页数据
       /// </summary>
       /// <param name="cur">当前页</param>
       /// <param name="size">当前页大小</param>
       /// <param name="order">当前页大小</param>
       /// <param name="keyFieldOrder">ID列排序方式[asc][desc]</param>
       /// <param name="columes">查询的列可为*</param>
       public static DataSet GetCurrentData(int cur, int size, string order, string keyFieldOrder, string columes, string filter)
       {

 czy.SQLCommon.DataPagerHelper.DataPagerQueryParams PagerQueryParams = new czy.SQLCommon.DataPagerHelper.DataPagerQueryParams();PagerQueryParams.Size = size;
         PagerQueryParams.TableName = "NewsInfo";
         PagerQueryParams.TableId = "n_id";
         PagerQueryParams.Order = order;
         PagerQueryParams.KeyFieldOrder = keyFieldOrder;
         PagerQueryParams.Colums = columes;
         PagerQueryParams.Filter = filter;
         DataSet ds = Factory.GetDataPager(PagerQueryParams).GetCurrentPageData(cur);
         return ds;
       }

       /// <summary>
       /// 查询当前页数据
       /// </summary>
       /// <param name="cur">当前页</param>
       /// <param name="size">当前页大小</param>
       /// <param name="order">当前页大小</param>
       /// <param name="keyFieldOrder">ID列排序方式[asc][desc]</param>
       /// <param name="columes">查询的列可为*</param>
       public static List<Models.NewsInfo> GetListCurrentData(int cur, int size)
       {
          return GetCurrentData( cur,  size).Tables[0].TableToList<Models.NewsInfo>();
       }

       /// <summary>
       /// 查询当前页数据
       /// </summary>
       /// <param name="cur">当前页</param>
       /// <param name="size">当前页大小</param>
       /// <param name="order">当前页大小</param>
       /// <param name="keyFieldOrder">ID列排序方式[asc][desc]</param>
       /// <param name="columes">查询的列可为*</param>
       public static List<Models.NewsInfo> GetListCurrentData(int cur, int size,string order,string keyFieldOrder,string columes,string filter)
       {
           DataTable dt=GetCurrentData(cur, size, order, keyFieldOrder, columes, filter).Tables[0];
           return dt.TableToList<Models.NewsInfo>();
       }

       /// <summary>
       /// 查询总条数
       /// </summary>
       /// <param name="size">当前大小</param>
       public static long GetTotleCount(int size)
       {

         czy.SQLCommon.DataPagerHelper.DataPagerQueryParams PagerQueryParams = new czy.SQLCommon.DataPagerHelper.DataPagerQueryParams();PagerQueryParams.Size = size;
         PagerQueryParams.TableName = "NewsInfo";
         PagerQueryParams.TableId = "n_id";
         //Util.PagerQueryParams.Order = "u_createDate asc";
         PagerQueryParams.KeyFieldOrder = "asc";
         PagerQueryParams.Colums = "*";
         return Factory.GetDataPager(PagerQueryParams).GetTotalCount();
       }

       /// <summary>
       /// 查询总条数
       /// </summary>
       /// <param name="size">当前大小</param>
       /// <param name="filter">条件</param>
       public static long GetTotleCount(int size,string filter)
       {

           czy.SQLCommon.DataPagerHelper.DataPagerQueryParams PagerQueryParams = new czy.SQLCommon.DataPagerHelper.DataPagerQueryParams(); PagerQueryParams.Size = size;
           PagerQueryParams.TableName = "NewsInfo";
           PagerQueryParams.TableId = "n_id";
           //Util.PagerQueryParams.Order = "u_createDate asc";
           PagerQueryParams.KeyFieldOrder = "asc";
           PagerQueryParams.Colums = "*";
           PagerQueryParams.Filter = filter;
           return Factory.GetDataPager(PagerQueryParams).GetTotalCount();
       }
       #endregion
    }
}
