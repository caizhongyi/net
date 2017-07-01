using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.Config;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.poke
{
    /// <summary>
    /// 打招呼
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
        /// 显示信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int uid = GetInt("uid", 0);
            if (uid > 0)
            {
                context.Put("suid", uid);
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                context.Put("susername", mdl.TrueName);
                if (uid == this.UserID)
                {
                    PageError("不能自己给自己打招呼", root + "/app/poke");
                    //context.Put("errors", "不能自己给自己打招呼");
                }
            }
            context.Put("cpagetitle", "给朋友打招呼");
            int DayPokeNumber = Convert.ToInt32(Public.GetXMLPokeValue("DayPokeNumber"));
            int UserNumber = Convert.ToInt32(Public.GetXMLPokeValue("UserNumber"));
            context.Put("maxpoke", DayPokeNumber);
            context.Put("maxsendfriend", UserNumber);
            pokelist(ref context);
        }

        /// <summary>
        /// 招呼列表
        /// </summary>
        /// <param name="context"></param>
        protected void pokelist(ref VelocityContext context)
        {
            string listSTR = string.Empty;
            foreach (KeyValuePair<int, PokeActionInfo> kv in PokeConfig.Config)
            {
                listSTR += "<li><label for=\"pokeact_" + kv.Key + "\" title=\"" + kv.Value.More + "\"><input type=\"radio\" value=\"" + kv.Key + "\" id=\"pokeact_" + kv.Key + "\" name=\"pokeaction\" /><img align=\"middle\" src=\"" + root + "/template/" + UiConfig.SkinStyle + "/images/poke/pokeact_" + kv.Key + ".gif\" /> " + kv.Value.Value + "</label></li>";
            }
            context.Put("pokelist", listSTR);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            int PokeKey = GetInt("pokeaction", 0);
            PokeInfo mdl = new PokeInfo();
            mdl.Id = 0;
            mdl.PokeForm = GetString("pokefrom");
            mdl.PokeKey = PokeKey;
            mdl.Poketo = GetString("poketo");
            mdl.PostTime = DateTime.Now;
            mdl.UserID = this.UserID;
            mdl.IsPub = Convert.ToByte(GetInt("ispubs", 1));
            string hidefrienduid = GetString("hidefrienduid");
            int m = 0;
            string[] hidefrienduidARR = hidefrienduid.Split(',');
            int sm = hidefrienduidARR.Length;
            if (PokeKey == 0)
            {
                context.Put("errors","请选择一个动作或自定义动作");
            }
            else
            {
                for (int i = 0; i < hidefrienduidARR.Length; i++)
                {
                    mdl.ReviceID = Convert.ToInt32(hidefrienduidARR[i]);
                    int n = JuSNS.Home.User.User.Instance.InsertPoke(mdl);
                    if (n == 1)
                    {
                        m++;
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), Convert.ToInt32(hidefrienduidARR[i]), "打了一个招呼", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.CreatPoke, 0));
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), Convert.ToInt32(hidefrienduidARR[i]), (int)EnumDynType.CreatPoke, string.Empty, DateTime.Now, PokeKey, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(14), 0, 0, "打招呼(系统)");
                    }
                }
                if (sm == m)
                {
                    context.Put("rights", "为" + m + "个好友打了招呼");
                }
                else
                {
                    context.Put("rights", "为" + m + "个好友打了招呼，失败" + (sm - m) + "个。");
                }
            }
            ShowInfo(ref context);
        }
    }
}
