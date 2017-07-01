using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.impl.Group
{
    class  Group:Dao.Group .IGroupInfo 
    {
        Util u = new Util();

        public DataSet GetGroupInfo()
        {
            string cmd = "Select * from T_GroupInfo  ";
            return u.GetDataSet(cmd);

        }

        public DataSet GetGroupInfoByUserID(string UserID)
        {
            string cmd = "Select * from T_GroupInfo Where UserID='" + UserID + "' ";
            return u.GetDataSet(cmd);

        }

        public string GetGroupIDByGroupName(string GroupName)
        {
            string cmd = "Select GroupID from T_GroupInfo Where GroupName='" + GroupName + "'";
            return u.GetDataSet(cmd).Tables[0].Rows[0].ItemArray[0].ToString();
        }


        public bool InsertGroupInfo(string UserID, string GroupName, string GroupNotice, string GroupRemark)
        {
            string cmd = "insert into T_GroupInfo(UserID,GroupName,GroupNotice,GroupRemark) values('" + UserID + "','" + GroupName + "','" + GroupNotice + "','" + GroupRemark + "')";
            int i = u.GetExecuteNonQuery(cmd);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }
        //
        public DataSet GetGroupInfo_no_CodeIDByID(string GroupID)
        {
            string cmd = "Select T_CustomerInfo.no_CodeID from T_CustomerInfo,T_Customer_Group Where GroupID='" + GroupID + "' and T_CustomerInfo.CID=T_Customer_Group.CustomerID";
            return u.GetDataSet(cmd);
        }

        public DataSet GetGroupInfoByID(string GroupID)
        {
            string cmd = "Select * from T_GroupInfo Where GroupID='" + GroupID + "'";
            return u.GetDataSet(cmd);
        }

        public bool DelGroupInfo(string GroupID)
        {
            string cmd = "Delete T_GroupInfo Where GroupID='" + GroupID + "'";
            int i = u.GetExecuteNonQuery(cmd);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }

        public bool UpdateGroupInfo(string GroupID, string GroupName, string GroupNotice, string GroupRemark)
        {
            string cmd = "Update T_GroupInfo set GroupName='" + GroupName + "',GroupNotice='" + GroupNotice + "',GroupRemark='" + GroupRemark + "' Where GroupID='" + GroupID + "'";
            int i = u.GetExecuteNonQuery(cmd);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }




        #region 获取自己所拥有的群


        public DataSet SelMyGroup(string userid)
        {
            string sqltext = "select T_Customer_Group.GroupID,GroupName,GroupNotice from T_GroupInfo,T_Customer_Group where T_Customer_Group.GroupID=T_GroupInfo.GroupID and CustomerID='"+userid+"'";
            return u.GetDataSet(sqltext);
        }
        #endregion


        #region 保存赠送记录


        public bool GiveIntegarTo(string userid, string Gid, float GNum, int GState)
        {
            string sqltext = "insert into T_GiveIntegral values('"+userid+"','"+Gid+"',"+GNum+",'"+DateTime.Now.ToShortDateString().ToString()+"',"+GState+",'赠送积分')";
            if (u.GetExecuteNonQuery(sqltext) > 0) {
                return true;
            }
            else
            {return false;}
        }

        #endregion

        #region 取消持续赠送信息


        public bool CancelGive(string id)
        {
            string sqltext = "update T_GiveIntegral set GState=0 where ID='" + id + "'";
            if (u.GetExecuteNonQuery(sqltext) > 0)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region 修改持续赠送积分
        public bool UpdateGive(string id, float GNum)
        {
            string sqltext = "update T_GiveIntegral set GIntegralNum="+GNum+" where ID='"+id+"'";
            if (u.GetExecuteNonQuery(sqltext) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 判断是否有持续赠送记录


        public string SelGiveCount(string useid)
        {
            string sqltext = "select count(ID) from T_GiveIntegral where GState='1' and UId='"+useid+"'";
            return u.GetStrExecuteScalar(sqltext);
        }

        #endregion

        #region 查找定制持续赠送的群信息


        public DataSet SelGiveInfo(string userid)
        {
            string sqltext = "select ID,GroupName,GInteGralNum from T_GiveIntegral,T_GroupInfo where GState='1' and Uid='"+userid+"' and GroupID=GUid";
            return u.GetDataSet(sqltext);
        }

        #endregion

        #region 查寻全部赠送信息


        public DataSet SelGiveAllInfo(string userid)
        {
            string sqltext = "select ID,GroupName,GInteGralNum,GTime,GState from T_GiveIntegral,T_GroupInfo where Uid='"+userid+"' and GUid=GroupID order by ID desc";
            return u.GetDataSet(sqltext);
        }

        #endregion

        #region IGroupInfo 成员


        public void DelAllGiveInfo(string userid)
        {
            string sqltext = "delete from T_GiveIntegral where Uid='"+userid+"'";
            u.GetExecuteNonQuery(sqltext);
        }

        #endregion
    }
}
