using JuSNS.Factory.User;
using JuSNS.Model;

namespace JuSNS.Home.User
{
    public class Box
    {
        static readonly private Box _instance = new Box();
        JuSNS.Factory.User.IBox dal;
        private Box()
        {
            dal = DataAccess.CreateBox();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Box Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 添加私信
        /// </summary>
        /// <param name="Info">私信实体类</param>
        /// <returns>添加成功返回1</returns>
        public int Add(MailInfo Info)
        {
            return dal.Add(Info);
        }

        /// <summary>
        /// 删除私信
        /// </summary>
        /// <param name="dType">类型,0为发件箱,1为收件箱</param>
        /// <param name="BoxID">私信编号</param>
        /// <returns>删除成功返回1</returns>
        public int Del(int dType, int BoxID, int UserID)
        {
            return dal.Del(dType, BoxID, UserID);
        }

        /// <summary>
        /// 回复私信
        /// </summary>
        /// <param name="Info">私信实体类</param>
        /// <returns>回复成功返回1</returns>
        public int Reply(MailInfo info)
        {
            return dal.Reply(info);
        }

         /// <summary>
        /// 取得最新回复内容
        /// </summary>
        /// <param name="boxID">私信编号</param>
        /// <param name="isMy">是否自已回复的最新内容</param>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回最新回复内容</returns>
        public string GetNewReContent(int boxID, bool isMy, int UserID)
        {
            return dal.GetNewReContent(boxID, isMy, UserID);
        }

        /// <summary>
        /// 取得最新的回复是否被阅读
        /// </summary>
        /// <param name="boxID">私信编号</param>
        /// <param name="UserID">用户编号</param>
        /// <returns>0为未读，1为已读</returns>
        public int GetNewReCNT(int boxID, int UserID)
        {
            return dal.GetNewReCNT(boxID, UserID);
        }

        /// <summary>
        /// 设置收到的信息为已读状态
        /// </summary>
        /// <param name="boxID">私信编号</param>
        /// <param name="UserID">用户编号</param>
        /// <returns>操作成功返回1</returns>
        public int Read(int boxID, int UserID)
        {
            return dal.Read(boxID, UserID);
        }

        /// <summary>
        /// 更新通知状态
        /// </summary>
        /// <param name="UserID">会员编号</param>
        public void UpdateNoticeMode(int userid)
        {
            dal.UpdateNoticeMode(userid);
        }

         /// <summary>
        /// 私信信息
        /// </summary>
        /// <param name="ID">私信编号</param>
        /// <returns>返回私信实体类</returns>
        public MailInfo Info(int ID)
        {
            return dal.Info(ID);
        }


        /// <summary>
        /// 得到未读邮件
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int GetBoxUnRead(int UserID)
        {
            return dal.GetBoxUnRead(UserID);
        }

    }
}