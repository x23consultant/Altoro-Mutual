using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Altoro
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public partial class Survey_Complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 0)
            {
                if (Application["rootPath"].ToString() + "survey_questions.aspx?step=d" != Session["thisPage"].ToString())
                {
                    lblTitle.Text = "Request Out of Order";
                    lblContent.Text = "<p>It appears that you attempted to skip or repeat some areas of this survey.  Please <a href=\"survey_questions.aspx\">return to the start page</a> to begin again.</p>";
                }
                else
                {
                    lblTitle.Text = "Thanks";
                    lblContent.Text = "<p>Thank you for completing our survey.  We are always working to improve our status in the eyes of our most important client: YOU.  Please enter your email below and we will notify you soon as to your winning status.  Thanks.</p><form method=\"get\" action=\"survey_complete.aspx\"><div style=\"padding-left:30px;\"><input type=\"text\" name=\"txtEmail\" style=\"width:200px;\" /> <input type=\"submit\" value=\"Submit\" style=\"width:100px;\" /></div></form>";
                }
            }
            else
            {
                lblTitle.Text = "Thanks";
                lblContent.Text = "<p>Thanks for your entry.  We will contact you shortly at:<br /><br /> <b>" + Request.Params["txtEmail"] + "</b></p>";
            }
        }
    }
}