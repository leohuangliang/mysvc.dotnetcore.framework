using System;
using System.Collections.Generic;
using System.Text;
using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace Infrastructure.Data.MongoDB.Tests
{
    [AggregateRootName("OtherAggs")]
    public class OtherAgg : AggregateRoot
    {
        public bool IsCEO { get; set; }
    }

    public class Boss : AggregateRoot
    {
        public bool IsCEO { get; set; }
    }
}
