using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LTP.IDBO;
using LTP.Utility;
using LTP.CodeHelper;

namespace LTP.BuilderWeb
{
    /// <summary>
    /// Web��������
    /// </summary>
    public class BuilderWeb : IBuilder.IBuilderWeb
    {
        #region ˽���ֶ�
        protected string _key = "ID";//Ĭ�ϵ�һ�������ֶ�		
        protected string _keyType = "int";//Ĭ�ϵ�һ����������        
        protected string _namespace = "Maticsoft"; //���������ռ���
        private string _folder="";//�����ļ���
        protected string _modelname; //model����           
        protected string _bllname; //model����
        protected List<ColumnInfo> _fieldlist;
        protected List<ColumnInfo> _keys;
        #endregion

        #region ��������
        /// <summary>
        /// ���������ռ��� 
        /// </summary>        
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// �����ļ�����
        /// </summary>
        public string Folder
        {
            set { _folder = value; }
            get { return _folder; }
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
        /// BLL����
        /// </summary>
        public string BLLName
        {
            set { _bllname = value; }
            get { return _bllname; }
        }
        
        /// <summary>
        /// ʵ��������������ռ�+����
        /// </summary>
        public string ModelSpace
        {           
            get
            {
                string _modelspace = _namespace + "." + "Model";
                if (_folder.Trim() != "")
                {
                    _modelspace += "." + _folder;
                }
                _modelspace += "." + ModelName;
                return _modelspace;
            }
        }        
        
        /// <summary>
        /// ҵ���߼���Ĳ��������ƶ���
        /// </summary>
        private string BLLSpace
        {
            get
            {
                string _bllspace = _namespace + "." + "BLL";
                if (_folder.Trim() != "")
                {
                    _bllspace += "." + _folder;
                }
                _bllspace += "." + BLLName;
                return _bllspace;
            }
        }
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
        /// ������ʶ�ֶ�
        /// </summary>
        protected string Key
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
        #endregion

        public BuilderWeb()
        {
        }

        #region Aspxҳ��html

        /// <summary>
        /// �õ���ʾ�����Ӵ����html����
        /// </summary>      
        public string GetAddAspx()
        {            
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            strclass.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if (IsIdentity)
                {
                    continue;
                }
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                strclass.AppendSpaceLine(1, "<tr>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                strclass.AppendSpaceLine(2, deText);
                strclass.AppendSpaceLine(1, "��</td>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                switch (columnType.Trim().ToLower())
                {
                    case "datetime":
                    case "smalldatetime":
                        strclass.AppendSpaceLine(2, "<INPUT onselectstart=\"return false;\" onkeypress=\"return false\" id=\"txt" + columnName + "\" onfocus=\"setday(this)\"");
                        strclass.AppendSpaceLine(2, " readOnly type=\"text\" size=\"10\" name=\"Text1\" runat=\"server\">");
                        break;
                    case "bit":
                        strclass.AppendSpaceLine(2, "<asp:CheckBox ID=\"chk" + columnName + "\" Text=\"" + deText + "\" runat=\"server\" Checked=\"False\" />");
                        break;
                    default:
                        strclass.AppendSpaceLine(2, "<asp:TextBox id=\"txt" + columnName + "\" runat=\"server\" Width=\"200px\"></asp:TextBox>");
                        break;
                }
                strclass.AppendSpaceLine(1, "</td></tr>");
            }

            //��ť
            strclass.AppendSpaceLine(1, "<tr>");
            strclass.AppendSpaceLine(1, "<td height=\"25\" colspan=\"2\"><div align=\"center\">");
            strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnAdd\" runat=\"server\" Text=\"�� �ύ ��\" OnClick=\"btnAdd_Click\" ></asp:Button>");
            //strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnCancel\" runat=\"server\" Text=\"�� ���� ��\" OnClick=\"btnCancel_Click\" ></asp:Button>");
            strclass.AppendSpaceLine(1, "</div></td></tr>");
            strclass.AppendLine("</table>");
            return strclass.ToString();

        }

        /// <summary>
        /// �õ���ʾ�����Ӵ����html����
        /// </summary>      
        public string GetUpdateAspx()
        {            
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("");
            strclass.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                if ((ispk) || (IsIdentity))
                {
                    strclass.AppendSpaceLine(1, "<tr>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                    strclass.AppendSpaceLine(2, deText);
                    strclass.AppendSpaceLine(1, "��</td>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                    strclass.AppendSpaceLine(2, "<asp:label id=\"lbl" + columnName + "\" runat=\"server\"></asp:label>");
                    strclass.AppendSpaceLine(1, "</td></tr>");
                }
                else
                {
                    //
                    strclass.AppendSpaceLine(1, "<tr>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                    strclass.AppendSpaceLine(2, deText);
                    strclass.AppendSpaceLine(1, "��</td>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                    switch (columnType.Trim())
                    {
                        case "datetime":
                        case "smalldatetime":
                            strclass.AppendSpaceLine(2, "<INPUT onselectstart=\"return false;\" onkeypress=\"return false\" id=\"txt" + columnName + "\" onfocus=\"setday(this)\"");
                            strclass.AppendSpaceLine(2, " readOnly type=\"text\" size=\"10\" name=\"Text1\" runat=\"server\">");
                            break;
                        case "bit":
                            strclass.AppendSpaceLine(2, "<asp:CheckBox ID=\"chk" + columnName + "\" Text=\"" + deText + "\" runat=\"server\" Checked=\"False\" />");
                            break;
                        default:
                            strclass.AppendSpaceLine(2, "<asp:TextBox id=\"txt" + columnName + "\" runat=\"server\" Width=\"200px\"></asp:TextBox>");
                            break;
                    }
                    strclass.AppendSpaceLine(1, "</td></tr>");
                }
            }
            
            //��ť
            strclass.AppendSpaceLine(1, "<tr>");
            strclass.AppendSpaceLine(1, "<td height=\"25\" colspan=\"2\"><div align=\"center\">");
            strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnUpdate\" runat=\"server\" Text=\"�� �ύ ��\" OnClick=\"btnUpdate_Click\" ></asp:Button>");
            //strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnCancel\" runat=\"server\" Text=\"�� ȡ�� ��\" OnClick=\"btnCancel_Click\" ></asp:Button>");
            strclass.AppendSpaceLine(1, "</div></td></tr>");
            strclass.AppendLine("</table>");
            return strclass.Value;

        }

        /// <summary>
        /// �õ���ʾ����ʾ�����html����
        /// </summary>     
        public string GetShowAspx()
        {            
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            strclass.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">" );
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;               
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                strclass.AppendSpaceLine(1,"<tr>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">" );
                strclass.AppendSpaceLine(2, deText );
                strclass.AppendSpaceLine(1, "��</td>" );
                strclass.AppendSpaceLine(1,"<td height=\"25\" width=\"*\" align=\"left\">" );
                switch (columnType.Trim())
                {
                    case "bit":
                        strclass.AppendSpaceLine(2, "<asp:CheckBox ID=\"chk" + columnName + "\" Text=\"" + deText + "\" runat=\"server\" Checked=\"False\" />" );
                        break;
                    default:
                        strclass.AppendSpaceLine(2, "<asp:Label id=\"lbl" + columnName + "\" runat=\"server\"></asp:Label>");
                        break;
                }
                strclass.AppendSpaceLine(1, "</td></tr>" );
            }
            strclass.AppendLine("</table>" );
            return strclass.ToString();

        }

        /// <summary>
        /// ��ɾ��3��ҳ�����
        /// </summary>      
        public string GetWebHtmlCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm)
        {
            StringPlus strclass = new StringPlus();
            if (AddForm)
            {
                strclass.AppendLine(" <!--******************************����ҳ�����********************************-->");
                strclass.AppendLine(GetAddAspx());
            }
            if (UpdateForm)
            {
                strclass.AppendLine(" <!--******************************�޸�ҳ�����********************************-->");
                strclass.AppendLine(GetUpdateAspx());
            }
            if (ShowForm)
            {
                strclass.AppendLine("  <!--******************************��ʾҳ�����********************************-->");
                strclass.AppendLine(GetShowAspx());
            }            
            return strclass.ToString();
        }
        #endregion

        #region ��ʾ�� CS
       
        /// <summary>
        /// ���ɱ�ʾ��ҳ���CS����
        /// </summary>
        /// <param name="ExistsKey"></param>
        /// <param name="AddForm">�Ƿ��������Ӵ���Ĵ���</param>
        /// <param name="UpdateForm">�Ƿ������޸Ĵ���Ĵ���</param>
        /// <param name="ShowForm">�Ƿ�������ʾ����Ĵ���</param>
        /// <param name="SearchForm">�Ƿ����ɲ�ѯ����Ĵ���</param>
        /// <returns></returns>
        public string GetWebCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm)
        {
            StringPlus strclass = new StringPlus();
            if (AddForm)
            {
                strclass.AppendLine("  /******************************���Ӵ������********************************/");
                strclass.AppendLine(GetAddAspxCs() );
            }
            if (UpdateForm)
            {
                strclass.AppendLine("  /******************************�޸Ĵ������********************************/");
                strclass.AppendLine("  /*�޸Ĵ���-��ʾ */");
                strclass.AppendLine(GetUpdateShowAspxCs() );
                strclass.AppendLine("  /*�޸Ĵ���-�ύ���� */");
                strclass.AppendLine(GetUpdateAspxCs() );
            }
            if (ShowForm)
            {
                strclass.AppendLine("  /******************************��ʾ�������********************************/");
                strclass.AppendLine(GetShowAspxCs() );
            }
            //if (DelForm)
            //{
            //    strclass.Append("  /******************************ɾ���������********************************/" );
            //    strclass.Append("");
            //    strclass.Append(CreatDeleteForm() );
            //}
            return strclass.Value;
        }

        /// <summary>
        /// �õ���ʾ�����Ӵ���Ĵ���
        /// </summary>      
        public string GetAddAspxCs()
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass0 = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(3,"string strErr=\"\";");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if ((IsIdentity))
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                        strclass0.AppendSpaceLine(3,"int " + columnName + "=int.Parse(this.txt" + columnName + ".Text);" );
                        strclass1.AppendSpaceLine(3,"if(!PageValidate.IsNumber(txt" + columnName + ".Text))" );
                        strclass1.AppendSpaceLine(3,"{");
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "�������֣�\\\\n\";	");
                        strclass1.AppendSpaceLine(3,"}");
                        break;
                    case "float":
                    case "numeric":
                    case "decimal":
                        strclass0.AppendSpaceLine(3,"decimal " + columnName + "=decimal.Parse(this.txt" + columnName + ".Text);" );
                        strclass1.AppendSpaceLine(3,"if(!PageValidate.IsDecimal(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3,"{");
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "�������֣�\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                    case "datetime":
                    case "smalldatetime":
                        strclass0.AppendSpaceLine(3,"DateTime " + columnName + "=DateTime.Parse(this.txt" + columnName + ".Text);" );
                        strclass1.AppendSpaceLine(3,"if(!PageValidate.IsDateTime(txt" + columnName + ".Text))" );
                        strclass1.AppendSpaceLine(3,"{" );
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "����ʱ���ʽ��\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                    case "bool":
                        strclass0.AppendSpaceLine(3,"bool " + columnName + "=this.chk" + columnName + ".Checked;" );
                        break;
                    case "byte[]":
                        strclass0.AppendSpaceLine(3,"byte[] " + columnName + "= new UnicodeEncoding().GetBytes(this.txt" + columnName + ".Text);" );
                        break;
                    default:
                        strclass0.AppendSpaceLine(3,"string " + columnName + "=this.txt" + columnName + ".Text;" );
                        strclass1.AppendSpaceLine(3,"if(this.txt" + columnName + ".Text ==\"\")" );
                        strclass1.AppendSpaceLine(3,"{" );
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "����Ϊ�գ�\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                }
                strclass2.AppendSpaceLine(3,"model." + columnName + "=" + columnName + ";" );
            }
            strclass.AppendLine(strclass1.ToString() );
            strclass.AppendSpaceLine(3,"if(strErr!=\"\")" );
            strclass.AppendSpaceLine(3,"{" );
            strclass.AppendSpaceLine(4,"MessageBox.Show(this,strErr);" );
            strclass.AppendSpaceLine(4,"return;" );
            strclass.AppendSpaceLine(3,"}" );
            strclass.AppendLine(strclass0.ToString() );
            strclass.AppendSpaceLine(3, ModelSpace + " model=new " + ModelSpace + "();" );
            strclass.AppendLine(strclass2.ToString());
            strclass.AppendSpaceLine(3, BLLSpace + " bll=new " + BLLSpace + "();" );
            strclass.AppendSpaceLine(3,"bll.Add(model);");
            return strclass.Value;
        }

        /// <summary>
        /// �õ��޸Ĵ���Ĵ���
        /// </summary>      
        public string GetUpdateAspxCs()
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass0 = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(3,"string strErr=\"\";");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if ((ispk) || (IsIdentity))
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                        strclass0.AppendSpaceLine(3,"int " + columnName + "=int.Parse(this.txt" + columnName + ".Text);" );
                        strclass1.AppendSpaceLine(3,"if(!PageValidate.IsNumber(txt" + columnName + ".Text))" );
                        strclass1.AppendSpaceLine(3,"{" );
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "�������֣�\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                    case "float":
                    case "numeric":
                    case "decimal":
                        strclass0.AppendSpaceLine(3,"decimal " + columnName + "=decimal.Parse(this.txt" + columnName + ".Text);" );
                        strclass1.AppendSpaceLine(3,"if(!PageValidate.IsDecimal(txt" + columnName + ".Text))" );
                        strclass1.AppendSpaceLine(3,"{" );
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "�������֣�\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                    case "datetime":
                    case "smalldatetime":
                        strclass0.AppendSpaceLine(3,"DateTime " + columnName + "=DateTime.Parse(this.txt" + columnName + ".Text);" );
                        strclass1.AppendSpaceLine(3,"if(!PageValidate.IsDateTime(txt" + columnName + ".Text))" );
                        strclass1.AppendSpaceLine(3,"{" );
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "����ʱ���ʽ��\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                    case "bool":
                        strclass0.AppendSpaceLine(3,"bool " + columnName + "=this.chk" + columnName + ".Checked;" );
                        break;
                    case "byte[]":
                        strclass0.AppendSpaceLine(3,"byte[] " + columnName + "= new UnicodeEncoding().GetBytes(this.txt" + columnName + ".Text);" );
                        break;
                    default:
                        strclass0.AppendSpaceLine(3,"string " + columnName + "=this.txt" + columnName + ".Text;" );
                        strclass1.AppendSpaceLine(3,"if(this.txt" + columnName + ".Text ==\"\")" );
                        strclass1.AppendSpaceLine(3,"{" );
                        strclass1.AppendSpaceLine(4,"strErr+=\"" + columnName + "����Ϊ�գ�\\\\n\";	" );
                        strclass1.AppendSpaceLine(3,"}" );
                        break;
                }
                strclass2.AppendSpaceLine(3,"model." + columnName + "=" + columnName + ";" );

            }
            strclass.AppendLine(strclass1.ToString() );
            strclass.AppendSpaceLine(3,"if(strErr!=\"\")" );
            strclass.AppendSpaceLine(3,"{" );
            strclass.AppendSpaceLine(4,"MessageBox.Show(this,strErr);" );
            strclass.AppendSpaceLine(4,"return;" );
            strclass.AppendSpaceLine(3,"}" );
            strclass.AppendLine(strclass0.ToString() );
            strclass.AppendLine();
            strclass.AppendSpaceLine(3,ModelSpace + " model=new " + ModelSpace + "();" );
            strclass.AppendLine(strclass2.ToString());
            strclass.AppendSpaceLine(3,BLLSpace + " bll=new " + BLLSpace + "();" );
            strclass.AppendSpaceLine(3,"bll.Update(model);");
            return strclass.ToString();
        }

        /// <summary>
        /// �õ��޸Ĵ���Ĵ���
        /// </summary>       
        public string GetUpdateShowAspxCs()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            string key = Key;
            strclass.AppendSpaceLine(1,"private void ShowInfo(" + LTP.CodeHelper.CodeCommon.GetInParameter(Keys) + ")" );
            strclass.AppendSpaceLine(1,"{" );
            strclass.AppendSpaceLine(2,BLLSpace + " bll=new " + BLLSpace + "();" );
            strclass.AppendSpaceLine(2, ModelSpace + " model=bll.GetModel(" + LTP.CodeHelper.CodeCommon.GetFieldstrlist(Keys) + ");" );
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;

                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                    case "float":
                    case "numeric":
                    case "decimal":
                    case "datetime":
                    case "smalldatetime":
                        if ((ispk) || (IsIdentity))
                        {
                            strclass.AppendSpaceLine(2,"this.lbl" + columnName + ".Text=model." + columnName + ".ToString();" );
                        }
                        else
                        {
                            strclass.AppendSpaceLine(2,"this.txt" + columnName + ".Text=model." + columnName + ".ToString();" );
                        }
                        break;
                    case "bool":
                        strclass.AppendSpaceLine(2,"this.chk" + columnName + ".Checked=model." + columnName + ";" );
                        break;
                    case "byte[]":
                        strclass.AppendSpaceLine(2,"this.txt" + columnName + ".Text=model." + columnName + ".ToString();" );
                        break;
                    default:
                        if ((ispk) || (IsIdentity))
                        {
                            strclass.AppendSpaceLine(2,"this.lbl" + columnName + ".Text=model." + columnName + ";" );
                        }
                        else
                        {
                            strclass.AppendSpaceLine(2,"this.txt" + columnName + ".Text=model." + columnName + ";" );
                        }
                        break;
                }
            }
            strclass.AppendLine();
            strclass.AppendSpaceLine(1,"}");
            return strclass.Value;
        }


        /// <summary>
        /// �õ���ʾ����ʾ����Ĵ���
        /// </summary>       
        public string GetShowAspxCs()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            string key = Key;
            strclass.AppendSpaceLine(1,"private void ShowInfo(" + LTP.CodeHelper.CodeCommon.GetInParameter(Keys) + ")" );
            strclass.AppendSpaceLine(1, "{" );
            strclass.AppendSpaceLine(2, BLLSpace + " bll=new " + BLLSpace + "();" );
            strclass.AppendSpaceLine(2, ModelSpace + " model=bll.GetModel(" + LTP.CodeHelper.CodeCommon.GetFieldstrlist(Keys) + ");" );
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if ((ispk) || (IsIdentity))
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                    case "float":
                    case "numeric":
                    case "decimal":
                    case "datetime":
                    case "smalldatetime":
                        strclass.AppendSpaceLine(2,"this.lbl" + columnName + ".Text=model." + columnName + ".ToString();" );
                        break;
                    case "bool":
                        strclass.AppendSpaceLine(2,"this.chk" + columnName + ".Checked=model." + columnName + ";" );
                        break;
                    case "byte[]":
                        strclass.AppendSpaceLine(2,"this.lbl" + columnName + ".Text=model." + columnName + ".ToString();" );
                        break;
                    default:
                        strclass.AppendSpaceLine(2,"this.lbl" + columnName + ".Text=model." + columnName + ";" );
                        break;
                }
            }
            strclass.AppendLine();
            strclass.AppendSpaceLine(1,"}");
            return strclass.ToString();
        }
        
        /// <summary>
        /// ɾ��ҳ��
        /// </summary>
        /// <returns></returns>
        public string CreatDeleteForm()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(1,"if(!Page.IsPostBack)");
            strclass.AppendSpaceLine(1,"{");
            strclass.AppendSpaceLine(2,BLLSpace + " bll=new " + BLLSpace + "();");
            switch (_keyType.Trim())
            {
                case "int":
                case "smallint":
                case "float":
                case "numeric":
                case "decimal":
                case "datetime":
                case "smalldatetime":
                    strclass.AppendSpaceLine(2,_keyType + " " + _key + "=" + _keyType + ".Parse(Request.Params[\"id\"]);");
                    break;
                default:
                    strclass.AppendSpaceLine(2,"string " + _key + "=Request.Params[\"id\"];");
                    break;
            }
            strclass.AppendSpaceLine(2,"bll.Delete(" + _key + ");");
            strclass.AppendSpaceLine(2,"Response.Redirect(\"index.aspx\");");
            strclass.AppendSpaceLine(1,"}");
            return strclass.Value;

        }

        public string CreatSearchForm()
        {
            StringPlus strclass = new StringPlus();

            return strclass.Value;
        }

        

        #endregion//��ʾ��

        #region  ����aspx.designer.cs
        /// <summary>
        /// ���Ӵ����html����
        /// </summary>      
        public string GetAddDesigner()
        {           
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if (IsIdentity)
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "datetime":
                    case "smalldatetime":
                        strclass.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
                        break;
                    case "bool":
                        strclass.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.CheckBox chk" + columnName + ";");
                        break;
                    default:
                        strclass.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
                        break;
                }
            }
            //��ť
            strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnAdd;");
            strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnCancel;");
            return strclass.ToString();

        }

        /// <summary>
        /// �޸Ĵ����html����
        /// </summary>      
        public string GetUpdateDesigner()
        {            
            StringPlus strclass = new StringPlus();
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;
                bool ispk = field.IsPK;
                bool IsIdentity = field.IsIdentity;
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                if ((ispk) || (IsIdentity))
                {
                    strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Label lbl" + columnName + ";");
                }
                else
                {
                    switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                    {
                        case "datetime":
                        case "smalldatetime":
                            strclass.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
                            break;
                        case "bool":
                            strclass.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.CheckBox chk" + columnName + ";");
                            break;
                        default:
                            strclass.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
                            break;
                    }
                }
            }

            //��ť            
            strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnAdd;");
            strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnCancel;");
            return strclass.Value;

        }

        /// <summary>
        /// ��ʾ�����html����
        /// </summary>     
        public string GetShowDesigner()
        {            
            StringPlus strclass = new StringPlus();
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.DeText;

                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "bool":
                        strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.CheckBox chk" + columnName + ";");
                        break;
                    default:
                        strclass.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Label lbl" + columnName + ";");
                        break;
                }

            }
            return strclass.ToString();

        }
        #endregion



    }
}
