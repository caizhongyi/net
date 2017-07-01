using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IUserTypeManage
    {
        bool AddUserType(UserType ut);
        bool DelUserType(int id);
        bool ModUserType(UserType ut);
        DataSet SelUserType();
    }
}
