using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyClass.CommandHelper;
using MyClass;

namespace mston.AdminManager.NewsType
{
    public partial class NewsTypeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type="big";

            Common.AuthenticationLogin(this);
            if (Request.QueryString["type"]!=null)
            {
                type = Request.QueryString["type"].ToString();
            }
            if (type.Trim() == "big")
            {
              //  RadioBigType.Checked = true;
                bigType.Style.Add("display", "bolck");
              //  smallType.Style.Add("display", "none");
                
            }
            else
            {
                //RadioSmallType.Checked = true;
                bigType.Style.Add("display", "none");
               // smallType.Style.Add("display", "bolck");
            }

            if (!IsPostBack)
            {
                BigTypeBind(list_paranType);
            }

           // RadioBigType.Attributes.Add("onclick", "ChangeType();");
            //RadioSmallType.Attributes.Add("onclick", "ChangeType();");
        }

        private bool InsertBigType(Company.Models.TypeInfo bigType)
        {
            string sql = string.Format("Insert into typeInfo(t_name,t_isDel,t_parentId,t_url,t_order) values(N'{0}','False','{1}',N'{2}','{3}')",
                bigType.t_name.ToSQLString(),
                bigType.t_parentId.ToString(),
                bigType.t_url.ToSQLString(),
                bigType.t_order
                );
            return  T_BasePage.DB.ExecuteNonQuery(sql) >0?true :false ;
        }
        //private bool InsertSmallType(Model.SmallType smallType)
        //{
        //    string sql = string.Format("Insert into smallType(smallType_name,smallType_createDate,bigType_id,smallType_isDel) values(N'{0}','{1}','{2}','False')",
        //        SQLCommandHelper.TranToSQL(smallType.SmallTypeName), 
        //        smallType.SmallTypeCreateDate.ToString("yyyy-MM-dd hh:mm:ss"),
        //        smallType.BigTypeId
        //        );
        //    return T_BasePage.DB.ExecuteNonQuery(sql) (sql) > 0 ? true : false;
        //}
        private void BigTypeBind(DropDownList DropRoles)
        {
            string sql = "select * from Typeinfo where t_isDel='False'";
            DropRoles.DataTextField = "t_Name";
            DropRoles.DataValueField = "t_id";
            DropRoles.DataSource =T_BasePage.DB.GetDataSet(sql);
            DropRoles.DataBind();
            DropRoles.Items.Insert(0,new ListItem("无", "0"));
        }
        protected void RadioBigType_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        protected void RadioSmallType_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Company.Models.TypeInfo bigType = new Company.Models.TypeInfo();
            bigType.t_name = TxtBigType.Text.Trim();
            bigType.t_url= TxtBigTypeUrl.Text.Trim();
            bigType.t_parentId = Convert.ToInt32(list_paranType.SelectedValue);
            try
            {
                bigType.t_order = Convert.ToInt32(TxtOrder.Text);
            }
            catch
            {
                JavaScriptHelper.ShowMsg(this, "请输入数字!");
            }
            if (bigType.t_name == string.Empty)
            {
                JavaScriptHelper.ShowMsg(this, "请输入类别名称!");
            }
            else
            {
                InsertBigType(bigType);
                Response.Redirect("NewsType.aspx");
            }
         
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Model.SmallType smallType = new Model.SmallType();
        //    smallType.SmallTypeName = TxtSmallType.Text.Trim();
        //    smallType.SmallTypeCreateDate = DateTime.Now;
        //    smallType.BigTypeId =Convert .ToInt32( DropBigType.SelectedValue);
        //    if (smallType.SmallTypeName == string.Empty)
        //    {
        //        JavaScriptHelper.ShowMsg(this, "请输入类别名称");
        //    }
        //    else
        //    {
        //        InsertSmallType(smallType);
        //        Response.Redirect("NewsType.aspx");
        //    }

            
        //}

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsType.aspx");
        }

    


        
    }
}
