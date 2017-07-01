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
using Model;
public partial class lxs : System.Web.UI.Page
{
   
    string sql = "";
    int id = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.bind(Repeater1, "select * from news_class2");
        id = fun.getQueryInt("id");
    


            sql = "select * from ygfc where id=" + id;
            DataTable dt = fun.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
        


                fun.bind(Repeater2, dt);
            }

        


      












    }





    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
            News n = new News();
            fun.getModel(n, " where id>" + id + " and class1_id=" + fun.getById(id.ToString(), "id", "news", "class1_id") + " order by id asc");
            if (n.Id > 0)
            {
                Response.Redirect("newdetail.aspx?id=" + n.Id);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('已经是第一页')", true);
            }
        
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
       
            News n = new News();
            fun.getModel(n, " where id<" + id + " and class1_id=" + fun.getById(id.ToString(), "id", "news", "class1_id") + " order by id desc");
            if (n.Id > 0)
            {
                Response.Redirect("newdetail.aspx?id=" + n.Id);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('已经是最后一页')", true);
            }
        
    }
}
