using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.AdvInfo
{
    public interface IIssueAreaInfo
    {
        #region InsertIssueAreaInfo 添加到Issue_Area表中
        /// <summary>
        /// 添加到Issue_Area表中
        /// </summary>
        /// <param name="issueArea">需要Adv_Id、User_Id、IssueAreaId、IssueAreaType字段，IssueAreaType为发布的区域类型：1为省份，2为城市，3为县/市，4为镇/小区，5为群发布</param>
        /// <returns></returns>
        bool InsertIssueAreaInfo(Issue_Area issueArea);
        #endregion

        #region SelIssueAreaInfoByAdvId 根据广告Id查询广告区域信息
        /// <summary>
        /// 根据广告Id查询广告区域信息
        /// </summary>
        /// <param name="AdvId">广告Id，需要Adv_Id字段</param>
        /// <param name="IssueTime">广告添加/修改时间</param>
        /// <returns></returns>
        DataTable SelIssueAreaInfoByAdvId(int AdvId, DateTime IssueTime);
        #endregion
    }
}
