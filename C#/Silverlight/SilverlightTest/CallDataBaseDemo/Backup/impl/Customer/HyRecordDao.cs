using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using Model;
using System.Data;

namespace DAL.impl.Customer
{
   
    class HyRecordDao:IHyRecord
    { 
        Util util = new Util();
        #region 加入会员

        public bool InsertHyInfo(HyRecord hr)
        {
            try{
                string sqltext = "insert into T_HyRecord values('" + hr.Userid + "','"+hr.Companyname+"',"+hr.Cytypeid+",'','"+hr.Cyusername+"','','"+hr.Cyphoto+"','"+hr.Cyfax+"','"+hr.Cymoto+"','"+hr.Cyemail+"','"+hr.Cyurl+"','"+hr.Cyaddress+"',0,'企业会员')";
                util.GetExecuteNonQuery(sqltext);
               return true;
            }
            catch{
                    return false;}
        }

        #endregion

        #region 判断是否为会员


        public string IsHy(string userid)
        {
            string sqltext = "select Auditing from T_HyRecord where UserId='"+userid+"'";
            return util.GetStrExecuteScalar(sqltext);
        }

        #endregion

        #region IHyRecord 成员


        public System.Data.DataSet CompanyXzList()
        {
            string sqltext = "select ID,CompanyXz from T_CompanXzType";
            return util.GetDataSet(sqltext);
        }

        #endregion

        public DataTable dtSelHyRecordInfoByUserId(string UserId) 
        {
            string cmdText = "select * from T_HyRecord where UserId='" + UserId + "'";
            return util.GetDataTable(cmdText);
        }
        /// <summary>
        /// 查询是否为企业会员的用户信息
        /// </summary>
        /// <param name="Auditing">0为不是会员，1为会员</param>
        /// <returns></returns>
        public DataTable dtSelHyRecordInfoAndT_CompanXzTypeByUserId(int Auditing)
        {
            string cmdText = "select * from T_HyRecord,T_CompanXzType where Auditing='" + Auditing + "' and T_CompanXzType.ID=T_HyRecord.CyTypeId ";
            return util.GetDataTable(cmdText);
        }

        public int UpdateHyRecordInfoByUserId(HyRecord hyRecordInfo) 
        {
            string cmdText = "update T_HyRecord set CompanyName='" + hyRecordInfo.Companyname + "',CyYyZz='" + hyRecordInfo.Cyyyzz + "',CyUserName='" + hyRecordInfo.Cyusername + "',CyId='" + hyRecordInfo.Ccyid + "',CyPhoto='" + hyRecordInfo.Cyphoto + "',CyFax='" + hyRecordInfo.Cyfax + "',CyMoto='" + hyRecordInfo.Cymoto + "',CyEmail='" + hyRecordInfo.Cyemail + "',CyUrl='" + hyRecordInfo.Cyurl + "',CyAddress='" + hyRecordInfo.Cyaddress + "' where UserId='" + hyRecordInfo.Userid + "'";
            return util.GetExecuteNonQuery(cmdText);
        }

         /// <summary>
         ///  通过审核
         /// </summary>
         /// <param name="UserId">用户ID</param>
         /// <param name="isMember">0为不通过审核,1为通过审核(即为会员)</param>
         /// <returns></returns>
        public int UpDateHyRecordInfoAuditingByUserId(string UserId, int isMember)
        {
            string cmdText = "update T_HyRecord set Auditing='"+isMember+"' where UserId='" + UserId + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
    }
}
