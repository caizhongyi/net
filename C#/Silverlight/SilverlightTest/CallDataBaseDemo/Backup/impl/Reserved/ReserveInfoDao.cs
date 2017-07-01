using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Reserved;

namespace DAL.impl.Reserved
{
    class ReserveInfoDao : IReserveInfo
    {  
        Util util = new Util();
        #region ��ȡ�����

        public System.Data.DataSet SelBigTypeInfo()
        {
            string sqltext = "select TBid,TBName from T_BigTypeInfo";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ��ȡС����


        public System.Data.DataSet GetSmallTypeInfo(string BigId)
        {
            string sqltext = "select TSid,TsName from T_SmalllTypeInfo where TBid='" + BigId + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ��ȡ������Ϣ


        public System.Data.DataSet GetAllInfo(string uid)
        {
            string sqltext = "select CTSId,TBName,TSName from T_BigTypeInfo,T_SmalllTypeInfo,CustomInfo where T_BigTypeInfo.TBid=T_SmalllTypeInfo.TBid and TSid=CustomInfo.CTSId and CCUId='"+uid+"'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ���涨����Ϣ


        public void InsertInfo(string uid, int typeid)
        {
            string sqltext="insert into CustomInfo(CCUId,CTSId,CRemark) values('"+uid+"',"+typeid+",'������Ϣ') ";
            try
            {
                util.GetExecuteNonQuery(sqltext);
            }
            catch{}
        }

        #endregion

        #region ȡ�����ж�����Ϣ

        public void DelInfo(string uid)
        {
            string sqldel = "delete from CustomInfo where CCUId='" + uid + "'";
            util.GetExecuteNonQuery(sqldel);
        }

        #endregion

        #region ȡ������������Ϣ


        public void DelOneInfo(string uid, string ctsid)
        {
            string sqltext = "delete from CustomInfo where CCUId='"+uid+"' and CTSId='"+ctsid+"'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion
    }
}
