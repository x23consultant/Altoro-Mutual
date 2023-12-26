<%@ Page Language="C#" Inherits="Altoro.QueryXPath" CodeFile="queryxpath.aspx.cs" MasterPageFile="~/bank/bank.master" Title="Altoro Mutual: Search News Articles" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

<h1>Search News Articles</h1>

<form id="QueryXpath" method="get" runat="server">
  <asp:Label id="Label1" runat="server" >Search our news articles database</asp:Label>
  <br /><br />
  <asp:TextBox id="TextBox1" runat="server" Width="300px">Enter title (e.g. Watchfire)</asp:TextBox>
  <asp:Button id="Button1" runat="server" Width="75px" Text="Query" onclick="Button1_Click"></asp:Button>
  <br /><br />
  <asp:Label id="Label2" runat="server" ></asp:Label>
</form>

</div>

</asp:content>