using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_UserInfo_UserInfoEdit : System.Web.UI.Page
{

    Models.UserInfo n = new Models.UserInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Guid id = new Guid();

        if (Request.QueryString["id"] != null)
        {
            id = Guid.Parse( Request.QueryString["id"]);
            n.u_id = id;
            if (!IsPostBack)
            {
                LoadData();
                GetValue();
            }
        }
    
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (n.u_id ==new Guid())
        {
            
            SetValue();
            if (BBL.UserInfo.Insert(n) > 0)
            { czy.MyClass.Web.JavaScript.Alert("添加成功!"); }
            else
            { czy.MyClass.Web.JavaScript.Alert("添加失败!"); }
        }
        else
        {
            BtnAdd.Text = "修改";
            SetValue();
            if(BBL.UserInfo.Update(n.u_id, n)>0)
            { czy.MyClass.Web.JavaScript.Alert("修改成功!"); }
            else
            { czy.MyClass.Web.JavaScript.Alert("修改失败!"); }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserInfo.aspx");
    }

    private void LoadData()
    {
        n = BBL.UserInfo.SelectList(n.u_id.ToString ())[0];
       
        if (n.u_id == new Guid())
        {
            BtnAdd.Text = "添加";
        }
        else
        {
            BtnAdd.Text = "修改";
        }
    }
    
    private void SetValue()
    {
        n.u_name = TxtName.Text;
        n.u_pwd = TxtPassword.Text;
        n.u_createDate = DateTime.Now;
        n.u_loginDate = DateTime.Now;
        n.u_birthday = DateTime.Now;
       // m.m = TxtURL.Text.ToString();
    }
    private void GetValue()
    {
        TxtPassword.Text = n.u_pwd;
        //DropType.SetIndexByValue(n.n_newsTypeId.ToString ());
        TxtName.Text = n.u_name;
    }
}

