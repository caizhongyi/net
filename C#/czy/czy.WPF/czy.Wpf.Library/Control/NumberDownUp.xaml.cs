using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace czy.Wpf.Library.Control
{
    /// <summary>
    /// NumberDownUp.xaml 的交互逻辑
    /// </summary>
    public partial class NumberDownUp : UserControl
    {
        public NumberDownUp()
        {
            InitializeComponent();
        }
        private double numericValue = 0;
        public double Value
        {

            get {
                try
                {
                    double number= Convert.ToDouble(ValueText.Text);
                    number = number > maxValue ? maxValue : number;
                    number = number < minValue ? minValue : number;
                    numericValue =number;
                }
                catch
                {
                    numericValue = minValue;
                }
                return numericValue; 
            }
            set
            {
                value = value > maxValue ? maxValue : value;
                value = value < minValue ? minValue : value;
                ValueText.Text = value.ToString();
                numericValue = value;
                NotifyPropertyChanged("Value");
            }
        }

        private double increment = 1;

        public double Increment
        {
            get { return increment; }
            set { increment = value; }
        }
        private double maxValue = 99;

        public double MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        private double minValue = 1;

        public double MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

      

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {

            double newValue = (Value + Increment);
            if (newValue > MaxValue)
            {
                Value = MaxValue;
            }
            else
            {
                Value = newValue;
            }

        }
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {

            double newValue = (Value - Increment);
            if (newValue < MinValue)
            {
                Value = MinValue;
            }
            else
            {
                Value = newValue;
            }

        }
        private void ValueText_LostFocus(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Value = double.Parse(ValueText.Text);
            //}
            //catch (Exception)
            //{
            //    Value = 0;
            //}

        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }



    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleValueConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return value.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            try
            {

                return double.Parse((string)value);

            }

            catch (Exception)
            {

                return 0;

            }

        }
        #endregion

    }


}

