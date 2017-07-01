using System;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.App;
using JuSNS.Profile;
using JuSNS.Config;
using JuSNS.Model;

namespace JuSNS.SQLServer.App
{
    public class TWrite : DbBase, ITWrite 
    {
        public string GetTwritterNew(object userid)
        {
            string sql = "select top 1 [Content] from NT_Twitter where UserID=" + userid+" and islock=0 order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                return dr["content"].ToString();
            }
            dr.Close();
            return string.Empty;
        }

        public string GetTwritterNew(object userid, out int tid)
        {
            tid = 0;
            string sql = "select top 1 id,[Content] from NT_Twitter where UserID=" + userid + " and islock=0 order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                tid = Convert.ToInt32(dr["id"]);
                return dr["content"].ToString();
            }
            dr.Close();
            return string.Empty;
        }

        public int InserTwitter(TwitterInfo info)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Comments", SqlDbType.Int);
            param[0].Value = info.Comments;
            param[1] = new SqlParameter("@Content", SqlDbType.NVarChar, 200);
            param[1].Value = info.Content;
            param[2] = new SqlParameter("@IsLock", SqlDbType.Bit);
            param[2].Value = info.IsLock;
            param[3] = new SqlParameter("@IsRec", SqlDbType.TinyInt);
            param[3].Value = info.IsRec;
            param[4] = new SqlParameter("@MType", SqlDbType.NVarChar,20);
            param[4].Value = info.MType;
            param[5] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
            param[5].Value = info.PostIP;
            param[6] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[6].Value = info.PostTime;
            param[7] = new SqlParameter("@UserID", SqlDbType.Int);
            param[7].Value = info.UserID;
            param[8] = new SqlParameter("@Pic", SqlDbType.NVarChar, 30);
            param[8].Value = info.Pic;
            param[9] = new SqlParameter("@Media", SqlDbType.NVarChar, 200);
            param[9].Value = info.Media;

            string sql = "insert into nt_twitter(UserID, [Content], PostTime, PostIP, IsLock, Comments, MType, isRec,Pic,Media)values(@UserID,@Content,@PostTime,@PostIP,@IsLock,@Comments,@MType,@IsRec,@Pic,@Media)";
            sql += ";Select SCOPE_IDENTITY()";
            int n= Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (n > 0)
            {
                JuSNS.Home.User.User.Instance.UpdateInte(info.UserID, JuSNS.Common.Public.JSplit(5), 0, 0, "发表微博");
            }
            return n;
        }

        public int DeleteTwitter(int tid, int userid)
        {
            string sql = string.Empty;
            int getuserid = GetTwitterInfo(tid).UserID;
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                sql = "delete from nt_twitter where ID=" + tid;
            }
            else
            {
                sql = "delete from nt_twitter where ID=" + tid + " and UserID=" + userid;
            }
            int n= DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                //扣除积分
                int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(5) * downinter), 0, 1, "删除了微博");
            }
            return n;
        }

        public int GetTwitterComments(object tid)
        {
            string sql = "select count(id) from NT_TwitterComment where Tid=" + tid + " and islock=0";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public TwitterInfo GetTwitterInfo(object tid)
        {
            string sql = "select ID, UserID, [Content], PostTime, PostIP, IsLock, Comments, MType, isRec, pic, media from NT_Twitter where id=" + tid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                TwitterInfo mdl = new TwitterInfo();
                mdl.Comments = Convert.ToInt32(dr["Comments"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.ID = Convert.ToInt32(dr["ID"]);
                mdl.IsLock = Convert.ToBoolean(dr["IsLock"]);
                mdl.IsRec = Convert.ToByte(dr["IsRec"]);
                mdl.Media = Convert.ToString(dr["Media"]);
                mdl.MType = Convert.ToString(dr["MType"]);
                mdl.Pic = Convert.ToString(dr["Pic"]);
                mdl.PostIP = Convert.ToString(dr["PostIP"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return mdl;
            }
            else
            {
                    dr.Close();
            return null;
            }
        }

        public TwitterCommentInfo GetTwitterCommentInfo(object cid)
        {
            string sql = "select  a.ID, a.UserID, a.Tid, a.[Content], a.CommentID, a.PostTime, a.PostIP, a.IsLock, b.TrueName from NT_TwitterComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                TwitterCommentInfo mdl = new TwitterCommentInfo();
                mdl.CommentID = Convert.ToInt32(dr["CommentID"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.ID = Convert.ToInt32(dr["ID"]);
                mdl.IsLock = Convert.ToBoolean(dr["IsLock"]);
                mdl.PostIP = Convert.ToString(dr["PostIP"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.Tid = Convert.ToInt32(dr["Tid"]);
                mdl.TrueName = Convert.ToString(dr["TrueName"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InserTwitterComment(TwitterCommentInfo Info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@Tid", SqlDbType.Int);
                param[0].Value = Info.Tid;
                param[1] = new SqlParameter("@CommentID", SqlDbType.Int);
                param[1].Value = Info.CommentID;
                param[2] = new SqlParameter("@Content", SqlDbType.NVarChar, 100);
                param[2].Value = Info.Content;
                param[3] = new SqlParameter("@IsLock", SqlDbType.Bit);
                param[3].Value = Info.IsLock;
                param[4] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[4].Value = Info.PostIP;
                param[5] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[5].Value = Info.PostTime;
                param[6] = new SqlParameter("@UserID", SqlDbType.Int);
                param[6].Value = Info.UserID;
                string sql = "insert into NT_TwitterComment(UserID, Tid, [Content], CommentID, PostTime, PostIP, IsLock)values(@UserID, @Tid, @Content, @CommentID, @PostTime, @PostIP, @IsLock)";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_Twitter set Comments=Comments+1 where ID=@Tid", param);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteTwitterComment(int cid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int guserid = 0; 
                try
                {
                    guserid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select userid from NT_TwitterComment where id=" + cid + "", null));
                }
                catch { }
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid, cn))
                {
                    sql = "delete from NT_TwitterComment where id=" + cid;
                }
                else
                {
                    sql = "delete from NT_TwitterComment where id=" + cid + " and UserID=" + uid;
                }
                sql += ";update NT_TwitterComment set CommentID=0 where CommentID=" + cid;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(guserid, (JuSNS.Common.Public.JSplit(6) * downinter), 0, 1, "删除了微博评论");
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
