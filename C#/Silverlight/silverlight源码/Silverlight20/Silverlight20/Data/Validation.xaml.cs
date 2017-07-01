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

using System.Text.RegularExpressions;

namespace Silverlight20.Data
{
    public partial class Validation : UserControl
    {
        public Validation()
        {
            InitializeComponent();
        }

        private void StackPanel_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                textBox.Background = new SolidColorBrush(Colors.Red);

            }
            else if (e.Action == ValidationErrorEventAction.Removed)
            {
                textBox.Background = new SolidColorBrush(Colors.White);
            }
        }
    }


    public class MyValidation
    {
        private string _count;
        public string Count
        {
            get { return _count; }
            set
            {
                if (!Regex.IsMatch(value, @"^\d+$"))
                    throw new Exception("必须是整数");

                _count = value;
            }
        }
    }
}
