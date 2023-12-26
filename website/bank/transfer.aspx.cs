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
  /// Summary description for transfer.
  /// </summary>
  public partial class Transfer : System.Web.UI.Page
  {

    protected System.Web.UI.WebControls.Label accountid;
    protected string userAccount;

    protected void Page_Load(object sender, System.EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      if (!(System.Convert.ToBoolean(Session["authenticated"])))
      {
                Server.Transfer("logout.aspx");
      }

      string thisUser = Request.Cookies["amUserId"].Value;
      GetAccounts(thisUser);

    if (this.Request.Params.Count > 3)
    {
      string debitAccount = this.Request.Params["debitAccount"];
      string creditAccount = this.Request.Params["creditAccount"];
      string transferAmount = this.Request.Params["transferAmount"];
     

      if (!string.IsNullOrEmpty(debitAccount) && !string.IsNullOrEmpty(creditAccount) && !string.IsNullOrEmpty(transferAmount))
      {
        Altoro.Services.MoneyTransfer myTransaction = new Altoro.Services.MoneyTransfer();

        myTransaction.transferDate = DateTime.Now.Date;
        myTransaction.debitAccount = debitAccount;
        myTransaction.creditAccount = creditAccount;
        myTransaction.transferAmount = Convert.ToDouble(transferAmount);

        Altoro.Services myTransferClass = new Altoro.Services();
        Transaction myResult = myTransferClass.TransferBalance(myTransaction);

        this.postResp.InnerHtml = "<span style='color: Red'>" + myResult.Message + "</span>";
      }
      else
      {
        this.postResp.InnerHtml =  "" ;
      }
    }
    }

    private void GetAccounts(string userId)
    {
      DataRow myRow=null;
      OleDbDataAdapter myAccount=null;
      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();
      string query ="SELECT accountid, acct_type From accounts WHERE userid = " + userId;
      DataSet ds = new DataSet();

      myAccount = new OleDbDataAdapter(query , myConnection);
      myAccount.Fill(ds, "accounts");

      DataTable myTable = ds.Tables["accounts"];
      for(int i=0; i<myTable.Rows.Count; i++)
      {
        myRow = myTable.Rows[i];
        ArrayList myList = new ArrayList();
        myList.Add(myRow["accountid"].ToString());
        myList.Add(myRow["accountid"].ToString() + " " + myRow["acct_type"].ToString());
        debitAccount.myItems.Add(myList);
        creditAccount.myItems.Add(myList);
      }

      myConnection.Close();

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
