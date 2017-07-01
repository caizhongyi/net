using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.box
{
    public class mail : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "发邮件");
            context.Put("sendfriendnumber", Public.GetXMLBaseValue("sendfriendnumber"));
            int fid = GetInt("fid", 0);
            if (fid > 0)
            {
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(fid);
                context.Put("susername", mdl.TrueName);
                context.Put("suid", fid);
                context.Put("title", GetString("title"));
                if (!string.IsNullOrEmpty(GetString("content")))
                {
                    context.Put("content", "留言内容：<br />" + GetString("content") + "<br />---------------");
                }
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            string errors = string.Empty;
            string hidefrienduid = GetString("hidefrienduid");
            string title = GetString("title");
            string txtcontent = GetString("txtcontent");
            int n = 0;
            if (string.IsNullOrEmpty(hidefrienduid) || string.IsNullOrEmpty(title))
            {
                errors += "发送给谁及邮件标题必须填写";
            }
            else
            {
                string[] uidARR=hidefrienduid.Split(',');
                for (int i = 0; i < uidARR.Length; i++)
                {
                    int m = JuSNS.Home.User.User.Instance.SendMail(this.UserID, Convert.ToInt32(uidARR[i]), title, txtcontent);
                    if (m > 0)
                    {
                        n++;
                    }
                }
            }
            if (string.IsNullOrEmpty(errors))
            {
                context.Put("rights", "共发送了" + n + "封邮件。邮件已经在您的发件箱中保存。");
            }
            else
            {
                context.Put("errors", errors);
            }
            ShowInfo(ref context);
        }
    }
}
