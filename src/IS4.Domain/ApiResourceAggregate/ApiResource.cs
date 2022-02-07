﻿using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Impl;
using System;
using System.Collections.Generic;

namespace MySvc.Framework.IS4.Domain.ApiResourceAggregate
{
    [AggregateRootName("ApiResources")]
    public class ApiResource : AggregateRoot
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AllowedAccessTokenSigningAlgorithms { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<ApiResourceSecret> Secrets { get; set; }
        public List<ApiResourceScope> Scopes { get; set; }
        public List<ApiResourceClaim> UserClaims { get; set; }
        public List<ApiResourceProperty> Properties { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }
    }
}
