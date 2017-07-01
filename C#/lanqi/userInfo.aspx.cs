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
using Model;
public partial class userInfo : System.Web.UI.Page
{
    public string username;
    public string xueli;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.SessionTest();
        userCenter u = Session["userinfo"] as userCenter;
        username = u.Username;
        xueli = u.Xueli;
        if (!IsPostBack)
        {
            
      
            userCenter uc = new userCenter();
            uc.Id = u.Id;
            fun.getModel(uc);
            txtAddress.Value = uc.Inaddress;
            txtMessage.Value = uc.Jianjie;
            txtMobil.Value = uc.Tel;
            txtName.Value = uc.Username;
            txtPhone.Value = uc.Ring;
            txtZip.Value = uc.PostNo;
            if (uc.Sex == "男")
            {
                sexMan.Checked = true;
            }
            else
            {
                sexWomen.Checked = true;
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
            userCenter u = Session["userinfo"] as userCenter;
            userCenter uc = new userCenter();
            uc.Id = u.Id;
            fun.getModel(uc);
            uc.Inaddress=txtAddress.Value;
            uc.Jianjie=txtMessage.Value;
            uc.Tel=txtMobil.Value;
            uc.Username=txtName.Value;
            uc.Ring=txtPhone.Value;
            uc.PostNo=txtZip.Value;
            if (sexMan.Checked == true)
            {
                
                uc.Sex = "男";
            }
            else
            {
                 uc.Sex = "女";
            }
            fun.DoSql(this,fun.update(uc),Request.Url.ToString());

        
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["userinfo"] = null;
        fun.AJAXalert("alert('已经成功退出');location='index.aspx'");
    }
}
