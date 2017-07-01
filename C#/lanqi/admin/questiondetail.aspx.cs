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

public partial class admin_questiondetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("question");
        if (!IsPostBack)
        {
            fun.bind(Select1, "select * from questionstate", "state", "id");
            fun.bind(superCatId,"select * from questiontype","type","id");
            string id = Request.QueryString["id"];
            if (fun.CheckStr(id) && fun.IsMatch(id))
            {
                DataTable dt = fun.GetDataTable("select * from question where id=" + id);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    siteName.Value = dr["name"].ToString();
                    siteDomain.Value = dr["ip"].ToString();
                    dayVisitAmount.Value = dr["fanwennum"].ToString();
                    superCatId.Value = dr["type"].ToString();
                    userName.InnerHtml =fun.getById( dr["userid"].ToString(),"id","usercenter","username");
                    siteDesc.Value = dr["question_content"].ToString();
                    siteRecordDetail.Value = dr["question_answer"].ToString();
                 
                    Select1.Value = dr["state"].ToString();
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (fun.CheckStr(Request.QueryString["id"]) && fun.IsMatch(Request.QueryString["id"]))
        //{

        //    string sid = Select1.Value;
        //    string sql = "update question set question_answer='" + an + "' ,state=" + sid + " where id=" + Request.QueryString["id"];
        //    fun.DoSql(this, sql, "questionmanager.aspx");
        //}
    }
}
