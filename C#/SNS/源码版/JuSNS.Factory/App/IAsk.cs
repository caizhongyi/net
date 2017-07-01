using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IAsk
    {
        List<AskClassInfo> GetAskClass(int ParentID);
        int DeleteAsk(int aid, int userid);
        int DeleteAskClass(int aid, int userid);
        AskClassInfo GetAskClassInfo(object aid);
        int InsertAskClass(AskClassInfo info);
        int InsertAsk(AskInfo info,out int aid);
        AskInfo GetAskInfo(object aid);
        int UpdateAskState(object aid, int flag,int userid);
        List<AskInfo> GetAskList(int number, string qstring, int flag);
        int SetAskBest(int uid, int infoid, int mid,int userid);
    }

    public sealed partial class DataAccess
    {
        public static IAsk CreateAsk()
        {
            string className = path + ".App.Ask";
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
            return (IAsk)objType;
        }
    }

}
