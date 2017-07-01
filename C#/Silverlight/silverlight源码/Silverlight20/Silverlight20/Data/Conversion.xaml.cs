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
using System.Windows.Data;

namespace Silverlight20.Data
{
    public partial class Conversion : UserControl
    {
        public Conversion()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(NotifyProperty_Loaded);
        }

        void NotifyProperty_Loaded(object sender, RoutedEventArgs e)
        {
            ellipse.DataContext = new MyColorEnum() { Color = ColorEnum.Red };

        }

        private void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyColorEnum color = ellipse.DataContext as MyColorEnum;

            if (color.Color == ColorEnum.Red)
                ellipse.DataContext = new MyColorEnum() { Color = ColorEnum.Green };
            else
                ellipse.DataContext = new MyColorEnum() { Color = ColorEnum.Red };
        }
    }

    public class ColorEnumToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ColorEnum color = (ColorEnum)value;

            SolidColorBrush brush = new SolidColorBrush(Colors.Red);

            if (color == ColorEnum.Green)
                brush = new SolidColorBrush(Colors.Green);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class MyColorEnum : INotifyPropertyChanged
    {
        private ColorEnum _color;
        public ColorEnum Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Color"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum ColorEnum
    {
        Red,
        Green
    }
}
