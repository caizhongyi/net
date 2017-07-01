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
using DAL.Model;
using BBL;

public partial class UserManger_UserManger : System.Web.UI.Page
{
    IUserInfo userInfo = BBLFactory.GetUserInfo();
    UserInfo userInfoMode = new UserInfo();
    protected void GvDataBind()
    {

        DataSet ds = userInfo.SelectUserInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "user_id" };
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
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditRowStyle.Width = 700;
        GridView1.EditIndex = e.NewEditIndex;
        GvDataBind();
       
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        userInfo.DelUserInfobyID(userID);
        GvDataBind();
  
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        Panel1.Visible = false;

        Label1.Text = user_nickname.Text;
        Label3.Text = LoginName.Text;
        //Label4.Text = user_pwdChack.Text;
        Label5.Text =user_sex.SelectedItem.Text ;
        Label6.Text = DrUser_type.SelectedItem.Text ;
        Label19.Text = DrUser_type.SelectedValue;
        Label7.Text = Calendar1.SelectedDate.ToString();
        Label8.Text = user_postalcode.Text;
        Label9.Text = user_address.Text;
        Label10.Text = user_tel.Text;
        Label11.Text = user_tel1.Text;
        Label12.Text = user_fax.Text;
        Label13.Text = user_email.Text;
        Label14.Text =user_email1.Text;
        Label15.Text=user_qq.Text ;
        Label16.Text=user_qq1.Text ;
        Label17.Text=wb_connect.Text ;
        Label18.Text=user_remark.Text ;
   

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        userInfoMode.User_id = Label3.Text;
        userInfoMode.User_nickname = Label1.Text ;
        userInfoMode.User_name = Label2.Text;
        userInfoMode.User_pwd = user_pwd.Text;
        userInfoMode.User_sex = Label5.Text;
        //userInfoMode.User_TypeID = Label19.Text;
        userInfoMode.User_birthday = Label7.Text; ;
        userInfoMode .User_postalcode= Label8.Text ;
        userInfoMode .User_address= Label9.Text ;
        userInfoMode .User_tel1= Label10.Text ;
        userInfoMode.User_tel2=Label11.Text ;
        userInfoMode .User_fax= Label12.Text ;
        userInfoMode .User_email1= Label13.Text ;
        userInfoMode.User_email2=Label14.Text ;
        userInfoMode .User_qq1 = Label15.Text;
        userInfoMode.User_qq2 = Label16.Text ;
        userInfoMode.Wb_connect=Label17.Text ;
        userInfoMode .User_remark= Label18.Text;
        userInfoMode.User_time =Convert.ToDateTime(DateTime.Now);

        bool i = userInfo.InsertUserInfo(userInfoMode, Convert.ToInt32(Label19.Text));
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
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        userInfoMode.User_id =GridView1.DataKeys[e.RowIndex].Value.ToString();      
        userInfoMode.User_nickname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_pwd = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_sex = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
        int typeid =Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text);
        userInfoMode.User_birthday = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_postalcode = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[9].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_address = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[10].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_tel1 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[11].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_tel2 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[12].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_fax = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[13].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_email1 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[14].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_email2 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[15].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_qq1 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[16].Controls[0])).Text.ToString().Trim();
        userInfoMode.User_qq2 = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[17].Controls[0])).Text.ToString().Trim();
        userInfoMode.Wb_connect = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[18].Controls[0])).Text.ToString().Trim();
        //数据只读....
        userInfoMode.User_time = Convert.ToDateTime(DateTime.Now);

        userInfoMode.User_remark = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[8].Controls[0])).Text.ToString().Trim();
        userInfo.UpdateUserInfo(userInfoMode,typeid);
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GvDataBind();
    }
}
