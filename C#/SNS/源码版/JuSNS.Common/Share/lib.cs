using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using JuSNS.Model;

namespace JuSNS.Common.Share
{
    public class lib
    {
        /// <summary>
        /// 根目录
        /// </summary>
        public static string ExName
        {
            get
            {
                return Public.GetXMLValue("siteExName");
            }
        }

        /// <summary>
        /// 获得列表/此处修改了，需要修改JuSNS.Model 下的EnumeRates.cs的EnumShareType函数
        /// </summary>
        public static string GetShareType(EnumShareType ShareType)
        {
            string listSTR = string.Empty;
            switch (ShareType)
            {
                case EnumShareType.ForNews:
                    listSTR += "新闻";
                    break;
                case EnumShareType.ForBlog:
                    listSTR += "日志";
                    break;
                case EnumShareType.ForGroup:
                    listSTR += "群组";
                    break;
                case EnumShareType.ForAsk:
                    listSTR += "问答";
                    break;
                case EnumShareType.ForActive:
                    listSTR += "活动";
                    break;
                case EnumShareType.ForGoods:
                    listSTR += "商品";
                    break;
                case EnumShareType.ForShop:
                    listSTR += "店铺";
                    break;
                case EnumShareType.ForWeb:
                    listSTR += "网页";
                    break;
                case EnumShareType.ForFlash:
                    listSTR += "Flash";
                    break;
                case EnumShareType.ForMusic:
                    listSTR += "音乐";
                    break;
                case EnumShareType.ForVodie:
                    listSTR += "视频";
                    break;
                case EnumShareType.ForAlbum:
                    listSTR += "相册";
                    break;
                case EnumShareType.ForPhoto:
                    listSTR += "相片";
                    break;
                case EnumShareType.ForVote:
                    listSTR += "投票";
                    break;
                case EnumShareType.ForTopic:
                    listSTR += "帖子";
                    break;
                case EnumShareType.ForFriend:
                    listSTR += "朋友";
                    break;
                case EnumShareType.ForMulte:
                    listSTR += "团购";
                    break;
                case EnumShareType.ForOther:
                    listSTR += "其他";
                    break;
            }
            return listSTR;
        }

        public static string GetURL(EnumShareType ShareType, string q)
        {
            string listSTR = string.Empty;
            switch (ShareType)
            {
                case EnumShareType.ForNews:
                    listSTR = Common.Public.URLWrite(q, "news");
                    break;
                case EnumShareType.ForBlog:
                    listSTR = Common.Public.URLWrite(q, "blog");
                    break;
                case EnumShareType.ForGroup:
                    listSTR =Public.URLWrite(q, "group");
                    break;
                case EnumShareType.ForAsk:
                    listSTR = Common.Public.URLWrite(q, "ask");
                    break;
                case EnumShareType.ForActive:
                    listSTR = Common.Public.URLWrite(q, "ative");
                    break;
                case EnumShareType.ForGoods:
                    listSTR = Common.Public.URLWrite(q, "goods");
                    break;
                case EnumShareType.ForShop:
                    listSTR = Common.Public.URLWrite(q, "shop");
                    break;
                case EnumShareType.ForAlbum:
                    listSTR = Public.URLWrite(q, "album");
                    break;
                case EnumShareType.ForPhoto:
                    listSTR = Public.URLWrite(q, "photo");
                    break;
                case EnumShareType.ForVote:
                    listSTR = Public.URLWrite(q, "vote");
                    break;
                case EnumShareType.ForTopic:
                    listSTR = Public.URLWrite(q, "topic");
                    break;
                case EnumShareType.ForFriend:
                    listSTR = Common.Public.URLWrite(q, "user");
                    break;
                case EnumShareType.ForMulte:
                    listSTR = Common.Public.URLWrite(q, "multe");
                    break;
                default:
                    listSTR = Public.rootDir + "/url" + ExName + "?urls=" + q;
                    break;
            }
            return listSTR;
        }

    }
}
