using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Altoro
{
    /// <summary>
    /// Show user transactions
    /// </summary>
    public partial class RecentTransactions : System.Web.UI.Page
    {
        public OleDbDataAdapter myTransactions;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!(System.Convert.ToBoolean(Session["authenticated"])))
            {
                Server.Transfer("logout.aspx");
            }

            BindGrid();
        }

        private void BindGrid()
        {
            OleDbConnection myConnection = new OleDbConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;

            string sAfter = Request.Form["after"];
            string sBefore = Request.Form["before"];
            string sSQL = "SELECT t.transid, t.accountid, t.description, t.amount FROM transactions t INNER JOIN accounts a ON t.accountid = a.accountid where 1=1 ";
            
            if (!(sAfter == null || sAfter == ""))
            {
                sSQL += " and t.trans_date >= " + sAfter;
            }
            if (!(sBefore == null || sBefore == ""))
            {
                sSQL += " and t.trans_date <= " + sBefore;
            }
            sSQL += " and a.userid = " + Request.Cookies["amUserId"].Value + " ORDER BY 1 DESC";

            // Format the dates for the respective databases
            string sDateRegEx = "((0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.]([0-9]{4}))";
            if (ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString.Contains("Microsoft.Jet.OLEDB.4.0"))
            {
                sSQL = Regex.Replace(sSQL, sDateRegEx, "#$4-$3-$2#");

                // Hack for MS Access which can not terminate a string
                sSQL = Regex.Replace(sSQL, "--.*", "");
            }
            else
            {
                sSQL = Regex.Replace(sSQL, sDateRegEx, "'$4-$3-$2'");
            }

            myTransactions = new OleDbDataAdapter(sSQL, myConnection);
            DataSet ds = new DataSet();
            myTransactions.Fill(ds, "trans");
            DataView Source = ds.Tables["trans"].DefaultView;

            MyTransactions.DataSource = Source;
            MyTransactions.DataBind();
            DisableViewState(MyTransactions);
        }
        public void DataGrid_PageChanger(Object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            MyTransactions.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void DisableViewState(DataGrid dg)
        {
            foreach (DataGridItem dgi in dg.Items)
            {
                dgi.EnableViewState = true;
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
