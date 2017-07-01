// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using DeepEarth.Events;

namespace DeepEarth.Controls
{
    
    /// <summary>
    /// <para>
    /// UI control to display the mouse geographic (Latitude, Longitude) coordinates plus the current zoom level
    /// </para>
    /// <example>
    /// 
    /// <code lang="XAML" title="Add CoordControl to map using Xaml">   
    /// <![CDATA[
    /// <DeepControls:CoordControl VerticalAlignment="Bottom" HorizontalAlignment="Right" />
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public class CoordControl : MapControl
    {
        private static Point _LastMouseLocation;
        private TextBlock _xCoordTextBox;
        private TextBlock _yCoordTextBox;
        private TextBlock _ZoomLevelTextBox;

        /// <summary>
        /// CoordControl constructor, will use the default instance of the map
        /// </summary>
        public CoordControl() : this(Map.DefaultInstance) { }

        /// <summary>
        /// CoordControl constructor, will use the specific instance of the map
        /// </summary>
        /// <param name="map">Instance of Map</param>
        public CoordControl(Map map)
        {
            _Map = map;
            DefaultStyleKey = typeof (CoordControl);
        }

        /// <summary>
        /// Overrides the default OnApplyTemplate to configure child FrameworkElement references from the applied template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _xCoordTextBox = (TextBlock) GetTemplateChild("PART_xCoordTextBlock");
            _yCoordTextBox = (TextBlock) GetTemplateChild("PART_yCoordTextBlock");
            _ZoomLevelTextBox = (TextBlock) GetTemplateChild("PART_ZoomTextBlock");

            // Test IsDesignTime, to help display control in blend correctly
            if (HtmlPage.IsEnabled)
            {
                //This call invokes the Map property to walk the VisualTree and find the controls map.
                //_Map = MapInstance;
                _Map.Events.MapMouseEnter += Events_MapMouseEnter;
                _Map.Events.MapMouseLeave += Events_MapMouseLeave;
                _Map.Events.MapMouseMove += Events_MapMouseMove;
                _Map.Events.MapZoomEnded += Events_MapZoomEnded;
                _Map.Events.MapZoomChanged += Events_MapZoomChanged;
                _Map.Events.MapViewChanged += Events_MapViewChanged;
            }
        }

        private void Events_MapViewChanged(Map map, MapEventArgs args)
        {
            if (IsEnabled)
            {
                _xCoordTextBox.Text = GetXCoordinate();
                _yCoordTextBox.Text = GetYCoordinate();
            }
        }

        private void Events_MapZoomChanged(Map map, double zoomLevel)
        {
            if (IsEnabled)
            {
                _ZoomLevelTextBox.Text = GetZoomLevel();
            }
        }

        private void Events_MapZoomEnded(Map map, MapEventArgs args)
        {
            if (IsEnabled)
            {
                _ZoomLevelTextBox.Text = GetZoomLevel();
            }
        }

        private void Events_MapMouseEnter(Map map, MouseEventArgs args)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
            if (IsEnabled)
            {
                _xCoordTextBox.Text = GetXCoordinate();
                _yCoordTextBox.Text = GetYCoordinate();
            }
        }

        private void Events_MapMouseMove(Map map, MouseEventArgs args)
        {
            _LastMouseLocation = args.GetPosition(map);

            if (IsEnabled)
            {
                _xCoordTextBox.Text = GetXCoordinate();
                _yCoordTextBox.Text = GetYCoordinate();
            }
        }

        private void Events_MapMouseLeave(Map map, MouseEventArgs args)
        {
            VisualStateManager.GoToState(this, "Normal", true);
            _xCoordTextBox.Text = GetXCoordinate();
            _yCoordTextBox.Text = GetYCoordinate();
        }

        private string GetZoomLevel()
        {
            string zoom = string.Empty;
            if (MapInstance.BaseLayer.Source != null)
            {
                zoom = string.Format("{0:N2}", MapInstance.ZoomLevel);
            }
            return zoom;
        }

        private string GetXCoordinate()
        {
            //Do not show coordinats if outside bounds of Earth.
            Point pixel = _LastMouseLocation;
            string xCoord = string.Empty;
            Point mapCoordinate = MapInstance.CoordHelper.PixelToGeo(pixel);

            if (MapInstance.BaseLayer.Source != null)
            {
                bool isSomePlaceOnEarth = _Map.BaseLayer.Source.CoordinateBounds.Contains(mapCoordinate);
                if (isSomePlaceOnEarth)
                {
                    xCoord = string.Format("{0:N4},", mapCoordinate.X);
                }
            }

            return xCoord;
        }

        private string GetYCoordinate()
        {
            //Do not show coordinats if outside bounds of Earth.
            Point pixel = _LastMouseLocation;
            string yCoord = string.Empty;
            Point mapCoordinate = MapInstance.CoordHelper.PixelToGeo(pixel);

            if (MapInstance.BaseLayer.Source != null)
            {
                bool isSomePlaceOnEarth = MapInstance.BaseLayer.Source.CoordinateBounds.Contains(mapCoordinate);
                if (isSomePlaceOnEarth)
                {
                    yCoord = string.Format("{0:N4}", mapCoordinate.Y);
                }
            }

            return yCoord;
        }
    }
}