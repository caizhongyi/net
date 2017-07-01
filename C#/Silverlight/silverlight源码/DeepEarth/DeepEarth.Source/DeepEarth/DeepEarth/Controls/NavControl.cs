using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DeepEarth.Events;

namespace DeepEarth.Controls
{

    /// <summary>
    /// <para>
    /// UI control for map navigation and toolbar options
    /// </para>
    /// <example>
    /// 
    /// <code lang="XAML" title="Add NavControl to map using Xaml, is located in the map style/template">   
    /// <![CDATA[
    /// <DeepControls:NavControl x:Name="navControl" RotationOn="True">
    ///    <--! Use StackPanel as a container to hold ToolBar Controls -->
    ///    <StackPanel Orientation="Horizontal" MinHeight="48.111">
    ///        Add controls to display in the NavControl ToolBar...
    ///    </StackPanel>
    /// </DeepControls:NavControl>
    /// ]]> 
    /// </code>
    /// </example>
    /// </summary>
    [TemplatePart(Name = "spin", Type = typeof(Button))]
    [TemplatePart(Name = "up", Type = typeof(Button))]
    [TemplatePart(Name = "down", Type = typeof(Button))]
    [TemplatePart(Name = "left", Type = typeof(Button))]
    [TemplatePart(Name = "right", Type = typeof(Button))]
    [TemplatePart(Name = "zoomIn", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "zoomOut", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "rotationGlobeGrid", Type = typeof(Grid))]
    [ContentProperty("Toolbar")]
    public class NavControl : MapControl
    {
        private DoubleAnimationUsingKeyFrames _AnimationCollapseToolbar;
        private DoubleAnimationUsingKeyFrames _AnimationExpandToolbar;
        private VisualState _CollapseNavigation;
        private ContentControl _ContentControl;
        private VisualState _ExpandNavigation;
        private double _LastToolbarWidth;
        private Point _MapCenter;
        private double _MapZoom;
        private Border _ToolbarBorder;
        private object _ToolbarContent;
        private double angle;
        private Button down;
        private bool expanded = true;
        private bool isRotating;
        private double lastXPosition;
        private double lastYPosition;

        //Variables to preserve map location during rotation.
        private Button left;
        private Button right;
        private Grid rotationGlobeGrid;
        private RotateTransform rotationGlobeGridAngle;
        private Button spin;
        private Button up;
        private RepeatButton zoomIn;
        private RepeatButton zoomOut;

        /// <summary>
        /// NavControl Constructor, will use the default instance of the map
        /// </summary>
        public NavControl() : this(Map.DefaultInstance) { }

        /// <summary>
        /// NavControl Constructor, will use the specific instance of the map
        /// </summary>
        /// <param name="map">Instance of Map</param>
        public NavControl(Map map)
        {
            _Map = map;
            DefaultStyleKey = typeof(NavControl);
        }

        /// <summary>
        /// Property set Map Rotation, by default is set to false/off
        /// </summary>
        public bool RotationOn { get; set; }

        /// <summary>
        /// Property to access the ToolBar content of the NavControl
        /// </summary>
        public object Toolbar
        {
            get { return _ContentControl.Content; }
            set
            {
                _ToolbarContent = value;
                if(_ContentControl != null)
                {
                    _ContentControl.Content = value;
                }
            }
        }

        private DoubleAnimationUsingKeyFrames AnimationExpandToolbar
        {
            get
            {
                if(_AnimationExpandToolbar == null)
                {
                    _AnimationExpandToolbar = new DoubleAnimationUsingKeyFrames();

                    Storyboard.SetTarget(_AnimationExpandToolbar, _ToolbarBorder);
                    Storyboard.SetTargetProperty(_AnimationExpandToolbar, new PropertyPath("Width")); 

                    var spline = new SplineDoubleKeyFrame();
                    //spline.Value = 0; Value is set based on size at runtime.
                    spline.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6));
                    spline.KeySpline.ControlPoint1 = new Point(0.5, 0);
                    spline.KeySpline.ControlPoint2 = new Point(0.5, 1);
                    _AnimationExpandToolbar.KeyFrames.Add(spline);


                    if(_ExpandNavigation != null)
                    {
                        _ExpandNavigation.Storyboard.Children.Add(_AnimationExpandToolbar);
                        _ExpandNavigation.Storyboard.Completed += (o, e) => { expanded = true; };
                    }

                }
                return _AnimationExpandToolbar;
            }
        }


        private DoubleAnimationUsingKeyFrames AnimationCollapseToolbar
        {
            get
            {
                if(_AnimationCollapseToolbar == null)
                {
                    _AnimationCollapseToolbar = new DoubleAnimationUsingKeyFrames();

                    Storyboard.SetTarget(_AnimationCollapseToolbar, _ToolbarBorder);
                    Storyboard.SetTargetProperty(_AnimationCollapseToolbar, new PropertyPath("Width")); //"(Width)"));

                    var spline = new SplineDoubleKeyFrame();
                    spline.Value = 0;
                    spline.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6));
                    spline.KeySpline.ControlPoint1 = new Point(0.5, 0);
                    spline.KeySpline.ControlPoint2 = new Point(0.5, 1);
                    _AnimationCollapseToolbar.KeyFrames.Add(spline);

                    if(_CollapseNavigation != null) _CollapseNavigation.Storyboard.Children.Add(_AnimationCollapseToolbar);
                }
                return _AnimationCollapseToolbar;
            }
        }

        /// <summary>
        /// Overrides the default OnApplyTemplate to configure child FrameworkElement references from the applied template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            spin = (Button)GetTemplateChild("spin");
            up = (Button)GetTemplateChild("up");
            down = (Button)GetTemplateChild("down");
            left = (Button)GetTemplateChild("left");
            right = (Button)GetTemplateChild("right");
            zoomIn = (RepeatButton)GetTemplateChild("zoomIn");
            zoomOut = (RepeatButton)GetTemplateChild("zoomOut");


            _ExpandNavigation = (VisualState)GetTemplateChild("Expanded");
            _CollapseNavigation = (VisualState)GetTemplateChild("Collapsed");


            _ToolbarBorder = (Border)GetTemplateChild("borderNavigation");
            _ContentControl = (ContentControl)GetTemplateChild("toolbarContent");
            if(_ToolbarContent != null) Toolbar = _ToolbarContent;

            rotationGlobeGrid = (Grid)GetTemplateChild("rotationGlobeGrid");
            rotationGlobeGridAngle = (RotateTransform)GetTemplateChild("rotationGlobeGridAngle");

            //Create the animations for Expand/Collapse of the upper toolbar
            _AnimationCollapseToolbar = AnimationCollapseToolbar;
            _AnimationExpandToolbar = AnimationExpandToolbar;


            spin.Click += spin_Click;
            zoomIn.Click += zoomIn_Click;
            zoomOut.Click += zoomOut_Click;

            up.Click += up_Click;
            down.Click += down_Click;
            left.Click += left_Click;
            right.Click += right_Click;

            rotationGlobeGrid.MouseLeftButtonDown += rotationGlobeGrid_MouseLeftButtonDown;
            rotationGlobeGrid.MouseLeftButtonUp += rotationGlobeGrid_MouseLeftButtonUp;
            rotationGlobeGrid.MouseMove += rotationGlobeGrid_MouseMove;

            if(HtmlPage.IsEnabled)
            {
                MapInstance.Events.MapRotationChanged += Events_MapRotationChanged;
            }
        }

        private void Events_MapRotationChanged(Map map, MapEventArgs args)
        {
            rotationGlobeGridAngle.Angle = map.RotationAngle;
        }


        private void spin_Click(object sender, RoutedEventArgs e)
        {
            if(expanded)
            {
                _ToolbarBorder.Width = _ToolbarBorder.ActualWidth;
                _LastToolbarWidth = Math.Max(_ToolbarBorder.ActualWidth, _ToolbarBorder.MinWidth);
                SetAnimationToolbar(0);
                VisualStateManager.GoToState(this, "Collapsed", true);
                expanded = false;
            }
            else
            {
                SetAnimationToolbar(_LastToolbarWidth);
                VisualStateManager.GoToState(this, "Expanded", true);
            }
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            if(MapInstance != null)
            {
                MapInstance.Pan(Direction.North);
            }
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            if(MapInstance != null)
            {
                MapInstance.Pan(Direction.South);
            }
        }

        private void left_Click(object sender, RoutedEventArgs e)
        {
            if(MapInstance != null)
            {
                MapInstance.Pan(Direction.West);
            }
        }

        private void right_Click(object sender, RoutedEventArgs e)
        {
            if(MapInstance != null)
            {
                MapInstance.Pan(Direction.East);
            }
        }

        private void zoomIn_Click(object sender, RoutedEventArgs e)
        {
            if(MapInstance != null)
            {
                MapInstance.ZoomOnPixelPoint(MapInstance.PixelCenter, 1);
            }
        }

        private void zoomOut_Click(object sender, RoutedEventArgs e)
        {
            if(MapInstance != null)
            {
                MapInstance.ZoomOnPixelPoint(MapInstance.PixelCenter, -1);
            }
        }


        private void SetAnimationToolbar(double width)
        {
            AnimationExpandToolbar.KeyFrames[0].Value = width;
        }

        private void rotationGlobeGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if(RotationOn)
            {
                var ui = sender as FrameworkElement;
                if(ui != null)
                {
                    //For the blackout issue, lets try to preserve the MSI coordinates
                    _MapCenter = MapInstance.GeoCenter;
                    _MapZoom = MapInstance.ZoomLevel;

                    isRotating = true;
                    ui.CaptureMouse();
                    lastXPosition = e.GetPosition(ui).X - (ui.ActualWidth / 2);
                    lastYPosition = e.GetPosition(ui).Y - (ui.ActualHeight / 2);
                }
                angle = rotationGlobeGridAngle.Angle;
            }
        }

        private void rotationGlobeGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var ui = sender as FrameworkElement;
            if(ui != null)
            {
                isRotating = false;
                ui.ReleaseMouseCapture();
            }
        }

        private void rotationGlobeGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if(isRotating)
            {
                var ui = sender as FrameworkElement;
                if(ui != null)
                {
                    double x = (e.GetPosition(ui).X - (ui.ActualWidth / 2));
                    double y = (e.GetPosition(ui).Y - (ui.ActualWidth / 2));
                    double theta = Math.Atan2(y, x) - Math.Atan2(lastYPosition, lastXPosition);
                    double angleInDegrees = Math.Round(theta * 180 / Math.PI);

                    angle = angle + angleInDegrees;

                    MapInstance.RotationAngle = angle;

                    rotationGlobeGridAngle.Angle = angle;

                    //For the blackout issue, lets try to preserve the MSI coordinates
                    MapInstance.SetViewCenter(_MapCenter, _MapZoom);
                }
            }
        }
    }
}