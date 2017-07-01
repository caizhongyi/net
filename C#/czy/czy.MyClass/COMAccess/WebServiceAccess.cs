using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using Microsoft.CSharp;
using System.Reflection;

namespace czy.MyClass.COMAccess
{
    /// <summary>
    /// Web服务访问类
    /// </summary>
    public sealed partial class WebServiceAccess
    {
        public WebServiceAccess()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region InvokeWebService
        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">Web服务地址</param>
        /// <param name="methodname">方法名称</param>
        /// <param name="args">参数</param>
        /// <returns>返回结果</returns>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return WebServiceAccess.InvokeWebService(url, null, methodname, args);
        }
        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">Web服务地址</param>
        /// <param name="classname">类名称</param>
        /// <param name="methodname">方法名称</param>
        /// <param name="args">参数</param>
        /// <returns>返回结果</returns>
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "nchost";
            if ((classname == null) || (classname == ""))
            {
                classname = WebServiceAccess.GetWsClassName(url);
            }

            try
            {
                //获取WSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();

                //设定编译参数
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);



                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                //object o1 = mi.Invoke(obj, args);
                //object obj1 = Activator.CreateInstance(o1.GetType());
                return mi.Invoke(obj, args);
                //PropertyInfo pInfo = o1.GetType().GetProperty("Result");
                //return pInfo.GetValue(obj1, null);
                ////return mi.Invoke(obj, args).GetType().GetProperty("Result").GetValue(mi.Invoke(obj, args),null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }
        /// <summary>
        /// 获取类名
        /// </summary>
        /// <param name="wsUrl"></param>
        /// <returns></returns>
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        }
        #endregion
    }

}
