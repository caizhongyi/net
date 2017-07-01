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

namespace czy.Silverlight.StoryBoard
{
    public class TemplatedControl1 : Control
    {
        Button btn;
        SolidColorBrush solid;
        public TemplatedControl1()
        {
            this.DefaultStyleKey = typeof(TemplatedControl1);
  
        }
        public override void OnApplyTemplate()
        {
            solid = base.GetTemplateChild("btn_solid") as SolidColorBrush;
            btn = base.GetTemplateChild("btn") as Button;
            btn.Click += new RoutedEventHandler(btn_Click);
            
        }

        public void btn_Click(object o,EventArgs e)
        {
           
        }

        private void AnimationDemo()
        {

           
        }
    }
}
