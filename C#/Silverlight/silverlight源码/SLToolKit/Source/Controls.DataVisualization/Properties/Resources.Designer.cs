﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Windows.Controls.DataVisualization.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Windows.Controls.DataVisualization.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Axis type is not Category..
        /// </summary>
        internal static string Axis_AxisTypeIsNotCategory {
            get {
                return ResourceManager.GetString("Axis_AxisTypeIsNotCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Axis type is not DateTime..
        /// </summary>
        internal static string Axis_AxisTypeIsNotDateTime {
            get {
                return ResourceManager.GetString("Axis_AxisTypeIsNotDateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Axis type is not Linear..
        /// </summary>
        internal static string Axis_AxisTypeIsNotLinear {
            get {
                return ResourceManager.GetString("Axis_AxisTypeIsNotLinear", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For an Axis of type DateTime, value must be able to be parsed as a DateTime or a double..
        /// </summary>
        internal static string Axis_DateTimeAxis_InvalidFormatArgument {
            get {
                return ResourceManager.GetString("Axis_DateTimeAxis_InvalidFormatArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For an Axis of type Linear, value must be able to be parsed as a double..
        /// </summary>
        internal static string Axis_LinearAxis_NonDoubleArgument {
            get {
                return ResourceManager.GetString("Axis_LinearAxis_NonDoubleArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Interval is of the wrong sign for the Minimum and Maximum values provided..
        /// </summary>
        internal static string Axis_LinearAxisAreaSizeChanged_WrongSign {
            get {
                return ResourceManager.GetString("Axis_LinearAxisAreaSizeChanged_WrongSign", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot change the type of an axis when it is in use by one or more series..
        /// </summary>
        internal static string Axis_OnAxisTypeChanged_CannotChangeTypeOfAnAxisWhenItIsInUseByASeries {
            get {
                return ResourceManager.GetString("Axis_OnAxisTypeChanged_CannotChangeTypeOfAnAxisWhenItIsInUseByASeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot have more than one horizontal axis..
        /// </summary>
        internal static string AxisCollection_CannotHaveMoreThanOneHorizontalAxis {
            get {
                return ResourceManager.GetString("AxisCollection_CannotHaveMoreThanOneHorizontalAxis", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot have more than one vertical axis..
        /// </summary>
        internal static string AxisCollection_CannotHaveMoreThanOneVerticalAxis {
            get {
                return ResourceManager.GetString("AxisCollection_CannotHaveMoreThanOneVerticalAxis", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Changing the Chart&apos;s Axes property is not supported; changes to the collection should be made with its .Add/.Remove methods instead..
        /// </summary>
        internal static string Chart_Axes_SetterNotSupported {
            get {
                return ResourceManager.GetString("Chart_Axes_SetterNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One axis cannot be used by multiple charts..
        /// </summary>
        internal static string Chart_RegisterWithSeries_OneAxisCannotBeUsedByMultipleCharts {
            get {
                return ResourceManager.GetString("Chart_RegisterWithSeries_OneAxisCannotBeUsedByMultipleCharts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Changing the Chart&apos;s Series property is not supported; changes to the collection should be made with its .Add/.Remove methods instead..
        /// </summary>
        internal static string Chart_Series_SetterNotSupported {
            get {
                return ResourceManager.GetString("Chart_Series_SetterNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Axis is not in the chart..
        /// </summary>
        internal static string Chart_UnregisterWithSeries_OneAxisCannotBeUsedByMultipleCharts {
            get {
                return ResourceManager.GetString("Chart_UnregisterWithSeries_OneAxisCannotBeUsedByMultipleCharts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is a read-only DependencyProperty..
        /// </summary>
        internal static string DataVisualization_ReadOnlyDependencyPropertyChange {
            get {
                return ResourceManager.GetString("DataVisualization_ReadOnlyDependencyPropertyChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plotting of non-numeric values on the dependent axis is not supported..
        /// </summary>
        internal static string DynamicSeriesWithAxes_DependentValueMustBeNumeric {
            get {
                return ResourceManager.GetString("DynamicSeriesWithAxes_DependentValueMustBeNumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid attempt to retrieve date time range for axis not in use by the series..
        /// </summary>
        internal static string DynamicSeriesWithAxes_GetDateTimeRangeGetNumericRange_InvalidAttemptToRetrieveDateTimeRangeForAxisNotInUseByTheSeries {
            get {
                return ResourceManager.GetString("DynamicSeriesWithAxes_GetDateTimeRangeGetNumericRange_InvalidAttemptToRetrieveDat" +
                        "eTimeRangeForAxisNotInUseByTheSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid attempt to retrieve numeric range for axis not in use by the series..
        /// </summary>
        internal static string DynamicSeriesWithAxes_GetNumericRange_InvalidAttemptToRetrieveNumericRangeForAxisNotInUseByTheSeries {
            get {
                return ResourceManager.GetString("DynamicSeriesWithAxes_GetNumericRange_InvalidAttemptToRetrieveNumericRangeForAxis" +
                        "NotInUseByTheSeries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum value must be larger than or equal to the minimum value..
        /// </summary>
        internal static string Range_ctor_MaximumValueMustBeLargerThanOrEqualToMinimumValue {
            get {
                return ResourceManager.GetString("Range_ctor_MaximumValueMustBeLargerThanOrEqualToMinimumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;Cannot read the Maximum of an empty range.&quot;.
        /// </summary>
        internal static string Range_get_Maximum_CannotReadTheMaximumOfAnEmptyRange {
            get {
                return ResourceManager.GetString("Range_get_Maximum_CannotReadTheMaximumOfAnEmptyRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;Cannot read the Minimum of an empty range.&quot;.
        /// </summary>
        internal static string Range_get_Minimum_CannotReadTheMinimumOfAnEmptyRange {
            get {
                return ResourceManager.GetString("Range_get_Minimum_CannotReadTheMinimumOfAnEmptyRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Collection is read-only..
        /// </summary>
        internal static string ReadOnlyObservableCollection_CollectionIsReadOnly {
            get {
                return ResourceManager.GetString("ReadOnlyObservableCollection_CollectionIsReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot register with an axis when not inside of a Chart&apos;s Series collection..
        /// </summary>
        internal static string Series_CannotRegisterWithAxisWhenNotInsideOfAChartsSeriesCollection {
            get {
                return ResourceManager.GetString("Series_CannotRegisterWithAxisWhenNotInsideOfAChartsSeriesCollection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DependentValue must be either be numeric or a DateTime..
        /// </summary>
        internal static string Series_DependentValueMustEitherBeANumericValueOrADateTime {
            get {
                return ResourceManager.GetString("Series_DependentValueMustEitherBeANumericValueOrADateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IndependentValue must be either be numeric or a DateTime..
        /// </summary>
        internal static string Series_IndependentValueMustEitherBeANumericValueOrADateTime {
            get {
                return ResourceManager.GetString("Series_IndependentValueMustEitherBeANumericValueOrADateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Series {0}.
        /// </summary>
        internal static string Series_OnGlobalSeriesIndexPropertyChanged_UntitledSeriesFormatString {
            get {
                return ResourceManager.GetString("Series_OnGlobalSeriesIndexPropertyChanged_UntitledSeriesFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Series is already in use by a different Host..
        /// </summary>
        internal static string Series_SeriesHost_SeriesHostPropertyNotNull {
            get {
                return ResourceManager.GetString("Series_SeriesHost_SeriesHostPropertyNotNull", resourceCulture);
            }
        }
    }
}
