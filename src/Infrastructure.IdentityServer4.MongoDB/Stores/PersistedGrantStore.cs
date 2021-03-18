using IdentityServer4.Extensions;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.IS4.MongoDB.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS4Models = IdentityServer4.Models;
using DomainModels = MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate;
namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Stores
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IMongoDBContext _context;
        private readonly DomainModels.IPersistedGrantRepository _persistedGrantRepository;
        private readonly ILogger<PersistedGrantStore> _logger;

        public PersistedGrantStore(IMongoDBContext context,
            DomainModels.IPersistedGrantRepository persistedGrantRepository,
            ILogger<PersistedGrantStore> logger)
        {
            _context = context;
            _persistedGrantRepository = persistedGrantRepository;
            _logger = logger;
        }

        public async Task StoreAsync(IS4Models.PersistedGrant token)
        {
            try
            {
                _context.BeginTransaction();
                var existing = await _persistedGrantRepository.GetAsync(new DomainModels.Specifications.MatchPersistedGrantByKeySpecification(token.Key));
                if (existing == null)
                {
                    _logger.LogDebug("{persistedGrantKey} not found in database", token.Key);

                    var persistedGrant = token.ToEntity();
                    await _persistedGrantRepository.AddAsync(persistedGrant);
                }
                else
                {
                    _logger.LogDebug("{persistedGrantKey} found in database", token.Key);

                    existing = token.ToEntity();

                    await _persistedGrantRepository.UpdateAsync(existing);
                }

                await _context.CommitAsync();
            }
            catch (Exception ex)
            {
                await _context.RollbackAsync();
                _logger.LogError(0, ex, "Exception storing persisted grant");
            }
        }

        public async Task<IS4Models.PersistedGrant> GetAsync(string key)
        {
            var persistedGrant = await _persistedGrantRepository.GetAsync(new DomainModels.Specifications.MatchPersistedGrantByKeySpecification(key));
            var model = persistedGrant.ToModel();

            _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);

            return model;
        }

        public async Task<IEnumerable<IS4Models.PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)

        {
            filter.Validate();
            var persistedGrants = await _persistedGrantRepository.GetListAsync(new DomainModels.Specifications.MatchPersistedGrantByFilterSpecification(
                subjectId: filter.SubjectId,
                clientId: filter.ClientId,
                sessionId: filter.SessionId,
                type: filter.Type));

            var models = persistedGrants.Select(x => x.ToModel());


            _logger.LogDebug("{persistedGrantCount} persisted grants found for {@filter}", models.Count(), filter);


            return models;
        }

        public async Task RemoveAsync(string key)
        {
            _logger.LogDebug("removing {persistedGrantKey} persisted grant from database", key);
            var persistedGrant = await _persistedGrantRepository.GetAsync(new DomainModels.Specifications.MatchPersistedGrantByKeySpecification(key));
            if (persistedGrant != null)
            {
                try
                {
                    _context.BeginTransaction();

                    await _persistedGrantRepository.RemoveAsync(persistedGrant);
                    await _context.CommitAsync();
                }
                catch (Exception e)
                {
                    await _context.RollbackAsync();
                    _logger.LogError(e, "remove {persistedGrantKey} persisted Error", key);
                    throw;
                }
            }

        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            filter.Validate();
            var persistedGrants = await _persistedGrantRepository.GetListAsync(new DomainModels.Specifications.MatchPersistedGrantByFilterSpecification(
                subjectId: filter.SubjectId,
                clientId: filter.ClientId,
                sessionId: filter.SessionId,
                type: filter.Type));
            var grants = persistedGrants.ToList();
            if (grants.Any())
            {
                try
                {

                    _context.BeginTransaction();
                    _logger.LogDebug("removing {persistedGrantCount} persisted grants from database for {@filter}", grants.Count, filter);
                    foreach (var persistedGrant in grants)
                    {
                        await _persistedGrantRepository.RemoveAsync(persistedGrant);
                    }

                    await _context.CommitAsync();
                }
                catch (Exception e)
                {
                    await _context.RollbackAsync();
                    _logger.LogInformation("removing {persistedGrantCount} persisted grants from database for subject {@filter}: {error}", grants.Count, filter, e.Message);
                    throw;
                }
            }
        }

    }
}
