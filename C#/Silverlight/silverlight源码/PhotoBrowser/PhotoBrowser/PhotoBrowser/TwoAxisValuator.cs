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
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Markup; // IAddChild, ContentPropertyAttribute
using _3DTools;

namespace PhotoBrowser
{
    /// <summary>
    /// Class to implement a TwoAxisValuator control.
    /// </summary>
    public class TwoAxisValuator : Viewport3DDecorator
    {               
        public TwoAxisValuator() : base()
        {
            // the transform that will be applied to the viewport 3d's camera
            _rotation = new AxisAngleRotation3D();
            _transform = new RotateTransform3D(_rotation);
            
            // we add a clear sheet of glass behind everything, this is
            // used so that we always get events while in the viewport 3D
            Border _eventSource = new Border();
            _eventSource.Background = Brushes.Transparent;      
            PreViewportChildren.Add(_eventSource);
        }

        /// <summary>
        /// Dependency property representing the "up" direction that the two-axis valuator rotates
        /// around when the mouse moves to the right.
        /// </summary>
        public static DependencyProperty UpProperty =
            DependencyProperty.Register(
                "UpProperty",
                typeof(Vector3D),
                typeof(TwoAxisValuator),
                new PropertyMetadata(new Vector3D(0, 1, 0)));

        public Vector3D Up
        {
            get { return (Vector3D)GetValue(UpProperty); }
            set { SetValue(UpProperty, value); }
        }      
 
        /// <summary>
        /// Dependency property representing the "right" direction that the two-axis valuator rotates
        /// around when the mouse moves up.
        /// </summary>
        public static DependencyProperty RightProperty =
            DependencyProperty.Register(
                "RightProperty",
                typeof(Vector3D),
                typeof(TwoAxisValuator),
                new PropertyMetadata(new Vector3D(1, 0, 0)));

        public Vector3D Right
        {
            get { return (Vector3D)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }        
       
        /// <summary>
        /// DependencyProperty letting us know whether or not the two-axis valuator is enabled
        /// </summary>
        public static DependencyProperty EnabledProperty =
            DependencyProperty.Register(
                "EnabledProperty",
                typeof(bool),
                typeof(TwoAxisValuator),
                new PropertyMetadata(false));

        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }

        /// <summary>
        /// DependencyProperty letting us know whether or not we can rotate around y
        /// </summary>
        public static DependencyProperty YAxisLockedProperty =
            DependencyProperty.Register(
                "YAxisLockedProperty",
                typeof(bool),
                typeof(TwoAxisValuator),
                new PropertyMetadata(false));

        public bool YAxisLocked
        {
            get { return (bool)GetValue(YAxisLockedProperty); }
            set { SetValue(YAxisLockedProperty, value); }
        }

        /// <summary>
        /// DependencyProperty letting us know whether or not we can rotate around x
        /// </summary>
        public static DependencyProperty XAxisLockedProperty =
            DependencyProperty.Register(
                "XAxisLockedProperty",
                typeof(bool),
                typeof(TwoAxisValuator),
                new PropertyMetadata(false));

        public bool XAxisLocked
        {
            get { return (bool)GetValue(XAxisLockedProperty); }
            set { SetValue(XAxisLockedProperty, value); }
        }

        
        #region Event Handling

        /// <summary>
        /// Captures the mouse if the two-axis valuator is enabled.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (Enabled)
            {
                _previousPosition2D = e.GetPosition(this);

                // if the mouse is already captured we won't attempt to revoke capture
                // and will instead ignore the event
                if (Mouse.Captured == null)
                {
                    Mouse.Capture(this, CaptureMode.Element);
                }
            }
        }

        /// <summary>
        /// Released capture if the two-axis valuator had previously gained capture.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (IsMouseCaptured)
            {
                Mouse.Capture(this, CaptureMode.None);
            }
        }

        /// <summary>
        /// Updates the current camera transformation if the mouse is captured
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsMouseCaptured)
            {
                Point currentPosition = e.GetPosition(this);

                // avoid any zero axis conditions
                if (currentPosition == _previousPosition2D) return;

                // Prefer tracking to zooming if both buttons are pressed.
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Track(_previousPosition2D, currentPosition);
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    Zoom(_previousPosition2D, currentPosition);
                }

                _previousPosition2D = currentPosition;

                Viewport3D viewport3D = this.Viewport3D;
                if (viewport3D != null)
                {
                    if (viewport3D.Camera.Transform != _transform)
                    {
                        viewport3D.Camera.Transform = _transform;
                    }
                }
            }
        }

        #endregion Event Handling

        /// <summary>
        /// Updates the two-axis valuator's rotation matrix based upon the
        /// difference between the previous position and the current one.
        /// </summary>
        /// <param name="previousPosition">Old position</param>
        /// <param name="currentPosition">New position</param>
        private void Track(Point previousPosition, Point currentPosition)
        {
            double xDiff = currentPosition.X - previousPosition.X;
            double yDiff = currentPosition.Y - previousPosition.Y;

            // create the rotations to be applied
            Quaternion horzRotation = new Quaternion(Up, -xDiff);
            Quaternion vertRotation = new Quaternion(Right, -yDiff);

            // Get the current orientantion from the RotateTransform3D
            AxisAngleRotation3D r = _rotation;
            Quaternion q = new Quaternion(_rotation.Axis, _rotation.Angle);

            // Compose the delta with the previous orientation
            if (!YAxisLocked) q *= horzRotation;
            if (!XAxisLocked) q *= vertRotation;
             
            // Write the new orientation back to the Rotation3D
            _rotation.Axis = q.Axis;
            _rotation.Angle = q.Angle;
        }

        /// <summary>
        /// Updates the field of view property of the camera based upon the
        /// difference between the previous position and the current one.
        /// Changing the field of view effectively zooms the viewport.
        /// </summary>
        /// <param name="previousPosition">Old position</param>
        /// <param name="currentPosition">New position</param>
        private void Zoom(Point previousPosition, Point currentPosition)
        {
            double yDelta = currentPosition.Y - previousPosition.Y;
            
            double scale = Math.Exp(yDelta / 100);    // e^(yDelta/100) is fairly arbitrary.

            if (Viewport3D != null & Viewport3D.Camera is PerspectiveCamera)
            {
                ((PerspectiveCamera)Viewport3D.Camera).FieldOfView += yDelta;
            }
        }

        //--------------------------------------------------------------------
        //
        // Private data
        //
        //--------------------------------------------------------------------
        private Point _previousPosition2D;

        private AxisAngleRotation3D _rotation;
        private Transform3D _transform;
    }
}
