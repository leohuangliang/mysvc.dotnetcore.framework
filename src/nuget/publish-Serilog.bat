set LatestVersion=6.0.2-beta6

nuget setapikey oy2dmiwvfxlulcnah5uacl64xz3hegiepd3u6vvzfrumeu -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Logging.Serilog\MySvc.Framework.Infrastructure.Serilog.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
