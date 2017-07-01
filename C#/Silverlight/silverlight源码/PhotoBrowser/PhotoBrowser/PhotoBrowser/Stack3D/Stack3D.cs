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
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using _3DTools;

namespace PhotoBrowser.Stack3D
{
    /// <summary>
    /// The Stack3D represents a stack of 3D objects
    /// </summary>
    public abstract class Stack3D : ModelVisual3D
    {
        public Stack3D()
        {
            _behindTransform = new TranslateTransform3D(0, 0, 0);
            _topRotateTransform = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
        }

        /// <summary>
        /// Adds a new item to the stack of objects
        /// </summary>
        /// <param name="o"></param>
        public virtual void AddItem(object o)
        {
            InteractiveVisual3D vis = GetVisual3DRepresentation(o);

            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0)));
            transform.Children.Add(new TranslateTransform3D());
            vis.Transform = transform;

            ((UIElement)vis.Visual).MouseLeftButtonDown +=
                new MouseButtonEventHandler(
                    delegate(object sender, MouseButtonEventArgs e)
                    {
                        OnMouseLeftButtonDown(vis, o, e);
                    });

            ((UIElement)vis.Visual).MouseLeftButtonUp +=
                new MouseButtonEventHandler(
                    delegate(object sender, MouseButtonEventArgs e)
                    {
                        OnMouseLeftButtonUp(vis, o, e);
                    });

            ((UIElement)vis.Visual).MouseMove +=
                new MouseEventHandler(
                    delegate(object sender, MouseEventArgs e)
                    {
                        OnMouseMove(vis, o, e);
                    });

            Children.Insert(0, vis); 
            UpdateToDefaultPositions();

            SelectionChanged(o);
        }
        
        /// <summary>
        /// Moves all the objects back to their default positions
        /// </summary>
        private void UpdateToDefaultPositions()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                SetDefaultPosition(i);
            }
        }

        /// <summary>
        /// Animates the object at the given index back to its default location and rotation
        /// </summary>
        /// <param name="index"></param>
        private void SetDefaultPosition(int index)
        {
            Vector3D offset = GetDefaultOffset(index);
            Duration d = new Duration(new TimeSpan(0, 0, 0, 0, 300));

            ModelVisual3D v = (ModelVisual3D)Children[index];            
            AxisAngleRotation3D rotation = (AxisAngleRotation3D)((RotateTransform3D)((Transform3DGroup)v.Transform).Children[0]).Rotation;
            TranslateTransform3D translation = ((TranslateTransform3D)((Transform3DGroup)v.Transform).Children[1]);

            // the following code figures out which in which direction to apply the rotation animation
            // so the object returns to its default rotation amount
            double rotateTargetVal = 0;
            double currRotation = rotation.Angle;
            if (currRotation > 360)
            {
                currRotation = currRotation % 360;
            }
            else if (currRotation < 0)
            {
                currRotation = 360 - (-currRotation) % 360;
            }

            if (currRotation > 90 && currRotation < 270)
            {
                rotateTargetVal = 180;
            }
            else
            {
                if (currRotation > 270) currRotation -= 360;                
            }

            DoubleAnimation rotateAnimation = new DoubleAnimation(currRotation, rotateTargetVal, d);
            DoubleAnimation xAnimation = new DoubleAnimation(offset.X, d);
            DoubleAnimation zAnimation = new DoubleAnimation(offset.Z, d);

            rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotateAnimation);
            ((RotateTransform3D)((Transform3DGroup)v.Transform).Children[0]).CenterX = ((ScaleTransform3D)v.Content.Transform).ScaleX / 2;

            translation.BeginAnimation(TranslateTransform3D.OffsetXProperty, xAnimation);
            translation.BeginAnimation(TranslateTransform3D.OffsetZProperty, zAnimation);                       
        }

        /// <summary>
        /// Gets the default offset for the object at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Vector3D GetDefaultOffset(int index)
        {
            double zSpacing = 0.0;
            if (index > 0)
            {
                zSpacing += (index) * ZSpacing;
            }

            double xSpacing = -0.03 * index;
            double ySpacing = 0 * index;

            return new Vector3D(xSpacing, ySpacing, zSpacing);
        }

        /// <summary>
        /// Moves the given object to the front of the stack
        /// </summary>
        /// <param name="o"></param>
        public virtual void MoveToFront(object o)
        {
            int index = GetIndex(o);

            if (index != 0)
            {
                ModelVisual3D v = (ModelVisual3D)this.Children[index];

                this.Children.RemoveAt(index);
                this.Children.Insert(0, v);

                for (int i = 1; i < Children.Count; i++)
                {
                    SetDefaultPosition(i);
                }

                AnimateToFront(v);

                SelectionChanged(o);
            }
        }

        /// <summary>
        /// Event used to represent when the selected item has changed
        /// </summary>
        public delegate void StackSelectionChangedEvent(object selected);
        public event StackSelectionChangedEvent SelectionChanged ;

        /// <summary>
        /// Animates the given 3D object to the front of the stack
        /// </summary>
        /// <param name="visual3D"></param>
        private void AnimateToFront(ModelVisual3D visual3D)
        {
            TranslateTransform3D transform = ((TranslateTransform3D)((Transform3DGroup)visual3D.Transform).Children[1]);

            LinearDoubleKeyFrame key1 = new LinearDoubleKeyFrame(-Width, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)));
            LinearDoubleKeyFrame key2 = new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)));
            LinearDoubleKeyFrame key3 = new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(900)));

            DoubleAnimationUsingKeyFrames doubleAnimX = new DoubleAnimationUsingKeyFrames();
            doubleAnimX.KeyFrames.Add(key1);
            doubleAnimX.KeyFrames.Add(key3);

            DoubleAnimationUsingKeyFrames doubleAnimZ = new DoubleAnimationUsingKeyFrames();
            doubleAnimZ.KeyFrames.Add(key2);
            doubleAnimZ.BeginTime = TimeSpan.FromMilliseconds(300);

            transform.BeginAnimation(TranslateTransform3D.OffsetXProperty, doubleAnimX);
            transform.BeginAnimation(TranslateTransform3D.OffsetZProperty, doubleAnimZ);
        }

        protected void OnMouseLeftButtonDown(InteractiveVisual3D visual3D, 
                                             Object data,
                                             MouseButtonEventArgs mouseEventArgs)
        {
            int index = Children.IndexOf(visual3D);
            if (index == 0)
            {
                // grab capture on the 2d element
                UIElement uie = (UIElement)visual3D.Visual;
                
                Viewport3D viewport = Visual3DHelper.GetVisual3DViewport(visual3D);

                mouseCapturePos = Mouse.GetPosition(viewport);

                // create the animation                                
                Duration d = new Duration(new TimeSpan(0, 0, 1));

                DoubleAnimation doubleAnimation = new DoubleAnimation(0, -Width, d);
                DoubleAnimation anotherAnimation = new DoubleAnimation(0, 0.01 * Width / ZSpacing, d);
                doubleAnimation.IsAdditive = true;
                anotherAnimation.IsAdditive = true;

                for (int i = 1; i < Children.Count; i++)
                {
                    ModelVisual3D v = (ModelVisual3D)Children[i];

                    TranslateTransform3D translate = (TranslateTransform3D)((Transform3DGroup)v.Transform).Children[1];
                    translate.BeginAnimation(TranslateTransform3D.OffsetZProperty, doubleAnimation);
                    translate.BeginAnimation(TranslateTransform3D.OffsetXProperty, anotherAnimation);
                }
                

                uie.CaptureMouse();
            }
            else
            {
                MoveToFront(data);
            }

            mouseEventArgs.Handled = true;
        }

        protected void OnMouseLeftButtonUp(InteractiveVisual3D visual3D,
                                           Object data,
                                           MouseButtonEventArgs mouseEventArgs)
        {
            if (((UIElement)visual3D.Visual).IsMouseCaptured)
            {
                UIElement uie = (UIElement)visual3D.Visual;
                uie.ReleaseMouseCapture();

                UpdateToDefaultPositions();

                mouseEventArgs.Handled = true;
            }
        }

        protected void OnMouseMove(InteractiveVisual3D visual3D,
                                   Object data,
                                   MouseEventArgs mouseEventArgs)
        {
            if (((UIElement)visual3D.Visual).IsMouseCaptured)
            {
                Viewport3D viewport = Visual3DHelper.GetVisual3DViewport(visual3D);
                Point newMouseCapturePos = Mouse.GetPosition(viewport);

                Vector diff = newMouseCapturePos - mouseCapturePos;
                mouseCapturePos = newMouseCapturePos;

                Transform3DGroup t3Dgroup = (Transform3DGroup)visual3D.Transform;
                RotateTransform3D rt3D = (RotateTransform3D)t3Dgroup.Children[0];
                AxisAngleRotation3D axisAngle = (AxisAngleRotation3D)rt3D.Rotation;

                if (axisAngle.HasAnimatedProperties)
                {
                    double oldAngle = axisAngle.Angle;
                    axisAngle.BeginAnimation(AxisAngleRotation3D.AngleProperty, null);
                    axisAngle.Angle = oldAngle;
                }
                axisAngle.Angle += diff.X;

                mouseEventArgs.Handled = true;
            }
        }

        /// <summary>
        /// Removes an item from the stack
        /// </summary>
        /// <param name="o"></param>
        public virtual void RemoveItem(object o)
        {
            int index = GetIndex(o);
            this.Children.RemoveAt(index);

            UpdateToDefaultPositions();

            if (index == 0) SelectionChanged(o);
        }

        /// <summary>
        /// Abstract method used to get the ModelVisual3D representation of the
        /// the given data
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected abstract InteractiveVisual3D GetVisual3DRepresentation(object o);

        /// <summary>
        /// Abstract method used to get the index of the given data
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected abstract int GetIndex(object o);

        /// <summary>
        /// The Total number of data items that this stack is displaying
        /// </summary>
        protected abstract int Count
        {
            get;
        }

        /// <summary>
        /// The Z spacing between the 3D objects in the stack
        /// </summary>
        protected abstract double ZSpacing
        {
            get;
        }

        /// <summary>
        /// The width of the 3D stack
        /// </summary>
        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The height of the 3D stack
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        ///////////////////////////////////
        // Private Data
        ///////////////////////////////////        

        private Point mouseCapturePos;
                
        private double _height = 0.4;
        private double _width = 0.4;
        
        private TranslateTransform3D _behindTransform;
        private AxisAngleRotation3D _topRotateTransform;       
    }
}
