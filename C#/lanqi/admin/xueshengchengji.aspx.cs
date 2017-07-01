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

public partial class xueshengchengji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("zhuangye");
        if (!IsPostBack)
        {
            fun.bind(DropDownList1, "select * from zhuangye", "name", "id");
            DataTable dt = fun.GetDataTable("select * from " + DropDownList1.SelectedItem.Text);
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
        string xm =fun.GetSafeStr( pwd.Value);
        string xh = fun.GetSafeStr( Text1 .Value);
        string sfz = fun.GetSafeStr(Text2.Value);
        string cj = fun.GetSafeStr(Text3.Value);
        string table = DropDownList1.SelectedItem.Text;
        string kemu = DropDownList2.SelectedItem.Text;
        if ( !fun.CheckStr(sfz))
        {
            fun.AJAXalert(this, "身份证为必填");
        }
        else if (fun.CheckName(table, "身份证", sfz))
        {
            string sql = string.Format("update  {0} set {1} ='{2}' where 身份证='{3}'", table, kemu, cj,sfz);
            fun.DoSql(this, sql);
        }
        else
        {
            string sql = string.Format("insert into {0}(姓名,学号,身份证,{1}) values('{2}','{3}','{4}','{5}')", table, kemu, xm, xh, sfz, cj);
            fun.DoSql(this, sql);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList2.Items.Clear();
        DataTable dt = fun.GetDataTable("select * from " + DropDownList1.SelectedItem.Text);
        foreach (DataColumn d in dt.Columns)
        {
            if (d.ColumnName != "姓名" && d.ColumnName != "学号" && d.ColumnName != "身份证")
            {
                ListItem l = new ListItem(d.ColumnName);
                DropDownList2.Items.Add(l);
            }
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
