using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Profile;
using JuSNS.Model;
using JuSNS.Config;

namespace JuSNS.SQLServer.App
{
    public class Group : DbBase, IGroup
    {
        public string GetClassName(int classid)
        {
            string sql = "select className from nt_groupclass where id=" + classid;
            object name = DbHelper.ExecuteScalar(CommandType.Text, sql, null);
            if (name == null)
                return string.Empty;
            else
                return name.ToString();
        }

        public List<GroupClassInfo> GetClassList(int parentid)
        {
            List<GroupClassInfo> infolist = new List<GroupClassInfo>();
            string sql = "select id,parentid,classname,iscreat from nt_groupclass where parentid=" + parentid + " order by id asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                GroupClassInfo info = new GroupClassInfo();
                info.ID = Convert.ToInt32(rd["id"]);
                info.Parentid = Convert.ToInt32(rd["Parentid"]);
                info.ClassName = rd["ClassName"].ToString();
                info.IsCreat = Convert.ToBoolean(rd["IsCreat"]);
                infolist.Add(info);
            }
            rd.Close(); rd.Dispose();
            return infolist;
        }

        public List<GroupTopicInfo> GetGroupTopicList(int number, int gid)
        {
            List<GroupTopicInfo> infolist = new List<GroupTopicInfo>();
            string sql = "select top " + number + " a.id, a.groupid, a.UserID, a.title, a.content, a.posttime, a.TopicID, a.isTop, a.lastpostTime, a.Replynumber,a.IsLock, a.Clicks,a.isbest,b.TrueName from nt_grouptopic AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.topicid=0 and a.islock=0 and a.groupid="+gid+" order by a.istop desc,a.isbest desc,a.lastpostTime desc,a.posttime desc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                GroupTopicInfo info = new GroupTopicInfo();
                info.Clicks = Convert.ToInt32(rd["Clicks"]);
                info.Content = Convert.ToString(rd["Content"]);
                info.TrueName = Convert.ToString(rd["TrueName"]);
                info.IsLock = Convert.ToByte(rd["IsLock"]);
                info.IsBest = Convert.ToByte(rd["IsBest"]);
                info.Groupid = Convert.ToInt32(rd["Groupid"]);
                info.Id = Convert.ToInt32(rd["Id"]);
                info.IsTop = Convert.ToBoolean(rd["IsTop"]);
                if (rd["LastpostTime"] != DBNull.Value) info.LastpostTime = Convert.ToDateTime(rd["LastpostTime"]);
                info.Posttime = Convert.ToDateTime(rd["Posttime"]);
                info.Replynumber = Convert.ToInt32(rd["Replynumber"]);
                info.Title = Convert.ToString(rd["Title"]);
                info.TopicID = Convert.ToInt32(rd["TopicID"]);
                info.UserID = Convert.ToInt32(rd["UserID"]);
                infolist.Add(info);
            }
            rd.Close(); rd.Dispose();
            return infolist;
        }

        public List<GroupMemberInfo> GetGroupMemberList(int number, int gid, int flag)
        {
            List<GroupMemberInfo> infolist = new List<GroupMemberInfo>();
            string sql = string.Empty;
            sql += "select top " + number + " a.ID, a.UserID, a.GroupID, a.JoinTime, a.Grade, a.Islock, b.TrueName from nt_groupmember AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.groupid=" + gid + " and a.islock=0";
            if (flag == 0)
            {
                sql += " ORDER BY a.Grade DESC, a.JoinTime DESC";
            }
            else
            {
                sql += " ORDER BY a.id desc";
            }
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                GroupMemberInfo info = new GroupMemberInfo();
                info.Grade = Convert.ToInt32(rd["Grade"]);
                info.GroupID = Convert.ToInt32(rd["GroupID"]);
                info.ID = Convert.ToInt32(rd["ID"]);
                info.Islock = Convert.ToBoolean(rd["Islock"]);
                info.JoinTime = Convert.ToDateTime(rd["JoinTime"]);
                info.TrueName = Convert.ToString(rd["TrueName"]);
                info.UserID = Convert.ToInt32(rd["UserID"]);
                infolist.Add(info);
            }
            rd.Close(); rd.Dispose();
            return infolist;
        }

        public bool IsJoinGroup(int groupid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "select userid from nt_group where id=" + groupid;
                int uid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, null));
                if (uid == userid) return true;
                sql = "select count(id) from nt_groupmember where groupid=" + groupid + " and islock=0 and userid=" + userid;
                object result = DbHelper.ExecuteScalar(cn,CommandType.Text, sql, null);
                return Convert.ToInt32(result) > 0 ? true : false;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int OutGroup(int groupid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "select userid from nt_group where id=" + groupid;
                int uid = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
                if (uid == userid) return 2;
                sql = "delete from nt_groupmember where groupid=" + groupid + " and userid=" + userid;
                int n= DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                sql = "update nt_group set Members=Members-1 where id=" + groupid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public string GetJoinGroup(int userid)
        {
            string resultSTR = "0";
            string sql = "select groupid from NT_GroupMember where userid=" + userid + " and islock=0 order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                resultSTR += "," + dr["groupid"];
            }
            dr.Close();
            return resultSTR;
        }

        public int DeleteGroup(int groupid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetGroupInfo(groupid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                {
                    sql = "delete from nt_group where id=" + groupid;
                }
                else
                {
                    sql = "delete from nt_group where id=" + groupid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "delete from nt_groupmember where groupid=" + groupid + "", null);
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(9) * downinter), 0, 1, "删除群组");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int SetGroupTop(int tid, int userid,int flag)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int gid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select groupid from nt_grouptopic where id=" + tid, null));
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid) || isGroupAdmin(gid, userid))
                {
                    sql = "update nt_grouptopic set isTop=" + flag + " where id=" + tid;
                }
                else
                {
                    sql = "update nt_grouptopic set isTop=" + flag + " where id=" + tid + " and UserID=" + userid;
                }
                return  DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int SetGroupBest(int tid, int userid, int flag)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int gid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select groupid from nt_grouptopic where id=" + tid, null));
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid) || isGroupAdmin(gid, userid))
                {
                    sql = "update nt_grouptopic set isbest=" + flag + " where id=" + tid;
                }
                else
                {
                    sql = "update nt_grouptopic set isbest=" + flag + " where id=" + tid + " and UserID=" + userid;
                }
                return DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteGroupFile(int tid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int gid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select groupid from nt_files where id=" + tid, null));
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid) || isGroupAdmin(gid, userid))
                {
                    sql = "delete from nt_files where id=" + tid;
                }
                else
                {
                    sql = "delete from nt_files where id=" + tid + " and UserID=" + userid;
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

        public int DeleteGroupTopic(int tid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int gid = Convert.ToInt32(DbHelper.ExecuteScalar(cn,CommandType.Text, "select groupid from nt_grouptopic where id=" + tid, null));
                int getuserid = GetTopicInfo(tid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid) || isGroupAdmin(gid, userid))
                {
                    sql = "delete from nt_grouptopic where id=" + tid;
                }
                else
                {
                    sql = "delete from nt_grouptopic where id=" + tid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, "delete from nt_grouptopic where TopicID=" + tid + "", null);
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(10) * downinter), 0, 1, "删除话题");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteGroupClass(int cid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int n = 0;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                {
                    n = DbHelper.ExecuteNonQuery(CommandType.Text, "delete from NT_GroupClass where id="+cid+"", null);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int JoinGroup(int groupid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int isCheck = 0;
                int p = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select Publics from nt_group where id=" + groupid, null));
                switch ((EnumPublics)p)
                {
                    case EnumPublics.Checked:
                        isCheck = 1;
                        break;
                    case EnumPublics.None:
                        return 2;
                }
                //获得群人数
                int groupcount = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(id) from nt_groupmember where groupid=" + groupid + " and islock=0", null));
                if (groupcount >= Convert.ToInt32(JuSNS.Common.Public.GetXMLGroupValue("groupMember")))
                    return 4;
                int m = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(id) from nt_groupmember where userid=" + userid + " and groupid=" + groupid, null));
                if (m > 0) return -1;
                string sql = "insert into nt_groupmember(UserID,GroupID,JoinTime,Grade,Islock)values(" + userid + "," + groupid + ",'" + DateTime.Now + "',0," + isCheck + ")";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0 && isCheck == 0)
                {
                    sql = "update nt_group set Members=Members+1 where id=" + groupid;
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    //这里为管理员或创建者插入通知信息。
                    return 0;
                }
                if (isCheck == 1)
                {
                    return 1;
                }
                return 3;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InviteFriend(int userid, int reciveid, int gid)
        {
            //return 0;
            //插入用户的通知
            string sql = "insert into NT_GroupInvite(GroupID, ReviceID, UserID, [Content], PostTime, IsAccept)values(" + gid + "," + reciveid + "," + userid + ",'邀请加入群','" + DateTime.Now + "',0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertandUpdate(GroupInfo info, out int gid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {

                SqlParameter[] param = GetParameters(info);
                string sql = string.Empty;
                int n = 0;
                if (info.Id > 0)
                {
                    sql = "update nt_group set GroupName=@GroupName,  Bulletin=@Bulletin, CityID=@CityID, Privacy=@Privacy, Publics=@Publics, Portrait=@Portrait, ClassID=@ClassID, skinDir=@skinDir,Islight=@Islight where ID=@Id AND UserID=@UserID";
                    n = DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, param);
                }
                else
                {
                    sql = "insert into nt_group (UserID, GroupName, Members, Bulletin, CityID, Privacy, Publics, Portrait, IsLock, PostTime, PostIP, isRec, ClassID, skinDir, Click, Islight) VALUES";
                    sql += "(@UserID, @GroupName, @Members, @Bulletin, @CityID, @Privacy, @Publics, @Portrait, @IsLock, @PostTime, @PostIP, @isRec, @ClassID, @skinDir, @Click, @Islight);";
                    sql += "Select SCOPE_IDENTITY()";
                    n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                    //插入群组会员
                    sql = "insert into nt_groupmember(UserID,GroupID,JoinTime,Grade,Islock)values(" + info.UserID + "," + n + ",'" + DateTime.Now + "',2,0)";
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }
                if (info.Id > 0) gid = info.Id; else gid = n;
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        public GroupInfo GetGroupInfo(object gid)
        {
            GroupInfo Info = new GroupInfo();
            string sql = "select id, UserID, GroupName, Members, Bulletin, CityID, Privacy, Publics, Portrait, IsLock, PostTime, PostIP, isRec, ClassID, skinDir, Click, Islight from NT_Group where id=" + gid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                Info.Bulletin = dr["Bulletin"].ToString();
                Info.CityID = Convert.ToInt32(dr["CityID"]);
                Info.ClassID = Convert.ToInt32(dr["ClassID"]);
                Info.Click = Convert.ToInt32(dr["Click"]);
                Info.GroupName = Convert.ToString(dr["GroupName"]);
                Info.Id = Convert.ToInt32(dr["Id"]);
                Info.Islight = Convert.ToBoolean(dr["Islight"]);
                Info.IsLock = Convert.ToBoolean(dr["IsLock"]);
                Info.IsRec = Convert.ToByte(dr["IsRec"]);
                Info.Members = Convert.ToInt32(dr["Members"]);
                Info.Portrait = Convert.ToString(dr["Portrait"]);
                Info.PostIP = Convert.ToString(dr["PostIP"]);
                Info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                Info.Privacy = Convert.ToInt32(dr["Privacy"]);
                Info.Publics = Convert.ToInt32(dr["Publics"]);
                Info.SkinDir = Convert.ToString(dr["SkinDir"]);
                Info.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return Info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public GroupClassInfo GetGroupClassInfo(int cid)
        {
            GroupClassInfo Info = new GroupClassInfo();
            string sql = "select id, className,Parentid from NT_GroupClass where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                Info.ID = cid;
                Info.Parentid = Convert.ToInt32(dr["Parentid"]);
                Info.ClassName = dr["ClassName"].ToString();
                dr.Close();
                return Info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InsertGroupClass(GroupClassInfo info)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 30);
            param[0].Value = info.ClassName;
            param[1] = new SqlParameter("@ID", SqlDbType.Int);
            param[1].Value = info.ID;
            param[2] = new SqlParameter("@IsCreat", SqlDbType.Bit);
            param[2].Value = info.IsCreat;
            param[3] = new SqlParameter("@Parentid", SqlDbType.Int);
            param[3].Value = info.Parentid;
            string sql = string.Empty;
            if (info.ID > 0)
            {
                sql = "update NT_GroupClass set ClassName=@ClassName, Parentid=@Parentid, IsCreat=@IsCreat where id=@ID";
            }
            else
            {
                sql = "insert into NT_GroupClass(className, parentid, isCreat)values(@ClassName, @Parentid, @IsCreat)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public FilesInfo GetFileInfo(object fid)
        {
            FilesInfo Info = new FilesInfo();
            string sql = "select * from NT_Files where id=" + fid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                Info.DownNumber = Convert.ToInt32(dr["DownNumber"]);
                Info.FileName = Convert.ToString(dr["FileName"]);
                Info.FileSize = Convert.ToInt32(dr["FileSize"]);
                Info.GroupID = Convert.ToInt32(dr["GroupID"]);
                Info.Id = Convert.ToInt32(dr["Id"]);
                Info.PostIP = Convert.ToString(dr["PostIP"]);
                Info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                Info.Title = Convert.ToString(dr["Title"]);
                Info.UserID = Convert.ToInt32(dr["UserID"]);
            }
            dr.Close();
            return Info;
        }

        public GroupTopicInfo GetTopicInfo(object tid)
        {
            GroupTopicInfo Info = new GroupTopicInfo();
            string sql = "select a.id, a.groupid, a.UserID, a.title, a.[content], a.posttime, a.TopicID, a.isTop, a.lastpostTime, a.Replynumber, a.Clicks, a.IsLock, a.IsBest, a.PostIP, b.TrueName from NT_GroupTopic AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID where a.id=" + tid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                Info.Clicks = Convert.ToInt32(dr["Clicks"]);
                Info.Content = Convert.ToString(dr["Content"]);
                Info.Groupid = Convert.ToInt32(dr["Groupid"]);
                Info.Id = Convert.ToInt32(tid);
                Info.IsBest = Convert.ToByte(dr["IsBest"]);
                Info.IsLock = Convert.ToByte(dr["IsLock"]);
                Info.IsTop = Convert.ToBoolean(dr["IsTop"]);
                if (dr["LastpostTime"] != DBNull.Value) Info.LastpostTime = Convert.ToDateTime(dr["LastpostTime"]);
                Info.Posttime = Convert.ToDateTime(dr["Posttime"]);
                Info.Replynumber = Convert.ToInt32(dr["Replynumber"]);
                Info.Title = Convert.ToString(dr["Title"]);
                Info.TopicID = Convert.ToInt32(dr["TopicID"]);
                Info.TrueName = Convert.ToString(dr["TrueName"]);
                Info.PostIP = Convert.ToString(dr["PostIP"]);
                Info.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return Info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int UpdateTopicContent(GroupTopicInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = GetTopicParameters(info);
                string sql = "update NT_GroupTopic set Content=@Content where id=@Id";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertTopic(GroupTopicInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = GetTopicParameters(info);
                //得到楼层
                int fnumber = 0;
                string sql = "select max(FoolNumber) as fnumber from NT_GroupTopic where topicid=@TopicID";
                try
                {
                    fnumber = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                }
                catch { };
                sql = "insert into NT_GroupTopic(groupid, UserID, title, [content], posttime, TopicID, isTop, lastpostTime, Replynumber, Clicks, IsLock, IsBest, PostIP, FoolNumber)";
                sql += "values(@Groupid,@UserID, @Title, @Content, @Posttime, @TopicID, @IsTop, @LastpostTime, @Replynumber, @Clicks, @IsLock, @IsBest, @PostIP, " + (fnumber + 1) + ");Select SCOPE_IDENTITY()";
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                //更新回复数
                if (n > 0&&info.TopicID>0)
                {
                    sql = "update NT_GroupTopic set Replynumber=Replynumber+1,lastpostTime=@LastpostTime where id=@TopicID";
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                }
                //增加积分
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        public void UpdateTopicClicks(int tid)
        {
            string sql = "update NT_GroupTopic set Clicks=Clicks+1 where id=" + tid;
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int GetGroupTopicCount(int gid, int flag)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                //更新群点击率
                sql = "update nt_group set click=click+1 where id=" + gid;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                switch (flag)
                {
                    case 0:
                        sql = "select count(id) from nt_GroupTopic where groupid=" + gid;
                        break;
                    case 1:
                        sql = "select count(id) from nt_GroupTopic where TopicID=0 and groupid=" + gid;
                        break;
                    case 2:
                        sql = "select count(id) from nt_GroupTopic where topicid>0 and groupid=" + gid;
                        break;
                }
                return Convert.ToInt32(DbHelper.ExecuteScalar(cn,CommandType.Text, sql, null));
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        public int GetGroupAlbumCount(int gid)
        {
            string sql = "select count(AlbumID) from nt_album where groupid=" + gid;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public int GetGroupFilesCount(int gid)
        {
            string sql = "select count(id) from nt_files where groupid=" + gid;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public int GetGroupMemberCount(int gid)
        {
            string sql = "select count(a.id) from nt_groupmember AS a inner Join NT_User AS b on a.userid=b.userid where a.groupid=" + gid+" and a.islock=0";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public int GetGroupAtiveCount(int gid)
        {
            return 0;
            //string sql = "select count(id) from nt_ative where groupid=" + gid;
            //return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public bool isGroupAdmin(int gid, int userid)
        {
            string sql = "select count(id) from nt_groupmember where groupid=" + gid + " and userid=" + userid + " and (grade=1 or grade=2)";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }

        public bool isGroupSuperAdmin(int gid, int userid)
        {
            string sql = "select count(id) from nt_groupmember where groupid=" + gid + " and userid=" + userid + " and (grade=2)";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }

        public int GetMaxTopicForUser(int gid, int userid)
        {
            try
            {
                string sql = "select max(id) from NT_GroupTopic where userid=" + userid + " and groupid=" + gid + "";
                return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
            }
            catch
            {
                return 0;
            }
        }

        public int GetFilesSize(int gid)
        {
            try
            {
                string sql = "select sum(filesize) from nt_files where groupid=" + gid;
                return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
            }
            catch { return 0; }
        }

        public int CheckGroupMember(int gid,int userid, int uid, int flag)
        {
            string sql = string.Empty;
            if (isGroupAdmin(gid, uid))
            {
                if (flag == 0)
                {
                    sql = "update nt_groupmember set islock=0 where groupid=" + gid + " and userid=" + userid + "";
                }
                else
                {
                    sql = "delete from nt_groupmember where groupid=" + gid + " and userid=" + userid + "";
                }
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return -1;
            }
        }
        public int DeleteGroupMember(int infoid, int uid)
        {
            string sql = string.Empty;
            sql = "delete from nt_groupmember where groupid=" + infoid + " and userid=" + uid + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int SetGroupAdmin(int gid, int userid, int uid, int flag)
        {
            string sql = string.Empty;
            if (isGroupSuperAdmin(gid, uid))
            {
                if (flag == 1)
                {
                    sql = "update nt_groupmember set grade=1 where groupid=" + gid + " and userid=" + userid + "";
                }
                else
                {
                    sql = "update nt_groupmember set grade=0 where groupid=" + gid + " and userid=" + userid + "";
                }
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int InsertFiles(FilesInfo info)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@DownNumber", SqlDbType.Int);
            param[0].Value = info.DownNumber;
            param[1] = new SqlParameter("@FileName", SqlDbType.NVarChar,30);
            param[1].Value = info.FileName;
            param[2] = new SqlParameter("@FileSize", SqlDbType.Int);
            param[2].Value = info.FileSize;
            param[3] = new SqlParameter("@GroupID", SqlDbType.Int);
            param[3].Value = info.GroupID;
            param[4] = new SqlParameter("@Id", SqlDbType.Int);
            param[4].Value = info.Id;
            param[5] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
            param[5].Value = info.PostIP;
            param[6] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[6].Value = info.PostTime;
            param[7] = new SqlParameter("@Title", SqlDbType.NVarChar,50);
            param[7].Value = info.Title;
            param[8] = new SqlParameter("@UserID", SqlDbType.Int);
            param[8].Value = info.UserID;
            string sql = "insert into nt_files(UserID, title, GroupID, FileName, FileSize, PostIP, PostTime, DownNumber)values(@UserID, @Title, @GroupID, @FileName, @FileSize, @PostIP, @PostTime, @DownNumber)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public string GetMemberList(int gid)
        {
            string listSTR = string.Empty;
            string sql = "select userid from NT_GroupMember where groupid=" + gid + " and islock=0";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while(dr.Read())
            {
                listSTR += dr["userid"] + ",";
            }
            dr.Close();
            return listSTR;
        }

        /// <summary>
        /// 构造函数(群组)
        /// </summary>
        /// <param name="Info">群组实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetParameters(GroupInfo Info)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@Bulletin", SqlDbType.NVarChar, 255);
            param[0].Value = Info.Bulletin;
            param[1] = new SqlParameter("@CityID", SqlDbType.Int);
            param[1].Value = Info.CityID;
            param[2] = new SqlParameter("@ClassID", SqlDbType.Int);
            param[2].Value = Info.ClassID;
            param[3] = new SqlParameter("@Click", SqlDbType.Int);
            param[3].Value = Info.Click;
            param[4] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 30);
            param[4].Value = Info.GroupName;
            param[5] = new SqlParameter("@Id", SqlDbType.Int);
            param[5].Value = Info.Id;
            param[6] = new SqlParameter("@Islight", SqlDbType.Bit);
            param[6].Value = Info.Islight;
            param[7] = new SqlParameter("@IsLock", SqlDbType.Bit);
            param[7].Value = Info.IsLock;
            param[8] = new SqlParameter("@IsRec", SqlDbType.TinyInt);
            param[8].Value = Info.IsRec;
            param[9] = new SqlParameter("@Members", SqlDbType.Int);
            param[9].Value = Info.Members;
            param[10] = new SqlParameter("@Portrait", SqlDbType.NVarChar,30);
            param[10].Value = Info.Portrait;
            param[11] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
            param[11].Value = Info.PostIP;
            param[12] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[12].Value = Info.PostTime;
            param[13] = new SqlParameter("@Privacy", SqlDbType.Int);
            param[13].Value = Info.Privacy;
            param[14] = new SqlParameter("@Publics", SqlDbType.Int);
            param[14].Value = Info.Publics;
            param[15] = new SqlParameter("@SkinDir", SqlDbType.NVarChar,30);
            param[15].Value = Info.SkinDir;
            param[16] = new SqlParameter("@UserID", SqlDbType.Int);
            param[16].Value = Info.UserID;
            return param;
        }

        /// <summary>
        /// 构造函数（群组话题）
        /// </summary>
        /// <param name="Info">群组实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetTopicParameters(GroupTopicInfo info)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Clicks", SqlDbType.Int);
            param[0].Value = info.Clicks;
            param[1] = new SqlParameter("@Content", SqlDbType.NText);
            param[1].Value = info.Content;
            param[2] = new SqlParameter("@Groupid", SqlDbType.Int);
            param[2].Value = info.Groupid;
            param[3] = new SqlParameter("@IsBest", SqlDbType.TinyInt);
            param[3].Value = info.IsBest;
            param[4] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[4].Value = info.IsLock;
            param[5] = new SqlParameter("@IsTop", SqlDbType.Bit);
            param[5].Value = info.IsTop;
            param[6] = new SqlParameter("@LastpostTime", SqlDbType.DateTime);
            param[6].Value = info.LastpostTime;
            param[7] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
            param[7].Value = info.PostIP;
            param[8] = new SqlParameter("@Posttime", SqlDbType.DateTime);
            param[8].Value = info.Posttime;
            param[9] = new SqlParameter("@Replynumber", SqlDbType.Int);
            param[9].Value = info.Replynumber;
            param[10] = new SqlParameter("@Title", SqlDbType.NVarChar,80);
            param[10].Value = info.Title;
            param[11] = new SqlParameter("@TopicID", SqlDbType.Int);
            param[11].Value = info.TopicID;
            param[12] = new SqlParameter("@UserID", SqlDbType.Int);
            param[12].Value = info.UserID;
            param[13] = new SqlParameter("@Id", SqlDbType.Int);
            param[13].Value = info.Id;

            return param;
        }
    }
}
