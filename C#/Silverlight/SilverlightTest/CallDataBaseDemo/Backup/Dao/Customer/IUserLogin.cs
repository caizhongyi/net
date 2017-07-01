using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL.Dao.Customer
{
    public interface IUserLogin
    {
        bool IsLogin(T_CustomerInfo customerinfo);
    }
}
