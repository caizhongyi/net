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
using Newtonsoft.Json;
using System.IO;

/// <summary>
///Newtonsoft.Json
/// </summary>
public class JsonConvert
{
    string _jsonText=string .Empty ;
    int _rowCount=0;
    int _columnCount = 0;

    /// <summary>
    /// json字符
    /// </summary>
    public string JsonText
    {
        get { return _jsonText; }
        set { _jsonText = value; }
    }
    /// <summary>
    /// 行数
    /// </summary>
    public int RowCount
    {
        get { return _rowCount; }
        set { _rowCount = value; }
    }
    /// <summary>
    /// 列数
    /// </summary>
    public int ColumnCount
    {
        get { return _columnCount; }
        set { _columnCount = value; }
    }

    JsonTextReader jr;
    /// <summary>
    /// json初始化
    /// </summary>
    /// <param name="jsonText">json字符窜</param>
    public JsonConvert(string jsonText)
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
         bool objReadStart = false;
         _jsonText = jsonText;
         jr = new JsonTextReader(new StringReader(jsonText));
         while (jr.Read())
         {
             if (jr.TokenType == JsonToken.StartObject)
             {
                 objReadStart = true;
                 this._columnCount++;
             }
             if (jr.TokenType == JsonToken.EndObject)
             {
                 objReadStart = false;
                 this._rowCount++;
                 this._columnCount = 0;
             }
         }
    }
    /// <summary>
    /// 读取json节点
    /// </summary>
    /// <param name="row">行Index</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns>值</returns>
    public object ReadJsonValue( int row, string propertyName)
    {
        object res = "";
        jr = new JsonTextReader(new StringReader(_jsonText));
        
        bool objReadStart = false;
        bool isProperty = false;

        int rowIndex = 0;
        //int columnInex = 0;
        while (jr.Read())
        {
            if (jr.TokenType == JsonToken.StartObject)
            {
                objReadStart = true; 
            }
            if (jr.TokenType == JsonToken.EndObject)
            {
                objReadStart = false;
                rowIndex++;
                isProperty = false;
            }
            if (objReadStart)
            {
                if (isProperty)
                {
                    res = jr.Value;
                    isProperty = false;
                }

                if (jr.TokenType == JsonToken.PropertyName)
                {
                    if (rowIndex == row)
                    {
                        isProperty = jr.Value.ToString() == propertyName ? true : false;
                    }
                }


            }
            else
            {

            }

        }
        return res;
    }

    /// <summary>
    /// 读取json节点
    /// </summary>
    /// <param name="row">行Index</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns>值</returns>
    public object ReadJsonValue(int row, int column)
    {
        object res = "";
        jr = new JsonTextReader(new StringReader(_jsonText));

        bool objReadStart = false;
        bool isProperty = false;

        int rowIndex = 0;
        int columnInex = 0;
        while (jr.Read())
        {
            if (jr.TokenType == JsonToken.StartObject)
            {
                objReadStart = true;
                columnInex++;
            }
            if (jr.TokenType == JsonToken.EndObject)
            {
                objReadStart = false;
                rowIndex++; columnInex = 0;
                isProperty = false;

            }
            if (objReadStart)
            {
                if (isProperty)
                {
                    res = jr.Value;
                    isProperty = false;
                }

                if (jr.TokenType == JsonToken.PropertyName)
                {
                    if (rowIndex == row)
                    {
                        isProperty = columnInex == column ? true : false;
                    }
                }


            }
            else
            {

            }

        }
        return res;
    }
}
