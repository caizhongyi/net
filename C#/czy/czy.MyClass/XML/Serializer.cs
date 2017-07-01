using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace czy.MyClass.XML
{
    ///  <summary>
    ///  序列化与反序列化
    ///  FileName                        :        Serializer.cs      
    ///  Verion                          :              0.10
    ///  Author                          :              zhouyu   http://max.cszi.com
    ///  Update                        :              2007-10-22
    ///  Description            :              序列化与反序列化,主要用于将对象里的数据序列化成XML数据,用于存于文本或是数据库
    ///  Thanks                        :              小浩http://cs.alienwave.cn  )
    ///  </summary>
    public class Serializer
    {
        /// <summary>
        /// 读取XML文件到类对像中
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <returns>类</returns>
        public static T XMLToClass<T>(string fileName) where T : class,new()
        {
            T lc = new T();
            if (File.Exists(fileName))
            {
                lc = czy.MyClass.XML.Serializer.XmlDeserializerFormFile(lc.GetType(), fileName) as T;
            }
            else
            {
                czy.MyClass.XML.Serializer.XmlSerializerToFile(lc, fileName);
            }
            return lc;
        }
        /// <summary>
        /// 类对像保存为XML文件
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">类</param>
        /// <param name="fileName">文件路径</param>
        public static void ClassToXML<T>(T t,string fileName) where T : class,new()
        {
          
            czy.MyClass.XML.Serializer.XmlSerializerToFile(t, fileName);
           
        }
        ///  <summary>
        ///  将文件反序列化成对象
        ///  使用:BlogSettingInfo  info  =    (BlogSettingInfo)  Serializer.XmlDeserializerFormFile(typeof(BlogSettingInfo),  @"H:CSBlog.xml");
        ///  </summary>
        ///  <param  name="type">对象类型</param>
        ///  <param  name="path">文件路径</param>
        ///  <returns></returns>
        public static object XmlDeserializerFormFile(Type type, string path)
        {
            return new XmlSerializer(type).Deserialize(new XmlTextReader(path));
        }


        ///  <summary>
        ///  将字符串内容反序列化成对象
        ///  使用:BlogSettingInfo  info  =  (BlogSettingInfo)Serializer.XmlDeserializerFormText(typeof(BlogSettingInfo),config);
        ///  </summary>
        ///  <param  name="type">对象类型</param>
        ///  <param  name="serializeText">被序列化的文本</param>
        ///  <returns></returns>
        public static object XmlDeserializerFormText(Type type, string serializeText)
        {
            using (StringReader reader = new StringReader(serializeText))
            {
                return new XmlSerializer(type).Deserialize(reader);
            }
        }


        ///  <summary>
        ///  将目标对象序列化成XML到文件中
        ///  </summary>
        ///  <param  name="target"></param>
        ///  <param  name="path"></param>
        public static void XmlSerializerToFile(object target, string path)
        {
            //XmlTextWriter  writer  =  new  XmlTextWriter(path,  Encoding.UTF8);
            try
            {
                StreamWriter writer = new StreamWriter(path);
                new XmlSerializer(target.GetType()).Serialize((StreamWriter)writer, target);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ///  <summary>
        ///  将目标对象序列化成完整的XML文档
        ///  </summary>
        ///  <param  name="target"></param>
        ///  <returns></returns>
        public static string XmlSerializerToXml(object target)
        {
            return XmlSerializerToText(target, false);
        }


        ///  <summary>
        ///  将目标对象序列化成XML文档内容(去除声明属性)
        ///  </summary>
        ///  <param  name="target"></param>
        ///  <returns></returns>
        public static string XmlSerializerToText(object target)
        {
            return XmlSerializerToText(target, true);
        }


        ///  <summary>
        ///  将目标对象序列化成XML文档
        ///  </summary>
        ///  <param  name="target"></param>
        ///  <param  name="isText">是否去除声明属性</param>
        ///  <returns></returns>
        private static string XmlSerializerToText(object target, bool isText)
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(target.GetType()).Serialize((TextWriter)writer, target);
            StringBuilder sb = writer.GetStringBuilder();
            writer.Close();
            if (isText)
            {
                sb.Replace("<?xml  version=\"1.0\"  encoding=\"utf-16\"?> ", "");
                sb.Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            }
            else
            {
                sb.Replace("utf-16", "utf-8");
            }
            return sb.ToString();
        }

    }

}
