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

public partial class Main : System.Web.UI.MasterPage
{
    /// <summary>
    /// 设置默认皮肤
    /// </summary>
    protected string skinName = "lovely";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        CheckLogin();
        
        //判断是否更换皮肤
        if (this.IsPostBack)
        {
            this.skinName = this.DropDownList1.SelectedValue;
            //保存当前皮肤到会话信息，可以保存在服务器
            Session["SkinName"] = skinName;
        }

        //判断会话信息中有没有包含皮肤信息，有则优先选择会话中的皮肤信息
        if (Session["SkinName"] != null)
        {
            this.skinName = Session["SkinName"].ToString();
            //设置下拉选择的状态和Session当中皮肤的状态是一致的
            this.DropDownList1.SelectedValue = this.skinName;
        }

    }
    /// <summary>
    /// 判断用户状态
    /// 如果登录成功,则显示后台控制面板
    /// 否则,显示登录部分
    /// </summary>
    private void CheckLogin()
    {
        if (Session["UserName"] != null)
        {
            this.User_Success.Visible = true;
            this.User_Login.Visible = false;
        }
        else
        {
            this.User_Success.Visible = false;
            this.User_Login.Visible = true;
        }
    }

   /// <summary>
   /// 登录系统
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string username = this.UserName.Text;
        string password = this.Password.Text;
        string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
        SqlConnection conn = new SqlConnection(connStr);
        string sql = "select * from UserInfo where UserName='" + username + "' AND Password='" + password + "'";
        SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();

        adapter.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            Session["UserName"] = username;
            this.User_Login.Visible = false;
            this.User_Success.Visible = true;
            this.Label1.Visible = false;

            if (Session["RedirectUrl"] != null)
            {
                string redirectUrl = Session["RedirectUrl"].ToString();
                Session["RedirectUrl"] = null;
                Response.Redirect(redirectUrl);//页面跳转
            }
        }
        else
        {
            this.Label1.Text = "<font color=red>登录失败</font>";
            this.Label1.Visible = true;
        }
       
    }

    /// <summary>
    /// 退出系统
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LogoutLink_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        CheckLogin();
        Response.Redirect("Default.aspx");
    }
}
