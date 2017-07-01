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

public partial class admin_user_productlist1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
    
            fun.bind(DropDownList2, "select * from user_producttype2", "type", "id", "所有一级类别");
          
            DropDownList3.Items.Add(new ListItem("所有二级类别", "0"));

            fun.BindPage("select * from user_product  order by paixu asc, join_date desc", AspNetPager1, rpProduct, 6);
            Session["sql"] = "select * from user_product  order by paixu asc, join_date desc";

        }

 }
 public string getTypeById2(string sid)
 {

     string id = fun.getById(sid, "id", "user_producttype3", "sjid");
     return fun.getById(id, "id", "user_producttype2", "type");


 }

 public string getTypeById3(string sid)
 {

     return fun.getById(sid, "id", "user_producttype3", "type");



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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(DropDownList2.SelectedValue);
        DropDownList3.Items.Clear();
        fun.bind(DropDownList3, "select * from user_productType3 where sjid=" + id, "type", "id", "所有二级类别");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql = "select * from user_product  where 1=1";
        
        int class2Id = int.Parse(DropDownList2.SelectedValue);
        int class3Id = int.Parse(DropDownList3.SelectedValue);
        if (class3Id != 0)
        {

            sql += " and typeid=" + class3Id;
        }

        if (DropDownList2.SelectedValue != "0" && DropDownList3.SelectedValue == "0")
        {
            DataTable dt = fun.GetDataTable("select * from user_producttype3 where sjid=" + class2Id);
            string str = "0";
            foreach (DataRow dr in dt.Rows)
            {
                str += "," + dr["id"].ToString();

            }


            sql += " and typeid in (" + str + ")";
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null)
        {
            sql = "delete from  user_product where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this, "请至少选择一项!");
        }
    }
}
