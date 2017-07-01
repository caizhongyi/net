using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DeepEarth;
//using DeepEarth.Behaviors;
using DeepEarth.Events;
using DeepEarth.Geometry;

namespace DeepEarthPrototype.Controls
{
    [TemplateVisualState(GroupName = "DisplayStates", Name = "DisplayOnTopRight")]
    [TemplateVisualState(GroupName = "DisplayStates", Name = "DisplayOnTopLeft")]
    [TemplateVisualState(GroupName = "DisplayStates", Name = "DisplayOnBottomRight")]
    [TemplateVisualState(GroupName = "DisplayStates", Name = "DisplayOnBottomLeft")]
    [TemplatePart(Name = PART_CloseButton, Type = typeof (ButtonBase))]
    public class InfoBox : ContentControl
    {
        public const string PART_CloseButton = "PART_CloseButton";
        private static readonly InfoBox instance = new InfoBox();
        private readonly DispatcherTimer activeTimer;

        private readonly Popup popup;
        private ButtonBase closeButton;
        private GeometryBase currentShape;
        private IInfoBoxContent infoBoxContent;
        private int version;

        private static InfoBoxEventBehavior _InfoBoxEventBehavior;

        public InfoBox()
        {
            DefaultStyleKey = typeof (InfoBox);
            IsTabStop = false;

            activeTimer = new DispatcherTimer();
            activeTimer.Tick += OnActiveTimerTick;

            HideDelay = TimeSpan.FromSeconds(2);

            popup = new Popup
                        {
                            Child = this
                        };

            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
        }

        #region IsVisible

        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
            "IsVisible",
            typeof (bool),
            typeof (InfoBox),
            new PropertyMetadata(OnIsVisibleChanged)
            );

        public bool IsVisible
        {
            get { return (bool) GetValue(IsVisibleProperty); }
            private set { SetValue(IsVisibleProperty, value); }
        }

        private static void OnIsVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (InfoBox) obj;
            control.popup.IsOpen = control.IsVisible;
            if (!control.IsVisible)
            {
                control.activeTimer.Stop();

                if (control.currentShape != null)
                {
                    control.currentShape.MouseEnter -= control.OnMouseEnter;
                    control.currentShape.MouseLeave -= control.OnMouseLeave;
                    control.currentShape = null;
                }

                if (control.infoBoxContent != null)
                {
                    control.infoBoxContent.OnHidden();
                    control.infoBoxContent = null;
                }
            }
        }

        #endregion

        #region IsMouseOver

        public static readonly DependencyProperty IsMouseOverProperty = DependencyProperty.Register(
            "IsMouseOver",
            typeof (bool),
            typeof (InfoBox),
            new PropertyMetadata(OnIsMouseOverChanged)
            );

        public bool IsMouseOver
        {
            get { return (bool) GetValue(IsMouseOverProperty); }
            private set { SetValue(IsMouseOverProperty, value); }
        }

        private static void OnIsMouseOverChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (InfoBox) obj;
            if (control.IsVisible)
            {
                control.ResetActiveTimer();
            }
        }

        #endregion

        #region HideDelay

        public static readonly DependencyProperty HideDelayProperty = DependencyProperty.Register(
            "HideDelay",
            typeof (TimeSpan),
            typeof (InfoBox),
            new PropertyMetadata(OnHideDelayChanged)
            );

        public TimeSpan HideDelay
        {
            get { return (TimeSpan) GetValue(HideDelayProperty); }
            set { SetValue(HideDelayProperty, value); }
        }

        private static void OnHideDelayChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (InfoBox) obj;
            control.activeTimer.Interval = control.HideDelay;
        }

        #endregion

        public GeometryBase ActiveShape
        {
            get { return currentShape; }
        }

        public Map ActiveMap
        {
            get
            {
                if (currentShape != null)
                {
                    return currentShape.MapInstance;
                }
                return null;
            }
        }

        public static InfoBox Instance
        {
            get { return instance; }
        }

        private void SetDisplayState(bool displayOnLeft, bool displayOnTop)
        {
            string stateName;

            if (displayOnLeft)
            {
                stateName = displayOnTop ? "DisplayOnTopLeft" : "DisplayOnBottomLeft";
            }
            else
            {
                stateName = displayOnTop ? "DisplayOnTopRight" : "DisplayOnBottomRight";
            }

            VisualStateManager.GoToState(this, stateName, false);
        }

        protected virtual void OnMouseEnter(object sender, MouseEventArgs e)
        {
            IsMouseOver = true;
        }

        protected virtual void OnMouseLeave(object sender, MouseEventArgs e)
        {
            IsMouseOver = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            closeButton = GetTemplateChild(PART_CloseButton) as ButtonBase;
            if (closeButton != null)
            {
                closeButton.Click += (o, e) => Hide(true);
            }
        }

        protected virtual void ShowCore(GeometryBase shape, object content, Point offset)
        {
            int _version = Interlocked.Increment(ref version);
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(() =>
                                           {
                                               if (_version == version)
                                               {
                                                   ShowCore(shape, content, offset);
                                               }
                                           });
            }
            else
            {
                InfoBoxEventBehavior.AttachToMap(shape.MapInstance, this);

                currentShape = shape;

                //use content control to ensure that the InfoBox.Template does 
                //not change from different ShapeLayer.InfoBoxTemplate values
                var contentControl = new ContentControl
                                         {
                                             Content = content
                                         };
                //if (shape.Layer.InfoBoxContentTemplate != null) {
                //    contentControl.ContentTemplate = shape.Layer.InfoBoxContentTemplate;
                //}

                Content = contentControl;

                currentShape.MouseEnter += OnMouseEnter;
                currentShape.MouseLeave += OnMouseLeave;

                IsVisible = true;
                IsMouseOver = true;

                Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                Size size = DesiredSize;
                GeneralTransform mapScreenTransform = shape.MapInstance.TransformToVisual(Application.Current.RootVisual);
                var mapScreenBounds = new Rect(
                    mapScreenTransform.Transform(new Point(0, 0)),
                    mapScreenTransform.Transform(new Point(shape.MapInstance.ActualWidth, shape.MapInstance.ActualHeight))
                    );

                bool displayOnLeft = false, displayOnTop = true;

                Point displayPoint = shape.TransformToVisual(Application.Current.RootVisual).Transform(new Point(0, 0));
                displayPoint.X += (offset.X + shape.ActualWidth);
                displayPoint.Y += (offset.Y - size.Height);

                if ((displayPoint.X + size.Width) > mapScreenBounds.Right)
                {
                    //revert the offset above
                    displayPoint.X -= (offset.X + shape.ActualWidth);

                    //shift the point for displaying on the left of the shape
                    displayPoint.X -= (offset.X + size.Width);
                    displayOnLeft = true;
                }

                if (displayPoint.Y < 0)
                {
                    //revert the offset above
                    displayPoint.Y -= (offset.Y - size.Height);

                    //shift the point for displaying on the bottom of the shape
                    //displayPoint.Y += (offset.Y + shape.ActualHeight);
                    displayPoint.Y += offset.Y;
                    displayOnTop = false;
                }

                SetDisplayState(displayOnLeft, displayOnTop);

                popup.HorizontalOffset = displayPoint.X;
                popup.VerticalOffset = displayPoint.Y;

                infoBoxContent = content as IInfoBoxContent;
                if (infoBoxContent != null)
                {
                    infoBoxContent.OnShown();
                }
            }
        }

        public void Show(IInfoBoxShape shape)
        {
            var offset = new Point(0, 0);
            {
                if (shape.InfoBoxOffset != null)
                {
                    offset = shape.InfoBoxOffset.Value;
                }
                else
                {
                    //if (shape.Layer.InfoBoxOffset != null)
                    //{
                    //    offset = shape.Layer.InfoBoxOffset.Value;
                    //}
                }
                Show(shape, offset);
            }
        }

        public void Show(IInfoBoxShape shape, Point offset)
        {
            ///TODO:  RoadWarrior; need to reevaluate Infobox implementation ;  
            //Hide(true);

            //int _version = System.Threading.Interlocked.Increment(ref version);

            //object content = shape.InfoBoxContent;
            //if (content == null) {
            //    var contentProvider = shape.Layer.InfoBoxContentProvider;
            //    if (contentProvider != null) {
            //        contentProvider(shape, o => {
            //            if (_version == version && o != null) {
            //                ShowCore(shape, o, offset);
            //            }
            //        });
            //        return;
            //    }
            //} else {
            //    ShowCore(shape, content, offset);
            //}
        }

        public static void ShowInfoBox(GeometryBase shape)
        {
            //Instance.Show(shape);
        }

        public static void ShowInfoBox(GeometryBase shape, Point offset)
        {
            //Instance.Show(shape, offset);
        }

        public void Hide()
        {
            Hide(false);
        }

        public void Hide(bool immediate)
        {
            if (immediate)
            {
                IsVisible = false;
            }
            else
            {
                ResetActiveTimer();
            }
        }

        public static void HideInfoBox()
        {
            Instance.Hide();
        }

        public static void HideInfoBox(bool immediate)
        {
            Instance.Hide(immediate);
        }

        private void ResetActiveTimer()
        {
            activeTimer.Stop();
            activeTimer.Start();
        }

        private void OnActiveTimerTick(object sender, EventArgs e)
        {
            if (IsMouseOver)
            {
                ResetActiveTimer();
            }
            else
            {
                IsVisible = false;
            }
        }

        #region InfoBoxEventBehavior

        private sealed class InfoBoxEventBehavior // : EventBehaviorBase
        {
            private InfoBox infoBox;
            private Map _Map;

            public InfoBoxEventBehavior(Map map, InfoBox infoBox) //: base(BehaviorName)
            {
                _Map = map;
                this.infoBox = infoBox;
            }

            public static string BehaviorName
            {
                get { return "InfoBox"; }
            }

            public static void AttachToMap(Map map, InfoBox infoBox)
            {
                var behavior = _InfoBoxEventBehavior;
                //var behavior = map.EventBehavior.List[BehaviorName] as InfoBoxEventBehavior;
                if (behavior != null)
                {
                    if (!ReferenceEquals(behavior.infoBox, infoBox))
                    {
                        behavior.infoBox.Hide(true);
                    }
                    behavior.infoBox = infoBox;
                }
                else
                {
                    _InfoBoxEventBehavior = new InfoBoxEventBehavior(map, infoBox);
                    //behavior = new InfoBoxEventBehavior(map, infoBox);
                    //map.EventBehavior.List.Add(behavior);
                }
            }

            private void ViewChanged(Map map, MapEventArgs args)
            {
                ///RoadWarrior:  This is so breaking encapsulation.
                //base.ViewChanged(map, args);
                infoBox.Hide(true);
            }
        }

        #endregion
    }
}