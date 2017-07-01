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

namespace SilverlightValidationDemo
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LayoutRoot_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                (e.OriginalSource as Control).Background = new SolidColorBrush(Colors.Yellow);
                tbMessage.Text= e.Error.Exception.Message;
            }

            if (e.Action == ValidationErrorEventAction.Removed)
            {
                (e.OriginalSource as Control).Background = new SolidColorBrush(Colors.White);
                tbMessage.Text = "";
            }
        }
    }
}
