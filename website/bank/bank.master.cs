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

public partial class BankMaster : System.Web.UI.MasterPage
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

        if ((Request.Cookies["amUserId"].Value == "1") && (Session["userName"].ToString() == "admin"))
        {
            Administration.Text = "<br style=\"line-height: 10px;\"/><b>ADMINISTRATION</b><ul class=\"sidebar\"><li><a href=\"../admin/application.aspx\">View Application Values</a></li><li><a href=\"../admin/admin.aspx\">Edit Users</a></li></ul>";
        }
    }
}
