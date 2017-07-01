using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class RamdomExtender
{
    /// <summary>
    /// 生成1到9A到Z的随机字符
    /// </summary>
    /// <param name="r">Random</param>
    /// <param name="count">生成的个数</param>
    /// <returns></returns>
    public static string RandomString(this Random r, int count)
    {
        string temp = string.Empty;
        temp = GetRmNum(count, string.Empty);
        return temp;
    }
    /// <summary>
    /// 生成1到9A到Z的随机字符
    /// </summary>
    /// <param name="r">Random</param>
    /// <param name="count">生成的个数</param>
    /// <param name="baseString">用于获取的基字符;例如:"1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,s,y,z";</param>
    /// <returns></returns>
    public static string RandomString(this Random r, int count, string baseString)
    {
        string temp = string.Empty;
        temp = GetRmNum(count, baseString);
        return temp;
    }

    /// <summary>
    /// 获取随机数。
    /// </summary>
    /// <param name="iNumBit">获取几位</param>
    /// <param name="baseString">用于获取的基字符;例如:"1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,s,y,z";</param>
    /// <returns>获取的随机数</returns>
    private static string GetRmNum(int iNumBit, string baseString)
    {
        string strValidate = baseString == string.Empty ? "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,s,y,z,1,2,3,4,5,6,7,8,9,0" : baseString;
        string[] strVArray = strValidate.Split(new Char[] { ',' });
        string strVNum = "";
        int iTemp = -1;

        Random objRandom = new Random();

        for (int i = 1; i < iNumBit + 1; i++)
        {
            if (iTemp != -1)
            {
                objRandom = new Random(i * iTemp * unchecked((int)DateTime.Now.Ticks));
            }

            int iT = objRandom.Next(strVArray.Length);

            if (iTemp != -1 && iTemp == iT)
            {
                return GetRmNum(iNumBit, baseString);
            }

            iTemp = iT;
            strVNum += strVArray[iT];
        }
        return strVNum;
    }
}

