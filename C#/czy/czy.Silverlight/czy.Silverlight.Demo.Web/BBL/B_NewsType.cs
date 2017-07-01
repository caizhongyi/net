using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class NewsType
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.NewsType(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.NewsType>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.NewsType(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.NewsType> list=new List<Model.NewsType>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.NewsType()
              {
                  nt_id =Convert.ToInt32(dr["Model.nt_id"]),
                  nt_name =Convert.ToString(dr["Model.nt_name"]),
                 nt_remark =Convert.ToString(dr["Model.nt_remark"])
               });
        }
         return list;
       }

       public static int Insert(Model.NewsType NewsType)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(NewsType, "nt_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.NewsType NewsType)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(NewsType, "nt_id", "nt_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("NewsType", new string[] { }, new string[] { }, "nt_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.NewsType(), "nt_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
