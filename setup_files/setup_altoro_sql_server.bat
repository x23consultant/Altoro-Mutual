@echo Proceeding with this script will delete and reinstall the AltoroMutual database.
@echo In addition, this script will give the local ASPNET account rights for the AltoroMutual database.
@echo Press Ctrl-C to abort or Enter to coninue
@pause

cd /d %0\..

set DBNAME=sscian\SQLEXPRESS

osql -S %DBNAME% -E -n -i create_altoro.sql

setlocal

set ASPNET_ACCOUNT=%COMPUTERNAME%\ASPNET
set OUTPUT_FILE=permin.sql

@echo use altoro>%OUTPUT_FILE%
@echo exec sp_grantlogin '%ASPNET_ACCOUNT%'>>%OUTPUT_FILE%
@echo exec sp_addrolemember 'db_owner', '%ASPNET_ACCOUNT%'>>%OUTPUT_FILE%
@echo exec sp_grantdbaccess '%ASPNET_ACCOUNT%'>>%OUTPUT_FILE%

@echo exec sp_grantlogin 'NT AUTHORITY\NETWORK SERVICE'>>%OUTPUT_FILE%
@echo exec sp_grantdbaccess 'NT AUTHORITY\NETWORK SERVICE'>>%OUTPUT_FILE%


osql -S %DBNAME% -d altoro -E -n -i %OUTPUT_FILE%

endlocal

@echo Setup Complete!
@pause