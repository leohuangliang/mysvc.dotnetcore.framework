using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;

namespace Infrastructure.Data.MongoDB.Tests
{
    public class PersonRepository : MongoDBRepository<Person>, IPersonRepository
    {
        public PersonRepository(IMongoDBContext context)
            : base(context)
        {
        }
    }

    public class LeaderReadOnlyRepository : ReadOnlyMongoDBRepository<Leader>, ILeaderReadOnlyRepository
    {
        public LeaderReadOnlyRepository(IMongoDBContext context, bool isBaseOnSession = false) : base(context, isBaseOnSession)
        {
        }
    }
}