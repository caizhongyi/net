using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.MyClass
{
    public class FontStyle
    {
        public static List<string> GetFontFamilies()
        {
            List<string> list = new List<string>();
            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in fonts.Families)
            {
                list.Add(family.Name);
            }
            return list;
        }
        public static List<int> GetFontSize()
        {
            List<string> allFontSizeName =new List<string>() { "8", "9", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72", "初号", "小初", "一号", "小一", "二号", "小二", "三号", "小三", "四号", "小四", "五号", "小五", "六号", "小六", "七号", "八号" };
            List<int> MySize = new List<int>() { 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            return MySize;
        }
    }
}
