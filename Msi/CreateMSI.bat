@echo off
echo Generating MSI Package...
cd /d %1
mm %2.mm
exit 
