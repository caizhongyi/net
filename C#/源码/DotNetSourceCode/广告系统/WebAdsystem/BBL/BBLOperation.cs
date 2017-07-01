using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.DataBaseModel;
using BBL;

namespace BBL
{

    public class AdInfo:BBL.IAdInfo
    {
       
        //public DataSet GetAdList()
        //{
           
        //}
        DAL.DataBaseInterface.IAdInfo Iadinfo = DAL.DataBaseFactory.EntityFactory.getAdInfo();
        public int InsetAdInfo(int Ad_ID, string Ad_name, string Ad_url, string Ad_operation, int Ad_type_id, string Remark)
        {
            DAL.DataBaseModel.AdInfoInfo adinfoModel = new AdInfoInfo();
            adinfoModel.Ad_ID = Ad_ID;
            adinfoModel.Ad_Name = Ad_name;
            adinfoModel.Ad_Url = Ad_url;
            adinfoModel.Ad_Operation = Ad_operation;
            adinfoModel.Ad_Type_Name = Ad_type_id;
            adinfoModel.Ad_Remark = Remark;
            
            int i = Iadinfo.AddAdInfo(adinfoModel);
            if (i > 0)
            {
                return i = 1;
            }
            else
            {
                return i = 0;
            }
        }
            public DataSet SelectAdInfo()
            {
                return Iadinfo.getAdInfoList();
                
            }


            public void UpdateAdInfo(AdInfoInfo adinfoinfo)
            {
                Iadinfo.UpdateAdInfo(adinfoinfo);
            }

            public void DelAdInfo(int id)
            {
                Iadinfo.DelAdInfo(id);
            }
        }
    }


        public class WbInfo : IWbInfo
        {


            public int InsetWbInfo(int WB_ID, string WB_Name, string WB_IP, string WB_Address, string  WB_Phone, string WB_Remark)
            {
                DAL.DataBaseInterface.IWBInfo IWb_Info = DAL.DataBaseFactory.EntityFactory.getWBInfo();
                DAL.DataBaseModel.WBInfoInfo Wb_Info = new WBInfoInfo();
                Wb_Info.WB_ID = WB_ID;
                Wb_Info.WB_Name = WB_Name;
                Wb_Info.WB_IP = WB_IP;
                Wb_Info.WB_Address = WB_Address;
                Wb_Info.WB_Phone = WB_Phone;
                Wb_Info.WB_Remark = WB_Remark;
               
                int i = IWb_Info.AddWBInfo(Wb_Info);
                if (i > 0)
                {
                    return i = 1;
                }
                else
                {
                    return i = 0;
                }
            }
            DataSet IWbInfo.GetWbInfo()
            {
                DAL.DataBaseInterface.IWBInfo iwbinfo = DAL.DataBaseFactory.EntityFactory.getWBInfo();
                return iwbinfo.getWBInfoList();
            }
            public void UpdataWbInfo(WBInfoInfo wbinfo)
            {
                DAL.DataBaseInterface.IWBInfo iwbinfo = DAL.DataBaseFactory.EntityFactory.getWBInfo();
                iwbinfo.UpdateWBInfo(wbinfo);
            }

            public void DelWbInfo(int id)
            {
                DAL.DataBaseInterface.IWBInfo iwbinfo = DAL.DataBaseFactory.EntityFactory.getWBInfo();
                iwbinfo.DelWBInfo(id);
            }
        }


public class UserInfo : BBL.IUserInfo
{
    DAL.DataBaseModel.UserInfoInfo UserInfoModel = new UserInfoInfo();
    DAL.DataBaseInterface.IUserInfo UserInfoOperation = DAL.DataBaseFactory.EntityFactory.getUserInfo();
    public int InsertUserInfo(string userName, string loginName, string loginPwd, int userRight, string userRemark)
    {
    
          UserInfoModel.User_Name=userName ;
          UserInfoModel.Login_Name=loginName;
          UserInfoModel.Login_Pwd=loginPwd;
          UserInfoModel.User_Right=userRight;
          UserInfoModel.User_Remark=userRemark;
          int i=UserInfoOperation.AddUserInfo(UserInfoModel);
          if (i > 0)
          {
              return i = 1;
          }
          else
          {
              return i = 0;
          }

    }
    public int UpdateUserInfo(string userName, string loginName, int userRight, string userRemark,int userID)
    {
        UserInfoModel.User_ID = userID;
        UserInfoModel.User_Name = userName;
        UserInfoModel.Login_Name = loginName;
        //UserInfoModel.Login_Pwd = loginPwd;
        UserInfoModel.User_Right = userRight;
        UserInfoModel.User_Remark = userRemark;
        int i = UserInfoOperation.UpdateUserInfo(UserInfoModel);
        if (i > 0)
        {
            return i = 1;
        }
        else
        {
            return i = 0;
        }
    }
    public int DelUserInfo(int userID)
    {
        UserInfoModel.User_ID = userID;
        int i = UserInfoOperation.DelUserInfo(UserInfoModel);
        if (i > 0)
        {
            return i = 1;
        }
        else
        {
            return i = 0;
        }
    }

    public DataSet SelectUserInfo()
    {
       return UserInfoOperation.getUserInfoList();
    }

    public DataSet ChackLogin(string loginName,string loginPwd)
    {
        UserInfoModel.Login_Name=loginName;
        UserInfoModel.Login_Pwd=loginPwd;
        return UserInfoOperation.ChackLogin(UserInfoModel);
    }
}


