using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;

namespace czy.MyClass.COMAccess
{
    /// <summary>
    /// ReflectionAssembly 的摘要说明
    ///动态访问程序集
    /// </summary>
    public class AssemblyReflection
    {
        #region 成员
        private System.Reflection.Assembly ass;

        public System.Reflection.Assembly Ass
        {
            get { return ass; }
        }
        private Type type;

        public Type Type
        {
            get { return type; }
        }
        private object obj;
        //Interface
        public object Obj
        {
            get { return obj; }
        }
        private string _DllPath;//dll路径

        public string Path
        {
            get { return _DllPath; }
        }
        private string _nameSpace_className;//命空间+类名

        public string NameSpace_className
        {
            get { return _nameSpace_className; }
        }
        #endregion

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="DllPath">组件所在路径</param>
        /// <param name="nameSpace_className">命名空间</param>
        public AssemblyReflection(string path,string space_className)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            _DllPath = path;
            _nameSpace_className = space_className;
	    ass = System.Reflection.Assembly.Load(_DllPath);
            //ass = System.Reflection.Assembly.LoadFile(_DllPath);
            type = ass.GetType(_nameSpace_className);//必须使用名称空间+类名称
            obj = ass.CreateInstance(space_className);//必须使用名称空间+类名称
        }

        #region 实例方法调用
        /// <summary>
        /// 实例方法调用(有返回值)
        /// </summary>
        /// <param name="instanceMethodName">方法名称</param>
        /// <param name="param">参数</param>
        /// <returns>返回值</returns>
        public object RunMethod(string instanceMethodName, object[] param)
        {
            object strObj;
            try
            {
                System.Reflection.MethodInfo method = type.GetMethod(instanceMethodName);//方法的名称
                strObj = method.Invoke(obj, param); //实例方法的调用
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strObj;
        }
        /// <summary>
        /// 实例方法调用(有返回值)
        /// </summary>
        /// <param name="instanceMethodName">方法名称</param>
        /// <returns>返回值</returns>
        public object RunMethod(string instanceMethodName)
        {

            object strObj;
            try
            {
                System.Reflection.MethodInfo method = type.GetMethod(instanceMethodName);//无参数的实例方法
                strObj = method.Invoke(obj, null);//实例方法的调用
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strObj;

        }
      

        #endregion

        #region 静态方法调用
        /// <summary>
        /// 静态方法调用(有返回值)
        /// </summary>
        /// <param name="staticMethodName">方法名称</param>
        /// <param name="param">参数</param>
        /// <returns>返回值</returns>
        public object RunStaticMethod(string staticMethodName, object[] param)
        {
            object strObj;
            try
            {
                System.Reflection.MethodInfo method = type.GetMethod(staticMethodName);//方法的名称
                strObj = method.Invoke(null, param); //静态方法的调用
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strObj;
        }
        /// <summary>
        /// 静态方法调用(有返回值)
        /// </summary>
        /// <param name="staticMethodName">方法名称</param>
        /// <returns>返回值</returns>
        public object RunStaticMethod(string staticMethodName)
        {
            object strObj;
            try
            {
                System.Reflection.MethodInfo method = type.GetMethod(staticMethodName);//无参数的静态方法
                strObj = method.Invoke(null, null); //静态方法的调用
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strObj;
        }
      
        #endregion

        #region 属性
        /// <summary>
        /// 静态获取属性
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public object GetStaticProperty(string propertyName)
        {
            return type.InvokeMember(propertyName, System.Reflection.BindingFlags.GetProperty, null, null, null);
        }
        /// <summary>
        /// 静态设置属性
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public void GetStaticProperty(string propertyName, object[] param)
        {
            type.InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, null, param);
        }
        /// <summary>
        /// 非静态获取属性
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public object GetProperty(string propertyName)
        {
            Object obj = type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            return type.InvokeMember(propertyName, System.Reflection.BindingFlags.GetProperty, null, obj, null);
        }
        /// <summary>
        /// 非静态设置属性
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public void GetProperty(string propertyName, object[] param)
        {
            Object obj = type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            type.InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, obj, param);
        }
        #endregion
    }

}