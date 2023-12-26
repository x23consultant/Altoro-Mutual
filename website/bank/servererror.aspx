<%@ Page Language="C#" Inherits="Altoro.ServerError" CodeFile="~/servererror.aspx.cs" MasterPageFile="~/template.master" Title="Altoro Mutual: Server Error" %>

<asp:content ID="cph" contentplaceholderid="Content" runat="server">

<div class="err" style="width: 99%;">

<h1>An Error Has Occurred</h1>

<h2>Summary:</h2>

<p><b><asp:Label ID="lblSummary" Runat="server" /></b></p>

<h2>Error Message:</h2>

<p><asp:Label ID="lblDetails" Runat="server" /></p>

</div>

</asp:content>