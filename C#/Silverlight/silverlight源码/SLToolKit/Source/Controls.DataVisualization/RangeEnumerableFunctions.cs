﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Windows.Controls.DataVisualization
{
    /// <summary>
    /// Collection of functions that manipulate streams of ranges.
    /// </summary>
    internal static class RangeEnumerableExtensions
    {
        /// <summary>
        /// Returns the minimum and maximum values in a stream.
        /// </summary>
        /// <typeparam name="T">The type of the stream.</typeparam>
        /// <param name="that">The stream.</param>
        /// <returns>The range of values in the stream.</returns>
        public static Range<T> GetRange<T>(this IEnumerable<T> that)
            where T : struct, IComparable<T>, IEquatable<T>
        {
            if (that.IsEmpty())
            {
                return Range<T>.Empty;
            }
            T minimum = that.First();
            T maximum = minimum;
            foreach (T value in that)
            {
                if (minimum.CompareTo(value) == 1)
                {
                    minimum = value;
                }
                if (maximum.CompareTo(value) == -1)
                {
                    maximum = value;
                }
            }
            return new Range<T>(minimum, maximum);
        }

        /// <summary>
        /// Returns a range encompassing all ranges in a stream.
        /// </summary>
        /// <typeparam name="T">The type of the minimum and maximum values.
        /// </typeparam>
        /// <param name="that">The stream.</param>
        /// <returns>A range encompassing all ranges in a stream.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nesting necessary to provide method for use with all types of Range<T>.")]
        public static Range<T> Sum<T>(this IEnumerable<Range<T>> that)
            where T : struct, IComparable<T>, IEquatable<T>
        {
            if (that.IsEmpty())
            {
                return Range<T>.Empty;
            }
            else
            {
                return that.Aggregate((x, y) => x.Add(y));
            }
        }
    }
}