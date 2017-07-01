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

        #region InsertIssueAreaInfo 添加到Issue_Area表中
        /// <summary>
        /// 添加到Issue_Area表中
        /// </summary>
        /// <param name="issueArea">需要Adv_Id、User_Id、IssueAreaId、IssueAreaType字段，IssueAreaType为发布的区域类型：1为省份，2为城市，3为县/市，4为镇/小区，5为群发布</param>
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

        #region SelIssueAreaInfoByAdvId 根据广告Id查询广告区域信息
        /// <summary>
        /// 根据广告Id查询广告区域信息
        /// </summary>
        /// <param name="AdvId">广告Id，需要Adv_Id字段</param>
        /// <param name="IssueTime">广告添加/修改时间</param>
        /// <returns></returns>
        public DataTable SelIssueAreaInfoByAdvId(int AdvId,DateTime IssueTime) 
        {
            string cmdText = "select * from Issue_Area where Adv_Id='" + AdvId + "' and IssueTime='" + IssueTime + "'";
            return util.GetDataSet(cmdText).Tables[0];
        }
        #endregion
    }
}
