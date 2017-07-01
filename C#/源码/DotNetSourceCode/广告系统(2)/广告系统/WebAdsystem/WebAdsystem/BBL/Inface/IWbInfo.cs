using System;
namespace BBL.Inface
{
    public interface IWbInfo
    {
        bool DelWbInfobyID(int id);
        bool InsertWbInfo(DAL.Model.WbList wbList, int rankid, int userid, int masterid, int areaid);
        System.Data.DataSet SelectWbInfo();
        bool UpdateWbInfo(DAL.Model.WbList wbList, int rankid, int userid, int masterid, int areaid);
    }
}
