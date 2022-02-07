﻿using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Impl;
using System.Collections.Generic;

namespace MySvc.Framework.IS4.Domain.ApiScopeAggregate
{
    [AggregateRootName("ApiScopes")]
    public class ApiScope : AggregateRoot
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<ApiScopeClaim> UserClaims { get; set; }
        public List<ApiScopeProperty> Properties { get; set; }
    }
}
