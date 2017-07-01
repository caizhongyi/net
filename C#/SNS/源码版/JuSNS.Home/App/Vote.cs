using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;


namespace JuSNS.Home.App
{
    public class Vote
    {
        static readonly private Vote _instance = new Vote();
        JuSNS.Factory.App.IVote dal;
        private Vote()
        {
            dal = DataAccess.CreateVote();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Vote Instance
        {
            get { return _instance; }
        }

        public List<VoteOptionInfo> OptionList(int vid)
        {
            return dal.OptionList(vid);
        }

        /// <summary>
        /// 取得用户投票选项
        /// </summary>
        /// <param name="vid">投票编号</param>
        /// <param name="userid">用户编号</param>
        /// <param name="tf">是否有投票</param>
        /// <returns>返回列表</returns>
        public List<VoteOptionInfo> OptionList(int vid, int userid, out bool tf)
        {
            return dal.OptionList(vid, userid,out tf);
        }

        /// <summary>
        /// 得到投票信息
        /// </summary>
        /// <param name="vid">投票ID</param>
        /// <returns>VoteInfo实体类</returns>
        public VoteInfo GetVoteInfo(object vid)
        {
            return dal.GetVoteInfo(vid);
        }

        /// <summary>
        /// 得到投票信息信息
        /// </summary>
        /// <param name="vid">投票ID</param>
        /// <returns>VoteInfo实体类</returns>
        public VoteToInfo GetVoteToInfo(object vid,object uid)
        {
            return dal.GetVoteToInfo(vid, uid);
        }

        /// <summary>
        /// 取得用户是否已投票
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="vid">投票编号</param>
        /// <returns>如果已投票返回true,否则false</returns>
        public bool IsVote(int userid, int vid)
        {
            return dal.IsVote(userid, vid);
        }

        /// <summary>
        /// 用户投票
        /// </summary>
        /// <param name="info">用户投票实体类</param>
        /// <returns>投票成功返回1</returns>
        public int Add(VoteToInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 删除投票
        /// </summary>
        /// <param name="vid">投票的ID</param>
        /// <param name="uid">操作用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteVote(int vid, int uid)
        {
            return dal.DeleteVote(vid, uid);
        }
        /// <summary>
        /// 删除投票结果
        /// </summary>
        /// <param name="vid">投票的ID</param>
        /// <param name="uid">操作用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteVoteTo(int vid, int uid)
        {
            return dal.DeleteVoteTo(vid, uid);
        }

        /// <summary>
        /// 添加投票
        /// </summary>
        /// <param name="info">投票实体类</param>
        /// <returns>添加成功返回投票编号</returns>
        public int AddVote(VoteInfo info)
        {
            return dal.AddVote(info);
        }

        /// <summary>
        /// 添加投票项
        /// </summary>
        /// <param name="info">投票实体类</param>
        /// <returns>添加成功返回投票编号</returns>
        public int AddOption(VoteOptionInfo info)
        {
            return dal.AddOption(info);
        }

    }
}