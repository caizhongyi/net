#pragma checksum "C:\Silverlight20\Silverlight20\Animation\Programmatically.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "03B2CB8DA0B93D7F1102526E84354548"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3053
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace Silverlight20.Animation {
    
    
    public partial class Programmatically : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.EllipseGeometry ellipseGeometry;
        
        internal System.Windows.Media.Animation.Storyboard storyboard;
        
        internal System.Windows.Media.Animation.PointAnimation pointAnimation;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Silverlight20;component/Animation/Programmatically.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.ellipseGeometry = ((System.Windows.Media.EllipseGeometry)(this.FindName("ellipseGeometry")));
            this.storyboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("storyboard")));
            this.pointAnimation = ((System.Windows.Media.Animation.PointAnimation)(this.FindName("pointAnimation")));
        }
    }
}
