using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.IS4.Domain.ApiResourceAggregate;
using MySvc.Framework.IS4.Domain.ApiResourceAggregate.Specifications;
using MySvc.Framework.IS4.Domain.ApiScopeAggregate;
using MySvc.Framework.IS4.Domain.ApiScopeAggregate.Specifications;
using MySvc.Framework.IS4.Domain.IdentityResourceAggregate;
using MySvc.Framework.IS4.Domain.IdentityResourceAggregate.Specifications;
using MySvc.Framework.IS4.MongoDB.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IS4Models = IdentityServer4.Models;

namespace MySvc.Framework.IS4.MongoDB.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly IMongoDBContext _context;
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IIdentityResourceRepository _identityResourceRepository;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly ILogger<ResourceStore> _logger;

        public ResourceStore(IMongoDBContext context,
            IApiResourceRepository resourceRepository,
            IIdentityResourceRepository identityResourceRepository,
            IApiScopeRepository apiScopeRepository,
            ILogger<ResourceStore> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _apiResourceRepository = resourceRepository ?? throw new ArgumentNullException(nameof(resourceRepository));
            _identityResourceRepository = identityResourceRepository ?? throw new ArgumentNullException(nameof(identityResourceRepository));
            _apiScopeRepository = apiScopeRepository ?? throw new ArgumentNullException(nameof(apiScopeRepository));
            _logger = logger;
        }

        public async Task<IEnumerable<IS4Models.ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var names = apiResourceNames.ToArray();

            var apis = await _apiResourceRepository.GetListAsync(
                new MatchApiResourceByScopeNamesSpecification(names));

            var models = apis.Select(x => x.ToModel()).ToArray();
            _logger.LogDebug("Found {scopes} API scopes in database", models.Select(x => x.Name));

            return models;
        }

        public async Task<IEnumerable<IS4Models.ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();

            var apis = await _apiResourceRepository.GetListAsync(
                new MatchApiResourceByScopeNamesSpecification(names));

            var models = apis.Select(x => x.ToModel()).ToArray();
            _logger.LogDebug("Found {scopes} API scopes in database", models.Select(x => x.Name));

            return models;
        }

        public async Task<IEnumerable<IS4Models.IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();

            var resources =
                await _identityResourceRepository.GetListAsync(
                    new MatchIdentityResourceByScopeNamesSpecification(names));

            var models = resources.Select(x => x.ToModel()).ToArray();
            _logger.LogDebug("Found {scopes} API scopes in database", models.Select(x => x.Name));

            return models;
        }

        public async Task<IEnumerable<IS4Models.ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();

            var apis = await _apiScopeRepository.GetListAsync(
                new MatchApiScopeByNamesSpecification(names));

            var models = apis.Select(x => x.ToModel()).ToArray();
            _logger.LogDebug("Found {scopes}  scopes in database", models.Select(x => x.Name));

            return models;
        }

        public async Task<IS4Models.Resources> GetAllResourcesAsync()
        {
            var identities = await _identityResourceRepository.GetAllAsync();
            var apis = await _apiResourceRepository.GetAllAsync();
            var scopes = await _apiScopeRepository.GetAllAsync();

            var apiModels = apis.Select(x => x.ToModel()).ToArray();
            var identityModels = identities.Select(x => x.ToModel()).ToArray();
            var apiScopeModels = scopes.Select(x => x.ToModel()).ToArray();

            var result = new IS4Models.Resources(
                identityModels,
                apiModels,
                apiScopeModels);

            _logger.LogDebug("Found {scopes} as all scopes in database,and {apis} as API resources", result.IdentityResources.Select(x => x.Name).Union(result.ApiScopes.Select(x => x.Name)),
                result.ApiResources.Select(x => x.Name));

            return result;
        }
    }
}
