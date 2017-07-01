using System;
using System.Collections.Generic;
using System.Text;
using BBL.Inface;
using DAL.Dao;
using DAL;
using DAL.Model;
using System.Data;

namespace BBL.Opeation
{
    class UserInfoService:IUserInfo
    {
        IUserInfoManage iim = FactoryDao.GetUserManage();
        #region IUserInfo ≥…‘±

        public bool UserLogin(UserInfo userinfo)
        {
            return iim.UserLogin(userinfo);
        }

        public bool DelUserInfobyID(string id)
        {
            return iim.DelUserInfo(id);
        }

        public bool InsertUserInfo(UserInfo userInfo, int typeID)
        {
            return iim.AddUserInfo(userInfo,typeID);
        }

        public DataSet SelectUserInfo()
        {
            return iim.SelUserInfo();
        }

        public bool UpdateUserInfo(UserInfo userInfo, int typeID)
        {
            return iim.ModUserInfo(userInfo,typeID);
        }

        #endregion
    }
}
