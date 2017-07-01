using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;

public class XmlControl
{
    protected string strXmlFile;
    protected XmlDocument objXmlDoc = new XmlDocument();

    public XmlControl(string XmlFile)
    {
        try
        {
            objXmlDoc.Load(XmlFile);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        strXmlFile = XmlFile;
    }

    public DataView GetData(string XmlPathNode)
    {
        //查找数据。返回一个DataView
        DataSet ds = new DataSet();
        StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
        ds.ReadXml(read);
        //int tag = ds.Tables[0].Rows.Count;
        return ds.Tables[0].DefaultView;
    }

    public DataSet GetDataSet(string XmlPathNode)
    {
        //查找数据。返回一个DataView
        DataSet ds = new DataSet();
        StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
        ds.ReadXml(read);
        //int tag = ds.Tables[0].Rows.Count;
        return ds;
    }

    public void Save()
    {
        //保存文檔。
        try
        {
            objXmlDoc.Save(strXmlFile);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        objXmlDoc = null;
    }
    
}