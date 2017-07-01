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

public partial class admin_honorTypeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("honortype");
        if (!IsPostBack)
        {
           
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from honorType where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    TextBox1.Text = dr["type"].ToString();
                    TextBox2.Text = dr["xuezhi"].ToString();
                    TextBox3.Text = dr["xuefei"].ToString();
                    TextBox4.Text = dr["shuoming"].ToString();
                    Image1.ImageUrl = dr["pic"].ToString();
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
            DataRow dr = fun.GetDataTable("select * from honorType where id=" + id).Rows[0];
            fun.delFile(Server.MapPath(dr["pic"].ToString()));
            string sql = "update honorType set pic='' where id=" + id;
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            Image1.ImageUrl = "";
        }
    }

    private void add()
    {
        string name = TextBox1.Text;

        if (!fun.CheckStr(name))
        {

            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);

            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile("", file1, out pic, out error);
            }

            string sql = string.Format("insert into honorType (pic,type,xuezhi,xuefei,shuoming) values('{0}','{1}','{2}','{3}','{4}')", pic, name,TextBox2.Text,TextBox3.Text,TextBox4.Text);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name =TextBox1.Text;
        if (!fun.CheckStr(name))
        {

            fun.AJAXalert(this, "名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);

            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = fun.GetDataTable("select * from honorType where id=" + id).Rows[0];

            string pic = "";
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile(Server.MapPath(dr["pic"].ToString()), file1, out pic, out error);
            }
            else
            {
                pic = dr["pic"].ToString();
            }

            string sql = string.Format("update honorType set pic='{0}',type='{1}',xuezhi='{3}',xuefei='{4}',shuoming='{5}' where id={2}", pic, name, id, TextBox2.Text, TextBox3.Text, TextBox4.Text);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }
}
