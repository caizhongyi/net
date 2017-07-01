using System;
namespace DAL.Dao
{
    public interface IPersonalScoreInfo
    {
        void InsertPresentScore(string CCID);

        #region UpDatePersentScore 根据用户ID更新扣除/增加后的积分
        /// <summary>
        /// 根据用户ID更新扣除/增加后的积分
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="ccurrNum">扣除/增加后的积分</param>
        /// <returns></returns>
        int UpDatePersentScore(string UserId, float ccurrNum);
        #endregion
    }
}
