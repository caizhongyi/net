using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.Reserved
{
    public interface IReserveInfo
    {
        DataSet SelBigTypeInfo();
        DataSet GetSmallTypeInfo(string BigId);
        DataSet GetAllInfo(string uid);
        void InsertInfo(string uid,int typeid);
        void DelInfo(string uid);
        void DelOneInfo(string uid, string ctsid);
    }
}
