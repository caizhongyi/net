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

public partial class jobYP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

           
        if (!this.IsPostBack)
        { bind();
		  bind2();
		}
    }
	
	 void bind2()
    {
        string sql;
        sql = "select *  from job order by id desc";
        DropDownList1.DataSource = fun.GetDataTable(sql);
        DropDownList1.DataTextField = "inviter";
        DropDownList1.DataValueField = "id";
        DropDownList1.DataBind();
        
    }
	
    private void bind()
    {
        string sql;
        sql = "select * from Jobyp where id=" + Request.QueryString["id"];
        DataTable dt = new DataTable();
        dt = fun.GetDataTable(sql);
        txtName.Value= dt.Rows[0]["txtName"].ToString();
		Gender.Value= dt.Rows[0]["Gender"].ToString();
      ddlyear.Value= dt.Rows[0]["ddlyear"].ToString();
	  ddlmonth.Value= dt.Rows[0]["ddlmonth"].ToString();
	  ddlday.Value= dt.Rows[0]["ddlday"].ToString();
	  txtFrom.Value= dt.Rows[0]["txtFrom"].ToString();
	  Marry.Value= dt.Rows[0]["Marry"].ToString();
	  txtIdentyCard.Value= dt.Rows[0]["txtIdentyCard"].ToString();
	  txtcSA.Value= dt.Rows[0]["txtcSA"].ToString();
	  txtTelephone.Value= dt.Rows[0]["txtTelephone"].ToString();
	  txtAddress.Value= dt.Rows[0]["txtAddress"].ToString();
	  txtEmail.Value= dt.Rows[0]["txtEmail"].ToString();
	  ddlStudyProcess.Value= dt.Rows[0]["ddlStudyProcess"].ToString();
	  txtMajor.Value= dt.Rows[0]["txtMajor"].ToString();
	  txtGraduateSchool.Value= dt.Rows[0]["txtGraduateSchool"].ToString();
	  ddlGraduateyear.Value= dt.Rows[0]["ddlGraduateyear"].ToString();
	  ddlGraduatemonth.Value= dt.Rows[0]["ddlGraduatemonth"].ToString();
	  ddlGraduateday.Value= dt.Rows[0]["ddlGraduateday"].ToString();
	   DropDownList1.Text = dt.Rows[0]["ypnameid"].ToString();
	  WaiPai.Value= dt.Rows[0]["WaiPai"].ToString();
	  txtDemandSalary.Value= dt.Rows[0]["txtDemandSalary"].ToString();
	  txtInfo.Value= dt.Rows[0]["txtInfo"].ToString();
	  txtProfile.Value= dt.Rows[0]["txtProfile"].ToString();
	 
	  
    }
}
