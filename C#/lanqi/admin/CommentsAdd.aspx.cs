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

public partial class admin_NewAdd2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("news");
        string id = Request.QueryString["id"];
        if (!IsPostBack)
        {

           
           
        }
     
        if (fun.CheckStr(id)&&fun.IsMatch(id))
        {
            Label1.Text = "修改信息";
            int pid = int.Parse(id);
            if (!IsPostBack)
            {
                string sql = string.Format("select * from comments where c_id={0}", pid);
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {

                    FCKeditor1.Value = dt.Rows[0]["c_content"].ToString();
              
                }
            }
           
        }
        else
        {
            Label1.Text = "添加信息";
            if (!IsPostBack)
            {
            
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Label1.Text == "添加信息")
        {
            AddNew();
        }
        else if (Label1.Text == "修改信息")
        {
            UpdateNew();
        }
    }


    protected void AddNew()
    {
     
       
    }
    protected void UpdateNew()
    {
           
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
}
