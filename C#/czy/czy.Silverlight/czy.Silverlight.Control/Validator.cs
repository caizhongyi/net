using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using czy.Silverlight.StoryBoard;

namespace czy.Silverlight.Controls
{
    public class Validator : Control
    {

        public Validator()
        {
            this.DefaultStyleKey = typeof(Validator);
        }
        string _value = string.Empty;
        bool allowEmpty = true;

        DisplayAttribute dispalyAttr;
        RegularExpressionAttribute rea;
        StringLengthAttribute sla;

        public string SourceFildName
        {
            get { return (string)base.GetValue(SourceFildNameProperty); }
            set { base.SetValue(SourceFildNameProperty, value); }
        }
        public static readonly DependencyProperty SourceFildNameProperty = DependencyProperty.Register(
            "SourceFildName",
            typeof(string),
            typeof(Validator),
            new PropertyMetadata("")
          );

        public Type SourceModelType
        {
            get { return (Type)base.GetValue(SourceModelProperty); }
            set { base.SetValue(SourceModelProperty, value); }
        }
        public static readonly DependencyProperty SourceModelProperty = DependencyProperty.Register(
            "SourceModel",
            typeof(Type),
            typeof(Validator),
            new PropertyMetadata(new object())
          );

        #region  关联对像
        public TextBox AsscociateObj
        {
            get { return (TextBox)base.GetValue(AsscociateObjProperty); }
            set { base.SetValue(AsscociateObjProperty, value); }
        }
        public static readonly DependencyProperty AsscociateObjProperty = DependencyProperty.Register(
            "AsscociateObj",
            typeof(TextBox),
            typeof(Validator),
            new PropertyMetadata(new TextBox())
          );
        #endregion

        public Boolean AllowEmpty
        {
            get { return (Boolean)base.GetValue(AllowEmptyProperty); }
            set { base.SetValue(AllowEmptyProperty, value); }
        }
        public static readonly DependencyProperty AllowEmptyProperty = DependencyProperty.Register(
            "AllowEmpty",
            typeof(Boolean),
            typeof(Validator),
            new PropertyMetadata(true)
          );
   
        /// <summary>
        /// 载入模版
        /// </summary>
        public override void OnApplyTemplate()
        {

           // object obj = AsscociateObj.GetBindingExpression(TextBox.TextProperty).ParentBinding.Source;
            //object obj = AsscociateObj.GetBindingExpression(TextBox.TextProperty).ParentBinding.Path;
          //  object obj = SourceModel; 
            //_value = SourceModelType.GetProperty(SourceFildName).GetValue(obj, null).ToString();
            object[] attrs = SourceModelType.GetProperty(SourceFildName).GetCustomAttributes(false);
            foreach (object attr in attrs)
            {
                if (attr is DisplayAttribute)
                {
                    dispalyAttr = (attr as DisplayAttribute);
                }
                if (attr is RegularExpressionAttribute)
                {
                    rea = (attr as RegularExpressionAttribute);
                }
                if (attr is StringLengthAttribute)
                {
                    sla = attr as StringLengthAttribute;
                }

            }
            AsscociateObj.LostFocus += new RoutedEventHandler(Element_LostFocus);
            AsscociateObj.GotFocus += new RoutedEventHandler(Element_GotFocus);
        }

        void Element_GotFocus(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            TextBlock t = base.GetTemplateChild("Resoult") as TextBlock;
            Border b = base.GetTemplateChild("border") as Border;
            BaseStoryBoardBuilder.OpacityStoryBoard(element, 0).Begin();
            BaseStoryBoardBuilder.MoveStoryBoardByTrans((TranslateTransform)b.RenderTransform, new Point(-20, 0)).Begin();

        }

        void Element_LostFocus(object sender, RoutedEventArgs e)
        { 
            FrameworkElement element = sender as FrameworkElement;
            TextBlock t = base.GetTemplateChild("Resoult") as TextBlock;
            Border b=base.GetTemplateChild("border") as Border;
           
            if (rea != null)
            {
                Regex regex = new Regex( rea.Pattern);
                if(!regex.IsMatch(_value))
                {
                   t.Text = rea.ErrorMessage;
                   czy.Silverlight.StoryBoard.BaseStoryBoardBuilder.OpacityStoryBoard(element, 1).Begin();
                   czy.Silverlight.StoryBoard.BaseStoryBoardBuilder.MoveStoryBoardByTrans((TranslateTransform)b.RenderTransform, new Point(0, 0)).Begin();

                }
            }

         
        }
    }
}
