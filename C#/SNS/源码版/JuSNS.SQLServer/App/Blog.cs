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
    public class Blog : DbBase, IBlog
    {
        public List<BlogClassInfo> GetBlogClass(int UserID, int ParentID)
        {
            List<BlogClassInfo> InfoList = new List<BlogClassInfo>();
            string sql = "select id,CName,OrderID,ParentID,UserID from NT_BlogClass WHERE ParentID=" + ParentID + " and UserID=" + UserID + " order by Orderid asc,id asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                BlogClassInfo info = new BlogClassInfo();
                info.CName = rd["CName"].ToString();
                info.Id = Convert.ToInt32(rd["Id"].ToString());
                info.OrderID = Convert.ToInt32(rd["OrderID"].ToString());
                info.ParentID = Convert.ToInt32(rd["ParentID"].ToString());
                info.UserID = Convert.ToInt32(rd["UserID"].ToString());
                InfoList.Add(info);
            }
            rd.Close();
            return InfoList;
        }

        public List<BlogFootInfo> GetBlogFoot(int Number, int FID)
        {
            List<BlogFootInfo> InfoList = new List<BlogFootInfo>();
            string sql = "select TOP " + Number + " a.id,a.UserID,a.blogID,a.CreatTime,b.TrueName from NT_BlogFoot AS a INNER JOIN NT_User AS b ON a.UserID=b.UserID WHERE a.blogID=" + FID + "  order by a.CreatTime DESC,a.id DESC";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                BlogFootInfo info = new BlogFootInfo();
                info.BlogID = Convert.ToInt32(rd["BlogID"].ToString());
                info.CreatTime = Convert.ToDateTime(rd["CreatTime"]);
                info.TrueName = rd["TrueName"].ToString();
                info.UserID = Convert.ToInt32(rd["UserID"].ToString());
                InfoList.Add(info);
            }
            rd.Close();
            return InfoList;
        }

        public List<BlogInfo> GetBlogList(int Number, int Flag, int UserID)
        {
            string sql = string.Empty;
            string orderBy = " Order by a.Id DESC";
            switch (Flag)
            {
                //0点击排行,1关注排行,2分享排行,3推荐,4评论排行,5最新更新
                case 0:
                    orderBy = " order by a.click Desc,a.id Desc";
                    break;
                case 1:
                    orderBy = " order by a.attNumber Desc,a.id Desc";
                    break;
                case 2:
                    orderBy = " order by a.shareNumber Desc,a.id Desc";
                    break;
                case 3:
                    orderBy = " order by a.isRec Desc,a.id Desc";
                    break;
                case 4:
                    orderBy = " order by a.comments Desc,a.id Desc";
                    break;
                case 5:
                    orderBy = " order by a.LastModTime Desc,a.id Desc";
                    break;
            }
            string WhereSTR = string.Empty;
            if (UserID > 0)
            {
                WhereSTR = " and a.UserID=" + UserID;
            }
            List<BlogInfo> InfoList = new List<BlogInfo>();
            sql = "select top " + Number + "  a.ID, a.Title,a.PostTime,a.UserID, b.TrueName from NT_Blog AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.IsLock=0" + WhereSTR + orderBy;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                BlogInfo info = new BlogInfo();
                info.ID = Convert.ToInt32(rd["ID"]);
                info.Title = rd["Title"].ToString();
                info.TrueName = rd["TrueName"].ToString();
                info.UserID = Convert.ToInt32(rd["UserID"]);
                info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                InfoList.Add(info);
            }
            rd.Close();
            return InfoList;
        }

        public int AddSortBlogClass(string SortName, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter param = new SqlParameter("@SortName", SqlDbType.NVarChar, 30);
                param.Value = SortName;
                string sql = "insert into nt_blogclass(CName, UserID, OrderID, ParentID)values(@SortName," + UserID + ",0,0)";
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                int topid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select top 1 id from nt_blogclass where userID=" + UserID + " order by id desc", null));
                return topid;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteBlogClass(int UserID, int bID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "delete from nt_blogclass where id=" + bID + " and userid=" + UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(UserID))
                {
                    sql = "delete from nt_blogclass where id=" + bID;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    sql = "update nt_blog set MyClassID=0 where MyClassID=" + bID;
                    sql += ";update nt_blog set sysClassID=0 where sysClassID=" + bID;
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public BlogClassInfo GetBlogClassInfo(object cid)
        {
            BlogClassInfo info = new BlogClassInfo();
            string sql = "select id, CName, UserID, OrderID, ParentID from nt_blogclass where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.CName = dr["cname"].ToString();
                info.Id = Convert.ToInt32(cid);
                info.OrderID = Convert.ToInt32(dr["OrderID"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return info;
            }
            dr.Close();
            return null;
        }

        public int InsertBlogClass(BlogClassInfo info)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CName", SqlDbType.NVarChar,30);
            param[0].Value = info.CName;
            param[1] = new SqlParameter("@Id", SqlDbType.Int);
            param[1].Value = info.Id;
            param[2] = new SqlParameter("@OrderID", SqlDbType.Int);
            param[2].Value = info.OrderID;
            param[3] = new SqlParameter("@ParentID", SqlDbType.Int);
            param[3].Value = info.ParentID;
            param[4] = new SqlParameter("@UserID", SqlDbType.Int);
            param[4].Value = info.UserID;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update NT_BlogClass set CName=@CName where id=@Id";
            }
            else
            {
                sql = "insert into nt_blogclass(CName, UserID, OrderID, ParentID)values(@CName, @UserID, @OrderID, @ParentID)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateBlog(BlogInfo Info)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@Attnumber", SqlDbType.Int);
            param[0].Value = Info.Attnumber;
            param[1] = new SqlParameter("@Click", SqlDbType.Int);
            param[1].Value = Info.Click;
            param[2] = new SqlParameter("@Comments", SqlDbType.Int);
            param[2].Value = Info.Comments;
            param[3] = new SqlParameter("@Content", SqlDbType.NText);
            param[3].Value = Info.Content;
            param[4] = new SqlParameter("@ID", SqlDbType.Int);
            param[4].Value = Info.ID;
            param[5] = new SqlParameter("@IsDraft", SqlDbType.TinyInt);
            param[5].Value = Info.IsDraft;
            param[6] = new SqlParameter("@IsLock", SqlDbType.Bit);
            param[6].Value = Info.IsLock;
            param[7] = new SqlParameter("@IsRec", SqlDbType.TinyInt);
            param[7].Value = Info.IsRec;
            param[8] = new SqlParameter("@LastModTime", SqlDbType.DateTime);
            param[8].Value = Info.LastModTime;
            param[9] = new SqlParameter("@MyClassID", SqlDbType.Int);
            param[9].Value = Info.MyClassID;
            param[10] = new SqlParameter("@PicPath", SqlDbType.NVarChar, 30);
            param[10].Value = Info.PicPath;
            param[11] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
            param[11].Value = Info.PostIP;
            param[12] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[12].Value = Info.PostTime;
            param[13] = new SqlParameter("@Privacy", SqlDbType.Int);
            param[13].Value = Info.Privacy;
            param[14] = new SqlParameter("@Reads", SqlDbType.Int);
            param[14].Value = Info.Reads;
            param[15] = new SqlParameter("@ShareNumber", SqlDbType.Int);
            param[15].Value = Info.ShareNumber;
            param[16] = new SqlParameter("@SysClassID", SqlDbType.Int);
            param[16].Value = Info.SysClassID;
            param[17] = new SqlParameter("@Title", SqlDbType.NVarChar, 40);
            param[17].Value = Info.Title;
            param[18] = new SqlParameter("@UserID", SqlDbType.Int);
            param[18].Value = Info.UserID;
            string sql = string.Empty;
            int n = 0;
            if (Info.ID > 0)
            {
                sql = "update NT_Blog set title=@Title, content=@Content, postip=@PostIP, islock=@IsLock, Privacy=@Privacy, PicPath=@PicPath, LastModTime=@LastModTime, IsDraft=@IsDraft, MyClassID=@MyClassID, SysClassID=@SysClassID where ID=@ID and UserID=@UserID";
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
                n = Info.ID;
            }
            else
            {
                sql += "insert into nt_blog(UserID, Title, [Content], PostTime, PostIP, IsLock, Comments, Privacy, PicPath, LastModTime, Reads, isRec, IsDraft, attnumber, click, ShareNumber, myClassID, sysClassID)values";
                sql += "(@UserID, @Title, @Content, @PostTime, @PostIP, @IsLock, @Comments, @Privacy, @PicPath, @LastModTime, @Reads, @IsRec, @IsDraft, @Attnumber, @Click, @ShareNumber, @MyClassID, @SysClassID);Select SCOPE_IDENTITY()";
                n=Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            }
            if (n == 0)
            {
                return 0;
            }
            else
            {
                return n;
            }
        }

        public BlogCommentInfo GetBlogCommentInfo(int CID)
        {
            BlogCommentInfo Info = new BlogCommentInfo();
            string sql = "select a.ID, a.BlogID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.CommID,b.truename from NT_BlogComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where Id=" + CID;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.BlogID = Convert.ToInt32(rd["BlogID"]);
                Info.CommID = Convert.ToInt32(rd["CommID"]);
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

        public BlogInfo GetBlogInfo(int UserID, object bID)
        {
            BlogInfo Info = new BlogInfo();
            string sql = string.Empty;
            if (UserID > 0)
            {
                sql = "select ID, UserID, Title, [Content], PostTime, PostIP, IsLock, Comments, Privacy, PicPath, LastModTime, Reads, isRec, IsDraft, attnumber, click, ShareNumber, myClassID, sysClassID from NT_blog where UserID=" + UserID + " and Id=" + bID;
            }
            else
            {
                sql = "select ID, UserID, Title, [Content], PostTime, PostIP, IsLock, Comments, Privacy, PicPath, LastModTime, Reads, isRec, IsDraft, attnumber, click, ShareNumber, myClassID, sysClassID from NT_blog where Id=" + bID;
            }
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.Attnumber = Convert.ToInt32(rd["Attnumber"]);
                Info.Click = Convert.ToInt32(rd["Click"]);
                Info.Comments = Convert.ToInt32(rd["Comments"]);
                Info.Content = rd["Content"].ToString();
                Info.ID = Convert.ToInt32(bID);
                Info.IsDraft = Convert.ToByte(rd["IsDraft"]);
                Info.IsLock = Convert.ToBoolean(rd["IsLock"]);
                Info.IsRec = Convert.ToByte(rd["IsRec"]);
                Info.LastModTime = Convert.ToDateTime(rd["LastModTime"]);
                Info.MyClassID = Convert.ToInt32(rd["MyClassID"]);
                Info.PicPath = rd["PicPath"].ToString();
                Info.PostIP = rd["PostIP"].ToString();
                Info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                Info.Privacy = Convert.ToInt32(rd["Privacy"]);
                Info.Reads = Convert.ToInt32(rd["Reads"]);
                Info.ShareNumber = Convert.ToInt32(rd["ShareNumber"]);
                Info.SysClassID = Convert.ToInt32(rd["SysClassID"]);
                Info.Title = rd["Title"].ToString();
                Info.UserID = Convert.ToInt32(rd["UserID"]);
                rd.Close();
                return Info;
            }
            else
            {
                rd.Close();
                return null;
            }
        }

        public BlogInfo GetBlogInfo(object bID)
        {
            BlogInfo Info = new BlogInfo();
            string sql = string.Empty;
            sql = "select ID, UserID, Title, [Content], PostTime, PostIP, IsLock, Comments, Privacy, PicPath, LastModTime, Reads, isRec, IsDraft, attnumber, click, ShareNumber, myClassID, sysClassID from NT_blog where Id=" + bID;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.Attnumber = Convert.ToInt32(rd["Attnumber"]);
                Info.Click = Convert.ToInt32(rd["Click"]);
                Info.Comments = Convert.ToInt32(rd["Comments"]);
                Info.Content = rd["Content"].ToString();
                Info.ID = Convert.ToInt32(bID);
                Info.IsDraft = Convert.ToByte(rd["IsDraft"]);
                Info.IsLock = Convert.ToBoolean(rd["IsLock"]);
                Info.IsRec = Convert.ToByte(rd["IsRec"]);
                Info.LastModTime = Convert.ToDateTime(rd["LastModTime"]);
                Info.MyClassID = Convert.ToInt32(rd["MyClassID"]);
                Info.PicPath = rd["PicPath"].ToString();
                Info.PostIP = rd["PostIP"].ToString();
                Info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                Info.Privacy = Convert.ToInt32(rd["Privacy"]);
                Info.Reads = Convert.ToInt32(rd["Reads"]);
                Info.ShareNumber = Convert.ToInt32(rd["ShareNumber"]);
                Info.SysClassID = Convert.ToInt32(rd["SysClassID"]);
                Info.Title = rd["Title"].ToString();
                Info.UserID = Convert.ToInt32(rd["UserID"]);
                rd.Close();
                return Info;
            }
            else
            {
                rd.Close();
                return null;
            }
        }

        public string GetClassName(int bID)
        {
            if (bID == 0) return "未分类";
            try
            {
                string sql = "select CName from nt_blogclass where id=" + bID;
                return DbHelper.ExecuteScalar(CommandType.Text, sql, null).ToString();
            }
            catch { return "---"; }
        }

        public int DeleteBlog(int BID, int UserID)
        {
            string sql = string.Empty;
            int getuserid = GetBlogInfo(BID).UserID;
            if (JuSNS.Home.User.User.Instance.IsAdmin(UserID))
            {
                sql = "delete from nt_blog where ID=" + BID;
            }
            else
            {
                sql = "delete from nt_blog where ID=" + BID + " and UserID=" + UserID;
            }
            int n= DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                //扣除积分
                int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(3) * downinter), 0, 1, "删除博客");
            }
            return n;
        }

        public int DeleteBlogComment(int CID, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int blogid = 0;
                int getuserid = 0;
                try
                {
                    sql = "select BlogID from nt_blogcomment where id=" + CID;
                    blogid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                    sql = "select userid from nt_blogcomment where id=" + CID;
                    getuserid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                }
                catch { }
                if (JuSNS.Home.User.User.Instance.IsAdmin(UserID))
                {
                    sql = "delete from nt_blogcomment where ID=" + CID;
                }
                else
                {
                    sql = "delete from nt_blogcomment where ID=" + CID + " and UserID=" + UserID;
                }
                sql += ";update nt_blogcomment set CommID=0 where CommID=" + CID;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_blog set Comments=Comments-1 where ID=" + blogid, null);
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(4) * downinter), 0, 1, "删除了博客评论");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int UpdateATT(int BID, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "update nt_blog set attnumber=attnumber+1 where ID=" + BID;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select attnumber from nt_blog where id=" + BID, null));
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int UpdateBlogState(int BID, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                //插入脚印
                string sql = "select count(id) from nt_blogfoot where userid=" + UserID + " and BlogID=" + BID + "";
                if (UserID > 0)
                {
                    if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null)) == 0)
                    {
                        sql = "insert into nt_blogfoot(BlogID,UserID,CreatTime)values(" + BID + "," + UserID + ",'" + DateTime.Now + "')";
                    }
                    else
                    {
                        sql = "update nt_blogfoot set CreatTime='" + DateTime.Now + "' where BlogID=" + BID + " and UserID=" + UserID;
                    }
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }
                sql = "update nt_blog set click=click+1 where ID=" + BID;
                return DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertBlogComment(BlogCommentInfo Info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@BlogID", SqlDbType.Int);
                param[0].Value = Info.BlogID;
                param[1] = new SqlParameter("@CommID", SqlDbType.Int);
                param[1].Value = Info.CommID;
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
                string sql = "insert into NT_BlogComment(BlogID,CommID,Content,IsLock,PostIP,PostTime,UserID)values(@BlogID,@CommID,@Content,@IsLock,@PostIP,@PostTime,@UserID)";
                int n = DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, param);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_blog set Comments=Comments+1 where ID=@BlogID", param);
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
