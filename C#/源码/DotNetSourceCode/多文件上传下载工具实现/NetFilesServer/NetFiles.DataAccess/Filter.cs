using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.DataAccess
{
    interface Filter
    {
        HFSoft.Data.Expressions.Expression GetFilter();
    }
}
