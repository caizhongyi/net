using JuSNS.Model;
using System.Collections.Generic;
using System.Reflection;
using System.Data;


namespace JuSNS.Factory.App
{
    public interface IGroup
    {
        string GetClassName(int classid);
        List<GroupClassInfo> GetClassList(int parentid);
        bool IsJoinGroup(int groupid, int userid);
        string GetJoinGroup(int userid);
        int DeleteGroup(int groupid, int userid);
        int DeleteGroupTopic(int tid, int userid);
        int DeleteGroupClass(int cid, int userid);
        int DeleteGroupFile(int tid, int userid);
        GroupClassInfo GetGroupClassInfo(int cid);
        int InsertGroupClass(GroupClassInfo info);
        int SetGroupTop(int tid, int userid, int flag);
        int SetGroupBest(int tid, int userid, int flag);
        int JoinGroup(int groupid, int userid);
        int OutGroup(int groupid, int userid);
        int InsertandUpdate(GroupInfo info, out int gid);
        GroupInfo GetGroupInfo(object gid);
        GroupTopicInfo GetTopicInfo(object tid);
        int GetGroupTopicCount(int gid, int flag);
        List<GroupTopicInfo> GetGroupTopicList(int number, int gid);
        List<GroupMemberInfo> GetGroupMemberList(int number, int gid,int flag);
        int GetGroupAlbumCount(int gid);
        int GetGroupFilesCount(int gid);
        int GetGroupMemberCount(int gid);
        int GetGroupAtiveCount(int gid);
        bool isGroupAdmin(int gid, int userid);
        int InsertTopic(GroupTopicInfo info);
        int UpdateTopicContent(GroupTopicInfo info);
        void UpdateTopicClicks(int tid);
        int GetMaxTopicForUser(int gid, int userid);
        int InviteFriend(int userid, int reciveid, int gid);
        FilesInfo GetFileInfo(object fid);
        int GetFilesSize(int gid);
        int InsertFiles(FilesInfo info);
        int CheckGroupMember(int gid, int userid, int uid, int flag);
        string GetMemberList(int gid);
        int DeleteGroupMember(int infoid, int uid);
        int SetGroupAdmin(int gid, int userid, int uid, int flag);
        bool isGroupSuperAdmin(int gid, int userid);
    }

    public sealed partial class DataAccess
    {
        public static IGroup CreateGroup()
        {
            string className = path + ".App.Group";
            object objType = JuSNS.Common.DataCache.GetCache(className);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(className);
                    JuSNS.Common.DataCache.SetCache(className, objType);// 写入缓存
                }
                catch { }
            }
            return (IGroup)objType;
        }
    }
}
