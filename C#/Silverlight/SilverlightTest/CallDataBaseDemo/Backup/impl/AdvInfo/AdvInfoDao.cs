using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.AdvInfo;
using System.Data;
using Model;

namespace DAL.impl.AdvInfo
{
    class AdvInfoDao : IAdvInfo
    {
        Util util = new Util();

        #region SelAdvInfoByUserID �����û�ID�����û���ӵ�еĹ���ѯ����
        /// <summary>
        /// �����û�ID�����û���ӵ�еĹ���ѯ����
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        /// <param name="AdvTypeId">���λ��ID</param>
        ///<param name="IsFree">�Ƿ�����ѹ��</param>
        /// <param name="IsIssue">�Ƿ����з�����</param>
        /// <returns></returns>
        public DataSet SelAdvInfoByUserID(string UserID,int AdvTypeId,int IsFree,int IsIssue)
        {
            string cmdText = "select Adv_Info.Adv_Id,Adv_Info.Adv_Name,Adv_Info.Adv_Time,Adv_Info.BigAdv_Url,Adv_Info.Adv_Title,Adv_Info.Adv_Content,T_BigTypeInfo.TBName,T_SmalllTypeInfo.TSName,Adv_Info.IsIssue from Adv_Info,T_BigTypeInfo,T_SmalllTypeInfo where Adv_Info.User_Id='" + UserID + "' and Adv_Info.Adv_Type_Id='" + AdvTypeId + "' and Adv_Info.IsFree='" + IsFree + "' and Adv_Info.IsIssue='" + IsIssue + "' and Adv_Info.TBid=T_BigTypeInfo.TBid and Adv_Info.TSid=T_SmalllTypeInfo.TSid";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAdvInfoByUserID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsFree"></param>
        /// <param name="IsIssue"></param>
        /// <returns></returns>
        public DataSet SelAdvInfoByUserID(string UserID,  int IsFree, int IsIssue)
        {
            string cmdText = "select * from Adv_Info,T_BigTypeInfo,T_SmalllTypeInfo,Adv_Type where Adv_Info.User_Id='" + UserID + "' and Adv_Info.IsFree='" + IsFree + "' and Adv_Info.IsIssue='" + IsIssue + "' and Adv_Info.TBid=T_BigTypeInfo.TBid and Adv_Info.TSid=T_SmalllTypeInfo.TSid and Adv_Type.Adv_Type_Id=Adv_Info.Adv_Type_Id";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region IsAddRightAdvSuccess �����û�ID�����û�Ҫ��ӵ��Ҳ�����Ϣ��ӵ����ݿ���
        /// <summary>
        /// �����û�ID�����û�Ҫ��ӵ��Ҳ�����Ϣ��ӵ����ݿ��в��õ�Id
        /// </summary>
        /// <returns></returns>
        public int IsAddRightAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,BigAdv_Url,Adv_Url,TBid,TSid,Adv_Remark,Adv_Type_Id,Adv_Time,User_Id,IsFree,IsIssue,Adv_Content,Adv_Title) values('" + advInfo.Adv_Name + "','" + advInfo.BigAdv_Url + "','" + advInfo.Adv_Url + "','" + advInfo.TBid + "','" + advInfo.TSid + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "','" + advInfo.IsIssue + "','','') select @@identity";
            return util.GetIntExecuteScalar(cmdText);
        }
        #endregion

        #region IsAddTopAdvSuccess �ж϶���������Ϣ�Ƿ���ӳɹ�
        /// <summary>
        /// �ж϶���������Ϣ�Ƿ���ӳɹ�
        /// </summary>
        /// <param name="advInfo">��������������ϸ��Ϣ</param>
        /// <returns></returns>
        public int IsAddTopAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,Adv_Title,Adv_Content,TSid,TBid,Adv_Type_Id,Adv_Remark,Adv_Time,User_Id,IsFree,IsIssue,Adv_Url,BigAdv_Url) values('" + advInfo.Adv_Name + "','" + advInfo.Adv_Title + "','" + advInfo.Adv_Content + "','" + advInfo.TSid + "','" + advInfo.TBid + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "','" + advInfo.IsIssue + "','','') select @@identity";
            return util.GetIntExecuteScalar(cmdText);
        }
        #endregion

        #region IsAddClassAdvSuccess �жϷ�����Ϣ�Ƿ���ӳɹ�
        /// <summary>
        /// �жϷ�����Ϣ�Ƿ���ӳɹ�
        /// </summary>
        /// <param name="advInfo">���������ϸ��Ϣ</param>
        /// <returns></returns>
        public int IsAddClassAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,Adv_Title,Adv_Content,TSid,TBid,Adv_Type_Id,Adv_Remark,Adv_Time,User_Id,IsFree,IsIssue,Adv_Url,BigAdv_Url) values('" + advInfo.Adv_Name + "','" + advInfo.Adv_Title + "','" + advInfo.Adv_Content + "','" + advInfo.TSid + "','" + advInfo.TBid + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "','" + advInfo.IsIssue + "','','') select @@identity";
            return util.GetIntExecuteScalar(cmdText);
        }
        #endregion

        #region IsAddFreeAdvSuccess �ж���ѹ���Ƿ���ӳɹ�
        /// <summary>
        /// �ж���ѹ���Ƿ���ӳɹ�
        /// </summary>
        /// <param name="advInfo">��ѹ�����ϸ��Ϣ</param>
        /// <returns></returns>
        public bool IsAddFreeAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,Adv_Title,Adv_Content,TSid,TBid,Adv_Type_Id,Adv_Remark,Adv_Time,User_Id,IsFree) values('" + advInfo.Adv_Name + "','" + advInfo.Adv_Title + "','" + advInfo.Adv_Content + "','" + advInfo.TSid + "','" + advInfo.TBid + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "')";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region UpdateAdvInfoByAdvId ���ݹ��Id���¹�淢���ֶ�
        /// <summary>
        /// ���ݹ��Id���¹�淢���ֶ�
        /// </summary>
        /// <param name="advInfo">���Ļ�����Ϣ IsIssueΪ0ʱ�ù��δ��������IsIssueΪ1ʱ�ѷ�����</param>
        /// <returns></returns>
        public bool UpdateAdvInfoByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set IsIssue='" + advInfo.IsIssue + "' where Adv_Id='" + advInfo.Adv_Id + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region SelAdvInfoByAdvId ���ݹ��Id�����Ļ�����Ϣ��ѯ����
        /// <summary>
        /// ���ݹ��Id�����Ļ�����Ϣ��ѯ����
        /// </summary>
        /// <param name="Adv_Id">���Id</param>
        /// <returns></returns>
        public DataTable SelAdvInfoByAdvId(int Adv_Id)
        {
            string cmdText = "select * from Adv_Info,Adv_Issue where Adv_Info.Adv_Id='" + Adv_Id + "'and Adv_Info.Adv_Id=Adv_Issue.Adv_Id";
            return util.GetDataSet(cmdText).Tables[0];
        }
        #endregion

        #region DelAdvInfoByAdvId ���ݹ��Id���������ɾ��
        /// <summary>
        /// ���ݹ��Id���������ɾ��
        /// </summary>
        /// <param name="advId">���Id</param>
        /// <returns></returns>
        public bool DelAdvInfoByAdvId(int advId)
        {
            string cmdText = "delete from Adv_Info where Adv_Id='" + advId + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i) 
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region UpdateAdvByAdvId ���ݹ��Id�޸Ķ����ͷ�������Ϣ
        /// <summary>
        /// ���ݹ��Id�޸Ķ����ͷ�������Ϣ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Content��Adv_Remark��Adv_Title��Adv_Id�ֶ�</param>
        /// <returns></returns>
        public bool UpdateAdvByAdvId(Adv_Info advInfo) 
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Content='" + advInfo.Adv_Content + "',Adv_Remark='" + advInfo.Adv_Remark + "',Adv_Title='" + advInfo.Adv_Title + "' where Adv_Id='" + advInfo.Adv_Id + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i) 
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region UpdateRightAdvByAdvId ���ݹ��Id�޸��Ҳ�����Ϣ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ�����Ϣ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Url��Adv_Remark��BigAdv_Url��Adv_Id�ֶ�</param>
        /// <returns></returns>
        public bool UpdateRightAdvByAdvId(Adv_Info advInfo) 
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Url='" + advInfo.Adv_Url + "',Adv_Remark='" + advInfo.Adv_Remark + "',BigAdv_Url='" + advInfo.BigAdv_Url + "' where Adv_Id='" + advInfo.Adv_Id + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region UpdateRightBigAdvByAdvId ���ݹ��Id�޸��Ҳ��ͼ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ��ͼ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Remark��BigAdv_Url��Adv_Id�ֶ�</param>
        /// <returns></returns>
        public bool UpdateRightBigAdvByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Remark='" + advInfo.Adv_Remark + "',BigAdv_Url='" + advInfo.BigAdv_Url + "' where Adv_Id='" + advInfo.Adv_Id + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region UpdateRightSmallAdvByAdvId ���ݹ��Id�޸��Ҳ�Сͼ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ�Сͼ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Remark��BigAdv_Url��Adv_Id�ֶ�</param>
        /// <returns></returns>
        public bool UpdateRightSmallAdvByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Remark='" + advInfo.Adv_Remark + "',Adv_Url='" + advInfo.Adv_Url + "' where Adv_Id='" + advInfo.Adv_Id + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region UpdateRightAdvNameByAdvId ���ݹ��Id�޸��Ҳ�����Ϣ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ�����Ϣ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Remark��Adv_Id�ֶ�</param>
        /// <returns></returns>
        public bool UpdateRightAdvNameByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Remark='" + advInfo.Adv_Remark + "' where Adv_Id='" + advInfo.Adv_Id + "'";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region ��ȡ�Ҳ���


        public DataTable SelAllAdvInfo()
        {
            string sqltext = "select Adv_Id,Adv_Name,Adv_Url from Adv_Info where Adv_Type_Id='6' order by Adv_Time desc ";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region ��ȡ������Ϣ


        public DataTable SeltopAdvInfo()
        {
            string sqltext = "select top 10 Adv_Id,Adv_Name from Adv_Info where Adv_Type_Id='3' order by Adv_Time desc";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region ��ȡȫ��������Ϣ


        public DataTable SelAlltopAdvInfo()
        {
            string sqltext = "select Adv_Id,Adv_Title,Adv_Content,Adv_Time from Adv_Info where Adv_Type_Id='3' order by Adv_Time desc";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region IAdvInfo �Ҳ����ǰ������Ϣ


        public DataTable SelSingleAdvInfo()
        {
            string sqltext = "select top 16 Adv_Id,Adv_Name,Adv_Url from Adv_Info where Adv_Type_Id='6' order by Adv_Time desc ";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region IAdvInfo ��Ա


        public DataTable SeltopInfo(string TSid)
        {
            string sqltext = "select * from T_SmalllTypeInfo,Adv_Info where Adv_Id='"+TSid+"' and T_SmalllTypeInfo.TSid=Adv_Info.TSid";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region IAdvInfo ��Ա


        public DataTable SelAlltTypeAdvInfo(string tsid)
        {
            string sqltext = "select * from Adv_Info where Adv_Type_Id='3' and TSid='"+tsid+"' order by Adv_Time desc";
            return util.GetDataTable(sqltext);
        }

        #endregion
    }
}
