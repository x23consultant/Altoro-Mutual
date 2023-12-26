<script Language="C#" runat="server">

/*
Watchfire Inc.
Nov 2006
This asax is used to establish all application parameters. These are global
parameters and common to all users.  NOT user specific
It is an include only file.  It will not be shown.
*/

    void Application_Start(Object sender, EventArgs E)
    {
        //Wouldn't want to start the count at 0
        Application.Lock();
        Application["Visitors"] = Convert.ToInt32("10342");
        Application.UnLock();
    }

    void Application_End(Object sender, EventArgs E)
    {
        // Clean up application resources here
    }

    void Session_Start(Object sender, EventArgs E)
    {
        HttpCookie SessionId = Request.Cookies["amSessionId"];

        if (SessionId == null) {
	        SessionId = new HttpCookie("amSessionId");

	        Application.Lock();
	        Application["Visitors"] = Convert.ToInt32(Application["Visitors"]) + 3;
	        Application.UnLock();

	        SessionId.Value = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + Application["Visitors"].ToString();
	        Response.Cookies.Add(SessionId);
	      }

        Session["userId"] = "";
				Session["userName"] = "";
				Session["firstName"] = "";
				Session["lastName"] = "";
        Session["authenticated"] = false;

        // Can only get the ApplicationPath within the context of a request
        // This is why I get this on the session start instead of app start
        if (Application["rootPath"] == null)
        {
            Application.Lock();
            if (Request.ApplicationPath.Length == 1)
            {
                Application["rootPath"] = "/";
            }
            else
            {
                Application["rootPath"] = Request.ApplicationPath + "/";
            }
            Application.UnLock();
        }
	}

    void Session_End(Object sender, EventArgs E)
    {
        Session["userId"] = null;
				Session["firstName"] = null;
				Session["lastName"] = null;
				Session["userName"] =null;
				Session["authenticated"]=null;
    }

    void Application_BeginRequest(Object sender, EventArgs E)
    {
		// Code at each page request
    }

    void Application_EndRequest(Object sender, EventArgs E)
    {
		// Code at the end of each request
    }

    void Application_Error(Object sender, EventArgs E)
    {
        try
        {
            Exception errorInfo = Server.GetLastError();
            string message = errorInfo.InnerException.ToString();
            int firstChar = message.IndexOf(":", 1) + 2;
            int lastChar = (message.IndexOf("\n", firstChar)) - firstChar;
            string summary = message.Substring(firstChar, lastChar);
            Session["ErrorSummary"] = summary;
            Session["ErrorDetails"] = message;
            Server.ClearError();
        }
        catch (Exception err)
        {
            Session["ErrorSummary"] = "An unknown error occurred.";
            Session["ErrorSummary"] = "";
        }
        Server.Transfer("servererror.aspx");
	}

</script>

