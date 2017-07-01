﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ADManger_AdManger : System.Web.UI.Page
{
    private static int delid;
    BBL.Inface.IAdvInfo advInfo = BBL.BBLFactory.GetAdvInfo();
    DAL.Model.AdvInfo AdvInfoMode = new DAL.Model.AdvInfo();
    protected void GvDataBind()
    {
        DataSet ds = advInfo.SelectAdvInfo();
        GridView1.DataSource = ds;
        GridView1.DataKeyNames = new string[] { "Adv_Id" };
        GridView1.DataBind();
    }
    public string GetdonwLoadPath(string IP, string type, string filename)
    {
        string picturespath = @"\Adpictures\";
        string path = "";
        switch (type)
        {
            case "背景广告": path = IP + picturespath + @"背景广告\" + filename;
                break;
            case "右侧广告1": path = IP + picturespath + @"右侧广告1\" + filename;
                break;
            case "IE首页广告": path = IP + picturespath + @"IE首页广告\" + filename;
                break;
            case "IE置顶广告": path = IP + picturespath + @"IE置顶广告\" + filename;
                break;
            case "右侧广告2": path = IP + picturespath + @"右侧广告2\" + filename;
                break;
            case "右侧广告3": path = IP + picturespath + @"右侧广告3\" + filename;
                break;
            case "右侧广告4": path = IP + picturespath + @"右侧广告4\" + filename;
                break;
            case "右侧广告5": path = IP + picturespath + @"右侧广告5\" + filename;
                break;
        }
        return path;
    }
    protected string fileName;
    protected string filepath = @"~/Adpictures/";

    public void upload(string rootpath, FileUpload file)
    {
        string path = Server.MapPath(rootpath);
        string ext = System.IO.Path.GetExtension(file.PostedFile.FileName).ToLower();
        DateTime dt = DateTime.Now;
        if (ext == ".jpg" || ext == ".bmp" || ext == ".gif")
        {
            fileName = AdvInfoMode.Adv_name;
            fileName += ext;
            file.SaveAs(path + @"/" + fileName);
        }
        else
        {
            Response.Write("<script>alert('图片格式不正确')</script>");
        }
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        if (!IsPostBack)
        {
            GvDataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        AdvInfoMode.Adv_name = Label1.Text = adv_name.Text;
        Label3.Text = DrAdv_master.SelectedItem.Text;
        Label2.Text = DrAdv_master.SelectedValue;
        Label4.Text = DrUser.SelectedItem.Text;
        Label8.Text = DrUser.SelectedValue;
        Label5.Text = adv_discount.Text;
        Label6.Text = DrPay_state.Text;
        Label7.Text = adv_content.Text;
        AdvInfoMode.User_id = 1;
        AdvInfoMode.Adv_time = DateTime.Now;
        AdvInfoMode.Adv_url = filepath + AdvInfoMode.Adv_name + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();

        try
        {
            upload(filepath, FileUpload1);

            bool i = advInfo.InsertAdvInfo(AdvInfoMode, Convert.ToInt32(Label2.Text), Convert.ToInt32(Label8.Text));
        }
        catch (Exception ex)
        {
            Response.Write("<script javascript:language>alert('" + ex.Message + "')</script>");
        }
        Image1.ImageUrl = AdvInfoMode.Adv_url;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Write("<script javascript:language>alert('增加成功');</script>");
    }
    
    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditRowStyle.Width = 700;
        GridView1.EditIndex = e.NewEditIndex;
        GvDataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Panel4.Visible = true;
        delid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        Label9.Text = GridView1.Rows[e.RowIndex].Cells[1].Text.ToString().Trim();
        Image2.ImageUrl = ((Image)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).ImageUrl;
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        AdvInfoMode.Adv_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        AdvInfoMode.Adv_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        AdvInfoMode.Adv_content = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        AdvInfoMode.Adv_url = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        int masterid = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim());
        AdvInfoMode.Adv_master = masterid;
        int userid = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim());
        AdvInfoMode.User_id = userid;
        AdvInfoMode.Adv_operation = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim());
        AdvInfoMode.Adv_clicknumber = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].Controls[0])).Text.ToString().Trim());
        AdvInfoMode.Adv_time = Convert.ToDateTime(DateTime.Now);
        AdvInfoMode.Adv_discount = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[9].Controls[0])).Text.ToString().Trim());
        AdvInfoMode.Adv_pay_state = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[10].Controls[0])).Text.ToString().Trim();
        advInfo.UpdateAdvInfo(AdvInfoMode, masterid, userid);
        GridView1.EditIndex = -1;
        GvDataBind();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        advInfo.DelAdvInfobyID(delid);
        GvDataBind();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        Panel4.Visible = false;
    }
}
