using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL
{
    public  class DataBaseOpertion
    {  
        /// <summary>
        /// ��ѯ���ݿ��µı���
        /// </summary>
        /// <param name="dataBaseName">���ݿ���</param>
        /// <returns></returns>
        public static  DataTable SelectDataBaseTable(string dataBaseName)
        {
            string cmd = "select Name from " + dataBaseName + "..sysobjects where xtype='u' and status>=0";
            return Util.GetDataTable(cmd);
        }

        public static DataTable SelectDataTableColumns(string tableName)
        {
            string cmd = "select a.name as [column],b.name as type from syscolumns a,systypes b where a.id=object_id('Adv_Info') and a.xtype=b.xtype";
            return Util.GetDataTable(cmd);
          
        }
    }
}
