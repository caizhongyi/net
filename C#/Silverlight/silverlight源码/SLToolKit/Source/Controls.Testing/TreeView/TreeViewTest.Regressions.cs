﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Windows.Controls.Testing
{
    /// <summary>
    /// Regression tests for TreeView.
    /// </summary>
    public partial class TreeViewTest
    {
        /// <summary>
        /// Verify that Background is TemplateBound in the default template.
        /// </summary>
        [TestMethod]
        [Description("Verify that Background is TemplateBound in the default template.")]
        [Asynchronous]
        [Bug("526773 - TreeView Background property not TemplateBound", Fixed = true)]
        public void BackgroundIsTemplateBoundInDefaultTemplate()
        {
            TreeView tree = DefaultTreeViewToTest;            

            TestAsync(
                tree,
                () => Assert.IsNotNull(tree.Template, "Template should not be null!"),
                () => Assert.IsNotNull(tree.GetVisualChild("ScrollViewer"), "Default Template doesn't contain a ScrollViewer element!"),                
                () => tree.Background = new SolidColorBrush(Colors.Red),
                () => Assert.AreEqual<Brush>(tree.Background, ((Control)tree.GetVisualChild("ScrollViewer")).Background, "Background in Default Template isn't TemplateBound!"));
        }
    }
}
