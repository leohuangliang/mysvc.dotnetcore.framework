@echo off
echo Running all unit tests...
dotnet test "%~dp0mysvc.dotnetcore.framework.sln" --verbosity normal
pause
