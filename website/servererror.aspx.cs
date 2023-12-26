using System;
using System.Collections;
using System.Collections.Specialized; 
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
  /// Summary description for error.
  /// </summary>
    public partial class ServerError : System.Web.UI.Page
  {
  
    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);


      if (!(Request.ServerVariables["URL"].ToString().Contains("servererror.aspx")))
      {
          Response.StatusCode = 500;
      }

        string errSummary;
        string errDetails;
        try
        {
            errSummary = Session["ErrorSummary"].ToString();
            Session.Contents.Remove("ErrorSummary");
            errDetails = Session["ErrorDetails"].ToString();
            Session.Contents.Remove("ErrorDetails");
        }
        catch (Exception err)
        {
            errSummary = "An unknown error occurred.";
            errDetails = "";
        }
        lblSummary.Text = Server.HtmlEncode(errSummary);
        // lblDetails.Text = Server.HtmlEncode(errDetails);
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
