using System;
using System.Collections.Generic;
using System.Text;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Impl;

namespace MySvc.Framework.Infrastructure.Data.MongoDB.Impl
{
    public class IntegrationEventLogRepository : MongoDBRepository<IntegrationEventLog>, IIntegrationEventLogRepository
    {
        public IntegrationEventLogRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
