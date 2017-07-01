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
using DAL.Model;
using BBL.Inface;
using BBL;

public partial class AeraManger_AeraManger : System.Web.UI.Page
{
    IAreaInfo iareainfo = BBLFactory.GetAreaInfo();
    AreaList arealist = new AreaList();
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
        DataSet ds = iareainfo.SelectAreaInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "Area_Id" };
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GvDataBind(); 
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        iareainfo.DelAreaInfobyID(userID);
        GvDataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        arealist.Area_id= Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        int pid=Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text);
        arealist.Area_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        arealist.Area_remark= ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        iareainfo.UpdateAreaInfo(arealist,pid);
        GridView1.EditIndex = -1;
        GvDataBind(); 
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditRowStyle.Width = 700;
        GridView1.EditIndex = e.NewEditIndex;
        GvDataBind(); 
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        Panel1.Visible = false;
        Label1.Text = area_name.Text;
        Label2.Text = DrProvince.SelectedValue.ToString();
        Label3.Text =area_remark.Text;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        arealist.Area_name = Label1.Text;
        //省份绑定
        int pid=2;
        arealist.Area_remark = Label3.Text;
        bool i = iareainfo.InsertAreaInfo(arealist,pid);
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
}
