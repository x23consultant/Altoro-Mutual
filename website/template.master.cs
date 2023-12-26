using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class TemplateMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
      // Used for Depth First Scanning on the front page survey
      Session["thisPage"] = Request.ServerVariables["URL"];
      if (Request.QueryString.ToString().Length > 0)
      {
        Session["thisPage"] += "?" + Request.QueryString.ToString();
      }

      if (System.Convert.ToBoolean(Session["authenticated"]))
      {
        LoginLink.NavigateUrl = "~/bank/logout.aspx";
        LoginLink.Text = "Sign Off";
        LoginLink.ToolTip = "Please click here to sign out of the Online Banking application.  You may also want to close your browser window.";
      }
      else
      {
        LoginLink.NavigateUrl = "~/bank/login.aspx";
        LoginLink.Text = "Sign In";
        LoginLink.ToolTip = "It does not appear that you have properly authenticated yourself.  Please click here to sign in.";
      }
    }
}
