using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;

namespace DAL.impl
{
    class AreaManageDao : IAreaManage
    {
        Util util = new Util();
        #region 地区管理

        public bool AddArea(AreaList al, int ProvinceId)
        {
            string sqltext = "insert into Area_List values ('" + al.Area_name + "'," + ProvinceId + ",'" + al.Area_remark + "')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelArea(int id)
        {
            string sqltext = "delete from Area_List where Area_Id="+id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModArea(AreaList al, int ProvinceId)
        {
            string sqltext = "update Area_List set Area_Name='"+al.Area_name+"',Province_Id="+ProvinceId+",Area_Remark='"+al.Area_remark+"' where Area_Id="+al.Area_id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelArea()
        {
            string sqltext = "select * from Area_List";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
