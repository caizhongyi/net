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

namespace Five.View.Components
{
    public partial class RestartPanelComponent : UserControl
    {

        public event EventHandler<RoutedEventArgs> RestartClick;

        public RestartPanelComponent()
        {
            InitializeComponent();
            RestartButton.Click += RestartButton_Click;
            

            FadeOut.Completed += FadeOut_Completed;
        }

        void FadeOut_Completed(object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartClick(sender, e);
        }

       
    }
}
