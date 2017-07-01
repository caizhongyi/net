using System;
using System.Collections.Generic;
using System.Text;

namespace czy.MyClass
{
    public sealed partial class StringPlus
    {
        public static List<string> GetStrArray(string str, char speater,bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss =  str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) &&s !=speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }
        public static string GetArrayStr(List<string> list,string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        
        
        #region ɾ�����һ���ַ�֮����ַ�

        /// <summary>
        /// ɾ������β��һ������
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

       
        #endregion




       

        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] ss = o_str.Split(sepeater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }


        #region ���ַ�����ʽת��Ϊ���ַ���
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //���Ϊ�գ����ؿ�ֵ
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //����ȥ���ָ���
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region ���ַ���ת��Ϊ����ʽ
        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string ReturnValue = "";
            //��������ֵ�����ؿգ�������������ʾ
            if (StrList == null)
            {
                ReturnValue = "";
                Error = "��������Ҫ���ָ�ʽ���ַ���";
            }
            else
            {
                //��鴫����ַ������Ⱥ���ʽ�Ƿ�ƥ��,�����ƥ�䣬��˵��ʹ�ô��󡣸���������Ϣ�����ؿ�ֵ
                int strListLength = StrList.Length;
                int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                if (strListLength != NewStyleLength)
                {
                    ReturnValue = "";
                    Error = "��ʽ��ʽ�ĳ�����������ַ����Ȳ���������������";
                }
                else
                {
                    //�������ʽ�зָ�����λ��
                    string Lengstr = "";
                    for (int i = 0; i < NewStyle.Length; i++)
                    {
                        if (NewStyle.Substring(i, 1) == SplitString)
                        {
                            Lengstr = Lengstr + "," + i;
                        }
                    }
                    if (Lengstr != "")
                    {
                        Lengstr = Lengstr.Substring(1);
                    }
                    //���ָ�����������ʽ�е�λ��
                    string[] str = Lengstr.Split(',');
                    foreach (string bb in str)
                    {
                        StrList = StrList.Insert(int.Parse(bb), SplitString);
                    }
                    //�������Ľ��
                    ReturnValue = StrList;
                    //��Ϊ�������������û�д���
                    Error = "";
                }
            }
            return ReturnValue;
        }
        #endregion
    }
}
