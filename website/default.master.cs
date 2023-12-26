using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DefaultMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.Convert.ToBoolean(Session["authenticated"]))
        {
            AccountLink.NavigateUrl = "~/bank/main.aspx";
            AccountLink.Text = "MY ACCOUNT";
            AccountLink.ToolTip = "Click here to access a summary view of your banking accounts with Altoro Mutual.";
        }
        else
        {
            AccountLink.NavigateUrl = "~/bank/login.aspx";
            AccountLink.Text = "ONLINE BANKING LOGIN";
            AccountLink.ToolTip = "You do not appear to have authenticated yourself with the application.  Click here to enter your valid username and password.";
        }

        string sFile = Request.QueryString["content"];
        if (sFile != null)
        {
            if (sFile.StartsWith("personal"))
            {
                CatLink1.CssClass = "focus";
            }
            else if (sFile.StartsWith("business"))
            {
                CatLink2.CssClass = "focus";
            }
            else if (sFile.StartsWith("inside") || sFile.StartsWith("pr/") || sFile.StartsWith("jobs/"))
            {
                CatLink3.CssClass = "focus";
            }
        }
    }
}
