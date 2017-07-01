using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IProvinceListManage
    {
        bool AddProvinceList(ProvinceList pl);
        bool DelProvinceList(int id);
        bool ModProvinceList(ProvinceList pl);
        DataSet SelProvinceList();
    }
}
