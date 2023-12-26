<%@ Page Language="C#" Inherits="Altoro.RecentTransactions" CodeFile="transaction.aspx.cs" MasterPageFile="bank.master" Title="Altoro Mutual: Recent Transactions" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

<h1>Recent Transactions</h1>

<form id="Form1" runat="server" method="post">
<table border="0" style="padding-bottom:10px;">
    <tr>
        <td valign=top>After</td>
        <td><input name="after" type="text" value="<% =Server.HtmlEncode(Request.Form["after"]) %>" /><br /><span class="credit">mm/dd/yyyy</span></td>
        <td valign=top>Before</td>
        <td><input name="before" type="text" value="<% =Server.HtmlEncode(Request.Form["before"]) %>"  /><br /><span class="credit">mm/dd/yyyy</span></td>
        <td valign=top><input type=submit value=Submit /></td>
    </tr>
</table>

<asp:DataGrid id="MyTransactions" runat="server"
    AlternatingItemStyle-BackColor=Gainsboro
    AllowPaging="True"
    AutoGenerateColumns="False"
    CellPadding="3"
    OnPageIndexChanged="DataGrid_PageChanger"
    PagerStyle-Mode="NumericPages"
    PageSize="50"
    Width="100%">
  <HeaderStyle Font-Bold="True"
                ForeColor="White"
                BackColor="#BFD7DA">
  </HeaderStyle>
  <Columns>
    <asp:BoundColumn DataField="transid" HeaderText="TransactionID" />
    <asp:BoundColumn DataField="accountid" HeaderText="AccountId" />
    <asp:BoundColumn DataField="description" HeaderText="Description" />
    <asp:BoundColumn DataField="amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
  </Columns>
</asp:DataGrid>
</form>

</div>

</asp:content>