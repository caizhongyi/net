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

public partial class admin_mapDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from mapdetail ","name","id");
            

            string sql = string.Format("select * from mapdetail where id="+int.Parse(DropDownList1.SelectedValue));
            DataRow dr = fun.GetDataTable(sql).Rows[0];
            FCKeditor1.Value = dr["remark"].ToString();
        }

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string content = fun.GetSafeStr(FCKeditor1.Value);
        int id = int.Parse(DropDownList1.SelectedValue);

        string sql = "";

        sql = string.Format("update mapdetail set remark='{0}' where id={1} ", content,id);


        fun.DoSql(this, sql);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string sql = string.Format("select * from mapdetail where id=" + int.Parse(DropDownList1.SelectedValue));
        DataRow dr = fun.GetDataTable(sql).Rows[0];
        FCKeditor1.Value = dr["remark"].ToString();
    }
}
