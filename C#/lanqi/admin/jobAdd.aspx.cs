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

public partial class admin_jobAdd : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (fun.CheckStr(Request.QueryString["id"]) && fun.IsMatch(Request.QueryString["id"]))
            {
                int id = int.Parse(Request.QueryString["id"]);
                string sql = "select * from job where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    TextBox_address.Text = dr["address"].ToString();
                    TextBox_deal.Text = dr["gongzi"].ToString();
                    TextBox_inviter.Text = dr["inviter"].ToString();
                    TextBox_qixian.Text = dr["qixian"].ToString();
                    TextBox_shu.Text = dr["renshu"].ToString();
                    FCKeditor1.Value = dr["yaoqiu"].ToString();
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = TextBox_inviter.Text;
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "招聘职位不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            string num =fun.GetSafeStr( TextBox_shu.Text);
            string address = fun.GetSafeStr(TextBox_address.Text);
            string gongzi = fun.GetSafeStr(TextBox_deal.Text);
            string qixian = fun.GetSafeStr(TextBox_qixian.Text);
            string xiuyao = fun.GetSafeStr(FCKeditor1.Value);
            string sql = "";
            if (fun.CheckStr(Request.QueryString["id"]) && fun.IsMatch(Request.QueryString["id"]))
            {
                sql = string.Format("update job set inviter='{0}',renshu='{1}',address='{2}',gongzi='{3}',qixian='{4}',yaoqiu='{5}' where id={6}",name,num,address,gongzi,qixian,xiuyao,int.Parse( Request.QueryString["id"]));
            }
            else
            {
               sql = string.Format("insert into  job (inviter,renshu,address,gongzi,qixian,yaoqiu,join_date) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", name, num, address, gongzi, qixian, xiuyao, DateTime.Now);
            }
            fun.DoSql(this,sql,Request.Url.ToString());
        }
    }
}
