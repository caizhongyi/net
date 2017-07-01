using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class Rebate
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Rebate(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.Rebate>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Rebate(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.Rebate> list=new List<Model.Rebate>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.Rebate()
              {
                  r_id =Convert.ToInt32(dr["Model.r_id"]),
                  R_userTypeId =Convert.ToInt32(dr["Model.R_userTypeId"]),
                  R_rebateValue =Convert.ToInt32(dr["Model.R_rebateValue"]),
                 R_userId =Convert.ToString(dr["Model.R_userId"])
               });
        }
         return list;
       }

       public static int Insert(Model.Rebate Rebate)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(Rebate, "r_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.Rebate Rebate)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(Rebate, "r_id", "r_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("Rebate", new string[] { }, new string[] { }, "r_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.Rebate(), "r_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
