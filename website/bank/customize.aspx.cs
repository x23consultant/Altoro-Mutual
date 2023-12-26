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
  /// Summary description for Customize.
  /// </summary>
  public partial class Customize : System.Web.UI.Page
  {
    protected System.IO.FileSystemWatcher fileSystemWatcher1;

        protected void Page_Load(object sender, System.EventArgs e)
    {
      HttpCookieCollection MyCookieColl;
      HttpCookie MyCookie;

      MyCookieColl = Request.Cookies;
      MyCookie = MyCookieColl["lang"];
      string lang = "";
      if (MyCookie != null)
      {
        lang = MyCookie.Value;
      }

      string langParam = Request.QueryString["lang"];

      if (langParam != "")
      {
        lang = langParam;
      }
      
      HttpCookie langCookie = new HttpCookie("lang", lang);
      Response.SetCookie(langCookie);
      langLabel.Text = lang;
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
      this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
      ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
      // 
      // fileSystemWatcher1
      // 
      this.fileSystemWatcher1.EnableRaisingEvents = true;
      ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();

    }
    #endregion
  }
}
