using System;
using System.Data;
using DAL.Model;
namespace BBL.Inface
{
    public interface IUserTypeInfo
    {
        bool DelUserTypeInfobyID(int id);
        bool InsertUserTypeInfo(UserType userType);
        DataSet SelectUserTypeInfo();
        bool UpdateUserTypeInfo(UserType userType);
    }
}
