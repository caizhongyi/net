﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Microsoft.Windows.Controls
{
    /// <summary>
    /// Represents a control that enables single value selection from a numeric
    /// range of values through a Spinner and TextBox.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [TemplateVisualState(Name = VisualStates.StateNormal, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateMouseOver, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StatePressed, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateDisabled, GroupName = VisualStates.GroupCommon)]

    [TemplateVisualState(Name = VisualStates.StateFocused, GroupName = VisualStates.GroupFocus)]
    [TemplateVisualState(Name = VisualStates.StateUnfocused, GroupName = VisualStates.GroupFocus)]

    [TemplatePart(Name = UpDownBase.ElementTextName, Type = typeof(TextBox))]
    [TemplatePart(Name = UpDownBase.ElementSpinnerName, Type = typeof(Spinner))]
    [StyleTypedProperty(Property = UpDownBase.SpinnerStyleName, StyleTargetType = typeof(Spinner))]
    public partial class NumericUpDown : UpDownBase<double>
    {
        #region Minimum
        /// <summary>
        /// Gets or sets the Minimum possible Value.
        /// </summary>
        /// <remarks>
        /// The default value is zero.
        /// </remarks>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Identifies the Minimum dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(NumericUpDown),
                new PropertyMetadata(0d, OnMinimumPropertyChanged));

        /// <summary>
        /// MinimumProperty property changed handler.
        /// </summary>
        /// <param name="d">NumericUpDown that changed its Minimum.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs for Minimum property.</param>
        private static void OnMinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)d;
            EnsureValidDoubleValue(d, e);

            // Note: this section is a workaround, containing my
            // logic to hold all calls to the property changed
            // methods until after all coercion has completed
            // ----------
            if (nud._levelsFromRootCall == 0)
            {
                nud._requestedMin = (double)e.NewValue;
                nud._initialMin = (double)e.OldValue;
                nud._initialMax = nud.Maximum;
                nud._initialVal = nud.Value;

                nud._levelsFromRootCall++;
                // do a check before set to avoid blowing away binding unnecessarily.
                if (nud.Minimum != nud._requestedMin)
                {
                    nud.Minimum = nud._requestedMin;
                }
                nud._levelsFromRootCall--;
            }
            nud._levelsFromRootCall++;
            // ----------

            nud.CoerceMaximum();
            nud.CoerceValue();

            // Note: this section completes my workaround to call 
            // the property changed logic if all coercion has completed
            // ----------
            nud._levelsFromRootCall--;
            if (nud._levelsFromRootCall == 0)
            {
                ////RangeBaseAutomationPeer peer = nud.GetAutomationPeer() as RangeBaseAutomationPeer;

                double minimum = nud.Minimum;
                if (nud._initialMin != minimum)
                {
                    ////if (peer != null)
                    ////{
                    ////    peer.RaiseMinimumPropertyChangedEvent(nud._initialMin, minimum);
                    ////}

                    nud.OnMinimumChanged(nud._initialMin, minimum);
                }

                double maximum = nud.Maximum;
                if (nud._initialMax != maximum)
                {
                    ////if (peer != null)
                    ////{
                    ////    peer.RaiseMaximumPropertyChangedEvent(nud._initialMax, maximum);
                    ////}

                    nud.OnMaximumChanged(nud._initialMax, maximum);
                }

                ////double value = nud.Value;
                ////if (nud._initialVal != value)
                ////{
                ////    ////if (peer != null)
                ////    ////{
                ////    ////    peer.RaiseValuePropertyChangedEvent(nud._initialVal, value);
                ////    ////}

                ////    nud.OnValueChanged(nud._initialVal, value);
                ////}
            }
            // ----------
        }
        #endregion Minimum

        #region Maximum
        /// <summary>
        /// Gets or sets the Maximum possible Value.
        /// </summary>
        /// <remarks>
        /// The default values is one.
        /// </remarks>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Identifies the Maximum dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(NumericUpDown),
                new PropertyMetadata(100d, OnMaximumPropertyChanged));

        /// <summary>
        /// MaximumProperty property changed handler.
        /// </summary>
        /// <param name="d">NumericUpDown that changed its Maximum.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs for Maximum property.</param>
        private static void OnMaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnsureValidDoubleValue(d, e);
            NumericUpDown nud = d as NumericUpDown;

            // Note: this section is a workaround, containing my
            // logic to hold all calls to the property changed
            // methods until after all coercion has completed
            // ----------
            if (nud._levelsFromRootCall == 0)
            {
                nud._requestedMax = (double)e.NewValue;
                nud._initialMax = (double)e.OldValue;
                nud._initialVal = nud.Value;
            }
            nud._levelsFromRootCall++;
            // ----------

            nud.CoerceMaximum();
            nud.CoerceValue();

            // Note: this section completes my workaround to call 
            // the property changed logic if all coercion has completed
            // ----------
            nud._levelsFromRootCall--;
            if (nud._levelsFromRootCall == 0)
            {
                ////RangeBaseAutomationPeer peer = nud.GetAutomationPeer() as RangeBaseAutomationPeer;

                double maximum = nud.Maximum;
                if (nud._initialMax != maximum)
                {
                    ////if (peer != null)
                    ////{
                    ////    peer.RaiseMaximumPropertyChangedEvent(nud._initialMax, maximum);
                    ////}

                    nud.OnMaximumChanged(nud._initialMax, maximum);
                }
                ////double value = nud.Value;
                ////if (nud._initialVal != value)
                ////{
                ////    ////if (peer != null)
                ////    ////{
                ////    ////    peer.RaiseValuePropertyChangedEvent(nud._initialVal, value);
                ////    ////}

                ////    nud.OnValueChanged(nud._initialVal, value);
                ////}
            }
            // ----------
        }
        #endregion Maximum

        #region Increment
        /// <summary>
        /// Gets or sets a value added or subtracted from the value property.
        ///  </summary>
        /// <remarks>
        /// The default values is one.
        /// </remarks>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        /// <summary>
        /// Identifies the Increment dependency property.
        /// </summary>
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register(
                "Increment",
                typeof(double),
                typeof(NumericUpDown),
                new PropertyMetadata(1d, OnIncrementPropertyChanged));

        /// <summary>
        /// IncrementProperty property changed handler.
        /// </summary>
        /// <param name="d">NumericUpDown that changed its Increment property.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs for Increment property.</param>
        private static void OnIncrementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)d;
            EnsureValidIncrementValue(d, e);

            // Note: this section is a workaround, containing my
            // logic to hold all calls to the property changed
            // methods until after all coercion has completed
            // ----------
            if (nud._levelsFromRootCall == 0)
            {
                nud._requestedInc = (double)e.NewValue;
                nud._initialInc = (double)e.OldValue;

                nud._levelsFromRootCall++;
                // do a check before set to avoid blowing away binding unnecessarily.
                if (nud.Increment != nud._requestedInc)
                {
                    nud.Increment = nud._requestedInc;
                }
                nud._levelsFromRootCall--;
            }
            // ----------

            // Note: this section completes my workaround to call 
            // the property changed logic if all coercion has completed
            // ----------
            if (nud._levelsFromRootCall == 0)
            {
                ////RangeBaseAutomationPeer peer = nud.GetAutomationPeer() as RangeBaseAutomationPeer;

                double increment = nud.Increment;
                if (nud._initialInc != increment)
                {
                    ////if (peer != null)
                    ////{
                    ////    peer.RaiseIncrementPropertyChangedEvent(nud._initialInc, increment);
                    ////}

                    nud.OnIncrementChanged(nud._initialInc, increment);
                }
            }
            // ----------
        }
        #endregion

        #region DecimalPlaces
        /// <summary>
        /// Gets or sets the number of decimal places that are displayed in the 
        /// NumericUpDown. 
        /// </summary>
        /// <remarks>
        /// The default value is zero.
        /// 
        /// DecimalPlaces decides output format of Value property.
        /// It is implemented via formatString field and FormatValue override.
        /// </remarks>
        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        /// <summary>
        /// Identifies the DecimalPlaces dependency property.
        /// </summary>
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register(
                "DecimalPlaces",
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(0, OnDecimalPlacesPropertyChanged));

        /// <summary>
        /// DecimalPlacesProperty property changed handler.
        /// </summary>
        /// <param name="d">NumericUpDown that changed its DecimalPlaces.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs for DecimalPlaces property.</param>
        private static void OnDecimalPlacesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnsureValidDecimalPlacesValue(d, e);

            NumericUpDown nud = d as NumericUpDown;
            nud.OnDecimalPlacesChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// Format string used to display Value property.
        /// </summary>
        /// <seealso cref="DecimalPlaces"/>
        /// <seealso cref="FormatValue"/>
        private string formatString = "F0";
        #endregion DecimalPlaces

        /// <summary>
        /// Initializes a new instance of the NumericUpDown class.
        /// </summary>
        public NumericUpDown() : base()
        {
            DefaultStyleKey = typeof(NumericUpDown);
            Interaction = new InteractionHelper(this);
        }

        #region Protected Methods
        /// <summary>
        /// Called when the DecimalPlaces property value has changed.
        /// </summary>
        /// <param name="oldValue">Old value of the DecimalPlaces property.</param>
        /// <param name="newValue">New value of the DecimalPlaces property.</param>
        protected virtual void OnDecimalPlacesChanged(int oldValue, int newValue)
        {
            formatString = string.Format(NumberFormatInfo.InvariantInfo, "F{0:D}", newValue);

            _levelsFromRootCall++;
            // force display refresh even when there is no value change.
            SetTextBoxText();
            _levelsFromRootCall--;
        }

        /// <summary>
        /// Called when the Increment property value has changed.
        /// </summary>
        /// <param name="oldValue">Old value of the Increment property.</param>
        /// <param name="newValue">New value of the Increment property.</param>
        protected virtual void OnIncrementChanged(double oldValue, double newValue)
        {
        }

        /// <summary>
        /// Called when the Minimum property value has changed.
        /// </summary>
        /// <param name="oldValue">Old value of the Minimum property.</param>
        /// <param name="newValue">New value of the Minimum property.</param>
        protected virtual void OnMinimumChanged(double oldValue, double newValue)
        {
        }

        /// <summary>
        /// Called when the Maximum property value has changed.
        /// </summary>
        /// <param name="oldValue">Old value of the Maximum property.</param>
        /// <param name="newValue">New value of the Maximum property.</param>
        protected virtual void OnMaximumChanged(double oldValue, double newValue)
        {
        }

        /// <summary>
        /// Override UpDownBase&lt;T&gt;.OnValueChanging to do validation and coercion.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnValueChanging(RoutedPropertyChangingEventArgs<double> e)
        {
            // Note: this section is a workaround, containing my
            // logic to hold all calls to the property changed
            // methods until after all coercion has completed
            // ----------
            if (_levelsFromRootCall == 0)
            {
                // validation
                EnsureValidDoubleValue(this, e.Property, e.OldValue, e.NewValue);

                _initialVal = e.OldValue;
                _requestedVal = e.NewValue;
                e.InCoercion = true;
            }
            _levelsFromRootCall++;
            // ----------

            CoerceValue();

            // Note: this section completes my workaround to call 
            // the property changed logic if all coercion has completed
            // ----------
            _levelsFromRootCall--;
            if (_levelsFromRootCall == 0)
            {
                e.InCoercion = false;
                double value = Value;
                if (_initialVal != value)
                {
                    ////RangeBaseAutomationPeer peer = GetAutomationPeer() as RangeBaseAutomationPeer;
                    ////if (peer != null)
                    ////{
                    ////    peer.RaiseValuePropertyChangedEvent(_initialVal, value);
                    ////}

                    e.NewValue = Value;
                    base.OnValueChanging(e);
                }
            }
            // ----------
        }

        /// <summary>
        /// Called by ApplyValue to parse user input as a decimal number.
        /// </summary>
        /// <param name="text">User input.</param>
        /// <returns>Value parsed from user input.</returns>
        protected override double ParseValue(string text)
        {
            return double.Parse(text, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Provides decimal specific value formatting for the value property.
        /// </summary>
        /// <returns>Formatted Value.</returns>
        protected override string FormatValue()
        {
            return Value.ToString(formatString, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Called by OnSpin when the spin direction is SpinDirection.Increase.
        /// </summary>
        protected override void OnIncrement()
        {
            Value = (double)((decimal)Value + (decimal)Increment);
            _requestedVal = Value;
        }

        /// <summary>
        /// Called by OnSpin when the spin direction is SpinDirection.Increase.
        /// </summary>
        protected override void OnDecrement()
        {
            Value = (double)((decimal)Value - (decimal)Increment);
            _requestedVal = Value;
        }
        #endregion

        #region Property Coersion and validation
        /// <summary>
        /// Levels from root call.
        /// </summary>
        private int _levelsFromRootCall;

        /// <summary>
        /// Initial Increment value.
        /// </summary>
        private double _initialInc = 1;

        /// <summary>
        /// Initial Minimum value.
        /// </summary>
        private double _initialMin;

        /// <summary>
        /// Initial Maximum value.
        /// </summary>
        private double _initialMax = 100;

        /// <summary>
        /// Initial Minimum value.
        /// </summary>
        private double _initialVal;

        /// <summary>
        /// Requested Increment value.
        /// </summary>
        private double _requestedInc = 1;

        /// <summary>
        /// Requested Minimum value.
        /// </summary>
        private double _requestedMin;

        /// <summary>
        /// Requested Maximum value.
        /// </summary>
        private double _requestedMax = 100;

        /// <summary>
        /// Requested Value value.
        /// </summary>
        private double _requestedVal;

        /// <summary>
        /// Ensure the Maximum is greater than or equal to the Minimum.
        /// </summary>
        private void CoerceMaximum()
        {
            double minimum = Minimum;
            double maximum = Maximum;

            // first check whether _requestedMax is good
            // second, check against Minimum to enforce coercion
            // last, handle DecimalPlaces change.
            if (_requestedMax != maximum)
            {
                // _requestedMax != maximum, because:
                // * Minimum changed, adjust Maximum
                //   - either _requestedMax good again
                //   - or new Minimum decides new Maximum
                // * Maximum changed and coerced (maximum == minimum), do nothing
                if (_requestedMax >= minimum)
                {
                    SetValue(MaximumProperty, _requestedMax);
                }
                else if (maximum != minimum)
                {
                    SetValue(MaximumProperty, minimum);
                }
            }
            else if (maximum < minimum)
            {
                // _requestedMax == maximum, enforce coercion
                SetValue(MaximumProperty, minimum);
            }
        }

        /// <summary>
        /// Ensure the value falls between the Minimum and Maximum values.
        /// This function assumes that (Maximum >= Minimum).
        /// </summary>
        private void CoerceValue()
        {
            double minimum = Minimum;
            double maximum = Maximum;
            Debug.Assert(maximum >= minimum, "Maximum value should have been coerced already!");
            double value = Value;

            if (_requestedVal != value)
            {
                if (_requestedVal >= minimum && _requestedVal <= maximum)
                {
                    SetValue(ValueProperty, _requestedVal);
                }
                else if (_requestedVal < minimum && value != minimum)
                {
                    SetValue(ValueProperty, minimum);
                }
                else if (_requestedVal > maximum && value != maximum)
                {
                    SetValue(ValueProperty, maximum);
                }
            }
            else if (value < minimum)
            {
                SetValue(ValueProperty, minimum);
            }
            else if (value > maximum)
            {
                SetValue(ValueProperty, maximum);
            }
        }

        /// <summary>
        /// Check if an object value is a valid double value.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="number">The double value to be returned.</param>
        /// <returns>true if a valid double; false otherwise.</returns>
        private static bool IsValidDoubleValue(object value, out double number)
        {
            number = (double)value;
            return !double.IsNaN(number) && !double.IsInfinity(number)
                && number <= (double)decimal.MaxValue && number >= (double)decimal.MinValue;
        }

        /// <summary>
        /// Ensure the new value of a dependency property change is a valid double value, 
        /// or revert the change and throw an exception.
        /// </summary>
        /// <remarks>
        /// EnsureValidDoubleValue(DependencyObject d, DependencyPropertyChangedEventArgs e) is simply a wrapper for 
        /// EnsureValidDoubleValue(DependencyObject d, DependencyProperty property, object oldValue, object newValue).
        /// </remarks>
        /// <param name="d">The DependencyObject whose DependencyProperty is changed.</param>
        /// <param name="e">The DependencyPropertyChangedEventArgs.</param>
        private static void EnsureValidDoubleValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnsureValidDoubleValue(d, e.Property, e.OldValue, e.NewValue);
        }

        /// <summary>
        /// Ensure the new value of a dependency property change is a valid double value, 
        /// or revert the change and throw an exception.
        /// </summary>
        /// <param name="d">The DependencyObject whose DependencyProperty is changed.</param>
        /// <param name="property">The DependencyProperty that changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void EnsureValidDoubleValue(DependencyObject d, DependencyProperty property, object oldValue, object newValue)
        {
            NumericUpDown nud = d as NumericUpDown;
            double number;
            if (!IsValidDoubleValue(newValue, out number))
            {
                // revert back to old value
                nud._levelsFromRootCall++;
                nud.SetValue(property, oldValue);
                nud._levelsFromRootCall--;

                // throw ArgumentException
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.NumericUpDown_EnsureValidDoubleValue_InvalidDoubleValue,
                    newValue);
                throw new ArgumentException(message, "newValue");
            }
        }

        /// <summary>
        /// Ensure the new value of Increment dependency property change is valid, 
        /// or revert the change and throw an exception.
        /// </summary>
        /// <param name="d">The DependencyObject whose DependencyProperty is changed.</param>
        /// <param name="e">The DependencyPropertyChangedEventArgs.</param>
        private static void EnsureValidIncrementValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)d;
            double number;
            if (!IsValidDoubleValue(e.NewValue, out number) || number <= 0)
            {
                // revert back to old value.
                nud._levelsFromRootCall++;
                nud.SetValue(e.Property, e.OldValue);
                nud._levelsFromRootCall--;

                // throw ArgumentException
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.NumericUpDown_EnsureValidIncrementValue_InvalidValue,
                    e.NewValue);
                throw new ArgumentException(message, "e");
            }
        }

        /// <summary>
        /// Ensure the new value of DecimalPlaces dependency property change is valid, 
        /// or revert the change and throw an exception.
        /// </summary>
        /// <param name="d">The DependencyObject whose DecimalPlaces DependencyProperty is changed.</param>
        /// <param name="e">The DependencyPropertyChangedEventArgs.</param>
        private static void EnsureValidDecimalPlacesValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown nud = d as NumericUpDown;
            int decimalPlaces = (int)e.NewValue;
            if (decimalPlaces < 0 || decimalPlaces > 15)
            {
                // revert the change
                nud._levelsFromRootCall++;
                nud.DecimalPlaces = (int)e.OldValue;
                nud._levelsFromRootCall--;

                // throw exception
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.NumericUpDown_OnDecimalPlacesPropertyChanged_InvalidValue,
                    e.NewValue);
                throw new ArgumentException(message, "e");
            }
        }
        #endregion 
    }
}
