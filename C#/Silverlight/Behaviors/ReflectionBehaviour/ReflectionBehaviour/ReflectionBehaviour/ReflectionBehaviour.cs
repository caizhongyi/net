using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Effects;

namespace ReflectionBehaviour
{
    /// <summary>
    /// The <see cref="ReflectionBehaviour"/> class.
    /// </summary>
    public class ReflectionBehaviour : Behavior<UIElement>
    {
        public static readonly DependencyProperty ElementHeightProperty;

        #region Overrides of Behavior

        private readonly ReflectionShader newEffect;
        private Effect oldEffect;

        /// <summary>
        /// Initializes the <see cref="ReflectionBehaviour"/> class.
        /// </summary>
        static ReflectionBehaviour()
        {
            ElementHeightProperty =
                DependencyProperty.Register("ElementHeight",
                                            typeof (double), typeof (ReflectionBehaviour),
                                            new PropertyMetadata(80.0, OnElementHeightChanged));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionBehaviour"/> class.
        /// </summary>
        public ReflectionBehaviour()
        {
            newEffect = new ReflectionShader();
        }

        /// <summary>
        /// Gets or sets the height of the element.
        /// </summary>
        /// <value>The height of the element.</value>
        public double ElementHeight
        {
            get { return (double) GetValue(ElementHeightProperty); }
            set { SetValue(ElementHeightProperty, value); }
        }

        /// <summary>
        /// Called when [element height changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnElementHeightChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            var rs = d as ReflectionBehaviour;
            if (rs != null) rs.OnElementHeightChanged((double) e.OldValue, (double) e.NewValue);
        }

        /// <summary>
        /// Called when [element height changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnElementHeightChanged(double oldValue, double newValue)
        {
            newEffect.PaddingBottomProp = newValue;
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            oldEffect = AssociatedObject.Effect;
            AssociatedObject.Effect = newEffect;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Effect = oldEffect;
        }

        #endregion
    }
}