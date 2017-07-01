using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class SqlCmdStr
    {
        /// <summary>
        /// 返回sql插入字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colsArgs">列名</param>
        /// <param name="paramArgs">参数</param>
        /// <returns></returns>
        public string GetInsert(string tableName,string[] colsArgs,string[] paramArgs)
        {
            int i = 0;
            string clostr = string.Empty;
            foreach (string col in colsArgs)
            {
                if (i == 0)
                {
                    clostr += col;
                }
                else 
                {
                    clostr += "," + col;
                }
                i++;
            }
            i = 0;
            string paramstr = string.Empty;
            foreach (string param in paramArgs)
            {
                try
                {
                    Convert.ToDateTime(param);
                    if (i == 0)
                    {
                        paramstr += "N'" + param + "'";
                    }
                    else
                    {
                        paramstr += ",N'" + param + "'";
                    }
                    i++;

                }
                catch 
                {
                    if (i == 0)
                    {
                        paramstr += "'" + param + "'";
                    }
                    else
                    {
                        paramstr+= ",'" + param + "'";
                    }
                    i++;
                }
            }
            string cmd = string.Format("insert into {0}({1}) values({2});", tableName, clostr, paramstr);
            return cmd;
        }

        /// <summary>
        /// 返回sql修改字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colsArgs">列名</param>
        /// <param name="paramArgs">参数</param>
        /// <param name="condtion">条件</param>
        /// <returns></returns>
        public string GetUpdate(string tableName, string[] colsArgs, string[] paramArgs,string condtion)
        {

            string upstr =string.Empty ;
            for (int i = 0, j = 0; i < colsArgs.Length && j < paramArgs.Length; i++, j++)
            {
                if (i == 0)
                {
                    try
                    {
                        Convert.ToDateTime(paramArgs[j]);
                        string p =string.Format ("{0}=N'{1}'",colsArgs[i],paramArgs[j]); 
                        upstr+=p;
                    }
                    catch
                    {
                        string p = string.Format("{0}='{1}'", colsArgs[i], paramArgs[j]);
                        upstr += p;
                    }
                }
                else
                {
                    try
                    {
                        Convert.ToDateTime(paramArgs[j]);
                        string p = string.Format(",{0}=N'{1}'", colsArgs[i], paramArgs[j]);
                        upstr += p;
                    }
                    catch
                    {
                        string p = string.Format(",{0}='{1}'", colsArgs[i], paramArgs[j]);
                        upstr += p;
                    }
                }
            }

            string cmd = string.Format("update {0} set {1} where {2}", tableName, upstr, condtion);
            return cmd;
        }

        /// <summary>
        /// 返回删除字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="conditon">条件</param>
        /// <returns></returns>
        public string GetDel(string tableName,string conditon)
        {
            return string.Format("delete from {0} where {1}",tableName, conditon);
        }
    }
}
