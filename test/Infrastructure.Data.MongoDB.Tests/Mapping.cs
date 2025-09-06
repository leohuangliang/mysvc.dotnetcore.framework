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
        private static bool _isMapped = false;
        private static readonly object _lock = new object();

        public static void Map()
        {
            if (_isMapped) return;

            lock (_lock)
            {
                if (_isMapped) return;

                if (!BsonClassMap.IsClassMapRegistered(typeof(Person)))
                {
                    BsonClassMap.RegisterClassMap<Person>(c =>
                    {
                        c.AutoMap();
                        c.SetIsRootClass(true);
                    });
                }

                if (!BsonClassMap.IsClassMapRegistered(typeof(Leader)))
                {
                    BsonClassMap.RegisterClassMap<Leader>(c =>
                    {
                        c.AutoMap();
                        c.SetIsRootClass(true);
                    });
                }

                if (!BsonClassMap.IsClassMapRegistered(typeof(GroupLeader)))
                    BsonClassMap.RegisterClassMap<GroupLeader>();
                if (!BsonClassMap.IsClassMapRegistered(typeof(CompanyLeader)))
                    BsonClassMap.RegisterClassMap<CompanyLeader>();

                _isMapped = true;
            }
        }

    }
}
