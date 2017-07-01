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

public partial class admin_cultureType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("culture");
        if (!IsPostBack)
        {
            fun.bind(slDy,"select * from sjtype","sjname","id");
            fun.BindPage("select * from culturetype where 1=1 order by paixu asc", AspNetPager3, rpNewClass3, 10);

        }
    }

    protected void Button6_Click(object sender, EventArgs e)//删除３级分类
    {
        string Id_str = Request["ch3"], sql = "";
        if (Request["ch3"] != null && Id_str != "")
        {
            sql = "delete from  culturetype where id in (" + Id_str + ")";
            string sqlstr = "delete from culture where typeid in (" + Id_str + ")";
            fun.DoSqlAJAX(sqlstr);
            fun.DoSql(this, sql, Request.Url.ToString(), "pic");
            //fun.BindPage("select * from user_producttype3  ", AspNetPager3, rpNewClass3, 3);

        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！');");
        }
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        string name = TextBox3.Text;
        string sjid = slDy.Value;
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "类别名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            if (fun.CheckName("culturetype", "type", name))
            {
                fun.AJAXalert(this, "该类别已经存在");
            }
            else
            {
                string sql = string.Format("insert into culturetype (type,sjid) values ('{0}',{1})", name,sjid);
                fun.DoSql(this, sql, Request.Url.ToString());

            }
        }
    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from culturetype  where 1=1 order by paixu asc", AspNetPager3, rpNewClass3, 10);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem r in rpNewClass3.Items)
        {
            TextBox t = r.FindControl("txtPaiXu") as TextBox;
            if (t.Text=="")
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
            string sql = "update cultureType set paixu=" + paixu + " where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());


        }
        fun.BindPage("select * from culturetype where 1=1 order by paixu asc", AspNetPager3, rpNewClass3, 10);
    }
}
