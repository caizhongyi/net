using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.impl.Customer
{
    class TFindPassWord:DAL.Dao.Customer.ITFindPassWord
    {
        Util u = new Util();
        /// <summary>
        ///  找回这密码
        /// </summary>
        /// <param name="F_CustomerID">用户ID</param>
        /// <param name="F_Address">地址</param>
        /// <param name="F_Tel">电话</param>
        /// <param name="FIDNumber">身份证信息</param>
        /// <param name="F_Remark">备注</param>
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
