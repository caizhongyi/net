using System.Collections.Generic;
using JuSNS.Factory.Other;
using JuSNS.Model;


namespace JuSNS.Home.Other
{
    public class Constellation
    {
        static readonly private Constellation _instance = new Constellation();
        JuSNS.Factory.Other.IConstellation dal;
        private Constellation()
        {
            dal = DataAccess.CreateConstellation();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Constellation Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 得到星座列表
        /// </summary>
        /// <returns></returns>
        public List<ConstellationInfo> GetList()
        {
            return dal.GetList();
        }

        public ConstellationInfo GetInfo(object cid)
        {
            return dal.GetInfo(cid);
        }
    }
}