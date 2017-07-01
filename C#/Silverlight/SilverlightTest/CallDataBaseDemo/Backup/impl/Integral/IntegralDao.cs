using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Integral;
using System.Data;

namespace DAL.impl.Integral
{
    class IntegralDao : IIntegral
    {
        Util util = new Util();
        #region 获得积分的基本导航信息

        public string InteralContent()
        {
            string sqltext = "select SContent from T_ScoreInfo";
            return util.GetStrExecuteScalar(sqltext);
        }
        #endregion
        #region 获得对应的礼品信息
        public double GetScoreNum(string PresetnId)
        {
            string sqltext = "select SNum from T_PresentInfo where PId='" + PresetnId + "'";
            return util.GetdoubleExecuteScalar(sqltext);
        }

        #endregion

        #region 获取个人当前积分


        public double GetCurrNum(string CusId)
        {
            string sqltext="select CCurrNum from T_PersonalScoreInfo where CCId='"+CusId+"'";
            return util.GetdoubleExecuteScalar(sqltext);
        }

        #endregion

        #region 获取个人当前金币


        public double GetCurrGold(string CusId)
        {
            string sqltext = "select CCurrGold from T_PersonalScoreInfo where CCId='" + CusId + "'";
            return util.GetdoubleExecuteScalar(sqltext);
        }
        #endregion

        #region 获取友情人数
        public int GetContributeNum(string recommendPeople)
        {
            string sqltext = "select count(*) from T_CustomerInfo where RecommendPeople='" + recommendPeople + "'";
            return util.GetIntExecuteScalar(sqltext);
        }

        #endregion

        #region 获取积分兑换日志


        public System.Data.DataSet GetInteralLog(string CusId)
        {
            string sqltext = "select CCPId,PName,CCPName,CCPNum,SNum,CCPLook,CCPTime from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and T_CustomerChangePresentInfo.CCPCId='" + CusId + "' order by CCPTime desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 删除积分日志记录


        public void DelIntegralLog(string id)
        {
            string sqltext = "delete from T_CustomerChangePresentInfo where T_CustomerChangePresentInfo.CCPId='"+id+"'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region 删除所有积分日志记录


        public void DelAllIntegralLog()
        {
            string sqltext = "delete from T_CustomerChangePresentInfo";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion


        #region 获得审核情况


        public System.Data.DataSet GetInteralLogthrough(string CusId)
        {
            string sqltext = "select CCPId,PName,CCPName,CCPNum,SNum,CCPLook,CCPTime from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and T_CustomerChangePresentInfo.CCPCId='" + CusId + "' and CCPLook='通过审核' order by CCPTime desc";
            return util.GetDataSet(sqltext);
        }

        public System.Data.DataSet GetInteralLogNotthrough(string CusId)
        {
            string sqltext = "select CCPId,PName,CCPName,CCPNum,SNum,CCPLook,CCPTime from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and T_CustomerChangePresentInfo.CCPCId='" + CusId + "' and CCPLook='等待审核' order by CCPTime desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 积分历史记录信息


        public string GetMonthNum(string CusId)
        {
            string sqltext = "select CMonthNum from T_PersonalScoreInfo where CCId='"+CusId+"'";
            return util.GetStrExecuteScalar(sqltext);
        }

        public string GetMonthScore(string CusId)
        {
            string sqltext = "select sum(CMonthScore) from T_ContributeScoreInfo where CUserId='"+CusId+"'";
            return util.GetStrExecuteScalar(sqltext);
        }

        public string GetHisScore(string CusId)
        {
            string sqltext = "select sum(CHisScore) from T_ContributeScoreInfo where CUserId='"+CusId+"'";
            return util.GetStrExecuteScalar(sqltext);
        }

        public string GetHisNum(string CusId)
        {
            string sqltext = "select CHisNum from T_PersonalScoreInfo where CCId='"+CusId+"'";
            return util.GetStrExecuteScalar(sqltext);
        }

        public DataSet GetYqInfo(string CusId)
        {
            string sqltext = "select T_ContributeScoreInfo.CId,CCustomerId,CMonthNum,(CMonthNum*0.1) from T_PersonalScoreInfo,T_ContributeScoreInfo where CUserId='262017D3A10B242B779EACCA00A76C05' and T_PersonalScoreInfo.CCId=T_ContributeScoreInfo.CCustomerId";
            return util.GetDataSet(sqltext);
        }
        #endregion

        #region 拒绝审请


        public DataSet GetInteralLogJJthrough(string cusid)
        {
            string sqltext = "select CCPId,PName,CCPName,CCPNum,SNum,CCPLook,CCPTime from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and T_CustomerChangePresentInfo.CCPCId='" + cusid + "' and CCPLook='拒绝审请' order by CCPTime desc";
            return util.GetDataSet(sqltext);
        }

        #endregion
    }
}
