using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class Express
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Express(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.Express>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.Express(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.Express> list=new List<Model.Express>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.Express()
              {
                  e_id =Convert.ToInt32(dr["Model.e_id"]),
                  e_price =Convert.ToDecimal(dr["Model.e_price"]),
                  e_createDate =Convert.ToDateTime(dr["Model.e_createDate"]),
                  e_name =Convert.ToString(dr["Model.e_name"]),
                 e_remark =Convert.ToString(dr["Model.e_remark"])
               });
        }
         return list;
       }

       public static int Insert(Model.Express Express)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(Express, "e_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.Express Express)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(Express, "e_id", "e_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("Express", new string[] { }, new string[] { }, "e_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.Express(), "e_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
