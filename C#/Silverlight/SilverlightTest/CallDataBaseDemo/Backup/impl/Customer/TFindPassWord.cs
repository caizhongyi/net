using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.impl.Customer
{
    class TFindPassWord:DAL.Dao.Customer.ITFindPassWord
    {
        Util u = new Util();
        /// <summary>
        ///  �һ�������
        /// </summary>
        /// <param name="F_CustomerID">�û�ID</param>
        /// <param name="F_Address">��ַ</param>
        /// <param name="F_Tel">�绰</param>
        /// <param name="FIDNumber">���֤��Ϣ</param>
        /// <param name="F_Remark">��ע</param>
        /// <returns></returns>
        public bool InsertFindPassWord(string F_CustomerID, string F_Address, string F_Tel, string F_IDNumber, string F_EMail, string F_Remark)
        {
            string cmd = "insert into T_FindPassWrod(F_CustomerID,F_Address,F_Tel,F_IDNumber,F_EMail,F_IsIssue,F_Remark) values('" + F_CustomerID + "','" + F_Address + "','" + F_Tel + "','" + F_IDNumber + "','" + F_EMail + "',0,'" + F_Remark + "')";
            int i = u.GetExecuteNonQuery(cmd);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            }
    }
}
