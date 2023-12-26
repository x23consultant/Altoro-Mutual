using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace Altoro
{
  /// <summary>
  /// Summary description for comment.
  /// </summary>
  public partial class comment : System.Web.UI.Page
  {

    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);

      writeToFile(Request.Params["cfile"],Request.Params["name"],Request.Params["email_addr"],Request.Params["subject"],Request.Params["comments"]);
    }
    private void writeToFile(string file, string name, string email_addr, string subject, string comments)
    {
      if ( Regex.IsMatch (file,"\\A\\w+\\.\\w{2,4}\\z") )
      {
        string commnt = name +  ", " + email_addr + ", " + subject + ", " + comments;

        FileInfo myFile = new FileInfo(Server.MapPath(file));
        StreamWriter outFile = myFile.CreateText();
        outFile.WriteLine(commnt);
        outFile.Close();
      }
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
