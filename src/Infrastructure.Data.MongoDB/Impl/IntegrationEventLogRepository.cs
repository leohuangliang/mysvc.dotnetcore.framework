using System;
using System.Collections.Generic;
using System.Text;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl
{
    public class IntegrationEventLogRepository : MongoDBRepository<IntegrationEventLog>, IIntegrationEventLogRepository
    {
        public IntegrationEventLogRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
