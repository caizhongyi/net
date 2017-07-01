using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Threading;
using System.Reflection.Emit;


 /// <summary>
/// DataSet扩展类
/// </summary>
public static class DataSetExtender
{  //增加string的方法, CheckCnCharacter

    /// <summary>
    /// 数据集转化为json字符
    /// </summary>
    /// <param name="_ds">数据集</param>
    /// <param name="path">路径 示例/: AppDomain .CurrentDomain.BaseDirectory/Directory/FileName.json</param>
    /// <param name="totalCount">总条数</param>
    /// <returns>json字符</returns>
    public static void ToWriteJson(this DataSet _ds, int totalCount, string path)
    {

        string jsonstr = _ds.ToJson(totalCount);
        if (File.Exists(path))
        {
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(jsonstr);
            sw.Close();
            fs.Close();
        }


    }
    /// <summary>
    /// 数据集转化为json字符
    /// </summary>
    /// <param name="_ds">数据集</param>
    /// <param name="path">路径 示例/: AppDomain .CurrentDomain.BaseDirectory/Directory/FileName.json</param>
    /// <param name="totalCount">总条数</param>
    /// <param name="pageSize">页大小</param>
    /// <returns>json字符</returns>
    public static void ToWriteJson(this DataSet _ds, int totalCount, int pageSize, string path)
    {

        string jsonstr = _ds.ToJson(totalCount, pageSize);
        if (File.Exists(path))
        {
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(jsonstr);
            sw.Close();
            fs.Close();
        }


    }
    /// <summary>
    /// 数据集转化为json字符
    /// </summary>
    /// <param name="_ds">数据集</param>
    /// <param name="totalCount">总条数</param>
    /// <returns>json字符</returns>
    public static string ToJson(this DataSet _ds, int totalCount)
    {
        string jsonStr = "";
        jsonStr = "{TotalCount:\"" + totalCount + "\",Data:[";

        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
        {
            if (i + 1 > _ds.Tables[0].Rows.Count)
            {
                continue;
            }
            string row = @"{";
            for (int j = 0; j < _ds.Tables[0].Columns.Count; j++)
            {
                if (j == 0)
                {
                    row += string.Format("{0}:\"{1}\"", _ds.Tables[0].Columns[j].ColumnName, _ds.Tables[0].Rows[i][j].ToString());
                }
                else
                {
                    row += string.Format(",{0}:\"{1}\"", _ds.Tables[0].Columns[j].ColumnName, _ds.Tables[0].Rows[i][j].ToString());
                }
            }
            row += "}";
            if (i == 0)
            {
                jsonStr += row;
            }
            else
            {
                jsonStr += "," + row;
            }
        }
        jsonStr += "]";
        jsonStr += "}";


        return jsonStr;

    }
    /// <summary>
    /// 数据集转化为json字符
    /// </summary>
    /// <param name="_ds">数据集</param>
    /// <param name="totalCount">总条数</param>
    /// <param name="pageSize">页大小</param>
    /// <returns>json字符</returns>
    public static string ToJson(this DataSet _ds, int totalCount, int pageSize)
    {
        string jsonStr = "";
        jsonStr = "{TotalCount:\"" + totalCount + "\",PageSize:\"" + pageSize + "\",Data:[";

        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
        {
            if (i + 1 > _ds.Tables[0].Rows.Count)
            {
                continue;
            }
            string row = @"{";
            for (int j = 0; j < _ds.Tables[0].Columns.Count; j++)
            {
                if (j == 0)
                {
                    row += string.Format("{0}:\"{1}\"", _ds.Tables[0].Columns[j].ColumnName, _ds.Tables[0].Rows[i][j].ToString());
                }
                else
                {
                    row += string.Format(",{0}:\"{1}\"", _ds.Tables[0].Columns[j].ColumnName, _ds.Tables[0].Rows[i][j].ToString());
                }
            }
            row += "}";
            if (i == 0)
            {
                jsonStr += row;
            }
            else
            {
                jsonStr += "," + row;
            }
        }
        jsonStr += "]";
        jsonStr += "}";


        return jsonStr;

    }
    /// <summary>
    /// 数据集转化为json字符
    /// </summary>
    /// <param name="_ds">数据集</param>
    /// <returns>json字符</returns>
    public static string ToJson(this DataSet _ds)
    {
        string jsonStr = "";
        jsonStr = "[";

        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
        {
            if (i + 1 > _ds.Tables[0].Rows.Count)
            {
                continue;
            }
            string row = @"{";
            for (int j = 0; j < _ds.Tables[0].Columns.Count; j++)
            {
                if (j == 0)
                {
                    row += string.Format("{0}:\"{1}\"", _ds.Tables[0].Columns[j].ColumnName, _ds.Tables[0].Rows[i][j].ToString());
                }
                else
                {
                    row += string.Format(",{0}:\"{1}\"", _ds.Tables[0].Columns[j].ColumnName, _ds.Tables[0].Rows[i][j].ToString());
                }
            }
            row += "}";
            if (i == 0)
            {
                jsonStr += row;
            }
            else
            {
                jsonStr += "," + row;
            }
        }
        jsonStr += "]";

        return jsonStr;

    }
    /// <summary>
    /// 数据集转化为json字符[二维数组格式]
    /// </summary>
    /// <param name="_ds">数据集</param>
    /// <param name="totalCount">总条数</param>
    /// <returns>json字符</returns>
    public static string ToJsonArray(this DataSet _ds)
    {
        string jsonStr = "";
        jsonStr = "[";

        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
        {
            if (i + 1 > _ds.Tables[0].Rows.Count)
            {
                continue;
            }
            string row = @"[";
            for (int j = 0; j < _ds.Tables[0].Columns.Count; j++)
            {
                if (j == 0)
                {
                    row += string.Format("{0}", _ds.Tables[0].Rows[i][j].ToString());
                }
                else
                {
                    row += string.Format(",{0}", _ds.Tables[0].Rows[i][j].ToString());
                }
            }
            row += "]";
            if (i == 0)
            {
                jsonStr += row;
            }
            else
            {
                jsonStr += "," + row;
            }
        }
        jsonStr += "]";
        jsonStr += "}";


        return jsonStr;

    }

    #region DataTable
    public static List<TResult> TableToList<TResult>(this DataTable dt) where TResult : class, new()
    {
        List<TResult> list = new List<TResult>();

        if (dt == null) return list;
        int len = dt.Rows.Count;

        try
        {
            for (int i = 0; i < len; i++)
            {
                TResult info = new TResult();
                foreach (DataColumn dc in dt.Rows[i].Table.Columns)
                {
                    if (dt.Rows[i][dc.ColumnName] == null || string.IsNullOrEmpty(dt.Rows[i][dc.ColumnName].ToString())) continue;
                    System.Reflection.PropertyInfo pi = info.GetType().GetProperty(dc.ColumnName);
                    if (pi!=null)
                    {
                        pi.SetValue(info, dt.Rows[i][dc.ColumnName], null);
                    }
                }
                list.Add(info);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        dt.Dispose();
        dt = null;

        return list;


    }
    public static DataTable OrderBy(this DataTable dt, string orderBy)
    {
        dt.DefaultView.Sort = orderBy;
        return dt.DefaultView.ToTable();
    }

    public static DataTable Where(this DataTable dt, string where)
    {
        DataTable resultDt = dt.Clone();
        DataRow[] resultRows = dt.Select(where);
        foreach (DataRow dr in resultRows) resultDt.Rows.Add(dr.ItemArray);
        return resultDt;
    }
    public static List<TResult> OrderBy<TResult>(this DataTable dt, string orderBy) where TResult : class,new()
    {
        return dt.OrderBy(orderBy).TableToList<TResult>();
    }
    public static List<TResult> Where<TResult>(this DataTable dt, string where) where TResult : class,new()
    { return dt.Where(where).TableToList<TResult>(); }
    public static List<TResult> ToPage<TResult>(this DataTable dt, int pageIndex, int pageSize, out int totalRecords) where TResult : class,new()
    {
        totalRecords = dt.Rows.Count;
        int startRow = (pageIndex - 1) * pageSize;
        int endRow = startRow + pageSize;
        if (startRow > totalRecords || startRow < 0) { startRow = 0; endRow = pageSize; }
        if (endRow > totalRecords + pageSize) { startRow = totalRecords - pageSize; endRow = totalRecords; }
        DataTable dt2 = dt.Clone();
        for (int i = startRow; i < endRow; i++) { if (i >= totalRecords) break; dt2.Rows.Add(dt.Rows[i].ItemArray); }
        return dt2.TableToList<TResult>();
    }
    //public static T Get<T>(this DataRow row, string field) { return row.Get<T>(field, default(T)); }
    //public static T Get<T>(this DataRow row, string field, T defaultValue)
    //{
    //    var value = row[field];
    //    if (value == DBNull.Value) return defaultValue;
    //    return value.ConvertTo<T>(defaultValue);
    //}
    //public static T Get<T>(this DataRowView row, string field) { return row.Get<T>(field, default(T)); }
    //public static T Get<T>(this DataRowView row, string field, T defaultValue)
    //{
    //    var value = row[field]; if (value == DBNull.Value) return defaultValue;
    //    return value.ConvertTo<T>(defaultValue);
    //}
    #endregion
}




