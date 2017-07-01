using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Data;


public static class Extender
{
    public static void Load(this Image image, string url)
    {
        image.Source = new BitmapImage(new Uri(url, UriKind.Relative));
    }

    public static void SetDataPager(this DataPager dataPager,long totalpagers)
    {
        List<int> itemCount = new List<int>();
        for (int i = 1; i <= totalpagers; i++) itemCount.Add(i);

        PagedCollectionView pcv = new PagedCollectionView(itemCount);

        pcv.PageSize = 1;

        dataPager.Source = pcv;
    }


}
