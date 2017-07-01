using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class UserBank
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.UserBank(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.UserBank>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.UserBank(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.UserBank> list=new List<Model.UserBank>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.UserBank()
              {
                  ub_number =Convert.ToInt32(dr["Model.ub_number"]),
                  ub_province =Convert.ToInt32(dr["Model.ub_province"]),
                  ub_city =Convert.ToInt32(dr["Model.ub_city"]),
                  ub_town =Convert.ToInt32(dr["Model.ub_town"]),
                  ub_id =Convert.ToInt64(dr["Model.ub_id"]),
                  ub_name =Convert.ToString(dr["Model.ub_name"]),
                  ub_country =Convert.ToString(dr["Model.ub_country"]),
                 ub_branch =Convert.ToString(dr["Model.ub_branch"])
               });
        }
         return list;
       }

       public static int Insert(Model.UserBank UserBank)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(UserBank, "ub_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.UserBank UserBank)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(UserBank, "ub_id", "ub_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("UserBank", new string[] { }, new string[] { }, "ub_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.UserBank(), "ub_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
