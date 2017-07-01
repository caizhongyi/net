using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Reflection.Emit;


namespace czy.MyClass.Json
{
    
    /// <summary>
    /// 类  名：JSONConvert
    /// 描  述：JSON解析类
    ///Jsonbject JobjCol = new Jsonbject();
    ///Jsonbject JobjTable = new Jsonbject();
    ///JobjCol.Add("sub1", "sub1");
    ///JobjCol.Add("sub2", "sub2");
    ///JobjTable.Add("col1", JobjCol);
    ///JobjTable.Add("col2", JobjCol);
    ///Response.Write( JobjTable["col1", "sub1"].ToString());
    /// </summary>
    public  class MyJson
    {
        private Jsonbject _json = new Jsonbject();//寄存器
        private  string jsonText = string.Empty;
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public  string  Deserialize(Jsonbject json)
        {
             jsonText += "[";
             for (int i = 0; i < json.Length; i++)
             {

                 if (i == 0 && i != (json.Length) - 1)
                 {
                     if (json[i].GetType() == json.GetType())
                     {
                         DeserializeCol((Jsonbject)json[i]);
                     }
                 }
                 else
                 {
                     jsonText += ",";
                     if (json[i].GetType() == json.GetType())
                     {
                         DeserializeCol((Jsonbject)json[i]);
                     }
                 }
             }
            jsonText += "]";
            return jsonText;
        }

        private  void DeserializeCol(Jsonbject json)
        {


            for (int i = 0; i < json.Length; i++)
            {
                string doublestr = "\"";
                if (json[i].GetType() != typeof(String))
                {
                    doublestr = string.Empty;
                }
           
                if (i == 0 && i != (json.Count / 2) - 1)
                {
                    jsonText += "{\"" + json.GetColName(i) + "\":" + doublestr + "";
                    DeCellIsJson(json, i);
                    jsonText += "" + doublestr + "";

                }
                else if (i == (json.Length) - 1)
                {
                    jsonText += ",\"" + json.GetColName(i) + "\":" + doublestr + "";
                    DeCellIsJson(json, i);
                    jsonText += "" + doublestr + "}";
                }
                else
                {
                    jsonText += ",\"" + json.GetColName(i) + "\":" + doublestr + "";
                    DeCellIsJson(json, i);
                    jsonText += "" + doublestr + "";
                }
            }

 
        }
        /// <summary>
        /// 反序列化列时检查值是否为对像
        /// </summary>
        /// <param name="json"></param>
        /// <param name="i"></param>
        private void DeCellIsJson(Jsonbject json,int i)
        {
            if (json[i].GetType() == json.GetType())
            {
                Deserialize((Jsonbject)json[i]);
            }
            else
            {
                jsonText += json[i].ToString();
            }
        }
    
        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="text">json标准格式为[{"demo1":"demo1","demo4":"demo4"},{"demo2":["demo3":"demo3"]}]</param>
        public  Jsonbject ReDeserialize(string text)
        {
            Jsonbject jsonTable = new Jsonbject();
            jsonTable.Add ("tab",ReDeserializeCol(text));
            return jsonTable;
        }
        public Jsonbject ReDeserializeCol(string cell)
        {
            Jsonbject jsoncol = new Jsonbject();
            MatchCollection matches = Regex.Matches(cell, "(\\\"(?<key>[^\\\"]+)\\\":\\\"(?<value>[^,\\\"]*)\\\")|(\\\"(?<key>[^\\\"]+)\\\":(?<value>[^,\\\"\\}]*))");
            foreach (Match match in matches)
            {
                string value = match.Groups["value"].Value;
                jsoncol.Add(match.Groups["key"].Value,value);
            }
            return jsoncol;
        }



    }
        /// <summary>
        /// 类  名：JSONObject
        /// 描  述：JSON对象类
        /// 编  写：dnawo
        /// 站  点：http://www.mzwu.com/
        /// 日  期：2010-01-06
        /// 版  本：1.1.0
        /// 更新历史：
        ///     2010-01-06  继承Dictionary<TKey, TValue>代替this[]
        /// </summary>
        public class Jsonbject : List<object>
        {
            List<object> keyList;
            List<object> valueList;
            private int _length=0;

            public int Length
            {
                get { return _length; }
           
            }
            public Jsonbject()
            {
                keyList = new List<object>();
                valueList = new List<object>();
            }
            public void Add(string key, object value)
            {
                keyList.Add(key);
                valueList.Add(value);
                this.Add(keyList);
                this.Add(valueList);
                _length = this.Count / 2;
                //if (value.GetType() == this.GetType())
                //{
                //   // Jsonbject  = new Jsonbject();
                //   // valueList.Add(this);
                //    // ((Jsonbject)this[this.Length])

                //    Type t = ((Jsonbject)value).GetType();
                //    List<ClassHelper.CustPropertyInfo> lcpi = new List<ClassHelper.CustPropertyInfo>();
                //    ClassHelper.CustPropertyInfo cpi;

                //    cpi = new ClassHelper.CustPropertyInfo("czy.MyClass.Json.Jsonbject",((Json.Jsonbject)value)[1].ToString ());
                //    lcpi.Add(cpi);

                //    //再加入上面定义的两个属性到我们生成的类t。
                //    t = ClassHelper.AddProperty(t, lcpi);


                //}
            }
            public string GetColName(int index)
            {
                for (int i = 0; i < this.keyList.Count; i++)
                {
                    if (index == i)
                    {
                        return this.keyList[i].ToString();
                    }
                }
                return "";
            }
    

            public object this[string colName]
            {
                get
                {

                    for (int i = 0; i < this.keyList.Count; i++)
                    {
                        if (colName.Trim() == this.GetColName(i).ToString().Trim())
                        {
                            return this.valueList[i];
                        }
                    }
                    return "";
                }
            }

            public new object this[int index]
            {
                get
                {

                    for (int i = 0; i < this.keyList.Count; i++)
                    {
                        if (index == i)
                        {
                            return this.valueList[i];
                        }
                    }
                    return "";
                }
            }

            public object this[string rowName, string colName]
            {
                get
                {

                    for (int i = 0; i < this.Length; i++)
                    {
                        if (rowName.Trim() == this.GetColName(i).ToString().Trim())
                        {
                            Json.Jsonbject o = ((Json.Jsonbject)this[i]);
                            for (int j = 0; j < o.Length; j++)
                            {
                                if (colName.Trim() == o.GetColName(j).ToString().Trim())
                                {
                                    return o[j];
                                }
                            }

                        }
                    }
                    return "";
                }
            }

            public object this[int rowIndex, int colIndex]
            {
                get
                {

                    for (int i = 0; i < this.Length; i++)
                    {
                        if (rowIndex == i)
                        {
                            Json.Jsonbject o = (Json.Jsonbject)this[i];
                            for (int j = 0; j < o.Length; j++)
                            {
                                if (colIndex == j)
                                {
                                    return o[j];
                                }
                            }
                        }
                    }
                    return "";
                }
            }

            public object this[int rowIndex, string colName]
            {
                get
                {

                    for (int i = 0; i < this.Length; i++)
                    {
                        if (rowIndex == i)
                        {
                            Json.Jsonbject o = (Json.Jsonbject)this[i];
                            for (int j = 0; j < o.Length; j++)
                            {
                                if (colName.Trim() == o.GetColName(j).ToString().Trim())
                                {
                                    return o[j];
                                }
                            }
                        }
                    }
                    return "";
                }
            }

        }

    



        /// <summary>
        /// 类  名：JSONArray
        /// 描  述：JSON数组类
        /// 编  写：dnawo
        /// 站  点：http://www.mzwu.com/
        /// 日  期：2010-01-06
        /// 版  本：1.1.0
        /// 更新历史：
        ///     2010-01-06  继承List<T>代替this[]
        /// </summary>
        public class JsonArray : List<object>
        {

        }


    }
