using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.User;
using JuSNS.Profile;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.Config;

namespace JuSNS.SQLServer.User
{
    public class User : DbBase, IUser
    {
        public bool IsAdmin(object UserID)
        {
            string sql = "select isadmin from nt_user where userid=" + UserID;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                if (Convert.ToInt32(dr["isadmin"])>0)
                {
                    dr.Close();
                    return true;
                }
            }
            dr.Close();
            return false;
        }

        public bool IsAdmin(object UserID, SqlConnection cn)
        {
            try
            {
                string sql = "select isadmin from nt_user where userid=" + UserID;
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, sql, null);
                if (dr.Read())
                {
                    if (Convert.ToInt32(dr["isadmin"])>0)
                    {
                        dr.Close();
                        return true;
                    }
                }
                dr.Close();
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="InputSTR">用户名/或电子邮件</param>
        /// <param name="LoginType">0电子邮件，1用户名，2手机</param>
        /// <returns>True或False</returns>
        public bool CheckUserExsit(string InputSTR, int LoginType)
        {
            string sql = string.Empty;
            switch (LoginType)
            {
                case 0:
                    sql = "select count(UserID) FROM NT_User where Email='" + InputSTR + "'";
                    break;
                case 1:
                    sql = "select count(UserID) FROM NT_User where UserName='" + InputSTR + "'";
                    break;
                case 2:
                    sql = "select count(UserID) FROM NT_User where Mobile='" + InputSTR + "'";
                    break;
            }
            object n = DbHelper.ExecuteScalar(CommandType.Text, sql, null);
            return Convert.ToInt32(n) > 0 ? true : false;
        }

        public bool CheckUserExsit(object UserID)
        {
            string sql = "select count(userid) from nt_user where UserID=" + UserID;
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="loginNum"></param>
        /// <returns></returns>
        public EnumLoginState CheckUser(int userId, string password, out int loginNum)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int lNum = 0;
                string pwd = string.Empty;
                EnumUserState us = EnumUserState.Lock;
                bool exist = false;
                string Sql = "select Password,State,LoginTimes from NT_User where UserID=" + userId;
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (rd.Read())
                {
                    pwd = rd["Password"].ToString();
                    us = (EnumUserState)Convert.ToInt16(rd["State"]);
                    exist = true;
                    lNum = Convert.ToInt32(rd["LoginTimes"]);
                }
                loginNum = lNum;
                rd.Close();
                if (!exist || pwd == string.Empty || pwd != password)
                {
                    return EnumLoginState.Err_NameOrPwdError;
                }
                if (us == EnumUserState.Register)
                {
                    if (UiConfig.RegVer == "1")
                    {
                        return EnumLoginState.Err_UnActivation;
                    }
                }
                else if (us == EnumUserState.Lock)
                {
                    return EnumLoginState.Err_Locked;
                }
                GetMemberLevels(userId, cn);
                UpdateInte(userId, Public.JSplit(2), 0, 0, "登录");
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, "update nt_user set LastLoginTime='" + DateTime.Now + "' where userid=" + userId, null);
                return EnumLoginState.Succeed;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 得到用户实例
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(object UserID)
        {
            UserInfo uInfo = new UserInfo();
            string sql = "select Email, Password, UserName, TrueName, Sex, Marriage , Mobile, BindMoblie, State, LastLoginTime, LastLoginIP, LoginTimes,ProvinceID, City, RegTime, RegIP, Portrait, InviterID, VerifyCode, ConfirmTime,isRec, integral, inteyb, Click,IsAdmin,AttNumber,MobileCode,IsVip,Money,MemberLevels from NT_User where UserID=" + UserID + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                uInfo.City = Convert.ToInt32(rd["City"]);
                uInfo.ProvinceID = Convert.ToInt32(rd["ProvinceID"]);
                uInfo.Click = Convert.ToInt32(rd["Click"]);
                if (rd["ConfirmTime"] != DBNull.Value) uInfo.ConfirmTime = Convert.ToDateTime(rd["ConfirmTime"]);
                uInfo.Email = rd["Email"].ToString();
                uInfo.Sex = Convert.ToByte(rd["Sex"]);
                uInfo.Marriage = Convert.ToByte(rd["Marriage"]);
                uInfo.Integral = Convert.ToInt32(rd["Integral"]);
                uInfo.Inteyb = Convert.ToInt32(rd["Inteyb"]);
                uInfo.InviterID = Convert.ToInt32(rd["InviterID"]);
                uInfo.Mobile = rd["Mobile"].ToString();
                uInfo.BindMoblie = Convert.ToBoolean(rd["BindMoblie"]);
                uInfo.IsRec = Convert.ToByte(rd["IsRec"]);
                uInfo.LastLoginIP = rd["LastLoginIP"].ToString();
                if (rd["LastLoginTime"] != DBNull.Value) uInfo.LastLoginTime = Convert.ToDateTime(rd["LastLoginTime"]);
                uInfo.LoginTimes = Convert.ToInt32(rd["LoginTimes"]);
                uInfo.Password = rd["Password"].ToString();
                uInfo.Portrait = Convert.ToInt32(rd["Portrait"]);
                uInfo.RegIP = rd["RegIP"].ToString();
                if (rd["RegTime"] != DBNull.Value) uInfo.RegTime = Convert.ToDateTime(rd["RegTime"]);
                uInfo.State = Convert.ToByte(rd["State"]);
                uInfo.UserID = Convert.ToInt32(UserID);
                uInfo.MobileCode = Convert.ToInt32(rd["MobileCode"]);
                uInfo.UserName = rd["UserName"].ToString();
                uInfo.TrueName = rd["TrueName"].ToString();
                uInfo.VerifyCode = rd["VerifyCode"].ToString();
                uInfo.IsAdmin = Convert.ToByte(rd["isAdmin"]);
                if (rd["AttNumber"] != DBNull.Value) uInfo.AttNumber = Convert.ToInt32(rd["AttNumber"]);
                uInfo.IsVip = Convert.ToBoolean(rd["IsVip"]);
                uInfo.Money = Convert.ToDouble(rd["Money"]);
                uInfo.MemberLevels = Convert.ToInt32(rd["MemberLevels"]);
                rd.Close();
                return uInfo;
            }
            rd.Close();
            return uInfo;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Password">新Password</param>
        /// <returns></returns>
        public bool ChangePassword(int UserID, string Password)
        {
            SqlParameter param = new SqlParameter("@Password", SqlDbType.NVarChar, 32);
            param.Value = Password;
            string sql = "update NT_User set Password=@Password where UserID=" + UserID + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param) > 0 ? true : false;
        }

        public int ChangeNowCity(int UserID, int homeprovince, int City)
        {
            string sql = "update NT_User set provinceID=" + homeprovince + ",City=" + City + " where UserID=" + UserID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public bool CheckEmail(int UserID, string Email)
        {
            string sql = "select count(userid) from nt_user where email='" + Email + "' and UserID!=" + UserID;
            object n = DbHelper.ExecuteScalar(CommandType.Text, sql, null);
            return Convert.ToInt32(n) > 0 ? true : false;
        }

        /// <summary>
        /// 修改Email
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Email">新Email</param>
        /// <returns>0修改成功，1你修改过了，还未激活,2修改成功，但未发送电子邮件。</returns>
        public int ChangeEmail(int UserID, string Email)
        {
            SqlConnection sqlConn = new SqlConnection(DBConfig.CnString);
            sqlConn.Open();
            SqlTransaction trans = sqlConn.BeginTransaction();
            try
            {
                SqlParameter param = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
                param.Value = Email;
                string exsql = "select count(id) FROM NT_SpareEmail where UserID=" + UserID;
                if (Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, exsql, null)) == 0)
                {
                    string RandSTR = JuSNS.Common.Rand.Str(15);
                    string sql = "Insert into NT_SpareEmail (UserID,Email,PostTime,PostIP,vCode)VALUES(" + UserID + ",@Email,'" + DateTime.Now + "','" + Public.GetClientIP() + "','" + RandSTR + "')";
                    int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    trans.Commit();
                    //发送电子邮件：修改电子邮件后，要激活电子邮件才可用
                    try
                    {
                        JuSNS.Common.EMail em = JuSNS.Common.EMail.CreateInstance();
                        string body = EmailConfig.modifyemail;
                        body = body.Replace("{SiteName}", UiConfig.SiteName);
                        body = body.Replace("{UserName}", GetUserInfo(UserID).UserName);
                        body = body.Replace("{Domain}", UiConfig.Domain);
                        body = body.Replace("{Date}", DateTime.Now.ToString("yyyy年MM月dd日"));
                        body = body.Replace("{Url}", UiConfig.RootUrl + "/Verify" + Public.GetXMLValue("siteExName") + "?do=ModifyEmail&userid=" + UserID + "&vcode=" + RandSTR.ToLower() + "");
                        em.Body = body;
                        em.Subject = "亲爱的" + GetUserInfo(UserID).UserName + "，请确认你在" + UiConfig.SiteName + "的注册邮箱";
                        em.To = Email.Trim();
                        em.From = UiConfig.SiteName + "<" + EmailConfig.from + ">";
                        em.Send();
                        return 0;
                    }
                    catch
                    {
                        return 2;
                    }
                }
                else
                {
                    return 1;
                }
            }
            catch (SqlException sqlEx)
            {
                trans.Rollback();
                throw sqlEx;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }


        public int UpdateUserInfo(UserInfo us, UserBaseInfo basi)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@UserID", SqlDbType.Int);
            param[0].Value = us.UserID;
            param[1] = new SqlParameter("@Marriage", SqlDbType.TinyInt);
            param[1].Value = us.Marriage;
            param[2] = new SqlParameter("@TrueName", SqlDbType.NVarChar, 20);
            param[2].Value = us.TrueName;
            param[3] = new SqlParameter("@Birthday", SqlDbType.DateTime);
            param[3].Value = basi.Birthday;
            param[4] = new SqlParameter("@BirthidayDisplay", SqlDbType.TinyInt);
            param[4].Value = basi.BirthidayDisplay;
            param[5] = new SqlParameter("@Constellation", SqlDbType.Int);
            param[5].Value = basi.Constellation;
            param[6] = new SqlParameter("@HomeCity", SqlDbType.Int);
            param[6].Value = basi.HomeCity;
            param[7] = new SqlParameter("@Vocation", SqlDbType.Int);
            param[7].Value = basi.Vocation;
            string sql = string.Empty;
            if (Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "select count(userid) from nt_userinfo where userid=@UserID", param)) > 0)
            {
                sql = "update nt_user set Marriage=@Marriage,TrueName=@TrueName where UserID=@UserID;";
                sql += "update nt_userinfo set Birthday=@Birthday,BirthidayDisplay=@BirthidayDisplay,Constellation=@Constellation,HomeCity=@HomeCity,Vocation=@Vocation where UserID=@UserID";
            }
            else
            {
                sql = "update nt_user set Marriage=@Marriage,TrueName=@TrueName where UserID=@UserID;";
                sql += "insert into nt_userinfo(UserID,Birthday,BirthidayDisplay,Constellation,HomeCity,Vocation)values(@UserID,@Birthday,@BirthidayDisplay,@Constellation,@HomeCity,@Vocation)";
            }
            //这里加积分

            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateUserBasicInfo(UserInfo us, UserBaseInfo basi)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@UserID", SqlDbType.Int);
            param[0].Value = us.UserID;
            param[1] = new SqlParameter("@MSN", SqlDbType.NVarChar, 50);
            param[1].Value = basi.MSN;
            param[2] = new SqlParameter("@QQ", SqlDbType.NVarChar, 12);
            param[2].Value = basi.QQ;
            param[3] = new SqlParameter("@Tel", SqlDbType.NVarChar, 20);
            param[3].Value = basi.Tel;
            param[4] = new SqlParameter("@City", SqlDbType.Int);
            param[4].Value = us.City;
            param[5] = new SqlParameter("@Addr", SqlDbType.NVarChar, 50);
            param[5].Value = basi.Addr;
            param[6] = new SqlParameter("@Website", SqlDbType.NVarChar, 60);
            param[6].Value = basi.WebSite;
            param[7] = new SqlParameter("@EmailPrivacy", SqlDbType.TinyInt);
            param[7].Value = basi.EmailPrivacy;
            param[8] = new SqlParameter("@MSNPrivacy", SqlDbType.Int);
            param[8].Value = basi.MSNPrivacy;
            param[9] = new SqlParameter("@QQPrivacy", SqlDbType.Int);
            param[9].Value = basi.QQPrivacy;
            param[10] = new SqlParameter("@TelPrivacy", SqlDbType.Int);
            param[10].Value = basi.TelPrivacy;
            param[11] = new SqlParameter("@AddrPrivacy", SqlDbType.Int);
            param[11].Value = basi.AddrPrivacy;
            param[12] = new SqlParameter("@WebSitePrivacy", SqlDbType.Int);
            param[12].Value = basi.WebSitePrivacy;
            param[13] = new SqlParameter("@ProvinceID", SqlDbType.Int);
            param[13].Value = us.ProvinceID;
            string sql = "update nt_user set City=@City,ProvinceID=@ProvinceID where UserID=@UserID;";
            sql += "update nt_userinfo set MSN=@MSN,QQ=@QQ,Tel=@Tel,Addr=@Addr,Website=@Website,EmailPrivacy=@EmailPrivacy,MSNPrivacy=@MSNPrivacy,QQPrivacy=@QQPrivacy,TelPrivacy=@TelPrivacy,AddrPrivacy=@AddrPrivacy,WebSitePrivacy=@WebSitePrivacy where UserID=@UserID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public EnumRegister Register(UserInfo ui, UserBaseInfo basi, out int uid)
        {
            string VerifyCode = JuSNS.Common.Rand.Str(10);
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[17];
                param[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                param[0].Direction = ParameterDirection.ReturnValue;
                param[1] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
                param[1].Value = ui.Email;
                param[2] = new SqlParameter("@IP", SqlDbType.Char, 15);
                param[2].Value = JuSNS.Common.Public.GetClientIP();
                param[3] = new SqlParameter("@Password", SqlDbType.NVarChar, 32);
                param[3].Value = ui.Password;
                param[4] = new SqlParameter("@Username", SqlDbType.NVarChar, 50);
                param[4].Value = ui.UserName;
                param[5] = new SqlParameter("@State", SqlDbType.Int);
                param[5].Value = (int)EnumUserState.Register;
                param[6] = new SqlParameter("@City", SqlDbType.Int);
                param[6].Value = ui.City;
                param[7] = new SqlParameter("@NowTime", SqlDbType.DateTime);
                param[7].Value = DateTime.Now;
                param[8] = new SqlParameter("@VerifyCode", SqlDbType.Char, 11);
                param[8].Value = VerifyCode;
                param[9] = new SqlParameter("@EmailItem", SqlDbType.VarChar, 1000);
                param[9].Value = EnumClass.GetEmailNotifyStr();
                param[10] = new SqlParameter("@PrivDef", SqlDbType.Int);
                param[10].Value = (int)EnumPrivacy.ForWholeSite;
                param[11] = new SqlParameter("@Sex", SqlDbType.TinyInt);
                param[11].Value = ui.Sex;
                param[12] = new SqlParameter("@TrueName", SqlDbType.VarChar, 20);
                param[12].Value = ui.TrueName;
                param[13] = new SqlParameter("@MobileCode", SqlDbType.Int);
                param[13].Value = Rand.Number(5);
                param[14] = new SqlParameter("@InviteID", SqlDbType.Int);
                param[14].Value = ui.InviterID;
                param[15] = new SqlParameter("@Birthday", SqlDbType.DateTime);
                param[15].Value = basi.Birthday;
                param[16] = new SqlParameter("@ProvinceID", SqlDbType.Int);
                param[16].Value = ui.ProvinceID;
                DbHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "JuSNS_Register", param);
                int retvalue = (int)param[0].Value;
                uid = 0;
                switch (retvalue)
                {
                    case 1:
                        #region 注册成功发送邮件
                        string qsql = "select UserID from NT_User where Email='" + ui.Email + "'";
                        uid = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, qsql, param));
                        //插入默认好友
                        InserDefaultFriend(uid);
                        try
                        {
                            #region 是否需要验证邮件
                            if (Public.GetXMLValue("RegVer") == "1")
                            {
                                JuSNS.Common.EMail em = JuSNS.Common.EMail.CreateInstance();
                                string body = EmailConfig.register;
                                body = body.Replace("{SiteName}", UiConfig.SiteName);
                                body = body.Replace("{UserName}", ui.UserName);
                                body = body.Replace("{Domain}", UiConfig.Domain);
                                body = body.Replace("{Date}", DateTime.Now.ToString("yyyy年MM月dd日"));
                                body = body.Replace("{Url}", UiConfig.URL + "/verify" + JuSNS.Common.Public.GetXMLValue("siteExName") + "?type=1&email=" + ui.Email + "&code=" + VerifyCode);
                                //body = body.Replace("{Url}", UiConfig.RootUrl + "/verify" + JuSNS.Common.Public.GetXMLValue("siteExName") + "?do=Register&email=" + ui.Email + "&vcode=" + VerifyCode + "");
                                em.Body = body;
                                em.Subject = "亲爱的" + ui.UserName + "，请确认你在" + UiConfig.SiteName + "的注册邮箱";
                                em.To = ui.Email;
                                em.From = UiConfig.SiteName + "<" + EmailConfig.from + ">";
                                em.Send();
                                return EnumRegister.Succeed;
                            }
                            else
                            {
                                return EnumRegister.SucceedNotMail;
                            }
                            #endregion
                        }
                        catch
                        {
                            return EnumRegister.SucceedNotMail;
                        }
                        #endregion
                    case 2:
                        return EnumRegister.EmailRepeat;
                    default:
                        return EnumRegister.UnexpectedError;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int GetUserIDForEmail(string email)
        {
            string sql = "select userid from nt_user where email='" + email + "'";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                int userid=Convert.ToInt32(dr["userid"]);
                dr.Close();
                return userid;
            }
            dr.Close();
            return 0;
        }

        /// <summary>
        /// Email邀请注册
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="code"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public EnumRegister EmailRegister(UserInfo ui, UserBaseInfo bi, string code, out int userId)
        {
            userId = 0;
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[13];
                param[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                param[0].Direction = ParameterDirection.ReturnValue;
                param[1] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
                param[1].Value = ui.Email;
                param[2] = new SqlParameter("@IP", SqlDbType.Char, 15);
                param[2].Value = JuSNS.Common.Public.GetClientIP();
                param[3] = new SqlParameter("@Password", SqlDbType.NVarChar, 32);
                param[3].Value = ui.Password;
                param[4] = new SqlParameter("@Username", SqlDbType.NVarChar, 50);
                param[4].Value = ui.UserName;
                param[5] = new SqlParameter("@State", SqlDbType.Int);
                param[5].Value = (int)EnumUserState.Normal;
                param[6] = new SqlParameter("@City", SqlDbType.Int);
                param[6].Value = ui.City;
                param[7] = new SqlParameter("@NowTime", SqlDbType.DateTime);
                param[7].Value = DateTime.Now;
                param[8] = new SqlParameter("@EmailItem", SqlDbType.VarChar, 1000);
                param[8].Value = EnumClass.GetEmailNotifyStr();
                param[9] = new SqlParameter("@Code", SqlDbType.Char, 11);
                param[9].Value = code;
                param[10] = new SqlParameter("@PrivDef", SqlDbType.Int);
                param[10].Value = (int)EnumPrivacy.ForWholeSite;
                param[11] = new SqlParameter("@NewUid", SqlDbType.Int);
                param[11].Direction = ParameterDirection.Output;
                param[12] = new SqlParameter("@Sex", SqlDbType.Int);
                param[12].Value = ui.Sex;
                DbHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "NTP_InviterRegister", param);
                int retvalue = (int)param[0].Value;
                switch (retvalue)
                {
                    case 1:
                        userId = (int)param[12].Value;
                        InserDefaultFriend(userId);
                        return EnumRegister.Succeed;
                    case 2:
                        return EnumRegister.InvalidCode;
                    case 3:
                        return EnumRegister.RegInviteCode;
                    case 4:
                        return EnumRegister.EmailRepeat;
                    default:
                        return EnumRegister.UnexpectedError;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }


        /// <summary>
        /// 获得最大UserID
        /// </summary>
        /// <returns></returns>
        public int GetMaxUserID()
        {
            string sql = "select max(userid) as maxid from nt_user";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }


        /// <summary>
        /// 为邀请注册者增加积分
        /// </summary>
        /// <param name="UserID"></param>
        public void GetInvReg(int UserID)
        {
            string sql = "select InviterID from NT_User where UserID=" + UserID;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                int InvID = Convert.ToInt32(dr["InviterID"]);
                if (InvID > 0)
                {
                    UpdateInte(Convert.ToInt32(InvID), 200, 0, 0, "邀请用户加入");
                }
            }
            dr.Close();
        }


        /// <summary>
        /// 增加积分或者金币
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Point">增加数量单位</param>
        /// <param name="Flag">0增加积分，1增加金币</param>
        /// <param name="ifoat">0增加，1减少</param>
        /// <param name="Content">描述</param>
        public void UpdateInte(int UserID, object Point, int Flag, int ifoat, string Content)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                int iPoint = 0;
                int gPoint = 0;
                double Money = 0;
                if (Point.ToString() != "0")
                {
                    switch (Flag)
                    {
                        case 0:
                            if (ifoat == 0)
                            {
                                sql = "update NT_User set integral=integral+" + Point + " where UserID=" + UserID;
                            }
                            else
                            {
                                sql = "update NT_User set integral=integral-" + Point + " where UserID=" + UserID;
                            }
                            iPoint = Convert.ToInt32(Point);
                            break;
                        case 1:
                            if (ifoat == 0)
                            {
                                sql = "update NT_User set inteyb=inteyb+" + Point + " where UserID=" + UserID;
                            }
                            else
                            {
                                sql = "update NT_User set inteyb=inteyb-" + Point + " where UserID=" + UserID;
                            }
                            gPoint = Convert.ToInt32(Point);
                            break;
                        case 2:
                            if (ifoat == 0)
                            {
                                sql = "update NT_User set money=money+" + Point + " where UserID=" + UserID;
                            }
                            else
                            {
                                sql = "update NT_User set money=money-" + Point + " where UserID=" + UserID;
                            }
                            Money = Convert.ToDouble(Point);
                            break;
                    }
                    DateTime GetDate = DateTime.Now;
                    string iSQL = "insert into NT_UserPointHistory(UserID,Point,GPoint,Money,UTF,CreatTime,Content) values(" + UserID + "," + iPoint + "," + gPoint + "," + Money + "," + ifoat + ",'" + GetDate + "','" + Content + "')";
                    if (Content == "登录")
                    {
                        //判断今天是否登录了 。
                        string gSQL = "select count(*) from NT_User where UserID=" + UserID + " and Convert(varchar(10),LastLoginTime,120) = Convert(varchar(10),getDate(),120)";
                        int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, gSQL, null).ToString());
                        if (n == 0)
                        {
                            //插入数据库
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, iSQL, null);
                            DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                        }
                    }
                    else
                    {
                        //插入数据库
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, iSQL, null);
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    }
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }


        /// <summary>
        /// 注册确认,验证后同时设置最后登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="md5Password"></param>
        /// <returns>0成功,-1验证码不正确,-2该用户已通过验证,-3数据修改失败</returns>
        public int RegisterConfirm(string code, string email, out int userId, out string userName, out string md5Password)
        {
            userId = 0;
            userName = string.Empty;
            md5Password = string.Empty;
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                EnumUserState state = EnumUserState.Normal;
                bool f = false;
                int inviter = 0;
                SqlParameter[] Param = new SqlParameter[2];
                Param[0] = new SqlParameter("@VerifyCode", SqlDbType.NVarChar, 12);
                Param[0].Value = code;
                Param[1] = new SqlParameter("@email", SqlDbType.NVarChar, 100);
                Param[1].Value = email;
                string Sql = "select UserID,UserName,Password,State,InviterID from NT_User where VerifyCode=@VerifyCode and Email=@email";
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, Param);
                if (rd.Read())
                {
                    userId = rd.GetInt32(0);
                    userName = rd.GetString(1);
                    md5Password = rd.GetString(2);
                    state = (EnumUserState)rd.GetInt32(3);
                    if (!rd.IsDBNull(4))
                        inviter = rd.GetInt32(4);
                    f = true;
                }
                rd.Close();
                if (!f)
                {
                    return -1;
                }
                if (state == EnumUserState.Normal)
                {
                    return -2;
                }
                bool isEmailNotSend = false;
                if (state == EnumUserState.NormalNotEmail)
                {
                    isEmailNotSend = true;
                }
                Sql = "select count(ID) from NT_Friend where (UserID=" + userId + " and FriendID=" + inviter + ") or (UserID=" + inviter + " and FriendID=" + userId + ")";
                int m = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                DateTime now = DateTime.Now;
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    Sql = "update NT_User set State=" + (int)EnumUserState.Normal + ",LastLoginTime='" + now + "',LastLoginIP='" + JuSNS.Common.Public.GetClientIP() + "',ConfirmTime='" + now + "' where VerifyCode=@VerifyCode";
                    int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, Param);
                    if (!isEmailNotSend)
                    {
                        if (inviter > 0 && m == 0)
                        {
                            Sql = "insert into NT_Friend (UserID,FriendID,State,AddTime) values (" + userId + "," + inviter + ",0,'" + now + "')";
                            n += DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                            Sql = "insert into NT_Friend (UserID,FriendID,State,AddTime) values (" + inviter + "," + userId + ",0,'" + now + "')";
                            n += DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                        }
                    }
                    trans.Commit();
                    if (n > 0)
                    {
                        if (state == 0)
                        {
                            InserDefaultFriend(userId);
                        }
                        return 0;
                    }
                    else
                        return -3;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 电子邮件激活
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ui"></param>
        /// <returns>0成功，1邮件发送失败，不需要激活</returns>
        public int EmailActive(string email, UserInfo ui)
        {
            try
            {
                SqlParameter param = new SqlParameter("@email", email);
                //获取verifyCode
                string sql = "select VerifyCode,state from NT_user where email=@email";
                string verifyCode = string.Empty;
                int stat = 0;
                IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, param);
                if (dr.Read())
                {
                    verifyCode = dr["VerifyCode"].ToString();
                    stat = Convert.ToInt32(dr["state"]);
                }
                dr.Close();
                if (stat == (int)EnumUserState.Normal)
                {
                    return 2;
                }
                JuSNS.Common.EMail em = JuSNS.Common.EMail.CreateInstance();
                string body = EmailConfig.emailactive;
                body = body.Replace("{SiteName}", UiConfig.SiteName);
                body = body.Replace("{UserName}", email);
                body = body.Replace("{Domain}", UiConfig.Domain);
                body = body.Replace("{Date}", DateTime.Now.ToString("yyyy年MM月dd日"));
                body = body.Replace("{Url}", UiConfig.RootUrl + "/Verify" + Public.GetXMLValue("siteExName") + "?do=EmailActive&active=0&r=" + verifyCode + "");
                em.Body = body;
                em.Subject = "尊敬的用户，激活您在" + UiConfig.SiteName + "的邮件帐号";
                em.To = ui.Email;
                em.From = UiConfig.SiteName + "<" + EmailConfig.from + ">";
                em.Send();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// 检查是否具有某项隐私查看权限
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool CheckPrivacyInfo(SqlTransaction trans, int UserID, string Type)
        {
            string sql = "select " + Type + " from NT_usersetting where UserID=" + UserID;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, sql, null));
            if (n == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 插入默认好友
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int InserDefaultFriend(int UserID)
        {
            int riCount = 0;
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                DateTime now = DateTime.Now;
                string dfriend = JuSNS.Common.Public.GetXMLValue("defaultfriend");
                string[] dfriendARR = null;
                if (!string.IsNullOrEmpty(dfriend))
                {
                    dfriendARR = dfriend.Split(',');
                    string sql = string.Empty;
                    for (int j = 0; j < dfriendARR.Length; j++)
                    {
                        if (UserID.ToString() != dfriendARR[j].ToString())
                        {
                            if (!IsFriends(cn, UserID, Convert.ToInt16(dfriendARR[j].ToString())))
                            {
                                sql = "insert into NT_Friend (UserID,FriendID,State,PostTime,FDegree,ClassID) values (" + UserID + "," + dfriendARR[j] + ",0,'" + now + "',0,0)";
                                sql += "insert into NT_Friend (UserID,FriendID,State,PostTime,FDegree,ClassID) values (" + dfriendARR[j] + "," + UserID + ",0,'" + now + "',0,0)";
                                riCount = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return riCount;
        }

        public int InsertFriend(FriendInfo Info, int Flag)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ClassID", SqlDbType.Int);
                param[0].Value = Info.ClassID;
                param[1] = new SqlParameter("@Descript", SqlDbType.NVarChar, 100);
                param[1].Value = Info.Descript;
                param[2] = new SqlParameter("@FDegree", SqlDbType.Int);
                param[2].Value = Info.FDegree;
                param[3] = new SqlParameter("@FriendID", SqlDbType.Int);
                param[3].Value = Info.FriendID;
                param[4] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[4].Value = Info.PostTime;
                param[5] = new SqlParameter("@State", SqlDbType.Int);
                if (Flag == 0)
                {
                    param[5].Value = Info.State;
                }
                else
                {
                    param[5].Value = 0;
                }
                param[6] = new SqlParameter("@UserID", SqlDbType.Int);
                param[6].Value = Info.UserID;
                string sqls = "select count(*) from nt_friend where  UserID=@UserID and FriendID=@FriendID";
                if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sqls, param)) > 0)
                    return -1;
                string sql = "insert into NT_Friend (UserID,FriendID,State,PostTime,FDegree,ClassID,Descript) values (@UserID,@FriendID,@State,@PostTime,1,@ClassID,@Descript)";
                sql += "insert into NT_Friend (UserID,FriendID,State,PostTime,FDegree,ClassID,Descript) values (@FriendID,@UserID,@State,@PostTime,@FDegree,@ClassID,@Descript)";
                return DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        /// <summary>
        /// 判断是否是好友
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="userid"></param>
        /// <param name="friendid"></param>
        /// <returns></returns>
        public bool IsFriends(object userid, object friendid)
        {
            if (userid.ToString() == friendid.ToString()) return false;
            string Sql = "select count(ID) from NT_Friend where UserID=" + userid + " and state=0 and FriendID=" + friendid;
            int n = (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 判断是否是好友
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="userid"></param>
        /// <param name="friendid"></param>
        /// <returns></returns>
        static internal bool IsFriends(SqlConnection cn, object userid, object friendid)
        {
            if (userid.ToString() == friendid.ToString()) return false;
            string Sql = "select count(ID) from NT_Friend where UserID=" + userid + "  and state=0 and FriendID=" + friendid;
            int n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 是否在同一个网络中。
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="userid"></param>
        /// <param name="friendid"></param>
        /// <returns></returns>
        static internal bool IsInSameNetwork(SqlConnection cn, int userid, int friendid)
        {
            string Sql = "select Province from NT_User where UserID=" + userid;
            object obj1 = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
            Sql = "select Province from NT_User where UserID=" + friendid;
            object obj2 = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
            if (obj1 != null && obj1 != DBNull.Value && obj1 == obj2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改所在地
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Province">省份</param>
        /// <param name="City">城市</param>
        /// <returns></returns>
        public int ChangeAddr(int UserID, int Province, int City)
        {
            SqlConnection sqlConn = new SqlConnection(DBConfig.CnString);
            sqlConn.Open();
            SqlTransaction trans = sqlConn.BeginTransaction();
            try
            {
                string sql = "update NT_User set Province=" + Province + ",City=" + City + " where UserID=" + UserID + "";
                int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, null);
                trans.Commit();
                return n;
            }
            catch (SqlException sqlEx)
            {
                trans.Rollback();
                throw sqlEx;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// 保存MSN
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="MSN">MSN帐号</param>
        /// <returns></returns>
        public int ChangeMSN(int UserID, string MSN)
        {
            SqlConnection sqlConn = new SqlConnection(DBConfig.CnString);
            sqlConn.Open();
            SqlTransaction trans = sqlConn.BeginTransaction();
            try
            {
                SqlParameter param = new SqlParameter("@MSN", SqlDbType.NVarChar, 50);
                param.Value = MSN;
                string sql = "update NT_UserInfo set MSN=@MSN where UserID=" + UserID.ToString();
                int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                trans.Commit();
                return n;
            }
            catch (SqlException sqlEx)
            {
                trans.Rollback();
                throw sqlEx;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// 保存GTalk
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="MSN">GTalk帐号</param>
        /// <returns></returns>
        public int ChangeGTalk(int UserID, string GTalk)
        {
            SqlConnection sqlConn = new SqlConnection(DBConfig.CnString);
            sqlConn.Open();
            SqlTransaction trans = sqlConn.BeginTransaction();
            try
            {
                SqlParameter param = new SqlParameter("@GTalk", SqlDbType.NVarChar, 50);
                param.Value = GTalk;
                string sql = "update NT_UserInfo set GTalk=@GTalk where UserID=" + UserID.ToString();
                int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                trans.Commit();
                return n;
            }
            catch (SqlException sqlEx)
            {
                trans.Rollback();
                throw sqlEx;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// 保存Mobile
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="MSN">Mobile帐号</param>
        /// <returns></returns>
        public int ChangeMobile(int UserID, string Mobile)
        {
            SqlConnection sqlConn = new SqlConnection(DBConfig.CnString);
            sqlConn.Open();
            SqlTransaction trans = sqlConn.BeginTransaction();
            try
            {
                SqlParameter param = new SqlParameter("@Mobile", SqlDbType.NChar, 15);
                param.Value = Mobile;
                string sql1 = "select count(userid) from nt_user where mobile=@Mobile and UserID!=" + UserID;
                object m = DbHelper.ExecuteScalar(trans, CommandType.Text, sql1, param);
                if (Convert.ToInt32(m) > 0) return -2;
                string sql = "update NT_User set Mobile=@Mobile,BindMoblie=0 where UserID=" + UserID;
                int n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                trans.Commit();
                return n;
            }
            catch (SqlException sqlEx)
            {
                trans.Rollback();
                throw sqlEx;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// 开始登录
        /// </summary>
        /// <param name="InputSTR">需要验证的字段</param>
        /// <param name="password">密码</param>
        /// <param name="userId">返回的UserID</param>
        /// <param name="userName">用户名</param>
        /// <param name="loginNum">登录次数</param>
        /// <param name="LoginType">0电子邮件，1用户名，2手机</param>
        /// <returns></returns>
        public EnumLoginState Login(string InputSTR, string password, out int UserId, out string UserName, out string TrueName, out int LoginNum, int LoginType)
        {
            UserId = -1;
            UserName = string.Empty;
            TrueName = string.Empty;
            LoginNum = 0;
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string pwd = string.Empty;
                EnumUserState us = EnumUserState.Lock;
                SqlParameter param = new SqlParameter("@InputSTR", InputSTR);
                bool exist = false;
                string Sql = string.Empty;
                switch (LoginType)
                {
                    case 0:
                        Sql = "select UserID,UserName,TrueName,Password,State,Portrait,loginTimes from NT_User where Email=@InputSTR";
                        break;
                    case 1:
                        Sql = "select UserID,UserName,TrueName,Password,State,Portrait,loginTimes from NT_User where UserName=@InputSTR";
                        break;
                    case 2:
                        Sql = "select UserID,UserName,TrueName,Password,State,Portrait,loginTimes from NT_User where Mobile=@InputSTR";
                        break;
                }
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, param);
                if (rd.Read())
                {
                    UserId = Convert.ToInt32(rd["UserID"]);
                    UserName = rd["UserName"].ToString();
                    TrueName = rd["TrueName"].ToString();
                    pwd = rd["Password"].ToString();
                    LoginNum = Convert.ToInt32(rd["loginTimes"]);
                    us = (EnumUserState)Convert.ToInt32(rd["State"]);
                    exist = true;
                }
                rd.Close();
                if (!exist || pwd == string.Empty || pwd != password)
                {
                    return EnumLoginState.Err_NameOrPwdError;
                }
                if (us == EnumUserState.Register)
                {
                    if (UiConfig.RegVer == "1")
                    {
                        return EnumLoginState.Err_UnActivation;
                    }
                }
                else if (us == EnumUserState.Lock)
                {
                    return EnumLoginState.Err_Locked;
                }
                //增加成长记录
                GetMemberLevels(UserId, cn);
                UpdateInte(UserId, Public.JSplit(2), 0, 0, "登录");
                Sql = "update NT_User set LastLoginTime='" + DateTime.Now + "',LastLoginIP='" + JuSNS.Common.Public.GetClientIP() + "',LoginTimes=LoginTimes+1 where UserID=" + UserId;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, null);
                JoinVipInfo jmdl = GetVipInfo(UserId); 
                if (jmdl == null)
                {
                    UpdateVip(UserId, 0);
                }
                else if (jmdl.IsLock == (byte)EnumCusState.ForNormal)
                {
                    DateTime ntime = DateTime.Now;
                    DateTime etime = jmdl.EndTime;
                    TimeSpan ts= ntime - etime;
                    if ((ts).Days > 0)
                    {
                        UpdateVip(UserId, 0);
                        InsertNotice(new NoticeInfo(0, UserId, UserId, "您VIP已经到期！", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.VipTimeout, 0));
                    }
                }
                return EnumLoginState.Succeed;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public void GetMemberLevels(int userid,SqlConnection cn)
        {
            if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from NT_User where UserID=" + userid + " and Convert(varchar(10),LastLoginTime,120) = Convert(varchar(10),getDate(),120)", null)) == 0)
            {
                int n = 1;
                //是否是VIP会员
                bool isvip = Convert.ToBoolean(DbHelper.ExecuteScalar(cn, CommandType.Text, "select isvip from nt_user where userid=" + userid, null));
                if (isvip)
                {
                    n = Convert.ToInt32(Public.GetXMLValue("loginStar", "~/config/base/vip.config"));
                }
                string sql = "update nt_user set MemberLevels=MemberLevels+" + n + " where userid=" + userid;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
            }
        }

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public EnumLoginState CheckLogin(int userId, string password, out int loginNum)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int lNum = 0;
                string pwd = string.Empty;
                EnumUserState us = EnumUserState.Lock;
                bool exist = false;
                string Sql = "select Password,State,LoginTimes from NT_User where UserID=" + userId;
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                if (rd.Read())
                {
                    pwd = rd.GetString(0);
                    us = (EnumUserState)rd.GetInt32(1);
                    exist = true;
                    lNum = rd.GetInt32(2);
                }
                loginNum = lNum;
                rd.Close();
                if (!exist || pwd == string.Empty || pwd != password)
                    return EnumLoginState.Err_NameOrPwdError;
                if (us == EnumUserState.Register)
                    return EnumLoginState.Err_UnActivation;
                else if (us == EnumUserState.Lock)
                    return EnumLoginState.Err_Locked;
                return EnumLoginState.Succeed;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 获取用户基本信息实体类
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public UserBaseInfo GetUserBaseInfo(object UserID)
        {
            UserBaseInfo UBasicInfo = new UserBaseInfo();
            string sql = "select * from NT_UserInfo where UserID=" + UserID + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                if (rd["Birthday"] != DBNull.Value) UBasicInfo.Birthday = (DateTime)rd["Birthday"];
                if (rd["BirthidayDisplay"] != DBNull.Value) UBasicInfo.BirthidayDisplay = (int)rd["BirthidayDisplay"];
                if (rd["Constellation"] != DBNull.Value) UBasicInfo.Constellation = (int)rd["Constellation"];
                UBasicInfo.MSN = Convert.ToString(rd["MSN"]);
                UBasicInfo.QQ = Convert.ToString(rd["QQ"]);
                if (rd["MobilePrivacy"] != DBNull.Value) UBasicInfo.MobilePrivacy = Convert.ToByte(rd["MobilePrivacy"]);
                if (rd["EmailPrivacy"] != DBNull.Value) UBasicInfo.EmailPrivacy = Convert.ToByte(rd["EmailPrivacy"]);
                UBasicInfo.Tel = Convert.ToString(rd["Tel"]);
                UBasicInfo.Addr = Convert.ToString(rd["Addr"]);
                UBasicInfo.WebSite = Convert.ToString(rd["WebSite"]);
                UBasicInfo.Favourite = Convert.ToString(rd["Favourite"]);
                UBasicInfo.FavMusics = Convert.ToString(rd["FavMusics"]);
                UBasicInfo.FavMovies = Convert.ToString(rd["FavMovies"]);
                UBasicInfo.FavCartoons = Convert.ToString(rd["FavCartoons"]);
                UBasicInfo.FavGames = Convert.ToString(rd["FavGames"]);
                UBasicInfo.FavSports = Convert.ToString(rd["FavSports"]);
                UBasicInfo.FavBooks = Convert.ToString(rd["FavBooks"]);
                UBasicInfo.FavAdages = Convert.ToString(rd["FavAdages"]);
                UBasicInfo.AppreciateMen = Convert.ToString(rd["AppreciateMen"]);
                UBasicInfo.Presentation = Convert.ToString(rd["Presentation"]);
                if (rd["Vocation"] != DBNull.Value) UBasicInfo.Vocation = (int)rd["Vocation"];
                if (rd["MSNPrivacy"] != DBNull.Value) UBasicInfo.MSNPrivacy = (int)rd["MSNPrivacy"];
                if (rd["QQPrivacy"] != DBNull.Value) UBasicInfo.QQPrivacy = (int)rd["QQPrivacy"];
                if (rd["TelPrivacy"] != DBNull.Value) UBasicInfo.TelPrivacy = (int)rd["TelPrivacy"];
                if (rd["AddrPrivacy"] != DBNull.Value) UBasicInfo.AddrPrivacy = (int)rd["AddrPrivacy"];
                if (rd["WebSitePrivacy"] != DBNull.Value) UBasicInfo.WebSitePrivacy = (int)rd["WebSitePrivacy"];
                if (rd["HomeCity"] != DBNull.Value) UBasicInfo.HomeCity = (int)rd["HomeCity"];
            }
            UBasicInfo.UserID = Convert.ToInt32(UserID);
            rd.Close();
            return UBasicInfo;
        }

        public UserSettingInfo GetUserSettingInfo(object userid)
        {
            UserSettingInfo uset = new UserSettingInfo();
            string sql = "select * from NT_UserSetting where UserID=" + userid;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                uset.ActAddFriend = Convert.ToBoolean(rd["ActAddFriend"]);
                uset.ActDeliver = Convert.ToBoolean(rd["ActDeliver"]);
                uset.ActLeaveWord = Convert.ToBoolean(rd["ActLeaveWord"]);
                uset.ActLogComment = Convert.ToBoolean(rd["ActLogComment"]);
                uset.ActMovieComment = Convert.ToBoolean(rd["ActMovieComment"]);
                uset.ActPhotoLasso = Convert.ToBoolean(rd["ActPhotoLasso"]);
                uset.ActPicComment = Convert.ToBoolean(rd["ActPicComment"]);
                uset.ActSecede = Convert.ToBoolean(rd["ActSecede"]);
                uset.ActShareComment = Convert.ToBoolean(rd["ActShareComment"]);
                uset.ActUpdateData = Convert.ToBoolean(rd["ActUpdateData"]);
                uset.LastPostIP = rd["LastPostIP"].ToString();
                uset.LastPostTime = Convert.ToDateTime(rd["LastPostTime"]);
                uset.PrivEducate = Convert.ToInt32(rd["PrivEducate"]);
                uset.PrivFavourite = Convert.ToInt32(rd["PrivFavourite"]);
                uset.PrivFriends = Convert.ToInt32(rd["PrivFriends"]);
                uset.PrivGroup = Convert.ToInt32(rd["PrivGroup"]);
                uset.PrivLasso = Convert.ToInt32(rd["PrivLasso"]);
                uset.PrivLeaveWord = Convert.ToInt32(rd["PrivLeaveWord"]);
                uset.PrivMiniBlog = Convert.ToInt32(rd["PrivMiniBlog"]);
                uset.PrivMovies = Convert.ToInt32(rd["PrivMovies"]);
                uset.PrivShare = Convert.ToInt32(rd["PrivShare"]);
                uset.PrivSpace = Convert.ToInt32(rd["PrivSpace"]);
                uset.UserID = Convert.ToInt32(userid);
            }
            rd.Close();
            return uset;
        }


        public string GetUserHeadPic(int userid, out int sex)
        {
            SqlParameter param = new SqlParameter("@UserID", userid);
            string sql = "select a.FilePath,b.sex from Nt_Photo a inner join NT_User b on a.UserID=b.UserID where b.UserID=@UserID and b.Portrait=a.Id order by id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, param);
            string filepath = string.Empty;
            sex = 0;
            if (dr.Read())
            {
                filepath = dr[0].ToString();
                sex = (byte)dr[1];
            }
            else
            {
                sex = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "select sex from nt_user where userid=" + userid, null));
            }
            dr.Close();
            return filepath;
        }

        public int UpdateUserHead(int PhotoID, int UserID)
        {
            string sql = "update nt_user set Portrait=" + PhotoID + " where UserID=" + UserID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertCareer(CareerInfo Info, int Flag)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Company", SqlDbType.NVarChar, 10);
            param[0].Value = Info.Company;
            param[1] = new SqlParameter("@LeaveTime", SqlDbType.NVarChar, 10);
            param[1].Value = Info.LeaveTime;
            param[2] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[2].Value = Info.PostTime;
            param[3] = new SqlParameter("@JoinTime", SqlDbType.NVarChar, 10);
            param[3].Value = Info.JoinTime;
            param[4] = new SqlParameter("@UserID", SqlDbType.Int);
            param[4].Value = Info.UserID;
            param[5] = new SqlParameter("@ID", SqlDbType.Int);
            param[5].Value = Info.ID;
            string sql = string.Empty;
            if (Flag == 1)
            {
                sql = "update NT_UserCareer set Company=@Company,JoinTime=@JoinTime,LeaveTime=@LeaveTime where UserID=@UserID and ID=@ID";
            }
            else
            {
                sql = "insert into NT_UserCareer(UserID, Company, JoinTime, PostTime, LeaveTime)values(@UserID, @Company, @JoinTime, @PostTime, @LeaveTime)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public CareerInfo GetCareerInfo(object cid)
        {
            CareerInfo uInfo = new CareerInfo();
            string sql = "select ID, UserID, Company, JoinTime, PostTime, LeaveTime from NT_UserCareer where ID=" + cid + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                uInfo.ID = Convert.ToInt32(rd["ID"]);
                uInfo.LeaveTime = rd["LeaveTime"].ToString();
                if (rd["PostTime"] != DBNull.Value) uInfo.PostTime = Convert.ToDateTime(rd["PostTime"]);
                uInfo.JoinTime = rd["JoinTime"].ToString();
                uInfo.Company = rd["Company"].ToString();
                uInfo.UserID = Convert.ToInt32(rd["UserID"]);
            }
            rd.Close();
            return uInfo;
        }

        public int DeleteCareer(object uid, object cid)
        {
            string sql = "delete from NT_UserCareer where userid=" + uid + " and id=" + cid + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertEducation(EducationInfo Info, int Flag)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Leaveyear", SqlDbType.NVarChar, 10);
            param[0].Value = Info.Leaveyear;
            param[1] = new SqlParameter("@Levels", SqlDbType.TinyInt);
            param[1].Value = Info.Levels;
            param[2] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[2].Value = Info.PostTime;
            param[3] = new SqlParameter("@SchoolName", SqlDbType.NVarChar, 50);
            param[3].Value = Info.SchoolName;
            param[4] = new SqlParameter("@UserID", SqlDbType.Int);
            param[4].Value = Info.UserID;
            param[5] = new SqlParameter("@ID", SqlDbType.Int);
            param[5].Value = Info.ID;
            string sql = string.Empty;
            if (Flag == 1)
            {
                sql = "update NT_UserEducation set schoolName=@SchoolName,Leaveyear=@Leaveyear,Levels=@Levels where UserID=@UserID and ID=@ID";
            }
            else
            {
                sql = "insert into NT_UserEducation(UserID, schoolName, leaveyear, PostTime, levels)values(@UserID, @SchoolName, @Leaveyear, @PostTime, @Levels)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public EducationInfo GetEducationInfo(object eid)
        {
            EducationInfo uInfo = new EducationInfo();
            string sql = "select ID, UserID, schoolName, leaveyear, PostTime, levels from NT_UserEducation where ID=" + eid + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                uInfo.ID = Convert.ToInt32(rd["ID"]);
                uInfo.Leaveyear = rd["Leaveyear"].ToString();
                if (rd["PostTime"] != DBNull.Value) uInfo.PostTime = Convert.ToDateTime(rd["PostTime"]);
                uInfo.Levels = Convert.ToByte(rd["Levels"]);
                uInfo.SchoolName = rd["SchoolName"].ToString();
                uInfo.UserID = Convert.ToInt32(rd["UserID"]);
            }
            rd.Close();
            return uInfo;
        }

        public int DeleteEducation(object uid, object eid)
        {
            string sql = "delete from NT_UserEducation where userid=" + uid + " and id=" + eid + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int UpdateLike(UserBaseInfo basi)
        {
            SqlConnection sqlConn = new SqlConnection(DBConfig.CnString);
            sqlConn.Open();
            SqlTransaction trans = sqlConn.BeginTransaction();
            try
            {
                SqlParameter[] param = null;
                int n = 0;
                SqlParameter paramID = new SqlParameter("@ID", basi.UserID);
                string sql = "select UserID from NT_UserInfo where UserID=@ID";
                DataTable dt = DbHelper.ExecuteTable(CommandType.Text, sql, paramID);//是否存在该会员记录
                string DynName = string.Empty;
                param = getFavouriteParameters(basi);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sql = "update NT_UserInfo set Favourite=@Favourite,FavMusics=@FavMusics,FavMovies=@FavMovies,FavCartoons=@FavCartoons,FavGames=@FavGames,FavSports=@FavSports,FavBooks=@FavBooks,FavAdages=@FavAdages,AppreciateMen=@AppreciateMen,Presentation=@Presentation where UserID=@UserID";
                    n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    trans.Commit();
                    dt.Clear(); dt.Dispose();
                }
                else
                {
                    sql = "insert into NT_UserInfo(UserID,Favourite,FavMusics,FavMovies,FavCartoons,FavGames,FavSports,FavBooks,FavAdages,AppreciateMen,Presentation) values (@UserID,@Favourite,@FavMusics,@FavMovies,@FavCartoons,@FavGames,@FavSports,@FavBooks,@FavAdages,@AppreciateMen,@Presentation)";
                    n = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    trans.Commit();
                }
                return n;
            }
            catch (SqlException sqlEx)
            {
                trans.Rollback();
                throw sqlEx;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Info">爱好信息</param>
        /// <returns>返回SQL参数</returns>
        private SqlParameter[] getFavouriteParameters(JuSNS.Model.UserBaseInfo Info)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            param[0].Value = Info.UserID;
            param[1] = new SqlParameter("@Favourite", SqlDbType.NVarChar, 200);
            param[1].Value = Info.Favourite;
            param[2] = new SqlParameter("@FavMusics", SqlDbType.NVarChar, 200);
            param[2].Value = Info.FavMusics;
            param[3] = new SqlParameter("@FavMovies", SqlDbType.NVarChar, 200);
            param[3].Value = Info.FavMovies;
            param[4] = new SqlParameter("@FavCartoons", SqlDbType.NVarChar, 200);
            param[4].Value = Info.FavCartoons;
            param[5] = new SqlParameter("@FavGames", SqlDbType.NVarChar, 200);
            param[5].Value = Info.FavGames;
            param[6] = new SqlParameter("@FavSports", SqlDbType.NVarChar, 200);
            param[6].Value = Info.FavSports;

            param[7] = new SqlParameter("@FavBooks", SqlDbType.NVarChar, 200);
            param[7].Value = Info.FavBooks;
            param[8] = new SqlParameter("@FavAdages", SqlDbType.NVarChar, 200);
            param[8].Value = Info.FavAdages;
            param[9] = new SqlParameter("@AppreciateMen", SqlDbType.NVarChar, 200);
            param[9].Value = Info.AppreciateMen;
            param[10] = new SqlParameter("@Presentation", SqlDbType.NVarChar, 200);
            param[10].Value = Info.Presentation;
            return param;
        }

        /// <summary>
        /// 取得教育和工作的隐私设置
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private EnumPrivacy GetWorkAndEduPrivacy(SqlConnection cn, int userid)
        {
            SqlParameter param = new SqlParameter("@userid", userid);
            string Sql = "select PrivEducate from NT_UserSetting where UserID=@userid";
            return (EnumPrivacy)Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
        }


        /// <summary>
        /// 取回密码
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="loginType">0电子邮件，1手机</param>
        /// <returns>0,成功,-2,邮箱无效,-3邮件发送失败</returns>
        public int Retrieve(string email, int loginType)
        {
            string userName = string.Empty;
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string Sql = "select UserName from NT_User where Email=@Email";
                if (loginType == 1)
                {
                    Sql = "select a.UserName from NT_User a inner join NT_UserInfo b on b.UserID=a.UserID where b.Mobile=@Email";
                }
                object obj = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, new SqlParameter("@Email", email));
                if (obj == null || obj == DBNull.Value)
                {
                    return -2;
                }

                userName = (string)obj;
                string code = JuSNS.Common.Rand.Str(15);
                Sql = "insert into NT_RetrievePwd (Email,PostTime,PostIP,State,ValidCode) values (@Email,'" + DateTime.Now + "','" + JuSNS.Common.Public.GetClientIP() + "',0,@ValidCode)";
                SqlParameter[] Param = new SqlParameter[2];
                Param[0] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
                Param[0].Value = email;
                Param[1] = new SqlParameter("@ValidCode", SqlDbType.Char, 15);
                Param[1].Value = code;
                try
                {
                    if (loginType == 0)
                    {
                        #region 发送邮件
                        JuSNS.Common.EMail eml = JuSNS.Common.EMail.CreateInstance();
                        eml.To = email;
                        eml.Subject = "亲爱的" + userName + "，你申请了重设" + UiConfig.SiteName + "的账户密码";
                        string body = EmailConfig.retrieve;
                        body = body.Replace("{SiteName}", UiConfig.SiteName);
                        body = body.Replace("{UserName}", userName);
                        body = body.Replace("{Domain}", UiConfig.Domain);
                        body = body.Replace("{Date}", DateTime.Now.ToString("yyyy年MM月dd日"));
                        body = body.Replace("{Url}", UiConfig.RootUrl + "/Verify" + Public.GetXMLValue("siteExName") + "?do=Repwd&uname=" + userName + "&vcode=" + code.ToLower());
                        eml.Body = body;
                        eml.Send();
                        #endregion
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, Param);
                    }
                    else
                    {
                        //手机接口获取密码。

                    }
                    return 0;
                }
                catch
                {
                    return -3;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }


        /// <summary>
        /// 重设密码,并设为已录录状态
        /// </summary>
        /// <param name="newPwd">加密过的新密码</param>
        /// <param name="code"></param>
        /// <returns>0成功，-1没有找到相关的申请记录,-2该申请已完成重设而无效,-3邮箱无效</returns>
        public int ResetPwd(string newPwd, string code, out int userId, out string userName, out string TrueName, out string userPortrait)
        {
            userId = 0;
            userName = string.Empty;
            TrueName = string.Empty;
            userPortrait = string.Empty;
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                bool f = false;
                int state = -1;
                int id = 0;
                string email = string.Empty;
                SqlParameter cd = new SqlParameter("@code", code);
                string Sql = "select ID,Email,State from NT_RetrievePwd where ValidCode=@code";
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, cd);
                if (rd.Read())
                {
                    id = rd.GetInt32(0);
                    email = rd.GetString(1);
                    state = rd.GetInt32(2);
                    f = true;
                }
                rd.Close();
                if (!f)
                {
                    return -1;
                }
                if (state != 0)
                    return -2;
                f = false;
                SqlParameter Param = new SqlParameter("@Email", email);
                Sql = "select a.UserID,a.UserName,b.FilePath,a.TrueName from NT_User a left join NT_Photo b on a.Portrait=b.PhotoID where a.Email=@Email";
                rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, Param);
                if (rd.Read())
                {
                    userId = rd.GetInt32(0);
                    userName = rd.GetString(1);
                    if (!rd.IsDBNull(2)) userPortrait = rd.GetString(2);
                    TrueName = rd.GetString(3);
                    f = true;
                }
                rd.Close();
                if (!f)
                    return -3;
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    Sql = "update NT_User set Password='" + newPwd + "',LastLoginTime='" + DateTime.Now + "',LastLoginIP='" + JuSNS.Common.Public.GetClientIP() + "',LoginTimes=LoginTimes+1 where Email=@Email";
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, Param);
                    Sql = "update NT_RetrievePwd set State=1,ConfirmTime='" + DateTime.Now + "',ConfirmIP='" + JuSNS.Common.Public.GetClientIP() + "' where ID=" + id;
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                    trans.Commit();
                    return 0;
                }
                catch
                {
                    trans.Rollback();
                    return -3;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 重置密码验证是否已经有记录
        /// </summary>
        /// <param name="r"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool GetRestPwdRecord(string r, string email)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@email", SqlDbType.NVarChar, 100);
            param[0].Value = email;
            param[1] = new SqlParameter("@code", SqlDbType.NVarChar, 15);
            param[1].Value = r;
            string Sql = "select count(ID) from NT_RetrievePwd where ValidCode=@code and email=@email";
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param).ToString());
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 取得隐私信息
        /// </summary>
        /// <param name="UserID">用户ID信息</param>
        /// <returns></returns>
        public PrivacyInfo GetPrivacy(int UserID)
        {
            string sql = "select UserID,PrivSpace,PrivFavourite,PrivEducate,PrivLasso,PrivFriends,PrivLeaveWord,PrivMiniBlog,PrivShare,PrivGroup,PrivMovies,ActUpdateData,ActAddFriend,ActLeaveWord,ActPicComment,ActSecede,ActDeliver,ActLogComment,ActMovieComment,ActPhotoLasso,ActShareComment,LastPostTime,LastPostIP from NT_UserSetting where UserID=" + UserID;
            PrivacyInfo pri = new PrivacyInfo();
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                pri.UserID = Convert.ToInt32(rd["UserID"]);
                pri.PrivSpace = Convert.ToInt32(rd["PrivSpace"]);
                pri.PrivFavourite = Convert.ToInt32(rd["PrivFavourite"]);
                pri.PrivEducate = Convert.ToInt32(rd["PrivEducate"]);
                pri.PrivLasso = Convert.ToInt32(rd["PrivLasso"]);
                pri.PrivFriends = Convert.ToInt32(rd["PrivFriends"]);
                pri.PrivLeaveWord = Convert.ToInt32(rd["PrivLeaveWord"]);
                pri.PrivMiniBlog = Convert.ToInt32(rd["PrivMiniBlog"]);
                pri.PrivShare = Convert.ToInt32(rd["PrivShare"]);
                pri.PrivGroup = Convert.ToInt32(rd["PrivGroup"]);
                pri.PrivMovies = Convert.ToInt32(rd["PrivMovies"]);
                pri.ActUpdateData = Convert.ToBoolean(rd["ActUpdateData"]);
                pri.ActAddFriend = Convert.ToBoolean(rd["ActAddFriend"]);
                pri.ActLeaveWord = Convert.ToBoolean(rd["ActLeaveWord"]);
                pri.ActPicComment = Convert.ToBoolean(rd["ActPicComment"]);
                pri.ActSecede = Convert.ToBoolean(rd["ActSecede"]);
                pri.ActDeliver = Convert.ToBoolean(rd["ActDeliver"]);
                pri.ActLogComment = Convert.ToBoolean(rd["ActLogComment"]);
                pri.ActMovieComment = Convert.ToBoolean(rd["ActMovieComment"]);
                pri.ActPhotoLasso = Convert.ToBoolean(rd["ActPhotoLasso"]);
                pri.ActShareComment = Convert.ToBoolean(rd["ActShareComment"]);
                pri.LastPostTime = Convert.ToDateTime(rd["LastPostTime"]);
                pri.LastPostIP = Convert.ToString(rd["LastPostIP"]);
            }
            else
            {
                pri.UserID = UserID;
            }
            rd.Close();
            return pri;
        }

        /// <summary>
        /// 设置隐私信息
        /// 返回0表示失败
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="privacyinfo">隐私信息</param>
        /// <returns></returns>
        public int SetPrivacy(int UserID, UserSettingInfo privacyinfo)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CnString);
            Conn.Open();

            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                SqlParameter[] param = GetPrivacyParm(privacyinfo);
                string sql = "select Count(*) from NT_UserSetting where UserID=" + UserID;
                if (Convert.ToInt32(DbHelper.ExecuteScalar(trans, CommandType.Text, sql, null)) > 0)
                {
                    sql = "update NT_UserSetting set PrivSpace=@PrivSpace,PrivFavourite=@PrivFavourite,PrivEducate=@PrivEducate,PrivLasso=@PrivLasso,PrivFriends=@PrivFriends,PrivLeaveWord=@PrivLeaveWord,PrivMiniBlog=@PrivMiniBlog,"
                    + "PrivShare=@PrivShare,PrivGroup=@PrivGroup,PrivMovies=@PrivMovies,ActUpdateData=@ActUpdateData,ActAddFriend=@ActAddFriend,ActLeaveWord=@ActLeaveWord,ActPicComment=@ActPicComment,ActSecede=@ActSecede,ActDeliver=@ActDeliver,ActLogComment=@ActLogComment,"
                    + "ActMovieComment=@ActMovieComment,ActPhotoLasso=@ActPhotoLasso,ActShareComment=@ActShareComment,LastPostTime=@LastPostTime,LastPostIP=@LastPostIP where UserID=@UserID";
                }
                else
                {
                    sql = "insert into NT_UserSetting (PrivSpace,PrivFavourite,PrivEducate,PrivLasso,PrivFriends,PrivLeaveWord,PrivMiniBlog,PrivShare,PrivGroup,PrivMovies,ActUpdateData,ActAddFriend,ActLeaveWord,ActPicComment,ActSecede,ActDeliver,ActLogComment,ActMovieComment,ActPhotoLasso,ActShareComment,LastPostTime,LastPostIP,UserID) values(@PrivSpace,@PrivFavourite,@PrivEducate,@PrivLasso,@PrivFriends,@PrivLeaveWord,@PrivMiniBlog,@PrivShare,@PrivGroup,@PrivMovies,@ActUpdateData,@ActAddFriend,@ActLeaveWord,@ActPicComment,@ActSecede,@ActDeliver,@ActLogComment,@ActMovieComment,@ActPhotoLasso,@ActShareComment,@LastPostTime,@LastPostIP,@UserID)";
                }

                int rint = DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                trans.Commit();
                return rint;
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

        public int DeleteFriend(object fid, object uid)
        {
            string sql = "delete nt_friend where UserID=" + uid + " and FriendID=" + fid + ";";
            sql += "delete nt_friend where UserID=" + fid + " and FriendID=" + uid + ";";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        /// <summary>
        /// 得到好友分类
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetFriendClass(int UserID)
        {
            string sql = "select id,CName,userid from nt_friendclass where UserID=" + UserID + " or UserID=0";
            return DbHelper.ExecuteTable(CommandType.Text, sql, null);
        }

        public int InsertFriendClass(object cname, object uid, int fid)
        {
            string sql = string.Empty;
            if (fid == 0)
            {
                sql = "insert into nt_friendclass(cname,userid)values('" + cname + "'," + uid + ")";
            }
            else
            {
                sql = "update nt_friendclass set cname='" + cname + "' where userid=" + uid + " and id=" + fid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DeleteFriendClass(object fid, object uid)
        {
            string sql = "delete from nt_friendclass where id=" + fid + " and userid=" + uid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int UpdateFriendClass(object fid, object cid, object uid)
        {
            string sql = "update nt_friend set classid=" + cid + " where userid=" + uid + " and friendid=" + fid + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public FriendInfo GetFriendInfo(object fid)
        {
            FriendInfo fInfo = new FriendInfo();
            string sql = "select ID, UserID, FriendID, State, descript, PostTime, ClassID, FDegree from NT_Friend where ID=" + fid + "";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                fInfo.ClassID = Convert.ToInt32(dr["ClassID"]);
                fInfo.Descript = dr["descript"].ToString();
                fInfo.FDegree = Convert.ToInt32(dr["FDegree"].ToString());
                fInfo.FriendID = Convert.ToInt32(dr["FriendID"].ToString());
                fInfo.ID = Convert.ToInt32(fid);
                fInfo.PostTime = Convert.ToDateTime(dr["PostTime"].ToString());
                fInfo.State = Convert.ToInt32(dr["State"].ToString());
                fInfo.UserID = Convert.ToInt32(dr["UserID"].ToString());
            }
            dr.Close();
            return fInfo;
        }

        public string GetFriendList(object UserID)
        {
            string listSTR = "0";
            string sql = "select FriendID from NT_Friend where UserID=" + UserID + " and state=0";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                listSTR += "," + dr["FriendID"];
            }
            dr.Close();
            return listSTR;
        }

        /// <summary>
        /// 隐私信息参数
        /// </summary>
        /// <param name="pri">隐私信息</param>
        /// <returns>sql参数</returns>
        private SqlParameter[] GetPrivacyParm(UserSettingInfo pri)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@UserID", SqlDbType.Int);
            param[0].Value = pri.UserID;
            param[1] = new SqlParameter("@PrivSpace", SqlDbType.Int);
            param[1].Value = pri.PrivSpace;
            param[2] = new SqlParameter("@PrivFavourite", SqlDbType.Int);
            param[2].Value = pri.PrivFavourite;
            param[3] = new SqlParameter("@PrivEducate", SqlDbType.Int);
            param[3].Value = pri.PrivEducate;
            param[4] = new SqlParameter("@PrivLasso", SqlDbType.Int);
            param[4].Value = pri.PrivLasso;
            param[5] = new SqlParameter("@PrivFriends", SqlDbType.Int);
            param[5].Value = pri.PrivFriends;
            param[6] = new SqlParameter("@PrivLeaveWord", SqlDbType.Int);
            param[6].Value = pri.PrivLeaveWord;
            param[7] = new SqlParameter("@PrivMiniBlog", SqlDbType.Int);
            param[7].Value = pri.PrivMiniBlog;
            param[8] = new SqlParameter("@PrivShare", SqlDbType.Int);
            param[8].Value = pri.PrivShare;
            param[9] = new SqlParameter("@PrivGroup", SqlDbType.Int);
            param[9].Value = pri.PrivGroup;
            param[10] = new SqlParameter("@PrivMovies", SqlDbType.Int);
            param[10].Value = pri.PrivMovies;
            param[11] = new SqlParameter("@ActUpdateData", SqlDbType.Bit);
            param[11].Value = pri.ActUpdateData;
            param[12] = new SqlParameter("@ActAddFriend", SqlDbType.Bit);
            param[12].Value = pri.ActAddFriend;
            param[13] = new SqlParameter("@ActLeaveWord", SqlDbType.Bit);
            param[13].Value = pri.ActLeaveWord;
            param[14] = new SqlParameter("@ActPicComment", SqlDbType.Bit);
            param[14].Value = pri.ActPicComment;
            param[15] = new SqlParameter("@ActSecede", SqlDbType.Bit);
            param[15].Value = pri.ActSecede;
            param[16] = new SqlParameter("@ActDeliver", SqlDbType.Bit);
            param[16].Value = pri.ActDeliver;
            param[17] = new SqlParameter("@ActLogComment", SqlDbType.Bit);
            param[17].Value = pri.ActLogComment;
            param[18] = new SqlParameter("@ActMovieComment", SqlDbType.Bit);
            param[18].Value = pri.ActMovieComment;
            param[19] = new SqlParameter("@ActPhotoLasso", SqlDbType.Bit);
            param[19].Value = pri.ActPhotoLasso;
            param[20] = new SqlParameter("@ActShareComment", SqlDbType.Bit);
            param[20].Value = pri.ActShareComment;
            param[21] = new SqlParameter("@LastPostTime", SqlDbType.DateTime, 8);
            param[21].Value = pri.LastPostTime;
            param[22] = new SqlParameter("@LastPostIP", SqlDbType.NChar, 15);
            param[22].Value = pri.LastPostIP;
            return param;
        }


        public DataTable GetUserFriendList(int Number, int UserID, int City, int Sex)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = string.Empty;
                //取得朋友列表
                string resultSTR = "0,";
                string FriendSQL = "select friendid from nt_friend where UserID=" + UserID;
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, FriendSQL, null);
                while (dr.Read())
                {
                    if (IsFriends(UserID, dr["friendid"]))
                    {
                        resultSTR += dr["friendid"] + ",";
                    }
                }
                dr.Close();
                resultSTR = Input.FixCommaStr(resultSTR);
                string whereSTR = string.Empty;
                if (City > 0)
                {
                    whereSTR = " and city=" + City + "";
                }
                if (Sex == -1)
                {
                    sql = "select top " + Number + " userid,username,truename,city,sex from nt_user where UserID<>" + UserID + " and userid not in (" + resultSTR + ") " + whereSTR + " order by newid()";
                }
                else
                {
                    sql = "select top " + Number + " userid,username,truename,city,sex from nt_user where sex=" + Sex + " and UserID<>" + UserID + " and userid not in (" + resultSTR + ")  " + whereSTR + " order by newid()";
                }
                return DbHelper.ExecuteTable(CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
        }

        public DataTable GetUserPossibleList(int Number, int UserID, string LastIP)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                //取得朋友列表
                string resultSTR = "0,";
                string resultSTR1 = "0,";
                string FriendSQL = "select friendid from nt_friend where UserID=" + UserID;
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, FriendSQL, null);
                while (dr.Read())
                {
                    if (!IsFriends(UserID, dr["friendid"]))
                    {
                        resultSTR += dr["friendid"] + ",";
                    }
                    else
                    {
                        resultSTR1 += dr["friendid"] + ",";
                    }
                }
                dr.Close();
                resultSTR = Input.FixCommaStr(resultSTR);
                resultSTR1 = Input.FixCommaStr(resultSTR1);
                string whereSTR = string.Empty;
                whereSTR = " and a.userid in (" + resultSTR + ")";
                if (!string.IsNullOrEmpty(LastIP)) { whereSTR = " and a.LastLoginIP like '%" + LastIP.Substring(0, (LastIP.LastIndexOf("."))) + "%' and a.userid not in (" + resultSTR1 + ")"; }
                string sql = "select top " + Number + " a.UserID,a.trueName,a.UserName,a.email from nt_user as a inner join nt_friend as b on a.UserID=b.friendID where  a.UserID<>" + UserID + " " + whereSTR + " order by newid()";
                return DbHelper.ExecuteTable(cn, CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public Dictionary<string, int> InviteFriends(int userId, string userName, string[] emails, string desc)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                Dictionary<string, int> dct = new Dictionary<string, int>();
                foreach (string eml in emails)
                {
                    string code = Rand.Str(10);
                    #region SqlParameter参数
                    SqlParameter[] Param = new SqlParameter[3];
                    Param[0] = new SqlParameter("@Email", SqlDbType.NVarChar, 150);
                    Param[0].Value = eml;
                    Param[1] = new SqlParameter("@desc", SqlDbType.NVarChar, 500);
                    Param[1].Value = desc;
                    Param[2] = new SqlParameter("@ValidCode", SqlDbType.Char, 11);
                    Param[2].Value = code;
                    #endregion
                    string Sql = "Select Count(UserID) from NT_User where Email=@Email";
                    int n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Param[0]);
                    Sql = "Select Count(ID) from NT_SpareEmail where Email=@Email";
                    n += (int)DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Param[0]);
                    if (n > 0)
                    {
                        dct.Add(eml, 1);
                        continue;
                    }
                    Sql = "select count(ID) from NT_FriendInvite where email=@Email and UserID=" + userId;
                    n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Param[0]);
                    if (n > 0)
                    {
                        dct.Add(eml, 2);
                        continue;
                    }
                    if (n > 0)
                    {
                        Sql = "update NT_FriendInvite set Replay=0,PostTime='" + DateTime.Now + "',PostIP='" + Public.GetClientIP() + "'";
                        Sql += ",ValidCode=@ValidCode where email=@Email and UserID=" + userId;
                    }
                    else
                    {
                        Sql = "insert into NT_FriendInvite (UserID, email, Reply, PostTime, PostIP, ReplyTime, ReplyIP, ValidCode, RegUserID) values (";
                        Sql += userId + ",@Email,0,'" + DateTime.Now + "','" + Public.GetClientIP() + "','" + DateTime.Now + "','',@ValidCode,0)";
                    }
                    try
                    {
                        #region 发送邮件
                        JuSNS.Common.EMail em = JuSNS.Common.EMail.CreateInstance();
                        string body = desc;
                        body = body.Replace("{VCODE}", code);
                        body = body.Replace("{EMAIL}", eml);
                        em.Body = body;
                        em.Subject = userName + "邀请您去" + UiConfig.SiteName;
                        em.To = eml;
                        em.From = UiConfig.SiteName + "<" + EmailConfig.from + ">";
                        em.Send();
                        #endregion

                        int m = DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, Param);
                        dct.Add(eml, 0);
                    }
                    catch
                    {
                        dct.Add(eml, 4);
                    }
                }
                return dct;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public FavoriteInfo GetFavorInfo(object fid)
        {
            FavoriteInfo mdl = new FavoriteInfo();
            string sql = "select id,UserID, URL, ClassID, IsPub, title, [content], PostTime from nt_favorite where id=" + fid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.ClassID = Convert.ToInt32(dr["ClassID"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.Id = Convert.ToInt32(dr["Id"]);
                mdl.IsPub = Convert.ToBoolean(dr["IsPub"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.Title = Convert.ToString(dr["Title"]);
                mdl.URL = Convert.ToString(dr["URL"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InsertFavorite(FavoriteInfo info)
        {
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@ClassID", SqlDbType.Int);
            Param[0].Value = info.ClassID;
            Param[1] = new SqlParameter("@Content", SqlDbType.NVarChar, 200);
            Param[1].Value = info.Content;
            Param[2] = new SqlParameter("@Id", SqlDbType.Int);
            Param[2].Value = info.Id;
            Param[3] = new SqlParameter("@IsPub", SqlDbType.Bit);
            Param[3].Value = info.IsPub;
            Param[4] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            Param[4].Value = info.PostTime;
            Param[5] = new SqlParameter("@Title", SqlDbType.NVarChar, 60);
            Param[5].Value = info.Title;
            Param[6] = new SqlParameter("@URL", SqlDbType.NVarChar, 200);
            Param[6].Value = info.URL;
            Param[7] = new SqlParameter("@UserID", SqlDbType.Int);
            Param[7].Value = info.UserID;
            string sql = "insert into nt_favorite(UserID, URL, ClassID, IsPub, title, [content], PostTime)values(@UserID, @URL, @ClassID, @IsPub, @Title, @Content, @PostTime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, Param);
        }

        public int GetFriendInvite(int UserID, string email, string code)
        {
            //0正确，1无效的参数，2已经验证过了。
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@email", SqlDbType.NVarChar, 150);
            Param[0].Value = email;
            Param[1] = new SqlParameter("@code", SqlDbType.NVarChar, 10);
            Param[1].Value = code;
            string sql = "select ID, UserID, email, Reply, PostTime, PostIP, ReplyTime, ReplyIP, ValidCode, RegUserID from NT_Friendinvite where UserID=" + UserID + " and email=@email and ValidCode=@code";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, Param);
            if (dr.Read())
            {
                if (dr["Reply"].ToString() == "1")
                {
                    dr.Close();
                    return 2;
                }
                else
                {
                    dr.Close();
                    return 0;
                }
            }
            dr.Close();
            return 1;
        }

        public int ReplayInvite(int UserID, int uID, string email)
        {
            string sql = "update NT_Friendinvite set Reply=1,ReplyTime='" + DateTime.Now + "',ReplyIP='" + Public.GetClientIP() + "',RegUserID=" + uID + " where UserID=" + UserID + " and email='" + email + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DeleteVisite(int ID, int UserID)
        {
            string sql = "delete from NT_Visit where id=" + ID + " and (UserID=" + UserID + " or VisitorID=" + UserID + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertChargeOrder(ChargeOrderInfo Info)
        {
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
            Param[0].Value = Info.CreatTime;
            Param[1] = new SqlParameter("@Gpoint", SqlDbType.Int);
            Param[1].Value = Info.Gpoint;
            Param[2] = new SqlParameter("@IsSucces", SqlDbType.Bit);
            Param[2].Value = Info.IsSucces;
            Param[3] = new SqlParameter("@OrderNumber", SqlDbType.NVarChar, 20);
            Param[3].Value = Info.OrderNumber;
            Param[4] = new SqlParameter("@Point", SqlDbType.Int);
            Param[4].Value = Info.Point;
            Param[5] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
            Param[5].Value = Info.PostIP;
            Param[6] = new SqlParameter("@UserID", SqlDbType.Int);
            Param[6].Value = Info.UserID;
            Param[7] = new SqlParameter("@Money", SqlDbType.Int);
            Param[7].Value = Info.Money;
            string sql = "insert into NT_ChargeOrder(point, gpoint, orderNumber, UserID, IsSucces, CreatTime, PostIP,Money)values(@Point,@Gpoint,@OrderNumber,@UserID,@IsSucces,@CreatTime,@PostIP,@Money)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, Param);
        }

        public MagicInfo GetMagicInfo(object mID)
        {
            MagicInfo mInfo = new MagicInfo();
            string sql = "select id, mName, pic, point, gpoint, number, buynumber, mdesc, mType, CreatTime, state, vTime from NT_Magic where id=" + mID + "";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                mInfo.Buynumber = Convert.ToInt32(rd["Buynumber"]);
                mInfo.CreatTime = Convert.ToDateTime(rd["CreatTime"]);
                mInfo.Gpoint = Convert.ToInt32(rd["Gpoint"]);
                mInfo.Id = Convert.ToInt32(mID);
                mInfo.Mdesc = rd["Mdesc"].ToString();
                mInfo.MName = rd["MName"].ToString();
                mInfo.MType = Convert.ToInt32(rd["MType"]);
                mInfo.Number = Convert.ToInt32(rd["Number"]);
                mInfo.Pic = rd["Pic"].ToString();
                mInfo.Point = Convert.ToInt32(rd["Point"]);
                mInfo.State = Convert.ToByte(rd["State"]);
                mInfo.VTime = Convert.ToInt32(rd["VTime"]);
            }
            rd.Close();
            return mInfo;
        }

        public int BuyMagic(int UserID, int mID, int Num)
        {
            //0购买成功，1积分不够，2金币不够，3库存不够
            MagicInfo mdl = GetMagicInfo(mID);
            int point = mdl.Point;
            int gpoint = mdl.Gpoint;
            int Number = mdl.Number;
            if (Num > Number)
            {
                return 3;
            }
            int epoint = Convert.ToInt32(point * UiConfig.Magic / 10) * Num;
            int egpoint = Convert.ToInt32(gpoint * UiConfig.Magic / 10) * Num;
            UserInfo uinfo = GetUserInfo(UserID);
            if (uinfo.Integral < epoint)
            {
                return 1;
            }

            if (uinfo.Inteyb < egpoint)
            {
                return 2;
            }

            string sql = "update nt_magic set number=number-" + Num + ",buynumber=buynumber+" + Num + " where id=" + mID;
            int n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                //插入到我的道具
                string qsql = "select count(id) from nt_magicinfo where mid=" + mID + " and userid=" + UserID + "";
                int m = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, qsql, null));
                if (m > 0)
                {
                    sql = "update nt_magicinfo set Number=Number+" + Num + " where mid=" + mID + " and userid=" + UserID + "";
                }
                else
                {
                    sql = "insert into nt_magicinfo(UserID, MID, PostTime, IsUse, SendUserID,Number)values(" + UserID + "," + mID + ",'" + DateTime.Now + "',0,0," + Num + ")";
                }
                n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                //扣除积分和金币
                sql = "update nt_user set Integral=Integral-" + epoint + ",Inteyb=Inteyb-" + egpoint + " where UserID=" + UserID;
                n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                //插入日志 0获得1赠送2使用
                sql = "insert into nt_magiclogs( UserID, mID, Num, MDesc, mType, PostTime)values(" + UserID + "," + mID + "," + Num + ",'购买了',0,'" + DateTime.Now + "')";
                n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
                //增加经验
                return 0;
            }
            else
            {
                return 4;
            }

        }

        public int SendCommentReplay(int bid, int cid, int uid, string cont, string type)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                if (uid == 0) return 0;
                SqlParameter param = new SqlParameter("@Content", SqlDbType.NVarChar, 300);
                param.Value = cont;
                string sql = string.Empty;
                switch (type)
                {
                    case "blog":
                        int BlogCommentCheck = Convert.ToInt32(Public.GetXMLBaseValue("BlogCommentCheck"));
                        sql = "insert into NT_BlogComment(BlogID, UserID, [Content], PostTime, PostIP, IsLock, CommID)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + BlogCommentCheck + "," + cid + ")";
                        break;
                    case "news":
                        int NewsCommentCheck = Convert.ToInt32(Public.GetXMLBaseValue("NewsCommentCheck"));
                        sql = "insert into NT_NewsComment(NewsID, UserID, [Content], PostTime, PostIP, IsLock, ParentID)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + NewsCommentCheck + "," + cid + ")";
                        break;
                    case "photo":
                        int CommentCheck = Convert.ToInt32(Public.GetXMLAlbumValue("CommentCheck"));
                        sql = "insert into NT_PhotoComment(PhotoID, UserID, [Content], PostTime, PostIP, IsLock, CommentID)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + CommentCheck + "," + cid + ")";
                        break;
                    case "ative":
                        int AtiveCommentCheck = Convert.ToInt32(Public.GetXMLAtiveValue("CommentCheck"));
                        sql = "insert into NT_AtiveComment(AtiveID, UserID, [Content], PostTime, PostIP, IsLock, CommentID)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + AtiveCommentCheck + "," + cid + ")";
                        break;
                    case "goods":
                        int GoodsCommentCheck = Convert.ToInt32(Public.GetXMLShopValue("GoodsCommentCheck"));
                        sql = "insert into NT_ShopComment(PID, UserID,Content, PostTime, PostIP, Islock, commid, cType)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + GoodsCommentCheck + "," + cid + ",0)";
                        break;
                    case "shop":
                        int shopCommentCheck = Convert.ToInt32(Public.GetXMLShopValue("GoodsCommentCheck"));
                        sql = "insert into NT_ShopComment(PID, UserID,Content, PostTime, PostIP, Islock, commid, cType)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + shopCommentCheck + "," + cid + ",1)";
                        break;
                    case "multe":
                        int multeCommentCheck = Convert.ToInt32(Public.GetXMLShopValue("GoodsCommentCheck"));
                        sql = "insert into NT_ShopComment(PID, UserID,Content, PostTime, PostIP, Islock, commid, cType)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + multeCommentCheck + "," + cid + ",2)";
                        break;
                    case "twitter":
                        int TwitterCommentCheck = Convert.ToInt32(Public.GetXMLBaseValue("TwitterCommentCheck"));
                        sql = "insert into NT_TwitterComment(TID, UserID,Content, PostTime, PostIP, Islock, CommentID)values(" + bid + "," + uid + ",@Content,'" + DateTime.Now + "','" + Public.GetClientIP() + "'," + TwitterCommentCheck + "," + cid + ")";
                        sql += "update nt_Twitter set Comments=Comments+1 where ID=" + bid + "";
                        break;
                }
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                if (n > 0)
                {
                    //这里开始增加评论数
                    switch (type)
                    {
                        case "blog":
                            sql = "update nt_blog set comments=comments+1 where id=" + bid;
                            break;
                        case "news":
                            sql = "update nt_news set comments=comments+1 where id=" + bid;
                            break;
                        case "photo":
                            sql = "update nt_photo set comments=comments+1 where id=" + bid;
                            break;
                        default:
                            sql = string.Empty;
                            break;
                    }
                    if (!string.IsNullOrEmpty(sql))
                    {
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
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

        public int InsertFilesRecord(int UserID, int InfoID, int Type)
        {
            string sql = "insert into nt_filesrecord(UserID,InfoID,Types,PostIP,PostTime)VALUES(" + UserID + "," + InfoID + "," + Type + ",'" + Public.GetClientIP() + "','" + DateTime.Now + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public bool isFilesDownload(int UserID, int InfoID, int Type)
        {
            string sql = "select count(id) from nt_filesrecord where UserID=" + UserID + " and InfoID=" + InfoID + " and Types=" + Type + "";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null)) > 0 ? true : false;
        }



        public List<FriendInfo> GetFriendListTop(int Number, int UserID)
        {
            List<FriendInfo> list = new List<FriendInfo>();
            string sql = "Select top " + Number + "  a.ID, a.UserID, a.FriendID, a.State, a.descript, a.PostTime, a.ClassID, a.FDegree, b.TrueName,a.FriendID,b.UserID " +
                         "From " +
                         "NT_Friend AS a INNER JOIN NT_User AS b ON a.FriendID = b.UserID " +
                         "Where a.UserID=" + UserID + " ORDER BY b.LastLoginTime DESC,b.userid desc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                FriendInfo info = new FriendInfo();
                info.ClassID = Convert.ToInt32(rd["ClassID"].ToString());
                info.Descript = rd["Descript"].ToString();
                info.FDegree = Convert.ToInt32(rd["FDegree"].ToString());
                info.FriendID = Convert.ToInt32(rd["FriendID"].ToString());
                info.ID = Convert.ToInt32(rd["ID"].ToString());
                info.PostTime = Convert.ToDateTime(rd["PostTime"].ToString());
                info.State = Convert.ToInt32(rd["State"].ToString());
                info.TrueName = rd["TrueName"].ToString();
                info.UserID = Convert.ToInt32(rd["UserID"].ToString());
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public List<GiftClassInfo> GetGiftClassList()
        {
            List<GiftClassInfo> list = new List<GiftClassInfo>();
            string sql = "Select Id,ClassName,ParentID from nt_giftclass order by id DESC";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                GiftClassInfo info = new GiftClassInfo();
                info.Id = Convert.ToInt32(rd["Id"]);
                info.ClassName = rd["ClassName"].ToString();
                info.ParentID = Convert.ToInt32(rd["ParentID"]);
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public List<FavoriteClassInfo> GetFavorList(int userid)
        {
            List<FavoriteClassInfo> list = new List<FavoriteClassInfo>();
            string sql = "Select Id,ClassName,IsPub from NT_FavoriteClass where userid=" + userid + " order by id asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (rd.Read())
            {
                FavoriteClassInfo info = new FavoriteClassInfo();
                info.Id = Convert.ToInt32(rd["Id"]);
                info.ClassName = rd["ClassName"].ToString();
                info.IsPub = Convert.ToBoolean(rd["IsPub"]);
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public int InsertDyn(DynInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@Content", SqlDbType.NVarChar,300);
                param[0].Value = info.Content;
                param[1] = new SqlParameter("@CUserID", SqlDbType.Int);
                param[1].Value = info.CUserID;
                param[2] = new SqlParameter("@DynType", SqlDbType.Int);
                param[2].Value = info.DynType;
                param[3] = new SqlParameter("@ID", SqlDbType.Int);
                param[3].Value = info.ID;
                param[4] = new SqlParameter("@Infoarr", SqlDbType.Int);
                param[4].Value = info.Infoarr;
                param[5] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[5].Value = info.PostTime;
                param[6] = new SqlParameter("@UserID", SqlDbType.Int);
                param[6].Value = info.UserID;
                string sql = "insert into nt_dyn(UserID, cUserID, dynType, [Content], PostTime, infoarr)values(@UserID, @CUserID, @DynType, @Content, @PostTime, @Infoarr)";
                return DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertNotice(NoticeInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@Content", SqlDbType.NVarChar, 255);
                param[0].Value = info.Content;
                param[1] = new SqlParameter("@CorrID", SqlDbType.Int);
                param[1].Value = info.CorrID;
                param[2] = new SqlParameter("@ID", SqlDbType.Int);
                param[2].Value = info.ID;
                param[3] = new SqlParameter("@IsRead", SqlDbType.Bit);
                param[3].Value = info.IsRead;
                param[4] = new SqlParameter("@MsgType", SqlDbType.TinyInt);
                param[4].Value = info.MsgType;
                param[5] = new SqlParameter("@PostIP", SqlDbType.NVarChar, 15);
                param[5].Value = info.PostIP;
                param[6] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[6].Value = info.PostTime;
                param[7] = new SqlParameter("@ReviceID", SqlDbType.Int);
                param[7].Value = info.ReviceID;
                param[8] = new SqlParameter("@UserID", SqlDbType.Int);
                param[8].Value = info.UserID;
                string sql = "insert into nt_notice( UserID, ReviceID, [Content], IsRead, PostTime, PostIP, MsgType, CorrID)values(@UserID, @ReviceID, @Content, @IsRead, @PostTime, @PostIP, @MsgType, @CorrID)";
                return DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }


        public int InsertGiftUser(GiftUserInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@GiftID", SqlDbType.Int);
                param[0].Value = info.GiftID;
                param[1] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[1].Value = info.PostTime;
                param[2] = new SqlParameter("@ReviceID", SqlDbType.Int);
                param[2].Value = info.ReviceID;
                param[3] = new SqlParameter("@UserID", SqlDbType.Int);
                param[3].Value = info.UserID;
                param[4] = new SqlParameter("@Content", SqlDbType.NVarChar, 500);
                param[4].Value = info.Content;
                string sql = string.Empty;
                int maxgift = Convert.ToInt32(Public.GetXMLGiftValue("maxgift"));
                int giftcount = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from nt_giftuser where UserID=@UserID and DATEDIFF(dd,PostTime,getdate())=0", param));
                if (giftcount >= maxgift)
                {
                    return -1;
                }
                else
                {
                    sql = "insert into nt_giftUser(GiftID,PostTime,ReviceID,UserID,Content)values(@GiftID,@PostTime,@ReviceID,@UserID,@Content)";
                    int n= DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                    //扣除积分
                    int gpoint = 0;
                    int point = 0;
                    sql = "select gpoint,point from nt_gift where id=@GiftID";
                    IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, sql, param);
                    if (dr.Read())
                    {
                        gpoint = Convert.ToInt32(dr["gpoint"]);
                        point = Convert.ToInt32(dr["gpoint"]);
                    }
                    UserInfo mdl = GetUserInfo(info.UserID);
                    if (mdl.Integral < point||mdl.Inteyb<gpoint)
                        return -2;
                    dr.Close();
                    if (gpoint > 0)
                    {
                        UpdateInte(info.UserID, gpoint, 1, 1, "赠送礼物扣除金币");
                    }
                    if (point > 0)
                    {
                        UpdateInte(info.UserID, point, 0, 1, "赠送礼物扣除积分");
                    }
                    return n;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        public int InsertCalend(CalendInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@Content", SqlDbType.NVarChar,250);
                param[0].Value = info.Content;
                param[1] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[1].Value = info.PostTime;
                param[2] = new SqlParameter("@EndTime", SqlDbType.DateTime);
                param[2].Value = info.EndTime;
                param[3] = new SqlParameter("@Id", SqlDbType.Int);
                param[3].Value = info.Id;
                param[4] = new SqlParameter("@NoteNumber", SqlDbType.Int);
                param[4].Value = info.NoteNumber;
                param[5] = new SqlParameter("@StartTime", SqlDbType.DateTime);
                param[5].Value = info.StartTime;
                param[6] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
                param[6].Value = info.Title;
                param[7] = new SqlParameter("@UserID", SqlDbType.Int);
                param[7].Value = info.UserID;
                string sql = string.Empty;
                int n = 9;
                if (info.Id > 0)
                {
                    sql = "update NT_Calend set Title=@Title, Content=@Content, PostTime=@PostTime, NoteNumber=@NoteNumber, StartTime=@StartTime, EndTime=@EndTime where userid=@UserID and id=@Id";
                    n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                }
                else
                {
                    sql = "insert into NT_Calend(UserID, Title, [Content], PostTime, NoteNumber, StartTime, EndTime)values(@UserID, @Title, @Content, @PostTime, @NoteNumber, @StartTime, @EndTime)";
                    n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }

        public CalendInfo GetCalendInfo(int cid)
        {
            CalendInfo mdl = new CalendInfo();
            string sql = "select * from  NT_Calend where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.EndTime = Convert.ToDateTime(dr["EndTime"]);
                mdl.Id = cid;
                mdl.NoteNumber = Convert.ToInt32(dr["NoteNumber"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.StartTime = Convert.ToDateTime(dr["StartTime"]);
                mdl.Title = Convert.ToString(dr["Title"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                  dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }

        }

        public int DeleteCalend(int cid, int uid)
        {
            string sql = string.Empty;
            if (IsAdmin(uid))
            {
                sql = "delete from nt_calend where ID=" + cid;
            }
            else
            {
                sql = "delete from nt_calend where ID=" + cid + " and UserID=" + uid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int deleteFavorite(int fid, int uid)
        {
            string sql = string.Empty;
            if (IsAdmin(uid))
            {
                sql = "delete from NT_Favorite where ID=" + fid;
            }
            else
            {
                sql = "delete from NT_Favorite where ID=" + fid + " and UserID=" + uid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int deleteFavoriteClass(int fid, int uid)
        {
            string sql = string.Empty;
            if (IsAdmin(uid))
            {
                sql = "delete from NT_Favoriteclass where ID=" + fid;
            }
            else
            {
                sql = "delete from NT_Favoriteclass where ID=" + fid + " and UserID=" + uid;
            }
            int n= DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "update NT_Favorite set classid=0 where classid=" + fid, null);
            }
            return n;
        }

        public int DeletePoke(int pid, int uid)
        {
            string sql = string.Empty;
            if (IsAdmin(uid))
            {
                sql = "delete from nt_poke where ID=" + pid;
            }
            else
            {
                sql = "delete from nt_poke where ID=" + pid + " and ReviceID=" + uid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int InsertPoke(PokeInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PokeForm", SqlDbType.NVarChar, 50);
                param[0].Value = info.PokeForm;
                param[1] = new SqlParameter("@PokeKey", SqlDbType.Int);
                param[1].Value = info.PokeKey;
                param[2] = new SqlParameter("@Poketo", SqlDbType.NVarChar,50);
                param[2].Value = info.Poketo;
                param[3] = new SqlParameter("@PostTime", SqlDbType.DateTime);
                param[3].Value = info.PostTime;
                param[4] = new SqlParameter("@ReviceID", SqlDbType.Int);
                param[4].Value = info.ReviceID;
                param[5] = new SqlParameter("@UserID", SqlDbType.Int);
                param[5].Value = info.UserID;
                param[6] = new SqlParameter("@IsPub", SqlDbType.TinyInt);
                param[6].Value = info.IsPub;
                string sql = string.Empty;
                int maxpoke = Convert.ToInt32(Public.GetXMLPokeValue("DayPokeNumber"));
                int pokecount = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from nt_poke where UserID=@UserID and DATEDIFF(dd,PostTime,getdate())=0", param));
                if (pokecount >= maxpoke)
                {
                    return -1;
                }
                else
                {
                    sql = "insert into nt_poke(UserID, ReviceID, PokeKey, PokeForm, Poketo, PostTime,IsPub)values(@UserID, @ReviceID, @PokeKey, @PokeForm, @Poketo, @PostTime,@IsPub)";
                    int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
                    return n;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int insertFavoriteClass(FavoriteClassInfo info)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 50);
            param[0].Value = info.ClassName;
            param[1] = new SqlParameter("@UserID", SqlDbType.Int);
            param[1].Value = info.UserID;
            param[2] = new SqlParameter("@IsPub", SqlDbType.Bit);
            param[2].Value = info.IsPub;
            string sql = "insert into NT_FavoriteClass(ClassName, IsPub, UserID)values(@ClassName, @IsPub, @UserID);Select SCOPE_IDENTITY()";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int InsertGbook(GBookInfo info)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Content", SqlDbType.NVarChar, 500);
            param[0].Value = info.Content;
            param[1] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[1].Value = info.IsLock;
            param[2] = new SqlParameter("@ParentID", SqlDbType.Int);
            param[2].Value = info.ParentID;
            param[3] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[3].Value = info.PostTime;
            param[4] = new SqlParameter("@SendID", SqlDbType.Int);
            param[4].Value = info.SendID;
            param[5] = new SqlParameter("@UserID", SqlDbType.Int);
            param[5].Value = info.UserID;
            string sql = "insert into NT_GBook(UserID, SendID, [Content], ParentID, PostTime, IsLock)values(@UserID, @SendID, @Content, @ParentID, @PostTime, @IsLock)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateMailState(int mid, int state)
        {
            string sql = "update nt_mailbox set isread=" + state + " where id=" + mid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DleteMailBox(int mid, int userid)
        {
            string sql = "delete nt_mailbox where userid="+userid+" and id=" + mid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DleteMailSend(int mid, int userid)
        {
            string sql = "delete nt_mailsend where userid=" + userid + " and id=" + mid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int SendMail(int userid, int reciveid, string title, string content)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                //插入收件箱
                string sql = "insert into nt_mailbox(UserID, SendID, Title, [Content], PostTime, IsRead)values(" + reciveid + "," + userid + ",'" + title + "','" + content + "','"+DateTime.Now+"',0)";
                int n = DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                if (n > 0)
                {
                    sql = "insert into nt_mailSend(UserID, ReviceID, PostTime, title, [content])values(" + userid + "," + reciveid + ",'" + DateTime.Now + "','" + title + "','" + content + "')";
                    n += DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DeleteGbook(int gid, int uid)
        {
            string sql = string.Empty;
            if (IsAdmin(uid))
            {
                sql = "delete from nt_gbook where id=" + gid;
            }
            else
            {
                sql = "delete from nt_gbook where id=" + gid + " and (userid=" + uid + " or sendid=" + uid + ")";
            }
            int n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            return n;
        }

        public List<UserInfo> RegisterUserNew(int number,int userid)
        {
            List<UserInfo> infolist = new List<UserInfo>();
            string whereSTR = string.Empty;
            if (userid > 0)
            {
                whereSTR = " and userid<>" + userid;
            }
            string sql = "select top " + number + " userid,truename from nt_user where state<>" + (byte)EnumUserState.Lock + " and Portrait<>0" + whereSTR + " order by userid desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                UserInfo mdl = new UserInfo();
                mdl.UserID = Convert.ToInt32(dr["userid"]);
                mdl.TrueName = Convert.ToString(dr["truename"]);
                infolist.Add(mdl);
            }
            dr.Close();
            return infolist;
        }

        /// <summary>
        /// 得到提醒信息
        /// </summary>
        /// <param name="userid">当前用户</param>
        /// <param name="infoid">相关ID</param>
        /// <param name="flag">0好友请求，1群组请求，2通知信息，3短信，4提醒</param>
        /// <returns>返回数量</returns>
        public int GetNote(int userid, int infoid, int flag)
        {
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select count(id) from nt_friend where userid=" + userid + " and state=1 and FDegree=0";
                    break;
                case 1:
                    sql = "select count(id) from NT_GroupInvite where ReviceID=" + userid + " and IsAccept=0";
                    break;
                case 2:
                    sql = "select count(id) from NT_Notice where ReviceID=" + userid + " and IsRead=0";
                    break;
                case 3:
                    sql = "select count(id) from NT_Mailbox where UserID=" + userid + " and IsRead=0";
                    break;
                case 4:
                    sql = "select count(id) from NT_Calend where UserID=" + userid + " and (DATEDIFF(d,getdate() , StartTime)<=" + Public.GetXMLBaseValue("noteNumber") + ") and  (DATEDIFF(d,getdate() , StartTime)>=0)";
                    break;
            }
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        public List<FriendInfo> GetFriendRequest(int userid)
        {
            List<FriendInfo> infolist = new List<FriendInfo>();
            string sql = "select a.id,a.FriendID,a.Descript,a.PostTime,b.Truename from nt_friend AS a INNER JOIN NT_User AS b on a.FriendID=b.UserID where a.state=1 and a.userid=" + userid + " and FDegree=0 order by a.id desc";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                FriendInfo mdl = new FriendInfo();
                mdl.FriendID = Convert.ToInt32(dr["FriendID"]);
                mdl.ID = Convert.ToInt32(dr["ID"]);
                mdl.Descript = Convert.ToString(dr["Descript"]);
                mdl.TrueName = Convert.ToString(dr["TrueName"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                infolist.Add(mdl);
            }
            dr.Close();
            return infolist;
        }

        public List<GroupInviteInfo> GetGroupRequest(int userid)
        {
            List<GroupInviteInfo> infolist = new List<GroupInviteInfo>();
            string sql = "select  a.ID, a.GroupID, a.UserID, a.ReviceID, a.[Content], a.IsAccept, a.PostTime, c.GroupName, b.TrueName from NT_GroupInvite AS a INNER JOIN NT_User AS b ON a.UserID = b.UserID INNER JOIN  NT_Group AS c ON a.GroupID = c.id WHERE  (a.IsAccept = 0) AND (a.ReviceID = "+userid+") ORDER BY a.ID DESC";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                GroupInviteInfo mdl = new GroupInviteInfo();
                mdl.GroupID = Convert.ToInt32(dr["GroupID"]);
                mdl.ID = Convert.ToInt32(dr["ID"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                mdl.ReviceID = Convert.ToInt32(dr["ReviceID"]);
                mdl.GroupName = Convert.ToString(dr["GroupName"]);
                mdl.TrueName = Convert.ToString(dr["TrueName"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                infolist.Add(mdl);
            }
            dr.Close();
            return infolist;
        }

        public int CheckFriend(int fid, int uid, int flag)
        {
            string sql = string.Empty;
            if (flag == 0)
            {
                sql = "update nt_friend set state=0 where  userid=" + uid + " and friendid=" + fid + ";";
                sql += "update nt_friend set state=0 where  userid=" + fid + " and friendid=" + uid;
            }
            else
            {
                sql = "delete from nt_friend where  userid=" + uid + " and friendid=" + fid + ";";
                sql += "delete from nt_friend where  userid=" + fid + " and friendid=" + uid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int CheckGroup(int userid, int uid, int gid, int flag)
        {
            string sql = string.Empty;
            int n = 0;
            if (flag == 0)
            {
                bool ismember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
                if (ismember)
                {
                    return -1;
                }
                sql = "insert into nt_groupmember(UserID,GroupID,JoinTime,Grade,Islock)values(" + uid + "," + gid + ",'" + DateTime.Now + "',0,0);";
                sql += "delete from nt_groupinvite where groupid=" + gid + " and ReviceID=" + uid + " and userid=" + userid+";";
                sql += "update nt_group set members=members+1 where id=" + gid;
            }
            else
            {
                sql += "delete from nt_groupinvite where groupid=" + gid + " and ReviceID=" + uid + " and userid=" + userid;
            }
            n = DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            if (n > 0)
            {
                n = 1;
            }
            return n;
        }

        public int InsertATT(ATTInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Atterid", SqlDbType.Int);
                param[0].Value = info.Atterid;
                param[1] = new SqlParameter("@Userid", SqlDbType.Int);
                param[1].Value = info.Userid;
                //是否关注过了
                string sql = "select count(id) from nt_att where userid=@Userid and Atterid=@Atterid";
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
                if (n > 0)
                    return -1;
                sql = "insert into nt_att(Atterid,Userid)values(@Atterid,@Userid)";
                return DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public void UpdateOnlineUser(OnlineUserInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                int deleteUser = Convert.ToInt32(Public.GetXMLBaseValue("deleteUser"));
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@LastIP", SqlDbType.NVarChar, 15);
                param[0].Value = info.LastIP;
                param[1] = new SqlParameter("@LastTime", SqlDbType.DateTime);
                param[1].Value = info.LastTime;
                param[2] = new SqlParameter("@LastUrl", SqlDbType.NVarChar, 200);
                param[2].Value = info.LastUrl;
                param[3] = new SqlParameter("@UserID", SqlDbType.Int);
                param[3].Value = info.UserID;
                param[4] = new SqlParameter("@UserName", SqlDbType.NVarChar, 20);
                param[4].Value = info.UserName;
                //param[5] = new SqlParameter("@DeleteTime", SqlDbType.DateTime);
                DateTime ntime =DateTime.Now.AddMinutes(-deleteUser);
                //检查是否已经在线
                string sql = string.Empty;
                bool isOnline = false;
                if (info.UserID > 0)
                {
                    sql = "select count(userid) from nt_onlineuser where userid=@UserID";
                    isOnline = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param)) > 0 ? true : false;
                }
                else
                {
                    sql = "select count(userid) from nt_onlineuser where userid=0 and LastIP=@LastIP";
                    isOnline = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, sql, param)) > 0 ? true : false;
                }
                if (isOnline)
                {
                    if (info.UserID > 0)
                    {
                        sql = "update nt_onlineuser set LastIP=@LastIP,lastTime=@LastTime,LastUrl=@LastUrl where userid=@UserID;";
                    }
                    else
                    {
                        sql = "update nt_onlineuser set lastTime=@LastTime,LastUrl=@LastUrl where userid=@UserID and LastIP=@LastIP;";
                    }
                }
                else
                {
                    sql = "insert into nt_onlineuser(LastIP,lastTime,LastUrl,UserID,UserName)values(@LastIP,@LastTime,@LastUrl,@UserID,@UserName);";
                }
                //删除不在线的会员
                sql += "delete from NT_OnlineUser where LastTime<'" + ntime + "'";
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public void UpdateNote(int userid)
        {
            string sql = "update NT_Notice set IsRead=1 where ReviceID=" + userid;
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public List<DynInfo> GetDynList(int number, int userid, string friendstr, string keys, string dyntype)
        {
            List<DynInfo> infolist = new List<DynInfo>();
            if(string.IsNullOrEmpty(friendstr))
                return null;
            string sql = string.Empty;
            string whereSTR = string.Empty;
            if (!string.IsNullOrEmpty(dyntype))
            {
                switch (dyntype)
                {
                    case "dyn-photo":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.CreatAlbum + " or  dynType=" + (byte)EnumDynType.CreatPhoto + " or  dynType=" + (byte)EnumDynType.PhotoComment + ")";
                        break;
                    case "dyn-blog":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.CreatBlog + " or  dynType=" + (byte)EnumDynType.BlogComment + ")";
                        break;
                    case "dyn-twitter":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.CreatTwitter + " or  dynType=" + (byte)EnumDynType.TwitterComment + ")";
                        break;
                    case "dyn-group":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.TopicComment + " or  dynType=" + (byte)EnumDynType.CreatTopic + " or  dynType=" + (byte)EnumDynType.JoinGroup + " or  dynType=" + (byte)EnumDynType.CreatGroup + " or  dynType=" + (byte)EnumDynType.ReplyTopic + ")";
                        break;
                    case "dyn-share":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.CreatShare + ")";
                        break;
                    case "dyn-state":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.Friend + " or  dynType=" + (byte)EnumDynType.InviteJoinSite + " or  dynType=" + (byte)EnumDynType.UpdateBasic + " or  dynType=" + (byte)EnumDynType.UpdateHeadPic + " or  dynType=" + (byte)EnumDynType.VerfiyEmail + ")";
                        break;
                    case "dyn-other":
                        whereSTR = " and (dynType=" + (byte)EnumDynType.ActiveComment + " or  dynType=" + (byte)EnumDynType.ActiveCreatPhoto + " or  dynType=" + (byte)EnumDynType.AskBest + " or  dynType=" + (byte)EnumDynType.AskComment + " or  dynType=" + (byte)EnumDynType.CreatActive + "";
                        whereSTR += " or  dynType=" + (byte)EnumDynType.CreatAPP + " or  dynType=" + (byte)EnumDynType.CreatAsk + " or  dynType=" + (byte)EnumDynType.CreatFaviote + " or  dynType=" + (byte)EnumDynType.CreatGift + " or  dynType=" + (byte)EnumDynType.CreatGoods + " or  dynType=" + (byte)EnumDynType.CreatMulte + " or  dynType=" + (byte)EnumDynType.CreatNews + "";
                        whereSTR += " or  dynType=" + (byte)EnumDynType.CreatPoke + " or  dynType=" + (byte)EnumDynType.CreatShop + " or  dynType=" + (byte)EnumDynType.CreatVote + " or  dynType=" + (byte)EnumDynType.GoodsComment + " or  dynType=" + (byte)EnumDynType.JoinVote + " or  dynType=" + (byte)EnumDynType.MulteComment + " or  dynType=" + (byte)EnumDynType.JoinMulte + " or  dynType=" + (byte)EnumDynType.NewsComment + ")";
                        break;
                }
            }
            if (string.IsNullOrEmpty(keys) || keys=="0")
            {
                sql = "select top " + number + " a.ID, a.UserID, a.cUserID, a.dynType, a.[Content], a.PostTime, a.infoarr,b.truename from nt_dyn AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.userid in (" + friendstr + ")" + whereSTR+" order by a.id desc";
            }
            else
            {
                sql = "select top " + number + " a.ID, a.UserID, a.cUserID, a.dynType, a.[Content], a.PostTime, a.infoarr,b.truename from nt_dyn AS a INNER JOIN NT_User AS b on a.userid=b.userid where a.userid in (" + friendstr + ") and a.userid not in (" + keys + ")" + whereSTR + " order by a.id desc";
            }
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                DynInfo mdl = new DynInfo();
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.TrueName = Convert.ToString(dr["TrueName"]);
                mdl.CUserID = Convert.ToInt32(dr["CUserID"]);
                mdl.DynType = Convert.ToInt32(dr["DynType"]);
                mdl.ID = Convert.ToInt32(dr["ID"]);
                mdl.Infoarr = Convert.ToInt32(dr["Infoarr"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                infolist.Add(mdl);
            }
            dr.Close();
            return infolist;
        }

        public GiftInfo GetGiftInfo(object gid)
        {
            GiftInfo mdl = new GiftInfo();
            string sql = "select * from nt_gift where id=" + gid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.ClassID = Convert.ToInt32(dr["classid"]);
                mdl.Content = Convert.ToString(dr["Content"]);
                mdl.GiftName = Convert.ToString(dr["GiftName"]);
                mdl.GPoint = Convert.ToInt32(dr["GPoint"]);
                mdl.Id = Convert.ToInt32(dr["Id"]);
                mdl.IsAd = Convert.ToBoolean(dr["IsAd"]);
                mdl.IsLock = Convert.ToByte(dr["IsLock"]);
                mdl.Pic = Convert.ToString(dr["Pic"]);
                mdl.Point = Convert.ToInt32(dr["Point"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.SendNumber = Convert.ToInt32(dr["SendNumber"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public string GetAtt(int userid)
        {
            string listSTR = string.Empty;
            string sql = "select atterid from NT_att where userid=" + userid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                listSTR += dr["atterid"] + ",";
            }
            dr.Close();
            return listSTR;
        }

        public JoinVipInfo GetVipInfo(int userid)
        {
            JoinVipInfo mdl = new JoinVipInfo();
            string sql = "select id,userid,posttime,endtime,islock from nt_joinvip where userid=" + userid + "";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                mdl.EndTime = Convert.ToDateTime(dr["endtime"]);
                mdl.Id = Convert.ToInt32(dr["id"]);
                mdl.IsLock = Convert.ToByte(dr["IsLock"]);
                mdl.PostTime = Convert.ToDateTime(dr["PostTime"]);
                mdl.UserID = Convert.ToInt32(dr["UserID"]);
                dr.Close();
                return mdl;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int JoinVip(int userid, string today, string joincontents)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@EndTime", SqlDbType.DateTime);
            param[0].Value = Convert.ToDateTime(today);
            param[1] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[1].Value = 1;
            param[2] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[2].Value = DateTime.Now;
            param[3] = new SqlParameter("@UserID", SqlDbType.Int);
            param[3].Value = userid;
            param[4] = new SqlParameter("@Content", SqlDbType.NVarChar,100);
            param[4].Value = joincontents;
            string sql = "select count(*) from nt_joinvip where userid=" + userid + " and islock=" + (byte)EnumCusState.ForLock + "";
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
            if (n > 0) return -1;
            sql = "insert into nt_joinvip(UserID, PostTime, EndTime, IsLock, [Content])values(@UserID, @PostTime, @EndTime, @IsLock, @Content)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateVip(int userid, int flag)
        {
            string sql = "update nt_user set isvip=" + flag + " where userid=" + userid;
            if (flag == 0)
            {
                sql += "delete from nt_joinvip where userid=" + userid;
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DeleteAtt(int aid, int uid)
        {
            string sql = "delete nt_att where id=" + aid + " and userid=" + uid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public void UpdateUserState(int uid, int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "update nt_user set Click=Click+1 where userid=" + uid;
                DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                if (userid != uid && userid > 0 && uid > 0)
                {
                    //是否有记录
                    int m = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(id) from nt_visit where userid=" + uid + " and VisitorID=" + userid + "", null));
                    if (m > 0)
                    {
                        sql = "update nt_visit set LastVisitTime='" + DateTime.Now + "',LastVisitIP='" + Public.GetClientIP() + "',VisitTimes=VisitTimes+1 where userid=" + uid + " and VisitorID=" + userid;
                    }
                    else
                    {
                        sql = "insert into nt_visit(UserID, VisitorID, LastVisitTime, LastVisitIP, VisitTimes)values(" + uid + "," + userid + ",'" + DateTime.Now + "','" + Public.GetClientIP() + "',1)";
                    }
                    DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public SpaceTemplateInfo GetSpaceTemplate(int tid)
        {
            SpaceTemplateInfo info = new SpaceTemplateInfo();
            string sql = "select ID, TName, TEName, PostTime, IsLock, IPoint, GPoint, UseNumber from NT_SpaceTemplate where id=" + tid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.GPoint = Convert.ToInt32(dr["gpoint"]);
                info.ID = Convert.ToInt32(dr["ID"]);
                info.IPoint = Convert.ToInt32(dr["IPoint"]);
                info.IsLock = Convert.ToByte(dr["IsLock"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.TEName = Convert.ToString(dr["TEName"]);
                info.TName = Convert.ToString(dr["TName"]);
                info.UseNumber = Convert.ToInt32(dr["UseNumber"]);
                dr.Close();
                return info;
            }
            else
            {
                dr.Close();
                return null;
            }
        }

        public int InsertSpaceTemplate(SpaceTemplateInfo info)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@GPoint", SqlDbType.Int);
            param[0].Value = info.GPoint;
            param[1] = new SqlParameter("@ID", SqlDbType.Int);
            param[1].Value = info.ID;
            param[2] = new SqlParameter("@IPoint", SqlDbType.Int);
            param[2].Value = info.IPoint;
            param[3] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[3].Value = info.IsLock;
            param[4] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[4].Value = info.PostTime;
            param[5] = new SqlParameter("@TEName", SqlDbType.NVarChar, 20);
            param[5].Value = info.TEName;
            param[6] = new SqlParameter("@TName", SqlDbType.NVarChar, 20);
            param[6].Value = info.TName;
            param[7] = new SqlParameter("@UseNumber", SqlDbType.Int);
            param[7].Value = info.UseNumber;
            string sql = string.Empty;
            if (info.ID > 0)
            {
                sql = "update NT_SpaceTemplate set TName=@TName, TEName=@TEName, PostTime=@PostTime, IsLock=@IsLock, IPoint=@IPoint, GPoint=@GPoint, UseNumber=@UseNumber where ID=@ID";
            }
            else
            {
                sql = "insert into NT_SpaceTemplate(TName, TEName, PostTime, IsLock, IPoint, GPoint, UseNumber)values(@TName, @TEName, @PostTime, @IsLock, @IPoint, @GPoint, @UseNumber)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int UpdateSpaceTemplate(int tid)
        {
            string sql = "update NT_SpaceTemplate set UseNumber=UseNumber+1 where id=" + tid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DeleteSpace(int tid, int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_SpaceTemplate where id=" + tid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int DeleteFlash(int tid, int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_Flash where id=" + tid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int DeleteGift(int fid, int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_Gift where id=" + fid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }       
        }

        public GiftClassInfo GetGiftClassInfo(int cid)
        {
            GiftClassInfo info = new GiftClassInfo();
            string sql = "select * from nt_giftclass where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.ClassName = dr["ClassName"].ToString();
                info.Id = cid;
                info.ParentID = Convert.ToInt32(dr["ParentID"]);
            }
            dr.Close();
            return info;
        }

        public int InsertGiftClass(GiftClassInfo info)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ClassName", SqlDbType.NVarChar, 30);
            param[0].Value = info.ClassName;
            param[1] = new SqlParameter("@Id", SqlDbType.Int);
            param[1].Value = info.Id;
            param[2] = new SqlParameter("@ParentID", SqlDbType.Int);
            param[2].Value = info.ParentID;

            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update NT_GiftClass set ClassName=@ClassName, ParentID=@ParentID where ID=@Id";
            }
            else
            {
                sql = "insert into NT_GiftClass(ClassName, ParentID)values(@ClassName, @ParentID)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int DeleteGiftClass(int fid, int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_GiftClass where id=" + fid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int InsertGift(GiftInfo info)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ClassID", SqlDbType.Int);
            param[0].Value = info.ClassID;
            param[1] = new SqlParameter("@Content", SqlDbType.NVarChar,250);
            param[1].Value = info.Content;
            param[2] = new SqlParameter("@GiftName", SqlDbType.NVarChar,50);
            param[2].Value = info.GiftName;
            param[3] = new SqlParameter("@GPoint", SqlDbType.Int);
            param[3].Value = info.GPoint;
            param[4] = new SqlParameter("@Id", SqlDbType.Int);
            param[4].Value = info.Id;
            param[5] = new SqlParameter("@IsAd", SqlDbType.Bit);
            param[5].Value = info.IsAd;
            param[6] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[6].Value = info.IsLock;
            param[7] = new SqlParameter("@Pic", SqlDbType.NVarChar,30);
            param[7].Value = info.Pic;
            param[8] = new SqlParameter("@Point", SqlDbType.Int);
            param[8].Value = info.Point;
            param[9] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[9].Value = info.PostTime;
            param[10] = new SqlParameter("@SendNumber", SqlDbType.Int);
            param[10].Value = info.SendNumber;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update NT_Gift set GiftName=@GiftName, Pic=@Pic, GPoint=@GPoint, Point=@Point, PostTime=@PostTime, Content=@Content, ClassID=@ClassID, IsAd=@IsAd, IsLock=@IsLock where ID=@Id";
            }
            else
            {
                sql = "insert into NT_Gift(GiftName, Pic, GPoint, Point, PostTime, SendNumber, [Content], ClassID, IsAd, IsLock)values(@GiftName, @Pic, @GPoint, @Point, @PostTime, @SendNumber, @Content, @ClassID, @IsAd, @IsLock)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int DeleteUserAll(int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_User where userid<>" + uid + " and isadmin=0";
                sql += ";delete from nt_userinfo where userid<>" + uid;
                sql += ";delete from NT_Album";
                sql += ";delete from NT_Ask";
                sql += ";delete from NT_Ative";
                sql += ";delete from NT_AtiveATT";
                sql += ";delete from NT_AtiveComment";
                sql += ";delete from NT_AtiveMember";
                sql += ";delete from NT_Att";
                sql += ";delete from NT_Blog";
                sql += ";delete from NT_BlogClass";
                sql += ";delete from NT_BlogComment";
                sql += ";delete from NT_Blogfoot";
                sql += ";delete from NT_Calend";
                sql += ";delete from NT_Friend";
                sql += ";delete from NT_Group";
                sql += ";delete from NT_GroupTopic";
                sql += ";delete from NT_GroupMember";
                sql += ";delete from NT_GroupInvite";
                sql += ";delete from NT_GiftUser";
                sql += ";delete from NT_Mailbox";
                sql += ";delete from NT_MailSend";
                sql += ";delete from NT_News";
                sql += ";delete from NT_NewsComment";
                sql += ";delete from NT_Notice";
                sql += ";delete from NT_Photo";
                sql += ";delete from NT_PhotoComment";
                sql += ";delete from NT_Poke";
                sql += ";delete from NT_Share";
                sql += ";delete from NT_Shop";
                sql += ";delete from NT_ShopComment";
                sql += ";delete from NT_ShopGoods";
                sql += ";delete from NT_ShopMultebuy";
                sql += ";delete from NT_ShopMulteMember";
                sql += ";delete from NT_ShopOrder";
                sql += ";delete from NT_ShopUserComment";
                sql += ";delete from NT_Twitter";
                sql += ";delete from NT_TwitterComment";
                sql += ";delete from NT_UserCareer";
                sql += ";delete from NT_UserEducation";
                sql += ";delete from NT_UserPointHistory";
                sql += ";delete from NT_UserSetting";
                sql += ";delete from NT_Visit";
                sql += ";delete from NT_Vote";
                sql += ";delete from NT_VoteDis";
                sql += ";delete from NT_VoteTo";
                sql += ";delete from NT_report";
                sql += ";delete from NT_Links";
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int DeleteUser(int userid, int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_User where userid=" + userid;
                sql += "delete from nt_userinfo where userid=" + userid;
                sql += "delete from NT_Album where userid=" + userid;
                sql += "delete from NT_Ask where userid=" + userid;
                sql += "delete from NT_Ative where userid=" + userid;
                sql += "delete from NT_AtiveATT where userid=" + userid;
                sql += "delete from NT_AtiveComment where userid=" + userid;
                sql += "delete from NT_AtiveMember where userid=" + userid;
                sql += "delete from NT_Att where userid=" + userid + " or atterid="+userid;
                sql += "delete from NT_Blog where userid=" + userid;
                sql += "delete from NT_BlogClass where userid=" + userid;
                sql += "delete from NT_BlogComment where userid=" + userid;
                sql += "delete from NT_Blogfoot where userid=" + userid;
                sql += "delete from NT_Calend where userid=" + userid;
                sql += "delete from NT_Friend where userid=" + userid + " or friendid=" + userid;
                sql += "delete from NT_Group where userid=" + userid;
                sql += "delete from NT_GiftUser where userid=" + userid + " or ReviceID=" + userid;
                sql += "delete from NT_GroupMember where userid=" + userid;
                sql += "delete from NT_Mailbox where userid=" + userid + " or SendID=" + userid;
                sql += "delete from NT_MailSend where userid=" + userid + " or ReviceID=" + userid;
                sql += "delete from NT_News where userid=" + userid;
                sql += "delete from NT_NewsComment where userid=" + userid;
                sql += "delete from NT_Notice where userid=" + userid + " or ReviceID=" + userid;
                sql += "delete from NT_Photo where userid=" + userid;
                sql += "delete from NT_PhotoComment where userid=" + userid;
                sql += "delete from NT_Poke where userid=" + userid + " or ReviceID=" + userid;
                sql += "delete from NT_Share where userid=" + userid;
                sql += "delete from NT_Shop where userid=" + userid;
                sql += "delete from NT_ShopComment where userid=" + userid;
                sql += "delete from NT_ShopGoods where userid=" + userid;
                sql += "delete from NT_ShopMultebuy where userid=" + userid;
                sql += "delete from NT_ShopMulteMember where userid=" + userid;
                sql += "delete from NT_ShopOrder where userid=" + userid;
                sql += "delete from NT_ShopUserComment where userid=" + userid;
                sql += "delete from NT_Twitter where userid=" + userid;
                sql += "delete from NT_TwitterComment where userid=" + userid;
                sql += "delete from NT_UserCareer where userid=" + userid;
                sql += "delete from NT_UserEducation where userid=" + userid;
                sql += "delete from NT_UserPointHistory where userid=" + userid;
                sql += "delete from NT_UserSetting where userid=" + userid;
                sql += "delete from NT_Visit where userid=" + userid + " or VisitorID=" + userid;
                sql += "delete from NT_Vote where userid=" + userid;
                sql += "delete from NT_VoteDis where userid=" + userid;
                sql += "delete from NT_VoteTo where userid=" + userid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            else
            {
                return 0;
            }
        }

        public int UpdateChargeOrderState(int oid, int flag)
        {
            string sql = "update NT_ChargeOrder set IsSucces=" + flag + " where id=" + oid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }

        public int DeleteChargeOrder(int oid, int uid)
        {
            if (IsAdmin(uid))
            {
                string sql = "delete from NT_ChargeOrder where id=" + oid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            return 0;
        }


        public int UpdateATT(int bid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "select count(id) from nt_att where userid=" + uid + " and atterid=" + bid;
                if (Convert.ToInt32(DbHelper.ExecuteScalar(cn,CommandType.Text, sql, null)) == 0)
                {
                    sql = "insert into nt_att(userid,atterid)values(" + uid + "," + bid + ")";
                    int m = DbHelper.ExecuteNonQuery(cn,CommandType.Text, sql, null);
                    if (m > 0)
                    {
                        sql = "update nt_user set attnumber=attnumber+1 where userid=" + bid;
                        DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                    }
                    return m;
                }
                else
                {
                    return -1;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int InsertFlash(FlashInfo info)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@BPic", SqlDbType.NVarChar, 30);
            param[0].Value = info.BPic;
            param[1] = new SqlParameter("@Id", SqlDbType.Int);
            param[1].Value = info.Id;
            param[2] = new SqlParameter("@IsLock", SqlDbType.Bit);
            param[2].Value = info.IsLock;
            param[3] = new SqlParameter("@OrderID", SqlDbType.Int);
            param[3].Value = info.OrderID;
            param[4] = new SqlParameter("@PostTime", SqlDbType.DateTime);
            param[4].Value = info.PostTime;
            param[5] = new SqlParameter("@SPic", SqlDbType.NVarChar, 30);
            param[5].Value = info.SPic;
            param[6] = new SqlParameter("@URL", SqlDbType.NVarChar, 150);
            param[6].Value = info.URL;
            string sql = string.Empty;
            if (info.Id > 0)
            {
                sql = "update NT_Flash set BPic=@BPic,SPic=@SPic, URL=@URL, IsLock=@IsLock, OrderID=@OrderID, PostTime=@PostTime where ID=@Id";
            }
            else
            {
                sql = "insert into NT_Flash(bPic, sPic, URL, IsLock, OrderID, PostTime)values(@BPic,@SPic, @URL, @IsLock, @OrderID, @PostTime)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }


        public FlashInfo GetFlashInfo(int fid)
        {
            FlashInfo info = new FlashInfo();
            string sql = "select id,bPic, sPic, URL, IsLock, OrderID, PostTime from nt_flash where id=" + fid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.BPic = dr["bpic"].ToString();
                info.SPic = dr["SPic"].ToString();
                info.Id = Convert.ToInt32(fid);
                info.IsLock = Convert.ToBoolean(dr["islock"]);
                info.OrderID = Convert.ToInt32(dr["OrderID"]);
                info.PostTime = Convert.ToDateTime(dr["PostTime"]);
                info.URL = dr["URL"].ToString();
            }
            dr.Close();
            return info;
        }

        public int ActivationEmail(int userid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "select state from nt_user where userid=" + userid;
                IDataReader dr = DbHelper.ExecuteReader(cn, CommandType.Text, sql, null);
                if(dr.Read())
                {
                    int state = Convert.ToInt32(dr["state"]);
                    dr.Close();
                    if (state == 5)
                        return 0;
                }
                dr.Close();
                sql = "update nt_user set state=5 where userid=" + userid;
                int n = DbHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                if (n > 0)
                {
                    DynInfo dyninfo = new DynInfo();
                    dyninfo.Content = "激活了电子邮件";
                    dyninfo.CUserID = 0;
                    dyninfo.DynType = (int)EnumDynType.VerfiyEmail;
                    dyninfo.Infoarr = userid;
                    dyninfo.PostTime = DateTime.Now;
                    dyninfo.UserID = userid;
                    InsertDyn(dyninfo);
                    UpdateInte(userid, Public.JSplit(1), 0, 0, "激活邮件");
                }
                return n;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int GetOnlineCount(int flag)
        {
            string sql = string.Empty;
            switch (flag)
            {
                case 0:
                    sql = "select count(id) from NT_Onlineuser where userid=0";
                    break;
                case 1:
                    sql = "select count(id) from NT_Onlineuser where userid<>0";
                    break;
                default:
                    sql = "select count(id) from NT_Onlineuser";
                    break;
            }
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
            string[] ShowRegCount = Public.GetXMLValue("onlinebase").Split(',');
            if (ShowRegCount[0] == "1")
            {
                n = n + Convert.ToInt32(ShowRegCount[1]);
            }
            return n;
        }

    }
}