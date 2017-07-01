using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.space.info
{
    public partial class head : UserPage
    {
        public string f = string.Empty;
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
            string r = GetString("r");
            if (!string.IsNullOrEmpty(r))
            {
                if (r == "nohead")
                {
                    context.Put("errors", "上传了头像才能剪切");
                }
                if (r == "succ")
                {
                    context.Put("rights", "更新头像成功,刷新本页面即可查看。");
                }
            }
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            ShowHeadList(ref context);
            f = GetQueryString("f");
            context.Put("flag", f);
            if (!string.IsNullOrEmpty(f) && f == "1") { context.Put("startlogin", true); context.Put("cpagetitle", "完善头像"); } else { context.Put("cpagetitle", "修改头像"); }
        }

        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            ShowInfo(ref context);
            UploadHead picup = new UploadHead();
            int rint  = picup.Start();
            if (rint !=0)
            {
                switch (rint)
                {
                    case -1:
                        context.Put("error", "上传失败");
                        break;
                    case 1:
                        context.Put("error", "图片太小");
                        break;
                    case 2:
                        context.Put("error", "图片太大。最多允许上传" + Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("picsize")) * 1000 + "KB的图片。");
                        break;
                    case 3:
                        context.Put("error", "图片格式不正确。格式只允许：" + JuSNS.Common.Public.GetXMLValue("pictype") + "");
                        break;
                }
            }
            else
            {
                PhotoInfo phi = new PhotoInfo();
                phi.AlbumID = 0;
                phi.Comments = 0;
                phi.Description = string.Empty;
                phi.FilePath = picup.OutFileName;
                SNSToKenCookie["headpic"] = picup.OutFileName; //写入cookie
                HttpContext.Current.Session["headpic"] = picup.OutFileName;
                phi.FileSize = picup.LastContentLength;
                phi.Height = picup.LastHeight;
                phi.Width = picup.LastWidth;
                phi.IsCover = false;
                //0为头像，1为普通照片
                phi.PhotoType = 0;
                phi.PostIP = Public.GetClientIP();
                phi.PostTime = DateTime.Now;
                //0正常，1锁定
                phi.State = 0;
                phi.UserID = this.UserID;
                phi.Views = 0;
                int photoid = JuSNS.Home.App.Photo.Instance.Add(phi);
                if (photoid > 0)
                {
                    JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(16), 0, 0, "更新头像");
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.UpdateHeadPic, this.GetHeadImage(this.UserID, 1), DateTime.Now, photoid, string.Empty));
                    context.Put("redirecturl", "head1" + ExName + "?f=" + GetQueryString("f"));
                }
            }
        }

        public void ShowHeadList(ref NVelocity.VelocityContext context)
        {
            DataTable dt = JuSNS.Home.App.Photo.Instance.GetPhotoList(0, this.UserID, 16);
            List<Hashtable> headlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable head = new Hashtable();
                head.Add("files", Public.GetSmallHeadPic(dr["FilePath"], 1));
                head.Add("id", dr["id"]);
                headlist.Add(head);
            }
            dt.Clear();dt.Dispose();
            context.Put("headlist", headlist);
        }
    }
}