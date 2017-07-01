using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PowerChatRoom_Chat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Session["Admin"] != null)//如果管理员登录的话
            {
                PowerTalkBox1.MyUserName = Session["Admin"].ToString();//管理员的Session登录才好用
                PowerTalkBox1.ChatContrlHtml = "";//为空时，则显示在线人员列表
                PowerTalkBox1.ToUserIdContent = "大家";
                PowerTalkBox1.InitLoad(); //初始化PowerTalkBox
            }
            else
            {
                if (Request.QueryString["OnLine"] != null)
                {
                    PowerTalkBox1.ToUserIdContent = Request.QueryString["OnLine"].ToString();
                }
                else
                {
                    PowerTalkBox1.ToUserIdContent = "在线客服";
                }
                PowerTalkBox1.InitLoad(); //初始化PowerTalkBox
            }
        }
    }
}
