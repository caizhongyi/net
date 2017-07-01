using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.Customer
{
    public interface IUserPublishMsg
    {
        bool InsertT_CustomerPublishedMsg(T_CustomerPublishedMsg CustomerPublishedMsg);
        DataTable SelT_CustomerPublishedMsgByID(string CustomerID);
        DataTable SelT_CustomerPublishedMsgByIDAndStatic(string CustomerID, int Static);
        bool DelT_CustomerPublishedMsgByID(string MsgID);
        bool UpdateT_CustomerPublishedMsgByID(string MsgID, string messageTitle, string messageContent);

        #region dtSelPublishMsgInfo ��ѯ���ܱ���Ϣ��¼
        /// <summary>
        /// ��ѯ���ܱ���Ϣ��¼
        /// </summary>
        /// <returns></returns>
        DataTable dtSelPublishMsgInfo();
        #endregion

        DataTable dtSelSelPublishMsgInfoByMsgID(int msgId);
    }
}
