using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class ProductImages
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.ProductImages(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.ProductImages>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.ProductImages(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.ProductImages> list=new List<Model.ProductImages>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.ProductImages()
              {
                  pi_id =Convert.ToInt32(dr["Model.pi_id"]),
                  pi_title =Convert.ToString(dr["Model.pi_title"]),
                  pi_url =Convert.ToString(dr["Model.pi_url"]),
                 pi_remark =Convert.ToString(dr["Model.pi_remark"])
               });
        }
         return list;
       }

       public static int Insert(Model.ProductImages ProductImages)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(ProductImages, "pi_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.ProductImages ProductImages)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(ProductImages, "pi_id", "pi_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("ProductImages", new string[] { }, new string[] { }, "pi_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.ProductImages(), "pi_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
