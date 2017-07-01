using System;
using System.Web;
using System.Reflection;
using System.IO;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.library.page
{
    /// <summary>
    /// AJAX
    /// </summary>
    public class ajax : BasePage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            string fn = GetString("action");
            
            MethodInfo methodInfo;

            methodInfo = this.GetType().GetMethod(fn, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);

            try
            {
                methodInfo.Invoke(this, null);
            }
            catch { }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            string fn = GetFormString("action");

            MethodInfo methodInfo;

            methodInfo = this.GetType().GetMethod(fn, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);

            try
            {
                methodInfo.Invoke(this, null);
            }
            catch{ }
        }

        /// <summary>
        /// 登录
        /// </summary>
        protected void LoginAjax()
        {
            string resultSTR = string.Empty;
            string username = GetString("username");
            string password = GetString("password");
            string vcode = GetString("vcode");
            string error = string.Empty;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                error = "用户名和密码必须填写";
            }
            int iscode = Convert.ToInt32(Public.GetXMLValue("loginCode"));
            if (iscode == 1)
            {
                if (HttpContext.Current.Session["JuSNSCheckCode"] != null)
                {
                    if (vcode != HttpContext.Current.Session["JuSNSCheckCode"].ToString())
                    {
                        error += "验证码不正确";
                    }
                }
                else
                {
                    error += "验证码不正确";
                }
            }
            if (!string.IsNullOrEmpty(error))
            {
                resultSTR = error;
            }
            else
            {
                EnumLoginState rev;
                int uid = 0;
                string uName = string.Empty;
                string trueName = string.Empty;
                int LoginNum = 0;
                if (JuSNS.Common.Input.isEmail(username))
                {
                    //电子邮件登录
                    rev = JuSNS.Home.User.User.Instance.Login(username, JuSNS.Common.Input.MD5(password, false), out uid, out uName, out trueName, out LoginNum, 0);
                }
                else if (JuSNS.Common.Input.isMobile(username))
                {
                    //手机登录
                    rev = JuSNS.Home.User.User.Instance.Login(username, JuSNS.Common.Input.MD5(password, false), out uid, out uName, out trueName, out LoginNum, 2);
                }
                else
                {
                    //用户名登录
                    rev = JuSNS.Home.User.User.Instance.Login(username, JuSNS.Common.Input.MD5(password, false), out uid, out uName, out trueName, out LoginNum, 1);
                }
                switch (rev)
                {
                    case EnumLoginState.Succeed:
                        SetCookie(uid, uName, JuSNS.Common.Input.MD5(password, false), false);
                        resultSTR = "登录成功Succeed";
                        break;
                    case EnumLoginState.Err_UnActivation:
                        resultSTR = "电子邮件没激活";
                        break;
                    case EnumLoginState.Err_NameOrPwdError:
                        resultSTR = "用户名或密码错误";
                        break;
                    case EnumLoginState.Err_Locked:
                        resultSTR = "您已经被锁定";
                        break;
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 加入或卸载应用程序
        /// </summary>
        protected void JoinAppAjax()
        {
            string resultSTR = string.Empty;
            int appid = GetInt("appid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            if (uid == 0)
            {
                resultSTR = "请先登录";
            }
            else
            {
                try
                {
                    string filepath = "~/space/info/my/user" + uid + ".config";
                    Public.CreatUserConfig(filepath);
                    string menulistNames = Public.GetXMLValue("menulist", filepath);
                    if (flag == 1)
                    {
                        AppInfo info = JuSNS.App.App.Instance.GetAppInfo(appid);
                        if (info != null)
                        {
                            int n = JuSNS.App.App.Instance.InsertSetupApp(appid, uid, 1);
                            if (n > 0)
                            {
                                JuSNS.App.App.Instance.UpdateAppSetup(appid);
                                Public.setXmlInnerText(filepath, "/configuration/menulist", menulistNames + "," + info.Appname + "|center/appinfo" + ExName + "?appid=" + appid);
                                resultSTR = "操作成功！succs";
                            }
                            else
                            {
                                resultSTR = "操作失败";
                            }
                        }
                        else
                        {
                            resultSTR = "发生错误。代码：Param Error!";
                        }
                    }
                    else
                    {
                        int n = JuSNS.App.App.Instance.InsertSetupApp(appid, uid, 0);
                        if (n > 0)
                        {
                            string tmpSTR = string.Empty;
                            string[] menulistNamesARR = menulistNames.Split(',');
                            for (int i = 0; i < menulistNamesARR.Length; i++)
                            {
                                if (menulistNamesARR[i].IndexOf("|center/appinfo" + ExName + "?appid=" + appid) == -1)
                                {
                                    tmpSTR += menulistNamesARR[i] + ",";
                                }
                            }
                            tmpSTR = Input.FixCommaStr(tmpSTR);
                            Public.setXmlInnerText(filepath, "/configuration/menulist", tmpSTR);
                            resultSTR = "操作成功！succs";
                        }
                        else
                        {
                            resultSTR = "操作失败";
                        }
                    }
                }
                catch
                {
                    resultSTR = "失败！";
                }
            }
            OutText(resultSTR);
        }

        /// <summary>
        /// 设置菜单
        /// </summary>
        protected void SetMenuDefaultAjax()
        {
            string resultSTR = string.Empty;
            int uid = GetInt("uid", 0);
            if (uid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                try
                {
                    //string filepath = "~/space/info/my/user" + uid + ".config";
                    //Public.CreatUserConfig(filepath);
                    //Public.setXmlInnerText(filepath, "/configuration/menulist", menulistNames);
                    //context.Put("rights", "菜单排序保存成功");

                    //string smenulist = Public.Menulist(0, false);
                    string filepath = "~/space/info/my/user" + uid + ".config";
                    Public.setXmlInnerText(filepath, "/configuration/menulist", Public.GetXMLValue("menulist", "~/space/info/my/user0.config"));
                    resultSTR = "操作成功！succs";
                }
                catch
                {
                    resultSTR = "失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 检查帐户是否存在
        /// </summary>
        protected void CheckUserExsit()
        {
            string values = GetString("values");
            int flags = GetInt("flag",0);
            string resultSTR = string.Empty;
            int flag = 1;
            if (flags == 1)
            {
                flag = 0;
            }
            else
            {
                if (JuSNS.Common.Input.isMobile(values))
                {
                    flag = 2;
                }
            }
            if (JuSNS.Home.User.User.Instance.CheckUserExsit(values, flag))
            {
                resultSTR = "1";
            }
            else
            {
                resultSTR = "0";
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 修改电子邮件
        /// </summary>
        protected void ModifyEmail()
        {
            string values = GetString("values");
            int uid = GetInt("uid", 0);
            string resultSTR = string.Empty;
            if (JuSNS.Home.User.User.Instance.CheckEmail(uid, values))
            {
                resultSTR = "电子邮件(" + values + ")已经存在，请重新选择邮件修改！";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.ChangeEmail(uid, values);
                //0修改成功，1你修改过了，还未激活,2修改成功，但未发送电子邮件
                switch (n)
                {
                    case 0:
                        resultSTR = "你申请使用 Email(" + values + ")作为登录邮箱，我们向该地址发送了一封邮件以确认其有效，请查收。激活后才能登录！";
                        break;
                    case 1:
                        resultSTR = "<strong>你已经修改过电子邮件了！</strong><br />但未激活电子邮件！您可以：<a href=\"" + root + "/Active" + ExName + "?action=modifyemail&email=" + values + "&uid=" + uid + "\">重新获取验证邮件</a>。";
                        break;
                    case 2:
                        resultSTR = "<strong>修改电子邮件(" + values + ")成功！</strong><br />由于系统原因，未发送激活电子邮件。<br /><a href=\"" + root + "/Active" + ExName + "?action=modifyemail&email=" + values + "&uid=" + uid + "\">重新获取验证邮件</a>。";
                        break;
                    //default:
                    //    resultSTR = "你申请使用 Email(" + values + ")作为登录邮箱，我们向该地址发送了一封邮件以确认其有效，请查收。激活后才能登录！";
                    //    break;
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 更新手机
        /// </summary>
        protected void ModifyMobile()
        {
            string values = GetString("values");
            int uid = GetInt("uid", 0);
            string resultSTR = string.Empty;
            if (!Input.isMobile(values))
            {
                resultSTR = "手机格式不正确，请不要在手机前面加“0”";
            }
            else
            {
                int n=JuSNS.Home.User.User.Instance.ChangeMobile(uid, values);
                if (n > 0)
                {
                    resultSTR = "修改手机成功！请绑定手机后生效";
                }
                else if (n == -2)
                {
                    resultSTR = "修改手机失败。手机已存在了！";
                }
                else
                {
                    resultSTR = "修改手机失败。代码：MobileUpdate";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除教育信息
        /// </summary>
        protected void DeleteEduAjax()
        {
            string resultSTR = string.Empty;
            int eid = GetInt("eid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || eid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.DeleteEducation(uid, eid);
                if (n > 0)
                {
                    resultSTR = "教育信息删除成功！succs";
                }
                else
                {
                    resultSTR = "教育信息删除失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除工作信息
        /// </summary>
        protected void DeleteCarAjax()
        {
            string resultSTR = string.Empty;
            int cid = GetInt("cid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || cid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.DeleteCareer(uid, cid);
                if (n > 0)
                {
                    resultSTR = "工作信息删除成功！succs";
                }
                else
                {
                    resultSTR = "工作信息删除失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除头像
        /// </summary>
        protected void DeleteHeadAjax()
        {
            string resultSTR = string.Empty;
            int hid = GetInt("hid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || hid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Photo.Instance.Delete(hid, uid);
                if (n > 0)
                {
                    resultSTR = "头像删除成功！succs";
                }
                else
                {
                    resultSTR = "头像删除失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 设置头像
        /// </summary>
        protected  void SetHeadAjax()
        {
            string resultSTR = string.Empty;
            int hid = GetInt("hid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || hid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.UpdateUserHead(hid, uid);
                if (n > 0)
                {
                    resultSTR = "设置头像成功。请刷新页面！succs";
                }
                else
                {
                    resultSTR = "设置头像失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除好友
        /// </summary>
        protected void DeleteFriendAjax()
        {
            string resultSTR = string.Empty;
            int fid = GetInt("fid", 0);
            int uid = GetInt("uid", 0);
            string truename = GetString("truename");
            if (uid == 0 || fid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.DeleteFriend(fid, uid);
                if (n > 0)
                {
                    resultSTR = "已经断开和" + truename + "的好友关系！succs";
                }
                else
                {
                    resultSTR = "操作失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 插入或修改好友分类
        /// </summary>
        protected void InsertOrModifyFriendClassAjax()
        {
            string resultSTR = string.Empty;
            int fid = GetInt("fid", 0);
            int uid = GetInt("uid", 0);
            string cname = GetString("cname");
            if (uid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.InsertFriendClass(cname, uid, fid);
                if (n > 0)
                {
                    resultSTR = "succs";
                }
                else
                {
                    resultSTR = "操作失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除好友分类
        /// </summary>
        protected void DeleteFriendClassAjax()
        {
            string resultSTR = string.Empty;
            int fid = GetInt("fid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || fid == 0)
            {
                resultSTR = "发生错误。代码：Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.DeleteFriendClass(fid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "操作失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除参观者
        /// </summary>
        protected void DeleteVisitAjax()
        {
            string resultSTR = string.Empty;
            int vid = GetInt("vid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || vid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.DeleteVisite(vid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "操作失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除Twitter
        /// </summary>
        protected void DeleteTwitterAjax()
        {
            string resultSTR = string.Empty;
            int tid = GetInt("tid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || tid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.TWrite.Instance.DeleteTwitter(tid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "操作失败！";
                }
            }
            OutText(resultSTR);
        }
        #region 日志
        /// <summary>
        /// 删除日志
        /// </summary>
        protected void DeleteBlogAjax()
        {
            string resultSTR = string.Empty;
            int bid = GetInt("bid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || bid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Blog.Instance.DeleteBlog(bid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "操作失败！";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// ATT
        /// </summary>
        protected void AttInfoAjax()
        {
            int n = 0;
            int bid = GetInt("bid", 0);
            int uid = GetInt("uid", 0);
            string info = GetString("info");
            if (uid != 0 && bid != 0)
            {
                switch (info)
                {
                    case "blog":
                        n = JuSNS.Home.App.Blog.Instance.UpdateATT(bid, uid);
                        break;
                    case "news":
                        n = JuSNS.Home.App.News.Instance.UpdateATT(bid, uid);
                        break;
                    case "user":
                        if (uid == bid)
                        {
                            n= -2;
                        }
                        else
                        {
                            n = JuSNS.Home.User.User.Instance.UpdateATT(bid, uid);
                        }
                        break;
                }
            }
            OutText(n);
        }
        /// <summary>
        /// 增加日志分类
        /// </summary>
        protected void AddBlogClassAjax()
        {
            string resultSTR = string.Empty;
            string sortname = GetString("sortname");
            int uid = GetInt("uid", 0);
            if (uid == 0 || sortname == "")
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Blog.Instance.AddSortBlogClass(sortname, uid);
                if (n > 0)
                {
                    resultSTR = n.ToString();
                }
                else
                {
                    resultSTR = "0";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除日志分类
        /// </summary>
        protected void DeleteBlogClassAjax()
        {
            string resultSTR = string.Empty;
            int bid = GetInt("bid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || bid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Blog.Instance.DeleteBlogClass(uid, bid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "删除失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除日志评论
        /// </summary>
        protected void DeleteBlogCommentAjax()
        {
            string resultSTR = string.Empty;
            int cid = GetInt("cid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || cid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Blog.Instance.DeleteBlogComment(cid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "删除失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除商品评论
        /// </summary>
        protected void DeleteGoodsCommentAjax()
        {
            string resultSTR = string.Empty;
            int cid = GetInt("cid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || cid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Shop.Instance.DeleteShopComment(cid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "删除失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 删除新闻评论
        /// </summary>
        protected void DeleteNewsCommentAjax()
        {
            string resultSTR = string.Empty;
            int cid = GetInt("cid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || cid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.News.Instance.DeleteNewsComment(cid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "删除失败";
                }
            }
            OutText(resultSTR);
        }

        #endregion
        /// <summary>
        /// 删除新闻
        /// </summary>
        protected void DeleteNewsAjax()
        {
            string resultSTR = string.Empty;
            int nid = GetInt("nid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || nid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.News.Instance.DeleteNews(nid, uid);
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "删除失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 设置群组置顶
        /// </summary>
        protected void SetGroupTopAjax()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            if (uid == 0 || infoid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Group.Instance.SetGroupTop(infoid, uid, flag);
                if (n > 0)
                {
                    resultSTR = "置顶成功succs";
                }
                else
                {
                    resultSTR = "置顶失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 设置帖子精华
        /// </summary>
        protected void SetGroupBestAjax()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            if (uid == 0 || infoid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Group.Instance.SetGroupBest(infoid, uid, flag);
                if (n > 0)
                {
                    resultSTR = "置顶成功succs";
                }
                else
                {
                    resultSTR = "置顶失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 发送评论
        /// </summary>
        protected void SendCommentAjax()
        {
            string resultSTR = string.Empty;
            int cid = GetInt("cid", 0);
            int bid = GetInt("bid", 0);
            int uid = GetInt("uid", 0);
            string cont = GetString("cont");
            string type = GetString("tp");
            if (uid == 0 || cid == 0 || string.IsNullOrEmpty(cont) || string.IsNullOrEmpty(type))
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.SendCommentReplay(bid, cid, uid, cont, type);
                if (n > 0)
                {
                    resultSTR = "回复成功succs";
                }
                else
                {
                    resultSTR = "回复失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 加入团购
        /// </summary>
        protected void JoinMulteAjax()
        {
            string resultSTR = string.Empty;
            int mid = GetInt("mid", 0);
            int uid = GetInt("uid", 0);
            string cont = GetString("cont");
            int n = 0;
            if (uid == 0 || mid == 0||string.IsNullOrEmpty(cont))
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Shop.Instance.JoinMulte(mid, uid, cont);
                if (n > 0)
                {
                    //插入动态
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.JoinMulte, string.Empty, DateTime.Now, mid, string.Empty));
                    JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(40), 0, 0, "加入团购");
                }
            }
            OutText(n);
        }
        /// <summary>
        /// 公共推荐
        /// </summary>
        protected void RecALLAjax()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            string type = GetString("type");
            int flag = GetInt("flag", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || infoid == 0 || string.IsNullOrEmpty(type))
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Web.Instance.RecAll(infoid, uid, flag, type);
                if (n > 0)
                {
                    resultSTR = "成功succs";
                }
                else
                {
                    resultSTR = "推荐失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 插入订单
        /// </summary>
        protected void ChargeOrderAjax()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            int flag = GetInt("flag", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || infoid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid))
                {
                    int n = JuSNS.Home.User.User.Instance.UpdateChargeOrderState(infoid, flag);
                    if (n > 0)
                    {
                        resultSTR = "成功succs";
                    }
                    else
                    {
                        resultSTR = "推荐失败";
                    }
                }
            }
            OutText(resultSTR);
        }

        /// <summary>
        /// 公共删除
        /// </summary>
        protected void DeleteALLAjax()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            string type = GetString("type");
            int uid = GetInt("uid", 0);
            if (uid == 0 || infoid == 0 || string.IsNullOrEmpty(type))
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = 0;
                switch (type)
                {
                    case "album":
                        n = JuSNS.Home.App.Album.Instance.DeleteAlbum(infoid, uid);
                        break;
                    case "photo":
                        n = JuSNS.Home.App.Photo.Instance.Delete(infoid, uid);
                        break;
                    case "photocomment":
                        n = JuSNS.Home.App.Photo.Instance.DeletePhotoComment(infoid, uid);
                        break;
                    case "group":
                        n = JuSNS.Home.App.Group.Instance.DeleteGroup(infoid, uid);
                        break;
                    case "grouptopic":
                        n = JuSNS.Home.App.Group.Instance.DeleteGroupTopic(infoid, uid);
                        break;
                    case "groupclass":
                        n = JuSNS.Home.App.Group.Instance.DeleteGroupClass(infoid, uid);
                        break;
                    case "groupfiles":
                        n = JuSNS.Home.App.Group.Instance.DeleteGroupFile(infoid, uid);
                        break;
                    case "ask":
                        n = JuSNS.Home.App.Ask.Instance.DeleteAsk(infoid, uid);
                        break;
                    case "askclass":
                        n = JuSNS.Home.App.Ask.Instance.DeleteAskClass(infoid, uid);
                        break;
                    case "ative":
                        n = JuSNS.Home.App.Ative.Instance.DeleteAtive(infoid, uid);
                        break;
                    case "activeclass":
                        n = JuSNS.Home.App.Ative.Instance.DeleteAtiveClass(infoid, uid);
                        break;
                    case "ativecomment":
                        n = JuSNS.Home.App.Ative.Instance.DeleteAtiveComment(infoid, uid);
                        break;
                    case "goods":
                        n = JuSNS.Home.App.Shop.Instance.DeleteGoods(infoid, uid);
                        break;
                    case "shop":
                        n = JuSNS.Home.App.Shop.Instance.DeleteShop(infoid, uid);
                        break;
                    case "shopclass":
                        n = JuSNS.Home.App.Shop.Instance.DeleteShopClass(infoid, uid);
                        break;
                    case "multe":
                        n = JuSNS.Home.App.Shop.Instance.DeleteMulte(infoid, uid);
                        break;
                    case "order":
                        n = JuSNS.Home.App.Shop.Instance.DeleteOrder(infoid, uid);
                        break;
                    case "share":
                        n = JuSNS.Home.App.Share.Instance.DeleteShare(infoid, uid);
                        break;
                    case "calend":
                        n = JuSNS.Home.User.User.Instance.DeleteCalend(infoid, uid);
                        break;
                    case "poke":
                        n = JuSNS.Home.User.User.Instance.DeletePoke(infoid, uid);
                        break;
                    case "vote":
                        n = JuSNS.Home.App.Vote.Instance.DeleteVote(infoid, uid);
                        break;
                    case "voteto":
                        n = JuSNS.Home.App.Vote.Instance.DeleteVoteTo(infoid, uid);
                        break;
                    case "favorite":
                        n = JuSNS.Home.User.User.Instance.deleteFavorite(infoid, uid);
                        break;
                    case "favoriteclass":
                        n = JuSNS.Home.User.User.Instance.deleteFavoriteClass(infoid, uid);
                        break;
                    case "mailbox":
                        n = JuSNS.Home.User.User.Instance.DleteMailBox(infoid, uid);
                        break;
                    case "mailsend":
                        n = JuSNS.Home.User.User.Instance.DleteMailSend(infoid, uid);
                        break;
                    case "gbook":
                        n = JuSNS.Home.User.User.Instance.DeleteGbook(infoid, uid);
                        break;
                    case "groupmember":
                        n = JuSNS.Home.App.Group.Instance.DeleteGroupMember(infoid, uid);
                        break;
                    case "twitter":
                        n = JuSNS.Home.App.TWrite.Instance.DeleteTwitter(infoid, uid);
                        break;
                    case "twittercomment":
                        n = JuSNS.Home.App.TWrite.Instance.DeleteTwitterComment(infoid, uid);
                        break;
                    case "help":
                        n = JuSNS.Home.App.Web.Instance.deletehelp(infoid, uid);
                        break;
                    case "space":
                        n = JuSNS.Home.User.User.Instance.DeleteSpace(infoid, uid);
                        break;
                    case "user":
                        n = JuSNS.Home.User.User.Instance.DeleteUser(infoid, uid);
                        break;
                    case "userall":
                        n = JuSNS.Home.User.User.Instance.DeleteUserAll(uid);
                        break;
                    case "newsclass":
                        n = JuSNS.Home.App.Web.Instance.DeleteNewsClass(infoid, uid);
                        break;
                    case "newscomment":
                        n = JuSNS.Home.App.News.Instance.DeleteNewsComment(infoid, uid);
                        break;
                    case "area":
                        n = JuSNS.Home.Other.Area.Instance.DeleteArea(infoid, uid);
                        break;
                    case "charges":
                        n = JuSNS.Home.User.User.Instance.DeleteChargeOrder(infoid, uid);
                        break;
                    case "flash":
                        n = JuSNS.Home.User.User.Instance.DeleteFlash(infoid, uid);
                        break;
                    case "blog":
                        n = JuSNS.Home.App.Blog.Instance.DeleteBlog(infoid, uid);
                        break;
                    case "blogclass":
                        n = JuSNS.Home.App.Blog.Instance.DeleteBlogClass(uid, infoid);
                        break;
                    case "blogcomment":
                        n = JuSNS.Home.App.Blog.Instance.DeleteBlogComment(infoid, uid);
                        break;
                    case "goodscomment":
                        n = JuSNS.Home.App.Shop.Instance.DeleteShopComment(infoid, uid);
                        break;
                    case "gift":
                        n = JuSNS.Home.User.User.Instance.DeleteGift(infoid, uid);
                        break;
                    case "giftclass":
                        n = JuSNS.Home.User.User.Instance.DeleteGiftClass(infoid, uid);
                        break;
                    case "shopnews":
                        n = JuSNS.Home.App.Shop.Instance.DeleteShopNews(infoid);
                        break;
                    case "ads":
                        n = JuSNS.Home.App.Web.Instance.DeleteAds(infoid, uid);
                        break;
                    case "report":
                        n = JuSNS.Home.App.Web.Instance.DeleteReport(infoid, uid);
                        break;
                    case "bookadmin":
                        n = JuSNS.Home.User.User.Instance.DeleteGbook(infoid, uid);
                        break;
                    case "app":
                        n = JuSNS.App.App.Instance.DeleteApp(infoid, uid);
                        break;
                    case "appdev":
                        n = JuSNS.App.App.Instance.DeleteAppdev(infoid, uid);
                        break;
                    case "dyn":
                        n = JuSNS.Home.App.Web.Instance.DeleteDyn(infoid, uid);
                        break;
                    case "links":
                        n = JuSNS.Home.App.Web.Instance.DeleteLinks(infoid, uid);
                        break;
                }
                if (n > 0)
                {
                    resultSTR = "删除成功succs";
                }
                else
                {
                    resultSTR = "删除失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 退出群组
        /// </summary>
        protected void OutGroupAjax()
        {
            string resultSTR = string.Empty;
            int gid = GetInt("gid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || gid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Group.Instance.OutGroup(gid, uid);
                if (n > 0)
                {
                    if (n == 2)
                    {
                        resultSTR = "创始人不能退出。creat";
                    }
                    else
                    {
                        resultSTR = "退出succs";
                    }
                }
                else
                {
                    resultSTR = "退出失败";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 设置公共状态
        /// </summary>
        protected void SetALLState()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            string type = GetString("type");
            if (uid == 0 || infoid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Web.Instance.SetAllState(infoid, uid, flag, type);
                if (n > 0)
                {
                    resultSTR = "succs";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected void DeleteStartAjax()
        {
            string resultSTR = string.Empty;
            string type = GetString("type");
            if (string.IsNullOrEmpty(type))
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Web.Instance.Start(type);
                if (n > 0)
                {
                    resultSTR = "succs";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 设置公共状态
        /// </summary>
        protected void CheckInfoState()
        {
            string resultSTR = string.Empty;
            int infoid = GetInt("infoid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            string type = GetString("type");
            if (uid == 0 || infoid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.App.Web.Instance.CheckInfoState(infoid, uid, flag, type);
                if (n > 0)
                {
                    resultSTR = "succs";
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 加入群组
        /// </summary>
        protected void JoinGroupAjax()
        {
            string resultSTR = string.Empty;
            int gid = GetInt("gid", 0);
            int uid = GetInt("uid", 0);
            if (uid == 0 || gid == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                //0成功，1需要审核，2拒绝加入，3加入失败
                int n = JuSNS.Home.App.Group.Instance.JoinGroup(gid, uid);
                switch (n)
                {
                    case -1:
                        resultSTR = "joined";
                        break;
                    case 0:
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.JoinGroup, string.Empty, DateTime.Now, gid, string.Empty));
                        resultSTR = "succs";
                        break;
                    case 1:
                        resultSTR = "check";
                        break;
                    case 2:
                        resultSTR = "none";
                        break;
                    case 3:
                        resultSTR = "error";
                        break;
                    case 4:
                        resultSTR = "max";
                        break;
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 购买道具
        /// </summary>
        protected void BuyMagic()
        {
            string resultSTR = string.Empty;
            int mid = GetInt("mid", 0);
            int uid = GetInt("uid", 0);
            int num = GetInt("num", 0);
            if (uid == 0 || mid == 0 || num == 0)
            {
                resultSTR = "发生错误。Param Error!";
            }
            else
            {
                int n = JuSNS.Home.User.User.Instance.BuyMagic(uid, mid, num);
                switch (n)
                {
                    case 0:
                        resultSTR = "购买成功succs";
                        break;
                    case 1:
                        resultSTR = "积分不够";
                        break;
                    case 2:
                        resultSTR = "金币不够";
                        break;
                    case 3:
                        resultSTR = "库存不够";
                        break;
                    case 4:
                        resultSTR = "异常错误：UpdateMagicError";
                        break;
                }
            }
            OutText(resultSTR);
        }
        /// <summary>
        /// 设置问答最佳答案
        /// </summary>
        protected void SetAskBestAjax()
        {
            int infoid = GetInt("infoid", 0);
            int uid = GetInt("uid", 0);
            int mid = GetInt("mid", 0);
            int userid = GetInt("userid", 0);
            int n = 0;
            if (uid == 0 || infoid == 0 || mid == 0)
            {
                n = 4;
            }
            else
            {
                //0成功，1已经有最佳答案了，2问题已经关闭，3不是自己的问题，4设置失败
                n = JuSNS.Home.App.Ask.Instance.SetAskBest(uid, infoid,mid,userid);
                if (n == 0)
                {
                    JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), userid, "您的答案设置为了最佳答案", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.SetAskBest, mid));
                    //插入动态
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, userid, 0, (int)EnumDynType.AskBest, string.Empty, DateTime.Now, mid, string.Empty));
                    //更新积分
                    JuSNS.Home.User.User.Instance.UpdateInte(userid, Public.JSplit(37), 0, 0, "问答设置为最佳答案(系统奖励)");
                }
            }
            OutText(n);
        }
        /// <summary>
        /// 设置群组管理员
        /// </summary>
        protected void SetAdminAjax()
        {
            int gid = GetInt("gid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            int userid = GetInt("userid", 0);
            int n = 0;
            if (uid == 0 || userid == 0 || gid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Group.Instance.SetGroupAdmin(gid, userid, uid, flag);
            }
            OutText(n);
        }

        protected void SetLightAjax()
        {
            int gid = GetInt("gid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            int n = 0;
            if (uid == 0  || gid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Web.Instance.SetGroupLight(gid, uid, flag);
            }
            OutText(n);
        }
        /// <summary>
        /// 更新商品信息
        /// </summary>
        protected void UpdateGoodsNumber()
        {
            int gid = GetInt("gid", 0);
            int uid = GetInt("uid", 0);
            string flag = GetString("flag");
            int n = 0;
            if (uid == 0 || gid == 0)
            {
                n = 0;
            }
            else
            {
                ShopGoodsInfo mdl = null;
                if (flag == "up")
                {
                    n = JuSNS.Home.App.Shop.Instance.UpdateGoodsState(gid, 1);
                    mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
                    if (n > 0)
                    {
                        n = mdl.TopNumber;
                    }
                }
                else
                {
                    n = JuSNS.Home.App.Shop.Instance.UpdateGoodsState(gid, 2);
                    mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
                    if (n > 0)
                    {
                        n = mdl.DownNumber;
                    }
                }
            }
            OutText(n);
        }
        /// <summary>
        /// 加入活动
        /// </summary>
        protected void JoinAtiveAjax()
        {
            int aid = GetInt("aid", 0);
            int uid = GetInt("uid", 0);
            int flag = GetInt("flag", 0);
            int n = 0;
            if (uid == 0 || aid == 0 || flag == 0)
            {
                n = 0;
            }
            else
            {
                //0失败，1参与了但是需要审核，2成功，3已经参与了，参与人已经达到上限。
                n = JuSNS.Home.App.Ative.Instance.JoinAtive(aid, uid, flag);
                if (n == 2)
                {
                    //插入动态
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.JoinActive, string.Empty, DateTime.Now, aid, string.Empty));
                    //更新积分
                    JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(18), 0, 0, "加入活动");
                }
            }
            OutText(n);
        }
        /// <summary>
        /// 分享
        /// </summary>
        protected void ShareAjax()
        {
            int infoid = GetInt("oid", 0);
            int uid = GetInt("uid", 0);
            string flag = GetString("flag");
            string sharetitle = GetString("sharetitle");
            string sharecontent = GetString("sharecontent");
            byte shareType = 0;
            switch (flag)
            {
                case "news":
                    shareType = (byte)EnumShareType.ForNews;
                    break;
                case "active":
                    shareType = (byte)EnumShareType.ForActive;
                    break;
                case "album":
                    shareType = (byte)EnumShareType.ForAlbum;
                    break;
                case "ask":
                    shareType = (byte)EnumShareType.ForAsk;
                    break;
                case "blog":
                    shareType = (byte)EnumShareType.ForBlog;
                    break;
                case "friend":
                    shareType = (byte)EnumShareType.ForFriend;
                    break;
                case "goods":
                    shareType = (byte)EnumShareType.ForGoods;
                    break;
                case "group":
                    shareType = (byte)EnumShareType.ForGroup;
                    break;
                case "multe":
                    shareType = (byte)EnumShareType.ForMulte;
                    break;
                case "other":
                    shareType = (byte)EnumShareType.ForOther;
                    break;
                case "photo":
                    shareType = (byte)EnumShareType.ForPhoto;
                    break;
                case "shop":
                    shareType = (byte)EnumShareType.ForShop;
                    break;
                case "topic":
                    shareType = (byte)EnumShareType.ForTopic;
                    break;
                case "vote":
                    shareType = (byte)EnumShareType.ForVote;
                    break;
            }
            int n = 0;
            if (uid == 0 || infoid == 0 || string.IsNullOrEmpty(flag) || string.IsNullOrEmpty(sharetitle))
            {
                n = 0;
            }
            else
            {
                JuSNS.Model.ShareInfo mdl = new ShareInfo();
                mdl.Comments = 0;
                mdl.Content = sharecontent;
                mdl.Infoid = infoid;
                mdl.Id = 0;
                mdl.IsLock = (byte)(EnumCusState.ForNormal);
                mdl.IsRec = false;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.ShareType = shareType;
                mdl.Title = sharetitle;
                mdl.UserID = uid;
                mdl.WebURL = string.Empty;
                n = JuSNS.Home.App.Share.Instance.InsertShare(mdl);
            }
            if (n > 0)
            {
                JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, uid, 0, (int)EnumDynType.CreatShare, string.Empty, DateTime.Now, n, string.Empty));
                //更新积分
                JuSNS.Home.User.User.Instance.UpdateInte(uid, Public.JSplit(12), 0, 0, "分享信息");
                n = 1;
            }
            OutText(n);
        }
        /// <summary>
        /// 检查活动会员
        /// </summary>
        protected void CheckAtiveMemberAjax()
        {
            int mid = GetInt("mid", 0);
            int userid = GetInt("userid", 0);
            int aid = GetInt("aid", 0);
            int flag = GetInt("flag", 0);
            int n = 0;
            if (mid == 0 || aid == 0)
            {
                n = 0;
            }
            else
            {
                //0失败，1成功，2人数达到上限
                n = JuSNS.Home.App.Ative.Instance.CheckAtiveMember(mid, aid, flag);
                if (n == 1)
                {
                    //插入动态
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.JoinActive, string.Empty, DateTime.Now, aid, string.Empty));
                    //更新积分
                    JuSNS.Home.User.User.Instance.UpdateInte(userid, Public.JSplit(18), 0, 0, "加入活动");
                }

            }
            OutText(n);
        }
        /// <summary>
        /// 退出活动
        /// </summary>
        protected void OutAtiveAjax()
        {
            int aid = GetInt("aid", 0);
            int uid = GetInt("uid", 0);
            int n = 0;
            if (uid == 0 || aid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Ative.Instance.OutAtive(aid, uid);
            }
            OutText(n);
        }
        /// <summary>
        /// 购买订单
        /// </summary>
        protected void PostOrderAjax()
        {
            int orderid = GetInt("orderid", 0);
            int uid = GetInt("uid", 0);
            int goodsid = GetInt("goodsid", 0);
            string ordernumber = GetString("ordernumber");
            int n = 0;
            if (uid == 0 || orderid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Shop.Instance.PostOrder(ordernumber, goodsid, orderid, uid);
            }
            OutText(n);
        }
        /// <summary>
        /// 接受订单
        /// </summary>
        protected void ReviceOrderAjax()
        {
            int orderid = GetInt("orderid", 0);
            int uid = GetInt("uid", 0);
            int n = 0;
            if (uid == 0 || orderid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Shop.Instance.ReviceOrder(orderid, uid);
            }
            OutText(n);
        }
        /// <summary>
        /// 投票
        /// </summary>
        protected void ToVoteAjax()
        {
            VoteInfo vinfo = new VoteInfo();
            int vid = GetInt("vid", 0);
            int uid = GetInt("uid", 0);
            int userid = GetInt("userid", 0);
            string option = GetString("option");
            string comm = GetString("comm");

            int n = 0;
            vinfo = JuSNS.Home.App.Vote.Instance.GetVoteInfo(vid);
            DateTime dnow = DateTime.Now;
            DateTime dvote = vinfo.EndTime;
            TimeSpan ts = dnow.Subtract(dvote);
            //n-1过期,-2过期
            if (ts.Days > 0)
            {
                n = -1;
            }
            //判断是否已经投票
            if (JuSNS.Home.App.Vote.Instance.IsVote(uid, vid))
            {
                n = -2;
            }

            VoteToInfo tovoteinfo = new VoteToInfo();

            tovoteinfo.ID = 0;
            tovoteinfo.OptionID = option;
            tovoteinfo.UserID = this.GetUserID();
            tovoteinfo.VoteID = vid;
            tovoteinfo.Content = comm;

            n = JuSNS.Home.App.Vote.Instance.Add(tovoteinfo);
            if (userid != uid)
            {
                JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), userid, "参与了你的投票", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.JoinVote, vid));
            }
            //插入动态
            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.JoinVote, string.Empty, DateTime.Now, vid, string.Empty));
            //更新积分
            JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(30), 0, 0, "参与投票(系统)");

            OutText(n);
        }
        /// <summary>
        /// 插入收藏夹
        /// </summary>
        protected void AddFavoriteAjax()
        {
            string classname = GetString("classname");
            int uid = GetInt("uid", 0);
            int n = 0;
            if (uid == 0)
            {
                n = 0;
            }
            else
            {
                FavoriteClassInfo mdl = new FavoriteClassInfo();
                mdl.ClassName = classname;
                mdl.IsPub = true;
                mdl.UserID = uid;
                n = JuSNS.Home.User.User.Instance.insertFavoriteClass(mdl);
            }
            OutText(n);
        }
        /// <summary>
        /// 更新邮件
        /// </summary>
        protected void UpdateMailBox()
        {
            int mid = GetInt("mid", 0);
            int state = GetInt("state", 0);
            int n = 0;
            if (mid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.User.User.Instance.UpdateMailState(mid, state);
            }
            OutText(n);
        }
        /// <summary>
        /// 检查群组会员
        /// </summary>
        protected void CheckGroupMemberAjax()
        {
            int userid = GetInt("userid", 0);
            int flag = GetInt("flag", 0);
            int uid = GetInt("uid", 0);
            int gid = GetInt("gid", 0);
            int n = 0;
            if (uid == 0 || userid == 0 || gid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.App.Group.Instance.CheckGroupMember(gid,userid, uid, flag);
            }
            OutText(n);
        }
        /// <summary>
        /// 检查好友
        /// </summary>
        protected void CheckFriendAjax()
        {
            int fid = GetInt("fid", 0);
            int flag = GetInt("flag", 0);
            int uid = GetInt("uid", 0);
            int n = 0;
            if (uid == 0 || fid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.User.User.Instance.CheckFriend(fid, uid, flag);
                if (n > 0)
                {
                    n = 1;
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, uid, fid, (int)EnumDynType.Friend, string.Empty, DateTime.Now, fid, string.Empty));
                    if (flag == 0)
                    {
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, uid, fid, "同意加为好友", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.Friend, fid));
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, uid, fid, "拒绝加为好友", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.Friend, fid));
                    }
                }
            }
            OutText(n);
        }
        /// <summary>
        /// 检查群组
        /// </summary>
        protected void CheckGroupAjax()
        {
            int userid = GetInt("userid", 0);
            int gid = GetInt("gid", 0);
            int flag = GetInt("flag", 0);
            int uid = GetInt("uid", 0);
            int n = 0;
            if (uid == 0 || userid == 0 || gid == 0)
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.User.User.Instance.CheckGroup(userid, uid, gid, flag);
                if (n > 0)
                    n = 1;
            }
            OutText(n);
        }
        /// <summary>
        /// 加入VIP会员
        /// </summary>
        protected void JoinVipAjax()
        {
            string today = GetString("today");
            string joincontents = GetString("joincontents");
            int uid = GetInt("uid", 0);
            int n = 0;
            if (uid == 0 || string.IsNullOrEmpty(today))
            {
                n = 0;
            }
            else
            {
                n = JuSNS.Home.User.User.Instance.JoinVip(uid, today, joincontents);
            }
            OutText(n);
        }
        /// <summary>
        /// ATT用户
        /// </summary>
        protected void ATTUserAjax()
        {
            int uid = GetInt("uid", 0);
            int aid = GetInt("aid", 0);
            int n = 0;
            if (uid == 0 || aid==0)
            {
                n = 0;
            }
            else
            {
                ATTInfo info = new ATTInfo(0, uid, aid);
                n = JuSNS.Home.User.User.Instance.InsertATT(info);
            }
            OutText(n);
        }
        /// <summary>
        /// 用户模板
        /// </summary>
        protected void UserTemplateAjax()
        {
            int tid = GetInt("tid", 0);
            int uid = GetInt("uid", 0);
            string tename = GetString("tename");
            int n = 1;
            if (tid == 0 || uid == 0)
            {
                n = 0;
            }
            else
            {
                if (uid != this.GetUserID()) n= -1;
                SpaceTemplateInfo info = JuSNS.Home.User.User.Instance.GetSpaceTemplate(tid);
                if (info == null) n= -1;
                if (info.IsLock != 0) n= -1;
                UserInfo uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                if (uinfo.Integral < info.IPoint || uinfo.Inteyb < info.GPoint)
                {
                    n = -2;
                }
                else
                {
                    if (info.IPoint > 0)
                    {
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), info.IPoint, 0, 1, "更换空间模板");
                    }
                    if (info.GPoint > 0)
                    {
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), info.GPoint, 1, 1, "更换空间模板");
                    }
                    if (n == 1)
                    {
                        string filepath = "~/space/info/my/user" + uid + ".config";
                        Public.CreatUserConfig(filepath);
                        Public.setXmlInnerText(filepath, "/configuration/space", tename);
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.UpdateTempalte, info.TEName, DateTime.Now, 0, string.Empty));
                        JuSNS.Home.User.User.Instance.UpdateSpaceTemplate(tid);
                    }
                }
            }
            OutText(n);
        }

        /// <summary>
        /// 屏蔽动态
        /// </summary>
        protected void CloseDynAjax()
        {
            int userid = GetInt("userid", 0);
            int uid = GetInt("uid", 0);
            string  errors = string.Empty;
            if (uid == 0 || userid == 0)
            {
                errors = "errors";
            }
            else
            {
                string filepath = "~/space/info/my/user" + uid + ".config";
                Public.CreatUserConfig(filepath);
                string valesed = Public.GetXMLValue("killuser", filepath);
                if (!string.IsNullOrEmpty(valesed))
                {
                    bool isvalue = false;
                    for (int i = 0; i < valesed.Split(',').Length; i++)
                    {
                        if (valesed.Split(',')[i] == userid.ToString())
                        {
                            isvalue = true;
                        }
                    }
                    if (!isvalue)
                    {
                        valesed += "," + userid;
                    }
                    else
                    {
                        errors = "closeed";
                    }
                }
                if (errors != "closeed")
                {
                    Public.setXmlInnerText(filepath, "/configuration/killuser", valesed);
                    errors = "succs";
                }
            }
            OutText(errors);
        }
        /// <summary>
        /// 留言
        /// </summary>
        protected void SendGBook()
        {
            int touid = GetInt("touid", 0);
            int uid = GetInt("uid", 0);
            string gbookcontent = GetString("gbookcontent");
            string errors = string.Empty;
            if (uid == 0 || touid == 0 || string.IsNullOrEmpty(gbookcontent))
            {
                errors = "错误的参数";
            }
            else
            {
                GBookInfo info = new GBookInfo();
                info.Content = gbookcontent;
                if (uid != this.GetUserID())
                {
                    errors = "错误的权限";
                }
                else
                {
                    byte ischeck = Convert.ToByte(Public.GetXMLBaseValue("gbookcheck"));
                    if (JuSNS.Home.User.User.Instance.IsAdmin(uid)) ischeck = 0;
                    info.IsLock = ischeck;
                    info.ParentID = 0;
                    info.PostTime = DateTime.Now;
                    info.SendID = uid;
                    info.UserID = touid;
                    int n = JuSNS.Home.User.User.Instance.InsertGbook(info);
                    if (n > 0)
                    {
                        errors = "1";
                    }
                }
            }
            OutText(errors);
        }
        /// <summary>
        /// 设置系统公告
        /// </summary>
        protected void SetSysShowAjax()
        {
            HttpCookie SNSMessageCookie = new HttpCookie("SNSShowSysMessage" + this.GetUserID());
            SNSMessageCookie["ShowSysMessage" + this.GetUserID()] = "1";
            SNSMessageCookie.Secure = false;
            System.Web.HttpContext.Current.Response.AppendCookie(SNSMessageCookie);
        }

        protected void EditTopicAjax()
        {
            string content = GetString("content");
            int uid = GetInt("uid", 0);
            int userid = GetInt("userid", 0);
            int tid = GetInt("tid", 0);
            string resultstr = "";
            if (uid == 0 || userid==0||string.IsNullOrEmpty(content))
            {
                resultstr = "0";
            }
            else
            {
                GroupTopicInfo info = JuSNS.Home.App.Group.Instance.GetTopicInfo(tid);
                if (JuSNS.Home.User.User.Instance.IsAdmin(uid) || info.UserID == uid)
                {
                    GroupTopicInfo mdl = new GroupTopicInfo();
                    mdl.Content = content + "<p style=\"color:#999999;\">" + JuSNS.Home.User.User.Instance.GetUserInfo(uid).TrueName + " 于 " + DateTime.Now + "编辑过</p>";
                    mdl.Id = tid;
                    mdl.Clicks = 0;
                    mdl.Groupid = 0;
                    mdl.IsBest = 0;
                    mdl.IsLock = 0;
                    mdl.IsTop = false;
                    mdl.LastpostTime = DateTime.Now;
                    mdl.PostIP = string.Empty;
                    mdl.Posttime = DateTime.Now;
                    mdl.Replynumber = 0;
                    mdl.Title = string.Empty;
                    mdl.TopicID = 0;
                    mdl.TrueName = string.Empty;
                    mdl.UserID = 0;
                    resultstr = JuSNS.Home.App.Group.Instance.UpdateTopicContent(mdl).ToString();
                }
                else
                {
                    resultstr = "0";
                }
            }
            OutText(resultstr);
        }
        /// <summary>
        /// 写入Twitter
        /// </summary>
        protected void SendTwitterAjax()
        {
            string cont = GetString("cont");
            int uid = GetInt("uid", 0);
            string error = "";
            if (uid == 0 || string.IsNullOrEmpty(cont))
            {
                error = "errors请填写内容。";
            }
            else
            {
                TwitterInfo mdl = new TwitterInfo();
                mdl.Comments = 0;
                mdl.Content = cont;
                mdl.ID = 0;
                int Twitter = Convert.ToInt32(Public.GetXMLBaseValue("Twitter"));
                if (Twitter == 1)
                {
                    mdl.IsLock = true;
                }
                else
                {
                    mdl.IsLock = false;
                }
                mdl.IsRec = 0;
                mdl.Media = string.Empty;
                mdl.MType = string.Empty;
                mdl.Pic = string.Empty;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.UserID = uid;
                int n = JuSNS.Home.App.TWrite.Instance.InserTwitter(mdl);
                if (n > 0)
                {
                    if (Twitter == 1)
                    {
                        error = "nopass发布成功！但是需要管理员审核后才能显示";
                    }
                    else
                    {
                        DynInfo dyninfo = new DynInfo();
                        dyninfo.Content = string.Empty;
                        dyninfo.CUserID = 0;
                        dyninfo.DynType = (int)EnumDynType.CreatTwitter;
                        dyninfo.Infoarr = n;
                        dyninfo.PostTime = DateTime.Now;
                        dyninfo.UserID = uid;
                        JuSNS.Home.User.User.Instance.InsertDyn(dyninfo); 
                        error = "succs" + Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(uid));
                    }
                }
                else
                {
                    error = "errors发布失败";
                }
            }
            OutText(error);
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        private void OutText(object s)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
    }

}
