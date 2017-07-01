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

public partial class admin_FuWu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("fuwuliucheng");
        if (!IsPostBack)
        {
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from FuWuLiuCheng where id=" + id;
                DataRow dr = fun.GetDataTable(sql).Rows[0];
                if (dr != null)
                {
                    SiteName.Value = dr["title"].ToString();
                    FCKeditor1.Value = dr["content"].ToString();
                    TextBox1.Text = dr["paixu"].ToString();
                }

            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            up();
        }
        else
        {
            add();
        }
    }


    private void add()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        string paixu = TextBox1.Text;
        if (!fun.CheckStr(name) || !fun.CheckStr(url))
        {

            fun.AJAXalert(this, "名称或内容不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);

            int p = int.Parse(paixu);
            string sql = string.Format("insert into FuWuLiuCheng (content,paixu,title) values('{0}','{1}','{2}')", url, p, name);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        string paixu = TextBox1.Text;
        if (!fun.CheckStr(name) || !fun.CheckStr(url))
        {

            fun.AJAXalert(this, "名称或内容不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            int id = int.Parse(Request.QueryString["id"]);
            int p = int.Parse(paixu);
            string sql = string.Format("update FuWuLiuCheng set content='{0}',paixu='{1}',title='{2}' where id={3}", url, p, name, id);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }
}
