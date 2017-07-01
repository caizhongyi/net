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
    public class AttributeHelper
    {
        public static object[] GetAttributes(object obj,string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetCustomAttributes(false);
        }
        public static object[] GetAttributes(object obj)
        {
            return  obj.GetType().GetCustomAttributes(false);
        }
    }
}
