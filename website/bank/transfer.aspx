<%@ Page Language="C#" Inherits="Altoro.Transfer" CodeFile="transfer.aspx.cs" MasterPageFile="bank.master" Title="Altoro Mutual: Transfer Funds" %>
<%@ Register TagPrefix="Altoro" Namespace="Altoro" %>

<asp:content ID="content" contentplaceholderid="Main" runat="server">

<script type="text/javascript" src="mozxpath.js"></script>
<script>

var oXML;

if(window.XMLHttpRequest) {
  try  {
    oXML = new XMLHttpRequest()
  } catch(e) {}
} else if(window.ActiveXObject) {
  try {
    oXML = new ActiveXObject("Msxml2.XMLHTTP")
  } catch(e) {
    try {
      oXML = new ActiveXObject("Microsoft.XMLHTTP")
    } catch(e) {}
  }
}

 function doTransfer()
 {
  var dbt=document.getElementById("debitAccount").value;
  var cdt=document.getElementById("creditAccount").value;
  var amt=document.getElementById("transferAmount").value;

  <!-- Some sample test accounts -->
  if(dbt.length==0){dbt=20;}
  if(cdt.length==0){cdt=21;}

  oXML.open("POST", "ws.asmx", true);

  oXML.onreadystatechange=function()
  {
    var curNode = null;
    if (oXML.readyState==4)
    {
      try {
        //curNode = oXML.responseXML.selectSingleNode("//Message");
        curNode = oXML.responseXML.selectSingleNode("//soap:Body").firstChild.firstChild.firstChild.nextSibling.firstChild.nodeValue;
      }
      catch(e)
      {
        //curNode = oXML.responseXML.selectSingleNode("//faultstring");
        curNode = oXML.responseXML.selectSingleNode("//soap:Body").firstChild.firstChild.nextSibling.firstChild.nodeValue;
      }
      if (curNode)
        document.getElementById("soapResp").innerHTML = "<span style='color: Red'>" + curNode + "</span>";
      else
        document.getElementById("soapResp").innerHTML = "<span style='color: Red'>An unknown error occurred.</span>";
    }
  }

  oXML.setRequestHeader("SOAPAction", "http://www.altoromutual.com/bank/ws/TransferBalance");
  oXML.setRequestHeader("Content-Type", "text/xml");

  var reqBody = '<?xml version="1.0" encoding="UTF-8"?>'+"\n"+
      '<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"'+"\n"+
      ' xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" '+"\n"+
      ' xmlns:xsd="http://www.w3.org/2001/XMLSchema">'+"\n"+
      ' <soap:Body>'+"\n"+
      '  <TransferBalance xmlns="http://www.altoromutual.com/bank/ws/">'+"\n"+
      '   <transDetails>'+"\n"+
      '    <transferDate>2000-01-01</transferDate>'+"\n"+
      '    <debitAccount>' + dbt + '</debitAccount>'+"\n"+
      '    <creditAccount>' + cdt + '</creditAccount>'+"\n"+
      '    <transferAmount>' + amt + '</transferAmount>'+"\n"+
      '   </transDetails>'+"\n"+
      '  </TransferBalance>'+"\n"+
      ' </soap:Body>'+"\n"+
      '</soap:Envelope>';

     oXML.send (reqBody);
}


</script>

<div class="fl" style="width: 99%;">

<form id="tForm" name="tForm" method="post">

<h1>Transfer Funds</h1>

<table cellSpacing="0" cellPadding="1" width="100%" border="0">
  <tr>
    <td><strong>From Account:</strong>
    </td>
    <td>
      <Altoro:ComboBox id="debitAccount" name="debitAccount" runat="server" />
    </td>
  </tr>
  <tr>
    <td><strong>To Account:</strong></td>
    <td>
      <Altoro:ComboBox id="creditAccount" name="creditAccount" runat="server" />
    </td>
  </tr>
  <tr>
    <td><strong> Amount to Transfer:</strong>
    </td>
    <td><input type="text" id="transferAmount" name="transferAmount"></td>
  </tr>
  <tr>
    <td colspan="2" align="center"><input type="button" name="transfer" value="Transfer Money" onclick="doTransfer();" ID="transfer"></td>
  </tr>
  <tr>
    <td colspan="2">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="center">
    <span id="postResp" align="center" runat="server" />
    <span id="soapResp" name="soapResp" align="center" />
    </td>
  </tr>
</table>
</form>

</div>

</asp:content>