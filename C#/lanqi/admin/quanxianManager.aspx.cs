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

public partial class quanxianManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        adminname.InnerHtml = fun.getById(Request.QueryString["id"],"id","admin","username");
        if (!IsPostBack)
        {
            string sql = "select * from sheziquanxian where userid=  " + Request.QueryString["id"];
            fun.quanxian("admin");
            fun.bind(DropDownList1, "select * from quanxian", "quanxianname", "id");

            fun.bind(rpNews, sql);
        }
        
    }




 
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (fun.CheckStr(Id_str))
        {
            sql = "delete from  sheziquanxian where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this, "请至少选择一项！");
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        string qid=DropDownList1.SelectedValue;
        string sql = "select * from sheziquanxian where userid="+id+" and quanxianid=1";
        if (fun.GetDataTable(sql).Rows.Count > 0)
        {
            fun.AJAXalert(this,"已经是超级管理员");
        }
        else if (qid == "1")
        {
            fun.DoSqlAJAX("delete from sheziquanxian where userid=" + id);
            fun.DoSql(this, string.Format("insert into sheziquanxian (userid,quanxianid) values({0},{1})", id, 1), Request.Url.ToString());
        }
        else
        {
            if (fun.getById(qid, "quanxianid", "sheziquanxian", "id") != "")
            {
                fun.AJAXalert(this, "已经拥有该权限");
            }
            else
            {
                fun.DoSql(this, string.Format("insert into sheziquanxian (userid,quanxianid) values({0},{1})", id, qid), Request.Url.ToString());
            }
        }
    }
}
