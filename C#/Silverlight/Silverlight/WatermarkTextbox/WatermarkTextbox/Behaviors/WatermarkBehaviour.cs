using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Behaviors
{
    /// <summary>
    /// The <see cref="WatermarkBehaviour"/> class.
    /// </summary>
    public class WatermarkBehaviour : Behavior<TextBox>
    {
        public static readonly DependencyProperty BrushColorProperty;
        public static readonly DependencyProperty WatermarkTextProperty;

        private Brush newBrush;
        private Brush oldBrush;

        #region Behavior Initialization

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            oldBrush = AssociatedObject.Foreground;
            newBrush = new SolidColorBrush(BrushColor);
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            AssociatedObject.GotFocus += AssociatedObjectGotFocus;
            AssociatedObject.LostFocus += AssociatedObjectLostFocus;
        }

        /// <summary>
        /// Handles the Loaded event of the AssociatedObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.Text.Equals(string.Empty))
                return;
            AssociatedObject.Foreground = newBrush;
            AssociatedObject.Text = WatermarkText;
        }

        /// <summary>
        /// Handles the LostFocus event of the AssociatedObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AssociatedObjectLostFocus(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.Text.Equals(string.Empty)) return;
            AssociatedObject.Foreground = newBrush;
            AssociatedObject.Text = WatermarkText;
        }

        /// <summary>
        /// Handles the GotFocus event of the AssociatedObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AssociatedObjectGotFocus(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.Text.Equals(WatermarkText)) return;
            AssociatedObject.Foreground = oldBrush;
            AssociatedObject.Text = string.Empty;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            AssociatedObject.GotFocus -= AssociatedObjectGotFocus;
            AssociatedObject.LostFocus -= AssociatedObjectLostFocus;
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="WatermarkBehaviour"/> class.
        /// </summary>
        static WatermarkBehaviour()
        {
            WatermarkTextProperty =
                DependencyProperty.Register("WatermarkText", typeof (string), typeof (WatermarkBehaviour),
                                            new UIPropertyMetadata("Enter some text here"));
            BrushColorProperty =
                DependencyProperty.Register("BrushColor", typeof (Color), typeof (WatermarkBehaviour));
        }

        /// <summary>
        /// Gets or sets the color of the brush.
        /// </summary>
        /// <value>The color of the brush.</value>
        public Color BrushColor
        {
            get { return (Color) GetValue(BrushColorProperty); }
            set { SetValue(BrushColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark text.
        /// </summary>
        /// <value>The watermark text.</value>
        public string WatermarkText
        {
            get { return (string) GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }
    }
}