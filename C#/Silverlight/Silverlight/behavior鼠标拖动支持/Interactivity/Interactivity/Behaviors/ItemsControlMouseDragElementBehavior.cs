
namespace Interactivity.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Media;

    public class ItemsControlMouseDragElementBehavior : Behavior<ItemsControl>
    {
        #region Fields

        private bool isDragging;
        private bool isMouseDown;
        private FrameworkElement itemContainer;
        private Point lastPoint;
        private FrameworkElement parent;
        private bool settingPosition;

        #endregion Fields

        #region Properties

        private FrameworkElement ParentElement
        {
            get
            {
                return parent;
            }
        }

        #endregion Properties

        #region Methods

        public static object GetDataObjectFromItemsControl(ItemsControl itemsControl, Point p)
        {
            UIElement element = itemsControl.InputHitTest(p) as UIElement;
            while (element != null)
            {
                if (element == itemsControl)
                    return null;

                object data = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
                else
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }
            }
            return null;
        }

        public static FrameworkElement GetItemContainerFromPoint(ItemsControl itemsControl, Point p)
        {
            FrameworkElement element = itemsControl.InputHitTest(p) as FrameworkElement;
            while (element != null)
            {
                if (element == itemsControl)
                    return null;

                object data = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
                if (data != DependencyProperty.UnsetValue)
                {
                    return element;
                }
                else
                {
                    element = VisualTreeHelper.GetParent(element) as FrameworkElement;
                }
            }
            return null;
        }

        internal static Rect GetLayoutRect(FrameworkElement element)
        {
            double actualWidth = element.ActualWidth;
            double actualHeight = element.ActualHeight;
            if ((element is Image) || (element is MediaElement))
            {
                if (element.Parent.GetType() == typeof(Canvas))
                {
                    actualWidth = double.IsNaN(element.Width) ? actualWidth : element.Width;
                    actualHeight = double.IsNaN(element.Height) ? actualHeight : element.Height;
                }
                else
                {
                    actualWidth = element.RenderSize.Width;
                    actualHeight = element.RenderSize.Height;
                }
            }
            actualWidth = (element.Visibility == Visibility.Collapsed) ? 0.0 : actualWidth;
            actualHeight = (element.Visibility == Visibility.Collapsed) ? 0.0 : actualHeight;
            Thickness margin = element.Margin;
            Rect layoutSlot = LayoutInformation.GetLayoutSlot(element);
            double x = 0.0;
            double y = 0.0;
            switch (element.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = layoutSlot.Left + margin.Left;
                    break;

                case HorizontalAlignment.Center:
                    x = ((((layoutSlot.Left + margin.Left) + layoutSlot.Right) - margin.Right) / 2.0) - (actualWidth / 2.0);
                    break;

                case HorizontalAlignment.Right:
                    x = (layoutSlot.Right - margin.Right) - actualWidth;
                    break;

                case HorizontalAlignment.Stretch:
                    x = Math.Max((double)(layoutSlot.Left + margin.Left), (double)(((((layoutSlot.Left + margin.Left) + layoutSlot.Right) - margin.Right) / 2.0) - (actualWidth / 2.0)));
                    break;
            }
            switch (element.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = layoutSlot.Top + margin.Top;
                    break;

                case VerticalAlignment.Center:
                    y = ((((layoutSlot.Top + margin.Top) + layoutSlot.Bottom) - margin.Bottom) / 2.0) - (actualHeight / 2.0);
                    break;

                case VerticalAlignment.Bottom:
                    y = (layoutSlot.Bottom - margin.Bottom) - actualHeight;
                    break;

                case VerticalAlignment.Stretch:
                    y = Math.Max((double)(layoutSlot.Top + margin.Top), (double)(((((layoutSlot.Top + margin.Top) + layoutSlot.Bottom) - margin.Bottom) / 2.0) - (actualHeight / 2.0)));
                    break;
            }
            return new Rect(x, y, actualWidth, actualHeight);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectPreviewMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObjectPreviewMouseLeftButtonDown;
        }

        private static Point GetTransformOffset(GeneralTransform transform)
        {
            return transform.Transform(new Point(0.0, 0.0));
        }


        private static Point TransformAsVector(GeneralTransform transform, double x, double y)
        {
            Point point = transform.Transform(new Point(0.0, 0.0));
            Point point2 = transform.Transform(new Point(x, y));
            return new Point(point2.X - point.X, point2.Y - point.Y);
        }

        private void ApplyTranslation(double x, double y)
        {
            if (this.ParentElement == null) return;
            Point point = TransformAsVector(this.itemContainer.TransformToVisual(this.ParentElement), x, y);
            x = point.X;
            y = point.Y;
            this.ApplyTranslationTransform(x, y);
        }

        private void ApplyTranslationTransform(double x, double y)
        {
            Transform renderTransform = itemContainer.RenderTransform;
            TransformGroup group = renderTransform as TransformGroup;
            MatrixTransform transform2 = renderTransform as MatrixTransform;
            TranslateTransform transform3 = renderTransform as TranslateTransform;
            if (transform3 == null)
            {
                if (group != null)
                {
                    if (group.Children.Count > 0)
                    {
                        transform3 = group.Children[group.Children.Count - 1] as TranslateTransform;
                    }
                    if (transform3 == null)
                    {
                        transform3 = new TranslateTransform();
                        group.Children.Add(transform3);
                    }
                }
                else
                {
                    if (transform2 != null)
                    {
                        Matrix matrix = transform2.Matrix;
                        matrix.OffsetX += x;
                        matrix.OffsetY += y;
                        MatrixTransform transform4 = new MatrixTransform
                                                         {
                                                             Matrix = matrix
                                                         };
                        itemContainer.RenderTransform = transform4;
                        return;
                    }
                    TransformGroup group2 = new TransformGroup();
                    transform3 = new TranslateTransform();
                    if (renderTransform != null)
                    {
                        group2.Children.Add(renderTransform);
                    }
                    group2.Children.Add(transform3);
                    itemContainer.RenderTransform = group2;
                }
            }
            transform3.X += x;
            transform3.Y += y;
        }

        private void AssociatedObjectPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))) return;

            ItemsControl itemsControl = (ItemsControl)sender;
            Point p = e.GetPosition(itemsControl);
            itemContainer = GetItemContainerFromPoint(itemsControl, p);
            if (itemContainer != null)
            {
                isMouseDown = true;
                lastPoint = p;
                this.parent = VisualTreeHelper.GetParent(itemContainer) as FrameworkElement;
                this.AssociatedObject.CaptureMouse();
                this.AssociatedObject.PreviewMouseMove += AssociatedObjectPreviewMouseMove;
                this.AssociatedObject.PreviewMouseLeftButtonUp += AssociatedObjectPreviewMouseLeftButtonUp;
            }
        }

        private void AssociatedObjectPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            isDragging = false;
            this.AssociatedObject.ReleaseMouseCapture();
            this.AssociatedObject.PreviewMouseMove -= AssociatedObjectPreviewMouseMove;
            this.AssociatedObject.PreviewMouseLeftButtonUp -= AssociatedObjectPreviewMouseLeftButtonUp;
        }

        private void AssociatedObjectPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                ItemsControl itemsControl = (ItemsControl)sender;
                Point position = e.GetPosition(itemsControl);
                double x = position.X - this.lastPoint.X;
                double y = position.Y - this.lastPoint.Y;
                this.lastPoint = position;

                if ((isDragging == false) &&
                    (Math.Abs(x) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    isDragging = true;
                }

                if (isDragging)
                {
                    this.settingPosition = true;
                    this.ApplyTranslation(x, y);
                    this.UpdatePosition();
                    this.settingPosition = false;
                }
            }
        }

        private void UpdatePosition()
        {
            Point transformOffset = GetTransformOffset(itemContainer.TransformToVisual(this.AssociatedObject));
            //TODO: add support for items to read their current X Y positions using attached properties 
            //this.X = transformOffset.X;
            //this.Y = transformOffset.Y;
        }

        #endregion Methods
    }
}