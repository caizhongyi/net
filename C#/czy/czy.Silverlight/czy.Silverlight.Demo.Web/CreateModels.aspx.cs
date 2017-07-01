using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace czy.Silverlight.Demo.Web
{
    public partial class CreateModels : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string conn="server=127.0.0.1,1384;database=czy_shop;uid=sa;pwd=sa";
           // MyDAL.SQL.SQLModelsBuilder.Create("czy_shop", conn,"czy.shop.Model", MyDAL.Enumeration.ConnStringType.String);
            MyDAL.SQL.SQLBBLBuilder.CreateBBL("czy_shop", conn, "czy.shop.BBL", MyDAL.DataBase.ConnStringType.String);
        }
    }
}