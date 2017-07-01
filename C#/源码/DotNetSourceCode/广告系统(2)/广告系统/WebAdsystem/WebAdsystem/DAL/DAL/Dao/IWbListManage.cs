using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IWbListManage
    {
        bool AddWbList(WbList wl, int rankid,int userid,int masterid,int areaid);
        bool DelWbList(int id);
        bool ModWbList(WbList wl, int rankid, int userid, int masterid, int areaid);
        DataSet SelWbList();
    }
}
