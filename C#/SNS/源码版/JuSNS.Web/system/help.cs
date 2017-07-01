using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.system
{
    public class help : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            context.Put("redirecturl", root+"/help");
        }
    }
}
