using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace czy.MyDAL.SQL
{
    /// <summary>
    /// WriteToTxt 的摘要说明
    /// </summary>
    public sealed partial class DBFileCreate
    {
        public DBFileCreate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
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

        //        message += "public class M" + dtTableName[i].Name.ToString() + "\n";
        //        message += "{" + "\n";

        //        DataTable dtColumnsName = GetSqlSchema.SelectDataTableColumns(dtTableName[i].Name.ToString());
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
        //        StreamWriter sw = new StreamWriter("d:\\interFace\\I" + dtTableName[i].Name.ToString() + ".cs", false, System.Text.Encoding.Default);
        //        sw.WriteLine(message);
        //        sw.Close();
        //        sw.Dispose();
        //    }

        //}


        /// <summary>
        /// 生成工厂类
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        public static void WriteFactoryToText(string dataBaseName, string _connstr, DataBase.ConnStringType _type)
        {
            List<ColumsSchema> dtTableName = DBSchema.SelectDataBaseTable(dataBaseName, _connstr,_type);
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

            for (int i = 0; i < dtTableName.Count; i++)
            {
                message += "       public static I" + dtTableName[i].Name + " Get" + dtTableName[i].Type + "()" + "\n";
                message += "       {" + "\n";
                message += "          return new " + dtTableName[i].Name + "();" + "\n";
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
        public static void WriteInterFaceToText(string dataBaseName, string _connstr, DataBase.ConnStringType _type)
        {
            List<ColumsSchema> dtTableName = DBSchema.SelectDataBaseTable(dataBaseName, _connstr, _type);
            for (int i = 0; i < dtTableName.Count; i++)
            {
                string message = string.Empty;//写入的信息
                message += "using System;" + "\n";
                message += "using System.Data;" + "\n";
                message += "using System.Configuration;" + "\n";
                message += "using System.Web;" + "\n";
                message += "using System.IO;" + "\n";
                message += "using DAL;" + "\n";
                message += "\r\r";

                message += "public interface I" + dtTableName[i].Name.ToString() + "\n";
                message += "{" + "\n";

                List<ColumsSchema> dtColumnsName = DBSchema.SelectDataTableColumns(dtTableName[i].Name.ToString(), _connstr, _type);
                message += "         DataTable Select" + dtTableName[i].Name.ToString() + "();" + "\n";

                string values = string.Empty;//insert要插入几项的值
                string param = string.Empty;//参数值
                string itemsValue = string.Empty;//insert中values的值
                string setValue = string.Empty;//update中的setvalue值
                for (int j = 1; j < dtColumnsName.Count; j++)
                {
                    if (j == 1)
                    {
                        values += dtColumnsName[j].Name.ToString();
                        param += "string " + dtColumnsName[j].Name.ToString();
                        itemsValue += "'\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                        setValue += dtColumnsName[j].Name.ToString() + "='\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                    }
                    else
                    {
                        values += "," + dtColumnsName[j].Name.ToString();
                        param += "," + "string " + dtColumnsName[j].Name.ToString();
                        itemsValue += "," + "'\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                        setValue += "," + dtColumnsName[j].Name.ToString() + "='\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                    }
                }

                message += "         int Insert" + dtTableName[i].Name.ToString() + "(" + param + ");" + "\n";
                message += "         int Delete" + dtTableName[i].Name.ToString() + "ByID(string " + dtColumnsName[i].Name.ToString() + ");" + "\n";
                message += "         int UpDate" + dtTableName[i].Name.ToString() + "ByID(string " + dtColumnsName[i].Name.ToString() + "," + param + ");" + "\n";


                message += "}";

                Directory.CreateDirectory("D:\\interFace");
                StreamWriter sw = new StreamWriter("d:\\interFace\\I" + dtTableName[i].Name.ToString() + ".cs", false, System.Text.Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// 生成类( 非存储过程)
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        public static void WriteSqlToText(string dataBaseName, string _connstr, DataBase.ConnStringType _type)
        {

            List<ColumsSchema> dtTableName = DBSchema.SelectDataBaseTable(dataBaseName, _connstr, _type);
            for (int i = 0; i < dtTableName.Count; i++)
            {
                string message = string.Empty;//写入的信息
                message += "using System;" + "\n";
                message += "using System.Data;" + "\n";
                message += "using System.Configuration;" + "\n";
                message += "using System.Web;" + "\n";
                message += "using System.IO;" + "\n";
                message += "using DAL;" + "\n";
                message += "\r\r";

                message += "public class " + dtTableName[i].Name.ToString() + ": I" + dtTableName[i].Name.ToString() + "\n";
                message += "{" + "\n";

                List<ColumsSchema>  dtColumnsName = DBSchema.SelectDataTableColumns(dtTableName[i].Name.ToString(), _connstr, _type);
                message += "       //查询" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  DataTable Select" + dtTableName[i].Name.ToString() + "()" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Select * from " + dtTableName[i].Name.ToString() + "\";" + "\n";
                message += "           return Util.GetDataTable(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                string values = string.Empty;//insert要插入几项的值
                string param = string.Empty;//参数值
                string itemsValue = string.Empty;//insert中values的值
                string setValue = string.Empty;//update中的setvalue值
                for (int j = 1; j < dtColumnsName.Count; j++)
                {
                    if (j == 1)
                    {
                        values += dtColumnsName[j].Name.ToString();
                        param += "string " + dtColumnsName[j].Name.ToString();
                        itemsValue += "'\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                        setValue += dtColumnsName[j].Name.ToString() + "='\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                    }
                    else
                    {
                        values += "," + dtColumnsName[j].Name.ToString();
                        param += "," + "string " + dtColumnsName[j].Name.ToString();
                        itemsValue += "," + "'\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                        setValue += "," + dtColumnsName[j].Name.ToString() + "='\" +" + dtColumnsName[j].Name.ToString() + "+\"'";
                    }
                }
                message += "       //插入" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  int Insert" + dtTableName[i].Name.ToString() + "(" + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"insert into " + dtTableName[i].Name.ToString() + "(" + values + ") values(" + itemsValue + ")\";" + "\n";
                message += "           return Util.GetExecuteNonQuery(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //删除" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  int Delete" + dtTableName[i].Name.ToString() + "ByID(string " + dtColumnsName[i].Name.ToString() + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Delete " + dtTableName[i].Name.ToString() + " where " + dtColumnsName[i].Name.ToString() + "='\"+" + dtColumnsName[i].Name.ToString() + "+\"'\";" + "\n";
                message += "           return  Util.GetExecuteNonQuery(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //修改" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  int UpDate" + dtTableName[i].Name.ToString() + "ByID(string " + dtColumnsName[i].Name.ToString() + "," + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"UpDate " + dtTableName[i].Name.ToString() + " set " + setValue + " where " + dtColumnsName[i].Name.ToString() + "='\"+" + dtColumnsName[i].Name.ToString() + "+\"'\";" + "\n";
                message += "           return  Util.GetExecuteNonQuery(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "}";

                Directory.CreateDirectory("D:\\sql");
                StreamWriter sw = new StreamWriter("d:\\sql\\" + dtTableName[i].Name.ToString() + ".cs", false, System.Text.Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }

        }

        //存储过程
        public static void WriteSqlProcToText(string dataBaseName, string _connstr, DataBase.ConnStringType _type)
        {

            List<ColumsSchema> dtTableName = DBSchema.SelectDataBaseTable(dataBaseName, _connstr, _type);
            for (int i = 0; i < dtTableName.Count; i++)
            {
                string message = string.Empty;//写入的信息
                message += "using System;" + "\n";
                message += "using System.Data;" + "\n";
                message += "using System.Configuration;" + "\n";
                message += "using System.Web;" + "\n";
                message += "using System.IO;" + "\n";
                message += "using DAL;" + "\n";
                message += "\r\r";

                message += "public class " + dtTableName[i].Name.ToString() + ": I" + dtTableName[i].Name.ToString() + "\n";
                message += "{" + "\n";

                List<ColumsSchema> dtColumnsName = DBSchema.SelectDataTableColumns(dtTableName[i].Name.ToString(), _connstr, _type);
                message += "       //查询" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  DataTable Select" + dtTableName[i].Name.ToString() + "()" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Select * from " + dtTableName[i].Name.ToString() + "\";" + "\n";
                message += "           return Util.GetDataTable(cmd);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                string param = string.Empty;//参数值
                for (int j = 1; j < dtColumnsName.Count; j++)
                {
                    param = "SqlParameter[] sqlParam = new SqlParameter[" + dtColumnsName.Count + "];";
                    param += "sqlParam[" + (j - 1) + "] = new SqlParamter(@\"" + dtTableName[i].Name.ToString() + "\", " + dtTableName[i].Name + ");" + "\n";
                    param += "sqlParam[" + (j - 1) + "].Value=" + dtTableName[i].Name.ToString() + ";";
                }
                message += "       //插入" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  int Insert" + dtTableName[i].Name.ToString() + "(" + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"insert" + dtTableName[i].Name.ToString() + "\";" + "\n";
                message += "           " + param + "\n";
                message += "           return Util.GetExecuteNonQuery(cmd,sqlParam);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //删除" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  int Delete" + dtTableName[i].Name.ToString() + "ByID(string " + dtColumnsName[i].Name.ToString() + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"Delete" + dtTableName[i].Name.ToString() + "\";" + "\n";
                message += "           SqlParameter[] sqlParam = new SqlParameter[1];";
                message += "           sqlParam[0] = new SqlParamter(@\"" + dtColumnsName[i].Name.ToString() + "\", " +dtTableName[i].Name.ToString() + ");" + "\n";
                message += "           sqlParam[0].Value=" + dtColumnsName[i].Name.ToString() + ";";
                message += "           return  Util.GetExecuteNonQuery(cmd,sqlParam);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "       //修改" + dtTableName[i].Name.ToString() + "\n";
                message += "       public  int UpDate" + dtTableName[i].Name.ToString() + "ByID(string " + dtColumnsName[i].Name.ToString() + "," + param + ")" + "\n";
                message += "       {" + "\n";
                message += "           string cmd=\"UpDate" + dtTableName[i].Name.ToString() + "\";" + "\n";
                message += "           " + param + "\n";
                message += "           return  Util.GetExecuteNonQuery(cmd,sqlParam);" + "\n";
                message += "       }" + "\n";
                message += "\r";

                message += "}";

                Directory.CreateDirectory("D:\\sql");
                StreamWriter sw = new StreamWriter("d:\\sql\\" + dtTableName[i].Name.ToString() + ".cs", false, System.Text.Encoding.Default);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }

        }

    }

}