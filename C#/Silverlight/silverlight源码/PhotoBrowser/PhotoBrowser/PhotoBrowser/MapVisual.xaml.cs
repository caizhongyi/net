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
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Input;

namespace PhotoBrowser
{
	public partial class MapVisual : Grid
	{
        public MapVisual()
		{
			this.InitializeComponent();

			mapImage.ContextMenu = null;
		}
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // capture the mouse and then add the drawing rect
            Mouse.Capture(mapImage);
            _mouseCaptureLoc = e.GetPosition(this);

            SelectionRect.Width = 0;
            SelectionRect.Height = 0;

            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mapImage.IsMouseCaptured)
            {
                Point currMouseLoc = e.GetPosition(mapImage);

                SelectionRect.Width = Math.Abs(_mouseCaptureLoc.X - currMouseLoc.X);
                SelectionRect.Height = Math.Abs(_mouseCaptureLoc.Y - currMouseLoc.Y);

                SelectionRect.Margin = new Thickness(Math.Min(_mouseCaptureLoc.X, currMouseLoc.X),
                                                     Math.Min(_mouseCaptureLoc.Y, currMouseLoc.Y),
                                                     0, 0);

                e.Handled = true;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (mapImage.IsMouseCaptured)
            {
                if (_mouseCaptureLoc == e.GetPosition(mapImage))
                {
                    mapImage.ContextMenu = null;
                }
                else
                {
                    mapImage.ContextMenu = cm;
                }

                mapImage.ReleaseMouseCapture();
            }
        }

        /// <summary>
        /// Takes the selection rectange and converts from x,y values in to latitude and
        /// longitude ones that can be used in a search
        /// </summary>
        /// <returns></returns>
        public Rect GetSearchRect()
        {
            double longitudeStart = (SelectionRect.Margin.Left / ActualWidth) * 360 - 180.0;
            double latitudeStart = -((SelectionRect.Margin.Top + SelectionRect.Height) / ActualHeight) * 180 + 90.0;

            double longitudeWidth = (SelectionRect.Width) / ActualWidth * 360;
            double latitudeWidth = (SelectionRect.Height) / ActualHeight * 180;

            return new Rect(longitudeStart, latitudeStart, longitudeWidth, latitudeWidth);
        }

        private Point _mouseCaptureLoc;
	}
}
