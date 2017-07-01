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
using BBL.Inface;

public partial class ADManger_AdIssue : System.Web.UI.Page
{
    IAdvIssueInfo advInfo = BBL.BBLFactory.GetAdvIssueInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        if (!IsPostBack)
        {
            GvDataBind();
        }
    }
    protected void GvDataBind()
    {
        DataSet ds = advInfo.SelectAdvIssue();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "Adv_Id" };
        GridView1.DataBind();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView1.EditIndex = -1;

        //GvDataBind();
    }
}
