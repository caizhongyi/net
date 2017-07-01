using System;
using System.Data;
namespace BBL
{
   public  interface IUserInfo
    {
        int DelUserInfo(int userID);
        int InsertUserInfo(string userName, string loginName, string loginPwd, int userRight, string userRemark);
        int UpdateUserInfo(string userName, string loginName, int userRight, string userRemark,int userID);
        DataSet SelectUserInfo();
       DataSet ChackLogin(string loginName, string loginPwd);
   }
}
