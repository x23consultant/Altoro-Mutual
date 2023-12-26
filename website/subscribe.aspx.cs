using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;


namespace Altoro
{
    public partial class Subscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HtmlMeta meta = new HtmlMeta();
            HtmlHead head = (HtmlHead)Page.Header;
            meta.Name = "keywords";
            meta.Content = "Altoro Mutual, subscription, mailing list";
            head.Controls.Add(meta);

            if (Request.Params["txtEmail"] != null)
            {
                String sEmail = Request.Params["txtEmail"];

                if (sEmail != null && sEmail.Length > 0)
                {
                    OleDbConnection myConnection = new OleDbConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
                    myConnection.Open();

                    OleDbCommand myCommand = new OleDbCommand();
                    OleDbTransaction mySubscribe;

                    // Start a local transaction
                    mySubscribe = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    // Assign transaction object for a pending local transaction
                    myCommand.Connection = myConnection;
                    myCommand.Transaction = mySubscribe;

                    myCommand.CommandText = "insert into subscribe (email) values ('" + sEmail + "')";
                    myCommand.ExecuteNonQuery();
                    mySubscribe.Commit();
                    myConnection.Close();

                    message.Text = "Thank you.  Your email " + sEmail + " has been accepted.";
                }
                else
                {
                    message.Text = "You must enter a valid email address.";
                }

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

        protected string GetEmail()
        {
            if (Request.Params["txtEmail"] != null)
            {
                return Request.Params["txtEmail"].ToString();
            }
            else
            {
                return "";
            }

        }
    }

}