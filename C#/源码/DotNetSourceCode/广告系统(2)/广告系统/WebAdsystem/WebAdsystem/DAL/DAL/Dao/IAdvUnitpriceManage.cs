using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IAdvUnitpriceManage
    {
        bool AddAdvUnitprice(AdvUnitprice aup,int rk_id,int adv_type_id);
        bool DelAdvUnitprice(int rk_id);
        bool ModAdvUnitprice(AdvUnitprice aup, int rk_id, int adv_type_id);
        DataSet SelAdvUnitprice();
    }
}
