using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace czy.MyDAL.SQL
{
    public sealed partial class SQLCodeBuilder
    {
        public enum DataBaseType
        {
            ACCESS,
            MSSQL
        }

        #region 私有
        string _password = "";

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        string _modelNameSpace = "Models";
        /// <summary>
        /// 模型命名空间
        /// </summary>
        public string ModelNameSpace
        {
            get { return _modelNameSpace; }
            set { _modelNameSpace = value; }
        }
        string _bblNameSpace = "BBL";
        /// <summary>
        /// BBL命名空间
        /// </summary>
        public string BblNameSpace
        {
            get { return _bblNameSpace; }
            set { _bblNameSpace = value; }
        }

        string conn = string.Empty;
        /// <summary>
        /// 链接字符窜
        /// </summary>
        public string Conn
        {
            get { return conn; }
            set { conn = value; }
        }
        string dbName = string.Empty;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName
        {
            get { return dbName; }
            set { dbName = value; }
        }
     
        #endregion
        public SQLCodeBuilder()
        { }
        public SQLCodeBuilder(string databaseConn, string databaseName)
        {
            conn = databaseConn;
            dbName = databaseName;
        }
       // Provider=Microsoft.Jet.OLEDB.4.0; Data Source=F:\wwwxiyanet\data\data.mdb
        /// <summary>
        /// BBL创建
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="conn"></param>
        /// <param name="nameSpace"></param>
        /// <param name="type"></param>
        public void CreateBBL(DataBase.ConnStringType type, DataBaseType databaseType)
        {
            string modelsNameSapnce = _modelNameSpace;
            string nameSpace = _bblNameSpace;
            if (databaseType == DataBaseType.MSSQL)
            {
                List<ColumsSchema> csList = SQL.DBSchema.SelectDataBaseTable(dbName, conn, type);
                BBLSaveFile(csList);
            }
            else
            {
                List<ColumsSchema> csList = SQL.DBSchema.SelectAccessTable(dbName, _password);
                BBLSaveFile(csList);
            }
           
        }

        private void BBLSaveFile(List<ColumsSchema> csList)
        {
            foreach (ColumsSchema dr in csList)
            {
                string name = dr.Name;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Data;");
                sb.AppendLine("using czy.IFactory;");
                sb.AppendLine("using czy.SQLAccess.CommandHelper;");
                sb.AppendLine("using czy.SQLAccess.DataPager;");
                sb.AppendLine(" ");
                sb.AppendLine("namespace " + _bblNameSpace);
                sb.AppendLine("{");
                sb.AppendLine("    public class " + name);
                sb.AppendLine("    {");
                sb.AppendLine("       #region SelectData");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                //查询
                sb.AppendLine("       public static DataSet Select()");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetSelectSQL(new " + _modelNameSpace + "." + name + "(), \"\");");
                sb.AppendLine("         return Factory.GetDataBase().GetDataSet(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       public static List<" + _modelNameSpace + "." + name + "> SelectList()");
                sb.AppendLine("       {");
                sb.AppendLine("         return Select().Tables[0].TableToList<" + _modelNameSpace + "." + name + ">();");
                sb.AppendLine("       }");
                sb.AppendLine("");
                //sb.AppendLine("       /// <summary>");
                //sb.AppendLine("       ///  获取刚插入自增值");
                //sb.AppendLine("       /// </summary>");
                //sb.AppendLine("       /// <param name=\"id\">跟据ID查询</param>");
                //sb.AppendLine("       public static string SelectIndentCurrentSQL()");
                //sb.AppendLine("       {");
                //sb.AppendLine("         string sql = @\"SELECT IDENT_CURRENT('"+name+"') from "+name);
                //sb.AppendLine("         return Factory.GetDataBase().GetDataSet(sql).Tables[0].Rows[0].ToString()");
                //sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"id\">跟据ID查询</param>");
                sb.AppendLine("       public static DataSet Select(object id)");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetSelectSQL(new " + _modelNameSpace + "." + name + "(), string .Format ( \"" + GetID(name.ToString()) + "={0}\",id.ToString()));");
                sb.AppendLine("         return Factory.GetDataBase().GetDataSet(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("");
                //sb.AppendLine("       /// <summary>");
                //sb.AppendLine("       /// 查询");
                //sb.AppendLine("       /// </summary>");
                //sb.AppendLine("       /// <param name=\"id\">跟据ID查询</param>");
                //sb.AppendLine("       public static List<"+modelsNameSapnce+"." + name + "> Select(int id)");
                //sb.AppendLine("       {");
                //sb.AppendLine("          return Select(id).Tables[0].TableToList<"+modelsNameSapnce+"." + name + ">();");
                //sb.AppendLine("       }");
                //sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"id\">跟据ID查询</param>");
                sb.AppendLine("       public static List<" + _modelNameSpace + "." + name + "> SelectList(object id)");
                sb.AppendLine("       {");
                sb.AppendLine("          return Select(id).Tables[0].TableToList<" + _modelNameSpace + "." + name + ">();");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"columnParams\">列用,号隔开</param>");
                sb.AppendLine("       /// <param name=\"filter\">条件</param>");
                sb.AppendLine("       public static DataSet Select(string columnParams, string filter)");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetSelectSQL(\"" + name + "\",columnParams,filter);");
                sb.AppendLine("         return Factory.GetDataBase().GetDataSet(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"columnParams\">列用,号隔开</param>");
                sb.AppendLine("       /// <param name=\"filter\">条件</param>");
                sb.AppendLine("       public static List<" + _modelNameSpace + "." + name + ">  SelectList(string columnParams, string filter)");
                sb.AppendLine("       {");
                sb.AppendLine("         return Select( columnParams,  filter).Tables[0].TableToList<" + _modelNameSpace + "." + name + "> ();");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       #endregion");
                sb.AppendLine("");
                sb.AppendLine("       #region Execute");
                //插入
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 插入");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"" + _modelNameSpace + "." + name + "\">模型</param>");
                sb.AppendLine("       public static int Insert(" + _modelNameSpace + "." + name + " " + name + ")");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetInsertSQL(" + name + ", \"" + GetID(name.ToString()) + "\");");
                sb.AppendLine("         return Factory.GetDataBase().ExecuteNonQuery(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("");
                //sb.AppendLine("       /// <summary>");
                //sb.AppendLine("       /// 查询");
                //sb.AppendLine("       /// </summary>");
                //sb.AppendLine("       /// <param name=\"id\">id</param>");
                //sb.AppendLine("       /// <param name=\"Mode."+name+"\">模型</param>");
                //sb.AppendLine("       public static int Update(int id,"+modelsNameSapnce+"." + name + " " + name + ")");
                //sb.AppendLine("       {");
                //sb.AppendLine("         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(" + name + ", \"" + GetID(name.ToString()) + "\", \"" + GetID(name.ToString()) + "=\" + id);");
                //sb.AppendLine("         return Factory.GetDataBase().ExecuteNonQuery(sql);");
                //sb.AppendLine("       }");
                //sb.AppendLine("");
                //修改
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"id\">id</param>");
                sb.AppendLine("       /// <param name=\"Mode." + name + "\">模型</param>");
                sb.AppendLine("       public static int Update(object id," + _modelNameSpace + "." + name + " " + name + ")");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetUpdateSQL(" + name + ", \"" + GetID(name.ToString()) + "\", \"" + GetID(name.ToString()) + "=\" + id.ToString());");
                sb.AppendLine("         return Factory.GetDataBase().ExecuteNonQuery(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"columns\">列</param>");
                sb.AppendLine("       /// <param name=\"values\">值</param>");
                sb.AppendLine("       /// <param name=\"filter\">条件</param>");
                sb.AppendLine("       public static int Update(string[] columns,string[] values,string filter)");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetUpdateSQL(\"" + name + "\", columns, values,filter);");
                sb.AppendLine("         return Factory.GetDataBase().ExecuteNonQuery(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("");
                //sb.AppendLine("       /// <summary>");
                //sb.AppendLine("       /// 删除");
                //sb.AppendLine("       /// </summary>");
                //sb.AppendLine("       /// <param name=\"id\">id</param>");
                //sb.AppendLine("       public static int Delete(int id)");
                //sb.AppendLine("       {");
                //sb.AppendLine("         string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new "+modelsNameSapnce+"." + name + "(), \"" + GetID(name.ToString()) + "=\" + id);");
                //sb.AppendLine("         return Factory.GetDataBase().ExecuteNonQuery(sql);");
                //sb.AppendLine("       }");

                //删除
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 删除");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"id\">id</param>");
                sb.AppendLine("       public static int Delete(object id)");
                sb.AppendLine("       {");
                sb.AppendLine("         string sql = SQLCommandBuilder.GetDelSQL(new " + _modelNameSpace + "." + name + "(), \"" + GetID(name.ToString()) + "=\" + id);");
                sb.AppendLine("         return Factory.GetDataBase().ExecuteNonQuery(sql);");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("       #endregion");
                sb.AppendLine("");

                //分页pagition
                sb.AppendLine("       #region 查询当前页数据");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询当前页数据");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"cur\">当前页</param>");
                sb.AppendLine("       /// <param name=\"size\">当前页大小</param>");
                sb.AppendLine("       public static DataSet GetCurrentData(int cur, int size)");
                sb.AppendLine("       {");
                sb.AppendLine("         DataPagerQueryParams PagerQueryParams = new DataPagerQueryParams();");
                sb.AppendLine("         PagerQueryParams.Size = size;");
                sb.AppendLine("         PagerQueryParams.TableName = \"" + name + "\";");
                sb.AppendLine("         PagerQueryParams.TableId = \"" + GetID(name.ToString()) + "\";");
                sb.AppendLine("         //Util.PagerQueryParams.Order = \"Order by u_createDate\";");
                sb.AppendLine("         PagerQueryParams.KeyFieldOrder = \"asc\";");
                sb.AppendLine("         PagerQueryParams.Colums = \"*\";");
                sb.AppendLine("         DataSet ds = Factory.GetDataPager(PagerQueryParams).GetCurrentPageData(cur);");
                sb.AppendLine("         return ds;");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询当前页数据");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"cur\">当前页</param>");
                sb.AppendLine("       /// <param name=\"size\">当前页大小</param>");
                sb.AppendLine("       /// <param name=\"order\">当前页大小</param>");
                sb.AppendLine("       /// <param name=\"keyFieldOrder\">ID列排序方式[asc][desc]</param>");
                sb.AppendLine("       /// <param name=\"columes\">查询的列可为*</param>");
                sb.AppendLine("       public static DataSet GetCurrentData(int cur, int size,string order,string keyFieldOrder,string columes,string filter)");
                sb.AppendLine("       {");
                sb.AppendLine("         DataPagerQueryParams PagerQueryParams = new DataPagerQueryParams();");
                sb.AppendLine("         PagerQueryParams.Size = size;");
                sb.AppendLine("         PagerQueryParams.TableName = \"" + name + "\";");
                sb.AppendLine("         PagerQueryParams.TableId = \"" + GetID(name.ToString()) + "\";");
                sb.AppendLine("         PagerQueryParams.Order = order;");
                sb.AppendLine("         PagerQueryParams.KeyFieldOrder = keyFieldOrder;");
                sb.AppendLine("         PagerQueryParams.Colums = columes;");
                sb.AppendLine("         PagerQueryParams.Filter = filter;");
                sb.AppendLine("         DataSet ds = Factory.GetDataPager(PagerQueryParams).GetCurrentPageData(cur);");
                sb.AppendLine("         return ds;");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询当前页数据");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"cur\">当前页</param>");
                sb.AppendLine("       /// <param name=\"size\">当前页大小</param>");
                sb.AppendLine("       /// <param name=\"order\">当前页大小</param>");
                sb.AppendLine("       /// <param name=\"keyFieldOrder\">ID列排序方式[asc][desc]</param>");
                sb.AppendLine("       /// <param name=\"columes\">查询的列可为*</param>");
                sb.AppendLine("       public static List<" + _modelNameSpace + "." + name + "> GetListCurrentData(int cur, int size)");
                sb.AppendLine("       {");
                sb.AppendLine("          return GetCurrentData( cur,  size).Tables[0].TableToList<" + _modelNameSpace + "." + name + ">();");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询当前页数据");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"cur\">当前页</param>");
                sb.AppendLine("       /// <param name=\"size\">当前页大小</param>");
                sb.AppendLine("       /// <param name=\"order\">当前页大小</param>");
                sb.AppendLine("       /// <param name=\"keyFieldOrder\">ID列排序方式[asc][desc]</param>");
                sb.AppendLine("       /// <param name=\"columes\">查询的列可为*</param>");
                sb.AppendLine("       public static List<" + _modelNameSpace + "." + name + "> GetListCurrentData(int cur, int size,string order,string keyFieldOrder,string columes,string filter)");
                sb.AppendLine("       {");
                sb.AppendLine("          return GetCurrentData( cur,  size, order, keyFieldOrder, columes,filter).Tables[0].TableToList<" + _modelNameSpace + "." + name + ">();");
                sb.AppendLine("       }");
                sb.AppendLine("");
                sb.AppendLine("       /// <summary>");
                sb.AppendLine("       /// 查询总条数");
                sb.AppendLine("       /// </summary>");
                sb.AppendLine("       /// <param name=\"size\">当前大小</param>");
                sb.AppendLine("       public static long GetTotleCount(int size)");
                sb.AppendLine("       {");
                sb.AppendLine("          DataPagerQueryParams PagerQueryParams = new DataPagerQueryParams();");
                sb.AppendLine("          PagerQueryParams.Size = size;");
                sb.AppendLine("          PagerQueryParams.TableName = \"" + name + "\";");
                sb.AppendLine("          PagerQueryParams.TableId = \"" + GetID(name.ToString()) + "\";");
                sb.AppendLine("          //Util.PagerQueryParams.Order = \"u_createDate asc\";");
                sb.AppendLine("          PagerQueryParams.KeyFieldOrder = \"asc\";");
                sb.AppendLine("          PagerQueryParams.Colums = \"*\";");
                sb.AppendLine("          return Factory.GetDataPager(PagerQueryParams).GetTotalCount();");
                sb.AppendLine("       }");
                sb.AppendLine("       #endregion");

                sb.AppendLine("    }");
                sb.AppendLine("}");

                string baseRoot = AppDomain.CurrentDomain.BaseDirectory + "BBL";
                if (!Directory.Exists(baseRoot))
                {
                    Directory.CreateDirectory(baseRoot);
                }
                FileStream fs = File.Create(baseRoot + "\\B_" + name + ".cs");
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }

        private  string GetID(string tableName)
        {
            string res=string .Empty ;
            char[] cs=  tableName.ToCharArray(); 
            foreach(char c in cs)
            {   
               if(new  Regex("[A-Z]").Match(c.ToString()).Success)
               {
                 res+=c;
               }
               if (c == '_')
               {
                   res += "_";
               }
            }
            return res.ToLower()+"_id";
         
        }
        public enum ApplicationType
        {
            Web,
            WinForm,
            WPF,
            Silverlight
        }
        /// <summary>
        /// 创建模型
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="type"></param>
        /// <param name="applicationType"></param>
        public  void CreateModel( czy.MyDAL.DataBase.ConnStringType type, ApplicationType applicationType,DataBaseType databaseType)
        {
            if (databaseType == DataBaseType.MSSQL)
            {
                //List<ColumsSchema> csList = SQL.DBSchema.SelectDataTableColumns(dbName, conn, type);
                List<ColumsSchema> tables = SQL.DBSchema.SelectDataBaseTable(dbName, conn, type);
                ModelSaveFile(databaseType,type, tables, _modelNameSpace, applicationType);
            }
            else
            {
                //List<ColumsSchema> csList = SQL.DBSchema.SelectAccessColumns(dbName, _password);
                List<ColumsSchema> tables = SQL.DBSchema.SelectAccessTable(dbName, _password);
                ModelSaveFile(databaseType,type, tables, _modelNameSpace, applicationType);
            }
        }

        private void ModelSaveFile (DataBaseType databaseType,czy.MyDAL.DataBase.ConnStringType type,List<ColumsSchema> csList, string nameSpace, ApplicationType applicationType)
        {
            foreach (ColumsSchema dr in csList)
            {
               
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("using System.ComponentModel;");
                if (applicationType == ApplicationType.Silverlight || applicationType == ApplicationType.WPF) sb.AppendLine("using System.ComponentModel.DataAnnotations;");
                sb.AppendLine("");
                sb.AppendLine("namespace " + nameSpace);
                sb.AppendLine("{");
                sb.AppendLine("    public class " + dr.Name + ": INotifyPropertyChanged");
                sb.AppendLine("    {");
                List<ColumsSchema> dt = new List<ColumsSchema>();
                if (databaseType == DataBaseType.MSSQL)
                {
                    dt = SQL.DBSchema.SelectDataTableColumns(dr.Name, conn, type);
                }
                else
                {
                    dt = SQL.DBSchema.SelectAccessColumns(dr.Name,dbName, _password);
                }
                foreach (ColumsSchema d in dt)
                {
                    string name = d.Name;
                    string ctype = d.Type;
                    sb.AppendLine("       private " + ctype.ToString() + " _" + name + ";");
                    sb.AppendLine("       public " + ctype.ToString() + " " + name);
                    sb.AppendLine("       {");
                    sb.AppendLine("            get{return _" + name + ";}");
                    sb.AppendLine("            set");
                    sb.AppendLine("               {");
                    sb.AppendLine("                  if (value != _" + name + ")");
                    sb.AppendLine("                  {");
                    sb.AppendLine("                       _" + name + " = value;");
                    ///加入验证
                    if (applicationType == ApplicationType.Silverlight || applicationType == ApplicationType.WPF) sb.AppendLine("                       Validator.ValidateProperty(value,new ValidationContext(this, null, null) { MemberName =\"" + name + "\" });");
                    if (applicationType == ApplicationType.Silverlight || applicationType == ApplicationType.WPF) sb.AppendLine("                       OnPropertyChanged(\"" + name + "\");");
                    sb.AppendLine("                  }");
                    sb.AppendLine("                }");
                    sb.AppendLine("       }");
                    sb.AppendLine("");
                }

                sb.AppendLine("        public event PropertyChangedEventHandler PropertyChanged;");
                sb.AppendLine("        public virtual void OnPropertyChanged(string propName)");
                sb.AppendLine("        {");
                sb.AppendLine("            if (PropertyChanged != null)");
                sb.AppendLine("           {PropertyChanged(this, new PropertyChangedEventArgs(propName));}");
                sb.AppendLine("       }");
                sb.AppendLine("   }");
                sb.AppendLine("}");

                string baseRoot = AppDomain.CurrentDomain.BaseDirectory + "Models";
                if (!Directory.Exists(baseRoot))
                {
                    Directory.CreateDirectory(baseRoot);
                }
                FileStream fs = File.Create(baseRoot + "\\M_" + dr.Name + ".cs");
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }
    
    }
}