using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using DAL.impl.Customer;
using DAL.Dao.Present;
using DAL.impl.Present;
using DAL.Dao.City;
using DAL.impl.City;
using DAL.impl.Integral;
using DAL.Dao.Integral;
using DAL.Dao.AdvInfo;
using DAL.impl.AdvInfo;
using DAL.Dao.Reserved;
using DAL.impl.Reserved;
using DAL.Dao;
using DAL.impl;

namespace DAL
{
    public class DALFactory
    {
        public static IUserLogin GetIsLogin()
        {
            return new UserLoginDao();
        }
        public static IPresent GetPresent()
        {
            return new PresentDao();
        }
        public static IUserRegist GetIsRegist()
        {
            return new UserRegistDao();
        }
        public static IUserInfoManage GetUserInfoManage()
        {
            return new UserInfoManageDao();
        }
        public static IProvinceInfo GetProvince()
        {
            return new ProvinceInfoDao();
        }
        public static ICityInfo GetCity()
        {
            return new CityInfoDao();
        }
        public static ICountryInfo GetCountry()
        {
            return new CountryInfoDao();
        }
        public static IAreaInfo GetArea()
        {
            return new AreaInfoDao();
        }
        public static IUserIdeaTypeInfo GetUserIdeaTypeInfo()
        {
            return new UserIdeaTypeInfoDao();
        }
        public static IUserIdeaInfo GetUserIdeaInfo()
        {
            return new UserIdeaInfoDao();
        }
        public static IIntegral GetIntegral()
        {
            return new IntegralDao();
        }
        public static IAdvInfo GetAdvInfo()
        {
            return new AdvInfoDao();
        }
        public static IAdvTypeInfo GetAdvTypeInfo()
        {
            return new AdvTypeInfoDao();
        }
        public static IAdvIssueInfo GetAdvIssueInfo()
        {
            return new AdvIssueInfoDao();
        }
        public static IAdvPriceInfo GetAdvPriceInfo() 
        {
            return new AdvPriceInfoDao();
        }

        public static Dao.IPersonalScoreInfo GetPersonalScoreInfo()
        {
            return new PersonalScoreInfo();
        }

        public static IUserPublishMsg GetUserPublishMsg()
        {
            return new UserPublishMsg();
        }
        public static IReserveInfo GetReserveInfo()
        {
            return new ReserveInfoDao();
        }
        public static Dao.Group.IGroupInfo GetGroupInfo()
        {
            return new impl.Group.Group();
        }
        public static Dao.Group.IGroup_CustomerInfo GetGroup_CustomerInfo()
        {
            return new impl.Group.Group_CustomerInfoDao();
        }

        public static IIssueAreaInfo GetIssueAreaInfo()
        {
            return new IssueAreaDao();
        }
        public static IHyRecord GetHyInfo()
        {
            return new HyRecordDao();
        }

        public static ITFindPassWord GetFindPassWord()
        {
            return new TFindPassWord();
        }
        public static IProfession GetProfession()
        {
            return new Profession();
        }
    }
}
