using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Dao.Customer
{
    public interface ITFindPassWord
    {
        bool InsertFindPassWord(string F_CustomerID, string F_Address, string F_Tel, string F_IDNumber, string F_EMail, string F_Remark);
    }
}
