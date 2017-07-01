using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Menu_MenuEdit : System.Web.UI.Page
{
    Models.Menu m = new Models.Menu();
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = 0;
        
            if (Request.QueryString["id"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                m.m_id = id;
                if (!IsPostBack)
                {
                    LoadData();
                }
            }
          
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (m.m_id == 0)
        {
            
            SetValue();
            BBL.Menu.Insert(m);
        }
        else
        {
            BtnAdd.Text = "修改";
            SetValue();
            BBL.Menu.Update(m.m_id,m);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private void LoadData()
    {
        m = BBL.Menu.SelectList(m.m_id)[0];
        GetValue();
        if (m.m_id == 0)
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
        m.m_parentId = TxtParentId.Text.ToString();
        m.m_name = TxtName.Text.ToString();
       // m.m = TxtURL.Text.ToString();
    }
    private void GetValue()
    {
        TxtParentId.Text  = m.m_parentId.ToString();
        TxtName.Text = TxtName.ToString();
    }
}