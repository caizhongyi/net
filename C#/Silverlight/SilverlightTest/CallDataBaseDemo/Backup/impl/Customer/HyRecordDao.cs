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
        #region �����Ա

        public bool InsertHyInfo(HyRecord hr)
        {
            try{
                string sqltext = "insert into T_HyRecord values('" + hr.Userid + "','"+hr.Companyname+"',"+hr.Cytypeid+",'','"+hr.Cyusername+"','','"+hr.Cyphoto+"','"+hr.Cyfax+"','"+hr.Cymoto+"','"+hr.Cyemail+"','"+hr.Cyurl+"','"+hr.Cyaddress+"',0,'��ҵ��Ա')";
                util.GetExecuteNonQuery(sqltext);
               return true;
            }
            catch{
                    return false;}
        }

        #endregion

        #region �ж��Ƿ�Ϊ��Ա


        public string IsHy(string userid)
        {
            string sqltext = "select Auditing from T_HyRecord where UserId='"+userid+"'";
            return util.GetStrExecuteScalar(sqltext);
        }

        #endregion

        #region IHyRecord ��Ա


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
        /// ��ѯ�Ƿ�Ϊ��ҵ��Ա���û���Ϣ
        /// </summary>
        /// <param name="Auditing">0Ϊ���ǻ�Ա��1Ϊ��Ա</param>
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
         ///  ͨ�����
         /// </summary>
         /// <param name="UserId">�û�ID</param>
         /// <param name="isMember">0Ϊ��ͨ�����,1Ϊͨ�����(��Ϊ��Ա)</param>
         /// <returns></returns>
        public int UpDateHyRecordInfoAuditingByUserId(string UserId, int isMember)
        {
            string cmdText = "update T_HyRecord set Auditing='"+isMember+"' where UserId='" + UserId + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
    }
}
