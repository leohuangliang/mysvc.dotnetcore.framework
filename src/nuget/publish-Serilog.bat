set LatestVersion=6.0.1-beta1

nuget setapikey oy2dmiwvfxlulcnah5uacl64xz3hegiepd3u6vvzfrumeu -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Logging.Serilog\MySvc.Framework.Infrastructure.Serilog.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
