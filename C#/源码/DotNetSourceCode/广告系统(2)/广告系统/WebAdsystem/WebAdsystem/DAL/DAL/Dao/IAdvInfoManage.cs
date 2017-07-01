using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using System.Data;

namespace DAL.Dao
{
    public interface IAdvInfoManage
    {

        bool AddAdvInfo(AdvInfo ai,int masterid,int userid);
        bool DelAdvInfo(int id);
        bool ModAdvInfo(AdvInfo ai,int masterid,int userid);
        DataSet SelAdvInfo();
    }
}
