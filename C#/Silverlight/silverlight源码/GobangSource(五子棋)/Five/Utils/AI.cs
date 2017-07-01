using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Five.Model.VO;

namespace Five.Utils
{
    public class AI
    {

        private static AI _instance;
        public static AI GetInstance()
        {
            if (_instance == null)
                _instance = new AI(true);
            return _instance;
        }

        private bool mifis;

        public int X { get; set; }

        public int Y { get; set; }

        public AI(bool ifis)
        {
            mifis = ifis;
        }

        public void Down(int[,] board)
        {
            int[,] q = new int[AppConfig.PIECEBOARD_WIDTH, AppConfig.PIECEBOARD_WIDTH];
            


            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] != -1)
                    {
                        q[i, j] = -1;
                    }
                    else
                    {
                        q[i, j] = FindQz(i, j, board);
                    }
                }
            }
            ForMax(q);
        }

        public void ForMax(int[,] q)
        {
            int max = 0;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (q[i, j] > max)
                    {
                        X = i;
                        Y = j;
                        max = q[i, j];
                    }
                }
            }
        }

        public int FindQz(int x, int y, int[,] board)
        {
            int qz = 0;
            const int w1 = 10000000;
            const int w2 = 50000;
            const int w3 = 10000;
            const int w4 = 5000;
            const int w5 = 1000;
            const int w6 = 500;
            const int w7 = 100;
            const int w8 = 50;
            const int w9 = -100000000;
            var move = new int[4];

            board[x, y] = mifis ? 0 : 1;

            move[0] = Rules.X1(x, y, board);
            move[1] = Rules.X2(x, y, board);
            move[2] = Rules.X3(x, y, board);
            move[3] = Rules.X4(x, y, board);
            if (x == 7 && y == 7)
            {
                qz += 1;
            }

            for (int i = 0; i < 4; i++)
            {
                if (Abs(move[i]) == 5)
                {
                    qz += w1;
                }
                else if (move[i] == 4)
                {
                    qz += w3;
                }
                else if (move[i] == 3)
                {
                    qz += w5;
                }
                else if (move[i] == 2)
                {
                    qz += w7;
                }

                if (mifis)
                {
                    if (Rules.Fails(move, board[x, y]))
                    {
                        qz += w9;
                    }
                }
            }

            board[x, y] = mifis ? 1 : 0;

            move[0] = Rules.X1(x, y, board);
            move[1] = Rules.X2(x, y, board);
            move[2] = Rules.X3(x, y, board);
            move[3] = Rules.X4(x, y, board);

            for (int i = 0; i < 4; i++)
            {
                if (Abs(move[i]) == 5)
                {
                    qz += w2;
                }
                else if (move[i] == 4)
                {
                    qz += w4;
                }
                else if (move[i] == 3)
                {
                    qz += w6;
                }
                else if (move[i] == 2)
                {
                    qz += w8;
                }
            }
            board[x, y] = -1;
            return qz;
        }

        public int Abs(int x)
        {
            if (x < 0)
            {
                return -x;
            }
            return x;
        }
    }
}
