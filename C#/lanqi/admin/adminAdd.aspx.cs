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

public partial class admin_adminAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("admin");
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            Label1.Text = "修改管理员";
            
          
            if (!IsPostBack)
            {
               
                string sql = "select * from admin where id=" + int.Parse(sid);

                if (fun.GetDataTable(sql).Rows.Count > 0)
                {
                    

                    DataRow dr = fun.GetDataTable(sql).Rows[0];
                    name.Value = dr["username"].ToString();
                    pwd.Value = dr["userpassword"].ToString();
                    checkpwd.Value = pwd.Value;
                    email.Value = dr["email"].ToString();
                    address.Value = dr["inaddress"].ToString();
                    ring.Value = dr["ring"].ToString();
                    tel.Value = dr["tel"].ToString();
                    if (dr["sex"].ToString() == "男")
                    {
                        rdMan.Checked = true;
                    }
                    else
                    {
                        rdWoman.Checked = true;
                    }
                }
            }
        }
        else
        {
            Label1.Text = "添加管理员";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = fun.GetSafeStr(Request.Form["name"]);
        string pwd = fun.GetSafeStr(Request.Form["pwd"]);
        string pwd1 = Request.Form["checkpwd"];
        if (!fun.CheckStr(name) || !fun.CheckStr(pwd))
        {
            Response.Write("<script>alert('用户名或密码不能为空');</script>");
        }
        else if (pwd != pwd1)
        {
            Response.Write("<script>alert('２次输入密码不一致');</script>");
        }
     
        else
        {
            string sex = fun.GetSafeStr(Request.Form["rd"]);
            string email = fun.GetSafeStr(Request.Form["email"]);
            string address = fun.GetSafeStr(Request.Form["address"]);
            string ring = fun.GetSafeStr(Request.Form["ring"]);
            string tel = fun.GetSafeStr(Request.Form["tel"]);
            pwd = fun.MD5(pwd);
            string sid = Request.QueryString["id"];
            string sql = "";
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                sql = string.Format("update admin set username='{0}',userpassword='{1}',sex='{2}',email='{3}',inaddress='{4}',ring='{5}' ,tel='{6}' where id={7}", name, pwd, sex, email, address, ring, tel, int.Parse(Request.QueryString["id"]));
                fun.DoSql(this, sql, Request.Url.ToString());
            }
            else
            {
                if (fun.CheckName("admin", "username", name))
                {
                    Response.Write("<script>alert('用户名已经存在，请更改用户名');</script>");
                }
                else
                {

                    sql = string.Format("insert into admin (username,userpassword,sex,email,inaddress,ring,tel) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", name, pwd, sex, email, address, ring, tel);
                    fun.DoSql(this, sql, Request.Url.ToString());
                }
            }
            

        }
    }
}
