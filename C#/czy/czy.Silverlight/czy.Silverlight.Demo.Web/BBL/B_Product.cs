using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class Product
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Product(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.Product>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Product(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.Product> list=new List<Model.Product>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.Product()
              {
                  p_id =Convert.ToString(dr["Model.p_id"]),
                  p_rebateValue =Convert.ToInt32(dr["Model.p_rebateValue"]),
                  P_imageId =Convert.ToInt32(dr["Model.P_imageId"]),
                  p_price =Convert.ToDecimal(dr["Model.p_price"]),
                  p_currentPrice =Convert.ToDecimal(dr["Model.p_currentPrice"]),
                  p_createDate =Convert.ToDateTime(dr["Model.p_createDate"]),
                  p_count =Convert.ToInt64(dr["Model.p_count"]),
                  p_totalCount =Convert.ToInt64(dr["Model.p_totalCount"]),
                  p_needCount =Convert.ToInt64(dr["Model.p_needCount"]),
                  p_typeId =Convert.ToInt64(dr["Model.p_typeId"]),
                  p_brandId =Convert.ToInt64(dr["Model.p_brandId"]),
                  p_materialId =Convert.ToInt64(dr["Model.p_materialId"]),
                  p_sizeId =Convert.ToInt64(dr["Model.p_sizeId"]),
                  p_colorId =Convert.ToInt64(dr["Model.p_colorId"]),
                  p_userId =Convert.ToString(dr["Model.p_userId"]),
                  p_name =Convert.ToString(dr["Model.p_name"]),
                 p_description =Convert.ToString(dr["Model.p_description"])
               });
        }
         return list;
       }

       public static int Insert(Model.Product Product)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(Product, "p_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.Product Product)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(Product, "p_id", "p_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("Product", new string[] { }, new string[] { }, "p_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.Product(), "p_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
