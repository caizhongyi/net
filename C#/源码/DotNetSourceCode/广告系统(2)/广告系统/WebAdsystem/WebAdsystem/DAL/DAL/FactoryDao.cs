using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.impl;

namespace DAL
{
    public class FactoryDao
    {
        public static IUserInfoManage GetUserManage()
        {
            return new UserInfoManageDao();
        }
        public static IWbRankManage GetWbRankManage()
        {
            return new WbRankManageDao();
        }
        public static IAdvUnitpriceManage GetAdvUnitpriceManage()
        {
            return new AdvUnitpriceManageDao();
        }
        public static IAdvTypeManage GetAdvTypeManage()
        {
            return new AdvTypeManageDao();
        }
        public static IProvinceListManage GetProvinceListManage()
        {
            return new ProvinceListManageDao();
        }
        public static IAreaManage GetAreaManage()
        {
            return new AreaManageDao();
        }
        public static IUserTypeManage GetUserTypeManage()
        {
            return new UserTypeManageDao();
        }
        public static IAdvInfoManage GetAdvInfoManage()
        {
            return new AdvInfoManageDao();
        }
        public static IWbListManage GetWbListManage()
        {
            return new WbListManageDao();
        }
        public static IAdvIssueManage GetAdvIssueManage()
        {
            return new AdvIssueManageDao();
        }
    }
}
