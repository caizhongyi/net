using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class WbRankInfo : BBL.Inface.IWbRankInfo
    {
        DAL.Dao.IWbRankManage WbRankManage = DAL.FactoryDao.GetWbRankManage();
        public bool InsertWbRankInfo(DAL.Model.WbRank wbRank)
        {
            
            return WbRankManage.AddRank(wbRank );
        }
        public bool UpdateWbRankInfo(DAL.Model.WbRank wbRank)
        {
            return WbRankManage.ModRank(wbRank);
        }
        public bool DelWbRankInfobyID(int id)
        {
            return WbRankManage.DelRank(id);
        }
        public DataSet SelectWbRankInfo()
        {
            return WbRankManage.SelRank();
        }
    }
}
