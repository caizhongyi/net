using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;
using JuSNS.Common;

namespace JuSNS.Factory.App
{
    public interface IAlbum
    {
        /// <summary>
        /// 指定相册封面图片地址
        /// </summary>
        /// <param name="AlbumID">相册ID</param>
        /// <returns>图片路径</returns>
        string CoverPath(int albumid);
        List<PhotoInfo> InfoList(int albumid, int userid, int number);
        int DeleteAlbum(int albumid, int userid);
        List<AlbumInfo> AlbumList(int userid, int gid);
        int Add(AlbumInfo info);
        AlbumInfo GetInfo(object albumid);
        int Edit(AlbumInfo info);
    }

    public sealed partial class DataAccess
    {
        public static IAlbum CreateAlbum()
        {
            //string className = path + ".App.Album";
            //return (IAlbum)Assembly.Load(path).CreateInstance(className);
            string CacheKey = path + ".App.Album";
            object objType = DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(CacheKey);
                    DataCache.SetCache(CacheKey, objType);// 写入缓存
                }
                catch { }
            }
            return (IAlbum)objType;
        }
    }

}
