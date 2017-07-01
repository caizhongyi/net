using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL.impl
{
    class WbRankManageDao : IWbRankManage
    {
        Util util = new Util();
        #region 网吧等级管理

        public bool AddRank(WbRank wbrank)
        {
            string sqltext = "insert into Wb_Rank values ('"+wbrank.Rk_name+"','"+wbrank.Rk_remark+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelRank(int id)
        {
            string sqltext = "delete from Wb_Rank where Rk_Id='" + id + "'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModRank(WbRank wbrank)
        {
            string sqltext = "update Wb_Rank set Rk_Name='"+wbrank.Rk_name+"',Rk_Remark='"+wbrank.Rk_remark+"' where Rk_Id='"+wbrank.Rk_id+"'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelRank()
        {
            string sqltext = "select * from Wb_Rank";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
