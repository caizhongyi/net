﻿#pragma checksum "..\..\app.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9F43657CC767430D05D7B8A7F8188E84"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行库版本:2.0.50727.3053
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using SDKSamples.ImageSample;
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


namespace SDKSamples.ImageSample {
    
    
    /// <summary>
    /// app
    /// </summary>
    public partial class app : System.Windows.Application {
        
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
            
            #line 5 "..\..\app.xaml"
            this.Startup += new System.Windows.StartupEventHandler(this.OnApplicationStartup);
            
            #line default
            #line hidden
            System.Uri resourceLocater = new System.Uri("/SDKSample;component/app.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\app.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main() {
            SDKSamples.ImageSample.app app = new SDKSamples.ImageSample.app();
            app.InitializeComponent();
            app.Run();
        }
    }
}