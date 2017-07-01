using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace DAL.Dao.Customer
{
    public interface IHyRecord
    {
        bool InsertHyInfo(HyRecord hr);
        string IsHy(string userid);
        DataSet CompanyXzList();
        DataTable dtSelHyRecordInfoByUserId(string UserId);
        int UpdateHyRecordInfoByUserId(HyRecord hyRecordInfo);
        int UpDateHyRecordInfoAuditingByUserId(string UserId, int isMember);
        DataTable dtSelHyRecordInfoAndT_CompanXzTypeByUserId(int Auditing);
    }
}
