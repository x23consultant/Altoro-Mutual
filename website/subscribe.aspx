<%@ Page Language="C#" Inherits="Altoro.Subscribe" CodeFile="subscribe.aspx.cs" MasterPageFile="~/default.master" Title="Altoro Mutual: Event Subscription" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

<h1>Subscribe</h1>

<p>We recognize that things are always evolving and changing here at Altoro Mutual.
Please enter your email below and we will automatically notify of noteworthy events.</p>

<form action="subscribe.aspx" method="post" name="subscribe" id="subscribe" onsubmit="return confirmEmail(txtEmail.value);">
  <table>
    <tr>
      <td colspan="2">
        <asp:Label Font-Bold="True" Font-Size="12" ForeColor="red" runat="server" ID="message" />
      </td>
    </tr>
    <tr>
      <td>
        Email:
      </td>
      <td>
        <input type="text" id="txtEmail" name="txtEmail" value="" style="width: 150px;">
      </td>
    </tr>
    <tr>
        <td></td>
        <td>
          <input type="submit" name="btnSubmit" value="Subscribe">
        </td>
      </tr>
  </table>
</form>

<script>
function confirmEmail(sEmail) {
  var msg = null;
  if (sEmail != "") {
    var emailFilter=/^[\w\d\.\%-]+@[\w\d\.\%-]+\.\w{2,4}$/;
    if (!(emailFilter.test(sEmail))) {
      var illegalChars= /[^\w\d\.\%\-@]/;
      if (sEmail.match(illegalChars)) {
          msg = "Your email can only contain alphanumeric\ncharacters and the following:  @.%-\n\n";
      } else {
        msg = "The email does not appear to be valid.  Please enter it again.\n\n";
      }
    }
  } else {
    msg = "Please enter an email address.\n\n";
  }
  if (msg != null) {
      alert(msg);
      return false;
  } else {
      return true;
  }
}
</script>

</div>

</asp:content>