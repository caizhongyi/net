using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.MyClass.Extender
{
    public class DoubleExtender
    {
        /// <summary>
        /// 保留两位小数，如"100"则为"100.00",如"100.0001",仍为"100.0001"
        /// </summary>
        /// <param name="decimalValue">double型变量</param>
        /// <param name="pos">保留的位数</param>
        /// <returns></returns>
        public static string ToString(this double decimalValue,int pos)
        {
            string[] values = decimalValue.ToString().Split('.');
            if (values.Length > 1)
            {
                if (values[1].Length <= 2)
                {
                    return string.Format("{0:0."+GetPos(pos)+"}", decimalValue);
                }
                else
                {
                    return decimalValue.ToString();
                }
            }
            else
            {
                return   string.Format("{0:0."+GetPos(pos)+"}", decimalValue);
            }
        }

        private static string GetPos(int pos)
        {
            string value=string .Empty ;
            for(int i=0;i<pos;i++)
            {
                value +="0";
            }
            return value;
            
        }
    }
}
