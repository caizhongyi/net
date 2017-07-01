using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;

namespace DAL.impl
{
    class AdvTypeManageDao : IAdvTypeManage
    {
        Util util = new Util();
        #region 实现桌面广告位管理

        public bool AddAdvType(AdvType at)
        {
            string sqltext = "insert into Adv_Type values ('"+at.Adv_type_name+"','"+at.Adv_type_remark+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelAdvType(int id)
        {
            string sqltext = "delete from Adv_Type where Adv_Type_Id='"+id+"'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModAdvType(AdvType at)
        {
            string sqltext = "update Adv_Type set Adv_Type_Name='"+at.Adv_type_name+"',Adv_Type_Remark='"+at.Adv_type_remark+"' where Adv_Type_Id='"+at.Adv_type_id+"'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelAdvType()
        {
            string sqltext = "select * from Adv_Type";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
