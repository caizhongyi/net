/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System.Windows;
using System.Windows.Controls;
using DeepEarth;

namespace VEDeepEarthExample.Controls
{
    public partial class Dashboard : UserControl
    {
        private bool expanded = true;

        public Dashboard()
        {
            // Required to initialize variables
            InitializeComponent();

            spin.Click += spin_Click;

            zoomIn.Click += zoomIn_Click;
            zoomOut.Click += zoomOut_Click;

            up.Click += up_Click;
            down.Click += down_Click;
            left.Click += left_Click;
            right.Click += right_Click;
        }

        Map mapControl;
        public Map MapControl 
        {
            get 
            {
                return this.mapControl;
            }
            set
            {
                if (this.mapControl != null) 
                {
                    this.mapControl.BaseLayers.CurrentChanged -= OnBaseLayerChanged;
                }
                this.mapControl = value;
                if (this.mapControl != null) 
                {
                    this.mapControl.BaseLayers.CurrentChanged += OnBaseLayerChanged;
                    this.baseLayers.ItemsSource = this.mapControl.BaseLayers;
                    this.OnBaseLayerChanged(this.mapControl.BaseLayers, System.EventArgs.Empty);
                }
                else
                {
                    this.baseLayers.ItemsSource = null;
                }
            }
        }

        private void OnBaseLayerChanged(object sender, System.EventArgs e) 
        {
            int index = this.MapControl.BaseLayers.CurrentPosition;
            if (this.baseLayers.SelectedIndex != index)
            {
                this.baseLayers.SelectedIndex = index;
                SelectedIndexWorkaround(this.baseLayers);
            }
        }

        public static void SelectedIndexWorkaround(ListBox listBox) {
            int selectedIndex = listBox.SelectedIndex;
            bool set = false;
            listBox.LayoutUpdated += delegate {
                                                  if (!set) {
                                                      // Toggle value to force the change
                                                      listBox.SelectedIndex = -1;
                                                      listBox.SelectedIndex = selectedIndex;
                                                      set = true;
                                                  }
            };
        }

        private void baseLayer_Changed(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                int index = this.baseLayers.SelectedIndex;
                if (this.MapControl.BaseLayers.CurrentPosition != index)
                {
                    this.MapControl.BaseLayers.GoTo(index);
                }
            }
        }

        private void spin_Click(object sender, RoutedEventArgs e)
        {
            if (expanded)
            {
                VisualStateManager.GoToState(this, "Collapsed", true);
                expanded = false;
            }
            else
            {
                VisualStateManager.GoToState(this, "Expanded", true);
                expanded = true;
            }
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                MapControl.Pan(Direction.North);
            }
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                MapControl.Pan(Direction.South);
            }
        }

        private void left_Click(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                MapControl.Pan(Direction.West);
            }
        }

        private void right_Click(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                MapControl.Pan(Direction.East);
            }
        }

        private void zoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                MapControl.ZoomLevel = MapControl.ZoomLevel + 1;
            }
        }

        private void zoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (MapControl != null)
            {
                MapControl.ZoomLevel = MapControl.ZoomLevel - 1;
            }
        }
    }
}