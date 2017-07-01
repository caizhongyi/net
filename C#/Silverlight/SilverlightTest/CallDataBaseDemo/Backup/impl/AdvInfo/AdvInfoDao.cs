using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.AdvInfo;
using System.Data;
using Model;

namespace DAL.impl.AdvInfo
{
    class AdvInfoDao : IAdvInfo
    {
        Util util = new Util();

        #region SelAdvInfoByUserID 根据用户ID将该用户所拥有的广告查询出来
        /// <summary>
        /// 根据用户ID将该用户所拥有的广告查询出来
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="AdvTypeId">广告位置ID</param>
        ///<param name="IsFree">是否是免费广告</param>
        /// <param name="IsIssue">是否已有发布过</param>
        /// <returns></returns>
        public DataSet SelAdvInfoByUserID(string UserID,int AdvTypeId,int IsFree,int IsIssue)
        {
            string cmdText = "select Adv_Info.Adv_Id,Adv_Info.Adv_Name,Adv_Info.Adv_Time,Adv_Info.BigAdv_Url,Adv_Info.Adv_Title,Adv_Info.Adv_Content,T_BigTypeInfo.TBName,T_SmalllTypeInfo.TSName,Adv_Info.IsIssue from Adv_Info,T_BigTypeInfo,T_SmalllTypeInfo where Adv_Info.User_Id='" + UserID + "' and Adv_Info.Adv_Type_Id='" + AdvTypeId + "' and Adv_Info.IsFree='" + IsFree + "' and Adv_Info.IsIssue='" + IsIssue + "' and Adv_Info.TBid=T_BigTypeInfo.TBid and Adv_Info.TSid=T_SmalllTypeInfo.TSid";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAdvInfoByUserID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsFree"></param>
        /// <param name="IsIssue"></param>
        /// <returns></returns>
        public DataSet SelAdvInfoByUserID(string UserID,  int IsFree, int IsIssue)
        {
            string cmdText = "select * from Adv_Info,T_BigTypeInfo,T_SmalllTypeInfo,Adv_Type where Adv_Info.User_Id='" + UserID + "' and Adv_Info.IsFree='" + IsFree + "' and Adv_Info.IsIssue='" + IsIssue + "' and Adv_Info.TBid=T_BigTypeInfo.TBid and Adv_Info.TSid=T_SmalllTypeInfo.TSid and Adv_Type.Adv_Type_Id=Adv_Info.Adv_Type_Id";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region IsAddRightAdvSuccess 根据用户ID将该用户要添加的右侧广告信息添加到数据库中
        /// <summary>
        /// 根据用户ID将该用户要添加的右侧广告信息添加到数据库中并得到Id
        /// </summary>
        /// <returns></returns>
        public int IsAddRightAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,BigAdv_Url,Adv_Url,TBid,TSid,Adv_Remark,Adv_Type_Id,Adv_Time,User_Id,IsFree,IsIssue,Adv_Content,Adv_Title) values('" + advInfo.Adv_Name + "','" + advInfo.BigAdv_Url + "','" + advInfo.Adv_Url + "','" + advInfo.TBid + "','" + advInfo.TSid + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "','" + advInfo.IsIssue + "','','') select @@identity";
            return util.GetIntExecuteScalar(cmdText);
        }
        #endregion

        #region IsAddTopAdvSuccess 判断顶部滚动信息是否添加成功
        /// <summary>
        /// 判断顶部滚动信息是否添加成功
        /// </summary>
        /// <param name="advInfo">顶部滚动广告的详细信息</param>
        /// <returns></returns>
        public int IsAddTopAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,Adv_Title,Adv_Content,TSid,TBid,Adv_Type_Id,Adv_Remark,Adv_Time,User_Id,IsFree,IsIssue,Adv_Url,BigAdv_Url) values('" + advInfo.Adv_Name + "','" + advInfo.Adv_Title + "','" + advInfo.Adv_Content + "','" + advInfo.TSid + "','" + advInfo.TBid + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "','" + advInfo.IsIssue + "','','') select @@identity";
            return util.GetIntExecuteScalar(cmdText);
        }
        #endregion

        #region IsAddClassAdvSuccess 判断分类信息是否添加成功
        /// <summary>
        /// 判断分类信息是否添加成功
        /// </summary>
        /// <param name="advInfo">分类广告的详细信息</param>
        /// <returns></returns>
        public int IsAddClassAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,Adv_Title,Adv_Content,TSid,TBid,Adv_Type_Id,Adv_Remark,Adv_Time,User_Id,IsFree,IsIssue,Adv_Url,BigAdv_Url) values('" + advInfo.Adv_Name + "','" + advInfo.Adv_Title + "','" + advInfo.Adv_Content + "','" + advInfo.TSid + "','" + advInfo.TBid + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "','" + advInfo.IsIssue + "','','') select @@identity";
            return util.GetIntExecuteScalar(cmdText);
        }
        #endregion

        #region IsAddFreeAdvSuccess 判断免费广告是否添加成功
        /// <summary>
        /// 判断免费广告是否添加成功
        /// </summary>
        /// <param name="advInfo">免费广告的详细信息</param>
        /// <returns></returns>
        public bool IsAddFreeAdvSuccess(Adv_Info advInfo)
        {
            string cmdText = "insert into Adv_Info(Adv_Name,Adv_Title,Adv_Content,TSid,TBid,Adv_Type_Id,Adv_Remark,Adv_Time,User_Id,IsFree) values('" + advInfo.Adv_Name + "','" + advInfo.Adv_Title + "','" + advInfo.Adv_Content + "','" + advInfo.TSid + "','" + advInfo.TBid + "','" + advInfo.Adv_Type_Id + "','" + advInfo.Adv_Remark + "','" + advInfo.Adv_Time + "','" + advInfo.User_Id + "','" + advInfo.IsFree + "')";
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

        #region UpdateAdvInfoByAdvId 根据广告Id更新广告发布字段
        /// <summary>
        /// 根据广告Id更新广告发布字段
        /// </summary>
        /// <param name="advInfo">广告的基本信息 IsIssue为0时该广告未发布过，IsIssue为1时已发布过</param>
        /// <returns></returns>
        public bool UpdateAdvInfoByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set IsIssue='" + advInfo.IsIssue + "' where Adv_Id='" + advInfo.Adv_Id + "'";
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

        #region SelAdvInfoByAdvId 根据广告Id将广告的基本信息查询出来
        /// <summary>
        /// 根据广告Id将广告的基本信息查询出来
        /// </summary>
        /// <param name="Adv_Id">广告Id</param>
        /// <returns></returns>
        public DataTable SelAdvInfoByAdvId(int Adv_Id)
        {
            string cmdText = "select * from Adv_Info,Adv_Issue where Adv_Info.Adv_Id='" + Adv_Id + "'and Adv_Info.Adv_Id=Adv_Issue.Adv_Id";
            return util.GetDataSet(cmdText).Tables[0];
        }
        #endregion

        #region DelAdvInfoByAdvId 根据广告Id将该条广告删除
        /// <summary>
        /// 根据广告Id将该条广告删除
        /// </summary>
        /// <param name="advId">广告Id</param>
        /// <returns></returns>
        public bool DelAdvInfoByAdvId(int advId)
        {
            string cmdText = "delete from Adv_Info where Adv_Id='" + advId + "'";
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

        #region UpdateAdvByAdvId 根据广告Id修改顶部和分类广告信息
        /// <summary>
        /// 根据广告Id修改顶部和分类广告信息
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Content、Adv_Remark、Adv_Title、Adv_Id字段</param>
        /// <returns></returns>
        public bool UpdateAdvByAdvId(Adv_Info advInfo) 
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Content='" + advInfo.Adv_Content + "',Adv_Remark='" + advInfo.Adv_Remark + "',Adv_Title='" + advInfo.Adv_Title + "' where Adv_Id='" + advInfo.Adv_Id + "'";
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

        #region UpdateRightAdvByAdvId 根据广告Id修改右侧广告信息
        /// <summary>
        /// 根据广告Id修改右侧广告信息
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Url、Adv_Remark、BigAdv_Url、Adv_Id字段</param>
        /// <returns></returns>
        public bool UpdateRightAdvByAdvId(Adv_Info advInfo) 
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Url='" + advInfo.Adv_Url + "',Adv_Remark='" + advInfo.Adv_Remark + "',BigAdv_Url='" + advInfo.BigAdv_Url + "' where Adv_Id='" + advInfo.Adv_Id + "'";
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

        #region UpdateRightBigAdvByAdvId 根据广告Id修改右侧大图
        /// <summary>
        /// 根据广告Id修改右侧大图
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Remark、BigAdv_Url、Adv_Id字段</param>
        /// <returns></returns>
        public bool UpdateRightBigAdvByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Remark='" + advInfo.Adv_Remark + "',BigAdv_Url='" + advInfo.BigAdv_Url + "' where Adv_Id='" + advInfo.Adv_Id + "'";
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

        #region UpdateRightSmallAdvByAdvId 根据广告Id修改右侧小图
        /// <summary>
        /// 根据广告Id修改右侧小图
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Remark、BigAdv_Url、Adv_Id字段</param>
        /// <returns></returns>
        public bool UpdateRightSmallAdvByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Remark='" + advInfo.Adv_Remark + "',Adv_Url='" + advInfo.Adv_Url + "' where Adv_Id='" + advInfo.Adv_Id + "'";
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

        #region UpdateRightAdvNameByAdvId 根据广告Id修改右侧广告信息
        /// <summary>
        /// 根据广告Id修改右侧广告信息
        /// </summary>
        /// <param name="advInfo">需要Adv_Name、Adv_Remark、Adv_Id字段</param>
        /// <returns></returns>
        public bool UpdateRightAdvNameByAdvId(Adv_Info advInfo)
        {
            string cmdText = "update Adv_Info set Adv_Name='" + advInfo.Adv_Name + "',Adv_Remark='" + advInfo.Adv_Remark + "' where Adv_Id='" + advInfo.Adv_Id + "'";
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

        #region 读取右侧广告


        public DataTable SelAllAdvInfo()
        {
            string sqltext = "select Adv_Id,Adv_Name,Adv_Url from Adv_Info where Adv_Type_Id='6' order by Adv_Time desc ";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region 读取顶部信息


        public DataTable SeltopAdvInfo()
        {
            string sqltext = "select top 10 Adv_Id,Adv_Name from Adv_Info where Adv_Type_Id='3' order by Adv_Time desc";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region 读取全部顶部信息


        public DataTable SelAlltopAdvInfo()
        {
            string sqltext = "select Adv_Id,Adv_Title,Adv_Content,Adv_Time from Adv_Info where Adv_Type_Id='3' order by Adv_Time desc";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region IAdvInfo 右侧广告的前几条信息


        public DataTable SelSingleAdvInfo()
        {
            string sqltext = "select top 16 Adv_Id,Adv_Name,Adv_Url from Adv_Info where Adv_Type_Id='6' order by Adv_Time desc ";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region IAdvInfo 成员


        public DataTable SeltopInfo(string TSid)
        {
            string sqltext = "select * from T_SmalllTypeInfo,Adv_Info where Adv_Id='"+TSid+"' and T_SmalllTypeInfo.TSid=Adv_Info.TSid";
            return util.GetDataTable(sqltext);
        }

        #endregion

        #region IAdvInfo 成员


        public DataTable SelAlltTypeAdvInfo(string tsid)
        {
            string sqltext = "select * from Adv_Info where Adv_Type_Id='3' and TSid='"+tsid+"' order by Adv_Time desc";
            return util.GetDataTable(sqltext);
        }

        #endregion
    }
}
