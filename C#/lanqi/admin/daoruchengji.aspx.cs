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

public partial class admin_daoruchengji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from honortype","type","id");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (File1.Value == "")
        {
            fun.AJAXalert(this, "请选择excel文件");
        }
        else
        {
            try
            {
                string str = File1.Value;
                string table = "chengji" + DropDownList1.SelectedValue;
                string sql = @"SELECT * INTO "+table+" FROM [excel 8.0;database=" + str + "].[sheet1$]";
                //string sql = @"insert  INTO zy3 select * FROM [excel 8.0;database=" + str + "].[sheet1$]";
                fun.DoSql(this, sql,Request.Url.ToString());
            }
            catch (Exception ex)
            {

                fun.AJAXalert(this, "该专业已经导入成绩表，请选择追加成绩记录或重新导入，若重新导入，原先数据将全部丢失");
            }
        }


    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (File1.Value == "")
        {
            fun.AJAXalert(this, "请选择excel文件");
        }
        else
        {
            try
            {
                string str = File1.Value;
                string table = "chengji" + DropDownList1.SelectedValue;
                
                string sql = @"insert  INTO "+table+" select * FROM [excel 8.0;database=" + str + "].[sheet1$]";
                fun.DoSql(this, sql, Request.Url.ToString());
            }
            catch (Exception ex)
            {

                fun.AJAXalert(this, "该专业还未导入成绩表，不能追加成绩，请先导入成绩表");
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (File1.Value == "")
        {
            fun.AJAXalert(this, "请选择excel文件");
        }
        else
        {
            try
            {
                string str = File1.Value;
                string table = "chengji" + DropDownList1.SelectedValue;

                fun.DoSqlAJAX( "   drop table "+table);
                string sql = @"SELECT * INTO " + table + " FROM [excel 8.0;database=" + str + "].[sheet1$]";
                //string sql = @"insert  INTO zy3 select * FROM [excel 8.0;database=" + str + "].[sheet1$]";
                fun.DoSql(this, sql, Request.Url.ToString());
            }
            catch (Exception ex)
            {

                string str = File1.Value;
                string table = "chengji" + DropDownList1.SelectedValue;
                string sql = @"SELECT * INTO " + table + " FROM [excel 8.0;database=" + str + "].[sheet1$]";
                //string sql = @"insert  INTO zy3 select * FROM [excel 8.0;database=" + str + "].[sheet1$]";
                fun.DoSql(this, sql, Request.Url.ToString());
            }
        }
    }
 
}
