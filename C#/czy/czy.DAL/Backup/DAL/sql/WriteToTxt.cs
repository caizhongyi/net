using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DAL;
using System.Data.SqlClient;

namespace MyClass
{
    /// <summary>
    /// WriteToTxt 的摘要说明
    /// </summary>
    public class WriteToTxt
    {
        public WriteToTxt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private static string GetType(string dbType)
        {
            return "";
        }

        private static string GetSqlType(string paramType)
        {
            switch (paramType)
            {
                case "varchar": return "SqlDbType.VarChar";

                case "char": return "SqlDbType.Char";

                case "text": return "SqlDbType.Text";

                case "datetime": return "SqlDbType.DateTime";

                case "money": return "SqlDbType.Money";

                case "decimal": return "SqlDbType.Decimal";

                case "bigint": return "SqlDbType.BigInt";

                case "bit": return "SqlDbType.Bit";

                case "float": return "SqlDbType.Float";

                case "tinyint": return "SqlDbType.TinyInt";

                case "nvarchar": return "SqlDbType.NVarChar";

                case "image": return "SqlDbType.Image";

                default: return "SqlDbType .VarChar";

            }
        }
        /// <summary>
        /// 生成工厂类
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        //public static void WriteDbPropety(string dataBaseName)
        //{
        //    DataTable dtTableName = GetSqlSchema.SelectDataBaseTable(dataBaseName);
        //    for (int i = 0; i < dtTableName.Rows.Count; i++)
        //    {
        //        string message = string.Empty;//写入的信息
        //        message += "using System;" + "\n";
        //        message += "using System.Data;" + "\n";
        //        message += "using System.Configuration;" + "\n";
        //        message += "using System.Web;" + "\n";
        //        message += "using System.IO;" + "\n";
        //        message += "using DAL;" + "\n";
        //        message += "\r\r";

        //        message += "public class M" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
        //        message += "{" + "\n";

        //        DataTable dtColumnsName = GetSqlSchema.SelectDataTableColumns(dtTableName.Rows[i].ItemArray[0].ToString());
        //        for (int j = 1; j < dtColumnsName.Rows.Count; j++)
        //        {
        //            message += "         private " + GetType(dtColumnsName.Rows[j]["type"].ToString()) + " " + dtColumnsName.Rows[j]["name"].ToString();
        //            message += "         public  " + GetType(dtColumnsName.Rows[j]["type"].ToString()) + " " + dtColumnsName.Rows[j]["name"].ToString();
        //            message += "        {";
        //            message += "         public  " + GetType(dtColumnsName.Rows[j]["type"].ToString()) + " " + dtColumnsName.Rows[j]["name"].ToString();
        //            message += "         public  " + GetType(dtColumnsName.Rows[j]["type"].ToString()) + " " + dtColumnsName.Rows[j]["name"].ToString();
        //            message += "         })";

        //        }
        //        message += "}";

        //        Directory.CreateDirectory("D:\\interFace");
        //        StreamWriter sw = new StreamWriter("d:\\interFace\\I" + dtTableName.Rows[i].ItemArray[0].ToString() + ".cs", false, System.Text.Encoding.Default);
        //        sw.WriteLine(message);
        //        sw.Close();
        //        sw.Dispose();
        //    }

        //}


        /// <summary>
        /// 生成工厂类
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        public static void WriteFactoryToText(string dataBaseName)
        {
            DataTable dtTableName = GetSqlSchema.SelectDataBaseTable(dataBaseName);
            string message = string.Empty;//写入的信息
            message += "using System;" + "\n";
            message += "using System.Data;" + "\n";
            message += "using System.Configuration;" + "\n";
            message += "using System.Web;" + "\n";
            message += "using System.IO;" + "\n";
            message += "using DAL;" + "\n";
            message += "\r\r";

            message += "public class Factory \n";
            message += "{" + "\n";

            for (int i = 0; i < dtTableName.Rows.Count; i++)
            {
                message += "       public static I" + dtTableName.Rows[i].ItemArray[0].ToString() + " Get" + dtTableName.Rows[i].ItemArray[0].ToString() + "()" + "\n";
                message += "       {" + "\n";
                message += "          return new " + dtTableName.Rows[i].ItemArray[0].ToString() + "();" + "\n";
                message += "       }" + "\n";
                message += "\r";
            }
            message += "       }" + "\n";
            StreamWriter sw = new StreamWriter("d:\\factory.cs", false, System.Text.Encoding.Default);
            sw.WriteLine(message);
            sw.Close();
            sw.Dispose();

        }
        /// <summary>
        /// 生成接口
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        public static void WriteInterFaceToText(string dataBaseName)
        {
            DataTable dtTableName = GetSqlSchema.SelectDataBaseTable(dataBaseName);
            for (int i = 0; i < dtTableName.Rows.Count; i++)
            {
                string message = string.Empty;//写入的信息
                message += "using System;" + "\n";
                message += "using System.Data;" + "\n";
                message += "using System.Configuration;" + "\n";
                message += "using System.Web;" + "\n";
                message += "using System.IO;" + "\n";
                message += "using DAL;" + "\n";
                message += "\r\r";

                message += "public interface I" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "{" + "\n";

                DataTable dtColumnsName = GetSqlSchema.SelectDataTableColumns(dtTableName.Rows[i].ItemArray[0].ToString());
                message += "         DataTable Select" + dtTableName.Rows[i].ItemArray[0].ToString() + "();" + "\n";

                string values = string.Empty;//insert要插入几项的值
                string param = string.Empty;//参数值
                string itemsValue = string.Empty;//insert中values的值
                string setValue = string.Empty;//update中的setvalue值
                for (int j = 1; j < dtColumnsName.Rows.Count; j++)
                {
                    if (j == 1)
                    {
                        values += dtColumnsName.Rows[j].ItemArray[0].ToString();
                        param += "string " + dtColumnsName.Rows[j].ItemArray[0].ToString();
                        itemsValue += "'\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                        setValue += dtColumnsName.Rows[j].ItemArray[0].ToString() + "='\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                    }
                    else
                    {
                        values += "," + dtColumnsName.Rows[j].ItemArray[0].ToString();
                        param += "," + "string " + dtColumnsName.Rows[j].ItemArray[0].ToString();
                        itemsValue += "," + "'\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                        setValue += "," + dtColumnsName.Rows[j].ItemArray[0].ToString() + "='\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                    }
                }

                message += "         int Insert" + dtTableName.Rows[i].ItemArray[0].ToString() + "(" + param + ");" + "\n";
                message += "         int Delete" + dtTableName.Rows[i].ItemArray[0].ToString() + "ByID(string " + dtColumnsName.Rows[0].ItemArray[0].ToString() + ");" + "\n";
                message += "         int UpDate" + dtTableName.Rows[i].ItemArray[0].ToString() + "ByID(string " + dtColumnsName.Rows[0].ItemArray[0].ToString() + "," + param + ");" + "\n";


                message += "}";

                Directory.CreateDirectory("D:\\interFace");
                StreamWriter sw = new StreamWriter("d:\\interFace\\I" + dtTableName.Rows[i].ItemArray[0].ToString() + ".cs", false, System.Text.Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// 生成类( 非存储过程)
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        public static void WriteSqlToText(string dataBaseName)
        {

            DataTable dtTableName = GetSqlSchema.SelectDataBaseTable(dataBaseName);
            for (int i = 0; i < dtTableName.Rows.Count; i++)
            {
                string message = string.Empty;//写入的信息
                message += "using System;" + "\n";
                message += "using System.Data;" + "\n";
                message += "using System.Configuration;" + "\n";
                message += "using System.Web;" + "\n";
                message += "using System.IO;" + "\n";
                message += "using DAL;" + "\n";
                message += "\r\r";

                message += "public class " + dtTableName.Rows[i].ItemArray[0].ToString() + ": I" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "{" + "\n";

                DataTable dtColumnsName = GetSqlSchema.SelectDataTableColumns(dtTableName.Rows[i].ItemArray[0].ToString());
                message += "       //查询" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  DataTable Select" + dtTableName.Rows[i].ItemArray[0].ToString() + "()" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Select * from " + dtTableName.Rows[i].ItemArray[0].ToString() + "\";" + "\n";
                message += "           return Util.GetDataTable(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                string values = string.Empty;//insert要插入几项的值
                string param = string.Empty;//参数值
                string itemsValue = string.Empty;//insert中values的值
                string setValue = string.Empty;//update中的setvalue值
                for (int j = 1; j < dtColumnsName.Rows.Count; j++)
                {
                    if (j == 1)
                    {
                        values += dtColumnsName.Rows[j].ItemArray[0].ToString();
                        param += "string " + dtColumnsName.Rows[j].ItemArray[0].ToString();
                        itemsValue += "'\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                        setValue += dtColumnsName.Rows[j].ItemArray[0].ToString() + "='\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                    }
                    else
                    {
                        values += "," + dtColumnsName.Rows[j].ItemArray[0].ToString();
                        param += "," + "string " + dtColumnsName.Rows[j].ItemArray[0].ToString();
                        itemsValue += "," + "'\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                        setValue += "," + dtColumnsName.Rows[j].ItemArray[0].ToString() + "='\" +" + dtColumnsName.Rows[j].ItemArray[0].ToString() + "+\"'";
                    }
                }
                message += "       //插入" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  int Insert" + dtTableName.Rows[i].ItemArray[0].ToString() + "(" + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"insert into " + dtTableName.Rows[i].ItemArray[0].ToString() + "(" + values + ") values(" + itemsValue + ")\";" + "\n";
                message += "           return Util.GetExecuteNonQuery(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //删除" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  int Delete" + dtTableName.Rows[i].ItemArray[0].ToString() + "ByID(string " + dtColumnsName.Rows[0].ItemArray[0].ToString() + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Delete " + dtTableName.Rows[i].ItemArray[0].ToString() + " where " + dtColumnsName.Rows[0].ItemArray[0].ToString() + "='\"+" + dtColumnsName.Rows[0].ItemArray[0].ToString() + "+\"'\";" + "\n";
                message += "           return  Util.GetExecuteNonQuery(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //修改" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  int UpDate" + dtTableName.Rows[i].ItemArray[0].ToString() + "ByID(string " + dtColumnsName.Rows[0].ItemArray[0].ToString() + "," + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"UpDate " + dtTableName.Rows[i].ItemArray[0].ToString() + " set " + setValue + " where " + dtColumnsName.Rows[0].ItemArray[0].ToString() + "='\"+" + dtColumnsName.Rows[0].ItemArray[0].ToString() + "+\"'\";" + "\n";
                message += "           return  Util.GetExecuteNonQuery(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "}";

                Directory.CreateDirectory("D:\\sql");
                StreamWriter sw = new StreamWriter("d:\\sql\\" + dtTableName.Rows[i].ItemArray[0].ToString() + ".cs", false, System.Text.Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }

        }

        //存储过程
        public static void WriteSqlProcToText(string dataBaseName)
        {

            DataTable dtTableName = GetSqlSchema.SelectDataBaseTable(dataBaseName);
            for (int i = 0; i < dtTableName.Rows.Count; i++)
            {
                string message = string.Empty;//写入的信息
                message += "using System;" + "\n";
                message += "using System.Data;" + "\n";
                message += "using System.Configuration;" + "\n";
                message += "using System.Web;" + "\n";
                message += "using System.IO;" + "\n";
                message += "using DAL;" + "\n";
                message += "\r\r";

                message += "public class " + dtTableName.Rows[i].ItemArray[0].ToString() + ": I" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "{" + "\n";

                DataTable dtColumnsName = GetSqlSchema.SelectDataTableColumns(dtTableName.Rows[i].ItemArray[0].ToString());
                message += "       //查询" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  DataTable Select" + dtTableName.Rows[i].ItemArray[0].ToString() + "()" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Select * from " + dtTableName.Rows[i].ItemArray[0].ToString() + "\";" + "\n";
                message += "           return Util.GetDataTable(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                string param = string.Empty;//参数值
                for (int j = 1; j < dtColumnsName.Rows.Count; j++)
                {
                    param = "SqlParameter[] sqlParam = new SqlParameter[" + dtColumnsName.Rows.Count + "];";
                    param += "sqlParam[" + (j - 1) + "] = new SqlParamter(@\"" + dtColumnsName.Rows[i].ItemArray[0].ToString() + "\", " + GetSqlType(dtTableName.Rows[i].ItemArray[1].ToString()) + ");" + "\n";
                    param += "sqlParam[" + (j - 1) + "].Value=" + dtColumnsName.Rows[i].ItemArray[0].ToString() + ";";
                }
                message += "       //插入" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  int Insert" + dtTableName.Rows[i].ItemArray[0].ToString() + "(" + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"insert" + dtTableName.Rows[i].ItemArray[0].ToString() + "\";" + "\n";
                message += "           " + param + "\n";
                message += "           return Util.GetExecuteNonQuery(cmd,sqlParam);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //删除" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  int Delete" + dtTableName.Rows[i].ItemArray[0].ToString() + "ByID(string " + dtColumnsName.Rows[0].ItemArray[0].ToString() + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Delete" + dtTableName.Rows[i].ItemArray[0].ToString() + "\";" + "\n";
                message += "           SqlParameter[] sqlParam = new SqlParameter[1];";
                message += "           sqlParam[0] = new SqlParamter(@\"" + dtColumnsName.Rows[0].ItemArray[0].ToString() + "\", " + GetSqlType(dtTableName.Rows[i].ItemArray[1].ToString()) + ");" + "\n";
                message += "           sqlParam[0].Value=" + dtColumnsName.Rows[0].ItemArray[0].ToString() + ";";
                message += "           return  Util.GetExecuteNonQuery(cmd,sqlParam);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //修改" + dtTableName.Rows[i].ItemArray[0].ToString() + "\n";
                message += "       public  int UpDate" + dtTableName.Rows[i].ItemArray[0].ToString() + "ByID(string " + dtColumnsName.Rows[0].ItemArray[0].ToString() + "," + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"UpDate" + dtTableName.Rows[i].ItemArray[0].ToString() + "\";" + "\n";
                message += "           " + param + "\n";
                message += "           return  Util.GetExecuteNonQuery(cmd,sqlParam);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "}";

                Directory.CreateDirectory("D:\\sql");
                StreamWriter sw = new StreamWriter("d:\\sql\\" + dtTableName.Rows[i].ItemArray[0].ToString() + ".cs", false, System.Text.Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }

        }

    }

}