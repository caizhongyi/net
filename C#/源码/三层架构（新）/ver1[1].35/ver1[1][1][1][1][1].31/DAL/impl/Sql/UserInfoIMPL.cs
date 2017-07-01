using System;
using System.Collections.Generic;
using System.Text;
using DAL.dao;
using DAL.model;
using System.Data.SqlClient;
using System.Data;

namespace DAL.impl.Sql
{
    class UserInfoIMPL:IUserInfo
    {
        public UserInfo loadByUserNameAndPassword(string username, string password)
        {
            SqlConnection conn = Utils.CreateConnection();
            string sql = "select * from UserInfo where username='" + username + "' and password='" + password + "' ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            UserInfo u1 = new UserInfo();
            if (dt.Rows.Count > 0)
            {
                u1.Username = dt.Rows[0]["Username"].ToString();
            }
            return u1;
        }
    }
}
