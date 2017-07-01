using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


public static class GraphicesExtender
{
    public static void DrawTable(this Graphics g, Rectangle rec, int cellHeight, int cellWidth)
    {
        g.FillRectangle(Brushes.White, rec);
        //SizeF sizeF = g.MeasureString("TEST", this.Font);
        StringFormat sf = new StringFormat();
        sf.LineAlignment = StringAlignment.Center;
        //int cellHeight = (int)sizeF.Height + 4;
        // int cellWidth = 80;
        int nbrColumns = 50;
        int nbrRows = 50;

        for (int row = 0; row < nbrRows; ++row)
        {
            for (int col = 0; col < nbrColumns; ++col)
            {
                Point cellLocation = new Point(col * cellWidth, row * cellHeight);
                Rectangle cellRect = new Rectangle(cellLocation.X, cellLocation.Y, cellWidth, cellHeight);
                if (cellRect.IntersectsWith(rec))
                {
                    Console.WriteLine("Row:{0}col:{1}", col, row);
                    g.FillRectangle(Brushes.LightGray, cellRect);
                    g.DrawRectangle(Pens.Black, cellRect);
                    string s = String.Format("{0},{1}", col, row);
                    g.DrawString(s, new Font("宋体", 12), Brushes.Black, cellRect, sf);
                }
                else
                {

                }
            }
        }


    }
}
