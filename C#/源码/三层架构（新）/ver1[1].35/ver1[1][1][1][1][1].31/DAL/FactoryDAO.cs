using System;
using System.Collections.Generic;
using System.Text;
using DAL.dao;
using DAL.impl.Sql;

namespace DAL
{
    public  class FactoryDAO
    {
        public static IUserInfo getUserInfo()
        {
            return new UserInfoIMPL();
        }
        
       
    }
}
