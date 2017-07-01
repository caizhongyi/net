using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SLMusicPlayer.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //add by 51aspx
            Response.Redirect("SLMusicPlayerTestPage.aspx");
        }
    }
}
