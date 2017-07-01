using System;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.App;
using JuSNS.Profile;
using JuSNS.Model;
using JuSNS.Config;


namespace JuSNS.SQLServer.App
{
    public class Photo : DbBase, IPhoto
    {

        public int UpdatePhotoCount(int AlbumID, int ImagesCount, int UserID)
        {
            string sql = "update nt_album set ImagesCount=" + ImagesCount + " where UserID=" + UserID + " and AlbumID=" + AlbumID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 添加照片
        /// </summary>
        /// <param name="Info">照片实体类</param>
        /// <returns>添加成功返回1</returns>
        public int Add(PhotoInfo Info)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CnString);
            Conn.Open();
            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                SqlParameter[] param = GetParameters(Info);
                string Sql = "Insert Into [NT_Photo]" +
                             "([AlbumID],[UserID],[Description],[Views],[FileSize],[State],[IsCover],[Comments],[PostTime]," +
                             "[PostIP],[PhotoType],[Width],[Height],[FilePath],[AtiveID]) " +
                             "Values(@AlbumID,@UploadUser,@Description,@Views,@FileSize,@State,@IsCover,@Comments,@PostTime," +
                             "@PostIP,@PhotoType,@Width,@Height,@FilePath,@AtiveID);" +
                             "Select SCOPE_IDENTITY()";
                int PhotoID = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, param));
                if (Info.AtiveID == 0)
                {
                    //更新照片数量
                    Sql = "Update [NT_Album] set [ImagesCount]=[ImagesCount]+1 where [AlbumID]=@AlbumID";
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, param);

                    //获取照片张数并设置封面
                    if (Info.PhotoType == 1)
                    {
                        Sql = "Select [ImagesCount] from [NT_Album] where  [AlbumID]=@AlbumID";
                        int photoCount = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, param));
                        if (photoCount == 1)
                        {
                            Sql = "Update  [NT_Photo] set iscover=1 where ID=" + PhotoID;
                            DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                        }
                    }
                    if (Info.PhotoType == 1)
                    {
                        //这里插入动态
                    }
                    else
                    {
                        //更新会员表头像地址
                        Sql = "Update [NT_User] Set [Portrait]=" + PhotoID + " Where [UserID]=@UploadUser";
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, param);
                        //插入头像动态
                    }
                }
                trans.Commit();
                return PhotoID;
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

        /// <summary>
        /// 修改照片
        /// </summary>
        /// <param name="Info">照片实体类</param>
        /// <returns>修改成功返回1</returns>
        public int Edit(PhotoInfo Info)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Description", SqlDbType.NVarChar,200);
            param[0].Value = Info.Description;
            param[1] = new SqlParameter("@IsCover", SqlDbType.Bit);
            param[1].Value = Info.IsCover;
            param[2] = new SqlParameter("@AlbumID", SqlDbType.Int);
            param[2].Value = Info.AlbumID;
            param[3] = new SqlParameter("@PhotoID", SqlDbType.Int);
            param[3].Value = Info.Id;
            param[4] = new SqlParameter("@UploadUser", SqlDbType.Int);
            param[4].Value = Info.UserID;

            string Sql = "Update [NT_Photo] " +
                         "Set [Description]=@Description,[IsCover]=@IsCover,[AlbumID]=@AlbumID " +
                         "Where [ID]=@PhotoID And [UserID]=@UploadUser";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int DeletePhotoComment(int comid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                //得到图片ID
                sql = "select photoid from nt_photocomment where id=" + comid;
                int photoid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                {
                    sql = "delete from nt_photocomment where id=" + comid;
                }
                else
                {
                    sql = "delete from nt_photocomment where id=" + comid + " and UserID=" + userid;
                }
                sql += ";update nt_photocomment set CommentID=0 where CommentID=" + comid;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_photo set Comments=Comments-1 where ID=" + photoid, null);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int Delete(int PhotoID, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                //得到相册ID
                int getuserid = GetInfo(PhotoID).UserID;
                sql = "select albumid from nt_photo where id=" + PhotoID;
                int albumid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                if (JuSNS.Home.User.User.Instance.IsAdmin(UserID))
                {
                    sql = "delete from nt_photo where id=" + PhotoID;
                }
                else
                {
                    sql = "delete from nt_photo where id=" + PhotoID + " and UserID=" + UserID;
                }
                int n= DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    sql = "update nt_album set ImagesCount=ImagesCount-1 where albumid=" + albumid;
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(8) * downinter), 0, 1, "删除了图片");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public AlbumInfo GetAlbumInfo(object aid)
        {
            AlbumInfo pInfo = new AlbumInfo();
            string sql = "select  * from NT_album where  albumid=" + aid + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                pInfo.AlbumID = Convert.ToInt32(rd["AlbumID"]);
                pInfo.CreateTime = Convert.ToDateTime(rd["CreateTime"]);
                pInfo.Description = rd["Description"].ToString();
                pInfo.GroupID = Convert.ToInt32(rd["GroupID"]);
                pInfo.ImagesCount = Convert.ToInt32(rd["ImagesCount"]);
                //pInfo.LastUploadTime = Convert.ToDateTime(rd["LastUploadTime"]);
                pInfo.Privacy = Convert.ToInt32(rd["Privacy"]);
                pInfo.Title = Convert.ToString(rd["Title"]);
                pInfo.UserID = Convert.ToInt32(rd["UserID"]);
                rd.Close();
                return pInfo;
            }
            else
            {
                rd.Close();
                return null;
            }
        }

        public PhotoInfo GetInfo(object PhotoID)
        {
            PhotoInfo pInfo = new PhotoInfo();
            string sql = "select  a.id, a.AlbumID, a.UserID, a.Description, a.Views, a.FileSize, a.State, a.IsCover,a.isrec, a.Comments, a.PostTime, a.PostIP, a.PhotoType, a.Width, a.Height, a.FilePath, a.ShareNumber, a.IsLock,b.truename from NT_photo AS a INNER JOIN NT_User AS b on a.userid=b.userid  where a.id=" + PhotoID + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                pInfo.AlbumID = Convert.ToInt32(rd["AlbumID"]);
                pInfo.Comments = Convert.ToInt32(rd["Comments"]);
                pInfo.Description = rd["Description"].ToString();
                pInfo.FilePath = rd["FilePath"].ToString();
                pInfo.FileSize = Convert.ToInt32(rd["FileSize"]);
                pInfo.Height = Convert.ToInt32(rd["Height"]);
                pInfo.IsCover = Convert.ToBoolean(rd["IsCover"]);
                pInfo.IsRec = Convert.ToBoolean(rd["IsRec"]);
                pInfo.PhotoType = Convert.ToInt32(rd["PhotoType"]);
                pInfo.PostIP = rd["PostIP"].ToString();
                pInfo.TrueName = rd["TrueName"].ToString();
                pInfo.AlbumName = string.Empty;
                pInfo.PostTime = Convert.ToDateTime(rd["PostTime"]);
                pInfo.State = Convert.ToInt32(rd["State"]);
                pInfo.UserID = Convert.ToInt32(rd["UserID"]);
                pInfo.Views = Convert.ToInt32(rd["Views"]);
                pInfo.Width = Convert.ToInt32(rd["Width"]);
                pInfo.ShareNumber = Convert.ToInt32(rd["ShareNumber"]);
                pInfo.IsLock = Convert.ToBoolean(rd["IsLock"]);
                pInfo.Id = Convert.ToInt32(rd["Id"]);
                rd.Close();
                return pInfo;
            }
            else
            {
                rd.Close();
                return null;
            }
        }

        public PhotoInfo GetInfoForUser(object UserID)
        {
            PhotoInfo pInfo = new PhotoInfo();
            string sql = "select  a.id, a.AlbumID, a.UserID, a.Description, a.Views, a.FileSize, a.State, a.IsCover, a.Comments, a.PostTime, a.PostIP, a.PhotoType, a.Width, a.Height, a.FilePath from NT_photo as a inner join nt_user as b on  b.Portrait = a.id where b.UserID=" + UserID + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                pInfo.AlbumID = Convert.ToInt32(rd["AlbumID"]);
                pInfo.Comments = Convert.ToInt32(rd["Comments"]);
                pInfo.Description = rd["Description"].ToString();
                pInfo.FilePath = rd["FilePath"].ToString();
                pInfo.FileSize = Convert.ToInt32(rd["FileSize"]);
                pInfo.Height = Convert.ToInt32(rd["Height"]);
                pInfo.IsCover = Convert.ToBoolean(rd["IsCover"]);
                pInfo.PhotoType = Convert.ToInt32(rd["PhotoType"]);
                pInfo.PostIP = rd["PostIP"].ToString();
                pInfo.PostTime = Convert.ToDateTime(rd["PostTime"]);
                pInfo.State = Convert.ToInt32(rd["State"]);
                pInfo.UserID = Convert.ToInt32(rd["UserID"]);
                pInfo.Views = Convert.ToInt32(rd["Views"]);
                pInfo.Width = Convert.ToInt32(rd["Width"]);
                rd.Close();
                return pInfo;
            }
            else
            {
                rd.Close();
                return null;
            }
        }

        public DataTable GetPhotoList(int AlbumID, int UserID, int Number)
        {
            string sql = "select top " + Number + " id, AlbumID, UserID, Description, Views, FileSize, State, IsCover, Comments, PostTime, PostIP, PhotoType, Width, Height, FilePath, ShareNumber, IsLock from nt_photo where AlbumID=" + AlbumID + " and UserID=" + UserID + " order by id desc";
            return  DbHelper.ExecuteTable(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">照片实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetParameters(PhotoInfo Info)
        {
            SqlParameter[] param = new SqlParameter[16];

            param[0] = new SqlParameter("@PhotoID", SqlDbType.Int, 4);
            param[0].Value = Info.Id;
            param[1] = new SqlParameter("@AlbumID", SqlDbType.Int, 4);
            param[1].Value = Info.AlbumID;
            param[2] = new SqlParameter("@Description", SqlDbType.NVarChar, 1000);
            param[2].Value = Info.Description;

            param[3] = new SqlParameter("@Views", SqlDbType.Int, 4);
            param[3].Value = Info.Views;
            param[4] = new SqlParameter("@FilePath", SqlDbType.NVarChar, 255);
            param[4].Value = Info.FilePath;
            param[5] = new SqlParameter("@FileSize", SqlDbType.Int, 4);
            param[5].Value = Info.FileSize;

            param[6] = new SqlParameter("@State", SqlDbType.Int, 4);
            param[6].Value = Info.State;
            param[7] = new SqlParameter("@IsCover", SqlDbType.Bit);
            param[7].Value = Info.IsCover;
            param[8] = new SqlParameter("@Comments", SqlDbType.Int, 4);
            param[8].Value = Info.Comments;

            param[9] = new SqlParameter("@PostTime", SqlDbType.DateTime, 8);
            param[9].Value = Info.PostTime;
            param[10] = new SqlParameter("@PostIP", SqlDbType.VarChar, 15);
            param[10].Value = Info.PostIP;

            param[11] = new SqlParameter("@PhotoType", SqlDbType.Int, 4);
            param[11].Value = Info.PhotoType;
            param[12] = new SqlParameter("@Width", SqlDbType.Int, 4);
            param[12].Value = Info.Width;
            param[13] = new SqlParameter("@Height", SqlDbType.Int, 4);
            param[13].Value = Info.Height;

            param[14] = new SqlParameter("@UploadUser", SqlDbType.Int, 4);
            param[14].Value = Info.UserID;

            param[15] = new SqlParameter("@AtiveID", SqlDbType.Int, 4);
            param[15].Value = Info.AtiveID;

            return param;
        }


        /// <summary>
        /// 同一相册下一张照片ID
        /// </summary>
        /// <param name="photoID">照片ID</param>
        /// <returns></returns>
        public int NextPhotoID(int albumid,int photoID,int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int returnValue = 0;
                string Sql = "select  top 1 id from [NT_Photo] where AlbumID=" + albumid + " and id>" + photoID + " and userid="+userid+" order by id asc";
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (dr.Read())
                {
                    returnValue = Convert.ToInt32(dr["id"]);
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    Sql = "select  top 1 id from [NT_Photo] where AlbumID=" + albumid + " order by id asc";
                    IDataReader dr1 = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                    if (dr1.Read())
                    {
                        returnValue = Convert.ToInt32(dr1["id"]);
                        dr.Close();
                    }
                    else { dr.Close(); }
                }
                return returnValue;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
        }

        /// <summary>
        /// 同一相册上一张照片ID
        /// </summary>
        /// <param name="photoID">照片ID</param>
        /// <returns></returns>
        public int PrePhotoID(int albumid, int photoID, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int returnValue = 0;
                string Sql = "select  top 1 id from [NT_Photo] where AlbumID=" + albumid + " and id<" + photoID + " and userid=" + userid + " order by id desc";
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (dr.Read())
                {
                    returnValue = Convert.ToInt32(dr["id"]);
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    Sql = "select  top 1 id from [NT_Photo] where AlbumID=" + albumid + " order by id desc";
                    IDataReader dr1 = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                    if (dr1.Read())
                    {
                        returnValue = Convert.ToInt32(dr1["id"]);
                        dr.Close();
                    }
                    else { dr.Close(); }
                }
                return returnValue;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
        }


        /// <summary>
        /// 照片位于相册中的第几张
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public int TheNumber(int albumid, int photoID, int userid)
        {
            int i = 0;
            string Sql = "select id from [NT_Photo] where AlbumID=" + albumid + " and islock=0 and userid=" + userid + " order by id asc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (dr.Read())
            {
                i++;
                int thisPhoto=Convert.ToInt32(dr[0]);
                if (thisPhoto == photoID)
                {
                    break;
                }
            }
            dr.Dispose();
            return i;
        }


        public PhotoCommentInfo GetPhotoCommentInfo(int CID)
        {
            PhotoCommentInfo Info = new PhotoCommentInfo();
            string sql = "select a.ID, a.PhotoID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.CommentID,b.truename from NT_PhotoComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where Id=" + CID;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.PhotoID = Convert.ToInt32(rd["PhotoID"]);
                Info.CommentID = Convert.ToInt32(rd["CommentID"]);
                Info.Content = rd["Content"].ToString();
                Info.ID = CID;
                Info.IsLock = Convert.ToBoolean(rd["IsLock"]);
                Info.PostIP = rd["PostIP"].ToString();
                Info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                Info.TrueName = rd["TrueName"].ToString();
                Info.UserID = Convert.ToInt32(rd["UserID"]);
            }
            rd.Close();
            return Info;
        }

        public int UpdateNumber(int photoid, int flg)
        {
            string sql = string.Empty;
            switch (flg)
            {
                case 0:
                    sql = "update nt_photo set views=views+1 where id=" + photoid;
                    break;
                case 1:
                    sql = "update nt_photo set comments=comments+1 where id=" + photoid;
                    break;
                case 2:
                    sql = "update nt_photo set ShareNumber=ShareNumber+1 where id=" + photoid;
                    break;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertPhotoComment(PhotoCommentInfo Info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PhotoID", SqlDbType.Int);
                param[0].Value = Info.PhotoID;
                param[1] = new SqlParameter("@CommentID", SqlDbType.Int);
                param[1].Value = Info.CommentID;
                param[2] = new SqlParameter("@Content", SqlDbType.NVarChar, 300);
                param[2].Value = Info.Content;
                param[3] = new SqlParameter("@IsLock", SqlDbType.Bit);
                param[3].Value = Info.IsLock;
                param[4] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[4].Value = Info.PostIP;
                param[5] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[5].Value = Info.PostTime;
                param[6] = new SqlParameter("@UserID", SqlDbType.Int);
                param[6].Value = Info.UserID;
                string sql = "insert into NT_PhotoComment(PhotoID,CommentID,Content,IsLock,PostIP,PostTime,UserID)values(@PhotoID,@CommentID,@Content,@IsLock,@PostIP,@PostTime,@UserID)";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_photo set Comments=Comments+1 where ID=@PhotoID", param);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
    }
}
