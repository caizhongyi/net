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

public partial class admin_ygfcAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("ygfc");
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from ygfctype","type","id");
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from ygfc where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    SiteName.Value = dr["name"].ToString();
                    FCKeditor1.Value = dr["honor_content"].ToString();
                    Image1.ImageUrl = dr["pic"].ToString();
                    DropDownList1.SelectedValue = dr["typeid"].ToString();
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
            DataRow dr = fun.GetDataTable("select * from ygfc where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["pic"].ToString()));
            string sql = "update ygfc set pic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            Image1.ImageUrl = "";
        }
    }

    private void add()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        if (!fun.CheckStr(name) )
        {

            fun.AJAXalert(this, "名称不能为空");
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

            string sql = string.Format("insert into ygfc (name,honor_content,pic,addtime,typeid) values('{0}','{1}','{2}','{3}',{4})", name, url, pic, DateTime.Now,DropDownList1.SelectedValue);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        if (!fun.CheckStr(name) )
        {

            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = fun.GetDataTable("select * from ygfc where id=" + id).Rows[0];

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

            string sql = string.Format("update ygfc set name='{0}',honor_content='{1}',pic='{2}',typeid={3} where id={4}", name, url, pic, DropDownList1.SelectedValue,id);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }
}
