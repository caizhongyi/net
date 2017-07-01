using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.Group
{
    public interface IGroupInfo
    {
        DataSet GetGroupInfoByUserID(string UserID);
        string GetGroupIDByGroupName(string GroupName);
        bool InsertGroupInfo(string UserID, string GroupName, string GroupNotice, string GroupRemark);
        DataSet GetGroupInfoByID(string GroupID);
        DataSet GetGroupInfo();
        bool DelGroupInfo(string GroupID);
        DataSet GetGroupInfo_no_CodeIDByID(string GroupID);
        bool UpdateGroupInfo(string GroupID, string GroupName, string GroupNotice, string GroupRemark);

        DataSet SelMyGroup(string userid);

        bool GiveIntegarTo(string userid, string Gid, float GNum, int GState);
        bool CancelGive(string id);
        bool UpdateGive(string id,float GNum);
        string SelGiveCount(string useid);
        DataSet SelGiveInfo(string userid);
        DataSet SelGiveAllInfo(string userid);
        void DelAllGiveInfo(string userid);
    }
}
