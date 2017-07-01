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
using Wuqi.Webdiyer;
using MyClass.CommandHelper;
using MyClass;

namespace mston.AdminManager.News
{
    public partial class News : System.Web.UI.Page
    {
  
        int pageSize = 20;
        DataSet ds;
        int totalCount = 0;
      //  Model.SmallType smallType;
        string ovalue = "n_createDate";
        string oArrangement = "desc";

        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);


            if (!IsPostBack)
            {
                //NewsBind();
                AspNetPager1.CurrentPageIndex = 1;
                BigTypeBind(DropBigType);
                
                if (SearchModel.Value == "0")
                {
                    ds = GetNewsData(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                }
                else
                {
                    ds = GetNewsDataBySearch(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                }
                RepBind(AspNetPager1.CurrentPageIndex, ds);
               

            }
            if (curHidden.Value != string.Empty)
            {
                ovalue = curHidden.Value.Split(' ')[0].ToString();
                oArrangement = curHidden.Value.Split(' ')[1].ToString();
            }
            
         
        }
        private void RepBind(int cur, DataSet ds)
        {
            //PagedDataSource pds = new PagedDataSource();
            //pds.DataSource = ds.Tables[0].DefaultView;
            //pds.AllowPaging = true;
            //pds.PageSize = pageSize;
            if (ds.Tables[0].Rows.Count==0)
            {
                if (cur != 1)
                {
                    cur -= 1;
                    if (SearchModel.Value == "0")
                    {
                        ds = GetNewsData(cur, ovalue,oArrangement);
                    }
                    else
                    {
                        ds = GetNewsDataBySearch(cur, ovalue,oArrangement);
                    }
               
                }
            }
            NewsBind(ds);
            //pds.CurrentPageIndex = cur;
            AspNetPager1.RecordCount = totalCount;
            AspNetPager1.PageSize=pageSize;
            AspNetPager1.CurrentPageIndex = cur;
            AspNetPager1.CssClass = "paginator";
        
        }

     

        private DataSet  GetNewsData(int cur,string orderFild,string order)
        {
            string sql = "select * from v_newsInfo "
            + "where n_isDel='False' order by n_createDate desc";
            MyClass.PageHelper.SqlPagerHelper sph = new MyClass.PageHelper.SqlPagerHelper("*", "n_id", "desc", "v_newsInfo", " n_isdel='False' ", pageSize, orderFild + " " + order, T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            DataSet ds = sph.GetCurrentPageDataBy2005(cur, out totalCount);
            return ds;
        }

        private DataSet GetNewsDataBySearch(int cur, string orderFild, string order)
        {
            ArrayList list = new ArrayList();
            list.Add(TxtTitle.Text.Trim());
            list.Add(TxtAuthor.Text.Trim());
            list.Add(TxtSource.Text.Trim());
            if (DropBigType.SelectedValue.Trim () == "0")
            { list.Add(string .Empty ); }
            else
            { list.Add(DropBigType.SelectedValue); }

            //if (DropSmallType.SelectedValue.Trim() == "0")
            //{
            //    list.Add(string .Empty );
            //}
            //else
            //{
            //    list.Add(DropSmallType.SelectedValue);
            //}

                switch (DropState.SelectedValue)
                {
                    case "0": list.Add(string .Empty );
                        break;
                    case "1": list.Add("True");
                        break;
                    case "2": list.Add("False");
                        break;
                }

            string param = string.Empty;

            param += " n_title like '%" + list[0].ToString() + "%'";
            param += " and n_author like '%" + list[1].ToString() + "%'";
            param += " and n_source like '%" + list[2].ToString() + "%'";
            if (list[3].ToString () != string .Empty )
            {
                param += " and n_typeId = '" + list[3].ToString() + "'";
            }
            //if (list[4].ToString() != string.Empty)
            //{
            //    param += " and smallType_id = '" + list[4].ToSQLString() + "'";
            //}
            if (list[4].ToString() != string.Empty)
            {
                param += " and n_isshow = '" + list[4].ToString() + "'";
            }
            string sql = "select * from v_newsInfo where " + param
                     + " and n_isdel='False' order by n_createDate desc";
            MyClass.PageHelper.SqlPagerHelper sph = new MyClass.PageHelper.SqlPagerHelper("*", "n_id", "desc", "v_newsInfo", param
                     + " and n_isdel='False'", pageSize, orderFild, T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            DataSet ds = sph.GetCurrentPageDataBy2005(cur, out totalCount);
            return ds;
        }

        private void NewsBind(DataSet ds)
        {
            RepNews.DataSource =ds;
            RepNews.DataBind();
            
        }

        protected void RepNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Company.Models.NewsInfo news = new Company.Models.NewsInfo();
            if (e.CommandName == "Edit")
            {

                news.n_id = Convert.ToInt32(((Label)RepNews.Items[e.Item.ItemIndex].FindControl("LabID")).Text);
                Response.Redirect("NewsUpdate.aspx?id=" + news.n_id);
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>window.open(\"UserInfoUpdate.aspx?id=" + userInfo.UserInfoId+"\",'_self')</script>");
            }
            if (e.CommandName == "Del")
            {

                news.n_id = Convert.ToInt32(((Label)RepNews.Items[e.Item.ItemIndex].FindControl("LabID")).Text);
                DelNews(news);
                if (SearchModel.Value == "0")
                {
                    ds = GetNewsData(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                }
                else
                {
                    ds = GetNewsDataBySearch(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                }
                RepBind(AspNetPager1.CurrentPageIndex, ds);
            }
            if (e.CommandName == "Show")
            {

                news.n_id = Convert.ToInt32(((Label)RepNews.Items[e.Item.ItemIndex].FindControl("LabID")).Text);
                if (e.CommandArgument.ToString().Trim() == "True")
                {
                    news.n_isShow = false;
                }
                else
                {
                    news.n_isShow = true;
                }
                IsShow(news);
                if (SearchModel.Value == "0")
                {
                    ds = GetNewsData(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                }
                else
                {
                    ds = GetNewsDataBySearch(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                }
               
                RepBind(AspNetPager1.CurrentPageIndex , ds);
            }
            if (e.CommandName == "order")
            {
                  string orderString =e.CommandArgument + "Order";
                  ovalue = e.CommandArgument.ToString ();
                  oArrangement = ((HiddenField)this.FindControl(orderString)).Value;
                  if (oArrangement == "asc")
                  {
                      if (SearchModel.Value == "0")
                      {
                          ds = GetNewsData(AspNetPager1.CurrentPageIndex,ovalue, oArrangement);
                      }
                      else
                      {
                          ds = GetNewsDataBySearch(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                      }
                      RepBind(AspNetPager1.CurrentPageIndex, ds);

                      ((HiddenField)this.FindControl(orderString)).Value = "desc";
                      //for (int i = 0; i < RepNews.Items.Count; i++)
                      //{
                      //    if (((HiddenField)RepNews.Items[i].FindControl(orderString)).ID == orderString)
                      //    {
                      //        ((HiddenField)RepNews.Items[i].FindControl(orderString)).Value = orderValue;
                      //    }
                      //}
                  }
                  else
                  {
                      if (SearchModel.Value == "0")
                      {
                          ds = GetNewsData(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                      }
                      else
                      {
                          ds = GetNewsDataBySearch(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
                      }
                      RepBind(AspNetPager1.CurrentPageIndex, ds);
                       ((HiddenField)this.FindControl(orderString)).Value = "asc";
                      //for (int i = 0; i < RepNews.Items.Count; i++)
                      //{
                      //    if (((HiddenField)RepNews.Items[i].FindControl(orderString)).ID == orderString)
                      //    {
                      //        ((HiddenField)RepNews.Items[i].FindControl(orderString)).Value = orderValue;
                      //    }
                      //}
                  }
                  curHidden.Value = ovalue +" "+ oArrangement;
            }
        }
        private bool DelNews(Company.Models.NewsInfo news )
        {
            //string sql = string.Format("delete from news where news_id='{0}'",news.NewsId);
            string sql = string.Format("update newsInfo set n_isdel='True' where n_id='{0}'", news.n_id);
            return T_BasePage.DB.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        bool IsShow(Company.Models.NewsInfo news)
        {
            string sql = string.Format("update newsInfo set n_isshow='{1}' where n_id='{0}'", news.n_id, news.n_isShow);
            return T_BasePage.DB.ExecuteNonQuery(sql)  > 0 ? true : false;
        }

        private void BigTypeBind(DropDownList DropRoles)
        {
            string sql =string .Format ( "select * from typeInfo where t_isdel='False'");
            DropRoles.DataTextField = "t_name";
            DropRoles.DataValueField = "t_id";
            DropRoles.DataSource = T_BasePage.DB.GetDataSet(sql);
            DropRoles.DataBind();
            ListItem item=new ListItem ("全部","0");
            DropRoles.Items.Insert(0, item);
        }

        //private void SmallTypeBind(DropDownList DropRoles,Model.SmallType smallType)
        //{
        //    string sql = string.Format("select * from smallType where bigType_id='{0}' and smallType_isDel='False'",  smallType.BigTypeId);
        //    DropRoles.DataTextField = "smallType_name";
        //    DropRoles.DataValueField = "smallType_id";
        //    DropRoles.DataSource = DBCommon.GetSqlDBObj().GetDataSet(sql);
        //    DropRoles.DataBind();
        //    ListItem item = new ListItem("全部", "0");
        //    DropRoles.Items.Insert(0, item);
        //}

        protected void RepNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
            {

                LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
                JavaScriptHelper.ShowConFirm(linkbtn, "确定是否删除？", null);
            }
        }

        protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
        {
            if (SearchModel.Value == "0")
            {
                ds = GetNewsData(e.NewPageIndex, ovalue, oArrangement);
            }
            else
            {
                ds = GetNewsDataBySearch(e.NewPageIndex, ovalue,oArrangement);
            }
            RepBind(e.NewPageIndex,ds);
        }




        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 0;
            SearchModel.Value = "1";
            ds = GetNewsDataBySearch(AspNetPager1.CurrentPageIndex, ovalue, oArrangement);
            RepBind(AspNetPager1.CurrentPageIndex, ds);
        }

        //protected void DropBigType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    smallType.BigTypeId = Convert .ToInt32( DropBigType.SelectedValue);
        //    SmallTypeBind(DropSmallType,smallType);
        //}
    }
}
