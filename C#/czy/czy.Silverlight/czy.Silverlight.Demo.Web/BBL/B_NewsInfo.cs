using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class NewsInfo
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.NewsInfo(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.NewsInfo>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.NewsInfo(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.NewsInfo> list=new List<Model.NewsInfo>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.NewsInfo()
              {
                  n_newsTypeId =Convert.ToInt32(dr["Model.n_newsTypeId"]),
                  n_createDate =Convert.ToDateTime(dr["Model.n_createDate"]),
                  n_startDate =Convert.ToDateTime(dr["Model.n_startDate"]),
                  n_endDate =Convert.ToDateTime(dr["Model.n_endDate"]),
                  n_id =Convert.ToInt64(dr["Model.n_id"]),
                  n_title =Convert.ToString(dr["Model.n_title"]),
                 n_content =Convert.ToString(dr["Model.n_content"])
               });
        }
         return list;
       }

       public static int Insert(Model.NewsInfo NewsInfo)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(NewsInfo, "ni_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.NewsInfo NewsInfo)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(NewsInfo, "ni_id", "ni_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("NewsInfo", new string[] { }, new string[] { }, "ni_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.NewsInfo(), "ni_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
