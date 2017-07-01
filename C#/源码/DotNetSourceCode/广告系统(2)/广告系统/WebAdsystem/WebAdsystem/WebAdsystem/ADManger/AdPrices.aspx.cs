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
using BBL;
using DAL.Model;

public partial class ADManger_AdPrices : System.Web.UI.Page
{
    IAdvUnitPriceInfo UnitPrice = BBLFactory.GetAdvUnitPriceInfo();
    AdvUnitprice UnitPriceMode = new AdvUnitprice();
    protected void GvDataBind()
    {
        DataSet ds = UnitPrice.SelectAdvUnitPriceInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "Rk_Id" };
        GridView1.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvDataBind();
        } 
        Panel2.Visible = false;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = DrAdv_type.SelectedItem.Text ;
        Label2.Text = unitprice.Text;
        Label3.Text = TextBox1.Text;
        Label4.Text = Calendar1.SelectedDate.ToString ();
        Panel2.Visible = true;
        Panel1.Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        int id = 2;
        int id2 = 3;
        UnitPriceMode.Unitprice = Convert.ToInt32(Label2.Text);
        UnitPriceMode.Up_remark = Label3.Text;
        UnitPriceMode.Moditf_time = Convert.ToDateTime(Calendar1.SelectedDate);
        bool i = UnitPrice.InsertAdvUnitPriceInfo(UnitPriceMode,id ,id2);
        if (i)
        {
            Response.Write("<script>alert('增加成功');</script>");
        }
        else
        {
            Response.Write("<script>alert('增加失败');</script>");
        }
        GvDataBind(); 
        Panel2.Visible = false;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        int id2 =Convert.ToInt32( ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim());
        UnitPriceMode.Unitprice=Convert.ToInt32 ( ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim());
        UnitPriceMode.Up_remark= ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        UnitPriceMode.Moditf_time=Convert.ToDateTime( ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim());
        UnitPrice.UpdateAdvUnitPriceInfo(UnitPriceMode, ID, id2);
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;

        GvDataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        UnitPrice.DelAdvUnitPriceInfobyID(ID);
        GvDataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditRowStyle.Width = 700;
        GridView1.EditIndex = e.NewEditIndex;

        GvDataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;

    }
}
