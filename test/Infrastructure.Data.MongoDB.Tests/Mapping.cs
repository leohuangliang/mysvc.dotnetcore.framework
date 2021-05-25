using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB.Tests
{
    public class Mapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Person>(c =>
            {
                c.AutoMap();
                c.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<Leader>(c =>
            {
                c.AutoMap();
                c.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<GroupLeader>();
            BsonClassMap.RegisterClassMap<CompanyLeader>();

        }

    }
}
