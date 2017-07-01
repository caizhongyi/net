// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.
//该源码首发自www.51aspx.com(５1ａｓｐｘ．ｃｏｍ)

using System;
using System.Windows;

namespace Microsoft.Windows.Controls.Samples
{
    /// <summary>
    /// Microsoft.Windows.Controls.DataVisualization samples application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the App class.
        /// </summary>
        public App()
        {
            Startup += delegate
            {
                SharedResources.Merge(Resources);
                RootVisual = new SampleBrowser(this) { Title = "Microsoft.Windows.Controls.DataVisualization" };
            };
            InitializeComponent();
        }
    }
}