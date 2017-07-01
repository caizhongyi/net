using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.AdvInfo;
using Model;
using System.Data;

namespace DAL.impl.AdvInfo
{
    class AdvIssueInfoDao:IAdvIssueInfo
    {
        Util util = new Util();

        #region IsIssueSuccess 判断广告是否发布成功
        /// <summary>
        /// 判断广告是否发布成功
        /// </summary>
        /// <param name="advIssue">发布广告需要知道的一些信息</param>
        /// <returns></returns>
        public bool IsIssueSuccess(Adv_Issue advIssue) 
        {
            string cmdText = "insert into Adv_Issue(Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time,IsRatify,NeedNum) values('" + advIssue.Adv_Id + "','" + advIssue.User_Id + "','" + advIssue.Adv_Startday + "','" + advIssue.Adv_Endday + "','" + advIssue.Adv_Issue_Time + "',0,'" + advIssue.NeedNum + "')";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0://广告发布失败
                    return false;
                default://广告发布成功
                    return true;
            }
        }
        #endregion

        #region InsertAdvIssueInfo 添加信息的时候同时添加到广告信息表
        /// <summary>
        /// 添加信息的时候同时添加到广告信息表
        /// </summary>
        /// <param name="advIssue">需要Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time字段</param>
        /// <returns></returns>
        public bool InsertAdvIssueInfo(Adv_Issue advIssue) 
        {
            string cmdText = "insert into Adv_Issue(Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time,IsRatify,NeedNum) values('" + advIssue.Adv_Id + "','" + advIssue.User_Id + "','" + advIssue.Adv_Startday + "','" + advIssue.Adv_Endday + "','" + advIssue.Adv_Issue_Time + "',3,'0')"; ;
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0://广告发布失败
                    return false;
                default://广告发布成功
                    return true;
            }
        }
        #endregion

        #region SelAdvingAdvIssueInfo 根据用户ID将进行中的广告查询出来
        /// <summary>
        /// 根据用户ID将进行中的广告查询出来
        /// </summary>
        /// <param name="advIssue">广告发布信息，需要用户Id和IsRatify字段</param>
        /// <returns></returns>
        public DataSet SelAdvingAdvIssueInfo(Adv_Issue advIssue)
        {
            //string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName as Address,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday>=(select convert(varchar(10),getdate(),120)) and Adv_Issue.IssueAreaId=T_AreaInfo.AId and T_AreaInfo.CId=T_CountyInfo.CId and T_CountyInfo.CityId=T_CityInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday>=(select convert(varchar(10),getdate(),120)) and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAdvIssueByIsRatify 根据IsRatify查询出广告审核是否通过
        /// <summary>
        /// 根据IsRatify查询出广告审核是否通过
        /// </summary>
        /// <param name="advIssue">广告发布信息，需要用户Id和IsRatify字段</param>
        /// <returns></returns>
        public DataSet SelAdvIssueByIsRatify(Adv_Issue advIssue) 
        {
            //string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName as Address,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.IssueAreaId=T_AreaInfo.AId and T_AreaInfo.CId=T_CountyInfo.CId and T_CountyInfo.CityId=T_CityInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelExpiredAdvByIsRatify 根据IsRatify和AdvEndday查询出过期的广告
        /// <summary>
        /// 根据IsRatify查询出过期的广告
        /// </summary>
        /// <param name="advIssue">广告发布信息，需要用户Id和IsRatify字段</param>
        /// <returns></returns>
        public DataSet SelExpiredAdvByIsRatify(Adv_Issue advIssue)
        {
            //string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName as Address,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday<(select convert(varchar(10),getdate(),120)) and Adv_Issue.IssueAreaId=T_AreaInfo.AId and T_AreaInfo.CId=T_CountyInfo.CId and T_CountyInfo.CityId=T_CityInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday<(select convert(varchar(10),getdate(),120)) and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region UpDateAdvIssueByIssueId 不修改小区时，重新发布信息
        /// <summary>
        /// 不修改小区时，重新发布信息
        /// </summary>
        /// <param name="advIssue">重新发布信息的基本信息</param>
        /// <returns></returns>
        public int UpDateAdvIssueByIssueId(Adv_Issue advIssue)
        {
            string cmdText = "update Adv_Issue set Adv_Startday='" + advIssue.Adv_Startday + "',Adv_Endday='" + advIssue.Adv_Endday + "',IsRatify='0' where IssueId='" + advIssue.IssueId + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UpDateAdvIssueByAreaId 重新选择小区并发布信息
        /// <summary>
        /// 重新选择小区并发布信息
        /// </summary>
        /// <param name="advIssue">重新发布信息的基本信息</param>
        /// <returns></returns>
        public int UpDateAdvIssueByAreaId(Adv_Issue advIssue)
        {
            //string cmdText = "update Adv_Issue set Adv_Startday='" + advIssue.Adv_Startday + "',Adv_Endday='" + advIssue.Adv_Endday + "',IssueAreaId='" + advIssue.IssueAreaId + "',IsRatify='0' where IssueId='" + advIssue.IssueId + "'";
            string cmdText = "";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UpdateIsRatifyByIsratify 更新IsRatify字段，0为未审核，1为通过审核，2为没通过审核，3为刚添加的信息
        /// <summary>
        /// 更新IsRatify字段，0为未审核，1为通过审核，2为没通过审核，3为刚添加的信息
        /// </summary>
        /// <param name="advIssue">需要IsRatify、NeedNum、Adv_Id字段</param>
        /// <returns></returns>
        public bool UpdateIsRatifyByIsratify(Adv_Issue advIssue)
        {
            string cmdText = "update Adv_Issue set IsRatify='" + advIssue.IsRatify + "',NeedNum='" + advIssue.NeedNum + "',Adv_Issue_Time='" + advIssue.Adv_Issue_Time + "',Adv_Startday='" + advIssue.Adv_Startday + "',Adv_Endday='" + advIssue.Adv_Endday + "' where Adv_Id='" + advIssue.Adv_Id + "'";
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

        #region UpdateIsRatifyByIsratify 根据广告Id修改IsRatify字段
        /// <summary>
        /// 根据广告Id修改IsRatify字段
        /// </summary>
        /// <param name="IsRatify">审核状态字段，0为未审核，1为通过审核，2为没通过审核，3为刚添加的信息</param>
        /// <param name="Adv_Id">广告Id</param>
        /// <returns></returns>
        public bool UpdateIsRatifyByIsratify(int IsRatify, int Adv_Id)
        {
            string cmdText = "update Adv_Issue set IsRatify='" + IsRatify + "' where Adv_Id='" + Adv_Id + "'";
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
    }
}
