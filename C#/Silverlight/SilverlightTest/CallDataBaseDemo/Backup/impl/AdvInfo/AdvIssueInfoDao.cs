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

        #region IsIssueSuccess �жϹ���Ƿ񷢲��ɹ�
        /// <summary>
        /// �жϹ���Ƿ񷢲��ɹ�
        /// </summary>
        /// <param name="advIssue">���������Ҫ֪����һЩ��Ϣ</param>
        /// <returns></returns>
        public bool IsIssueSuccess(Adv_Issue advIssue) 
        {
            string cmdText = "insert into Adv_Issue(Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time,IsRatify,NeedNum) values('" + advIssue.Adv_Id + "','" + advIssue.User_Id + "','" + advIssue.Adv_Startday + "','" + advIssue.Adv_Endday + "','" + advIssue.Adv_Issue_Time + "',0,'" + advIssue.NeedNum + "')";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0://��淢��ʧ��
                    return false;
                default://��淢���ɹ�
                    return true;
            }
        }
        #endregion

        #region InsertAdvIssueInfo �����Ϣ��ʱ��ͬʱ��ӵ������Ϣ��
        /// <summary>
        /// �����Ϣ��ʱ��ͬʱ��ӵ������Ϣ��
        /// </summary>
        /// <param name="advIssue">��ҪAdv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time�ֶ�</param>
        /// <returns></returns>
        public bool InsertAdvIssueInfo(Adv_Issue advIssue) 
        {
            string cmdText = "insert into Adv_Issue(Adv_Id,User_Id,Adv_Startday,Adv_Endday,Adv_Issue_Time,IsRatify,NeedNum) values('" + advIssue.Adv_Id + "','" + advIssue.User_Id + "','" + advIssue.Adv_Startday + "','" + advIssue.Adv_Endday + "','" + advIssue.Adv_Issue_Time + "',3,'0')"; ;
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0://��淢��ʧ��
                    return false;
                default://��淢���ɹ�
                    return true;
            }
        }
        #endregion

        #region SelAdvingAdvIssueInfo �����û�ID�������еĹ���ѯ����
        /// <summary>
        /// �����û�ID�������еĹ���ѯ����
        /// </summary>
        /// <param name="advIssue">��淢����Ϣ����Ҫ�û�Id��IsRatify�ֶ�</param>
        /// <returns></returns>
        public DataSet SelAdvingAdvIssueInfo(Adv_Issue advIssue)
        {
            //string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName as Address,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday>=(select convert(varchar(10),getdate(),120)) and Adv_Issue.IssueAreaId=T_AreaInfo.AId and T_AreaInfo.CId=T_CountyInfo.CId and T_CountyInfo.CityId=T_CityInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday>=(select convert(varchar(10),getdate(),120)) and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAdvIssueByIsRatify ����IsRatify��ѯ���������Ƿ�ͨ��
        /// <summary>
        /// ����IsRatify��ѯ���������Ƿ�ͨ��
        /// </summary>
        /// <param name="advIssue">��淢����Ϣ����Ҫ�û�Id��IsRatify�ֶ�</param>
        /// <returns></returns>
        public DataSet SelAdvIssueByIsRatify(Adv_Issue advIssue) 
        {
            //string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName as Address,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.IssueAreaId=T_AreaInfo.AId and T_AreaInfo.CId=T_CountyInfo.CId and T_CountyInfo.CityId=T_CityInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelExpiredAdvByIsRatify ����IsRatify��AdvEndday��ѯ�����ڵĹ��
        /// <summary>
        /// ����IsRatify��ѯ�����ڵĹ��
        /// </summary>
        /// <param name="advIssue">��淢����Ϣ����Ҫ�û�Id��IsRatify�ֶ�</param>
        /// <returns></returns>
        public DataSet SelExpiredAdvByIsRatify(Adv_Issue advIssue)
        {
            //string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName as Address,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday<(select convert(varchar(10),getdate(),120)) and Adv_Issue.IssueAreaId=T_AreaInfo.AId and T_AreaInfo.CId=T_CountyInfo.CId and T_CountyInfo.CityId=T_CityInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            string cmdText = "select Adv_Issue.Adv_Id,Adv_Info.Adv_Name,Adv_Issue.User_Id,Adv_Issue.Adv_Startday,Adv_Issue.Adv_Endday,Adv_Issue.Adv_Issue_Time,Adv_Issue.IsRatify,Adv_Issue.IssueId,Adv_Type.Adv_Type_Name from Adv_Issue,Adv_Info,Adv_Type where Adv_Issue.User_Id='" + advIssue.User_Id + "' and Adv_Issue.IsRatify='" + advIssue.IsRatify + "' and Adv_Issue.Adv_Endday<(select convert(varchar(10),getdate(),120)) and Adv_Issue.Adv_Id=Adv_Info.Adv_Id and Adv_Info.Adv_Type_Id=Adv_Type.Adv_Type_Id order by Adv_Issue_Time desc";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region UpDateAdvIssueByIssueId ���޸�С��ʱ�����·�����Ϣ
        /// <summary>
        /// ���޸�С��ʱ�����·�����Ϣ
        /// </summary>
        /// <param name="advIssue">���·�����Ϣ�Ļ�����Ϣ</param>
        /// <returns></returns>
        public int UpDateAdvIssueByIssueId(Adv_Issue advIssue)
        {
            string cmdText = "update Adv_Issue set Adv_Startday='" + advIssue.Adv_Startday + "',Adv_Endday='" + advIssue.Adv_Endday + "',IsRatify='0' where IssueId='" + advIssue.IssueId + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UpDateAdvIssueByAreaId ����ѡ��С����������Ϣ
        /// <summary>
        /// ����ѡ��С����������Ϣ
        /// </summary>
        /// <param name="advIssue">���·�����Ϣ�Ļ�����Ϣ</param>
        /// <returns></returns>
        public int UpDateAdvIssueByAreaId(Adv_Issue advIssue)
        {
            //string cmdText = "update Adv_Issue set Adv_Startday='" + advIssue.Adv_Startday + "',Adv_Endday='" + advIssue.Adv_Endday + "',IssueAreaId='" + advIssue.IssueAreaId + "',IsRatify='0' where IssueId='" + advIssue.IssueId + "'";
            string cmdText = "";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UpdateIsRatifyByIsratify ����IsRatify�ֶΣ�0Ϊδ��ˣ�1Ϊͨ����ˣ�2Ϊûͨ����ˣ�3Ϊ����ӵ���Ϣ
        /// <summary>
        /// ����IsRatify�ֶΣ�0Ϊδ��ˣ�1Ϊͨ����ˣ�2Ϊûͨ����ˣ�3Ϊ����ӵ���Ϣ
        /// </summary>
        /// <param name="advIssue">��ҪIsRatify��NeedNum��Adv_Id�ֶ�</param>
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

        #region UpdateIsRatifyByIsratify ���ݹ��Id�޸�IsRatify�ֶ�
        /// <summary>
        /// ���ݹ��Id�޸�IsRatify�ֶ�
        /// </summary>
        /// <param name="IsRatify">���״̬�ֶΣ�0Ϊδ��ˣ�1Ϊͨ����ˣ�2Ϊûͨ����ˣ�3Ϊ����ӵ���Ϣ</param>
        /// <param name="Adv_Id">���Id</param>
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
