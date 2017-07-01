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
using System.Threading;

public partial class admin_HonorAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("honor");
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from honortype","type","id");
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from honor where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    DropDownList1.SelectedValue = dr["typeid"].ToString();
                    SiteName.Value = dr["name"].ToString();
                    FCKeditor1.Value= dr["honor_content"].ToString();
                    Image1.ImageUrl = dr["pic"].ToString();
                    Image2.ImageUrl = dr["bpic"].ToString();
                    TextBox3.Text = dr["zhuozhe"].ToString();
                    TextBox4.Text = dr["hot"].ToString();
                    TextBox5.Text = dr["laiyuan"].ToString();
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            up();
        }
        else
        {
            add();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            int id = int.Parse(sid);
            DataRow dr = fun.GetDataTable("select * from honor where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["pic"].ToString()));
            string sql = "update honor set pic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            Image1.ImageUrl = "";
        }
    }

    private void add()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        if (!fun.CheckStr(name) )
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('名称或内容不能为空')", true);
         
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            string pic = "";
            string bpic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile("", file1, out pic, out error);
            }
            Thread.Sleep(1000);
            if (file2.Value != "")
            {
                fun.upFile("", file2, out bpic, out error);
            }
            string sql = string.Format("insert into honor (name,honor_content,pic,addtime,bpic,typeid,zhuozhe,laiyuan,hot,change_date) values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},'{9}')", name, url, pic, DateTime.Now, bpic, int.Parse(DropDownList1.SelectedValue),TextBox3.Text,TextBox5.Text,TextBox4.Text,DateTime.Now);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        if (!fun.CheckStr(name) )
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('名称或内容不能为空')", true);
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = fun.GetDataTable("select * from honor where id=" + id).Rows[0];

            string pic = "";
            string bpic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile(Server.MapPath(dr["pic"].ToString()), file1, out pic, out error);
            }
            else
            {
                pic = dr["pic"].ToString();
            }
            Thread.Sleep(1000);
            if (file2.Value != "")
            {
                fun.upFile(Server.MapPath(dr["bpic"].ToString()), file2, out bpic, out error);
            }
            else
            {
                bpic = dr["bpic"].ToString();
            }

            string sql = string.Format("update honor set name='{0}',honor_content='{1}',pic='{2}', bpic='{3}',typeid={4},zhuozhe='{6}',laiyuan='{7}',hot='{8}',change_date='{9}' where id={5}", name, url, pic, bpic, int.Parse(DropDownList1.SelectedValue), id, TextBox3.Text, TextBox5.Text, TextBox4.Text, DateTime.Now);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string sid = Request.QueryString["id"];
        if (fun.CheckStr(sid) && fun.IsMatch(sid))
        {
            int id = int.Parse(sid);
            DataRow dr = fun.GetDataTable("select * from honor where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["bpic"].ToString()));
            string sql = "update honor set bpic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            Image2.ImageUrl = "";
        }
    }
}
