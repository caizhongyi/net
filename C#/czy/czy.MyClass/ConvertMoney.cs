using System;
using System.Collections.Generic;
using System.Text;

namespace czy.MyClass
{
    /// <summary> 
    /// ConvertMoney ��ժҪ˵���� 
    /// </summary> 
    public class ConvertMoney
    {
        static string[,] replaces ={
             {"����Ǫ","��Ǫ"},
             {"Բ���","Բ��"}
             };
        /// <summary> 
        /// ת������Ҵ�С��� 
        /// </summary> 
        /// <param name="num">���</param> 
        /// <returns>���ش�д��ʽ</returns> 
        public static string ConvertToCN(decimal num)
        {
            string str1 = "��Ҽ��������½��ƾ�";            //0-9����Ӧ�ĺ��� 
            string str2 = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԲ�Ƿ�"; //����λ����Ӧ�ĺ��� 
            string str3 = "";    //��ԭnumֵ��ȡ����ֵ 
            string str4 = "";    //���ֵ��ַ�����ʽ 
            string str5 = "";  //����Ҵ�д�����ʽ 
            int i;    //ѭ������ 
            int j;    //num��ֵ����100���ַ������� 
            string ch1 = "";    //���ֵĺ������ 
            string ch2 = "";    //����λ�ĺ��ֶ��� 
            int nzero = 0;  //����������������ֵ�Ǽ��� 
            int temp;            //��ԭnumֵ��ȡ����ֵ 

            num = Math.Round(Math.Abs(num), 2);    //��numȡ����ֵ����������ȡ2λС�� 
            str4 = ((long)(num * 100)).ToString();        //��num��100��ת�����ַ�����ʽ 
            j = str4.Length;      //�ҳ����λ 
            if (j > 15) { return "���"; }
            str2 = str2.Substring(15 - j);   //ȡ����Ӧλ����str2��ֵ���磺200.55,jΪ5����str2=��ʰԪ�Ƿ� 

            //ѭ��ȡ��ÿһλ��Ҫת����ֵ 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //ȡ����ת����ĳһλ��ֵ 
                temp = Convert.ToInt32(str3);      //ת��Ϊ���� 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //����ȡλ����ΪԪ�����ڡ������ϵ�����ʱ 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "��" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //��λ�����ڣ��ڣ���Ԫλ�ȹؼ�λ 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "��" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //�����λ����λ��Ԫλ�������д�� 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //���һλ���֣�Ϊ0ʱ�����ϡ����� 
                    str5 = str5 + '��';
                }
            }
            if (num == 0)
            {
                str5 = "��Ԫ��";
            }
            return replace(str5);
        }

        /**/
        /// <summary> 
        /// һ�����أ����ַ�����ת���������ڵ���CmycurD(decimal num) 
        /// </summary> 
        /// <param name="num">�û�����Ľ��ַ�����ʽδת��decimal</param> 
        /// <returns></returns> 
        public static string ConvertToCN(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return ConvertToCN(num);
            }
            catch
            {
                return "��������ʽ��";
            }
        }


        /// <summary>  
        /// ���ת��д
        /// </summary>  
        /// <param name="LowerMoney"></param>  
        /// <returns></returns>  
        public static string MoneyToChinese(string LowerMoney)
        {
            string functionReturnValue = null;
            bool IsNegative = false; // �Ƿ��Ǹ���  
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // �Ǹ�������תΪ����  
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // ������λС�� 123.489��123.49����123.4��123.4  
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney = LowerMoney + ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "Բ";
                        break;
                    case "0":
                        strUpart = "��";
                        break;
                    case "1":
                        strUpart = "Ҽ";
                        break;
                    case "2":
                        strUpart = "��";
                        break;
                    case "3":
                        strUpart = "��";
                        break;
                    case "4":
                        strUpart = "��";
                        break;
                    case "5":
                        strUpart = "��";
                        break;
                    case "6":
                        strUpart = "½";
                        break;
                    case "7":
                        strUpart = "��";
                        break;
                    case "8":
                        strUpart = "��";
                        break;
                    case "9":
                        strUpart = "��";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "��";
                        break;
                    case 2:
                        strUpart = strUpart + "��";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "ʰ";
                        break;
                    case 6:
                        strUpart = strUpart + "��";
                        break;
                    case 7:
                        strUpart = strUpart + "Ǫ";
                        break;
                    case 8:
                        strUpart = strUpart + "��";
                        break;
                    case 9:
                        strUpart = strUpart + "ʰ";
                        break;
                    case 10:
                        strUpart = strUpart + "��";
                        break;
                    case 11:
                        strUpart = strUpart + "Ǫ";
                        break;
                    case 12:
                        strUpart = strUpart + "��";
                        break;
                    case 13:
                        strUpart = strUpart + "ʰ";
                        break;
                    case 14:
                        strUpart = strUpart + "��";
                        break;
                    case 15:
                        strUpart = strUpart + "Ǫ";
                        break;
                    case 16:
                        strUpart = strUpart + "��";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("��ʰ", "��");
            strUpper = strUpper.Replace("���", "��");
            strUpper = strUpper.Replace("��Ǫ", "��");
            strUpper = strUpper.Replace("������", "��");
            strUpper = strUpper.Replace("����", "��");
            strUpper = strUpper.Replace("������", "��");
            strUpper = strUpper.Replace("���", "��");
            strUpper = strUpper.Replace("���", "��");
            strUpper = strUpper.Replace("����������Բ", "��Բ");
            strUpper = strUpper.Replace("��������Բ", "��Բ");
            strUpper = strUpper.Replace("��������", "��");
            strUpper = strUpper.Replace("������Բ", "��Բ");
            strUpper = strUpper.Replace("����", "��");
            strUpper = strUpper.Replace("����", "��");
            strUpper = strUpper.Replace("��Բ", "Բ");
            strUpper = strUpper.Replace("����", "��");

            // ��ҼԲ���µĽ��Ĵ���  
            if (strUpper.Substring(0, 1) == "Բ")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "��")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "��")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "��")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "��")
            {
                strUpper = "��Բ��";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "��" + replace(functionReturnValue);
            }
            else
            {
                return replace(functionReturnValue);
            }
        }

   
        private static string replace(string chinese)
        {
            for (int i = 0; i < replaces.Length / 2; i++)
            {
                chinese = chinese.Replace(replaces[i, 0], replaces[i, 1]);
            }
            chinese = Del0(chinese, '��', 'Ǫ');
            chinese = Del0(chinese, 'Բ', '��');
            return chinese;
        }
        private static string Del0(string chinese, char prev, char last)
        {
            try
            {
                if (chinese.IndexOf(prev) != -1 && chinese.IndexOf(last) != -1)
                {
                    string[] sArray = chinese.Split(prev, last);
                    sArray[0] += prev;
                    sArray[1] += last;
                    sArray[1] = sArray[1].Replace("��", "");
                    chinese = string.Empty;
                    foreach (string s in sArray)
                    {
                        chinese += s;
                    }
                }
                return chinese;
            }
            catch { return chinese; }

        }
    }
}