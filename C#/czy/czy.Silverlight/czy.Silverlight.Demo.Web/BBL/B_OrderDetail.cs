using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class OrderDetail
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.OrderDetail(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.OrderDetail>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.OrderDetail(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.OrderDetail> list=new List<Model.OrderDetail>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.OrderDetail()
              {
                  od_id =Convert.ToString(dr["Model.od_id"]),
                  od_productId =Convert.ToString(dr["Model.od_productId"]),
                 od_remark =Convert.ToString(dr["Model.od_remark"])
               });
        }
         return list;
       }

       public static int Insert(Model.OrderDetail OrderDetail)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(OrderDetail, "od_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.OrderDetail OrderDetail)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(OrderDetail, "od_id", "od_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("OrderDetail", new string[] { }, new string[] { }, "od_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.OrderDetail(), "od_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
