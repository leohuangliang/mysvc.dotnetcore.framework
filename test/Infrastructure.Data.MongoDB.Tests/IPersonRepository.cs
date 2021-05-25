using System;
using MySvc.DotNetCore.Framework.Domain.Core;
using Xunit;

namespace Infrastructure.Data.MongoDB.Tests
{
    public interface IPersonRepository : IRepository<Person>
    {
    }

    public interface ILeaderReadOnlyRepository : IReadOnlyRepository<Leader>
    {

    }
}
