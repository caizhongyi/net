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
public partial class culture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fun.quanxian("culture");
            fun.bind(DropDownList2, "select * from sjtype", "sjname", "id");
           
            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {
                int id = int.Parse(sid);
                string sql = "select * from culture where id="+id;
                DataRow dr = fun.GetDataTable(sql).Rows[0];
                if (dr != null)
                {
                    SiteName.Value = dr["title"].ToString();
                    FCKeditor1.Value = dr["content"].ToString();
                    Image1.ImageUrl = dr["picture"].ToString();
                    DropDownList1.SelectedValue = dr["typeid"].ToString();
                    DropDownList2.SelectedValue = dr["sjid"].ToString();
                    TextBox3.Text = dr["zhuozhe"].ToString();
                    TextBox4.Text = dr["hot"].ToString();
                    TextBox5.Text = dr["laiyuan"].ToString();
                }

            }
            string sjid = DropDownList2.SelectedValue;
            fun.bind(DropDownList1, "select * from culturetype where sjid=" + sjid, "type", "id");
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
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
      
    }

    private void add()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        if (!fun.CheckStr(name) || !fun.CheckStr(url))
        {

            fun.AJAXalert(this, "名称或内容不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            string pic = "";
           
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile("", file1, out pic, out error);
            }

            string sql = string.Format("insert into culture ([content],picture,title,typeid,zhuozhe,laiyuan,hot,change_date,sjid) values('{0}','{1}','{2}',{3},'{4}','{5}',{6},'{7}',{8})", url,pic,name,DropDownList1.SelectedValue,TextBox3.Text,TextBox5.Text,TextBox4.Text,DateTime.Now,DropDownList2.SelectedValue);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }

    private void up()
    {
        string name = SiteName.Value;
        string url = FCKeditor1.Value;
        if (!fun.CheckStr(name) || !fun.CheckStr(url))
        {

            fun.AJAXalert(this, "名称或内容不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            url = fun.GetSafeStr(url);
            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = fun.GetDataTable("select * from culture where id=" + id).Rows[0];

            string pic = "";
          
            string error = "";
            if (file1.Value != "")
            {
                fun.upFile(Server.MapPath(dr["picture"].ToString()), file1, out pic, out error);
            }
            else
            {
                pic = dr["picture"].ToString();
            }


            string sql = string.Format("update culture set content='{0}',picture='{1}',title='{2}',typeid={3},zhuozhe='{5}',laiyuan='{6}',hot='{7}',change_date='{8}',sjid={9} where id={4}", url, pic, name, int.Parse(DropDownList1.SelectedValue), id, TextBox3.Text, TextBox5.Text, TextBox4.Text, DateTime.Now,DropDownList2.SelectedValue);
            fun.DoSql(this, sql, Request.Url.ToString());
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string  sjid = DropDownList2.SelectedValue;
        fun.bind(DropDownList1, "select * from culturetype where sjid="+sjid, "type", "id");
    }
}
