set LatestVersion=6.0.2-beta4

nuget setapikey oy2dtcyi3vr6h5uxx5lam6ukgyhozszeno77lbvjkkif6i -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Domain.Core\MySvc.Framework.Domain.Core.%LatestVersion%.nupkg   -Source https://api.nuget.org/v3/index.json
@REM nuget.exe push nuget-packages\Domain.Core.Extensions\MySvc.Framework.Domain.Core.Extensions.%LatestVersion%.nupkg   -Source https://api.nuget.org/v3/index.json

nuget.exe push nuget-packages\Infrastructure.Adapter.AutoMapper\MySvc.Framework.Infrastructure.AutoMapper.%LatestVersion%.nupkg   -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Client\MySvc.Framework.Infrastructure.Authorization.Client.%LatestVersion%.nupkg -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Admin\MySvc.Framework.Infrastructure.Authorization.Admin.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Merchant\MySvc.Framework.Infrastructure.Authorization.Merchant.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.InternalClient\MySvc.Framework.Infrastructure.Authorization.InternalClient.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json

nuget.exe push nuget-packages\Infrastructure.Crosscutting\MySvc.Framework.Infrastructure.Crosscutting.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.IntegrationEventService\MySvc.Framework.Infrastructure.IntegrationEventService.%LatestVersion%.nupkg -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.Json.NewtonsoftJson\MySvc.Framework.Infrastructure.NewtonsoftJson.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.Cache.StackExchangeRedis\MySvc.Framework.Infrastructure.Crosscutting.StackExchangeRedis.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Data.MongoDB\MySvc.Framework.Infrastructure.Data.MongoDB.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Job.Hangfire\MySvc.Framework.Infrastructure.Job.Hangfire.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.IdentityServer4.MongoDB\MySvc.Framework.IS4.MongoDB.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\IS4.Domain\MySvc.Framework.IS4.Domain.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Logging.Serilog\MySvc.Framework.Infrastructure.Serilog.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator\MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis\MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator.Redis.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json


@REM nuget.exe push nuget-packages\MlkPwgen\MySvc.DotNetCore.MlkPwgen.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
@REM nuget.exe push nuget-packages\PayPalCoreSDK\MySvc.DotNetCore.PayPalCoreSDK.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
@REM nuget.exe push nuget-packages\PayPalMerchantSDK\MySvc.DotNetCore.PayPalMerchantSDK.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
