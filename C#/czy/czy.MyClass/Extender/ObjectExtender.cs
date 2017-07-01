using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


public static class ObjectExtender
{
    /// <summary>
    ///  sql参数未验证的单引号替换
    /// </summary>
    /// <param name="o">对像</param>
    /// <returns>返回结果</returns>
    public static object ToSQLSafe(this object o)
    {
        //object o = Activator.CreateInstance(type);
        //foreach (PropertyInfo pi in t.GetProperties())
        //{
        //    TranToSQL(pi);
        //}
        Type type = o.GetType();
        PropertyInfo[] pi = type.GetProperties();
        for (int i = 0; i < pi.Length; i++)
        {
            if (pi[i].GetValue(o, null) != null)
            {
                string s = pi[i].GetValue(o, null).ToString();
                TypeCode t = Type.GetTypeCode(pi[i].PropertyType);
                if (t == TypeCode.String)
                {
                    pi[i].SetValue(o, s as object, null);
                }
            }
        }
        //return o.ToString().Replace ("'","''");
        return o;
    }
}
   

