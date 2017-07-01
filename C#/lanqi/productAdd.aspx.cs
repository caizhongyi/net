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
public partial class productAdd : System.Web.UI.Page
{

    int id = 0;
    public string username;
    public string xueli;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.SessionTest();
       
        userCenter uc = Session["userinfo"] as userCenter;
        if (uc.Xueli != "高级会员")
        {
            fun.AJAXalert("alert('你还不是高级会员，没有开通该功能');location='productmanager.aspx'");
        }
        username = uc.Username;
        xueli = uc.Xueli;
        id = fun.getQueryInt("id");
        if (!IsPostBack)
        {
            fun.bind(slDy, "select * from user_producttype2", "type", "id");
            fun.bind(slTYpe,"select * from user_producttype3","type","id");
            user_Product u = new user_Product();
            u.Id = id;
            if (fun.getModel(u))
            {
                txtName.Value = u.Name;
                textarea.Value = u.Explain;
                txtAddress.Value = u.Maker_address;
                txtPrice.Value = u.Newprice.ToString();
                slTYpe.Value = u.Typeid.ToString();
                slDy.Value = u.Typeid2.ToString();
                

            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (id == 0)
        {
            pAdd();
        }
        else
        {
            pUpdate();
        }
    }
    private void pAdd()
    {
        if (!fun.CheckStr(txtName.Value))
        {
         
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('名称不能为空')", true);
        }
        else if (!fun.CheckStr(txtPrice.Value) || !fun.IsMatch(txtPrice.Value))
        {
          
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('请输入正确的价格')", true);
        }
        else
        {

            user_Product up = new user_Product();
            up.Name = txtName.Value;
            up.Explain = textarea.Value;
            up.Maker_address = txtAddress.Value;
            up.Newprice = double.Parse(txtPrice.Value);
            up.Typeid = int.Parse(slTYpe.Value);
            up.Sjid = 2;
            up.Userid = ((userCenter)Session["userinfo"]).Id;
            up.Typeid2 = int.Parse(slDy.Value);
            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile("", file1, out pic, out error);
            }
            up.Picture = pic;
            fun.DoSql(this, fun.insert(up), Request.Url.ToString());
        }
    }

    private void pUpdate()
    {
        if (!fun.CheckStr(txtName.Value))
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('名称不能为空')", true);
        }
        else if (!fun.CheckStr(txtPrice.Value) || !fun.IsMatch(txtPrice.Value))
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('请输入正确的价格')", true);
        }
        else
        {
            user_Product up = new user_Product();
            up.Id = id;
            fun.getModel(up);
            up.Name = txtName.Value;
            up.Explain = textarea.Value;
            up.Maker_address = txtAddress.Value;
            up.Newprice = double.Parse(txtPrice.Value);
            up.Typeid = int.Parse(slTYpe.Value);
            up.Sjid = 2;
            up.Userid = ((userCenter)Session["userinfo"]).Id;
            up.Typeid2 = int.Parse(slDy.Value);
            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile(Server.MapPath(up.Picture), file1, out pic, out error, "admin/upImages/");
                up.Picture = pic;
            }

            fun.DoSql(this, fun.update(up), Request.Url.ToString());
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["userinfo"] = null;
        fun.AJAXalert("alert('已经成功退出');location='index.aspx'");
    }
}
