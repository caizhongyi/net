using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.Integral
{
    public interface IIntegral
    {
        string InteralContent();
        double GetScoreNum(string PresetnId);
        double GetCurrNum(string CusId);
        double GetCurrGold(string CusId);
        int GetContributeNum(string CusId);
        DataSet GetInteralLog(string CusId);
        void DelIntegralLog(string id);
        void DelAllIntegralLog();
        DataSet GetInteralLogthrough(string CusId);
        DataSet GetInteralLogNotthrough(string CusId);
        DataSet GetInteralLogJJthrough(string cusid);

        string GetMonthNum(string CusId);
        string GetMonthScore(string CusId);
        string GetHisScore(string CusId);
        string GetHisNum(string CusId);
        DataSet GetYqInfo(string CusId);
    }
}
