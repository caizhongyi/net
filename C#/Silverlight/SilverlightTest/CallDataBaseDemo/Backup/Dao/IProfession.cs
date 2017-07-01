using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao
{
    public interface  IProfession
    {
        DataTable SelectPeopleNumByTownAndProfessionAndCompanyName(string townName);
        DataTable SelectPeopleNumByTownAndProfession(int professionID, string townName);
        int SelectIsCorporateMembersBytTownName(string townName);
    }
}
