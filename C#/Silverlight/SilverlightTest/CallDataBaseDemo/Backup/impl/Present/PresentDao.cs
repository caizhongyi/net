using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Present;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL.impl.Present
{
    class PresentDao : IPresent
    {
        Util util = new Util();
        #region 礼品列表

        public System.Data.DataSet GetPresentList()
        {
            string sqltext = "select PId,PName,PPicture,PNum,SNum from T_PresentInfo order by PId desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 礼品数量更改


        public void UpdataPresentCount(string Pid, int num)
        {
            string sqltext = "update T_PresentInfo set PNum=PNum-'" + num + "'  where Pid='" + Pid + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region 进行积分兑换


        public int InsertChangePresent(T_CustomerChangePresentInfo ccpi)
        {
            string sqltext = "insert into T_CustomerChangePresentInfo values('" + ccpi.CCPCId + "','" + ccpi.CCPPId + "','" + ccpi.Ccpnum + "','" + ccpi.Ccpname + "','" + ccpi.Ccpaddress + "','" + ccpi.CCPInfo + "','" + DateTime.Now + "','等待审核','')";
            return util.GetExecuteNonQuery(sqltext);
        }

        //修改积分
        public void updatePresentInttral(string Num, string CusId)
        {
            string sqltext = "update T_PersonalScoreInfo set CCurrNum='" + Num + "' where CCId='" + CusId + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        //修改礼品数量

        #endregion



        #region 取几个图片


        public DataSet GetDefaultPresentList()
        {
            string sqltext = "select top 4 PId,PName,PPicture,PNum,SNum from T_PresentInfo order by PId desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 取得礼品类型


        public DataSet GetTypeList()
        {
            string sqltext = "select TId,TName from T_PresentTypeInfo";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 发布礼品


        public bool InsertPicture(T_PresentInfo tpi)
        {
            try
            {
                string sqltext = "insert into T_PresentInfo values('" + tpi.Pname + "','" + tpi.PTId + "','" + tpi.PNum + "','" + tpi.PPicture + "','" + tpi.Pusername + "','" + tpi.Snum + "','" + tpi.Pmon + "','礼品')";
                util.GetExecuteNonQuery(sqltext);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region 个人礼品信息


        public DataSet MyPresent(string userid)
        {
            string sqltext = "select PId,PName,TName,PNum,PPicture,SNum from T_PresentTypeInfo,T_PresentInfo where T_PresentInfo.PTId=T_PresentTypeInfo.TId and PUserName='" + userid + "' order by PId desc";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 删除我的礼品


        public void delMyPresent(string userid, string pid)
        {
            string sqltext = "delete from T_PresentInfo where PUserName='" + userid + "' and PId='" + pid + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region 获得个人礼品兑换


        public DataSet SelExchangePresent(string userid)
        {
            string sqltext = "select CCPId,PName,PPicture,CCPName,CCPNum,CCPAddress,CCPInfo,CCPTime,CCPLook from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and PUserName='" + userid + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 获得审核通过的个人礼品信息


        public DataSet SelExchange(string userid, string info)
        {
            string sqltext = "select CCPId,PName,PPicture,CCPName,CCPNum,CCPAddress,CCPInfo,CCPTime,CCPLook from T_CustomerChangePresentInfo,T_PresentInfo where T_PresentInfo.PId=T_CustomerChangePresentInfo.CCPPId and PUserName='" + userid + "' and CCPLook='" + info + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion


        #region 审核通过


        public void UpdateExchangePresent(string ccpid, string info)
        {
            string sqltext = "update T_CustomerChangePresentInfo set CCPLook='" + info + "'  where CCPId='" + ccpid + "'";
            util.GetExecuteNonQuery(sqltext);
        }

        #endregion

        #region 选中礼品行


        public DataSet SelExchangeUp(string ccpid)
        {
            string sqltext = "select * from T_CustomerChangePresentInfo where CCPId='" + ccpid + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 获取图片信息


        public DataSet SelPicture(string pid)
        {
            string sqltext = "select PId,PName,PPicture from T_PresentInfo where PId='" + pid + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 获取图片的详细信息


        public DataSet SelAllPictureInfo(string pid)
        {
            string sqltext = "select PId,PName,PNum,PPicture,SNum,PMon from T_PresentInfo where PId='"+pid+"'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 查出精彩活动信息


        public DataSet dtSelActivityInfo()
        {
            string sqltext = "Select top 4 AId,Replace(convert(char,[ATime],111),'/','-')+'       '+ATitle as ContentAndTime from ActivityInfo order by [AId] desc";
            return util.GetDataSet(sqltext);
        }

        public DataSet dtSelSelSelActivityInfoByAID(int AId)
        {
            string cmdText = "select ATitle,AContant,ATime,CName from T_CustomerInfo,ActivityInfo where T_CustomerInfo.CId=ActivityInfo.AUId and AId='"+AId+"'";
            return util.GetDataSet(cmdText);
        }

        #endregion
    }
}
