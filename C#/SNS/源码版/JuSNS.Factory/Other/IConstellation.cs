using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.Other
{
    public interface IConstellation
    {
        List<ConstellationInfo> GetList();
        ConstellationInfo GetInfo(object cid);
    }

    public sealed partial class DataAccess
    {
        public static IConstellation CreateConstellation()
        {
            string className = path + ".Other.Constellation";
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
            return (IConstellation)objType;
        }
    }
}
