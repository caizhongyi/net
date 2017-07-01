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

public partial class admin_lifeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            Label1.Text = "修改公告";
            int pid = int.Parse(sid);
            if (!IsPostBack)
            {
                string sql = string.Format("select * from life where id={0}", pid);
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["title"].ToString();

                    FCKeditor1.Value = dt.Rows[0]["content"].ToString();
                }


            }
        }
        else
        {

            Label1.Text = "添加公告";
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Label1.Text == "添加公告")
        {
            Add();
        }
        else if (Label1.Text == "修改公告")
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

            string remark = fun.GetSafeStr(FCKeditor1.Value);
            name = fun.GetSafeStr(name);

            string sql = string.Format("insert into life (title,[content]) values ('{0}','{1}')", name, remark);
            fun.DoSql(this, sql, Request.Url.ToString());

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

            string remark = fun.GetSafeStr(FCKeditor1.Value);
            name = fun.GetSafeStr(name);

            int id = int.Parse(Request.QueryString["id"]);
            string sql = string.Format("update life set title='{0}',[content]='{1}' where id={2}", name, remark, id);
            fun.DoSql(this, sql, Request.Url.ToString());

        }
    }

}
