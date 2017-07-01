using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_car_color : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            fun.BindPage("select a.*,b.xname from car_color a inner join car_type b on a.cid=b.xid", AspNetPager3, rpNewClass3, 10);
            Bind();
        }
    }
    private void Bind()
    {
        if (Request.QueryString["cid"] != null)
        {
            this.DropDownList1.DataBind();
            string sql = "select * from car_color where colorid=" + Request.QueryString["cid"];
            DataSet ds = fun.getDataSet(sql);
            this.TextBox3.Text = ds.Tables[0].Rows[0][1].ToString();
            this.DropDownList1.SelectedIndex = this.DropDownList1.Items.IndexOf(this.DropDownList1.Items.FindByValue(ds.Tables[0].Rows[0][2].ToString()));
            this.Button5.Text = "修改";
        }
    }
    protected void Button6_Click(object sender, EventArgs e)//删除３级分类
    {
        string Id_str = Request["ch3"], sql = "";
        if (Request["ch3"] != null && Id_str != "")
        {
            
            string sqlstr2 = "delete from car_color where colorid in  (" + Id_str + ")";




            fun.DoSql(this, sqlstr2, Request.Url.ToString());
            //fun.BindPage("select * from user_producttype3  ", AspNetPager3, rpNewClass3, 3);

        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！');");
        }
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        string name = TextBox3.Text;
        string cid = this.DropDownList1.SelectedValue;
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
               name = fun.GetSafeStr(name);
           
                if (Request.QueryString["cid"] == null)
                {
                    string sql = string.Format("insert into car_color (colorname,cid) values ('{0}',{1})", name, cid);
                    fun.DoSql(this, sql, Request.Url.ToString());
                }
                else
                {

                    string sql = "update car_color set colorname='" + name + "', cid=" + cid + " where colorid=" + Request.QueryString["cid"];
                    this.Button5.Text = "添加";
                    fun.DoSql(this, sql, Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?")));
                }

           
        }
    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select a.*,b.xname from car_color a inner join car_type b on a.cid=b.xid ", AspNetPager3, rpNewClass3, 10);
    }
}
