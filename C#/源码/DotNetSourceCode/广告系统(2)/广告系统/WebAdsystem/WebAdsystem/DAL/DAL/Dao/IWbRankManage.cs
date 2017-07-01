using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IWbRankManage
    {
        bool AddRank(WbRank wbrank);
        bool DelRank(int id);
        bool ModRank(WbRank wbrank);
        DataSet SelRank();
    }
}
