using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace czy.MyClass.COMAccess
{
    /// <summary>
    /// 接口访问
    /// </summary>
    public sealed partial class InterfaceAccess
    {
        /// <summary>
        /// 根据ClassName创建类
        /// </summary>
        /// <param name="className">组件路径</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名称</param>
        /// <returns>返回的结果为接口类型</returns>
        public static object CreateObject(string assemblyPath, string nameSpace, string className)
        {
            try
            {
                return Assembly.Load(assemblyPath).CreateInstance(nameSpace + "." + className);
            }
            catch
            {
                return null;
            }
        }
    }
}
