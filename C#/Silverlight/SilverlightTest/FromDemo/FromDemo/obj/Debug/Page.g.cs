#pragma checksum "F:\SilverlightTest\FromDemo\FromDemo\Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1F25E053B0149834AC092FC1B88EB0B4"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行库版本:2.0.50727.3053
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


namespace FromDemo {
    
    
    public partial class Page : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Canvas list_canvas;
        
        internal System.Windows.Media.ScaleTransform list_stf;
        
        internal System.Windows.Controls.Button changesize_btn;
        
        internal System.Windows.Controls.ListBox input_list;
        
        internal System.Windows.Controls.Canvas save_canvas;
        
        internal System.Windows.Controls.ListBox save_list;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/FromDemo;component/Page.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.list_canvas = ((System.Windows.Controls.Canvas)(this.FindName("list_canvas")));
            this.list_stf = ((System.Windows.Media.ScaleTransform)(this.FindName("list_stf")));
            this.changesize_btn = ((System.Windows.Controls.Button)(this.FindName("changesize_btn")));
            this.input_list = ((System.Windows.Controls.ListBox)(this.FindName("input_list")));
            this.save_canvas = ((System.Windows.Controls.Canvas)(this.FindName("save_canvas")));
            this.save_list = ((System.Windows.Controls.ListBox)(this.FindName("save_list")));
        }
    }
}
