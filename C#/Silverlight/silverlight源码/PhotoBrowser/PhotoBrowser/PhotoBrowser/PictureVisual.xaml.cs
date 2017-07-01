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
using System.Windows;

namespace PhotoBrowser
{
    public partial class PictureVisual : System.Windows.Controls.Grid
    {
        public PictureVisual()
		{
			InitializeComponent();

            buttonPanel.Visibility = Visibility.Hidden;
            closeButton.Visibility = Visibility.Hidden;

            DisableLocation();
            DisableComments();
        }

        protected override void  OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
 	         base.OnMouseEnter(e);

             buttonPanel.Visibility = Visibility.Visible;
             closeButton.Visibility = Visibility.Visible;
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            this.buttonPanel.Visibility = Visibility.Hidden;
            this.closeButton.Visibility = Visibility.Hidden;
        }

        public void EnableLocation()
        {
            geoButton.Visibility = Visibility.Visible;
        }

        public void DisableLocation()
        {
            geoButton.Visibility = Visibility.Collapsed;
        }

        public void EnableComments()
        {
            commentsButton.Visibility = Visibility.Visible;      
        }

        public void DisableComments()
        {
            commentsButton.Visibility = Visibility.Collapsed;
        }
    }
}
