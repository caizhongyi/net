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

public partial class admin_flashPicAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fun.quanxian("flashpic");
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from flashpic where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    SiteName.Value = dr["web_name"].ToString();
                    SiteUrl.Value = dr["web_address"].ToString();
                    Image1.ImageUrl = dr["pic"].ToString();
                    flaType.Value = dr["type"].ToString();
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

    private void add()
    {
        string name = SiteName.Value;
        string url = SiteUrl.Value;
        string type = flaType.Value;
        if (!fun.CheckStr(name) || !fun.CheckStr(url))
        {

            fun.AJAXalert(this, "链接名称或链接地址不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile("", file1, out pic, out error);
            }

            string sql = string.Format("insert into flashpic (web_name,web_address,pic,addtime,type) values('{0}','{1}','{2}','{3}','{4}')", name, url, pic, DateTime.Now, type);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name = SiteName.Value;
        string url = SiteUrl.Value;
        string type = flaType.Value;
        if (!fun.CheckStr(name) || !fun.CheckStr(url))
        {

            fun.AJAXalert(this, "链接名称或链接地址不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = fun.GetDataTable("select * from flashpic where id=" + id).Rows[0];

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

            string sql = string.Format("update flashpic set web_name='{0}',web_address='{1}',pic='{2}',type='{4}' where id={3}", name, url, pic, id,type);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            int id = int.Parse(sid);
            DataRow dr = fun.GetDataTable("select * from flashpic where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["pic"].ToString()));
            string sql = "update flashpic set pic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            Image1.ImageUrl = "";
        }
    }
}
