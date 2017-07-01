using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.DataBaseModel;

namespace DAL.DataBaseInterface
{
    public interface IAd_WB
    {
        DataTable getAd_WBList(string sql);//���ݲ�ѯ����ȡ����
        void AddAd_WB(Ad_WBInfo obj);//���
        void UpdateAd_WB(Ad_WBInfo obj);//�޸�
        void DelAd_WB(Ad_WBInfo obj);//ɾ��
        void UpdeteAd_WB(string sql);//���ݸ�������������
    }

    public interface IAdType
    {
        DataTable getAdTypeList(string sql);//���ݲ�ѯ����ȡ����
        void AddAdType(AdTypeInfo obj);//���
        void UpdateAdType(AdTypeInfo obj);//�޸�
        void DelAdType(AdTypeInfo obj);//ɾ��
        void UpdeteAdType(string sql);//���ݸ�������������
    }

    public interface IUserInfo
    {
        DataSet ChackLogin(UserInfoInfo obj);
        DataSet getUserInfoList();//���ݲ�ѯ����ȡ����
        int AddUserInfo(UserInfoInfo obj);//���
        int UpdateUserInfo(UserInfoInfo obj);//�޸�
        int DelUserInfo(UserInfoInfo obj);//ɾ��
        void UpdeteUserInfo(string sql);//���ݸ�������������
    }

    public interface IWBInfo
    {
        DataSet getWBInfoList();//���ݲ�ѯ����ȡ����
        int AddWBInfo(WBInfoInfo obj);//���
        void UpdateWBInfo(WBInfoInfo obj);//�޸�
        void DelWBInfo(int id);//ɾ��
        void UpdeteWBInfo(string sql);//���ݸ�������������
    }

    public interface IClientInfo
    {
        DataTable getClientInfoList(string sql);//���ݲ�ѯ����ȡ����
        void AddClientInfo(ClientInfoInfo obj);//���
        void UpdateClientInfo(ClientInfoInfo obj);//�޸�
        void DelClientInfo(ClientInfoInfo obj);//ɾ��
        void UpdeteClientInfo(string sql);//���ݸ�������������
    }

    public interface IWB_Extension
    {
        DataTable getWB_ExtensionList(string sql);//���ݲ�ѯ����ȡ����
        void AddWB_Extension(WB_ExtensionInfo obj);//���
        void UpdateWB_Extension(WB_ExtensionInfo obj);//�޸�
        void DelWB_Extension(WB_ExtensionInfo obj);//ɾ��
        void UpdeteWB_Extension(string sql);//���ݸ�������������
    }

    public interface IAdInfo
    {
        DataSet getAdInfoList();//���ݲ�ѯ����ȡ����
        int AddAdInfo(AdInfoInfo obj);//���
        void UpdateAdInfo(AdInfoInfo obj);//�޸�
        void DelAdInfo(int id);//ɾ��
    }

    public interface IClient_Wb
    {
        DataTable getClient_WbList(string sql);//���ݲ�ѯ����ȡ����
        void AddClient_Wb(Client_WbInfo obj);//���
        void UpdateClient_Wb(Client_WbInfo obj);//�޸�
        void DelClient_Wb(Client_WbInfo obj);//ɾ��
        void UpdeteClient_Wb(string sql);//���ݸ�������������
    }

    public interface Isysdiagrams
    {
        DataTable getsysdiagramsList(string sql);//���ݲ�ѯ����ȡ����
        void Addsysdiagrams(sysdiagramsInfo obj);//���
        void Updatesysdiagrams(sysdiagramsInfo obj);//�޸�
        void Delsysdiagrams(sysdiagramsInfo obj);//ɾ��
        void Updetesysdiagrams(string sql);//���ݸ�������������
    }

}
