using System;
using System.Collections;

using FluorineFx;

namespace ServiceLibrary
{
    [RemotingService]
    class TestService
    {
        public IList Test(IList list)
        {
            return list;
        }
    }
}
