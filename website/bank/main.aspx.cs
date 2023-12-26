using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Altoro
{
    /// <summary>
    /// Summary description for welcome.
    /// </summary>
    public partial class Default : Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!(System.Convert.ToBoolean(Session["authenticated"])))
            {
                Server.Transfer("logout.aspx");
            }

            string thisUser = Request.Cookies["amUserId"].Value;

            DataRow myRow;
            DataTable acctTable = GetAccounts(thisUser);

            CheckPromo(thisUser);

            for (int i = 0; i < acctTable.Rows.Count; i++)
            {
                myRow = acctTable.Rows[i];
                ArrayList myList = new ArrayList();
                myList.Add(myRow["accountid"].ToString());
                myList.Add(myRow["accountid"].ToString() + " " + myRow["acct_type"].ToString());
                listAccounts.myItems.Add(myList);
            }
        }

        private DataTable GetAccounts(string userId)
        {
            OleDbConnection myConnection = new OleDbConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
            myConnection.Open();
            string query = "SELECT accountid, acct_type From accounts WHERE userid = " + userId;
            OleDbDataAdapter myAccounts = new OleDbDataAdapter(query, myConnection);

            DataSet ds = new DataSet();
            myAccounts.Fill(ds, "accounts");
            DataTable myTable = ds.Tables["accounts"];
            myConnection.Close();
            return myTable;
        }

        private void WritePromo(string cType, string cLimit, string cInterest)
        {
            string promoText = "<table width=590 border=0>";
            promoText += "<tr><td><h2>Congratulations! </h2></td></tr>";
            promoText += "<tr><td>You have been pre-approved for an Altoro ";
            promoText += cType;
            promoText += " Visa with a credit limit of $";
            promoText += cLimit;
            promoText += "!</td></tr>";
            promoText += "<tr><td>Click <a href='apply.aspx";
            promoText += "'>Here</a> to apply.</td></tr></table>";
            promo.Visible = true;
            promo.Text = promoText;

        }


        private void CheckPromo(string strUserId)
        {
            if (Request.Cookies["amCreditOffer"] != null)
            {
	            HttpCookie CreditOffer = Request.Cookies["amCreditOffer"];

              WritePromo(CreditOffer["CardType"], CreditOffer["Limit"], CreditOffer["Interest"]);
            }
        }

        protected String GetSessionValue(String key)
        {
            if (Request.Cookies["amUserId"].Value==Session["userId"].ToString())
            {
            	return Session[key].ToString();
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
