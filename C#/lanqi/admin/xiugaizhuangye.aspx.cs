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

public partial class admin_xiugaizhuangye : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = Request.QueryString["name"];
        mc.InnerHtml = Request.QueryString["name"];
        if (!IsPostBack)
        {
            DataTable dt = fun.GetDataTable("select * from " + name);
            foreach (DataColumn d in dt.Columns)
            {
                if (d.ColumnName != "姓名" && d.ColumnName != "学号" && d.ColumnName != "身份证")
                {
                    ListItem l = new ListItem(d.ColumnName);
                    DropDownList2.Items.Add(l);
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = TextBox1.Text;
        if (!fun.CheckStr(str))
        {
            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
            string name = Request.QueryString["name"];
            string oldc = DropDownList2.SelectedItem.Text;
            string newc=str;
            DataTable dt=fun.GetDataTable("select * from "+name);
            string sql = "alter table   " + name + " drop column " + DropDownList2.SelectedItem.Text;
            fun.DoSqlAJAX(sql);
            string sql1 = "alter table " + name + " add " + newc + " string";
            fun.DoSqlAJAX(sql1);
            foreach (DataRow dr in dt.Rows)
            {
                string sql2 = string.Format("update {0} set {1}='{2}' where 身份证='{3}'", name, newc, dr[oldc].ToString(), dr["身份证"].ToString());
                fun.DoSqlAJAX(sql2);
            }
            fun.AJAXalert(this, "alert('修改成功');location='" + Request.Url.ToString() + "'");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string name = Request.QueryString["name"];
        string sql = "alter table   "+name+" drop column "+DropDownList2.SelectedItem.Text;
        fun.DoSqlAJAX(sql);
        fun.AJAXalert(this,"alert('删除成功');location='"+Request.Url.ToString()+"'");
    }
}
