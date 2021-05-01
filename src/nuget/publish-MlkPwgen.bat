set LatestVersion=5.0.0-beta1

nuget setapikey oy2dmiwvfxlulcnah5uacl64xz3hegiepd3u6vvzfrumeu -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\MlkPwgen\MySvc.DotNetCore.MlkPwgen.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
