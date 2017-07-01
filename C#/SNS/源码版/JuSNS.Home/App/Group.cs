using System.Data;
using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Group
    {
        static readonly private Group _instance = new Group();
        JuSNS.Factory.App.IGroup dal;
        private Group()
        {
            dal = DataAccess.CreateGroup();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Group Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 根据classid获取群组分类名称
        /// </summary>
        /// <param name="classid">分类ID</param>
        /// <returns>分类名称</returns>
        public string GetClassName(int classid)
        {
            object result =  dal.GetClassName(classid);
            if (string.IsNullOrEmpty(result.ToString()))
                return "无分类";
            else 
                return result.ToString();
        }

        /// <summary>
        /// 获得群组分类
        /// </summary>
        /// <param name="parentid">父ID</param>
        /// <returns>List类</returns>
        public List<GroupClassInfo> GetClassList(int parentid)
        {
            return dal.GetClassList(parentid);
        }

        /// <summary>
        /// 是否加入了指定群里面
        /// </summary>
        /// <param name="groupid">群ID</param>
        /// <param name="userid">用户</param>
        /// <returns>返回true或false</returns>
        public bool IsJoinGroup(int groupid, int userid)
        {
            return dal.IsJoinGroup(groupid, userid);
        }

        /// <summary>
        /// 得到加入的群ID字符串
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>得到以"," 分开的字符串</returns>
        public string GetJoinGroup(int userid)
        {
            return dal.GetJoinGroup(userid);
        }

        /// <summary>
        /// 删除群
        /// </summary>
        /// <param name="groupid">群ID</param>
        /// <param name="userid">当前删除群的用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteGroup(int groupid, int userid)
        {
            return dal.DeleteGroup(groupid, userid);
        }

        /// <summary>
        /// 删除群主题
        /// </summary>
        /// <param name="tid">话题ID</param>
        /// <param name="userid">当前用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteGroupTopic(int tid, int userid)
        {
            return dal.DeleteGroupTopic(tid, userid);
        }

        /// <summary>
        /// 删除社群分类
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteGroupClass(int cid, int userid)
        {
            return dal.DeleteGroupClass(cid, userid);
        }
        /// <summary>
        /// 删除群附件
        /// </summary>
        /// <param name="tid">ID</param>
        /// <param name="userid">当前用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteGroupFile(int tid, int userid)
        {
            return dal.DeleteGroupFile(tid, userid);
        }

        /// <summary>
        /// 得到群组分类信息
        /// </summary>
        /// <param name="cid">得到群组分类</param>
        public GroupClassInfo GetGroupClassInfo(int cid)
        {
            return dal.GetGroupClassInfo(cid);
        }

        /// <summary>
        /// 插入社群分类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGroupClass(GroupClassInfo info)
        {
            return dal.InsertGroupClass(info);
        }


        /// <summary>
        /// 设置话题置顶
        /// </summary>
        /// <param name="tid">话题ID</param>
        /// <param name="userid">当前用户ID</param>
        /// <param name="flag">0取消置顶，1置顶</param>
        /// <returns>0失败1成功</returns>
        public int SetGroupTop(int tid, int userid, int flag)
        {
            return dal.SetGroupTop(tid, userid, flag);
        }
        /// <summary>
        /// 设置话题精华
        /// </summary>
        /// <param name="tid">话题ID</param>
        /// <param name="userid">当前用户ID</param>
        /// <param name="flag">0取消精华，1设置为精华</param>
        /// <returns>0失败1成功</returns>
        public int SetGroupBest(int tid, int userid, int flag)
        {
            return dal.SetGroupBest(tid, userid, flag);
        }

        /// <summary>
        /// 加入群
        /// </summary>
        /// <param name="groupid">群ID</param>
        /// <param name="userid">申请加入的人</param>
        /// <returns>0成功，1需要审核，2拒绝加入，3加入失败</returns>
        public int JoinGroup(int groupid, int userid)
        {
            return dal.JoinGroup(groupid, userid);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="groupid">群ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>1成功，0失败，2是创始人不能退出</returns>
        public int OutGroup(int groupid, int userid)
        {
            return dal.OutGroup(groupid, userid);
        }

        /// <summary>
        /// 插入或更新群信息
        /// </summary>
        /// <param name="info">群组实体类</param>
        /// <param name="gid">返回群组ID</param>
        /// <returns>大于0，成功</returns>
        public int InsertandUpdate(GroupInfo info,out int gid)
        {
            return dal.InsertandUpdate(info, out gid);
        }

        /// <summary>
        /// 得到指定群组信息
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>得到群实体类</returns>
        public GroupInfo GetGroupInfo(object gid)
        {
            return dal.GetGroupInfo(gid);
        }

        /// <summary>
        /// 得到指定帖子信息
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <returns>得到群实体类</returns>
        public GroupTopicInfo GetTopicInfo(object tid)
        {
            return dal.GetTopicInfo(tid);
        }

        /// <summary>
        /// 得到指定群组的话题数量
        /// </summary>
        /// <param name="gid">群组ID</param>
        /// <param name="flag">0所有话题，1只有群组主题，2只有群组回复</param>
        /// <returns>群组主题数</returns>
        public int GetGroupTopicCount(int gid,int flag)
        {
            return dal.GetGroupTopicCount(gid,flag);
        }

        /// <summary>
        /// 得到指定群内的帖子列表
        /// </summary>
        /// <param name="number">调用数</param>
        /// <param name="gid">群ID</param>
        /// <returns>返回List列表实体</returns>
        public List<GroupTopicInfo> GetGroupTopicList(int number, int gid)
        {
            return dal.GetGroupTopicList(number, gid);
        }

        /// <summary>
        /// 得到某个群的成员
        /// </summary>
        /// <param name="number">调用数</param>
        /// <param name="gid">群ID</param>
        /// <param name="flag">0以管理员排序,1以最新加入会员排序</param>
        /// <returns>返回List列表实体</returns>
        public List<GroupMemberInfo> GetGroupMemberList(int number, int gid,int flag)
        {
            return dal.GetGroupMemberList(number, gid, flag);
        }

        /// <summary>
        /// 得到群相册数量
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>群相册数量</returns>
        public int GetGroupAlbumCount(int gid)
        {
            return dal.GetGroupAlbumCount(gid);
        }
        /// <summary>
        /// 得到群附件数量
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>群附件数量</returns>
        public int GetGroupFilesCount(int gid)
        {
            return dal.GetGroupFilesCount(gid);
        }

        /// <summary>
        /// 得到群会员数量
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>群会员数量</returns>
        public int GetGroupMemberCount(int gid)
        {
            return dal.GetGroupMemberCount(gid);
        }

        /// <summary>
        /// 得到群活动数量
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>群活动数量</returns>
        public int GetGroupAtiveCount(int gid)
        {
            return dal.GetGroupAtiveCount(gid);
        }

        /// <summary>
        /// 是否是群管理员
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回true或false</returns>
        public bool isGroupAdmin(int gid, int userid)
        {
            return dal.isGroupAdmin(gid, userid);
        }

        /// <summary>
        /// 插入帖子回复
        /// </summary>
        /// <param name="info">帖子实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertTopic(GroupTopicInfo info)
        {
            return dal.InsertTopic(info);
        }

        /// <summary>
        /// 更新帖子内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateTopicContent(GroupTopicInfo info)
        {
            return dal.UpdateTopicContent(info);
        }

        /// <summary>
        /// 更新查看次数
        /// </summary>
        /// <param name="tid">帖子ID</param>
        public void UpdateTopicClicks(int tid)
        {
            dal.UpdateTopicClicks(tid);
        }

        /// <summary>
        /// 获得用户发布的最新的帖子ID
        /// </summary>
        /// <param name="gid">群组</param>
        /// <param name="userid">用户ID</param>
        /// <returns>帖子ID</returns>
        public int GetMaxTopicForUser(int gid, int userid)
        {
            return dal.GetMaxTopicForUser(gid, userid);
        }

        /// <summary>
        /// 邀请好友加入群
        /// </summary>
        /// <param name="userid">邀请者</param>
        /// <param name="reciveid">被邀请者</param>
        /// <param name="gid">邀请加入的群</param>
        /// <returns>0失败，1成功</returns>
        public int InviteFriend(int userid, int reciveid, int gid)
        {
            return dal.InviteFriend(userid, reciveid, gid);
        }

        /// <summary>
        /// 得到附件单个信息
        /// </summary>
        /// <param name="fid">附件ID</param>
        /// <returns>附件实体类</returns>
        public FilesInfo GetFileInfo(object fid)
        {
            return dal.GetFileInfo(fid);
        }

        /// <summary>
        /// 得到群空间总大小（已使用）
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>返回空间已使用大小，单位KB</returns>
        public int GetFilesSize(int gid)
        {
            return dal.GetFilesSize(gid);
        }

        /// <summary>
        /// 向数据库中插入附件
        /// </summary>
        /// <param name="info">附件实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertFiles(FilesInfo info)
        {
            return dal.InsertFiles(info);
        }

        /// <summary>
        /// 群组会员通过审核
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <param name="userid">被操作的用户ID</param>
        /// <param name="uid">操作的管理员用户ID</param>
        /// <param name="flag">0审核，1拒绝</param>
        /// <returns>0失败，1成功</returns>
        public int CheckGroupMember(int gid,int userid, int uid, int flag)
        {
            return dal.CheckGroupMember(gid, userid, uid, flag);
        }

        /// <summary>
        /// 得到加入某个群内的用户列表
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <returns>群会员字符串</returns>
        public string GetMemberList(int gid)
        {
            return dal.GetMemberList(gid);
        }

        /// <summary>
        /// 删除群会员
        /// </summary>
        /// <param name="infoid">ID</param>
        /// <param name="uid">操作者</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteGroupMember(int infoid, int uid)
        {
            return dal.DeleteGroupMember(infoid, uid);
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool isGroupSuperAdmin(int gid, int userid)
        {
            return dal.isGroupSuperAdmin(gid, userid);
        }

        /// <summary>
        /// 设置为群管理员
        /// </summary>
        /// <param name="gid">群ID</param>
        /// <param name="userid">被操作者</param>
        /// <param name="uid">操作者</param>
        /// <param name="flag">1设置为管理员，0取消管理员</param>
        /// <returns>0失败，1成功</returns>
        public int SetGroupAdmin(int gid, int userid, int uid, int flag)
        {
            return dal.SetGroupAdmin(gid, userid, uid, flag);
        }
    }
}
