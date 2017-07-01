using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;

namespace DAL.impl
{
    class AdvIssueManageDao : IAdvIssueManage
    {
        Util util = new Util();
        #region 广告发布管理

        public bool AddAdvIssue(AdvIssue ai, int advid, int wbid, int advtypeid)
        {
            string sqltext = "insert into Wb_List values(" + advid + "," + wbid + "," + advtypeid + ",'" + ai.Adv_startday + "','" + ai.Adv_endday + "'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelAdvIssue(int advid, int wbid, int advtypeid)
        {
            string sqltext = "delete from Adv_Issue where Adv_Id=" + advid + ",Wb_Id=" + wbid + ",Adv_Type_Id="+advtypeid+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModAdvIssue(AdvIssue ai, int advid, int wbid, int advtypeid)
        {
            string sqltext = "update Adv_Issue set Adv_Startday='"+ai.Adv_startday+"',Adv_Endday='"+ai.Adv_endday+"' where where Adv_Id=" + advid + ",Wb_Id=" + wbid + ",Adv_Type_Id=" + advtypeid + "";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelAdvIssue()
        {
            string sqltext = "select * from Adv_Issue";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
