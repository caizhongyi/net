#pragma checksum "E:\downcode\YYFly\YYFly\Controls\Parts\Flower.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9A74F3267F6E75CD29B21BFB82D5A1E7"
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


namespace YYFly.Controls.Parts {
    
    
    public partial class Flower : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid 花瓣;
        
        internal System.Windows.Controls.Grid 花心;
        
        internal System.Windows.Shapes.Ellipse Container;
        
        internal System.Windows.Controls.Grid 茎和叶;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/YYFly;component/Controls/Parts/Flower.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.花瓣 = ((System.Windows.Controls.Grid)(this.FindName("花瓣")));
            this.花心 = ((System.Windows.Controls.Grid)(this.FindName("花心")));
            this.Container = ((System.Windows.Shapes.Ellipse)(this.FindName("Container")));
            this.茎和叶 = ((System.Windows.Controls.Grid)(this.FindName("茎和叶")));
        }
    }
}
