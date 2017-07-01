using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using Model;

namespace DAL.impl.Customer
{
    class UserLoginDao :IUserLogin
    {
        Util util = new Util();
        #region ÓÃ»§µÇÂ¼

        public bool IsLogin(T_CustomerInfo customerinfo)
        {
            string sqlText = "select CPwd from T_CustomerInfo where CId='" + customerinfo.CId + "'";
            string PwdStr = util.GetStrExecuteScalar(sqlText);
            return (customerinfo.CPwd == PwdStr) ? true : false;
        }
        #endregion
    }
}
