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
using System.IO;
public partial class admin_chakanchengji : System.Web.UI.Page
{
    public string t;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("zhuangye");
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from zhuangye","name","id");
          
        }
        t = DropDownList1.SelectedItem.Text;
        GridView1.Visible = true;
       
            GridView1.DataSource = fun.GetDataTable("select * from " + DropDownList1.SelectedItem.Text);
            GridView1.DataBind();
            GridView2.DataSource = fun.GetDataTable("select * from " + DropDownList1.SelectedItem.Text);
            GridView2.DataBind();
            fun.bind(rpAdmin, "select * from admin");
        
    
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string upid = GridView1.DataKeys[e.RowIndex].Value.ToString();

        //string u = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        //string str = "update questiontype set type='" + re.CheckStr(u) + "' where id=" + upid;


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
     
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind(); ;
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    GridView1.DataSource = fun.GetDataTable("select * from chengji" + DropDownList1.SelectedValue);
        //    GridView1.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    fun.AJAXalert(this, "该专业还没有任何成绩数据");
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //GridView2.Visible = true;
        //ExportExcel(GridView2);
        //GridView2.Visible = false;
        ExportExcel(rpAdmin);
    }
    public void ExportExcel(GridView exportTargetGridView)
    {
        HttpContext.Current.Response.ClearContent();

        HttpContext.Current.Response.Charset = "GB2312";

        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF7;

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");

        HttpContext.Current.Response.ContentType = "application/excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        exportTargetGridView.RenderControl(htw);

        HttpContext.Current.Response.Write(sw.ToString());

        HttpContext.Current.Response.End();
    }



    public override void VerifyRenderingInServerForm(Control control)//注意重写这个函数
    {

    }


    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cmd = e.CommandName;
        string id = Convert.ToString(e.CommandArgument);
        if (cmd == "del")
        {
            string sql = "delete  from "+DropDownList1.SelectedItem.Text+" where  身份证='"+id+"'";
            fun.DoSqlAJAX(sql);
            GridView1.DataSource = fun.GetDataTable("select * from " + DropDownList1.SelectedItem.Text);
            GridView1.DataBind();
            GridView2.DataSource = fun.GetDataTable("select * from " + DropDownList1.SelectedItem.Text);
            GridView2.DataBind();
        }
       
    }
    public void ExportExcel(Repeater GridView1)
    {

//设置网络输出流的HTTP字符集为UTF-8，Current为当前 HTTP 请求获取 HttpContext 对象
        HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        //设置输出流HTTPMIME类型为excel
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        //将HTTP头添加到输出流
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("文章访问统计") + ".xls");
        //不保存该控件的视图状态
        GridView1.Page.EnableViewState = false;
        System.IO.StringWriter sw = new System.IO.StringWriter();

        //将文本写入到输出流
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        //将服务器控件的内容输出到HtmlTextWriter对象中
        GridView1.RenderControl(hw);

        hw.Write("<div>" +"111" + "</div>");
        //StringWriter.ToString返回包含迄今为止写入到当前 StringWriter 中的字符的字符串
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    
    }

}
