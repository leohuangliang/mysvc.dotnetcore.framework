using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.IS4.Domain.ClientAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly IDBContext _context;
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<CorsPolicyService> _logger;


        public CorsPolicyService(IDBContext context, IClientRepository clientRepository, ILogger<CorsPolicyService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository)); ;
            _logger = logger;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            // If we use SelectMany directly, we got a NotSupportedException inside MongoDB driver.
            // Details: 
            // System.NotSupportedException: Unable to determine the serialization information for the collection 
            // selector in the tree: aggregate([]).SelectMany(x => x.AllowedCorsOrigins.Select(y => y.Origin))
            var list = await _clientRepository.GetAllAsync();
            var origins = list.AsQueryable().Select(x => x.AllowedCorsOrigins.Select(y => y.Origin)).ToList();

            // As a workaround, we use SelectMany in memory.
            var distinctOrigins = origins.SelectMany(o => o).Where(x => x != null).Distinct();

            var isAllowed = distinctOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            _logger.LogDebug("Origin {origin} is allowed: {originAllowed}", origin, isAllowed);

            return isAllowed;
        }
    }
}
