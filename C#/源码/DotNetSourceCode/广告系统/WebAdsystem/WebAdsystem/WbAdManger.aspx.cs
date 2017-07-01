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

public partial class WbAdManger : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AdTypeBind();

        }
     
       
    }
    void AdTypeBind()
    {
        BBL.IAdInfo iadinfo = BBLFactory.GetAdInfo();
        GridView1.DataSource= iadinfo.SelectAdInfo();
        GridView1.DataKeyNames = new string[] { "Ad_ID" };
        GridView1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        IAdInfo iadinfo = BBL.BBLFactory.GetAdInfo();
        AdInfoInfo adinfoinfo = new AdInfoInfo();
        adinfoinfo.Ad_ID =Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        adinfoinfo.Ad_Name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        adinfoinfo.Ad_Type_Name = 1;
        adinfoinfo.Ad_Url = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        adinfoinfo.Ad_Operation = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        adinfoinfo.Ad_ClickNum =Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim());
        adinfoinfo.Ad_time = DateTime.Now.ToString();
        adinfoinfo.Ad_Remark = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].Controls[0])).Text.ToString().Trim();
      
        iadinfo.UpdateAdInfo(adinfoinfo);
        GridView1.EditIndex = -1;
        AdTypeBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        IAdInfo iadinfo= BBL.BBLFactory.GetAdInfo();
        int id =Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        iadinfo.DelAdInfo(id);
        AdTypeBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
      
        AdTypeBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        AdTypeBind();
    }
}
