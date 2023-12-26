use altoro
exec sp_grantlogin 'SSCIAN\ASPNET'
exec sp_addrolemember 'db_owner', 'SSCIAN\ASPNET'
exec sp_grantdbaccess 'SSCIAN\ASPNET'
exec sp_grantlogin 'NT AUTHORITY\NETWORK SERVICE'
exec sp_grantdbaccess 'NT AUTHORITY\NETWORK SERVICE'
