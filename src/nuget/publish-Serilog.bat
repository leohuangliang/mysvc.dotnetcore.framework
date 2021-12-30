set LatestVersion=6.0.0-beta3

nuget setapikey oy2dmiwvfxlulcnah5uacl64xz3hegiepd3u6vvzfrumeu -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Logging.Serilog\MySvc.DotNetCore.Framework.Infrastructure.Logging.Serilog.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
