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

using System.ComponentModel;

namespace Silverlight20.Data
{
    public partial class NotifyProperty : UserControl
    {
        public NotifyProperty()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(NotifyProperty_Loaded);
        }

        void NotifyProperty_Loaded(object sender, RoutedEventArgs e)
        {
            ellipse.DataContext = new MyColor() { Brush = new SolidColorBrush(Colors.Red) };

        }

        private void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyColor color = ellipse.DataContext as MyColor;
            
            if (color.Brush.Color == Colors.Red)
                ellipse.DataContext = new MyColor() { Brush = new SolidColorBrush(Colors.Green) };
            else
                ellipse.DataContext = new MyColor() { Brush = new SolidColorBrush(Colors.Red) };
        }
    }

    public class MyColor : INotifyPropertyChanged
    {
        private SolidColorBrush _brush;
        public SolidColorBrush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Brush"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
