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

namespace czy.Silverlight.Behaviors
{
    public class MouseMoveOpacitySizeBehavior : Behavior<FrameworkElement>
    {
        private double tempValue;
        private Size tempSize;
        private DependencyObject tagObject;

        public double OpactiyValue
        {
            get { return (double)GetValue(OpactiyValueProperty); }
            set { SetValue(OpactiyValueProperty, value); }
        }

        public Size SizeValue
        {
            get { return (Size)GetValue(SizeValueProperty); }
            set { SetValue(SizeValueProperty, value); }
        }
        //public String ObjectValue
        //{
        //    get { return (String)GetValue(ObjectValueProperty); }
        //    set { SetValue(ObjectValueProperty, value); }
        //}

        //public static readonly DependencyProperty ObjectValueProperty = DependencyProperty.Register(
        //  "ObjectValue",
        //  typeof(String),
        //  typeof(OpactiyBehavior),
        //  new PropertyMetadata(null)
        //);

        public static readonly DependencyProperty SizeValueProperty = DependencyProperty.Register(
            "SizeValue",
            typeof(Size),
            typeof(MouseMoveOpactiyBehavior),
            new PropertyMetadata(new Size(100, 100))
          );

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
                
                tempValue =this.AssociatedObject.Opacity;
                tempSize = new Size(this.AssociatedObject.Width, this.AssociatedObject.Height);
                this.AssociatedObject.MouseLeave += new MouseEventHandler(AssociatedObject_MouseLeave);
                this.AssociatedObject.MouseEnter += new MouseEventHandler(AssociatedObject_MouseEnter);
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
            //StoryBoard.StoryBoardBuilder.GetOpactiyStoryBoard(f, OpactiyValue, tempValue).Begin();
            StoryBoard.StoryBoardBuilder.GetOpactiySizeStoryBoard(f, OpactiyValue, tempValue, tempSize, SizeValue).Begin();
        }
        public void AssociatedObject_MouseEnter(object o, EventArgs e)
        {
            FrameworkElement f = o as FrameworkElement;
           // StoryBoard.StoryBoardBuilder.GetOpactiyStoryBoard(f, tempValue, OpactiyValue).Begin();
            StoryBoard.StoryBoardBuilder.GetOpactiySizeStoryBoard(f, tempValue, OpactiyValue, SizeValue, tempSize).Begin();
        }
    }
}
