using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    /// <summary>
    /// 相册事务处理
    /// </summary>
    public class Album
    {
        static readonly private Album _instance = new Album();
        JuSNS.Factory.App.IAlbum dal;
        private Album()
        {
            dal = DataAccess.CreateAlbum();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Album Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 封面图片路径
        /// </summary>
        /// <param name="albumid">相册ID</param>
        /// <returns></returns>
        public string CoverPath(int albumid)
        {
            string returnPath = dal.CoverPath(albumid);
            if (string.IsNullOrEmpty(returnPath))
            {
                return JuSNS.Common.Public.rootDir + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/album/album.gif";
            }
            return returnPath;
        }

        /// <summary>
        /// 照片信息列表
        /// </summary>
        /// <param name="albumid">相册编号</param>
        /// <param name="userid">用户编号</param>
        /// <param name="number">调用数量</param>
        /// <returns>返回照片集</returns>
        public List<PhotoInfo> InfoList(int albumid, int userid, int number)
        {
            return dal.InfoList(albumid, userid, number);
        }

        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="albumid">相册ID，0表示头像相册</param>
        /// <param name="userid">用户名</param>
        /// <returns></returns>
        public int DeleteAlbum(int albumid, int userid)
        {
            return dal.DeleteAlbum(albumid, userid);
        }

        /// <summary>
        /// 获取指定用户相册集
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="gid">社群ID</param>
        /// <returns>返回相册实体类集合</returns>
        public List<AlbumInfo> AlbumList(int userid,int gid)
        {
            return dal.AlbumList(userid,gid);
        }

         /// <summary>
        /// 添加相册
        /// </summary>
        /// <param name="info">相册实体类</param>
        /// <returns>返回相册ID</returns>
        public int Add(AlbumInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 得到相册信息
        /// </summary>
        /// <param name="albumid">相册ID</param>
        /// <returns>返回相册实体类</returns>
        public AlbumInfo GetInfo(object albumid)
        {
            return dal.GetInfo(albumid);
        }

        /// <summary>
        /// 编辑相册
        /// </summary>
        /// <param name="info">相册实体类</param>
        /// <returns>1成功，0失败</returns>
        public int Edit(AlbumInfo info)
        {
            return dal.Edit(info);
        }
    }
}
