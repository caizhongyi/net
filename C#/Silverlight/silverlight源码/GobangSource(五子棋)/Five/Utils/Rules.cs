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

namespace Five.Utils
{
    public class Rules
    {
        public static bool Exit(int x, int y, int[,] board)
        {
            if (board[x, y] != -1)
            {
                return true;
            }
            return false;

        }

        //左右方向
        public static int X1(int x, int y, int[,] board)
        {
            int flag = 0;
            int count = 1;
            int i = x + 1;
            while (i < 15)
            {
                if (board[x, y] == board[i, y])
                {
                    count++;
                    i++;
                }
                else
                {
                    break;
                }
            }

            if (i == 15)
            {
                flag++;
            }
            else
            {
                if (board[i, y] != -1)
                {
                    flag++;
                }
            }

            i = x - 1;
            while (i > 0)
            {
                if (board[x, y] == board[i, y])
                {
                    count++;
                    i--;
                }
                else
                {
                    break;
                }
            }

            if (i == -1)
            {
                flag++;
            }
            else
            {
                if (board[i, y] != -1)
                {
                    flag++;
                }
            }

            if (flag == 2)
                return -count;

            if (flag == 1 && count == 3)
                return -count;

            return count;
        }

        //上下方向
        public static int X2(int x, int y, int[,] board)
        {
            int flag = 0;
            int count = 1;
            int i = y + 1;
            while (i < 15)
            {
                if (board[x, i] == board[x, y])
                {
                    count++;
                    i++;
                }
                else
                {
                    break;
                }
            }

            if (i == 15)
            {
                flag++;
            }
            else
            {
                if (board[x, i] != -1)
                {
                    flag++;
                }
            }

            i = y - 1;
            while (i > 0)
            {
                if (board[x, i] == board[x, y])
                {
                    count++;
                    i--;
                }
                else
                {
                    break;
                }
            }

            if (i == -1)
            {
                flag++;
            }
            else
            {
                if (board[i, y] != -1)
                {
                    flag++;
                }
            }

            if (flag == 2)
            {
                return -count;
            }
          
                if (flag == 1 && count == 3)
                {
                    return -count;
                }
              
                    return count;
              
           
        }

        //左上的斜线方向
        public static int X3(int x, int y, int[,] board)
        {
            int flag = 0;
            int count = 1;
            int i = x - 1;
            int j = y - 1;
            while (i > 0 && j > 0)
            {
                if (board[x, y] == board[i, j])
                {
                    count++;
                    i--;
                    j--;
                }
                else
                {
                    break;
                }
            }

            if (i == -1 || j == -1)
            {
                flag++;
            }
            else
            {
                if (board[i, j] != -1)
                {
                    flag++;
                }
            }

            i = x + 1;
            j = y + 1;
            while (i < 15 && j < 15)
            {
                if (board[x, y] == board[i, j])
                {
                    count++;
                    i++;
                    j++;
                }
                else
                {
                    break;
                }
            }

            if (i == 15 || j == 15)
            {
                flag++;
            }
            else
            {
                if (board[i, y] != -1)
                {
                    flag++;
                }
            }

            if (flag == 2)
            {
                return -count;
            }

            if (flag == 1 && count == 3)
                return -count;


            return count;


        }

        //右上的斜线方向
        public static int X4(int x, int y, int[,] board)
        {
            int flag = 0;
            int count = 1;
            int i = x - 1;
            int j = y + 1;

            while (i > 0 && j < 15)
            {
                if (board[i, j] == board[x, y])
                {
                    count++;
                    i--;
                    j++;
                }
                else
                    break;
              
            }

            if (i == -1 || j == 15)
                flag++;
          
            else
            {
                if (board[i, j] != -1)
                    flag++;
              
            }

            i = x + 1;
            j = y - 1;

            while (i < 15 && j >= 0)
            {
                if (board[i, j] == board[x, y])
                {
                    count++;
                    i++;
                    j--;
                }
                else
                    break;
            }

            if (i == 15 || j == -1)
            {
                flag++;
            }
            else
            {
                if (board[i, y] != -1)
                    flag++;
            }

            if (flag == 2)
            {
                return -count;
            }
            if (flag == 1 && count == 3)
                return -count;

            return count;

        }

        public static bool Fails(int[] move, int stonetype)
        {
            if (stonetype == 0)
            {
                int count = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (move[i] == 3)
                    {
                        count++;
                    }
                }

                if (count > 1)
                {
                    return true;
                }
                return false;


            }
            return false;

        }

        public static bool Over(int[,] board)
        {
            bool over = true;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == -1)
                    {
                        over = false;
                    }
                }
            }
            return over;
        }

        public static int Winer(int x, int y, int[,] board)
        {
            var move = new int[4];
            move[0] = X1(x, y, board);
            move[1] = X2(x, y, board);
            move[2] = X3(x, y, board);
            move[3] = X4(x, y, board);
            bool win = false;

            if (Fails(move, 1))
                return 0;

            for (int i = 0; i < 4; i++)
            {
                if (Abs(move[i]) == 5)
                    win = true;
            }

            if (win)
                return 1;

            if (Over(board))
                return 2;

            return 3;
        }

        /// <summary>
        /// Abses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public static int Abs(int x)
        {
            if (x < 0)
                return -x;

            return x;
        }
    }
}
