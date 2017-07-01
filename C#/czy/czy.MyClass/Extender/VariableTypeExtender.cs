using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


public static class VariableTypeExtender
{
    /// <summary>
    /// 加入的byte数组
    /// </summary>
    /// <param name="bytes">当前数组</param>
    /// <param name="otherBytes">加入的byte数组</param>
    /// <returns>加入后的byte数组</returns>
    public static byte[] JoinBytes(this byte[] bytes, byte[] otherBytes)
    {
        byte[] newBytes = new byte[bytes.Length + otherBytes.Length];
        Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);
        Buffer.BlockCopy(otherBytes, 0, newBytes,bytes.Length,otherBytes.Length);
        return newBytes;
    }
    /// <summary>
    /// 截取byte数组一部分
    /// </summary>
    /// <param name="bytes">当前数组</param>
    /// <param name="start">起始位置</param>
    /// <param name="length">长度</param>
    /// <returns>截取后的byte数组</returns>
    public static byte[] SubBytes(this byte[] bytes,int start,int length)
    {
        byte[] newBytes = new byte[length];
        Buffer.BlockCopy(bytes, start, newBytes, 0, length);
        return newBytes;
    }

    /// <summary>
    /// byte数组值的总和
    /// </summary>
    /// <param name="bytes">当前数组</param>
    /// <returns>byte数组值的总和</returns>
    public static long BytesSum(this byte[] bytes)
    {
        long sum = 0;
        foreach (byte o in bytes)
        {
            sum += Convert.ToInt64(o);
        }
        return sum;
    }
}

