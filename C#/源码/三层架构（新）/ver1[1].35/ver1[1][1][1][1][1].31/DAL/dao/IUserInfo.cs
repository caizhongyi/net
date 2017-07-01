using System;
using System.Collections.Generic;
using System.Text;
using DAL.model;

namespace DAL.dao
{
   public  interface IUserInfo
    {
        UserInfo loadByUserNameAndPassword(string username, string password);
    }
}
