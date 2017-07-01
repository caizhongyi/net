using System;
using System.Collections.Generic;
using System.Data;
using JuSNS.Factory.Other;
using JuSNS.Profile;
using JuSNS.Model;

namespace JuSNS.SQLServer.Other
{
    public class Constellation : DbBase, IConstellation
    {
        public List<ConstellationInfo> GetList()
        {
            List<ConstellationInfo> list = new List<ConstellationInfo>();
            string Sql = "select ID,Constellation from NT_constellation order by ID asc";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                ConstellationInfo info = new ConstellationInfo();
                info.Constellation = rd["Constellation"].ToString();
                info.Id = Convert.ToInt32(rd["ID"]);
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public ConstellationInfo GetInfo(object cid)
        {
            ConstellationInfo info = new ConstellationInfo();
            string sql = "select * from NT_Constellation where id=" + cid;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                info.Constellation = dr["Constellation"].ToString();
                info.Id = Convert.ToInt32(cid);
            }
            dr.Close();
            return info;
        }
    }
}
