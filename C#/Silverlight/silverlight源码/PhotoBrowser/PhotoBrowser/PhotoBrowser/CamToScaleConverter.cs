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
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Media3D;
using _3DTools;

namespace PhotoBrowser
{
    /// <summary>
    /// Camera to Scale Converter.
    /// 
    /// Given the perspective camera used on the Viewport3D, and the Width and Height of the
    /// window, this gives a scale transform such that a rectangle stretches to fill the entire
    /// Viewport3D it is contained within.  Conversion assumes the camera is located on the Z axis, 
    /// looking down it, and that the rectangle is positioned at the origin.
    /// </summary>
    [ValueConversion(typeof(Camera), typeof(ScaleTransform3D))]
    public class CamToScaleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            PerspectiveCamera p = values[0] as PerspectiveCamera;
            
            if (p != null && values[1] is double && values[2] is double)
            {
                double width = (double)values[1];
                double height = (double)values[2];

                double aspectRatio = width / height;

                double hFoV = MathUtils.DegreesToRadians(p.FieldOfView) / 2;
                double xScale = Math.Tan(hFoV) * p.Position.Z;
                double yScale = 1 / aspectRatio * xScale;
                return new ScaleTransform3D(xScale, yScale, 1.0);
            }

            return new ScaleTransform3D(1, 1, 1);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


}
