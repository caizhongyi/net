using System;
using System.Collections;

using FluorineFx.Data.Assemblers;

namespace ServiceLibrary
{
    class BuddyAssembler : Assembler
    {
        private BuddyDAO _buddyDAO;

        public BuddyAssembler()
        {
            _buddyDAO = new BuddyDAO();
        }

        public override IList Fill(IList fillParameters)
        {
            return _buddyDAO.GetBuddies(fillParameters[0] as string);
        }

        public override void CreateItem(object item)
        {
        }

        public override void DeleteItem(object previousVersion)
        {            
        }

        public override void UpdateItem(object newVersion, object previousVersion, IList changes)
        {
        }

        public override object GetItem(IDictionary identity)
        {
            return null;
        }
    }
}
