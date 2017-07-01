using System;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.User;
using JuSNS.Profile;
using JuSNS.Model;
using JuSNS.Config;

namespace JuSNS.SQLServer.User
{
    public class Box : DbBase, IBox
    {

        /// <summary>
        /// 添加私信
        /// </summary>
        /// <param name="Info">私信实体类</param>
        /// <returns>添加成功返回1</returns>
        public int Add(MailInfo Info)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CnString);
            Conn.Open();
            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                SqlParameter[] param = GetParameters(Info);
                //添加私信表
                string Sql = "Insert Into [NT_Mail]" +
                             "([Sender],[Receiver],[Title],[Content],[PostTime],[PostIP],[TopicID],[IsReply],[LtType]," +
                             "[RelativeID],[IsRead])" +
                             "Values(@Sender,@Receiver,@Title,@Content,@PostTime,@PostIP,@TopicID,@IsReply,@LtType,@RelativeID,@IsRead);" +
                             "Select SCOPE_IDENTITY()";
                int BoxID = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, param));

                //添加收件箱
                Sql = "Insert Into [NT_MailInbox]" +
                      "([UserID],[BoxID]) " +
                      "Values(" + Info.Receiver + "," + BoxID + ")";
                DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);

                //添加发件箱
                Sql = "Insert Into [NT_MailOutbox]" +
                      "([UserID],[BoxID]) " +
                      "Values(" + Info.Sender + "," + BoxID + ")";
                int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                trans.Commit();
                return n;
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
        /// 删除私信
        /// </summary>
        /// <param name="dType">类型,0为发件箱,1为收件箱</param>
        /// <param name="LetterID">私信编号</param>
        /// <returns>删除成功返回1</returns>
        public int Del(int dType, int BoxID, int UserID)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CnString);
            Conn.Open();
            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                string Sql = string.Empty;
                int n = 0;

                if (dType == 0)
                {
                    //查找收件箱是否还存在此记录
                    Sql = "Select Count([ID]) From [NT_MailInbox] Where [LetterID]=" + BoxID;
                    n = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, null));
                    if (n == 0)
                    {
                        //未找到记录代表收件箱相符记录已被删除，此时删除私信表记录
                        Sql = "Delete From  [NT_Mail] " +
                              "Where ([ID]=" + BoxID + " Or [TopicID]=" + BoxID + ") And " +
                              "([Sender]=" + UserID + " Or [Receiver]=" + UserID + ")";
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                    }
                    //删除发件箱
                    Sql = "Delete From  [NT_MailOutbox] Where [LetterID]=" + BoxID + " And UserID=" + UserID;
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                }
                else
                {
                    //查找发件箱是否还存在此记录
                    Sql = "Select Count([ID]) From [NT_MailOutbox] Where [LetterID]=" + BoxID;
                    n = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, null));
                    if (n == 0)
                    {
                        //未找到记录代表发件箱相符记录已被删除，此时删除私信表记录
                        Sql = "Delete From  [NT_Mail] " +
                              "Where ([ID]=" + BoxID + " Or [TopicID]=" + BoxID + ") And " +
                              "([Sender]=" + UserID + " Or [Receiver]=" + UserID + ")";
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                    }
                    //删除收件箱
                    Sql = "Delete From  [NT_MailInbox] Where [LetterID]=" + BoxID + " And UserID=" + UserID;
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                }
                trans.Commit();
                return n;
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
        /// 回复私信
        /// </summary>
        /// <param name="Info">私信实体类</param>
        /// <returns>回复成功返回1</returns>
        public int Reply(MailInfo info)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CnString);
            Conn.Open();
            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                SqlParameter[] param = GetParameters(info);
                string Sql = "Select [Receiver] From  [NT_Mail] Where [ID]=" + info.TopicID;
                int Receiver = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, null));
                if (Receiver == info.Sender)
                {
                    Sql = "Select [Sender] From  [NT_Mail] Where [ID]=" + info.TopicID;
                    Receiver = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, null));
                }
                //添加私信表
                Sql = "Insert Into [NT_Mail]" +
                      "([Sender],[Receiver],[Title],[Content],[PostTime],[PostIP],[TopicID],[IsReply],[LtType]," +
                      "[RelativeID],[IsRead])" +
                      "Values(@Sender," + Receiver + ",@Title,@Content,@PostTime,@PostIP,@TopicID,@IsReply,@LtType,@RelativeID,@IsRead);";
                int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, param);

                Sql = "Select Count([ID]) From [NT_MailInbox] Where UserID=" + Receiver + " And [boxID]=" + info.TopicID;
                int tempcnt = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, null));
                if (tempcnt > 0)
                {
                    //添加收件箱
                    Sql = "Insert Into [NT_MailInbox]([UserID],[boxID]) " +
                          "Values(" + info.Receiver + "," + info.TopicID + ")";
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                }
                else
                {
                    //添加收件箱
                    Sql = "Insert Into [NT_MailInbox]([UserID],[boxID]) " +
                          "Values(" + Receiver + "," + info.TopicID + ")";
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                }

                //添加发件箱
                Sql = "Insert Into [NT_MailOutbox]([UserID],[boxID]) Values(" + info.Sender + "," + info.TopicID + ")";
                DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);

                //如果是接收者回复私信，则把私信状态置为已回复
                Sql = "Select Count([ID]) From [NT_Mail] Where [Receiver]=" + info.Sender + " And [ID]=" + info.TopicID;
                int cnt = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, null));
                if (cnt == 1)
                {
                    Sql = "Update [NT_Mail] Set [IsReply]=1 Where [ID]=" + info.TopicID + " And [Receiver]=" + info.Sender;
                    DbHelper.ExecuteScalar(trans, CommandType.Text, Sql, param);
                }

                trans.Commit();
                return n;
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
        /// 取得最新回复内容
        /// </summary>
        /// <param name="boxID">私信编号</param>
        /// <param name="isMy">是否自已回复的最新内容</param>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回最新回复内容</returns>
        public string GetNewReContent(int boxID, bool isMy, int UserID)
        {
            string Temp = "";
            if (isMy)
                Temp = " And [Sender]=" + UserID;
            string Sql = "Select Content From [NT_Mail] " +
                         "Where ([TopicID]=" + boxID + " Or [ID]=" + boxID + ") " + Temp + " Order By [ID] Desc";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }


        /// <summary>
        /// 取得最新的回复是否被阅读
        /// </summary>
        /// <param name="boxID">私信编号</param>
        /// <param name="UserID">用户编号</param>
        /// <returns>0为未读，1为已读</returns>
        public int GetNewReCNT(int boxID, int UserID)
        {
            string Sql = "Select Count([ID]) From [NT_Mail] " +
                         "Where [IsRead]=0 And ([TopicID]=" + boxID + " Or [ID]=" + boxID + ") And [Receiver]=" + UserID;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
            if (n > 0)
                return 0;
            else
                return 1;
        }


        /// <summary>
        /// 设置收到的信息为已读状态
        /// </summary>
        /// <param name="boxID">私信编号</param>
        /// <param name="UserID">用户编号</param>
        /// <returns>操作成功返回1</returns>
        public int Read(int boxID, int UserID)
        {
            string Sql = "Update [NT_Mail] Set [IsRead]=1 " +
                         "Where ([TopicID]=" + boxID + " Or [ID]=" + boxID + ") And [Receiver]=" + UserID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新通知状态
        /// </summary>
        /// <param name="UserID">会员编号</param>
        public void UpdateNoticeMode(int UserID)
        {
            string Sql = "Update NT_Notice Set IsRead=1 Where Receiver=" + UserID;
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 私信信息
        /// </summary>
        /// <param name="PhotoID">私信编号</param>
        /// <returns>返回私信实体类</returns>
        public MailInfo Info(int ID)
        {
            MailInfo info = new MailInfo();
            string Sql = "Select [ID],[Sender],[Receiver]," +
                         "[Title],[Content],[PostTime],[PostIP],[TopicID],[IsReply],[LtType],[RelativeID],[IsRead] " +
                         "From " +
                         "[NT_Mail] " +
                         "Where [ID]=" + ID;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            if (rd.Read())
            {
                info.ID = Convert.ToInt32(rd["ID"].ToString());
                info.Sender = Convert.ToInt32(rd["Sender"].ToString());
                info.Receiver = Convert.ToInt32(rd["Receiver"].ToString());
                if (rd["Title"] != DBNull.Value) info.Title = rd["Title"].ToString();
                if (rd["Content"] != DBNull.Value) info.Content = rd["Content"].ToString();
                info.PostTime = Convert.ToDateTime(rd["PostTime"].ToString());
                if (rd["PostIP"] != DBNull.Value) info.PostIP = rd["PostIP"].ToString();
                info.TopicID = Convert.ToInt32(rd["TopicID"].ToString());
                info.IsReply = Convert.ToBoolean(rd["IsReply"].ToString());
                info.LtType = Convert.ToInt32(rd["LtType"].ToString());
                info.RelativeID = Convert.ToInt32(rd["RelativeID"].ToString());
                info.IsRead = Convert.ToInt32(rd["IsRead"].ToString());
            }
            return info;
        }

        public int GetBoxUnRead(int UserID)
        {
            string sql = "Select Count([ID]) From  [NT_Mail] Where [IsRead]=0 And [Receiver]=" + UserID + " and (ID IN (SELECT BoxID FROM NT_Mailinbox) or TopicID in (SELECT BoxID FROM NT_Mailinbox))";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">私信实体类</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] GetParameters(MailInfo Info)
        {
            SqlParameter[] param = new SqlParameter[12];

            param[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[0].Value = Info.ID;
            param[1] = new SqlParameter("@Sender", SqlDbType.Int, 4);
            param[1].Value = Info.Sender;
            param[2] = new SqlParameter("@Receiver", SqlDbType.Int, 4);
            param[2].Value = Info.Receiver;

            param[3] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            param[3].Value = Info.Title;
            param[4] = new SqlParameter("@Content", SqlDbType.NVarChar, 500);
            param[4].Value = Info.Content;
            param[5] = new SqlParameter("@PostTime", SqlDbType.DateTime, 8);
            param[5].Value = Info.PostTime;

            param[6] = new SqlParameter("@PostIP", SqlDbType.VarChar, 15);
            param[6].Value = Info.PostIP;
            param[7] = new SqlParameter("@TopicID", SqlDbType.Int, 4);
            param[7].Value = Info.TopicID;
            param[8] = new SqlParameter("@IsReply", SqlDbType.Bit);
            param[8].Value = Info.IsReply;

            param[9] = new SqlParameter("@LtType", SqlDbType.Int, 4);
            param[9].Value = Info.LtType;
            param[10] = new SqlParameter("@RelativeID", SqlDbType.Int, 4);
            param[10].Value = Info.RelativeID;
            param[11] = new SqlParameter("@IsRead", SqlDbType.Int, 4);
            param[11].Value = Info.IsRead;

            return param;
        }
    }
}
