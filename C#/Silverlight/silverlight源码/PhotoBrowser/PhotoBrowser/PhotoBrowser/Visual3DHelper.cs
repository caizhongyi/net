//---------------------------------------------------------------------------
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Limited Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
// All other rights reserved.
//
// This file is part of the 3D Tools for Windows Presentation Foundation
// project.  For more information, see:
// 
// http://CodePlex.com/Wiki/View.aspx?ProjectName=3DTools
//
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace PhotoBrowser
{
    public class Visual3DHelper
    {
        /// <summary>
        /// Returns the Viewport3D that contains the passed in Visual3D
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static Viewport3D GetVisual3DViewport(DependencyObject visual)
        {
            if (!(visual is Visual3D))
            {
                throw new ArgumentException("Must be of type Visual3D.", "visual");
            }

            while (visual != null)
            {
                if (!(visual is Visual3D))
                {
                    break;
                }

                visual = VisualTreeHelper.GetParent(visual);
            }

            Viewport3DVisual viewportVisual = visual as Viewport3DVisual;
            Viewport3D viewport = VisualTreeHelper.GetParent(viewportVisual) as Viewport3D;

            return viewport;
        }
    }
}
