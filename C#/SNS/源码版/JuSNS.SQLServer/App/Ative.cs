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
    public class Ative : DbBase, IAtive
    {
        public List<AtiveClassInfo> GetAtiveClassList(int parentid)
        {
            List<AtiveClassInfo> infolist = new List<AtiveClassInfo>();
            string sql = "select Id,ClassName,ParentID from nt_ativeclass where parentid=" + parentid + " order by id asc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AtiveClassInfo info = new AtiveClassInfo();
                info.ClassName = Convert.ToString(dr["ClassName"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public int InsertUpdate(AtiveInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[25];
                param[0] = new SqlParameter("@AddRess", SqlDbType.NVarChar, 100);
                param[0].Value = info.AddRess;
                param[1] = new SqlParameter("@AreaID", SqlDbType.Int);
                param[1].Value = info.AreaID;
                param[2] = new SqlParameter("@AtiveName", SqlDbType.NVarChar, 60);
                param[2].Value = info.AtiveName;
                param[3] = new SqlParameter("@ATT", SqlDbType.Int);
                param[3].Value = info.ATT;
                param[4] = new SqlParameter("@BaoMingTime", SqlDbType.DateTime);
                param[4].Value = info.BaoMingTime;
                param[5] = new SqlParameter("@ClassID", SqlDbType.Int);
                param[5].Value = info.ClassID;
                param[6] = new SqlParameter("@Clicks", SqlDbType.Int);
                param[6].Value = info.Clicks;
                param[7] = new SqlParameter("@Content", SqlDbType.NText);
                param[7].Value = info.Content;
                param[8] = new SqlParameter("@EndTime", SqlDbType.DateTime);
                param[8].Value = info.EndTime;
                param[9] = new SqlParameter("@GroupID", SqlDbType.Int);
                param[9].Value = info.GroupID;
                param[10] = new SqlParameter("@IsChecks", SqlDbType.TinyInt);
                param[10].Value = info.IsChecks;
                param[11] = new SqlParameter("@Id", SqlDbType.Int);
                param[11].Value = info.Id;
                param[12] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[12].Value = info.IsLock;
                param[13] = new SqlParameter("@IsRec", SqlDbType.TinyInt);
                param[13].Value = info.IsRec;
                param[14] = new SqlParameter("@Links", SqlDbType.NVarChar, 200);
                param[14].Value = info.Links;
                param[15] = new SqlParameter("@Members", SqlDbType.Int);
                param[15].Value = info.Members;
                param[16] = new SqlParameter("@Money", SqlDbType.NVarChar, 30);
                param[16].Value = info.Money;
                param[17] = new SqlParameter("@Note", SqlDbType.NVarChar, 200);
                param[17].Value = info.Note;
                param[18] = new SqlParameter("@PersionNumber", SqlDbType.Int);
                param[18].Value = info.PersionNumber;
                param[19] = new SqlParameter("@Photo", SqlDbType.NVarChar, 30);
                param[19].Value = info.Photo;
                param[20] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[20].Value = info.PostIP;
                param[21] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[21].Value = info.PostTime;
                param[22] = new SqlParameter("@StartTime", SqlDbType.DateTime);
                param[22].Value = info.StartTime;
                param[23] = new SqlParameter("@UserID", SqlDbType.Int);
                param[23].Value = info.UserID;
                param[24] = new SqlParameter("@AreaID1", SqlDbType.Int);
                param[24].Value = info.AreaID1;
                string sql = string.Empty;
                if (info.Id > 0)
                {
                    sql = "update NT_Ative set AtiveName=@AtiveName, AreaID=@AreaID, AreaID1=@AreaID1,ClassID=@ClassID, StartTime=@StartTime, EndTime=@EndTime, IsLock=@IsLock, Money=@Money, BaoMingTime=@BaoMingTime, PersionNumber=@PersionNumber, AddRess=@AddRess, GroupID=@GroupID, Content=@Content,  PostIP=@PostIP, Links=@Links, Photo=@Photo, IsChecks=@IsChecks, IsRec=@IsRec, Note=@Note where id=@Id and userid=@UserID";
                    int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                    if (m > 0)
                        return info.Id;
                    else
                        return 0;
                }
                else
                {
                    sql = "Insert Into NT_Ative(AtiveName, AreaID, ClassID,AreaID1, StartTime, EndTime, UserID, Members, IsLock, Money, BaoMingTime, PersionNumber, Clicks, ATT, AddRess, GroupID, [Content],  PostTime, PostIP, Links, Photo, IsChecks, IsRec, Note)values(@AtiveName, @AreaID, @ClassID,@AreaID1, @StartTime, @EndTime, @UserID, @Members, @IsLock, @Money, @BaoMingTime, @PersionNumber, @Clicks, @ATT, @AddRess, @GroupID, @Content,  @PostTime, @PostIP, @Links, @Photo, @IsChecks, @IsRec, @Note)";
                    sql += ";Select SCOPE_IDENTITY()";
                    return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public AtiveInfo GetAtiveInfo(object aid)
        {
            AtiveInfo info = new AtiveInfo();
            string sql = "select a.id,a.AtiveName, a.AreaID, a.ClassID,a.AreaID1, a.StartTime, a.EndTime, a.UserID, a.Members, a.IsLock, a.Money, a.BaoMingTime, a.PersionNumber, a.Clicks, a.ATT, a.AddRess, a.GroupID, a.[Content],  a.PostTime, a.PostIP, a.Links, a.Photo, a.IsChecks, a.IsRec, a.Note,b.truename,c.ClassName from nt_ative AS a INNER JOIN NT_User AS b on a.userid=b.userid INNER JOIN NT_AtiveClass AS c on c.id=a.classid where a.id=" + aid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.AddRess = Convert.ToString(dr["AddRess"]);
                info.AreaID = Convert.ToInt32(dr["AreaID"]);
                info.AreaID1 = Convert.ToInt32(dr["AreaID1"]);
                info.AtiveName = Convert.ToString(dr["AtiveName"]);
                info.TrueName = Convert.ToString(dr["TrueName"]);
                info.ClassName = Convert.ToString(dr["ClassName"]);
                info.ATT = Convert.ToInt32(dr["ATT"]);
                info.BaoMingTime = Convert.ToDateTime(dr["BaoMingTime"]);
                info.ClassID = Convert.ToInt32(dr["ClassID"]);
                info.Clicks = Convert.ToInt32(dr["Clicks"]);
                info.Content = Convert.ToString(dr["Content"]);
                info.EndTime = Convert.ToDateTime(dr["EndTime"]);
                info.GroupID = Convert.ToInt32(dr["GroupID"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                info.IsChecks = Convert.ToByte(dr["IsChecks"]);
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.IsRec = Convert.ToByte(dr["IsRec"]);
                info.Links = Convert.ToString(dr["Links"]);
                info.Members = Convert.ToInt32(dr["Members"]);
                info.Money = Convert.ToString(dr["Money"]);
                info.Note = Convert.ToString(dr["Note"]);
                info.PersionNumber = Convert.ToInt32(dr["PersionNumber"]);
                info.Photo = Convert.ToString(dr["Photo"]);
                info.PostIP = Convert.ToString(dr["PostIP"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.StartTime = Convert.ToDateTime(dr["StartTime"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public List<AtiveInfo> GetFriendAtive(int number, string q, int userid)
        {
            List<AtiveInfo> infolist = new List<AtiveInfo>();
            if (q.Length > 2) q = q.Replace("0,", string.Empty);
            string sql = "select  top " + number + " b.ativename,b.id from nt_ativemember AS a INNER JOIN NT_Ative AS b on b.id=a.Aid where a.userid in (" + q + ") and a.state<>0 order by a.postTime desc,a.id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AtiveInfo info = new AtiveInfo();
                info.Id = Convert.ToInt32(dr["id"]);
                info.AtiveName = Convert.ToString(dr["ativename"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public void UpdateClick(int aid)
        {
            string sql = "update nt_ative set clicks=clicks+1 where id=" + aid;
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int GetMembers(int aid, int flag)
        {
            string sql = "select count(id) from nt_ativemember where state=" + flag + " and aid=" + aid;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        /// <summary>
        /// 参与或者关注活动
        /// </summary>
        /// <param name="aid">活动ID</param>
        /// <param name="uid">参与者ID</param>
        /// <param name="flag">1关注活动，2参与活动</param>
        /// <returns>0失败，1参与了但是需要审核，2成功，3已经参与了</returns>
        public int JoinAtive(int aid, int uid, int flag)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                //得到是否需要审核
                int j = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(id) from nt_ativemember where aid=" + aid + " and state=" + flag + " and userid=" + uid, null));
                if (j > 0)
                    return 3;
                int ischeck = 0;
                try
                {
                    ischeck = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select IsChecks from nt_ative where id=" + aid, null));
                }
                catch { return 0; }
                int state = flag;
                if (state == 2)
                {
                    if (ischeck == 1)
                    {
                        state = 0;
                    }
                }
                //插入记录
                string sql = string.Empty;
                int k = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(id) from nt_ativemember where aid=" + aid + " and userid=" + uid, null));
                if (k > 0)
                {
                    sql = "update nt_ativemember set state=" + state + " where aid=" + aid + " and userid=" + uid + "";
                }
                else
                {
                    sql = "insert into nt_ativemember(UserID, Aid, PostTime, Members, State)values(" + uid + "," + aid + ",'" + DateTime.Now + "',1," + state + ")";
                }
                int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                //0失败，1参与了但是需要审核，2成功，3已经参与了
                if (m > 0)
                {
                    if (state == 0)
                        return 1;
                    else
                        return 2;
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 得到是否参加或者关注过活动
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="uid"></param>
        /// <returns>-1没有记录，0参与了未审核，1已经关注过了，2已经参与了</returns>
        public int GetAtiveATT(int aid, int uid)
        {
            string sql = "select state from nt_ativemember where aid=" + aid + " and userid=" + uid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                int state = Convert.ToByte(dr["state"]);
                dr.Close();
                return state;
            }
            else
            {
                dr.Close();
                return -1;
            }
        }

        public int OutAtive(int aid, int uid)
        {
            string sql = "delete from nt_ativemember where aid=" + aid + " and userid=" + uid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int GetCheckMembers(int aid)
        {
            string sql = "select count(id) from nt_ativemember where state=0 and aid=" + aid;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public List<AtiveMemberInfo> GetCheckMemberList(int number,int aid,int flag)
        {
            List<AtiveMemberInfo> infolist = new List<AtiveMemberInfo>();
            string sql = "select top " + number + " a.id,a.userid,a.aid,a.posttime,b.truename from nt_ativemember AS a INNER JOIN NT_user AS b on a.userid=b.userid where a.state=" + flag + " and a.aid=" + aid + " order by a.posttime asc,a.id asc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AtiveMemberInfo info = new AtiveMemberInfo();
                info.Id = Convert.ToInt32(dr["Id"]);
                info.Aid = Convert.ToInt32(dr["aid"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.TrueName = Convert.ToString(dr["TrueName"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public List<PhotoInfo> GetAtiveAlbumList(int number, int aid)
        {
            List<PhotoInfo> infolist = new List<PhotoInfo>();
            string sql = "select top " + number + " a.id,a.AlbumID,a.AtiveID,a.FilePath,a.UserID,a.PostTime,b.truename from nt_photo AS a INNER JOIN NT_user AS b on a.userid=b.userid where a.islock=0  and a.AtiveID=" + aid + " order by a.posttime desc,a.id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                PhotoInfo info = new PhotoInfo();
                info.Id = Convert.ToInt32(dr["Id"]);
                info.AtiveID = Convert.ToInt32(dr["AtiveID"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.TrueName = Convert.ToString(dr["TrueName"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.FilePath = Convert.ToString(dr["FilePath"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        //0失败，1成功，2人数达到上限
        public int CheckAtiveMember(int mid, int aid, int flag)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (flag == 2)
                {
                    sql = "select count(id) from nt_ativemember where aid=" + aid + " and state=2";
                    int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn,CommandType.Text, sql, null));
                    //得到允许的会员数
                    AtiveInfo mdl = GetAtiveInfo(aid);
                    if (mdl.PersionNumber > 0)
                    {
                        if (n >= mdl.PersionNumber)
                        {
                            return 2;
                        }
                    }
                    sql = "update nt_ativemember set state=2 where id=" + mid + " and aid=" + aid;
                    int j = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    if (j > 0)
                    {
                        //插入通知
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    sql = "delete from nt_ativemember where id=" + mid + " and aid=" + aid;
                    int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    if (m > 0)
                    {
                        //插入通知
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int AtiveImgCount(int aid)
        {
            string sql = "select count(id) from nt_Photo where AtiveID=" + aid + " and islock=0";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public int TheNumber(int aid, int photoID)
        {
            int i = 0;
            string Sql = "select id from [NT_Photo] where ativeid=" + aid + " and islock=0 order by id asc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (dr.Read())
            {
                i++;
                int thisPhoto = Convert.ToInt32(dr[0]);
                if (thisPhoto == photoID)
                {
                    break;
                }
            }
            dr.Dispose();
            return i;
        }

        public int PrePhotoID(int aid, int photoID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int returnValue = 0;
                string Sql = "select  top 1 id from [NT_Photo] where ativeid=" + aid + " and id<" + photoID + " order by id desc";
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (dr.Read())
                {
                    returnValue = Convert.ToInt32(dr["id"]);
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    Sql = "select  top 1 id from [NT_Photo] where ativeid=" + aid + " order by id desc";
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

        public int NextPhotoID(int aid, int photoID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int returnValue = 0;
                string Sql = "select  top 1 id from [NT_Photo] where ativeid=" + aid + " and id>" + photoID + " order by id asc";
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (dr.Read())
                {
                    returnValue = Convert.ToInt32(dr["id"]);
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    Sql = "select  top 1 id from [NT_Photo] where ativeid=" + aid + " order by id asc";
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
        public int InsertAtiveComment(AtiveCommentInfo Info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@AtiveID", SqlDbType.Int);
                param[0].Value = Info.AtiveID;
                param[1] = new SqlParameter("@CommentID", SqlDbType.Int);
                param[1].Value = Info.CommentID;
                param[2] = new SqlParameter("@Content", SqlDbType.NVarChar, 300);
                param[2].Value = Info.Content;
                param[3] = new SqlParameter("@IsLock", SqlDbType.Bit);
                param[3].Value = Info.IsLock;
                param[4] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[4].Value = Info.PostIP;
                param[5] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[5].Value = Info.Posttime;
                param[6] = new SqlParameter("@UserID", SqlDbType.Int);
                param[6].Value = Info.Userid;
                string sql = "insert into NT_AtiveComment(AtiveID,CommentID,Content,IsLock,PostIP,PostTime,UserID)values(@AtiveID,@CommentID,@Content,@IsLock,@PostIP,@PostTime,@UserID)";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                //更新积分
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteAtive(int aid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetAtiveInfo(aid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_Ative where id=" + aid;
                }
                else
                {
                    sql = "delete from NT_Ative where id=" + aid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(18) * downinter), 0, 1, "删除活动");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteAtiveClass(int aid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_AtiveClass where id=" + aid;
                    sql += "delete from NT_Ative where classid=" + aid;
                }
                else
                {
                    sql = "delete from NT_AtiveClass where id=" + aid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);

                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteAtiveComment(int comid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int aid = 0;
                int getuserid = 0;
                try
                {
                    sql = "select ativeid from nt_ativecomment where id=" + comid;
                    aid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                    sql = "select userid from nt_ativecomment where id=" + comid;
                    getuserid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                }
                catch { }
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn) || GetAtiveInfo(aid).UserID == userid)
                {
                    sql = "delete from NT_AtiveComment where id=" + comid;
                }
                else
                {
                    sql = "delete from NT_AtiveComment where id=" + comid + " and UserID=" + userid;
                }
                sql += ";update NT_AtiveComment set CommentID=0 where CommentID=" + comid;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(19) * downinter), 0, 1, "删除活动评论");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public AtiveCommentInfo GetAtiveCommentInfo(int cid)
        {
            AtiveCommentInfo mdl = new AtiveCommentInfo();
            string sql = "select a.id, a.userid, a.ativeID, a.posttime, a.postIP, a.IsLock, a.[content], a.CommentID,b.trueName from nt_ativecomment AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.AtiveID = Convert.ToInt32(dr["AtiveID"]);
                mdl.CommentID = Convert.ToInt32(dr["CommentID"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.TrueName = Convert.ToString(dr["TrueName"]);
                mdl.Id = Convert.ToInt32(cid);
                mdl.IsLock = Convert.ToBoolean(dr["IsLock"]);
                mdl.PostIP = Convert.ToString(dr["PostIP"]);
                mdl.Posttime = Convert.ToDateTime(dr["Posttime"]);
                mdl.Userid = Convert.ToInt32(dr["Userid"]);
            }
            else
            {
                dr.Close();
                return null;
            }
            dr.Close();
            return mdl;
        }

        public PhotoInfo GetInfo(object PhotoID)
        {
            PhotoInfo pInfo = new PhotoInfo();
            string sql = "select  a.id, a.AlbumID, a.UserID, a.Description, a.Views, a.FileSize, a.State, a.IsCover, a.Comments, a.PostTime, a.PostIP, a.PhotoType, a.Width, a.Height, a.FilePath, a.ShareNumber, a.IsLock,b.truename from NT_photo AS a INNER JOIN NT_User AS b on a.userid=b.userid  where a.id=" + PhotoID + "";
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
                pInfo.TrueName = rd["TrueName"].ToString();
                pInfo.PostTime = Convert.ToDateTime(rd["PostTime"]);
                pInfo.State = Convert.ToInt32(rd["State"]);
                pInfo.UserID = Convert.ToInt32(rd["UserID"]);
                pInfo.Views = Convert.ToInt32(rd["Views"]);
                pInfo.Width = Convert.ToInt32(rd["Width"]);
                pInfo.ShareNumber = Convert.ToInt32(rd["ShareNumber"]);
                pInfo.IsLock = Convert.ToBoolean(rd["IsLock"]);
                pInfo.Id = Convert.ToInt32(rd["Id"]);
            }
            rd.Close();
            return pInfo;
        }

        public AtiveClassInfo GetAtiveClassInfo(object aid)
        {
            AtiveClassInfo pInfo = new AtiveClassInfo();
            string sql = "select  * from NT_AtiveClass where id=" + aid + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                pInfo.Id = Convert.ToInt32(rd["Id"]);
                pInfo.ParentID = Convert.ToInt32(rd["ParentID"]);
                pInfo.ClassName = rd["ClassName"].ToString();
            }
            rd.Close();
            return pInfo;
        }

        public int InsetAtiveClass(AtiveClassInfo info)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Id", SqlDbType.Int);
            param[0].Value = info.Id;
            param[1] = new SqlParameter("@ParentID", SqlDbType.Int);
            param[1].Value = info.ParentID;
            param[2] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 30);
            param[2].Value = info.ClassName;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update NT_AtiveClass set ParentID=@ParentID,ClassName=@ClassName where id=@Id";
            }
            else
            {
                sql = "insert into NT_AtiveClass(ParentID,ClassName)values(@ParentID,@ClassName)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }
    }
}