using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IAdvTypeManage
    {
        bool AddAdvType(AdvType at);
        bool DelAdvType(int id);
        bool ModAdvType(AdvType at);
        DataSet SelAdvType();
    }
}
