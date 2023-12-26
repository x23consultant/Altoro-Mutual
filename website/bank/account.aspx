<%@ Page Language="C#" Inherits="Altoro.Account" CodeFile="account.aspx.cs" MasterPageFile="bank.master" Title="Altoro Mutual: Account Information" %>
<%@ Register TagPrefix="Altoro" Namespace="Altoro" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

<!-- To modify account information do not connect to SQL source directly.  Make all changes
through the admin page. -->

<h1>Account History - <asp:Label ID="accountid" Runat=server/></h1>

<table width="590" border="0">
  <tr>
    <td colspan=2>
      <table cellSpacing="0" cellPadding="1" width="100%" border="1">
        <tr>
          <th colSpan="2">
            Balance Detail</th></tr>
        <tr>
          <th align="left" width="80%" height="26">
            <form id="Form1" method="post" action="account.aspx">
              <Altoro:ComboBox id="listAccounts" name="listAccounts" runat="server" />
              <input type="submit" id="btnGetAccount" Value="Select Account">
          </th>
          </FORM>
          <th align="middle" height="26">
            Amount
          </th>
        </tr>
        <tr>
          <td>Ending balance as of
            <% =System.DateTime.Now %>
          </td>
          <td align="right"><asp:label id="Balance1" Runat="server"></asp:label></td>
        </tr>
        <tr>
          <td>Available balance
          </td>
          <td align="right">
            <asp:Label ID="Balance2" Runat="server" /></td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td>
      <Altoro:LineItemTable id="Credit" debit="false" runat="server" />
    </td>
  </tr>
  <tr>
    <td>
      <Altoro:LineItemTable debit="true" id="Debit" runat="server" />
    </td>
  </tr>
</table>

</div>

</asp:content>