<%@ Page Language="C#" Inherits="Altoro.Admin" CodeFile="admin.aspx.cs" MasterPageFile="~/bank/admin/admin.master" Title="Altoro Mutual: Administration" %>
<%@ Register TagPrefix="Altoro" Namespace="Altoro" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<div class="fl" style="width: 99%;">

<script language="javascript">

function confirmpass(myform)
{
  if (myform.password1.value.length && (myform.password1.value==myform.password2.value))
  {
    return true;
  }
  else
  {
    myform.password1.value="";
    myform.password2.value="";
    myform.password1.focus();
    alert ("Passwords do not match");
    return false;
  }

}
</script>

<!-- Be careful what you change.  All changes are made directly to Altoro.mdb database. -->

<h1>Edit User Information</h1>

<table width="100%" border="0">
<form id="addAccount" name="addAccount" action="admin.aspx" method="post">
  <tr>
    <td colspan="4">
      <h2>Add an account to an existing user.</h2>
    </td>
  </tr>
  <tr>
    <th>
      Users:
    </th>
    <th>
      Account Types:
    </th>
    <th>&nbsp;</th>
    <th>&nbsp;</th>
  </tr>
  <tr>
    <td>
      <Altoro:ComboBox id="users" runat="server" />
    </td>
    <td>
      <Select name="accttypes">
        <option Value="Checking">Checking</option>
        <option Value="Savings" Selected>Savings</option>
        <option Value="IRA">IRA</option>
      </Select></td>
    <td></td>
    <td><input type="submit" value="Add Account"></td>
  </tr>
</form>
<form id="changePass" name="changePass" action="admin.aspx" method="post" onsubmit="return confirmpass(this);">
  <tr>
    <td colspan="4"><h2>Change user's password.</h2></td>
  </tr>
  <tr>
    <th>
      Users:
    </th>
    <th>
      Password:
    </th>
    <th>
      Confirm:
    </th>
    <th>&nbsp;</th>
  </tr>
  <tr>
    <td>
      <Altoro:ComboBox id="passUsers" runat="server" />
    </td>
    <td>
      <input type="password" name="password1">
    </td>
    <td>
      <input type="password" name="password2">
    </td>
    <td>
      <input type="submit" name="change" value="Change Password">
    </td>
  </tr>
</form>
<form method="post" name="addUser" action="admin.aspx" id="addUser" onsubmit="return confirmpass(this);">
  <tr>
    <td colspan="4"><h2>Add an new user.</h2></td>
  </tr>
  <tr>
    <th>
      First Name:
      <br>
      Last Name:
    </th>
    <th>
      Username:
    </th>
    <th>
      Password:
      <br>
      Confirm:
    </th>
    <th>
      &nbsp;</th>
  </tr>
  <tr>
    <td>
      <input type="text" name="firstname">
      <br>
      <input type="text" name="lastname">
    </td>
    <td>
      <input type="text" name="username">
    </td>
    <td>
      <input type="password" name="password1">
      <br>
      <input type="password" name="password2">
    </td>
    <td>
      <input type="submit" name="add" value="Add User">
    </td>
  </tr>
  <tr>
    <td colspan="4">It is highly recommended that you leave the username as first
      initial last name. The user id will be created automatically.
    </td>
  </tr>
</form>
</table>

</div>

</asp:content>
