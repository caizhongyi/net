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

namespace Five
{
    public partial class Page : UserControl
    {
        public EventHandler<RoutedEventArgs> StartModeSelectClick;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            InitializeComponent();
            ApplicationFacade.Instance.Startup(this);
            StartSelectModeButton.Click += new RoutedEventHandler(StartSelectModeButton_Click);
        }

        void StartSelectModeButton_Click(object sender, RoutedEventArgs e)
        {
            StartModeSelectClick(sender, e);    
        }

    }
}
