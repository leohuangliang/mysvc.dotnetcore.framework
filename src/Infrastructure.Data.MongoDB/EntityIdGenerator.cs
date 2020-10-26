
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MySvc.DotNetCore.Framework.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB
{
    public class EntityIdGenerator : IEntityIdGenerator
    {
        public string GenerateId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
