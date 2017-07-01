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
    public partial class NewsUpdate : System.Web.UI.Page
    {
        Company.Models.NewsInfo news;
       // Model.SmallType smallType;
        Company.Models.TypeInfo bigType;
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);

            news = new Company.Models.NewsInfo();
            //smallType = new Model.SmallType();
            bigType = new Company.Models.TypeInfo();

            if(Request .QueryString ["id"]!=null)
            {
                newsId.Value= Request .QueryString ["id"].ToString ();
                news.n_id = Convert.ToInt32(newsId.Value);
            }
            if (!IsPostBack)
            {
                BigTypeBind(DropBigType);

                DataSet ds=GetNewsData( news);
                if(ds.Tables [0].Rows .Count>0)
                {
                    FCKeditor1.Value = ds.Tables[0].Rows[0]["n_content"].ToString();
                    TxtNewsTitle.Text = ds.Tables[0].Rows[0]["n_title"].ToString();
                    TxtNewsSource.Text = ds.Tables[0].Rows[0]["n_source"].ToString();
                    TxtNewsAuthor.Text = ds.Tables[0].Rows[0]["n_author"].ToString();
                    bigType.t_Id = Convert .ToInt32( ds.Tables[0].Rows[0]["t_id"]);
                  //  smallType.SmallTypeId = Convert.ToInt32(ds.Tables[0].Rows[0]["smallType_id"]);
                   // smallType.BigTypeId = bigType.BigTypeId;
                    if (ds.Tables[0].Rows[0]["n_isShow"].ToString() == "True")
                    {
                        RadioVisable.Checked = true;
                    }
                    else
                    {
                        RadioHidden.Checked = true;
                    }
                    DropBigType.SelectedIndex = DropBigType.Items.IndexOf(DropBigType.Items.FindByValue(bigType.t_Id.ToString()));
                   // SmallTypeBind(DropSmallType, smallType);
                  
                 //   DropSmallType.SelectedIndex = DropSmallType.Items.IndexOf(DropSmallType.Items.FindByValue(smallType.SmallTypeId.ToString()));
                }
            }
        }
        private void BigTypeBind(DropDownList DropRoles)
        {
            string sql = "select * from TypeInfo where t_isdel='False'";
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
        //        ListItem listItem = new ListItem("请选择", "0");
        //        DropRoles.Items.Add(listItem);
        //    }
           
        //}

        private DataSet  GetNewsData( Company.Models.NewsInfo news)
        { 
            string sql=string .Format ("select * from v_newsinfo where n_id='{0}'",news.n_id);
            return T_BasePage.DB.GetDataSet(sql);
        
        }

        private bool UpdateNews(Company.Models.NewsInfo news)
        {
            string sql = string.Format("update newsinfo set n_title=N'{0}',n_content=N'{1}',n_typeId='{2}',n_createDate=N'{3}',n_source=N'{4}',n_author=N'{5}',n_isShow='{6}' where n_id='{7}'",
                news.n_title.ToSQLString(),
                news.n_content.ToSQLString(),
                news.n_typeId,
               // news.NewsSmallType,
                news.n_createDate,
                news.n_source.ToSQLString(),
                news.n_author.ToSQLString(),
                news.n_isShow,
                news.n_id
                );
            return T_BasePage.DB.ExecuteNonQuery(sql)  > 1 ? true : false;
        }


        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            news.n_id = Convert .ToInt32( newsId.Value.Trim());
            news.n_title = TxtNewsTitle.Text.Trim();
            news.n_content = FCKeditor1.Value;
            news.n_author = TxtNewsAuthor.Text.Trim();
            news.n_typeId = Convert.ToInt32(DropBigType.SelectedValue);
           // news.NewsSmallType = Convert.ToInt32(DropSmallType.SelectedValue);
            news.n_isShow=RadioVisable.Checked?true:false ;
            news.n_source = TxtNewsSource.Text.Trim();
            news.n_createDate = DateTime.Now;
            if (news.n_title == string.Empty)
            {
                JavaScriptHelper.ShowMsg(this, "请输入标题名称!");
            }
            //else if (news.NewsSmallType == 0)
            //{
            //    JavaScriptHelper.ShowMsg(this, "请选择新闻所属的小类别!");
            //}
            else
            {
                UpdateNews(news);
                Response.Redirect("News.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("News.aspx");
        }

        //protected void DropBigType_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    smallType.BigTypeId = Convert.ToInt32(DropBigType.SelectedValue);
        //    SmallTypeBind(DropSmallType, smallType);
        //}


    }
}
