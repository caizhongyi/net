using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.calend
{
    /// <summary>
    /// 日历
    /// </summary>
    public class @default : UserPage
    {
        /// <summary>
        /// 加载
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
            int cid = GetInt("cid", 0);
            int confignotenumber = Convert.ToInt32(Public.GetXMLBaseValue("noteNumber"));
            context.Put("confignotenumber", confignotenumber);
            if (cid > 0)
            {
                CalendInfo mdl = JuSNS.Home.User.User.Instance.GetCalendInfo(cid);
                if (mdl.UserID != this.UserID)
                {
                    context.Put("errors", "不是你的日历，不能修改！");
                }
                else
                {
                    context.Put("Content", mdl.Content);
                    context.Put("EndTime", mdl.EndTime);
                    context.Put("NoteNumber", mdl.NoteNumber);
                    context.Put("StartTime", mdl.StartTime);
                    context.Put("Title", mdl.Title);
                }
                context.Put("cpagetitle", "修改日历");
            }
            else
            {
                context.Put("cpagetitle", "创建日历");
            }
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            string errors = string.Empty;
            CalendInfo mdl = new CalendInfo();
            mdl.Content = GetString("txtcontent");
            mdl.Id = GetInt("cid", 0);
            mdl.NoteNumber = GetInt("txtnotenumber",0);
            mdl.PostTime = DateTime.Now;
            mdl.StartTime = GetDateTime("txtstarttime", DateTime.Now);
            mdl.EndTime = GetDateTime("txtendtime", DateTime.Now);
            mdl.Title = GetString("txttitle");
            mdl.UserID = this.UserID;
            if (string.IsNullOrEmpty(GetString("txttitle")))
            {
                errors += "填写标题<br />";
            }
            if (string.IsNullOrEmpty(GetString("txtstarttime")) || string.IsNullOrEmpty(GetString("txtendtime")))
            {
                errors += "开始时间和结束时间必须填写";
            }

            if (string.IsNullOrEmpty(errors))
            {
                int n = JuSNS.Home.User.User.Instance.InsertCalend(mdl);
                if (n > 0)
                {
                    //JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatActive, string.Empty, DateTime.Now, n));
                    //更新积分
                    JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(38), 0, 0, "创建日历");
                    context.Put("rights", "操作成功！");
                }
                else
                {
                    context.Put("errors", "操作失败！");
                }
            }
            else
            {
                context.Put("errors", errors);
            }
            ShowInfo(ref context);
        }
    }
}
