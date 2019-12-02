# GitRemote
https://github.com/fcnatra/GitRemote

Command line tools to remote operate with Git

<strong>Usage:</strong>
<br/><code>dotnet GitRemote.dll -GitApiUrl:https://YourGitApiUrl -GitToken:YourGitToken -GroupName:GroupNameContainingProjects -ListProjects
</code>

This tool was initially intented to list and download all projects, and to search a text inside the source code.

To do so, redirect the output to a file and use it as input source to the batch file SearchInCode.cmd

<strong>GitRemote</strong> dll aplication and <strong>ListProjectsByGroup.html</strong> allow to get a list of projects from a Git repository

<b>Sample:</b>
<br/><code>
dotnet GitRemote.dll -GitApiUrl:TheUrl -GitToken:TheToken -GroupName:TheGroup -ListProjects > projectList.txt

SearchInCode "text1 or text2" text3 text4 | projectList.txt

DownloadAndSearchInCode TextToSearch < fileWithAListOfGitProjectsUrl
</code>
