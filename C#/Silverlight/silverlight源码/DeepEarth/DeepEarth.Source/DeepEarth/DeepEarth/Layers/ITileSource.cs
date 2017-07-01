// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;

namespace DeepEarth.Layers
{
    /// <summary>
    /// <para>
    /// An Interface for a Raster tile source.
    /// The layer must be able to supply Image URI's for the given MultiScaleImage tile coordinates.
    /// We also mandate that a UIElement with copyright information be supplied
    /// </para>
    /// </summary>
    public interface ITileSource
    {
        /// <summary>
        /// Gets the URI (URL) for the image at the supplied X,Y tile position and tileLevel
        /// </summary>
        /// <param name="tileLevel">The TileLevel of the MSI control, 1 is 1 pixel, 2 is 2 pixel, 3 is 4 pixels, 4 is 16px, N is 2^N pixels</param>
        /// <param name="tilePositionX">The number of tiles from the left, 0 based</param>
        /// <param name="tilePositionY">The number of tiles from the top, 0 based</param>
        /// <returns>The URI of the image tile</returns>
        Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY);

        /// <summary>
        /// Gets the Copyright control for the tile source
        /// </summary>
        /// <returns>The control containing the copyright information for display</returns>
        UIElement GetCopyright();
    }
}