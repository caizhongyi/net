using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


public static class ClassExtender
{
    /// <summary>
    /// 获取属性
    /// </summary>
    /// <typeparam name="ClassT">类型</typeparam>
    /// <param name="t">类</param>
    /// <param name="propertyName">属性名称</param>
    /// <returns>PropertyInfo</returns>
    public static PropertyInfo GetProperty<ClassT>(this ClassT t, string propertyName) where ClassT : class,new()
    {
        foreach (PropertyInfo p in t.GetType().GetProperties())
        {
            if (p.Name == propertyName)
            {
                return p;
            }
        }
        return null;
    }
}

