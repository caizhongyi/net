using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory.Other;
using JuSNS.Profile;
using JuSNS.Config;
using JuSNS.Model;

namespace JuSNS.SQLServer.Other
{
    public class Area : DbBase, IArea
    {
        /// <summary>
        /// 获取省市列表
        /// </summary>
        /// <returns></returns>
        public List<DictAreaInfo> GetArea()
        {
            List<DictAreaInfo> list = new List<DictAreaInfo>();
            string Sql = "select ID,ParentID,Name,islock from NT_Dict_Area where IsLock=0 order by ID asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                DictAreaInfo info = new DictAreaInfo(rd.GetInt32(0), rd.GetInt32(1), rd.GetString(2), rd.GetBoolean(3));
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        /// <summary>
        /// 取得城市列表
        /// </summary>
        /// <param name="ParertID"></param>
        /// <returns></returns>
        public List<DictAreaInfo> CityList(int ParertID)
        {
            List<DictAreaInfo> list = new List<DictAreaInfo>();
            string Sql = "select ID,ParentID,Name,islock from NT_Dict_Area where IsLock=0 And ParentID=" + ParertID + " order by id asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                DictAreaInfo info = new DictAreaInfo(rd.GetInt32(0), rd.GetInt32(1), rd.GetString(2), rd.GetBoolean(3));
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        /// <summary>
        /// 取得某个省市详细信息
        /// </summary>
        /// <param name="id">省市编号</param>
        /// <returns></returns>
        public DictAreaInfo GetAreaInfo(int id)
        {
            DictAreaInfo info = new DictAreaInfo();
            string Sql = "select ID,ParentID,Name,IsLock from NT_Dict_Area where ID=" + id;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                info.ID = Convert.ToInt32(rd["ID"]);
                info.ParentID = Convert.ToInt32(rd["ParentID"]);
                if (rd["Name"] != DBNull.Value)
                    info.Name = rd["Name"].ToString();
                else
                    info.Name = string.Empty;
                info.IsLock = Convert.ToBoolean(rd["IsLock"]);
            }
            rd.Close();
            return info;
        }


        /// <summary>
        /// 根据名称取得地区ID
        /// </summary>
        /// <param name="AreaName">地域名称</param>
        /// <returns></returns>
        public int GetAreaID(string AreaName)
        {
            SqlParameter parm = new SqlParameter("AreaName", SqlDbType.NVarChar);
            parm.Value = AreaName;
            string sql = "select ID from NT_Dict_area where [name]=@AreaName";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, parm);
            if (rd.Read())
            {
                rd.Close();
                return Convert.ToInt32(rd[0]);
            }
            else
            {
                rd.Close();
                return 0;
            }
        }

        public List<VocationInfo> GetVotionList()
        {
            List<VocationInfo> list = new List<VocationInfo>();
            string Sql = "select ID,VocName,IsLock from NT_dict_vocation where islock=0 order by ID asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                VocationInfo info = new VocationInfo();
                info.VocName = rd["VocName"].ToString();
                info.ID = Convert.ToInt32(rd["ID"]);
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public int InsertArea(DictAreaInfo info)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ID", SqlDbType.Int);
            param[0].Value = info.ID;
            param[1] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
            param[1].Value = info.IsLock;
            param[2] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            param[2].Value = info.Name;
            param[3] = new SqlParameter("@ParentID", SqlDbType.Int);
            param[3].Value = info.ParentID;
            string sql = string.Empty;
            if (info.ID > 0)
            {
                sql = "update NT_Dict_Area set name=@Name,parentid=@ParentID where ID=@ID";
            }
            else
            {
                sql = "insert into NT_Dict_Area(ParentID, Name, IsLock)values(@ParentID, @Name, @IsLock)";
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        public int DeleteArea(int aid, int uid)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CnString);
            cn.Open();
            try
            {
                string sql = "delete from NT_Dict_Area where id=" + aid;
                return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            } 
        }
    }
}
