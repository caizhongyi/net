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

public partial class admin_NewClass2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        fun.quanxian("news_class2");
        
        if (!IsPostBack)
        {
           
            fun.BindPage("select * from news_class2  ", AspNetPager2, rpNewClass2, 8);
           
        }
    }
   
    protected void Button4_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch2"], sql = "";
        if (Request["ch2"] != null && Id_str != "")
        {
            string sqlstr = "delete from  news where class1_id in (" + Id_str + ")";
            sql = "delete from  news_class2 where id in (" + Id_str + ")";
            fun.DoSqlAJAX( sqlstr);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this, "请至少选择一项！");
        }
    }
 
    protected void Button3_Click(object sender, EventArgs e)
    {
        string name = TextBox2.Text;
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "类别名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            if (fun.CheckName("news_class2", "type", name))
            {
                fun.AJAXalert(this, "该类别已经存在");
            }
            else
            {
               
                string sql = string.Format("insert into news_class2 (type) values ('{0}')", name);
                fun.DoSql(this, sql, Request.Url.ToString());


            }
        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from news_class2  ", AspNetPager2, rpNewClass2, 8);
    }
    protected void UpLoad1_Load(object sender, EventArgs e)
    {

    }
}
