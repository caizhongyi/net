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

public partial class admin_xiugaichengji : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("zhuanye");
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
            string id = Request.QueryString["id"];
            string table = Request.QueryString["table"];
            if (fun.CheckStr(id) && fun.CheckStr(table))
            {
              
                DropDownList2.Items.Clear();
                id = fun.GetSafeStr(id);
                table = fun.GetSafeStr(table);
                DataTable dt2 = fun.GetDataTable("select * from " + table);
                foreach (DataColumn d in dt2.Columns)
                {
                    if (d.ColumnName != "姓名" && d.ColumnName != "学号" && d.ColumnName != "身份证")
                    {
                        ListItem l = new ListItem(d.ColumnName);
                        DropDownList2.Items.Add(l);
                    }
                }
                DropDownList1.SelectedItem.Text = table;
                DropDownList1.Enabled = false;
                string sql = "select * from " + table + " where 身份证='" + id + "'";
                DataTable dt1 = fun.GetDataTable(sql);
                pwd.Value = dt1.Rows[0]["姓名"].ToString();
                Text1.Value = dt1.Rows[0]["学号"].ToString();
                Text2.Value = dt1.Rows[0]["身份证"].ToString();
                Text3.Value = dt1.Rows[0][DropDownList2.SelectedItem.Text].ToString();



            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string xm = fun.GetSafeStr(pwd.Value);
        string xh = fun.GetSafeStr(Text1.Value);
        string sfz = fun.GetSafeStr(Text2.Value);
        string cj = fun.GetSafeStr(Text3.Value);
        string table = DropDownList1.SelectedItem.Text;
        string kemu = DropDownList2.SelectedItem.Text;
        if (!fun.CheckStr(sfz))
        {
            fun.AJAXalert(this, "身份证为必填");
        }
        else
        {
            string id = Request.QueryString["id"];
            string t = Request.QueryString["table"];
            if (fun.CheckStr(id) && fun.CheckStr(table))
            {
                string sql = string.Format("update  {0} set {1} ='{2}',姓名='{4}',学号='{5}',身份证='{6}' where 身份证='{3}'", table, kemu, cj, id, xm, xh, sfz);
                fun.DoSql(this, sql, "chakanchengji.aspx");
            }
     
        }
   
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        string table = Request.QueryString["table"];
        if (fun.CheckStr(id) && fun.CheckStr(table))
        {

            string sql = "select * from " + table + " where 身份证='" + id + "'";
            DataTable dt1 = fun.GetDataTable(sql);

            Text3.Value = dt1.Rows[0][DropDownList2.SelectedItem.Text].ToString();



        }
    }
}
