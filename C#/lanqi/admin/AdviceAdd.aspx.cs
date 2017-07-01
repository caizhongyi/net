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

public partial class admin_AdviceAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("advice");
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid)&&fun.IsMatch(sid))
        {
            Label1.Text = "修改简介";
            int pid = int.Parse(sid);
            if (!IsPostBack)
            {
                string sql = string.Format("select * from advice where id={0}", pid);
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["title"].ToString();
                    TextBox3.Text = dt.Rows[0]["zhuozhe"].ToString();
                    TextBox4.Text = dt.Rows[0]["hot"].ToString();
                    TextBox5.Text = dt.Rows[0]["laiyuan"].ToString();
                    FCKeditor1.Value = dt.Rows[0]["content"].ToString();
                }


            }
        }
        else
        {

            Label1.Text = "添加简介";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Label1.Text == "添加简介")
        {
            Add();
        }
        else if (Label1.Text == "修改简介")
        {
            Update();
        }
    }
    protected void Add()
    {
        string name = txtName.Text;
        if (!fun.CheckStr(name))
        {
            Response.Write("<script>alert('标题不能为空');</script>");
        }
        else
        {
           
            string remark =FCKeditor1.Value;
            name = fun.GetSafeStr(name);
            
            string sql = string.Format("insert into advice (title,[content]) values ('{0}','{1}')", name,  remark);
            fun.DoSql(this,sql,Request.Url.ToString());
            
        }
    }
    protected void Update()
    {
        string name = txtName.Text;
        if (!fun.CheckStr(name))
        {
            Response.Write("<script>alert('标题不能为空');</script>");
        }
        else
        {

            string remark = FCKeditor1.Value;
            name = fun.GetSafeStr(name);
            
            int id = int.Parse(Request.QueryString["id"]);
            string sql = string.Format("update advice set title='{0}',[content]='{1}',zhuozhe='{3}',laiyuan='{4}',hot={5},change_date='{6}' where id={2}", name,  remark,  id,TextBox3.Text,TextBox5.Text,TextBox4.Text,DateTime.Now);
            fun.DoSql(this, sql, Request.Url.ToString());
          
        }
    }
}
