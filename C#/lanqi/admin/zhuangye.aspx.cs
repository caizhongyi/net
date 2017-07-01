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

public partial class admin_zhuangye : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("zhuangye");
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from zhuangye","name","id");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string n = fun.GetSafeStr(pwd.Value);
        if (!fun.CheckStr(n))
        {
            fun.AJAXalert(this, "专业班级名称不能为空");
        }
        else
        {
            try
            {
                string sql = "create table " + n;
                fun.DoSqlAJAX(sql);
                string sql11 = "alter table " + n + " add 姓名 string;";
                fun.DoSqlAJAX(sql11);
                string sql12 = "alter table " + n + " add 学号 string;";
                fun.DoSqlAJAX(sql12);
                string sql13 = "alter table " + n + " add 身份证 string;";
                fun.DoSqlAJAX(sql13);
                string sql2 = "insert into zhuangye (name) values ('"+n+"')";
                fun.DoSqlAJAX(sql2);
                fun.AJAXalert(this, "alert('添加成功');location='" + Request.Url.ToString() + "'");
            }
            catch (Exception)
            {
                
               fun.AJAXalert(this, "该班级已经存在或发生异常");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = fun.GetSafeStr(address.Value);
        if (!fun.CheckStr(str))
        {
            fun.AJAXalert(this, "专业课程名称不能为空");
        }
        else
        {
            try
            {
                string sql = "alter table " + DropDownList1.SelectedItem.Text + " add " + str + " string";
                fun.DoSqlAJAX(sql);
                fun.AJAXalert(this, "添加成功");
            }
            catch (Exception)
            {
                
                 fun.AJAXalert(this, "该课程已经存在或发生异常");
            }
        }
     
    }
}
