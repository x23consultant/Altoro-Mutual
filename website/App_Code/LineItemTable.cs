//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'LineItemTable' in the code behind file in 'bank\account.aspx.cs' is moved to this file.
//====================================================================


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


	public class LineItemTable : System.Web.UI.Page
	{
		private string myAccount;
		private string myDebit;
		private string tableHeader;
		private DataTable myDetails;

		public string accountNumber
		{
			get
			{
				return myAccount;
			}
			set
			{
				myAccount=value;
			}
		}

		public string debit
		{
			get
			{
				return myDebit;
			}
			set
			{
				myDebit=value.ToLower();
				if (myDebit == "true")
				{
					tableHeader="Debits";
				}
				else
				{
					tableHeader="Credits";
				}
			}
		}


		public LineItemTable()
		{

		}

		protected DataTable GetLineItems()
		{
			OleDbConnection myConnection = new OleDbConnection();
			myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
			myConnection.Open();
			string query = "SELECT accountid, ";
			if (ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString.Substring(9,8) == "SQLOLEDB")
			{
				query += "Convert(char,trans_date,101) as tdate,";
			}
			else
			{
				query += "format(trans_date,'mm/dd/yyyy') as tdate,";
			}
			query += "description, amount ";
			query += "From transactions WHERE debit";
			query +=  myDebit=="true"?"<>":"=";
			query += "0 and accountid = " ;
			query += myAccount;
			OleDbDataAdapter myTrans = new OleDbDataAdapter(query , myConnection);

			DataSet ds = new DataSet();
			myTrans.Fill(ds, "transactions");
			DataTable myTable = ds.Tables["transactions"];
			myConnection.Close();
			return myTable;
		}



		protected override void Render(HtmlTextWriter output)
		{
			myDetails =	GetLineItems();
			output.Write("<br><b>" + tableHeader + "</b>");
			output.Write("<table border=1 cellpadding=2 cellspacing=0 width='590'>");
			output.Write("<tr><th width=100>Account<th bgcolor=#cccccc width=100>");
			output.Write("Date </th><th width=290>Description</th>");
			output.Write("<th width=100>Amount</th></tr></table>");
			output.Write("<DIV ID='credits' STYLE='overflow: hidden; overflow-y: scroll; width:590px; height: 152px; padding:0px; margin: 0px' >");
			output.Write("<table border=1 cellpadding=2 cellspacing=0 width='574'>");
			foreach (DataRow myRow in myDetails.Rows)
			{
				output.Write("<tr><td width=99>" + myRow["accountid"] + "</td>");
				output.Write("<td width=99>");
				output.Write(myRow["tdate"]);
				output.Write("</td><td width=292>");
				output.Write(myRow["description"]);
				output.Write("</td><td width=84 align=right>");
				output.Write(myRow["amount"]);
				output.Write("</td></tr>");
			}
			output.Write("</table>");
		}
	}

}