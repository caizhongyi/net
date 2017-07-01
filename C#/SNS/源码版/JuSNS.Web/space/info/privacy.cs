using System;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.space.info
{
    public class privacy : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            HttpContext.Current.Response.Expires = 0;
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "隐私设置");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("username", "用户名：" + mdl.UserName);
            context.Put("email", "电子邮件：" + mdl.Email);
            context.Put("mobilesendcontent", "编写【" + Public.GetXMLValue("sendmobileNumber") + "】发送到 " + Public.GetXMLValue("sendMobileBind") + "，根据提示完成绑定。");
            if (!string.IsNullOrEmpty(mdl.Mobile))
            {
                context.Put("mobile", "手机：" + mdl.Mobile);
                string bindMobile = string.Empty;
                if (mdl.BindMoblie)
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',0);\" title=\"取消绑定\">已绑定</a>";
                }
                else
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',1);\" title=\"绑定手机\">未绑定</a>";
                }
                context.Put("bindmobile", "(" + bindMobile + ")");
            }
            else
            {
                context.Put("mobile", "手机：<a href=\"javascript:void(0);\" onclick=\"jQuery('#hideMobile').toggle();\" title=\"填写手机\">未填写</a>");
                context.Put("bindmobile", string.Empty);
            }
            ShowItem(ref context);
        }

        protected void ShowItem(ref VelocityContext context)
        {
            UserSettingInfo mdl = JuSNS.Home.User.User.Instance.GetUserSettingInfo(this.UserID);
            context.Put("PrivEducate", GetArr(mdl.PrivEducate));
            context.Put("PrivFavourite", GetArr(mdl.PrivFavourite));
            context.Put("PrivFriends", GetArr(mdl.PrivFriends));
            context.Put("PrivGroup", GetArr(mdl.PrivGroup));
            context.Put("PrivLasso", GetArr(mdl.PrivLasso));
            context.Put("PrivLeaveWord", GetArr(mdl.PrivLeaveWord));
            context.Put("PrivMiniBlog", GetArr(mdl.PrivMiniBlog));
            context.Put("PrivMovies", GetArr(mdl.PrivMovies));
            context.Put("PrivShare", GetArr(mdl.PrivShare));
            context.Put("PrivSpace", GetArr(mdl.PrivSpace));
            if (mdl.ActAddFriend) { context.Put("ActAddFriend", "checked"); } else { context.Put("ActAddFriend", string.Empty); }
            if (mdl.ActDeliver) { context.Put("ActDeliver", "checked"); } else { context.Put("ActDeliver", string.Empty); }
            if (mdl.ActLeaveWord) { context.Put("ActLeaveWord", "checked"); } else { context.Put("ActLeaveWord", string.Empty); }
            if (mdl.ActLogComment) { context.Put("ActLogComment", "checked"); } else { context.Put("ActLogComment", string.Empty); }
            if (mdl.ActMovieComment) { context.Put("ActMovieComment", "checked"); } else { context.Put("ActMovieComment", string.Empty); }
            if (mdl.ActPhotoLasso) { context.Put("ActPhotoLasso", "checked"); } else { context.Put("ActPhotoLasso", string.Empty); }
            if (mdl.ActPicComment) { context.Put("ActPicComment", "checked"); } else { context.Put("ActPicComment", string.Empty); }
            if (mdl.ActSecede) { context.Put("ActSecede", "checked"); } else { context.Put("ActSecede", string.Empty); }
            if (mdl.ActShareComment) { context.Put("ActShareComment", "checked"); } else { context.Put("ActShareComment", string.Empty); }
            if (mdl.ActUpdateData) { context.Put("ActUpdateData", "checked"); } else { context.Put("ActUpdateData", string.Empty); }
        }

        protected string GetArr(int Item)
        {
            return Public.GetPrivacy(Item);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            UserSettingInfo mdl = new UserSettingInfo();
            mdl.PrivEducate = GetInt("PrivEducate", 0);
            mdl.PrivFavourite = GetInt("PrivFavourite", 0);
            mdl.PrivFriends = GetInt("PrivFriends", 0);
            mdl.PrivGroup = GetInt("PrivGroup", 0);
            mdl.PrivLasso = GetInt("PrivLasso", 0);
            mdl.PrivLeaveWord = GetInt("PrivLeaveWord", 0);
            mdl.PrivMiniBlog = GetInt("PrivMiniBlog", 0);
            mdl.PrivMovies = GetInt("PrivMovies", 0);
            mdl.PrivShare = GetInt("PrivShare", 0);
            mdl.PrivSpace = GetInt("PrivSpace", 0);
            if (!string.IsNullOrEmpty(GetString("ActAddFriend"))) { mdl.ActAddFriend = true; } else { mdl.ActAddFriend = false; }
            if (!string.IsNullOrEmpty(GetString("ActDeliver"))) { mdl.ActDeliver = true; } else { mdl.ActAddFriend = false; }
            if (!string.IsNullOrEmpty(GetString("ActLeaveWord"))) { mdl.ActLeaveWord = true; } else { mdl.ActLeaveWord = false; }
            if (!string.IsNullOrEmpty(GetString("ActLogComment"))) { mdl.ActLogComment = true; } else { mdl.ActLogComment = false; }
            if (!string.IsNullOrEmpty(GetString("ActMovieComment"))) { mdl.ActMovieComment = true; } else { mdl.ActMovieComment = false; }
            if (!string.IsNullOrEmpty(GetString("ActPhotoLasso"))) { mdl.ActPhotoLasso = true; } else { mdl.ActPhotoLasso = false; }
            if (!string.IsNullOrEmpty(GetString("ActPicComment"))) { mdl.ActPicComment = true; } else { mdl.ActPicComment = false; }
            if (!string.IsNullOrEmpty(GetString("ActSecede"))) { mdl.ActSecede = true; } else { mdl.ActSecede = false; }
            if (!string.IsNullOrEmpty(GetString("ActShareComment"))) { mdl.ActShareComment = true; } else { mdl.ActShareComment = false; }
            if (!string.IsNullOrEmpty(GetString("ActUpdateData"))) { mdl.ActUpdateData = true; } else { mdl.ActUpdateData = false; }
            mdl.UserID = this.UserID;
            mdl.LastPostIP = Public.GetClientIP();
            mdl.LastPostTime = DateTime.Now;
            if (JuSNS.Home.User.User.Instance.SetPrivacy(this.UserID, mdl) > 0)
            {
                context.Put("rights", "设置隐私状态成功！");
            }
            else
            {
                context.Put("errors", "设置隐私状态失败！");
            }
            ShowInfo(ref context);
        }


    }
}
