using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace czy.MyClass.COMAccess
{
    /// <summary>
    /// IISCOM 的摘要说明
    /// IISCOM 组件访问类
    /// </summary>
    public sealed partial class IISCOMAccess
    {
        #region 成员
        private System.Type oType;

        public System.Type OType
        {
            get { return oType; }
        }
        #endregion

        /// <summary>
        /// 初始化ISSCOM类
        /// </summary>
        /// <param name="nameSpace_className">空间名+类名</param>
        public IISCOMAccess(string nameSpace_className)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            oType = System.Type.GetTypeFromProgID(nameSpace_className);

        }

        #region 方法
        /// <summary>
        /// 调用IIS注册组件方法(有返回值)
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="Obj">参数数组</param>
        /// <returns>返回值</returns>
        public object ReIISCOMGetMethod(string methodName, object[] param)
        {
            object o = System.Activator.CreateInstance(oType);
            object returnValue = oType.InvokeMember(methodName, System.Reflection.BindingFlags.InvokeMethod, null, o, param);
            return returnValue;
        }

        /// <summary>
        /// 调用IIS注册组件方法(无返回值)
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="Obj">参数数组</param>
        public void IISCOMGetMethod(string methodName, object[] param)
        {
            object o = System.Activator.CreateInstance(oType);
            oType.InvokeMember(methodName, System.Reflection.BindingFlags.InvokeMethod, null, o, param);
        }

        /// <summary>
        /// 调用IIS注册组件方法(有返回值)
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="Obj">参数数组</param>
        /// <returns>返回值</returns>
        public object ReIISCOMGetStaticMethod(string methodName, object[] param)
        {
            object returnValue = oType.InvokeMember(methodName, System.Reflection.BindingFlags.InvokeMethod, null, null, param);
            return returnValue;
        }

        /// <summary>
        /// 调用IIS注册组件方法(无返回值)
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="Obj">参数数组</param>
        public void IISCOMGetStaticMethod(string methodName, object[] param)
        {
            oType.InvokeMember(methodName, System.Reflection.BindingFlags.InvokeMethod, null, null, param);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取IIS注册组件属性值
        /// </summary>
        /// <param name="methodName">属性名称</param>
        public object IISCOMGetProperty(string propertyName)
        {
            object o = System.Activator.CreateInstance(oType);
            object value = oType.InvokeMember(propertyName, System.Reflection.BindingFlags.GetProperty, null, o, null);
            return value;
        }

        /// <summary>
        /// 设置IIS注册组件属性值
        /// </summary>
        /// <param name="methodName">属性名称</param>
        /// <param name="Obj">参数数组</param>
        public void IISCOMSetProperty(string propertyName, object[] param)
        {
            object o = System.Activator.CreateInstance(oType);
            oType.InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, o, param);
        }

        /// <summary>
        /// 获取IIS注册组件属性值
        /// </summary>
        /// <param name="methodName">属性名称</param>
        public object IISCOMStaticGetProperty(string propertyName)
        {
            object value = oType.InvokeMember(propertyName, System.Reflection.BindingFlags.GetProperty, null, null, null);
            return value;
        }

        /// <summary>
        /// 设置IIS注册组件属性值
        /// </summary>
        /// <param name="methodName">属性名称</param>
        /// <param name="Obj">参数数组</param>
        public void IISCOMStaticSetProperty(string propertyName, object[] param)
        {
            oType.InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, null, param);
        }
        #endregion
    }

}