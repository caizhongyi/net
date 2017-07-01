using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.Group
{
   public  interface IGroup_CustomerInfo
    {
        bool InsertGroup_Customer(string GroupID, string CustomerID);
        DataSet GetGroup_CustomeByCustomerID(string CustomerID);
        bool ChackGroup_Customer(string CustomerID, string GroupID);
        DataSet GetGroup_Customer(string CustomerID, string GroupID);
        bool  DelGroup_Customer(string GroupID, string CustomerID);
       DataSet GetGroupPelpleNum(string UserID);
    }
}
