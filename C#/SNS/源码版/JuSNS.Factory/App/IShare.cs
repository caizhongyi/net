using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IShare
    {
        int InsertShare(ShareInfo info);
        int DeleteShare(int sid, int uid);
        ShareInfo GetInfo(object sid);
    }

    public sealed partial class DataAccess
    {
        public static IShare CreateShare()
        {
            string className = path + ".App.Share";
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
            return (IShare)objType;
        }
    }

}
