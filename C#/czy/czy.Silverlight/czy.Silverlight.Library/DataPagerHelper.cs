using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Data;

namespace czy.Silverlight.Library
{
    public class DataPagerHelper
    {
        #region SqlProc
        /*
            存储过程1——DataPager：
    Create PROCEDURE dbo.DataPager

    (

      @pagesize int,

      @currentpage int

    )

    AS

    declare @totalnum int

    set @totalnum = (select count(EmployeeID) from Employee)

    if @currentpage <= ceiling(convert(real,@totalnum)/convert(real,@pagesize))

    select * from 

    (select TOP (@pagesize) * FROM (SELECT TOP (@pagesize * @currentpage) * from Employee ORDER BY EmployeeID ASC) as A ORDER BY EmployeeID DESC) as B ORDER BY EmployeeID ASC

 

    存储过程2——GetTotalPagers：
    Create PROCEDURE dbo.GetTotalPagers

    @pagesize int

    AS

    declare @totalnum int

    set @totalnum = (select count(EmployeeID) from Employee)

    return (ceiling(convert(real,@totalnum)/convert(real,@pagesize)))
         * */
        #endregion

       
    }
}
