using System.Collections.Generic;
using JuSNS.Factory.Other;
using JuSNS.Model;

namespace JuSNS.Home.Other
{
    public class Area
    {
        static readonly private Area _instance = new Area();
       JuSNS.Factory.Other.IArea dal;
       private Area()
       {
           dal = DataAccess.CreateArea();
       }

        /// <summary>
        /// 取得实例
        /// </summary>
       static public Area Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 得到省份列表
        /// </summary>
        /// <returns></returns>
        public List<DictAreaInfo> GetArea()
        {
            return dal.GetArea();
        }

        /// <summary>
        /// 得到城市列表
        /// </summary>
        /// <param name="ParertID"></param>
        /// <returns></returns>
        public List<DictAreaInfo> CityList(int ParertID)
        {
            return dal.CityList(ParertID);
        }

        /// <summary>
        /// 取得某个省市详细信息
        /// </summary>
        /// <param name="id">省市编号</param>
        /// <returns></returns>
        public DictAreaInfo GetAreaInfo(int id)
        {
            return dal.GetAreaInfo(id);
        }

        /// <summary>
        /// 根据名称取得地区ID
        /// </summary>
        /// <param name="AreaName">地域名称</param>
        /// <returns></returns>
        public int GetAreaID(string AreaName)
        {
            return dal.GetAreaID(AreaName);
        }

        /// <summary>
        /// 加载行业
        /// </summary>
        /// <returns></returns>
        public List<VocationInfo> GetVotionList()
        {
            return dal.GetVotionList();
        }

        /// <summary>
        /// 插入或者更新地区
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertArea(DictAreaInfo info)
        {
            return dal.InsertArea(info);
        }

        /// <summary>
        /// 删除地区
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteArea(int aid,int uid)
        {
            return dal.DeleteArea(aid, uid);
        }
    }
}
