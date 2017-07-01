using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace ServiceLibrary
{
    class BuddyDAO
    {
        public IList GetBuddies(string userId)
        {
            ArrayList list = new ArrayList();

            using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["MessengerConnectionString"].ConnectionString))
            {
                OleDbCommand command = new OleDbCommand("SELECT buddy_id FROM buddy WHERE user_id='" + userId + "'", connection);
                connection.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Buddy buddy = new Buddy();
                        buddy.userId = userId;
                        buddy.buddyId = reader.GetString(0);
                        list.Add(buddy);
                    }
                }
            }
            return list;
        }
    }
}
