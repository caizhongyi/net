using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using LTP.Utility;
using LTP.IDBO;
using LTP.CodeHelper;
namespace LTP.BuilderBLLComm
{
    /// <summary>
    /// ҵ���������
    /// </summary>
    public class BuilderBLL : IBuilder.IBuilderBLL
    {
        #region ˽�б���
        protected string _key = "ID";//Ĭ�ϵ�һ�������ֶ�		
        protected string _keyType = "int";//Ĭ�ϵ�һ����������
        NameRule namerule = new NameRule();
        #endregion

        #region ��������
        private List<ColumnInfo> _fieldlist;
        private List<ColumnInfo> _keys;
        private string _namespace; //���������ռ���
        private string _modelspace;
        private string _modelname;//model���� 
        private string _bllname;//bll����    
        private string _dalname;//dal����    
        private string _modelpath;
        private string _bllpath;
        private string _factorypath;
        private string _idalpath;
        private string _iclass;
        private string _dalpath;
        private string _dalspace;
        private bool isHasIdentity;
        private string dbType;

        /// <summary>
        /// ѡ����ֶμ���
        /// </summary>
        public List<ColumnInfo> Fieldlist
        {
            set { _fieldlist = value; }
            get { return _fieldlist; }
        }   
        /// <summary>
        /// �����������ֶ��б� 
        /// </summary>
        public List<ColumnInfo> Keys
        {
            set { _keys = value; }
            get { return _keys; }
        }
        /// <summary>
        /// ���������ռ���
        /// </summary>
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }

        /*============================*/

        /// <summary>
        /// ʵ����������ռ�
        /// </summary>
        public string Modelpath
        {
            set { _modelpath = value; }
            get { return _modelpath; }
        }
        /// <summary>
        /// Model����
        /// </summary>
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }

        /// <summary>
        /// ʵ��������������ռ� + ������������ Modelpath+ModelName
        /// </summary>
        public string ModelSpace
        {
            get { return Modelpath + "." +ModelName; }
        }
                
        /*============================*/

        /// <summary>
        /// ҵ���߼���������ռ�
        /// </summary>
        public string BLLpath
        {
            set { _bllpath = value; }
            get { return _bllpath; }
        }
        /// <summary>
        /// BLL����
        /// </summary>
        public string BLLName
        {
            set { _bllname = value; }
            get { return _bllname; }
        }
        
        /*============================*/

        /// <summary>
        /// ���ݲ�������ռ�
        /// </summary>
        public string DALpath
        {
            set { _dalpath = value; }
            get { return _dalpath; }
        }
        /// <summary>
        /// DAL����
        /// </summary>
        public string DALName
        {
            set { _dalname = value; }
            get { return _dalname; }
        } 
        
        /// <summary>
        /// ���ݲ�������ռ�+ ������������ DALpath + DALName
        /// </summary>
        public string DALSpace
        {
            get { return DALpath + "." + DALName; }
        }

        /*============================*/
        /// <summary>
        /// ������������ռ�
        /// </summary>
        public string Factorypath
        {
            set { _factorypath = value; }
            get { return _factorypath; }
        }
        /// <summary>
        /// �ӿڵ������ռ�
        /// </summary>
        public string IDALpath
        {
            set { _idalpath = value; }
            get { return _idalpath; }
        }
        /// <summary>
        /// �ӿ���
        /// </summary>
        public string IClass
        {
            set { _iclass = value; }
            get { return _iclass; }
        }

        /*============================*/
               
        /// <summary>
        /// �Ƿ����Զ�������ʶ��
        /// </summary>
        public bool IsHasIdentity
        {
            set { isHasIdentity = value; }
            get { return isHasIdentity; }
        }
        public string DbType
        {
            set { dbType = value; }
            get { return dbType; }
        }
        /// <summary>
        /// ������ʶ�ֶ�
        /// </summary>
        public string Key
        {
            get
            {
                foreach (ColumnInfo key in _keys)
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
                return _key;
            }
        }
        private string KeysNullTip
        {
            get
            {
                if (_keys.Count == 0)
                {
                    return "//�ñ���������Ϣ�����Զ�������/�����ֶ�";
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion		

        #region  ���캯��
        public BuilderBLL()
        { 
        }
        public BuilderBLL(List<ColumnInfo> keys, string modelspace)
        {
            _modelspace = modelspace;
            Keys = keys;
            foreach (ColumnInfo key in _keys)
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
                        
        #region ҵ��㷽��
        /// <summary>
        /// �õ�������Ĵ���
        /// </summary>      
        public string GetBLLCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel,bool GetModelByCache, bool List)
        {            
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("using System;");
            strclass.AppendLine("using System.Data;");
            strclass.AppendLine("using System.Collections.Generic;");
            if (GetModelByCache)
            {
                strclass.AppendLine("using LTP.Common;");
            }
            strclass.AppendLine("using " + Modelpath + ";");
            if ((Factorypath != "")&&(Factorypath !=null))
            {
                strclass.AppendLine("using " + Factorypath + ";");
            }
            if ((IDALpath != "")&&(IDALpath !=null))
            {
                strclass.AppendLine("using " + IDALpath + ";");
            }
            strclass.AppendLine("namespace " + BLLpath );
            strclass.AppendLine("{" );
            strclass.AppendSpaceLine(1, "/// <summary>" );
            strclass.AppendSpaceLine(1, "/// ҵ���߼���" + BLLName + " ��ժҪ˵����");
            strclass.AppendSpaceLine(1, "/// </summary>" );
            strclass.AppendSpaceLine(1, "public class " + BLLName);
            strclass.AppendSpaceLine(1, "{" );
            
            if ((IClass != "") && (IClass != null))
            {
                strclass.AppendSpaceLine(2, "private readonly " + IClass + " dal=" + "DataAccess.Create" + DALName + "();");
            }
            else
            {
                strclass.AppendSpaceLine(2, "private readonly " + DALSpace + " dal=" + "new " + DALSpace + "();");
            }
            strclass.AppendSpaceLine(2, "public " + BLLName + "()");
            strclass.AppendSpaceLine(2, "{}" );
            strclass.AppendSpaceLine(2, "#region  ��Ա����" );

            #region  ��������
            if (Maxid)
            {
                if (Keys.Count > 0)
                {
                    foreach (ColumnInfo obj in Keys)
                    {
                        if (CodeCommon.DbTypeToCS(obj.TypeName) == "int")
                        {
                            if (obj.IsPK)
                            {
                                strclass.AppendLine(CreatBLLGetMaxID() );
                                break;
                            }
                        }
                    }
                }
            }
            if (Exists)
            {
                strclass.AppendLine(CreatBLLExists() );
            }
            if (Add)
            {
                strclass.AppendLine(CreatBLLADD() );
            }
            if (Update)
            {
                strclass.AppendLine(CreatBLLUpdate() );
            }
            if (Delete)
            {
                strclass.AppendLine(CreatBLLDelete() );
            }
            if (GetModel)
            {
                strclass.AppendLine(CreatBLLGetModel() );                         
            }
            if (GetModelByCache)
            {
                strclass.AppendLine(CreatBLLGetModelByCache(ModelName) );
            }
            if (List)
            {
                strclass.AppendLine(CreatBLLGetList() );
                strclass.AppendLine(CreatBLLGetAllList() );
                strclass.AppendLine(CreatBLLGetListByPage() );
            }

            #endregion
            strclass.AppendSpaceLine(2, "#endregion  ��Ա����" );
            strclass.AppendSpaceLine(1, "}" );
            strclass.AppendLine("}" );
            strclass.AppendLine("");

            return strclass.ToString();
        }

        #endregion

        #region ���巽������

        public string CreatBLLGetMaxID()
        {
            StringPlus strclass = new StringPlus();
            if (_keys.Count > 0)
            {
                string keyname = "";
                foreach (ColumnInfo obj in _keys)
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
                            strclass.AppendSpaceLine(3, "return dal.GetMaxId();");
                            strclass.AppendSpaceLine(2, "}");
                            break;
                        }
                    }
                }
            }

            
            return strclass.ToString();
        }
        public string CreatBLLExists()
        {
            StringPlus strclass = new StringPlus();
            if (_keys.Count > 0)
            {
                strclass.AppendSpaceLine(2, "/// <summary>");
                strclass.AppendSpaceLine(2, "/// �Ƿ���ڸü�¼");
                strclass.AppendSpaceLine(2, "/// </summary>");
                strclass.AppendSpaceLine(2, "public bool Exists(" + LTP.CodeHelper.CodeCommon.GetInParameter(Keys) + ")");
                strclass.AppendSpaceLine(2, "{");
                strclass.AppendSpaceLine(3, "return dal.Exists(" + LTP.CodeHelper.CodeCommon.GetFieldstrlist(Keys) + ");");
                strclass.AppendSpaceLine(2, "}");
            }            
            return strclass.ToString();
        }
        public string CreatBLLADD()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// ����һ������" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            string strretu = "void";
            if ((DbType == "SQL2000" || DbType == "SQL2005") && (IsHasIdentity))
            {
                strretu = "int ";
            }            
            strclass.AppendSpaceLine(2,  "public " + strretu + " Add(" + ModelSpace + " model)" );
            strclass.AppendSpaceLine(2,  "{" );
            if (strretu == "void")
            {
                strclass.AppendSpaceLine(3,  "dal.Add(model);" );
            }
            else
            {
                strclass.AppendSpaceLine(3,  "return dal.Add(model);" );
            }
            strclass.AppendSpaceLine(2,  "}");
            return strclass.ToString();
        }
        public string CreatBLLUpdate()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// ����һ������" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            strclass.AppendSpaceLine(2,  "public void Update(" + ModelSpace + " model)" );
            strclass.AppendSpaceLine(2,  "{" );
            strclass.AppendSpaceLine(3,  "dal.Update(model);" );
            strclass.AppendSpaceLine(2,  "}");
            return strclass.ToString();
        }
        public string CreatBLLDelete()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// ɾ��һ������" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            strclass.AppendSpaceLine(2,  "public void Delete(" + LTP.CodeHelper.CodeCommon.GetInParameter(Keys) + ")" );
            strclass.AppendSpaceLine(2,  "{" );
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3,  "dal.Delete(" + LTP.CodeHelper.CodeCommon.GetFieldstrlist(Keys) + ");" );
            strclass.AppendSpaceLine(2,  "}");
            return strclass.ToString();
        }
        public string CreatBLLGetModel()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// �õ�һ������ʵ��" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            strclass.AppendSpaceLine(2,  "public " + ModelSpace + " GetModel(" + LTP.CodeHelper.CodeCommon.GetInParameter(Keys) + ")" );
            strclass.AppendSpaceLine(2,  "{" );
            strclass.AppendSpaceLine(3, KeysNullTip);
            strclass.AppendSpaceLine(3,  "return dal.GetModel(" + LTP.CodeHelper.CodeCommon.GetFieldstrlist(Keys) + ");" );
            strclass.AppendSpaceLine(2,  "}");
            return strclass.ToString();

        }
        public string CreatBLLGetModelByCache(string ModelName)
        {
            StringPlus strclass = new StringPlus();            
            strclass.AppendSpaceLine(2, "/// <summary>" );
            strclass.AppendSpaceLine(2, "/// �õ�һ������ʵ�壬�ӻ����С�");
            strclass.AppendSpaceLine(2, "/// </summary>" );
            strclass.AppendSpaceLine(2, "public " + ModelSpace + " GetModelByCache(" + LTP.CodeHelper.CodeCommon.GetInParameter(Keys) + ")");
            strclass.AppendSpaceLine(2, "{" );
            strclass.AppendSpaceLine(3, KeysNullTip);
            string para = "";
            if (Keys.Count > 0)
            {
                para = "+ " + LTP.CodeHelper.CodeCommon.GetFieldstrlistAdd(Keys);
            }
            strclass.AppendSpaceLine(3, "string CacheKey = \"" + ModelName + "Model-\" " + para + ";");
            strclass.AppendSpaceLine(3, "object objModel = LTP.Common.DataCache.GetCache(CacheKey);");
            strclass.AppendSpaceLine(3, "if (objModel == null)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "try");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "objModel = dal.GetModel(" + LTP.CodeHelper.CodeCommon.GetFieldstrlist(Keys) + ");");
            strclass.AppendSpaceLine(5, "if (objModel != null)");
            strclass.AppendSpaceLine(5, "{");
            strclass.AppendSpaceLine(6, "int ModelCache = LTP.Common.ConfigHelper.GetConfigInt(\"ModelCache\");");
            strclass.AppendSpaceLine(6, "LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);");
            strclass.AppendSpaceLine(5, "}");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(4, "catch{}");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return (" + ModelSpace + ")objModel;");
            strclass.AppendSpaceLine(2, "}");
            return strclass.Value;

        }
        public string CreatBLLGetList()
        {
            StringPlus strclass = new StringPlus();
            //����DataSet
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// ��������б�" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            strclass.AppendSpaceLine(2,  "public DataSet GetList(string strWhere)" );
            strclass.AppendSpaceLine(2,  "{" );
            strclass.AppendSpaceLine(3,  "return dal.GetList(strWhere);" );
            strclass.AppendSpaceLine(2,  "}");

            if ((DbType == "SQL2000") ||
                (DbType == "SQL2005") ||
                (DbType == "SQL2008"))
            {
                //����DataSet
                strclass.AppendSpaceLine(2, "/// <summary>");
                strclass.AppendSpaceLine(2, "/// ���ǰ��������");
                strclass.AppendSpaceLine(2, "/// </summary>");
                strclass.AppendSpaceLine(2, "public DataSet GetList(int Top,string strWhere,string filedOrder)");
                strclass.AppendSpaceLine(2, "{");
                strclass.AppendSpaceLine(3, "return dal.GetList(Top,strWhere,filedOrder);");
                strclass.AppendSpaceLine(2, "}");
            }
            //����List<>
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ��������б�");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public List<" + ModelSpace + "> GetModelList(string strWhere)");
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "DataSet ds = dal.GetList(strWhere);");
            strclass.AppendSpaceLine(3, "return DataTableToList(ds.Tables[0]);");
            strclass.AppendSpaceLine(2, "}");


            //����List<>
            strclass.AppendSpaceLine(2, "/// <summary>");
            strclass.AppendSpaceLine(2, "/// ��������б�");
            strclass.AppendSpaceLine(2, "/// </summary>");
            strclass.AppendSpaceLine(2, "public List<" + ModelSpace + "> DataTableToList(DataTable dt)");
            strclass.AppendSpaceLine(2, "{");
            //strclass.AppendSpaceLine(3, "DataSet ds = dal.GetList(strWhere);");
            strclass.AppendSpaceLine(3, "List<" + ModelSpace + "> modelList = new List<" + ModelSpace + ">();");
            strclass.AppendSpaceLine(3, "int rowsCount = dt.Rows.Count;");
            strclass.AppendSpaceLine(3, "if (rowsCount > 0)");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, ModelSpace+" model;");
            strclass.AppendSpaceLine(4, "for (int n = 0; n < rowsCount; n++)");
            strclass.AppendSpaceLine(4, "{");
            strclass.AppendSpaceLine(5, "model = new " + ModelSpace + "();");

            #region �ֶθ�ֵ
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                switch (CodeCommon.DbTypeToCS(columnType))
                {
                    case "int":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=int.Parse(dt.Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    case "decimal":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=decimal.Parse(dt.Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    case "float":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=float.Parse(dt.Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    case "DateTime":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=DateTime.Parse(dt.Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    case "string":
                        {
                            strclass.AppendSpaceLine(5, "model." + columnName + "=dt.Rows[n][\"" + columnName + "\"].ToString();");
                        }
                        break;
                    case "bool":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "if((dt.Rows[n][\"" + columnName + "\"].ToString()==\"1\")||(dt.Rows[n][\"" + columnName + "\"].ToString().ToLower()==\"true\"))");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=true;");
                            strclass.AppendSpaceLine(6, "}");
                            strclass.AppendSpaceLine(6, "else");
                            strclass.AppendSpaceLine(6, "{");
                            strclass.AppendSpaceLine(7, "model." + columnName + "=false;");
                            strclass.AppendSpaceLine(6, "}");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    case "byte[]":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=(byte[])dt.Rows[n][\"" + columnName + "\"];");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    case "Guid":
                        {
                            strclass.AppendSpaceLine(5, "if(dt.Rows[n][\"" + columnName + "\"].ToString()!=\"\")");
                            strclass.AppendSpaceLine(5, "{");
                            strclass.AppendSpaceLine(6, "model." + columnName + "=new Guid(dt.Rows[n][\"" + columnName + "\"].ToString());");
                            strclass.AppendSpaceLine(5, "}");
                        }
                        break;
                    default:
                        strclass.AppendSpaceLine(5, "//model." + columnName + "=dt.Rows[n][\"" + columnName + "\"].ToString();");
                        break;
                }
            }
            #endregion

            strclass.AppendSpaceLine(5, "modelList.Add(model);");
            strclass.AppendSpaceLine(4, "}");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendSpaceLine(3, "return modelList;");
            strclass.AppendSpaceLine(2, "}");



            return strclass.ToString();

        }
        public string CreatBLLGetAllList()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// ��������б�" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            strclass.AppendSpaceLine(2,  "public DataSet GetAllList()" );
            strclass.AppendSpaceLine(2,  "{" );
            strclass.AppendSpaceLine(3,  "return GetList(\"\");" );
            strclass.AppendSpaceLine(2,  "}");
            return strclass.ToString();
        }
        public string CreatBLLGetListByPage()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(2,  "/// <summary>" );
            strclass.AppendSpaceLine(2,  "/// ��������б�" );
            strclass.AppendSpaceLine(2,  "/// </summary>" );
            strclass.AppendSpaceLine(2,  "//public DataSet GetList(int PageSize,int PageIndex,string strWhere)" );
            strclass.AppendSpaceLine(2,  "//{" );
            strclass.AppendSpaceLine(3,  "//return dal.GetList(PageSize,PageIndex,strWhere);" );
            strclass.AppendSpaceLine(2,  "//}");
            return strclass.ToString();
        }

        #endregion


    }
}
