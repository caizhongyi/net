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

public partial class admin_car : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("culture");
        if (!IsPostBack)
        {
           
            fun.BindPage("select * from car_no", AspNetPager3, rpNewClass3, 10);
            Bind();
        } 
    }
    private void Bind() 
    {
         if(Request.QueryString["cid"]!=null){
            string sql = "select * from car_no where cid="+Request.QueryString["cid"];
            this.TextBox3.Text = fun.getDataSet(sql).Tables[0].Rows[0][1].ToString();
            this.Button5.Text = "修改";
        }
    }
       protected void Button6_Click(object sender, EventArgs e)//删除３级分类
    {
        string Id_str = Request["ch3"], sql = "";
        if (Request["ch3"] != null && Id_str != "")
        {
            sql = "delete from  car_no where cid in (" + Id_str + ")";
            string sqlstr2="delete from car_color where cid in (select xid from car_type where cid in (" + Id_str + "))";
            string sqlstr1="delete from car_type where cid in (" + Id_str + ")";
         
            fun.DoSqlAJAX(sqlstr2);
             fun.DoSqlAJAX(sqlstr1);
            fun.DoSql(this, sql, Request.Url.ToString());
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
       
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            if (fun.CheckName("car_no", "cname", name))
            {
                fun.AJAXalert(this, "该类别已经存在");
            }
            else
            {
                if (Request.QueryString["cid"] == null)
                {
                    string sql = string.Format("insert into car_no (cname) values ('{0}')", name);
                    fun.DoSql(this, sql, Request.Url.ToString());
                }
                else 
                {

                    string sql = "update car_no set cname='"+name+"' where cid="+Request.QueryString["cid"];
                    this.Button5.Text = "添加";
                    fun.DoSql(this, sql, Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?")));
                }

            }
        }
    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from car_no ", AspNetPager3, rpNewClass3, 10);
    }

}

