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
    public class Share : DbBase, IShare
    {
        public int InsertShare(ShareInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@Comments", SqlDbType.Int);
                param[0].Value = info.Comments;
                param[1] = new SqlParameter("@Content", SqlDbType.NText);
                param[1].Value = info.Content;
                param[2] = new SqlParameter("@Id", SqlDbType.Int);
                param[2].Value = info.Id;
                param[3] = new SqlParameter("@Infoid", SqlDbType.Int);
                param[3].Value = info.Infoid;
                param[4] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                param[4].Value = info.IsLock;
                param[5] = new SqlParameter("@IsRec", SqlDbType.Bit);
                param[5].Value = info.IsRec;
                param[6] = new SqlParameter("@PostIP", SqlDbType.NVarChar,15);
                param[6].Value = info.PostIP;
                param[7] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[7].Value = info.PostTime;
                param[8] = new SqlParameter("@ShareType", SqlDbType.TinyInt);
                param[8].Value = info.ShareType;
                param[9] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
                param[9].Value = info.Title;
                param[10] = new SqlParameter("@UserID", SqlDbType.Int);
                param[10].Value = info.UserID;
                param[11] = new SqlParameter("@WebURL", SqlDbType.NVarChar, 120);
                param[11].Value = info.WebURL;
                string sql = "Insert Into NT_Share(UserID, ShareType, infoid, PostTime, PostIP, IsLock, Title, [Content], webURL, Comments, IsRec)values(@UserID, @ShareType, @Infoid, @PostTime, @PostIP, @IsLock, @Title, @Content, @WebURL, @Comments, @IsRec);Select SCOPE_IDENTITY()";
                return Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param));
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteShare(int sid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int getuserid = GetInfo(sid).UserID;
                if (JuSNS.Home.User.User.Instance.IsAdmin(userid, cn))
                {
                    sql = "delete from NT_Share where id=" + sid;
                }
                else
                {
                    sql = "delete from NT_Share where id=" + sid + " and UserID=" + userid;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    //扣除积分
                    int downinter = Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("downintel"));
                    JuSNS.Home.User.User.Instance.UpdateInte(getuserid, (JuSNS.Common.Public.JSplit(12) * downinter), 0, 1, "删除分享");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public ShareInfo GetInfo(object sid)
        {
            ShareInfo info = new ShareInfo();
            string sql = "select UserID, ShareType, infoid, PostTime, PostIP, IsLock, Title, Content, webURL, Comments, IsRec from nt_share where id=" + sid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Comments = Convert.ToInt32(dr["Comments"]);
                info.Content = Convert.ToString(dr["Content"]);
                info.Id = Convert.ToInt32(sid);
                info.Infoid = Convert.ToInt32(dr["Infoid"]);
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.IsRec = Convert.ToBoolean(dr["IsRec"]);
                info.PostIP = Convert.ToString(dr["PostIP"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.ShareType = Convert.ToByte(dr["ShareType"]);
                info.Title = Convert.ToString(dr["Title"]);
                info.UserID = Convert.ToInt32(dr["UserID"]);
                info.WebURL = Convert.ToString(dr["WebURL"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }
    }
}
