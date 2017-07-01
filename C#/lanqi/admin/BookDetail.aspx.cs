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

public partial class admin_BookDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("guestbook");
        string sid=Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            DataTable dt = fun.GetDataTable("select * from guestbook where id=" + int.Parse(sid));
            if (dt.Rows.Count > 0)
            {
                DataRow dr=dt.Rows[0];
                lblTitle.Text = dr["title"].ToString();
                FCKeditor1.Value=dr["content"].ToString();
                lblAddress.Text = dr["address"].ToString();
                lblTime.Text = dr["addtime"].ToString();
                lblName.Text = dr["username"].ToString();
                lblSex.Text = dr["sex"].ToString();
                lblTel.Text = dr["tel"].ToString();
                lblEmail.Text = dr["email"].ToString();
                Label2.Text = dr["ordername"].ToString();
            }
;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Book.aspx");
    }
}
