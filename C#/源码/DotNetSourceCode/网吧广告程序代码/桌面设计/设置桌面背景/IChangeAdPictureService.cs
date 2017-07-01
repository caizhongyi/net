using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SetWallpaper
{
    public interface IChangeAdPictureService
    {
        void GetPictureTime();

        Image ChanageAdSize(string StrFileName, int width, int heigh);
    }
}
