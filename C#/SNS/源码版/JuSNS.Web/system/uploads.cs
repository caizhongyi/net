using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class uploads : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            context.Put("redirecturl", "sysconfig"+ExName+"?path=~/config/photo/photo.config");
        }
    }
}
