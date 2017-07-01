using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.Other
{
    public interface IArea
    {
        List<DictAreaInfo> GetArea();
        List<VocationInfo> GetVotionList();
        int InsertArea(DictAreaInfo info);
        int DeleteArea(int aid, int uid);
        List<DictAreaInfo> CityList(int ParertID);
        DictAreaInfo GetAreaInfo(int id);
        int GetAreaID(string AreaName);
    }

    public sealed partial class DataAccess
    {
        public static IArea CreateArea()
        {
            string className = path + ".Other.Area";
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
            return (IArea)objType;
        }
    }
}
