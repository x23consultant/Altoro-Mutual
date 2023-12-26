<%@ Page Language="C#" %>
<html>
<head><title>
Altoro Mutual Test Page
</title>
</head>

 <body><center>
<img src="images/logo.gif">
       
<p>
<br>
If ASP.Net is installed correctly a message should appear below.

<p>

      
<p>
<span style='color: red;'>
<% Response.Write("ASP.Net is installed and functioning"); %>
</span>         
          
</p>
<p>
<br>
If nothing appears above in red, you do not have ASP.Net installed correctly.</p><p>  Please refer to the documentation provided by Microsoft to get ASP.Net installed and functioning.</P>
</center>

</body>
</html>
