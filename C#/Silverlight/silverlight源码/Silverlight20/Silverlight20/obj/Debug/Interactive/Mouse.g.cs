#pragma checksum "C:\Silverlight20\Silverlight20\Interactive\Mouse.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E96065E84FE176217C7644A9DBEA12CE"
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


namespace Silverlight20.Interactive {
    
    
    public partial class Mouse : System.Windows.Controls.UserControl {
        
        internal System.Windows.Shapes.Ellipse ellipse;
        
        internal System.Windows.Shapes.Rectangle rectangle;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Silverlight20;component/Interactive/Mouse.xaml", System.UriKind.Relative));
            this.ellipse = ((System.Windows.Shapes.Ellipse)(this.FindName("ellipse")));
            this.rectangle = ((System.Windows.Shapes.Rectangle)(this.FindName("rectangle")));
        }
    }
}
