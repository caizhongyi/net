using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;

/// <summary>
///AdminAjaxMethods 的摘要说明
/// </summary>
public class T_AdminAjaxMethods 
{

    Index index;
    public T_AdminAjaxMethods()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        index = new Index();
    }
    [AjaxPro.AjaxMethod]
    public int CheckUser(string user,string pwd)
    {
        return 0;
    }
    [AjaxPro.AjaxMethod]
    public bool ResponseFilterIndex()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + T_AdminBasePage.Filter_root + T_AdminBasePage.Filter_index;
        string indexHTML = index.GetIndexHTML();
        return MyClass.FileOpeation.MyStream.StreamWrite(path, System.Text.Encoding.UTF8, indexHTML);
    }
 
}
