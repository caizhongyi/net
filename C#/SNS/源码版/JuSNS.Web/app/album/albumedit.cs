using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.album
{
    /// <summary>
    /// 编辑相册
    /// </summary>
    public class albumedit : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int aid = GetInt("aid",0);
            string ptitle = string.Empty;
            AlbumInfo mdl = JuSNS.Home.App.Album.Instance.GetInfo(aid);
            context.Put("cpagetitle", "编辑" + mdl.Title + "相册");
            context.Put("albumName", Input.GetSubString(mdl.Title, 10));
            context.Put("aid", aid);
            context.Put("albumaName", mdl.Title);
            context.Put("albumdesc", mdl.Description);
            context.Put("privacy", Public.GetPrivacy(mdl.Privacy));
            if (mdl.UserID != this.UserID)  context.Put("errors", "错误的参数：UserParamError");
            int gid = GetInt("gid", 0);
            string groupname = GetString("groupname");
            if (gid > 0)
            {
                if (string.IsNullOrEmpty(groupname))
                {
                    context.Put("errors", "参数传递错误");
                }
            }
            context.Put("groupname", groupname);
            context.Put("gid", gid);
        }

        /// <summary>
        /// 提交信息
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            int aid = GetInt("aid", 0);
            string albumname = GetString("albumname");
            string albumdesc = GetString("albumdesc");
            int Privacy = GetInt("Privacy", 0);
            AlbumInfo mdl = new AlbumInfo();
            mdl.AlbumID = aid;
            mdl.UserID = this.UserID;
            mdl.Description = albumdesc;
            mdl.Privacy = Privacy;
            mdl.Title = albumname;
            if (JuSNS.Home.App.Album.Instance.Edit(mdl) > 0)
            {
                context.Put("rights", "修改相册成功");
            }
            else
            {
                context.Put("errors", "修改相册失败");
            }
            ShowInfo(ref context);
        }
    }
}
