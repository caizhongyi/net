using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvIssueInfo
    {
        #region IsIssueSuccess �жϹ���Ƿ񷢲��ɹ�
        /// <summary>
        /// �жϹ���Ƿ񷢲��ɹ�
        /// </summary>
        /// <param name="advIssue">���������Ҫ֪����һЩ��Ϣ</param>
        /// <returns></returns>
        bool IsIssueSuccess(Adv_Issue advIssue);
        #endregion

        #region InsertAdvIssueInfo �����Ϣ��ʱ��ͬʱ��ӵ������Ϣ��
        /// <summary>
        /// �����Ϣ��ʱ��ͬʱ��ӵ������Ϣ��
        /// </summary>
        /// <param name="advIssue">Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time�ֶ�</param>
        /// <returns></returns>
        bool InsertAdvIssueInfo(Adv_Issue advIssue);
        #endregion

        #region SelAdvingAdvIssueInfo �����û�ID�������еĹ���ѯ����
        /// <summary>
        /// �����û�ID�������еĹ���ѯ����
        /// </summary>
        /// <param name="advIssue">��淢����Ϣ����Ҫ�û�Id��IsRatify�ֶ�</param>
        /// <returns></returns>
        DataSet SelAdvingAdvIssueInfo(Adv_Issue advIssue);
        #endregion

        #region SelAdvIssueByIsRatify ����IsRatify��ѯ���������Ƿ�ͨ��
        /// <summary>
        /// ����IsRatify��ѯ���������Ƿ�ͨ��
        /// </summary>
        /// <param name="advIssue">��淢����Ϣ����Ҫ�û�Id��IsRatify�ֶ�</param>
        /// <returns></returns>
        DataSet SelAdvIssueByIsRatify(Adv_Issue advIssue);
        #endregion

        #region SelExpiredAdvByIsRatify ����IsRatify��AdvEndday��ѯ�����ڵĹ��
        /// <summary>
        /// ����IsRatify��ѯ�����ڵĹ��
        /// </summary>
        /// <param name="advIssue">��淢����Ϣ����Ҫ�û�Id��IsRatify�ֶ�</param>
        /// <returns></returns>
        DataSet SelExpiredAdvByIsRatify(Adv_Issue advIssue);
        #endregion

        #region UpDateAdvIssueByIssueId ���޸�С��ʱ�����·�����Ϣ
        /// <summary>
        /// ���޸�С��ʱ�����·�����Ϣ
        /// </summary>
        /// <param name="advIssue">���·�����Ϣ�Ļ�����Ϣ</param>
        /// <returns></returns>
        int UpDateAdvIssueByIssueId(Adv_Issue advIssue);
        #endregion

        #region UpDateAdvIssueByAreaId ����ѡ��С����������Ϣ
        /// <summary>
        /// ����ѡ��С����������Ϣ
        /// </summary>
        /// <param name="advIssue">���·�����Ϣ�Ļ�����Ϣ</param>
        /// <returns></returns>
        int UpDateAdvIssueByAreaId(Adv_Issue advIssue);
        #endregion

        #region UpdateIsRatifyByIsratify ����IsRatify�ֶΣ�0Ϊδ��ˣ�1Ϊͨ����ˣ�2Ϊûͨ����ˣ�3Ϊ����ӵ���Ϣ
        /// <summary>
        /// ����IsRatify�ֶΣ�0Ϊδ��ˣ�1Ϊͨ����ˣ�2Ϊûͨ����ˣ�3Ϊ����ӵ���Ϣ
        /// </summary>
        /// <param name="advIssue">��ҪIsRatify��NeedNum��Adv_Id�ֶ�</param>
        /// <returns></returns>
        bool UpdateIsRatifyByIsratify(Adv_Issue advIssue);
        #endregion

        #region UpdateIsRatifyByIsratify ���ݹ��Id�޸�IsRatify�ֶ�
        /// <summary>
        /// ���ݹ��Id�޸�IsRatify�ֶ�
        /// </summary>
        /// <param name="IsRatify">���״̬�ֶΣ�0Ϊδ��ˣ�1Ϊͨ����ˣ�2Ϊûͨ����ˣ�3Ϊ����ӵ���Ϣ</param>
        /// <param name="Adv_Id">���Id</param>
        /// <returns></returns>
        bool UpdateIsRatifyByIsratify(int IsRatify, int Adv_Id);
        #endregion
    }
}
