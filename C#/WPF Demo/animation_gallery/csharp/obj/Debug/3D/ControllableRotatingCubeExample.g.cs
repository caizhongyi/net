﻿#pragma checksum "..\..\..\3D\ControllableRotatingCubeExample.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "80712C13FD4A6331767C2064935B483D"
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
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Microsoft.Samples.Animation.AnimationGallery {
    
    
    /// <summary>
    /// ControllableRotatingCubeExample
    /// </summary>
    public partial class ControllableRotatingCubeExample : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Animation.BeginStoryboard LeftSpinBeginStoryboard;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Animation.BeginStoryboard RightSpinBeginStoryboard;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Animation.BeginStoryboard UpwardSpinBeginStoryboard;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Animation.BeginStoryboard DownwardSpinBeginStoryboard;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Media3D.PerspectiveCamera myPerspectiveCamera;
        
        #line default
        #line hidden
        
        
        #line 259 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Media3D.AxisAngleRotation3D myHorizontalRotation;
        
        #line default
        #line hidden
        
        
        #line 264 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Media3D.AxisAngleRotation3D myVerticalRotation;
        
        #line default
        #line hidden
        
        
        #line 267 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
        internal System.Windows.Media.Media3D.TranslateTransform3D myTranslateTransform;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/AnimationGallery;component/3d/controllablerotatingcubeexample.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\3D\ControllableRotatingCubeExample.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LeftSpinBeginStoryboard = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 2:
            this.RightSpinBeginStoryboard = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 3:
            this.UpwardSpinBeginStoryboard = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 4:
            this.DownwardSpinBeginStoryboard = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 5:
            this.myPerspectiveCamera = ((System.Windows.Media.Media3D.PerspectiveCamera)(target));
            return;
            case 6:
            this.myHorizontalRotation = ((System.Windows.Media.Media3D.AxisAngleRotation3D)(target));
            return;
            case 7:
            this.myVerticalRotation = ((System.Windows.Media.Media3D.AxisAngleRotation3D)(target));
            return;
            case 8:
            this.myTranslateTransform = ((System.Windows.Media.Media3D.TranslateTransform3D)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
