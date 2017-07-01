using System;
namespace BBL.Inface
{
    public interface IAdvIssueInfo
    {
        bool DelAdvIssuebyID(int advid, int wbid, int advtypeid);
        bool InsertAdvIssue(DAL.Model.AdvIssue advIssue, int advid, int wbid, int advtypeid);
        System.Data.DataSet SelectAdvIssue();
        bool UpdateAdvIssue(DAL.Model.AdvIssue advIssue, int advid, int wbid, int advtypeid);
    }
}
