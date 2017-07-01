using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.AdvInfo
{
    public interface IIssueAreaInfo
    {
        #region InsertIssueAreaInfo ��ӵ�Issue_Area����
        /// <summary>
        /// ��ӵ�Issue_Area����
        /// </summary>
        /// <param name="issueArea">��ҪAdv_Id��User_Id��IssueAreaId��IssueAreaType�ֶΣ�IssueAreaTypeΪ�������������ͣ�1Ϊʡ�ݣ�2Ϊ���У�3Ϊ��/�У�4Ϊ��/С����5ΪȺ����</param>
        /// <returns></returns>
        bool InsertIssueAreaInfo(Issue_Area issueArea);
        #endregion

        #region SelIssueAreaInfoByAdvId ���ݹ��Id��ѯ���������Ϣ
        /// <summary>
        /// ���ݹ��Id��ѯ���������Ϣ
        /// </summary>
        /// <param name="AdvId">���Id����ҪAdv_Id�ֶ�</param>
        /// <param name="IssueTime">������/�޸�ʱ��</param>
        /// <returns></returns>
        DataTable SelIssueAreaInfoByAdvId(int AdvId, DateTime IssueTime);
        #endregion
    }
}
