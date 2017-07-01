using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Shop
    {
        static readonly private Shop _instance = new Shop();
        JuSNS.Factory.App.IShop dal;
        private Shop()
        {
            dal = DataAccess.CreateShop();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Shop Instance
        {
            get { return _instance; }
        }


        /// <summary>
        /// 得到指定商品信息
        /// </summary>
        /// <param name="gid">商品的ID</param>
        /// <returns>ShopGoodsInfo实体类</returns>
        public ShopGoodsInfo GetGoodsInfo(object gid)
        {
            return dal.GetGoodsInfo(gid);
        }
        /// <summary>
        /// 得到指定商品分类信息
        /// </summary>
        /// <param name="gid">商品的ID</param>
        /// <returns>ShopClassInfo实体类</returns>
        public ShopClassInfo GetShopClassInfo(object cid)
        {
            return dal.GetShopClassInfo(cid);
        }

        /// <summary>
        /// 得到商品/商铺分类
        /// </summary>
        /// <param name="parentid">父类ID</param>
        /// <returns>返回LIST集合</returns>
        public List<ShopClassInfo> GetShopClass(int parentid)
        {
            return dal.GetShopClass(parentid);
        }

        /// <summary>
        /// 添加或更新商品
        /// </summary>
        /// <param name="info">商品实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertShopGoods(ShopGoodsInfo info)
        {
            return dal.InsertShopGoods(info);
        }

        /// <summary>
        /// 得到店铺ID
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>0为无店铺，大于表示店铺ID</returns>
        public int GetShopID(int userid)
        {
            return dal.GetShopID(userid);
        }

        /// <summary>
        /// 插入店铺或修改店铺
        /// </summary>
        /// <param name="info">店铺实体类</param>
        /// <returns>返回店铺ID</returns>
        public int InsertShop(ShopInfo info)
        {
            return dal.InsertShop(info);
        }

        /// <summary>
        /// 得到店铺信息(通过ID)
        /// </summary>
        /// <param name="sid">店铺ID</param>
        /// <returns></returns>
        public ShopInfo GetShopForID(object sid)
        {
            return dal.GetShopForID(sid);
        }

        /// <summary>
        /// 得到店铺信息(通过用户ID)
        /// </summary>
        /// <param name="userid">店铺ID</param>
        /// <returns></returns>
        public ShopInfo GetShopForUserID(object userid)
        {
            return dal.GetShopForUserID(userid);
        }

        /// <summary>
        /// 得到店铺列表
        /// </summary>
        /// <param name="number">得到数量</param>
        /// <param name="isrec">0普通，1推荐</param>
        /// <returns>LIST列表</returns>
        public List<ShopInfo> GetShopList(int number, int isrec)
        {
            return dal.GetShopList(number, isrec);
        }

        /// <summary>
        /// 得到品牌列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="isrec">0普通，1推荐</param>
        /// <returns>LIST列表</returns>
        public List<ShopGoodsInfo> GetGoodsList(int number, int isrec)
        {
            return dal.GetGoodsList(number, isrec);
        }

        /// <summary>
        /// 得到团购列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="isrec">0普通，1推荐</param>
        /// <returns>LIST列表</returns>
        public List<ShopMulteBuyInfo> GetMulteList(int number, int isrec)
        {
            return dal.GetMulteList(number, isrec);
        }


        /// <summary>
        /// 更改商品状态
        /// </summary>
        /// <param name="gid">商品ID</param>
        /// <param name="gtype">0点击率率，1顶一下，2踩一下，3增加累计销售量</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateGoodsState(int gid, int gtype)
        {
            return dal.UpdateGoodsState(gid, gtype);
        }

        /// <summary>
        /// 得到制定用户的商品列表
        /// </summary>
        /// <param name="number">调用数</param>
        /// <param name="userid">用户ID</param>
        /// <returns>商品列表LIST实例类</returns>
        public List<ShopGoodsInfo> GetUserGoodsList(int number, int userid)
        {
            return dal.GetUserGoodsList(number, userid);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteGoods(int infoid, int userid)
        {
            return dal.DeleteGoods(infoid, userid);
        }
        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="infoid">店铺ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteShop(int infoid, int userid)
        {
            return dal.DeleteShop(infoid, userid);
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="infoid">ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteShopClass(int infoid, int userid)
        {
            return dal.DeleteShopClass(infoid, userid);
        }

        /// <summary>
        /// 插入分类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertShopClass(ShopClassInfo info)
        {
            return dal.InsertShopClass(info);
        }

        /// <summary>
        /// 加入团购活动
        /// </summary>
        /// <param name="mid">团购ID</param>
        /// <param name="uid">加入者用户ID</param>
        /// <param name="cont">联系方式</param>
        /// <returns>0失败，1成功</returns>
        public int JoinMulte(int mid, int uid, string cont)
        {
            return dal.JoinMulte(mid, uid, cont);
        }

        /// <summary>
        /// 删除团购信息
        /// </summary>
        /// <param name="infoid">团购ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteMulte(int infoid, int userid)
        {
            return dal.DeleteMulte(infoid, userid);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteOrder(int infoid, int userid)
        {
            return dal.DeleteOrder(infoid, userid);
        }

        /// <summary>
        /// 插入商品购买订单号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InserShopOrder(ShopOrderInfo info)
        {
            return dal.InserShopOrder(info);
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <returns></returns>
        public int UpdateShopOrder(int oid)
        {
            return dal.UpdateShopOrder(oid);
        }

        /// <summary>
        /// 为订单发货
        /// </summary>
        /// <param name="ordernumber">订单编号</param>
        /// <param name="gid">商品ID</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public int PostOrder(string ordernumber,int gid,int orderid,int uid)
        {
            return dal.PostOrder(ordernumber, gid, orderid, uid);
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="orderid">订单ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public int ReviceOrder(int orderid, int uid)
        {
            return dal.ReviceOrder(orderid, uid);
        }

        /// <summary>
        /// 得到指定评论的信息
        /// </summary>
        /// <param name="id">评论的ID</param>
        /// <returns>GoodsCommentInfo实体类</returns>
        public GoodsCommentInfo GetGoodsCommentInfo(object id)
        {
            return dal.GetGoodsCommentInfo(id);
        }

        /// <summary>
        /// 插入商品/店铺评论
        /// </summary>
        /// <param name="info">评论实体类</param>
        /// <returns>1成功，0失败</returns>
        public int InsertShopComment(GoodsCommentInfo info)
        {
            return dal.InsertShopComment(info);
        }

        /// <summary>
        /// 删除商品评论
        /// </summary>
        /// <param name="cid">被删除的评论ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>1成功，0失败</returns>
        public int DeleteShopComment(int cid, int userid)
        {
            return dal.DeleteShopComment(cid, userid);
        }

        /// <summary>
        /// 插入商品积分
        /// </summary>
        /// <param name="info">商品积分评论实体类</param>
        /// <returns>0失败，1成功，-1已经评论过了。</returns>
        public int InsertUserComment(ShopUserCommentInfo info)
        {
            return dal.InsertUserComment(info);
        }

        /// <summary>
        /// 得到某个商品的平均积分
        /// </summary>
        /// <param name="gid">商品ID</param>
        /// <returns>平均分</returns>
        public double GetGoodsSore(int gid)
        {
            return dal.GetGoodsSore(gid);
        }

        /// <summary>
        /// 得到是否打个评分
        /// </summary>
        /// <param name="gid">商品ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回True或False</returns>
        public bool IsShopUserComment(int gid, int userid)
        {
            return dal.isShopUserComment(gid, userid);
        }

        /// <summary>
        /// 得到指定的团购信息
        /// </summary>
        /// <param name="mid">团购ID</param>
        /// <returns>ShopMulteBuyInfo实体类</returns>
        public ShopMulteBuyInfo GetMulteBuyInfo(object mid)
        {
            return dal.GetMulteBuyInfo(mid);
        }

        /// <summary>
        /// 插入更新团购信息
        /// </summary>
        /// <param name="info">团购实体类</param>
        /// <returns>返回的ID</returns>
        public int InsertMulteBuy(ShopMulteBuyInfo info)
        {
            return dal.InsertMulteBuy(info);
        }

        /// <summary>
        /// 得到公告基本信息
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public ShopNewsInfo GetShopNewsInfo(int nid)
        {
            return dal.GetShopNewsInfo(nid);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public int DeleteShopNews(int nid)
        {
            return dal.DeleteShopNews(nid);
        }

        /// <summary>
        /// 插入店铺公告
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertShopNews(ShopNewsInfo info)
        {
            return dal.InsertShopNews(info);
        }

        public int UpdateShopNewsClicks(int nid)
        {
            return dal.UpdateShopNewsClicks(nid);
        }
    }
}
