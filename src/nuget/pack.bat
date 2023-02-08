dotnet build -c Release ..\..\mysvc.dotnetcore.framework.sln
@REM dotnet build -c Release ..\PayPal\PayPal.SDK.NET5.sln

nuget pack nuspecs\Domain.Core.nuspec -OutputDirectory nuget-packages\Domain.Core
@REM nuget pack nuspecs\Domain.Core.Extensions.nuspec -OutputDirectory nuget-packages\Domain.Core.Extensions

nuget pack nuspecs\Infrastructure.Adapter.AutoMapper.nuspec -OutputDirectory nuget-packages\Infrastructure.Adapter.AutoMapper
nuget pack nuspecs\Infrastructure.Authorization.Client.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.Client
nuget pack nuspecs\Infrastructure.Authorization.Admin.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.Admin
nuget pack nuspecs\Infrastructure.Authorization.InternalClient.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.InternalClient
nuget pack nuspecs\Infrastructure.Authorization.Merchant.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.Merchant
nuget pack nuspecs\Infrastructure.Crosscutting.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting
nuget pack nuspecs\Infrastructure.Crosscutting.Json.NewtonsoftJson.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting.Json.NewtonsoftJson
nuget pack nuspecs\Infrastructure.Crosscutting.Cache.StackExchangeRedis.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting.Cache.StackExchangeRedis
nuget pack nuspecs\Infrastructure.Data.MongoDB.nuspec -OutputDirectory nuget-packages\Infrastructure.Data.MongoDB
nuget pack nuspecs\Infrastructure.Job.Hangfire.nuspec -OutputDirectory nuget-packages\Infrastructure.Job.Hangfire
nuget pack nuspecs\Infrastructure.IntegrationEventService.nuspec -OutputDirectory nuget-packages\Infrastructure.IntegrationEventService
nuget pack nuspecs\IS4.Domain.nuspec -OutputDirectory nuget-packages\IS4.Domain
nuget pack nuspecs\Infrastructure.IdentityServer4.MongoDB.nuspec -OutputDirectory nuget-packages\Infrastructure.IdentityServer4.MongoDB
nuget pack nuspecs\Infrastructure.Logging.Serilog.nuspec -OutputDirectory nuget-packages\Infrastructure.Logging.Serilog
nuget pack nuspecs\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator
nuget pack nuspecs\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis

@REM nuget pack nuspecs\MlkPwgen.nuspec -OutputDirectory nuget-packages\MlkPwgen

@REM nuget pack nuspecs\PayPalCoreSDK.nuspec -OutputDirectory nuget-packages\PayPalCoreSDK
@REM nuget pack nuspecs\PayPalMerchantSDK.nuspec -OutputDirectory nuget-packages\PayPalMerchantSDK