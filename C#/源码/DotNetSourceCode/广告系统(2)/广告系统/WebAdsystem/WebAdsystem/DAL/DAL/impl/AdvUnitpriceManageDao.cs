using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using System.Data;
using DAL.Model;

namespace DAL.impl
{
    class AdvUnitpriceManageDao : IAdvUnitpriceManage
    {
        Util util = new Util();
        #region 实现广告位价格管理

        public bool AddAdvUnitprice(AdvUnitprice aup, int rk_id, int adv_type_id)
        {
            string sqltext = "insert into Adv_Unit_Price values(" + rk_id + "," + adv_type_id + ","+aup.Unitprice+",'"+aup.Up_remark+"','"+aup.Moditf_time+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelAdvUnitprice(int rk_id)
        {
            string sqltext = "delete from Adv_Unit_Price where rk_id=" + rk_id ;
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModAdvUnitprice(AdvUnitprice aup, int rk_id, int adv_type_id)
        {
            string sqltext = "update Adv_Unit_Price set Unit_Price="+aup.Unitprice+",Remark='"+aup.Up_remark+"' Modify_Time='"+aup.Moditf_time+"' where rk_id="+rk_id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelAdvUnitprice()
        {
            string sqltext = "select * from Adv_Unit_Price";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
