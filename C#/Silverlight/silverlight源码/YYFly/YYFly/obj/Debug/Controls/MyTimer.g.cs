#pragma checksum "C:\Documents and Settings\webabcd\桌面\YYFly\YYFly\Controls\MyTimer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B9B3D9E156E491DB76A56FB95B7ED425"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace YYFly.Controls {
    
    
    public partial class MyTimer : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.TextBlock time;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/YYFly;component/Controls/MyTimer.xaml", System.UriKind.Relative));
            this.time = ((System.Windows.Controls.TextBlock)(this.FindName("time")));
        }
    }
}
