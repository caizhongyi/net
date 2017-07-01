using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.impl.Present
{
    class PersonalScoreInfo : DAL.Dao.IPersonalScoreInfo
    {

        Util util = new Util();
        #region 插入用户积分


        public void InsertPresentScore(string CCID)
        {
            string sqltext = "insert into T_PersonalScoreInfo(CCId,CMonthNum,CCurrNum,CCurrGold,CHisNum,CRemark) values('" + CCID + "','100','100','0','100','null')";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region UpDatePersentScore 根据用户ID更新扣除/增加后的积分
        /// <summary>
        /// 根据用户ID更新扣除/增加后的积分
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="ccurrNum">扣除/增加后的积分</param>
        /// <returns></returns>
        public int UpDatePersentScore(string UserId,float ccurrNum) 
        {
            string cmdText = "update T_PersonalScoreInfo set CCurrNum='" + ccurrNum + "' where CCID='" + UserId + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion
    }
}
