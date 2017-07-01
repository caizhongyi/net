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

public partial class user_productList2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            fun.bind(DropDownList3,"select * from user_producttype3","type","id","所有类别");

            fun.BindPage("select * from user_product  order by paixu asc, join_date desc", AspNetPager1, rpProduct, 6);
            Session["sql"] = "select * from user_product  order by paixu asc, join_date desc";
            
        }
        
           
                
        //fun.bind(rpProduct, "select * from user_product  order by join_date desc", 6, Label1, Label2, start, prev, next, max);
            
       
    }


    public string getTypeById3(string sid)
    {

        return fun.getById(sid, "id", "user_producttype3", "type");
       


    }
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null)
        {
            sql = "delete from  user_product where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString(),"picture","smallpicture");
        }
        else
        {
            fun.AJAXalert(this,"请至少选择一项!");
        }


        //fun.bind(rpProduct, "select * from user_product  order by join_date desc", 6, Label1, Label2, start, prev, next, max);
        
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql = "select * from user_product  where 1=1";
   
        int class3Id = int.Parse(DropDownList3.SelectedValue);
        if (class3Id != 0)
        {

            sql += " and typeid=" + class3Id;
        }
    
       
        sql += " and name like '%" + TextBox1.Text + "%'";
        sql += " order by paixu asc, join_date desc";
        Session["sql"] = sql;
        fun.BindPage(sql, AspNetPager1, rpProduct, 6);
        AspNetPager1.CurrentPageIndex = 1;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage(Session["sql"].ToString(), AspNetPager1, rpProduct, 6);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem r in rpProduct.Items)
        {
            TextBox t = r.FindControl("txtPaiXu") as TextBox;
            if (!fun.CheckStr(t.Text) || !fun.IsMatch(t.Text))
            {
                fun.AJAXalert(this, "排序只能用数字");
                return;
            }
            int paixu = int.Parse(t.Text);
            Label cb = r.FindControl("lblId") as Label;
            int id = int.Parse(cb.Text);
            string sql = "update user_product set paixu=" + paixu + " where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());


        }
    }
}
