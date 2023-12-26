using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Altoro
{
  /// <summary>
  /// Summary description for WebForm1.
  /// </summary>
  public partial class Apply : System.Web.UI.Page
  {
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      if (!(System.Convert.ToBoolean(Session["authenticated"])))
      {
                Server.Transfer("logout.aspx");        
      }

            HttpCookie CreditOffer = Request.Cookies["amCreditOffer"];

            string cLimit = "";
            string cInterest = "";
            string cType = "";
            
            if (CreditOffer != null)
            {
                cLimit = CreditOffer["Limit"];
                cInterest = CreditOffer["Interest"];
                cType = CreditOffer["CardType"];
            }

            string contentMsg = "";

            if (Request.HttpMethod == "POST")
            {
                // Normal application would process credit request here
                contentMsg = "Your new Altoro Mutual " + cType + " VISA with a $" + cLimit + " and " + cInterest + "% APR will be sent in the mail.";
            }
            else
            {
                contentMsg = "<p><b>No application is needed.</b>";
                contentMsg += "To approve your new $" + cLimit;
                contentMsg += " Altoro Mutual " + cType + " Visa<br />";
                contentMsg += "with an " + cInterest + "% APR simply ";
                contentMsg += "enter your password below.</p>";
                contentMsg += "<form method=\"post\" name=\"Credit\" action=\"apply.aspx\">";
                contentMsg += "<table border=0><tr><td>Password:</td><td>";
                contentMsg += "<input type=\"password\" name=\"passwd\"></td></tr>";
                contentMsg += "<tr><td></td><td><input type=\"submit\" name=\"Submit\" value=\"Submit\"></td></tr></table></form>";
            }
            lblMessage.Text = contentMsg;
            lblType.Text = cType;
    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit(e);
    }
    
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {    

    }
    #endregion
  }
}
