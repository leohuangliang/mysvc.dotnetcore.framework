using System.Collections.Generic;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Options;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data.MongoDB.Tests
{
    public class InitMongoDB
    {
        private readonly MongoDBManager _mongoDbManager;
        public InitMongoDB(IOptions<MongoDBSettings> mongoDbSettingsAccessor)
        {
            
            _mongoDbManager = new MongoDBManager(mongoDbSettingsAccessor);
        }

        public void CreateCollections(List<string> collectionNames)
        {
            foreach (var name in collectionNames)
            {
                var filter = new BsonDocument("name", name);
                var collections = _mongoDbManager.Database.ListCollections(new ListCollectionsOptions(){Filter =  filter});
                bool exists = collections.Any();
                if (!exists)
                {
                    _mongoDbManager.Database.CreateCollection(name);
                }
            }
            
            
        }
    }
}
