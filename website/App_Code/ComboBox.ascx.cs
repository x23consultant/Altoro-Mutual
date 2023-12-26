namespace Altoro
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Web.UI;

	/// <summary>
	///		Summary description for ComboBox.
	/// </summary>
	public class ComboBox : System.Web.UI.Control
	{
		public ArrayList myItems;
		private string cmbName;

		public ComboBox()
		{
			myItems = new ArrayList();

		}

		public string Name
		{
			get {return cmbName;}
			set {cmbName=value;}

		}

		protected override void Render(HtmlTextWriter output)
		{
			output.Write("<select id=\"" + cmbName + "\" name=\"" + cmbName + "\" >");
			foreach (ArrayList thisItem in myItems)
			{
				output.Write("<option value=\"" + thisItem[0] + "\">" + thisItem[1] + "</option>");
			}
			output.Write("</select>");
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
