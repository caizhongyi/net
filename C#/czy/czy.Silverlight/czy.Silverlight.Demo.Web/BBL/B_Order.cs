using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class Order
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Order(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.Order>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Order(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.Order> list=new List<Model.Order>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.Order()
              {
                  o_id =Convert.ToString(dr["Model.o_id"]),
                  o_orderDetailId =Convert.ToString(dr["Model.o_orderDetailId"]),
                  o_state =Convert.ToInt32(dr["Model.o_state"]),
                  o_createDate =Convert.ToDateTime(dr["Model.o_createDate"]),
                  o_startDate =Convert.ToDateTime(dr["Model.o_startDate"]),
                  o_endDate =Convert.ToDateTime(dr["Model.o_endDate"]),
                  o_userId =Convert.ToString(dr["Model.o_userId"]),
                  o_orderUserId =Convert.ToString(dr["Model.o_orderUserId"]),
                  o_fromAddress =Convert.ToString(dr["Model.o_fromAddress"]),
                 o_toAddress =Convert.ToString(dr["Model.o_toAddress"])
               });
        }
         return list;
       }

       public static int Insert(Model.Order Order)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(Order, "o_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.Order Order)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(Order, "o_id", "o_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("Order", new string[] { }, new string[] { }, "o_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.Order(), "o_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
