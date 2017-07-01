using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.impl.Customer
{
    class UserPublishMsg :DAL.Dao.Customer.IUserPublishMsg
    {
        Util util = new Util();
        #region 用户发布消息
        /// <summary>
        /// 用户发布消息
        /// </summary>
        /// <param name="CustomerInfo">用户填写的详细信息</param>
        /// <returns></returns>
        public  bool InsertT_CustomerPublishedMsg(T_CustomerPublishedMsg CustomerPublishedMsg)
        {
            string cmdText = "insert into T_CustomerPublishedMsg(CustomerID,MessageTitle,MessageContent,time,static) values('" + CustomerPublishedMsg.CustomerID + "','" + CustomerPublishedMsg.MessageTitle + "','" + CustomerPublishedMsg.MessageContent + "','" + CustomerPublishedMsg.Time + "',0)";
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

        #region Sel用户发布的消息
        /// <summary>
        /// Sel用户发布的消息
        /// </summary>
        /// <param name="CustomerInfo">用户填写的详细信息</param>
        /// <returns></returns>
        public DataTable SelT_CustomerPublishedMsgByID(string CustomerID)
        {
            string cmdText = "Select * from T_CustomerPublishedMsg Where CustomerID='"+CustomerID+"'";
            return util.GetDataTable(cmdText);
        }
        #endregion

        #region Sel用户发布的消息
        /// <summary>
        /// Sel用户发布的消息
        /// </summary>
    
        /// <returns></returns>
        public DataTable SelT_CustomerPublishedMsgByIDAndStatic(string CustomerID, int Static)
        {
            string cmdText = "Select * from T_CustomerPublishedMsg Where CustomerID='" + CustomerID + "' and static='"+Static+"'order by time desc";
            return util.GetDataTable(cmdText);
        }
        #endregion

        #region 删除用户发布的消息
        /// <summary>
        /// 删除用户发布的消息
        /// </summary>
       
        /// <returns></returns>
        public bool DelT_CustomerPublishedMsgByID(string MsgID)
        {
            string cmdText = "Delete from T_CustomerPublishedMsg Where MsgID='" + MsgID + "'";
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

        #region 修改用户发布的消息
        /// <summary>
        /// 删除用户发布的消息
        /// </summary>
      
        /// <returns></returns>
        public bool UpdateT_CustomerPublishedMsgByID(string MsgID, string messageTitle,string messageContent)
        {
            string cmdText = "update T_CustomerPublishedMsg  set MessageTitle='" + messageTitle + "',MessageContent='" + messageContent + "' Where MsgID='" + MsgID + "'";
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

        #region dtSelPublishMsgInfo 查询出周边信息记录
        /// <summary>
        /// 查询出周边信息记录
        /// </summary>
        /// <returns></returns>
        public DataTable dtSelPublishMsgInfo() 
        {
            string cmdText = "Select top 4 MsgID,MessageContent,Replace(convert(char,[time],111),'/','-')+'       '+MessageTitle as ContentAndTime from T_CustomerPublishedMsg order by [time] desc";
            return util.GetDataTable(cmdText);
        }
        #endregion

        public DataTable dtSelSelPublishMsgInfoByMsgID(int msgId) 
        {
            string cmdText = "select MsgID,CustomerID,MessageTitle,MessageContent,[time],Static,CName from T_CustomerPublishedMsg,T_CustomerInfo where MsgID='" + msgId + "' and T_CustomerInfo.CId=T_CustomerPublishedMsg.CustomerID";
            return util.GetDataTable(cmdText);
        }

    }
}
