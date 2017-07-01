using System;
using System.Collections.Generic;
using System.Text;
using Model;
using DAL.Dao.AdvInfo;
using System.Data;

namespace DAL.impl.AdvInfo
{
    class IssueAreaDao:IIssueAreaInfo
    {
        Util util = new Util();

        #region InsertIssueAreaInfo ��ӵ�Issue_Area����
        /// <summary>
        /// ��ӵ�Issue_Area����
        /// </summary>
        /// <param name="issueArea">��ҪAdv_Id��User_Id��IssueAreaId��IssueAreaType�ֶΣ�IssueAreaTypeΪ�������������ͣ�1Ϊʡ�ݣ�2Ϊ���У�3Ϊ��/�У�4Ϊ��/С����5ΪȺ����</param>
        /// <returns></returns>
        public bool InsertIssueAreaInfo(Issue_Area issueArea) 
        {
            string cmdText = "insert into Issue_Area values ('" + issueArea.Adv_Id + "','" + issueArea.User_Id + "','" + issueArea.IssueAreaId + "','" + issueArea.IssueAreaType + "','" + issueArea.IssueTime + "')";
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

        #region SelIssueAreaInfoByAdvId ���ݹ��Id��ѯ���������Ϣ
        /// <summary>
        /// ���ݹ��Id��ѯ���������Ϣ
        /// </summary>
        /// <param name="AdvId">���Id����ҪAdv_Id�ֶ�</param>
        /// <param name="IssueTime">������/�޸�ʱ��</param>
        /// <returns></returns>
        public DataTable SelIssueAreaInfoByAdvId(int AdvId,DateTime IssueTime) 
        {
            string cmdText = "select * from Issue_Area where Adv_Id='" + AdvId + "' and IssueTime='" + IssueTime + "'";
            return util.GetDataSet(cmdText).Tables[0];
        }
        #endregion
    }
}
