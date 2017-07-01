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
            fun.bind(DropDownList2, "select * from news_class2", "type", "id");
        
            TextBox1.Text = "20";

            TextBox4.Text = "99";
            if (!fun.CheckStr(id))
            {
                id = "0";
            }
            int sjid = fun.getIntById(id,"id","news","class1_id");
            fun.bind(slType, "select * from news_class where sjid=" + sjid, "class1", "id","请选择");
        }
     
        if (fun.CheckStr(id)&&fun.IsMatch(id))
        {
            Label1.Text = "修改信息";
            int pid = int.Parse(id);
            if (!IsPostBack)
            {
                string sql = string.Format("select * from news where id={0}", pid);
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {

                    txtName.Text = dt.Rows[0]["title"].ToString();
                    fujian.InnerHtml = dt.Rows[0]["pic"].ToString();
                    TextBox3.Text = dt.Rows[0]["zhuozhe"].ToString();
                    TextBox4.Text = dt.Rows[0]["hot"].ToString();
                    TextBox5.Text = dt.Rows[0]["laiyuan"].ToString();

                    DropDownList2.SelectedValue = dt.Rows[0]["class1_id"].ToString();

                    slType.Value = dt.Rows[0]["sjid"].ToString();
                    FCKeditor1.Value = dt.Rows[0]["content"].ToString();
                    TextBox1.Text = dt.Rows[0]["paixu"].ToString();
                    if (dt.Rows[0]["istj"].ToString() == "是")
                    {
                        rdy.Checked = true;
                    }
                    else
                    {
                        rdn.Checked = true;
                    }
                }
            }
           
        }
        else
        {
            Label1.Text = "添加信息";
            if (!IsPostBack)
            {
                rdn.Checked = true;
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
        string name = txtName.Text;
        string paixu=TextBox1.Text;
        
        if (!fun.CheckStr(name))
        {
            Response.Write("<script>alert('标题不能为空');</script>");
        }
        else if (!fun.CheckStr(paixu) || !fun.IsMatch(paixu))
        {
            Response.Write("<script>alert('排序只能用数字');</script>");
        }
        else
        {
            int typeid = int.Parse(DropDownList2.SelectedValue);
            if ((slType.Value == "0" && typeid == 23) || (slType.Value == "0" && typeid == 20))
            {
                fun.AJAXalert(this, "请选择二级分类");
            }
            else
            {
                string pic = "";
                string error = "";
                if (file1.Value != "")
                {
                    fun.upFile("", file1, out pic, out error);
                }
                string remark =fun.GetSafeStr( FCKeditor1.Value);
                name = fun.GetSafeStr(name);
                string sql = string.Format("insert into news (title,class1_id,[content],istj,paixu,pic,zhuozhe,laiyuan, hot,change_date,sjid) values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}',{10})", name, typeid, remark,Request.Form["rdtj"],int.Parse(TextBox1.Text),pic,TextBox3.Text,TextBox5.Text,int.Parse( TextBox4.Text),DateTime.Now,slType.Value);
                fun.DoSql(this, sql, Request.Url.ToString());
            }
        }
    }
    protected void UpdateNew()
    {
        string name = txtName.Text;
        if (!fun.CheckStr(name))
        {
            Response.Write("<script>alert('标题不能为空');</script>");
        }
        else
        {
            int typeid = int.Parse(DropDownList2.SelectedValue);
            if ((slType.Value == "0" && typeid == 23) || (slType.Value == "0" && typeid == 20) || (slType.Value == "0" && typeid == 5))
            {
                fun.AJAXalert(this, "请选择二级分类");
            }
            else
            {
                int id = int.Parse(Request.QueryString["id"]);
                DataRow dr = fun.GetDataTable("select * from news where id=" + id).Rows[0];

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
                string remark =fun.GetSafeStr( FCKeditor1.Value);
                name = fun.GetSafeStr(name);
             
                string sql = string.Format("update news set title='{0}',class1_id={1},[content]='{2}',istj='{3}',paixu={4},pic='{6}',zhuozhe='{7}',laiyuan='{8}',hot={9},change_date='{10}',sjid={11} where id={5}", name, typeid, remark,Request.Form["rdtj"],int.Parse(TextBox1.Text), id,pic,TextBox3.Text,TextBox5.Text,TextBox4.Text,DateTime.Now,slType.Value);
                fun.DoSql(this, sql, Request.Url.ToString());
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sjid = DropDownList2.SelectedValue;
        fun.bind(slType, "select * from news_class where sjid=" + sjid, "class1", "id", "请选择");
    }
}
