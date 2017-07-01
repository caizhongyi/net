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

namespace mston.AdminManager.News
{
    public partial class NewsEdit : System.Web.UI.Page
    {
       // Model.SmallType smallType;
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);

            //smallType = new Model.SmallType();
            if (!IsPostBack)
            {
                 BigTypeBind(DropBigType);
                 //smallType.BigTypeId = Convert .ToInt32( DropBigType.Items[0].Value);
                 //SmallTypeBind(DropSmallType, smallType);
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Company.Models.NewsInfo news = new Company.Models.NewsInfo();
            news.n_title = TxtNewsTitle.Text.Trim();
            news.n_content = FCKeditor1.Value;
            news.n_createDate = DateTime.Now;
            news.n_typeId = Convert.ToInt32(DropBigType.SelectedValue);
           // news.n_SmallType = Convert.ToInt32(DropSmallType.SelectedValue);
            news.n_isShow = RadioVisable.Checked == true ? true : false;
            news.n_source = TxtNewSource.Text.Trim();
            MyClass.User.Login.ILogin ilogin = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            news.n_author = ilogin.UserName;
            news.n_isDel = false;
            if (news.n_title == string.Empty)
            {
                JavaScriptHelper.ShowMsg(this, "请输入标题名称!");
            }
            //else if (news.NewsSmallType==0)
            //{
            //    JavaScriptHelper.ShowMsg(this, "请选择新闻所属的小类别!");
            //}
            else
            {
                Insert(news);
                Response.Redirect("News.aspx");
            }
            
        }
        private int Insert(Company.Models.NewsInfo news)
        {
          
            string sql = string.Format("insert into newsInfo (n_title,n_content,n_typeid,n_createDate,n_source,n_author,n_isShow,n_isDel) "
                + "values(N'{0}',N'{1}','{2}','{3}',N'{4}',N'{5}','{6}','{7}')",
               news.n_title.ToSQLString(),
               news.n_content.ToSQLString(),
                news.n_typeId,
                //news.NewsSmallType,
                news.n_createDate,
                news.n_source.ToSQLString(),
                news.n_author.ToSQLString(),
                news.n_isShow,
                news.n_isDel.ToString()
                );
            return T_BasePage.DB.ExecuteNonQuery(sql);
        }

    

        private void BigTypeBind(DropDownList DropRoles)
        {
            string sql = "select * from Typeinfo where t_isdel='False'";
            DropRoles.DataTextField = "t_name";
            DropRoles.DataValueField = "t_id";
            DropRoles.DataSource = T_BasePage.DB.GetDataSet(sql);
            DropRoles.DataBind();
        }

        //private void SmallTypeBind(DropDownList DropRoles, Model.SmallType smallType)
        //{
        //    string sql = string.Format("select * from smallType where bigType_id='{0}' and smallType_isDel='False'", smallType.BigTypeId);
        //    DropRoles.DataTextField = "smallType_name";
        //    DropRoles.DataValueField = "smallType_id";
        //    DataSet ds = DBCommon.GetSqlDBObj().GetDataSet(sql);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DropRoles.DataSource = ds;
        //        DropRoles.DataBind();
        //    }
        //    else
        //    {
        //        DropRoles.Items.Clear();
        //        ListItem listItem = new ListItem("请选择","0");
        //        DropRoles.Items.Add(listItem);
        //    }
        //}


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("News.aspx");
        }

        //protected void DropBigType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    smallType.BigTypeId = Convert.ToInt32(DropBigType.SelectedValue);
        //    SmallTypeBind(DropSmallType, smallType);
        //}
    }
}
