using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
 
namespace czy.shop.BBL
{
    public class UserInfo
    {
       public static DataSet Select()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.UserInfo(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.GetDataSet(sql);
       }

       public static List<Model.UserInfo>  SelectToList()
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Model.UserInfo(), "");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         DataSet ds = idb.GetDataSet(sql);
         List<Model.UserInfo> list=new List<Model.UserInfo>();
         foreach (DataRow dr in ds.Tables[0].Rows)
         {
              list.Add(new Model.UserInfo()
              {
                  u_typeId =Convert.ToInt32(dr["Model.u_typeId"]),
                  u_province =Convert.ToInt32(dr["Model.u_province"]),
                  u_city =Convert.ToInt32(dr["Model.u_city"]),
                  u_town =Convert.ToInt32(dr["Model.u_town"]),
                  u_qq =Convert.ToInt32(dr["Model.u_qq"]),
                  u_bussinessType =Convert.ToInt32(dr["Model.u_bussinessType"]),
                  u_loginTime =Convert.ToInt32(dr["Model.u_loginTime"]),
                  u_loginMaxTime =Convert.ToInt32(dr["Model.u_loginMaxTime"]),
                  u_state =Convert.ToInt32(dr["Model.u_state"]),
                  u_money =Convert.ToDecimal(dr["Model.u_money"]),
                  u_birthday =Convert.ToDateTime(dr["Model.u_birthday"]),
                  u_createDate =Convert.ToDateTime(dr["Model.u_createDate"]),
                  u_loginDate =Convert.ToDateTime(dr["Model.u_loginDate"]),
                  u_bankId =Convert.ToInt64(dr["Model.u_bankId"]),
                  u_id =Convert.ToString(dr["Model.u_id"]),
                  u_name =Convert.ToString(dr["Model.u_name"]),
                  u_address =Convert.ToString(dr["Model.u_address"]),
                  u_tel =Convert.ToString(dr["Model.u_tel"]),
                  u_email =Convert.ToString(dr["Model.u_email"]),
                 u_sex =Convert.ToString(dr["Model.u_sex"])
               });
        }
         return list;
       }

       public static int Insert(Model.UserInfo UserInfo)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(UserInfo, "ui_id");
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Update(int id,Model.UserInfo UserInfo)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(UserInfo, "ui_id", "ui_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int Delete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("UserInfo", new string[] { }, new string[] { }, "ui_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

       public static int RealDelete(int id)
       {
         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Model.UserInfo(), "ui_id=" + id);
         MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
         return idb.ExecuteNonQuery(sql);
       }

    }
}
