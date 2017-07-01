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
    [Sample("(5)Scatter")]
    public partial class ScatterSample : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the ScatterSample class.
        /// </summary>
        public ScatterSample()
        {
            InitializeComponent();

            SampleGenerators.GenerateNumericSeriesSamples(GeneratedChartsPanel, () => new ScatterSeries(), true);
            SampleGenerators.GenerateDateTimeValueSeriesSamples(GeneratedChartsPanel, () => new ScatterSeries());
            SampleGenerators.GenerateValueValueSeriesSamples(GeneratedChartsPanel, () => new ScatterSeries());
            SampleGenerators.GenerateMultipleValueSeriesSamples(GeneratedChartsPanel, () => new ScatterSeries(), true);
        }
    }
}