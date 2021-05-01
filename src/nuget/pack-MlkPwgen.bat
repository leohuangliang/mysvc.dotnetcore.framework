dotnet build -c Release ..\..\MlkPwgen\MlkPwgen.csproj

nuget pack nuspecs\MlkPwgen.nuspec -OutputDirectory nuget-packages\MlkPwgen
