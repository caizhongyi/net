using JuSNS.Model;
using System.Reflection;
using System.Data;

namespace JuSNS.Factory.App
{
    public interface IPhoto
    {
        int UpdatePhotoCount(int AlbumID, int ImagesCount, int UserID);
        int Add(PhotoInfo pi);
        int Edit(PhotoInfo Info);
        int Delete(int PhotoID, int UserID);
        int DeletePhotoComment(int comid, int userid);
        PhotoInfo GetInfo(object PhotoID);
        AlbumInfo GetAlbumInfo(object aid);
        PhotoInfo GetInfoForUser(object UserID);
        DataTable GetPhotoList(int AlbumID, int UserID, int Number);
        int NextPhotoID(int albumid, int photoID, int userid);
        int PrePhotoID(int albumid, int photoID, int userid);
        int TheNumber(int albumid, int photoID, int userid);
        PhotoCommentInfo GetPhotoCommentInfo(int CID);
        int UpdateNumber(int photoid, int flg);
        int InsertPhotoComment(PhotoCommentInfo Info);
    }

    public sealed partial class DataAccess
    {
        public static IPhoto CreatePhoto()
        {
            string className = path + ".App.Photo";
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
            return (IPhoto)objType;
        }
    }
}
