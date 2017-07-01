using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.share
{
    public class url : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int sid = GetInt("id", 0);
            ShareInfo mdl = JuSNS.Home.App.Share.Instance.GetInfo(sid);
            if (mdl.IsLock != (byte)EnumCusState.ForNormal)
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                if (
                    (EnumShareType)mdl.ShareType == EnumShareType.ForMusic
                    || (EnumShareType)mdl.ShareType == EnumShareType.ForWeb
                    || (EnumShareType)mdl.ShareType == EnumShareType.ForVodie
                    || (EnumShareType)mdl.ShareType == EnumShareType.ForFlash
                    )
                {
                    context.Put("redirecturl", JuSNS.Common.Share.lib.GetURL((EnumShareType)mdl.ShareType, mdl.WebURL));
                }
                else
                {
                    context.Put("redirecturl", JuSNS.Common.Share.lib.GetURL((EnumShareType)mdl.ShareType, mdl.Infoid.ToString()));
                }
            }
        }
    }
}
