using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.Dao;

namespace DAL.impl
{
    class Profession : IProfession
    {
        Util util = new Util();
        /// <summary>
        /// 某地区所有行业的人数
        /// </summary>
        /// <param name="townName"></param>
        /// <returns></returns>
        public DataTable SelectPeopleNumByTownAndProfessionAndCompanyName(string townName)
        {
            string cmd = "select professionID, professionName,T_HyRecord.CyTypeID,sum(case when CyTYpeID is not null  and  countryName='" + townName + "' then 1 else 0 end  ) as peopleNum from T_Profession left join T_HyRecord on T_Profession.ProfessionID=T_HyRecord.CyTypeID left join T_customerInfo on T_CustomerInfo.CID=T_Hyrecord.UserID  group by professionID, professionName,T_HyRecord.CyTypeID";
            return util.GetDataTable(cmd);
        }
        
        /// <summary>
        /// 某地区某行业所有公司的人数
        /// </summary>
        /// <param name="professionID"></param>
        /// <param name="townName"></param>
        /// <returns></returns>
        public DataTable SelectPeopleNumByTownAndProfession(int professionID,string townName)
        {
            string cmd = "select CompanyName,T_Profession.professionName,T_CustomerInfo.countryName,T_HyRecord.CyTypeID,count(*) as peopleNum from T_Profession left join T_HyRecord on T_Profession.ProfessionID=T_HyRecord.CyTypeID   join T_CustomerInfo  on T_CustomerInfo.CID=T_HyRecord.UserID where    countryName='" + townName + "' and cyTypeID='" + professionID + "' group by CompanyName,T_Profession.professionName,T_CustomerInfo.countryName,T_HyRecord.CyTypeID";
            return util.GetDataTable(cmd);
        }

        //public DataTable SelectCompany_Prof
        public int SelectIsCorporateMembersBytTownName(string townName)
        {
            string cmd = "Select Count(*) from T_CustomerInfo where IsCorporateMembers=0";
            return Convert.ToInt32( util.GetDataTable(cmd).Rows[0].ItemArray[0]);
        }
     
    }
}
