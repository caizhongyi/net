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

public partial class admin_NewClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
          
        
        if (!IsPostBack)
        {
            fun.BindPage("select * from news_class  ", AspNetPager1, rpNewClass, 3);
            fun.BindPage("select * from news_class2  ", AspNetPager2, rpNewClass2, 3);
            fun.bind(DropDownList1,"select * from news_class2","class","id");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null && Id_str != "")
        {
            string str = "0";
            DataTable dt = fun.GetDataTable("select * from news_class2 where sjid in (" + Id_str + ")");
            foreach (DataRow dr in dt.Rows)
            {
                str += "," + dr["id"].ToString();
            }


            string sqlstr = "delete from news where class1_id in (" + str + ")";
            string sql2 = "delete from  news_class2 where sjid in (" + Id_str + ")";
            sql = "delete from  news_class where id in (" + Id_str + ")";


            fun.DoSqlAJAX(sqlstr);
            fun.DoSqlAJAX(sql2);
            fun.DoSql(this, sql, Request.Url.ToString());
            //fun.BindPage("select * from user_producttype3  ", AspNetPager3, rpNewClass3, 3);

        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！');location='NewClass.aspx'");
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string name=TextBox1.Text;
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "类别名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            if (fun.CheckName("news_class", "class1", name))
            {
                fun.AJAXalert(this, "该类别已经存在");
            }
            else
            {
                string sql = string.Format("insert into news_class (class1) values ('{0}')", name);
                fun.DoSql(this, sql, Request.Url.ToString());

                
            }
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
                int id =int.Parse( DropDownList1.SelectedValue);
                string sql = string.Format("insert into news_class2 (type,sjid) values ('{0}',{1})", name,id);
                fun.DoSql(this, sql, Request.Url.ToString());


            }
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from news_class  ", AspNetPager1, rpNewClass, 3);
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from news_class2  ", AspNetPager2, rpNewClass2, 3);
    }
    protected void UpLoad1_Load(object sender, EventArgs e)
    {

    }
}
