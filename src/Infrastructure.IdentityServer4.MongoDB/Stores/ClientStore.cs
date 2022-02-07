using IdentityModel;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using MySvc.Framework.Domain.Core.Specification;
using MySvc.Framework.Infrastructure.Data.MongoDB;

using MySvc.Framework.IS4.MongoDB.Mappers;
using System;
using System.Threading.Tasks;
using IS4Models=IdentityServer4.Models;
using DomainModels = MySvc.Framework.IS4.Domain.ClientAggregate;
namespace MySvc.Framework.IS4.MongoDB.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IMongoDBContext _context;
        private readonly DomainModels.IClientRepository _clientRepository;
        private readonly ILogger<ClientStore> _logger;

        public ClientStore(IMongoDBContext context,
            DomainModels.IClientRepository clientRepository,
            ILogger<ClientStore> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IS4Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await _clientRepository.GetAsync(Specification<DomainModels.Client>.Eval(x => x.ClientId == clientId));

            var model = client.ToModel();
            _logger.LogDebug("{clientId} found in database: {clientIdFound}", clientId, model != null);

            return model;
        }
    }
}
