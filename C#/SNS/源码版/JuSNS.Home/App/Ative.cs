using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;


namespace JuSNS.Home.App
{
    public class Ative
    {
        static readonly private Ative _instance = new Ative();
        JuSNS.Factory.App.IAtive dal;
        private Ative()
        {
            dal = DataAccess.CreateAtive();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Ative Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 得到活动分类
        /// </summary>
        /// <param name="parentid">父ID</param>
        /// <returns>List实体类</returns>
        public List<AtiveClassInfo> GetAtiveClassList(int parentid)
        {
            return dal.GetAtiveClassList(parentid);
        }

        /// <summary>
        /// 插入或更新活动
        /// </summary>
        /// <param name="info">活动实体类</param>
        /// <returns>返回相关ID</returns>
        public int InsertUpdate(AtiveInfo info)
        {
            return dal.InsertUpdate(info);
        }

        /// <summary>
        /// 得到指定活动的信息
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <returns>活动详情实体类</returns>
        public AtiveInfo GetAtiveInfo(object aid)
        {
            return dal.GetAtiveInfo(aid);
        }

        /// <summary>
        /// 得到指定活动分类的信息
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <returns>活动详情实体类</returns>
        public AtiveClassInfo GetAtiveClassInfo(object aid)
        {
            return dal.GetAtiveClassInfo(aid);
        }

        public int InsetAtiveClass(AtiveClassInfo info)
        {
            return dal.InsetAtiveClass(info);
        }

        /// <summary>
        /// 得到好友最新参加的活动
        /// </summary>
        /// <param name="number">显示数量</param>
        /// <param name="q">好友查询字符串</param>
        /// <param name="userid">当前用户ID</param>
        /// <returns>活动实体类集合</returns>
        public List<AtiveInfo> GetFriendAtive(int number, string q, int userid)
        {
            return dal.GetFriendAtive(number, q, userid);
        }

        /// <summary>
        /// 更新活动点击率
        /// </summary>
        /// <param name="aid">活动ID</param>
        public void UpdateClick(int aid)
        {
            dal.UpdateClick(aid);
        }

        /// <summary>
        /// 得到报名活动人数或关注人数
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="flag">1表示关注活动，2表示报名活动</param>
        /// <returns></returns>
        public int GetMembers(int aid, int flag)
        {
            return dal.GetMembers(aid, flag);
        }

        /// <summary>
        /// 参与或者关注活动
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="uid">参与者ID</param>
        /// <param name="flag">1关注活动，2参与活动</param>
        /// <returns>0失败，1参与了但是需要审核，2成功，3已经参与了</returns>
        public int JoinAtive(int aid,int uid, int flag)
        {
            return dal.JoinAtive(aid,uid, flag);
        }

        /// <summary>
        /// 得到是否参加或者关注过活动
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns>-1没有记录，0参与了未审核，1已经关注过了，2已经参与了</returns>
        public int GetAtiveATT(int aid, int uid)
        {
            return dal.GetAtiveATT(aid, uid);
        }

        /// <summary>
        /// 退出活动
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int OutAtive(int aid, int uid)
        {
            return dal.OutAtive(aid, uid);
        }

        /// <summary>
        /// 得到需要审核的参加活动的会员。
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <returns>需要审核的数量</returns>
        public int GetCheckMembers(int aid)
        {
            return dal.GetCheckMembers(aid);
        }

        /// <summary>
        /// 得到需要审核的会员
        /// </summary>
        /// <param name="number">要调用的列表数量</param>
        /// <param name="aid">活动ID</param>
        /// <param name="flag">得到要调用的会员列表类型,0表示要审核的，1表示关注的，2表示参与的</param>
        /// <returns>活动需要审核的会员列表LIST实体类</returns>
        public List<AtiveMemberInfo> GetCheckMemberList(int number, int aid, int flag)
        {
            return dal.GetCheckMemberList(number, aid, flag);
        }

        /// <summary>
        /// 得到活动相册
        /// </summary>
        /// <param name="number"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public List<PhotoInfo> GetAtiveAlbumList(int number, int aid)
        {
            return dal.GetAtiveAlbumList(number, aid);
        }

        /// <summary>
        /// 审核会员
        /// </summary>
        /// <param name="mid">操作的数据库记录ID</param>
        /// <param name="aid">活动编号</param>
        /// <param name="flag">2审核，0取消审核</param>
        /// <returns>0失败，1成功，2人数达到上限</returns>
        public int CheckAtiveMember(int mid,int aid, int flag)
        {
            return dal.CheckAtiveMember(mid,aid, flag);
        }

        /// <summary>
        /// 得到某个活动中的图片数量
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <returns>图片数量</returns>
        public int AtiveImgCount(int aid)
        {
            return dal.AtiveImgCount(aid);
        }


        /// <summary>
        /// 相片中的第几张
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public int TheNumber(int aid, int photoID)
        {
            return dal.TheNumber(aid, photoID);
        }

        /// <summary>
        /// 活动相册中的上一张
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="photoID">相片ID</param>
        /// <returns>第几张</returns>
        public int PrePhotoID(int aid, int photoID)
        {
            return dal.PrePhotoID(aid, photoID);
        }

        /// <summary>
        /// 活动相册中的下一张
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="photoID">相片ID</param>
        /// <returns>第几张</returns>
        public int NextPhotoID(int aid, int photoID)
        {
            return dal.NextPhotoID(aid, photoID);
        }

        /// <summary>
        /// 插入活动评论
        /// </summary>
        /// <param name="info">活动评论实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertAtiveComment(AtiveCommentInfo info)
        {
            return dal.InsertAtiveComment(info);
        }

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="aid">删除用户</param>
        /// <param name="uid">操作用户</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteAtive(int aid, int uid)
        {
            return dal.DeleteAtive(aid, uid);
        }

        /// <summary>
        /// 删除活动分类
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteAtiveClass(int aid, int uid)
        {
            return dal.DeleteAtiveClass(aid, uid);
        }

        /// <summary>
        /// 得到活动评论具体信息
        /// </summary>
        /// <param name="cid">活动评论的ID</param>
        /// <returns></returns>
        public AtiveCommentInfo GetAtiveCommentInfo(int cid)
        {
            return dal.GetAtiveCommentInfo(cid);
        }
        /// <summary>
        /// 删除留言评论
        /// </summary>
        /// <param name="aid">删除用户</param>
        /// <param name="uid">操作用户</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteAtiveComment(int aid, int uid)
        {
            return dal.DeleteAtiveComment(aid, uid);
        }


        /// <summary>
        /// 得到照片信息(PhotoID)
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <returns></returns>
        public PhotoInfo GetInfo(object PhotoID)
        {
            return dal.GetInfo(PhotoID);
        }
    }
}
