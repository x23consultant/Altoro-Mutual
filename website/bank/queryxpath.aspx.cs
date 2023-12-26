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
using System.Xml;
using System.Xml.XPath;

namespace Altoro
{
  /// <summary>
  /// Summary description for QueryXpath.
  /// </summary>
  public partial class QueryXPath : System.Web.UI.Page
  {
    protected void Page_Load(object sender, System.EventArgs e)
    {


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

    protected void Button1_Click(object sender, System.EventArgs e)
    {
      XmlDocument Xml2 = new XmlDocument();
      Xml2.Load (this.MapPath ("..") + "/pr/Docs.xml");
      XPathNavigator nav = Xml2.CreateNavigator();

            String XPathXpr = "string(/news/publication[contains(title,'" + TextBox1.Text + "') and (isPublic/text()='True')]/title/text())";
            XPathExpression expr = nav.Compile(XPathXpr);
            String acc = Convert.ToString(nav.Evaluate(expr));

      if (acc!="")
      {
        Label2.Text="Found news title: <br>"+acc;
      }
      else
      {
        Label2.Text="News title not found, try again";
      }

    }

    private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
    }
  }
}
