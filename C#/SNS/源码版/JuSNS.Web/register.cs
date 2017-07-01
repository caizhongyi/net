using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web
{
    public class register : BasePage
    {
        public string loginCode = Public.GetXMLValue("loginCode");
        public string isRegMobile = Public.GetXMLValue("isRegMobile");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "用户注册");
            #region 是否允许注册
            string RegSwitch = Public.GetXMLValue("OpenReg");
            string errSTR = string.Empty;
            string uid = GetString("uid");
            if (RegSwitch != "1")
            {
                if (RegSwitch == "0")
                {
                    errSTR += "注册已经关闭，请与管理员联系！";
                }
                else if (RegSwitch == "2")
                {
                    if (string.IsNullOrEmpty(uid))
                    {
                        errSTR += "系统未开启注册，需要邀请才能注册！";
                    }
                }
            }
            #endregion
            string reciveemail = GetString("email");
            if (!string.IsNullOrEmpty(reciveemail) && reciveemail.IndexOf("@") > -1)
            {
                context.Put("reciveemail", reciveemail);
            }
            int inviteid = 0;
            int errorNumber = 0;
            #region 邀请注册参数
            UserInfo mdl = null;
            if (!string.IsNullOrEmpty(uid)) 
            {
                if (uid.IndexOf("_") == -1)
                {
                    errorNumber++;
                }
                else
                {
                    if (!Input.IsInteger(uid.Split('_')[1]))
                    {
                        errorNumber++;
                    }
                    else
                    {
                        if (!JuSNS.Home.User.User.Instance.CheckUserExsit(uid.Split('_')[1]))
                        {
                            errorNumber++;
                        }
                        else
                        {
                            mdl = JuSNS.Home.User.User.Instance.GetUserInfo(uid.Split('_')[1]);
                            if (Input.MD5(mdl.VerifyCode, true) != uid.Split('_')[0])
                            {
                                errorNumber++;
                            }
                            else
                            {
                                inviteid = Convert.ToInt32(uid.Split('_')[1]);
                            }
                        }
                    }
                }
            }
            #endregion
            if (errorNumber > 0)
            {
                context.Put("paramerror", "错误的参数");
            }
            else
            {
                context.Put("inviteid", inviteid);
                //邮件邀请
                #region 邮件邀请
                string code = GetString("code");
                string email = GetString("email");
                context.Put("replayEmail", email);
                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(email))
                {
                    //验证邮件是否合法
                    //0正确，1无效的参数，2已经验证过了
                    int n = JuSNS.Home.User.User.Instance.GetFriendInvite(inviteid, email, code);
                    switch (n)
                    {
                        case 1:
                            context.Put("paramerror", "无效的参数");
                            break;
                        case 2:
                            context.Put("paramerror", "已经验证过了");
                            break;
                    }
                }
                #endregion
                this.GetBirthdayYear(ref context);
                this.GetBirthdayMonth(ref context);
                this.GetBirthdayDay(ref context);
                context.Put("invitecontent", Public.GetXMLValue("inviteReg"));
                if (inviteid > 0)
                {
                    context.Put("isinvite", true);
                    context.Put("spaceurl", this.GetSpaceURL(inviteid));
                    context.Put("userhead", this.GetHeadImage(inviteid));
                    context.Put("username", mdl.TrueName);
                }

                #region 开关设置
                string isRealName = Public.GetXMLValue("isRealName");
                if (isRealName == "1")
                {
                    context.Put("isrealname", string.Empty);
                }
                if (loginCode == "1")
                {
                    context.Put("isvcode", string.Empty);
                }
                if (!string.IsNullOrEmpty(errSTR))
                {
                    context.Put("colse", errSTR);
                }
                if (isRegMobile == "1")
                {
                    context.Put("mobile", "/手机");
                }
                else
                {
                    context.Put("mobile", string.Empty);
                }
                #endregion 
            }
            
        }

        public void GetBirthdayYear(ref NVelocity.VelocityContext context)
        {
            string listSTR = string.Empty;
            for (int i = 2006; i > 1960; i--)
            {
                listSTR += "<option value=\"" + i + "\">" + i + "</option>";
            }
            context.Put("birthdayYear", listSTR);
        }

        public void GetBirthdayMonth(ref NVelocity.VelocityContext context)
        {
            string listSTR = string.Empty;
            for (int i = 1; i < 13; i++)
            {
                listSTR += "<option value=\"" + i + "\">" + i + "</option>";
            }
            context.Put("birthdayMonth", listSTR);
        }
        public void GetBirthdayDay(ref NVelocity.VelocityContext context)
        {
            string listSTR = string.Empty;
            for (int i = 1; i < 32; i++)
            {
                listSTR += "<option value=\"" + i + "\">" + i + "</option>";
            }
            context.Put("birthdayDay", listSTR);
        }

        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            ShowInfo(ref context);
            #region 合法性判断
            string username = GetFormString("username");
            string email = GetFormString("email");
            string password = GetFormString("password");
            string confimpassword = GetFormString("confimpassword");
            string truename = GetFormString("truename");
            int inviteid = GetFormInt("inviteid", 0);
            string replayEmail = GetFormString("replayEmail");
            if (string.IsNullOrEmpty(truename))
            {
                truename = username;
            }
            int provinceid = GetFormInt("SlctProvince", 0);
            int city = GetFormInt("SlctCity", 0);
            int sex = GetFormInt("sex", 0);
            string vcode = GetFormString("vcode");
            string errSTR = string.Empty;

            if (string.IsNullOrEmpty(username)) { errSTR += "用户名 &nbsp; "; }
            if (string.IsNullOrEmpty(email)) { errSTR += "电子邮件 &nbsp; "; }
            if (string.IsNullOrEmpty(password)) { errSTR += "密码 &nbsp; "; }
            if (string.IsNullOrEmpty(confimpassword))
            {
                errSTR += "密码不正确 &nbsp; ";
            }
            else
            {
                if (confimpassword != password)
                {
                    errSTR += "两次密码不一致 &nbsp; ";
                }
            }
            string errSTR1 = string.Empty;
            if (!JuSNS.Common.Input.isEmail(email)) { errSTR1 += "电子邮件格式不正确！ "; }
            if (string.IsNullOrEmpty(truename)) { errSTR += "真实姓名 &nbsp; "; }
            string birthdayYear = GetString("birthdayYear");
            string birthdayMonth = GetString("birthdayMonth");
            string birthdayDay = GetString("birthdayDay");
            if (string.IsNullOrEmpty(birthdayYear) || string.IsNullOrEmpty(birthdayMonth) || string.IsNullOrEmpty(birthdayDay)) { errSTR += "生日 &nbsp; "; }
            if (city == 0) { errSTR += "目前你居住地 &nbsp; "; }
            if (loginCode == "1")
            {
                if (HttpContext.Current.Session["JuSNSCheckCode"] == null)
                {
                    errSTR += "验证码不正确 &nbsp; ";
                }
                else
                {
                    if (vcode != HttpContext.Current.Session["JuSNSCheckCode"].ToString())
                    {
                        errSTR += "验证码不正确 &nbsp; ";
                    }
                }
            }
            #endregion
            if (!string.IsNullOrEmpty(errSTR) || !string.IsNullOrEmpty(errSTR1))
            {
                if (!string.IsNullOrEmpty(errSTR))
                {
                    context.Put("errors", errSTR);
                }
                else
                {
                    context.Put("errors", errSTR1);
                }
            }
            else
            {
                if (JuSNS.Home.User.User.Instance.CheckUserExsit(username, 1))
                {
                    if (!string.IsNullOrEmpty(errSTR))
                    {
                        context.Put("errors", "用户名(" + username + ")已经存在！请重新选择一个用户名");
                    }
                }
                else
                {
                    if (JuSNS.Home.User.User.Instance.CheckUserExsit(email, 0))
                    {
                        context.Put("errors", "电子邮件(" + email + ")已经存在！请重新选择一个电子邮件");
                    }
                    else
                    {
                        UserInfo ui = new UserInfo();
                        ui.UserName = username;
                        ui.Email = email;
                        ui.Password = Input.MD5(password, false);
                        ui.ProvinceID = provinceid;
                        ui.City = city;
                        ui.TrueName = truename;
                        ui.InviterID = inviteid;
                        ui.Sex = Convert.ToByte(sex);
                        UserBaseInfo basi = new UserBaseInfo();
                        string bday = birthdayYear + "-" + birthdayMonth + "-" + birthdayDay;
                        basi.Birthday = Convert.ToDateTime(bday);
                        int uid = 0;
                        string uName = string.Empty;
                        EnumRegister ret = JuSNS.Home.User.User.Instance.Register(ui, basi, out uid);
                        switch (ret)
                        {
                            case EnumRegister.Succeed:
                                //如果是邀请注册，则相互加为好友
                                if (inviteid > 0)
                                {
                                    FriendInfo mdl = new FriendInfo();
                                    mdl.ClassID = 0;
                                    mdl.Descript = "邀请注册";
                                    mdl.FDegree = 0;
                                    mdl.FriendID = inviteid;
                                    mdl.PostTime = DateTime.Now;
                                    mdl.State = 0;
                                    mdl.UserID = uid;
                                    int n = JuSNS.Home.User.User.Instance.InsertFriend(mdl, 1);
                                    if (!string.IsNullOrEmpty(replayEmail) && replayEmail.Length > 5)
                                    {
                                        UpdateInvite(inviteid, uid, replayEmail);
                                    }
                                    //为邀请注册的用户增加积分
                                    JuSNS.Home.User.User.Instance.UpdateInte(inviteid, Public.JSplit(1), 0, 0, "邀请好友注册！");
                                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, inviteid, 0, (int)EnumDynType.InviteJoinSite, string.Empty, DateTime.Now, uid, string.Empty));
                                }
                                SetCookie(uid, JuSNS.Home.User.User.Instance.GetUserInfo(uid).UserName, JuSNS.Common.Input.MD5(password, false), false);
                                //插入默认好友
                                //InsertDefault(uid);
                                context.Put("redirecturl", "result" + ExName + "?uid=" + uid);
                                break;
                            case EnumRegister.SucceedNotMail:
                                //如果是邀请注册，则相互加为好友
                                if (inviteid > 0)
                                {
                                    FriendInfo mdl = new FriendInfo();
                                    mdl.ClassID = 0;
                                    mdl.Descript = "邀请注册";
                                    mdl.FDegree = 0;
                                    mdl.FriendID = inviteid;
                                    mdl.PostTime = DateTime.Now;
                                    mdl.State = 0;
                                    mdl.UserID = uid;
                                    int n = JuSNS.Home.User.User.Instance.InsertFriend(mdl, 1);
                                    if (!string.IsNullOrEmpty(replayEmail) && replayEmail.Length > 5)
                                    {
                                        UpdateInvite(inviteid, uid, replayEmail);
                                    }
                                    //为邀请注册的用户增加积分
                                    JuSNS.Home.User.User.Instance.UpdateInte(inviteid, Public.JSplit(1), 0, 0, "邀请好友注册！");
                                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, inviteid, 0, (int)EnumDynType.InviteJoinSite, string.Empty, DateTime.Now, uid, string.Empty));
                                }
                                SetCookie(uid, JuSNS.Home.User.User.Instance.GetUserInfo(uid).UserName, JuSNS.Common.Input.MD5(password, false), false);
                                context.Put("redirecturl", "result" + ExName + "?uid=" + uid);
                                break;
                            case EnumRegister.EmailRepeat:
                                errSTR = "该Email(" + email + ")地址已注册过帐户！";
                                break;
                            default:
                                errSTR = "系统错误,请稍后再试";
                                break;
                        }
                        if (!string.IsNullOrEmpty(errSTR))
                        {
                            context.Put("errors", errSTR);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新好友邀请表，并回复
        /// </summary>
        /// <returns></returns>
        protected void UpdateInvite(int UserID,int uID,string email)
        {
            JuSNS.Home.User.User.Instance.ReplayInvite(UserID, uID, email);
        }

    }
}
