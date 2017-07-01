using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IMMENSITY.SWFUploadAPI
{
    public class SWFWebConfigManage
    {
        public static string GetByAppSettingsKey(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = SWFDataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if (objModel != null)
                    {
                        SWFDataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                    else
                    {
                        objModel = string.Empty;//5%1%a%s%p%x
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }
    }
}
