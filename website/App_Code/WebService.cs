namespace Altoro
{
  using System;
  using System.Collections;
  using System.Configuration;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Data.OleDb;
  using System.Diagnostics;
  using System.Web;
  using System.Web.Services;

  public struct AccountData
  {
  	public int	ID;
  	public string Type;
  }

  public struct Transaction
  {
    public bool Success;
    public string Message;
  }

  [WebService(Namespace="http://www.altoromutual.com/bank/ws/",
              Description="Core services offered by Altoro Mutual bank.")]
  public class Services : System.Web.Services.WebService
  {
  	public Services() { }

    public class MoneyTransfer
    {
      public MoneyTransfer() { }

      public DateTime transferDate;
      public string debitAccount;
      public string creditAccount;
      public double transferAmount;

      public bool setTransferDate(DateTime dtm)
      {
        this.transferDate = dtm;
        return (true);
      }

      public bool setDebitAcct(string dId)
      {
        this.debitAccount = dId;
        return (true);
      }

      public bool setCreditAcct(string cId)
      {
        this.creditAccount = cId;
        return (true);
      }

      public bool setAmount(double amt)
      {
        this.transferAmount = amt;
        return (true);
      }
    }

    [WebMethod(Description="Determine if a UserId is valid and exists.")]
    public bool IsValidUser(string UserId)
    {
      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();

      string query = "SELECT * From users WHERE userid = " + UserId;
      OleDbDataAdapter myAccounts = new OleDbDataAdapter(query, myConnection);

      DataSet ds = new DataSet();
      myAccounts.Fill(ds, "Users");

      if (ds.Tables["Users"].Rows.Count==0)
      {
        myConnection.Close();
        return false;
      }
      else
      {
        myConnection.Close();
        return true;
      }

    }

    [WebMethod(Description="Retrieve accounts by UserId.")]
    public AccountData [] GetUserAccounts(int UserId)
    {
      AccountData [] Accounts = null;

      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();

      string query = "SELECT accountid, acct_type From accounts WHERE userid = " + UserId;
      OleDbDataAdapter myAccounts = new OleDbDataAdapter(query, myConnection);

      DataSet ds = new DataSet();
      myAccounts.Fill(ds, "Accounts");

      if (ds.Tables["Accounts"].Rows.Count>0)
      {
      	Accounts = new AccountData[ds.Tables["Accounts"].Rows.Count];

      	for (int i=0; i<ds.Tables["Accounts"].Rows.Count; i++)
        {
        	Accounts[i].ID = (int)ds.Tables["Accounts"].Rows[i]["accountid"];
        	Accounts[i].Type = (string)ds.Tables["Accounts"].Rows[i]["acct_type"];
        }
      }

      myConnection.Close();
      return Accounts;
    }

    [WebMethod(Description="Transfer funds from one account to another.")]
    public Transaction TransferBalance(MoneyTransfer transDetails)
    {
      Transaction myTransact = new Transaction();

      myTransact.Success = false;
      myTransact.Message = "";

      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();

      OleDbCommand myCommand = new OleDbCommand();
      OleDbTransaction myTrans;

      // Start a local transaction
      myTrans = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
      // Assign transaction object for a pending local transaction
      myCommand.Connection = myConnection;
      myCommand.Transaction = myTrans;

      try
      {
        myCommand.CommandText = "Select count(*) from accounts where accountid=" + transDetails.debitAccount;
        Int32 nRows = (Int32)myCommand.ExecuteScalar();
        if (nRows == 0)
        {
          myTransact.Success = false;
          myTransact.Message = "Transaction failed - debit account does not exist";
        }
        else
        {
          myCommand.CommandText = "Select count(*) from accounts where accountid=" + transDetails.creditAccount;
          nRows = (Int32)myCommand.ExecuteScalar();
          if (nRows == 0)
          {
            myTransact.Success = false;
            myTransact.Message = "Transaction failed - credit account does not exist";
          }
          else if (DateTime.Compare(DateTime.Now, transDetails.transferDate) < 0)
          {
          	myTransact.Success = true;
          	myTransact.Message = "FUTURE ACTION: $" + transDetails.transferAmount + " will be transferred from Account " + transDetails.debitAccount + " into Account " + transDetails.creditAccount + " on " + transDetails.transferDate + ".";
          }
        	else
          {
            myCommand.CommandText = "Insert into transactions (accountid, debit, description, trans_date ,amount) VALUES (" + transDetails.debitAccount + ", 1, 'Balance Withdrawal', '" + DateTime.Now.ToShortDateString() + "', " + transDetails.transferAmount + ")";
            myCommand.ExecuteNonQuery();
            myCommand.CommandText = "Insert into transactions (accountid, debit, description, trans_date ,amount) VALUES (" + transDetails.creditAccount + ", 0, 'Balance Deposit', '" + DateTime.Now.ToShortDateString() + "', " + transDetails.transferAmount + ")";
            myCommand.ExecuteNonQuery();
            myTrans.Commit();

            myTransact.Success = true;
            myTransact.Message = "$" + transDetails.transferAmount + " was successfully transferred from Account " + transDetails.debitAccount + " into Account " + transDetails.creditAccount + " at " + DateTime.Now.ToString() + ".";
          }
        }
      }
      catch (Exception e)
      {
        myTrans.Rollback();
        myTransact.Success = false;
        myTransact.Message = e.ToString();
      }

      myConnection.Close();
      return myTransact;
    }
  }
}