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
    public class News : DbBase, INews
    {
        public int DeleteNews(int nid, int UserID)
        {
            string sql = string.Empty;
            int getuserid = GetNewsInfo(nid).UserID;
            if (JuSNS.Home.User.User.Instance.IsAdmin(UserID))
            {
                sql = "delete from nt_news where ID=" + nid;
            }
            else
            {
                sql = "delete from nt_news where ID=" + nid + " and UserID=" + UserID;
            }
            int n= DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                //扣除积分
                int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(21) * downinter), 0, 1, "删除资讯");
            }
            return n;
        }

        public List<NewsChannelInfo> GetNewsChannel(int ParentID, int Type)
        {
            List<NewsChannelInfo> InfoList = new List<NewsChannelInfo>();
            string sql = "select id, ChannelName, ParentID, PerPageNumber, Pic,OrderID, ChannelType FROM NT_NewsChannel WHERE ParentID=" + ParentID + " and ChannelType=" + Type + " order by Orderid asc,id asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                NewsChannelInfo info = new NewsChannelInfo();
                info.ChannelName = rd["ChannelName"].ToString();
                info.ChannelType = Convert.ToByte(rd["ChannelType"]);
                info.Id = Convert.ToInt32(rd["Id"]);
                info.OrderID = Convert.ToInt32(rd["OrderID"]);
                info.ParentID = Convert.ToInt32(rd["ParentID"]);
                info.PerPageNumber = Convert.ToInt32(rd["PerPageNumber"]);
                info.Pic = rd["Pic"].ToString();
                InfoList.Add(info);
            }
            rd.Close();
            return InfoList;
        }

        public int InsertUpdateNewsClass(NewsChannelInfo Info)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@ChannelName", SqlDbType.NVarChar, 30);
            param[0].Value = Info.ChannelName;
            param[1] = new SqlParameter("@ChannelType", SqlDbType.TinyInt);
            param[1].Value = Info.ChannelType;
            param[2] = new SqlParameter("@Id", SqlDbType.Int);
            param[2].Value = Info.Id;
            param[3] = new SqlParameter("@OrderID", SqlDbType.Int);
            param[3].Value = Info.OrderID;
            param[4] = new SqlParameter("@ParentID", SqlDbType.Int);
            param[4].Value = Info.ParentID;
            param[5] = new SqlParameter("@PerPageNumber", SqlDbType.Int);
            param[5].Value = Info.PerPageNumber;
            param[6] = new SqlParameter("@Pic", SqlDbType.NVarChar,30);
            param[6].Value = Info.Pic;

            string sql = string.Empty;
            int n = 0;
            if (Info.Id > 0)
            {
                sql = "update NT_NewsChannel set ChannelName=@ChannelName, ParentID=@ParentID, PerPageNumber=@PerPageNumber,Pic=@Pic, ChannelType=@ChannelType, OrderID=@OrderID where id=@Id";
            }
            else
            {
                sql += "insert into NT_NewsChannel(ChannelName, ParentID, PerPageNumber, Pic, ChannelType, OrderID)values";
                sql += "(@ChannelName, @ParentID, @PerPageNumber, @Pic, @ChannelType, @OrderID)";
            }
            n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            return n;
        }

        public int InsertNews(NewsInfo Info)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@Attnumber", SqlDbType.Int);
            param[0].Value = Info.AttNumber;
            param[1] = new SqlParameter("@ClassID", SqlDbType.Int);
            param[1].Value = Info.ClassID;
            param[2] = new SqlParameter("@Click", SqlDbType.Int);
            param[2].Value = Info.Click;
            param[3] = new SqlParameter("@Comments", SqlDbType.Int);
            param[3].Value = Info.Comments;
            param[4] = new SqlParameter("@Content", SqlDbType.NText);
            param[4].Value = Info.Content;
            param[5] = new SqlParameter("@Files", SqlDbType.NVarChar, 40);
            param[5].Value = Info.Files;
            param[6] = new SqlParameter("@GPoint", SqlDbType.Int);
            param[6].Value = Info.GPoint;
            param[7] = new SqlParameter("@Id", SqlDbType.Int);
            param[7].Value = Info.Id;
            param[8] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[8].Value = Info.IsLock;
            param[9] = new SqlParameter("@IsRec", SqlDbType.TinyInt);
            param[9].Value = Info.IsRec;
            param[10] = new SqlParameter("@IsSys", SqlDbType.TinyInt);
            param[10].Value = Info.IsSys;
            param[11] = new SqlParameter("@Keywords", SqlDbType.NVarChar, 30);
            param[11].Value = Info.Keywords;
            param[12] = new SqlParameter("@Pic", SqlDbType.NVarChar,30);
            param[12].Value = Info.Pic;
            param[13] = new SqlParameter("@Point", SqlDbType.Int);
            param[13].Value = Info.Point;
            param[14] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
            param[14].Value = Info.PostIP;
            param[15] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[15].Value = Info.PostTime;
            param[16] = new SqlParameter("@ShareNumber", SqlDbType.Int);
            param[16].Value = Info.ShareNumber;
            param[17] = new SqlParameter("@Source", SqlDbType.NVarChar, 20);
            param[17].Value = Info.Source;
            param[18] = new SqlParameter("@SpecialList", SqlDbType.NVarChar, 30);
            param[18].Value = Info.SpecialList;
            param[19] = new SqlParameter("@Title", SqlDbType.NVarChar, 60);
            param[19].Value = Info.Title;
            param[20] = new SqlParameter("@UserID", SqlDbType.Int);
            param[20].Value = Info.UserID;
            param[21] = new SqlParameter("@Color", SqlDbType.NVarChar,8);
            param[21].Value = Info.Color;
            param[22] = new SqlParameter("@Bold", SqlDbType.TinyInt);
            param[22].Value = Info.Bold;
            param[23] = new SqlParameter("@Italic", SqlDbType.TinyInt);
            param[23].Value = Info.Italic;
            string sql = string.Empty;
            int n = 0;
            if (Info.Id > 0)
            {
                sql = "update nt_news set Title=@Title,Color=@Color,Bold=@Bold,Italic=@Italic, Content=@Content, ClassID=@ClassID,IsLock=@IsLock, Pic=@Pic, Files=@Files, Keywords=@Keywords, Source=@Source, Point=@Point, GPoint=@GPoint where id=@Id and UserID=@UserID";
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
                n = Info.Id;
            }
            else
            {
                sql += "insert into nt_news(title, Color, Bold, Italic, [Content], UserID, ClassID, PostTime, PostIP, IsLock, IsRec, Click, IsSys, ShareNumber, Pic, Files, AttNumber, Comments, Keywords, Source, Point, GPoint, SpecialList)values";
                sql += "(@Title, @Color, @Bold, @Italic, @Content, @UserID, @ClassID, @PostTime, @PostIP, @IsLock, @IsRec, @Click, @IsSys, @ShareNumber, @Pic, @Files, @AttNumber, @Comments, @Keywords, @Source, @Point, @GPoint, @SpecialList)";
                sql += ";Select SCOPE_IDENTITY()";
                n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
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

        public NewsInfo GetNewsInfo(object nid)
        {
            NewsInfo Info = new NewsInfo();
            string sql = "select title,Color,Bold,Italic, [Content], UserID, ClassID, PostTime, PostIP, IsLock, IsRec, Click, IsSys, ShareNumber, Pic, Files, AttNumber, Comments, Keywords, Source, Point, GPoint, SpecialList FROM NT_news where id=" + nid;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.AttNumber = Convert.ToInt32(rd["AttNumber"]);
                Info.ClassID = Convert.ToInt32(rd["ClassID"]);
                Info.Click = Convert.ToInt32(rd["Click"]);
                Info.Comments = Convert.ToInt32(rd["Comments"]);
                Info.Content = rd["Content"].ToString();
                if (rd["Color"] != DBNull.Value) Info.Color = rd["Color"].ToString();
                Info.Files = rd["Files"].ToString();
                Info.GPoint = Convert.ToInt32(rd["GPoint"]);
                Info.IsLock = Convert.ToByte(rd["IsLock"]);
                if (rd["Bold"] != DBNull.Value) Info.Bold = Convert.ToByte(rd["Bold"]);
                if (rd["Italic"] != DBNull.Value) Info.Italic = Convert.ToByte(rd["Italic"]);
                Info.IsRec = Convert.ToByte(rd["IsRec"]);
                Info.IsSys = Convert.ToByte(rd["IsSys"]);
                Info.Keywords = rd["Keywords"].ToString();
                Info.Pic = rd["Pic"].ToString();
                Info.Point = Convert.ToInt32(rd["Point"]);
                Info.PostIP = rd["PostIP"].ToString();
                Info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                Info.ShareNumber = Convert.ToInt32(rd["ShareNumber"]);
                Info.Source = rd["Source"].ToString();
                Info.SpecialList = rd["SpecialList"].ToString();
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

        public NewsChannelInfo GetNewsChannelInfo(object cid)
        {
            NewsChannelInfo info = new NewsChannelInfo();
            string sql = "select * from NT_NewsChannel where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.ChannelName = dr["ChannelName"].ToString();
                info.ChannelType = (byte)dr["ChannelType"];
                info.Id = Convert.ToInt32(cid);
                info.OrderID = Convert.ToInt32(dr["OrderID"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                info.PerPageNumber = Convert.ToInt32(dr["PerPageNumber"]);
                info.Pic = dr["Pic"].ToString();
                dr.Close();
                return info;
            }
            dr.Close();
            return null;
        }

        public int UpdateNewsState(int nid)
        {
            string sql = "update nt_news set click=click+1 where ID=" + nid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int UpdateATT(int NID, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "update nt_news set AttNumber=AttNumber+1 where ID=" + NID;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select AttNumber from nt_news where id=" + NID, null));
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public NewsCommentInfo GetNewsCommentInfo(int CID)
        {
            NewsCommentInfo Info = new NewsCommentInfo();
            string sql = "select a.ID, a.NewsID, a.UserID, a.[Content], a.PostTime, a.PostIP, a.IsLock, a.ParentID,b.truename from NT_NewsComment AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where Id=" + CID;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                Info.NewsID = Convert.ToInt32(rd["NewsID"]);
                Info.ParentID = Convert.ToInt32(rd["ParentID"]);
                Info.Content = rd["Content"].ToString();
                Info.Id = CID;
                Info.IsLock = Convert.ToByte(rd["IsLock"]);
                Info.PostIP = rd["PostIP"].ToString();
                Info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                Info.TrueName = rd["TrueName"].ToString();
                Info.UserID = Convert.ToInt32(rd["UserID"]);
            }
            rd.Close();
            return Info;
        }

        public List<NewsInfo> GetNewsList(int Number, int Flag, int UserID)
        {
            string sql = string.Empty;
            string orderBy = " Order by a.Id DESC";
            switch (Flag)
            {
                //0点击排行,1关注排行,2分享排行,3推荐,4评论排行,5最新更新,6系统新闻
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
            }
            string WhereSTR = string.Empty;
            if (UserID > 0)
            {
                WhereSTR = " and a.UserID=" + UserID;
            }
            List<NewsInfo> InfoList = new List<NewsInfo>();
            if (Flag == 6)
            {
                sql = "select top " + Number + "  ID, Title,color,bold,italic,Content,PostTime from NT_News where IsLock=0 and issys=1 order by id desc";
            }
            else
            {
                sql = "select top " + Number + "  a.ID, a.Title,color,bold,italic,a.Content,a.PostTime,a.UserID, b.TrueName from NT_News AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.IsLock=0" + WhereSTR + orderBy;
            }
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                NewsInfo info = new NewsInfo();
                info.Id = Convert.ToInt32(rd["ID"]);
                info.Title = rd["Title"].ToString();
                if (rd["Color"] != DBNull.Value) info.Color = rd["Color"].ToString();
                if (rd["Bold"] != DBNull.Value) info.Bold = Convert.ToByte(rd["Bold"]);
                if (rd["Italic"] != DBNull.Value) info.Italic = Convert.ToByte(rd["Italic"]);
                info.Content = rd["Content"].ToString();
                if (Flag != 6)
                {
                    info.TrueName = rd["TrueName"].ToString();
                    info.UserID = Convert.ToInt32(rd["UserID"]);
                }
                info.PostTime = Convert.ToDateTime(rd["PostTime"]);
                InfoList.Add(info);
            }
            rd.Close();
            return InfoList;
        }

        public int InsertNewsComment(NewsCommentInfo Info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@NewsID", SqlDbType.Int);
                param[0].Value = Info.NewsID;
                param[1] = new SqlParameter("@ParentID", SqlDbType.Int);
                param[1].Value = Info.ParentID;
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
                string sql = "insert into NT_NewsComment(NewsID,ParentID,Content,IsLock,PostIP,PostTime,UserID)values(@NewsID,@ParentID,@Content,@IsLock,@PostIP,@PostTime,@UserID)";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_news set Comments=Comments+1 where ID=@NewsID", param);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteNewsComment(int CID, int UserID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = 0;
                try
                {
                    getuserid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select userid from nt_Newscomment where id=" + CID + "", null));
                }
                catch { }
                if (JuSNS.Home.User.User.Instance.IsAdmin(UserID))
                {
                    sql = "delete from nt_Newscomment where ID=" + CID;
                }
                else
                {
                    sql = "delete from nt_Newscomment where ID=" + CID + " and UserID=" + UserID;
                }
                sql += ";update nt_Newscomment set parentID=0 where parentID=" + CID;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_news set Comments=Comments-1 where ID=" + CID, null);
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(22) * downinter), 0, 1, "删除资讯评论");
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
