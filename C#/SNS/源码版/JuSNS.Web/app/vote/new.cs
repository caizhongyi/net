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

namespace JuSNS.Web.app.vote
{
    public class @new : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "发起投票");
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string errors = check();
            if (!string.IsNullOrEmpty(errors))
            {
                context.Put("errors", errors);
            }
            else
            {
                VoteInfo Info = new VoteInfo();
                VoteOptionInfo vo = new VoteOptionInfo();
                Info.Id = 0;
                Info.Title = GetString("Title");
                Info.Content = GetString("content");
                Info.UserID = this.UserID;
                Info.PostTime = DateTime.Now;
                Info.Mode = Convert.ToByte(GetInt("Mode", 0));
                if (GetInt("isEnd", 0) > 0)
                    Info.EndTime = Convert.ToDateTime(GetString("EndTime"));
                else
                    Info.EndTime = DateTime.Now.AddYears(2);
                Info.JCnt = 0;
                Info.IsFriend = Convert.ToByte(GetInt("IsFriend", 0));
                int VoteID = JuSNS.Home.App.Vote.Instance.AddVote(Info);
                string[] option = new string[21];
                for (int i = 1; i < option.Length; i++)
                {
                    if (!string.IsNullOrEmpty(GetString("Option_" + i)))
                    {
                        VoteOptionInfo oInfo = new VoteOptionInfo();
                        oInfo.ID = 0;
                        oInfo.OptionName = GetString("Option_" + i);
                        oInfo.VoteID = VoteID;
                        oInfo.Cnt = 0;
                        JuSNS.Home.App.Vote.Instance.AddOption(oInfo);
                    }
                }

                JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatVote, string.Empty, DateTime.Now, VoteID, string.Empty));
                //更新积分
                JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(29), 0, 0, "发起投票");

                context.Put("redirecturl", "view" + ExName + "?vid=" + VoteID);
            }
            ShowInfo(ref context);
        }

        /// <summary>
        /// 检测参数是否合法
        /// </summary>
        protected string check()
        {
            string errors = string.Empty;
            string[] option = new string[21];
            int OpNum = GetInt("OpNum", 0);
            int j = 0;
            for (int i = 1; i <= OpNum; i++)
            {
                string s = GetString("Option_" + i);
                if (!(s == null || s.Trim() == ""))
                {
                    j++;
                }
                option[i] = s;
            }
            if (j == 0)
            {
                errors += "至少填写一个项目<br />";
            }
            if (GetInt("isEnd",0)>0)
            {
                string endtime = GetString("EndTime");
                if (string.IsNullOrEmpty(endtime))
                {
                    errors += "填写结束日期<br />";
                }
                else
                {
                    if (!Input.IsDate(endtime))
                    {
                        errors += "结束日记不是日期格式<br />";
                    }
                }
                DateTime d = DateTime.Now;
                TimeSpan ts = DateTime.Now - Convert.ToDateTime(endtime);
                if (ts.Days > 0)
                {
                    errors += "结束日期不能少于今天<br />";
                }
            }
            return errors;
        }
    }
}
