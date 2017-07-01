﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Windows.Controls.Samples
{
    /// <summary>
    /// A sample AutoCompleteBox with a DataGrid selection adapter.
    /// </summary>
    [Sample("AutoCompleteBox/Scenarios/DataGrid")]
    public partial class DataGridAutoCompleteBox : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the type.
        /// </summary>
        public DataGridAutoCompleteBox()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        /// <summary>
        /// Handle the loaded event.
        /// </summary>
        /// <param name="sender">The source object.</param>
        /// <param name="e">The event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Text search with custom filters, like the Airport, that match 
            // more than the primary string value of the control, leads to 
            // undesirable side effects.
            MyAutoCompleteBox.IsTextCompletionEnabled = false;

            // Bind to the sample airport data
            MyAutoCompleteBox.ItemsSource = Airport.SampleAirports;
            
            // A custom search, the same that is used in the basic lambda file
            MyAutoCompleteBox.SearchMode = AutoCompleteSearchMode.Custom;
            MyAutoCompleteBox.ItemFilter = (search, item) =>
            {
                Airport airport = item as Airport;
                if (airport != null)
                {
                    // Interested in: Name, City, FAA code
                    string filter = search.ToUpper(CultureInfo.InvariantCulture);
                    return (airport.CodeFaa.ToUpper(CultureInfo.InvariantCulture).Contains(filter)
                        || airport.City.ToUpper(CultureInfo.InvariantCulture).Contains(filter)
                        || airport.Name.ToUpper(CultureInfo.InvariantCulture).Contains(filter));
                }

                return false;
            };
        }
    }
}