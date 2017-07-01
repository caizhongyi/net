using System;
using System.Net;
using System.Windows;

namespace SilverlighUnloadedBehavior
{
    public class MyTestControlViewModel
    {
        public void OnUnloaded()
        {
            MessageBox.Show("OnUnloaded is the view model.");
        }
    }
}
