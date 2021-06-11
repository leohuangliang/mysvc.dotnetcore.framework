using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.DotNetCore.Framework.IS4.Domain.ClientAggregate;
using System;
using System.Collections.Generic;
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
            var list = await _clientRepository.GetAllAsync();

            var originList = new List<string>();

            foreach (Client client in list)
            {
                if (client.AllowedCorsOrigins != null)
                {
                    client.AllowedCorsOrigins.ForEach(c =>
                    {
                        if (c != null && !c.Origin.IsNullOrBlank()) originList.Add(c.Origin);
                    });
                }
            }

            // As a workaround, we use SelectMany in memory.
            var distinctOrigins = originList.Distinct();

            var isAllowed = distinctOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            _logger.LogDebug("Origin {origin} is allowed: {originAllowed}", origin, isAllowed);

            return isAllowed;
        }
    }
}
