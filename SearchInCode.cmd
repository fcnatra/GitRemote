@echo off
echo.
echo Usage: SearchInCode TextToSearch TextToSearch TextToSearch [^> Redirect_Output_To_File_If_Needed.txt]
echo     Searches the THREE (3) text strings in all files in this folder and subfolders (AND logic - inclusive)
echo.

if '%1' == '' goto END
call :DeleteTemporalFiles

findstr /s /i /n /p %1 * > tempResults.tmp0

if '%2' == '' goto PrintResults
findstr /i /n %2 * >> tempResults.tmp0

if '%3' == '' goto PrintResults
findstr /i /n %3 * >> tempResults.tmp0

:PrintResults
type tempResults.tmp0

:END
call :DeleteTemporalFiles

echo ^</END^>
exit /b

:DeleteTemporalFiles
del tempResults.tmp0 /f /q 2> nul
exit /b
