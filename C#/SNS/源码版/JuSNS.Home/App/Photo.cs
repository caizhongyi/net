using System.Data;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Photo
    {
        static readonly private Photo _instance = new Photo();
        JuSNS.Factory.App.IPhoto dal;
        private Photo()
        {
            dal = DataAccess.CreatePhoto();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Photo Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 更新图片数量
        /// </summary>
        /// <param name="AlbumID"></param>
        /// <param name="ImagesCount"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpdatePhotoCount(int AlbumID, int ImagesCount,int UserID)
        {
            return dal.UpdatePhotoCount(AlbumID, ImagesCount, UserID);
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public int Add(PhotoInfo pi)
        {
            return dal.Add(pi);
        }

        /// <summary>
        /// 修改照片
        /// </summary>
        /// <param name="Info">照片实体类</param>
        /// <returns>修改成功返回1</returns>
        public int Edit(PhotoInfo Info)
        {
            return dal.Edit(Info);
        }


        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="PhotoID">图片ID</param>
        /// <param name="UserID">图片所在的用户ID</param>
        /// <returns></returns>
        public int Delete(int PhotoID, int UserID)
        {
            return dal.Delete(PhotoID, UserID);
        }

        /// <summary>
        /// 删除图片评论
        /// </summary>
        /// <param name="comid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeletePhotoComment(int comid, int userid)
        {
            return dal.DeletePhotoComment(comid, userid);
        }

        /// <summary>
        /// 得到照片信息(PhotoID)
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <returns></returns>
        public PhotoInfo GetInfo(object PhotoID)
        {
            return dal.GetInfo(PhotoID);
        }


        /// <summary>
        /// 得到相册信息
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <returns></returns>
        public AlbumInfo GetAlbumInfo(object aid)
        {
            return dal.GetAlbumInfo(aid);
        }



        /// <summary>
        /// 根据用户头像得到照片信息(UserID)
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public PhotoInfo GetInfoForUser(object UserID)
        {
            return dal.GetInfoForUser(UserID);
        }

        /// <summary>
        /// 得到所有相册
        /// </summary>
        /// <param name="AlbumID">相册</param>
        /// <param name="UserID">用户</param>
        /// <param name="Number">调用数量</param>
        /// <returns></returns>
        public DataTable GetPhotoList(int AlbumID, int UserID, int Number)
        {
            return dal.GetPhotoList(AlbumID, UserID, Number);
        }

        /// <summary>
        /// 同一相册上一张照片ID
        /// </summary>
        public int NextPhotoID(int albumid, int photoID, int userid)
        {
            return dal.NextPhotoID(albumid, photoID, userid);
        }

        /// <summary>
        /// 同一相册上一张照片ID
        /// </summary>
        public int PrePhotoID(int albumid, int photoID, int userid)
        {
            return dal.PrePhotoID(albumid, photoID, userid);
        }

        /// <summary>
        /// 照片位于相册中的第几张
        /// </summary>
        public int TheNumber(int albumid, int photoID,int userid)
        {
            return dal.TheNumber(albumid, photoID, userid);
        }

        /// <summary>
        /// 得到评论具体信息
        /// </summary>
        /// <param name="cid">图片评论的ID</param>
        /// <returns></returns>
        public PhotoCommentInfo GetPhotoCommentInfo(int cid)
        {
            return dal.GetPhotoCommentInfo(cid);
        }

        /// <summary>
        /// 更新相片信息
        /// </summary>
        /// <param name="photoid">相片ID</param>
        /// <param name="flg">0表示更新点击率，1表示更新评论数量，2更新分享数量</param>
        /// <returns>0或1</returns>
        public int UpdateNumber(int photoid,int flg)
        {
            return dal.UpdateNumber(photoid, flg);
        }

        /// <summary>
        /// 插入日志评论
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public int InsertPhotoComment(PhotoCommentInfo Info)
        {
            return dal.InsertPhotoComment(Info);
        }
    }
}
