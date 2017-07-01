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
    public class App : DbBase, IApp
    {
        public bool IsDeveloper(int userid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
                return true;
            string sql = "select count(userid) from NT_AppDeveloper where userid="+userid+" and islock=" + (byte)EnumCusState.ForNormal;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }

        public int InsertDev(AppDeveloperInfo info)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Email", SqlDbType.NVarChar, 150);
            param[0].Value = info.Email;
            param[1] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[1].Value = info.IsLock;
            param[2] = new SqlParameter("@JoinTime", SqlDbType.DateTime);
            param[2].Value = info.JoinTime;
            param[3] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30);
            param[3].Value = info.Mobile;
            param[4] = new SqlParameter("@Tel", SqlDbType.NVarChar, 20);
            param[4].Value = info.Tel;
            param[5] = new SqlParameter("@Userid", SqlDbType.Int);
            param[5].Value = info.Userid;
            param[6] = new SqlParameter("@Userkey", SqlDbType.NVarChar, 32);
            param[6].Value = info.Userkey;
            param[7] = new SqlParameter("@Username", SqlDbType.NVarChar,20);
            param[7].Value = info.Username;
            string sql = "insert into NT_AppDeveloper(userid, userkey, JoinTime, tel, mobile, email, IsLock,Username)values(@Userid, @Userkey, @JoinTime, @Tel, @Mobile, @Email, @IsLock,@Username)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public AppDeveloperInfo GetDevInfo(int userid)
        {
            AppDeveloperInfo info = new AppDeveloperInfo();
            string sql = "select userid, userkey, JoinTime, tel, mobile, email, IsLock,Username from NT_AppDeveloper where userid=" + userid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Email = dr["Email"].ToString();
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.JoinTime = Convert.ToDateTime(dr["JoinTime"]);
                info.Mobile = dr["Mobile"].ToString();
                info.Tel = dr["Tel"].ToString();
                info.Userid = int.Parse(dr["Userid"].ToString());
                info.Userkey = dr["Userkey"].ToString();
                info.Username = dr["Username"].ToString();
                dr.Close();
                return info;
            }
            dr.Close();
            return null;
        }

        public int InsertApp(AppInfo info)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@Appkey", SqlDbType.NVarChar, 32);
            param[0].Value = info.Appkey;
            param[1] = new SqlParameter("@Appname", SqlDbType.NVarChar,60);
            param[1].Value = info.Appname;
            param[2] = new SqlParameter("@Classid", SqlDbType.Int);
            param[2].Value = info.Classid;
            param[3] = new SqlParameter("@Click", SqlDbType.Int);
            param[3].Value = info.Click;
            param[4] = new SqlParameter("@Content", SqlDbType.NText);
            param[4].Value = info.Content;
            param[5] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
            param[5].Value = info.CreatTime;
            param[6] = new SqlParameter("@Height", SqlDbType.Int);
            param[6].Value = info.Height;
            param[7] = new SqlParameter("@Icon", SqlDbType.NVarChar, 30);
            param[7].Value = info.Icon;
            param[8] = new SqlParameter("@Id", SqlDbType.Int);
            param[8].Value = info.Id;
            param[9] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[9].Value = info.IsLock;
            param[10] = new SqlParameter("@Pic", SqlDbType.NVarChar, 30);
            param[10].Value = info.Pic;
            param[11] = new SqlParameter("@SetupContent", SqlDbType.NText);
            param[11].Value = info.SetupContent;
            param[12] = new SqlParameter("@SetupNumber", SqlDbType.Int);
            param[12].Value = info.SetupNumber;
            param[13] = new SqlParameter("@TargetStyle", SqlDbType.TinyInt);
            param[13].Value = info.TargetStyle;
            param[14] = new SqlParameter("@Url", SqlDbType.NVarChar, 250);
            param[14].Value = info.Url;
            param[15] = new SqlParameter("@UserID", SqlDbType.Int);
            param[15].Value = info.UserID;
            param[16] = new SqlParameter("@Width", SqlDbType.Int);
            param[16].Value = info.Width;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update nt_app Appname=@Appname, Classid=@Classid, Icon=@Icon, Pic=@Pic, CreatTime=@CreatTime, IsLock=@IsLock, Content=@Content, Url=@Url, TargetStyle=@TargetStyle, Width=@Width, Height=@Height, SetupContent=@SetupContent where id=@Id";
            }
            else
            {
                sql = "insert into nt_app( appname, appkey, classid, icon, pic, UserID, CreatTime, IsLock, [Content], url, click, setupNumber, targetStyle, width, height, SetupContent)values(@Appname, @Appkey, @Classid, @Icon, @Pic, @UserID, @CreatTime, @IsLock, @Content, @Url, @Click, @SetupNumber, @TargetStyle, @Width, @Height, @SetupContent)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateAppClick(int appid)
        {
            string sql = "update nt_app set click=click+1 where id=" + appid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int UpdateAppSetup(int appid)
        {
            string sql = "update nt_app set setupNumber=setupNumber+1 where id=" + appid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertSetupApp(int appid, int userid, int flag)
        {
            string sql = string.Empty;
            if (flag == 1)
            {
                sql = "insert into NT_AppSetup(userid,appid,PostTime)values(" + userid + "," + appid + ",'" + DateTime.Now + "')";
            }
            else
            {
                sql = "delete from NT_AppSetup where appid=" + appid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public AppInfo GetAppInfo(int appid)
        {
            AppInfo info  = new AppInfo();
            string sql = "select  id, appname, appkey, classid, icon, pic, UserID, CreatTime, IsLock, [Content], url, click, setupNumber, targetStyle, width, height, SetupContent from nt_app where id=" + appid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Appkey = dr["appkey"].ToString();
                info.Appname = dr["Appname"].ToString();
                info.Classid = int.Parse(dr["Classid"].ToString());
                info.Click = int.Parse(dr["Click"].ToString());
                info.Content = dr["Content"].ToString();
                info.CreatTime = DateTime.Parse(dr["CreatTime"].ToString());
                info.Height = int.Parse(dr["Height"].ToString());
                info.Icon = dr["Icon"].ToString();
                info.Id = int.Parse(dr["Id"].ToString());
                info.IsLock = byte.Parse(dr["IsLock"].ToString());
                info.Pic = dr["Pic"].ToString();
                info.SetupContent = dr["SetupContent"].ToString();
                info.SetupNumber = int.Parse(dr["SetupNumber"].ToString());
                info.TargetStyle = byte.Parse(dr["TargetStyle"].ToString());
                info.Url = dr["Url"].ToString();
                info.UserID = int.Parse(dr["UserID"].ToString());
                info.Width = int.Parse(dr["Width"].ToString());
                dr.Close();
                return info;
            }
            dr.Close();
            return null;
        }

        public int DeleteApp(int appid, int userid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                string sql = "delete from nt_app where id=" + appid;
                sql += "delete from NT_AppSetup where appid="+appid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public int DeleteAppdev(int appid, int userid)
        {
            if (JuSNS.Home.User.User.Instance.IsAdmin(userid))
            {
                string sql = "delete from NT_AppDeveloper where userid=" + appid;
                sql += "delete from NT_App where userid=" + appid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }

        public bool IsSetupApp(int appid, int userid)
        {
            string sql = "select count(userid) from NT_AppSetup where appid=" + appid + " and userid=" + userid;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }

    }

}