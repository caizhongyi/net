using System;
using System.Collections;

using FluorineFx.Context;
using FluorineFx.Messaging;
using FluorineFx.Data;
using FluorineFx.Data.Assemblers;
using FluorineFx.Messaging.Api;

namespace ServiceLibrary
{
    class UserSessionAssembler : Assembler, ISessionListener
    {
        private UserSessionDAO _userSessionDAO;

        public UserSessionAssembler()
        {
            _userSessionDAO = new UserSessionDAO();
            _userSessionDAO.DeleteAll();
            ClientManager.AddSessionCreatedListener(this);
        }

        #region ISessionListener Members

        public void SessionCreated(IClient client)
        {
            client.AddSessionDestroyedListener(this);
        }

        public void SessionDestroyed(IClient client)
        {
            UserSession userSession = _userSessionDAO.GetSessionById(client.Id);
            if (userSession == null)
            {
                return;
            }
            DataServiceTransaction dtx = DataServiceTransaction.Begin();
            try
            {
                _userSessionDAO.Delete(userSession);
                dtx.DeleteItem("chat-sessions", userSession);
                dtx.RefreshFill("chat-sessions", null);
            }
            finally
            {
                dtx.Commit();
            }
        }

        #endregion


        public override IList Fill(IList fillParameters)
        {
            if (fillParameters.Count == 0)
                return _userSessionDAO.GetList();
            else
                return _userSessionDAO.GetBuddySessions(fillParameters[0] as string);
        }

        public override void CreateItem(Object item)
        {
            UserSession userSession = item as UserSession;
            if(!_userSessionDAO.Logon(userSession.userId, userSession.password))
            {
                throw new MessageException("Logon failed.");
            }
            userSession.password = string.Empty;
            try
            {
                _userSessionDAO.Create(userSession, FluorineContext.Current.ClientId);
            }
            catch (Exception e)
            {
                throw new MessageException("Unable to create user session.", e);
            }

            DataServiceTransaction dtx = DataServiceTransaction.Begin();
            dtx.RefreshFill("chat-sessions", null);
        }

        public override void UpdateItem(Object newVersion, Object prevVersion, IList changes)
        {
            _userSessionDAO.Update(newVersion as UserSession);
        }

        public override void DeleteItem(Object prevVersion)
        {
            _userSessionDAO.Delete(prevVersion as UserSession);
        }

        public override Object GetItem(IDictionary id)
        {
            string userId = id["userId"] as string;
            return _userSessionDAO.GetItem(userId);
        }
    }
}
