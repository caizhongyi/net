using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.App;
using JuSNS.Profile;
using JuSNS.Model;
using JuSNS.Config;

namespace JuSNS.SQLServer.App
{
    public class Album : DbBase, IAlbum
    {
        /// <summary>
        /// 指定相册封面图片地址
        /// </summary>
        /// <param name="albumid">相册ID</param>
        /// <returns>图片路径</returns>
        public string CoverPath(int albumid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "Select top 1 a.[FilePath] " +
                             " From " +
                             "[NT_Photo] a" +
                             " inner join [NT_album] b on b.albumid=a.albumid Where a.albumid=" + albumid + " and a.islock=0 order by a.IsCover desc,a.id asc";
                return Convert.ToString(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        /// <summary>
        /// 照片信息列表
        /// </summary>
        /// <param name="albumid">相册编号</param>
        /// <param name="userid">用户编号</param>
        /// <returns>返回照片集</returns>
        public List<PhotoInfo> InfoList(int albumid, int userid, int number)
        {
            string topstr = string.Empty;
            if (number >0)
            {
                topstr = " top " + number;
            }
            string Sql = "Select " + topstr + " [id],[albumid],[Description],[Views],[FileSize]," +
                         "[State],[IsCover],[Comments],[PostTime],[PostIP],[IsLock],[PhotoType],[Width],[Height],[userid]," +
                         "[FilePath] " +
                         "From " +
                         "[NT_Photo] where IsLock=0";
            Sql += " and [albumid]=" + albumid;
            if (userid != 0)
            {
                Sql += " and [userid]=" + userid;
            }
            Sql += " order by PostTime desc,id desc";
            List<PhotoInfo> infolist = new List<PhotoInfo>();
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                PhotoInfo info = new PhotoInfo();
                info.Id = Convert.ToInt32(rd["id"].ToString());
                info.AlbumID = Convert.ToInt32(rd["albumid"].ToString());
                if (rd["Description"] != DBNull.Value) info.Description = rd["Description"].ToString();
                info.FileSize = Convert.ToInt32(rd["FileSize"].ToString());
                info.State = Convert.ToInt32(rd["State"].ToString());
                info.IsCover = Convert.ToBoolean(rd["IsCover"].ToString());
                info.Comments = Convert.ToInt32(rd["Comments"].ToString());
                info.PostTime = Convert.ToDateTime(rd["PostTime"].ToString());
                if (rd["PostIP"] != DBNull.Value) info.PostIP = rd["PostIP"].ToString();
                info.IsLock =Convert.ToBoolean(rd["IsLock"].ToString());
                info.PhotoType = Convert.ToInt32(rd["PhotoType"].ToString());
                info.Width = Convert.ToInt32(rd["Width"].ToString());
                info.Height = Convert.ToInt32(rd["Height"].ToString());
                info.UserID = Convert.ToInt32(rd["userid"].ToString());
                info.Views = Convert.ToInt32(rd["Views"].ToString());
                if (rd["FilePath"] != DBNull.Value) info.FilePath = rd["FilePath"].ToString();
                infolist.Add(info);
            }
            rd.Dispose();
            return infolist;
        }

        public int DeleteAlbum(int albumid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetInfo(albumid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                {
                    sql = "delete from nt_album where albumid=" + albumid;
                }
                else
                {
                    sql = "delete from nt_album where albumid=" + albumid + " and userid=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "delete from nt_photo where albumid=" + albumid + "", null);
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(7) * downinter), 0, 1, "删除了相册");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        public List<AlbumInfo> AlbumList(int userid,int gid)
        {
            List<AlbumInfo> InfoList = new List<AlbumInfo>();
            string wherestr = string.Empty;
            if (gid > 0)
            {
                wherestr = " and groupid=" + gid;
            }
            string sql = "Select [albumid],[userid],[Title]," +
                         "[Description],[ImagesCount],[CreateTime],[Privacy],[GroupID] " +
                         "From " +
                         "[NT_Album] " +
                         "Where [userid]=" + userid + wherestr + " order by albumid desc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                AlbumInfo info = new AlbumInfo();
                info.AlbumID = Convert.ToInt32(rd["albumid"].ToString());
                info.UserID = Convert.ToInt32(rd["userid"].ToString());
                if (rd["Title"] != DBNull.Value) info.Title = rd["Title"].ToString();
                if (rd["Description"] != DBNull.Value) info.Description = rd["Description"].ToString();
                info.ImagesCount = Convert.ToInt32(rd["ImagesCount"].ToString());
                info.CreateTime = Convert.ToDateTime(rd["CreateTime"].ToString());
                info.Privacy =Convert.ToInt32(rd["Privacy"].ToString());
                if (rd["GroupID"] != DBNull.Value) info.GroupID = Convert.ToInt32(rd["GroupID"].ToString());
                InfoList.Add(info);
            }
            rd.Close();
            return InfoList;
        }

        public int Add(AlbumInfo Info)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CnString);
            Conn.Open();
            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                SqlParameter[] param = GetParameters(Info);
                string Sql = "Insert Into [NT_Album]" +
                             "([userid],[Title],[Description],[ImagesCount],[CreateTime],[Privacy],[GroupID]) " +
                             "Values(@userid,@Title,@Description,@ImagesCount,@CreateTime,@Privacy,@GroupID);" +
                             "Select SCOPE_IDENTITY()";
                int albumid = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, param));
                trans.Commit();
                return albumid;
            }
            catch (SqlException e)
            {
                trans.Rollback();
                throw e;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public AlbumInfo GetInfo(object albumid)
        {
            AlbumInfo Info = new AlbumInfo();
            string sql = "select albumid, userid, Title, Description, ImagesCount, CreateTime, Privacy, LastUploadTime, GroupID from nt_album where albumid=" + albumid;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.AlbumID = Convert.ToInt32(albumid);
                Info.CreateTime = Convert.ToDateTime(rd["CreateTime"]);
                Info.Description = rd["Description"].ToString();
                Info.GroupID = Convert.ToInt32(rd["GroupID"]);
                Info.ImagesCount = Convert.ToInt32(rd["ImagesCount"]);
                if (rd["LastUploadTime"] != DBNull.Value) Info.LastUploadTime = Convert.ToDateTime(rd["LastUploadTime"]);
                Info.Privacy = Convert.ToInt32(rd["Privacy"]);
                Info.Title = rd["Title"].ToString();
                Info.UserID = Convert.ToInt32(rd["userid"]);
            }
            rd.Close();
            return Info;
        }

        public int Edit(AlbumInfo Info)
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@albumid", SqlDbType.Int, 4);
            param[0].Value = Info.AlbumID;
            param[1] = new SqlParameter("@userid", SqlDbType.Int, 4);
            param[1].Value = Info.UserID;
            param[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            param[2].Value = Info.Title;

            param[3] = new SqlParameter("@Description", SqlDbType.NVarChar, 255);
            param[3].Value = Info.Description;

            param[4] = new SqlParameter("@Privacy", SqlDbType.Int);
            param[4].Value = Info.Privacy;

            string sql = "update nt_album set title=@Title,Description=@Description,Privacy=@Privacy where userid=@userid and albumid=@albumid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">群组实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetParameters(AlbumInfo Info)
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@albumid", SqlDbType.Int, 4);
            param[0].Value = Info.AlbumID;
            param[1] = new SqlParameter("@userid", SqlDbType.Int, 4);
            param[1].Value = Info.UserID;
            param[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            param[2].Value = Info.Title;

            param[3] = new SqlParameter("@Description", SqlDbType.NVarChar, 255);
            param[3].Value = Info.Description;
            if (Info.Description == null)
            {
                param[3].Value = "";
            }
            param[4] = new SqlParameter("@ImagesCount", SqlDbType.Int, 4);
            param[4].Value = Info.ImagesCount;
            param[5] = new SqlParameter("@CreateTime", SqlDbType.DateTime, 8);
            param[5].Value = Info.CreateTime;

            param[6] = new SqlParameter("@Privacy", SqlDbType.Int, 4);
            param[6].Value = (int)Info.Privacy;

            param[7] = new SqlParameter("@GroupID", SqlDbType.Int, 4);
            param[7].Value = Info.GroupID;

            return param;
        }
    }
}
