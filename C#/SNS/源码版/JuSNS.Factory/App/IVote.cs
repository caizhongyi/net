using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IVote
    {
        List<VoteOptionInfo> OptionList(int vid);
        List<VoteOptionInfo> OptionList(int vid, int userid, out bool tf);
        VoteInfo GetVoteInfo(object vid);
        VoteToInfo GetVoteToInfo(object vid, object uid);
        bool IsVote(int userid, int vid);
        int Add(VoteToInfo info);
        int DeleteVote(int vid, int uid);
        int DeleteVoteTo(int vid, int uid);
        int AddVote(VoteInfo info);
        int AddOption(VoteOptionInfo info);
    }

    public sealed partial class DataAccess
    {
        public static IVote CreateVote()
        {
            string className = path + ".App.Vote";
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
            return (IVote)objType;
        }
    }

}
