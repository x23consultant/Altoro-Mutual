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
using System.Reflection;
using System.Configuration;

namespace Altoro
{
  /// <summary>
  /// Summary description for admin.
  /// </summary>
  public partial class Admin : System.Web.UI.Page
  {

    protected void Page_Load(object sender, System.EventArgs e)
    {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!(System.Convert.ToBoolean(Session["authenticated"]) || System.Convert.ToBoolean(Session["admin"])))
            {
                Response.Redirect("login.aspx");
            }
      GetUsers();
    }

    private void GetUsers()
    {
      OleDbConnection myConnection = new OleDbConnection();
      myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
      myConnection.Open();
      string query ="SELECT * From users";
      OleDbDataAdapter myUsers = new OleDbDataAdapter(query , myConnection);

      DataSet ds = new DataSet();
      myUsers.Fill(ds, "users");
      DataTable myTable = ds.Tables["users"];

      foreach(DataRow myRow in myTable.Rows)
      {
        ArrayList myList = new ArrayList();
        myList.Add(myRow["userid"].ToString());
        myList.Add(myRow["userid"].ToString() + " " + myRow["username"].ToString());
        users.myItems.Add(myList);
        passUsers.myItems.Add(myList);
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
