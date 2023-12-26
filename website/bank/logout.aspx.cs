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
  /// Summary description for logout.
  /// </summary>
  public partial class LogOut : System.Web.UI.Page
  {
    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      // Put user code to initialize the page here

            Session.Abandon();

	          DateTime dtNow = DateTime.Now;
	          TimeSpan tsDay = new TimeSpan(-1, 0, 0, 0);

            HttpCookie ThisCookie;

            ThisCookie = new HttpCookie("amUserId");
            ThisCookie.Expires = dtNow.Add(tsDay);
            Response.Cookies.Add(ThisCookie);

            ThisCookie = new HttpCookie("amCreditOffer");
            ThisCookie.Expires = dtNow.Add(tsDay);
            Response.Cookies.Add(ThisCookie);

            if (Request.ServerVariables["URL"].ToString().Contains("logout.aspx")) {
                // The user requested to logout
                Response.Redirect("../default.aspx");
            } else {
                // The user was probably transferred because of an invalid session

                int nRand = RandomNumber(0, 5);

                switch (nRand)
                {
                    case 1:
                        Response.StatusCode = 301;
                        Response.RedirectLocation = "login.aspx";
                        break;
                    case 2:
                        Response.Write("<html><body>You have lost your session, please <a href=\"login.aspx\">log back in</a>.");
                        break;
                    case 3:
                        Response.Write("<html><head><scr" + "ipt>window.location.href=\"login.aspx\";</scr" + "ipt></head></html>");
                        break;
                    case 4:
                        Response.Write("<html><head><meta http-equiv=refresh content=\"0; URL=login.aspx\"></head></html>");
                        break;
                    case 5:
                        Response.Write("<html><body onload=\"window.location.href=\\\"login.aspx\\\";\"></body></html>");
                        break;
                    default:
                        Response.Redirect("login.aspx");
                        break;
                }
            }
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
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
