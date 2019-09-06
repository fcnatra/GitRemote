@echo off
echo.
echo Usage: DownloadAndSearchInCode TextToSearch ^< fileWithGitProjectsUrl
echo     GitClient must be configured to connect to git server
echo     Downloads all GIT projects received on pipe input and search the specified text in all of them

call :ClearVariables

if '%1'=='' goto :NoTextToSearch

set /p "ln="
if "%ln%-"=="-" goto :NoRedirectedInput

set outputFile=textSearchResult.txt
echo Downloading code
echo -------
set processingProject=%ln%
call :ProcessProject

for /f "delims=" %%A in ('findstr "^"') do (
  echo.
  set processingProject=%%A
  call :ProcessProject
)

echo ---------------------------------------------- >> %outputFile%
echo RESULTS OF SEARCHING THE STRING: %1 >> %outputFile%
findstr /s /i /n /p %1 * >>  %outputFile%

echo.
echo RESULTS OF SEARCHING WERE SAVED TO %outputFile%
echo.
echo ^</END^>

:ClearVariables
set ln=
set processingProject=
set outputFile=
exit /b

:ProcessProject
git clone %processingProject%
exit /b

:NoRedirectedInput
echo.
echo There is no redirected input
echo.
echo ^</END^>
exit /b

:NoTextToSearch
echo.
echo There is no text to search
echo.
echo ^</END^>
exit /b
