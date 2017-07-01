using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using System.Data;
using DAL.Model;

namespace DAL.impl
{
    class ProvinceListManageDao : IProvinceListManage
    {
        Util util = new Util();

        #region 省份管理

        public bool AddProvinceList(ProvinceList pl)
        {
            string sqltext = "insert into Province_List values('"+pl.Province_name+"','"+pl.Remark+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelProvinceList(int id)
        {
            string sqltext = "delete from Province_List where Province_Id="+id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModProvinceList(ProvinceList pl)
        {
            string sqltext = "update Province_List set Province_Name='"+pl.Province_name+"',Province_Remark='"+pl.Remark+"' where Province_Id="+pl.Province_id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelProvinceList()
        {
            string sqltext = "select * from Province_List";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
