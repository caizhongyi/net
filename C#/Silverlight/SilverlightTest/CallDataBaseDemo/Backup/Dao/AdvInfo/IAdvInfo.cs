using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvInfo
    {
        #region SelAdvInfoByUserID 根据用户ID将该用户所拥有的广告查询出来
        /// <summary>
        /// 根据用户ID将该用户所拥有的广告查询出来
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="AdvTypeId">广告位置ID</param>
        ///<param name="IsFree">是否是免费广告</param>
        /// <param name="IsIssue">是否已有发布过</param>
        /// <returns></returns>
        DataSet SelAdvInfoByUserID(string UserID, int AdvTypeId, int IsFree, int IsIssue);
        #endregion

        DataSet SelAdvInfoByUserID(string UserID,  int IsFree, int IsIssue);

        #region IsAddRightAdvSuccess 根据用户ID将该用户要添加的右侧广告信息添加到数据库中
        /// <summary>
        /// 根据用户ID将该用户要添加的右侧广告信息添加到数据库中
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int IsAddRightAdvSuccess(Adv_Info advInfo);
        #endregion

        #region IsAddTopAdvSuccess 判断顶部滚动信息是否添加成功
        /// <summary>
        /// 判断顶部滚动信息是否添加成功
        /// </summary>
        /// <param name="advInfo">顶部滚动广告的详细信息</param>
        /// <returns></returns>
        int IsAddTopAdvSuccess(Adv_Info advInfo);
        #endregion

        #region IsAddClassAdvSuccess 判断分类信息是否添加成功
        /// <summary>
        /// 判断分类信息是否添加成功
        /// </summary>
        /// <param name="advInfo">分类广告的详细信息</param>
        /// <returns></returns>
        int IsAddClassAdvSuccess(Adv_Info advInfo);
        #endregion

        #region IsAddFreeAdvSuccess 判断免费广告是否添加成功
        /// <summary>
        /// 判断免费广告是否添加成功
        /// </summary>
        /// <param name="advInfo">免费广告的详细信息</param>
        /// <returns></returns>
        bool IsAddFreeAdvSuccess(Adv_Info advInfo);
        #endregion 

        #region UpdateAdvInfoByAdvId 根据广告Id更新广告发布字段
        /// <summary>
        /// 根据广告Id更新广告发布字段
        /// </summary>
        /// <param name="advInfo">广告的基本信息 IsIssue为0时该广告未发布过，IsIssue为1时已发布过</param>
        /// <returns></returns>
        bool UpdateAdvInfoByAdvId(Adv_Info advInfo);
        #endregion

        #region SelAdvInfoByAdvId 根据广告Id将广告的基本信息查询出来
        /// <summary>
        /// 根据广告Id将广告的基本信息查询出来
        /// </summary>
        /// <param name="Adv_Id">广告Id</param>
        /// <returns></returns>
        DataTable SelAdvInfoByAdvId(int Adv_Id);
        #endregion

        #region DelAdvInfoByAdvId 根据广告Id将该条广告删除
        /// <summary>
        /// 根据广告Id将该条广告删除
        /// </summary>
        /// <param name="advId">广告Id</param>
        /// <returns></returns>
        bool DelAdvInfoByAdvId(int advId);
        #endregion

        #region UpdateAdvByAdvId 根据广告Id修改顶部和分类广告信息
        /// <summary>
        /// 根据广告Id修改顶部和分类广告信息
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Content、Adv_Remark、Adv_Title、Adv_Id字段</param>
        /// <returns></returns>
        bool UpdateAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightAdvByAdvId 根据广告Id修改右侧广告信息
        /// <summary>
        /// 根据广告Id修改右侧广告信息
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Url、Adv_Remark、BigAdv_Url、Adv_Id字段</param>
        /// <returns></returns>
        bool UpdateRightAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightBigAdvByAdvId 根据广告Id修改右侧大图
        /// <summary>
        /// 根据广告Id修改右侧大图
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Remark、BigAdv_Url、Adv_Id字段</param>
        /// <returns></returns>
        bool UpdateRightBigAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightSmallAdvByAdvId 根据广告Id修改右侧小图
        /// <summary>
        /// 根据广告Id修改右侧小图
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Remark、BigAdv_Url、Adv_Id字段</param>
        /// <returns></returns>
        bool UpdateRightSmallAdvByAdvId(Adv_Info advInfo);
        #endregion

        #region UpdateRightAdvNameByAdvId 根据广告Id修改右侧广告信息
        /// <summary>
        /// 根据广告Id修改右侧广告信息
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Remark、Adv_Id字段</param>
        /// <returns></returns>
        bool UpdateRightAdvNameByAdvId(Adv_Info advInfo);
        #endregion

        #region SelAllAdvInfo 右侧广告的基本信息
        /// <summary>
        /// 根据广告Id将广告的基本信息查询出来
        /// </summary>
        /// <param name="Adv_Id">广告Id</param>
        /// <returns></returns>
        DataTable SelAllAdvInfo();
        #endregion

        #region SelSingleAdvInfo 右侧广告的前几条信息
        /// <summary>
        /// 根据广告Id将广告的基本信息查询出来
        /// </summary>
        /// <param name="Adv_Id">广告Id</param>
        /// <returns></returns>
        DataTable SelSingleAdvInfo();
        #endregion


        #region  顶部广告的基本信息
        DataTable SeltopAdvInfo();
        #endregion

        #region  顶部全部广告的基本信息
        DataTable SelAlltopAdvInfo();
        #endregion

        #region  顶部广告的详细信息
        DataTable SeltopInfo(string TSid);
        #endregion

        #region  分类全部广告的基本信息
        DataTable SelAlltTypeAdvInfo(string tsid);
        #endregion


    }
}
