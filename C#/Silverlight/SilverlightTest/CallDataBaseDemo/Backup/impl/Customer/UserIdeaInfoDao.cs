using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using Model;

namespace DAL.impl.Customer
{
    class UserIdeaInfoDao : IUserIdeaInfo
    {
        Util util = new Util();

        #region InsertIdearByUserID ���û�������������ӵ����ݿ���
        /// <summary>
        /// ���û�������������ӵ����ݿ���
        /// </summary>
        /// <param name="customerIdeaInfo">�û��������ϸ��Ϣ</param>
        /// <returns></returns>
        public int InsertIdearByUserID(T_CustomerIdeaInfo customerIdeaInfo)
        {
            //�������Ҫ��˳��
            string cmdText = "insert into T_CustomerIdeaInfo values('" + customerIdeaInfo.ITitle + "','" + customerIdeaInfo.ITId + "','" + customerIdeaInfo.IContent + "','" + customerIdeaInfo.ITime + "','" + customerIdeaInfo.ICId + "','"+customerIdeaInfo.IRemark+"')";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion
    }
}
