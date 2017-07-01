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
using System.Windows.Automation.Provider;
using System.ComponentModel;
using czy.Silverlight.StoryBoard;

namespace czy.Silverlight.Behaviors
{
    public class MouseMoveOpactiyBehavior : Behavior<FrameworkElement>
    {
        
        //private UIElement _element = null;
        //private UIElement Element
        //{
        //    get { return _element; }
        //    set
        //    {
        //        if (_element != value)
        //        {
        //            _element = value;
        //            if (_element != null)
        //            {
        //                var peer = FrameworkElementAutomationPeer.CreatePeerForElement(_element);
        //               // _scrollProvider = peer.GetPattern(PatternInterface.Scroll) as Iu;
        //            }
        //            else
        //            {
        //               // _scrollProvider = null;
        //            }
        //        }
        //    }
        //}
        private double tempValue;
 
        public double OpactiyValue
        {
            get { return (double)GetValue(OpactiyValueProperty); }
            set { SetValue(OpactiyValueProperty, value); }
        }



        public static readonly DependencyProperty OpactiyValueProperty = DependencyProperty.Register(
            "OpactiyValue",
            typeof(double),
            typeof(MouseMoveOpactiyBehavior),
            new PropertyMetadata(0.5)
          );
        public static void SetOpactiyValue(UIElement element, Boolean value)
        {
            element.SetValue(OpactiyValueProperty, value);
        }
        public static double GetOpactiyValue(UIElement element)
        {
            return (double)element.GetValue(OpactiyValueProperty);
        }
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject != null)
            {
                tempValue = this.AssociatedObject.Opacity;
                this.AssociatedObject.MouseLeave+=new MouseEventHandler(AssociatedObject_MouseLeave);
                this.AssociatedObject.MouseEnter+=new MouseEventHandler(AssociatedObject_MouseEnter);
            }

            //LoadChildScrollViewer();
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
        private void AssociatedObject_MouseLeave(object o, EventArgs e)
        {
            FrameworkElement f = o as FrameworkElement;
            StoryBoardBuilder.GetOpactiyStoryBoard(f, OpactiyValue, tempValue).Begin();
        }
        public void AssociatedObject_MouseEnter(object o, EventArgs e)
        {
            FrameworkElement f = o as FrameworkElement;
            StoryBoardBuilder.GetOpactiyStoryBoard(f, tempValue, OpactiyValue).Begin();
        }
    }
}
