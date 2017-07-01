using System;
using System.Text;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;
namespace JuSNS.Web.app.favorite
{
    public class @new: UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "添加新收藏");
            context.Put("classlist",ShowClassList(ref context));
        }

        protected string ShowClassList(ref VelocityContext context)
        {
            string list = string.Empty;
            List<FavoriteClassInfo> infolist = JuSNS.Home.User.User.Instance.GetFavorList(this.UserID);
            foreach (FavoriteClassInfo info in infolist)
            {
                    list += "<option value=\"" + info.Id + "\">" + info.ClassName + "</option>";
            }
            return list;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            FavoriteInfo mdl = new FavoriteInfo();
            string title = GetString("title");
            string url = GetString("url");
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(url))
            {
                context.Put("errors", "带*的必须填写");
            }
            else
            {
                mdl.ClassID = GetInt("classid", 0);
                mdl.Content = GetString("content");
                mdl.Id = 0;
                if (GetInt("ispub", 0) > 0)
                {
                    mdl.IsPub = true;
                }
                else
                {
                    mdl.IsPub = false;
                }
                mdl.PostTime = DateTime.Now;
                mdl.Title = title;
                mdl.URL = url;
                mdl.UserID = this.UserID;
                int n = JuSNS.Home.User.User.Instance.InsertFavorite(mdl);
                if (n > 0)
                {
                    if (GetInt("ispub", 0) > 0)
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatFaviote, string.Empty, DateTime.Now, this.UserID, string.Empty));
                    }
                    //更新积分
                    JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(39), 0, 0, "添加收藏");
                    context.Put("rights", "添加收藏成功");
                }
                else
                {
                    context.Put("errors", "添加收藏失败");
                }
            }
            ShowInfo(ref context);
        }

    }
}
