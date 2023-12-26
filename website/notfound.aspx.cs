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
  /// Summary description for notfound.
  /// </summary>
  public partial class NotFound : System.Web.UI.Page
  {


    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.StatusCode = 404;

      HtmlMeta meta = new HtmlMeta();
      HtmlHead head = (HtmlHead)Page.Header;
      meta.Name = "keywords";
      meta.Content = "File not Found";
      head.Controls.Add(meta);

      string requestedpage="";
      if (Request.QueryString["aspxerrorpath"] !=null)
      {
        requestedpage = Request.QueryString["aspxerrorpath"];
      }
      else if (Request.QueryString["Path"] !=null)
      {
        requestedpage = Request.QueryString["Path"];
      }
      error.Text = "Could not find the page you requested. <br><br><b>";
      error.Text += requestedpage;
      error.Text += "</b><br><br>Please check your spelling. ";
      error.Text += "If the spelling is correct and the page still does not exist contact the System Administrator.";

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
