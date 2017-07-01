using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class Product_NewsInfo
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Product_NewsInfo(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.Product_NewsInfo>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Product_NewsInfo(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.Product_NewsInfo> list=new List<Model.Product_NewsInfo>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.Product_NewsInfo()
              {
                  p_n_productId =Convert.ToString(dr["Model.p_n_productId"]),
                  p_n_id =Convert.ToInt64(dr["Model.p_n_id"]),
                 p_n_newsId =Convert.ToInt64(dr["Model.p_n_newsId"])
               });
        }
         return list;
       }

       public static int Insert(Model.Product_NewsInfo Product_NewsInfo)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(Product_NewsInfo, "p_ni_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.Product_NewsInfo Product_NewsInfo)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(Product_NewsInfo, "p_ni_id", "p_ni_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("Product_NewsInfo", new string[] { }, new string[] { }, "p_ni_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.Product_NewsInfo(), "p_ni_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
