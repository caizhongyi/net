using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DeepEarth;
using DeepEarth.Geometry;

namespace DeepEarthPrototype.Controls
{
    public class DevPin : PointBase
    {

        private DoubleAnimation _AnimateScaleX;
        private DoubleAnimation _AnimateScaleY;

        private Image _ImageInfo;
        private bool _IsMouseOver;
        private Grid _MaxBalloon;
        private Grid _MinBalloon;
        private Storyboard _ScaleAnimation;
        private ScaleTransform _ScalePin;
        private Storyboard _ShowInfoBalloon;


        //public DevPin(GeometryLayer layer): base(layer)
        public DevPin()
        {
            MouseEnter += MouseEnterPin;
            MouseLeave += MouseLeavePin;
            Style = Application.Current.Resources["DevPinStyle"] as Style;
        }

        #region MouseOver-Based Scaling Functions

        private Storyboard ScaleAnimation
        {
            get
            {
                if (_ScaleAnimation == null)
                {
                    // Create two DoubleAnimations and set their properties.
                    _ScaleAnimation = new Storyboard {Duration = new Duration(TimeSpan.FromSeconds(0.4))};
                    _ScaleAnimation.Children.Add(AnimateScaleX);
                    _ScaleAnimation.Children.Add(AnimateScaleY);
                }
                return _ScaleAnimation;
            }
        }

        private DoubleAnimation AnimateScaleX
        {
            get
            {
                if (_AnimateScaleX == null)
                {
                    _AnimateScaleX = new DoubleAnimation {Duration = new Duration(TimeSpan.FromSeconds(0.4))};
                    Storyboard.SetTarget(_AnimateScaleX, _ScalePin);
                    Storyboard.SetTargetProperty(_AnimateScaleX, new PropertyPath("(ScaleX)"));
                }
                return _AnimateScaleX;
            }
        }

        private DoubleAnimation AnimateScaleY
        {
            get
            {
                if (_AnimateScaleY == null)
                {
                    _AnimateScaleY = new DoubleAnimation {Duration = new Duration(TimeSpan.FromSeconds(0.4))};
                    Storyboard.SetTarget(_AnimateScaleY, _ScalePin);
                    Storyboard.SetTargetProperty(_AnimateScaleY, new PropertyPath("(ScaleY)"));
                }
                return _AnimateScaleY;
            }
        }

        private void MouseEnterPin(object sender, MouseEventArgs e)
        {
            _IsMouseOver = true;
            MapInstance.Events.EnableMouseClicks = false;
            this.Layer.SendToTop(this);
            UpdateVisualState();
        }

        private void MouseLeavePin(object sender, MouseEventArgs e)
        {
            _IsMouseOver = false;
            MapInstance.Events.EnableMouseClicks = true;
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            if (_IsMouseOver)
            {
                ScaleTo(1.5);
            }
            else
            {
                ScaleTo(1);
            }
        }

        internal void ScaleTo(double ToValue)
        {
            AnimateScaleX.To = ToValue;
            AnimateScaleY.To = ToValue;
            ScaleAnimation.Begin();
        }

        #endregion

        #region Balloon Functions

        private bool _CommandsInitialized;

        private void MinBalloon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();

            InitializeCommands();

            _ShowInfoBalloon.Begin();
            _MaxBalloon.Visibility = Visibility.Visible;
            _MinBalloon.Visibility = Visibility.Collapsed;
        }

        private void HideInfoBalloon(object sender, MouseEventArgs e)
        {
            HideInfoBalloon();
        }

        private void HideInfoBalloon()
        {
            _ShowInfoBalloon.Stop();
            _MaxBalloon.Visibility = Visibility.Collapsed;
            _MinBalloon.Visibility = Visibility.Visible;
        }

        private void InitializeCommands()
        {
            if (_CommandsInitialized == false)
            {
                _MaxBalloon = (Grid) GetTemplateChild("PART_MaxBalloon");
                _MaxBalloon.MouseLeave += HideInfoBalloon;
                _CommandsInitialized = true;
            }
        }

        #endregion

        public override void OnApplyTemplate()
        {
            _ImageInfo = (Image) GetTemplateChild("PART_InfoImage");
            _ScalePin = (ScaleTransform) GetTemplateChild("PART_PinScale");
            _ShowInfoBalloon = (Storyboard) GetTemplateChild("PART_ShowInfoBalloon");
            _MinBalloon = (Grid) GetTemplateChild("PART_MinBalloon");


            bool designTime = (HtmlPage.IsEnabled == false);
            if (designTime == false)
            {
                _ImageInfo.MouseLeftButtonDown += MinBalloon_MouseLeftButtonDown;
                _IsLoaded = true;
                ForceMeasure();
                Layer.UpdateChildLocation(this);
            }
        }


    }
}