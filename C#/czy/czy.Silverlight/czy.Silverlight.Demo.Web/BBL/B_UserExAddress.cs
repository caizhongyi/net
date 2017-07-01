using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class UserExAddress
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.UserExAddress(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.UserExAddress>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.UserExAddress(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.UserExAddress> list=new List<Model.UserExAddress>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.UserExAddress()
              {
                  uea_id =Convert.ToInt32(dr["Model.uea_id"]),
                  uea_type =Convert.ToInt32(dr["Model.uea_type"]),
                  uea_name =Convert.ToString(dr["Model.uea_name"]),
                 uea_address =Convert.ToString(dr["Model.uea_address"])
               });
        }
         return list;
       }

       public static int Insert(Model.UserExAddress UserExAddress)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(UserExAddress, "uea_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.UserExAddress UserExAddress)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(UserExAddress, "uea_id", "uea_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("UserExAddress", new string[] { }, new string[] { }, "uea_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.UserExAddress(), "uea_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
