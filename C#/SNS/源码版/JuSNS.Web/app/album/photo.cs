using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.album
{
    public class photo : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "最新图片");
             ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLAlbumValue("NewPhotoNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_photo_new_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.GetUserID()).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("pic", this.GetSmallPic(dr["FilePath"].ToString(), 1));
                info.Add("desc", Input.GetSubString(dr["Description"].ToString(), 14));
                info.Add("descmore", dr["Description"]);
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == this.GetUserID() || isadmin >0)
                {
                    isOp = true;
                    info.Add("edit", string.Empty);
                }
                if (isOp)
                {
                    info.Add("showop", "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + this.GetUserID() + ",'photo')\" class=\"showok1\"></a>");
                }
                else
                {
                    info.Add("showop", string.Empty);
                }
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}