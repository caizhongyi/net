#pragma checksum "G:\GobangSource\Five\View\Components\ShadowComponent.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E87B3D78A805B3F3D19F2D3FC6BA3663"
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


namespace Five.View.Components {
    
    
    public partial class ShadowComponent : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard Wind;
        
        internal System.Windows.Controls.Image Leaves1;
        
        internal System.Windows.Controls.Image Leaves2;
        
        internal System.Windows.Controls.Image Leaves3;
        
        internal System.Windows.Controls.Image Leaves4;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Five;component/View/Components/ShadowComponent.xaml", System.UriKind.Relative));
            this.Wind = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Wind")));
            this.Leaves1 = ((System.Windows.Controls.Image)(this.FindName("Leaves1")));
            this.Leaves2 = ((System.Windows.Controls.Image)(this.FindName("Leaves2")));
            this.Leaves3 = ((System.Windows.Controls.Image)(this.FindName("Leaves3")));
            this.Leaves4 = ((System.Windows.Controls.Image)(this.FindName("Leaves4")));
        }
    }
}
