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
    public class Web : DbBase, IWeb
    {
        public int RecAll(int infoid, int uid, int flag, string type)
        {
            byte isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            if (isadmin < 1) return 0;
            string sql = string.Empty;
            int getuserid = 0;
            string titleSTR = string.Empty;
            switch (type)
            {
                case "album":
                    sql = "update nt_album set isrec=" + flag + " where albumid=" + infoid;
                    getuserid = JuSNS.Home.App.Album.Instance.GetInfo(infoid).UserID;
                    titleSTR = "相册";
                    break;
                case "photo":
                    sql = "update nt_photo set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Photo.Instance.GetInfo(infoid).UserID;
                    titleSTR = "相片";
                    break;
                case "group":
                    sql = "update NT_Group set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Group.Instance.GetGroupInfo(infoid).UserID;
                    titleSTR = "群组";
                    break;
                case "topic":
                    sql = "update NT_GroupTopic set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Group.Instance.GetTopicInfo(infoid).UserID;
                    titleSTR = "话题";
                    break;
                case "ask":
                    sql = "update NT_Ask set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Ask.Instance.GetAskInfo(infoid).UserID;
                    titleSTR = "问答";
                    break;
                case "ative":
                    sql = "update NT_Ative set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(infoid).UserID;
                    titleSTR = "活动";
                    break;
                case "active":
                    sql = "update NT_Ative set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(infoid).UserID;
                    titleSTR = "活动";
                    break;
                case "goods":
                    sql = "update NT_ShopGoods set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(infoid).UserID;
                    titleSTR = "商品";
                    break;
                case "shop":
                    sql = "update NT_Shop set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Shop.Instance.GetShopForID(infoid).UserID;
                    titleSTR = "店铺";
                    break;
                case "multe":
                    sql = "update NT_ShopMultebuy set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Shop.Instance.GetMulteBuyInfo(infoid).UserID;
                    titleSTR = "团购";
                    break;
                case "vote":
                    sql = "update NT_Vote set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Vote.Instance.GetVoteInfo(infoid).UserID;
                    titleSTR = "投票";
                    break;
                case "twitter":
                    sql = "update NT_Twitter set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.TWrite.Instance.GetTwitterInfo(infoid).UserID;
                    titleSTR = "微博";
                    break;
                case "blog":
                    sql = "update NT_blog set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.Blog.Instance.GetBlogInfo(infoid).UserID;
                    titleSTR = "博客";
                    break;
                case "news":
                    sql = "update NT_news set isrec=" + flag + " where id=" + infoid;
                    getuserid = JuSNS.Home.App.News.Instance.GetNewsInfo(infoid).UserID;
                    titleSTR = "资讯";
                    break;
                case "user":
                    sql = "update NT_user set isrec=" + flag + " where userid=" + infoid;
                    getuserid = infoid;
                    titleSTR = "会员";
                    break;
                case "useradmin":
                    sql = "update NT_user set isAdmin=" + flag + " where userid=" + infoid;
                    break;
                case "userVip":
                    sql = "update NT_user set IsVip=" + flag + " where userid=" + infoid;
                    break;
            }
            if (type != "useradmin" && type != "userVip" && flag == 1)
            {
                JuSNS.Home.User.User.Instance.UpdateInte(getuserid, JuSNS.Common.Public.JSplit(41), 0, 0, titleSTR + "被推荐");
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int deletehelp(int hid, int uid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(uid))
            {
                string sql = "delete from nt_help where id=" + hid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public HelpInfo GetHelp(int helpid)
        {
            HelpInfo mdl = new HelpInfo();
            string sql = "select * from nt_help where id=" + helpid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.Content = dr["Content"].ToString();
                mdl.HelpID = dr["HelpID"].ToString();
                mdl.ID = (int)dr["ID"];
                mdl.Title = dr["Title"].ToString();
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public HelpInfo GetHelpQ(string q)
        {
            HelpInfo mdl = new HelpInfo();
            string sql = "select * from nt_help where HelpID='" + q + "'";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.Content = dr["Content"].ToString();
                mdl.HelpID = dr["HelpID"].ToString();
                mdl.ID = (int)dr["ID"];
                mdl.Title = dr["Title"].ToString();
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InsertHelp(HelpInfo info)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Content", SqlDbType.NText);
            param[0].Value = info.Content;
            param[1] = new SqlParameter("@HelpID", SqlDbType.NVarChar, 30);
            param[1].Value = info.HelpID;
            param[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            param[2].Value = info.Title;
            param[3] = new SqlParameter("@ID", SqlDbType.Int);
            param[3].Value = info.ID;
            string sql = string.Empty;
            if (info.ID > 0)
            {
                sql = "update nt_help set  HelpID=@HelpID, Title=@Title, [Content]=@Content where id=@ID";
            }
            else
            {
                sql = "insert into nt_help( HelpID, Title, Content)values(@HelpID, @Title, @Content)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        /// <summary>
        /// 得到用户排行
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="flag">0表示热门Blog用户，1热门用户，2热门美女，3热门帅哥，4人气排行，5活跃度，6积分排行，7推荐用户，8被关注度排行</param>
        /// <returns>List列表实体</returns>
        public List<UserInfo> GetTopUser(int number, int flag)
        {
            List<UserInfo> infolist = new List<UserInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " b.truename,a.userid from nt_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where b.state<>" + (byte)EnumUserState.Lock + " and a.IsDraft<>1 order by a.click desc,b.click desc,b.LastLoginTime desc,b.userid desc";
                    break;
                case 1:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " order by Click desc,userid desc";
                    break;
                case 2:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " and sex=1 order by Click desc,userid desc";
                    break;
                case 3:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " and sex=0 order by Click desc,userid desc";
                    break;
                case 4:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " order by (Click+attnumber) desc,userid desc";
                    break;
                case 5:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " order by (Click+attnumber+integral+inteyb) desc,userid desc";
                    break;
                case 6:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " order by integral desc,userid desc";
                    break;
                case 7:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " order by isrec desc,userid desc";
                    break;
                case 8:
                    sql = "select top " + number + " truename,userid from nt_user where state<>" + (byte)EnumUserState.Lock + " order by attnumber desc,userid desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                UserInfo info = new UserInfo();
                info.TrueName = dr["truename"].ToString();
                info.UserID = Convert.ToInt32(dr["UserID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }


        public List<BlogInfo> GetBlogList(int number, int flag)
        {
            List<BlogInfo> infolist = new List<BlogInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.title, b.truename,a.userid,a.PostTime from nt_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and a.IsDraft<>1 order by a.click desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + " a.id,a.title, b.truename,a.userid,a.PostTime from nt_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and a.IsDraft<>1 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.title, b.truename,a.userid,a.PostTime from nt_blog AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and a.IsDraft<>1 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                BlogInfo info = new BlogInfo();
                info.TrueName = dr["truename"].ToString();
                info.Title = dr["Title"].ToString();
                info.ID = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        //0热门，1最新，2推荐
        public List<AlbumInfo> GetGAlbumList(int number, int flag)
        {
            List<AlbumInfo> infolist = new List<AlbumInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " AlbumID,Title from nt_album where 1=1  order by ImagesCount desc,AlbumID desc";
                    break;
                case 1:
                    sql = "select top " + number + " AlbumID,Title from nt_album where 1=1  order by AlbumID desc";
                    break;
                case 2:
                    sql = "select top " + number + " AlbumID,Title from nt_album where 1=1  order by isRec desc,ImagesCount desc,AlbumID desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AlbumInfo info = new AlbumInfo();
                info.AlbumID = int.Parse(dr["AlbumID"].ToString());
                info.Title = dr["Title"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<NewsInfo> GetNewsList(int number, int flag)
        {
            List<NewsInfo> infolist = new List<NewsInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.title,a.color,a.bold,a.italic,a.content, b.truename,a.userid,a.PostTime,a.classid,c.ChannelName from nt_news AS a INNER JOIN NT_User AS b on a.userid=b.userid INNER JOIN NT_Newschannel AS c on a.classid=c.id where a.islock=" + (byte)EnumCusState.ForNormal + "  order by a.click desc,a.PostTime desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + " a.id,a.title,a.color,a.bold,a.italic,a.content, b.truename,a.userid,a.PostTime,a.classid,c.ChannelName from nt_news AS a INNER JOIN NT_User AS b on a.userid=b.userid INNER JOIN NT_Newschannel AS c on a.classid=c.id where a.islock=" + (byte)EnumCusState.ForNormal + " order by a.PostTime desc,a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.title,a.color,a.bold,a.italic,a.content, b.truename,a.userid,a.PostTime,a.classid,c.ChannelName from nt_news AS a INNER JOIN NT_User AS b on a.userid=b.userid INNER JOIN NT_Newschannel AS c on a.classid=c.id where a.islock=" + (byte)EnumCusState.ForNormal + " order by a.isrec desc,a.PostTime desc,a.id desc";
                    break;
                case 3:
                    sql = "select top " + number + " a.id,a.title,a.color,a.bold,a.italic,a.content, b.truename,a.userid,a.PostTime,a.classid,c.ChannelName from nt_news AS a INNER JOIN NT_User AS b on a.userid=b.userid INNER JOIN NT_Newschannel AS c on a.classid=c.id where a.islock=" + (byte)EnumCusState.ForNormal + " order by a.isrec desc,a.PostTime desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                NewsInfo info = new NewsInfo();
                info.TrueName = dr["truename"].ToString();
                info.Title = dr["Title"].ToString();
                if (dr["Color"] != DBNull.Value) info.Color = dr["Color"].ToString();
                if (dr["Bold"] != DBNull.Value) info.Bold = Convert.ToByte(dr["Bold"]);
                if (dr["Italic"] != DBNull.Value) info.Italic = Convert.ToByte(dr["Italic"]);
                info.Content = dr["Content"].ToString();
                info.ChannelName = dr["ChannelName"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.ClassID = int.Parse(dr["ClassID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<GroupInfo> GetGroupList(int number, int flag)
        {
            List<GroupInfo> infolist = new List<GroupInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.GroupName,a.Members,a.Portrait, b.truename,a.userid,a.PostTime from nt_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0  order by a.click desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + " a.id,a.GroupName,a.Members,a.Portrait, b.truename,a.userid,a.PostTime from nt_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.GroupName,a.Members,a.Portrait, b.truename,a.userid,a.PostTime from nt_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 order by a.isrec desc,a.id desc";
                    break;
                case 3:
                    sql = "select top " + number + " a.id,a.GroupName,a.Members,a.Portrait, b.truename,a.userid,a.PostTime from nt_group AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.Islight=1 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                GroupInfo info = new GroupInfo();
                info.GroupName = dr["GroupName"].ToString();
                info.Members = int.Parse(dr["Members"].ToString());
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Portrait = dr["Portrait"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<ShopGoodsInfo> GetGoodsList(int number, int flag)
        {
            List<ShopGoodsInfo> infolist = new List<ShopGoodsInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.goodsname,a.mprice,a.userid,a.pic,a.PostTime, b.truename from NT_ShopGoods AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0  order by a.click desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + "  a.id,a.goodsname,a.mprice,a.userid,a.pic,a.PostTime, b.truename  from NT_ShopGoods AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.goodsname,a.mprice,a.userid,a.pic,a.PostTime, b.truename from NT_ShopGoods AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopGoodsInfo info = new ShopGoodsInfo();
                info.GoodsName = dr["GoodsName"].ToString();
                info.MPrice = Convert.ToDouble(dr["MPrice"]);
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Pic = dr["Pic"].ToString();
                info.TrueName = dr["TrueName"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<ShopInfo> GetShopList(int number, int flag)
        {
            List<ShopInfo> infolist = new List<ShopInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.ShopName,a.PostTime,a.userid,a.pic, b.truename from NT_Shop AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0  order by a.click desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + "  a.id,a.ShopName,a.PostTime,a.userid,a.pic, b.truename  from NT_Shop AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.ShopName,a.PostTime,a.userid,a.pic, b.truename from NT_Shop AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                ShopInfo info = new ShopInfo();
                info.ShopName = dr["ShopName"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.Pic = dr["Pic"].ToString();
                info.TrueName = dr["TrueName"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<PhotoInfo> GetPhotoList(int number, int flag)
        {
            List<PhotoInfo> infolist = new List<PhotoInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.FilePath,a.PostTime,a.userid, b.truename from NT_Photo AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.AlbumID<>0  order by a.Views desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + "  a.id,a.FilePath,a.PostTime,a.userid, b.truename  from NT_Photo AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.AlbumID<>0 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.FilePath,a.PostTime,a.userid, b.truename from NT_Photo AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.AlbumID<>0 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                PhotoInfo info = new PhotoInfo();
                info.FilePath = dr["FilePath"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.TrueName = dr["TrueName"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<AtiveInfo> GetActiveList(int number, int flag)
        {
            List<AtiveInfo> infolist = new List<AtiveInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.AtiveName,a.PostTime,a.Members,a.StartTime,a.EndTime,a.userid, b.truename from NT_Ative AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and DATEDIFF(dd,EndTime,getdate())<0  order by a.Views desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + "  a.id,a.AtiveName,a.PostTime,a.Members,a.StartTime,a.EndTime,a.userid, b.truename  from NT_Ative AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and DATEDIFF(dd,EndTime,getdate())<0 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.AtiveName,a.PostTime,a.Members,a.StartTime,a.EndTime,a.userid, b.truename from NT_Ative AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and DATEDIFF(dd,EndTime,getdate())<0 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AtiveInfo info = new AtiveInfo();
                info.AtiveName = dr["AtiveName"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.StartTime = DateTime.Parse(dr["StartTime"].ToString());
                info.EndTime = DateTime.Parse(dr["EndTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.TrueName = dr["TrueName"].ToString();
                info.Members = int.Parse(dr["Members"].ToString());
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<AskInfo> GetAskList(int number, int flag)
        {
            List<AskInfo> infolist = new List<AskInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.Title,a.PostTime,a.jiFen,a.isJinji,a.userid, b.truename from NT_Ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.isClose=0 and a.ParentID=0  order by a.Views desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + "  a.id,a.Title,a.PostTime,a.jiFen,a.isJinji,a.userid, b.truename  from NT_Ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.isClose=0 and a.ParentID=0 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.Title,a.PostTime,a.jiFen,a.isJinji,a.userid, b.truename from NT_Ask AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock=0 and a.isClose=0 and a.ParentID=0 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AskInfo info = new AskInfo();
                info.Title = dr["Title"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.TrueName = dr["TrueName"].ToString();
                info.JiFen = int.Parse(dr["JiFen"].ToString());
                info.IsJinji = Convert.ToByte(dr["IsJinji"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<VoteInfo> GetVoteList(int number, int flag)
        {
            List<VoteInfo> infolist = new List<VoteInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.Title,a.PostTime,a.Mode,a.userid, b.truename from NT_Vote AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1  order by (a.JCnt+a.vcnt) desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + "  a.id,a.Title,a.PostTime,a.Mode,a.userid, b.truename  from NT_Vote AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1 order by a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.Title,a.PostTime,a.Mode,a.userid, b.truename from NT_Vote AS a INNER JOIN NT_User AS b on a.userid=b.userid where 1=1 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                VoteInfo info = new VoteInfo();
                info.Title = dr["Title"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.TrueName = dr["TrueName"].ToString();
                info.Mode = Convert.ToByte(dr["Mode"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public int GetGroupCount(int gid, int flag)
        {
            string whereSTR = " and TopicID<>0";
            if (flag == 0)
            {
                whereSTR = " and TopicID=0";
            }
            string sql = "select count(id) from NT_GroupTopic where groupid=" + gid + " and islock=" + (byte)EnumCusState.ForNormal + whereSTR;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public List<TwitterInfo> GetTwitterList(int number, int flag)
        {
            List<TwitterInfo> infolist = new List<TwitterInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.content, b.truename,a.userid,a.PostTime from NT_Twitter AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and len(content)>8  order by a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + " a.id,a.content, b.truename,a.userid,a.PostTime from NT_Twitter AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and len(content)>8 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                TwitterInfo info = new TwitterInfo();
                info.TrueName = dr["truename"].ToString();
                info.Content = dr["Content"].ToString();
                info.ID = int.Parse(dr["ID"].ToString());
                info.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<GroupTopicInfo> GetTopicList(int number, int flag)
        {
            List<GroupTopicInfo> infolist = new List<GroupTopicInfo>();
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select top " + number + " a.id,a.title,a.content, b.truename,a.userid,a.PostTime from NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and a.TopicID=0  order by a.Clicks desc,a.id desc";
                    break;
                case 1:
                    sql = "select top " + number + " a.id,a.title,a.content, b.truename,a.userid,a.PostTime from NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and a.TopicID=0 order by a.posttime desc,a.id desc";
                    break;
                case 2:
                    sql = "select top " + number + " a.id,a.title,a.content, b.truename,a.userid,a.PostTime from NT_GroupTopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.islock<>" + (byte)EnumCusState.ForLock + " and a.TopicID=0 order by a.isrec desc,a.id desc";
                    break;
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                GroupTopicInfo info = new GroupTopicInfo();
                info.TrueName = dr["truename"].ToString();
                info.Title = dr["Title"].ToString();
                info.Content = dr["Content"].ToString();
                info.Id = int.Parse(dr["ID"].ToString());
                info.Posttime = DateTime.Parse(dr["PostTime"].ToString());
                info.UserID = Convert.ToInt32(dr["UserID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<GroupClassInfo> GetTopicClassList(int parentid)
        {
            List<GroupClassInfo> infolist = new List<GroupClassInfo>();
            string sql = "select id,className,parentid from NT_GroupClass where parentid=" + parentid + "  order by id asc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                GroupClassInfo info = new GroupClassInfo();
                info.ClassName = dr["ClassName"].ToString();
                info.ID = int.Parse(dr["ID"].ToString());
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public int SetAllState(int infoid, int uid, int flag, string type)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid, cn))
                {
                    string sql = string.Empty;
                    switch (type)
                    {
                        case "user":
                            sql = "update nt_user set state=" + flag + " where userid=" + infoid;
                            break;
                    }
                    return DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }
                else
                    return 0;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int CheckInfoState(int infoid, int uid, int flag, string type)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int n = 0;
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid, cn))
                {
                    string sql = string.Empty;
                    switch (type)
                    {
                        case "vip":
                            sql = "update NT_JoinVip set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            if (n > 0)
                            {
                                sql = "select userid from NT_JoinVip where id=" + infoid;
                                int userid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                                if (flag == 0)
                                {
                                    sql = "update nt_user set isvip=1 where userid=" + userid;
                                }
                                else
                                {
                                    sql = "update nt_user set isvip=0 where userid=" + userid;
                                }
                                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            }
                            break;
                        case "news":
                            sql = "update NT_news set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "group":
                            sql = "update NT_group set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "topic":
                            sql = "update NT_groupTopic set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "blog":
                            sql = "update NT_blog set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "twitter":
                            sql = "update NT_Twitter set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "photo":
                            sql = "update NT_photo set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "goods":
                            sql = "update NT_ShopGoods set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "shop":
                            sql = "update NT_Shop set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "multe":
                            sql = "update NT_ShopMultebuy set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "active":
                            sql = "update NT_Ative set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "ask":
                            sql = "update NT_ask set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "newscomment":
                            sql = "update NT_NewsComment set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "blogcomment":
                            sql = "update NT_BlogComment set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "twittercomment":
                            sql = "update NT_TwitterComment set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "photocomment":
                            sql = "update NT_PhotoComment set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "goodscomment":
                            sql = "update NT_ShopComment set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "activecomment":
                            sql = "update NT_AtiveComment set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "ads":
                            sql = "update nt_ads set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "app":
                            sql = "update nt_app set IsLock=" + flag + " where id=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                        case "appdev":
                            sql = "update NT_AppDeveloper set IsLock=" + flag + " where userid=" + infoid;
                            n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            break;
                    }
                    return n;
                }
                else
                    return 0;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int Start(string type)
        {
            string sql = string.Empty;
            switch (type)
            {
                case "news":
                    sql = "delete from NT_news;";
                    sql += "delete from NT_NewsChannel;";
                    sql += "delete from NT_NewsComment";
                    break;
                case "mail":
                    sql = "delete from NT_Mailbox;";
                    break;
                case "send":
                    sql = "delete from NT_MailSend;";
                    break;
                case "book":
                    sql = "delete from NT_GBook;";
                    break;
                case "area":
                    sql = "delete from NT_Dict_Area;";
                    break;
                case "blog":
                    sql = "delete from NT_Blog;";
                    sql += "delete from NT_Blogclass;";
                    sql += "delete from NT_BlogComment;";
                    sql += "delete from NT_Blogfoot";
                    break;
                case "group":
                    sql = "delete from NT_group;";
                    sql += "delete from NT_GroupClass;";
                    sql += "delete from NT_GroupInvite;";
                    sql += "delete from NT_GroupMember;";
                    sql += "delete from NT_GroupTopic";
                    break;
                case "twitter":
                    sql = "delete from NT_Twitter;";
                    sql += "delete from NT_TwitterComment";
                    break;
                case "photo":
                    sql = "delete from NT_Photo;";
                    sql += "delete from NT_PhotoComment";
                    break;
                case "album":
                    sql = "delete from NT_Photo;";
                    sql += "delete from NT_PhotoComment;";
                    sql += "delete from NT_Album";
                    break;
                case "goods":
                    sql = "delete from NT_ShopGoods;";
                    sql += "delete from NT_ShopMultebuy;";
                    sql += "delete from NT_ShopMulteMember;";
                    sql += "delete from NT_ShopOrder;";
                    sql += "delete from NT_ShopUserComment;";
                    break;
                case "shop":
                    sql = "delete from NT_Shop;";
                    sql += "delete from NT_ShopComment;";
                    break;
                case "active":
                    sql = "delete from NT_Ative;";
                    sql += "delete from NT_AtiveATT;";
                    sql += "delete from NT_AtiveClass;";
                    sql += "delete from NT_AtiveComment;";
                    sql += "delete from NT_AtiveMember;";
                    break;
                case "poke":
                    sql = "delete from NT_poke;";
                    break;
                case "dyn":
                    sql = "delete from NT_Dyn;";
                    break;
                case "notice":
                    sql = "delete from NT_Notice;";
                    break;
                case "ask":
                    sql = "delete from NT_Ask;";
                    sql += "delete from NT_AskClass;";
                    sql += "delete from NT_AskUser;";
                    break;
                case "vote":
                    sql = "delete from NT_Vote;";
                    sql += "delete from NT_VoteDis;";
                    sql += "delete from NT_VoteOption;";
                    sql += "delete from NT_VoteTo;";
                    break;
                case "fav":
                    sql = "delete from NT_Favorite;";
                    sql += "delete from NT_FavoriteClass;";
                    break;
                case "gift":
                    sql = "delete from NT_Gift;";
                    sql += "delete from NT_GiftClass;";
                    sql += "delete from NT_GiftUser;";
                    break;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null); ;
        }

        public int DeleteNewsClass(int infoid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid, cn))
                {
                    sql = "delete from NT_NewsChannel where ID=" + infoid;
                    return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                }
                else
                    return 0;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertAds(AdsInfo info)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Click", SqlDbType.Int);
            param[0].Value = info.Click;
            param[1] = new SqlParameter("@Content", SqlDbType.NVarChar,200);
            param[1].Value = info.Content;
            param[2] = new SqlParameter("@EndTime", SqlDbType.DateTime);
            param[2].Value = info.EndTime;
            param[3] = new SqlParameter("@Id", SqlDbType.Int);
            param[3].Value = info.Id;
            param[4] = new SqlParameter("@IsLock", SqlDbType.Bit);
            param[4].Value = info.IsLock;
            param[5] = new SqlParameter("@Pic", SqlDbType.NVarChar,30);
            param[5].Value = info.Pic;
            param[6] = new SqlParameter("@PositionType", SqlDbType.NVarChar, 10);
            param[6].Value = info.PositionType;
            param[7] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            param[7].Value = info.Title;
            param[8] = new SqlParameter("@URL", SqlDbType.NVarChar, 200);
            param[8].Value = info.URL;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update nt_ads set Title=@Title, Content=@Content, Pic=@Pic, URL=@URL, IsLock=@IsLock, PositionType=@PositionType, EndTime=@EndTime where id=@Id";
            }
            else
            {
                sql = "insert into nt_ads(Title, [Content], Pic, URL, Click, IsLock, positionType, EndTime)values(@Title, @Content, @Pic, @URL, @Click, @IsLock, @PositionType, @EndTime)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public AdsInfo GetAdsInfo(int aid)
        {
            AdsInfo info = new AdsInfo();
            string sql = "select  id, Title, [Content], Pic, URL, Click, IsLock, positionType, EndTime from nt_ads where id=" + aid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Click = int.Parse(dr["click"].ToString());
                info.Content = dr["Content"].ToString();
                info.EndTime = DateTime.Parse(dr["EndTime"].ToString());
                info.Id = aid;
                info.IsLock = bool.Parse(dr["IsLock"].ToString());
                info.Pic = dr["Pic"].ToString();
                info.PositionType = dr["PositionType"].ToString();
                info.Title = dr["Title"].ToString();
                info.URL = dr["URL"].ToString();
            }
            dr.Close();
            return info;
        }

        public int DeleteAds(int infoid, int userid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                string sql = "delete from nt_ads where id=" + infoid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public List<AdsInfo> GetAdsList(string ptype,int number)
        {
            List<AdsInfo> infolist = new List<AdsInfo>();
            string sql = "select top " + number + " id, Title, [Content], Pic, URL, Click, IsLock, positionType, EndTime from nt_ads where positionType='" + ptype + "' and DATEDIFF(dd,EndTime,getdate())<0 order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AdsInfo info = new AdsInfo();
                info.Click = int.Parse(dr["click"].ToString());
                info.Content = dr["Content"].ToString();
                info.EndTime = DateTime.Parse(dr["EndTime"].ToString());
                info.Id = int.Parse(dr["id"].ToString()); ;
                info.IsLock = bool.Parse(dr["IsLock"].ToString());
                info.Pic = dr["Pic"].ToString();
                info.PositionType = dr["PositionType"].ToString();
                info.Title = dr["Title"].ToString();
                info.URL = dr["URL"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public int InsertEmailNotice(EmailNoticeInfo info)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Email", SqlDbType.NVarChar,150);
            param[0].Value = info.Email;
            param[1] = new SqlParameter("@Id", SqlDbType.Int);
            param[1].Value = info.Id;
            param[2] = new SqlParameter("@Ntype", SqlDbType.TinyInt);
            param[2].Value = info.Ntype;
            param[3] = new SqlParameter("@Posttime", SqlDbType.DateTime);
            param[3].Value = info.Posttime;
            param[4] = new SqlParameter("@Userid", SqlDbType.Int);
            param[4].Value = info.Userid;
            param[5] = new SqlParameter("@Vcode", SqlDbType.NVarChar, 16);
            param[5].Value = info.Vcode;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "delete from nt_emailnotice where Vcode=@Vcode";
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
            }
            else
            {
                sql = "insert into nt_emailnotice(userid, email, vcode, posttime, ntype)values(@Userid, @Email, @Vcode, @Posttime, @Ntype);Select SCOPE_IDENTITY()";
                return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            }
        }

        public EmailNoticeInfo GetEmailNoticeInfo(string vcode)
        {
            EmailNoticeInfo info = new EmailNoticeInfo();
            string sql = "select id,userid, email, vcode, posttime, ntype from nt_emailnotice where vcode='" + vcode + "'";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Email = dr["email"].ToString();
                info.Id = Convert.ToInt32(dr["id"]);
                info.Ntype = Convert.ToByte(dr["Ntype"]);
                info.Posttime = Convert.ToDateTime(dr["posttime"]);
                info.Userid = Convert.ToInt32(dr["Userid"]);
                info.Vcode = vcode;
                dr.Close();
                return info;
            }
            dr.Close();
            return null;
        }

        public int InsertReport(ReportInfo info)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Content", SqlDbType.NVarChar, 500);
            param[0].Value = info.Content;
            param[1] = new SqlParameter("@Id", SqlDbType.Int);
            param[1].Value = info.Id;
            param[2] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
            param[2].Value = info.PostIP;
            param[3] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[3].Value = info.PostTime;
            param[4] = new SqlParameter("@Urls", SqlDbType.NVarChar,200);
            param[4].Value = info.Urls;
            param[5] = new SqlParameter("@UserID", SqlDbType.Int);
            param[5].Value = info.UserID;
            string sql = "insert into nt_report([Content], PostTime, PostIP, Urls, UserID)values(@Content, @PostTime, @PostIP, @Urls, @UserID)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int DeleteReport(int rid, int userid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                string sql = "delete from NT_Report where id=" + rid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public int DeleteDyn(int did, int userid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                string sql = "delete from NT_dyn where id=" + did;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public List<LinksInfo> GetLinksList(int number, int type)
        {
            List<LinksInfo> infolist = new List<LinksInfo>();
            string sql = "select  id, LinkName, URL, LinkType, Pic, Islock from nt_links where islock=0 and LinkType="+type+"  order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                LinksInfo info = new LinksInfo();
                info.Id = int.Parse(dr["id"].ToString());
                info.Islock = Convert.ToBoolean(dr["islock"]);
                info.LinkName = dr["LinkName"].ToString();
                info.LinkType = Convert.ToByte(dr["LinkType"]);
                info.Pic = dr["Pic"].ToString();
                info.URL = dr["URL"].ToString();
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public LinksInfo GetLinksInfo(int lid)
        {
            string sql = "select  id, LinkName, URL, LinkType, Pic, Islock from nt_links where id="+lid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            LinksInfo info = new LinksInfo();
            if (dr.Read())
            {
                info.Id = int.Parse(dr["id"].ToString());
                info.Islock = Convert.ToBoolean(dr["islock"]);
                info.LinkName = dr["LinkName"].ToString();
                info.LinkType = Convert.ToByte(dr["LinkType"]);
                info.Pic = dr["Pic"].ToString();
                info.URL = dr["URL"].ToString();
            }
            dr.Close();
            return info;
        }

        public int DeleteLinks(int lid,int uid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(uid))
            {
                string sql = "delete from nt_links where id=" + lid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public int InsertLinks(LinksInfo info)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Id", SqlDbType.Int);
            param[0].Value = info.Id;
            param[1] = new SqlParameter("@Islock", SqlDbType.Bit);
            param[1].Value = info.Islock;
            param[2] = new SqlParameter("@LinkName", SqlDbType.NVarChar, 50);
            param[2].Value = info.LinkName;
            param[3] = new SqlParameter("@LinkType", SqlDbType.TinyInt);
            param[3].Value = info.LinkType;
            param[4] = new SqlParameter("@Pic", SqlDbType.NVarChar, 30);
            param[4].Value = info.Pic;
            param[5] = new SqlParameter("@URL", SqlDbType.NVarChar,100);
            param[5].Value = info.URL;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update nt_links set LinkName=@LinkName, URL=@URL, LinkType=@LinkType, Pic=@Pic where id=@Id";
            }
            else
            {
                sql = "insert into nt_links(LinkName, URL, LinkType, Pic, Islock)values(@LinkName, @URL, @LinkType, @Pic, @Islock)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int SetGroupLight(int gid, int uid, int flag)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(uid))
            {
                string sql = "update nt_group set Islight=" + flag + " where id=" + gid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public bool GetApiKey(string apikey)
        {
            string sql = "select count(*) from NT_Api where apikey='" + apikey + "' and islock=0";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }

        public bool ProgrameInstall()
        {
            string sql = "select count(*) from nt_user";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }
    }

}
