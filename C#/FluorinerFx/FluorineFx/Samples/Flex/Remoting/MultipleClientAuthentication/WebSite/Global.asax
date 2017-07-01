<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    if (ticket.UserData != null && !ticket.UserData.StartsWith(FluorineFx.Context.FluorineContext.FluorineTicket))
                    {
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                    }
                    else
                    {
                        //Let Fluorine to manage the UserData of the ticket
                        
                        //If aspx pages need the correct principal object then read it from the cache and set
                        //This is required as for non-remoting aspx pages Fluorine will not restore the principal
                        System.Security.Principal.IPrincipal principal = HttpContext.Current.Cache[ticket.UserData] as System.Security.Principal.IPrincipal;
                        HttpContext.Current.User = principal;
                    }
                }
            }
        }
    }    
           
</script>
