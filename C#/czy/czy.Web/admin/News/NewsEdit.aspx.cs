using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Menu_MenuEdit : System.Web.UI.Page
{
    Models.NewsInfo n = new Models.NewsInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
            int id = 0;
        
            if (Request.QueryString["id"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                n.n_id = id;
                if (!IsPostBack)
                {
                    LoadData();
                }
            } 
            if (!IsPostBack)
            {
                DropListbind();
                GetValue();
            }
    }
    private void DropListbind()
    {
        DropType.DataSource = BBL.NewsType.Select();
        DropType.DataTextField = "nt_name";
        DropType.DataValueField = "nt_id";
        DropType.DataBind();
        DropType.Items.Insert(0, new ListItem("请选择", "0"));

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (n.n_id == 0)
        {
            
            SetValue();
            if (BBL.NewsInfo.Insert(n)>0)
            { czy.MyClass.Web.JavaScript.Alert("添加成功!"); }
            else
            { czy.MyClass.Web.JavaScript.Alert("添加失败!"); }
        }
        else
        {
            BtnAdd.Text = "修改";
            SetValue();
            if (BBL.NewsInfo.Update(n.n_id, n) > 0)
            {
                czy.MyClass.Web.JavaScript.Alert("修改成功!");
            }
            else
            {
                czy.MyClass.Web.JavaScript.Alert("修改失败!");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("News.aspx");
    }

    private void LoadData()
    {
        n = BBL.NewsInfo.SelectList(n.n_id)[0];
       // GetValue();
        if (n.n_id == 0)
        {
            BtnAdd.Text = "添加";
        }
        else
        {
            BtnAdd.Text = "修改";
        }
    }
    
    private void SetValue()
    {
        n.n_content = FCKeditor1.Value;
        n.n_createDate = DateTime.Now;
        n.n_endDate = DateTime.Now;
        n.n_newsTypeId = 1;
        n.n_startDate = DateTime.Now;
        n.n_newsTypeId = Convert .ToInt32( DropType.SelectedValue);
        n.n_title = TxtName.Text;
       // m.m = TxtURL.Text.ToString();
    }
    private void GetValue()
    {
        FCKeditor1.Value = n.n_content;
        DropType.SetIndexByValue(n.n_newsTypeId.ToString ());
        TxtName.Text = n.n_title;
    }
}