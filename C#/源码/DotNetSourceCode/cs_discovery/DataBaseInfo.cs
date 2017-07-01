/*
 
	C#����֮��ϵ�н̳�������ʾ����
	
	���������ѧϰ�Ͳο�ʹ��

	���� Ԭ���� 2008��5��15 
	
	MSN			yyf9989@hotmail.com
	
	QQ			28348092
	
	���߲���	http://xdesigner.cnblogs.com/
	
	ʹ���������ߵ�����֪ʶ��Ȩ��

*/
using System;
using System.Data ;
using System.Data.OleDb ;
using System.Xml ;

namespace XDesignerData
{
	/// <summary>
	/// �������ݿ��ṹ�Ķ���
	/// </summary>
	/// <remarks>
	/// �������ܷ���Access2000,Oracle,SQLServer �����ݿ�,���������ṹ����.
	/// Ҳ�ɴ�PDM�ļ��м��ر�ṹ����
	/// ���� Ԭ���� </remarks>
	[System.Serializable()]
	public class DataBaseInfo : System.ICloneable
	{
		/// <summary>
		/// ����Ϊ�ĳ�ʼ������
		/// </summary>
		public DataBaseInfo()
		{
		}

		private string strName = null;
		/// <summary>
		/// ��������
		/// </summary>
		public string Name
		{
			get{ return strName ;}
			set{ strName = value;}
		}
		private string strDescription = null;
		/// <summary>
		/// ����˵��
		/// </summary>
		public string Description
		{
			get{ return strDescription ;}
			set{ strDescription = value;}
		}
		/// <summary>
		/// �ܹ��������ֶθ���
		/// </summary>
		public int FieldCount
		{
			get
			{
				int iCount = 0 ;
				foreach( TableInfo table in myTables )
				{
					iCount += table.Fields.Count ;
				}
				return iCount ;
			}
		}

		private TableInfoCollection myTables = new TableInfoCollection();
		/// <summary>
		/// ���ݿ����Ϣ�б�
		/// </summary>
		public TableInfoCollection Tables
		{
			get{ return myTables ;}
		}

		/// <summary>
		/// ���ݱ���Ϣ�б�����
		/// </summary>
		public class TableInfoCollection : System.Collections.CollectionBase 
		{
			/// <summary>
			/// ����ָ����ŵı���Ϣ����
			/// </summary>
			public TableInfo this [ int index ]
			{
				get{ return ( TableInfo ) this.List[ index ] ;}
			}
			/// <summary>
			/// ����ָ�����Ƶı���Ϣ����
			/// </summary>
			public TableInfo this[ string strTableName ]
			{
				get
				{
					foreach( TableInfo t in this )
					{
						if( string.Compare( t.Name , strTableName , true ) == 0 )
							return t ;
					}
					return null;
				}
			}
			/// <summary>
			/// ���б���ӱ����
			/// </summary>
			/// <param name="table">�����</param>
			/// <returns>�����������б��е����</returns>
			public int Add( TableInfo table )
			{
				return this.List.Add( table );
			}
			public void Remove( TableInfo table )
			{
				this.List.Remove( table );
			}
		}

		/// <summary>
		/// ���ָ���������ֶ������ֶζ���
		/// </summary>
		/// <param name="TableName">����</param>
		/// <param name="FieldName">�ֶ���</param>
		/// <returns>��õ��ֶζ���,��δ�ҵ��򷵻ؿ�����</returns>
		public FieldInfo GetField( string TableName , string FieldName )
		{
			TableInfo table = myTables[ TableName ];
			if( table != null )
				return table.Fields[ FieldName ] ;
			return null;
		}

		/// <summary>
		/// ���ָ��ȫ���Ƶ��ֶζ���
		/// </summary>
		/// <param name="FullName">�ֶ�����,��ʽΪ ����.�ֶ���</param>
		/// <returns>��õ��ֶζ���,��Ϊ�ҵ������ؿ�����</returns>
		public FieldInfo GetField( string FullName )
		{
			if( FullName == null )
				return null ; 
			int index = FullName.IndexOf(".");
			if( index <= 0 )
				return null;
			return GetField( 
				FullName.Substring( 0 , index ).Trim() , 
				FullName.Substring( index + 1 ).Trim());
		}


		/// <summary>
		/// �����������
		/// </summary>
		public enum FillStyleConst
		{
			/// <summary>
			/// ����ʽ
			/// </summary>
			None ,
			/// <summary>
			/// ��PDM�ļ�������
			/// </summary>
			PDM ,
			/// <summary>
			/// ��Access2000���ݿ�������
			/// </summary>
			Access2000 ,
			/// <summary>
			/// ��SQLSERVER���ݿ�������
			/// </summary>
			SQLServer ,
			/// <summary>
			/// ��ORACLE���ݿ�������
			/// </summary>
			Oracle
			//			/// <summary>
			//			/// ��XML�ĵ�������
			//			/// </summary>
			//			XMLDocument
		}

		/// <summary>
		/// ���������ʽ
		/// </summary>
		protected FillStyleConst intFillStyle = FillStyleConst.None ;
		/// <summary>
		/// ���������ʽ
		/// </summary>
		public FillStyleConst FillStyle
		{
			get{ return intFillStyle ;}
			set{ intFillStyle = value;}
		}

		#region ��PDM�ĵ����ض������� *****************************************

		/// <summary>
		/// ��һ��PDM���ݽṹ�����ļ��м������ݽṹ��Ϣ
		/// </summary>
		/// <param name="strFileName">PDM�ļ���</param>
		/// <returns>���ص��ֶ���Ϣ����</returns>
		public int LoadFromPDMXMLFile( string strFileName )
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.Load( strFileName );
			return LoadFromPDMXMLDocument( doc );
		}
		/// <summary>
		/// ��PDM���ݽṹ����XML�ļ��м������ݽṹ��Ϣ
		/// </summary>
		/// <param name="doc">XML�ĵ�����</param>
		/// <returns>���ص��ֶ���Ϣ����</returns>
		public int LoadFromPDMXMLDocument( XmlDocument doc )
		{
			intFillStyle = FillStyleConst.PDM ;
			int RecordCount = 0 ;
			myTables.Clear();
			XmlNamespaceManager nsm = new XmlNamespaceManager( doc.NameTable );
			nsm.AddNamespace( "a" , "attribute" );
			nsm.AddNamespace( "c" , "collection" );
			nsm.AddNamespace( "o" , "object");
			XmlNode RootNode = doc.SelectSingleNode("/Model/o:RootObject/c:Children/o:Model" , nsm );
			if( RootNode == null )
				return 0 ;
			strName = ReadXMLValue( RootNode , "a:Name" , nsm );
			strDescription = strName ;
			// ���ݱ�
			foreach( XmlNode TableNode in RootNode.SelectNodes("c:Tables/o:Table" , nsm ))
			{
				TableInfo table = new TableInfo();
				myTables.Add( table );
				table.Name = ReadXMLValue( TableNode , "a:Code" , nsm );
				table.Remark = ReadXMLValue( TableNode , "a:Name" , nsm );
				string keyid = ReadXMLValue( TableNode , "c:PrimaryKey/o:Key/@Ref" , nsm );
				System.Collections.Specialized.StringCollection Keys =
					new System.Collections.Specialized.StringCollection();
				if( keyid != null )
				{
					foreach( XmlNode KeyNode in TableNode.SelectNodes(
						"c:Keys/o:Key[@Id = '" + keyid + "']/c:Key.Columns/o:Column/@Ref" , nsm ))
					{
						Keys.Add( KeyNode.Value );
					}
				}
				foreach( XmlNode FieldNode in TableNode.SelectNodes("c:Columns/o:Column" , nsm ))
				{
					RecordCount ++ ;
					string id = ( ( XmlElement )  FieldNode).GetAttribute("Id");
					FieldInfo field = new FieldInfo();
					table.Fields.Add( field );
					field.Name = ReadXMLValue( FieldNode , "a:Code" , nsm );
					field.Remark = ReadXMLValue( FieldNode , "a:Name" , nsm );
					field.Description = ReadXMLValue( FieldNode , "a:Comment" , nsm );
					string FieldType = ReadXMLValue( FieldNode , "a:DataType" , nsm );
					if( FieldType != null )
					{
						int index = FieldType.IndexOf("(");
						if( index > 0 )
							FieldType = FieldType.Substring( 0 , index );
					}
					field.FieldType = FieldType ;

					field.FieldWidth = ReadXMLValue( FieldNode , "a:Length" , nsm );
					if( Keys.Contains( id ))
						field.PrimaryKey = true;
				}
			}
			return RecordCount ;
		}

		private string ReadXMLValue(
			System.Xml.XmlNode node ,
			string path , 
			System.Xml.XmlNamespaceManager nsm )
		{
			System.Xml.XmlNode node2 = node.SelectSingleNode( path  , nsm );
			if( node2 == null )
				return null ;
			else
			{
				if( node2 is System.Xml.XmlElement )
					return ( ( System.Xml.XmlElement ) node2).InnerText ;
				else
					return node2.Value ;
			}
		}

		#endregion

		#region �������ݿ���ض������� ****************************************

		/// <summary>
		/// ��ָ�����Ƶ�Access2000���ݿ��м������ݿ�ṹ��Ϣ
		/// </summary>
		/// <param name="strFileName">���ݿ��ļ���</param>
		/// <returns>���ص��ֶ���Ϣ����</returns>
		public int LoadFromAccess2000( string strFileName )
		{
			using( OleDbConnection conn = new OleDbConnection())
			{
				conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName ;
				conn.Open();
				int result = LoadFromAccess2000( conn );
				conn.Close();
				return result ;
			}
		}

		/// <summary>
		/// �� Jet40( Access2000 ) �����ݿ��м������ݽṹ��Ϣ
		/// </summary>
		/// <param name="myConn">���ݿ����Ӷ���</param>
		/// <returns>���ص��ֶ���Ϣ����</returns>
		public int LoadFromAccess2000( OleDbConnection myConn )
		{
			intFillStyle = FillStyleConst.Access2000 ;
			int RecordCount = 0 ;
			myTables.Clear();
			string dbName = myConn.DataSource ;
			if( dbName != null )
				strName = System.IO.Path.GetFileName( dbName );
			using(System.Data.DataTable myDataTable =
					  myConn.GetOleDbSchemaTable( System.Data.OleDb.OleDbSchemaGuid.Columns , null))
			{
				foreach( System.Data.DataRow myRow in myDataTable.Rows )
				{
					string strTable = Convert.ToString( myRow["TABLE_NAME"] );
					if( ! strTable.StartsWith("MSys"))
					{
						TableInfo myTable = myTables[ strTable ] ;
						if( myTable == null )
						{
							myTable = new TableInfo();
							myTable.Name = strTable ;
							myTables.Add( myTable );
						}
						FieldInfo myField = new FieldInfo();
						myTable.Fields.Add( myField );
						myField.Name  = Convert.ToString( myRow["COLUMN_NAME"]);
						myField.Nullable = Convert.ToBoolean( myRow["IS_NULLABLE"]);
						System.Data.OleDb.OleDbType intType = (System.Data.OleDb.OleDbType)
							Convert.ToInt32( myRow["DATA_TYPE"]);
						if( System.DBNull.Value.Equals( myRow["DESCRIPTION"] ) == false )
						{
							myField.Remark = Convert.ToString( myRow["DESCRIPTION"] ) ;
						}
						if( intType == System.Data.OleDb.OleDbType.WChar )
						{
							myField.FieldType = "Char" ;
						}
						else
						{
							myField.FieldType  = intType.ToString();
						}
						myField.FieldWidth  = Convert.ToString( myRow["CHARACTER_MAXIMUM_LENGTH"]);
						RecordCount ++ ;
					}
				}//foreach
			}//using
			using( System.Data.DataTable myDataTable = 
					   myConn.GetOleDbSchemaTable( System.Data.OleDb.OleDbSchemaGuid.Indexes , null))
			{
				foreach( System.Data.DataRow myRow in myDataTable.Rows )
				{
					string strTable = Convert.ToString( myRow["TABLE_NAME"] );
					TableInfo myTable = myTables[ strTable ];
					if( myTable != null )
					{
						FieldInfo myField = myTable.Fields[ Convert.ToString( myRow["COLUMN_NAME"])];
						if( myField != null)
						{
							myField.Indexed  = true;
							myField.PrimaryKey = ( Convert.ToBoolean( myRow["PRIMARY_KEY"]));
						}
					}
				}//foreach
			}//using
			return RecordCount ;
		}//public int LoadFromAccess2000( OleDbConnection myConn )

		/// <summary>
		/// �� Oracle �������ݿ�ṹ��Ϣ
		/// </summary>
		/// <param name="myConn">���ݿ����Ӷ���</param>
		/// <returns>���ص��ֶ���Ϣ����</returns>
		public int LoadFromOracle( IDbConnection  myConn )
		{
			intFillStyle = FillStyleConst.Oracle ;
			int RecordCount = 0 ;
			string strSQL = null;
			strSQL = "Select TName,CName,coltype,width  From Col Order by TName,CName";
			myTables.Clear();
			if( myConn is OleDbConnection )
			{
				strName =  ( ( System.Data.OleDb.OleDbConnection ) myConn ).DataSource 
					+ " - " + myConn.Database ;
			}
			else
				strName = myConn.Database ;
			using( System.Data.IDbCommand myCmd = myConn.CreateCommand())
			{
				myCmd.CommandText = strSQL ;
				IDataReader myReader = myCmd.ExecuteReader( CommandBehavior.SingleResult );
				TableInfo LastTable = null;
				while( myReader.Read())
				{
					string TableName = myReader.GetString(0).Trim();
					if( LastTable == null || LastTable.Name != TableName )
					{
						LastTable = new TableInfo();
						myTables.Add( LastTable );
						LastTable.Name = TableName ;
					}
					FieldInfo NewField = new FieldInfo();
					LastTable.Fields.Add( NewField );
					NewField.Name = myReader.GetString(1);
					NewField.FieldType = myReader.GetString(2);
					NewField.FieldWidth = myReader[3].ToString();
					RecordCount ++ ;
				}//while
				myReader.Close();
			
				myCmd.CommandText = @"
select table_name , 
	column_name , 
	index_name 
from user_ind_columns 
order by table_name , column_name ";
				myReader = myCmd.ExecuteReader( CommandBehavior.SingleResult );
				TableInfo myTable = null;
				while( myReader.Read())
				{
					myTable = myTables[ myReader.GetString(0)];
					if( myTable != null )
					{
						string IDName = myReader.GetString(2);
						string FieldName = myReader.GetString(1);
						FieldInfo myField = myTable.Fields[ FieldName ];
						if( myField != null )
						{
							myField.Indexed = true ;
							if( IDName.StartsWith("PK") )
							{
								myField.PrimaryKey = true;
							}
						}
					}
				}//while
				myReader.Close();
			}//using
			return RecordCount ;
		}//public int LoadFromOracle( System.Data.IDbConnection myConn )

		/// <summary>
		/// �� SQLServer �м������ݿ�ṹ��Ϣ
		/// </summary>
		/// <param name="myConn">���ݿ����Ӷ���</param>
		/// <returns>���ص��ֶ���Ϣ����</returns>
		public int LoadFromSQLServer( IDbConnection myConn )
		{
			intFillStyle = FillStyleConst.SQLServer ;
			int RecordCount = 0 ;
	
			if( myConn is OleDbConnection )
				strName = ( ( OleDbConnection ) myConn ).DataSource ;
			else if( myConn is System.Data.SqlClient.SqlConnection )
				strName = ( ( System.Data.SqlClient.SqlConnection ) myConn ).DataSource ;
			strName = strName + " - " + myConn.Database ;

			string strSQL = null;
			strSQL = @"
select
	sysobjects.name ,
	syscolumns.name  ,
	systypes.name ,
	syscolumns.length , 
	syscolumns.isnullable ,
	sysobjects.type
from 
	syscolumns,
	sysobjects,
	systypes 
where 
	syscolumns.id=sysobjects.id 
	and syscolumns.xusertype=systypes.xusertype 
	and (sysobjects.type='U' or sysobjects.type='V' )
	and systypes.name <>'_default_' 
	and systypes.name<>'sysname' 
order by 
	sysobjects.name,
	syscolumns.name";
			myTables.Clear();

			using( System.Data.IDbCommand myCmd = myConn.CreateCommand())
			{
				myCmd.CommandText = strSQL ;
				IDataReader myReader = myCmd.ExecuteReader( CommandBehavior.SingleResult );
				TableInfo LastTable = null;
				while( myReader.Read())
				{
					string TableName = myReader.GetString(0).Trim();
					if( LastTable == null || LastTable.Name != TableName )
					{
						LastTable = new TableInfo();
						myTables.Add( LastTable );
						LastTable.Name = TableName ;
						LastTable.Tag = Convert.ToString( myReader.GetValue( 5 ));
					}
					FieldInfo NewField = new FieldInfo();
					LastTable.Fields.Add( NewField );
					NewField.Name = myReader.GetString(1);
					NewField.FieldType = myReader.GetString(2);
					NewField.FieldWidth = myReader[3].ToString();
					if( myReader.IsDBNull( 4 ) == false)
						NewField.Nullable = (myReader.GetInt32(4) == 1);
					RecordCount ++ ;
				}//while
				myReader.Close();
				// ����������Ϣ
				for( int iCount = myTables.Count - 1 ; iCount >= 0 ; iCount -- )
				{
					TableInfo myTable = myTables[ iCount ] ;
					if( string.Compare( ( string ) myTable.Tag , "U" , true ) == 0 )
					{
						try
						{
							myCmd.CommandText = "sp_helpindex \"" + myTable.Name + "\"" ;
							//myCmd.CommandType = System.Data.CommandType.Text ;
							myReader = myCmd.ExecuteReader( );
							while( myReader.Read())
							{
								string strKeyName = myReader.GetString(0);
								string strDesc = myReader.GetString(1);
								string strFields = myReader.GetString(2);
								bool bolPrimary = ( strDesc.ToLower().IndexOf("primary") >= 0 );
								foreach( string strField in strFields.Split(','))
								{
									FieldInfo myField = myTable.Fields[ strField.Trim()];
									if( myField != null)
									{
										myField.Indexed = true;
										myField.PrimaryKey = bolPrimary ;
									}
								}//foreach
							}//while
							myReader.Close();
						}
						catch( Exception ext )
						{
							//this.List.Remove( myTable );
							myTable.Name = myTable.Name + " " + ext.Message ;
						}
					}
				}//foreach
			}//using
			return RecordCount ;
		}//public int LoadFromSQLServer( System.Data.IDbConnection myConn )

		#endregion

		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>����Ʒ</returns>
		public object Clone()
		{
			DataBaseInfo info = new DataBaseInfo();
			info.intFillStyle = this.intFillStyle ;
			info.strDescription = this.strDescription ;
			info.Name = this.strName ;
			foreach( TableInfo table in myTables )
			{
				TableInfo NewTable = new TableInfo();
				NewTable.Name = table.Name ;
				NewTable.Remark = table.Remark ;
				NewTable.Description = table.Description ;
				NewTable.Tag = table.Tag ;
				info.myTables.Add( NewTable );
				foreach( FieldInfo field in table.Fields )
				{
					FieldInfo NewField = new FieldInfo();
					NewField.Name = field.Name ;
					NewField.Remark = field.Remark ;
					NewField.Description = field.Description ;
					NewField.FieldType = field.FieldType ;
					NewField.FieldWidth = field.FieldWidth ;
					NewField.Indexed = field.Indexed ;
					NewField.Nullable = field.Nullable ;
					NewField.PrimaryKey = field.PrimaryKey ;
					NewTable.Fields.Add( NewField );
				}
			}
			return info ;
		}
	}//public class DBStructInformation

	/// <summary>
	/// ���ݱ���Ϣ����
	/// </summary>
	[System.Serializable()]
	[System.Xml.Serialization.XmlType("Table")]
	public class TableInfo
	{
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public TableInfo( )
		{
			myFields.myOwnerTable = this ;
		}
		private string strName = null;
		/// <summary>
		/// ��������
		/// </summary>
		public string Name
		{
			get{ return strName ;}
			set{ strName = value;}
		}
		private string strRemark = null;
		/// <summary>
		/// ����˵��,һ�����Ϊ����������
		/// </summary>
		public string Remark 
		{
			get{ return strRemark ;}
			set{ strRemark = value;}
		}
		private string strDescription = null;
		/// <summary>
		/// ����˵��
		/// </summary>
		public string Description
		{
			get{ return strDescription ;}
			set{ strDescription = value;}
		}

		private object objTag = null;
		/// <summary>
		/// ���󸽼�����
		/// </summary>
		public object Tag
		{
			get{ return objTag ;}
			set{ objTag = value;}
		}
		private FieldInfoCollection myFields = new FieldInfoCollection();
		/// <summary>
		/// �ֶζ����б�
		/// </summary>
		public FieldInfoCollection Fields
		{
			get{ return myFields ;}
		}

		/// <summary>
		/// �ֶζ����б�����
		/// </summary>
		public class FieldInfoCollection : System.Collections.CollectionBase 
		{
			internal TableInfo myOwnerTable = null;
			/// <summary>
			/// ����ָ����ŵ��ֶ���Ϣ
			/// </summary>
			public FieldInfo this[ int index ]
			{
				get{ return ( FieldInfo) this.InnerList[ index ] ;}
			}
			/// <summary>
			/// ����ָ�����Ƶ��ֶ���Ϣ
			/// </summary>
			public FieldInfo this[ string strFieldName ]
			{
				get
				{
					foreach( FieldInfo f in this.InnerList )
					{
						if( string.Compare( f.Name , strFieldName , true ) == 0 )
							return f ;
					}
					return null;
				}
			}
			/// <summary>
			/// ����ֶ���Ϣ����
			/// </summary>
			/// <param name="info">�ֶ���Ϣ����</param>
			/// <returns>�ֶζ������б��е����</returns>
			public int Add( FieldInfo info )
			{
				info.OwnerTable = myOwnerTable ;
				return this.List.Add( info );
			}
			public void Remove( FieldInfo info )
			{
				this.List.Remove( info );
			}
		}
	}//public class TableInfo

	/// <summary>
	/// �ֶ���Ϣ����
	/// </summary>
	[System.Serializable()]
	[System.Xml.Serialization.XmlType("Field")]
	public class FieldInfo
	{
		private string strName = null;
		/// <summary>
		/// �ֶ�����
		/// </summary>
		public string Name
		{
			get{ return strName ;}
			set{ strName = value;}
		}
		private string strRemark = null;
		/// <summary>
		/// �ֶ�˵��,һ�����Ϊ�ֶ�������
		/// </summary>
		public string Remark 
		{
			get{ return strRemark ;}
			set{ strRemark = value;}
		}
		private string strDescription = null;
		/// <summary>
		/// �ֶ�˵��
		/// </summary>
		public string Description
		{
			get{ return strDescription ;}
			set{ strDescription = value;}
		}

		private string strFieldType = null;
		/// <summary>
		/// �ֶ�����
		/// </summary>
		public string FieldType
		{
			get{ return strFieldType ;}
			set{ strFieldType = value;}
		}

		/// <summary>
		/// �ж��ֶ��Ƿ����ַ����ֶ�
		/// </summary>
		public bool IsString
		{
			get
			{
				return TypeContainStrings( new string[]{ "char" , "text" });
			}
			set{}
		}
		
		/// <summary>
		/// �ж��ֶ��Ƿ��������ֶ�
		/// </summary>
		public bool IsInteger
		{
			get
			{
				return TypeContainStrings( new string[]{ "int" , "bit" });
			}
			set{}
		}

		/// <summary>
		/// �ж��ֶ��Ƿ��ǲ�������
		/// </summary>
		public bool IsBoolean
		{
			get
			{
				return TypeContainStrings( new string[]{ "bit" });
			}
			set{}
		}

		/// <summary>
		/// �ֶ��Ƿ�����ֵ���ֶ�
		/// </summary>
		public bool IsNumberic
		{
			get
			{
				return TypeContainStrings( new string[]{ "number", "decimal" , "numberic" , "float" , "real" , "double" });
			}
			set{}
		}
		/// <summary>
		/// �Ƿ����������͵��ֶ�
		/// </summary>
		public bool IsDateTime
		{
			get
			{
				return TypeContainStrings( new string[]{"date" , "datetime"});
			}
			set{}
		}

		/// <summary>
		/// �Ƿ��Ƕ��������͵��ֶ�
		/// </summary>
		public bool IsBinary
		{
			get
			{
				return TypeContainStrings( new string[]{"binary" , "long" , "image" });
			}
			set{}
		}
	
		private bool TypeContainStrings( string[] items )
		{
			string type = this.strFieldType ;
			if( type != null )
			{
				type = type.ToLower();
				foreach( string item in items )
				{
					if( type.IndexOf( item ) >= 0 )
						return true ;
				}
			}
			return false;
		}

		
		/// <summary>
		/// �ֶζ�Ӧ����������
		/// </summary>
		[System.Xml.Serialization.XmlIgnore()]
		public Type ValueType
		{
			get
			{
				if( this.IsBoolean )
					return typeof( bool );
				if( this.IsInteger )
					return typeof( int );
				if( this.IsBinary )
					return typeof( byte[] );
				if( this.IsDateTime )
					return typeof( DateTime );
				if( this.IsNumberic )
					return typeof( double );
				return typeof( string);
			}
		}

		/// <summary>
		/// �ֶζ�Ӧ��������������
		/// </summary>
		public string ValueTypeName
		{
			get
			{
				return this.ValueType.FullName ;
			}
			set{}
		}

		private string strFieldWidth = "";
		/// <summary>
		/// �ֶο��
		/// </summary>
		public string FieldWidth
		{ 
			get{ return strFieldWidth ;}
			set{ strFieldWidth = value;}
		}

		private bool bolNullable = true ;
		/// <summary>
		/// �ֶοɷ�Ϊ��
		/// </summary>
		//[System.ComponentModel.DefaultValue( true )]
		public bool Nullable
		{
			get{ return bolNullable ;}
			set{ bolNullable = value;}
		}

		private bool bolPrimaryKey = false;
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public bool PrimaryKey
		{
			get{ return bolPrimaryKey ;}
			set{ bolPrimaryKey = value;}
		}

		private bool bolIndexed = false;
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public bool Indexed
		{
			get{ return bolIndexed ;}
			set{ bolIndexed = value;}
		}

		/// <summary>
		/// �ֶ�ȫ��
		/// </summary>
		public string FullName
		{
			get
			{
				if( myOwnerTable == null )
					return strName ;
				else
					return myOwnerTable.Name + "." + strName ;
			}
		}
		/// <summary>
		/// ���ر�ʾ������ַ���
		/// </summary>
		/// <returns>�ַ���</returns>
		public override string ToString()
		{
			return FullName ;
		}

		private TableInfo myOwnerTable = null;
		/// <summary>
		/// �ֶ����ڵ����ݱ����
		/// </summary>
		[System.Xml.Serialization.XmlIgnore()]
		public TableInfo OwnerTable
		{
			get{ return myOwnerTable ;}
			set{ myOwnerTable = value;}
		}
	}//public class FieldInfo
}