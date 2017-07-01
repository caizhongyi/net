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
    /// <summary>
    /// 元素关系类
    /// </summary>
    public class ElementAssociated
    { 
        /// <summary>
        /// 获取父级元素
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static  UIElement GetParentElement(DependencyObject obj)
        {
            return VisualTreeHelper.GetParent(obj) as UIElement;
        }
    }
}
