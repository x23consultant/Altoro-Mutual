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
  public partial class Search : System.Web.UI.Page
  {

    protected void Page_Load(object sender, System.EventArgs e)
    {
      HtmlMeta meta = new HtmlMeta();
      HtmlHead head = (HtmlHead)Page.Header;
      meta.Name = "keywords";
      meta.Content = "Altoro Mutual, search, query, find";
      head.Controls.Add(meta);

      lblSearch.Text=Request.Params["txtSearch"];
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
