using System;
using System.Collections.Generic;
using System.Text;
using DAL.DataBaseInterface;
using DAL.DataBaseOperate;

namespace DAL.DataBaseFactory
{
    public class EntityFactory
    {
        public static IAd_WB getAd_WB()
        {
            return new Ad_WB();
        }
        public static IAdType getAdType()
        {
            return new AdType();
        }
        public static IUserInfo getUserInfo()
        {
            return new UserInfo();
        }
        public static IWBInfo getWBInfo()
        {
            return new WBInfo();
        }
        public static IClientInfo getClientInfo()
        {
            return new ClientInfo();
        }
        public static IWB_Extension getWB_Extension()
        {
            return new WB_Extension();
        }
        public static IAdInfo getAdInfo()
        {
            return new AdInfo();
        }
        public static IClient_Wb getClient_Wb()
        {
            return new Client_Wb();
        }
        public static Isysdiagrams getsysdiagrams()
        {
            return new sysdiagrams();
        }
    }
}
