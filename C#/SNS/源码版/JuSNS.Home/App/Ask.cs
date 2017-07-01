using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Ask
    {
        static readonly private Ask _instance = new Ask();
        JuSNS.Factory.App.IAsk dal;
        private Ask()
        {
            dal = DataAccess.CreateAsk();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Ask Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 得到问答的分类列表
        /// </summary>
        /// <param name="ParentID">父ID</param>
        /// <returns>List列表实体</returns>
        public List<AskClassInfo> GetAskClass(int ParentID)
        {
            return dal.GetAskClass(ParentID);
        }

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteAsk(int aid, int userid)
        {
            return dal.DeleteAsk(aid, userid);
        }

        /// <summary>
        /// 得到指定ID的问答分类
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public AskClassInfo GetAskClassInfo(object aid)
        {
            return dal.GetAskClassInfo(aid);
        }

        /// <summary>
        /// 插入问题分类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertAskClass(AskClassInfo info)
        {
            return dal.InsertAskClass(info);
        }

        /// <summary>
        /// 删除问题分类
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteAskClass(int aid, int userid)
        {
            return dal.DeleteAskClass(aid, userid);
        }

        /// <summary>
        /// 插入新问题或更新新问题
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0失败，1积分不足，2成功</returns>
        public int InsertAsk(AskInfo info,out int aid)
        {
            return dal.InsertAsk(info, out aid);
        }

        /// <summary>
        /// 得到指定问题的信息
        /// </summary>
        /// <param name="aid">问题ID</param>
        /// <returns>AskInfo实体类</returns>
        public AskInfo GetAskInfo(object aid)
        {
            return dal.GetAskInfo(aid);
        }

        /// <summary>
        /// 更新问题状态
        /// </summary>
        /// <param name="aid">问题ID</param>
        /// <param name="flag">0增加点击率，1更新问题为关闭状态，2设置问题为最佳答案</param>
        /// <param name="userid">操作者用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateAskState(object aid, int flag,int userid)
        {
            return dal.UpdateAskState(aid, flag, userid);
        }

        /// <summary>
        /// 得到问题列表
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="qstring">查询字符串</param>
        /// <param name="flag">0高分悬赏，1最新问题，2相关问题（联合qstring查询,未用）</param>
        /// <returns></returns>
        public List<AskInfo> GetAskList(int number,string qstring, int flag)
        {
            return dal.GetAskList(number, qstring, flag);
        }

        /// <summary>
        /// 设置为最佳答案
        /// </summary>
        /// <param name="uid">操作ID</param>
        /// <param name="infoid">操作的答案</param>
        /// <param name="mid">答案的问题ID</param>
        /// <param name="userid">被操作用户</param>
        /// <returns>0成功，1已经有最佳答案了，2问题已经关闭，3不是自己的问题，4设置失败</returns>
        public int SetAskBest(int uid, int infoid, int mid,int userid)
        {
            return dal.SetAskBest(uid, infoid, mid,userid);
        }
    }
}
