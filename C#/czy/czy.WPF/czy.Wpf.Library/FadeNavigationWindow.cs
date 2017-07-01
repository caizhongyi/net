using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace czy.Wpf.Library
{
    /// <summary>
    /// 渐淡式切换页面
    /// </summary>
    public class FadeNavigationWindow
    {
        NavigationWindow window = null;
        public  void NavigationWindow_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            window = (sender as NavigationWindow);
            if (window.Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;
                _navArgs = e;
                window.IsHitTestVisible = false;
                DoubleAnimation da = new DoubleAnimation(0.3d, new Duration(TimeSpan.FromMilliseconds(300)));
                da.Completed += FadeOutCompleted;
                window.BeginAnimation(NavigationWindow.OpacityProperty, da);
            }
            _allowDirectNavigation = false;
        }

        private void FadeOutCompleted(object sender, EventArgs e)
        {
            (sender as AnimationClock).Completed -= FadeOutCompleted;

            window.IsHitTestVisible = true;

            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                    {
                        window.NavigationService.Navigate(_navArgs.Content);
                    }
                    else
                    {
                        window.NavigationService.Navigate(_navArgs.Uri);
                    }
                    break;
                case NavigationMode.Back:
                    window.NavigationService.GoBack();
                    break;

                case NavigationMode.Forward:
                    window.NavigationService.GoForward();
                    break;
                case NavigationMode.Refresh:
                    window.NavigationService.Refresh();
                    break;
            }

            window.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate()
            {
                DoubleAnimation da = new DoubleAnimation(1.0d, new Duration(TimeSpan.FromMilliseconds(200)));
                window.BeginAnimation(NavigationWindow.OpacityProperty, da);
            });
        }

        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;
    }
}

