using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CaptchaImage
{
  public partial class JpegImage : System.Web.UI.Page
  {
    private void Page_Load(object sender, System.EventArgs e)
    {
      // Create a CAPTCHA image using the text stored in the Session object.
      CaptchaImage wb = new CaptchaImage("", 1, 1, "Arial");

      // Change the response headers to output a JPEG image.
      this.Response.Clear();
      this.Response.ContentType = "image/jpeg";

      // Write the image to the response stream in JPEG format.
      wb.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

      // Dispose of the CAPTCHA image object.
      wb.Dispose();

      HttpCookie WebBug = Request.Cookies["TRACK"];

      if (WebBug == null)
      {
		    WebBug = new HttpCookie("TRACK");
		    WebBug.Value = "QlP7Myt3zfSMlSwEIh5I6dtyo";
		    Response.Cookies.Add(WebBug);
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
      this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion
  }
}
