<%@ Page Language="C#" Inherits="Altoro.Contact" CodeFile="feedback.aspx.cs" MasterPageFile="~/default.master" Title="Altoro Mutual: Feedback" %>

<asp:content ID="cph" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

<h1>Feedback</h1>

<p>Our Frequently Asked Questions area will help you with many of your inquiries.<br />
If you can't find your question, return to this page and use the e-mail form below.</p>

<p><b>IMPORTANT!</b> This feedback facility is not secure.  Please do not send any <br />
account information in a message sent from here.</p>

<form name="cmt" method="post" action="comment.aspx">

<!--- Dave- Hard code this into the final script - Possible security problem.
  Re-generated every Tuesday and old files are saved to .bak format at L:\backup\website\oldfiles    --->
<input type="hidden" name="cfile" value="comments.txt">

<table border=0>
  <tr>
    <td align=right>To:</td>
    <td valign=top><b>Online Banking</b> </td>
  </tr>
  <tr>
    <td align=right>Your Name:</td>
    <td valign=top><input name=name size=25 type=text value = "<% =Session["firstName"] + " " + Session["lastName"] %>"></td>
  </tr>
  <tr>
    <td align=right>Your Email Address:</td>
    <td valign=top><input name=email_addr type=text size=25></td>
  </tr>
  <tr>
    <td align=right>Subject:</td>
    <td valign=top><input name=subject size=25></td>
  </tr>
  <tr>
    <td align=right valign=top>Question/Comment:</td>
    <td><textarea cols=65 name=comments rows=8 wrap=PHYSICAL align="top"></textarea></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td><input type=submit value=" Submit " name="submit">&nbsp;<input type=reset value=" Clear Form " name="reset"></td>
  </tr>
</table>
</form>

<br /><br />

<img id="bug" src="" height=1 width=1 runat="server" />

</div>

</asp:content>
