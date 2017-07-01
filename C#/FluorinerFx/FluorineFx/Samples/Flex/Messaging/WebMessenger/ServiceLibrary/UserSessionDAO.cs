using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace ServiceLibrary
{
    class UserSessionDAO
    {
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MessengerConnectionString"].ConnectionString;
            return connectionString;
        }

	    public IList GetList() 
        {
            ArrayList list = new ArrayList();

            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT user_id, status FROM [session]", connection);
                connection.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserSession session = new UserSession();
                        session.userId = reader.GetString(0);
                        session.status = reader.GetString(1);
                        list.Add(session);
                    }
                }
            }
            return list;
        }

	    public UserSession GetItem(string userId)
        {
            UserSession session = null;
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT user_id, status FROM [session] WHERE session_id='"+userId+"'", connection);
                connection.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        session = new UserSession();
                        session.userId = reader.GetString(0);
                        session.status = reader.GetString(1);
                    }
                }
            }
		    return session;
        }

	    public UserSession GetSessionById(string sessionId) 
        {
            UserSession session = null;
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT user_id, status FROM [session] WHERE session_id='"+sessionId+"'", connection);
                connection.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        session = new UserSession();
                        session.userId = reader.GetString(0);
                        session.status = reader.GetString(1);
                    }
                }
            }
		    return session;
        }		
	

    	public IList GetBuddySessions(string userId) 
        {
            ArrayList list = new ArrayList();

            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT buddy.buddy_id, [session].status FROM [buddy] INNER JOIN [session] ON [session].user_id=[buddy].buddy_id WHERE [buddy].user_id='" + userId + "'", connection);
                connection.Open();
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserSession session = new UserSession();
                        session.userId = reader.GetString(0);
                        if( !reader.IsDBNull(1) )
                            session.status = reader.GetString(1);
                        list.Add(session);
                    }
                }
            }
            return list;
	    }

	    public UserSession Update(UserSession session) 
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );

				command.CommandText = @"UPDATE [session] SET status = ? WHERE user_id = ? ";
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("status", System.Data.OleDb.OleDbType.VarWChar, 50, "status")).Value = session.status;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("user_id", System.Data.OleDb.OleDbType.VarWChar, 20, "user_id")).Value = session.userId;
				int rowsAffected = command.ExecuteNonQuery();
			}
            return session;
        }

	    public void Delete(UserSession session)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );
                command.CommandText = "DELETE FROM [session] WHERE user_id='"+session.userId+"'";
				int rowsAffected = command.ExecuteNonQuery();
            }
        }

	    public void DeleteAll()
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );
                command.CommandText = "DELETE FROM [session]";
				int rowsAffected = command.ExecuteNonQuery();
            }
        }

        public UserSession Create(UserSession session, string sessionId)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "INSERT INTO [session] (session_id, user_id, status) VALUES (@session_id, @user_id, @status)";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.Add("@session_id", OleDbType.VarWChar, 100).Value = sessionId;
                command.Parameters.Add("@user_id", OleDbType.VarWChar, 20).Value = session.userId;
                command.Parameters.Add("@status", OleDbType.VarWChar, 50).Value = session.status;
                command.ExecuteNonQuery();
            }
            return session;
        }


        public bool Logon(string userId, string password)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(null, connection);
                //command.CommandText = "SELECT count(*) FROM [user] WHERE user_id='" + userId + "' AND password='" + password + "'";
                //Well...MS Access here
                command.CommandText = "SELECT count(*) FROM [user] WHERE StrComp(user_id, '" + userId + "', 0) = 0 AND StrComp(password, '" + password + "', 0) = 0";
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    int count = reader.GetInt32(0);
                    return count == 1;
                }
            }
        }
    }
}
