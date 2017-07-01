using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using LTP.Utility;
using LTP.IDBO;
using LTP.CodeHelper;
namespace LTP.BuilderDALTranParam
{
    /// <summary>
    /// ���ݷ��ʲ���빹������Parameter��ʽ��
    /// </summary>
    public class BuilderDAL : LTP.IBuilder.IBuilderDALTran
    {

        #region ˽�б���
        protected string _key = "ID";//��ʶ�У��������ֶ�		
        protected string _keyType = "int";//��ʶ�У��������ֶ�����        
        #endregion

        #region ��������
        IDbObject dbobj;
        private string _dbname;
        private string _tablenameparent;
        private string _tablenameson;
        private List<ColumnInfo> _fieldlistparent;
        private List<ColumnInfo> _keysparent; // �����������ֶ��б�      
        private List<ColumnInfo> _fieldlistson;
        private List<ColumnInfo> _keysson; // �����������ֶ��б�                
        private string _namespace; //���������ռ���
        private string _folder; //�����ļ���               
        private string _modelpath;
        private string _modelnameparent;
        private string _modelnameson; 
        private string _dalpath;
        private string _dalnameparent;
        private string _dalnameson; 
        private string _idalpath;
        private string _iclass;
        private string _dbhelperName;//���ݿ��������    
        private string _procprefix;

        public IDbObject DbObject
        {
            set { dbobj = value; }
            get { return dbobj; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string DbName
        {
            set { _dbname = value; }
            get { return _dbname; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string TableNameParent
        {
            set { _tablenameparent = value; }
            get { return _tablenameparent; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string TableNameSon
        {
            set { _tablenameson = value; }
            get { return _tablenameson; }
        }
        
        /// <summary>
        /// ѡ����ֶμ���
        /// </summary>
        public List<ColumnInfo> FieldlistParent
        {
            set { _fieldlistparent = value; }
            get { return _fieldlistparent; }
        }
        /// <summary>
        /// ѡ����ֶμ���
        /// </summary>
        public List<ColumnInfo> FieldlistSon
        {
            set { _fieldlistson = value; }
            get { return _fieldlistson; }
        }
        /// <summary>
        /// �����������ֶεļ���
        /// </summary>
        public List<ColumnInfo> KeysParent
        {
            set { _keysparent = value; }
            get { return _keysparent; }
        }
        /// <summary>
        /// �����������ֶεļ���
        /// </summary>
        public List<ColumnInfo> KeysSon
        {
            set { _keysson = value; }
            get { return _keysson; }
        }
        /// <summary>
        /// ���������ռ���
        /// </summary>
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// �����ļ���
        /// </summary>
        public string Folder
        {
            set { _folder = value; }
            get { return _folder; }
        }
        
        /// <summary>
        /// ʵ����������ռ�
        /// </summary>
        public string Modelpath
        {
            set { _modelpath = value; }
            get { return _modelpath; }
        }
        /// <summary>
        /// Model����(����)
        /// </summary>
        public string ModelNameParent
        {
            set { _modelnameparent = value; }
            get { return _modelnameparent; }
        }
        /// <summary>
        ///  Model����(����)
        /// </summary>
        public string ModelNameSon
        {
            set { _modelnameson = value; }
            get { return _modelnameson; }
        }
        /// <summary>
        /// ʵ��������������ռ� + ����
        /// </summary>
        public string ModelSpaceParent
        {
            get { return Modelpath + "." + ModelNameParent; }
        }
        /// <summary>
        /// ʵ��������������ռ� + ����
        /// </summary>
        public string ModelSpaceSon
        {
            get { return Modelpath + "." + ModelNameSon; }
        }
        /// <summary>
        /// ���ݲ�������ռ�
        /// </summary>
        public string DALpath
        {
            set { _dalpath = value; }
            get
            {
                return _dalpath;
            }
        }
        /// <summary>
        /// ���ݲ������(����)
        /// </summary>
        public string DALNameParent
        {
            set { _dalnameparent = value; }
            get { return _dalnameparent; }
        }
        /// <summary>
        /// ���ݲ������(����)
        /// </summary>
        public string DALNameSon
        {
            set { _dalnameson = value; }
            get { return _dalnameson; }
        }

        /// <summary>
        /// �ӿڵ������ռ�
        /// </summary>
        public string IDALpath
        {
            set { _idalpath = value; }
            get
            {
                return _idalpath;
            }
        }
        /// <summary>
        /// �ӿ�����
        /// </summary>
        public string IClass
        {
            set { _iclass = value; }
            get
            {
                return _iclass;
            }
        }
        /// <summary>
        /// ���ݿ��������
        /// </summary>
        public string DbHelperName
        {
            set { _dbhelperName = value; }
            get { return _dbhelperName; }
        }
        /// <summary>
        /// �洢����ǰ׺ 
        /// </summary>       
        public string ProcPrefix
        {
            set { _procprefix = value; }
            get { return _procprefix; }
        }
        #endregion

        #region ��������

        /// <summary>
        /// ��ѡ�ֶε� select �б�
        /// </summary>
        public string Fieldstrlist
        {
            get
            {
                StringPlus _fields = new StringPlus();
                foreach (ColumnInfo obj in FieldlistParent)
                {
                    _fields.Append(obj.ColumnName + ",");
                }
                _fields.DelLastComma();
                return _fields.Value;
            }
        }
        /// <summary>
        /// ��ѡ�ֶε� select �б�
        /// </summary>
        public string FieldstrlistSon
        {
            get
            {
                StringPlus _fields = new StringPlus();
                foreach (ColumnInfo obj in FieldlistSon)
                {
                    _fields.Append(obj.ColumnName + ",");
                }
                _fields.DelLastComma();
                return _fields.Value;
            }
        }

        /// <summary>
        /// ��ͬ���ݿ����ǰ׺
        /// </summary>
        public string DbParaHead
        {
            get
            {
                switch (dbobj.DbType)
                {
                    case "SQL2000":
                    case "SQL2005":
                        return "Sql";
                    case "Oracle":
                        return "Oracle";
                    case "OleDb":
                        return "OleDb";
                    default:
                        return "Sql";
                }
            }

        }
        /// <summary>
        ///  ��ͬ���ݿ��ֶ�����
        /// </summary>
        public string DbParaDbType
        {
            get
            {
                switch (dbobj.DbType)
                {
                    case "SQL2000":
                    case "SQL2005":
                        return "SqlDbType";
                    case "Oracle":
                        return "OracleType";
                    case "OleDb":
                        return "OleDbType";
                    default:
                        return "SqlDbType";
                }
            }
        }

        /// <summary>
        /// �洢���̲��� ���÷���@
        /// </summary>
        public string preParameter
        {
            get
            {
                switch (dbobj.DbType)
                {
                    case "SQL2000":
                    case "SQL2005":
                        return "@";
                    case "Oracle":
                        return ":";
                    //case "OleDb":
                    // break;
                    default:
                        return "@";

                }
            }
        }

        /// <summary>
        /// �����Ƿ��б�ʶ��
        /// </summary>
        public bool IsHasIdentity
        {
            get
            {
                bool isid = false;
                if (_keysparent.Count > 0)
                {
                    foreach (ColumnInfo key in _keysparent)
                    {
                        if (key.IsIdentity)
                        {
                            isid = true;
                        }
                    }
                }
                return isid;
            }
        }
        #endregion

        #region ���캯��

        public BuilderDAL()
        {
        }
        public BuilderDAL(IDbObject idbobj)
        {
            dbobj = idbobj;
        }

        public BuilderDAL(IDbObject idbobj, string dbname, string tablename, string modelname, 
            List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string namepace,
            string folder, string dbherlpername, string modelpath, 
            string dalpath, string idalpath, string iclass)
        {
            dbobj = idbobj;
            _dbname = dbname;
            _tablenameparent = tablename;
            _modelnameparent = modelname;
            _namespace = namepace;
            _folder = folder;
            _dbhelperName = dbherlpername;
            _modelpath = modelpath;            
            _dalpath = dalpath;
            _idalpath = idalpath;
            _iclass = iclass;
            FieldlistParent = fieldlist;
            KeysParent = keys;
            foreach (ColumnInfo key in _keysparent)
            {
                _key = key.ColumnName;
                _keyType = key.TypeName;
                if (key.IsIdentity)
                {
                    _key = key.ColumnName;
                    _keyType = CodeCommon.DbTypeToCS(key.TypeName);
                    break;
                }
            }
        }

        #endregion


        #region  ��������Ϣ �õ��������б�

        /// <summary>
        /// �õ�Where������� - Parameter��ʽ (���磺����Exists  Delete  GetModel ��where)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetWhereExpression(List<ColumnInfo> keys)
        {
            StringPlus strclass = new StringPlus();
            foreach (ColumnInfo key in keys)
            {
                strclass.Append(key.ColumnName + "=" + preParameter + key.ColumnName + " and ");
            }
            strclass.DelLastChar("and");
            return strclass.Value;
        }

        /// <summary>
        /// ����sql����еĲ����б�(���磺����Add  Exists  Update Delete  GetModel �Ĳ�������)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetPreParameter(List<ColumnInfo> keys)
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters = {");
            int n = 0;
            foreach (ColumnInfo key in keys)
            {
                strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "" + key.ColumnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, key.TypeName, "") + "),");
                strclass2.AppendSpaceLine(3, "parameters[" + n.ToString() + "].Value = " + key.ColumnName + ";");
                n++;
            }
            strclass.DelLastComma();
            strclass.AppendLine("};");
            strclass.Append(strclass2.Value);
            return strclass.Value;
        }
        /// <summary>
        /// ����sql����еĲ����б�(���磺����Add  Exists  Update Delete  GetModel �Ĳ�������)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetPreParameter(List<ColumnInfo> keys, string numPara)
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters" + numPara + " = {");
            int n = 0;
            foreach (ColumnInfo key in keys)
            {
                strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "" + key.ColumnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, key.TypeName, "") + "),");
                strclass2.AppendSpaceLine(3, "parameters" + numPara + "[" + n.ToString() + "].Value = " + key.ColumnName + ";");
                n++;
            }
            strclass.DelLastComma();
            strclass.AppendLine("};");
            strclass.Append(strclass2.Value);
            return strclass.Value;
        }

        #endregion

        #region ���ݲ�(������)
        /// <summary>
        /// �õ�������Ĵ���
        /// </summary>     
        public string GetDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("using System;");
            strclass.AppendLine("using System.Data;");
            strclass.AppendLine("using System.Text;");
            strclass.AppendLine("using System.Collections.Generic;");
            switch (dbobj.DbType)
            {
                case "SQL2005":
                    strclass.AppendLine("using System.Data.SqlClient;");
                    break;
                case "SQL2000":
                    strclass.AppendLine("using System.Data.SqlClient;");
                    break;
                case "Oracle":
                    strclass.AppendLine("using System.Data.OracleClient;");
                    break;
                case "OleDb":
                    strclass.AppendLine("using System.Data.OleDb;");
                    break;
            }
            if (IDALpath != "")
            {
                strclass.AppendLine("using " + IDALpath + ";");
            }
            strclass.AppendLine("using Maticsoft.DBUtility;//�����������");
            strclass.AppendLine("namespace " + DALpath);
            strclass.AppendLine("{");
            strclass.AppendSpaceLine(1, "/// <summary>");
            strclass.AppendSpaceLine(1, "/// ���ݷ�����" + DALNameParent + "��");
            strclass.AppendSpaceLine(1, "/// </summary>");
            strclass.AppendSpace(1, "public class " + DALNameParent);
            if (IClass != "")
            {
                strclass.Append(":" + IClass);
            }
            strclass.AppendLine("");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendSpaceLine(2, "public " + DALNameParent + "()");
            strclass.AppendSpaceLine(2, "{}");
            strclass.AppendSpaceLine(2, "#region  ��Ա����");

            #region  ��������
            if (Maxid)
            {
                strclass.AppendLine(CreatGetMaxID());
            }
            if (Exists)
            {
                strclass.AppendLine(CreatExists());
            }
            if (Add)
            {
                strclass.AppendLine(CreatAdd());
            }
            if (Update)
            {
                strclass.AppendLine(CreatUpdate());
            }
            if (Delete)
            {
                strclass.AppendLine(CreatDelete());
            }
            if (GetModel)
            {
                strclass.AppendLine(CreatGetModel());
            }
            if (List)
            {
                strclass.AppendLine(CreatGetList());
                strclass.AppendLine(CreatGetListByPageProc());
            }
            #endregion

            strclass.AppendSpaceLine(2, "#endregion  ��Ա����");
            strclass.AppendSpaceLine(1, "}");
            strclass.AppendLine("}");
            strclass.AppendLine("");

            return strclass.ToString();
        }

        #endregion

        #region ���ݲ�(ʹ��Parameterʵ��)

        /// <summary>
        /// �õ����ID�ķ�������
        /// </summary>
        public string CreatGetMaxID()
        {
            StringPlus strclass = new StringPlus();
            if (_keysparent.Count > 0)
            {
                string keyname = "";
                foreach (ColumnInfo obj in _keysparent)
                {
                    if (CodeCommon.DbTypeToCS(obj.TypeName) == "int")
                    {
                        keyname = obj.ColumnName;
                        if (obj.IsPK)
                        {
                            strclass.AppendLine("");
                            strclass.AppendSpaceLine(2, "/// <summary>");
                            strclass.AppendSpaceLine(2, "/// �õ����ID");
                            strclass.AppendSpaceLine(2, "/// </summary>");
                            strclass.AppendSpaceLine(2, "public int GetMaxId()");
                            strclass.AppendSpaceLine(2, "{");
                            strclass.AppendSpaceLine(2, "return " + DbHelperName + ".GetMaxID(\"" + keyname + "\", \"" + _tablenameparent + "\"); ");
                            strclass.AppendSpaceLine(2, "}");
                            break;
                        }
                    }
                }
            }
            return strclass.ToString();
        }

        /// <summary>
        /// �õ�Exists�����Ĵ���
        /// </summary>
        public string CreatExists()
        {
            StringPlus strclass = new StringPlus();
            if (_keysparent.Count > 0)
            {
                strclass.AppendSpaceLine(2, "/// <summary>");
                strclass.AppendSpaceLine(2, "/// �Ƿ���ڸü�¼");
                strclass.AppendSpaceLine(2, "/// </summary>");
                strclass.AppendSpaceLine(2, "public bool Exists(" + LTP.CodeHelper.CodeCommon.GetInParameter(KeysParent) + ")");
                strclass.AppendSpaceLine(2, "{");
                strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
                strclass.AppendSpaceLine(3, "strSql.Append(\"select count(1) from " + _tablenameparent + "\");");
                strclass.AppendSpaceLine(3, "strSql.Append(\" where " + GetWhereExpression(KeysParent) + "\");");

                strclass.AppendLine(GetPreParameter(KeysParent));

                strclass.AppendSpaceLine(3, "return " + DbHelperName + ".Exists(strSql.ToString(),parameters);");
                strclass.AppendSpaceLine(2, "}");
            }
            return strclass.Value;
        }

        /// <summary>
        /// �õ�Add()�Ĵ���
        /// </summary>        
        public string CreatAdd()
        {
            //if (ModelSpaceParent == "")
            //{
            //    ModelSpaceParent = "ModelClassName"; ;
            //}
            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            StringPlus strclass3 = new StringPlus();
            StringPlus strclass4 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ����һ������,�����ӱ�����");
            strclass.AppendSpaceLine(2, "/// </summary>");
            string strretu = "void";
            if ((dbobj.DbType == "SQL2000" || dbobj.DbType == "SQL2005" || dbobj.DbType == "SQL2008") && (IsHasIdentity))
            {
                strretu = "int";
            }
            //��������ͷ
            string strFun = CodeCommon.Space(2) + "public " + strretu + " Add(" + ModelSpaceParent + " model)";
            strclass.AppendLine(strFun);
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"insert into " + _tablenameparent + "(\");");
            strclass1.AppendSpace(3, "strSql.Append(\"");
            int n = 0;
            int nkey = 0;
            foreach (ColumnInfo field in FieldlistParent)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                string Length = field.Length;
                if (field.IsIdentity)
                {
                    //nkey = n;
                    continue;
                }
                strclass3.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + columnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, columnType, Length) + "),");
                strclass1.Append(columnName + ",");
                strclass2.Append(preParameter + columnName + ",");
                strclass4.AppendSpaceLine(3, "parameters[" + n + "].Value = model." + columnName + ";");
                n++;
            }
            if ((dbobj.DbType == "SQL2000" || dbobj.DbType == "SQL2005" || dbobj.DbType == "SQL2008") && (IsHasIdentity))
            {
                nkey = n;
                strclass3.AppendSpaceLine(5, "new SqlParameter(\"@ReturnValue\",SqlDbType.Int),");
                strclass4.AppendSpaceLine(3, "parameters[" + nkey.ToString() + "].Direction = ParameterDirection.Output;");
            }

            //ȥ�����Ķ���
            strclass1.DelLastComma();
            strclass2.DelLastComma();
            strclass3.DelLastComma();
            strclass1.AppendLine(")\");");
            strclass.Append(strclass1.ToString());
            strclass.AppendSpaceLine(3, "strSql.Append(\" values (\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\"" + strclass2.ToString() + ")\");");
            if ((dbobj.DbType == "SQL2000" || dbobj.DbType == "SQL2005" || dbobj.DbType == "SQL2008") && (IsHasIdentity))
            {
                strclass.AppendSpaceLine(3, "strSql.Append(\";set @ReturnValue= @@IDENTITY\");");
            }
            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters = {");
            strclass.Append(strclass3.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass4.Value);

            #region tran
            strclass.AppendSpaceLine(3, "List<CommandInfo> sqllist = new List<CommandInfo>();");
            strclass.AppendSpaceLine(3, "CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);");
            strclass.AppendSpaceLine(3, "sqllist.Add(cmd);");

            strclass.AppendSpaceLine(3, "StringBuilder strSql2;");
            strclass.AppendSpaceLine(3, "foreach (" + ModelSpaceSon + " models in model." + ModelNameSon + "s)");
            strclass.AppendSpaceLine(3, "{");

            StringPlus strclass11 = new StringPlus();
            StringPlus strclass21 = new StringPlus();
            StringPlus strclass31 = new StringPlus();
            StringPlus strclass41 = new StringPlus();
            //�µ�����
            strclass.AppendSpaceLine(4, "strSql2=new StringBuilder();");
            strclass.AppendSpaceLine(4, "strSql2.Append(\"insert into " + _tablenameson + "(\");");
            strclass11.AppendSpace(4, "strSql2.Append(\"");
            int ns = 0;
            foreach (ColumnInfo field in FieldlistSon)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                string Length = field.Length;
                if (field.IsIdentity)
                {
                    continue;
                }
                strclass31.AppendSpaceLine(6, "new " + DbParaHead + "Parameter(\"" + preParameter + columnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, columnType, Length) + "),");
                strclass11.Append(columnName + ",");
                strclass21.Append(preParameter + columnName + ",");
                strclass41.AppendSpaceLine(4, "parameters2[" + ns + "].Value = models." + columnName + ";");
                ns++;
            }
            strclass11.DelLastComma();
            strclass21.DelLastComma();
            strclass31.DelLastComma();
            strclass11.AppendLine(")\");");
            strclass.Append(strclass11.ToString());
            strclass.AppendSpaceLine(4, "strSql2.Append(\" values (\");");
            strclass.AppendSpaceLine(4, "strSql2.Append(\"" + strclass21.ToString() + ")\");");
            //if (IsHasIdentity)
            //{
            //    strclass.AppendSpaceLine(4, "strSql2.Append(\";select @@IDENTITY\");");
            //}
            strclass.AppendSpaceLine(4, "" + DbParaHead + "Parameter[] parameters2 = {");
            strclass.Append(strclass31.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass41.Value);
            //end�µ�����

            strclass.AppendSpaceLine(4, "cmd = new CommandInfo(strSql2.ToString(), parameters2);");
            strclass.AppendSpaceLine(4, "sqllist.Add(cmd);");
            strclass.AppendSpaceLine(3, "}");
            #endregion


            //���¶��巽��ͷ
            if ((dbobj.DbType == "SQL2000" || dbobj.DbType == "SQL2005" || dbobj.DbType == "SQL2008") && (IsHasIdentity))
            {
                strclass.AppendSpaceLine(3, DbHelperName + ".ExecuteSqlTranWithIndentity(sqllist);");
                strclass.AppendSpaceLine(3, "return (" + _keyType + ")parameters[" + nkey + "].Value;");
            }
            else
            {
                strclass.AppendSpaceLine(3, "" + DbHelperName + ".ExecuteSqlTran(sqllist);");
            }
            strclass.AppendSpace(2, "}");
            return strclass.ToString();
        }

        /// <summary>
        /// �õ�Update�����Ĵ���
        /// </summary>     
        public string CreatUpdate()
        {
            //if (ModelSpaceParent == "")
            //{
            //    ModelSpaceParent = "ModelClassName"; ;
            //}
            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();

            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ����һ������");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Update(" + ModelSpaceParent + " model)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"update " + _tablenameparent + " set \");");
            int n = 0;
            foreach (ColumnInfo field in FieldlistParent)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPK;

                strclass1.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + columnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, columnType, Length) + "),");
                strclass2.AppendSpaceLine(3, "parameters[" + n + "].Value = model." + columnName + ";");
                n++;
                if (field.IsIdentity || field.IsPK || (KeysParent.Contains(field)))
                {
                    continue;
                }
                strclass.AppendSpaceLine(3, "strSql.Append(\"" + columnName + "=" + preParameter + columnName + ",\");");
            }


            //ȥ�����Ķ���			
            strclass.DelLastComma();
            strclass.AppendLine("\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + GetWhereExpression(KeysParent) + "\");");

            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters = {");
            strclass1.DelLastComma();
            strclass.Append(strclass1.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass2.Value);
            strclass.AppendSpaceLine(3, "" + DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(2, "}");
            return strclass.ToString();
        }
        /// <summary>
        /// �õ�Delete�Ĵ���
        /// </summary>
        public string CreatDelete()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ɾ��һ�����ݣ����ӱ������������");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public void Delete(" + LTP.CodeHelper.CodeCommon.GetInParameter(KeysParent) + ")");
            strclass.AppendSpaceLine(2, "{");

            strclass.AppendSpaceLine(3, "List<CommandInfo> sqllist = new List<CommandInfo>();");

            //��
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            if (dbobj.DbType != "OleDb")
            {
                strclass.AppendSpaceLine(3, "strSql.Append(\"delete " + _tablenameparent + " \");");
            }
            else
            {
                strclass.AppendSpaceLine(3, "strSql.Append(\"delete from " + _tablenameparent + " \");");
            }
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + GetWhereExpression(KeysParent) + "\");");
            strclass.AppendLine(GetPreParameter(KeysParent));
            strclass.AppendSpaceLine(3, "CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);");
            strclass.AppendSpaceLine(3, "sqllist.Add(cmd);");

            //��
            strclass.AppendSpaceLine(3, "StringBuilder strSql2=new StringBuilder();");
            if (dbobj.DbType != "OleDb")
            {
                strclass.AppendSpaceLine(3, "strSql2.Append(\"delete " + _tablenameson + " \");");
            }
            else
            {
                strclass.AppendSpaceLine(3, "strSql2.Append(\"delete from " + _tablenameson + " \");");
            }
            strclass.AppendSpaceLine(3, "strSql2.Append(\" where " + GetWhereExpression(KeysSon) + "\");");
            strclass.AppendLine(GetPreParameter(KeysSon, "2"));
            strclass.AppendSpaceLine(3, "cmd = new CommandInfo(strSql2.ToString(), parameters2);");
            strclass.AppendSpaceLine(3, "sqllist.Add(cmd);");

            strclass.AppendSpaceLine(3, "" + DbHelperName + ".ExecuteSqlTran(sqllist);");

            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }

        /// <summary>
        /// �õ�GetModel()�Ĵ���
        /// </summary>
        public string CreatGetModel()
        {
            //if (ModelSpaceParent == "")
            //{
            //    ModelSpaceParent = "ModelClassName"; ;
            //}
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// �õ�һ������ʵ��");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public " + ModelSpaceParent + " GetModel(" + LTP.CodeHelper.CodeCommon.GetInParameter(KeysParent) + ")");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql.Append(\"select " + Fieldstrlist + " from " + _tablenameparent + " \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" where " + GetWhereExpression(KeysParent) + "\");");

            strclass.AppendLine(GetPreParameter(KeysParent));

            strclass.AppendSpaceLine(3, "" + ModelSpaceParent + " model=new " + ModelSpaceParent + "();");
            strclass.AppendSpaceLine(3, "DataSet ds=" + DbHelperName + ".Query(strSql.ToString(),parameters);");
            strclass.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
            strclass.AppendSpaceLine(3, "{");

            //��������
            strclass.AppendSpaceLine(4, "#region  ������Ϣ");
            foreach (ColumnInfo field in FieldlistParent)
            {
                #region �ֶ�����
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=int.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "long":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=long.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=decimal.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=float.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=DateTime.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(4, "model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "if((ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString().ToLower()==\"true\"))");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=true;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(5, "else");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=false;");
                            strclass.AppendSpaceLine(5, "}");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=(byte[])ds.Tables[0].Rows[0][\"" + columnName + "\"];");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    case "Guid":
                        {
                            strclass.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(4, "{");
                            strclass.AppendSpaceLine(5, "model." + columnName + "=new Guid(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(4, "}");
                        }
                        break;
                    default:
                        strclass.AppendSpaceLine(4, "//model." + columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
                        break;
                }
                #endregion

            }
            strclass.AppendSpaceLine(4, "#endregion  ������Ϣend");
            strclass.AppendLine();

            #region �ӱ�����

            strclass.AppendSpaceLine(4, "#region  �ӱ���Ϣ");
            strclass.AppendSpaceLine(4, "StringBuilder strSql2=new StringBuilder();");
            strclass.AppendSpaceLine(4, "strSql2.Append(\"select " + FieldstrlistSon + " from " + _tablenameson + " \");");
            strclass.AppendSpaceLine(4, "strSql2.Append(\" where " + GetWhereExpression(KeysSon) + "\");");
            strclass.AppendLine(GetPreParameter(KeysParent,"2"));
            strclass.AppendSpaceLine(4, "DataSet ds2=" + DbHelperName + ".Query(strSql2.ToString(),parameters2);");
            strclass.AppendSpaceLine(4, "if(ds2.Tables[0].Rows.Count>0)");
            strclass.AppendSpaceLine(4, "{");


            strclass.AppendSpaceLine(5, "#region  �ӱ��ֶ���Ϣ");
            strclass.AppendSpaceLine(5, "int i = ds2.Tables[0].Rows.Count;");
            strclass.AppendSpaceLine(5, "List<" + ModelSpaceSon + "> models = new List<" + ModelSpaceSon + ">();");
            strclass.AppendSpaceLine(5, ModelSpaceSon+" modelt;");
            strclass.AppendSpaceLine(5, "for (int n = 0; n < i; n++)");
            strclass.AppendSpaceLine(5, "{");
            strclass.AppendSpaceLine(6, "modelt = new " + ModelSpaceSon + "();");
            foreach (ColumnInfo field in FieldlistSon)
            {
                #region �ֶ�����
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(6, "if(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "modelt." + columnName + "=int.Parse(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(6, "}");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(6, "if(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "modelt." + columnName + "=decimal.Parse(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(6, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(6, "if(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "modelt." + columnName + "=DateTime.Parse(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(6, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(6, "modelt." + columnName + "=ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(6, "if(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "if((ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()==\"1\")||(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString().ToLower()==\"true\"))");
                            strclass.AppendSpaceLine(7, "{");
                            strclass.AppendSpaceLine(8, "modelt." + columnName + "=true;");
                            strclass.AppendSpaceLine(7, "}");
                            strclass.AppendSpaceLine(7, "else");
                            strclass.AppendSpaceLine(7, "{");
                            strclass.AppendSpaceLine(8, "modelt." + columnName + "=false;");
                            strclass.AppendSpaceLine(7, "}");
                            strclass.AppendSpaceLine(6, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            strclass.AppendSpaceLine(6, "if(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "modelt." + columnName + "=(byte[])ds2.Tables[0].Rows[n][\"" + columnName + "\"];");
                            strclass.AppendSpaceLine(6, "}");
                        }
                        break;
                    case "Guid":
                        {
                            strclass.AppendSpaceLine(6, "if(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "modelt." + columnName + "=new Guid(ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(6, "}");
                        }
                        break;
                    default:
                        strclass.AppendSpaceLine(6, "modelt." + columnName + "=ds2.Tables[0].Rows[n][\"" + columnName + "\"].ToString();");
                        break;
                }
                #endregion
            }
            strclass.AppendSpaceLine(6, "models.Add(modelt);");
            strclass.AppendSpaceLine(5, "}");
            strclass.AppendSpaceLine(5, "model." + ModelNameSon + "s = models;");
            strclass.AppendSpaceLine(5, "#endregion  �ӱ��ֶ���Ϣend");
                      
            strclass.AppendSpaceLine(4, "}");

            strclass.AppendSpaceLine(4, "#endregion  �ӱ���Ϣend");
            #endregion

            strclass.AppendLine();
            strclass.AppendSpaceLine(4, "return model;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "else");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "return null;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(2, "}");
            return strclass.ToString();
        }

        /// <summary>
        /// �õ�GetList()�Ĵ���
        /// </summary>
        public string CreatGetList()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ��������б�");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public DataSet GetList(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            strclass.AppendSpace(3, "strSql.Append(\"select ");
            strclass.AppendLine(Fieldstrlist + " \");");
            strclass.AppendSpaceLine(3, "strSql.Append(\" FROM " + TableNameParent + " \");");
            strclass.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return " + DbHelperName + ".Query(strSql.ToString());");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;
        }

        /// <summary>
        /// �õ�GetList()�Ĵ���
        /// </summary>
        public string CreatGetListByPageProc()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2, "/*");
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ��ҳ��ȡ�����б�");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public DataSet GetList(int PageSize,int PageIndex,string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters = {");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "tblName\", " + DbParaDbType + ".VarChar, 255),");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "fldName\", " + DbParaDbType + ".VarChar, 255),");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "PageSize\", " + DbParaDbType + "." + CodeCommon.CSToProcType(dbobj.DbType, "int") + "),");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "PageIndex\", " + DbParaDbType + "." + CodeCommon.CSToProcType(dbobj.DbType, "int") + "),");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "IsReCount\", " + DbParaDbType + "." + CodeCommon.CSToProcType(dbobj.DbType, "bit") + "),");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "OrderType\", " + DbParaDbType + "." + CodeCommon.CSToProcType(dbobj.DbType, "bit") + "),");
            strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "strWhere\", " + DbParaDbType + ".VarChar,1000),");
            strclass.AppendSpaceLine(5, "};");
            strclass.AppendSpaceLine(3, "parameters[0].Value = \"" + this.TableNameParent + "\";");
            strclass.AppendSpaceLine(3, "parameters[1].Value = \"" + this._keysparent + "\";");
            strclass.AppendSpaceLine(3, "parameters[2].Value = PageSize;");
            strclass.AppendSpaceLine(3, "parameters[3].Value = PageIndex;");
            strclass.AppendSpaceLine(3, "parameters[4].Value = 0;");
            strclass.AppendSpaceLine(3, "parameters[5].Value = 0;");
            strclass.AppendSpaceLine(3, "parameters[6].Value = strWhere;	");
            strclass.AppendSpaceLine(3, "return " + DbHelperName + ".RunProcedure(\"UP_GetRecordByPage\",parameters,\"ds\");");
            strclass.AppendSpaceLine(2, "}*/");
            return strclass.Value;
        }

        #endregion


    }
}
