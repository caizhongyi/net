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
using czy.MyClass;

namespace mston.AdminManager.NewsType
{
    public partial class NewsTypeUpdate : System.Web.UI.Page
    {
        czy.Models.TypeInfo bigType = new czy.Models.TypeInfo();
       // Model.SmallType smallType;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase.AuthenticationLogin();

            int id = 0;
            string type = "big";
 

            if (!IsPostBack)
            {
                BigTypeBind(list_parantType);
            }

            if (Request.QueryString["id"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                bigType.t_Id = id;
            }
            if (Request.QueryString["type"] != null)
            {
                type = Request.QueryString["type"].ToString();
            }
          

            SetValue(type, id);
        }

        private void SetValue(string type,int id)
        {

            if (type == "big")
            {
                bigType.t_Id = id;
              //  PanelSmallType.Visible = false;

              
                if (!IsPostBack)
                {
                    DataSet ds = GetBigType(bigType);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bigType.t_name = ds.Tables[0].Rows[0]["t_name"].ToString();
                        bigType.t_url = ds.Tables[0].Rows[0]["t_url"].ToString();
                        bigType.t_isDel = Convert.ToBoolean(ds.Tables[0].Rows[0]["t_isDel"]);
                        bigType.t_parentId =Convert .ToInt32( ds.Tables[0].Rows[0]["t_parentId"].ToString());
                        bigType.t_order = Convert .ToInt32( ds.Tables[0].Rows[0]["t_order"]);
                        TxtBigType.Text = bigType.t_name;
                        TxtBigTypeUrl.Text = bigType.t_url;
                        TxtOrder.Text = bigType.t_order.ToString();
                        list_parantType.SetIndexByValue(bigType.t_parentId.ToString ());
                    }
                }
            }
            else
            {
              //  smallType.SmallTypeId = id;
                PanelBigType.Visible = false;
                if (!IsPostBack)
                {
                   // DataSet ds = GetSmallType(smallType);
                 //   if (ds.Tables[0].Rows.Count > 0)
                  //  {
                        //TxtSmallType.Text = ds.Tables[0].Rows[0]["smallType_name"].ToString();
                       // smallType .BigTypeId=Convert .ToInt32(ds.Tables[0].Rows[0]["bigType_id"]);
                       // DropBigType.SelectedIndex = DropBigType.Items.IndexOf(DropBigType.Items.FindByValue(smallType.BigTypeId.ToString()));
                  //  }
                }
            }
        }

        private DataSet GetBigType(czy.Models.TypeInfo bigType)
        {
          //  string sql =string .Format ( "select * from TypeInfo where t_id='{0}'",bigType.t_Id.ToString ());
            return TypeInfo.Select(bigType.t_Id);
        }

        //private DataSet GetSmallType(Model .SmallType smallType)
        //{
        //    string sql =string .Format ( "select * from smallType where smallType_id='{0}'",smallType .SmallTypeId);
        //    return DBCommon.GetSqlDBObj().GetDataSet(sql);
        //}
        private void BigTypeBind(DropDownList DropRoles)
        {
            string sql = "select * from TypeInfo";
            DropRoles.DataTextField = "t_name";
            DropRoles.DataValueField = "t_id";
            DropRoles.DataSource = TypeInfo.Select();
            DropRoles.DataBind();
            DropRoles.Items.Insert(0, new ListItem("无", "0"));
        }

        private bool UpdateBigType(czy.Models.TypeInfo bigType)
        {
            object[] param = new object[] { bigType.t_name.ToSQLString(), bigType.t_url.ToSQLString(), bigType.t_order ,bigType.t_parentId,bigType.t_Id};
            string sql = string.Format("update TypeInfo set t_name=N'{0}',t_url=N'{1}',t_order='{2}',t_parentId='{3}'  where t_id='{4}'",param );
            return TypeInfo.Insert(bigType) > 0 ? true : false;
        }

        //private bool UpdateSmallType(Model.SmallType smallType)
        //{
        //    string sql = string.Format("update smallType set smallType_name=N'{0}',bigType_id='{2}' where smallType_id='{1}'", 
        //        SQLCommandHelper.TranToSQL(smallType.SmallTypeName), 
        //        smallType.SmallTypeId,
        //        smallType.BigTypeId);
        //    return T_BasePage.DB.ExecuteNonQuery(sql) (sql) > 0 ? true : false;
        //}

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsType.aspx");
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            bigType.t_name = TxtBigType.Text.Trim();
            bigType.t_url = TxtBigTypeUrl.Text.Trim();
            try { bigType.t_order = Convert.ToInt32(TxtOrder.Text); }
            catch { JavaScriptHelper.ShowMsg(this, "请输入数字!"); }
            bigType.t_parentId = Convert .ToInt32( list_parantType.SelectedValue);
            bool res=UpdateBigType( bigType);
            Response.Redirect("NewsType.aspx");
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    smallType.SmallTypeName = TxtSmallType.Text.Trim();
        //    smallType.BigTypeId = Convert.ToInt32(DropBigType.SelectedValue);
        //    UpdateSmallType(smallType);
        //    Response.Redirect("NewsType.aspx");
        //}

        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    bigType.BigTypeName = TxtBigType.Text.Trim();
        //    Response.Write(string .Format ( "<script> window.open('AddNewSubType.aspx?id={0}&name={1}','_self') </script>",bigType .BigTypeId,Server .UrlDecode( bigType .BigTypeName)));
        //}
    }
}
