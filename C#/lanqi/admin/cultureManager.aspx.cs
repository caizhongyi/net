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
using System.Text;
public partial class admin_cultureManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("culture");
        if (!IsPostBack)
        {
            fun.BindPage("select * from culture  order by  paixu desc", AspNetPager1, rpNews, 6);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from culture  order by  paixu desc", AspNetPager1, rpNews, 6);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (fun.CheckStr(Id_str))
        {
            sql = "delete from  culture where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString(),"picture");
        }
        else
        {
            fun.AJAXalert(this, "请至少选择一项！");
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        StringBuilder sr = new StringBuilder();
        foreach (RepeaterItem r in rpNews.Items)
        {
            TextBox t = r.FindControl("txtPaiXu") as TextBox;
            if (t.Text == "")
            {
                t.Text = "0";

            }
            if (!fun.CheckStr(t.Text) || !fun.IsMatch(t.Text))
            {
                fun.AJAXalert(this, "排序只能用数字");
                return;
            }
            int paixu = int.Parse(t.Text);
            Label cb = r.FindControl("lblId") as Label;
            int id = int.Parse(cb.Text);
            string sql = "  update  culture set paixu=" + paixu + " where id=" + id;
            sr.Append(sql);
           


        }
        fun.DoSql(this, sr.ToString(), Request.Url.ToString());
    }
}
