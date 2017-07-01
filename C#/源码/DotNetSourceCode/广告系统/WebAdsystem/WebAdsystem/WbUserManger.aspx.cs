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

public partial class WbUserManger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvDataBind();
        }
       
    }

    BBL.IUserInfo UserInfoOperation = BBL.BBLFactory.GetUserInfo();
    protected void GvDataBind()
    {

        DataSet ds = UserInfoOperation.SelectUserInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "User_ID" };
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int userID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        string loginName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        string userName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        //string loginPwd= ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        string userRight= ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        string userRemark = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
        UserInfoOperation.UpdateUserInfo(userName, loginName,Convert .ToInt32( userRight), userRemark,userID);
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex= -1;
       
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
        int userID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        UserInfoOperation.DelUserInfo(userID);
        GvDataBind();
    }
}
