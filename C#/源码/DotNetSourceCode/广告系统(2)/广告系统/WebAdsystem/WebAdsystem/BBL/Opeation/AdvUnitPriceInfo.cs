using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class AdvUnitPriceInfo : BBL.Inface.IAdvUnitPriceInfo 
    {
        DAL.Dao.IAdvUnitpriceManage UnitPriceInfo = DAL.FactoryDao.GetAdvUnitpriceManage();
        public bool InsertAdvUnitPriceInfo(DAL.Model.AdvUnitprice advUnitPrice,int rkid,int advTypeID)
        {
            return UnitPriceInfo .AddAdvUnitprice(advUnitPrice ,rkid,advTypeID);
        }
        public bool UpdateAdvUnitPriceInfo(DAL.Model.AdvUnitprice advUnitPrice, int rkid, int advTypeID)
        {
            return UnitPriceInfo.ModAdvUnitprice(advUnitPrice ,rkid,advTypeID);
        }
        public bool DelAdvUnitPriceInfobyID(int rkid)
        {
            return UnitPriceInfo .DelAdvUnitprice(rkid);
        }
        public DataSet SelectAdvUnitPriceInfo()
        {
            return UnitPriceInfo .SelAdvUnitprice();
        }

    }

}
