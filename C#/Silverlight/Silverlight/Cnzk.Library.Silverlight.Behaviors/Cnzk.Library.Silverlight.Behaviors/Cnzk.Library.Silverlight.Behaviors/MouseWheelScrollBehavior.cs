using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interactivity;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Automation;

namespace Cnzk.Library.Silverlight.Behaviors {
    public enum ScrollOrientations {
        Auto = 0,
        None = 1,
        Vertical = 2,
        Horizontal = 3
    }

    public class MouseWheelScrollBehavior : Behavior<FrameworkElement> {
        public MouseWheelScrollBehavior() {
        }

        private ScrollViewer scrollObject = null;
        private ScrollViewer ScrollObject {
            get { return scrollObject; }
            set {
                if (scrollObject != value) {
                    scrollObject = value;
                    if (scrollObject != null) {
                        var peer = FrameworkElementAutomationPeer.CreatePeerForElement(ScrollObject);
                        _scrollProvider = peer.GetPattern(PatternInterface.Scroll) as IScrollProvider;
                    } else {
                        _scrollProvider = null;
                    }
                }
            }
        }

        private IScrollProvider _scrollProvider = null;

        private ScrollViewer GetChildScrollViewer(DependencyObject parentObject) {
            ScrollViewer result = parentObject as ScrollViewer;

            if (result == null && parentObject != null) {
                if (parentObject is Popup) {
                    result = GetChildScrollViewer(((Popup)parentObject).Child);
                } else {
                    var total = VisualTreeHelper.GetChildrenCount(parentObject);
                    for (int i = 0; i < total; i++) {
                        var child = VisualTreeHelper.GetChild(parentObject, i);
                        result = GetChildScrollViewer(child);
                        if (result != null) break;
                    }
                }
            }

            return result;
        }

        protected override void OnAttached() {
            base.OnAttached();

            if (this.AssociatedObject != null) {
                this.AssociatedObject.LayoutUpdated += AssociatedObject_LayoutUpdated;
            }

            LoadChildScrollViewer();
        }

        void AssociatedObject_LayoutUpdated(object sender, EventArgs e) {
            LoadChildScrollViewer();
        }

        private void LoadChildScrollViewer() {
            this.ScrollObject = GetChildScrollViewer(this.AssociatedObject);
            if (this.ScrollObject != null) {
                this.ScrollObject.MouseWheel += scrollObject_MouseWheel;
                if (this.AssociatedObject != null && !(this.AssociatedObject is ScrollViewer)) {
                    this.AssociatedObject.LayoutUpdated -= AssociatedObject_LayoutUpdated;
                }
            }
        }

        private void scrollObject_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (_scrollProvider != null) {
                int newDelta = InvertScrollDirection ? -e.Delta : e.Delta;
                ScrollAmount amount = newDelta < 0 ? ScrollAmount.SmallIncrement : ScrollAmount.SmallDecrement;
                switch (GetCurrentScrollOrientation()) {
                    case ScrollOrientations.Vertical:
                        e.Handled = true;
                        _scrollProvider.Scroll(ScrollAmount.NoAmount, amount);
                        break;
                    case ScrollOrientations.Horizontal:
                        e.Handled = true;
                        _scrollProvider.Scroll(amount, ScrollAmount.NoAmount);
                        break;
                }
            }
        }

        private ScrollOrientations GetCurrentScrollOrientation() {
            ScrollOrientations result = ScrollOrientations.None;
            var scrl = this.ScrollObject;
            var sd = ScrollOrientation;
            if (scrl != null && sd != ScrollOrientations.None) {
                if (sd == ScrollOrientations.Auto) {
                    if (scrl.ScrollableHeight > 0) {
                        result = ScrollOrientations.Vertical;
                    } else if (scrl.ScrollableWidth > 0) {
                        result = ScrollOrientations.Horizontal;
                    }
                } else {
                    result = sd;
                }
            }

            return result;
        }

        protected override void OnDetaching() {
            base.OnDetaching();

            if (this.ScrollObject != null) {
                this.ScrollObject.MouseWheel -= scrollObject_MouseWheel;
            }
        }

        #region DependencyProperty ScrollOrientation
        public static readonly DependencyProperty ScrollOrientationProperty = DependencyProperty.Register("ScrollOrientation",
            typeof(ScrollOrientations), typeof(MouseWheelScrollBehavior), null);

        [DefaultValue(ScrollOrientations.Auto)]
        public ScrollOrientations ScrollOrientation {
            get { return (ScrollOrientations)GetValue(ScrollOrientationProperty); }
            set { SetValue(ScrollOrientationProperty, value); }
        }
        #endregion

        #region DependencyProperty InvertScrollDirection
        public static readonly DependencyProperty InvertScrollDirectionProperty = DependencyProperty.Register("InvertScrollDirection",
            typeof(bool), typeof(MouseWheelScrollBehavior), null);

        [DefaultValue(false)]
        public bool InvertScrollDirection {
            get { return (bool)GetValue(InvertScrollDirectionProperty); }
            set { SetValue(InvertScrollDirectionProperty, value); }
        }
        #endregion

    }
}
