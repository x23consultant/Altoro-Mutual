<%@ Page Language="C#" CodeFile="login.aspx.cs" AutoEventWireup="false" Inherits="CaptchaImage._Default" MasterPageFile="~/default.master" Title="Altoro Mutual: Administration" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<h1>Administration Login</h1>

<!-- Password: Altoro1234 -->

<form id="Default" method="post" runat="server">
  <img id="captcha" src="captcha.aspx" /><br />
  <p>
    <strong>Enter the code shown above:</strong><br />
    <asp:TextBox id="CodeNumberTextBox" runat="server" /><br /><br />
    <strong>Enter the administrative password:</strong><br />
    <input id="Password" type=password runat="server" /><br /><br />
    <asp:Button id="SubmitButton" runat="server" Text="Submit" /><br />
  </p>
  <p><asp:Label id="MessageLabel" runat="server"></asp:Label></p>
</form>

<script>
window.onload = document.forms[1].elements[1].focus();
</script>

</asp:content>