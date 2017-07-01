using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace WebService1
{
    /// <summary>
    /// UploadService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class UploadService : System.Web.Services.WebService
    {
        private MySoapHeader soapheader;
        public MySoapHeader SoapHeader
        {
            get { return soapheader; }
            set { soapheader = value; }
        }
        
        [WebMethod(Description="保存文件")]
        [SoapHeader("SoapHeader")]
        public void SaveFile(byte[] binData, string filename)
        {
            HeaderCheck.check(this);
            Image image = Image.FromStream(new MemoryStream());
            image.Save(Server.MapPath(filename));
        }
        [WebMethod(Description="删除文件")]
        [SoapHeader("SoapHeader")]
        public void DeleteFile(string filename)
        {
            HeaderCheck.check(this);
            if(File.Exists(Server.MapPath(filename)))
            {
                File.Delete(Server.MapPath(filename));
            }
        }
    }
}
