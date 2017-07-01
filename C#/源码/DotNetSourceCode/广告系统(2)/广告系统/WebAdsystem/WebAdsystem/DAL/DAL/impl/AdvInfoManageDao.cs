using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;

namespace DAL.impl
{
    class AdvInfoManageDao : IAdvInfoManage
    {
        Util util = new Util();
        #region 广告信息管理

        public bool AddAdvInfo(AdvInfo ai, int masterid, int userid)
        {
            string sqlText = "insert into Adv_Info values('"+ai.Adv_name+"','"+ai.Adv_content+"','"+ai.Adv_url+"',"+masterid+","+userid+","+ai.Adv_operation+","+ai.Adv_clicknumber+",'"+ai.Adv_time+"',"+ai.Adv_discount+",'"+ai.Adv_pay_state+"')";
            int i = util.GetExecuteNonQuery(sqlText);
            return (i > 0) ? true : false;
        }

        public bool DelAdvInfo(int id)
        {
            string sqlText = "delete from Adv_Info where Adv_Id="+id+"";
            int i = util.GetExecuteNonQuery(sqlText);
            return (i > 0) ? true : false;
        }

        public bool ModAdvInfo(AdvInfo ai, int masterid, int userid)
        {
            string sqlText = "update Adv_Info set Adv_Name='" + ai.Adv_name + "',Adv_Content='" + ai.Adv_content + "',Adv_Url='" + ai.Adv_url + "',Adv_Master_Id=" + masterid + ",User_Id=" + userid + ",Adv_Operation=" + ai.Adv_operation + ",Adv_ClickNumber=" + ai.Adv_clicknumber + ",Adv_Time='" + ai.Adv_time + "',Adv_Discount=" + ai.Adv_discount + ",Pay_State="+ai.Adv_pay_state+" where Adv_Id="+ai.Adv_id+"";
            int i = util.GetExecuteNonQuery(sqlText);
            return (i > 0) ? true : false;
        }

        public DataSet SelAdvInfo()
        {
            string sqltext = "select * from Adv_Info";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
