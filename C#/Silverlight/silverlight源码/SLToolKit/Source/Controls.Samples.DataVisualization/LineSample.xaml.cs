﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Windows.Controls;
using Microsoft.Windows.Controls.DataVisualization.Charting;

namespace Microsoft.Windows.Controls.Samples
{
    /// <summary>
    /// Sample page demonstrating ScatterSeries.
    /// </summary>
    [Sample("(4)Line")]
    public partial class LineSample : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the ScatterSample class.
        /// </summary>
        public LineSample()
        {
            InitializeComponent();

            SampleGenerators.GenerateNumericSeriesSamples(GeneratedChartsPanel, () => new LineSeries(), true);
            SampleGenerators.GenerateDateTimeValueSeriesSamples(GeneratedChartsPanel, () => new LineSeries());
            SampleGenerators.GenerateMultipleValueSeriesSamples(GeneratedChartsPanel, () => new LineSeries(), true);
        }
    }
}