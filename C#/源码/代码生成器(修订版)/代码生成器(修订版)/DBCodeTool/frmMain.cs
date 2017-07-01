using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBCodeTool
{
    public partial class frmMain : Form
    {
        public SqlConnection cn = null;

        public frmMain()
        {
            InitializeComponent();
            cn = new SqlConnection();
        }

        private void getDataBaseList()
        {
            string sysDataBaseStr = "master|model|msdb|tempdb|";

            this.txtDataBase.Items.Clear();
            cn.ConnectionString = "Server="+txtServer.Text.Trim()+";DataBase=master;UID="+txtUID.Text.Trim()+";PWD="+txtPWD.Text.Trim()+";";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_helpdb";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count-1; i++)
                {
                    if (sysDataBaseStr.IndexOf(dt.Rows[i]["name"].ToString().ToLower()) < 0)
                    {
                        this.txtDataBase.Items.Add(dt.Rows[i]["name"].ToString());
                    }
                }
                this.txtDataBase.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            getDataBaseList();
            getTableList();
        }

        private void getTableList()
        {
            this.txtTable.Items.Clear();
            cn.ConnectionString = "Server=" + txtServer.Text.Trim() + ";DataBase=" + txtDataBase.Text.Trim() + ";UID=" + txtUID.Text.Trim() + ";PWD=" + txtPWD.Text.Trim() + ";";
            SqlCommand cmd = new SqlCommand("Select * from [sysobjects] where xtype='U'", cn);
            cmd.Parameters.Add(new SqlParameter("@objname", txtDataBase.Text.Trim()));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.txtTable.Items.Add(dt.Rows[i][0].ToString());
                }
                this.txtTable.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTableList();
        }

        private string getColumnCode(string cName, string cDataType, string cLength, string splitStr)
        {
            string str = "";
            if ((cDataType.ToLower() == "char") || (cDataType.ToLower() == "varchar") || (cDataType.ToLower() == "nvarchar") || (cDataType.ToLower() == "nchar"))
            {
                str = cName + " " + cDataType + "(" + cLength + ")" + splitStr;
            }
            else
            {
                str = cName + " " + cDataType + splitStr;
            }
            return "  "+str+"\n";
        }

        private void getSqlCode(string tblName)
        {
            cn.ConnectionString = "Server=" + txtServer.Text.Trim() + ";DataBase=" + txtDataBase.Text.Trim() + ";UID=" + txtUID.Text.Trim() + ";PWD=" + txtPWD.Text.Trim() + ";";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_help";
            cmd.Connection = cn;
            cmd.Parameters.Add(new SqlParameter("@objname", txtTable.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.richTextBox1.AppendText("Create Table " + tblName + "\n");
            this.richTextBox1.AppendText("(\n");
            for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
            {
                if (i < ds.Tables[1].Rows.Count - 1)
                {
                    this.richTextBox1.AppendText(getColumnCode(ds.Tables[1].Rows[i]["Column_name"].ToString(), ds.Tables[1].Rows[i]["Type"].ToString(), ds.Tables[1].Rows[i]["Length"].ToString(), ","));
                }
                else
                {
                    this.richTextBox1.AppendText(getColumnCode(ds.Tables[1].Rows[i]["Column_name"].ToString(), ds.Tables[1].Rows[i]["Type"].ToString(), ds.Tables[1].Rows[i]["Length"].ToString(), ""));
                }
            }
            this.richTextBox1.AppendText(")\n\n");
            this.richTextBox1.Focus();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            getSqlCode(txtTable.Text);
        }

        private void getClassCode(string tblName)
        {
            cn.ConnectionString = "Server=" + txtServer.Text.Trim() + ";DataBase=" + txtDataBase.Text.Trim() + ";UID=" + txtUID.Text.Trim() + ";PWD=" + txtPWD.Text.Trim() + ";";
            SqlCommand cmd = new SqlCommand("Select * from ["+tblName+"]", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.richTextBox1.AppendText("public class " + tblName + "Info\n");
            this.richTextBox1.AppendText("{\n");
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                if (getTypeName(dt.Columns[i].DataType.Name) == "int64")
                    this.richTextBox1.AppendText("   public Int64 " + dt.Columns[i].ColumnName + ";\n");
                else
                    if(getTypeName(dt.Columns[i].DataType.Name) == "guid")
                        this.richTextBox1.AppendText("   public Guid " + dt.Columns[i].ColumnName + ";\n");
                    else
                        this.richTextBox1.AppendText("   public " + getTypeName(dt.Columns[i].DataType.Name) + " " + dt.Columns[i].ColumnName + ";\n");
            }
            this.richTextBox1.AppendText("}\n\n");
            
        }

        private string getTypeName(string datatype)
        {
            string temp = "";
            if (datatype == "Int32" || datatype == "Int16")
            {
                temp = "Int";
            }
            else if (datatype == "Decimal")
            {
                temp = "float";
            }
            else if (datatype == "DateTime")
            {
                temp = "string";
            }
            else if (datatype == "Boolean")
            {
                temp = "bool";
            }
            else
            {
                temp = datatype.ToLower();
            }
            return temp;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= this.txtTable.Items.Count - 1;i++ )
                getClassCode(txtTable.Items[i].ToString());
            
        }

        private void getInterfaceCode(string tblName)
        {
            this.richTextBox1.AppendText("public interface I"+tblName+"\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("   DataTable get" + tblName + "List(string sql);//根据查询语句获取数据\n");
            this.richTextBox1.AppendText("   void Add" + tblName + "(" + tblName + "Info obj);//添加\n");
            this.richTextBox1.AppendText("   void Update" + tblName + "(" + tblName + "Info obj);//修改\n");
            this.richTextBox1.AppendText("   void Del" + tblName + "(" + tblName + "Info obj);//删除\n");
            this.richTextBox1.AppendText("   void Updete" + tblName + "(string sql);//根据更新语句更新数据\n");
            this.richTextBox1.AppendText("}\n\n");
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            getInterfaceCode(txtTable.Text);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            getClassCode(txtTable.Text);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= this.txtTable.Items.Count - 1;i++ )
                getInterfaceCode(txtTable.Items[i].ToString());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= this.txtTable.Items.Count - 1;i++ )
                getSqlCode(txtTable.Items[i].ToString());
        }

        private void getClassCode2(string tblName)
        {
            this.richTextBox1.AppendText("/****************** 下面是 " + tblName + " 类 ********************/\n");
            this.richTextBox1.AppendText("/// <实体类摘要>\n");
            this.richTextBox1.AppendText("/// 类名："+tblName+"\n");
            this.richTextBox1.AppendText("/// 版本：1.0.0.0\n");
            this.richTextBox1.AppendText("/// 时间："+System.DateTime.Now.ToString()+"\n");
            this.richTextBox1.AppendText("/// 说明：本实体类由代码生成器生成\n");
            this.richTextBox1.AppendText("/// </实体类摘要>\n\n");
            this.richTextBox1.AppendText("class " + tblName + ":I"+tblName+"\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("   SqlHelp db = null;\n");
            this.richTextBox1.AppendText("   public " + tblName + "()\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("       db = new SqlHelp();");
            this.richTextBox1.AppendText("   }\n\n");
            this.richTextBox1.AppendText("   public DataTable get" + tblName + "List(string sql) //根据查询语句获取数据\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("       return db.ExecuteQuery(sql);\n");
            this.richTextBox1.AppendText("   }\n\n");
            this.richTextBox1.AppendText("   public void Add" + tblName + "(" + tblName + "Info obj) //添加\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("       //\n");
            this.richTextBox1.AppendText("   }\n\n");
            this.richTextBox1.AppendText("   public void Update" + tblName + "(" + tblName + "Info obj) //修改\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("       //\n");
            this.richTextBox1.AppendText("   }\n\n");
            this.richTextBox1.AppendText("   public void Del" + tblName + "(" + tblName + "Info obj) //删除\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("       //\n");
            this.richTextBox1.AppendText("   }\n\n");
            this.richTextBox1.AppendText("   public void Updete" + tblName + "(string sql) //根据更新语句更新数据\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("       db.ExecuteNonQuery(sql);\n");
            this.richTextBox1.AppendText("   }\n\n");
            this.richTextBox1.AppendText("}\n\n");
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            getClassCode2(txtTable.Text);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= this.txtTable.Items.Count - 1; i++)
                getClassCode2(txtTable.Items[i].ToString());
        }

        private void getEntityFactoryCode()
        {
            this.richTextBox1.AppendText("public class EntityFactory\n");
            this.richTextBox1.AppendText("{\n");
            for (int i = 0; i <= this.txtTable.Items.Count - 1; i++)
            {
                this.richTextBox1.AppendText("   public static I" + txtTable.Items[i].ToString() + " get" + txtTable.Items[i].ToString() + "()\n");
                this.richTextBox1.AppendText("   {\n");
                this.richTextBox1.AppendText("      return new T" + txtTable.Items[i].ToString() + "();\n");
                this.richTextBox1.AppendText("   }\n");
            }
            this.richTextBox1.AppendText("}\n");
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            this.richTextBox1.AppendText("public class EntityFactory\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("   public static I" + txtTable.Text + " get" + txtTable.Text + "()\n");
            this.richTextBox1.AppendText("   {\n");
            this.richTextBox1.AppendText("      return new " + txtTable.Text + "();\n");
            this.richTextBox1.AppendText("   }\n");
            this.richTextBox1.AppendText("}\n");
        }

        private void ToolStripMenuItem20_Click(object sender, EventArgs e)
        {
            getEntityFactoryCode();
        }

        private void getDBOpClass()
        {
            this.richTextBox1.AppendText("public class TSqlHelp\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("    SqlConnection cn = null;\n");

            this.richTextBox1.AppendText("    public TSqlHelp()\n");
            this.richTextBox1.AppendText("    {\n");
            this.richTextBox1.AppendText("        cn = new SqlConnection(" + (char)(34) + "Server=.;DataBase=" + txtDataBase.Text.Trim() + ";UID=" + txtUID.Text.Trim() + ";PWD="+ txtPWD.Text.Trim() +"" + (char)(34) + ");\n");
            this.richTextBox1.AppendText("    }\n\n");

            this.richTextBox1.AppendText("    public DataTable ExecuteQuery(string sql)\n");
            this.richTextBox1.AppendText("    {\n");
            this.richTextBox1.AppendText("        SqlCommand cmd = new SqlCommand(sql, cn);\n");
            this.richTextBox1.AppendText("        SqlDataAdapter da = new SqlDataAdapter(cmd);\n");
            this.richTextBox1.AppendText("        DataTable dt = new DataTable();\n");
            this.richTextBox1.AppendText("        da.Fill(dt);\n");
            this.richTextBox1.AppendText("        return dt;\n");
            this.richTextBox1.AppendText("     }\n\n");

            this.richTextBox1.AppendText("    public void ExecuteNonQuery(string sql)\n");
            this.richTextBox1.AppendText("    {\n");
            this.richTextBox1.AppendText("        SqlCommand cmd = new SqlCommand(sql, cn);\n");
            this.richTextBox1.AppendText("        cn.Open();\n");
            this.richTextBox1.AppendText("        cmd.ExecuteNonQuery();\n");
            this.richTextBox1.AppendText("        cn.Close();\n");
            this.richTextBox1.AppendText("    }\n\n");

            this.richTextBox1.AppendText("}\n\n");
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            getDBOpClass();
        }
    }

}