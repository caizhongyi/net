using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL.Model;

namespace DAL.Dao
{
    public interface IAdvIssueManage
    {
        bool AddAdvIssue(AdvIssue ai,int advid,int wbid,int advtypeid);
        bool DelAdvIssue(int advid, int wbid, int advtypeid);
        bool ModAdvIssue(AdvIssue ai, int advid, int wbid, int advtypeid);
        DataSet SelAdvIssue();
    }
}
