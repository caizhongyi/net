// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using DeepEarth.Events;

namespace DeepEarth.Controls
{

    /// <summary>
    /// <para>
    /// UI Control to display the current map scale 
    /// </para>
    /// <example>
    /// 
    /// <code lang="XAML" title="Add ScaleControl to map using Xaml, is located in the map style/template">   
    /// <![CDATA[
    /// <DeepControls:ScaleControl VerticalAlignment="Bottom" HorizontalAlignment="Right" Margin="12,12,12,40" />
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    [TemplatePart(Name = PART_LabelTextBlock, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_ScaleTextBlock, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_UnitTextBlock, Type = typeof(TextBlock))]
    public class ScaleControl : MapControl
    {
        private const string PART_LabelTextBlock = "PART_LabelTextBlock";
        private const string PART_ScaleTextBlock = "PART_ScaleTextBlock";
        private const string PART_UnitTextBlock = "PART_UnitTextBlock";

        private TextBlock _LabelTextBox;
        private TextBlock _ScaleTextBox;
        private TextBlock _UnitTextBox;

        /// <summary>
        /// ScaleControl Constructor, will use the default instance of the map
        /// </summary>
        public ScaleControl() : this(Map.DefaultInstance) {}

        /// <summary>
        /// ScaleControl Constructor, will use the specific instance of the map
        /// </summary>
        /// <param name="map">Instance of Map</param>
        public ScaleControl(Map map)
        {
            _Map = map;
            DefaultStyleKey = typeof(ScaleControl);
        }

        /// <summary>
        /// Overrides the default OnApplyTemplate to configure child FrameworkElement references from the applied template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _LabelTextBox = (TextBlock)GetTemplateChild(PART_LabelTextBlock);
            _ScaleTextBox = (TextBlock)GetTemplateChild(PART_ScaleTextBlock);
            _UnitTextBox = (TextBlock)GetTemplateChild(PART_UnitTextBlock);

            // Test IsDesignTime, to help display control in blend correctly
            if(HtmlPage.IsEnabled)
            {
                //This call invokes the Map property to walk the VisualTree and find the controls map.
                // _Map = MapInstance;
                _Map.Events.MapZoomEnded += Events_MapZoomEnded;
                _Map.Events.MapZoomChanged += Events_MapZoomChanged;
                _Map.Events.MapViewChanged += Events_MapViewChanged;

                _LabelTextBox.Text = "Scale";
                _UnitTextBox.Text = _Map.DisplayUnit.ToString();
            }
        }

        void Events_MapViewChanged(Map map, MapEventArgs args)
        {
            _ScaleTextBox.Text = GetMapScale(map);
        }

        private void Events_MapZoomChanged(Map map, double zoomLevel)
        {
            _ScaleTextBox.Text = GetMapScale(map);
        }

        private void Events_MapZoomEnded(Map map, MapEventArgs args)
        {
            _ScaleTextBox.Text = GetMapScale(map);
        }

        private static string GetMapScale(Map map)
        {
            return "1:" + Math.Round(map.Scale, 0);
        }

    }
}