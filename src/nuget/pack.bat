dotnet build -c Release ..\..\mysvc.dotnetcore.framework.sln
nuget pack nuspecs\Domain.Core.nuspec -OutputDirectory nuget-packages\Domain.Core
nuget pack nuspecs\Infrastructure.Authorization.Client.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.Client
nuget pack nuspecs\Infrastructure.Authorization.Admin.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.Admin
nuget pack nuspecs\Infrastructure.Authorization.Merchant.nuspec -OutputDirectory nuget-packages\Infrastructure.Authorization.Merchant
nuget pack nuspecs\Infrastructure.Crosscutting.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting
nuget pack nuspecs\Infrastructure.Crosscutting.Json.NewtonsoftJson.nuspec -OutputDirectory nuget-packages\Infrastructure.Crosscutting.Json.NewtonsoftJson
nuget pack nuspecs\Infrastructure.Data.MongoDB.nuspec -OutputDirectory nuget-packages\Infrastructure.Data.MongoDB
nuget pack nuspecs\Infrastructure.Job.Hangfire.nuspec -OutputDirectory nuget-packages\Infrastructure.Job.Hangfire
nuget pack nuspecs\Infrastructure.IntegrationEventService.nuspec -OutputDirectory nuget-packages\Infrastructure.IntegrationEventService

nuget pack nuspecs\IS4.Domain.nuspec -OutputDirectory nuget-packages\IS4.Domain
nuget pack nuspecs\Infrastructure.IdentityServer4.MongoDB.nuspec -OutputDirectory nuget-packages\Infrastructure.IdentityServer4.MongoDB

