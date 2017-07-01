using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IUserInfoManage
    {
        //实现登录
        bool UserLogin(UserInfo userinfo);
        //查询广告业务员
        DataSet SelAdvUserInfo();

        bool AddUserInfo(UserInfo ui,int typeid);
        bool DelUserInfo(string id);
        bool ModUserInfo(UserInfo ui,int typeid);
        DataSet SelUserInfo();
    }
}
