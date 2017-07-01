using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.DataBaseModel;
using DAL.DataBaseInterface;
using DAL.impl.Sql;
using System.Data.SqlClient;

namespace DAL.DataBaseOperate
{
    /****************** ������ TAd_WB �� ********************/
    /// <ʵ����ժҪ>
    /// ������Ad_WB
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class Ad_WB : IAd_WB
    {

        TSqlHelp tssql = null;
        public Ad_WB()
        {
           tssql = new TSqlHelp();
        }

        public DataTable getAd_WBList(string sql) //���ݲ�ѯ����ȡ����
        {
            return tssql.ExecuteQuery(sql);
        }

        public void AddAd_WB(Ad_WBInfo obj) //���
        {
            //
        }

        public void UpdateAd_WB(Ad_WBInfo obj) //�޸�
        {
            //
        }

        public void DelAd_WB(Ad_WBInfo obj) //ɾ��
        {
            //
        }

        public void UpdeteAd_WB(string sql) //���ݸ�������������
        {
            //db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ TAdType �� ********************/
    /// <ʵ����ժҪ>
    /// ������AdType
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class AdType : IAdType
    {
        TSqlHelp db = null;
        public AdType()
        {
            db = new TSqlHelp();
        }

        public DataTable getAdTypeList(string sql) //���ݲ�ѯ����ȡ����
        {
            return db.ExecuteQuery(sql);
        }

        public void AddAdType(AdTypeInfo obj) //���
        {
            //
        }

        public void UpdateAdType(AdTypeInfo obj) //�޸�
        {
            //
        }

        public void DelAdType(AdTypeInfo obj) //ɾ��
        {
            //
        }

        public void UpdeteAdType(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ TUserInfo �� ********************/
    /// <ʵ����ժҪ>
    /// ������UserInfo
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class UserInfo : IUserInfo
    {
        TSqlHelp db = null;
        public UserInfo()
        {
            db = new TSqlHelp();
        }

        public DataSet ChackLogin(UserInfoInfo obj)
        {
            string cmd = "ChackLogin";
            SqlParameter[] sqlparmeter = new SqlParameter[2];
            sqlparmeter[0] = new SqlParameter("login_Name", SqlDbType.VarChar);
            sqlparmeter[0].Value = obj.Login_Name;
            sqlparmeter[1] = new SqlParameter("Login_Pwd", SqlDbType.VarChar);
            sqlparmeter[1].Value = obj.Login_Pwd;
            return  db.SqlGetDataSet(cmd, sqlparmeter, CommandType.StoredProcedure);
            
        }

        public DataSet getUserInfoList() //���ݲ�ѯ����ȡ����
        {
            string cmd = "UserInfoSelectCommand";
            return db.SqlGetDataSet(cmd);
        }

        public int  AddUserInfo(UserInfoInfo obj) //���
        {
            string cmd = "UserInfoInsertCommand";
            SqlParameter[] sqlparmeter = new SqlParameter[5];
            sqlparmeter[0] = new SqlParameter("User_Name", SqlDbType.VarChar);
            sqlparmeter[0].Value = obj.User_Name;
            sqlparmeter[1] = new SqlParameter("Login_Name", SqlDbType.VarChar);
            sqlparmeter[1].Value = obj.Login_Name;
            sqlparmeter[2] = new SqlParameter("Login_Pwd", SqlDbType.VarChar);
            sqlparmeter[2].Value = obj.Login_Pwd;
            sqlparmeter[3] = new SqlParameter("User_Right", SqlDbType.VarChar);
            sqlparmeter[3].Value = obj.User_Right;
            sqlparmeter[4] = new SqlParameter("User_Remark", SqlDbType.VarChar);
            sqlparmeter[4].Value = obj.User_Remark;
            int i = db.ExecuteNonQuery(cmd, sqlparmeter, CommandType.StoredProcedure);
            if (i > 0)
            {
                return i = 1;
            }
            else
            {
                return i = 0;
            }
        }

        public int UpdateUserInfo(UserInfoInfo obj) //�޸�
        {
            string cmd = "UserInfoUpdateCommand";
            SqlParameter[] sqlparmeter = new SqlParameter[5];
            sqlparmeter[0] = new SqlParameter("User_Name", SqlDbType.VarChar);
            sqlparmeter[0].Value = obj.User_Name;
            sqlparmeter[1] = new SqlParameter("Login_Name", SqlDbType.VarChar);
            sqlparmeter[1].Value = obj.Login_Name;
          
            sqlparmeter[2] = new SqlParameter("User_Right", SqlDbType.VarChar);
            sqlparmeter[2].Value = obj.User_Right;
            sqlparmeter[3] = new SqlParameter("User_Remark", SqlDbType.VarChar);
            sqlparmeter[3].Value = obj.User_Remark;
            sqlparmeter[4] = new SqlParameter("User_ID", SqlDbType.VarChar);
            sqlparmeter[4].Value = obj.User_ID;
            int i = db.ExecuteNonQuery(cmd, sqlparmeter, CommandType.StoredProcedure);
            if (i > 0)
            {
                return i = 1;
            }
            else
            {
                return i = 0;
            }
        }

        public int DelUserInfo(UserInfoInfo obj) //ɾ��
        {
            string cmd = "UserInfoDeleteCommand";
            SqlParameter[] sqlparmeter = new SqlParameter[1];

            sqlparmeter[0] = new SqlParameter("User_ID", SqlDbType.Int );
            sqlparmeter[0].Value = obj.User_ID;
         
            int i = db.ExecuteNonQuery(cmd, sqlparmeter, CommandType.StoredProcedure);
            if (i > 0)
            {
                return i = 1;
            }
            else
            {
                return i = 0;
            }
        }

        public void UpdeteUserInfo(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ TWBInfo �� ********************/
    /// <ʵ����ժҪ>
    /// ������WBInfo
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class WBInfo : IWBInfo
    {
        TSqlHelp db = null;
        public WBInfo()
        {
            db = new TSqlHelp();
        }

        public DataSet getWBInfoList() //���ݲ�ѯ����ȡ����
        {
            string sql = "select * from WBInfo,ad_client,ClientInfo Where ad_client.Client_ID=ClientInfo.client_id  and WBInfo.wb_id=ad_client.wb_id";
            return db.SqlGetDataSet(sql);
          
        }

        public int AddWBInfo(WBInfoInfo obj) //���
        {

            string cmd = "WbInsertCommand";
            SqlParameter[] sqlparmeter = new SqlParameter[6];
            sqlparmeter[0] = new SqlParameter("Wb_ID", SqlDbType.Int);
            sqlparmeter[0].Value = obj.WB_ID;
            sqlparmeter[1] = new SqlParameter("Wb_Name", SqlDbType.VarChar);
            sqlparmeter[1].Value = obj.WB_Name;
            sqlparmeter[2] = new SqlParameter("WB_IP", SqlDbType.VarChar);
            sqlparmeter[2].Value = obj.WB_IP;
            sqlparmeter[3] = new SqlParameter("WB_Phone", SqlDbType.VarChar);
            sqlparmeter[3].Value = obj.WB_Phone;
            sqlparmeter[4] = new SqlParameter("WB_Address", SqlDbType.VarChar);
            sqlparmeter[4].Value = obj.WB_Address;
            sqlparmeter[5] = new SqlParameter("WB_Remark", SqlDbType.VarChar);
            sqlparmeter[5].Value = obj.WB_Remark;
            int i = db.ExecuteNonQuery(cmd, sqlparmeter, CommandType.StoredProcedure);
            if (i > 0)
            {
                return i = 1;
            }
            else
            {
                return i = 0;
            }
        }

        public void UpdateWBInfo(WBInfoInfo obj) //�޸�
        {
            string sql = "Update WBInfo set Wb_Name='" + obj.WB_Name + "',WB_IP='" + obj.WB_IP + "',WB_Address='" + obj.WB_Address + "',WB_Phone='" + obj.WB_Phone + "',WB_Register_Time='" + obj.WB_Register_Time + "',WB_Remark='" + obj.WB_Remark + "' where WB_ID='"+obj.WB_ID+"' ";
            db.ExecuteQuery(sql);

        }

        public void DelWBInfo(int id) //ɾ��
        {
            string sql = "delete from WBInfo where WB_ID='"+id+"'";
            db.ExecuteQuery(sql);
        }

        public void UpdeteWBInfo(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ TClientInfo �� ********************/
    /// <ʵ����ժҪ>
    /// ������ClientInfo
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class ClientInfo : IClientInfo
    {
        TSqlHelp db = null;
        public ClientInfo()
        {
            db = new TSqlHelp();
        }

        public DataTable getClientInfoList(string sql) //���ݲ�ѯ����ȡ����
        {
            return db.ExecuteQuery(sql);
        }

        public void AddClientInfo(ClientInfoInfo obj) //���
        {
            //
        }

        public void UpdateClientInfo(ClientInfoInfo obj) //�޸�
        {
            //
        }

        public void DelClientInfo(ClientInfoInfo obj) //ɾ��
        {
            //
        }

        public void UpdeteClientInfo(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ TWB_Extension �� ********************/
    /// <ʵ����ժҪ>
    /// ������WB_Extension
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class WB_Extension : IWB_Extension
    {
        TSqlHelp db = null;
        public WB_Extension()
        {
            db = new TSqlHelp();
        }

        public DataTable getWB_ExtensionList(string sql) //���ݲ�ѯ����ȡ����
        {
            return db.ExecuteQuery(sql);
        }

        public void AddWB_Extension(WB_ExtensionInfo obj) //���
        {
            //
        }

        public void UpdateWB_Extension(WB_ExtensionInfo obj) //�޸�
        {
            //
        }

        public void DelWB_Extension(WB_ExtensionInfo obj) //ɾ��
        {
            //
        }

        public void UpdeteWB_Extension(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ TAdInfo �� ********************/
    /// <ʵ����ժҪ>
    /// ������AdInfo
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

   class AdInfo : IAdInfo
    {
        TSqlHelp db = null;
        public AdInfo()
        {
            db = new TSqlHelp();
        }

       public DataSet getAdInfoList() //���ݲ�ѯ����ȡ����
       {
           string com = "AdSelectCommand";
           return db.SqlGetDataSet(com);
       }

        public int AddAdInfo(AdInfoInfo obj) //���
        {
           
            string cmd = "AdInsertCommand";
            SqlParameter[] sqlparmeter = new SqlParameter[6];
            sqlparmeter[0] = new SqlParameter("Ad_ID", SqlDbType.Int);
            sqlparmeter[0].Value = obj .Ad_ID;
            sqlparmeter[1] = new SqlParameter("Ad_Name", SqlDbType.VarChar);
            sqlparmeter[1].Value =obj .Ad_Name;
            sqlparmeter[2] = new SqlParameter("Ad_Url", SqlDbType.VarChar);
            sqlparmeter[2].Value = obj .Ad_Url;
            sqlparmeter[3] = new SqlParameter("Ad_Operation", SqlDbType.VarChar);
            sqlparmeter[3].Value = obj.Ad_Operation;
            sqlparmeter[4] = new SqlParameter("Ad_Remark", SqlDbType.VarChar);
            sqlparmeter[4].Value =obj .Ad_Remark;
            sqlparmeter[5] = new SqlParameter("Ad_Type_ID", SqlDbType.Int);
            sqlparmeter[5].Value = obj.Ad_Type_Name;
            int i = db.ExecuteNonQuery(cmd, sqlparmeter, CommandType.StoredProcedure);
            if (i > 0)
            {
                return i = 1;
            }
            else
            {
                return i = 0;
            }
        }

       public void UpdateAdInfo(AdInfoInfo obj) //���ݸ�������������
        {
            string sql = "update AdInfo set Ad_Type_ID='" +obj.Ad_Type_Name+ "',Ad_Name='" + obj.Ad_Name + "',Ad_Url='" + obj.Ad_Url + "',Ad_ClickNum='" + obj.Ad_ClickNum + "',Ad_Operation='" + obj.Ad_Operation + "'Ad_Remark='" + obj.Ad_Remark + "',Ad_time='" + obj.Ad_time + "' where Ad_ID='" + obj.Ad_ID + "'";
            db.ExecuteQuery(sql);
        }

        public void DelAdInfo(int id) //ɾ��
        {
            string sql = "delete from AdInfo where Ad_ID='"+id+"'";
            db.ExecuteQuery(sql);
        }
    }

    /****************** ������ TClient_Wb �� ********************/
    /// <ʵ����ժҪ>
    /// ������Client_Wb
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:11
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class Client_Wb : IClient_Wb
    {
        TSqlHelp db = null;
        public Client_Wb()
        {
            db = new TSqlHelp();
        }

        public DataTable getClient_WbList(string sql) //���ݲ�ѯ����ȡ����
        {
            return db.ExecuteQuery(sql);
        }

        public void AddClient_Wb(Client_WbInfo obj) //���
        {
            //
        }

        public void UpdateClient_Wb(Client_WbInfo obj) //�޸�
        {
            //
        }

        public void DelClient_Wb(Client_WbInfo obj) //ɾ��
        {
            //
        }

        public void UpdeteClient_Wb(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }

    /****************** ������ Tsysdiagrams �� ********************/
    /// <ʵ����ժҪ>
    /// ������sysdiagrams
    /// �汾��1.0.0.0
    /// ʱ�䣺2008-10-26 20:53:12
    /// ˵������ʵ�����ɴ�������������
    /// </ʵ����ժҪ>

    class sysdiagrams : Isysdiagrams
    {
        TSqlHelp db = null;
        public sysdiagrams()
        {
            db = new TSqlHelp();
        }

        public DataTable getsysdiagramsList(string sql) //���ݲ�ѯ����ȡ����
        {
            return db.ExecuteQuery(sql);
        }

        public void Addsysdiagrams(sysdiagramsInfo obj) //���
        {
            //
        }

        public void Updatesysdiagrams(sysdiagramsInfo obj) //�޸�
        {
            //
        }

        public void Delsysdiagrams(sysdiagramsInfo obj) //ɾ��
        {
            //
        }

        public void Updetesysdiagrams(string sql) //���ݸ�������������
        {
            db.ExecuteNonQuery(sql);
        }

    }
}
