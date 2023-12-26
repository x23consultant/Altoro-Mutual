using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Altoro
{
  /// <summary>
  /// Summary description for account.
  /// </summary>
  public partial class Account : System.Web.UI.Page
  {
    protected Altoro.LineItemTable Lineitemtable1;

    protected string userAccount;

    
    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      if (!(System.Convert.ToBoolean(Session["authenticated"])))
      {
        Server.Transfer("logout.aspx");
      }

      if (Request.Params["admin"]=="true")
      {
        Response.Redirect("admin.aspx");
      }


      string thisUser = Request.Cookies["amUserId"].Value;
      GetAccounts(thisUser);
      if (Request.Params["listAccounts"] == null)
      {
        ArrayList myAccounts = (ArrayList)listAccounts.myItems[0];
        userAccount = myAccounts[0].ToString();
      }
      else
      {
        userAccount = Request.Params["listAccounts"];
      }


      accountid.Text = Server.HtmlEncode(userAccount);
      string myBalance = GetBalance(userAccount);
      Balance1.Text=myBalance;
      Balance2.Text=myBalance;
      Credit.accountNumber=userAccount;
      Debit.accountNumber=userAccount;

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

    private void GetAccounts(string userId)
    {
      DataRow myRow;
      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();
      string query ="SELECT accountid, acct_type From accounts WHERE userid = " + userId;

      OleDbDataAdapter myAccount = new OleDbDataAdapter(query , myConnection);

      DataSet ds = new DataSet();
      myAccount.Fill(ds, "accounts");
      DataTable myTable = ds.Tables["accounts"];

      for(int i=0; i<myTable.Rows.Count; i++)
      {
        myRow = myTable.Rows[i];
        ArrayList myList = new ArrayList();
        myList.Add(myRow["accountid"].ToString());
        myList.Add(myRow["accountid"].ToString() + " " + myRow["acct_type"].ToString());
        listAccounts.myItems.Add(myList);
      }

      myConnection.Close();

    }

    private string GetBalance(string accountNumber)
    {
      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();

      string query;

      if (ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString.Substring(9,8) == "SQLOLEDB")
      {
        query = "SELECT SUM(CASE WHEN debit=0 THEN amount ELSE 0-amount END) as Balance FROM transactions WHERE accountid=" + accountNumber;
      }
      else
      {
        query ="SELECT SUM(IIF(debit=0,amount,0-amount)) AS Balance FROM transactions WHERE accountid=" + accountNumber;
      }

      OleDbDataAdapter myBalance = new OleDbDataAdapter(query , myConnection);

      DataSet ds = new DataSet();
      myBalance.Fill(ds, "balance");
      DataTable myTable = ds.Tables["balance"];
      DataRow myRow = myTable.Rows[0];
      string balanceReturn = myRow["balance"].ToString();
      myConnection.Close();
      return balanceReturn;
    }
  }





}
