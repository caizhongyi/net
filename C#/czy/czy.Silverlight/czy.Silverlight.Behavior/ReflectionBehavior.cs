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
using System.Windows.Interactivity;
using System.Windows.Automation.Peers;
using System.Windows.Data;

namespace czy.Silverlight.Behaviors
{
    public class ReflectionBehavior : Behavior<FrameworkElement>
    {
        public FrameworkElement _reflectionObj;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject != null)
            {
            
                CreateReflectProperty();
                Binding left = new Binding();
                Binding top = new Binding();
                left.Source = Canvas.GetLeft(this.AssociatedObject);
                top.Source = Canvas.GetTop(this.AssociatedObject);
                _reflectionObj.SetBinding(Canvas.LeftProperty, left);
                _reflectionObj.SetBinding(Canvas.LeftProperty, top);
                //Canvas.SetLeft(_reflectionObj,
                this.AssociatedObject.Opacity = 0.4; 
             
            }

            //LoadChildScrollViewer();
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();

            //if (this.ScrollObject != null)
            //{
            //    this.ScrollObject.MouseWheel -= scrollObject_MouseWheel;
            //}
        }

        private void  CreateReflectProperty()
        {
            var peer = FrameworkElementAutomationPeer.CreatePeerForElement(_reflectionObj);
            TransformGroup tg = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            SkewTransform stf = new SkewTransform();
            TranslateTransform tt = new TranslateTransform();

            st.ScaleX = -0.75;
            stf.AngleX = -15;
            tt.X = -30;
            tt.Y = this.AssociatedObject.Height;

            tg.Children.Add(st);
            tg.Children.Add(stf);
            tg.Children.Add(tt);
            _reflectionObj.RenderTransform = tg;

            LinearGradientBrush lgb = new LinearGradientBrush();
            lgb.StartPoint = new Point(0.5, 0.0);
            lgb.EndPoint = new Point(0.5, 1.0);
            GradientStop gs = new GradientStop();
            gs.Offset = 0;
            gs.Color = Colors.White;
            GradientStop gs1 = new GradientStop();
            gs.Offset = 0;
            gs.Color = Colors.Black;
            lgb.GradientStops.Add(gs);
            lgb.GradientStops.Add(gs1);
            _reflectionObj.OpacityMask = lgb;
            if (_reflectionObj.Parent != null)
            {
            //   VisualTreeHelper.GetParent(_reflectionObj).Children.Add (_reflectionObj);
            }
        }
    }
}
