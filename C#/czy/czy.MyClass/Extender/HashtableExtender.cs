using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


public static class HashtableExtender
{
    /// <summary>
    /// 转化为json字符窜
    /// </summary>
    /// <param name="ht">Hashtable</param>
    /// <returns>json字符窜</returns>
    public static string ToJson(this Hashtable ht)
    {
        StringBuilder sb = new StringBuilder();
        foreach (DictionaryEntry d in ht)
        {
            sb.Append("{" + d.Key.ToString() + ":" + d.Value.ToString() + "}");
        }
        return sb.ToString();
    }
}

