using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Altoro
{
  /// <summary>
  /// Summary description
  /// </summary>
  ///
    public partial class Default : System.Web.UI.Page
  {

    private string LoadFile(string myFile)
    {
      string returnString="";
      string tmpStr;
      string fileType = myFile.Substring((myFile.Length)-4,4);
      if (fileType == ".txt" || fileType ==".htm")
      {
        string openFile ="";
        int i = 0;
        TextReader infile= null;
        while (myFile[i] != 0)
        {
          //returnString += System.Char.GetUnicodeCategory(myFile,i).ToString();
          openFile+= myFile[i];
          i++;
          if (i == myFile.Length)
            break;
        }

        infile = File.OpenText(openFile);

        while((tmpStr = infile.ReadLine())!=null)
        {
          returnString += tmpStr;
        }
        infile.Close();
      }
      else
      {

        returnString = "Error!  File must be of type TXT or HTM";
      }

      return returnString;

    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);

      string fileToLoad = Server.MapPath("") + "./static/";
      if(Request.QueryString["content"] == null) {
          fileToLoad += "default.htm";
      } else {
          fileToLoad += Request.QueryString["content"];
      }
      string fileContent = LoadFile(fileToLoad);

      //Keyword handling
      if (fileContent.StartsWith("<!-- "))
      {
        //Extract and add keywords
        string fileKeywords = fileContent.Substring(14,fileContent.IndexOf(" -->",14)-14);
        fileContent = fileContent.Substring(fileContent.IndexOf("-->") + 3);
        HtmlMeta meta = new HtmlMeta();
        HtmlHead head = (HtmlHead)Page.Header;
        meta.Name = "keywords";
        meta.Content = fileKeywords;
        head.Controls.Add(meta);

        //TODO: Auto generate description
        meta.Name = "description";
        meta.Content = "Altoro Mutual offers a broad range of commercial, private, retail and mortgage banking services to small and middle-market businesses and individuals.";
        head.Controls.Add(meta);
      }

      //Add content to page
      lblContent.Text = fileContent;
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
