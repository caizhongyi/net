using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class gc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.bind(Repeater1,"select * from news_class2");
        this.DropDownList1.DataBound += new EventHandler(DropDownList1_DataBound);
    }

    void DropDownList1_DataBound(object sender, EventArgs e)
    {
        SqlDataSource2.SelectParameters[0].DefaultValue = " cid=" + this.DropDownList1.Items[0].Value;
        this.SqlDataSource2.DataBind();
        this.DropDownList2.DataBind();
        SqlDataSource3.SelectParameters[0].DefaultValue = " cid=" + this.DropDownList2.Items[0].Value;;// =="" ? this.DropDownList2.Items[0].Value : this.DropDownList2.SelectedValue;
        this.SqlDataSource3.DataBind();
        this.DropDownList3.DataBind();
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Add();
    }
    protected void Add()
    {

        string carname = this.DropDownList1.Items.Count>0?this.DropDownList1.SelectedItem.Text:"";
        string cartype = this.DropDownList2.Items.Count > 0 ? this.DropDownList2.SelectedItem.Text : "";
        string carcolor = this.DropDownList3.Items.Count > 0 ? this.DropDownList3.SelectedItem.Text : "";

        string username=this.txt_username.Text;
             string  email=this.txt_email.Text;
             string phone = this.txt_mobile.Text;
             string tel = this.txt_del.Text;
             string address = this.txt_address.Text;
             string postcode = this.txt_code.Text;
             int iscome = this.r1.Checked==true?1:0;
             string content = this.txt_cont.Value;




             string sql = string.Format("insert into user_carnote (carname,cartype,carcolor,username,email,phone,tel,address,postcode,iscome,content) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}')", carname, cartype, carcolor, username, email, phone, tel, address, postcode, iscome, content);
            fun.DoSql(this, sql, Request.Url.ToString());

        
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource2.SelectParameters[0].DefaultValue = " cid=" + this.DropDownList1.SelectedValue ;//== "" ? this.DropDownList1.Items[0].Value : this.DropDownList1.SelectedValue;
        this.SqlDataSource2.DataBind();
        this.DropDownList2.DataBind();
        DropDownList2_SelectedIndexChanged(sender,e);
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource3.SelectParameters[0].DefaultValue = " cid=" + this.DropDownList2.SelectedValue;// =="" ? this.DropDownList2.Items[0].Value : this.DropDownList2.SelectedValue;
        this.SqlDataSource3.DataBind();
        this.DropDownList3.DataBind();

    }
}
