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
using System.Data.SqlClient;

public partial class Ni_QQ_QQscript : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (PowerTalkBox.PowerTalk.ExistUser("在线客服"))
        {
            this.ZiXun.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='OnlineQQ/head.gif'   onmousemove='this.border=1' onmouseout='this.border=0' onmousedown='this.border=2' border='0' onclick=\"window.open('IM/Chat.aspx?OnLine=" + "在线客服" + "','','width=650,height=489');\" ><br>咨询师: " + "在线客服" + "<br>";
        }
        else
        {
            this.ZiXun.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='OnlineQQ/uhead.gif'   onmousemove='this.border=1' onmouseout='this.border=0' onmousedown='this.border=2' border='0' ><br>咨询师: " + "在线客服" + "<br>";
        }
        if (PowerTalkBox.PowerTalk.ExistUser("技术支持"))
        {
            this.ZiXun.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='OnlineQQ/head.gif'   onmousemove='this.border=1' onmouseout='this.border=0' onmousedown='this.border=2' border='0' onclick=\"window.open('IM/Chat.aspx?OnLine=" + "技术支持" + "','','width=650,height=489');\" ><br>咨询师: " + "技术支持" + "<br>";
        }
        else
        {
            this.ZiXun.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='OnlineQQ/uhead.gif'   onmousemove='this.border=1' onmouseout='this.border=0' onmousedown='this.border=2' border='0'  ><br>咨询师: " + "技术支持" + "<br>";
        }
    }
    }

