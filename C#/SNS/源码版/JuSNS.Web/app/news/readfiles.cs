using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.news
{
    public class readfiles : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int nid = GetInt("nid", 0);
            if (nid > 0)
            {
                NewsInfo mdl = JuSNS.Home.App.News.Instance.GetNewsInfo(nid);
                if (mdl.Point > 0 || mdl.GPoint > 0)
                {
                    if (!JuSNS.Home.User.User.Instance.isFilesDownload(this.UserID, nid, 0))
                    {
                        UserInfo uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
                        if (uinfo.Integral < mdl.Point)
                        {
                            PageError("积分不足！需要" + mdl.Point + "个积分，您现在积分：" + uinfo.Integral + "", "javascript:history.back();");
                        }
                        else
                        {
                            JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, mdl.Point, 0, 1, "下载资源扣除积分");
                        }
                        if (uinfo.Inteyb < mdl.GPoint)
                        {
                            PageError(UiConfig.gName + "不足！需要" + mdl.GPoint + "个" + UiConfig.gName + "，您现在" + UiConfig.gName + "：" + uinfo.Inteyb + "", "javascript:history.back();");
                        }
                        else
                        {
                            JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, mdl.GPoint, 1, 1, "下载资源扣除金币");
                        }
                        JuSNS.Home.User.User.Instance.InsertFilesRecord(this.UserID, nid, 0);
                    }
                }
                string FilesURL = Public.GetXMLBaseValue("ContentPath") + "/" + mdl.Files;
                context.Put("redirecturl", FilesURL);
            }
        }

    }
}
