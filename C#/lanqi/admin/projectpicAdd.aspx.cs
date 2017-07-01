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

public partial class admin_projectpicAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from project_pic where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    sContent.Value = dr["content"].ToString();

                    fujian.InnerHtml = dr["pic"].ToString();
                    mingcheng.Value = dr["title"].ToString();
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            int id = int.Parse(sid);
            DataRow dr = fun.GetDataTable("select * from project_pic where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["pic"].ToString()));
            string sql = "update project_pic set pic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            mingcheng.Value = "";
        }
    }

    private void add()
    {
        
        string t = mingcheng.Value;
        string c = sContent.Value;
        if (!fun.CheckStr(t))
        {

            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
           
            t = fun.GetSafeStr(t);

            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile("", file1, out pic, out error);
            }

            string sql = string.Format("insert into project_pic (pic,content,title) values('{0}','{1}','{2}')", pic, c, t);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name = sContent.Value;
        string t = mingcheng.Value;
        if (!fun.CheckStr(t))
        {

            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            t = fun.GetSafeStr(t);
            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = fun.GetDataTable("select * from project_pic where id=" + id).Rows[0];

            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile(Server.MapPath(dr["pic"].ToString()), file1, out pic, out error);
            }
            else
            {
                pic = dr["pic"].ToString();
            }

            string sql = string.Format("update project_pic set pic='{0}',content='{1}',title='{3}' where id={2}", pic, name, id, t);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }
}
