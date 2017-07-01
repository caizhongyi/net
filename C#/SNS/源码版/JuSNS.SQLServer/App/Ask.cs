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
    public class Ask : DbBase, IAsk
    {
        public List<AskClassInfo> GetAskClass(int ParentID)
        {
            List<AskClassInfo> infolist = new List<AskClassInfo>();
            string sql = "select id,ClassName,ParentID from nt_askclass where parentid=" + ParentID;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AskClassInfo info = new AskClassInfo();
                info.ClassName = dr["classname"].ToString();
                info.Id = Convert.ToInt32(dr["id"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        public int DeleteAsk(int aid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetAskInfo(aid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                {
                    sql = "delete from nt_ask where id=" + aid + ";";
                }
                else
                {
                    sql = "delete from nt_ask where id=" + aid + " and UserID=" + userid + ";";
                }
                sql += "delete from nt_ask where parentid=" + aid + "";
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(37) * downinter), 0, 1, "删除问答");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteAskClass(int aid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                {
                    sql = "delete from NT_AskClass where id=" + aid + ";";
                    sql += "delete from NT_AskClass where parentid=" + aid + ";";
                    sql += "delete from NT_Ask where classid=" + aid + ";";
                    int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    return n;
                }
                return 0;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertAsk(AskInfo info,out int aid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[15];
                param[0] = new SqlParameter("@ClassID", SqlDbType.Int);
                param[0].Value = info.ClassID;
                param[1] = new SqlParameter("@Click", SqlDbType.Int);
                param[1].Value = info.Click;
                param[2] = new SqlParameter("@Content", SqlDbType.NVarChar, 2000);
                param[2].Value = info.Content;
                param[3] = new SqlParameter("@Id", SqlDbType.Int);
                param[3].Value = info.Id;
                param[4] = new SqlParameter("@IsBest", SqlDbType.TinyInt);
                param[4].Value = info.IsBest;
                param[5] = new SqlParameter("@IsClose", SqlDbType.TinyInt);
                param[5].Value = info.IsClose;
                param[6] = new SqlParameter("@IsJinji", SqlDbType.TinyInt);
                param[6].Value = info.IsJinji;
                param[7] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[7].Value = info.IsLock;
                param[8] = new SqlParameter("@JiFen", SqlDbType.Int);
                param[8].Value = info.JiFen;
                param[9] = new SqlParameter("@ParentID", SqlDbType.Int);
                param[9].Value = info.ParentID;
                param[10] = new SqlParameter("@Pic", SqlDbType.NVarChar, 30);
                param[10].Value = info.Pic;
                param[11] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[11].Value = info.PostTime;
                param[12] = new SqlParameter("@Tag", SqlDbType.NVarChar, 100);
                param[12].Value = info.Tag;
                param[13] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
                param[13].Value = info.Title;
                param[14] = new SqlParameter("@UserID", SqlDbType.Int);
                param[14].Value = info.UserID;
                string sql = string.Empty;
                sql = "insert into nt_ask(ClassID, ParentID, Title, [Content], PostTime, jiFen, UserID, isLock, Tag, click, Pic, isClose, isJinji, isBest)";
                sql += "values(@ClassID, @ParentID, @Title, @Content, @PostTime, @JiFen, @UserID, @IsLock, @Tag, @Click, @Pic, @IsClose,@IsJinji, @IsBest);Select SCOPE_IDENTITY()";
                //开始扣除积分
                int xs = Convert.ToInt32(JuSNS.Common.Public.GetXMLAskValue("isJinji"));//紧急扣除积分
                int wt = info.JiFen;
                int myjifen = JuSNS.Home.User.User.Instance.GetUserInfo(info.UserID).Integral;
                int rjifen = wt;
                if (info.IsJinji == 1)
                {
                    rjifen = (xs + wt);
                }
                //0失败，1积分不足，2成功
                if (myjifen < rjifen)
                {
                    aid = 0;
                    return 1;
                }
                else
                {
                    int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
                    aid = n;
                    if (n > 0)
                    {
                        JuSNS.Home.User.User.Instance.UpdateInte(info.UserID, rjifen, 0, 1, "提问扣除积分");
                        //DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_user set Integral=Integral-" + (rjifen) + " where UserID=@UserID", param);
                        //更新积分历史
                        return 2;
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

        public AskInfo GetAskInfo(object aid)
        {
            AskInfo info = new AskInfo();
            string sql = "select a.id, a.ClassID, a.ParentID, a.Title, a.[Content], a.PostTime, a.jiFen, a.UserID, a.isLock, a.Tag, a.click, a.Pic, a.isClose, a.isJinji, a.isBest, b.ClassName from NT_Ask AS a INNER JOIN  NT_AskClass AS b ON a.ClassID = b.id where a.islock=0 and a.id=" + aid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.ClassID = Convert.ToInt32(dr["ClassID"]);
                info.ClassName = Convert.ToString(dr["ClassName"]);
                info.Click = Convert.ToInt32(dr["Click"]);
                info.Content = Convert.ToString(dr["Content"]);
                info.Id = Convert.ToInt32(aid);
                if (dr["IsBest"] != DBNull.Value) info.IsBest = Convert.ToByte(dr["IsBest"]);
                info.IsClose = Convert.ToByte(dr["IsClose"]);
                info.IsJinji = Convert.ToByte(dr["IsJinji"]);
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.JiFen = Convert.ToInt32(dr["JiFen"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                if (dr["Pic"] != DBNull.Value) info.Pic = Convert.ToString(dr["Pic"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                if (dr["Tag"] != DBNull.Value) info.Tag = Convert.ToString(dr["Tag"]);
                info.Title = Convert.ToString(dr["Title"]);
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

        public AskClassInfo GetAskClassInfo(object aid)
        {
            AskClassInfo info = new AskClassInfo();
            string sql = "select * from NT_AskClass where id=" + aid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Id = Convert.ToInt32(aid);
                info.ClassName = Convert.ToString(dr["ClassName"]);
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        /// <summary>
        /// 更新问题状态
        /// </summary>
        /// <param name="aid">问题ID</param>
        /// <param name="flag">0增加点击率，1更新问题为关闭状态，2设置问题为最佳答案</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateAskState(object aid, int flag, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                switch (flag)
                {
                    case 0:
                        sql = "update nt_ask set click=click+1 where id=" + aid;
                        break;
                    case 1:
                        if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                        {
                            sql = "update nt_ask set isClose=1 where id=" + aid;
                        }
                        else
                        {
                            sql = "update nt_ask set isClose=1 where id=" + aid + " and userid=" + aid;
                        }
                        break;
                    case 2:
                        sql = "update nt_ask set isbest=1 where id=" + aid + " and userid=" + aid;
                        break;
                }
                return DbHelper.ExecuteNonQuery(cn, CommandType.Text,sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 得到问题列表
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="qstring">查询字符串</param>
        /// <param name="flag">0高分悬赏，1最新问题，2相关问题（联合qstring查询,未用）</param>
        /// <returns></returns>
        public List<AskInfo> GetAskList(int number, string qstring, int flag)
        {
            List<AskInfo> infolist = new List<AskInfo>();
            string whereSTR=string.Empty;
            switch (flag)
            {
                case 0:
                    whereSTR = " and isJinji=1 order by newid()";
                    break;
                case 1:
                    whereSTR = " order by posttime desc,id desc";
                    break;
            }
            string sql = "select top " + number + " title,id from nt_ask where islock=0" + whereSTR;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                AskInfo info = new AskInfo();
                info.Title = Convert.ToString(dr["title"]);
                info.Id = Convert.ToInt32(dr["Id"]);
                infolist.Add(info);
            }
            dr.Close();
            return infolist;
        }

        /// <summary>
        /// 设置为最佳答案
        /// </summary>
        /// <param name="uid">操作ID</param>
        /// <param name="infoid">操作的答案</param>
        /// <param name="mid">答案的问题ID</param>
        /// <param name="userid">被操作用户</param>
        /// <returns>0成功，1已经有最佳答案了，2问题已经关闭，3不是自己的问题，4设置失败</returns>
        public int SetAskBest(int uid, int infoid, int mid,int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                //判断问题是否关闭
                AskInfo mdl = GetAskInfo(mid);
                if(mdl.UserID!=uid) return 3;

                //是否是自己的问题
                if (mdl.IsClose == 1) return 2;                

                //检查是否有最佳答案
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(id) from nt_ask where ParentID=" + mid + " and islock=0 and isbest=1"));
                if (n > 0)
                    return 1;
                else
                {

                    int j = DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_ask set isbest=1 where id=" + infoid, null);
                    if (j > 0)
                    {
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_ask set IsClose=1 where id=" + mid, null);
                        //为用户增加积分
                        int point = mdl.JiFen;
                        byte isjiji = mdl.IsJinji;
                        int bestask = Convert.ToInt32(JuSNS.Common.Public.GetXMLAskValue("bestask"));
                        int isJinji = Convert.ToInt32(JuSNS.Common.Public.GetXMLAskValue("isJinji"));
                        int totol = point + bestask;
                        if (isjiji == 1)
                        {
                            totol = totol + isJinji;
                        }
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_user set integral=integral+" + totol + " where userid=" + userid, null);
                        JuSNS.Home.User.User.Instance.UpdateInte(userid, totol, 0, 0, "设置为最佳答案获得积分");
                        return 0;
                    }
                    else
                        return 4;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertAskClass(AskClassInfo info)
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
                sql = "update NT_AskClass set ParentID=@ParentID,ClassName=@ClassName where id=@Id";
            }
            else
            {
                sql = "insert into NT_AskClass(ParentID,ClassName)values(@ParentID,@ClassName)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }
    }
}
