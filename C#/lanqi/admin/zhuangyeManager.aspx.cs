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

public partial class admin_zhuangyeManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("zhuangye");
        if (!IsPostBack)
        {

            fun.BindPage("select * from zhuangye ", AspNetPager3, rpNewClass3, 6);

        }
    }

    protected void Button6_Click(object sender, EventArgs e)//删除３级分类
    {
        string Id_str = Request["ch3"], sql = "";
        if (Request["ch3"] != null && Id_str != "")
        {
            sql = "delete from  zhuangye where id in (" + Id_str + ")";
            DataTable dt = fun.GetDataTable("select * from  zhuangye where id in (" + Id_str + ")");
            string str = "";
            foreach (DataRow dr in dt.Rows)
            {
                str += "," + dr["name"].ToString();
            }
            fun.DoSqlAJAX("drop table "+str.Substring(1));
            fun.DoSql(this, sql, Request.Url.ToString());
            //fun.BindPage("select * from user_producttype3  ", AspNetPager3, rpNewClass3, 3);
            
        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！');");
        }
    }


   

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from zhuangye ", AspNetPager3, rpNewClass3, 6);
    }
}
