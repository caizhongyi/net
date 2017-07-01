using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class Country
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Country(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.Country>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Country(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.Country> list=new List<Model.Country>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.Country()
              {
                  c_id =Convert.ToInt32(dr["Model.c_id"]),
                 c_name =Convert.ToString(dr["Model.c_name"])
               });
        }
         return list;
       }

       public static int Insert(Model.Country Country)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(Country, "c_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.Country Country)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(Country, "c_id", "c_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("Country", new string[] { }, new string[] { }, "c_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.Country(), "c_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
