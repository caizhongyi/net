using System;
using System.Collections.Generic;
using System.Text;

namespace SetWallpaper
{
    public class Factory
    {
        public static IChangeWallpaperService GetNewPicture()
        {
            return new ChangeWallpaperService();
        }
    }
}
