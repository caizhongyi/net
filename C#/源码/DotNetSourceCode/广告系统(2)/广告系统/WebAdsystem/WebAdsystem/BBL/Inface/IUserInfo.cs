using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace BBL.Inface
{
    public interface IUserInfo
    {
        bool UserLogin(UserInfo userinfo);
        bool DelUserInfobyID(string id);
        bool InsertUserInfo(UserInfo userInfo, int typeID);
        DataSet SelectUserInfo();
        bool UpdateUserInfo(UserInfo userInfo, int typeID);
    }
}
