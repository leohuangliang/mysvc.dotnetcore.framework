using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Impl;
using System;
using System.Collections.Generic;

namespace MySvc.Framework.IS4.Domain.IdentityResourceAggregate
{
    [AggregateRootName("IdentityResources")]
    public class IdentityResource : AggregateRoot
    {

        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<IdentityResourceClaim> UserClaims { get; set; }
        public List<IdentityResourceProperty> Properties { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public bool NonEditable { get; set; }
    }
}
