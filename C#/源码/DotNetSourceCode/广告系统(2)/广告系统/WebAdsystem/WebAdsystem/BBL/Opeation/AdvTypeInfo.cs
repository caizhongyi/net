using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class AdvTypeInfo : BBL.Inface.IAdvTypeInfo 
    {
        DAL.Dao.IAdvTypeManage advTypeManage = DAL.FactoryDao.GetAdvTypeManage();
        public bool InsertAdvTypeInfo(DAL.Model.AdvType advType)
        {
           return  advTypeManage.AddAdvType(advType);
        }
        public bool UpdateAdvTypeInfo(DAL.Model.AdvType advType)
        {
            return advTypeManage .ModAdvType(advType);
        }
        public bool DelAdvTypeInfobyID(int id)
        {
            return advTypeManage.DelAdvType(id);
        }
        public DataSet SelectAdvTypeInfo()
        {
            return advTypeManage.SelAdvType();
        }
    }
}
