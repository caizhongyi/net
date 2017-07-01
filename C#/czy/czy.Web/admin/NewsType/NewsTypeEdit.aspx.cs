using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_NewsTypeEdit : System.Web.UI.Page
{
    Models.NewsType n = new Models.NewsType();
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = 0;
        
            if (Request.QueryString["id"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                n.nt_id = id;
               
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
        DropType.Items.Insert(0,new ListItem("请选择","0"));
  
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (n.nt_id == 0)
        {
            
            SetValue();
            if (BBL.NewsType.Insert(n) > 0) 
            { czy.MyClass.Web.JavaScript.Alert("添加成功!"); }
            else
            { czy.MyClass.Web.JavaScript.Alert("添加失败!"); }
        }
        else
        {
            BtnAdd.Text = "修改";
            SetValue();
            if (BBL.NewsType.Update(n.nt_id, n) > 0)
            { czy.MyClass.Web.JavaScript.Alert("修改成功!"); }
            else
            { czy.MyClass.Web.JavaScript.Alert("修改失败!"); }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewsType.aspx");
    }

    private void LoadData()
    {
        n = BBL.NewsType.SelectList(n.nt_id)[0];
     
        if (n.nt_id == 0)
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
        n.nt_name = TxtName.Text;
        n.nt_remark = TxtRemark.Text ;
        n.parentId =Convert .ToInt32( DropType.SelectedValue);
       // m.m = TxtURL.Text.ToString();
    }
    private void GetValue()
    {
        TxtRemark.Text = n.nt_remark;
        //DropType.SetIndexByValue(n.n_newsTypeId.ToString ());
        TxtName.Text = n.nt_name;
        DropType.SetIndexByValue(DropType.SelectedValue);
    }
}