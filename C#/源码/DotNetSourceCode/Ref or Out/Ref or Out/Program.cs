using System;
using System.Collections.Generic;
using System.Text;

namespace Ref_or_Out
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b;
            squareRef(ref a);
            squareOut(out b);
            Console.WriteLine("The a in the Main is: " + a);
            Console.WriteLine("The b in the Main is: " + b);
            Console.ReadLine();
        }
        static void squareRef(ref int x)
        {
            x = 3;
            x = x * x;
            Console.WriteLine("The x in the squareRef is: " + x);
        }
        static void squareOut(out int y)
        {
            y = 10;
            y = y * y;
            Console.WriteLine("The y in the squareOut is: " + y);
        }
    }
}
