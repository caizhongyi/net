using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ative
{
    /// <summary>
    /// 添加活动
    /// </summary>
    public class @new : UserPage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAtiveValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ative");
            }
            else
            {
                int limited = Convert.ToInt32(Public.GetXMLAtiveValue("limited"));
                int ishead = Convert.ToInt32(Public.GetXMLAtiveValue("ishead"));
                if (limited > 0)
                {
                    if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).Integral < limited)
                    {
                        PageError("积分达到" + limited + "才能发起活动", root + "/app/ative");
                    }
                }
                if (ishead > 0)
                {
                    if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).Portrait <1)
                    {
                        PageError("上传了头像才能发起活动", root + "/app/ative");
                    }
                }
                ShowInfo(ref context);
            }
        }

        /// <summary>
        /// 显示分类
        /// </summary>
        /// <param name="tmp"></param>
        /// <param name="parentid"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        protected string  ShowClassList(string tmp,int parentid,int sid)
        {
            string listSTR = string.Empty;
            List<AtiveClassInfo> infolist = JuSNS.Home.App.Ative.Instance.GetAtiveClassList(parentid);
            foreach (AtiveClassInfo info in infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=\"" + info.Id + "\" selected>" + tmp + info.ClassName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.Id + "\">" + tmp + info.ClassName + "</option>";
                }
                listSTR += ShowClassList(" -- ", info.Id, sid);
            }
            return listSTR;
        }
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int aid = GetInt("aid", 0);
            context.Put("aid", aid);
            int areaid = 0;
            int areaid1 = 0;
            int sid = 0;
            if (aid == 0)
            {
                context.Put("cpagetitle", "发起活动");
                context.Put("photo", string.Empty);
            }
            else
            {
                AtiveInfo mdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
                if (this.UserID != mdl.UserID)
                {
                    context.Put("errors", "不是您的活动，您不能修改");
                    context.Put("cpagetitle", "修改活动");
                }
                else
                {
                    context.Put("cpagetitle", "修改活动");
                    areaid = mdl.AreaID;
                    areaid1 = mdl.AreaID1;
                    sid = mdl.ClassID;
                    context.Put("address", mdl.AddRess);
                    context.Put("title", mdl.AtiveName);
                    context.Put("baomingtime", mdl.BaoMingTime.ToString("yyyy-MM-dd"));
                    context.Put("content", mdl.Content);
                    context.Put("endtime", mdl.EndTime.ToString("yyyy-MM-dd"));
                    if (mdl.IsChecks==1) { context.Put("ischeck", "checked"); } else { context.Put("ischeck", string.Empty); }
                    context.Put("links", mdl.Links);
                    context.Put("money", mdl.Money);
                    context.Put("note", mdl.Note);
                    context.Put("persionnumber", mdl.PersionNumber);
                    context.Put("photo", mdl.Photo);
                    context.Put("starttime", mdl.StartTime.ToString("yyyy-MM-dd"));
                }
            }
            context.Put("classlist", ShowClassList(string.Empty, 0, sid));
            context.Put("sitem", areaid1);
            context.Put("areaid", areaid);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            AtiveInfo mdl = new AtiveInfo();
            string error = string.Empty;
            string title = GetString("txttitle");
            string txtstarttime = GetString("txtstarttime");
            string endtime = GetString("txtendtime");
            string txtBaoMingTime = GetString("txtBaoMingTime");
            string txtcontent = GetString("txtcontent");
            string txtAddRess = GetString("txtAddRess");
            string txtlinks = GetString("txtlinks");
            if (string.IsNullOrEmpty(title) 
                || string.IsNullOrEmpty(txtstarttime)
                || string.IsNullOrEmpty(endtime)
                || string.IsNullOrEmpty(txtBaoMingTime)
                || string.IsNullOrEmpty(txtcontent)
                || string.IsNullOrEmpty(txtAddRess)
                || string.IsNullOrEmpty(txtlinks))
            {
                error += "带*的必须填写";
            }
            if (!string.IsNullOrEmpty(error))
            {
                context.Put("errors", error);
            }
            else
            {
                mdl.AddRess = txtAddRess;
                mdl.AreaID = GetInt("sltareaid", 0);
                mdl.AreaID1 = GetInt("sltareaid1", 0);
                mdl.AtiveName = title;
                mdl.ATT = 0;
                DateTime bmingtime = GetDateTime("txtBaoMingTime", DateTime.Now.AddDays(10));
                DateTime sendtime = GetDateTime("txtendtime", DateTime.Now.AddDays(20));
                DateTime stime = GetDateTime("txtstarttime", DateTime.Now.AddDays(5));
                string tmperr = string.Empty;
                if ((sendtime - bmingtime).Days < 0)
                {
                    tmperr = "活动结束时间不能小于报名时间！";
                }
                if ((sendtime - stime).Days < 0)
                {
                    tmperr += "<br />活动结束时间不能小于开始时间！";
                }

                if (!string.IsNullOrEmpty(tmperr))
                {
                    context.Put("errors", tmperr);
                }
                else
                {
                    mdl.BaoMingTime = bmingtime;
                    mdl.ClassID = GetInt("sltclassid", 0);
                    mdl.Clicks = 0;
                    mdl.Content = txtcontent;
                    mdl.EndTime = sendtime;
                    mdl.GroupID = 0;
                    mdl.Id = GetInt("aid", 0);
                    mdl.IsChecks = Convert.ToByte(GetInt("radisCheck", 0));
                    byte ischeck = Convert.ToByte(Public.GetXMLAtiveValue("ischeck"));
                    if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
                    {
                        ischeck = 0;
                    }
                    string passSTR = string.Empty;
                    if (ischeck == 1)
                    {
                        passSTR = "但需要审核";
                    }
                    mdl.IsLock = ischeck;
                    mdl.IsRec = 0;
                    mdl.Links = txtlinks;
                    mdl.Members = 0;
                    mdl.Money = GetString("txtMoney");
                    mdl.Note = GetString("txtNote");
                    mdl.PersionNumber = GetInt("txtPersionNumber", 0);
                    string Pic = string.Empty;
                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        string Path = Public.GetXMLAtiveValue("picpath");
                        HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                        Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Path);
                        if (string.IsNullOrEmpty(Pic))
                        {
                            Pic = GetString("hidephoto");
                        }
                    }
                    else
                    {
                        Pic = GetString("hidephoto");
                    }
                    mdl.Photo = Pic;
                    mdl.PostIP = Public.GetClientIP();
                    mdl.PostTime = DateTime.Now;
                    mdl.StartTime = stime;
                    mdl.UserID = this.UserID;
                    int n = JuSNS.Home.App.Ative.Instance.InsertUpdate(mdl);
                    if (n > 0)
                    {
                        if (ischeck == 1)
                        {
                            if (GetInt("aid", 0) == 0)
                            {
                                //PageRight("添加活动成功，但是需要审核才能显示。", HttpContext.Current.Request.Url.ToString(), true);
                                context.Put("rights", "添加活动成功！" + passSTR);
                            }
                            else
                            {
                                //PageRight("修改活动成功，但是需要审核才能显示", HttpContext.Current.Request.Url.ToString(), true);
                                context.Put("rights", "修改活动成功！" + passSTR);
                            }
                        }
                        else
                        {
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatActive, string.Empty, DateTime.Now, n, string.Empty));
                            //更新积分
                            JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(36), 0, 0, "发布活动");
                            PageRight("操作成功。", Public.URLWrite(n, "ative"), true);
                            //context.Put("redirecturl", Public.URLWrite(n, "ative"));
                        }
                    }
                    else
                    {
                        context.Put("errors", "保存活动失败");
                    }
                }
            }
            ShowInfo(ref context);
        }
    }
}
