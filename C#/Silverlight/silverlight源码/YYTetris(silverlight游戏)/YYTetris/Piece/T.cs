﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace YYTetris.Piece
{
    public class T : PieceBase
    {
        public override void InitPiece()
        {
            Matrix = new int[,]
            {
                {0,0,1,0},
                {0,1,1,1},
                {0,0,0,0},
                {0,0,0,0}
            };

            MaxIndex = 3;
        }

        public override int[,] GetRotate()
        {
            switch (GetNextIndex())
            {
                case 0:
                    return new int[,]
                    {
                        {0,0,1,0},
                        {0,1,1,1},
                        {0,0,0,0},
                        {0,0,0,0}
                    };
                case 1:
                    return new int[,]
                    {
                        {0,0,1,0},
                        {0,0,1,1},
                        {0,0,1,0},
                        {0,0,0,0}
                    };
                case 2:
                    return new int[,]
                    {
                        {0,0,0,0},
                        {0,1,1,1},
                        {0,0,1,0},
                        {0,0,0,0}
                    };
                case 3:
                    return new int[,]
                    {
                        {0,0,1,0},
                        {0,1,1,0},
                        {0,0,1,0},
                        {0,0,0,0}
                    };
                default:
                    return Matrix;
            }
        }

        public override Color Color
        {
            get { return Helper.GetColor("#dddd22"); }
        }
    }
}
