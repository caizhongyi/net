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
using czy.BBL;


namespace mston.AdminManager.NewsType
{
    public partial class AddNewSubType : System.Web.UI.Page
    {
        czy.Models.TypeInfo bigType = new czy.Models.TypeInfo();
       // Model.SmallType smallType;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Common.AuthenticationLogin(this);
            //if (Request.QueryString["id"] != null)
            //{
            //    bigType.BigTypeId = Convert.ToInt32(Request.QueryString["id"]);
            //}
            //if (Request.QueryString["name"] != null)
            //{
            //    bigType.BigTypeName = Server.UrlDecode(Request.QueryString["name"]);
            //}

            //hidBigTypeId.Value = bigType.BigTypeId.ToString ();
            //LabBigTypeName.Text = bigType.BigTypeName;


            //smallType.SmallTypeName = TxtSmallType.Text.Trim();
            //smallType.BigTypeId = bigType.BigTypeId;
            //smallType.SmallTypeCreateDate = DateTime.Now;
        }

        private bool InsertSmallType(czy.Models.TypeInfo typeInfo)
        {
            string sql = string.Format("Insert into TypeInfo(t_name,t_url,t_parentId,t_isDel,t_order) values(N'{0}','{1}','{2}','{3}','{4}')",
                typeInfo.t_name.ToSQLString (),
                typeInfo.t_url.ToSQLString (),
                typeInfo .t_parentId,
                typeInfo.t_isDel,
                typeInfo.t_order
                );
            return TypeInfo.Insert(typeInfo)> 0 ? true : false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (smallType.SmallTypeName == string.Empty)
            //{ JavaScriptHelper.ShowMsg(this, "请输入类别名称"); }
            
            //else
            //{
            //    InsertSmallType(smallType);
            //}
            //Response.Redirect("NewsType.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsType.aspx");
        }
    }
}
