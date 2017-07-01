using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class book : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "留言管理");
        }
    }
}
