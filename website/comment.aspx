<%@ Page Language="C#" Inherits="Altoro.comment" CodeFile="comment.aspx.cs" MasterPageFile="~/default.master" Title="Altoro Mutual: Thank-You" %>

<asp:content ID="cph" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

 <h1>Thank You</h1>
 
 <p>Thank you for your comments, <%=Request.Params["name"] %>.  They will be reviewed by our Customer Service staff and
 given the full attention that they deserve.</p>

</div>

</asp:content>
