using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.MyClass.Web.UI
{
    public class FormTable
    {
        //public enum FormTableType
        //{
        //    HTML,
        //    Auto
        //}
        object o=new object ();
        public FormTable()
        { }
        public FormTable(object o)
        {
            this.o = o;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmls">name:xxx;bind:xxxx;html:xxx</param>
        /// <returns></returns>
        public String CreateFromTable(string[] htmls)
        {
            StringBuilder sb = new StringBuilder();
            Type t = o.GetType();

            System.Reflection.PropertyInfo[] p = t.GetProperties();
            foreach (string html in htmls)
            {
                String[] row = html.Split(';');
                
                string[] fild1= row[0].Split(':');
                string[] fild2= row[1].Split(':');
                string[] fild3= row[2].Split(':');

                sb.AppendLine("<div>" + fild1[1] + "</div>");
                sb.AppendLine("<div>" + fild1[1] + "</div>");
            }
            return sb.ToString();
        }
    }
}
