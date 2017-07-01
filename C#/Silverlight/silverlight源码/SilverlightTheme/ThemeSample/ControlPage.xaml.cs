using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ThemeSample
{
    public partial class ControlPage : UserControl
    {
        public ControlPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ControlPage_Loaded);
        }

        void ControlPage_Loaded(object sender, RoutedEventArgs e)
        {
            MyGrid.ItemsSource = City.PugetSound;
        }

       
    }
}
