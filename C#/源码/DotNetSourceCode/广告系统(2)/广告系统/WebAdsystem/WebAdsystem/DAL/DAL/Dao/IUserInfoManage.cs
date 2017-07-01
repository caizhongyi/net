using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IUserInfoManage
    {
        //ʵ�ֵ�¼
        bool UserLogin(UserInfo userinfo);
        //��ѯ���ҵ��Ա
        DataSet SelAdvUserInfo();

        bool AddUserInfo(UserInfo ui,int typeid);
        bool DelUserInfo(string id);
        bool ModUserInfo(UserInfo ui,int typeid);
        DataSet SelUserInfo();
    }
}
