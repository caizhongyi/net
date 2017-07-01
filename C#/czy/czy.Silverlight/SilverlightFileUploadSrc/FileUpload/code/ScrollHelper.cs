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
using System.Windows.Browser;

namespace DC.FileUpload
{
    public class ScrollHelper
    {
        public double incremental { get; set; }
        private ScrollViewer _scroll;
        private bool mouseinside = false;
        public ScrollHelper(ScrollViewer scroll)
        {
            incremental = 50;

            HtmlPage.Window.AttachEvent("DOMMouseScroll", OnMouseWheelTurned);
            HtmlPage.Window.AttachEvent("onmousewheel", OnMouseWheelTurned);
            HtmlPage.Document.AttachEvent("onmousewheel", OnMouseWheelTurned);


            _scroll = scroll;
            FrameworkElement fe = _scroll.Content as FrameworkElement;
            if (fe != null)
            {
                fe.MouseEnter += new MouseEventHandler(MouseEnter);
                fe.MouseLeave += new MouseEventHandler(MouseLeave);
                fe.MouseMove += new MouseEventHandler(MouseMove);
            }
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseinside)
                mouseinside = true;
        }

        void MouseLeave(object sender, MouseEventArgs e)
        {
            mouseinside = false;
        }

        void MouseEnter(object sender, MouseEventArgs e)
        {
            mouseinside = true;
        }

        private void OnMouseWheelTurned(Object sender, HtmlEventArgs args)
        {
            if (!mouseinside)
                return;
            double delta = 0;
            ScriptObject eventObj = args.EventObject;
            if (eventObj.GetProperty("wheelDelta") != null)  // IE and Opera
            {
                delta = ((double)eventObj.GetProperty("wheelDelta")) / 120;
                if (HtmlPage.Window.GetProperty("opera") != null)
                    delta = -delta;
            }
            else if (eventObj.GetProperty("detail") != null) // Mozilla and Safari 
            {
                delta = -((double)eventObj.GetProperty("detail")) / 3;
                if (HtmlPage.BrowserInformation.UserAgent.IndexOf("Macintosh") != -1)
                    delta = delta * 3;
            }
            if (delta != 0)
            {
                args.PreventDefault();
                eventObj.SetProperty("returnValue", false);
            }

            double Offset = _scroll.VerticalOffset;
            Offset -= delta * incremental;

            if (Offset < 0)
                Offset = 0;
            else if (Offset > _scroll.ScrollableHeight)
                Offset = _scroll.ScrollableHeight;
            // In the real case I call my ScrollTo() method which will check the Offset value and modify it to my needs as I explain at the end of the comment

            _scroll.ScrollToVerticalOffset(Offset);
        }
    }
}
