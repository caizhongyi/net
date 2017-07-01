using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Model;
using JuSNS.Common;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.center
{
    /// <summary>
    /// 创建应用程序
    /// </summary>
    public class @new : UserPage
    {
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        /// <summary>
        /// 公共显示
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "创建新的应用程序");
            int appid = GetInt("appid", 0);
            int sid = 0;
            bool isdev = JuSNS.App.App.Instance.IsDeveloper(this.UserID);
            if (isdev)
            {
                AppDeveloperInfo mdl = JuSNS.App.App.Instance.GetDevInfo(this.UserID);
                context.Put("isdev", true);
                if (appid > 0)
                {
                    AppInfo info = JuSNS.App.App.Instance.GetAppInfo(appid);
                    if (info == null)
                    {
                        context.Put("errors", "错误的参数");
                    }
                    else
                    {
                        sid = info.Classid;
                        context.Put("appname", info.Appname);
                        context.Put("appkey", info.Appkey);
                        context.Put("ico", info.Icon);
                        context.Put("pic", info.Pic);
                        context.Put("IsLock", info.IsLock);
                        context.Put("content", info.Content);
                        context.Put("url", info.Url);
                        context.Put("height", info.Height);
                        context.Put("width", info.Width);
                        context.Put("SetupContent", info.SetupContent);
                    }
                }
                else
                {
                    string appkey = Input.MD5(Guid.NewGuid().ToString(), false);
                    context.Put("appkey", Input.MD5(Guid.NewGuid().ToString(), false));
                }
                context.Put("classlist", GetClassList(sid));
            }
            else
            {
                context.Put("errors", "您不是开发者");
            }
        }
        /// <summary>
        /// 得到分类
        /// </summary>
        /// <returns></returns>
        protected string GetClassList(int sid)
        {
            string listSTR = string.Empty;
            foreach (KeyValuePair<int, string> kv in JuSNS.Config.UserApiConfig.AppType)
            {
                if (sid == kv.Key)
                {
                    listSTR += "<option value=\"" + kv.Key + "\" selected>" + kv.Value + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + kv.Key + "\">" + kv.Value + "</option>";
                }
            }
            return listSTR;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            string appname = GetString("appname");
            string content = GetString("content");
            string url = GetString("url");
            string height = GetString("height");
            string width = GetString("width");
            if (string.IsNullOrEmpty(appname) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(url) || string.IsNullOrEmpty(height) || string.IsNullOrEmpty(width))
            {
                context.Put("errors", "名称、介绍、链接地址、页面高度及页面宽度必须填写。");
            }
            else
            {
                AppInfo info = new AppInfo();
                info.Appkey = GetString("appkey");
                info.Appname = GetString("appname");
                info.Classid = GetInt("classid", 0);
                info.Click = 0;
                info.Content = content;
                info.CreatTime = DateTime.Now;
                info.Height = Convert.ToInt32(height);
                string pic = GetString("hideico");
                string pic1 = GetString("hidepic");
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Public.GetXMLPageValue("apppath"));
                }
                info.Icon = pic;
                if (HttpContext.Current.Request.Files.Count > 1)
                {
                    HttpPostedFile hpf1 = HttpContext.Current.Request.Files[1];
                    pic1 = Public.GetFile(hpf1, Public.GetXMLValue("pictype"), Public.GetXMLPageValue("apppath"));
                }
                info.Pic = pic1;

                info.Id = GetInt("appid", 0);
                info.IsLock = Convert.ToByte(GetInt("IsLock", 0));
                info.SetupContent = GetString("SetupContent");
                info.SetupNumber = 0;
                info.TargetStyle = 0;
                info.Url = url;
                info.UserID = this.UserID;
                info.Width = Convert.ToInt32(width);
                int n = JuSNS.App.App.Instance.InsertApp(info);
                if (n > 0)
                {
                    context.Put("rights", "提交应用程序，等待管理员审核");
                }
                else
                {
                    context.Put("errors", "发生错误");
                }

            }
            ShowInfo(ref context);
        }
    }
}