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
    public partial class Survey_Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sAllowedReferer = "";
            Boolean bCheckReferer = true;

            switch(Request.QueryString["step"])
            {
                case "a":
                    lblTitle.Text = "Question 1";
                    lblContent.Text = "<p>Which of the following groups includes your age?<ul>  <li><a href=\"survey_questions.aspx?step=b\">13 years or less</a></li>  <li><a href=\"survey_questions.aspx?step=b\">14-17</a></li>  <li><a href=\"survey_questions.aspx?step=b\">18-24</a></li>  <li><a href=\"survey_questions.aspx?step=b\">25-34</a></li>  <li><a href=\"survey_questions.aspx?step=b\">35-44</a></li>  <li><a href=\"survey_questions.aspx?step=b\">45-54</a></li>  <li><a href=\"survey_questions.aspx?step=b\">55-64</a></li>  <li><a href=\"survey_questions.aspx?step=b\">65-74</a></li>  <li><a href=\"survey_questions.aspx?step=b\">75+</a></li></ul></p>";
                    sAllowedReferer = "survey_questions.aspx";
                    break;
                case "b":
                    lblTitle.Text = "Question 2";
                    lblContent.Text = "<p>Have you bookmarked our website?<ul><li><a href=\"survey_questions.aspx?step=c\">Yes</a></li>  <li><a href=\"survey_questions.aspx?step=c\">No</a></li></ul></p>";
                    sAllowedReferer = "survey_questions.aspx?step=a";
                    break;
                case "c":
                    lblTitle.Text = "Question 3";
                    lblContent.Text = "<p>Are you ... <ul><li><a href=\"survey_questions.aspx?step=d\">Male</a></li><li><a href=\"survey_questions.aspx?step=d\">Female</a></li></ul></p>";
                    sAllowedReferer = "survey_questions.aspx?step=b";
                    break;
                case "d":
                    lblTitle.Text = "Question 4";
                    lblContent.Text = "<p>Are you impressed with our new design?<ul><li><a href=\"survey_complete.aspx\">Yes</a></li><li><a href=\"survey_complete.aspx\">No</a></li></ul></p>";
                    sAllowedReferer = "survey_questions.aspx?step=c";
                    break;
                default:
                    bCheckReferer = false;
                    lblTitle.Text = "Welcome";
                    lblContent.Text = "<p>If you complete this survey, you have an opportunity to win an iPod.  Would you like to continue?<br /><ul><li /><a href=\"survey_questions.aspx?step=a\">Yes</a></li><li><a href=\"survey_questions.aspx?step=a\">No</a></li></ul></p>";
                    break;
            }
            if ((bCheckReferer) && (Application["rootPath"].ToString() + sAllowedReferer != Session["thisPage"].ToString()))
            {
                lblTitle.Text = "Request Out of Order";
                lblContent.Text = "<p>It appears that you attempted to skip or repeat some areas of this survey.  Please <a href=\"survey_questions.aspx\">return to the start page</a> to begin again.</p>";
            }

        }
    }
}