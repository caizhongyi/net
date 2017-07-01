using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IAreaManage
    {
        bool AddArea(AreaList al,int ProvinceId);
        bool DelArea(int id);
        bool ModArea(AreaList al, int ProvinceId);
        DataSet SelArea();
    }
}
