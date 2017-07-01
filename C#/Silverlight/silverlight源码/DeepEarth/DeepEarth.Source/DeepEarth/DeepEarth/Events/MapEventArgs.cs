// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;

namespace DeepEarth.Events
{
    ///<summary>
    /// <para>
    /// Extends EventsArgs class and adds a Handled property.
    /// </para>
    ///</summary>
    public class MapEventArgs : EventArgs
    {
        ///<summary>
        /// Indicates if an event was handled within another event handler.
        ///</summary>
        public bool Handled { get; set; }
    }

    ///<summary>
    /// Extends EventsArgs class with specific properties for handling mouse wheel events.
    ///</summary>
    public class MouseWheelEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the MouseWheelEventArgs class with the specified delta.
        /// </summary>
        /// <param name="delta">The delta calculated for the mouse wheel event.</param>
        public MouseWheelEventArgs(double delta)
        {
            Delta = delta;
        }

        /// <summary>
        /// Gets a value that specifies the delta and direction of the mouse wheel event. This value is normalized across browsers.
        /// </summary>
        public double Delta { get; private set; }

        /// <summary>
        /// Gets or sets a value that indicates whether or not the mouse wheel event has been handled.
        /// </summary>
        public bool Handled { get; set; }
    }
}