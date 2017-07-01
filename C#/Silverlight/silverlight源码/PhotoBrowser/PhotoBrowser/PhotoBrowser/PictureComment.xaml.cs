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
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace PhotoBrowser
{
    public partial class PictureComment : System.Windows.Controls.Grid
    {
        public PictureComment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Scales the overall width and height based on which side of the image
        /// is larger.
        /// </summary>
        /// <param name="bitmap"></param>
        public void UpdateImage(BitmapImage bitmap)
        {
            pictureVisual.image.Source = bitmap;

            if (bitmap.Width > bitmap.Height)
            {
                Height = (Width / 2.0) * bitmap.Height / bitmap.Width;
            }
            else
            {
                Width = 2 * Height * bitmap.Width / bitmap.Height;
            }
        }
    }
}
