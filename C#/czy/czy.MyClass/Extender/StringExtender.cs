using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class StringExtender
{

    #region 从字符串里随机得到，规定个数的字符串.

    /// <summary>
    /// 从字符串里随机得到，规定个数的字符串.
    /// </summary>
    /// <param name="allChar"></param>
    /// <param name="CodeCount"></param>
    /// <returns></returns>
    public static string GetRandomCode(this string allChar, int CodeCount)
    {
        //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
        string[] allCharArray = allChar.Split(',');
        string RandomCode = "";
        int temp = -1;
        Random rand = new Random();
        for (int i = 0; i < CodeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
            }

            int t = rand.Next(allCharArray.Length - 1);

            while (temp == t)
            {
                t = rand.Next(allCharArray.Length - 1);
            }

            temp = t;
            RandomCode += allCharArray[t];
        }
        return RandomCode;
    }

    #endregion
    /// <summary>
    /// 超出的字以...显示
    /// </summary>
    /// <param name="str">字符窜</param>
    /// <param name="n">要显示的字数</param>
    /// <returns></returns>
    public static string ToViewLength(this string str, int n)
    {
        ///
        ///格式化字符串长度，超出部分显示省略号,区分汉字跟字母。汉字2个字节，字母数字一个字节
        ///
        string temp = string.Empty;
        if (System.Text.Encoding.Default.GetByteCount(str) <= n)//如果长度比需要的长度n小,返回原字符串
        {
            return str;
        }
        else
        {
            int t = 0;
            char[] q = str.ToCharArray();
            for (int i = 0; i < q.Length && t < n; i++)
            {
                if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)//是否汉字
                {
                    temp += q[i];
                    t += 2;
                }
                else
                {
                    temp += q[i];
                    t++;
                }
            }
            return (temp + "...");
        }
    }

    #region sql参数未验证的单引号替换
    /// <summary>
    /// sql参数未验证的单引号替换
    /// </summary>
    /// <param name="str">字符窜</param>
    /// <returns>返回替换后的字符窜</returns>
    public static string ToSQLSafe(this string str)
    {
        return str.Replace("'", "''");
    }
    /// <summary>
    /// sql参数未验证的单引号替换
    /// </summary>
    /// <param name="str">字符窜</param>
    /// <returns>返回替换后的字符窜</returns>
    public static string[] ToSQLSafe(this string[] str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            str[i] = str[i].Replace("'", "''");
        }
        return str;
    }

    #endregion

    /// <summary>
    /// 转全角的函数(SBC case)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToSBC(this string input)
    {
        //半角转全角：
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }
            if (c[i] < 127)
                c[i] = (char)(c[i] + 65248);
        }
        return new string(c);
    }

    /// <summary>
    ///  转半角的函数(SBC case)
    /// </summary>
    /// <param name="input">输入</param>
    /// <returns></returns>
    public static string ToDBC(this string input)
    {
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }
            if (c[i] > 65280 && c[i] < 65375)
                c[i] = (char)(c[i] - 65248);
        }
        return new string(c);
    }

    /// <summary>
    /// 删除最后结尾的指定字符后的字符
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strchar">删除该字符后的字符</param>
    /// <returns></returns>
    public static string DelLastChar(this string str, string strchar)
    {
        return str.Substring(0, str.LastIndexOf(strchar));
    }

    public static string ToSafe(this string str)
    {
        return str == null ? string.Empty : str;
    }

}




