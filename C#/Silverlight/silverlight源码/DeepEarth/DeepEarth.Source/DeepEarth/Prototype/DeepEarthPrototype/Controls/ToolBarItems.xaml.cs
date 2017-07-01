/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DeepEarth;

namespace DeepEarthPrototype.Controls
{
    public partial class ToolBarItems // : IMapElement
    {
        private Map _Map;
        public SidePanelControl sidePanel;

        public ToolBarItems()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        #region IMapElement Members

        public Map MapInstance
        {
            get
            {
                if (_Map == null) _Map = Map.GetMapInstance(this);
                return _Map;
            }
            set
            {
                if (ReferenceEquals(_Map, value))
                {
                    return;
                }

                _Map = value;
            }
        }

        #endregion

        private void ToggleFullScreen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
        }

        private void ToolBarResetMap_Click(object sender, RoutedEventArgs e)
        {
            MapInstance.SetViewFullMap();
        }


        private void ToggleDraw_Click(object sender, RoutedEventArgs e)
        {
            MapInstance.DragMode = Map.DragBehavior.Draw;
        }

        private void ToggleMapMode_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleMapMode == null) ToggleMapMode = sender as Button;
            if (ToggleDragModeImage == null)
            {
                if (ToggleMapMode != null) ToggleDragModeImage = (Image) ToggleMapMode.FindName("ToggleDragModeImage");
            }

            switch (MapInstance.DragMode)
            {
                case Map.DragBehavior.Select:
                    MapInstance.DragMode = Map.DragBehavior.Pan;
                    ToolTipService.SetToolTip(ToggleMapMode, "Change to Select Mode");
                    if (ToggleDragModeImage != null) ToggleDragModeImage.Source = new BitmapImage(new Uri("../Resources/ToolBar/Pan.png", UriKind.Relative));
                    break;
                case Map.DragBehavior.Pan:
                    MapInstance.DragMode = Map.DragBehavior.Select;
                    ToolTipService.SetToolTip(ToggleMapMode, "Change to Pan Mode");
                    if (ToggleDragModeImage != null) ToggleDragModeImage.Source = new BitmapImage(new Uri("../Resources/ToolBar/Bounds.png", UriKind.Relative));
                    break;
            }
        }

        private void ToolBarMapNorth_Click(object sender, RoutedEventArgs e)
        {
            MapInstance.RotationAngle = 0;
        }
    }
}