// TileServerDemo shows how to build a Virtual Earth compatible tile server
// (C) Copyright 2006 - 2007 Active Web Solutions Ltd
//
// This program is free software; you can redistribute it and/or
// modify as long as you acknowledge the copyright holder.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 

/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

namespace DeepEarth.Provider.WebMapService
{
    /// <summary>
    /// A box describing the bounds of a virtual earth map tile
    /// </summary>
    public class BBox
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public BBox(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}



