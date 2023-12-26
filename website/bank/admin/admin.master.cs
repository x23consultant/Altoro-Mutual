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

public partial class AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.Convert.ToBoolean(Session["authenticated"]))
        {
            AccountLink.NavigateUrl = "~/bank/main.aspx";
            AccountLink.Text = "MY ACCOUNT";
        }
        else
        {
            AccountLink.NavigateUrl = "~/bank/login.aspx";
            AccountLink.Text = "ONLINE BANKING LOGIN";
        }

    }
}
