::######################################################################################################################################
:: This file 
:: 1. Creates a new MSI using the config.bat. 
::    Assume the build source files are in the appFiles folder
::######################################################################################################################################

@Echo off
 
call Msi\Config.bat

cd "%VCRRefundDIRECTORY%\Msi\MsiScripts"

Echo ***** Copy Dependencies ******
del "%VCRRefundDIRECTORY%\Msi\appfiles\*.ll" /q
copy "%VCRRefundDIRECTORY%\Dependencies\*.dll"  "%VCRRefundDIRECTORY%\Msi\appfiles"
copy "%VCRRefundDIRECTORY%\Dependencies\RegFree\*.*"  "%VCRRefundDIRECTORY%\Msi\appfiles"
copy "%VCRRefundDIRECTORY%\Dependencies\Config\*.*"  "%VCRRefundDIRECTORY%\Msi\appfiles"
copy "%VCRRefundDIRECTORY%\VCRRefndsConsoleApp\bin\release\*.*"  "%VCRRefundDIRECTORY%\Msi\appfiles"
copy "%VCRRefundDIRECTORY%\VCRRefndFTPApp\bin\release\*.*"  "%VCRRefundDIRECTORY%\Msi\appfiles"

Echo ***** Creating the MSI *****
call mm "VCRRefund.mm"

Echo ***** Copy the MSI to staging folder *****
if not exist "%MSISTAGINGDIRECTORY%\VCRRefund" mkdir "%MSISTAGINGDIRECTORY%\VCRRefund" 
copy "%VCRRefundDIRECTORY%\Msi\MsiScripts\out\VCRRefund.mm\MSI\*.*"  %MSISTAGINGDIRECTORY%\VCRRefund


Echo *****  MSI BUILT SUCCESSFULLY. ***** 
