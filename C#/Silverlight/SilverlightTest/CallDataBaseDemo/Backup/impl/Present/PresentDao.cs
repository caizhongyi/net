using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Present;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL.impl.Present
{
    class PresentDao : IPresent
    {
        Util util = new Util();
        #region ��Ʒ�б�

        public System.Data.DataSet GetPresentList()
        {
            string sqltext = "select PId,PName,PPicture,PNum,SNum from T_PresentInfo order by PId desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ��Ʒ��������


        public void UpdataPresentCount(string Pid, int num)
        {
            string sqltext = "update T_PresentInfo set PNum=PNum-'" + num + "'  where Pid='" + Pid + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region ���л��ֶһ�


        public int InsertChangePresent(T_CustomerChangePresentInfo ccpi)
        {
            string sqltext = "insert into T_CustomerChangePresentInfo values('" + ccpi.CCPCId + "','" + ccpi.CCPPId + "','" + ccpi.Ccpnum + "','" + ccpi.Ccpname + "','" + ccpi.Ccpaddress + "','" + ccpi.CCPInfo + "','" + DateTime.Now + "','�ȴ����','')";
            return util.GetExecuteNonQuery(sqltext);
        }

        //�޸Ļ���
        public void updatePresentInttral(string Num, string CusId)
        {
            string sqltext = "update T_PersonalScoreInfo set CCurrNum='" + Num + "' where CCId='" + CusId + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        //�޸���Ʒ����

        #endregion



        #region ȡ����ͼƬ


        public DataSet GetDefaultPresentList()
        {
            string sqltext = "select top 4 PId,PName,PPicture,PNum,SNum from T_PresentInfo order by PId desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ȡ����Ʒ����


        public DataSet GetTypeList()
        {
            string sqltext = "select TId,TName from T_PresentTypeInfo";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ������Ʒ


        public bool InsertPicture(T_PresentInfo tpi)
        {
            try
            {
                string sqltext = "insert into T_PresentInfo values('" + tpi.Pname + "','" + tpi.PTId + "','" + tpi.PNum + "','" + tpi.PPicture + "','" + tpi.Pusername + "','" + tpi.Snum + "','" + tpi.Pmon + "','��Ʒ')";
                util.GetExecuteNonQuery(sqltext);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region ������Ʒ��Ϣ


        public DataSet MyPresent(string userid)
        {
            string sqltext = "select PId,PName,TName,PNum,PPicture,SNum from T_PresentTypeInfo,T_PresentInfo where T_PresentInfo.PTId=T_PresentTypeInfo.TId and PUserName='" + userid + "' order by PId desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ɾ���ҵ���Ʒ


        public void delMyPresent(string userid, string pid)
        {
            string sqltext = "delete from T_PresentInfo where PUserName='" + userid + "' and PId='" + pid + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region ��ø�����Ʒ�һ�


        public DataSet SelExchangePresent(string userid)
        {
            string sqltext = "select CCPId,PName,PPicture,CCPName,CCPNum,CCPAddress,CCPInfo,CCPTime,CCPLook from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and PUserName='" + userid + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ������ͨ���ĸ�����Ʒ��Ϣ


        public DataSet SelExchange(string userid, string info)
        {
            string sqltext = "select CCPId,PName,PPicture,CCPName,CCPNum,CCPAddress,CCPInfo,CCPTime,CCPLook from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and PUserName='" + userid + "' and CCPLook='" + info + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion


        #region ���ͨ��


        public void UpdateExchangePresent(string ccpid, string info)
        {
            string sqltext = "update T_CustomerChangePresentInfo set CCPLook='" + info + "'  where CCPId='" + ccpid + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region ѡ����Ʒ��


        public DataSet SelExchangeUp(string ccpid)
        {
            string sqltext = "select * from T_CustomerChangePresentInfo where CCPId='" + ccpid + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ��ȡͼƬ��Ϣ


        public DataSet SelPicture(string pid)
        {
            string sqltext = "select PId,PName,PPicture from T_PresentInfo where PId='" + pid + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ��ȡͼƬ����ϸ��Ϣ


        public DataSet SelAllPictureInfo(string pid)
        {
            string sqltext = "select PId,PName,PNum,PPicture,SNum,PMon from T_PresentInfo where PId='"+pid+"'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ������ʻ��Ϣ


        public DataSet dtSelActivityInfo()
        {
            string sqltext = "Select top 4 AId,Replace(convert(char,[ATime],111),'/','-')+'       '+ATitle as ContentAndTime from ActivityInfo order by [AId] desc";
            return util.GetDataSet(sqltext);
        }

        public DataSet dtSelSelSelActivityInfoByAID(int AId)
        {
            string cmdText = "select ATitle,AContant,ATime,CName from T_CustomerInfo,ActivityInfo where T_CustomerInfo.CId=ActivityInfo.AUId and AId='"+AId+"'";
            return util.GetDataSet(cmdText);
        }

        #endregion
    }
}
