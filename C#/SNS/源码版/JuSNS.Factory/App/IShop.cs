using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;


namespace JuSNS.Factory.App
{
    public interface IShop
    {
        ShopGoodsInfo GetGoodsInfo(object gid);
        ShopClassInfo GetShopClassInfo(object cid);
        List<ShopClassInfo> GetShopClass(int parentid);
        int InsertShopGoods(ShopGoodsInfo info);
        int GetShopID(int userid);
        int InsertShop(ShopInfo info);
        ShopInfo GetShopForID(object sid);
        ShopInfo GetShopForUserID(object userid);
        List<ShopInfo> GetShopList(int number, int isrec);
        List<ShopGoodsInfo> GetGoodsList(int number, int isrec);
        List<ShopMulteBuyInfo> GetMulteList(int number, int isrec);
        List<ShopGoodsInfo> GetUserGoodsList(int number, int userid);
        int DeleteGoods(int infoid, int userid);
        int DeleteShop(int infoid, int userid);
        int DeleteShopClass(int infoid, int userid);
        int InsertShopClass(ShopClassInfo info);
        int JoinMulte(int mid, int uid, string cont);
        int DeleteMulte(int infoid, int userid);
        int DeleteOrder(int infoid, int userid);
        int InserShopOrder(ShopOrderInfo info);
        int UpdateShopOrder(int oid);
        int PostOrder(string ordernumber, int gid, int orderid, int uid);
        int ReviceOrder(int orderid, int uid);
        GoodsCommentInfo GetGoodsCommentInfo(object id);
        int InsertShopComment(GoodsCommentInfo info);
        int DeleteShopComment(int cid, int userid);
        int UpdateGoodsState(int gid, int gtype);
        int InsertUserComment(ShopUserCommentInfo info);
        double GetGoodsSore(int gid);
        bool isShopUserComment(int gid, int userid);
        ShopMulteBuyInfo GetMulteBuyInfo(object mid);
        int InsertMulteBuy(ShopMulteBuyInfo info);
        ShopNewsInfo GetShopNewsInfo(int nid);
        int InsertShopNews(ShopNewsInfo info);
        int UpdateShopNewsClicks(int nid);
        int DeleteShopNews(int nid);
    }

    public sealed partial class DataAccess
    {
        public static IShop CreateShop()
        {
            string className = path + ".App.Shop";
            object objType = JuSNS.Common.DataCache.GetCache(className);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(className);
                    JuSNS.Common.DataCache.SetCache(className, objType);// 写入缓存
                }
                catch { }
            }
            return (IShop)objType;
        }
    }
}
