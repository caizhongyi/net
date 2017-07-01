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
using BBL;
using DAL.DataBaseModel;

public partial class WbManger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvDataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
  {
    //    GridView1.EditIndex = e.NewEditIndex;

    //    ((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[1].Controls[0]));
    //    //((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[2].Controls[0])).Text.ToString().Trim();
    //    //((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[3].Controls[0])).Text.ToString().Trim();
    //    //((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[4].Controls[0])).Text.ToString().Trim();
    //    //((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[5].Controls[0])).Text.ToString().Trim();
    //    //((TextBox)(GridView1.Rows[e.NewEditIndex].Cells[6].Controls[0])).Text.ToString().Trim();
    //    GvDataBind();
    }
    protected void GvDataBind()
    {
        IWbInfo IWb_Info = BBLFactory.GetWbInfo();
        DataSet ds = IWb_Info.GetWbInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "WB_ID" };
        GridView1.DataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex= -1;
        GvDataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        IWbInfo IWb_Info = BBLFactory.GetWbInfo();
        WBInfoInfo wbii = new WBInfoInfo();
        wbii.WB_ID =Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        wbii.WB_Name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        wbii.WB_IP = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        wbii.WB_Address = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        wbii.WB_Phone = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        wbii.WB_Register_Time = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
        wbii.WB_Remark = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
        IWb_Info.UpdataWbInfo(wbii);
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IWbInfo IWb_Info = BBLFactory.GetWbInfo();
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        IWb_Info.DelWbInfo(id);
        GvDataBind();
    }
}
