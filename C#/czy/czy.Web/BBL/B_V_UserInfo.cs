using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using czy.IFactory;
using czy.MyClass.CommandHelper;
 
namespace BBL
{
    public class V_UserInfo
    {
       #region SelectData
       /// <summary>
       /// 查询
       /// </summary>
       public static DataSet Select()
       {
         string sql = SQLCommandBuilder.GetSelectSQL(new Models.V_UserInfo(), "");
         return Factory.GetDataBase().GetDataSet(sql);
       }
       /// <summary>
       /// 查询
       /// </summary>
       public static List<Models.V_UserInfo> SelectList()
       {
         return Select().Tables[0].TableToList<Models.V_UserInfo>();
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">跟据ID查询</param>
       public static DataSet Select(object id)
       {
         string sql = SQLCommandBuilder.GetSelectSQL(new Models.V_UserInfo(), string .Format ( "v_ui_id={0}",id.ToString()));
         return Factory.GetDataBase().GetDataSet(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">跟据ID查询</param>
       public static List<Models.V_UserInfo> SelectList(object id)
       {
          return Select(id).Tables[0].TableToList<Models.V_UserInfo>();
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columnParams">列用,号隔开</param>
       /// <param name="filter">条件</param>
       public static DataSet Select(string columnParams, string filter)
       {
         string sql = SQLCommandBuilder.GetSelectSQL("V_UserInfo",columnParams,filter);
         return Factory.GetDataBase().GetDataSet(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="columnParams">列用,号隔开</param>
       /// <param name="filter">条件</param>
       public static List<Models.V_UserInfo>  SelectList(string columnParams, string filter)
       {
         return Select( columnParams,  filter).Tables[0].TableToList<Models.V_UserInfo> ();
       }

       #endregion

       #region Execute
       /// <summary>
       /// 插入
       /// </summary>
       /// <param name="Models.V_UserInfo">模型</param>
       public static int Insert(Models.V_UserInfo V_UserInfo)
       {
         string sql = SQLCommandBuilder.GetInsertSQL(V_UserInfo, "v_ui_id");
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="id">id</param>
       /// <param name="Mode.V_UserInfo">模型</param>
       public static int Update(object id,Models.V_UserInfo V_UserInfo)
       {
         string sql = SQLCommandBuilder.GetUpdateSQL(V_UserInfo, "v_ui_id", "v_ui_id=" + id.ToString());
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
         string sql = SQLCommandBuilder.GetUpdateSQL("V_UserInfo", columns, values,filter);
         return Factory.GetDataBase().ExecuteNonQuery(sql);
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id">id</param>
       public static int Delete(object id)
       {
         string sql = SQLCommandBuilder.GetDelSQL(new Models.V_UserInfo(), "v_ui_id=" + id);
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
         Factory.PagerQueryParams.Size = size;
         Factory.PagerQueryParams.TableName = "V_UserInfo";
         Factory.PagerQueryParams.TableId = "v_ui_id";
         //Util.PagerQueryParams.Order = "Order by u_createDate";
         Factory.PagerQueryParams.KeyFieldOrder = "asc";
         Factory.PagerQueryParams.Colums = "*";
         DataSet ds = Factory.GetDataPager().GetCurrentPageData(cur);
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
         Factory.PagerQueryParams.Size = size;
         Factory.PagerQueryParams.TableName = "V_UserInfo";
         Factory.PagerQueryParams.TableId = "v_ui_id";
         Factory.PagerQueryParams.Order = order;
         Factory.PagerQueryParams.KeyFieldOrder = keyFieldOrder;
         Factory.PagerQueryParams.Colums = columes;
         DataSet ds = Factory.GetDataPager().GetCurrentPageData(cur);
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
       public static List<Models.V_UserInfo> GetListCurrentData(int cur, int size)
       {
          return GetCurrentData( cur,  size).Tables[0].TableToList<Models.V_UserInfo>();
       }

       /// <summary>
       /// 查询当前页数据
       /// </summary>
       /// <param name="cur">当前页</param>
       /// <param name="size">当前页大小</param>
       /// <param name="order">当前页大小</param>
       /// <param name="keyFieldOrder">ID列排序方式[asc][desc]</param>
       /// <param name="columes">查询的列可为*</param>
       public static List<Models.V_UserInfo> GetListCurrentData(int cur, int size,string order,string keyFieldOrder,string columes)
       {
          return GetCurrentData( cur,  size, order, keyFieldOrder, columes).Tables[0].TableToList<Models.V_UserInfo>();
       }

       /// <summary>
       /// 查询总条数
       /// </summary>
       /// <param name="size">当前大小</param>
       public static long GetTotleCount(int size)
       {
         Factory.PagerQueryParams.Size = size;
         Factory.PagerQueryParams.TableName = "V_UserInfo";
         Factory.PagerQueryParams.TableId = "v_ui_id";
         //Util.PagerQueryParams.Order = "u_createDate asc";
         Factory.PagerQueryParams.KeyFieldOrder = "asc";
         Factory.PagerQueryParams.Colums = "*";
         return Factory.GetDataPager().GetTotalCount();
       }
       #endregion
    }
}
