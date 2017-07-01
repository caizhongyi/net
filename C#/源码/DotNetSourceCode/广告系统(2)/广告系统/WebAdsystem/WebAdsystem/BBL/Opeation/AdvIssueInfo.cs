using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BBL.Opeation
{
    class AdvIssueInfo : BBL.Inface.IAdvIssueInfo 
    {
        DAL.Dao.IAdvIssueManage AdvUssyeMange = DAL.FactoryDao.GetAdvIssueManage();
        public bool InsertAdvIssue(DAL.Model.AdvIssue advIssue, int advid, int wbid, int advtypeid)
        {
            bool i = AdvUssyeMange.AddAdvIssue(advIssue, advid, wbid, advtypeid);
            return i;
        }
        public bool UpdateAdvIssue(DAL.Model.AdvIssue advIssue, int advid, int wbid, int advtypeid)
        {
            bool i = AdvUssyeMange.ModAdvIssue(advIssue, advid, wbid, advtypeid);
            return i;
        }
        public bool DelAdvIssuebyID(int advid, int wbid, int advtypeid)
        {
            bool i=AdvUssyeMange.DelAdvIssue(advid,wbid,advtypeid);
            return i;
        }
        public DataSet SelectAdvIssue()
        {

            return AdvUssyeMange.SelAdvIssue() ;
        }
    }
}
