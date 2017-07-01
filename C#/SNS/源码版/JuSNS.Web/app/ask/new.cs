using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ask
{
    public class @new : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAskValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ask");
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            base.Page_Load(ref context);
            context.Put("cpagetitle", "提问题");
            context.Put("mypoint", mdl.Integral);
            context.Put("jjpoint", Public.GetXMLAskValue("isJinji"));
            context.Put("classlist", ShowClassList("", 0));
        }

        protected string ShowClassList(string tmpSTR, int parentid)
        {
            string list = string.Empty;
            List<AskClassInfo> infolist = JuSNS.Home.App.Ask.Instance.GetAskClass(parentid);
            foreach (AskClassInfo info in infolist)
            {
                list += "<option value=\"" + info.Id + "\">" + tmpSTR + info.ClassName + "</option>\r\n";
                list += ShowClassList(" -- ", info.Id);
            }
            return list;
        }


        public override void Page_PostBack(ref VelocityContext context)
        {
            if (string.IsNullOrEmpty(GetString("txttitle")) || string.IsNullOrEmpty(GetString("txtcontent")))
            {
                context.Put("errors", "标题和问题内容必须填写");
            }
            else
            {
                AskInfo mdl = new AskInfo();
                mdl.ClassID = GetInt("classid", 0);
                mdl.Click = 0;
                mdl.Content = GetString("txtcontent");
                mdl.IsBest = 0;
                mdl.IsClose = 0;
                mdl.IsJinji = Convert.ToByte(GetInt("isJinji", 0));
                byte ischeck = Convert.ToByte(Public.GetXMLAskValue("ischck"));
                if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
                {
                    ischeck = 0;
                }
                mdl.IsLock = ischeck;
                mdl.JiFen = GetInt("txtjifen", 0);
                mdl.ParentID = 0;
                string Pic = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    string Path = Public.GetXMLAskValue("askpicpath");
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Path);
                }
                mdl.Pic = Pic;
                mdl.PostTime = DateTime.Now;
                mdl.Tag = GetString("txtkeywords");
                mdl.Title = GetString("txttitle");
                mdl.UserID = this.UserID;
                int aid = 0;
                int n = JuSNS.Home.App.Ask.Instance.InsertAsk(mdl,out aid);
                //0失败，1积分不足，2成功
                if (n == 2)
                {
                    if (ischeck == 1)
                    {
                        context.Put("rights", "发布成功，但是需要审核才能显示。");
                        //PageRight("发布成功，但是需要审核才能显示。", HttpContext.Current.Request.Url.ToString(), true);
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatAsk, string.Empty, DateTime.Now, aid, string.Empty));
                        context.Put("redirecturl", Public.URLWrite(aid, "ask"));
                        //PageRight("发布成功。", Public.URLWrite(aid, "ask"), true);
                    }
                }
                else if (n == 1)
                {
                    context.Put("errors", "积分不足");
                }
                else
                {
                    context.Put("errors", "更新失败");
                }
                ShowInfo(ref context);
            }
        }
    }
}
