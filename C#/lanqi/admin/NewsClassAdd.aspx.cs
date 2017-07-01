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

public partial class admin_NewsClassAdd : System.Web.UI.Page
{
    private static string table = "";
    private static string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (fun.CheckStr(Request.QueryString["id"]) && fun.IsMatch(Request.QueryString["id"]))
        {

            if (!IsPostBack)
            {
                int nid = int.Parse(Request.QueryString["id"]);
                
                if (Request.QueryString["type"] == "news_class2")
                {
                    table = "news_class2";
                    str = "type";
                    newtype.InnerHtml = "新闻二级分类";
                    fun.bind(DropDownList1, "select * from news_class", "class1", "id");
                    DataTable dt = fun.GetDataTable("select * from " + table + " where id=" + nid);
                    if (dt.Rows.Count > 0)
                    {
                        SiteUrl.Value = dt.Rows[0]["type"].ToString();
                        DropDownList1.SelectedValue = dt.Rows[0]["sjid"].ToString();
                        Image1.ImageUrl = dt.Rows[0]["pic"].ToString();

                    }
                }
                else if (Request.QueryString["type"] == "news_class")
                {
                    table = "news_class";
                    str = "class1";
                    newtype.InnerHtml = "新闻一级分类";
                    DataTable dt = fun.GetDataTable("select * from " + table + " where id=" + nid);
                    if (dt.Rows.Count > 0)
                    {
                        SiteUrl.Value = dt.Rows[0]["class1"].ToString();
                        Image1.ImageUrl = dt.Rows[0]["pic"].ToString();
                        title.Visible = false;

                    }
                }
                
       
                Session["name"] = SiteUrl.Value;
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = SiteUrl.Value;
        int nid=int.Parse(Request.QueryString["id"]);
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "类别名称不能为空");
        }
        else
        {
  
            up();


        }
    }

    private void up()
    {
        string name = fun.GetSafeStr(SiteUrl.Value);
        if (SiteUrl.Value != Session["name"].ToString())
        {
            if (fun.CheckName(table, str, name))
            {
                fun.AJAXalert(this, "该类别已经存在");
                return;
            }


        }
        int id = int.Parse(Request.QueryString["id"]);
        DataRow dr = fun.GetDataTable("select * from "+table+" where id=" + id).Rows[0];

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
        string sql = "";
        if (table == "news_class2")
        {
            int tid = int.Parse(DropDownList1.SelectedValue);
            sql = string.Format("update news_class2 set type='{0}',sjid={1},pic='{2}' where id={3} ", name, tid, pic, id);
        }
        else if (table == "news_class")
        {
             sql = string.Format("update news_class set class1='{0}',pic='{1}' where id={2} ", name,  pic, id);
        }
        fun.DoSql(this, sql, Request.Url.ToString());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            int id = int.Parse(sid);
            DataRow dr = fun.GetDataTable("select * from "+table+" where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["pic"].ToString()));
            string sql = "update "+table+" set pic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            Image1.ImageUrl = "";
        }

    }
}
