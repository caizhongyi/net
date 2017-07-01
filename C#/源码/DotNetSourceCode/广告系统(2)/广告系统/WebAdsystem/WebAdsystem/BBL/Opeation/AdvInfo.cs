using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class AdvInfo : BBL.Inface.IAdvInfo 
    {
        DAL.Dao.IAdvInfoManage AdvInfoMange = DAL.FactoryDao.GetAdvInfoManage();

        public bool InsertAdvInfo(DAL.Model.AdvInfo AdvInfo, int masterID, int userID)
        {

            bool i = AdvInfoMange.AddAdvInfo(AdvInfo, masterID, userID);
            return i;   
        }
        public bool UpdateAdvInfo(DAL.Model.AdvInfo AdvInfo, int masterID, int userID)
        {
            bool i = AdvInfoMange.ModAdvInfo(AdvInfo, masterID, userID);
            return i;
        }
        public bool DelAdvInfobyID(int advid)
        {
            bool i = AdvInfoMange.DelAdvInfo(advid);
            return i;
        }
        public DataSet  SelectAdvInfo()
        {
            DataSet ds = AdvInfoMange.SelAdvInfo();
            return ds;
        }


    }

}
