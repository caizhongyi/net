using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvIssueInfo
    {
        #region IsIssueSuccess 判断广告是否发布成功
        /// <summary>
        /// 判断广告是否发布成功
        /// </summary>
        /// <param name="advIssue">发布广告需要知道的一些信息</param>
        /// <returns></returns>
        bool IsIssueSuccess(Adv_Issue advIssue);
        #endregion

        #region InsertAdvIssueInfo 添加信息的时候同时添加到广告信息表
        /// <summary>
        /// 添加信息的时候同时添加到广告信息表
        /// </summary>
        /// <param name="advIssue">Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time字段</param>
        /// <returns></returns>
        bool InsertAdvIssueInfo(Adv_Issue advIssue);
        #endregion

        #region SelAdvingAdvIssueInfo 根据用户ID将进行中的广告查询出来
        /// <summary>
        /// 根据用户ID将进行中的广告查询出来
        /// </summary>
        /// <param name="advIssue">广告发布信息，需要用户Id和IsRatify字段</param>
        /// <returns></returns>
        DataSet SelAdvingAdvIssueInfo(Adv_Issue advIssue);
        #endregion

        #region SelAdvIssueByIsRatify 根据IsRatify查询出广告审核是否通过
        /// <summary>
        /// 根据IsRatify查询出广告审核是否通过
        /// </summary>
        /// <param name="advIssue">广告发布信息，需要用户Id和IsRatify字段</param>
        /// <returns></returns>
        DataSet SelAdvIssueByIsRatify(Adv_Issue advIssue);
        #endregion

        #region SelExpiredAdvByIsRatify 根据IsRatify和AdvEndday查询出过期的广告
        /// <summary>
        /// 根据IsRatify查询出过期的广告
        /// </summary>
        /// <param name="advIssue">广告发布信息，需要用户Id和IsRatify字段</param>
        /// <returns></returns>
        DataSet SelExpiredAdvByIsRatify(Adv_Issue advIssue);
        #endregion

        #region UpDateAdvIssueByIssueId 不修改小区时，重新发布信息
        /// <summary>
        /// 不修改小区时，重新发布信息
        /// </summary>
        /// <param name="advIssue">重新发布信息的基本信息</param>
        /// <returns></returns>
        int UpDateAdvIssueByIssueId(Adv_Issue advIssue);
        #endregion

        #region UpDateAdvIssueByAreaId 重新选择小区并发布信息
        /// <summary>
        /// 重新选择小区并发布信息
        /// </summary>
        /// <param name="advIssue">重新发布信息的基本信息</param>
        /// <returns></returns>
        int UpDateAdvIssueByAreaId(Adv_Issue advIssue);
        #endregion

        #region UpdateIsRatifyByIsratify 更新IsRatify字段，0为未审核，1为通过审核，2为没通过审核，3为刚添加的信息
        /// <summary>
        /// 更新IsRatify字段，0为未审核，1为通过审核，2为没通过审核，3为刚添加的信息
        /// </summary>
        /// <param name="advIssue">需要IsRatify、NeedNum、Adv_Id字段</param>
        /// <returns></returns>
        bool UpdateIsRatifyByIsratify(Adv_Issue advIssue);
        #endregion

        #region UpdateIsRatifyByIsratify 根据广告Id修改IsRatify字段
        /// <summary>
        /// 根据广告Id修改IsRatify字段
        /// </summary>
        /// <param name="IsRatify">审核状态字段，0为未审核，1为通过审核，2为没通过审核，3为刚添加的信息</param>
        /// <param name="Adv_Id">广告Id</param>
        /// <returns></returns>
        bool UpdateIsRatifyByIsratify(int IsRatify, int Adv_Id);
        #endregion
    }
}
