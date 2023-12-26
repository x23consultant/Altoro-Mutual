using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;


namespace Altoro
{
  public partial class Authentication : Page
  {

    protected void Page_Load(object sender, System.EventArgs e)
    {
      // Put user code to initialize the page here
      Response.Cache.SetCacheability(HttpCacheability.NoCache);

      HtmlMeta meta = new HtmlMeta();
      HtmlHead head = (HtmlHead)Page.Header;
      meta.Name = "keywords";
      meta.Content = "Altoro Mutual Login, login, authenticate";
      head.Controls.Add(meta);

      if(Request.Params["passw"] != null)
      {
        String uname = Request.Params["uid"];
        String passwd = Request.Params["passw"];
        String msg = ValidateUser(uname, passwd);

        if (msg == "Success")
        {
            Response.Redirect("main.aspx");
        }
        else
        {
          message.Text = "Login Failed: " + msg;
        }
      }
    }

    protected string ValidateUser(String uName, String pWord)
    {
      //Set default status to Success
      string status = "Success";

      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();

      string query2 = "SELECT * From users WHERE username = '" + uName + "'";
      string query1 = query2 + " AND password = '" + pWord + "'";

      if (ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString.Contains("Microsoft.Jet.OLEDB.4.0"))
      {
          // Hack for MS Access which can not terminate a string
          query1 = Regex.Replace(query1, "--.*", "");
          query2 = Regex.Replace(query2, "--.*", "");
      }

      DataSet ds = new DataSet();

      OleDbDataAdapter myLogin = new OleDbDataAdapter(query1, myConnection);
      myLogin.Fill(ds, "user");

      if (ds.Tables["user"].Rows.Count==0)
      {
	      OleDbDataAdapter myFailed = new OleDbDataAdapter(query2, myConnection);
	      myFailed.Fill(ds, "user");

        if (ds.Tables["user"].Rows.Count==0)
        {
             status = "We're sorry, but this username was not found in our system.  Please try again.";
        }
        else
        {
            status = "Your password appears to be invalid.  Please re-enter your password carefully.";
        }
      }
      else
      {
	        //Get the row returned by the query
	        DataRow myRow = ds.Tables["user"].Rows[0];

          //Set the Session variables.
          Session["userId"] = myRow["userid"];
          Session["userName"] = myRow["username"];
          Session["firstName"] = myRow["first_name"];
          Session["lastName"] = myRow["last_name"];
          Session["authenticated"] = true;

          //Close the database collection.
          myConnection.Close();

          //Set UserInfo cookie
          DateTime dtNow = DateTime.Now;
          TimeSpan tsHour = new TimeSpan(0, 0, 180, 0);

          string sCookieUser = new Base64Decoder(uName).GetDecoded();

          HttpCookie UserInfo = Request.Cookies["amUserInfo"];
          if ((UserInfo == null) || (sCookieUser != Session["userName"].ToString()))
          {
              UserInfo = new HttpCookie("amUserInfo");
              UserInfo["UserName"] = new Base64Encoder(uName).GetEncoded();
              UserInfo["Password"] = new Base64Encoder(pWord).GetEncoded();

              UserInfo.Expires = dtNow.Add(tsHour);
              Response.Cookies.Add(UserInfo);
          }

          HttpCookie UserId = Request.Cookies["amUserId"];
          UserId = new HttpCookie("amUserId");
          UserId.Value = Session["userId"].ToString();
          Response.Cookies.Add(UserId);

          query1 = "SELECT userid, approved, card_type,interest, limit FROM promo WHERE userid=" + Session["userId"];
          OleDbDataAdapter myApproval = new OleDbDataAdapter(query1, myConnection);

          myApproval.Fill(ds, "promo");
          DataTable myTable = ds.Tables["promo"];
          DataRow curRow = myTable.Rows[0];

          if (System.Convert.ToBoolean(curRow["approved"]))
          {
            HttpCookie CreditOffer = Request.Cookies["amCreditOffer"];
          	CreditOffer = new HttpCookie("amCreditOffer");

            CreditOffer["CardType"] = curRow["card_type"].ToString();
            CreditOffer["Limit"] = curRow["limit"].ToString();
            CreditOffer["Interest"] = curRow["interest"].ToString();
          	Response.Cookies.Add(CreditOffer);
          }
      }

      myConnection.Close();
      return status;
    }

      protected string GetUserName()
      {
          HttpCookie UserInfo = Request.Cookies["amUserInfo"];

          if (Request.Params["uid"] != null)
          {
              return Request.Params["uid"].ToString();
          }
          if (UserInfo != null)
          {
              return new Base64Decoder(UserInfo["UserName"]).GetDecoded();
          }
          else
          {
              return "";
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