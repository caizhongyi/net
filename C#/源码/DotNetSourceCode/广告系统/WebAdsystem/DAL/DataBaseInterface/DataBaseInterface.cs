using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.DataBaseModel;

namespace DAL.DataBaseInterface
{
    public interface IAd_WB
    {
        DataTable getAd_WBList(string sql);//根据查询语句获取数据
        void AddAd_WB(Ad_WBInfo obj);//添加
        void UpdateAd_WB(Ad_WBInfo obj);//修改
        void DelAd_WB(Ad_WBInfo obj);//删除
        void UpdeteAd_WB(string sql);//根据更新语句更新数据
    }

    public interface IAdType
    {
        DataTable getAdTypeList(string sql);//根据查询语句获取数据
        void AddAdType(AdTypeInfo obj);//添加
        void UpdateAdType(AdTypeInfo obj);//修改
        void DelAdType(AdTypeInfo obj);//删除
        void UpdeteAdType(string sql);//根据更新语句更新数据
    }

    public interface IUserInfo
    {
        DataSet ChackLogin(UserInfoInfo obj);
        DataSet getUserInfoList();//根据查询语句获取数据
        int AddUserInfo(UserInfoInfo obj);//添加
        int UpdateUserInfo(UserInfoInfo obj);//修改
        int DelUserInfo(UserInfoInfo obj);//删除
        void UpdeteUserInfo(string sql);//根据更新语句更新数据
    }

    public interface IWBInfo
    {
        DataSet getWBInfoList();//根据查询语句获取数据
        int AddWBInfo(WBInfoInfo obj);//添加
        void UpdateWBInfo(WBInfoInfo obj);//修改
        void DelWBInfo(int id);//删除
        void UpdeteWBInfo(string sql);//根据更新语句更新数据
    }

    public interface IClientInfo
    {
        DataTable getClientInfoList(string sql);//根据查询语句获取数据
        void AddClientInfo(ClientInfoInfo obj);//添加
        void UpdateClientInfo(ClientInfoInfo obj);//修改
        void DelClientInfo(ClientInfoInfo obj);//删除
        void UpdeteClientInfo(string sql);//根据更新语句更新数据
    }

    public interface IWB_Extension
    {
        DataTable getWB_ExtensionList(string sql);//根据查询语句获取数据
        void AddWB_Extension(WB_ExtensionInfo obj);//添加
        void UpdateWB_Extension(WB_ExtensionInfo obj);//修改
        void DelWB_Extension(WB_ExtensionInfo obj);//删除
        void UpdeteWB_Extension(string sql);//根据更新语句更新数据
    }

    public interface IAdInfo
    {
        DataSet getAdInfoList();//根据查询语句获取数据
        int AddAdInfo(AdInfoInfo obj);//添加
        void UpdateAdInfo(AdInfoInfo obj);//修改
        void DelAdInfo(int id);//删除
    }

    public interface IClient_Wb
    {
        DataTable getClient_WbList(string sql);//根据查询语句获取数据
        void AddClient_Wb(Client_WbInfo obj);//添加
        void UpdateClient_Wb(Client_WbInfo obj);//修改
        void DelClient_Wb(Client_WbInfo obj);//删除
        void UpdeteClient_Wb(string sql);//根据更新语句更新数据
    }

    public interface Isysdiagrams
    {
        DataTable getsysdiagramsList(string sql);//根据查询语句获取数据
        void Addsysdiagrams(sysdiagramsInfo obj);//添加
        void Updatesysdiagrams(sysdiagramsInfo obj);//修改
        void Delsysdiagrams(sysdiagramsInfo obj);//删除
        void Updetesysdiagrams(string sql);//根据更新语句更新数据
    }

}
