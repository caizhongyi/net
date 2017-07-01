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
using System.Windows.Data;
using System.Collections.Generic;

namespace czy.Silverlight.Library.Data
{
    public class ValidateHelper
    {
        /// <summary>
        /// 验证Validator自更新验证.
        /// </summary>
        /// <param name="controls">控件集合</param>
        /// <param name="propertys">属性集合</param>
        /// <returns>其上有错返回false</returns>
        public static bool BindingExpressionUpdate(List<Control> controls,List<DependencyProperty> propertys)
        {
           bool resoult=true ;
           for(int i=0;i<controls.Count;i++)
           {
             BindingExpression beUsername = controls[i].GetBindingExpression(propertys[i]);
             beUsername.UpdateSource();
             if (Validation.GetHasError(controls[i]))
             {
                 resoult = false;
             }
           }
           return resoult;
        }
        /// <summary>
        /// 验证Validator自更新验证.
        /// </summary>
        /// <param name="controls">控件集合</param>
        /// <param name="propertys">属性集合</param>
        /// <returns>其上有错返回false</returns>
        public static bool BindingExpressionUpdate(List<Control> controls, List<DependencyProperty> propertys, int errorCount)
        {
            bool resoult = true;
            for (int i = 0; i < controls.Count; i++)
            {
                BindingExpression beUsername = controls[i].GetBindingExpression(propertys[i]);
                beUsername.UpdateSource();
                if (Validation.GetHasError(controls[i]))
                {
                    resoult = false;
                    errorCount++;
                }
            }
            return resoult;
        }
    }
}
