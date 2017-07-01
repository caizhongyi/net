using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class WbInfo : BBL.Inface.IWbInfo 
    {
        DAL.Dao.IWbListManage WbListManage = DAL.FactoryDao.GetWbListManage();
        public bool InsertWbInfo(DAL.Model.WbList wbList,int rankid,int userid,int masterid,int areaid)
        {
            return WbListManage.AddWbList(wbList,rankid,userid,masterid,areaid);
        }
        public bool UpdateWbInfo(DAL.Model.WbList wbList, int rankid, int userid, int masterid, int areaid)
        {
            return WbListManage.ModWbList(wbList, rankid, userid, masterid, areaid); ;
        }
        public bool DelWbInfobyID(int id)
        {
            return WbListManage.DelWbList(id); ;
        }
        public DataSet SelectWbInfo()
        {
            return WbListManage.SelWbList();
        }
    }
}
