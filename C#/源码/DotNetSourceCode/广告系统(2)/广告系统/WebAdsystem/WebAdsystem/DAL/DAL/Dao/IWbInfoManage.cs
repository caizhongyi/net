using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.Model;

namespace DAL.Dao
{
    public interface IWbInfoManage
    {
        bool AddWbInfo(WbList wl);
        bool DelWbInfo(int id);
        bool ModWbInfo(WbList wl);
        DataSet SelWbInfo();
    }
}
