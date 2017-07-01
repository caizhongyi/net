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

public partial class ADManger_AdType : System.Web.UI.Page
{

    BBL.Inface.IAdvTypeInfo AdvType = BBL.BBLFactory.GetAdvTypeInfo();
    DAL.Model.AdvType AdvTypeMode = new DAL.Model.AdvType();
    protected void Page_Load(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        if (!IsPostBack)
        {
            GvDataBind();
        }
        
    }
    protected void GvDataBind()
    {

        DataSet ds = AdvType.SelectAdvTypeInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "adv_type_id" };
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        AdvTypeMode.Adv_type_name = Label1.Text;
        AdvTypeMode.Adv_type_remark = Label2.Text;
        bool i = AdvType.InsertAdvTypeInfo(AdvTypeMode);
        if (i)
        {
            Response.Write("<script>alert('增加成功');</script>");
        }
        else
        {
            Response.Write("<script>alert('增加失败');</script>");
        }
        GvDataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        Label1.Text = adv_type_name.Text;
        Label2.Text = adv_type_remark.Text;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        AdvTypeMode.Adv_type_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        AdvTypeMode.Adv_type_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        AdvTypeMode.Adv_type_remark = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();

        AdvType.UpdateAdvTypeInfo(AdvTypeMode);
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;

        GvDataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditRowStyle.Width = 700;
        GridView1.EditIndex = e.NewEditIndex;

        GvDataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        AdvType.DelAdvTypeInfobyID(ID);
        GvDataBind();
    }
}
