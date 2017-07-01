using System;
using System.Collections.Generic;
using System.Text;
using ReadImageName;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            IReadImageName r = ReadImageName.ReadImageNameFactory.ReadImageName();
            for (int i = 0; i < r.ImageName().Length; i++) 
            {
                Console.WriteLine(r.ImageName()[i]);
            }
        }
    }
}
