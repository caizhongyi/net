using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvInfo
    {
        #region SelAdvInfoByUserID �����û�ID�����û���ӵ�еĹ���ѯ����
        /// <summary>
        /// �����û�ID�����û���ӵ�еĹ���ѯ����
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        /// <param name="AdvTypeId">���λ��ID</param>
        ///<param name="IsFree">�Ƿ�����ѹ��</param>
        /// <param name="IsIssue">�Ƿ����з�����</param>
        /// <returns></returns>
        DataSet SelAdvInfoByUserID(string UserID, int AdvTypeId, int IsFree, int IsIssue);
        #endregion

        DataSet SelAdvInfoByUserID(string UserID,  int IsFree, int IsIssue);

        #region IsAddRightAdvSuccess �����û�ID�����û�Ҫ��ӵ��Ҳ�����Ϣ��ӵ����ݿ���
        /// <summary>
        /// �����û�ID�����û�Ҫ��ӵ��Ҳ�����Ϣ��ӵ����ݿ���
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int IsAddRightAdvSuccess(Adv_Info advInfo);
        #endregion

        #region IsAddTopAdvSuccess �ж϶���������Ϣ�Ƿ���ӳɹ�
        /// <summary>
        /// �ж϶���������Ϣ�Ƿ���ӳɹ�
        /// </summary>
        /// <param name="advInfo">��������������ϸ��Ϣ</param>
        /// <returns></returns>
        int IsAddTopAdvSuccess(Adv_Info advInfo);
        #endregion

        #region IsAddClassAdvSuccess �жϷ�����Ϣ�Ƿ���ӳɹ�
        /// <summary>
        /// �жϷ�����Ϣ�Ƿ���ӳɹ�
        /// </summary>
        /// <param name="advInfo">���������ϸ��Ϣ</param>
        /// <returns></returns>
        int IsAddClassAdvSuccess(Adv_Info advInfo);
        #endregion

        #region IsAddFreeAdvSuccess �ж���ѹ���Ƿ���ӳɹ�
        /// <summary>
        /// �ж���ѹ���Ƿ���ӳɹ�
        /// </summary>
        /// <param name="advInfo">��ѹ�����ϸ��Ϣ</param>
        /// <returns></returns>
        bool IsAddFreeAdvSuccess(Adv_Info advInfo);
        #endregion 

        #region UpdateAdvInfoByAdvId ���ݹ��Id���¹�淢���ֶ�
        /// <summary>
        /// ���ݹ��Id���¹�淢���ֶ�
        /// </summary>
        /// <param name="advInfo">���Ļ�����Ϣ IsIssueΪ0ʱ�ù��δ��������IsIssueΪ1ʱ�ѷ�����</param>
        /// <returns></returns>
        bool UpdateAdvInfoByAdvId(Adv_Info advInfo);
        #endregion

        #region SelAdvInfoByAdvId ���ݹ��Id�����Ļ�����Ϣ��ѯ����
        /// <summary>
        /// ���ݹ��Id�����Ļ�����Ϣ��ѯ����
        /// </summary>
        /// <param name="Adv_Id">���Id</param>
        /// <returns></returns>
        DataTable SelAdvInfoByAdvId(int Adv_Id);
        #endregion

        #region DelAdvInfoByAdvId ���ݹ��Id���������ɾ��
        /// <summary>
        /// ���ݹ��Id���������ɾ��
        /// </summary>
        /// <param name="advId">���Id</param>
        /// <returns></returns>
        bool DelAdvInfoByAdvId(int advId);
        #endregion

        #region UpdateAdvByAdvId ���ݹ��Id�޸Ķ����ͷ�������Ϣ
        /// <summary>
        /// ���ݹ��Id�޸Ķ����ͷ�������Ϣ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Content��Adv_Remark��Adv_Title��Adv_Id�ֶ�</param>
        /// <returns></returns>
        bool UpdateAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightAdvByAdvId ���ݹ��Id�޸��Ҳ�����Ϣ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ�����Ϣ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Url��Adv_Remark��BigAdv_Url��Adv_Id�ֶ�</param>
        /// <returns></returns>
        bool UpdateRightAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightBigAdvByAdvId ���ݹ��Id�޸��Ҳ��ͼ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ��ͼ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Remark��BigAdv_Url��Adv_Id�ֶ�</param>
        /// <returns></returns>
        bool UpdateRightBigAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightSmallAdvByAdvId ���ݹ��Id�޸��Ҳ�Сͼ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ�Сͼ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Remark��BigAdv_Url��Adv_Id�ֶ�</param>
        /// <returns></returns>
        bool UpdateRightSmallAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightAdvNameByAdvId ���ݹ��Id�޸��Ҳ�����Ϣ
        /// <summary>
        /// ���ݹ��Id�޸��Ҳ�����Ϣ
        /// </summary>
        /// <param name="advInfo">��ҪAdv_Name��Adv_Remark��Adv_Id�ֶ�</param>
        /// <returns></returns>
        bool UpdateRightAdvNameByAdvId(Adv_Info advInfo);
        #endregion

        #region SelAllAdvInfo �Ҳ���Ļ�����Ϣ
        /// <summary>
        /// ���ݹ��Id�����Ļ�����Ϣ��ѯ����
        /// </summary>
        /// <param name="Adv_Id">���Id</param>
        /// <returns></returns>
        DataTable SelAllAdvInfo();
        #endregion

        #region SelSingleAdvInfo �Ҳ����ǰ������Ϣ
        /// <summary>
        /// ���ݹ��Id�����Ļ�����Ϣ��ѯ����
        /// </summary>
        /// <param name="Adv_Id">���Id</param>
        /// <returns></returns>
        DataTable SelSingleAdvInfo();
        #endregion


        #region  �������Ļ�����Ϣ
        DataTable SeltopAdvInfo();
        #endregion

        #region  ����ȫ�����Ļ�����Ϣ
        DataTable SelAlltopAdvInfo();
        #endregion

        #region  ����������ϸ��Ϣ
        DataTable SeltopInfo(string TSid);
        #endregion

        #region  ����ȫ�����Ļ�����Ϣ
        DataTable SelAlltTypeAdvInfo(string tsid);
        #endregion


    }
}
