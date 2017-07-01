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
    public class Vote : DbBase, IVote
    {

        /// <summary>
        /// 选项列表
        /// </summary>
        /// <param name="VoteID">投票编号</param>
        /// <returns>返回列表</returns>
        public List<VoteOptionInfo> OptionList(int vid)
        {
            List<VoteOptionInfo> list = new List<VoteOptionInfo>();
            string Sql = "Select [ID],[VoteID],[OptionName],[Cnt] From [NT_VoteOption] Where [VoteID]=" + vid;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                VoteOptionInfo info = new VoteOptionInfo();
                info.ID = Convert.ToInt32(rd["ID"].ToString());
                info.VoteID = Convert.ToInt32(rd["VoteID"].ToString());
                if (rd["OptionName"] != DBNull.Value) info.OptionName = rd["OptionName"].ToString();
                info.Cnt = Convert.ToInt32(rd["Cnt"].ToString());
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        /// <summary>
        /// 取得用户投票选项
        /// </summary>
        /// <param name="vid">投票编号</param>
        /// <param name="userid">用户编号</param>
        /// <param name="tf">如果用户已投票返回true,否则返回false</param>
        /// <returns>返回列表</returns>
        public List<VoteOptionInfo> OptionList(int vid, int userid,out bool tf)
        {
            List<VoteOptionInfo> list = new List<VoteOptionInfo>();
            tf = false;
            string Sql = "Select Count([ID]) From [NT_VoteTo] Where [VoteID]=" + vid + " And [UserID]=" + userid + " And [OptionID] is Not  Null";
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
            if (n > 0)
            {
                tf = true;
                Sql = "Select a.[ID],a.[VoteID],[OptionName],[Cnt] From " +
                      "[NT_VoteOption] as a inner join [NT_VoteTo] as b " +
                      "on charindex(','+cast(a.id as varchar(20))+',', ','+b.OptionID+',')>0 " +
                      "and b.[VoteID]=" + vid + " And b.[UserID]=" + userid;
            }
            else
            {
                Sql = "Select [ID],[VoteID],[OptionName],[Cnt] From [NT_VoteOption] Where [VoteID]=" + vid;
            }
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                VoteOptionInfo info = new VoteOptionInfo();
                info.ID = Convert.ToInt32(rd["ID"].ToString());
                info.VoteID = Convert.ToInt32(rd["VoteID"].ToString());
                if (rd["OptionName"] != DBNull.Value) info.OptionName = rd["OptionName"].ToString();
                info.Cnt = Convert.ToInt32(rd["Cnt"].ToString());
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public VoteInfo GetVoteInfo(object vid)
        {
            VoteInfo mdl = new VoteInfo();
            string sql = "select id, UserID, Title, [Content], PostTime, Mode, EndTime, JCnt, VCnt, IsFriend from nt_vote where id=" + vid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.Content = Convert.ToString(dr["content"]);
                mdl.EndTime = Convert.ToDateTime(dr["EndTime"]);
                mdl.Id = Convert.ToInt32(vid);
                mdl.IsFriend = Convert.ToByte(dr["IsFriend"]);
                mdl.JCnt = Convert.ToInt32(dr["JCnt"]);
                mdl.Mode = Convert.ToByte(dr["Mode"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.Title = Convert.ToString(dr["Title"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                mdl.VCnt = Convert.ToInt32(dr["VCnt"]);
            }
            dr.Close();
            return mdl;
        }

        public VoteToInfo GetVoteToInfo(object vid, object uid)
        {
            VoteToInfo mdl = new VoteToInfo();
            string sql = "select id, content, PostTime, UserID, VoteID from nt_voteTo where voteid=" + vid+" AND userid="+uid+"";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.Content = Convert.ToString(dr["content"]);
                mdl.ID = Convert.ToInt32(dr["ID"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                mdl.VoteID = Convert.ToInt32(dr["VoteID"]);
            }
            dr.Close();
            return mdl;
        }

        /// <summary>
        /// 取得用户是否已投票
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="vid">投票编号</param>
        /// <returns>如果已投票返回true,否则false</returns>
        public bool IsVote(int userid, int vid)
        {
            bool tf = false;
            string Sql = "Select Count([ID]) From [NT_VoteTo] Where [UserID]=" + userid + " And [VoteID]=" + vid + " And [OptionID] is Not Null";
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
            if (n > 0)
                tf = true;
            return tf;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">投票选项实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetParameters(VoteToInfo info)
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = info.ID;
            param[1] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            param[1].Value = info.UserID;
            param[2] = new SqlParameter("@VoteID", SqlDbType.Int, 4);
            param[2].Value = info.VoteID;

            param[3] = new SqlParameter("@OptionID", SqlDbType.NVarChar, 1000);
            param[3].Value = info.OptionID;
            param[4] = new SqlParameter("@Content", SqlDbType.NVarChar, 200);
            param[4].Value = info.Content;

            return param;
        }

        public int Add(VoteToInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = GetParameters(info);
                //检测是否已投过票，投过票直接返回-1
                string Sql = "Select Count([ID]) From [NT_VoteTo] Where [UserID]=@UserID And [VoteID]=@VoteID";
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
                if (n == 0)
                {
                    if (info.OptionID != "" && info.Content != "")
                    {
                        Sql = "Insert Into [NT_VoteTo]([UserID],[VoteID],[OptionID],[Content],PostTime) Values(@UserID,@VoteID,@OptionID,@Content,'" + DateTime.Now + "')";
                        n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));

                        Sql = "Update [NT_Vote] Set [JCnt]=[JCnt]+1,[Vcnt]=[Vcnt]+1 Where ID=@VoteID";
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);

                        //增加投票选项数量
                        Sql = "Update [Nt_VoteOption] Set [Cnt]=[Cnt]+1 Where [ID] In(" + info.OptionID + ")";
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                    }
                    //检测用户是否已评论过此投票，如果投票过直接更新投票数,否则同时更新投票数与参与人数
                    else if (!string.IsNullOrEmpty(info.OptionID))
                    {
                        Sql = "Insert Into [NT_VoteTo]([UserID],[VoteID],[OptionID],PostTime) Values(@UserID,@VoteID,@OptionID,'" + DateTime.Now + "')";
                        n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));

                        Sql = "Update [NT_Vote] Set [JCnt]=[JCnt]+1,[Vcnt]=[Vcnt]+1 Where ID=@VoteID";
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);

                        //增加投票选项数量
                        Sql = "Update [Nt_VoteOption] Set [Cnt]=[Cnt]+1 Where [ID] In(" + info.OptionID + ")";
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                    }
                    else
                    {
                        Sql = "Insert Into [NT_VoteTo]([UserID],[VoteID],[Content],PostTime) Values(@UserID,@VoteID,@Content,'" + DateTime.Now + "')";
                        n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));

                        Sql = "Update [NT_Vote] Set [JCnt]=[JCnt]+1 Where ID=@VoteID";
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                    }

                    //取得投票发起者编号
                    //Sql = "Select [UserID] From [NT_Vote] Where [ID]=" + info.VoteID;
                    //int suid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null));
                    //JUser ju = new JUser();
                    //if (ju.CheckPrivacyInfo(trans, Info.UserID, "ActMovieComment"))
                    //{
                    //    UserDyn(trans, 0, 0, "joinvote", Info.UserID, 0, Info.VoteID, "参与了一个投票", Info.VoteID.ToString());
                    //}
                    //if (suid != Info.UserID)
                    //{
                    //    Notify(trans, EnumNotifyType.ToVote, Info.UserID, suid, "参与了一个投票", Info.VoteID);
                    //}
                    //                    Notify(trans, EnumNotifyType.VoteComment, Info.UserID, uid, "回复了评论", id);
                }
                else
                {
                    Sql = "Select Count([ID]) From [NT_VoteTo] Where [UserID]=@UserID And [VoteID]=@VoteID And [OptionID] is Not Null";
                    int k = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
                    if (k > 0)
                    {
                        Sql = "Update [NT_VoteTo] Set [Content]=@Content Where [UserID]=@UserID And [VoteID]=@VoteID";
                        n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));
                    }
                    else
                    {
                        if (info.OptionID != "" && info.Content != "")
                        {
                            Sql = "Update [NT_VoteTo] Set [OptionID]=@OptionID,[Content]=@Content Where [UserID]=@UserID And [VoteID]=@VoteID";
                            n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));

                            Sql = "Update [NT_Vote] Set [Vcnt]=[Vcnt]+1 Where ID=@VoteID";
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);

                            //增加投票选项数量
                            Sql = "Update [Nt_VoteOption] Set [Cnt]=[Cnt]+1 Where [ID] In(" + info.OptionID + ")";
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                        }
                        //检测用户是否已评论过此投票，如果投票过直接更新投票数,否则同时更新投票数与参与人数
                        else if (info.OptionID != "")
                        {
                            Sql = "Update [NT_VoteTo] Set [OptionID]=@OptionID Where [UserID]=@UserID And [VoteID]=@VoteID";
                            n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));

                            Sql = "Update [NT_Vote] Set [Vcnt]=[Vcnt]+1 Where ID=@VoteID";
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);

                            //增加投票选项数量
                            Sql = "Update [Nt_VoteOption] Set [Cnt]=[Cnt]+1 Where [ID] In(" + info.OptionID + ")";
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                        }
                        else
                        {
                            Sql = "Update [NT_VoteTo] Set [Content]=@Content Where [UserID]=@UserID And [VoteID]=@VoteID";
                            n = Convert.ToInt32(DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param));

                            Sql = "Update [NT_Vote] Set [JCnt]=[JCnt]+1 Where ID=@VoteID";
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                        }
                    }
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteVote(int vid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid,cn))
                {
                    sql = "delete from nt_vote where ID=" + vid;
                }
                else
                {
                    sql = "delete from nt_vote where ID=" + vid + " and UserID=" + uid;
                }
                int getuserid=GetVoteInfo(vid).UserID;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    sql += "delete from NT_VoteOption where VoteID=" + vid + ";";
                    sql += "delete from NT_VoteTo where VoteID=" + vid + ";";
                    DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(29) * downinter), 0, 1, "删除投票");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteVoteTo(int vid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid))
                {
                    sql = "delete from nt_voteto where ID=" + vid;
                }
                else
                {
                    sql = "delete from nt_voteto where ID=" + vid + " and UserID=" + uid;
                }
                int n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int AddVote(VoteInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = GetVoteParameters(info);
                string Sql = "Insert Into [NT_Vote]([Title],[Content],[UserID],[PostTime],[Mode],[EndTime],[JCnt],[VCnt],[IsFriend]) " +
                             "Values(@Title,@Intro,@UserID,@AddTime,@Mode,@EndTime,@JCnt,@VCnt,@IsFriend);" +
                             "Select SCOPE_IDENTITY();";
                int id = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
                //JUser ju = new JUser();
                //if (ju.CheckPrivacyInfo(trans, Info.UserID, "ActMovieComment"))
                //{
                //    UserDyn(trans, 0, 0, "addvote", Info.UserID, 0, id, "添加了一个投票", id.ToString());
                //}
                //trans.Commit();

                return id;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">投票实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetVoteParameters(VoteInfo Info)
        {
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = Info.Id;
            param[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 60);
            param[1].Value = Info.Title;
            param[2] = new SqlParameter("@Intro", SqlDbType.NText);
            param[2].Value = Info.Content;

            param[3] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            param[3].Value = Info.UserID;
            param[4] = new SqlParameter("@AddTime", SqlDbType.DateTime, 8);
            param[4].Value = Info.PostTime;
            param[5] = new SqlParameter("@Mode", SqlDbType.Int, 4);
            param[5].Value = Info.Mode;

            param[6] = new SqlParameter("@EndTime", SqlDbType.DateTime, 8);
            param[6].Value = Info.EndTime;
            param[7] = new SqlParameter("@JCnt", SqlDbType.Int, 4);
            param[7].Value = Info.JCnt;
            param[8] = new SqlParameter("@VCnt", SqlDbType.Int, 4);
            param[8].Value = Info.VCnt;

            param[9] = new SqlParameter("@IsFriend", SqlDbType.Int, 4);
            param[9].Value = Info.IsFriend;
            return param;
        }

        public int AddOption(VoteOptionInfo info)
        {
            SqlParameter[] param = GetOptionParameters(info);
            string Sql = "Insert Into [NT_VoteOption]([VoteID],[OptionName],[Cnt]) Values(@VoteID,@OptionName,@Cnt)";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">投票选项实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetOptionParameters(VoteOptionInfo Info)
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = Info.ID;
            param[1] = new SqlParameter("@VoteID", SqlDbType.Int, 4);
            param[1].Value = Info.VoteID;
            param[2] = new SqlParameter("@OptionName", SqlDbType.NVarChar, 100);
            param[2].Value = Info.OptionName;

            param[3] = new SqlParameter("@Cnt", SqlDbType.Int, 4);
            param[3].Value = Info.Cnt;

            return param;
        }
    }
}
