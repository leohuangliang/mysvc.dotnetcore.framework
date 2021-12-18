set LatestVersion=5.0.1-beta7

nuget setapikey oy2dmiwvfxlulcnah5uacl64xz3hegiepd3u6vvzfrumeu -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Domain.Core\MySvc.DotNetCore.Framework.Domain.Core.%LatestVersion%.nupkg   -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Client\MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client.%LatestVersion%.nupkg -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Admin\MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Authorization.Merchant\MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting\MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.IntegrationEventService\MySvc.DotNetCore.Framework.Infrastructure.IntegrationEventService.%LatestVersion%.nupkg -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Crosscutting.Json.NewtonsoftJson\MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json.NewtonsoftJson.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Data.MongoDB\MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Job.Hangfire\MySvc.DotNetCore.Framework.Infrastructure.Job.Hangfire.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.IdentityServer4.MongoDB\MySvc.DotNetCore.Framework.IS4.MongoDB.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\IS4.Domain\MySvc.DotNetCore.Framework.IS4.Domain.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\Infrastructure.Logging.Serilog\MySvc.DotNetCore.Framework.Infrastructure.Logging.Serilog.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\MlkPwgen\MySvc.DotNetCore.MlkPwgen.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\PayPalCoreSDK\MySvc.DotNetCore.PayPalCoreSDK.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\PayPalMerchantSDK\MySvc.DotNetCore.PayPalMerchantSDK.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
