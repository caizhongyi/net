using JuSNS.Common;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.home.menu
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "更改菜单");
            context.Put("SetMenulist", Public.Menulist(this.UserID, false));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string menulistNames = GetString("menulistNames");
            if (string.IsNullOrEmpty(menulistNames))
            {
                context.Put("errors", "至少要设置一项做为菜单");
            }
            else
            {
                string filepath = "~/space/info/my/user" + UserID + ".config";
                Public.CreatUserConfig(filepath);
                Public.setXmlInnerText(filepath, "/configuration/menulist", menulistNames);
                context.Put("rights", "菜单排序保存成功");
            }
            ShowInfo(ref context);
        }
    }
}