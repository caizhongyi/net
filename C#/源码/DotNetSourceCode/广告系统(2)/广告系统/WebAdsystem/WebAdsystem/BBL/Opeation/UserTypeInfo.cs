using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class UserTypeInfo : BBL.Inface.IUserTypeInfo
    {
        DAL.Dao.IUserTypeManage UserType = DAL.FactoryDao.GetUserTypeManage();
        public bool InsertUserTypeInfo(DAL.Model.UserType userType)
        {
             return UserType .AddUserType(userType);         
        }
        public bool UpdateUserTypeInfo(DAL.Model.UserType userType)
        {
            return UserType .ModUserType(userType);
        }
        public bool DelUserTypeInfobyID(int id)
        {
            return UserType.DelUserType(id) ;
        }
        public DataSet SelectUserTypeInfo()
        {
            return UserType.SelUserType() ;
        }
    }
}
