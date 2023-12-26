'==============================================================================
'
'  The .NET AltoroMutual Setup
'
'  File: setup.vbs
'  Date: October 24, 2006
'
'  Creates a new optional Virtual directory for AltoroMutual in the current directory.
'  Optionally will install AltoroMutual as the Default Web Site for IIS.
'==============================================================================

Option Explicit

' Name of web to create
Dim sName : sName="altoro"

' Needed paths
Dim sPath : sPath = Left(Wscript.ScriptFullName, Len(Wscript.ScriptFullName ) - Len(Wscript.ScriptName))
Dim sWebPath : sWebPath = sPath & "website"
Dim sStpPath : sStpPath = sPath & "setup_files"

' Root IIS node
Dim sRoot : sRoot = "IIS://localhost/w3svc/1/Root"

' User credentials to add to local machine
Dim sUsername : sUsername = "AppScanTest"
Dim sPassword : sPassword = "1ASTest!"

'Setup the website as needed
Call SetupWebsite()

'Setup the database as needed
Call SetupDatabase()

' Add the user to local machine
Call AddUser(sUsername, sPassword)

' Edit the HOSTS file as needed
Call EditHostsFile()

' Notify the user that setup is complete
Call MsgBox("Installation Complete", 64, "Success")

' ----------------------------------------------------------------------------
'
' Helper Functions
'
' -----------------------------------------------------------------------------

Sub SetupWebsite()

  On Error Resume Next
  Dim oIIS, oItem, nIIS, sWebSites
  Dim oWeb, oRoot, oShell
  Dim sMsg, bRoot
  Dim sRootName
  Dim sDotNetPath

  Set oShell = CreateObject("WScript.Shell")

  'Check to see if .NET framework 2.0 is installed
  sDotNetPath = oShell.RegRead("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\policy\v2.0\50727")
  If (Err <> 0) Then
    Call ErrorDisplay("You do not appear to have the .NET 2.0 framework installed." & _
                     "Please ensure that has been installed before setting up this" & _
                     "AltoroMutual site.")
    WScript.Quit
  End If

  'Determine the root path to the .NET framework
  sDotNetPath = oShell.RegRead("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\InstallRoot")
  If (Err <> 0) Then
    Call ErrorDisplay("Could not determine the root path of the .NET Framework installation.")
    WScript.Quit
  End If

  nIIS = "None"

  'Check on available webservers
  Set oIIS = getObject("IIS://localhost/w3svc")
  For each oItem in oIIS
    If (oItem.Class = "IIsWebServer") Then
      sWebSites = sWebsites & oItem.Name & " - " & oItem.ServerComment & vbCrLf
      If nIIS = "None" Then
        nIIS = CStr(oItem.Name)
      Else
        nIIS = "Many"
      End If
    End If
  Next

  Select Case nIIS
    Case "None"
      Call MsgBox("You do not have IIS installed.", 48, "Stopping")
      Exit Sub
    Case "Many"
      nIIS = InputBox("Enter the NUMBER of the Web Site where you wish to install AltoroMutual:" & vbCrLf & vbCrLf & sWebsites,"Select Web Site to Install To")
  End Select

  sRoot = "IIS://localhost/w3svc/" & nIIS & "/Root"

  sMsg = "Do you want to install AltoroMutual as a virtual directory on this web site?" & vbCrLf & vbCrLf & "Click 'Yes' to install AltoroMutual as a virtual directory." & vbCrLf & "Click 'No' to replace the default web site." & vbCrLf & "Click 'Cancel' to skip this step."

  Select Case MsgBox(sMsg, vbYesNoCancel or vbQuestion, "AltoroMutual Web Site")
    Case vbYes
      bRoot = False
      sRootName = sName & "/"
    Case vbNo
      bRoot = True
      sRootName = ""
    Case Else
      Exit Sub
   End Select

  Set oRoot = GetObject(sRoot)

  If (Err <> 0) Then
    Call ErrorDisplay("Unable to access Root directory ")
    Exit Sub
  End If

  If bRoot Then
    Set oWeb = GetObject(sRoot)
  Else
    Set oWeb = GetObject(sRoot & "/" & sName)
    If (Err = 0) Then
      'Delete existing virtual directory
      oRoot.Delete "IIsWebVirtualDir", sName
    End If

    Err = 0 ' reset error
    Set oWeb = oRoot.Create("IIsWebVirtualDir", sName)

    If (Err <> 0) Then
      Call ErrorDisplay("Unable to create " & oWeb.ADsPath & ".")
      Exit Sub
    End If
  End If

  'Set properties on the new web
  oWeb.AccessExecute = True
  oWeb.Path = sWebPath
  oWeb.AppFriendlyName = sName
  oWeb.AppCreate True
  oWeb.AuthFlags = 1
  oWeb.AuthNTLM = False
  oWeb.EnableDirBrowsing = False
  oWeb.DefaultDoc = "default.aspx"
  oWeb.EnableDefaultDoc = True
  oWeb.SetInfo

  Set oWeb = oRoot.Create("IIsWebDirectory", sRootName & "admin")
  If Err <> 0 Then
    Err = 0
    Set oWeb = GetObject(sRoot & "/" & sRootName & "admin")
  End If
  With oWeb
    oWeb.AccessExecute = False
    oWeb.AccessScript = True
    oWeb.SetInfo
  End With

  Err = 0 ' reset error
  Set oWeb = oRoot.Create("IIsWebDirectory", sRootName & "bank")
  If Err <> 0 Then
    Err = 0
    Set oWeb = GetObject(sRoot & "/" & sRootName & "bank")
  End If
  With oWeb
    oWeb.AccessExecute = False
    oWeb.AccessScript = True
    oWeb.EnableDirBrowsing = True
    oWeb.SetInfo
  End With

  Set oWeb = oRoot.Create("IIsWebDirectory", sRootName & "bank/20060308_bak")
  If Err <> 0 Then
    Err = 0
    Set oWeb = GetObject(sRoot & "/" & sRootName & "bank/20060308_bak")
  End If
  With oWeb
    oWeb.AccessExecute = False
    oWeb.AccessScript = False
    oWeb.EnableDirBrowsing = False
    oWeb.SetInfo
  End With

  Set oWeb = oRoot.Create("IIsWebDirectory", sRootName & "bank/members")
  If Err <> 0 Then
    Err = 0
    Set oWeb = GetObject(sRoot & "/" & sRootName & "bank/members")
  End If
  With oWeb
    oWeb.AccessExecute = False
    oWeb.AccessScript = False
    oWeb.AuthFlags = 2
    oWeb.DefaultLogonDomain = ""
    oWeb.EnableDirBrowsing = False
    oWeb.Realm = ""
    oWeb.SetInfo
  End With

  Set oWeb = oRoot.Create("IIsWebDirectory", sRootName & "images")
  If Err <> 0 Then
    Err = 0
    Set oWeb = GetObject(sRoot & "/" & sRootName & "images")
  End If
  With oWeb
    oWeb.AccessExecute = False
    oWeb.AccessScript = False
    oWeb.SetInfo
  End With

  Set oWeb = oRoot.Create("IIsWebDirectory", sRootName & "pr")
  If Err <> 0 Then
    Err = 0
    Set oWeb = GetObject(sRoot & "/" & sRootName & "pr")
  End If
  With oWeb
    oWeb.AccessExecute = False
    oWeb.AccessScript = False
    oWeb.EnableDirBrowsing = True
    oWeb.SetInfo
  End With

  oShell.Run ("cmd /c attrib -R " & sWebPath & "\*.* /S")
  oShell.Run ("cmd /c cacls " & sWebPath & "\comments.txt /E /G ASPNET:F")
  oShell.Run ("cmd /c cacls " & sWebPath & "\App_Data\altoro.mdb /E /G ASPNET:F")
  oShell.Run ("cmd /c " & sDotNetPath & "v2.0.50727\aspnet_regiis -s w3svc/" & nIIS & "/Root/" & sRootName & " -norestart")

  If (Err <> 0) Then
    Call ErrorDisplay("Unable to save all changes for " & sRoot & "/" & sRootName & ".")
  Else
    Call MsgBox(Now & " " & sRoot & "/" & sRootName & " created successfully.", 64, "Success")
  End If

  Set oWeb = Nothing
  Set oRoot = Nothing
  Set oShell = Nothing

End Sub


Sub SetupDatabase()

  On Error Resume Next

  Dim oXML, oNode, oFSO, oShell
  Dim sConn, bMSSQL
  Dim sMsg

  sMsg ="Will you use AltoroMutual with SQL Server?" & vbCrLf & vbCrLf & "Select 'Yes' to choose MS SQL Server." & vbCrLf & "Select 'No' for the Access database file." & vbCrLf & "Click 'Cancel' to exit the setup."

  Select Case MsgBox(sMsg, vbYesNoCancel or vbQuestion, "Database Type")
    Case vbYes
      bMSSQL = True
      sConn = "Provider=SQLOLEDB;Server=(local);database=altoro;Integrated Security=SSPI;"
    Case vbNo
      bMSSQL = False
      sConn = "Provider=Microsoft.Jet.OLEDB.4.0; User ID=Admin; Data Source=" & sWebPath & "\App_Data\altoro.mdb;"
    Case Else
      Exit Sub
   End Select

  Set oXML = CreateObject("Microsoft.XMLDOM")
  Set oFSO = CreateObject("Scripting.FileSystemObject")
  Set oShell = CreateObject("WScript.Shell")

	If bMSSQL Then
		sConn = Replace(sConn, "(local)",oShell.RegRead("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName\ComputerName"))
	End If

  oXML.async="false"
  oXML.load("website\web.config")

  Set oNode = oXML.getElementsByTagName("add")
  oNode.item(0).setAttribute "connectionString", sConn

  oXML.save "website\web.config"

  If (Err <> 0) Then
    Call ErrorDisplay("An error occurred while saving the web.config files.")
    Exit Sub
  End If


  sMsg ="Would you like to delete the Altoro database (if present) and reinstall?" & vbCrLf & vbCrLf & "Select 'Yes' to refresh the data." & vbCrLf & "Select 'No' to do this manually or to keep existing data."

  If MsgBox(sMsg, vbYesNo or vbQuestion, "Re-install Database") = vbYes Then

    Dim sFile

    Err = 0 ' reset error

    If bMSSQL Then

      sFile = oFSO.BuildPath(sStpPath,"setup_altoro_sql_server.bat")

      If oFSO.FileExists(sFile) Then
        oShell.Run sFile,1,True
      Else
        Call ErrorDisplay("No database setup script was found - " & sFile)
      End If

      sMsg = "An error occurred while configuring the MS SQL instance."

    Else

      sFile = oFSO.BuildPath(sStpPath,"altoro.mdb")

      If oFSO.FileExists(sFile) Then
        oFSO.CopyFile sFile, oFSO.BuildPath(sWebPath & "\App_Data","altoro.mdb"), True
      Else
        Call ErrorDisplay("The default Access database was not found - " & sFile)
      End If

      sMsg = "An error occurred while copying the Altoro MDB file."

    End If

  End If

  If (Err <> 0) Then
    Call ErrorDisplay(sMsg)
  Else
    Call MsgBox("The database has been configured successfully.", 64, "Success")
  End If


  'Check for boot.ini on the drive root. This is used for the Insecure File Access test in AppScan
  If not oFSO.FileExists(Left(sStpPath,3) & "boot.ini") Then

		sMsg ="A boot.ini file was not found in the root of the " & Left(sStpPath,2) & " drive." & vbCrLf & vbCrLf & "Select 'Yes' to copy a sample boot.ini file here." & vbCrLf & "Select 'No' to do this manually."

		If MsgBox(sMsg, vbYesNo or vbQuestion, "Install boot.ini file") = vbYes Then
			oFSO.CopyFile oFSO.BuildPath(sStpPath,"boot.ini"), Left(sStpPath,3) & "boot.ini", True
		End If

  End If

  Set oShell = Nothing
  Set oXML = Nothing
  Set oFSO = Nothing

End Sub

Sub AddUser(sUser, sPass)

  Dim oNetwork, oComputer, oAccounts, oUser
  Dim sCompName, bUserExists, sMsg

  sMsg = "Do you want to create a new local user on this machine? " & vbCrLf & vbCrLf & _
         "This will allow you to show the power of the Authentication " & vbCrLf & _
         "Tester power tool that comes with AppScan.  You can run this " & vbCrLf & _
         "tool against:" & _
         vbCrLf & vbCrLf & "http://this.site/bank/member/" & vbCrLf & vbCrLf & _
         "which is protected with Basic Windows authentication."

  If MsgBox(sMsg, vbYesNo or vbQuestion, "Add User") = vbYes Then

    Set oNetwork = CreateObject("WScript.Network")
    sCompName = oNetwork.ComputerName

    bUserExists = False

    Set oComputer = GetObject("WinNT://" & sCompName)

    oComputer.Filter = Array("User")

    For Each oUser In oComputer
      If (oUser.Name = sUser) And Not (bUserExists) Then
        bUserExists = True
      End If
    Next

    If not bUserExists Then
      Set oAccounts = GetObject("WinNT://" & sCompName & ",computer")
      Set oUser = oAccounts.Create("user", sUser)
      oUser.SetPassword sPass
      oUser.SetInfo
      Call MsgBox("The user has been created successfully.", 64, "Success")
    Else
      Call MsgBox("This local user already exists.", 64, "Already Exists")
    End If

    Set oNetwork = Nothing
    Set oComputer = Nothing
    Set oAccounts = Nothing
    Set oUser = Nothing

  End If

End Sub

Sub EditHostsFile()

  Dim sMsg

  sMsg = "Did you want to alter the HOSTS file to point www.altoromutual.com to the localhost?"

  Select Case MsgBox(sMsg, vbYesNo or vbQuestion, "Alter HOSTS file?")
    Case vbYes

      Dim oFSO, oFile, sFile

      sFile = "C:\WINDOWS\system32\drivers\etc\hosts"

      Set oFSO = CreateObject("Scripting.FileSystemObject")

      If oFSO.FileExists(sFile) Then

        Set oFile = oFSO.OpenTextFile(sFile,1,False)

        If InStr(oFile.ReadAll,"www.altoromutual.com") = 0 Then

          Set oFile = oFSO.OpenTextFile(sFile,8,False)
          oFile.WriteLine("127.0.0.1       www.altoromutual.com")

          Call DisplayMessage("HOSTS file updated.")
        Else
          Call DisplayMessage("HOSTS file already contains reference")
        End If

      Else
        Call DisplayMessage("Unable to find the HOSTS file")
      End If

      If IsObject(oFile) Then
        oFile.Close
        Set oFile = Nothing
      End If

      Set oFSO = Nothing

    Case Else
      Exit Sub
   End Select

End Sub


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Displays error message.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub ErrorDisplay(sMsg)
  Call MsgBox(Now & ". Error Code: " & Hex(Err) & " - " & sMsg, 16, "Error")
  Err = 0
End Sub