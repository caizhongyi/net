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

public partial class admin_honorTypeManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("honortype");
        if (!IsPostBack)
        {

            fun.BindPage("select * from honortype ", AspNetPager3, rpNewClass3, 3);

        }
    }

    protected void Button6_Click(object sender, EventArgs e)//删除３级分类
    {
        string Id_str = Request["ch3"], sql = "";
        if (Request["ch3"] != null && Id_str != "")
        {
            sql = "delete from  honortype where id in (" + Id_str + ")";
            string sqlstr = "delete from honor where typeid in (" + Id_str + ")";
            fun.DoSqlAJAX(sqlstr);
            fun.DoSql(this, sql, Request.Url.ToString(),"pic");
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
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "类别名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            if (fun.CheckName("honortype", "type", name))
            {
                fun.AJAXalert(this, "该类别已经存在");
            }
            else
            {
                string sql = string.Format("insert into honortype (type) values ('{0}')", name);
                fun.DoSql(this, sql, Request.Url.ToString());

            }
        }
    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from honortype  ", AspNetPager3, rpNewClass3, 3);
    }
}
