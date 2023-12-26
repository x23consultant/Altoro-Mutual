using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Configuration;

namespace Altoro
{
  /// <summary>
  /// Summary description for admin.
  /// </summary>
    public partial class Application : System.Web.UI.Page
  {

    protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!(System.Convert.ToBoolean(Session["authenticated"]) || System.Convert.ToBoolean(Session["admin"])))
            {
                Response.Redirect("login.aspx");
            }
            Content.Text += "<table><tr><td class=cc colspan=2><b>Session Contents Information</b></td></tr>";
            foreach (string item in Session.Contents)
            {
                Content.Text += "<tr><td class=t>" + item + "</td><td>" + Session[item].ToString() + "</td></tr>";
            }
            Content.Text += "<tr><td class=cc colspan=2><b>Application Contents Information</b></td></tr>";
            foreach (string item in Application.Contents)
            {
                Content.Text += "<tr><td class=t>" + item + "</td><td>" + Application[item].ToString() + "</td></tr>";
            }
            Content.Text += "</table><br />";
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
