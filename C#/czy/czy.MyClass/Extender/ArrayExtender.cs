using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class ArrayExtender
{
    public static long BytesSum(this byte[] array)
    {
        long sum = 0;
        foreach (byte o in array)
        {
            sum += Convert.ToInt64(o);
        }
        return sum;
    }
}

