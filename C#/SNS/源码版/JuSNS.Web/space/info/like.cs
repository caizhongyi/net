using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.space.info
{
    public class like : UserPage
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
            context.Put("cpagetitle", "个人爱好");
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

            UserBaseInfo basi = JuSNS.Home.User.User.Instance.GetUserBaseInfo(this.UserID);
            context.Put("FavAdages", basi.FavAdages);
            context.Put("FavBooks", basi.FavBooks);
            context.Put("FavCartoons", basi.FavCartoons);
            context.Put("FavGames", basi.FavGames);
            context.Put("FavMovies", basi.FavMovies);
            context.Put("FavMusics", basi.FavMusics);
            context.Put("Favourite", basi.Favourite);
            context.Put("FavSports", basi.FavSports);
            context.Put("AppreciateMen", basi.AppreciateMen);
            context.Put("Presentation", basi.Presentation);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            UserBaseInfo uf = new UserBaseInfo();
            uf.UserID = this.UserID;
            uf.Favourite = GetFormString("Favourite");
            uf.FavMovies = GetFormString("FavMovies");
            uf.FavMusics = GetFormString("FavMusics");
            uf.FavCartoons = GetFormString("FavCartoons");
            uf.FavGames = GetFormString("FavGames");
            uf.FavSports = GetFormString("FavSports");
            uf.FavBooks = GetFormString("FavBooks");
            uf.FavAdages = GetFormString("FavAdages");
            uf.AppreciateMen = GetFormString("AppreciateMen");
            uf.Presentation = GetFormString("Presentation");
            int m = JuSNS.Home.User.User.Instance.UpdateLike(uf);
            ShowInfo(ref context);
            if (m > 0)
            {
                context.Put("rights", "保存个人爱好成功！");
            }
            else
            {
                context.Put("errors", "保存个人爱好失败！");
            }
        }

    }
}
