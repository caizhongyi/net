using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using czy.IFactory;
using czy.MyClass.CommandHelper;
 
namespace BBL
{
    public class NewsType
    {
       #region SelectData
       /// <summary>
       /// 查询
       /// </summary>
       public static DataSet Select()
       {
         string sql = SQLCommandBuilder.GetSelectSQL(new Models.NewsType(), "");
         return Factory.GetDataBase().GetDataSet(sql);
       }
       /// <summary>
       /// 查询
       /// </summary>
       public static List<Models.NewsType> SelectList()
       {
         return Select().Tables[0].TableToList<Models.NewsType>();
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">跟据ID查询</param>
       public static DataSet Select(object id)
       {
         string sql = SQLCommandBuilder.GetSelectSQL(new Models.NewsType(), string .Format ( "nt_id={0}",id.ToString()));
         return Factory.GetDataBase().GetDataSet(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">跟据ID查询</param>
       public static List<Models.NewsType> SelectList(object id)
       {
          return Select(id).Tables[0].TableToList<Models.NewsType>();
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columnParams">列用,号隔开</param>
       /// <param name="filter">条件</param>
       public static DataSet Select(string columnParams, string filter)
       {
         string sql = SQLCommandBuilder.GetSelectSQL("NewsType",columnParams,filter);
         return Factory.GetDataBase().GetDataSet(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columnParams">列用,号隔开</param>
       /// <param name="filter">条件</param>
       public static List<Models.NewsType>  SelectList(string columnParams, string filter)
       {
         return Select( columnParams,  filter).Tables[0].TableToList<Models.NewsType> ();
       }

       #endregion

       #region Execute
       /// <summary>
       /// 插入
       /// </summary>
       /// <param name="Models.NewsType">模型</param>
       public static int Insert(Models.NewsType NewsType)
       {
         string sql = SQLCommandBuilder.GetInsertSQL(NewsType, "nt_id");
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">id</param>
       /// <param name="Mode.NewsType">模型</param>
       public static int Update(object id,Models.NewsType NewsType)
       {
         string sql = SQLCommandBuilder.GetUpdateSQL(NewsType, "nt_id", "nt_id=" + id.ToString());
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
         string sql = SQLCommandBuilder.GetUpdateSQL("NewsType", columns, values,filter);
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id">id</param>
       public static int Delete(object id)
       {
         string sql = SQLCommandBuilder.GetDelSQL(new Models.NewsType(), "nt_id=" + id);
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
         PagerQueryParams.TableName = "NewsType";
         PagerQueryParams.TableId = "nt_id";
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
       public static DataSet GetCurrentData(int cur, int size,string order,string keyFieldOrder,string columes)
       {
         czy.SQLCommon.DataPagerHelper.DataPagerQueryParams PagerQueryParams = new czy.SQLCommon.DataPagerHelper.DataPagerQueryParams();PagerQueryParams.Size = size;
         PagerQueryParams.TableName = "NewsType";
         PagerQueryParams.TableId = "nt_id";
         PagerQueryParams.Order = order;
         PagerQueryParams.KeyFieldOrder = keyFieldOrder;
         PagerQueryParams.Colums = columes;
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
       public static List<Models.NewsType> GetListCurrentData(int cur, int size)
       {
          return GetCurrentData( cur,  size).Tables[0].TableToList<Models.NewsType>();
       }

       /// <summary>
       /// 查询当前页数据
       /// </summary>
       /// <param name="cur">当前页</param>
       /// <param name="size">当前页大小</param>
       /// <param name="order">当前页大小</param>
       /// <param name="keyFieldOrder">ID列排序方式[asc][desc]</param>
       /// <param name="columes">查询的列可为*</param>
       public static List<Models.NewsType> GetListCurrentData(int cur, int size,string order,string keyFieldOrder,string columes)
       {
          return GetCurrentData( cur,  size, order, keyFieldOrder, columes).Tables[0].TableToList<Models.NewsType>();
       }

       /// <summary>
       /// 查询总条数
       /// </summary>
       /// <param name="size">当前大小</param>
       public static long GetTotleCount(int size)
       {
         
         czy.SQLCommon.DataPagerHelper.DataPagerQueryParams PagerQueryParams = new czy.SQLCommon.DataPagerHelper.DataPagerQueryParams();PagerQueryParams.Size = size;
         PagerQueryParams.TableName = "NewsType";
         PagerQueryParams.TableId = "nt_id";
         //Util.PagerQueryParams.Order = "u_createDate asc";
         PagerQueryParams.KeyFieldOrder = "asc";
         PagerQueryParams.Colums = "*";
         return Factory.GetDataPager(PagerQueryParams).GetTotalCount();
       }
       #endregion
    }
}
