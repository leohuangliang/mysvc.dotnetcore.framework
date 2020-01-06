set LatestVersion=1.2.0

nuget setapikey oy2krio3vzunlbfzz4uapb3d2fnwggwute7zt7s5fohj5m -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Domain.Core\MySvc.DotNetCore.Framework.Domain.Core.%LatestVersion%.nupkg   -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Adapter.AutoMapper\MySvc.DotNetCore.Framework.Infrastructure.Adapter.AutoMapper.%LatestVersion%.nupkg   -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Client\MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client.%LatestVersion%.nupkg -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Admin\MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Merchant\MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting\MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.EventBus.Cap\MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Cap.%LatestVersion%.nupkg -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.Json.NewtonsoftJson\MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json.NewtonsoftJson.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Data.MongoDB\MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Job.Hangfire\MySvc.DotNetCore.Framework.Infrastructure.Job.Hangfire.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
