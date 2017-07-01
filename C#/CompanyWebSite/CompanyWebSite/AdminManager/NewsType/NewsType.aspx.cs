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
using MyClass;

namespace mston.AdminManager.Type
{
    public partial class Type : System.Web.UI.Page
    {
        Company.Models.TypeInfo bigType =new Company.Models.TypeInfo ();
      //  Model.SmallType smallType;
        AspNetPager _aspNetPager;
      //  AspNetPager _aspNetPager1;
        int pageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);

            _aspNetPager = new AspNetPager();
          //  _aspNetPager1 = new AspNetPager();
            AddAspNetPager(PlaceHolder1,_aspNetPager);
           // AddAspNetPager(PlaceHolder2,_aspNetPager1);

            _aspNetPager.PageChanging += new PageChangingEventHandler(_aspNetPager_PageChanging);
           // _aspNetPager1.PageChanging += new PageChangingEventHandler(_aspNetPager_PageChanging1);
            if (!IsPostBack)
            {
                BigTypeBind(GetBigType(), 0, RepBigType);
              //  BigTypeBind(DropBigType);


             //   SmallTypeBind(GetSmallType(), 0, RepSmallType);
            }
        }

        private void BigTypeBind(DropDownList DropRoles)
        {
            string sql = string.Format("select * from TypeInfo where t_isdel='False'");
            DropRoles.DataTextField = "t_name";
            DropRoles.DataValueField = "t_id";
            DropRoles.DataSource =T_BasePage.DB.GetDataSet(sql);
            DropRoles.DataBind();
            ListItem item = new ListItem("全部", "0");
            DropRoles.Items.Insert(0, item);
        }

        void BigTypeBind(DataSet ds, int cur,Repeater rep)
        {
            AspNetPager(cur, ds, rep, pageSize, _aspNetPager);
        }
        //void SmallTypeBind(DataSet ds, int cur, Repeater rep)
        //{
        //    AspNetPager(cur, ds, rep, 20, _aspNetPager1);
        //}

        public void AspNetPager(int cur, DataSet ds, Repeater rep, int pageSize, AspNetPager _aspNetPager)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = ds.Tables[0].DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            if (pds.PageCount - 1 < cur)
            {
                if (cur != 0)
                {
                    cur -= 1;
                }
            }
            pds.CurrentPageIndex = cur;
            _aspNetPager.RecordCount = pds.DataSourceCount;
            _aspNetPager.PageSize = pageSize;
            _aspNetPager.CurrentPageIndex = cur+1;
            rep.DataSource = pds;
            rep.DataBind();
        }

        void AddAspNetPager(PlaceHolder pl, AspNetPager _aspNetPager)
        {
            
            _aspNetPager.FirstPageText = "首页";
            _aspNetPager.LastPageText = "尾页";
            _aspNetPager.NextPageText = "下一页";
            _aspNetPager.PrevPageText = "上一页";
            _aspNetPager.ShowInputBox = ShowInputBox.Never;
            _aspNetPager.CssClass = "paginator";
            pl.Controls.Add(_aspNetPager);
        }



        public void _aspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            BigTypeBind(GetBigType(), e.NewPageIndex-1, RepBigType);
        }
        //public void _aspNetPager_PageChanging1(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        //{

        //    if (DropBigType.SelectedItem.Value.Trim() == "0")
        //    {
        //        SmallTypeBind(GetSmallType(), e.NewPageIndex - 1, RepSmallType);
        //    }
        //    else
        //    {
        //        smallType.BigTypeId = Convert.ToInt32(DropBigType.SelectedValue);
        //        SmallTypeBind(GetSmallTypeById(smallType), e.NewPageIndex - 1, RepSmallType);
        //    }
        //}

        DataSet GetBigType()
        {
            string sql = "select * from TypeInfo where t_isdel='False'";
            return T_BasePage.DB.GetDataSet(sql);
        }

 

        private void BigTypeBind(PagedDataSource pds)
        {
            RepBigType.DataSource = pds;
            RepBigType.DataBind();
        }

        //private DataSet GetSmallType()
        //{
        //    string sql = "select * from smallType left join bigType on smallType.bigType_id=bigType.bigType_id where smallType_isDel='False'";
        //    return  DBCommon.GetSqlDBObj().GetDataSet(sql);
           
        //}

        //private DataSet GetSmallTypeById(Model.SmallType smallType)
        //{
        //    string sql =string .Format ( "select * from smallType left join bigType on smallType.bigType_id=bigType.bigType_id where smallType_isDel='False' and smallType.bigType_Id='{0}'",smallType .BigTypeId);
        //    return DBCommon.GetSqlDBObj().GetDataSet(sql);

        //}

        //protected void RepSmallType_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    smallType = new Model.SmallType();
        //    smallType.SmallTypeId = Convert.ToInt32(((Label)RepSmallType.Items[e.Item.ItemIndex].FindControl("LabID")).Text);
        //    if (e.CommandName == "Edit")
        //    {
        //        Response.Redirect("NewsTypeUpdate.aspx?id=" + smallType.SmallTypeId);
        //        //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>window.open(\"UserInfoUpdate.aspx?id=" + userInfo.UserInfoId+"\",'_self')</script>");
        //    }
        //    if (e.CommandName == "Del")
        //    {
        //         DelSmallType(smallType);

        //         if (DropBigType.SelectedItem.Value.Trim() == "0")
        //         {
        //             SmallTypeBind(GetSmallType(), _aspNetPager1.CurrentPageIndex - 1, RepSmallType);
        //         }
        //         else
        //         {
        //             smallType.BigTypeId = Convert.ToInt32(DropBigType.SelectedValue);
        //             SmallTypeBind(GetSmallTypeById(smallType), _aspNetPager1.CurrentPageIndex - 1, RepSmallType);
        //         }
        //    }
        //}

     

        protected void RepBigType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            bigType = new Company.Models.TypeInfo();
            bigType.t_Id = Convert.ToInt32(((Label)RepBigType.Items[e.Item.ItemIndex].FindControl("LabID")).Text);

            if (e.CommandName == "Edit")
            {
                Response.Redirect("NewsTypeUpdate.aspx?id=" + bigType.t_Id.ToString ());
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>window.open(\"UserInfoUpdate.aspx?id=" + userInfo.UserInfoId+"\",'_self')</script>");
            }
            if (e.CommandName == "Del")
            {
                DelBigType(bigType);

                //SmallTypeBind(GetSmallType(), _aspNetPager1.CurrentPageIndex - 1, RepSmallType);
                BigTypeBind(GetBigType(), _aspNetPager.CurrentPageIndex - 1, RepBigType);
              //  BigTypeBind(DropBigType);
               
            }
        }

        protected void RepBigType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
                Literal literal=(Literal)e.Item.FindControl("Literal1");
                if (drv["t_parentId"].ToString() == "0" )
                {
                    linkbtn.Enabled = false;
                }
                else
                {
                   JavaScriptHelper.ShowConFirm(linkbtn, "是否删除？", null);
                }
         
                //literal.Text = string.Format("<a href='AddNewSubType.aspx?id={0}&name={1}'>添加子类别</a>",drv["t_id"], Server.UrlEncode(drv["t_name"].ToString()));
                
            }
        }

        //protected void RepSmallType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
        //    {
        //        DataRowView drv=(DataRowView)e.Item.DataItem;
        //        LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
        //        if (drv["smallType_id"].ToString() == "100" || drv["smallType_id"].ToString() == "101" || drv["smallType_id"].ToString() == "102" || drv["smallType_id"].ToString() == "103")
        //        {
        //            linkbtn.Enabled = false;
        //        }
        //        else
        //        {
        //            JavaScriptHelper.ShowConFirm(linkbtn, "是否删除？", null);
        //        }
        //    }
        //}


      

        private bool DelBigType(Company.Models.TypeInfo bigType)
        {
           // string sql = string.Format("delete from bigType where bigType_id='{0}' and bigType_isDel='False'", bigType.BigTypeId);
            string sql1 = " begin tran";
            string sql2 = string.Format(" update TypeInfo set t_isdel='True'    where t_id='{0}' ", bigType.t_Id);
           // string sql3 = string.Format(" update smallType set smallType_isDel='True'   where bigType_id='{0}' ", bigType.BigTypeId);
            string sql4 = string.Format(" update NewsInfo set n_isdel='True'   where n_typeid='{0}' ", bigType.t_Id);
            string sql5 = " if(@@Error>0) begin rollback tran end else begin commit tran end";
            string sql = sql1 + sql2 + sql4+sql5;
            return T_BasePage.DB.ExecuteNonQuery(sql) > 0 ? true : false;
        }
        //private bool DelSmallType(Model.SmallType samllType)
        //{
        //   // string sql = string.Format("delete from smallType where smallType_id='{0}' and smallType_isDel='False'", smallType.SmallTypeId);
        //    string sql1 = " begin tran";
        //    string sql2 = string.Format(" update news set news_isDel='True'    where smallType_id='{0}' ", smallType.SmallTypeId);
        //    string sql3 = string.Format(" update smallType set smallType_isDel='True'   where smallType_id='{0}' ", smallType.SmallTypeId);
        //    string sql4 = " if(@@Error>0) begin rollback tran end else begin commit tran end";
        //    string sql = sql1 + sql2 + sql3 + sql4;
        //    return T_BasePage.DB.ExecuteNonQuery(sql) (sql) > 0 ? true : false;
        //}

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            //if (DropBigType.SelectedItem.Value.Trim() == "0")
            //{
            //    SmallTypeBind(GetSmallType(), _aspNetPager1.CurrentPageIndex - 1, RepSmallType);
            //}
            //else
            //{
            //    smallType.BigTypeId = Convert.ToInt32(DropBigType.SelectedValue);
            //    SmallTypeBind(GetSmallTypeById(smallType), _aspNetPager1.CurrentPageIndex - 1, RepSmallType);
            //}
        }

     
    }
}
