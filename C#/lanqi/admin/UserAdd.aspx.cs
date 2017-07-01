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

public partial class admin_UserAdd : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("usercenter");
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            Label1.Text = "修改会员";
            if (!IsPostBack)
            {
                string sql = "select * from usercenter where id=" + int.Parse(sid);

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
                    Text1.Value = dr["name"].ToString();
                    Text2.Value = dr["qq"].ToString();
                    Text3.Value = dr["mingzhu"].ToString();
                    Text4.Value = dr["xueli"].ToString();
                    
                 
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
            Label1.Text = "添加会员";
        }
      
    }
 
    protected void UserControl()
    {
        string name =fun.GetSafeStr( Request.Form["name"]);
        string pwd =fun.GetSafeStr( Request.Form["pwd"]);
        string pwd1 = Request.Form["checkpwd"];
   
      
      
            string sex =fun.GetSafeStr( Request.Form["rd"]);
            string email =fun.GetSafeStr( Request.Form["email"]);
            string address =fun.GetSafeStr( Request.Form["address"]);
            string ring =fun.GetSafeStr( Request.Form["ring"]);
            string tel =fun.GetSafeStr( Request.Form["tel"]);
        
            string sid = Request.QueryString["id"];
            string sql = "";
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                pwd = fun.MD5(pwd);
                sql = string.Format("update usercenter set username='{0}',sex='{2}',email='{3}',inaddress='{4}',ring='{5}' ,tel='{6}',name='{8}',qq='{9}',mingzhu='{10}',xueli='{11}' where id={7}", name, pwd, sex, email, address, ring, tel, int.Parse(Request.QueryString["id"]), Text1.Value, Text2.Value, Text3.Value, Text4.Value);
                 fun.DoSql(this, sql, Request.Url.ToString());
            }
            else
            {
                if (!fun.CheckStr(name) || !fun.CheckStr(pwd))
                {
                    Response.Write("<script>alert('用户名或密码不能为空');</script>");
                }
                else if (pwd != pwd1)
                {
                    Response.Write("<script>alert('２次输入密码不一致');</script>");
                }
                else if (fun.CheckName("usercenter", "username", name))
                {
                    Response.Write("<script>alert('用户名已经存在，请更改用户名');</script>");
                }
                else
                {
                    pwd = fun.MD5(pwd);
                    sql = string.Format("insert into usercenter (username,userpassword,sex,email,inaddress,ring,tel,name,qq,mingzhu,xueli) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", name, pwd, sex, email, address, ring, tel,Text1.Value,Text2.Value,Text3.Value,Text4.Value);
                    fun.DoSql(this, sql, Request.Url.ToString());
                }
            }
           
           
        
    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        UserControl();
        
    }
}
