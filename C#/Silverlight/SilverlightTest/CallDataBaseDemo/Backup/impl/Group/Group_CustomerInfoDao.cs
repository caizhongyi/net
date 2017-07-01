using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.impl.Group
{
    class Group_CustomerInfoDao : Dao.Group.IGroup_CustomerInfo
    {
        Util u = new Util();
        public bool InsertGroup_Customer(string GroupID, string CustomerID)
        {
            string cmd = "insert into T_Customer_Group values('" + GroupID + "','" + CustomerID + "')";
            int i = u.GetExecuteNonQuery(cmd);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }

        public bool DelGroup_Customer(string GroupID, string CustomerID)
        {
            string cmd = "Delete T_Customer_Group where CustomerID='" + CustomerID + "'  and GroupID='" + GroupID + "'";
            int i = u.GetExecuteNonQuery(cmd);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }

        public DataSet GetGroup_CustomeByCustomerID(string CustomerID)
        {
            string cmd = "Select  * from T_Customer_Group,T_GroupInfo Where T_Customer_Group.CustomerID='" + CustomerID + "'and T_Customer_Group.GroupID=T_GroupInfo.GroupID";
            return u.GetDataSet(cmd);
        }

        public bool ChackGroup_Customer(string CustomerID, string GroupID)
        {

            string cmd = "Select  * from T_Customer_Group Where CustomerID='" + CustomerID + "'  and GroupID='" + GroupID + "'";
            DataSet ds= u.GetDataSet(cmd);
            if (ds.Tables [0].Rows .Count  > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataSet GetGroup_Customer(string CustomerID, string GroupID)
        {

            string cmd = "Select  * from T_Customer_Group Where CustomerID='" + CustomerID + "'  and GroupID='" + GroupID + "'";
            return  u.GetDataSet(cmd);
        
        }

        public DataSet GetGroupPelpleNum(string UserID)
        {
            string cmd = "select T_GroupInfo.GroupID,GroupType,GroupName,CustomerID,sum(case when CustomerID is null then 0 else 1 end) as peopleNum  from T_Customer_Group left join T_GroupInfo on  T_GroupInfo.GroupID=T_Customer_Group.GroupID where UserID='" + UserID + "' group by T_GroupInfo.GroupID,GroupType,GroupName,CustomerID";
            return u.GetDataSet(cmd);

        }
    }
}
