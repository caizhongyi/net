using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.impl.Present
{
    class PersonalScoreInfo : DAL.Dao.IPersonalScoreInfo
    {

        Util util = new Util();
        #region �����û�����


        public void InsertPresentScore(string CCID)
        {
            string sqltext = "insert into T_PersonalScoreInfo(CCId,CMonthNum,CCurrNum,CCurrGold,CHisNum,CRemark) values('" + CCID + "','100','100','0','100','null')";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region UpDatePersentScore �����û�ID���¿۳�/���Ӻ�Ļ���
        /// <summary>
        /// �����û�ID���¿۳�/���Ӻ�Ļ���
        /// </summary>
        /// <param name="UserId">�û�Id</param>
        /// <param name="ccurrNum">�۳�/���Ӻ�Ļ���</param>
        /// <returns></returns>
        public int UpDatePersentScore(string UserId,float ccurrNum) 
        {
            string cmdText = "update T_PersonalScoreInfo set CCurrNum='" + ccurrNum + "' where CCID='" + UserId + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion
    }
}
