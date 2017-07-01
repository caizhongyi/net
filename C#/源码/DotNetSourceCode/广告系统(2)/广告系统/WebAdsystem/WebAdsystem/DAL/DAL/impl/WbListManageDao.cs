using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using System.Data;
using DAL.Model;

namespace DAL.impl
{
    class WbListManageDao : IWbListManage
    {
        Util util = new Util();
        #region 网吧信息管理

        public bool AddWbList(WbList wl, int rankid, int userid, int masterid, int areaid)
        {
            string sqltext = "insert into Wb_List values('"+wl.Wb_name+"',"+wl.Wb_c_number+",'"+wl.Wb_ip+"',"+areaid+","+rankid+","+userid+","+masterid+",'"+wl.Wb_time+"','"+wl.Wb_remark+"','"+wl.Wb_postalcode+"','"+wl.Wb_address+"','"+wl.Wb_tel1+"','"+wl.Wb_tel2+"','"+wl.Wb_fax+"','"+wl.Wb_email1+"','"+wl.Wb_email2+"','"+wl.Wb_qq1+"','"+wl.Wb_qq2+"','"+wl.Wb_manager+"','"+wl.Wb_connect+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelWbList(int id)
        {
            string sqltext = "delete from Wb_List where Wb_Id="+id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModWbList(WbList wl, int rankid, int userid, int masterid, int areaid)
        {
            string sqltext = "update Wb_List set Wb_Name='"+wl.Wb_name+"',Wb_C_Number='"+wl.Wb_c_number+"',Wb_Ip='"+wl.Wb_ip+"',Wb_Area_Id="+areaid+",Rk_Id='"+rankid+"',User_Id="+userid+",Wb_Master_Id='"+masterid+"',Wb_Time='"+wl.Wb_time+"',Wb_Remark='"+wl.Wb_remark+"',Wb_Postalcode='"+wl.Wb_postalcode+"',Wb_Address='"+wl.Wb_address+"',Wb_Tel1='"+wl.Wb_tel1+"',Wb_Tel2='"+wl.Wb_tel2+"',Wb_Fax='"+wl.Wb_fax+"',Wb_Email1='"+wl.Wb_email1+"',Wb_Email2='"+wl.Wb_email2+"',Wb_QQ1='"+wl.Wb_qq1+"',Wb_QQ2='"+wl.Wb_qq2+"',Wb_Manager='"+wl.Wb_manager+"',Wb_Connect='"+wl.Wb_connect+"' where Wb_Id=" + wl.Wb_id + "";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelWbList()
        {
            string sqltext = "select * from Wb_List";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
