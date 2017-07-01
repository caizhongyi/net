using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace czy.Silverlight.Library
{

    public class StyleManager
    {
        /// <summary>
        /// 动态加载样式文件
        /// </summary>
        /// <param name="url">样式文件路径</param>
        /// <param name="assemblyName">程序集名称</param>
        public static void  AddStyleLink(string url,string assemblyName)
        {
            //Uri uri = new Uri(url, UriKind.Relative);
            //ImplicitStyleManager.SetResourceDictionaryUri(control, uri);
            //ImplicitStyleManager.SetApplyMode(control, ImplicitStylesApplyMode.Auto);
            //ImplicitStyleManager.Apply(control);
            Uri uri = new Uri(url, UriKind.Relative);
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new Uri("/" + assemblyName + ";component/" + uri, UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(rd);

        }   
        /// <summary>
        /// 动态加载样式文件
        /// </summary>
        /// <param name="url">样式文件路径</param>
        public static void AddStyleLink(string url)
        {
            //Uri uri = new Uri(url, UriKind.Relative);
            //ImplicitStyleManager.SetResourceDictionaryUri(control, uri);
            //ImplicitStyleManager.SetApplyMode(control, ImplicitStylesApplyMode.Auto);
            //ImplicitStyleManager.Apply(control);
            Uri uri = new Uri(url, UriKind.Relative);
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = uri;
            Application.Current.Resources.MergedDictionaries.Add(rd);

        }
        /// <summary>
        /// 设置样式(些只适用于全局样式,局部控件样式请用this.Resources[styleName])
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="styleName">样式名称</param>
        public static void SetStyle(FrameworkElement element,string styleName)
        {
            element.Style=  Application.Current.Resources[styleName] as Style;
        }
    }
}
